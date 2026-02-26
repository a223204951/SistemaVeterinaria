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
        // BANDERAS PARA INDICAR SI ES INSERCIÓN O EDICIÓN
        public bool Insert = false;
        public bool Edit = false;

        public FrmRegistrarCliente()
        {
            InitializeComponent();
        }

        // EVENTO LOAD DEL FORMULARIO
        private void FrmRegistrarCliente_Load(object sender, EventArgs e)
        {
            // POSICIONAR EL FORMULARIO EN LA ESQUINA SUPERIOR IZQUIERDA
            this.Top = 0;
            this.Left = 0;

            // CAMBIAR EL TÍTULO SEGÚN LA OPERACIÓN
            if (Insert)
            {
                label1.Text = "📝 Registrar Nuevo Cliente";
                rbtnactivo.Checked = true;
            }
            else if (Edit)
            {
                label1.Text = "✏️ Editar Cliente";
            }
        }

        // EVENTO CLICK DEL BOTÓN GUARDAR
        private void btnguardar_Click(object sender, EventArgs e)
        {
            // VALIDAR QUE EL CAMPO DE NOMBRE NO ESTÉ VACÍO
            if (string.IsNullOrWhiteSpace(txtnombre.Text))
            {
                MessageBox.Show("Por favor, ingrese el nombre del cliente",
                    "Sistema Veterinaria",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                txtnombre.Focus();
                return;
            }

            // VALIDAR QUE EL CAMPO DE TELÉFONO NO ESTÉ VACÍO
            if (string.IsNullOrWhiteSpace(txttelefono.Text))
            {
                MessageBox.Show("Por favor, ingrese el teléfono del cliente",
                    "Sistema Veterinaria",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                txttelefono.Focus();
                return;
            }

            // DETERMINAR EL ESTADO SEGÚN EL RADIOBUTTON SELECCIONADO
            string estado = rbtnactivo.Checked ? "ACTIVO" : "INACTIVO";

            try
            {
                string resultado;

                if (Insert)
                {
                    // INSERTAR NUEVO CLIENTE CON EL USUARIO ACTUAL PARA AUDITORÍA
                    resultado = CN_Cliente.Guardar(
                        txtnombre.Text.Trim(),
                        txttelefono.Text.Trim(),
                        txtdireccion.Text.Trim(),
                        estado,
                        FrmLogin.UsuarioActual
                    );

                    if (resultado == "OK")
                    {
                        MessageBox.Show("✅ Cliente registrado correctamente",
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
                    // EDITAR CLIENTE EXISTENTE CON EL USUARIO ACTUAL PARA AUDITORÍA
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
                        MessageBox.Show("✅ Cliente actualizado correctamente",
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

                // RESETEAR LAS BANDERAS
                this.Insert = false;
                this.Edit = false;

                // VOLVER AL LISTADO Y REFRESCARLO AUTOMÁTICAMENTE
                VolverAlListado();
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Error: " + ex.Message + "\n\nDetalles: " + ex.StackTrace,
                    "Sistema Veterinaria",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // EVENTO CLICK DEL BOTÓN CANCELAR
        private void btncancelar_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show(
                "¿Está seguro que desea cancelar?\n\nLos cambios no guardados se perderán.",
                "Sistema Veterinaria",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
            {
                VolverAlListado();
            }
        }

        // MÉTODO PARA VOLVER AL LISTADO DE CLIENTES Y REFRESCARLO
        private void VolverAlListado()
        {
            // BUSCAR EL FORMULARIO DEL MENÚ PRINCIPAL
            FrmMenuPrincipal menuPrincipal = Application.OpenForms.OfType<FrmMenuPrincipal>().FirstOrDefault();

            if (menuPrincipal != null)
            {
                // ESTAMOS DENTRO DEL MENÚ PRINCIPAL
                this.Close();

                // REFRESCAR EL LISTADO PARA QUE APAREZCA EL NUEVO/EDITADO CLIENTE
                menuPrincipal.RefrescarListadoClientes();
            }
            else
            {
                // MODO STANDALONE (por si se usa fuera del menú)
                FrmListadoClientes frm = (FrmListadoClientes)Application.OpenForms["FrmListadoCliente"];

                if (frm != null)
                {
                    frm.Mostrar();
                    frm.Show();
                    frm.BringToFront();
                }
                else
                {
                    FrmListadoClientes nuevoFrm = new FrmListadoClientes();
                    nuevoFrm.Show();
                }

                this.Close();
            }
        }
    }
}