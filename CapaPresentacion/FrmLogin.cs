using System;
using System.Drawing;
using System.Windows.Forms;
using CapaNegocio;

namespace CapaPresentacion
{
    public partial class FrmLogin : Form
    {
        public static string UsuarioActual = "";
        public static string RolActual = "";
        public static int IdSesionActual = 0;

        public FrmLogin()
        {
            InitializeComponent();
            AgregarEfectosVisuales();
        }

        // =============================================
        // EFECTOS VISUALES: foco en campos y hover en botones
        // =============================================
        private void AgregarEfectosVisuales()
        {
            // Highlight azul al enfocar campos
            txtUsuario.Enter += (s, e) => { panelLineaUsuario.BackColor = Color.FromArgb(52, 152, 219); panelLineaUsuario.Height = 3; };
            txtUsuario.Leave += (s, e) => { panelLineaUsuario.BackColor = Color.FromArgb(189, 195, 199); panelLineaUsuario.Height = 2; };

            txtPass.Enter += (s, e) => { panelLineaPass.BackColor = Color.FromArgb(52, 152, 219); panelLineaPass.Height = 3; };
            txtPass.Leave += (s, e) => { panelLineaPass.BackColor = Color.FromArgb(189, 195, 199); panelLineaPass.Height = 2; };

            // Hover en botón Ingresar
            btnIngresar.MouseEnter += (s, e) => btnIngresar.BackColor = Color.FromArgb(41, 128, 185);
            btnIngresar.MouseLeave += (s, e) => btnIngresar.BackColor = Color.FromArgb(52, 152, 219);

            // Hover en botón Salir
            btnSalir.MouseEnter += (s, e) => btnSalir.BackColor = Color.FromArgb(192, 57, 43);
            btnSalir.MouseLeave += (s, e) => btnSalir.BackColor = Color.FromArgb(231, 76, 60);

            // Enter en txtPass también dispara el login
            txtPass.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    btnIngresar_Click(s, e);
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                }
            };

            // Enter en txtUsuario pasa foco a contraseña
            txtUsuario.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    txtPass.Focus();
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                }
            };
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            UsuarioActual = "";
            RolActual = "";
            IdSesionActual = 0;
            txtUsuario.Focus();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsuario.Text))
            {
                MostrarError("Por favor, ingrese el nombre de usuario");
                txtUsuario.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPass.Text))
            {
                MostrarError("Por favor, ingrese la contraseña");
                txtPass.Focus();
                return;
            }

            try
            {
                // Efecto visual: deshabilitar botón mientras valida
                btnIngresar.Enabled = false;
                btnIngresar.Text = "⏳  Verificando...";

                CN_Usuario objNegocio = new CN_Usuario();
                bool respuesta = objNegocio.ValidarUsuario(txtUsuario.Text, txtPass.Text);

                if (respuesta)
                {
                    UsuarioActual = txtUsuario.Text;
                    RolActual = objNegocio.ObtenerRol(UsuarioActual);
                    IdSesionActual = CN_Sesion.IniciarSesion(UsuarioActual);

                    this.Hide();
                    FrmMenuPrincipal menuPrincipal = new FrmMenuPrincipal();
                    menuPrincipal.Show();
                    menuPrincipal.FormClosed += (s, args) => this.Close();
                }
                else
                {
                    // Resaltar líneas en rojo para indicar error
                    panelLineaUsuario.BackColor = Color.FromArgb(231, 76, 60);
                    panelLineaPass.BackColor = Color.FromArgb(231, 76, 60);

                    MostrarError("❌ Usuario o contraseña incorrectos\n\nPor favor verifique sus credenciales.");
                    txtUsuario.Clear();
                    txtPass.Clear();
                    txtUsuario.Focus();

                    // Restaurar colores de línea
                    panelLineaUsuario.BackColor = Color.FromArgb(189, 195, 199);
                    panelLineaPass.BackColor = Color.FromArgb(189, 195, 199);

                    btnIngresar.Enabled = true;
                    btnIngresar.Text = "▶  Ingresar al sistema";
                }
            }
            catch (Exception ex)
            {
                btnIngresar.Enabled = true;
                btnIngresar.Text = "▶  Ingresar al sistema";
                MostrarError("Error al iniciar sesión: " + ex.Message);
            }
        }

        private void MostrarError(string mensaje)
        {
            MessageBox.Show(mensaje, "Sistema Veterinaria",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}