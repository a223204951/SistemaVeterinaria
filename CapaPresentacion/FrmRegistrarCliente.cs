using System;
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

        private void FrmRegistrarCliente_Load(object sender, EventArgs e)
        {
            // *** Centrar en pantalla (no en la esquina) ***
            this.StartPosition = FormStartPosition.CenterScreen;

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

        private void btnguardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtnombre.Text))
            {
                MessageBox.Show("Por favor, ingrese el nombre del cliente",
                    "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtnombre.Focus(); return;
            }
            if (string.IsNullOrWhiteSpace(txttelefono.Text))
            {
                MessageBox.Show("Por favor, ingrese el teléfono del cliente",
                    "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txttelefono.Focus(); return;
            }

            string estado = rbtnactivo.Checked ? "ACTIVO" : "INACTIVO";

            try
            {
                string resultado;

                if (Insert)
                {
                    resultado = CN_Cliente.Guardar(
                        txtnombre.Text.Trim(), txttelefono.Text.Trim(),
                        txtdireccion.Text.Trim(), estado, FrmLogin.UsuarioActual);
                }
                else if (Edit)
                {
                    resultado = CN_Cliente.Editar(
                        Convert.ToInt32(txtidcliente.Text),
                        txtnombre.Text.Trim(), txttelefono.Text.Trim(),
                        txtdireccion.Text.Trim(), estado, FrmLogin.UsuarioActual);
                }
                else
                {
                    MessageBox.Show("No se ha definido la operación",
                        "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (resultado == "OK")
                {
                    string accion = Insert ? "registrado" : "actualizado";
                    MessageBox.Show($"✅ Cliente {accion} correctamente",
                        "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // *** Señal para que FrmListadoClientes refresque ***
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show(resultado,
                        "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Error: " + ex.Message,
                    "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btncancelar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Cancelar? Los cambios no guardados se perderán.",
                    "Sistema Veterinaria", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                == DialogResult.Yes)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }
    }
}