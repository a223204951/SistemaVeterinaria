using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using CapaNegocio;

namespace CapaPresentacion
{
    /// <summary>
    /// FORMULARIO DE VENTAS
    /// Permite registrar ventas con carrito múltiple.
    /// Al confirmar aplica automáticamente la regla del 10%.
    /// </summary>
    public partial class FrmVentas : Form
    {
        // ── Estado de la venta activa ─────────────────────────────────────────
        private int _idVentaActual = -1;
        private bool _ventaAbierta = false;
        private DataTable _carrito = null;

        public FrmVentas()
        {
            InitializeComponent();
        }

        // ── LOAD ─────────────────────────────────────────────────────────────
        private void FrmVentas_Load(object sender, EventArgs e)
        {
            CargarClientes();
            CargarProductos("");
            InicializarCarrito();
            ActualizarEstadoBotones();
        }

        // ── Inicializar DataTable del carrito (vista local) ───────────────────
        private void InicializarCarrito()
        {
            _carrito = new DataTable("Carrito");
            _carrito.Columns.Add("iddetalle", typeof(int));
            _carrito.Columns.Add("idproducto", typeof(int));
            _carrito.Columns.Add("producto", typeof(string));
            _carrito.Columns.Add("categoria", typeof(string));
            _carrito.Columns.Add("cantidad", typeof(int));
            _carrito.Columns.Add("precio_unit", typeof(decimal));
            _carrito.Columns.Add("subtotal", typeof(decimal));

            dgvCarrito.DataSource = _carrito;
            ConfigurarColumnaCarrito();
            ActualizarTotales();
        }

        private void ConfigurarColumnaCarrito()
        {
            if (dgvCarrito.Columns.Count == 0) return;

            dgvCarrito.Columns["iddetalle"].Visible = false;
            dgvCarrito.Columns["idproducto"].Visible = false;

            dgvCarrito.Columns["producto"].HeaderText = "Producto";
            dgvCarrito.Columns["categoria"].HeaderText = "Categoría";
            dgvCarrito.Columns["cantidad"].HeaderText = "Cant.";
            dgvCarrito.Columns["precio_unit"].HeaderText = "Precio Unit.";
            dgvCarrito.Columns["subtotal"].HeaderText = "Subtotal";

            dgvCarrito.Columns["precio_unit"].DefaultCellStyle.Format = "C2";
            dgvCarrito.Columns["subtotal"].DefaultCellStyle.Format = "C2";
            dgvCarrito.Columns["cantidad"].Width = 60;
            dgvCarrito.Columns["precio_unit"].Width = 100;
            dgvCarrito.Columns["subtotal"].Width = 110;
        }

