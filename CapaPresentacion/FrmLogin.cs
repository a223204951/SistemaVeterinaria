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
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            CN_Usuario objNegocio = new CN_Usuario();
            bool respuesta = objNegocio.ValidarUsuario(txtUsuario.Text, txtPass.Text);

            if (respuesta)
            {
                this.Hide();
                FrmClientes menu = new FrmClientes();
                menu.Show();
                // Si cierran el menú, se cierra toda la app
                menu.FormClosed += (s, args) => this.Close();
            }
            else
            {
                MessageBox.Show("Usuario o contraseña incorrectos");
            }
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {

        }
    }
}