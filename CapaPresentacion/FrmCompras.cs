using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using CapaNegocio;

namespace CapaPresentacion
{
    public partial class FrmCompras : Form
    {
        private int _idCompraActual = -1;
        private bool _compraAbierta = false;
        private DataTable _carrito = null;

        // Campos para modo automático (desde alerta de stock bajo)
        private int _idProveedorAutomatic = -1;
        private DataTable _productosAutomatic = null;   // columnas: idproducto, nombre, stock, idproveedor
        private bool _modoAutomatico = false;

        // ── Constructor normal ────────────────────────────────────────────────
        public FrmCompras() { InitializeComponent(); }

        // ── Constructor desde alerta de stock bajo ────────────────────────────
        public FrmCompras(int idproveedor, DataTable productosStockBajo)
        {
            InitializeComponent();
            _idProveedorAutomatic = idproveedor;
            _productosAutomatic = productosStockBajo;
            _modoAutomatico = true;
        }

        // =====================================================================
        // LOAD
        // =====================================================================
        private void FrmCompras_Load(object sender, EventArgs e)
        {
            CargarProveedores();
            InicializarCarrito();
            ActualizarEstadoBotones();

            if (_modoAutomatico && _idProveedorAutomatic > 0 && _productosAutomatic != null)
                IniciarCompraAutomatica();
        }

