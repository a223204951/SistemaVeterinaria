using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using CapaNegocio;

namespace CapaPresentacion
{
    /// <summary>
    /// FORMULARIO DE COMPRAS
    /// Registra compras a proveedores con carrito múltiple.
    /// Suma stock al confirmar. No aplica regla del 10% (solo ventas la disparan).
    /// </summary>
    public partial class FrmCompras : Form
    {
        private int _idCompraActual = -1;
        private bool _compraAbierta = false;
        private DataTable _carrito = null;

        public FrmCompras()
        {
            InitializeComponent();
        }

        private void FrmCompras_Load(object sender, EventArgs e)
        {
            CargarProveedores();
            CargarProductos("");
            InicializarCarrito();
            ActualizarEstadoBotones();
        }

        // ── Inicializar carrito local ─────────────────────────────────────────
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
            ConfigurarColumnas();
            ActualizarTotales();
        }

        private void ConfigurarColumnas()
        {
            if (dgvCarrito.Columns.Count == 0) return;
            dgvCarrito.Columns["idproducto"].Visible = false;
            dgvCarrito.Columns["producto"].HeaderText = "Producto";
            dgvCarrito.Columns["categoria"].HeaderText = "Categoría";
            dgvCarrito.Columns["cantidad"].HeaderText = "Cant.";
            dgvCarrito.Columns["precio_unit"].HeaderText = "Precio Unit.";
            dgvCarrito.Columns["subtotal"].HeaderText = "Subtotal";
            dgvCarrito.Columns["precio_unit"].DefaultCellStyle.Format = "C2";
            dgvCarrito.Columns["subtotal"].DefaultCellStyle.Format = "C2";
        }

        // ── Cargar datos iniciales ────────────────────────────────────────────
        private void CargarProveedores()
        {
            try
            {
                DataTable dt = CN_Compra.ObtenerProveedores();
                cmbProveedor.DataSource = dt;
                cmbProveedor.DisplayMember = "nombre";
                cmbProveedor.ValueMember = "idproveedor";
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
                dgvProductos.DataSource = CN_Compra.BuscarProducto(buscar);
                if (dgvProductos.Columns.Count > 0)
                {
                    dgvProductos.Columns["idproducto"].Visible = false;
                    dgvProductos.Columns["nombre"].HeaderText = "Producto";
                    dgvProductos.Columns["categoria"].HeaderText = "Categoría";
                    dgvProductos.Columns["precio_actual"].HeaderText = "Precio Actual";
                    dgvProductos.Columns["stock"].HeaderText = "Stock Actual";
                    dgvProductos.Columns["precio_actual"].DefaultCellStyle.Format = "C2";
                }
            }
            catch { }
        }

        // ── NUEVA COMPRA ──────────────────────────────────────────────────────
        private void btnNuevaCompra_Click(object sender, EventArgs e)
        {
            if (cmbProveedor.SelectedValue == null)
            {
                MessageBox.Show("⚠️ Seleccione un proveedor.",
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
                InicializarCarrito();
                lblCompraId.Text = $"Compra #  {_idCompraActual}";
                lblCompraId.ForeColor = Color.FromArgb(46, 204, 113);
                cmbProveedor.Enabled = false;
                ActualizarEstadoBotones();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message,
                    "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ── AGREGAR PRODUCTO ──────────────────────────────────────────────────
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
                MessageBox.Show("Ingrese cantidad y precio de compra válidos.",
                    "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                DataGridViewRow row = dgvProductos.CurrentRow;
                int idproducto = Convert.ToInt32(row.Cells["idproducto"].Value);
                string nombre = row.Cells["nombre"].Value.ToString();
                string categoria = row.Cells["categoria"].Value.ToString();
                int cantidad = Convert.ToInt32(nudCantidad.Value);
                decimal precioUnit = nudPrecioCompra.Value;

                string resultado = CN_Compra.AgregarProducto(
                    _idCompraActual, idproducto, cantidad, precioUnit);

                if (resultado == "OK")
                {
                    // Agregar al carrito local
                    _carrito.Rows.Add(idproducto, nombre, categoria,
                        cantidad, precioUnit, cantidad * precioUnit);
                    ActualizarTotales();
                    CargarProductos(txtBuscarProducto.Text);
                    nudCantidad.Value = 1;
                    nudPrecioCompra.Value = 1;
                }
                else
                    MessageBox.Show("❌ " + resultado,
                        "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message,
                    "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ── CONFIRMAR COMPRA ──────────────────────────────────────────────────
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
                    $"Subtotal:  ${subtotal:N2}\n" +
                    $"IVA (16%): ${iva:N2}\n" +
                    $"TOTAL:     ${total:N2}\n\n" +
                    $"El stock de los productos se actualizará.",
                    "Sistema Veterinaria", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                == DialogResult.Yes)
            {
                string resultado = CN_Compra.ConfirmarCompra(_idCompraActual, _carrito);

                if (resultado == "OK")
                {
                    MessageBox.Show(
                        $"✅ Compra #{_idCompraActual} confirmada.\n\nTotal: ${total:N2}",
                        "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    _compraAbierta = false;
                    _idCompraActual = -1;
                    InicializarCarrito();
                    lblCompraId.Text = "Sin compra activa";
                    lblCompraId.ForeColor = Color.FromArgb(149, 165, 166);
                    cmbProveedor.Enabled = true;
                    CargarProductos("");
                    ActualizarEstadoBotones();
                }
                else
                    MessageBox.Show("❌ " + resultado,
                        "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ── QUITAR DEL CARRITO (solo visual — el stock ya fue sumado en BD) ───
        private void btnQuitar_Click(object sender, EventArgs e)
        {
            if (dgvCarrito.SelectedRows.Count == 0) return;
            // Nota: en compras solo limpiamos localmente si aún no se confirmó
            int idx = dgvCarrito.CurrentRow.Index;
            _carrito.Rows.RemoveAt(idx);
            ActualizarTotales();
        }

        // ── Totales ───────────────────────────────────────────────────────────
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

        private void txtBuscarProducto_TextChanged(object sender, EventArgs e)
        {
            string txt = txtBuscarProducto.Text.Trim();
            if (txt == "Buscar producto...") return;
            CargarProductos(txt);
        }

        private void dgvProductos_DoubleClick(object sender, EventArgs e)
            => btnAgregar_Click(sender, e);
    }
}