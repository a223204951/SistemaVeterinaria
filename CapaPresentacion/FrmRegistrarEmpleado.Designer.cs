using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace CapaPresentacion
{
    partial class FrmRegistrarEmpleado
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
            this.txtIdEmpleado = new TextBox();
            this.lblNombre = new Label();
            this.txtNombre = new TextBox();
            this.lblApellidos = new Label();
            this.txtApellidos = new TextBox();
            this.lblTelefono = new Label();
            this.txtTelefono = new TextBox();
            this.lblDireccion = new Label();
            this.txtDireccion = new TextBox();
            this.lblCorreo = new Label();
            this.txtCorreo = new TextBox();
            this.lblTipo = new Label();
            this.cmbTipo = new ComboBox();
            this.lblEstado = new Label();
            this.rbtnActivo = new RadioButton();
            this.rbtnInactivo = new RadioButton();
            this.panelVeterinario = new Panel();
            this.lblCedulaReq = new Label();
            this.lblCedula = new Label();
            this.txtCedula = new TextBox();
            this.lblEspecialidad = new Label();
            this.txtEspecialidad = new TextBox();
            this.btnGuardar = new Button();
            this.btnCancelar = new Button();

            this.panelVeterinario.SuspendLayout();
            this.SuspendLayout();

            // ── lblTitulo ─────────────────────────────────────────────────────
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            this.lblTitulo.ForeColor = Color.FromArgb(52, 73, 94);
            this.lblTitulo.Location = new Point(20, 18);
            this.lblTitulo.Text = "➕ Registrar Nuevo Empleado";

            // ── txtIdEmpleado (oculto) ────────────────────────────────────────
            this.txtIdEmpleado.Location = new Point(0, 0);
            this.txtIdEmpleado.Visible = false;

            // ────────────────────────────────────────────────────────────────
            // COLUMNA IZQUIERDA  x=20
            // ────────────────────────────────────────────────────────────────

            this.lblNombre.Text = "Nombre *";
            this.lblNombre.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblNombre.ForeColor = Color.FromArgb(52, 73, 94);
            this.lblNombre.Location = new Point(20, 62);
            this.lblNombre.AutoSize = true;

            this.txtNombre.Font = new Font("Segoe UI", 10F);
            this.txtNombre.Location = new Point(20, 82);
            this.txtNombre.Size = new Size(265, 28);
            this.txtNombre.MaxLength = 30;

            this.lblApellidos.Text = "Apellidos *";
            this.lblApellidos.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblApellidos.ForeColor = Color.FromArgb(52, 73, 94);
            this.lblApellidos.Location = new Point(20, 122);
            this.lblApellidos.AutoSize = true;

            this.txtApellidos.Font = new Font("Segoe UI", 10F);
            this.txtApellidos.Location = new Point(20, 142);
            this.txtApellidos.Size = new Size(265, 28);
            this.txtApellidos.MaxLength = 50;

            this.lblTelefono.Text = "Teléfono";
            this.lblTelefono.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblTelefono.ForeColor = Color.FromArgb(52, 73, 94);
            this.lblTelefono.Location = new Point(20, 182);
            this.lblTelefono.AutoSize = true;

            this.txtTelefono.Font = new Font("Segoe UI", 10F);
            this.txtTelefono.Location = new Point(20, 202);
            this.txtTelefono.Size = new Size(265, 28);
            this.txtTelefono.MaxLength = 15;

            this.lblDireccion.Text = "Dirección";
            this.lblDireccion.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblDireccion.ForeColor = Color.FromArgb(52, 73, 94);
            this.lblDireccion.Location = new Point(20, 242);
            this.lblDireccion.AutoSize = true;

            this.txtDireccion.Font = new Font("Segoe UI", 10F);
            this.txtDireccion.Location = new Point(20, 262);
            this.txtDireccion.Size = new Size(265, 28);
            this.txtDireccion.MaxLength = 50;

            this.lblCorreo.Text = "Correo electrónico";
            this.lblCorreo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblCorreo.ForeColor = Color.FromArgb(52, 73, 94);
            this.lblCorreo.Location = new Point(20, 302);
            this.lblCorreo.AutoSize = true;

            this.txtCorreo.Font = new Font("Segoe UI", 10F);
            this.txtCorreo.Location = new Point(20, 322);
            this.txtCorreo.Size = new Size(265, 28);
            this.txtCorreo.MaxLength = 50;

            // ────────────────────────────────────────────────────────────────
            // COLUMNA DERECHA  x=310
            // ────────────────────────────────────────────────────────────────

            this.lblTipo.Text = "Tipo de Empleado *";
            this.lblTipo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblTipo.ForeColor = Color.FromArgb(52, 73, 94);
            this.lblTipo.Location = new Point(310, 62);
            this.lblTipo.AutoSize = true;

            this.cmbTipo.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbTipo.Font = new Font("Segoe UI", 10F);
            this.cmbTipo.Location = new Point(310, 82);
            this.cmbTipo.Size = new Size(265, 30);
            this.cmbTipo.Items.AddRange(new object[] {
                "VETERINARIO", "CAJERO", "ASISTENTE", "ADMINISTRADOR" });
            this.cmbTipo.SelectedIndexChanged +=
                new System.EventHandler(this.cmbTipo_SelectedIndexChanged);

            this.lblEstado.Text = "Estado *";
            this.lblEstado.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblEstado.ForeColor = Color.FromArgb(52, 73, 94);
            this.lblEstado.Location = new Point(310, 130);
            this.lblEstado.AutoSize = true;

            this.rbtnActivo.Text = "✓ ACTIVO";
            this.rbtnActivo.Font = new Font("Segoe UI", 10F);
            this.rbtnActivo.ForeColor = Color.FromArgb(46, 204, 113);
            this.rbtnActivo.Location = new Point(310, 152);
            this.rbtnActivo.Checked = true;
            this.rbtnActivo.AutoSize = true;
            this.rbtnActivo.Cursor = Cursors.Hand;

            this.rbtnInactivo.Text = "✗ INACTIVO";
            this.rbtnInactivo.Font = new Font("Segoe UI", 10F);
            this.rbtnInactivo.ForeColor = Color.FromArgb(231, 76, 60);
            this.rbtnInactivo.Location = new Point(440, 152);
            this.rbtnInactivo.AutoSize = true;
            this.rbtnInactivo.Cursor = Cursors.Hand;

            // ── panelVeterinario (cédula + especialidad) ──────────────────────
            // Visible solo cuando tipo = VETERINARIO
            this.panelVeterinario.BackColor = Color.FromArgb(232, 248, 245);
            this.panelVeterinario.BorderStyle = BorderStyle.FixedSingle;
            this.panelVeterinario.Location = new Point(310, 195);
            this.panelVeterinario.Size = new Size(265, 165);
            this.panelVeterinario.Visible = false;
            this.panelVeterinario.Controls.AddRange(new Control[] {
                this.lblCedula, this.txtCedula,
                this.lblEspecialidad, this.txtEspecialidad });

            this.lblCedula.Text = "Cédula Profesional *";
            this.lblCedula.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblCedula.ForeColor = Color.FromArgb(22, 160, 133);
            this.lblCedula.Location = new Point(10, 10);
            this.lblCedula.AutoSize = true;

            this.txtCedula.Font = new Font("Segoe UI", 10F);
            this.txtCedula.Location = new Point(10, 30);
            this.txtCedula.Size = new Size(240, 28);
            this.txtCedula.MaxLength = 20;

            this.lblEspecialidad.Text = "Especialidad";
            this.lblEspecialidad.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblEspecialidad.ForeColor = Color.FromArgb(22, 160, 133);
            this.lblEspecialidad.Location = new Point(10, 70);
            this.lblEspecialidad.AutoSize = true;

            this.txtEspecialidad.Font = new Font("Segoe UI", 10F);
            this.txtEspecialidad.Location = new Point(10, 90);
            this.txtEspecialidad.Size = new Size(240, 28);
            this.txtEspecialidad.MaxLength = 50;

            // lblCedulaReq — aviso fuera del panel (debajo de él)
            this.lblCedulaReq.Text = "⚕️ Cédula y especialidad requeridas para veterinarios";
            this.lblCedulaReq.Font = new Font("Segoe UI", 8F, FontStyle.Italic);
            this.lblCedulaReq.ForeColor = Color.FromArgb(22, 160, 133);
            this.lblCedulaReq.Location = new Point(310, 368);
            this.lblCedulaReq.Size = new Size(265, 18);
            this.lblCedulaReq.Visible = false;

            // ── Botones ───────────────────────────────────────────────────────
            this.btnGuardar.Text = "💾 Guardar";
            this.btnGuardar.Location = new Point(310, 400);
            this.btnGuardar.Size = new Size(125, 42);
            this.btnGuardar.BackColor = Color.FromArgb(46, 204, 113);
            this.btnGuardar.ForeColor = Color.White;
            this.btnGuardar.FlatStyle = FlatStyle.Flat;
            this.btnGuardar.FlatAppearance.BorderSize = 0;
            this.btnGuardar.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnGuardar.Cursor = Cursors.Hand;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);

            this.btnCancelar.Text = "✗ Cancelar";
            this.btnCancelar.Location = new Point(445, 400);
            this.btnCancelar.Size = new Size(130, 42);
            this.btnCancelar.BackColor = Color.FromArgb(231, 76, 60);
            this.btnCancelar.ForeColor = Color.White;
            this.btnCancelar.FlatStyle = FlatStyle.Flat;
            this.btnCancelar.FlatAppearance.BorderSize = 0;
            this.btnCancelar.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnCancelar.Cursor = Cursors.Hand;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);

            // ── FrmRegistrarEmpleado ──────────────────────────────────────────
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = Color.FromArgb(236, 240, 241);
            this.ClientSize = new Size(600, 460);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.KeyPreview = true;
            this.Text = "Empleado";
            this.Controls.AddRange(new Control[] {
                this.lblTitulo, this.txtIdEmpleado,
                this.lblNombre,    this.txtNombre,
                this.lblApellidos, this.txtApellidos,
                this.lblTelefono,  this.txtTelefono,
                this.lblDireccion, this.txtDireccion,
                this.lblCorreo,    this.txtCorreo,
                this.lblTipo,      this.cmbTipo,
                this.lblEstado,    this.rbtnActivo, this.rbtnInactivo,
                this.panelVeterinario,
                this.lblCedulaReq,
                this.btnGuardar,   this.btnCancelar
            });
            this.Load += new System.EventHandler(this.FrmRegistrarEmpleado_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmRegistrarEmpleado_KeyDown);

            this.panelVeterinario.ResumeLayout(false);
            this.panelVeterinario.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private Label lblTitulo;
        public TextBox txtIdEmpleado;
        private Label lblNombre;
        public TextBox txtNombre;
        private Label lblApellidos;
        public TextBox txtApellidos;
        private Label lblTelefono;
        public TextBox txtTelefono;
        private Label lblDireccion;
        public TextBox txtDireccion;
        private Label lblCorreo;
        public TextBox txtCorreo;
        private Label lblTipo;
        public ComboBox cmbTipo;
        private Label lblEstado;
        public RadioButton rbtnActivo;
        public RadioButton rbtnInactivo;
        private Panel panelVeterinario;
        private Label lblCedulaReq;
        private Label lblCedula;
        public TextBox txtCedula;
        private Label lblEspecialidad;
        public TextBox txtEspecialidad;
        private Button btnGuardar;
        private Button btnCancelar;
    }
}