        // =====================================================================
        // COMPRA AUTOMÁTICA
        // =====================================================================
        private void IniciarCompraAutomatica()
        {
            try
            {
                // 1. Seleccionar proveedor en el ComboBox
                for (int i = 0; i < cmbProveedor.Items.Count; i++)
                {
                    DataRowView drv = cmbProveedor.Items[i] as DataRowView;
                    if (drv != null &&
                        Convert.ToInt32(drv["idproveedor"]) == _idProveedorAutomatic)
                    {
                        cmbProveedor.SelectedIndex = i;
                        break;
                    }
                }

                if (cmbProveedor.SelectedValue == null || cmbProveedor.SelectedIndex < 0)
                {
                    MessageBox.Show(
                        "⚠️ No se encontró el proveedor. Seleccione uno manualmente.",
                        "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 2. Crear la compra en BD
                _idCompraActual = CN_Compra.CrearCompra(
                    Convert.ToInt32(cmbProveedor.SelectedValue),
                    FrmLogin.UsuarioActual);

                if (_idCompraActual <= 0)
                {
                    MessageBox.Show(
                        "❌ No se pudo crear la compra automática.\n\n" +
                        "Verifique que el usuario de sesión esté activo en la BD.",
                        "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                _compraAbierta = true;
                lblCompraId.Text = $"Compra #  {_idCompraActual}";
                lblCompraId.ForeColor = Color.FromArgb(46, 204, 113);
                cmbProveedor.Enabled = false;

                // 3. Obtener precios actuales directamente desde la BD
                //    (el grid aún no está cargado en el Load)
                const int STOCK_OBJETIVO = 100;
                DataTable todosProductos = CN_Compra.BuscarProducto("");

                int productosAgregados = 0;

                foreach (DataRow prod in _productosAutomatic.Rows)
                {
                    int idproducto = Convert.ToInt32(prod["idproducto"]);
                    string nombreProd = prod["nombre"].ToString();
                    int stockActual = Convert.ToInt32(prod["stock"]);
                    int cantidadSugerida = Math.Max(1, STOCK_OBJETIVO - stockActual);

                    // Buscar precio actual
                    decimal precioUnit = 1m;
                    string catNombre = "";
                    foreach (DataRow r in todosProductos.Rows)
                    {
                        if (Convert.ToInt32(r["idproducto"]) == idproducto)
                        {
                            if (r["precio_actual"] != DBNull.Value)
                                precioUnit = Convert.ToDecimal(r["precio_actual"]);
                            catNombre = r["categoria"]?.ToString() ?? "";
                            break;
                        }
                    }

                    // Agregar a BD
                    string res = CN_Compra.AgregarProducto(
                        _idCompraActual, idproducto, cantidadSugerida, precioUnit);

                    if (res == "OK")
                    {
                        _carrito.Rows.Add(
                            idproducto, nombreProd, catNombre,
                            cantidadSugerida, precioUnit,
                            cantidadSugerida * precioUnit);
                        productosAgregados++;
                    }
                }

                // 4. Cargar grid de productos y actualizar UI
                CargarProductos("");
                ActualizarTotales();
                ActualizarEstadoBotones();

                if (productosAgregados == 0)
                {
                    MessageBox.Show(
                        "⚠️ No se pudo agregar ningún producto.\n" +
                        "Verifique que los productos estén activos.",
                        "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var (sub, iva, tot) = CN_Compra.CalcularTotales(_carrito);
                MessageBox.Show(
                    $"✅ Compra automática creada con {productosAgregados} producto(s).\n\n" +
                    $"Proveedor:  {cmbProveedor.Text}\n" +
                    $"Subtotal:   ${sub:N2}\n" +
                    $"IVA (16%):  ${iva:N2}\n" +
                    $"TOTAL:      ${tot:N2}\n\n" +
                    "Puede ajustar cantidades o precios antes de confirmar.\n" +
                    "Cuando esté listo, haga clic en ✔️ Confirmar Compra.",
                    "Compra Automática — Stock Bajo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al iniciar compra automática: " + ex.Message,
                    "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // =====================================================================
        // CARRITO
        // =====================================================================
        private void InicializarCarrito()
        {
            _carrito = new DataTable("Carrito");
            _carrito.Columns.Add("idproducto", typeof(int));
            _carrito.Columns.Add("producto", typeof(string));
            _carrito.Columns.Add("categoria", typeof(string));
            _carrito.Columns.Add("cantidad", typeof(int));
            _carrito.Columns.Add("precio_unit", typeof(decimal));
            _carrito.Columns.Add("subtotal", typeof(decimal));
            dgvCarrito.DataSource = _carrito;
            ConfigurarColumnasCarrito();
            ActualizarTotales();
        }

        private void ConfigurarColumnasCarrito()
        {
            if (dgvCarrito.Columns.Count == 0) return;
            dgvCarrito.Columns["idproducto"].Visible = false;
            dgvCarrito.Columns["producto"].HeaderText = "Producto";
            dgvCarrito.Columns["categoria"].HeaderText = "Categoría";
            dgvCarrito.Columns["cantidad"].HeaderText = "Cant.";
            dgvCarrito.Columns["cantidad"].Width = 60;
            dgvCarrito.Columns["precio_unit"].HeaderText = "Precio";
            dgvCarrito.Columns["precio_unit"].Width = 100;
            dgvCarrito.Columns["precio_unit"].DefaultCellStyle.Format = "C2";
            dgvCarrito.Columns["subtotal"].HeaderText = "Subtotal";
            dgvCarrito.Columns["subtotal"].Width = 110;
            dgvCarrito.Columns["subtotal"].DefaultCellStyle.Format = "C2";
        }

        // =====================================================================
        // CARGAR DATOS
        // =====================================================================
        private void CargarProveedores()
        {
            try
            {
                DataTable dt = CN_Compra.ObtenerProveedores();
                cmbProveedor.DataSource = dt;
                cmbProveedor.DisplayMember = "nombre";
                cmbProveedor.ValueMember = "idproveedor";
                cmbProveedor.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar proveedores: " + ex.Message,
                    "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarProductos(string buscar)
        {
            try
            {
                DataTable dt;
                if (_compraAbierta && cmbProveedor.SelectedValue != null)
                    dt = CN_Compra.BuscarProductoPorProveedor(
                            Convert.ToInt32(cmbProveedor.SelectedValue), buscar);
                else
                    dt = CN_Compra.BuscarProducto(buscar);

                dgvProductos.DataSource = dt;
                if (dgvProductos.Columns.Count == 0) return;
                if (dgvProductos.Columns.Contains("idproducto"))
                    dgvProductos.Columns["idproducto"].Visible = false;
                if (dgvProductos.Columns.Contains("nombre"))
                    dgvProductos.Columns["nombre"].HeaderText = "Producto";
                if (dgvProductos.Columns.Contains("categoria"))
                    dgvProductos.Columns["categoria"].HeaderText = "Categoría";
                if (dgvProductos.Columns.Contains("precio_actual"))
                {
                    dgvProductos.Columns["precio_actual"].HeaderText = "Precio $";
                    dgvProductos.Columns["precio_actual"].DefaultCellStyle.Format = "C2";
                    dgvProductos.Columns["precio_actual"].Width = 90;
                }
                if (dgvProductos.Columns.Contains("stock"))
                {
                    dgvProductos.Columns["stock"].HeaderText = "Stock";
                    dgvProductos.Columns["stock"].Width = 65;
                }
            }
            catch { }
        }

        // =====================================================================
        // NUEVA COMPRA (manual)
        // =====================================================================
        private void btnNuevaCompra_Click(object sender, EventArgs e)
        {
            if (cmbProveedor.SelectedValue == null || cmbProveedor.SelectedIndex < 0)
            {
                MessageBox.Show("⚠️ Seleccione un proveedor antes de iniciar la compra.",
                    "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {

                int idproveedor = Convert.ToInt32(cmbProveedor.SelectedValue);
                _idCompraActual = CN_Compra.CrearCompra(idproveedor, FrmLogin.UsuarioActual);
                if (_idCompraActual <= 0)
                {
                    MessageBox.Show("❌ No se pudo crear la compra.",
                        "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                _compraAbierta = true;
                lblCompraId.Text = $"Compra #  {_idCompraActual}";
                lblCompraId.ForeColor = Color.FromArgb(46, 204, 113);
                cmbProveedor.Enabled = false;
                InicializarCarrito();
                CargarProductos("");
                ActualizarEstadoBotones();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message,
                    "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // =====================================================================
        // SELECCIÓN DE PRODUCTO → precio automático
        // =====================================================================
        private void dgvProductos_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvProductos.SelectedRows.Count == 0) return;
            if (!dgvProductos.Columns.Contains("precio_actual")) return;
            object v = dgvProductos.CurrentRow.Cells["precio_actual"].Value;
            if (v != null && v != DBNull.Value)
            {
                decimal p = Convert.ToDecimal(v);
                nudPrecioCompra.Value = p > 0 ? p : 1;
                ActualizarSubtotalPreview();
            }
        }

        private void nudCantidad_ValueChanged(object sender, EventArgs e)
            => ActualizarSubtotalPreview();

        private void nudPrecioCompra_ValueChanged(object sender, EventArgs e)
            => ActualizarSubtotalPreview();

        private void ActualizarSubtotalPreview()
        {
            decimal sub = nudCantidad.Value * nudPrecioCompra.Value;
            lblSubtotalPreview.Text = $"Subtotal: {sub:C2}";
            lblSubtotalPreview.ForeColor = sub > 0
                ? Color.FromArgb(52, 152, 219)
                : Color.Gray;
        }

        // =====================================================================
        // AGREGAR PRODUCTO
        // =====================================================================
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (!_compraAbierta)
            {
                MessageBox.Show("⚠️ Primero inicie una nueva compra.",
                    "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (dgvProductos.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione un producto.",
                    "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (nudCantidad.Value <= 0 || nudPrecioCompra.Value <= 0)
            {
                MessageBox.Show("Ingrese cantidad y precio válidos.",
                    "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                DataGridViewRow row = dgvProductos.CurrentRow;
                int id = Convert.ToInt32(row.Cells["idproducto"].Value);
                string nombre = row.Cells["nombre"].Value.ToString();
                string cat = row.Cells["categoria"].Value.ToString();
                int cant = Convert.ToInt32(nudCantidad.Value);
                decimal precio = nudPrecioCompra.Value;

                bool existe = false;
                foreach (DataRow cr in _carrito.Rows)
                {
                    if (Convert.ToInt32(cr["idproducto"]) == id)
                    {
                        int nc = Convert.ToInt32(cr["cantidad"]) + cant;
                        cr["cantidad"] = nc;
                        cr["precio_unit"] = precio;
                        cr["subtotal"] = nc * precio;
                        existe = true;
                        break;
                    }
                }
                if (!existe)
                    _carrito.Rows.Add(id, nombre, cat, cant, precio, cant * precio);

                string res = CN_Compra.AgregarProducto(_idCompraActual, id, cant, precio);
                if (res == "OK")
                {
                    ActualizarTotales();
                    CargarProductos(
                        txtBuscarProducto.Text == "Buscar producto..." ? "" : txtBuscarProducto.Text);
                    nudCantidad.Value = 1;
                    ActualizarSubtotalPreview();
                }
                else
                {
                    if (!existe) _carrito.Rows.RemoveAt(_carrito.Rows.Count - 1);
                    MessageBox.Show("❌ " + res,
                        "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message,
                    "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // =====================================================================
        // CONFIRMAR COMPRA
        // =====================================================================
        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            if (_carrito == null || _carrito.Rows.Count == 0)
            {
                MessageBox.Show("⚠️ Agregue al menos un producto.",
                    "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var (subtotal, iva, total) = CN_Compra.CalcularTotales(_carrito);
            if (MessageBox.Show(
                    $"¿Confirmar Compra #{_idCompraActual}?\n\n" +
                    $"Proveedor:  {cmbProveedor.Text}\n" +
                    $"Subtotal:   ${subtotal:N2}\n" +
                    $"IVA (16%):  ${iva:N2}\n" +
                    $"TOTAL:      ${total:N2}\n\n" +
                    "El stock se actualizará y la compra quedará en el historial.",
                    "Sistema Veterinaria",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            string resultado = CN_Compra.ConfirmarCompra(_idCompraActual, _carrito);
            if (resultado == "OK")
            {
                MessageBox.Show(
                    $"✅ Compra #{_idCompraActual} confirmada.\n\nTotal: ${total:N2}\n\n" +
                    "El inventario fue actualizado y la compra se guardó en el historial.",
                    "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ResetearFormulario();
            }
            else
                MessageBox.Show("❌ " + resultado,
                    "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        // =====================================================================
        // QUITAR DEL CARRITO
        // =====================================================================
        private void btnQuitar_Click(object sender, EventArgs e)
        {
            if (dgvCarrito.SelectedRows.Count == 0) return;
            _carrito.Rows.RemoveAt(dgvCarrito.CurrentRow.Index);
            ActualizarTotales();
        }

        // =====================================================================
        // TOTALES
        // =====================================================================
        private void ActualizarTotales()
        {
            var (subtotal, iva, total) = CN_Compra.CalcularTotales(_carrito);
            lblSubtotal.Text = $"Subtotal:  ${subtotal:N2}";
            lblIva.Text = $"IVA (16%): ${iva:N2}";
            lblTotal.Text = $"TOTAL:     ${total:N2}";
            lblTotal.ForeColor = total > 0
                ? Color.FromArgb(46, 204, 113)
                : Color.FromArgb(52, 73, 94);
        }

        private void ActualizarEstadoBotones()
        {
            btnAgregar.Enabled = _compraAbierta;
            btnQuitar.Enabled = _compraAbierta;
            btnConfirmar.Enabled = _compraAbierta;
        }

        // =====================================================================
        // BÚSQUEDA
        // =====================================================================
        private void txtBuscarProducto_TextChanged(object sender, EventArgs e)
        {
            string txt = txtBuscarProducto.Text.Trim();
            if (txt == "Buscar producto...") return;
            CargarProductos(txt);
        }

        private void dgvProductos_DoubleClick(object sender, EventArgs e)
            => btnAgregar_Click(sender, e);

        // =====================================================================
        // CANCELAR COMPRA EN CURSO
        // =====================================================================
        private void btnCancelarCompra_Click(object sender, EventArgs e)
        {
            if (!_compraAbierta) return;
            if (MessageBox.Show(
                    $"¿Cancelar la Compra #{_idCompraActual}?\n\n" +
                    "Los productos ya agregados devolverán su stock.",
                    "Sistema Veterinaria",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;

            // ✅ CORRECCIÓN: revertir stock usando SQL correcto (restar lo que se sumó)
            if (_carrito != null)
            {
                foreach (DataRow row in _carrito.Rows)
                {
                    int idprod = Convert.ToInt32(row["idproducto"]);
                    int cant = Convert.ToInt32(row["cantidad"]);
                    try
                    {
                        using (var con = new System.Data.SqlClient.SqlConnection(
                            CapaDatos.CD_Conexion.Conn))
                        {
                            con.Open();
                            // sp_insert_detalle_compra sumó el stock → hay que restarlo
                            var cmd = new System.Data.SqlClient.SqlCommand(
                                "UPDATE producto SET stock = stock - @cant WHERE idproducto = @id",
                                con);
                            cmd.Parameters.AddWithValue("@cant", cant);
                            cmd.Parameters.AddWithValue("@id", idprod);
                            cmd.ExecuteNonQuery();

                            // Marcar la compra como CANCELADA en BD
                            var cmd2 = new System.Data.SqlClient.SqlCommand(
                                "UPDATE compra SET estado = 'CANCELADA' WHERE idcompra = @idcompra",
                                con);
                            cmd2.Parameters.AddWithValue("@idcompra", _idCompraActual);
                            cmd2.ExecuteNonQuery();
                        }
                    }
                    catch { }
                }
            }

            ResetearFormulario();
            MessageBox.Show("✅ Compra cancelada.",
                "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // =====================================================================
        // RESETEAR FORMULARIO
        // =====================================================================
        private void ResetearFormulario()
        {
            _compraAbierta = false;
            _idCompraActual = -1;
            _modoAutomatico = false;

            InicializarCarrito();
            lblCompraId.Text = "Sin compra activa";
            lblCompraId.ForeColor = Color.FromArgb(149, 165, 166);
            cmbProveedor.Enabled = true;
            cmbProveedor.SelectedIndex = -1;
            CargarProductos("");
            ActualizarEstadoBotones();
            lblSubtotalPreview.Text = "Subtotal: $0.00";
        }
    }
}