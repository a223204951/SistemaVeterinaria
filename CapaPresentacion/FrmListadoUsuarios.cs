using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using CapaNegocio;

namespace CapaPresentacion
{
    /// <summary>
    /// FORMULARIO — GESTIÓN DE USUARIOS
    /// CRUD completo: listar, crear, editar, dar de baja y resetear contraseña.
    /// Solo accesible por ADMINISTRADOR.
    /// Los usuarios dados de baja no pueden hacer login.
    /// </summary>
    public partial class FrmListadoUsuarios : Form
    {
        // Tabla maestra cargada desde BD; los filtros operan en memoria
        private DataTable _tablaMaestra = null;

        public FrmListadoUsuarios()
        {
            InitializeComponent();
        }

        // ── Load ──────────────────────────────────────────────────────────────
        private void FrmListadoUsuarios_Load(object sender, EventArgs e)
        {
            this.Top = 0;
            this.Left = 0;

            if (FrmLogin.RolActual != "ADMINISTRADOR")
            {
                MessageBox.Show(
                    "⚠️ Solo los administradores pueden gestionar usuarios.",
                    "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                // Ocultar botones de edición
                btnNuevo.Visible = false;
                btnEditar.Visible = false;
                btnEliminar.Visible = false;
                btnResetPass.Visible = false;
            }

            CargarDesdeBaseDeDatos();
            AplicarFiltros();
        }

        // ── Cargar desde BD ───────────────────────────────────────────────────
        private void CargarDesdeBaseDeDatos()
        {
            try
            {
                _tablaMaestra = CN_UsuarioGestion.Listar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar usuarios: " + ex.Message,
                    "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _tablaMaestra = new DataTable();
            }
        }

        /// <summary>
        /// Recarga desde BD y reaplica filtros. Llamar tras cada operación CRUD.
        /// </summary>
        public void Mostrar()
        {
            CargarDesdeBaseDeDatos();
            AplicarFiltros();
        }

        // ── Filtros en memoria ────────────────────────────────────────────────
        private void AplicarFiltros()
        {
            if (_tablaMaestra == null) return;

            bool ningunCheck = !chkAdmin.Checked && !chkVet.Checked
                            && !chkCajero.Checked && !chkAsistente.Checked
                            && !chkInactivo.Checked;

            string texto = txtBuscar.Text.Trim().ToUpper();

            DataTable filtrada = _tablaMaestra.Clone();

            foreach (DataRow row in _tablaMaestra.Rows)
            {
                string acceso = (row["acceso"]?.ToString() ?? "").ToUpper();
                string estado = (row["estado"]?.ToString() ?? "").ToUpper();
                string usuario = (row["usuario"]?.ToString() ?? "").ToUpper();
                string empleado = (row["nombre_empleado"]?.ToString() ?? "").ToUpper();

                // Filtro checkbox
                bool pasaCheck = ningunCheck
                    || (chkAdmin.Checked && acceso == "ADMINISTRADOR")
                    || (chkVet.Checked && acceso == "VETERINARIO")
                    || (chkCajero.Checked && acceso == "CAJERO")
                    || (chkAsistente.Checked && acceso == "ASISTENTE")
                    || (chkInactivo.Checked && estado == "INACTIVO");

                if (!pasaCheck) continue;

                // Filtro texto
                bool pasaTexto = string.IsNullOrWhiteSpace(texto)
                    || usuario.Contains(texto)
                    || empleado.Contains(texto);

                if (pasaTexto)
                    filtrada.ImportRow(row);
            }

            dgvUsuarios.DataSource = filtrada;
            ConfigurarColumnas();
            ColorizarFilas();
            ActualizarContador(filtrada.Rows.Count);
        }

        // ── Columnas ──────────────────────────────────────────────────────────
        private void ConfigurarColumnas()
        {
            if (dgvUsuarios.Columns.Count == 0) return;

            if (dgvUsuarios.Columns.Contains("idusuario"))
            { dgvUsuarios.Columns["idusuario"].HeaderText = "ID"; dgvUsuarios.Columns["idusuario"].Width = 50; }

            if (dgvUsuarios.Columns.Contains("usuario"))
            { dgvUsuarios.Columns["usuario"].HeaderText = "Usuario"; dgvUsuarios.Columns["usuario"].Width = 160; }

            if (dgvUsuarios.Columns.Contains("acceso"))
            { dgvUsuarios.Columns["acceso"].HeaderText = "Nivel de Acceso"; dgvUsuarios.Columns["acceso"].Width = 160; }

            if (dgvUsuarios.Columns.Contains("estado"))
            { dgvUsuarios.Columns["estado"].HeaderText = "Estado"; dgvUsuarios.Columns["estado"].Width = 100; }

            if (dgvUsuarios.Columns.Contains("idempleado"))
                dgvUsuarios.Columns["idempleado"].Visible = false;

            if (dgvUsuarios.Columns.Contains("nombre_empleado"))
                dgvUsuarios.Columns["nombre_empleado"].HeaderText = "Empleado Vinculado";

            if (dgvUsuarios.Columns.Contains("tipo_empleado"))
            { dgvUsuarios.Columns["tipo_empleado"].HeaderText = "Tipo Empleado"; dgvUsuarios.Columns["tipo_empleado"].Width = 140; }
        }

        // ── Colorear filas ────────────────────────────────────────────────────
        private void ColorizarFilas()
        {
            foreach (DataGridViewRow row in dgvUsuarios.Rows)
            {
                string estado = row.Cells["estado"]?.Value?.ToString()?.ToUpper() ?? "";
                string acceso = row.Cells["acceso"]?.Value?.ToString()?.ToUpper() ?? "";

                // Inactivos en gris primero
                if (estado == "INACTIVO")
                {
                    row.DefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);
                    row.DefaultCellStyle.ForeColor = Color.FromArgb(149, 165, 166);
                    continue;
                }

                // Activos: colorear por nivel de acceso
                switch (acceso)
                {
                    case "ADMINISTRADOR":
                        row.DefaultCellStyle.BackColor = Color.FromArgb(245, 238, 248);
                        row.DefaultCellStyle.ForeColor = Color.FromArgb(142, 68, 173);
                        break;
                    case "VETERINARIO":
                        row.DefaultCellStyle.BackColor = Color.FromArgb(232, 248, 245);
                        row.DefaultCellStyle.ForeColor = Color.FromArgb(22, 160, 133);
                        break;
                    case "CAJERO":
                        row.DefaultCellStyle.BackColor = Color.FromArgb(235, 245, 251);
                        row.DefaultCellStyle.ForeColor = Color.FromArgb(41, 128, 185);
                        break;
                    case "ASISTENTE":
                        row.DefaultCellStyle.BackColor = Color.FromArgb(253, 245, 230);
                        row.DefaultCellStyle.ForeColor = Color.FromArgb(230, 126, 34);
                        break;
                }
            }
        }

