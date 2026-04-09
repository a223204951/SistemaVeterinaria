using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using CapaNegocio;

namespace CapaPresentacion
{
    /// <summary>
    /// FORMULARIO REGISTRAR / EDITAR USUARIO
    /// Permite crear y editar usuarios vinculados a empleados.
    /// </summary>
    public partial class FrmRegistrarUsuario : Form
    {
        public bool Insert = false;
        public bool Edit = false;

        public FrmRegistrarUsuario()
        {
            InitializeComponent();
        }

        private void FrmRegistrarUsuario_Load(object sender, EventArgs e)
        {
            if (Insert)
            {
                lblTitulo.Text = "👤 Registrar Nuevo Usuario";
                rbtnActivo.Checked = true;
                cmbAcceso.SelectedIndex = -1;
                CargarEmpleados();
            }
            else if (Edit)
            {
                lblTitulo.Text = "✏️ Editar Usuario";
                // Mostrar checkbox para cambiar contraseña
                chkCambiarPassword.Visible = true;
                // Bloquear campos de contraseña hasta que se marque el checkbox
                txtPass.Enabled = false;
                txtPassConfirm.Enabled = false;
                txtPass.Text = "••••••••";
                txtPassConfirm.Text = "••••••••";
                lblPass.ForeColor = Color.FromArgb(149, 165, 166);
                lblPassConfirm.ForeColor = Color.FromArgb(149, 165, 166);
                // Cargar empleados (todos los activos para edición)
                CargarEmpleadosEdicion();
            }
        }

        // ── Cargar empleados sin usuario (para nuevo) ─────────────────────────
        private void CargarEmpleados()
        {
            try
            {
                DataTable dt = CN_UsuarioGestion.ListarEmpleadosSinUsuario();
                cmbEmpleado.DataSource = dt;
                cmbEmpleado.DisplayMember = "nombre_completo";
                cmbEmpleado.ValueMember = "idempleado";
                cmbEmpleado.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar empleados: " + ex.Message,
                    "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ── Cargar todos los empleados activos (para edición) ─────────────────
        private void CargarEmpleadosEdicion()
        {
            try
            {
                int idExcluir = 0;
                if (!string.IsNullOrEmpty(txtIdUsuario.Text) &&
                    int.TryParse(txtIdUsuario.Text, out int id))
                    idExcluir = id;

                DataTable dt = CN_UsuarioGestion.ListarEmpleadosSinUsuario(idExcluir);
                cmbEmpleado.DataSource = dt;
                cmbEmpleado.DisplayMember = "nombre_completo";
                cmbEmpleado.ValueMember = "idempleado";
                cmbEmpleado.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar empleados: " + ex.Message,
                    "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ── Método público para preseleccionar empleado al editar ─────────────
        public void SetEmpleado(int idempleado)
        {
            if (cmbEmpleado.DataSource == null) return;
            cmbEmpleado.SelectedValue = idempleado;
        }

        // ── Evento: selección de empleado ─────────────────────────────────────
        private void cmbEmpleado_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbEmpleado.SelectedIndex < 0 || cmbEmpleado.SelectedItem == null)
            {
                lblInfoNombre.Text = "Seleccione un empleado";
                lblInfoNombre.ForeColor = Color.FromArgb(41, 128, 185);
                lblInfoTipo.Text = "";
                return;
            }

            DataRowView drv = cmbEmpleado.SelectedItem as DataRowView;
            if (drv == null) return;

            string nombre = drv["nombre_completo"]?.ToString() ?? "";
            string tipo = drv["tipo_empleado"]?.ToString() ?? "";

            lblInfoNombre.Text = nombre;
            lblInfoTipo.Text = $"Tipo: {tipo}";

            // Sugerir nivel de acceso según tipo de empleado
            if (cmbAcceso.SelectedIndex < 0)
            {
                switch (tipo.ToUpper())
                {
                    case "VETERINARIO": cmbAcceso.SelectedItem = "VETERINARIO"; break;
                    case "CAJERO": cmbAcceso.SelectedItem = "CAJERO"; break;
                    case "ASISTENTE": cmbAcceso.SelectedItem = "ASISTENTE"; break;
                    case "ADMINISTRADOR": cmbAcceso.SelectedItem = "ADMINISTRADOR"; break;
                }
            }
        }

        // ── Evento: selección de nivel de acceso ──────────────────────────────
        private void cmbAcceso_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbAcceso.SelectedItem == null) return;

            // Colorear el ComboBox según el nivel
            switch (cmbAcceso.SelectedItem.ToString())
            {
                case "ADMINISTRADOR":
                    cmbAcceso.ForeColor = Color.FromArgb(142, 68, 173); break;
                case "VETERINARIO":
                    cmbAcceso.ForeColor = Color.FromArgb(22, 160, 133); break;
                case "CAJERO":
                    cmbAcceso.ForeColor = Color.FromArgb(41, 128, 185); break;
                case "ASISTENTE":
                    cmbAcceso.ForeColor = Color.FromArgb(230, 126, 34); break;
                default:
                    cmbAcceso.ForeColor = Color.FromArgb(52, 73, 94); break;
            }
        }

