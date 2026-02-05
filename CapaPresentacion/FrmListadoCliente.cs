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
    public partial class FrmListadoCliente : Form
    {
        public FrmListadoCliente()
        {
            InitializeComponent();
        }
        private void FrmListadoCliente_Load(object sender, EventArgs e)
        {
            this.Top = 0;
            this.Left = 0;

            Mostrar();
        }

        // MÉTODO PARA MOSTRAR LOS CLIENTES EN EL DATAGRIDVIEW
        private void Mostrar()
        {
            this.dlistado.DataSource = CN_Cliente.Listar();
        }

        // MÉTODO PARA BUSCAR LOS CLIENTES POR NOMBRE
        private void BuscarNombre()
        {
            this.dlistado.DataSource = CN_Cliente.BuscarNombre(txtbuscar.Text);
        }

        // MÉTODO PARA BUSCAR LOS CLIENTES POR DNI
        private void BuscarDni()
        {
            this.dlistado.DataSource = CN_Cliente.BuscarId(txtbuscar.Text);
        }

        private void btnbuscar_Click(object sender, EventArgs e)
        {
            if (rbtnnombre.Checked)
            {
                BuscarNombre();
            }
            else if (rbtnidcliente.Checked)
            {
                BuscarDni();
            }
            else
            {
                MessageBox.Show("Seleccione un criterio de búsqueda",
                    "Sistema de Ventas",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        private void btnnuevo_Click(object sender, EventArgs e)
        {
            FrmRegistrarCliente form = new FrmRegistrarCliente();

            form.Insert = true;

            form.Show();
            this.Hide();
        }

        private void btneditar_Click(object sender, EventArgs e)
        {
            FrmRegistrarCliente form = new FrmRegistrarCliente();

            form.Edit = true;

            form.txtidcliente.Text = this.dlistado.CurrentRow.Cells["idcliente"].Value.ToString();
            form.txtnombre.Text = this.dlistado.CurrentRow.Cells["nombre"].Value.ToString();
            form.txttelefono.Text = this.dlistado.CurrentRow.Cells["telefono"].Value.ToString();
            form.txtdireccion.Text = this.dlistado.CurrentRow.Cells["direccion"].Value.ToString();

            string estado = this.dlistado.CurrentRow.Cells["estado"].Value.ToString();
            if (estado == "ACTIVO")
            {
                form.rbtnactivo.Checked = true;
            }
            else
            {
                form.rbtninactivo.Checked = true;
            }

            form.Show();
            this.Hide();
        }

        private void btneliminar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult opcion;
                opcion = MessageBox.Show("¿Realmente desea eliminar el(los) cliente(s) seleccionado?",
                    "Sistema de Ventas",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Question);
                if (dlistado.SelectedRows.Count > 0)
                {
                    if (opcion == DialogResult.OK)
                    {
                        string idcliente = dlistado.CurrentRow.Cells["idcliente"].Value.ToString();
                        CN_Cliente.Eliminar(Convert.ToInt32(idcliente));

                        MessageBox.Show("Cliente eliminado correctamente",
                            "Sistema de Ventas",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

                        Mostrar();
                    }
                }
                Mostrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void FrmListadoCliente_Load_1(object sender, EventArgs e)
        {

        }
    }
}
