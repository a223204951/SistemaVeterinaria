using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using CapaNegocio;

namespace CapaPresentacion
{
    /// <summary>
    /// FORMULARIO — GESTIÓN DE EMPLEADOS
    ///
    /// CORRECCIONES v3:
    /// • Filtro de tipo/estado: comparación case-insensitive con ToUpper() para
    ///   que funcione con empleados que tengan 'Veterinario', 'VETERINARIO', etc.
    /// • Búsqueda por ID: usa LIKE (parcial) en vez de igualdad exacta, de modo
    ///   que al escribir "1" aparecen IDs 1, 10, 11, 12…
    ///
    /// • Búsqueda por Nombre/Apellido  ó  por ID  (radio buttons idénticos a
    ///   FrmListadoClientes / FrmListadoMascotas).
    /// • Filtros por tipo/estado con CheckBoxes (selección múltiple).
    ///   Sin ningún CheckBox marcado → se muestran TODOS.
    /// • Coloreado: verde=veterinario, azul=cajero, naranja=asistente,
    ///   morado=administrador, gris=inactivo.
    /// </summary>
    public partial class FrmListadoEmpleados : Form
    {
        private CN_Usuario _cnUsuario = new CN_Usuario();

        // Tabla completa desde BD; los filtros actúan sobre ella en memoria
        private DataTable _tablaMaestra = null;

        public FrmListadoEmpleados()
        {
            InitializeComponent();
        }

        // ── Load ──────────────────────────────────────────────────────────────
        private void FrmListadoEmpleados_Load(object sender, EventArgs e)
        {
            this.Top = 0;
            this.Left = 0;
            CargarDesdeBaseDeDatos();
            AplicarFiltros();
            ConfigurarPermisos();
        }