        private void ActualizarContador(int total)
        {
            int activos = 0;
            foreach (DataGridViewRow row in dgvUsuarios.Rows)
                if ((row.Cells["estado"]?.Value?.ToString() ?? "").ToUpper() == "ACTIVO")
                    activos++;
            lblTotal.Text = $"Total: {total} usuario(s) | Activos: {activos}";
        }

        // ── Eventos búsqueda / filtros ────────────────────────────────────────
        private void txtBuscar_TextChanged(object sender, EventArgs e) => AplicarFiltros();
        private void chkFiltro_CheckedChanged(object sender, EventArgs e) => AplicarFiltros();

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtBuscar.Clear();

            // Desuscribir temporalmente para un solo repintado
            chkAdmin.CheckedChanged -= chkFiltro_CheckedChanged;
            chkVet.CheckedChanged -= chkFiltro_CheckedChanged;
            chkCajero.CheckedChanged -= chkFiltro_CheckedChanged;
            chkAsistente.CheckedChanged -= chkFiltro_CheckedChanged;
            chkInactivo.CheckedChanged -= chkFiltro_CheckedChanged;

            chkAdmin.Checked = chkVet.Checked = chkCajero.Checked =
                chkAsistente.Checked = chkInactivo.Checked = false;

            chkAdmin.CheckedChanged += chkFiltro_CheckedChanged;
            chkVet.CheckedChanged += chkFiltro_CheckedChanged;
            chkCajero.CheckedChanged += chkFiltro_CheckedChanged;
            chkAsistente.CheckedChanged += chkFiltro_CheckedChanged;
            chkInactivo.CheckedChanged += chkFiltro_CheckedChanged;

