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

        // EVENTO LOAD DEL FORMULARIO
        private void FrmListadoCliente_Load(object sender, EventArgs e)
        {
            this.Top = 0;
            this.Left = 0;

            Mostrar();
        }

        // MÉTODO PARA MOSTRAR TODOS LOS CLIENTES EN EL DATAGRIDVIEW
        public void Mostrar()
        {
            try
            {
                this.dlistado.DataSource = CN_Cliente.Listar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.StackTrace,
                    "Sistema Veterinaria",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // MÉTODO PARA BUSCAR CLIENTES POR NOMBRE
        private void BuscarNombre()
        {
            this.dlistado.DataSource = CN_Cliente.BuscarNombre(txtbuscar.Text);
        }

        // MÉTODO PARA BUSCAR CLIENTES POR ID
        private void BuscarId()
        {
            this.dlistado.DataSource = CN_Cliente.BuscarId(txtbuscar.Text);
        }

        // EVENTO CLICK DEL BOTÓN BUSCAR
        private void btnbuscar_Click(object sender, EventArgs e)
        {
            if (rbtnnombre.Checked)
            {
                BuscarNombre();
            }
            else if (rbtnidcliente.Checked)
            {
                BuscarId();
            }
            else
            {
                MessageBox.Show("Seleccione un criterio de búsqueda",
                    "Sistema Veterinaria",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        // EVENTO CLICK DEL BOTÓN NUEVO
        private void btnnuevo_Click(object sender, EventArgs e)
        {
            FrmRegistrarCliente form = new FrmRegistrarCliente();
            form.Insert = true;
            form.Show();
            this.Hide();
        }

        // EVENTO CLICK DEL BOTÓN EDITAR
        private void btneditar_Click(object sender, EventArgs e)
        {
            if (dlistado.SelectedRows.Count > 0)
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
            else
            {
                MessageBox.Show("Seleccione un cliente para editar",
                    "Sistema Veterinaria",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        // EVENTO CLICK DEL BOTÓN ELIMINAR
        private void btneliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dlistado.SelectedRows.Count > 0)
                {
                    DialogResult opcion = MessageBox.Show("¿Realmente desea eliminar permanentemente el cliente seleccionado?",
                        "Sistema Veterinaria",
                        MessageBoxButtons.OKCancel,
                        MessageBoxIcon.Question);

                    if (opcion == DialogResult.OK)
                    {
                        string idcliente = dlistado.CurrentRow.Cells["idcliente"].Value.ToString();
                        string resultado = CN_Cliente.Eliminar(Convert.ToInt32(idcliente), FrmLogin.UsuarioActual);

                        if (resultado == "OK")
                        {
                            MessageBox.Show("Cliente eliminado correctamente",
                                "Sistema Veterinaria",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

                            Mostrar();
                        }
                        else
                        {
                            MessageBox.Show(resultado,
                                "Sistema Veterinaria",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Seleccione un cliente para eliminar",
                        "Sistema Veterinaria",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.StackTrace,
                    "Sistema Veterinaria",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // EVENTO CLICK DEL BOTÓN SALIR
        private void btnsalir_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show(
                "¿Está seguro que desea cerrar sesión?",
                "Sistema Veterinaria",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
            {
                // Buscar si hay un formulario de login abierto (oculto)
                FrmLogin login = (FrmLogin)Application.OpenForms["FrmLogin"];

                if (login != null)
                {
                    // Si existe, mostrarlo
                    login.Show();
                    login.txtUsuario.Clear();
                    login.txtPass.Clear();
                    login.txtUsuario.Focus();
                }
                else
                {
                    // Si no existe, crear uno nuevo
                    login = new FrmLogin();
                    login.Show();
                }

                // Cerrar el formulario de listado
                this.Close();
            }
        }
    }
}