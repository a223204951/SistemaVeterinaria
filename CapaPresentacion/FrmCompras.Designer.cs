using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace CapaPresentacion
{
    partial class FrmCompras
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
            DataGridViewCellStyle csH = new DataGridViewCellStyle();
            DataGridViewCellStyle csC = new DataGridViewCellStyle();

            csH.BackColor = Color.FromArgb(52, 73, 94); csH.ForeColor = Color.White;
            csH.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            csH.SelectionBackColor = Color.FromArgb(52, 73, 94);
            csH.Alignment = DataGridViewContentAlignment.MiddleLeft;

            csC.BackColor = Color.White; csC.ForeColor = Color.FromArgb(52, 73, 94);
            csC.Font = new Font("Segoe UI", 9F);
            csC.SelectionBackColor = Color.FromArgb(52, 152, 219);
            csC.SelectionForeColor = Color.White;

            this.lblTitulo = new Label();
            this.lblCompraId = new Label();
            this.panelCatalogo = new Panel();
            this.lblCatalogo = new Label();
            this.txtBuscarProducto = new TextBox();
            this.dgvProductos = new DataGridView();
            this.lblCantidad = new Label();
            this.nudCantidad = new NumericUpDown();
            this.lblPrecioCompra = new Label();
            this.nudPrecioCompra = new NumericUpDown();
            this.btnAgregar = new Button();
            this.panelCarrito = new Panel();
            this.lblCarritoTitulo = new Label();
            this.dgvCarrito = new DataGridView();
            this.btnQuitar = new Button();
            this.panelResumen = new Panel();
            this.lblProveedorLbl = new Label();
            this.cmbProveedor = new ComboBox();
            this.panelTotales = new Panel();
            this.lblSubtotal = new Label();
            this.lblIva = new Label();
            this.lblTotal = new Label();
            this.btnNuevaCompra = new Button();
            this.btnConfirmar = new Button();

            this.panelCatalogo.SuspendLayout();
            ((ISupportInitialize)this.dgvProductos).BeginInit();
            ((ISupportInitialize)this.nudCantidad).BeginInit();
            ((ISupportInitialize)this.nudPrecioCompra).BeginInit();
            this.panelCarrito.SuspendLayout();
            ((ISupportInitialize)this.dgvCarrito).BeginInit();
            this.panelResumen.SuspendLayout();
            this.panelTotales.SuspendLayout();
            this.SuspendLayout();

            // lblTitulo
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            this.lblTitulo.ForeColor = Color.FromArgb(52, 73, 94);
            this.lblTitulo.Location = new Point(20, 15);
            this.lblTitulo.Text = "📥 Módulo de Compras";

            // lblCompraId
            this.lblCompraId.AutoSize = true;
            this.lblCompraId.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            this.lblCompraId.ForeColor = Color.FromArgb(149, 165, 166);
            this.lblCompraId.Location = new Point(20, 55);
            this.lblCompraId.Text = "Sin compra activa";

            // panelCatalogo
            this.panelCatalogo.BackColor = Color.White;
            this.panelCatalogo.BorderStyle = BorderStyle.FixedSingle;
            this.panelCatalogo.Location = new Point(15, 85);
            this.panelCatalogo.Size = new Size(370, 490);
            this.panelCatalogo.Controls.AddRange(new Control[] {
                this.lblCatalogo, this.txtBuscarProducto, this.dgvProductos,
                this.lblCantidad, this.nudCantidad,
                this.lblPrecioCompra, this.nudPrecioCompra, this.btnAgregar });

            this.lblCatalogo.Text = "📦 Catálogo de Productos";
            this.lblCatalogo.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.lblCatalogo.ForeColor = Color.FromArgb(52, 73, 94);
            this.lblCatalogo.Location = new Point(10, 10); this.lblCatalogo.AutoSize = true;

            this.txtBuscarProducto.Font = new Font("Segoe UI", 10F);
            this.txtBuscarProducto.Location = new Point(10, 38);
            this.txtBuscarProducto.Size = new Size(345, 30);
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
            this.dgvProductos.Size = new Size(345, 300);
            this.dgvProductos.AllowUserToAddRows = false; this.dgvProductos.AllowUserToDeleteRows = false;
            this.dgvProductos.ReadOnly = true; this.dgvProductos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvProductos.RowHeadersVisible = false; this.dgvProductos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvProductos.ColumnHeadersDefaultCellStyle = csH; this.dgvProductos.DefaultCellStyle = csC;
            this.dgvProductos.EnableHeadersVisualStyles = false; this.dgvProductos.ColumnHeadersHeight = 35;
            this.dgvProductos.RowTemplate.Height = 30; this.dgvProductos.BackgroundColor = Color.White;
            this.dgvProductos.BorderStyle = BorderStyle.None;
            this.dgvProductos.DoubleClick += new System.EventHandler(this.dgvProductos_DoubleClick);

            this.lblCantidad.Text = "Cantidad:"; this.lblCantidad.AutoSize = true;
            this.lblCantidad.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblCantidad.ForeColor = Color.FromArgb(52, 73, 94);
            this.lblCantidad.Location = new Point(10, 385);

            this.nudCantidad.Location = new Point(10, 405); this.nudCantidad.Size = new Size(70, 30);
            this.nudCantidad.Minimum = 1; this.nudCantidad.Maximum = 9999; this.nudCantidad.Value = 1;
            this.nudCantidad.Font = new Font("Segoe UI", 10F);

            this.lblPrecioCompra.Text = "Precio compra ($):"; this.lblPrecioCompra.AutoSize = true;
            this.lblPrecioCompra.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblPrecioCompra.ForeColor = Color.FromArgb(52, 73, 94);
            this.lblPrecioCompra.Location = new Point(90, 385);

            this.nudPrecioCompra.Location = new Point(90, 405); this.nudPrecioCompra.Size = new Size(100, 30);
            this.nudPrecioCompra.DecimalPlaces = 2; this.nudPrecioCompra.Minimum = 0.01m;
            this.nudPrecioCompra.Maximum = 999999; this.nudPrecioCompra.Value = 1;
            this.nudPrecioCompra.Font = new Font("Segoe UI", 10F);

            this.btnAgregar.Text = "➕ Agregar";
            this.btnAgregar.Location = new Point(200, 402); this.btnAgregar.Size = new Size(155, 38);
            this.btnAgregar.BackColor = Color.FromArgb(52, 152, 219); this.btnAgregar.ForeColor = Color.White;
            this.btnAgregar.FlatStyle = FlatStyle.Flat; this.btnAgregar.FlatAppearance.BorderSize = 0;
            this.btnAgregar.Font = new Font("Segoe UI", 9F, FontStyle.Bold); this.btnAgregar.Cursor = Cursors.Hand;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);

            // panelCarrito
            this.panelCarrito.BackColor = Color.White;
            this.panelCarrito.BorderStyle = BorderStyle.FixedSingle;
            this.panelCarrito.Location = new Point(395, 85);
            this.panelCarrito.Size = new Size(370, 490);
            this.panelCarrito.Controls.AddRange(new Control[] {
                this.lblCarritoTitulo, this.dgvCarrito, this.btnQuitar });

            this.lblCarritoTitulo.Text = "📋 Detalle de Compra"; this.lblCarritoTitulo.AutoSize = true;
            this.lblCarritoTitulo.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.lblCarritoTitulo.ForeColor = Color.FromArgb(52, 73, 94); this.lblCarritoTitulo.Location = new Point(10, 10);

            this.dgvCarrito.Location = new Point(10, 38); this.dgvCarrito.Size = new Size(345, 390);
            this.dgvCarrito.AllowUserToAddRows = false; this.dgvCarrito.AllowUserToDeleteRows = false;
            this.dgvCarrito.ReadOnly = true; this.dgvCarrito.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvCarrito.RowHeadersVisible = false; this.dgvCarrito.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCarrito.ColumnHeadersDefaultCellStyle = csH; this.dgvCarrito.DefaultCellStyle = csC;
            this.dgvCarrito.EnableHeadersVisualStyles = false; this.dgvCarrito.ColumnHeadersHeight = 35;
            this.dgvCarrito.RowTemplate.Height = 30; this.dgvCarrito.BackgroundColor = Color.White;
            this.dgvCarrito.BorderStyle = BorderStyle.None;

            this.btnQuitar.Text = "🗑️ Quitar seleccionado";
            this.btnQuitar.Location = new Point(10, 440); this.btnQuitar.Size = new Size(345, 38);
            this.btnQuitar.BackColor = Color.FromArgb(231, 76, 60); this.btnQuitar.ForeColor = Color.White;
            this.btnQuitar.FlatStyle = FlatStyle.Flat; this.btnQuitar.FlatAppearance.BorderSize = 0;
            this.btnQuitar.Font = new Font("Segoe UI", 9F, FontStyle.Bold); this.btnQuitar.Cursor = Cursors.Hand;
            this.btnQuitar.Click += new System.EventHandler(this.btnQuitar_Click);

            // panelResumen
            this.panelResumen.BackColor = Color.White;
            this.panelResumen.BorderStyle = BorderStyle.FixedSingle;
            this.panelResumen.Location = new Point(775, 85);
            this.panelResumen.Size = new Size(160, 490);
            this.panelResumen.Controls.AddRange(new Control[] {
                this.lblProveedorLbl, this.cmbProveedor,
                this.panelTotales, this.btnNuevaCompra, this.btnConfirmar });

            this.lblProveedorLbl.Text = "Proveedor:"; this.lblProveedorLbl.AutoSize = true;
            this.lblProveedorLbl.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblProveedorLbl.ForeColor = Color.FromArgb(52, 73, 94); this.lblProveedorLbl.Location = new Point(10, 10);

            this.cmbProveedor.Location = new Point(10, 30); this.cmbProveedor.Size = new Size(140, 30);
            this.cmbProveedor.DropDownStyle = ComboBoxStyle.DropDownList; this.cmbProveedor.Font = new Font("Segoe UI", 9F);

            this.panelTotales.BackColor = Color.FromArgb(236, 240, 241);
            this.panelTotales.BorderStyle = BorderStyle.FixedSingle;
            this.panelTotales.Location = new Point(10, 75); this.panelTotales.Size = new Size(140, 105);
            this.panelTotales.Controls.AddRange(new Control[] { this.lblSubtotal, this.lblIva, this.lblTotal });

            this.lblSubtotal.Text = "Subtotal:  $0.00"; this.lblSubtotal.AutoSize = true;
            this.lblSubtotal.Font = new Font("Segoe UI", 8F); this.lblSubtotal.ForeColor = Color.FromArgb(52, 73, 94);
            this.lblSubtotal.Location = new Point(5, 8);

            this.lblIva.Text = "IVA (16%): $0.00"; this.lblIva.AutoSize = true;
            this.lblIva.Font = new Font("Segoe UI", 8F); this.lblIva.ForeColor = Color.FromArgb(52, 73, 94);
            this.lblIva.Location = new Point(5, 38);

            this.lblTotal.Text = "TOTAL:     $0.00"; this.lblTotal.AutoSize = true;
            this.lblTotal.Font = new Font("Segoe UI", 10F, FontStyle.Bold); this.lblTotal.ForeColor = Color.FromArgb(52, 73, 94);
            this.lblTotal.Location = new Point(5, 70);

            this.btnNuevaCompra.Text = "🆕 Nueva Compra";
            this.btnNuevaCompra.Location = new Point(10, 195); this.btnNuevaCompra.Size = new Size(140, 40);
            this.btnNuevaCompra.BackColor = Color.FromArgb(46, 204, 113); this.btnNuevaCompra.ForeColor = Color.White;
            this.btnNuevaCompra.FlatStyle = FlatStyle.Flat; this.btnNuevaCompra.FlatAppearance.BorderSize = 0;
            this.btnNuevaCompra.Font = new Font("Segoe UI", 9F, FontStyle.Bold); this.btnNuevaCompra.Cursor = Cursors.Hand;
            this.btnNuevaCompra.Click += new System.EventHandler(this.btnNuevaCompra_Click);

            this.btnConfirmar.Text = "✅ Confirmar";
            this.btnConfirmar.Location = new Point(10, 250); this.btnConfirmar.Size = new Size(140, 40);
            this.btnConfirmar.BackColor = Color.FromArgb(52, 152, 219); this.btnConfirmar.ForeColor = Color.White;
            this.btnConfirmar.FlatStyle = FlatStyle.Flat; this.btnConfirmar.FlatAppearance.BorderSize = 0;
            this.btnConfirmar.Font = new Font("Segoe UI", 9F, FontStyle.Bold); this.btnConfirmar.Cursor = Cursors.Hand;
            this.btnConfirmar.Click += new System.EventHandler(this.btnConfirmar_Click);

            // FrmCompras
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = Color.FromArgb(236, 240, 241);
            this.ClientSize = new Size(950, 600);
            this.FormBorderStyle = FormBorderStyle.None;
            this.Name = "FrmCompras";
            this.Text = "Compras";
            this.Controls.AddRange(new Control[] {
                this.lblTitulo, this.lblCompraId,
                this.panelCatalogo, this.panelCarrito, this.panelResumen });
            this.Load += new System.EventHandler(this.FrmCompras_Load);

            this.panelCatalogo.ResumeLayout(false); this.panelCatalogo.PerformLayout();
            ((ISupportInitialize)this.dgvProductos).EndInit();
            ((ISupportInitialize)this.nudCantidad).EndInit();
            ((ISupportInitialize)this.nudPrecioCompra).EndInit();
            this.panelCarrito.ResumeLayout(false); this.panelCarrito.PerformLayout();
            ((ISupportInitialize)this.dgvCarrito).EndInit();
            this.panelResumen.ResumeLayout(false); this.panelResumen.PerformLayout();
            this.panelTotales.ResumeLayout(false); this.panelTotales.PerformLayout();
            this.ResumeLayout(false); this.PerformLayout();
        }

        #endregion

        private Label lblTitulo, lblCompraId;
        private Panel panelCatalogo, panelCarrito, panelResumen, panelTotales;
        private Label lblCatalogo, lblCantidad, lblPrecioCompra, lblCarritoTitulo;
        private Label lblProveedorLbl, lblSubtotal, lblIva, lblTotal;
        private TextBox txtBuscarProducto;
        private DataGridView dgvProductos, dgvCarrito;
        private NumericUpDown nudCantidad, nudPrecioCompra;
        private ComboBox cmbProveedor;
        private Button btnAgregar, btnQuitar, btnNuevaCompra, btnConfirmar;
    }
}