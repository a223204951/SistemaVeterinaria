using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using CapaNegocio;

namespace CapaPresentacion
{
    /// <summary>
    /// FORMULARIO — GESTIÓN DE EMPLEADOS
    /// Listado principal con búsqueda por nombre/ID, CRUD completo
    /// y coloreado por tipo de empleado y estado.
    /// Sigue el patrón visual de FrmListadoClientes / FrmListadoProveedores.
    /// </summary>
    public partial class FrmListadoEmpleados : Form
    {
        private CN_Usuario _cnUsuario = new CN_Usuario();

        public FrmListadoEmpleados()
        {
            InitializeComponent();
        }

        // ── Load ──────────────────────────────────────────────────────────────
        private void FrmListadoEmpleados_Load(object sender, EventArgs e)
        {
            this.Top = 0;
            this.Left = 0;
            Mostrar();
            ConfigurarPermisos();
        }

        // ── Permisos ──────────────────────────────────────────────────────────
        private void ConfigurarPermisos()
        {
            string rol = FrmLogin.RolActual;

            // Solo ADMINISTRADOR puede crear, editar y eliminar empleados
            bool esAdmin = (rol == "ADMINISTRADOR");
            btnNuevo.Visible = esAdmin || TryPerm(rol, "crear");
            btnEditar.Visible = esAdmin || TryPerm(rol, "editar");
            btnEliminar.Visible = esAdmin || TryPerm(rol, "eliminar");
        }

        private bool TryPerm(string rol, string tipo)
        {
            try
            {
                switch (tipo)
                {
                    case "crear": return _cnUsuario.PuedeCrear(rol, "Empleados");
                    case "editar": return _cnUsuario.PuedeEditar(rol, "Empleados");
                    case "eliminar": return _cnUsuario.PuedeEliminar(rol, "Empleados");
                    default: return false;
                }
            }
            catch { return false; }
        }