            AplicarFiltros();
        }

        // ── NUEVO ─────────────────────────────────────────────────────────────
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            if (FrmLogin.RolActual != "ADMINISTRADOR")
            {
                MessageBox.Show("No tiene permisos para crear usuarios.",
                    "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var form = new FrmRegistrarUsuario { Insert = true };
            if (form.ShowDialog() == DialogResult.OK) Mostrar();
        }

        // ── EDITAR ────────────────────────────────────────────────────────────
        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (FrmLogin.RolActual != "ADMINISTRADOR")
            {
                MessageBox.Show("No tiene permisos para editar usuarios.",
                    "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dgvUsuarios.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione un usuario para editar.",
                    "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow row = dgvUsuarios.CurrentRow;
            var form = new FrmRegistrarUsuario { Edit = true };

            form.txtIdUsuario.Text = row.Cells["idusuario"].Value.ToString();
            form.txtUsuario.Text = row.Cells["usuario"].Value?.ToString() ?? "";
            form.cmbAcceso.SelectedItem = row.Cells["acceso"].Value?.ToString();

            string estado = row.Cells["estado"].Value?.ToString()?.ToUpper() ?? "ACTIVO";
            form.rbtnActivo.Checked = (estado == "ACTIVO");
            form.rbtnInactivo.Checked = (estado != "ACTIVO");

            // Preseleccionar empleado después de que el form cargue sus combos
            form.Load += (s2, e2) =>
            {
                if (row.Cells["idempleado"].Value != DBNull.Value &&
                    row.Cells["idempleado"].Value != null)
                {
                    int idEmp = Convert.ToInt32(row.Cells["idempleado"].Value);
                    form.SetEmpleado(idEmp);
                }
            };

            if (form.ShowDialog() == DialogResult.OK) Mostrar();
        }

        // ── DAR DE BAJA ───────────────────────────────────────────────────────
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (FrmLogin.RolActual != "ADMINISTRADOR")
            {
                MessageBox.Show("No tiene permisos para dar de baja usuarios.",
                    "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dgvUsuarios.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione un usuario.",
                    "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow row = dgvUsuarios.CurrentRow;
            string usuario = row.Cells["usuario"].Value?.ToString() ?? "";
            string estado = row.Cells["estado"].Value?.ToString()?.ToUpper() ?? "";

            // No permitir dar de baja al usuario actualmente logueado
            if (usuario.Equals(FrmLogin.UsuarioActual, System.StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show(
                    "⚠️ No puedes dar de baja tu propio usuario mientras estás logueado.",
                    "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (estado == "INACTIVO")
            {
                MessageBox.Show("Este usuario ya está inactivo.",
                    "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show(
                    $"¿Dar de baja al usuario '{usuario}'?\n\n" +
                    "El usuario quedará INACTIVO y no podrá iniciar sesión.",
                    "Sistema Veterinaria", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                == DialogResult.Yes)
            {
                try
                {
                    int id = Convert.ToInt32(row.Cells["idusuario"].Value);
                    string resultado = CN_UsuarioGestion.Eliminar(id);

                    if (resultado == "OK")
                    {
                        MessageBox.Show(
                            $"✅ Usuario '{usuario}' dado de baja.\nYa no podrá iniciar sesión.",
                            "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Mostrar();
                    }
                    else
                        MessageBox.Show("❌ " + resultado,
                            "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message,
                        "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // ── RESETEAR CONTRASEÑA ───────────────────────────────────────────────
        private void btnResetPass_Click(object sender, EventArgs e)
        {
            if (FrmLogin.RolActual != "ADMINISTRADOR")
            {
                MessageBox.Show("No tiene permisos para resetear contraseñas.",
                    "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dgvUsuarios.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione un usuario.",
                    "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow row = dgvUsuarios.CurrentRow;
            string usuario = row.Cells["usuario"].Value?.ToString() ?? "";
            int id = Convert.ToInt32(row.Cells["idusuario"].Value);

            // Diálogo de nueva contraseña
            using (Form dlg = CrearDialogoResetPass(usuario))
            {
                if (dlg.ShowDialog(this) != DialogResult.OK) return;

                TextBox txtNew = (TextBox)dlg.Controls["txtNew"];
                TextBox txtConf = (TextBox)dlg.Controls["txtConf"];

                string resultado = CN_UsuarioGestion.ResetPassword(
                    id, txtNew.Text, txtConf.Text);

                if (resultado == "OK")
                    MessageBox.Show(
                        $"✅ Contraseña del usuario '{usuario}' actualizada.",
                        "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("❌ " + resultado,
                        "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ── Diálogo de reset de contraseña ────────────────────────────────────
        private Form CrearDialogoResetPass(string usuario)
        {
            var dlg = new Form
            {
                Text = $"Resetear contraseña — {usuario}",
                Size = new Size(380, 230),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false,
                BackColor = Color.FromArgb(236, 240, 241)
            };

            var lblTitle = new Label
            {
                Text = $"Nueva contraseña para: {usuario}",
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = Color.FromArgb(52, 73, 94),
                Location = new Point(15, 15),
                AutoSize = true
            };

            var lblN = new Label
            {
                Text = "Nueva contraseña:",
                AutoSize = true,
                Font = new Font("Segoe UI", 9F),
                Location = new Point(15, 42)
            };
            var txtNew = new TextBox
            {
                Name = "txtNew",
                Font = new Font("Segoe UI", 10F),
                Location = new Point(15, 60),
                Size = new Size(340, 28),
                PasswordChar = '●',
                MaxLength = 20
            };

            var lblC = new Label
            {
                Text = "Confirmar contraseña:",
                AutoSize = true,
                Font = new Font("Segoe UI", 9F),
                Location = new Point(15, 98)
            };
            var txtConf = new TextBox
            {
                Name = "txtConf",
                Font = new Font("Segoe UI", 10F),
                Location = new Point(15, 116),
                Size = new Size(340, 28),
                PasswordChar = '●',
                MaxLength = 20
            };

            var btnOk = new Button
            {
                Text = "✅ Guardar",
                Location = new Point(100, 155),
                Size = new Size(120, 36),
                BackColor = Color.FromArgb(46, 204, 113),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                DialogResult = DialogResult.OK
            };
            btnOk.FlatAppearance.BorderSize = 0;

            var btnCx = new Button
            {
                Text = "✗ Cancelar",
                Location = new Point(228, 155),
                Size = new Size(127, 36),
                BackColor = Color.FromArgb(149, 165, 166),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                DialogResult = DialogResult.Cancel
            };
            btnCx.FlatAppearance.BorderSize = 0;

            dlg.Controls.AddRange(new System.Windows.Forms.Control[] {
                lblTitle, lblN, txtNew, lblC, txtConf, btnOk, btnCx });
            dlg.AcceptButton = btnOk;
            dlg.CancelButton = btnCx;
            return dlg;
        }

        // ── Doble click = editar ──────────────────────────────────────────────
        private void dgvUsuarios_DoubleClick(object sender, EventArgs e)
        {
            if (FrmLogin.RolActual == "ADMINISTRADOR")
                btnEditar_Click(sender, e);
        }
    }
}