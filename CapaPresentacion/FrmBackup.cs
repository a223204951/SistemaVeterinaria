using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using CapaDatos;
using CapaNegocio;

namespace CapaPresentacion
{
    /// <summary>
    /// FORMULARIO DE BACKUP Y RESTAURACIÓN — SOLO ADMINISTRADOR
    ///
    /// FUNCIONES:
    ///   A) Backup SQL a archivo  — segmentado por módulos y filtro de fecha
    ///   B) Snapshot Automático   — guarda el backup directo en la BD (sin archivo)
    ///   C) Volver a la Actualidad — aplica el snapshot más reciente (o el elegido)
    ///   D) Exportar CSV          — un .csv por tabla en una carpeta
    ///   E) Importar/Restaurar SQL desde archivo externo
    ///   F) Importar CSV desde archivo externo
    /// </summary>
    public partial class FrmBackup : Form
    {
        // ── Módulos disponibles con sus tablas asociadas ──────────────────────
        private static readonly Dictionary<string, string[]> ModulosTablas =
            new Dictionary<string, string[]>(StringComparer.OrdinalIgnoreCase)
            {
                { "Clientes",    new[] { "cliente",   "auditoria_cliente" } },
                { "Mascotas",    new[] { "mascota" } },
                { "Empleados",   new[] { "empleado" } },
                { "Usuarios",    new[] { "usuario",   "sesiones_usuario", "permisos_rol" } },
                { "Productos",   new[] { "producto",  "categoria_producto", "historial_precios", "movimiento_stock" } },
                { "Proveedores", new[] { "proveedor", "proveedor_producto" } },
                { "Ventas",      new[] { "venta",     "detalle_venta" } },
                { "Compras",     new[] { "compra",    "detalle_compra" } },
                { "Citas",       new[] { "cita",      "consulta", "pago" } },
            };

        // Tabla interna donde se guardan los snapshots automáticos
        private const string TABLA_SNAPSHOTS = "_vet_snapshots";

        public FrmBackup()
        {
            InitializeComponent();
        }

