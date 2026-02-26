using System.Drawing;
using System.Windows.Forms;

namespace CapaPresentacion
{
    partial class FrmLogin
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.panelIzquierdo = new System.Windows.Forms.Panel();
            this.lblEmoji = new System.Windows.Forms.Label();
            this.lblNombreSistema = new System.Windows.Forms.Label();
            this.lblSlogan = new System.Windows.Forms.Label();
            this.panelDerecho = new System.Windows.Forms.Panel();
            this.lblBienvenido = new System.Windows.Forms.Label();
            this.lblSubtitulo = new System.Windows.Forms.Label();
            this.lblUsuario = new System.Windows.Forms.Label();
            this.txtUsuario = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtPass = new System.Windows.Forms.TextBox();
            this.btnIngresar = new System.Windows.Forms.Button();
            this.btnSalir = new System.Windows.Forms.Button();
            this.lblVersion = new System.Windows.Forms.Label();
            this.panelLineaUsuario = new System.Windows.Forms.Panel();
            this.panelLineaPass = new System.Windows.Forms.Panel();
            this.panelIzquierdo.SuspendLayout();
            this.panelDerecho.SuspendLayout();
            this.SuspendLayout();

            // =============================================
            // FORM
            // =============================================
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(820, 500);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FrmLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sistema Veterinaria — Iniciar Sesión";
            this.BackColor = System.Drawing.Color.FromArgb(236, 240, 241);
            this.Load += new System.EventHandler(this.FrmLogin_Load);

            // =============================================
            // PANEL IZQUIERDO  (azul oscuro — branding)
            // =============================================
            this.panelIzquierdo.BackColor = System.Drawing.Color.FromArgb(34, 49, 63);
            this.panelIzquierdo.Location = new System.Drawing.Point(0, 0);
            this.panelIzquierdo.Name = "panelIzquierdo";
            this.panelIzquierdo.Size = new System.Drawing.Size(340, 500);
            this.panelIzquierdo.TabIndex = 0;

            // Emoji mascota
            this.lblEmoji.AutoSize = false;
            this.lblEmoji.Text = "🐾";
            this.lblEmoji.Font = new System.Drawing.Font("Segoe UI Emoji", 56F);
            this.lblEmoji.ForeColor = System.Drawing.Color.White;
            this.lblEmoji.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblEmoji.Location = new System.Drawing.Point(0, 100);
            this.lblEmoji.Size = new System.Drawing.Size(340, 100);
            this.lblEmoji.Name = "lblEmoji";

            // Nombre del sistema
            this.lblNombreSistema.AutoSize = false;
            this.lblNombreSistema.Text = "VetSystem";
            this.lblNombreSistema.Font = new System.Drawing.Font("Segoe UI", 28F, System.Drawing.FontStyle.Bold);
            this.lblNombreSistema.ForeColor = System.Drawing.Color.White;
            this.lblNombreSistema.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblNombreSistema.Location = new System.Drawing.Point(0, 205);
            this.lblNombreSistema.Size = new System.Drawing.Size(340, 55);
            this.lblNombreSistema.Name = "lblNombreSistema";

            // Línea decorativa bajo el título
            Panel lineaDeco = new Panel();
            lineaDeco.BackColor = System.Drawing.Color.FromArgb(52, 152, 219);
            lineaDeco.Location = new System.Drawing.Point(120, 268);
            lineaDeco.Size = new System.Drawing.Size(100, 4);
            lineaDeco.Name = "lineaDeco";

