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

        public FrmCompras() { InitializeComponent(); }

        private void FrmCompras_Load(object sender, EventArgs e)
        {
            CargarProveedores();
            InicializarCarrito();
            ActualizarEstadoBotones();
        }

        // ── Carrito ───────────────────────────────────────────────────────────
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
            dgvCarrito.Columns["cantidad"].HeaderText = "Cant."; dgvCarrito.Columns["cantidad"].Width = 60;
            dgvCarrito.Columns["precio_unit"].HeaderText = "Precio"; dgvCarrito.Columns["precio_unit"].Width = 100; dgvCarrito.Columns["precio_unit"].DefaultCellStyle.Format = "C2";
            dgvCarrito.Columns["subtotal"].HeaderText = "Subtotal"; dgvCarrito.Columns["subtotal"].Width = 110; dgvCarrito.Columns["subtotal"].DefaultCellStyle.Format = "C2";
        }

        // ── Cargar datos ──────────────────────────────────────────────────────
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
                if (dgvProductos.Columns.Contains("idproducto")) dgvProductos.Columns["idproducto"].Visible = false;
                if (dgvProductos.Columns.Contains("nombre")) dgvProductos.Columns["nombre"].HeaderText = "Producto";
                if (dgvProductos.Columns.Contains("categoria")) dgvProductos.Columns["categoria"].HeaderText = "Categoría";
                if (dgvProductos.Columns.Contains("precio_actual")) { dgvProductos.Columns["precio_actual"].HeaderText = "Precio $"; dgvProductos.Columns["precio_actual"].DefaultCellStyle.Format = "C2"; dgvProductos.Columns["precio_actual"].Width = 90; }
                if (dgvProductos.Columns.Contains("stock")) { dgvProductos.Columns["stock"].HeaderText = "Stock"; dgvProductos.Columns["stock"].Width = 65; }
            }
            catch { }
        }

        // ── Nueva compra ──────────────────────────────────────────────────────
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
                { MessageBox.Show("❌ No se pudo crear la compra.", "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }

                _compraAbierta = true;
                lblCompraId.Text = $"Compra #  {_idCompraActual}";
                lblCompraId.ForeColor = Color.FromArgb(46, 204, 113);
                cmbProveedor.Enabled = false;
                InicializarCarrito();
                CargarProductos("");
                ActualizarEstadoBotones();
            }
            catch (Exception ex) { MessageBox.Show("Error: " + ex.Message, "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        // ── Selección de producto → precio automático ─────────────────────────
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

        private void nudCantidad_ValueChanged(object sender, EventArgs e) => ActualizarSubtotalPreview();
        private void nudPrecioCompra_ValueChanged(object sender, EventArgs e) => ActualizarSubtotalPreview();

        private void ActualizarSubtotalPreview()
        {
            decimal sub = nudCantidad.Value * nudPrecioCompra.Value;
            lblSubtotalPreview.Text = $"Subtotal: {sub:C2}";
            lblSubtotalPreview.ForeColor = sub > 0 ? Color.FromArgb(52, 152, 219) : Color.Gray;
        }

        // ── Agregar ───────────────────────────────────────────────────────────
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (!_compraAbierta) { MessageBox.Show("⚠️ Primero inicie una nueva compra.", "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            if (dgvProductos.SelectedRows.Count == 0) { MessageBox.Show("Seleccione un producto.", "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            if (nudCantidad.Value <= 0 || nudPrecioCompra.Value <= 0) { MessageBox.Show("Ingrese cantidad y precio válidos.", "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

            try
            {
                DataGridViewRow row = dgvProductos.CurrentRow;
                int id = Convert.ToInt32(row.Cells["idproducto"].Value);
                string nombre = row.Cells["nombre"].Value.ToString();
                string cat = row.Cells["categoria"].Value.ToString();
                int cant = Convert.ToInt32(nudCantidad.Value);
                decimal precio = nudPrecioCompra.Value;

                // Acumular si ya existe en carrito
                bool existe = false;
                foreach (DataRow cr in _carrito.Rows)
                {
                    if (Convert.ToInt32(cr["idproducto"]) == id)
                    { int nc = Convert.ToInt32(cr["cantidad"]) + cant; cr["cantidad"] = nc; cr["precio_unit"] = precio; cr["subtotal"] = nc * precio; existe = true; break; }
                }
                if (!existe) _carrito.Rows.Add(id, nombre, cat, cant, precio, cant * precio);

                string res = CN_Compra.AgregarProducto(_idCompraActual, id, cant, precio);
                if (res == "OK")
                {
                    ActualizarTotales();
                    CargarProductos(txtBuscarProducto.Text == "Buscar producto..." ? "" : txtBuscarProducto.Text);
                    nudCantidad.Value = 1;
                    ActualizarSubtotalPreview();
                }
                else
                {
                    if (!existe) _carrito.Rows.RemoveAt(_carrito.Rows.Count - 1);
                    MessageBox.Show("❌ " + res, "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex) { MessageBox.Show("Error: " + ex.Message, "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        // ── Confirmar ─────────────────────────────────────────────────────────
        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            if (_carrito == null || _carrito.Rows.Count == 0)
            { MessageBox.Show("⚠️ Agregue al menos un producto.", "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

            var (subtotal, iva, total) = CN_Compra.CalcularTotales(_carrito);
            if (MessageBox.Show(
                    $"¿Confirmar Compra #{_idCompraActual}?\n\nProveedor:  {cmbProveedor.Text}\nSubtotal:   ${subtotal:N2}\nIVA (16%):  ${iva:N2}\nTOTAL:      ${total:N2}\n\nEl stock se actualizará.",
                    "Sistema Veterinaria", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            string resultado = CN_Compra.ConfirmarCompra(_idCompraActual, _carrito);
            if (resultado == "OK")
            {
                MessageBox.Show($"✅ Compra #{_idCompraActual} confirmada.\n\nTotal: ${total:N2}", "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _compraAbierta = false; _idCompraActual = -1;
                InicializarCarrito();
                lblCompraId.Text = "Sin compra activa";
                lblCompraId.ForeColor = Color.FromArgb(149, 165, 166);
                cmbProveedor.Enabled = true;
                cmbProveedor.SelectedIndex = -1;
                CargarProductos(""); ActualizarEstadoBotones();
                lblSubtotalPreview.Text = "Subtotal: $0.00";
            }
            else MessageBox.Show("❌ " + resultado, "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        // ── Quitar ────────────────────────────────────────────────────────────
        private void btnQuitar_Click(object sender, EventArgs e)
        {
            if (dgvCarrito.SelectedRows.Count == 0) return;
            _carrito.Rows.RemoveAt(dgvCarrito.CurrentRow.Index);
            ActualizarTotales();
        }

        // ── Totales ───────────────────────────────────────────────────────────
        private void ActualizarTotales()
        {
            var (subtotal, iva, total) = CN_Compra.CalcularTotales(_carrito);
            lblSubtotal.Text = $"Subtotal:  ${subtotal:N2}";
            lblIva.Text = $"IVA (16%): ${iva:N2}";
            lblTotal.Text = $"TOTAL:     ${total:N2}";
            lblTotal.ForeColor = total > 0 ? Color.FromArgb(46, 204, 113) : Color.FromArgb(52, 73, 94);
        }

        private void ActualizarEstadoBotones()
        {
            btnAgregar.Enabled = _compraAbierta;
            btnQuitar.Enabled = _compraAbierta;
            btnConfirmar.Enabled = _compraAbierta;
        }

        private void txtBuscarProducto_TextChanged(object sender, EventArgs e)
        {
            string txt = txtBuscarProducto.Text.Trim();
            if (txt == "Buscar producto...") return;
            CargarProductos(txt);
        }

        private void dgvProductos_DoubleClick(object sender, EventArgs e) => btnAgregar_Click(sender, e);

        // ── Cancelar compra en curso ──────────────────────────────────────────
        private void btnCancelarCompra_Click(object sender, EventArgs e)
        {
            if (!_compraAbierta) return;
            if (MessageBox.Show(
                    $"¿Cancelar la Compra #{_idCompraActual}?\n\nLos productos ya agregados devolverán su stock.",
                    "Sistema Veterinaria", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
                != DialogResult.Yes) return;

            // El stock ya fue sumado al agregar cada producto con sp_insert_detalle_compra
            // Se revierten devolviendo el stock de cada item del carrito
            if (_carrito != null)
            {
                foreach (System.Data.DataRow row in _carrito.Rows)
                {
                    int idprod = Convert.ToInt32(row["idproducto"]);
                    int cant = Convert.ToInt32(row["cantidad"]);
                    // Descontar el stock que ya fue sumado
                    try
                    {
                        using (var con = new System.Data.SqlClient.SqlConnection(CapaDatos.CD_Conexion.Conn))
                        {
                            con.Open();
                            var cmd = new System.Data.SqlClient.SqlCommand(
                                "UPDATE producto SET stock = stock - @cant WHERE idproducto = @id", con);
                            cmd.Parameters.AddWithValue("@cant", cant);
                            cmd.Parameters.AddWithValue("@id", idprod);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    catch { }
                }
            }

            _compraAbierta = false; _idCompraActual = -1;
            InicializarCarrito();
            lblCompraId.Text = "Sin compra activa";
            lblCompraId.ForeColor = Color.FromArgb(149, 165, 166);
            cmbProveedor.Enabled = true;
            cmbProveedor.SelectedIndex = -1;
            CargarProductos(""); ActualizarEstadoBotones();
            lblSubtotalPreview.Text = "Subtotal: $0.00";
            MessageBox.Show("✅ Compra cancelada.", "Sistema Veterinaria",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}