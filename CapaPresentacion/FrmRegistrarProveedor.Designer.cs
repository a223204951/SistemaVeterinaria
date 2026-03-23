using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace CapaPresentacion
{
    partial class FrmRegistrarProveedor
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
            this.lblTitulo = new Label();
            this.txtIdProveedor = new TextBox();
            this.lblNombre = new Label();
            this.txtNombre = new TextBox();
            this.lblTelefono = new Label();
            this.txtTelefono = new TextBox();
            this.lblDireccion = new Label();
            this.txtDireccion = new TextBox();
            this.lblCorreo = new Label();
            this.txtCorreo = new TextBox();
            this.lblEstado = new Label();
            this.rbtnActivo = new RadioButton();
            this.rbtnInactivo = new RadioButton();
            this.btnGuardar = new Button();
            this.btnCancelar = new Button();
            this.SuspendLayout();

            // lblTitulo
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            this.lblTitulo.ForeColor = Color.FromArgb(52, 73, 94);
            this.lblTitulo.Location = new Point(20, 18);
            this.lblTitulo.Text = "➕ Registrar Nuevo Proveedor";

            // txtIdProveedor (oculto)
            this.txtIdProveedor.Location = new Point(0, 0);
            this.txtIdProveedor.Visible = false;

            // lblNombre
            this.lblNombre.Text = "Nombre *";
            this.lblNombre.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblNombre.ForeColor = Color.FromArgb(52, 73, 94);
            this.lblNombre.Location = new Point(20, 60);
            this.lblNombre.AutoSize = true;

            // txtNombre
            this.txtNombre.Font = new Font("Segoe UI", 10F);
            this.txtNombre.Location = new Point(20, 80);
            this.txtNombre.Size = new Size(360, 28);
            this.txtNombre.MaxLength = 50;

            // lblTelefono
            this.lblTelefono.Text = "Teléfono";
            this.lblTelefono.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblTelefono.ForeColor = Color.FromArgb(52, 73, 94);
            this.lblTelefono.Location = new Point(20, 120);
            this.lblTelefono.AutoSize = true;

            // txtTelefono
            this.txtTelefono.Font = new Font("Segoe UI", 10F);
            this.txtTelefono.Location = new Point(20, 140);
            this.txtTelefono.Size = new Size(360, 28);
            this.txtTelefono.MaxLength = 15;

            // lblDireccion
            this.lblDireccion.Text = "Dirección";
            this.lblDireccion.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblDireccion.ForeColor = Color.FromArgb(52, 73, 94);
            this.lblDireccion.Location = new Point(20, 180);
            this.lblDireccion.AutoSize = true;

            // txtDireccion
            this.txtDireccion.Font = new Font("Segoe UI", 10F);
            this.txtDireccion.Location = new Point(20, 200);
            this.txtDireccion.Size = new Size(360, 28);
            this.txtDireccion.MaxLength = 50;

            // lblCorreo
            this.lblCorreo.Text = "Correo electrónico";
            this.lblCorreo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblCorreo.ForeColor = Color.FromArgb(52, 73, 94);
            this.lblCorreo.Location = new Point(20, 240);
            this.lblCorreo.AutoSize = true;

            // txtCorreo
            this.txtCorreo.Font = new Font("Segoe UI", 10F);
            this.txtCorreo.Location = new Point(20, 260);
            this.txtCorreo.Size = new Size(360, 28);
            this.txtCorreo.MaxLength = 50;

            // lblEstado
            this.lblEstado.Text = "Estado *";
            this.lblEstado.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblEstado.ForeColor = Color.FromArgb(52, 73, 94);
            this.lblEstado.Location = new Point(20, 305);
            this.lblEstado.AutoSize = true;

            // rbtnActivo
            this.rbtnActivo.Text = "Activo";
            this.rbtnActivo.Font = new Font("Segoe UI", 9F);
            this.rbtnActivo.Location = new Point(20, 325);
            this.rbtnActivo.AutoSize = true;
            this.rbtnActivo.Checked = true;

            // rbtnInactivo
            this.rbtnInactivo.Text = "Inactivo";
            this.rbtnInactivo.Font = new Font("Segoe UI", 9F);
            this.rbtnInactivo.Location = new Point(100, 325);
            this.rbtnInactivo.AutoSize = true;

            // btnGuardar
            this.btnGuardar.Text = "💾 Guardar";
            this.btnGuardar.Location = new Point(20, 370);
            this.btnGuardar.Size = new Size(170, 42);
            this.btnGuardar.BackColor = Color.FromArgb(46, 204, 113);
            this.btnGuardar.ForeColor = Color.White;
            this.btnGuardar.FlatStyle = FlatStyle.Flat;
            this.btnGuardar.FlatAppearance.BorderSize = 0;
            this.btnGuardar.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnGuardar.Cursor = Cursors.Hand;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);

            // btnCancelar
            this.btnCancelar.Text = "✗ Cancelar";
            this.btnCancelar.Location = new Point(200, 370);
            this.btnCancelar.Size = new Size(170, 42);
            this.btnCancelar.BackColor = Color.FromArgb(231, 76, 60);
            this.btnCancelar.ForeColor = Color.White;
            this.btnCancelar.FlatStyle = FlatStyle.Flat;
            this.btnCancelar.FlatAppearance.BorderSize = 0;
            this.btnCancelar.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnCancelar.Cursor = Cursors.Hand;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);

            // FrmRegistrarProveedor
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = Color.FromArgb(236, 240, 241);
            this.ClientSize = new Size(400, 430);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.KeyPreview = true;
            this.Text = "Proveedor";
            this.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblTitulo, this.txtIdProveedor,
                this.lblNombre,    this.txtNombre,
                this.lblTelefono,  this.txtTelefono,
                this.lblDireccion, this.txtDireccion,
                this.lblCorreo,    this.txtCorreo,
                this.lblEstado,    this.rbtnActivo, this.rbtnInactivo,
                this.btnGuardar,   this.btnCancelar
            });
            this.Load += new System.EventHandler(this.FrmRegistrarProveedor_Load);
            this.KeyDown += new KeyEventHandler(this.FrmRegistrarProveedor_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion

        private Label lblTitulo;
        public TextBox txtIdProveedor;
        private Label lblNombre;
        public TextBox txtNombre;
        private Label lblTelefono;
        public TextBox txtTelefono;
        private Label lblDireccion;
        public TextBox txtDireccion;
        private Label lblCorreo;
        public TextBox txtCorreo;
        private Label lblEstado;
        public RadioButton rbtnActivo;
        public RadioButton rbtnInactivo;
        private Button btnGuardar;
        private Button btnCancelar;
    }
}