        // ── Permisos ──────────────────────────────────────────────────────────
        private void ConfigurarPermisos()
        {
            string rol = FrmLogin.RolActual;
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

        // ── Carga desde BD ────────────────────────────────────────────────────
        private void CargarDesdeBaseDeDatos()
        {
            try
            {
                _tablaMaestra = CN_Empleado.Listar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar empleados: " + ex.Message,
                    "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _tablaMaestra = new DataTable();
            }
        }

        /// <summary>
        /// Recarga desde BD y vuelve a aplicar filtros.
        /// Llamar tras crear / editar / eliminar un empleado.
        /// </summary>
        public void Mostrar()
        {
            CargarDesdeBaseDeDatos();
            AplicarFiltros();
        }

        // ── Motor de filtros ──────────────────────────────────────────────────
        /// <summary>
        /// Combina la búsqueda de texto con los CheckBoxes de tipo/estado
        /// y actualiza el DataGridView sin volver a la BD.
        ///
        /// CORRECCIÓN CLAVE: la comparación de tipo_empleado se hace con
        /// ToUpper() para que funcione con valores 'Veterinario', 'VETERINARIO',
        /// 'veterinario', etc. (los empleados previos podían tener casing distinto).
        /// </summary>
        private void AplicarFiltros()
        {
            if (_tablaMaestra == null) return;

            bool ningunCheckbox = !chkVeterinario.Checked && !chkCajero.Checked
                               && !chkAsistente.Checked && !chkAdministrador.Checked
                               && !chkInactivo.Checked;

            string texto = txtBuscar.Text.Trim().ToUpper();

            DataTable filtrada = _tablaMaestra.Clone(); // misma estructura, sin filas

            foreach (DataRow row in _tablaMaestra.Rows)
            {
                // ── Normalizar tipo y estado a mayúsculas para comparar ────────
                // Esto resuelve el problema con empleados que tenían 'Veterinario'
                // en lugar de 'VETERINARIO' porque fueron insertados con SPs viejos.
                string tipoUpper = (row["tipo_empleado"]?.ToString() ?? "").ToUpper();
                string estadoUpper = (row["estado"]?.ToString() ?? "").ToUpper();

                // ── Filtro por CheckBox ────────────────────────────────────────
                bool pasaTipo = ningunCheckbox
                    || (chkVeterinario.Checked && tipoUpper == "VETERINARIO")
                    || (chkCajero.Checked && tipoUpper == "CAJERO")
                    || (chkAsistente.Checked && tipoUpper == "ASISTENTE")
                    || (chkAdministrador.Checked && tipoUpper == "ADMINISTRADOR")
                    || (chkInactivo.Checked && estadoUpper == "INACTIVO");

                if (!pasaTipo) continue;

                // ── Filtro por texto de búsqueda ──────────────────────────────
                bool pasaTexto = true;
                if (!string.IsNullOrWhiteSpace(texto))
                {
                    if (rbtNombre.Checked)
                    {
                        // Búsqueda parcial en nombre O apellidos (case-insensitive)
                        string nombre = (row["nombre"]?.ToString() ?? "").ToUpper();
                        string apellidos = (row["apellidos"]?.ToString() ?? "").ToUpper();
                        pasaTexto = nombre.Contains(texto) || apellidos.Contains(texto);
                    }
                    else // rbtId — búsqueda PARCIAL: "1" encuentra 1, 10, 11, 12...
                    {
                        string id = (row["idempleado"]?.ToString() ?? "");
                        // StartsWith da mejor UX que Contains para IDs numéricos:
                        // escribir "1" muestra 1, 10, 11... pero no 21, 31...
                        // Si prefieren Contains basta con cambiar la siguiente línea.
                        pasaTexto = id.StartsWith(texto);
                    }
                }

                if (pasaTexto)
                    filtrada.ImportRow(row);
            }

            dgvEmpleados.DataSource = filtrada;
            ConfigurarColumnas();
            ColorizarFilas();
            ActualizarContador(filtrada.Rows.Count);
        }

        // ── Columnas ──────────────────────────────────────────────────────────
        private void ConfigurarColumnas()
        {
            if (dgvEmpleados.Columns.Count == 0) return;

            foreach (string col in new[] { "cedula_profesional", "nombre_completo" })
                if (dgvEmpleados.Columns.Contains(col))
                    dgvEmpleados.Columns[col].Visible = false;

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

        // ── Colorear filas ────────────────────────────────────────────────────
        // También normaliza con ToUpper() para que el color se aplique
        // independientemente del casing almacenado en BD.
        private void ColorizarFilas()
        {
            foreach (DataGridViewRow row in dgvEmpleados.Rows)
            {
                if (row.Cells["estado"].Value == null) continue;

                string estadoUpper = row.Cells["estado"].Value.ToString().ToUpper();
                string tipoUpper = dgvEmpleados.Columns.Contains("tipo_empleado") &&
                                     row.Cells["tipo_empleado"].Value != null
                                         ? row.Cells["tipo_empleado"].Value.ToString().ToUpper()
                                         : "";

                if (estadoUpper == "INACTIVO")
                {
                    row.DefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);
                    row.DefaultCellStyle.ForeColor = Color.FromArgb(149, 165, 166);
                    continue;
                }

                switch (tipoUpper)
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

        private void ActualizarContador(int total)
        {
            int activos = 0;
            foreach (DataGridViewRow row in dgvEmpleados.Rows)
                if ((row.Cells["estado"].Value?.ToString() ?? "").ToUpper() == "ACTIVO")
                    activos++;

            lblTotal.Text = $"Mostrando: {total} empleado(s) | Activos: {activos}";
        }

        // ── Eventos de búsqueda ───────────────────────────────────────────────
        private void txtBuscar_TextChanged(object sender, EventArgs e) => AplicarFiltros();
        private void btnBuscar_Click(object sender, EventArgs e) => AplicarFiltros();

        // ── Eventos de CheckBoxes ─────────────────────────────────────────────
        private void chkFiltro_CheckedChanged(object sender, EventArgs e) => AplicarFiltros();

        // ── Botón Limpiar ─────────────────────────────────────────────────────
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtBuscar.Clear();
            rbtNombre.Checked = true;

            // Desuscribir temporalmente para hacer una sola llamada a AplicarFiltros
            chkVeterinario.CheckedChanged -= chkFiltro_CheckedChanged;
            chkCajero.CheckedChanged -= chkFiltro_CheckedChanged;
            chkAsistente.CheckedChanged -= chkFiltro_CheckedChanged;
            chkAdministrador.CheckedChanged -= chkFiltro_CheckedChanged;
            chkInactivo.CheckedChanged -= chkFiltro_CheckedChanged;

            chkVeterinario.Checked = false;
            chkCajero.Checked = false;
            chkAsistente.Checked = false;
            chkAdministrador.Checked = false;
            chkInactivo.Checked = false;

            chkVeterinario.CheckedChanged += chkFiltro_CheckedChanged;
            chkCajero.CheckedChanged += chkFiltro_CheckedChanged;
            chkAsistente.CheckedChanged += chkFiltro_CheckedChanged;
            chkAdministrador.CheckedChanged += chkFiltro_CheckedChanged;
            chkInactivo.CheckedChanged += chkFiltro_CheckedChanged;

            AplicarFiltros();
        }

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

            string estado = row.Cells["estado"].Value?.ToString() ?? "ACTIVO";
            form.rbtnActivo.Checked = (estado.ToUpper() == "ACTIVO");
            form.rbtnInactivo.Checked = (estado.ToUpper() != "ACTIVO");

            // Normalizar tipo a mayúsculas para que SetTipoEmpleado lo encuentre
            string tipo = (row.Cells["tipo_empleado"].Value?.ToString() ?? "ASISTENTE").ToUpper();
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
    }
}