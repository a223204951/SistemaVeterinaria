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

            // Mostrar botones de admin solo si es admin
            bool esAdmin = FrmLogin.UsuarioActual.ToLower() == "admin";
            btnAuditoria.Visible = esAdmin;
            btnSesiones.Visible = esAdmin;
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
            // REGISTRAR CIERRE DE SESIÓN
            if (FrmLogin.IdSesionActual > 0)
            {
                CN_Sesion.CerrarSesion(FrmLogin.IdSesionActual);
                FrmLogin.IdSesionActual = 0;
            }

            // Volver al login
            FrmLogin login = new FrmLogin();
            login.Show();
            this.Close();
        }

        // EVENTO CLICK DEL BOTÓN AUDITORÍA (SOLO ADMIN)
        private void btnAuditoria_Click(object sender, EventArgs e)
        {
            // Verificar que solo el admin pueda acceder
            if (FrmLogin.UsuarioActual.ToLower() == "admin")
            {
                FrmAuditoria formAuditoria = new FrmAuditoria();
                formAuditoria.ShowDialog();
            }
            else
            {
                MessageBox.Show("Solo el administrador puede acceder a la auditoría",
                    "Sistema Veterinaria",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }
        // EVENTO CLICK DEL BOTÓN SESIONES (SOLO ADMIN)
        private void btnSesiones_Click(object sender, EventArgs e)
        {
            if (FrmLogin.UsuarioActual.ToLower() == "admin")
            {
                FrmSesiones formSesiones = new FrmSesiones();
                formSesiones.ShowDialog();
            }
            else
            {
                MessageBox.Show("Solo el administrador puede ver las sesiones",
                    "Sistema Veterinaria",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }
    }
}