            // Slogan
            this.lblSlogan.AutoSize = false;
            this.lblSlogan.Text = "Gestión integral para\nclínicas veterinarias";
            this.lblSlogan.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Italic);
            this.lblSlogan.ForeColor = System.Drawing.Color.FromArgb(149, 165, 166);
            this.lblSlogan.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblSlogan.Location = new System.Drawing.Point(0, 285);
            this.lblSlogan.Size = new System.Drawing.Size(340, 55);
            this.lblSlogan.Name = "lblSlogan";

            this.panelIzquierdo.Controls.Add(this.lblEmoji);
            this.panelIzquierdo.Controls.Add(this.lblNombreSistema);
            this.panelIzquierdo.Controls.Add(lineaDeco);
            this.panelIzquierdo.Controls.Add(this.lblSlogan);

            // =============================================
            // PANEL DERECHO  (blanco — formulario)
            // =============================================
            this.panelDerecho.BackColor = System.Drawing.Color.White;
            this.panelDerecho.Location = new System.Drawing.Point(340, 0);
            this.panelDerecho.Name = "panelDerecho";
            this.panelDerecho.Size = new System.Drawing.Size(480, 500);
            this.panelDerecho.TabIndex = 1;

            // Bienvenido
            this.lblBienvenido.AutoSize = false;
            this.lblBienvenido.Text = "¡Bienvenido!";
            this.lblBienvenido.Font = new System.Drawing.Font("Segoe UI", 22F, System.Drawing.FontStyle.Bold);
            this.lblBienvenido.ForeColor = System.Drawing.Color.FromArgb(34, 49, 63);
            this.lblBienvenido.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.lblBienvenido.Location = new System.Drawing.Point(60, 70);
            this.lblBienvenido.Size = new System.Drawing.Size(360, 50);
            this.lblBienvenido.Name = "lblBienvenido";

            // Subtítulo
            this.lblSubtitulo.AutoSize = false;
            this.lblSubtitulo.Text = "Ingrese sus credenciales para continuar";
            this.lblSubtitulo.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSubtitulo.ForeColor = System.Drawing.Color.FromArgb(127, 140, 141);
            this.lblSubtitulo.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.lblSubtitulo.Location = new System.Drawing.Point(60, 124);
            this.lblSubtitulo.Size = new System.Drawing.Size(360, 24);
            this.lblSubtitulo.Name = "lblSubtitulo";

            // ── CAMPO USUARIO ──────────────────────────
            this.lblUsuario.AutoSize = false;
            this.lblUsuario.Text = "👤  Usuario";
            this.lblUsuario.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblUsuario.ForeColor = System.Drawing.Color.FromArgb(52, 73, 94);
            this.lblUsuario.Location = new System.Drawing.Point(60, 175);
            this.lblUsuario.Size = new System.Drawing.Size(360, 22);
            this.lblUsuario.Name = "lblUsuario";

            this.txtUsuario.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtUsuario.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtUsuario.ForeColor = System.Drawing.Color.FromArgb(34, 49, 63);
            this.txtUsuario.Location = new System.Drawing.Point(60, 200);
            this.txtUsuario.Name = "txtUsuario";
            this.txtUsuario.Size = new System.Drawing.Size(360, 28);
            this.txtUsuario.TabIndex = 0;
            this.txtUsuario.BackColor = System.Drawing.Color.White;

            this.panelLineaUsuario.BackColor = System.Drawing.Color.FromArgb(189, 195, 199);
            this.panelLineaUsuario.Location = new System.Drawing.Point(60, 230);
            this.panelLineaUsuario.Size = new System.Drawing.Size(360, 2);
            this.panelLineaUsuario.Name = "panelLineaUsuario";

            // ── CAMPO CONTRASEÑA ───────────────────────
            this.lblPassword.AutoSize = false;
            this.lblPassword.Text = "🔒  Contraseña";
            this.lblPassword.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblPassword.ForeColor = System.Drawing.Color.FromArgb(52, 73, 94);
            this.lblPassword.Location = new System.Drawing.Point(60, 265);
            this.lblPassword.Size = new System.Drawing.Size(360, 22);
            this.lblPassword.Name = "lblPassword";

            this.txtPass.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPass.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtPass.ForeColor = System.Drawing.Color.FromArgb(34, 49, 63);
            this.txtPass.PasswordChar = '●';
            this.txtPass.Location = new System.Drawing.Point(60, 290);
            this.txtPass.Name = "txtPass";
            this.txtPass.Size = new System.Drawing.Size(360, 28);
            this.txtPass.TabIndex = 1;
            this.txtPass.BackColor = System.Drawing.Color.White;

            this.panelLineaPass.BackColor = System.Drawing.Color.FromArgb(189, 195, 199);
            this.panelLineaPass.Location = new System.Drawing.Point(60, 320);
            this.panelLineaPass.Size = new System.Drawing.Size(360, 2);
            this.panelLineaPass.Name = "panelLineaPass";

            // ── BOTÓN INGRESAR ─────────────────────────
            this.btnIngresar.BackColor = System.Drawing.Color.FromArgb(52, 152, 219);
            this.btnIngresar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnIngresar.FlatAppearance.BorderSize = 0;
            this.btnIngresar.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnIngresar.ForeColor = System.Drawing.Color.White;
            this.btnIngresar.Location = new System.Drawing.Point(60, 360);
            this.btnIngresar.Name = "btnIngresar";
            this.btnIngresar.Size = new System.Drawing.Size(240, 48);
            this.btnIngresar.TabIndex = 2;
            this.btnIngresar.Text = "▶  Ingresar al sistema";
            this.btnIngresar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnIngresar.UseVisualStyleBackColor = false;
            this.btnIngresar.Click += new System.EventHandler(this.btnIngresar_Click);

            // ── BOTÓN SALIR ────────────────────────────
            this.btnSalir.BackColor = System.Drawing.Color.FromArgb(231, 76, 60);
            this.btnSalir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSalir.FlatAppearance.BorderSize = 0;
            this.btnSalir.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnSalir.ForeColor = System.Drawing.Color.White;
            this.btnSalir.Location = new System.Drawing.Point(310, 360);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(110, 48);
            this.btnSalir.TabIndex = 3;
            this.btnSalir.Text = "✕  Salir";
            this.btnSalir.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSalir.UseVisualStyleBackColor = false;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);

            // ── VERSIÓN ────────────────────────────────
            this.lblVersion.AutoSize = false;
            this.lblVersion.Text = "v1.0  •  Sistema Veterinaria © 2025";
            this.lblVersion.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblVersion.ForeColor = System.Drawing.Color.FromArgb(189, 195, 199);
            this.lblVersion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblVersion.Location = new System.Drawing.Point(60, 448);
            this.lblVersion.Size = new System.Drawing.Size(360, 20);
            this.lblVersion.Name = "lblVersion";

            // Añadir controles al panel derecho
            this.panelDerecho.Controls.Add(this.lblBienvenido);
            this.panelDerecho.Controls.Add(this.lblSubtitulo);
            this.panelDerecho.Controls.Add(this.lblUsuario);
            this.panelDerecho.Controls.Add(this.txtUsuario);
            this.panelDerecho.Controls.Add(this.panelLineaUsuario);
            this.panelDerecho.Controls.Add(this.lblPassword);
            this.panelDerecho.Controls.Add(this.txtPass);
            this.panelDerecho.Controls.Add(this.panelLineaPass);
            this.panelDerecho.Controls.Add(this.btnIngresar);
            this.panelDerecho.Controls.Add(this.btnSalir);
            this.panelDerecho.Controls.Add(this.lblVersion);

            // Añadir paneles al form
            this.Controls.Add(this.panelIzquierdo);
            this.Controls.Add(this.panelDerecho);

            this.panelIzquierdo.ResumeLayout(false);
            this.panelDerecho.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        // PANEL IZQUIERDO
        private System.Windows.Forms.Panel panelIzquierdo;
        private System.Windows.Forms.Label lblEmoji;
        private System.Windows.Forms.Label lblNombreSistema;
        private System.Windows.Forms.Label lblSlogan;

        // PANEL DERECHO
        private System.Windows.Forms.Panel panelDerecho;
        private System.Windows.Forms.Label lblBienvenido;
        private System.Windows.Forms.Label lblSubtitulo;
        private System.Windows.Forms.Label lblUsuario;
        public System.Windows.Forms.TextBox txtUsuario;
        private System.Windows.Forms.Panel panelLineaUsuario;
        private System.Windows.Forms.Label lblPassword;
        public System.Windows.Forms.TextBox txtPass;
        private System.Windows.Forms.Panel panelLineaPass;
        private System.Windows.Forms.Button btnIngresar;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.Label lblVersion;
    }
}