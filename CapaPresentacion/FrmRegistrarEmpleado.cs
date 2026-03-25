using System;
using System.Drawing;
using System.Windows.Forms;
using CapaNegocio;

namespace CapaPresentacion
{
    /// <summary>
    /// FORMULARIO — REGISTRAR / EDITAR EMPLEADO
    /// Campos: nombre, apellidos, teléfono, dirección, correo,
    ///         tipo de empleado, cédula profesional, especialidad, estado.
    /// El panel de cédula+especialidad solo es visible para VETERINARIO.
    /// Sigue el patrón visual de FrmRegistrarProveedor / FrmRegistrarCliente.
    /// </summary>
    public partial class FrmRegistrarEmpleado : Form
    {
        public bool Insert = false;
        public bool Edit = false;

        public FrmRegistrarEmpleado()
        {
            InitializeComponent();
        }

        // ── Load ──────────────────────────────────────────────────────────────
        private void FrmRegistrarEmpleado_Load(object sender, EventArgs e)
        {
            if (Insert)
            {
                lblTitulo.Text = "➕ Registrar Nuevo Empleado";
                rbtnActivo.Checked = true;
                cmbTipo.SelectedIndex = 0; // VETERINARIO por defecto
            }
            else if (Edit)
            {
                lblTitulo.Text = "✏️ Editar Empleado";
            }

            ActualizarPanelVeterinario();
        }

        // ── Método público para que FrmListadoEmpleados establezca el tipo ────
        public void SetTipoEmpleado(string tipo)
        {
            foreach (object item in cmbTipo.Items)
            {
                if (item.ToString() == tipo)
                {
                    cmbTipo.SelectedItem = item;
                    return;
                }
            }
            if (cmbTipo.Items.Count > 0)
                cmbTipo.SelectedIndex = 0;
        }

        // ── Mostrar/ocultar panel veterinario ─────────────────────────────────
        private void ActualizarPanelVeterinario()
        {
            bool esVet = cmbTipo.SelectedItem?.ToString() == "VETERINARIO";
            panelVeterinario.Visible = esVet;
            lblCedulaReq.Visible = esVet;
        }

        private void cmbTipo_SelectedIndexChanged(object sender, EventArgs e)
            => ActualizarPanelVeterinario();

        // ── Guardar ───────────────────────────────────────────────────────────
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("⚠️ El nombre es obligatorio.",
                    "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombre.Focus(); return;
            }
            if (string.IsNullOrWhiteSpace(txtApellidos.Text))
            {
                MessageBox.Show("⚠️ Los apellidos son obligatorios.",
                    "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtApellidos.Focus(); return;
            }
            if (cmbTipo.SelectedIndex < 0)
            {
                MessageBox.Show("⚠️ Seleccione el tipo de empleado.",
                    "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbTipo.Focus(); return;
            }

            string tipo = cmbTipo.SelectedItem.ToString();
            string estado = rbtnActivo.Checked ? "ACTIVO" : "INACTIVO";

            try
            {
                string resultado;

                if (Insert)
                {
                    resultado = CN_Empleado.Guardar(
                        txtNombre.Text.Trim(),
                        txtApellidos.Text.Trim(),
                        txtTelefono.Text.Trim(),
                        txtDireccion.Text.Trim(),
                        txtCorreo.Text.Trim(),
                        estado,
                        tipo,
                        txtCedula.Text.Trim(),
                        txtEspecialidad.Text.Trim());

                    if (resultado == "OK")
                    {
                        MessageBox.Show(
                            $"✅ Empleado registrado correctamente\n\n" +
                            $"Nombre: {txtNombre.Text.Trim()} {txtApellidos.Text.Trim()}\n" +
                            $"Tipo: {tipo}",
                            "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                        MessageBox.Show("❌ " + resultado,
                            "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (Edit)
                {
                    int id = Convert.ToInt32(txtIdEmpleado.Text);
                    resultado = CN_Empleado.Editar(
                        id,
                        txtNombre.Text.Trim(),
                        txtApellidos.Text.Trim(),
                        txtTelefono.Text.Trim(),
                        txtDireccion.Text.Trim(),
                        txtCorreo.Text.Trim(),
                        estado,
                        tipo,
                        txtCedula.Text.Trim(),
                        txtEspecialidad.Text.Trim());

                    if (resultado == "OK")
                    {
                        MessageBox.Show(
                            $"✅ Empleado actualizado correctamente\n\n" +
                            $"Nombre: {txtNombre.Text.Trim()} {txtApellidos.Text.Trim()}",
                            "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                        MessageBox.Show("❌ " + resultado,
                            "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Error al guardar: " + ex.Message,
                    "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ── Cancelar ─────────────────────────────────────────────────────────
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            bool hayCambios = !string.IsNullOrWhiteSpace(txtNombre.Text)
                           || !string.IsNullOrWhiteSpace(txtApellidos.Text);

            if (hayCambios)
            {
                if (MessageBox.Show("¿Cancelar? Los cambios no guardados se perderán.",
                        "Sistema Veterinaria", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    == DialogResult.Yes)
                {
                    this.DialogResult = DialogResult.Cancel;
                    this.Close();
                }
            }
            else
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }

        // ── Teclas rápidas ────────────────────────────────────────────────────
        private void FrmRegistrarEmpleado_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !btnCancelar.Focused)
            { btnGuardar_Click(sender, e); e.Handled = true; }

            if (e.KeyCode == Keys.Escape)
            { btnCancelar_Click(sender, e); e.Handled = true; }
        }
    }
}