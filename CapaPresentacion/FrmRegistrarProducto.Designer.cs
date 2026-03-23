using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace CapaPresentacion
{
    partial class FrmRegistrarProducto
    {
        private IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            // ── Declarar controles existentes ─────────────────────────────────
            this.lblTitulo = new Label();
            this.lblNombre = new Label();
            this.txtNombre = new TextBox();
            this.lblDescripcion = new Label();
            this.txtDescripcion = new TextBox();
            this.lblPrecio = new Label();
            this.nudPrecio = new NumericUpDown();
            this.lblInfoPrecio = new Label();
            this.lblStock = new Label();
            this.nudStock = new NumericUpDown();
            this.lblInfoStock = new Label();
            this.lblCategoria = new Label();
            this.cmbCategoria = new ComboBox();
            this.chkEsMedicamento = new CheckBox();
            this.lblVencimiento = new Label();
            this.dtpVencimiento = new DateTimePicker();
            this.lblEstado = new Label();
            this.rbtnActivo = new RadioButton();
            this.rbtnInactivo = new RadioButton();
            this.txtIdProducto = new TextBox();
            this.lblProveedor = new Label();
            this.cmbProveedor = new ComboBox();
            this.btnGuardar = new Button();
            this.btnCancelar = new Button();

            // ── Nuevos controles para código de barras ─────────────────────────
            this.panelBarcode = new Panel();
            this.lblBarcodeTitulo = new Label();
            this.picCodigoBarras = new PictureBox();
            this.lblCodigoBarrasNum = new Label();
            this.btnRegenerarCodigo = new Button();

            ((ISupportInitialize)this.nudPrecio).BeginInit();
            ((ISupportInitialize)this.nudStock).BeginInit();
            ((ISupportInitialize)this.picCodigoBarras).BeginInit();
            this.panelBarcode.SuspendLayout();
            this.SuspendLayout();

            // ── lblTitulo ─────────────────────────────────────────────────────
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            this.lblTitulo.ForeColor = Color.FromArgb(52, 73, 94);
            this.lblTitulo.Location = new Point(20, 15);
            this.lblTitulo.Text = "📝 Registrar Nuevo Producto";

            // ── txtIdProducto (oculto) ────────────────────────────────────────
            this.txtIdProducto.Location = new Point(0, 0);
            this.txtIdProducto.Visible = false;

            // ── COLUMNA IZQUIERDA — datos del producto ────────────────────────

            // Nombre
            this.lblNombre.Text = "Nombre *";
            this.lblNombre.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblNombre.ForeColor = Color.FromArgb(52, 73, 94);
            this.lblNombre.Location = new Point(20, 58); this.lblNombre.AutoSize = true;

            this.txtNombre.Font = new Font("Segoe UI", 10F);
            this.txtNombre.Location = new Point(20, 78);
            this.txtNombre.Size = new Size(290, 28);
            this.txtNombre.MaxLength = 100;

            // Descripción
            this.lblDescripcion.Text = "Descripción";
            this.lblDescripcion.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblDescripcion.ForeColor = Color.FromArgb(52, 73, 94);
            this.lblDescripcion.Location = new Point(20, 115); this.lblDescripcion.AutoSize = true;

            this.txtDescripcion.Font = new Font("Segoe UI", 10F);
            this.txtDescripcion.Location = new Point(20, 135);
            this.txtDescripcion.Size = new Size(290, 60);
            this.txtDescripcion.Multiline = true;
            this.txtDescripcion.MaxLength = 500;

            // Precio
            this.lblPrecio.Text = "Precio (MXN) *";
            this.lblPrecio.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblPrecio.ForeColor = Color.FromArgb(52, 73, 94);
            this.lblPrecio.Location = new Point(20, 205); this.lblPrecio.AutoSize = true;

            this.nudPrecio.Location = new Point(20, 225);
            this.nudPrecio.Size = new Size(130, 28);
            this.nudPrecio.DecimalPlaces = 2;
            this.nudPrecio.Minimum = 0;
            this.nudPrecio.Maximum = 999999;
            this.nudPrecio.Value = 1;
            this.nudPrecio.Font = new Font("Segoe UI", 10F);
            this.nudPrecio.ValueChanged += new System.EventHandler(this.nudPrecio_ValueChanged);

            this.lblInfoPrecio.Text = "💰 Precio moderado";
            this.lblInfoPrecio.AutoSize = true;
            this.lblInfoPrecio.Font = new Font("Segoe UI", 8F, FontStyle.Italic);
            this.lblInfoPrecio.ForeColor = Color.FromArgb(46, 204, 113);
            this.lblInfoPrecio.Location = new Point(160, 230);

            // Stock
            this.lblStock.Text = "Stock *";
            this.lblStock.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblStock.ForeColor = Color.FromArgb(52, 73, 94);
            this.lblStock.Location = new Point(20, 263); this.lblStock.AutoSize = true;

            this.nudStock.Location = new Point(20, 283);
            this.nudStock.Size = new Size(130, 28);
            this.nudStock.Minimum = 0;
            this.nudStock.Maximum = 99999;
            this.nudStock.Font = new Font("Segoe UI", 10F);
            this.nudStock.ValueChanged += new System.EventHandler(this.nudStock_ValueChanged);

            this.lblInfoStock.Text = "✅ Stock suficiente";
            this.lblInfoStock.AutoSize = true;
            this.lblInfoStock.Font = new Font("Segoe UI", 8F, FontStyle.Italic);
            this.lblInfoStock.ForeColor = Color.FromArgb(46, 204, 113);
            this.lblInfoStock.Location = new Point(160, 288);

            // Categoría
            this.lblCategoria.Text = "Categoría *";
            this.lblCategoria.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblCategoria.ForeColor = Color.FromArgb(52, 73, 94);
            this.lblCategoria.Location = new Point(20, 323); this.lblCategoria.AutoSize = true;

            this.cmbCategoria.Location = new Point(20, 343);
            this.cmbCategoria.Size = new Size(290, 30);
            this.cmbCategoria.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbCategoria.Font = new Font("Segoe UI", 10F);

            // Medicamento + vencimiento
            this.chkEsMedicamento.Text = "Es medicamento";
            this.chkEsMedicamento.Font = new Font("Segoe UI", 9F);
            this.chkEsMedicamento.Location = new Point(20, 385);
            this.chkEsMedicamento.AutoSize = true;
            this.chkEsMedicamento.CheckedChanged += new System.EventHandler(this.chkEsMedicamento_CheckedChanged);

            this.lblVencimiento.Text = "Fecha vencimiento";
            this.lblVencimiento.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblVencimiento.ForeColor = Color.FromArgb(149, 165, 166);
            this.lblVencimiento.Location = new Point(20, 413); this.lblVencimiento.AutoSize = true;

            this.dtpVencimiento.Font = new Font("Segoe UI", 10F);
            this.dtpVencimiento.Format = DateTimePickerFormat.Short;
            this.dtpVencimiento.Location = new Point(20, 433);
            this.dtpVencimiento.Size = new Size(160, 28);
            this.dtpVencimiento.Enabled = false;

            // Estado
            this.lblEstado.Text = "Estado *";
            this.lblEstado.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblEstado.ForeColor = Color.FromArgb(52, 73, 94);
            this.lblEstado.Location = new Point(20, 473); this.lblEstado.AutoSize = true;

            this.rbtnActivo.Text = "Activo";
            this.rbtnActivo.Font = new Font("Segoe UI", 9F);
            this.rbtnActivo.Location = new Point(20, 493);
            this.rbtnActivo.AutoSize = true;
            this.rbtnActivo.Checked = true;

            // lblProveedor
            this.lblProveedor.Text = "Proveedor";
            this.lblProveedor.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblProveedor.ForeColor = Color.FromArgb(52, 73, 94);
            this.lblProveedor.Location = new Point(20, 453);
            this.lblProveedor.AutoSize = true;

            // cmbProveedor
            this.cmbProveedor.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbProveedor.Font = new Font("Segoe UI", 10F);
            this.cmbProveedor.Location = new Point(20, 473);
            this.cmbProveedor.Size = new Size(290, 30);

            this.rbtnInactivo.Text = "Inactivo";
            this.rbtnInactivo.Font = new Font("Segoe UI", 9F);
            this.rbtnInactivo.Location = new Point(100, 493);
            this.rbtnInactivo.AutoSize = true;

            // ── PANEL CÓDIGO DE BARRAS (columna derecha) ──────────────────────
            this.panelBarcode.BackColor = Color.White;
            this.panelBarcode.BorderStyle = BorderStyle.FixedSingle;
            this.panelBarcode.Location = new Point(330, 55);
            this.panelBarcode.Size = new Size(290, 220);
            this.panelBarcode.Controls.AddRange(new Control[] {
                this.lblBarcodeTitulo,
                this.picCodigoBarras,
                this.lblCodigoBarrasNum,
                this.btnRegenerarCodigo
            });

            this.lblBarcodeTitulo.Text = "🔲 Código de Barras EAN-13";
            this.lblBarcodeTitulo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblBarcodeTitulo.ForeColor = Color.FromArgb(52, 73, 94);
            this.lblBarcodeTitulo.Location = new Point(8, 10);
            this.lblBarcodeTitulo.AutoSize = true;

            this.picCodigoBarras.Location = new Point(8, 35);
            this.picCodigoBarras.Size = new Size(272, 90);
            this.picCodigoBarras.SizeMode = PictureBoxSizeMode.StretchImage;
            this.picCodigoBarras.BackColor = Color.White;
            this.picCodigoBarras.BorderStyle = BorderStyle.FixedSingle;
            this.picCodigoBarras.Visible = false;

            this.lblCodigoBarrasNum.Text = "Se generará al guardar";
            this.lblCodigoBarrasNum.Font = new Font("Courier New", 10F, FontStyle.Bold);
            this.lblCodigoBarrasNum.ForeColor = Color.FromArgb(149, 165, 166);
            this.lblCodigoBarrasNum.Location = new Point(8, 132);
            this.lblCodigoBarrasNum.Size = new Size(272, 22);
            this.lblCodigoBarrasNum.TextAlign = ContentAlignment.MiddleCenter;

            this.btnRegenerarCodigo.Text = "🔄 Regenerar código";
            this.btnRegenerarCodigo.Location = new Point(8, 163);
            this.btnRegenerarCodigo.Size = new Size(272, 36);
            this.btnRegenerarCodigo.BackColor = Color.FromArgb(142, 68, 173);
            this.btnRegenerarCodigo.ForeColor = Color.White;
            this.btnRegenerarCodigo.FlatStyle = FlatStyle.Flat;
            this.btnRegenerarCodigo.FlatAppearance.BorderSize = 0;
            this.btnRegenerarCodigo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.btnRegenerarCodigo.Cursor = Cursors.Hand;
            this.btnRegenerarCodigo.Visible = false;
            this.btnRegenerarCodigo.Click += new System.EventHandler(this.btnRegenerarCodigo_Click);

            // ── Botones Guardar / Cancelar ────────────────────────────────────
            this.btnGuardar.Text = "💾 Guardar";
            this.btnGuardar.Location = new Point(20, 535);
            this.btnGuardar.Size = new Size(140, 42);
            this.btnGuardar.BackColor = Color.FromArgb(46, 204, 113);
            this.btnGuardar.ForeColor = Color.White;
            this.btnGuardar.FlatStyle = FlatStyle.Flat;
            this.btnGuardar.FlatAppearance.BorderSize = 0;
            this.btnGuardar.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnGuardar.Cursor = Cursors.Hand;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);

            this.btnCancelar.Text = "✗ Cancelar";
            this.btnCancelar.Location = new Point(170, 535);
            this.btnCancelar.Size = new Size(140, 42);
            this.btnCancelar.BackColor = Color.FromArgb(231, 76, 60);
            this.btnCancelar.ForeColor = Color.White;
            this.btnCancelar.FlatStyle = FlatStyle.Flat;
            this.btnCancelar.FlatAppearance.BorderSize = 0;
            this.btnCancelar.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnCancelar.Cursor = Cursors.Hand;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);

            // ── FrmRegistrarProducto ──────────────────────────────────────────
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = Color.FromArgb(236, 240, 241);
            this.ClientSize = new Size(640, 595);
            this.KeyPreview = true;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Text = "Producto";
            this.Controls.AddRange(new Control[] {
                this.lblProveedor, this.cmbProveedor,
                this.lblTitulo, this.txtIdProducto,
                // Col izquierda
                this.lblNombre, this.txtNombre,
                this.lblDescripcion, this.txtDescripcion,
                this.lblPrecio, this.nudPrecio, this.lblInfoPrecio,
                this.lblStock, this.nudStock, this.lblInfoStock,
                this.lblCategoria, this.cmbCategoria,
                this.chkEsMedicamento, this.lblVencimiento, this.dtpVencimiento,
                this.lblEstado, this.rbtnActivo, this.rbtnInactivo,
                this.btnGuardar, this.btnCancelar,
                // Col derecha
                this.panelBarcode
            });
            this.Load += new System.EventHandler(this.FrmRegistrarProducto_Load);
            this.KeyDown += new KeyEventHandler(this.FrmRegistrarProducto_KeyDown);

            ((ISupportInitialize)this.nudPrecio).EndInit();
            ((ISupportInitialize)this.nudStock).EndInit();
            ((ISupportInitialize)this.picCodigoBarras).EndInit();
            this.panelBarcode.ResumeLayout(false);
            this.panelBarcode.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        // ── Declaraciones ─────────────────────────────────────────────────────
        private Label lblTitulo;
        public TextBox txtIdProducto;
        private Label lblNombre;
        public TextBox txtNombre;
        private Label lblDescripcion;
        public TextBox txtDescripcion;
        private Label lblPrecio;
        public NumericUpDown nudPrecio;
        private Label lblInfoPrecio;
        private Label lblStock;
        public NumericUpDown nudStock;
        private Label lblInfoStock;
        private Label lblCategoria;
        public ComboBox cmbCategoria;
        public CheckBox chkEsMedicamento;
        private Label lblVencimiento;
        public DateTimePicker dtpVencimiento;
        private Label lblEstado;
        public RadioButton rbtnActivo;
        public RadioButton rbtnInactivo;
        private Label lblProveedor;
        public ComboBox cmbProveedor;
        private Button btnGuardar;
        private Button btnCancelar;
        // Nuevos
        private Panel panelBarcode;
        private Label lblBarcodeTitulo;
        private PictureBox picCodigoBarras;
        private Label lblCodigoBarrasNum;
        private Button btnRegenerarCodigo;
    }
}