        // ─────────────────────────────────────────────────────────────────────
        // LOAD
        // ─────────────────────────────────────────────────────────────────────
        private void FrmBackup_Load(object sender, EventArgs e)
        {
            if (FrmLogin.RolActual != "ADMINISTRADOR")
            {
                MessageBox.Show("⚠️ Solo los administradores pueden acceder a este módulo.",
                    "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            dtpFechaInicio.Value = DateTime.Now.AddMonths(-1);
            dtpFechaFin.Value = DateTime.Now;

            CargarModulos();
            ActualizarEstadoFechas();
            GarantizarTablaSnapshots();
            RefrescarListaSnapshots();
        }

        // ─────────────────────────────────────────────────────────────────────
        // MÓDULOS (checkboxes)
        // ─────────────────────────────────────────────────────────────────────
        private void CargarModulos()
        {
            flpModulos.Controls.Clear();

            CheckBox chkTodos = new CheckBox
            {
                Text = "✅ Seleccionar todos",
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = Color.FromArgb(52, 73, 94),
                Width = 220,
                Tag = "TODOS"
            };
            chkTodos.CheckedChanged += ChkTodos_CheckedChanged;
            flpModulos.Controls.Add(chkTodos);

            Panel sep = new Panel
            {
                Width = 220,
                Height = 1,
                BackColor = Color.FromArgb(220, 220, 220),
                Margin = new Padding(3, 4, 3, 4)
            };
            flpModulos.Controls.Add(sep);

            foreach (string modulo in ModulosTablas.Keys)
            {
                CheckBox chk = new CheckBox
                {
                    Text = modulo,
                    Font = new Font("Segoe UI", 9F),
                    ForeColor = Color.FromArgb(52, 73, 94),
                    Width = 220,
                    Checked = true,
                    Tag = modulo
                };
                flpModulos.Controls.Add(chk);
            }
        }

        private void ChkTodos_CheckedChanged(object sender, EventArgs e)
        {
            bool estado = ((CheckBox)sender).Checked;
            foreach (Control ctrl in flpModulos.Controls)
                if (ctrl is CheckBox chk && chk.Tag?.ToString() != "TODOS")
                    chk.Checked = estado;
        }

        // ─────────────────────────────────────────────────────────────────────
        // FILTRO DE FECHAS
        // ─────────────────────────────────────────────────────────────────────
        private void rbtnHastaHoy_CheckedChanged(object sender, EventArgs e) => ActualizarEstadoFechas();
        private void rbtnRangoFechas_CheckedChanged(object sender, EventArgs e) => ActualizarEstadoFechas();
        private void rbtnSinFiltro_CheckedChanged(object sender, EventArgs e) => ActualizarEstadoFechas();

        private void ActualizarEstadoFechas()
        {
            bool esRango = rbtnRangoFechas.Checked;
            bool esHastaHoy = rbtnHastaHoy.Checked;

            dtpFechaInicio.Enabled = esRango || esHastaHoy;
            dtpFechaFin.Enabled = esRango;
            lblFechaInicio.Enabled = esRango || esHastaHoy;
            lblFechaFin.Enabled = esRango;

            if (esHastaHoy) dtpFechaFin.Value = DateTime.Now;
        }

        private List<string> ObtenerModulosSeleccionados()
        {
            var lista = new List<string>();
            foreach (Control ctrl in flpModulos.Controls)
                if (ctrl is CheckBox chk && chk.Checked && chk.Tag?.ToString() != "TODOS")
                    lista.Add(chk.Tag.ToString());
            return lista;
        }

        // ═════════════════════════════════════════════════════════════════════
        // A) BACKUP SQL A ARCHIVO EXTERNO
        // ═════════════════════════════════════════════════════════════════════
        private void btnBackupSQL_Click(object sender, EventArgs e)
        {
            List<string> modulos = ObtenerModulosSeleccionados();
            if (modulos.Count == 0)
            {
                MessageBox.Show("⚠️ Seleccione al menos un módulo para respaldar.",
                    "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SaveFileDialog dlg = new SaveFileDialog
            {
                Title = "Guardar respaldo SQL",
                Filter = "Archivo SQL|*.sql",
                FileName = $"VeterinariaBD_Backup_{DateTime.Now:yyyyMMdd_HHmm}.sql",
                DefaultExt = "sql"
            };
            if (dlg.ShowDialog() != DialogResult.OK) return;

            try
            {
                btnBackupSQL.Enabled = false;
                btnBackupSQL.Text = "⏳ Generando...";
                progressBar.Visible = true;
                progressBar.Value = 0;
                progressBar.Maximum = modulos.Count;

                StringBuilder sb = GenerarScriptSQL(modulos, out int totalRegistros);
                File.WriteAllText(dlg.FileName, sb.ToString(), Encoding.UTF8);

                progressBar.Visible = false;
                btnBackupSQL.Enabled = true;
                btnBackupSQL.Text = "💾 Backup SQL";

                AgregarLog($"✅ Backup SQL generado: {Path.GetFileName(dlg.FileName)} | " +
                           $"Módulos: {modulos.Count} | Registros: {totalRegistros}");

                MessageBox.Show(
                    $"✅ Respaldo SQL generado correctamente.\n\n" +
                    $"Archivo:    {Path.GetFileName(dlg.FileName)}\n" +
                    $"Módulos:    {modulos.Count}\n" +
                    $"Registros:  {totalRegistros}\n" +
                    $"Tamaño:     {new FileInfo(dlg.FileName).Length / 1024.0:N1} KB",
                    "Backup completado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                progressBar.Visible = false;
                btnBackupSQL.Enabled = true;
                btnBackupSQL.Text = "💾 Backup SQL";
                AgregarLog($"❌ Error en Backup SQL: {ex.Message}");
                MessageBox.Show("❌ " + ex.Message,
                    "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ═════════════════════════════════════════════════════════════════════
        // B) SNAPSHOT AUTOMÁTICO — guarda en la BD, sin archivo externo
        // ═════════════════════════════════════════════════════════════════════

        /// <summary>
        /// Crea la tabla _vet_snapshots en la BD si no existe todavía.
        /// </summary>
        private void GarantizarTablaSnapshots()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(CD_Conexion.Conn))
                {
                    con.Open();
                    string sql = $@"
                        IF NOT EXISTS (
                            SELECT 1 FROM INFORMATION_SCHEMA.TABLES
                            WHERE TABLE_NAME = '{TABLA_SNAPSHOTS}'
                        )
                        CREATE TABLE [dbo].[{TABLA_SNAPSHOTS}] (
                            [id]             INT IDENTITY(1,1) PRIMARY KEY,
                            [etiqueta]       NVARCHAR(100) NOT NULL,
                            [fecha_creacion] DATETIME      NOT NULL DEFAULT GETDATE(),
                            [modulos]        NVARCHAR(500) NOT NULL,
                            [script_sql]     NVARCHAR(MAX) NOT NULL
                        );";
                    new SqlCommand(sql, con).ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                AgregarLog($"⚠️ No se pudo garantizar la tabla de snapshots: {ex.Message}");
            }
        }

        private void btnSnapshotAuto_Click(object sender, EventArgs e)
        {
            List<string> modulos = ObtenerModulosSeleccionados();
            if (modulos.Count == 0)
            {
                MessageBox.Show("⚠️ Seleccione al menos un módulo para el snapshot.",
                    "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string etiqueta = MostrarDialogoEtiqueta();
            if (etiqueta == null) return; // usuario canceló

            try
            {
                btnSnapshotAuto.Enabled = false;
                btnSnapshotAuto.Text = "⏳ Guardando...";
                progressBar.Visible = true;
                progressBar.Value = 0;
                progressBar.Maximum = modulos.Count;

                StringBuilder sb = GenerarScriptSQL(modulos, out int totalRegistros);
                string scriptCompleto = sb.ToString();
                string modulosStr = string.Join(", ", modulos);

                using (SqlConnection con = new SqlConnection(CD_Conexion.Conn))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(
                        $"INSERT INTO [dbo].[{TABLA_SNAPSHOTS}] (etiqueta, modulos, script_sql) " +
                        "VALUES (@et, @mod, @sql)", con)
                    { CommandTimeout = 120 };
                    cmd.Parameters.AddWithValue("@et", etiqueta);
                    cmd.Parameters.AddWithValue("@mod", modulosStr);
                    cmd.Parameters.AddWithValue("@sql", scriptCompleto);
                    cmd.ExecuteNonQuery();
                }

                progressBar.Visible = false;
                btnSnapshotAuto.Enabled = true;
                btnSnapshotAuto.Text = "📸 Snapshot Automático";

                AgregarLog($"✅ Snapshot guardado: \"{etiqueta}\" | " +
                           $"Módulos: {modulos.Count} | Registros: {totalRegistros}");
                RefrescarListaSnapshots();

                MessageBox.Show(
                    $"✅ Snapshot guardado correctamente en la base de datos.\n\n" +
                    $"Etiqueta:   {etiqueta}\n" +
                    $"Módulos:    {modulosStr}\n" +
                    $"Registros:  {totalRegistros}\n\n" +
                    "Puedes restaurarlo desde la pestaña 'Snapshots'.",
                    "Snapshot completado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                progressBar.Visible = false;
                btnSnapshotAuto.Enabled = true;
                btnSnapshotAuto.Text = "📸 Snapshot Automático";
                AgregarLog($"❌ Error al guardar snapshot: {ex.Message}");
                MessageBox.Show("❌ " + ex.Message,
                    "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Mini-diálogo para pedir la etiqueta del snapshot.
        /// Devuelve null si el usuario canceló.
        /// </summary>
        private string MostrarDialogoEtiqueta()
        {
            Form dlg = new Form
            {
                Text = "Etiqueta del Snapshot",
                Size = new Size(420, 178),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false,
                BackColor = Color.FromArgb(236, 240, 241)
            };
            Label lbl = new Label
            {
                Text = "Descripción para identificar este snapshot:",
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.FromArgb(52, 73, 94),
                Location = new Point(15, 18),
                Size = new Size(385, 20)
            };
            TextBox txt = new TextBox
            {
                Font = new Font("Segoe UI", 10F),
                Location = new Point(15, 44),
                Size = new Size(385, 28),
                MaxLength = 90,
                Text = $"Snapshot {DateTime.Now:dd/MM/yyyy HH:mm}"
            };
            Button btnOk = new Button
            {
                Text = "✅ Guardar",
                Location = new Point(193, 92),
                Size = new Size(100, 36),
                BackColor = Color.FromArgb(142, 68, 173),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                DialogResult = DialogResult.OK,
                Cursor = Cursors.Hand
            };
            btnOk.FlatAppearance.BorderSize = 0;
            Button btnCancel = new Button
            {
                Text = "✗ Cancelar",
                Location = new Point(301, 92),
                Size = new Size(99, 36),
                BackColor = Color.FromArgb(149, 165, 166),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                DialogResult = DialogResult.Cancel,
                Cursor = Cursors.Hand
            };
            btnCancel.FlatAppearance.BorderSize = 0;
            dlg.Controls.AddRange(new Control[] { lbl, txt, btnOk, btnCancel });
            dlg.AcceptButton = btnOk;
            dlg.CancelButton = btnCancel;

            return dlg.ShowDialog(this) == DialogResult.OK
                ? (string.IsNullOrWhiteSpace(txt.Text)
                    ? $"Snapshot {DateTime.Now:dd/MM/yyyy HH:mm}"
                    : txt.Text.Trim())
                : null;
        }

        // ── Refrescar la lista de snapshots en la pestaña Snapshots ──────────
        private void RefrescarListaSnapshots()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(CD_Conexion.Conn))
                {
                    SqlCommand cmd = new SqlCommand(
                        $"SELECT id, etiqueta, fecha_creacion, modulos, " +
                        $"LEN(script_sql)/1024 AS tamano_kb " +
                        $"FROM [dbo].[{TABLA_SNAPSHOTS}] ORDER BY fecha_creacion DESC", con);
                    DataTable dt = new DataTable();
                    new SqlDataAdapter(cmd).Fill(dt);
                    dgvSnapshots.DataSource = dt;
                    ConfigurarColumnasSnapshots();
                }
            }
            catch { /* tabla puede no existir todavía */ }
        }

        private void ConfigurarColumnasSnapshots()
        {
            if (dgvSnapshots.Columns.Count == 0) return;
            if (dgvSnapshots.Columns.Contains("id")) dgvSnapshots.Columns["id"].Visible = false;
            if (dgvSnapshots.Columns.Contains("etiqueta"))
            { dgvSnapshots.Columns["etiqueta"].HeaderText = "Descripción"; dgvSnapshots.Columns["etiqueta"].FillWeight = 40; }
            if (dgvSnapshots.Columns.Contains("fecha_creacion"))
            { dgvSnapshots.Columns["fecha_creacion"].HeaderText = "Fecha"; dgvSnapshots.Columns["fecha_creacion"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm"; dgvSnapshots.Columns["fecha_creacion"].FillWeight = 25; }
            if (dgvSnapshots.Columns.Contains("modulos"))
            { dgvSnapshots.Columns["modulos"].HeaderText = "Módulos"; dgvSnapshots.Columns["modulos"].FillWeight = 25; }
            if (dgvSnapshots.Columns.Contains("tamano_kb"))
            { dgvSnapshots.Columns["tamano_kb"].HeaderText = "KB"; dgvSnapshots.Columns["tamano_kb"].FillWeight = 10; }
        }

        // ── Restaurar snapshot seleccionado del grid ──────────────────────────
        private void btnRestaurarSnapshot_Click(object sender, EventArgs e)
        {
            if (dgvSnapshots.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione un snapshot de la lista.",
                    "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int id = Convert.ToInt32(dgvSnapshots.CurrentRow.Cells["id"].Value);
            string etiqueta = dgvSnapshots.CurrentRow.Cells["etiqueta"].Value?.ToString() ?? "";
            string fecha = Convert.ToDateTime(dgvSnapshots.CurrentRow.Cells["fecha_creacion"].Value)
                                     .ToString("dd/MM/yyyy HH:mm");

            if (MessageBox.Show(
                    $"⚠️ ¿Restaurar el snapshot?\n\n" +
                    $"Descripción: {etiqueta}\n" +
                    $"Fecha:       {fecha}\n\n" +
                    "Los registros con el mismo ID serán omitidos (INSERT IF NOT EXISTS).\n" +
                    "¿Continuar?",
                    "Confirmar restauración",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;

            try
            {
                btnRestaurarSnapshot.Enabled = false;
                btnRestaurarSnapshot.Text = "⏳ Restaurando...";
                progressBar.Visible = true;

                string script;
                using (SqlConnection con = new SqlConnection(CD_Conexion.Conn))
                {
                    SqlCommand cmd = new SqlCommand(
                        $"SELECT script_sql FROM [dbo].[{TABLA_SNAPSHOTS}] WHERE id = @id", con);
                    cmd.Parameters.AddWithValue("@id", id);
                    script = cmd.ExecuteScalar()?.ToString() ?? "";
                }
                if (string.IsNullOrWhiteSpace(script))
                    throw new Exception("El snapshot está vacío o no se encontró.");

                EjecutarScriptSQL(script);

                progressBar.Visible = false;
                btnRestaurarSnapshot.Enabled = true;
                btnRestaurarSnapshot.Text = "♻️ Restaurar Snapshot";

                AgregarLog($"✅ Snapshot restaurado: \"{etiqueta}\" ({fecha})");
                MessageBox.Show($"✅ Snapshot \"{etiqueta}\" restaurado correctamente.",
                    "Restauración completada", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                progressBar.Visible = false;
                btnRestaurarSnapshot.Enabled = true;
                btnRestaurarSnapshot.Text = "♻️ Restaurar Snapshot";
                AgregarLog($"❌ Error restaurando snapshot: {ex.Message}");
                MessageBox.Show("❌ " + ex.Message,
                    "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ── Eliminar snapshot ─────────────────────────────────────────────────
        private void btnEliminarSnapshot_Click(object sender, EventArgs e)
        {
            if (dgvSnapshots.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione un snapshot para eliminar.",
                    "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int id = Convert.ToInt32(dgvSnapshots.CurrentRow.Cells["id"].Value);
            string etiqueta = dgvSnapshots.CurrentRow.Cells["etiqueta"].Value?.ToString() ?? "";

            if (MessageBox.Show($"¿Eliminar el snapshot \"{etiqueta}\"?\nEsta acción no se puede deshacer.",
                    "Confirmar eliminación",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            try
            {
                using (SqlConnection con = new SqlConnection(CD_Conexion.Conn))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(
                        $"DELETE FROM [dbo].[{TABLA_SNAPSHOTS}] WHERE id = @id", con);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
                AgregarLog($"🗑️ Snapshot eliminado: \"{etiqueta}\"");
                RefrescarListaSnapshots();
            }
            catch (Exception ex)
            {
                AgregarLog($"❌ Error eliminando snapshot: {ex.Message}");
                MessageBox.Show("❌ " + ex.Message,
                    "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ═════════════════════════════════════════════════════════════════════
        // C) VOLVER A LA ACTUALIDAD
        //    Aplica el snapshot más reciente (o el seleccionado en el grid).
        //    Útil tras haber restaurado un backup antiguo y querer regresar
        //    al estado guardado más reciente.
        // ═════════════════════════════════════════════════════════════════════
        private void btnVolverActualidad_Click(object sender, EventArgs e)
        {
            int idUsar = -1;
            string etiquetaUsar = "";

            // Si hay fila seleccionada en el grid de snapshots, preguntar si la usa
            if (dgvSnapshots.SelectedRows.Count > 0)
            {
                string etiquetaSel = dgvSnapshots.CurrentRow.Cells["etiqueta"].Value?.ToString() ?? "";
                string fechaSel = Convert.ToDateTime(
                    dgvSnapshots.CurrentRow.Cells["fecha_creacion"].Value)
                    .ToString("dd/MM/yyyy HH:mm");

                DialogResult resp = MessageBox.Show(
                    $"Tiene seleccionado el snapshot:\n\n" +
                    $"  \"{etiquetaSel}\"  ({fechaSel})\n\n" +
                    "• Sí  → restaura el snapshot seleccionado como estado actual\n" +
                    "• No  → restaura el snapshot MÁS RECIENTE automáticamente\n" +
                    "• Cancelar → volver sin hacer nada",
                    "Volver a la Actualidad",
                    MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                if (resp == DialogResult.Cancel) return;
                if (resp == DialogResult.Yes)
                {
                    idUsar = Convert.ToInt32(dgvSnapshots.CurrentRow.Cells["id"].Value);
                    etiquetaUsar = etiquetaSel;
                }
            }

            // Si no se eligió manualmente, tomar el más reciente
            if (idUsar < 0)
            {
                try
                {
                    using (SqlConnection con = new SqlConnection(CD_Conexion.Conn))
                    {
                        SqlCommand cmd = new SqlCommand(
                            $"SELECT TOP 1 id, etiqueta FROM [dbo].[{TABLA_SNAPSHOTS}] " +
                            "ORDER BY fecha_creacion DESC", con);
                        using (SqlDataReader r = cmd.ExecuteReader())
                        {
                            if (!r.Read())
                            {
                                MessageBox.Show(
                                    "⚠️ No hay snapshots guardados en el sistema.\n\n" +
                                    "Primero genere un Snapshot Automático desde la pestaña 'Backup'.",
                                    "Sin snapshots", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            idUsar = r.GetInt32(0);
                            etiquetaUsar = r.GetString(1);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("❌ " + ex.Message,
                        "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            if (MessageBox.Show(
                    $"¿Restaurar el snapshot \"{etiquetaUsar}\" como estado actual?\n\n" +
                    "Los registros con el mismo ID serán omitidos (INSERT IF NOT EXISTS).",
                    "Confirmar — Volver a la Actualidad",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            try
            {
                btnVolverActualidad.Enabled = false;
                btnVolverActualidad.Text = "⏳ Restaurando...";
                progressBar.Visible = true;

                string script;
                using (SqlConnection con = new SqlConnection(CD_Conexion.Conn))
                {
                    SqlCommand cmd = new SqlCommand(
                        $"SELECT script_sql FROM [dbo].[{TABLA_SNAPSHOTS}] WHERE id = @id", con);
                    cmd.Parameters.AddWithValue("@id", idUsar);
                    script = cmd.ExecuteScalar()?.ToString() ?? "";
                }
                EjecutarScriptSQL(script);

                progressBar.Visible = false;
                btnVolverActualidad.Enabled = true;
                btnVolverActualidad.Text = "🔄 Volver a la Actualidad";

                AgregarLog($"✅ Actualidad restaurada desde snapshot: \"{etiquetaUsar}\"");
                MessageBox.Show($"✅ Estado restaurado correctamente desde:\n\"{etiquetaUsar}\"",
                    "Actualidad restaurada", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                progressBar.Visible = false;
                btnVolverActualidad.Enabled = true;
                btnVolverActualidad.Text = "🔄 Volver a la Actualidad";
                AgregarLog($"❌ Error: {ex.Message}");
                MessageBox.Show("❌ " + ex.Message,
                    "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ═════════════════════════════════════════════════════════════════════
        // D) EXPORTAR CSV
        // ═════════════════════════════════════════════════════════════════════
        private void btnBackupCSV_Click(object sender, EventArgs e)
        {
            List<string> modulos = ObtenerModulosSeleccionados();
            if (modulos.Count == 0)
            {
                MessageBox.Show("⚠️ Seleccione al menos un módulo para exportar.",
                    "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            FolderBrowserDialog dlg = new FolderBrowserDialog
            {
                Description = "Seleccione la carpeta donde guardar los archivos CSV",
                ShowNewFolderButton = true
            };
            if (dlg.ShowDialog() != DialogResult.OK) return;

            try
            {
                btnBackupCSV.Enabled = false;
                btnBackupCSV.Text = "⏳ Exportando...";
                progressBar.Visible = true;
                progressBar.Value = 0;
                progressBar.Maximum = modulos.Count;

                string carpeta = Path.Combine(dlg.SelectedPath,
                    $"VetBackup_CSV_{DateTime.Now:yyyyMMdd_HHmm}");
                Directory.CreateDirectory(carpeta);

                int totalRegistros = 0, archivosCreados = 0;

                foreach (string modulo in modulos)
                {
                    if (!ModulosTablas.TryGetValue(modulo, out string[] tablas)) continue;
                    foreach (string tabla in tablas)
                    {
                        DataTable dt = ObtenerDatosTabla(tabla);
                        if (dt == null || dt.Rows.Count == 0) continue;
                        EscribirCSV(dt, Path.Combine(carpeta, $"{tabla}.csv"));
                        totalRegistros += dt.Rows.Count;
                        archivosCreados++;
                    }
                    progressBar.Value++;
                    Application.DoEvents();
                }

                progressBar.Visible = false;
                btnBackupCSV.Enabled = true;
                btnBackupCSV.Text = "📊 Exportar CSV";

                AgregarLog($"✅ CSV exportados: {archivosCreados} archivos | " +
                           $"Registros: {totalRegistros} | Carpeta: {carpeta}");

                MessageBox.Show(
                    $"✅ Exportación CSV completada.\n\n" +
                    $"Archivos:   {archivosCreados}\n" +
                    $"Registros:  {totalRegistros}\n" +
                    $"Carpeta:    {carpeta}",
                    "Exportación completada", MessageBoxButtons.OK, MessageBoxIcon.Information);

                System.Diagnostics.Process.Start("explorer.exe", carpeta);
            }
            catch (Exception ex)
            {
                progressBar.Visible = false;
                btnBackupCSV.Enabled = true;
                btnBackupCSV.Text = "📊 Exportar CSV";
                AgregarLog($"❌ Error exportando CSV: {ex.Message}");
                MessageBox.Show("❌ " + ex.Message,
                    "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ═════════════════════════════════════════════════════════════════════
        // E) RESTAURAR DESDE ARCHIVO SQL EXTERNO
        // ═════════════════════════════════════════════════════════════════════
        private void btnRestaurar_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog
            {
                Title = "Seleccionar archivo de respaldo",
                Filter = "Archivo SQL|*.sql|Todos los archivos|*.*"
            };
            if (dlg.ShowDialog() != DialogResult.OK) return;

            if (MessageBox.Show(
                    $"⚠️ ADVERTENCIA: La importación puede SOBRESCRIBIR registros existentes.\n\n" +
                    $"Archivo: {Path.GetFileName(dlg.FileName)}\n\n" +
                    "¿Desea continuar?",
                    "Confirmar restauración",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;

            try
            {
                btnRestaurar.Enabled = false;
                btnRestaurar.Text = "⏳ Restaurando...";
                progressBar.Visible = true;

                EjecutarScriptSQL(File.ReadAllText(dlg.FileName, Encoding.UTF8));

                progressBar.Visible = false;
                btnRestaurar.Enabled = true;
                btnRestaurar.Text = "📥 Importar .SQL";

                AgregarLog($"✅ Restauración completada desde: {Path.GetFileName(dlg.FileName)}");
                MessageBox.Show(
                    $"✅ Restauración completada.\n\nArchivo: {Path.GetFileName(dlg.FileName)}",
                    "Restauración completada", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                progressBar.Visible = false;
                btnRestaurar.Enabled = true;
                btnRestaurar.Text = "📥 Importar .SQL";
                AgregarLog($"❌ Error en restauración: {ex.Message}");
                MessageBox.Show("❌ " + ex.Message,
                    "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ═════════════════════════════════════════════════════════════════════
        // F) IMPORTAR CSV
        // ═════════════════════════════════════════════════════════════════════
        private void btnImportarCSV_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog
            {
                Title = "Seleccionar archivo CSV para importar",
                Filter = "Archivos CSV|*.csv",
                Multiselect = true
            };
            if (dlg.ShowDialog() != DialogResult.OK) return;

            if (MessageBox.Show(
                    $"⚠️ Se importarán {dlg.FileNames.Length} archivo(s) CSV.\n\n" +
                    "Registros con el mismo ID serán omitidos.\n\n¿Continuar?",
                    "Confirmar importación",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            try
            {
                btnImportarCSV.Enabled = false;
                btnImportarCSV.Text = "⏳ Importando...";
                progressBar.Visible = true;
                progressBar.Maximum = dlg.FileNames.Length;
                progressBar.Value = 0;

                int totalImportados = 0;
                var errores = new List<string>();

                foreach (string archivo in dlg.FileNames)
                {
                    try
                    {
                        int n = ImportarDesdeCSV(archivo);
                        totalImportados += n;
                        AgregarLog($"✅ Importado: {Path.GetFileName(archivo)} ({n} registros)");
                    }
                    catch (Exception ex)
                    {
                        errores.Add($"{Path.GetFileName(archivo)}: {ex.Message}");
                        AgregarLog($"❌ Error en {Path.GetFileName(archivo)}: {ex.Message}");
                    }
                    progressBar.Value++;
                    Application.DoEvents();
                }

                progressBar.Visible = false;
                btnImportarCSV.Enabled = true;
                btnImportarCSV.Text = "📤 Importar CSV";

                string msg = $"✅ Importación CSV completada.\n\nRegistros importados: {totalImportados}";
                if (errores.Count > 0) msg += $"\n\n⚠️ Errores ({errores.Count}):\n" + string.Join("\n", errores);
                MessageBox.Show(msg, "Importación completada", MessageBoxButtons.OK,
                    errores.Count > 0 ? MessageBoxIcon.Warning : MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                progressBar.Visible = false;
                btnImportarCSV.Enabled = true;
                btnImportarCSV.Text = "📤 Importar CSV";
                MessageBox.Show("❌ " + ex.Message,
                    "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ─────────────────────────────────────────────────────────────────────
        // LIMPIAR LOG
        // ─────────────────────────────────────────────────────────────────────
        private void btnLimpiarLog_Click(object sender, EventArgs e) => rtbLog.Clear();

        // ═════════════════════════════════════════════════════════════════════
        // HELPERS — GENERACIÓN DEL SCRIPT SQL
        // ═════════════════════════════════════════════════════════════════════
        private StringBuilder GenerarScriptSQL(List<string> modulos, out int totalRegistros)
        {
            totalRegistros = 0;
            var sb = new StringBuilder();

            sb.AppendLine("-- ============================================================");
            sb.AppendLine($"-- BACKUP VeterinariaBD — Generado: {DateTime.Now:dd/MM/yyyy HH:mm:ss}");
            sb.AppendLine($"-- Usuario: {FrmLogin.UsuarioActual}");
            sb.AppendLine($"-- Módulos: {string.Join(", ", modulos)}");
            sb.AppendLine("-- Filtro de fecha: " + ObtenerDescripcionFiltroFecha());
            sb.AppendLine("-- ============================================================");
            sb.AppendLine("USE [VeterinariaBD]");
            sb.AppendLine("GO");
            sb.AppendLine("SET NOCOUNT ON;");
            sb.AppendLine();

            ObtenerRangoFechas(out DateTime? desde, out DateTime? hasta);

            foreach (string modulo in modulos)
            {
                if (!ModulosTablas.TryGetValue(modulo, out string[] tablas)) continue;

                sb.AppendLine($"-- ── MÓDULO: {modulo.ToUpper()} ──────────────────────────────────────");
                sb.AppendLine();

                foreach (string tabla in tablas)
                {
                    DataTable dt = ObtenerDatosTablaConFecha(tabla, desde, hasta);
                    if (dt == null || dt.Rows.Count == 0)
                    {
                        sb.AppendLine($"-- Tabla [{tabla}]: sin registros en el período seleccionado.");
                        sb.AppendLine();
                        continue;
                    }

                    sb.AppendLine($"-- Tabla [{tabla}] — {dt.Rows.Count} registros");
                    sb.AppendLine($"SET IDENTITY_INSERT [dbo].[{tabla}] ON;");

                    foreach (DataRow row in dt.Rows)
                    {
                        sb.AppendLine(GenerarInsertRow(tabla, dt.Columns, row));
                        totalRegistros++;
                    }

                    sb.AppendLine($"SET IDENTITY_INSERT [dbo].[{tabla}] OFF;");
                    sb.AppendLine("GO");
                    sb.AppendLine();
                }

                progressBar.Value = Math.Min(progressBar.Value + 1, progressBar.Maximum);
                Application.DoEvents();
            }

            sb.AppendLine("-- ============================================================");
            sb.AppendLine($"-- FIN DEL BACKUP — Total registros: {totalRegistros}");
            sb.AppendLine("-- ============================================================");

            return sb;
        }

        private string GenerarInsertRow(string tabla, DataColumnCollection cols, DataRow row)
        {
            var colNames = new List<string>();
            var colValues = new List<string>();

            foreach (DataColumn col in cols)
            {
                colNames.Add($"[{col.ColumnName}]");
                object val = row[col];
                if (val == null || val == DBNull.Value)
                    colValues.Add("NULL");
                else if (val is bool b)
                    colValues.Add(b ? "1" : "0");
                else if (val is DateTime dt)
                    colValues.Add($"'{dt:yyyy-MM-dd HH:mm:ss}'");
                else if (val is decimal || val is int || val is long || val is double || val is float)
                    colValues.Add(Convert.ToString(val, System.Globalization.CultureInfo.InvariantCulture));
                else
                    colValues.Add($"N'{val.ToString().Replace("'", "''")}'");
            }

            return
                $"IF NOT EXISTS (SELECT 1 FROM [dbo].[{tabla}] WHERE [{cols[0].ColumnName}] = {colValues[0]})\r\n" +
                $"    INSERT INTO [dbo].[{tabla}] ({string.Join(", ", colNames)})\r\n" +
                $"    VALUES ({string.Join(", ", colValues)});";
        }

        // ── Columnas de fecha por tabla (para filtrado temporal) ──────────────
        private static readonly Dictionary<string, string> ColumnasFecha =
            new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { "auditoria_cliente",  "fecha" },
                { "venta",              "fecha" },
                { "compra",             "fecha" },
                { "sesiones_usuario",   "fecha_inicio" },
                { "historial_precios",  "fecha" },
                { "movimiento_stock",   "fecha" },
                { "cita",               "fecha" },
                { "producto",           "fecha_creacion" },
                { "categoria_producto", "fecha_creacion" },
            };

        private DataTable ObtenerDatosTablaConFecha(string tabla, DateTime? desde, DateTime? hasta)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(CD_Conexion.Conn))
                {
                    con.Open();
                    string where = "";
                    if ((desde.HasValue || hasta.HasValue) &&
                        ColumnasFecha.TryGetValue(tabla, out string colFecha))
                    {
                        var conds = new List<string>();
                        if (desde.HasValue) conds.Add($"[{colFecha}] >= '{desde.Value:yyyy-MM-dd}'");
                        if (hasta.HasValue) conds.Add($"[{colFecha}] <= '{hasta.Value:yyyy-MM-dd 23:59:59}'");
                        if (conds.Count > 0) where = " WHERE " + string.Join(" AND ", conds);
                    }
                    SqlCommand cmd = new SqlCommand(
                        $"SELECT * FROM [dbo].[{tabla}]{where}", con)
                    { CommandTimeout = 120 };
                    DataTable dt = new DataTable(tabla);
                    new SqlDataAdapter(cmd).Fill(dt);
                    return dt;
                }
            }
            catch { return null; }
        }

        private DataTable ObtenerDatosTabla(string tabla)
            => ObtenerDatosTablaConFecha(tabla, null, null);

        // ── Ejecutar script SQL dividido en bloques GO ────────────────────────
        private void EjecutarScriptSQL(string script)
        {
            string[] bloques = script.Split(
                new[] { "\r\nGO\r\n", "\nGO\n", "\r\nGO\n", "\nGO\r\n", "\r\nGO", "\nGO" },
                StringSplitOptions.RemoveEmptyEntries);

            using (SqlConnection con = new SqlConnection(CD_Conexion.Conn))
            {
                con.Open();
                progressBar.Maximum = bloques.Length;
                progressBar.Value = 0;

                foreach (string bloque in bloques)
                {
                    string sql = bloque.Trim();
                    if (string.IsNullOrWhiteSpace(sql) || sql.StartsWith("--")) continue;
                    try
                    {
                        new SqlCommand(sql, con) { CommandTimeout = 120 }.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        // Ignorar errores de IDENTITY_INSERT (8101) y duplicados (2627/2601)
                        if (ex.Number != 8101 && ex.Number != 2627 && ex.Number != 2601)
                            AgregarLog($"⚠️ Advertencia SQL: {ex.Message}");
                    }
                    progressBar.Value = Math.Min(progressBar.Value + 1, progressBar.Maximum);
                    Application.DoEvents();
                }
            }
        }

        // ── CSV: escribir archivo ─────────────────────────────────────────────
        private void EscribirCSV(DataTable dt, string ruta)
        {
            var sb = new StringBuilder();
            var cols = new List<string>();
            foreach (DataColumn col in dt.Columns) cols.Add($"\"{col.ColumnName}\"");
            sb.AppendLine(string.Join(",", cols));

            foreach (DataRow row in dt.Rows)
            {
                var vals = new List<string>();
                foreach (DataColumn col in dt.Columns)
                {
                    object v = row[col];
                    if (v == null || v == DBNull.Value) vals.Add("");
                    else vals.Add($"\"{v.ToString().Replace("\"", "\"\"")}\"");
                }
                sb.AppendLine(string.Join(",", vals));
            }
            File.WriteAllText(ruta, sb.ToString(), Encoding.UTF8);
        }

        // ── CSV: importar archivo ─────────────────────────────────────────────
        private int ImportarDesdeCSV(string archivo)
        {
            string tabla = Path.GetFileNameWithoutExtension(archivo);
            bool tablaValida = false;
            foreach (var kvp in ModulosTablas)
            {
                foreach (string t in kvp.Value)
                    if (t.Equals(tabla, StringComparison.OrdinalIgnoreCase))
                    { tablaValida = true; break; }
                if (tablaValida) break;
            }
            if (!tablaValida)
                throw new Exception($"Tabla '{tabla}' no reconocida. Solo se pueden importar tablas del sistema.");

            string[] lineas = File.ReadAllLines(archivo, Encoding.UTF8);
            if (lineas.Length < 2) return 0;

            string[] encabezados = ParsearLineaCSV(lineas[0]);
            int importados = 0;

            using (SqlConnection con = new SqlConnection(CD_Conexion.Conn))
            {
                con.Open();
                try { new SqlCommand($"SET IDENTITY_INSERT [dbo].[{tabla}] ON", con).ExecuteNonQuery(); } catch { }

                for (int i = 1; i < lineas.Length; i++)
                {
                    if (string.IsNullOrWhiteSpace(lineas[i])) continue;
                    string[] valores = ParsearLineaCSV(lineas[i]);
                    if (valores.Length != encabezados.Length) continue;

                    var colNames = new List<string>();
                    var paramNames = new List<string>();
                    for (int j = 0; j < encabezados.Length; j++)
                    { colNames.Add($"[{encabezados[j]}]"); paramNames.Add($"@p{j}"); }

                    string sql =
                        $"IF NOT EXISTS (SELECT 1 FROM [dbo].[{tabla}] WHERE [{encabezados[0]}] = @p0)\r\n" +
                        $"  INSERT INTO [dbo].[{tabla}] ({string.Join(",", colNames)})\r\n" +
                        $"  VALUES ({string.Join(",", paramNames)})";

                    SqlCommand cmd = new SqlCommand(sql, con);
                    for (int j = 0; j < valores.Length; j++)
                    {
                        string v = valores[j];
                        if (string.IsNullOrEmpty(v)) cmd.Parameters.AddWithValue($"@p{j}", DBNull.Value);
                        else cmd.Parameters.AddWithValue($"@p{j}", v);
                    }
                    try { cmd.ExecuteNonQuery(); importados++; } catch { }
                }

                try { new SqlCommand($"SET IDENTITY_INSERT [dbo].[{tabla}] OFF", con).ExecuteNonQuery(); } catch { }
            }
            return importados;
        }

        private string[] ParsearLineaCSV(string linea)
        {
            var campos = new List<string>();
            bool enComillas = false;
            var campo = new StringBuilder();
            for (int i = 0; i < linea.Length; i++)
            {
                char c = linea[i];
                if (c == '"')
                {
                    if (enComillas && i + 1 < linea.Length && linea[i + 1] == '"')
                    { campo.Append('"'); i++; }
                    else enComillas = !enComillas;
                }
                else if (c == ',' && !enComillas)
                { campos.Add(campo.ToString()); campo.Clear(); }
                else campo.Append(c);
            }
            campos.Add(campo.ToString());
            return campos.ToArray();
        }

        // ── Helpers de fechas ─────────────────────────────────────────────────
        private void ObtenerRangoFechas(out DateTime? desde, out DateTime? hasta)
        {
            desde = null; hasta = null;
            if (rbtnHastaHoy.Checked)
            { desde = dtpFechaInicio.Value.Date; hasta = DateTime.Now; }
            else if (rbtnRangoFechas.Checked)
            { desde = dtpFechaInicio.Value.Date; hasta = dtpFechaFin.Value.Date; }
        }

        private string ObtenerDescripcionFiltroFecha()
        {
            if (rbtnSinFiltro.Checked) return "Sin filtro (todos los registros)";
            if (rbtnHastaHoy.Checked) return $"Desde {dtpFechaInicio.Value:dd/MM/yyyy} hasta hoy";
            if (rbtnRangoFechas.Checked) return $"Del {dtpFechaInicio.Value:dd/MM/yyyy} al {dtpFechaFin.Value:dd/MM/yyyy}";
            return "";
        }

        // ── Log ───────────────────────────────────────────────────────────────
        private void AgregarLog(string mensaje)
        {
            if (rtbLog.InvokeRequired)
            { rtbLog.Invoke(new Action<string>(AgregarLog), mensaje); return; }
            rtbLog.AppendText($"[{DateTime.Now:HH:mm:ss}]  {mensaje}\n");
            rtbLog.ScrollToCaret();
        }
    }
}