        // ── Cargar / Mostrar ──────────────────────────────────────────────────
        public void Mostrar()
        {
            try
            {
                DataTable datos = CN_Empleado.Listar();
                dgvEmpleados.DataSource = datos;
                ConfigurarColumnas();
                ColorizarFilas();
                ActualizarContador();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar empleados: " + ex.Message,
                    "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurarColumnas()
        {
            if (dgvEmpleados.Columns.Count == 0) return;

            // Ocultar columnas de detalle interno
            foreach (string col in new[] { "cedula_profesional" })
                if (dgvEmpleados.Columns.Contains(col))
                    dgvEmpleados.Columns[col].Visible = false;

            // Renombrar encabezados
            void R(string c, string h, int w = 0)
            {
                if (!dgvEmpleados.Columns.Contains(c)) return;
                dgvEmpleados.Columns[c].HeaderText = h;
                if (w > 0) dgvEmpleados.Columns[c].Width = w;
            }

            R("idempleado", "ID", 50);
            R("nombre", "Nombre", 140);
            R("apellidos", "Apellidos", 160);
            R("telefono", "Teléfono", 115);
            R("direccion", "Dirección", 180);
            R("correo", "Correo", 180);
            R("estado", "Estado", 90);
            R("tipo_empleado", "Tipo", 120);
            R("especialidad", "Especialidad", 140);
        }

        private void ColorizarFilas()
        {
            foreach (DataGridViewRow row in dgvEmpleados.Rows)
            {
                if (row.Cells["estado"].Value == null) continue;

                string estado = row.Cells["estado"].Value.ToString();
                string tipo = dgvEmpleados.Columns.Contains("tipo_empleado") &&
                                row.Cells["tipo_empleado"].Value != null
                                    ? row.Cells["tipo_empleado"].Value.ToString()
                                    : "";

                // Inactivos en gris
                if (estado == "INACTIVO")
                {
                    row.DefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);
                    row.DefaultCellStyle.ForeColor = Color.FromArgb(149, 165, 166);
                    continue;
                }

                // Activos: color según tipo
                switch (tipo)
                {
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
                    case "ADMINISTRADOR":
                        row.DefaultCellStyle.BackColor = Color.FromArgb(245, 238, 248);
                        row.DefaultCellStyle.ForeColor = Color.FromArgb(142, 68, 173);
                        break;
                }
            }
        }

        private void ActualizarContador()
        {
            int total = dgvEmpleados.Rows.Count;
            int activos = 0;
            foreach (DataGridViewRow row in dgvEmpleados.Rows)
                if (row.Cells["estado"].Value?.ToString() == "ACTIVO") activos++;

            lblTotal.Text = $"Total: {total} empleados | Activos: {activos}";
        }

        // ── Búsqueda en tiempo real ────────────────────────────────────────────
        private void Buscar()
        {
            try
            {
                string texto = txtBuscar.Text.Trim();
                if (string.IsNullOrWhiteSpace(texto)) { Mostrar(); return; }

                DataTable datos = rbtNombre.Checked
                    ? CN_Empleado.BuscarNombre(texto)
                    : CN_Empleado.BuscarId(texto);

                dgvEmpleados.DataSource = datos;
                ConfigurarColumnas();
                ColorizarFilas();
                ActualizarContador();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en la búsqueda: " + ex.Message,
                    "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e) => Buscar();
        private void btnBuscar_Click(object sender, EventArgs e) => Buscar();

        // ── CRUD ──────────────────────────────────────────────────────────────
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            FrmRegistrarEmpleado form = new FrmRegistrarEmpleado { Insert = true };
            if (form.ShowDialog() == DialogResult.OK)
                Mostrar();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvEmpleados.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione un empleado para editar.",
                    "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow row = dgvEmpleados.CurrentRow;
            FrmRegistrarEmpleado form = new FrmRegistrarEmpleado { Edit = true };

            form.txtIdEmpleado.Text = row.Cells["idempleado"].Value.ToString();
            form.txtNombre.Text = row.Cells["nombre"].Value?.ToString() ?? "";
            form.txtApellidos.Text = row.Cells["apellidos"].Value?.ToString() ?? "";
            form.txtTelefono.Text = row.Cells["telefono"].Value?.ToString() ?? "";
            form.txtDireccion.Text = row.Cells["direccion"].Value?.ToString() ?? "";
            form.txtCorreo.Text = row.Cells["correo"].Value?.ToString() ?? "";
            form.txtCedula.Text = dgvEmpleados.Columns.Contains("cedula_profesional")
                                           ? row.Cells["cedula_profesional"].Value?.ToString() ?? ""
                                           : "";
            form.txtEspecialidad.Text = row.Cells["especialidad"].Value?.ToString() ?? "";

            // Estado
            string estado = row.Cells["estado"].Value?.ToString() ?? "ACTIVO";
            form.rbtnActivo.Checked = (estado == "ACTIVO");
            form.rbtnInactivo.Checked = (estado != "ACTIVO");

            // Tipo de empleado
            string tipo = row.Cells["tipo_empleado"].Value?.ToString() ?? "ASISTENTE";
            form.SetTipoEmpleado(tipo);

            if (form.ShowDialog() == DialogResult.OK)
                Mostrar();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvEmpleados.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione un empleado para dar de baja.",
                    "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string nombre = dgvEmpleados.CurrentRow.Cells["nombre"].Value?.ToString() ?? "";
            string apellidos = dgvEmpleados.CurrentRow.Cells["apellidos"].Value?.ToString() ?? "";

            if (MessageBox.Show(
                    $"¿Dar de baja al empleado?\n\n{nombre} {apellidos}\n\n" +
                    "El empleado quedará INACTIVO pero se conservará su historial.",
                    "Sistema Veterinaria", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                == DialogResult.Yes)
            {
                try
                {
                    int id = Convert.ToInt32(dgvEmpleados.CurrentRow.Cells["idempleado"].Value);
                    string res = CN_Empleado.Eliminar(id);

                    if (res == "OK")
                    {
                        MessageBox.Show("✅ Empleado dado de baja correctamente.",
                            "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Mostrar();
                    }
                    else
                        MessageBox.Show("❌ " + res,
                            "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message,
                        "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Doble clic = editar
        private void dgvEmpleados_DoubleClick(object sender, EventArgs e)
        {
            if (TryPerm(FrmLogin.RolActual, "editar") || FrmLogin.RolActual == "ADMINISTRADOR")
                btnEditar_Click(sender, e);
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtBuscar.Clear();
            rbtNombre.Checked = true;
            Mostrar();
        }
    }
}