        // ── Cargar clientes en ComboBox ───────────────────────────────────────
        private void CargarClientes()
        {
            try
            {
                DataTable clientes = CN_Venta.ObtenerClientes();
                // Agregar opción "Cliente General"
                DataRow general = clientes.NewRow();
                general["idcliente"] = 0;
                general["nombre"] = "— Cliente General —";
                clientes.Rows.InsertAt(general, 0);

                cmbCliente.DataSource = clientes;
                cmbCliente.DisplayMember = "nombre";
                cmbCliente.ValueMember = "idcliente";
                cmbCliente.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar clientes: " + ex.Message,
                    "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ── Cargar catálogo de productos ──────────────────────────────────────
        private void CargarProductos(string buscar)
        {
            try
            {
                dgvProductos.DataSource = CN_Venta.BuscarProducto(buscar);
                ConfigurarColumnaProductos();
            }
            catch { }
        }

        private void ConfigurarColumnaProductos()
        {
            if (dgvProductos.Columns.Count == 0) return;
            dgvProductos.Columns["idproducto"].Visible = false;
            dgvProductos.Columns["nombre"].HeaderText = "Producto";
            dgvProductos.Columns["categoria"].HeaderText = "Categoría";
            dgvProductos.Columns["precio"].HeaderText = "Precio";
            dgvProductos.Columns["stock"].HeaderText = "Stock";
            dgvProductos.Columns["precio"].DefaultCellStyle.Format = "C2";
        }

        // ── NUEVA VENTA ───────────────────────────────────────────────────────
        private void btnNuevaVenta_Click(object sender, EventArgs e)
        {
            if (_ventaAbierta)
            {
                if (MessageBox.Show("Hay una venta en proceso. ¿Desea cancelarla y abrir una nueva?",
                        "Sistema Veterinaria", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    == DialogResult.Yes)
                {
                    CN_Venta.CancelarVenta(_idVentaActual);
                    _ventaAbierta = false;
                }
                else return;
            }

            try
            {
                int idcliente = Convert.ToInt32(cmbCliente.SelectedValue);
                _idVentaActual = CN_Venta.CrearVenta(idcliente, FrmLogin.UsuarioActual);

                if (_idVentaActual <= 0)
                {
                    MessageBox.Show("❌ No se pudo crear la venta. Verifique que el usuario esté activo.",
                        "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                _ventaAbierta = true;
                InicializarCarrito();
                lblVentaId.Text = $"Venta #  {_idVentaActual}";
                lblVentaId.ForeColor = Color.FromArgb(46, 204, 113);
                ActualizarEstadoBotones();

                MessageBox.Show($"✅ Venta #{_idVentaActual} iniciada.\nAgregue productos al carrito.",
                    "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message,
                    "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ── AGREGAR PRODUCTO AL CARRITO ───────────────────────────────────────
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (!_ventaAbierta)
            {
                MessageBox.Show("⚠️ Primero inicie una nueva venta.",
                    "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dgvProductos.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione un producto del catálogo.",
                    "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int cantidad = Convert.ToInt32(nudCantidad.Value);
            if (cantidad <= 0)
            {
                MessageBox.Show("La cantidad debe ser mayor a 0.",
                    "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                DataGridViewRow row = dgvProductos.CurrentRow;
                int idproducto = Convert.ToInt32(row.Cells["idproducto"].Value);
                string nombreProducto = row.Cells["nombre"].Value.ToString();
                string categoriaP = row.Cells["categoria"].Value.ToString();
                decimal precioUnit = Convert.ToDecimal(row.Cells["precio"].Value);
                int stockDisponible = Convert.ToInt32(row.Cells["stock"].Value);

                if (cantidad > stockDisponible)
                {
                    MessageBox.Show($"⚠️ Stock insuficiente.\nDisponible: {stockDisponible} unidades.",
                        "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Llamar a la capa de negocio
                string resultado = CN_Venta.AgregarProducto(_idVentaActual, idproducto, cantidad);

                if (resultado == "OK")
                {
                    // Refrescar carrito desde BD
                    RefrescarCarrito();
                    nudCantidad.Value = 1;
                    CargarProductos(txtBuscarProducto.Text); // actualizar stock en catálogo
                }
                else
                {
                    MessageBox.Show("❌ " + resultado,
                        "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message,
                    "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ── QUITAR PRODUCTO DEL CARRITO ───────────────────────────────────────
        private void btnQuitar_Click(object sender, EventArgs e)
        {
            if (dgvCarrito.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione un producto del carrito para quitarlo.",
                    "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string producto = dgvCarrito.CurrentRow.Cells["producto"].Value.ToString();

            if (MessageBox.Show($"¿Quitar '{producto}' del carrito?",
                    "Sistema Veterinaria", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                == DialogResult.Yes)
            {
                int iddetalle = Convert.ToInt32(dgvCarrito.CurrentRow.Cells["iddetalle"].Value);
                string resultado = CN_Venta.QuitarProducto(iddetalle);

                if (resultado == "OK")
                {
                    RefrescarCarrito();
                    CargarProductos(txtBuscarProducto.Text);
                }
                else
                    MessageBox.Show("❌ " + resultado,
                        "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ── CONFIRMAR VENTA ───────────────────────────────────────────────────
        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            if (!_ventaAbierta || _idVentaActual <= 0)
            {
                MessageBox.Show("No hay venta activa.",
                    "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataTable detalle = CN_Venta.ObtenerDetalle(_idVentaActual);
            if (detalle == null || detalle.Rows.Count == 0)
            {
                MessageBox.Show("⚠️ Agregue al menos un producto antes de confirmar.",
                    "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var (subtotal, iva, total) = CN_Venta.CalcularTotales(detalle);

            if (MessageBox.Show(
                    $"¿Confirmar Venta #{_idVentaActual}?\n\n" +
                    $"Subtotal:  ${subtotal:N2}\n" +
                    $"IVA (16%): ${iva:N2}\n" +
                    $"TOTAL:     ${total:N2}\n\n" +
                    $"⚠️ Se aplicará el ajuste de precios del 10% automáticamente.",
                    "Sistema Veterinaria", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                == DialogResult.Yes)
            {
                try
                {
                    btnConfirmar.Enabled = false;
                    btnConfirmar.Text = "⏳ Procesando...";

                    string resultado = CN_Venta.ConfirmarVenta(_idVentaActual, detalle);

                    if (resultado == "OK")
                    {
                        MessageBox.Show(
                            $"✅ Venta #{_idVentaActual} confirmada exitosamente.\n\n" +
                            $"Total cobrado: ${total:N2}\n\n" +
                            $"📊 Se aplicó ajuste de precios:\n" +
                            $"  • Productos vendidos: +10% precio\n" +
                            $"  • Otros productos activos: -10% precio",
                            "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Limpiar estado
                        _ventaAbierta = false;
                        _idVentaActual = -1;
                        InicializarCarrito();
                        lblVentaId.Text = "Sin venta activa";
                        lblVentaId.ForeColor = Color.FromArgb(149, 165, 166);
                        cmbCliente.SelectedIndex = 0;
                        CargarProductos("");
                        ActualizarEstadoBotones();
                    }
                    else
                    {
                        MessageBox.Show("❌ " + resultado,
                            "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                finally
                {
                    btnConfirmar.Enabled = true;
                    btnConfirmar.Text = "✅ Confirmar Venta";
                }
            }
        }

        // ── CANCELAR VENTA ────────────────────────────────────────────────────
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (!_ventaAbierta || _idVentaActual <= 0)
            {
                MessageBox.Show("No hay venta activa para cancelar.",
                    "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show($"¿Cancelar Venta #{_idVentaActual}?\n\nSe devolverá el stock de todos los productos.",
                    "Sistema Veterinaria", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                == DialogResult.Yes)
            {
                string resultado = CN_Venta.CancelarVenta(_idVentaActual);

                if (resultado == "OK")
                {
                    _ventaAbierta = false;
                    _idVentaActual = -1;
                    InicializarCarrito();
                    lblVentaId.Text = "Sin venta activa";
                    lblVentaId.ForeColor = Color.FromArgb(149, 165, 166);
                    CargarProductos("");
                    ActualizarEstadoBotones();

                    MessageBox.Show("✅ Venta cancelada. Stock restaurado.",
                        "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    MessageBox.Show("❌ " + resultado,
                        "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ── Refrescar carrito desde BD ────────────────────────────────────────
        private void RefrescarCarrito()
        {
            try
            {
                DataTable detalleBD = CN_Venta.ObtenerDetalle(_idVentaActual);
                _carrito.Rows.Clear();

                foreach (DataRow row in detalleBD.Rows)
                {
                    _carrito.Rows.Add(
                        row["iddetalle"],
                        row["idproducto"],
                        row["producto"],
                        row["categoria"],
                        row["cantidad"],
                        row["precio_unit"],
                        row["subtotal"]
                    );
                }

                ActualizarTotales();
            }
            catch { }
        }

        // ── Actualizar panel de totales ───────────────────────────────────────
        private void ActualizarTotales()
        {
            var (subtotal, iva, total) = CN_Venta.CalcularTotales(_carrito);

            lblSubtotal.Text = $"Subtotal:  ${subtotal:N2}";
            lblIva.Text = $"IVA (16%): ${iva:N2}";
            lblTotal.Text = $"TOTAL:     ${total:N2}";

            lblTotal.ForeColor = total > 0
                ? Color.FromArgb(46, 204, 113)
                : Color.FromArgb(52, 73, 94);
        }

        // ── Habilitar / deshabilitar botones según estado ─────────────────────
        private void ActualizarEstadoBotones()
        {
            btnAgregar.Enabled = _ventaAbierta;
            btnQuitar.Enabled = _ventaAbierta;
            btnConfirmar.Enabled = _ventaAbierta;
            btnCancelar.Enabled = _ventaAbierta;
            cmbCliente.Enabled = !_ventaAbierta;
        }

        // ── Búsqueda en tiempo real del catálogo ──────────────────────────────
        private void txtBuscarProducto_TextChanged(object sender, EventArgs e)
        {
            string txt = txtBuscarProducto.Text.Trim();
            if (txt == "Buscar producto...") return;
            CargarProductos(txt);
        }

        // ── Doble click en catálogo = agregar ─────────────────────────────────
        private void dgvProductos_DoubleClick(object sender, EventArgs e)
            => btnAgregar_Click(sender, e);
    }
}