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
    public partial class FrmRegistrarCliente : Form
    {
        public bool Insert = false;
        public bool Edit = false;

        public FrmRegistrarCliente()
        {
            InitializeComponent();
        }

        // EVENTO LOAD DEL FORMULARIO
        private void FrmRegistrarCliente_Load(object sender, EventArgs e)
        {
            this.Top = 0;
            this.Left = 0;

            // Si es inserción, habilitar el RadioButton Activo por defecto
            if (Insert)
            {
                rbtnactivo.Checked = true;
            }
        }

        // EVENTO CLICK DEL BOTÓN GUARDAR
        private void btnguardar_Click(object sender, EventArgs e)
        {
            // Validar que los campos obligatorios no estén vacíos
            if (string.IsNullOrWhiteSpace(txtnombre.Text))
            {
                MessageBox.Show("Ingrese el nombre del cliente",
                    "Sistema Veterinaria",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                txtnombre.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txttelefono.Text))
            {
                MessageBox.Show("Ingrese el teléfono del cliente",
                    "Sistema Veterinaria",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                txttelefono.Focus();
                return;
            }

            // Determinar el estado según el RadioButton seleccionado
            string estado = rbtnactivo.Checked ? "ACTIVO" : "INACTIVO";

            try
            {
                string resultado;

                if (Insert)
                {
                    // Insertar nuevo cliente
                    resultado = CN_Cliente.Guardar(
                        txtnombre.Text.Trim(),
                        txttelefono.Text.Trim(),
                        txtdireccion.Text.Trim(),
                        estado,
                        FrmLogin.UsuarioActual
                    );

                    if (resultado == "OK")
                    {
                        MessageBox.Show("Cliente registrado correctamente",
                            "Sistema Veterinaria",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show(resultado,
                            "Sistema Veterinaria",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        return;
                    }
                }
                else if (Edit)
                {
                    // Editar cliente existente
                    resultado = CN_Cliente.Editar(
                        Convert.ToInt32(txtidcliente.Text),
                        txtnombre.Text.Trim(),
                        txttelefono.Text.Trim(),
                        txtdireccion.Text.Trim(),
                        estado,
                        FrmLogin.UsuarioActual
                    );

                    if (resultado == "OK")
                    {
                        MessageBox.Show("Cliente actualizado correctamente",
                            "Sistema Veterinaria",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show(resultado,
                            "Sistema Veterinaria",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("No se ha definido la operación a realizar",
                        "Sistema Veterinaria",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                // Resetear las banderas
                this.Insert = false;
                this.Edit = false;

                // Buscar el formulario de listado que ya existe
                FrmListadoCliente frm = (FrmListadoCliente)Application.OpenForms["FrmListadoCliente"];

                if (frm != null)
                {
                    frm.Mostrar();
                    frm.Show();
                    frm.BringToFront();
                }
                else
                {
                    FrmListadoCliente nuevoFrm = new FrmListadoCliente();
                    nuevoFrm.Show();
                }

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.StackTrace,
                    "Sistema Veterinaria",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }

        }

        // EVENTO CLICK DEL BOTÓN CANCELAR
        private void btncancelar_Click(object sender, EventArgs e)
        {
            FrmListadoCliente frm = (FrmListadoCliente)Application.OpenForms["FrmListadoCliente"];

            if (frm != null)
            {
                frm.Show();
                frm.BringToFront();
            }
            else
            {
                FrmListadoCliente nuevoFrm = new FrmListadoCliente();
                nuevoFrm.Show();
            }

            this.Close();
        }
    }
}