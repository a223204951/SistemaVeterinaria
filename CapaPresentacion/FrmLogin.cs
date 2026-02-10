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

namespace CapaPresentacion
{
    // USUARIO DE PRUEBA: admin
    // CONTRASEÑA: 1234
    public partial class FrmLogin : Form
    {
        // Variable estática para guardar el usuario actual
        public static string UsuarioActual = "";

        public FrmLogin()
        {
            InitializeComponent();
        }

        // EVENTO LOAD DEL FORMULARIO
        private void FrmLogin_Load(object sender, EventArgs e)
        {
        }

        // EVENTO CLICK DEL BOTÓN SALIR
        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // EVENTO CLICK DEL BOTÓN INGRESAR
        private void btnIngresar_Click(object sender, EventArgs e)
        {
            // Validar que los campos no estén vacíos
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

            // Validar credenciales con la capa de negocio
            CN_Usuario objNegocio = new CN_Usuario();
            bool respuesta = objNegocio.ValidarUsuario(txtUsuario.Text, txtPass.Text);

            if (respuesta)
            {
                // GUARDAR USUARIO ACTUAL
                UsuarioActual = txtUsuario.Text;

                // Si las credenciales son correctas, abrir el listado de clientes
                this.Hide(); // Ocultar el login (NO cerrar)

                FrmListadoCliente menu = new FrmListadoCliente();
                menu.Show();
                menu.FormClosed += (s, args) => this.Close();

                // IMPORTANTE: Ya no vinculamos FormClosed aquí
                // Si cierran el menú, solo volvemos al login
            }
            else
            {
                MessageBox.Show("Usuario o contraseña incorrectos",
                    "Sistema Veterinaria",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                // Limpiar los campos y enfocar el campo de usuario
                txtUsuario.Clear();
                txtPass.Clear();
                txtUsuario.Focus();
            }
        }
    }
}