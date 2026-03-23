using System;
using System.Drawing;
using System.Windows.Forms;
using CapaNegocio;

namespace CapaPresentacion
{
    /// <summary>
    /// FORMULARIO REGISTRAR / EDITAR PROVEEDOR
    /// Patrón idéntico a FrmRegistrarProducto / FrmRegistrarCliente
    /// </summary>
    public partial class FrmRegistrarProveedor : Form
    {
        public bool Insert = false;
        public bool Edit = false;

        public FrmRegistrarProveedor()
        {
            InitializeComponent();
        }

        private void FrmRegistrarProveedor_Load(object sender, EventArgs e)
        {
            if (Insert)
            {
                lblTitulo.Text = "➕ Registrar Nuevo Proveedor";
                rbtnActivo.Checked = true;
            }
            else if (Edit)
            {
                lblTitulo.Text = "✏️ Editar Proveedor";
            }
        }

        // ── Guardar ───────────────────────────────────────────────────────────
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("⚠️ El nombre del proveedor es obligatorio.",
                    "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombre.Focus(); return;
            }

            if (!string.IsNullOrWhiteSpace(txtCorreo.Text) && !txtCorreo.Text.Contains("@"))
            {
                MessageBox.Show("⚠️ El correo electrónico no es válido.",
                    "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCorreo.Focus(); return;
            }

            string estado = rbtnActivo.Checked ? "ACTIVO" : "INACTIVO";

            try
            {
                string resultado;

                if (Insert)
                {
                    resultado = CN_Proveedor.Guardar(
                        txtNombre.Text.Trim(),
                        txtTelefono.Text.Trim(),
                        txtDireccion.Text.Trim(),
                        txtCorreo.Text.Trim(),
                        estado);

                    if (resultado == "OK")
                    {
                        MessageBox.Show(
                            $"✅ Proveedor registrado correctamente\n\nNombre: {txtNombre.Text.Trim()}",
                            "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("❌ " + resultado,
                            "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else if (Edit)
                {
                    int idproveedor = Convert.ToInt32(txtIdProveedor.Text);
                    resultado = CN_Proveedor.Editar(
                        idproveedor,
                        txtNombre.Text.Trim(),
                        txtTelefono.Text.Trim(),
                        txtDireccion.Text.Trim(),
                        txtCorreo.Text.Trim(),
                        estado);

                    if (resultado == "OK")
                    {
                        MessageBox.Show(
                            $"✅ Proveedor actualizado correctamente\n\nNombre: {txtNombre.Text.Trim()}",
                            "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("❌ " + resultado,
                            "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Error: " + ex.Message,
                    "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ── Cancelar ─────────────────────────────────────────────────────────
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtNombre.Text))
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
        private void FrmRegistrarProveedor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !btnCancelar.Focused)
            { btnGuardar_Click(sender, e); e.Handled = true; }
            if (e.KeyCode == Keys.Escape)
            { btnCancelar_Click(sender, e); e.Handled = true; }
        }
    }
}