using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace CapaPresentacion
{
    partial class FrmRegistrarUsuario
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
            this.txtIdUsuario = new TextBox();

            // Panel formulario
            this.panelForm = new Panel();

            // Columna izquierda
            this.lblUsuario = new Label();
            this.txtUsuario = new TextBox();
            this.lblPass = new Label();
            this.txtPass = new TextBox();
            this.lblPassConfirm = new Label();
            this.txtPassConfirm = new TextBox();
            this.chkCambiarPassword = new CheckBox();

            // Columna derecha
            this.lblAcceso = new Label();
            this.cmbAcceso = new ComboBox();
            this.lblEmpleado = new Label();
            this.cmbEmpleado = new ComboBox();
            this.lblEstado = new Label();
            this.rbtnActivo = new RadioButton();
            this.rbtnInactivo = new RadioButton();

            // Panel info empleado
            this.panelInfoEmpleado = new Panel();
            this.lblInfoTipo = new Label();
            this.lblInfoNombre = new Label();

            // Botones
            this.btnGuardar = new Button();
            this.btnCancelar = new Button();

            this.panelForm.SuspendLayout();
            this.panelInfoEmpleado.SuspendLayout();
            this.SuspendLayout();

            // ── lblTitulo ─────────────────────────────────────────────────────
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            this.lblTitulo.ForeColor = Color.FromArgb(52, 73, 94);
            this.lblTitulo.Location = new Point(20, 18);
            this.lblTitulo.Text = "👤 Registrar Nuevo Usuario";

            // ── txtIdUsuario (oculto) ─────────────────────────────────────────
            this.txtIdUsuario.Visible = false;
            this.txtIdUsuario.Location = new Point(0, 0);

            // ── panelForm ─────────────────────────────────────────────────────
            this.panelForm.BackColor = Color.White;
            this.panelForm.BorderStyle = BorderStyle.FixedSingle;
            this.panelForm.Location = new Point(20, 62);
            this.panelForm.Size = new Size(560, 340);

            // ── COLUMNA IZQUIERDA (x=20) ──────────────────────────────────────

            // Usuario
            this.lblUsuario.Text = "Nombre de usuario *";
            this.lblUsuario.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblUsuario.ForeColor = Color.FromArgb(52, 73, 94);
            this.lblUsuario.Location = new Point(20, 18);
            this.lblUsuario.AutoSize = true;

            this.txtUsuario.Font = new Font("Segoe UI", 10F);
            this.txtUsuario.Location = new Point(20, 38);
            this.txtUsuario.Size = new Size(248, 28);
            this.txtUsuario.MaxLength = 20;
            this.txtUsuario.CharacterCasing = CharacterCasing.Lower;

            // Contraseña
            this.lblPass.Text = "Contraseña *";
            this.lblPass.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblPass.ForeColor = Color.FromArgb(52, 73, 94);
            this.lblPass.Location = new Point(20, 78);
            this.lblPass.AutoSize = true;

            this.txtPass.Font = new Font("Segoe UI", 10F);
            this.txtPass.Location = new Point(20, 98);
            this.txtPass.Size = new Size(248, 28);
            this.txtPass.MaxLength = 20;
            this.txtPass.PasswordChar = '●';

            // Confirmar contraseña
            this.lblPassConfirm.Text = "Confirmar contraseña *";
            this.lblPassConfirm.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblPassConfirm.ForeColor = Color.FromArgb(52, 73, 94);
            this.lblPassConfirm.Location = new Point(20, 138);
            this.lblPassConfirm.AutoSize = true;

            this.txtPassConfirm.Font = new Font("Segoe UI", 10F);
            this.txtPassConfirm.Location = new Point(20, 158);
            this.txtPassConfirm.Size = new Size(248, 28);
            this.txtPassConfirm.MaxLength = 20;
            this.txtPassConfirm.PasswordChar = '●';

            // CheckBox cambiar contraseña (solo visible en edición)
            this.chkCambiarPassword.Text = "Cambiar contraseña";
            this.chkCambiarPassword.Font = new Font("Segoe UI", 9F);
            this.chkCambiarPassword.Location = new Point(20, 198);
            this.chkCambiarPassword.AutoSize = true;
            this.chkCambiarPassword.Visible = false;
            this.chkCambiarPassword.CheckedChanged += new System.EventHandler(this.chkCambiarPassword_CheckedChanged);

            // ── COLUMNA DERECHA (x=295) ───────────────────────────────────────

            // Nivel de acceso
            this.lblAcceso.Text = "Nivel de acceso *";
            this.lblAcceso.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblAcceso.ForeColor = Color.FromArgb(52, 73, 94);
            this.lblAcceso.Location = new Point(295, 18);
            this.lblAcceso.AutoSize = true;

            this.cmbAcceso.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbAcceso.Font = new Font("Segoe UI", 10F);
            this.cmbAcceso.Location = new Point(295, 38);
            this.cmbAcceso.Size = new Size(245, 30);
            this.cmbAcceso.Items.AddRange(new object[] {
                "ADMINISTRADOR", "VETERINARIO", "CAJERO", "ASISTENTE" });
            this.cmbAcceso.SelectedIndexChanged += new System.EventHandler(this.cmbAcceso_SelectedIndexChanged);

            // Empleado vinculado
            this.lblEmpleado.Text = "Empleado vinculado *";
            this.lblEmpleado.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblEmpleado.ForeColor = Color.FromArgb(52, 73, 94);
            this.lblEmpleado.Location = new Point(295, 78);
            this.lblEmpleado.AutoSize = true;

            this.cmbEmpleado.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbEmpleado.Font = new Font("Segoe UI", 9.5F);
            this.cmbEmpleado.Location = new Point(295, 98);
            this.cmbEmpleado.Size = new Size(245, 30);
            this.cmbEmpleado.SelectedIndexChanged += new System.EventHandler(this.cmbEmpleado_SelectedIndexChanged);

            // Panel info empleado
            this.panelInfoEmpleado.BackColor = Color.FromArgb(235, 245, 251);
            this.panelInfoEmpleado.BorderStyle = BorderStyle.FixedSingle;
            this.panelInfoEmpleado.Location = new Point(295, 138);
            this.panelInfoEmpleado.Size = new Size(245, 52);

            this.lblInfoNombre.Text = "Seleccione un empleado";
            this.lblInfoNombre.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            this.lblInfoNombre.ForeColor = Color.FromArgb(41, 128, 185);
            this.lblInfoNombre.Location = new Point(8, 8);
            this.lblInfoNombre.Size = new Size(228, 18);

            this.lblInfoTipo.Text = "";
            this.lblInfoTipo.Font = new Font("Segoe UI", 8F, FontStyle.Italic);
            this.lblInfoTipo.ForeColor = Color.FromArgb(127, 140, 141);
            this.lblInfoTipo.Location = new Point(8, 28);
            this.lblInfoTipo.Size = new Size(228, 16);

            this.panelInfoEmpleado.Controls.Add(this.lblInfoNombre);
            this.panelInfoEmpleado.Controls.Add(this.lblInfoTipo);

            // Estado
            this.lblEstado.Text = "Estado *";
            this.lblEstado.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblEstado.ForeColor = Color.FromArgb(52, 73, 94);
            this.lblEstado.Location = new Point(295, 202);
            this.lblEstado.AutoSize = true;

            this.rbtnActivo.Text = "✓ ACTIVO";
            this.rbtnActivo.Font = new Font("Segoe UI", 9F);
            this.rbtnActivo.ForeColor = Color.FromArgb(46, 204, 113);
            this.rbtnActivo.Location = new Point(295, 222);
            this.rbtnActivo.AutoSize = true;
            this.rbtnActivo.Checked = true;
            this.rbtnActivo.Cursor = Cursors.Hand;

            this.rbtnInactivo.Text = "✗ INACTIVO";
            this.rbtnInactivo.Font = new Font("Segoe UI", 9F);
            this.rbtnInactivo.ForeColor = Color.FromArgb(231, 76, 60);
            this.rbtnInactivo.Location = new Point(400, 222);
            this.rbtnInactivo.AutoSize = true;
            this.rbtnInactivo.Cursor = Cursors.Hand;

            // ── Agregar controles al panel ────────────────────────────────────
            this.panelForm.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblUsuario, this.txtUsuario,
                this.lblPass, this.txtPass,
                this.lblPassConfirm, this.txtPassConfirm,
                this.chkCambiarPassword,
                this.lblAcceso, this.cmbAcceso,
                this.lblEmpleado, this.cmbEmpleado,
                this.panelInfoEmpleado,
                this.lblEstado, this.rbtnActivo, this.rbtnInactivo
            });

            // ── Botones ───────────────────────────────────────────────────────
            this.btnGuardar.Text = "💾 Guardar";
            this.btnGuardar.Location = new Point(20, 420);
            this.btnGuardar.Size = new Size(145, 42);
            this.btnGuardar.BackColor = Color.FromArgb(46, 204, 113);
            this.btnGuardar.ForeColor = Color.White;
            this.btnGuardar.FlatStyle = FlatStyle.Flat;
            this.btnGuardar.FlatAppearance.BorderSize = 0;
            this.btnGuardar.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnGuardar.Cursor = Cursors.Hand;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);

            this.btnCancelar.Text = "✗ Cancelar";
            this.btnCancelar.Location = new Point(175, 420);
            this.btnCancelar.Size = new Size(145, 42);
            this.btnCancelar.BackColor = Color.FromArgb(231, 76, 60);
            this.btnCancelar.ForeColor = Color.White;
            this.btnCancelar.FlatStyle = FlatStyle.Flat;
            this.btnCancelar.FlatAppearance.BorderSize = 0;
            this.btnCancelar.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnCancelar.Cursor = Cursors.Hand;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);

            // ── FrmRegistrarUsuario ───────────────────────────────────────────
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = Color.FromArgb(236, 240, 241);
            this.ClientSize = new Size(600, 480);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.KeyPreview = true;
            this.Text = "Usuario";
            this.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblTitulo, this.txtIdUsuario,
                this.panelForm,
                this.btnGuardar, this.btnCancelar
            });
            this.Load += new System.EventHandler(this.FrmRegistrarUsuario_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmRegistrarUsuario_KeyDown);

            this.panelInfoEmpleado.ResumeLayout(false);
            this.panelInfoEmpleado.PerformLayout();
            this.panelForm.ResumeLayout(false);
            this.panelForm.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion

        private Label lblTitulo;
        public TextBox txtIdUsuario;
        private Panel panelForm;
        private Label lblUsuario;
        public TextBox txtUsuario;
        private Label lblPass;
        public TextBox txtPass;
        private Label lblPassConfirm;
        public TextBox txtPassConfirm;
        public CheckBox chkCambiarPassword;
        private Label lblAcceso;
        public ComboBox cmbAcceso;
        private Label lblEmpleado;
        public ComboBox cmbEmpleado;
        private Panel panelInfoEmpleado;
        private Label lblInfoNombre;
        private Label lblInfoTipo;
        private Label lblEstado;
        public RadioButton rbtnActivo;
        public RadioButton rbtnInactivo;
        private Button btnGuardar;
        private Button btnCancelar;
    }
}