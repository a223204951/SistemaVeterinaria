using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaNegocio;
using CapaDatos;

namespace CapaPresentacion
{
    // USUARIO DE PRUEBA: admin
    // CONTRASEÑA: 1234
    public partial class FrmLogin : Form
    {
        // VARIABLES ESTÁTICAS PARA LA SESIÓN ACTUAL
        public static string UsuarioActual = "";
        public static int IdSesionActual = 0;

        public FrmLogin()
        {
            InitializeComponent();
        }

        // EVENTO LOAD DEL FORMULARIO
        private void FrmLogin_Load(object sender, EventArgs e)
        {
            // Cerrar sesiones huérfanas al iniciar
            try
            {
                CD_Sesion sesionDato = new CD_Sesion();
            }
            catch { }
        }

        // EVENTO CLICK DEL BOTÓN SALIR
        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // EVENTO CLICK DEL BOTÓN INGRESAR
        private void btnIngresar_Click(object sender, EventArgs e)
        {
            // Validar campos vacíos
            if (string.IsNullOrWhiteSpace(txtUsuario.Text))
            {
                MessageBox.Show("Ingrese el nombre de usuario",
                    "Sistema Veterinaria",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                txtUsuario.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPass.Text))
            {
                MessageBox.Show("Ingrese la contraseña",
                    "Sistema Veterinaria",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                txtPass.Focus();
                return;
            }

            // Validar credenciales
            CN_Usuario objNegocio = new CN_Usuario();
            bool respuesta = objNegocio.ValidarUsuario(txtUsuario.Text, txtPass.Text);

            if (respuesta)
            {
                // GUARDAR USUARIO ACTUAL
                UsuarioActual = txtUsuario.Text;

                // REGISTRAR INICIO DE SESIÓN
                IdSesionActual = CN_Sesion.IniciarSesion(UsuarioActual);

                // Abrir listado de clientes
                this.Hide();
                FrmListadoCliente menu = new FrmListadoCliente();
                menu.Show();

                // Al cerrar el menú, cerrar sesión y volver al login
                menu.FormClosed += (s, args) =>
                {
                    // REGISTRAR CIERRE DE SESIÓN
                    if (IdSesionActual > 0)
                    {
                        CN_Sesion.CerrarSesion(IdSesionActual);
                        IdSesionActual = 0;
                    }
                    this.Close();
                };
            }
            else
            {
                MessageBox.Show("Usuario o contraseña incorrectos",
                    "Sistema Veterinaria",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                txtUsuario.Clear();
                txtPass.Clear();
                txtUsuario.Focus();
            }
        }
    }
}