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
    public partial class FrmListadoClientes : Form
    {
        public FrmListadoClientes()
        {
            InitializeComponent();
        }

        // EVENTO LOAD DEL FORMULARIO
        private void FrmListadoCliente_Load(object sender, EventArgs e)
        {
            // POSICIONAR EL FORMULARIO EN LA ESQUINA SUPERIOR IZQUIERDA
            this.Top = 0;
            this.Left = 0;

            // CARGAR TODOS LOS CLIENTES EN EL DATAGRIDVIEW
            Mostrar();
        }

        // MÉTODO PARA MOSTRAR TODOS LOS CLIENTES EN EL DATAGRIDVIEW
        public void Mostrar()
        {
            this.dlistado.DataSource = CN_Cliente.Listar();
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
            // VERIFICAR QUÉ CRITERIO DE BÚSQUEDA ESTÁ SELECCIONADO
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
            // CREAR UNA INSTANCIA DEL FORMULARIO DE REGISTRO
            FrmRegistrarCliente form = new FrmRegistrarCliente();

            // INDICAR QUE ES UNA OPERACIÓN DE INSERCIÓN
            form.Insert = true;

            // MOSTRAR EL FORMULARIO Y OCULTAR EL ACTUAL
            form.Show();
            this.Hide();
        }

        // EVENTO CLICK DEL BOTÓN EDITAR
        private void btneditar_Click(object sender, EventArgs e)
        {
            // VERIFICAR QUE HAYA UNA FILA SELECCIONADA
            if (dlistado.SelectedRows.Count > 0)
            {
                // CREAR UNA INSTANCIA DEL FORMULARIO DE REGISTRO
                FrmRegistrarCliente form = new FrmRegistrarCliente();

                // INDICAR QUE ES UNA OPERACIÓN DE EDICIÓN
                form.Edit = true;

                // CARGAR LOS DATOS DEL CLIENTE SELECCIONADO EN LOS CAMPOS DEL FORMULARIO
                form.txtidcliente.Text = this.dlistado.CurrentRow.Cells["idcliente"].Value.ToString();
                form.txtnombre.Text = this.dlistado.CurrentRow.Cells["nombre"].Value.ToString();
                form.txttelefono.Text = this.dlistado.CurrentRow.Cells["telefono"].Value.ToString();
                form.txtdireccion.Text = this.dlistado.CurrentRow.Cells["direccion"].Value.ToString();

                // SELECCIONAR EL RADIOBUTTON DE ESTADO CORRESPONDIENTE
                string estado = this.dlistado.CurrentRow.Cells["estado"].Value.ToString();
                if (estado == "ACTIVO")
                {
                    form.rbtnactivo.Checked = true;
                }
                else
                {
                    form.rbtninactivo.Checked = true;
                }

                // MOSTRAR EL FORMULARIO Y OCULTAR EL ACTUAL
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
                // VERIFICAR QUE HAYA UNA FILA SELECCIONADA
                if (dlistado.SelectedRows.Count > 0)
                {
                    // CONFIRMAR LA ELIMINACIÓN CON EL USUARIO
                    DialogResult opcion = MessageBox.Show("¿Realmente desea eliminar permanentemente el cliente seleccionado?",
                        "Sistema Veterinaria",
                        MessageBoxButtons.OKCancel,
                        MessageBoxIcon.Question);

                    if (opcion == DialogResult.OK)
                    {
                        // OBTENER EL ID DEL CLIENTE SELECCIONADO
                        string idcliente = dlistado.CurrentRow.Cells["idcliente"].Value.ToString();

                        // ELIMINAR EL CLIENTE (CON EL USUARIO ACTUAL PARA AUDITORÍA)
                        string resultado = CN_Cliente.Eliminar(
                            Convert.ToInt32(idcliente),
                            FrmLogin.UsuarioActual
                        );

                        // VERIFICAR SI LA ELIMINACIÓN FUE EXITOSA
                        if (resultado == "OK")
                        {
                            MessageBox.Show("Cliente eliminado correctamente",
                                "Sistema Veterinaria",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

                            // RECARGAR LA LISTA DE CLIENTES
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
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }
    }
}