        // ── Evento: checkbox cambiar contraseña ───────────────────────────────
        private void chkCambiarPassword_CheckedChanged(object sender, EventArgs e)
        {
            bool cambiar = chkCambiarPassword.Checked;
            txtPass.Enabled = cambiar;
            txtPassConfirm.Enabled = cambiar;

            if (cambiar)
            {
                txtPass.Text = "";
                txtPassConfirm.Text = "";
                lblPass.ForeColor = Color.FromArgb(52, 73, 94);
                lblPassConfirm.ForeColor = Color.FromArgb(52, 73, 94);
                txtPass.Focus();
            }
            else
            {
                txtPass.Text = "••••••••";
                txtPassConfirm.Text = "••••••••";
                lblPass.ForeColor = Color.FromArgb(149, 165, 166);
                lblPassConfirm.ForeColor = Color.FromArgb(149, 165, 166);
            }
        }

        // ── Guardar ───────────────────────────────────────────────────────────
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string usuario = txtUsuario.Text.Trim();
            string acceso = cmbAcceso.SelectedItem?.ToString() ?? "";
            string estado = rbtnActivo.Checked ? "ACTIVO" : "INACTIVO";
            int idempleado = cmbEmpleado.SelectedValue != null
                                ? Convert.ToInt32(cmbEmpleado.SelectedValue) : 0;

            try
            {
                string resultado;

                if (Insert)
                {
                    resultado = CN_UsuarioGestion.Guardar(
                        usuario, txtPass.Text, txtPassConfirm.Text,
                        acceso, estado, idempleado);

                    if (resultado == "OK")
                    {
                        MessageBox.Show(
                            $"✅ Usuario '{usuario}' registrado correctamente\n\n" +
                            $"Acceso: {acceso}",
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
                    int idusuario = Convert.ToInt32(txtIdUsuario.Text);
                    bool cambiarPass = chkCambiarPassword.Checked;

                    resultado = CN_UsuarioGestion.Editar(
                        idusuario, usuario,
                        cambiarPass ? txtPass.Text : "",
                        cambiarPass ? txtPassConfirm.Text : "",
                        acceso, estado, idempleado, cambiarPass);

                    if (resultado == "OK")
                    {
                        MessageBox.Show(
                            $"✅ Usuario '{usuario}' actualizado correctamente",
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
                MessageBox.Show("❌ Error: " + ex.Message,
                    "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ── Cancelar ─────────────────────────────────────────────────────────
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtUsuario.Text))
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

        private void FrmRegistrarUsuario_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !btnCancelar.Focused)
            { btnGuardar_Click(sender, e); e.Handled = true; }
            if (e.KeyCode == Keys.Escape)
            { btnCancelar_Click(sender, e); e.Handled = true; }
        }
    }
}