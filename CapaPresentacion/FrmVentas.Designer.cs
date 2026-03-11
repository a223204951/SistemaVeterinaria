using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace CapaPresentacion
{
    partial class FrmVentas
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            DataGridViewCellStyle csHeader = new DataGridViewCellStyle();
            DataGridViewCellStyle csCell = new DataGridViewCellStyle();

            // ── Controles ─────────────────────────────────────────────────────
            this.lblTitulo = new Label();
            this.lblVentaId = new Label();

            // Panel izquierdo — catálogo
            this.panelCatalogo = new Panel();
            this.lblCatalogo = new Label();
            this.txtBuscarProducto = new TextBox();
            this.dgvProductos = new DataGridView();
            this.lblCantidad = new Label();
            this.nudCantidad = new NumericUpDown();
            this.btnAgregar = new Button();

            // Panel central — carrito
            this.panelCarrito = new Panel();
            this.lblCarrito = new Label();
            this.dgvCarrito = new DataGridView();

            // Panel derecho — resumen + cliente + botones
            this.panelResumen = new Panel();
            this.lblClienteLbl = new Label();
            this.cmbCliente = new ComboBox();
            this.panelTotales = new Panel();
            this.lblSubtotal = new Label();
            this.lblIva = new Label();
            this.lblTotal = new Label();
            this.btnNuevaVenta = new Button();
            this.btnQuitar = new Button();
            this.btnConfirmar = new Button();
            this.btnCancelar = new Button();
            this.lblInfoPrecio = new Label();

            this.panelCatalogo.SuspendLayout();
            ((ISupportInitialize)this.dgvProductos).BeginInit();
            ((ISupportInitialize)this.nudCantidad).BeginInit();
            this.panelCarrito.SuspendLayout();
            ((ISupportInitialize)this.dgvCarrito).BeginInit();
            this.panelResumen.SuspendLayout();
            this.panelTotales.SuspendLayout();
            this.SuspendLayout();

            // ── Estilos DataGridView compartidos ──────────────────────────────
            csHeader.BackColor = Color.FromArgb(52, 73, 94);
            csHeader.ForeColor = Color.White;
            csHeader.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            csHeader.Alignment = DataGridViewContentAlignment.MiddleLeft;
            csHeader.SelectionBackColor = Color.FromArgb(52, 73, 94);

            csCell.BackColor = Color.White;
            csCell.ForeColor = Color.FromArgb(52, 73, 94);
            csCell.Font = new Font("Segoe UI", 9F);
            csCell.SelectionBackColor = Color.FromArgb(52, 152, 219);
            csCell.SelectionForeColor = Color.White;

            // ── lblTitulo ─────────────────────────────────────────────────────
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            this.lblTitulo.ForeColor = Color.FromArgb(52, 73, 94);
            this.lblTitulo.Location = new Point(20, 15);
            this.lblTitulo.Text = "🛒 Módulo de Ventas";

            // ── lblVentaId ────────────────────────────────────────────────────
            this.lblVentaId.AutoSize = true;
            this.lblVentaId.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            this.lblVentaId.ForeColor = Color.FromArgb(149, 165, 166);
            this.lblVentaId.Location = new Point(20, 55);
            this.lblVentaId.Text = "Sin venta activa";

            // ── panelCatalogo (izquierda) ─────────────────────────────────────
            this.panelCatalogo.BackColor = Color.White;
            this.panelCatalogo.BorderStyle = BorderStyle.FixedSingle;
            this.panelCatalogo.Location = new Point(15, 85);
            this.panelCatalogo.Size = new Size(360, 490);
            this.panelCatalogo.Controls.AddRange(new Control[] {
                this.lblCatalogo, this.txtBuscarProducto,
                this.dgvProductos, this.lblCantidad, this.nudCantidad, this.btnAgregar });

            this.lblCatalogo.Text = "📦 Catálogo de Productos";
            this.lblCatalogo.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.lblCatalogo.ForeColor = Color.FromArgb(52, 73, 94);
            this.lblCatalogo.Location = new Point(10, 10);
            this.lblCatalogo.AutoSize = true;

            this.txtBuscarProducto.Font = new Font("Segoe UI", 10F);
            this.txtBuscarProducto.Location = new Point(10, 38);
            this.txtBuscarProducto.Size = new Size(335, 30);
            this.txtBuscarProducto.Text = "Buscar producto...";
            this.txtBuscarProducto.ForeColor = Color.Gray;
            this.txtBuscarProducto.GotFocus += (s, ev) => {
                if (this.txtBuscarProducto.Text == "Buscar producto...")
                {
                    this.txtBuscarProducto.Text = "";
                    this.txtBuscarProducto.ForeColor = Color.Black;
                }
            };
            this.txtBuscarProducto.LostFocus += (s, ev) => {
                if (this.txtBuscarProducto.Text == "")
                {
                    this.txtBuscarProducto.Text = "Buscar producto...";
                    this.txtBuscarProducto.ForeColor = Color.Gray;
                }
            };
            this.txtBuscarProducto.TextChanged += new System.EventHandler(this.txtBuscarProducto_TextChanged);

            this.dgvProductos.Location = new Point(10, 75);
            this.dgvProductos.Size = new Size(335, 330);
            this.dgvProductos.AllowUserToAddRows = false;
            this.dgvProductos.AllowUserToDeleteRows = false;
            this.dgvProductos.ReadOnly = true;
            this.dgvProductos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvProductos.RowHeadersVisible = false;
            this.dgvProductos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvProductos.ColumnHeadersDefaultCellStyle = csHeader;
            this.dgvProductos.DefaultCellStyle = csCell;
            this.dgvProductos.EnableHeadersVisualStyles = false;
            this.dgvProductos.ColumnHeadersHeight = 35;
            this.dgvProductos.RowTemplate.Height = 30;
            this.dgvProductos.BackgroundColor = Color.White;
            this.dgvProductos.BorderStyle = BorderStyle.None;
            this.dgvProductos.DoubleClick += new System.EventHandler(this.dgvProductos_DoubleClick);

            this.lblCantidad.Text = "Cantidad:";
            this.lblCantidad.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblCantidad.ForeColor = Color.FromArgb(52, 73, 94);
            this.lblCantidad.Location = new Point(10, 415);
            this.lblCantidad.AutoSize = true;

            this.nudCantidad.Location = new Point(10, 438);
            this.nudCantidad.Size = new Size(80, 30);
            this.nudCantidad.Minimum = 1;
            this.nudCantidad.Maximum = 9999;
            this.nudCantidad.Value = 1;
            this.nudCantidad.Font = new Font("Segoe UI", 10F);

            this.btnAgregar.Text = "➕ Agregar al Carrito";
            this.btnAgregar.Location = new Point(100, 435);
            this.btnAgregar.Size = new Size(245, 38);
            this.btnAgregar.BackColor = Color.FromArgb(52, 152, 219);
            this.btnAgregar.ForeColor = Color.White;
            this.btnAgregar.FlatStyle = FlatStyle.Flat;
            this.btnAgregar.FlatAppearance.BorderSize = 0;
            this.btnAgregar.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.btnAgregar.Cursor = Cursors.Hand;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);

            // ── panelCarrito (centro) ─────────────────────────────────────────
            this.panelCarrito.BackColor = Color.White;
            this.panelCarrito.BorderStyle = BorderStyle.FixedSingle;
            this.panelCarrito.Location = new Point(385, 85);
            this.panelCarrito.Size = new Size(370, 490);
            this.panelCarrito.Controls.AddRange(new Control[] {
                this.lblCarrito, this.dgvCarrito, this.btnQuitar });

            this.lblCarrito.Text = "🛒 Carrito de Venta";
            this.lblCarrito.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.lblCarrito.ForeColor = Color.FromArgb(52, 73, 94);
            this.lblCarrito.Location = new Point(10, 10);
            this.lblCarrito.AutoSize = true;

            this.dgvCarrito.Location = new Point(10, 38);
            this.dgvCarrito.Size = new Size(345, 395);
            this.dgvCarrito.AllowUserToAddRows = false;
            this.dgvCarrito.AllowUserToDeleteRows = false;
            this.dgvCarrito.ReadOnly = true;
            this.dgvCarrito.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvCarrito.RowHeadersVisible = false;
            this.dgvCarrito.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCarrito.ColumnHeadersDefaultCellStyle = csHeader;
            this.dgvCarrito.DefaultCellStyle = csCell;
            this.dgvCarrito.EnableHeadersVisualStyles = false;
            this.dgvCarrito.ColumnHeadersHeight = 35;
            this.dgvCarrito.RowTemplate.Height = 30;
            this.dgvCarrito.BackgroundColor = Color.White;
            this.dgvCarrito.BorderStyle = BorderStyle.None;

            this.btnQuitar.Text = "🗑️ Quitar seleccionado";
            this.btnQuitar.Location = new Point(10, 440);
            this.btnQuitar.Size = new Size(345, 38);
            this.btnQuitar.BackColor = Color.FromArgb(231, 76, 60);
            this.btnQuitar.ForeColor = Color.White;
            this.btnQuitar.FlatStyle = FlatStyle.Flat;
            this.btnQuitar.FlatAppearance.BorderSize = 0;
            this.btnQuitar.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.btnQuitar.Cursor = Cursors.Hand;
            this.btnQuitar.Click += new System.EventHandler(this.btnQuitar_Click);

            // ── panelResumen (derecha) ────────────────────────────────────────
            this.panelResumen.BackColor = Color.White;
            this.panelResumen.BorderStyle = BorderStyle.FixedSingle;
            this.panelResumen.Location = new Point(765, 85);
            this.panelResumen.Size = new Size(165, 490);
            this.panelResumen.Controls.AddRange(new Control[] {
                this.lblClienteLbl, this.cmbCliente,
                this.panelTotales,
                this.btnNuevaVenta, this.btnConfirmar, this.btnCancelar,
                this.lblInfoPrecio });

            this.lblClienteLbl.Text = "Cliente:";
            this.lblClienteLbl.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblClienteLbl.ForeColor = Color.FromArgb(52, 73, 94);
            this.lblClienteLbl.Location = new Point(10, 10);
            this.lblClienteLbl.AutoSize = true;

            this.cmbCliente.Location = new Point(10, 30);
            this.cmbCliente.Size = new Size(145, 30);
            this.cmbCliente.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbCliente.Font = new Font("Segoe UI", 9F);

            // panelTotales
            this.panelTotales.BackColor = Color.FromArgb(236, 240, 241);
            this.panelTotales.BorderStyle = BorderStyle.FixedSingle;
            this.panelTotales.Location = new Point(10, 75);
            this.panelTotales.Size = new Size(145, 100);
            this.panelTotales.Controls.AddRange(new Control[] {
                this.lblSubtotal, this.lblIva, this.lblTotal });

            this.lblSubtotal.Text = "Subtotal:  $0.00";
            this.lblSubtotal.Font = new Font("Segoe UI", 8F);
            this.lblSubtotal.ForeColor = Color.FromArgb(52, 73, 94);
            this.lblSubtotal.Location = new Point(5, 8);
            this.lblSubtotal.AutoSize = true;

            this.lblIva.Text = "IVA (16%): $0.00";
            this.lblIva.Font = new Font("Segoe UI", 8F);
            this.lblIva.ForeColor = Color.FromArgb(52, 73, 94);
            this.lblIva.Location = new Point(5, 35);
            this.lblIva.AutoSize = true;

            this.lblTotal.Text = "TOTAL:     $0.00";
            this.lblTotal.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.lblTotal.ForeColor = Color.FromArgb(52, 73, 94);
            this.lblTotal.Location = new Point(5, 65);
            this.lblTotal.AutoSize = true;

            // Botones de acción
            this.btnNuevaVenta.Text = "🆕 Nueva Venta";
            this.btnNuevaVenta.Location = new Point(10, 190);
            this.btnNuevaVenta.Size = new Size(145, 40);
            this.btnNuevaVenta.BackColor = Color.FromArgb(46, 204, 113);
            this.btnNuevaVenta.ForeColor = Color.White;
            this.btnNuevaVenta.FlatStyle = FlatStyle.Flat;
            this.btnNuevaVenta.FlatAppearance.BorderSize = 0;
            this.btnNuevaVenta.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.btnNuevaVenta.Cursor = Cursors.Hand;
            this.btnNuevaVenta.Click += new System.EventHandler(this.btnNuevaVenta_Click);

            this.btnConfirmar.Text = "✅ Confirmar Venta";
            this.btnConfirmar.Location = new Point(10, 245);
            this.btnConfirmar.Size = new Size(145, 40);
            this.btnConfirmar.BackColor = Color.FromArgb(52, 152, 219);
            this.btnConfirmar.ForeColor = Color.White;
            this.btnConfirmar.FlatStyle = FlatStyle.Flat;
            this.btnConfirmar.FlatAppearance.BorderSize = 0;
            this.btnConfirmar.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.btnConfirmar.Cursor = Cursors.Hand;
            this.btnConfirmar.Click += new System.EventHandler(this.btnConfirmar_Click);

            this.btnCancelar.Text = "✗ Cancelar Venta";
            this.btnCancelar.Location = new Point(10, 300);
            this.btnCancelar.Size = new Size(145, 40);
            this.btnCancelar.BackColor = Color.FromArgb(231, 76, 60);
            this.btnCancelar.ForeColor = Color.White;
            this.btnCancelar.FlatStyle = FlatStyle.Flat;
            this.btnCancelar.FlatAppearance.BorderSize = 0;
            this.btnCancelar.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.btnCancelar.Cursor = Cursors.Hand;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);

            this.lblInfoPrecio.Text =
                "ℹ️ Al confirmar:\n• Vendido +10%\n• Otros -10%";
            this.lblInfoPrecio.Font = new Font("Segoe UI", 8F, FontStyle.Italic);
            this.lblInfoPrecio.ForeColor = Color.FromArgb(142, 68, 173);
            this.lblInfoPrecio.Location = new Point(10, 355);
            this.lblInfoPrecio.Size = new Size(145, 60);

            // ── FrmVentas ─────────────────────────────────────────────────────
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = Color.FromArgb(236, 240, 241);
            this.ClientSize = new Size(950, 600);
            this.FormBorderStyle = FormBorderStyle.None;
            this.Name = "FrmVentas";
            this.Text = "Ventas";
            this.Controls.AddRange(new Control[] {
                this.lblTitulo, this.lblVentaId,
                this.panelCatalogo, this.panelCarrito, this.panelResumen });
            this.Load += new System.EventHandler(this.FrmVentas_Load);

            this.panelCatalogo.ResumeLayout(false);
            this.panelCatalogo.PerformLayout();
            ((ISupportInitialize)this.dgvProductos).EndInit();
            ((ISupportInitialize)this.nudCantidad).EndInit();
            this.panelCarrito.ResumeLayout(false);
            this.panelCarrito.PerformLayout();
            ((ISupportInitialize)this.dgvCarrito).EndInit();
            this.panelResumen.ResumeLayout(false);
            this.panelResumen.PerformLayout();
            this.panelTotales.ResumeLayout(false);
            this.panelTotales.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        // Declaraciones
        private Label lblTitulo, lblVentaId;
        private Panel panelCatalogo, panelCarrito, panelResumen, panelTotales;
        private Label lblCatalogo, lblCantidad, lblCarrito;
        private Label lblClienteLbl, lblSubtotal, lblIva, lblTotal, lblInfoPrecio;
        private TextBox txtBuscarProducto;
        private DataGridView dgvProductos, dgvCarrito;
        private NumericUpDown nudCantidad;
        private ComboBox cmbCliente;
        private Button btnAgregar, btnQuitar, btnNuevaVenta, btnConfirmar, btnCancelar;
    }
}