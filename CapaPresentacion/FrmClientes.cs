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
    public partial class FrmClientes : Form
    {
        CN_Cliente objetoCN = new CN_Cliente();

        public FrmClientes()
        {
            InitializeComponent();
        }

        private void FrmClientes_Load(object sender, EventArgs e)
        {
            MostrarClientes();
        }

        private void MostrarClientes()
        {
            dgvClientes.DataSource = objetoCN.MostrarClientes();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                objetoCN.InsertarCliente(txtNombre.Text, txtTelefono.Text, txtDireccion.Text, "ACTIVO");
                MessageBox.Show("Guardado correctamente");
                MostrarClientes();
                txtNombre.Clear(); txtTelefono.Clear(); txtDireccion.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}
