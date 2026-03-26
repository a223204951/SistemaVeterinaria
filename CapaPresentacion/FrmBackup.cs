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
    /// PESTAÑAS:
    ///   Tab 1 "💾 Backup / Importar"
    ///     • Panel izquierdo  : módulos checkboxes + filtro de fechas
    ///     • Panel central    : acciones (Backup SQL, Snapshot Auto, CSV, Importar)
    ///     • Panel derecho    : log de operaciones
    ///
    ///   Tab 2 "📸 Snapshots"
    ///     • Grid con snapshots guardados en BD
    ///     • Panel detalle    : muestra info completa del snapshot seleccionado
    ///     • Botones          : Cargar/Restaurar, Renombrar, Eliminar, Volver a la Actualidad
    ///
    /// La tabla interna _vet_snapshots almacena:
    ///   id, etiqueta, descripcion_auto (JSON-like con metadatos), fecha_creacion,
    ///   modulos, filtro_fecha, total_registros, script_sql
    /// </summary>
    public partial class FrmBackup : Form
    {
        // ── Módulos → tablas ──────────────────────────────────────────────────
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

        private const string TABLA_SNAPSHOTS = "_vet_snapshots";

        public FrmBackup() { InitializeComponent(); }

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

            // Suscribir selección del grid para mostrar detalle
            dgvSnapshots.SelectionChanged += DgvSnapshots_SelectionChanged;
        }

        // ─────────────────────────────────────────────────────────────────────
        // MÓDULOS (checkboxes dinámicos)
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
            chkTodos.CheckedChanged += (s, ev) =>
            {
                bool st = ((CheckBox)s).Checked;
                foreach (Control c in flpModulos.Controls)
                    if (c is CheckBox ck && ck.Tag?.ToString() != "TODOS") ck.Checked = st;
            };
            flpModulos.Controls.Add(chkTodos);
            flpModulos.Controls.Add(new Panel { Width = 220, Height = 1, BackColor = Color.FromArgb(220, 220, 220), Margin = new Padding(3, 4, 3, 4) });

            foreach (string modulo in ModulosTablas.Keys)
                flpModulos.Controls.Add(new CheckBox
                {
                    Text = modulo,
                    Font = new Font("Segoe UI", 9F),
                    ForeColor = Color.FromArgb(52, 73, 94),
                    Width = 220,
                    Checked = true,
                    Tag = modulo
                });
        }

        private List<string> ObtenerModulosSeleccionados()
        {
            var lista = new List<string>();
            foreach (Control c in flpModulos.Controls)
                if (c is CheckBox ck && ck.Checked && ck.Tag?.ToString() != "TODOS")
                    lista.Add(ck.Tag.ToString());
            return lista;
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

        // ═════════════════════════════════════════════════════════════════════
        // A) BACKUP SQL A ARCHIVO
        // ═════════════════════════════════════════════════════════════════════
        private void btnBackupSQL_Click(object sender, EventArgs e)
        {
            List<string> modulos = ObtenerModulosSeleccionados();
            if (modulos.Count == 0) { MsgWarn("Seleccione al menos un módulo."); return; }

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
                SetBusy(btnBackupSQL, "💾 Backup SQL (archivo)", "⏳ Generando...", modulos.Count);
                StringBuilder sb = GenerarScriptSQL(modulos, out int total);
                File.WriteAllText(dlg.FileName, sb.ToString(), Encoding.UTF8);
                SetIdle(btnBackupSQL, "💾 Backup SQL (archivo)");
                AgregarLog($"✅ Backup SQL: {Path.GetFileName(dlg.FileName)} | Módulos: {modulos.Count} | Registros: {total}");
                MessageBox.Show(
                    $"✅ Respaldo SQL generado.\n\nArchivo: {Path.GetFileName(dlg.FileName)}\n" +
                    $"Módulos: {modulos.Count} | Registros: {total}\nTamaño: {new FileInfo(dlg.FileName).Length / 1024.0:N1} KB",
                    "Backup completado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex) { SetIdle(btnBackupSQL, "💾 Backup SQL (archivo)"); MsgErr(ex.Message); AgregarLog("❌ " + ex.Message); }
        }

        // ═════════════════════════════════════════════════════════════════════
        // B) SNAPSHOT AUTOMÁTICO — guarda en BD sin archivo externo
        // ═════════════════════════════════════════════════════════════════════

        /// <summary>Crea la tabla _vet_snapshots si no existe (incluye columnas de metadatos).</summary>
        private void GarantizarTablaSnapshots()
        {
            try
            {
                using (var con = new SqlConnection(CD_Conexion.Conn))
                {
                    con.Open();
                    new SqlCommand($@"
                        IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME='{TABLA_SNAPSHOTS}')
                        CREATE TABLE [dbo].[{TABLA_SNAPSHOTS}] (
                            [id]               INT IDENTITY(1,1) PRIMARY KEY,
                            [etiqueta]         NVARCHAR(100) NOT NULL,
                            [fecha_creacion]   DATETIME      NOT NULL DEFAULT GETDATE(),
                            [modulos]          NVARCHAR(500) NOT NULL,
                            [filtro_fecha]     NVARCHAR(100) NOT NULL DEFAULT 'Sin filtro',
                            [total_registros]  INT           NOT NULL DEFAULT 0,
                            [detalle_tablas]   NVARCHAR(MAX) NULL,
                            [script_sql]       NVARCHAR(MAX) NOT NULL
                        );", con).ExecuteNonQuery();
                }
            }
            catch (Exception ex) { AgregarLog($"⚠️ No se pudo crear tabla snapshots: {ex.Message}"); }
        }

        private void btnSnapshotAuto_Click(object sender, EventArgs e)
        {
            List<string> modulos = ObtenerModulosSeleccionados();
            if (modulos.Count == 0) { MsgWarn("Seleccione al menos un módulo."); return; }

            string etiqueta = MostrarDialogoEtiqueta();
            if (etiqueta == null) return;

            try
            {
                SetBusy(btnSnapshotAuto, "📸 Snapshot Automático", "⏳ Guardando...", modulos.Count);

                // Generar script y recopilar metadatos detallados
                var detalleTablas = new StringBuilder();
                int totalRegistros = GenerarScriptConDetalle(modulos, detalleTablas, out StringBuilder sbScript);

                string filtroDesc = ObtenerDescripcionFiltroFecha();
                string modulosStr = string.Join(", ", modulos);
                string detalleStr = detalleTablas.ToString();

                using (var con = new SqlConnection(CD_Conexion.Conn))
                {
                    con.Open();
                    var cmd = new SqlCommand(
                        $"INSERT INTO [dbo].[{TABLA_SNAPSHOTS}] " +
                        "(etiqueta, modulos, filtro_fecha, total_registros, detalle_tablas, script_sql) " +
                        "VALUES (@et, @mod, @filtro, @total, @detalle, @sql)", con)
                    { CommandTimeout = 180 };
                    cmd.Parameters.AddWithValue("@et", etiqueta);
                    cmd.Parameters.AddWithValue("@mod", modulosStr);
                    cmd.Parameters.AddWithValue("@filtro", filtroDesc);
                    cmd.Parameters.AddWithValue("@total", totalRegistros);
                    cmd.Parameters.AddWithValue("@detalle", detalleStr);
                    cmd.Parameters.AddWithValue("@sql", sbScript.ToString());
                    cmd.ExecuteNonQuery();
                }

                SetIdle(btnSnapshotAuto, "📸 Snapshot Automático");
                AgregarLog($"✅ Snapshot \"{etiqueta}\" | Módulos: {modulos.Count} | Registros: {totalRegistros}");
                RefrescarListaSnapshots();

                MessageBox.Show(
                    $"✅ Snapshot guardado en la base de datos.\n\n" +
                    $"Etiqueta:    {etiqueta}\n" +
                    $"Módulos:     {modulosStr}\n" +
                    $"Filtro:      {filtroDesc}\n" +
                    $"Registros:   {totalRegistros}\n\n" +
                    "Consúltalo en la pestaña '📸 Snapshots'.",
                    "Snapshot completado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                SetIdle(btnSnapshotAuto, "📸 Snapshot Automático");
                AgregarLog("❌ " + ex.Message);
                MsgErr(ex.Message);
            }
        }

        /// <summary>
        /// Como GenerarScriptSQL pero también devuelve un detalle tabla‑por‑tabla
        /// con el número de registros incluidos en el backup.
        /// </summary>
        private int GenerarScriptConDetalle(List<string> modulos,
            StringBuilder detalleTablas, out StringBuilder sbScript)
        {
            int totalGlobal = 0;
            sbScript = new StringBuilder();

            ObtenerRangoFechas(out DateTime? desde, out DateTime? hasta);

            sbScript.AppendLine($"-- BACKUP VeterinariaBD — {DateTime.Now:dd/MM/yyyy HH:mm:ss}");
            sbScript.AppendLine($"-- Usuario: {FrmLogin.UsuarioActual}");
            sbScript.AppendLine($"-- Filtro: {ObtenerDescripcionFiltroFecha()}");
            sbScript.AppendLine("USE [VeterinariaBD]"); sbScript.AppendLine("GO");
            sbScript.AppendLine("SET NOCOUNT ON;"); sbScript.AppendLine();

            foreach (string modulo in modulos)
            {
                if (!ModulosTablas.TryGetValue(modulo, out string[] tablas)) continue;
                sbScript.AppendLine($"-- MÓDULO: {modulo.ToUpper()}");

                foreach (string tabla in tablas)
                {
                    DataTable dt = ObtenerDatosTablaConFecha(tabla, desde, hasta);
                    int n = dt?.Rows.Count ?? 0;
                    detalleTablas.AppendLine($"{tabla}: {n} registros");
                    totalGlobal += n;

                    if (n == 0) { sbScript.AppendLine($"-- [{tabla}]: sin registros."); sbScript.AppendLine(); continue; }

                    sbScript.AppendLine($"-- [{tabla}] — {n} registros");
                    sbScript.AppendLine($"SET IDENTITY_INSERT [dbo].[{tabla}] ON;");
                    foreach (DataRow row in dt.Rows) sbScript.AppendLine(GenerarInsertRow(tabla, dt.Columns, row));
                    sbScript.AppendLine($"SET IDENTITY_INSERT [dbo].[{tabla}] OFF;");
                    sbScript.AppendLine("GO"); sbScript.AppendLine();
                }

                progressBar.Value = Math.Min(progressBar.Value + 1, progressBar.Maximum);
                Application.DoEvents();
            }

            sbScript.AppendLine($"-- FIN — Total registros: {totalGlobal}");
            return totalGlobal;
        }

        /// <summary>Diálogo mini para pedir la etiqueta del snapshot. Devuelve null si cancela.</summary>
        private string MostrarDialogoEtiqueta()
        {
            Form dlg = new Form
            {
                Text = "Etiqueta del Snapshot",
                Size = new Size(430, 185),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false,
                BackColor = Color.FromArgb(236, 240, 241)
            };
            var lbl = new Label
            {
                Text = "Título descriptivo (los metadatos se guardarán automáticamente):",
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.FromArgb(52, 73, 94),
                Location = new Point(15, 18),
                Size = new Size(395, 20)
            };
            var txt = new TextBox
            {
                Font = new Font("Segoe UI", 10F),
                Location = new Point(15, 44),
                Size = new Size(395, 28),
                MaxLength = 90,
                Text = $"Snapshot {DateTime.Now:dd/MM/yyyy HH:mm}"
            };
            Button btnOk = MakeBtn("✅ Guardar", Color.FromArgb(142, 68, 173), new Point(200, 96), new Size(100, 36));
            btnOk.DialogResult = DialogResult.OK;
            Button btnCx = MakeBtn("✗ Cancelar", Color.FromArgb(149, 165, 166), new Point(308, 96), new Size(102, 36));
            btnCx.DialogResult = DialogResult.Cancel;
            dlg.Controls.AddRange(new Control[] { lbl, txt, btnOk, btnCx });
            dlg.AcceptButton = btnOk; dlg.CancelButton = btnCx;
            return dlg.ShowDialog(this) == DialogResult.OK
                ? (string.IsNullOrWhiteSpace(txt.Text) ? $"Snapshot {DateTime.Now:dd/MM/yyyy HH:mm}" : txt.Text.Trim())
                : null;
        }

        // ─────────────────────────────────────────────────────────────────────
        // GRID DE SNAPSHOTS — carga y detalle
        // ─────────────────────────────────────────────────────────────────────
        private void RefrescarListaSnapshots()
        {
            try
            {
                using (var con = new SqlConnection(CD_Conexion.Conn))
                {
                    var cmd = new SqlCommand(
                        $"SELECT id, etiqueta, fecha_creacion, modulos, filtro_fecha, " +
                        $"total_registros, LEN(script_sql)/1024 AS tamano_kb " +
                        $"FROM [dbo].[{TABLA_SNAPSHOTS}] ORDER BY fecha_creacion DESC", con);
                    var dt = new DataTable();
                    new SqlDataAdapter(cmd).Fill(dt);
                    dgvSnapshots.DataSource = dt;
                    ConfigurarColumnasSnapshots();
                }
            }
            catch { /* tabla puede no existir aún */ }
            LimpiarDetalleSnapshot();
        }

        private void ConfigurarColumnasSnapshots()
        {
            if (dgvSnapshots.Columns.Count == 0) return;
            if (dgvSnapshots.Columns.Contains("id")) dgvSnapshots.Columns["id"].Visible = false;
            if (dgvSnapshots.Columns.Contains("etiqueta")) { dgvSnapshots.Columns["etiqueta"].HeaderText = "Etiqueta"; dgvSnapshots.Columns["etiqueta"].FillWeight = 30; }
            if (dgvSnapshots.Columns.Contains("fecha_creacion")) { dgvSnapshots.Columns["fecha_creacion"].HeaderText = "Fecha creación"; dgvSnapshots.Columns["fecha_creacion"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm"; dgvSnapshots.Columns["fecha_creacion"].FillWeight = 18; }
            if (dgvSnapshots.Columns.Contains("modulos")) { dgvSnapshots.Columns["modulos"].HeaderText = "Módulos"; dgvSnapshots.Columns["modulos"].FillWeight = 27; }
            if (dgvSnapshots.Columns.Contains("filtro_fecha")) { dgvSnapshots.Columns["filtro_fecha"].HeaderText = "Filtro de fecha"; dgvSnapshots.Columns["filtro_fecha"].FillWeight = 18; }
            if (dgvSnapshots.Columns.Contains("total_registros")) { dgvSnapshots.Columns["total_registros"].HeaderText = "Registros"; dgvSnapshots.Columns["total_registros"].FillWeight = 10; }
            if (dgvSnapshots.Columns.Contains("tamano_kb")) { dgvSnapshots.Columns["tamano_kb"].HeaderText = "KB"; dgvSnapshots.Columns["tamano_kb"].FillWeight = 7; }
        }

        /// <summary>Al seleccionar una fila, carga el detalle de tablas en el panel derecho.</summary>
        private void DgvSnapshots_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvSnapshots.SelectedRows.Count == 0) { LimpiarDetalleSnapshot(); return; }

            try
            {
                int id = Convert.ToInt32(dgvSnapshots.CurrentRow.Cells["id"].Value);
                using (var con = new SqlConnection(CD_Conexion.Conn))
                {
                    var cmd = new SqlCommand(
                        $"SELECT etiqueta, fecha_creacion, modulos, filtro_fecha, " +
                        $"total_registros, detalle_tablas FROM [dbo].[{TABLA_SNAPSHOTS}] WHERE id=@id", con);
                    cmd.Parameters.AddWithValue("@id", id);
                    using (var r = cmd.ExecuteReader())
                    {
                        if (!r.Read()) { LimpiarDetalleSnapshot(); return; }

                        lblDetEtiqueta.Text = r["etiqueta"]?.ToString() ?? "";
                        lblDetFecha.Text = Convert.ToDateTime(r["fecha_creacion"]).ToString("dd/MM/yyyy HH:mm:ss");
                        lblDetModulos.Text = r["modulos"]?.ToString() ?? "";
                        lblDetFiltro.Text = r["filtro_fecha"]?.ToString() ?? "Sin filtro";
                        lblDetRegistros.Text = $"{r["total_registros"]} registros totales";
                        rtbDetTablas.Text = r["detalle_tablas"]?.ToString() ?? "(sin detalle)";
                    }
                }
                panelDetalle.Visible = true;
            }
            catch { LimpiarDetalleSnapshot(); }
        }

        private void LimpiarDetalleSnapshot()
        {
            lblDetEtiqueta.Text = "—";
            lblDetFecha.Text = "—";
            lblDetModulos.Text = "—";
            lblDetFiltro.Text = "—";
            lblDetRegistros.Text = "—";
            rtbDetTablas.Text = "";
            panelDetalle.Visible = false;
        }

        // ─────────────────────────────────────────────────────────────────────
        // BOTONES DE ACCIÓN SOBRE EL SNAPSHOT SELECCIONADO
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>Restaura el snapshot seleccionado (INSERT IF NOT EXISTS).</summary>
        private void btnRestaurarSnapshot_Click(object sender, EventArgs e)
        {
            if (!VerificarSeleccion()) return;
            int id = SnapId();
            string etiqueta = SnapEtiqueta();
            string fecha = SnapFecha();

            if (MessageBox.Show(
                    $"⚠️ ¿Restaurar el snapshot?\n\n" +
                    $"Etiqueta: {etiqueta}\nFecha: {fecha}\n\n" +
                    "Registros con el mismo ID serán omitidos.\n¿Continuar?",
                    "Confirmar restauración", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
                != DialogResult.Yes) return;

            try
            {
                SetBusy(btnRestaurarSnapshot, "♻️ Restaurar", "⏳ Restaurando...");
                EjecutarScriptDesdeBD(id);
                SetIdle(btnRestaurarSnapshot, "♻️ Restaurar");
                AgregarLog($"✅ Snapshot restaurado: \"{etiqueta}\" ({fecha})");
                MessageBox.Show($"✅ Snapshot \"{etiqueta}\" restaurado.",
                    "Restauración completada", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex) { SetIdle(btnRestaurarSnapshot, "♻️ Restaurar"); AgregarLog("❌ " + ex.Message); MsgErr(ex.Message); }
        }

        /// <summary>Permite cambiar la etiqueta del snapshot seleccionado.</summary>
        private void btnRenombrarSnapshot_Click(object sender, EventArgs e)
        {
            if (!VerificarSeleccion()) return;
            int id = SnapId();
            string etiquetaActual = SnapEtiqueta();

            // Mini-diálogo reutilizando la misma lógica
            Form dlg = new Form
            {
                Text = "Renombrar Snapshot",
                Size = new Size(430, 155),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false,
                BackColor = Color.FromArgb(236, 240, 241)
            };
            var lbl = new Label
            {
                Text = "Nueva etiqueta:",
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.FromArgb(52, 73, 94),
                Location = new Point(15, 18),
                Size = new Size(200, 20)
            };
            var txt = new TextBox
            {
                Font = new Font("Segoe UI", 10F),
                Location = new Point(15, 40),
                Size = new Size(395, 28),
                MaxLength = 90,
                Text = etiquetaActual
            };
            Button btnOk = MakeBtn("✅ Guardar", Color.FromArgb(142, 68, 173), new Point(200, 80), new Size(100, 34));
            btnOk.DialogResult = DialogResult.OK;
            Button btnCx = MakeBtn("✗ Cancelar", Color.FromArgb(149, 165, 166), new Point(308, 80), new Size(102, 34));
            btnCx.DialogResult = DialogResult.Cancel;
            dlg.Controls.AddRange(new Control[] { lbl, txt, btnOk, btnCx });
            dlg.AcceptButton = btnOk; dlg.CancelButton = btnCx;

            if (dlg.ShowDialog(this) != DialogResult.OK) return;
            string nueva = txt.Text.Trim();
            if (string.IsNullOrWhiteSpace(nueva)) return;

            try
            {
                using (var con = new SqlConnection(CD_Conexion.Conn))
                {
                    con.Open();
                    var cmd = new SqlCommand($"UPDATE [dbo].[{TABLA_SNAPSHOTS}] SET etiqueta=@et WHERE id=@id", con);
                    cmd.Parameters.AddWithValue("@et", nueva);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
                AgregarLog($"✏️ Snapshot renombrado: \"{etiquetaActual}\" → \"{nueva}\"");
                RefrescarListaSnapshots();
            }
            catch (Exception ex) { MsgErr(ex.Message); }
        }

        /// <summary>Elimina el snapshot seleccionado de la BD.</summary>
        private void btnEliminarSnapshot_Click(object sender, EventArgs e)
        {
            if (!VerificarSeleccion()) return;
            int id = SnapId();
            string etiqueta = SnapEtiqueta();

            if (MessageBox.Show($"¿Eliminar el snapshot \"{etiqueta}\"?\nEsta acción no se puede deshacer.",
                    "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                != DialogResult.Yes) return;

            try
            {
                using (var con = new SqlConnection(CD_Conexion.Conn))
                {
                    con.Open();
                    var cmd = new SqlCommand($"DELETE FROM [dbo].[{TABLA_SNAPSHOTS}] WHERE id=@id", con);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
                AgregarLog($"🗑️ Snapshot eliminado: \"{etiqueta}\"");
                RefrescarListaSnapshots();
            }
            catch (Exception ex) { MsgErr(ex.Message); }
        }

        // ═════════════════════════════════════════════════════════════════════
        // C) VOLVER A LA ACTUALIDAD
        // ═════════════════════════════════════════════════════════════════════
        private void btnVolverActualidad_Click(object sender, EventArgs e)
        {
            int idUsar = -1;
            string etiquetaUsar = "";

            // Si hay fila seleccionada en el grid, preguntar
            if (dgvSnapshots.SelectedRows.Count > 0)
            {
                string etSel = SnapEtiqueta();
                string feSel = SnapFecha();
                DialogResult resp = MessageBox.Show(
                    $"Snapshot seleccionado:\n  \"{etSel}\"  ({feSel})\n\n" +
                    "• Sí  → restaura el snapshot seleccionado\n" +
                    "• No  → restaura el MÁS RECIENTE automáticamente\n" +
                    "• Cancelar → volver sin hacer nada",
                    "Volver a la Actualidad", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                if (resp == DialogResult.Cancel) return;
                if (resp == DialogResult.Yes) { idUsar = SnapId(); etiquetaUsar = etSel; }
            }

            // Tomar el más reciente si no se eligió manualmente
            if (idUsar < 0)
            {
                try
                {
                    using (var con = new SqlConnection(CD_Conexion.Conn))
                    {
                        var cmd = new SqlCommand(
                            $"SELECT TOP 1 id, etiqueta FROM [dbo].[{TABLA_SNAPSHOTS}] ORDER BY fecha_creacion DESC", con);
                        using (var r = cmd.ExecuteReader())
                        {
                            if (!r.Read())
                            {
                                MsgWarn("No hay snapshots guardados.\nGenere primero un Snapshot Automático.");
                                return;
                            }
                            idUsar = r.GetInt32(0); etiquetaUsar = r.GetString(1);
                        }
                    }
                }
                catch (Exception ex) { MsgErr(ex.Message); return; }
            }

            if (MessageBox.Show(
                    $"¿Restaurar \"{etiquetaUsar}\" como estado actual?\n" +
                    "Registros con el mismo ID serán omitidos.",
                    "Confirmar — Volver a la Actualidad",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            try
            {
                SetBusy(btnVolverActualidad, "🔄 Volver a la Actualidad", "⏳ Restaurando...");
                EjecutarScriptDesdeBD(idUsar);
                SetIdle(btnVolverActualidad, "🔄 Volver a la Actualidad");
                AgregarLog($"✅ Actualidad restaurada desde: \"{etiquetaUsar}\"");
                MessageBox.Show($"✅ Estado restaurado desde:\n\"{etiquetaUsar}\"",
                    "Actualidad restaurada", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                SetIdle(btnVolverActualidad, "🔄 Volver a la Actualidad");
                AgregarLog("❌ " + ex.Message); MsgErr(ex.Message);
            }
        }

        // ═════════════════════════════════════════════════════════════════════
        // D) EXPORTAR CSV
        // ═════════════════════════════════════════════════════════════════════
        private void btnBackupCSV_Click(object sender, EventArgs e)
        {
            List<string> modulos = ObtenerModulosSeleccionados();
            if (modulos.Count == 0) { MsgWarn("Seleccione al menos un módulo."); return; }

            FolderBrowserDialog dlg = new FolderBrowserDialog
            { Description = "Seleccione la carpeta destino", ShowNewFolderButton = true };
            if (dlg.ShowDialog() != DialogResult.OK) return;

            try
            {
                SetBusy(btnBackupCSV, "📊 Exportar CSV", "⏳ Exportando...", modulos.Count);
                string carpeta = Path.Combine(dlg.SelectedPath, $"VetBackup_CSV_{DateTime.Now:yyyyMMdd_HHmm}");
                Directory.CreateDirectory(carpeta);
                int total = 0, archivos = 0;

                foreach (string modulo in modulos)
                {
                    if (!ModulosTablas.TryGetValue(modulo, out string[] tablas)) continue;
                    foreach (string tabla in tablas)
                    {
                        DataTable dt = ObtenerDatosTabla(tabla);
                        if (dt == null || dt.Rows.Count == 0) continue;
                        EscribirCSV(dt, Path.Combine(carpeta, $"{tabla}.csv"));
                        total += dt.Rows.Count; archivos++;
                    }
                    progressBar.Value++; Application.DoEvents();
                }

                SetIdle(btnBackupCSV, "📊 Exportar CSV");
                AgregarLog($"✅ CSV: {archivos} archivos | {total} registros | {carpeta}");
                MessageBox.Show($"✅ Exportación CSV completada.\n\nArchivos: {archivos} | Registros: {total}\nCarpeta: {carpeta}",
                    "CSV exportado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                System.Diagnostics.Process.Start("explorer.exe", carpeta);
            }
            catch (Exception ex) { SetIdle(btnBackupCSV, "📊 Exportar CSV"); MsgErr(ex.Message); AgregarLog("❌ " + ex.Message); }
        }

        // ═════════════════════════════════════════════════════════════════════
        // E) RESTAURAR DESDE ARCHIVO SQL
        // ═════════════════════════════════════════════════════════════════════
        private void btnRestaurar_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog
            { Title = "Archivo de respaldo", Filter = "Archivo SQL|*.sql|Todos|*.*" };
            if (dlg.ShowDialog() != DialogResult.OK) return;

            if (MessageBox.Show(
                    $"⚠️ La importación puede sobrescribir datos.\n\nArchivo: {Path.GetFileName(dlg.FileName)}\n\n¿Continuar?",
                    "Confirmar restauración", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
                != DialogResult.Yes) return;

            try
            {
                SetBusy(btnRestaurar, "📥 Importar .SQL", "⏳ Restaurando...");
                EjecutarScriptSQL(File.ReadAllText(dlg.FileName, Encoding.UTF8));
                SetIdle(btnRestaurar, "📥 Importar .SQL");
                AgregarLog($"✅ Restaurado desde archivo: {Path.GetFileName(dlg.FileName)}");
                MessageBox.Show($"✅ Restauración completada.\nArchivo: {Path.GetFileName(dlg.FileName)}",
                    "Restauración completada", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex) { SetIdle(btnRestaurar, "📥 Importar .SQL"); MsgErr(ex.Message); AgregarLog("❌ " + ex.Message); }
        }

        // ═════════════════════════════════════════════════════════════════════
        // F) IMPORTAR CSV
        // ═════════════════════════════════════════════════════════════════════
        private void btnImportarCSV_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog
            { Title = "Importar CSV", Filter = "CSV|*.csv", Multiselect = true };
            if (dlg.ShowDialog() != DialogResult.OK) return;

            if (MessageBox.Show(
                    $"Se importarán {dlg.FileNames.Length} archivo(s). Registros duplicados serán omitidos.\n\n¿Continuar?",
                    "Confirmar importación", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                != DialogResult.Yes) return;

            try
            {
                SetBusy(btnImportarCSV, "📤 Importar CSV", "⏳ Importando...", dlg.FileNames.Length);
                int total = 0; var errores = new List<string>();

                foreach (string archivo in dlg.FileNames)
                {
                    try { total += ImportarDesdeCSV(archivo); AgregarLog($"✅ {Path.GetFileName(archivo)}"); }
                    catch (Exception ex) { errores.Add($"{Path.GetFileName(archivo)}: {ex.Message}"); AgregarLog($"❌ {Path.GetFileName(archivo)}: {ex.Message}"); }
                    progressBar.Value++; Application.DoEvents();
                }

                SetIdle(btnImportarCSV, "📤 Importar CSV");
                string msg = $"✅ Importación completada.\nRegistros importados: {total}";
                if (errores.Count > 0) msg += $"\n\n⚠️ Errores ({errores.Count}):\n" + string.Join("\n", errores);
                MessageBox.Show(msg, "Importación CSV", MessageBoxButtons.OK,
                    errores.Count > 0 ? MessageBoxIcon.Warning : MessageBoxIcon.Information);
            }
            catch (Exception ex) { SetIdle(btnImportarCSV, "📤 Importar CSV"); MsgErr(ex.Message); }
        }

        // ─────────────────────────────────────────────────────────────────────
        // LOG
        // ─────────────────────────────────────────────────────────────────────
        private void btnLimpiarLog_Click(object sender, EventArgs e) => rtbLog.Clear();

        private void AgregarLog(string msg)
        {
            if (rtbLog.InvokeRequired) { rtbLog.Invoke(new Action<string>(AgregarLog), msg); return; }
            rtbLog.AppendText($"[{DateTime.Now:HH:mm:ss}]  {msg}\n");
            rtbLog.ScrollToCaret();
        }

        // ═════════════════════════════════════════════════════════════════════
        // HELPERS INTERNOS
        // ═════════════════════════════════════════════════════════════════════

        // ── Columnas de fecha por tabla ───────────────────────────────────────
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
                using (var con = new SqlConnection(CD_Conexion.Conn))
                {
                    con.Open();
                    string where = "";
                    if ((desde.HasValue || hasta.HasValue) && ColumnasFecha.TryGetValue(tabla, out string col))
                    {
                        var conds = new List<string>();
                        if (desde.HasValue) conds.Add($"[{col}] >= '{desde.Value:yyyy-MM-dd}'");
                        if (hasta.HasValue) conds.Add($"[{col}] <= '{hasta.Value:yyyy-MM-dd 23:59:59}'");
                        if (conds.Count > 0) where = " WHERE " + string.Join(" AND ", conds);
                    }
                    var dt = new DataTable(tabla);
                    new SqlDataAdapter(new SqlCommand($"SELECT * FROM [dbo].[{tabla}]{where}", con) { CommandTimeout = 120 }).Fill(dt);
                    return dt;
                }
            }
            catch { return null; }
        }

        private DataTable ObtenerDatosTabla(string tabla) => ObtenerDatosTablaConFecha(tabla, null, null);

        private StringBuilder GenerarScriptSQL(List<string> modulos, out int totalRegistros)
        {
            var sbDet = new StringBuilder();
            totalRegistros = GenerarScriptConDetalle(modulos, sbDet, out StringBuilder sb);
            return sb;
        }

        private string GenerarInsertRow(string tabla, DataColumnCollection cols, DataRow row)
        {
            var names = new List<string>(); var vals = new List<string>();
            foreach (DataColumn col in cols)
            {
                names.Add($"[{col.ColumnName}]");
                object v = row[col];
                if (v == null || v == DBNull.Value) vals.Add("NULL");
                else if (v is bool b) vals.Add(b ? "1" : "0");
                else if (v is DateTime dt) vals.Add($"'{dt:yyyy-MM-dd HH:mm:ss}'");
                else if (v is decimal || v is int || v is long || v is double || v is float)
                    vals.Add(Convert.ToString(v, System.Globalization.CultureInfo.InvariantCulture));
                else vals.Add($"N'{v.ToString().Replace("'", "''")}'");
            }
            return
                $"IF NOT EXISTS (SELECT 1 FROM [dbo].[{tabla}] WHERE [{cols[0].ColumnName}]={vals[0]})\r\n" +
                $"    INSERT INTO [dbo].[{tabla}] ({string.Join(", ", names)}) VALUES ({string.Join(", ", vals)});";
        }

        private void EjecutarScriptDesdeBD(int id)
        {
            string script;
            using (var con = new SqlConnection(CD_Conexion.Conn))
            {
                var cmd = new SqlCommand($"SELECT script_sql FROM [dbo].[{TABLA_SNAPSHOTS}] WHERE id=@id", con);
                cmd.Parameters.AddWithValue("@id", id);
                script = cmd.ExecuteScalar()?.ToString() ?? "";
            }
            if (string.IsNullOrWhiteSpace(script)) throw new Exception("El snapshot está vacío o no se encontró.");
            EjecutarScriptSQL(script);
        }

        private void EjecutarScriptSQL(string script)
        {
            string[] bloques = script.Split(
                new[] { "\r\nGO\r\n", "\nGO\n", "\r\nGO\n", "\nGO\r\n", "\r\nGO", "\nGO" },
                StringSplitOptions.RemoveEmptyEntries);

            using (var con = new SqlConnection(CD_Conexion.Conn))
            {
                con.Open();
                progressBar.Maximum = bloques.Length; progressBar.Value = 0;
                foreach (string bloque in bloques)
                {
                    string sql = bloque.Trim();
                    if (string.IsNullOrWhiteSpace(sql) || sql.StartsWith("--")) continue;
                    try { new SqlCommand(sql, con) { CommandTimeout = 120 }.ExecuteNonQuery(); }
                    catch (SqlException ex)
                    { if (ex.Number != 8101 && ex.Number != 2627 && ex.Number != 2601) AgregarLog($"⚠️ {ex.Message}"); }
                    progressBar.Value = Math.Min(progressBar.Value + 1, progressBar.Maximum);
                    Application.DoEvents();
                }
            }
        }

        // ── CSV helpers ───────────────────────────────────────────────────────
        private void EscribirCSV(DataTable dt, string ruta)
        {
            var sb = new StringBuilder();
            var cols = new List<string>();
            foreach (DataColumn col in dt.Columns) cols.Add($"\"{col.ColumnName}\"");
            sb.AppendLine(string.Join(",", cols));
            foreach (DataRow row in dt.Rows)
            {
                var v2 = new List<string>();
                foreach (DataColumn col in dt.Columns)
                {
                    object v = row[col];
                    v2.Add(v == null || v == DBNull.Value ? "" : $"\"{v.ToString().Replace("\"", "\"\"")}\"");
                }
                sb.AppendLine(string.Join(",", v2));
            }
            File.WriteAllText(ruta, sb.ToString(), Encoding.UTF8);
        }

        private int ImportarDesdeCSV(string archivo)
        {
            string tabla = Path.GetFileNameWithoutExtension(archivo);
            bool valida = false;
            foreach (var kvp in ModulosTablas) foreach (string t in kvp.Value) if (t.Equals(tabla, StringComparison.OrdinalIgnoreCase)) { valida = true; break; }
            if (!valida) throw new Exception($"Tabla '{tabla}' no reconocida.");

            string[] lineas = File.ReadAllLines(archivo, Encoding.UTF8);
            if (lineas.Length < 2) return 0;
            string[] headers = ParsearCSV(lineas[0]);
            int importados = 0;

            using (var con = new SqlConnection(CD_Conexion.Conn))
            {
                con.Open();
                try { new SqlCommand($"SET IDENTITY_INSERT [dbo].[{tabla}] ON", con).ExecuteNonQuery(); } catch { }
                for (int i = 1; i < lineas.Length; i++)
                {
                    if (string.IsNullOrWhiteSpace(lineas[i])) continue;
                    string[] vals = ParsearCSV(lineas[i]);
                    if (vals.Length != headers.Length) continue;
                    var cn = new List<string>(); var pn = new List<string>();
                    for (int j = 0; j < headers.Length; j++) { cn.Add($"[{headers[j]}]"); pn.Add($"@p{j}"); }
                    var cmd = new SqlCommand(
                        $"IF NOT EXISTS(SELECT 1 FROM [dbo].[{tabla}] WHERE [{headers[0]}]=@p0)\r\n" +
                        $"  INSERT INTO [dbo].[{tabla}] ({string.Join(",", cn)}) VALUES ({string.Join(",", pn)})", con);
                    for (int j = 0; j < vals.Length; j++)
                        cmd.Parameters.AddWithValue($"@p{j}", string.IsNullOrEmpty(vals[j]) ? (object)DBNull.Value : vals[j]);
                    try { cmd.ExecuteNonQuery(); importados++; } catch { }
                }
                try { new SqlCommand($"SET IDENTITY_INSERT [dbo].[{tabla}] OFF", con).ExecuteNonQuery(); } catch { }
            }
            return importados;
        }

        private string[] ParsearCSV(string linea)
        {
            var campos = new List<string>(); bool q = false; var f = new StringBuilder();
            for (int i = 0; i < linea.Length; i++)
            {
                char c = linea[i];
                if (c == '"') { if (q && i + 1 < linea.Length && linea[i + 1] == '"') { f.Append('"'); i++; } else q = !q; }
                else if (c == ',' && !q) { campos.Add(f.ToString()); f.Clear(); }
                else f.Append(c);
            }
            campos.Add(f.ToString()); return campos.ToArray();
        }

        // ── Rango de fechas ───────────────────────────────────────────────────
        private void ObtenerRangoFechas(out DateTime? desde, out DateTime? hasta)
        {
            desde = null; hasta = null;
            if (rbtnHastaHoy.Checked) { desde = dtpFechaInicio.Value.Date; hasta = DateTime.Now; }
            else if (rbtnRangoFechas.Checked) { desde = dtpFechaInicio.Value.Date; hasta = dtpFechaFin.Value.Date; }
        }

        private string ObtenerDescripcionFiltroFecha()
        {
            if (rbtnSinFiltro.Checked) return "Sin filtro (todos los registros)";
            if (rbtnHastaHoy.Checked) return $"Desde {dtpFechaInicio.Value:dd/MM/yyyy} hasta hoy";
            if (rbtnRangoFechas.Checked) return $"Del {dtpFechaInicio.Value:dd/MM/yyyy} al {dtpFechaFin.Value:dd/MM/yyyy}";
            return "";
        }

        // ── Atajos para el snapshot seleccionado en el grid ───────────────────
        private bool VerificarSeleccion()
        {
            if (dgvSnapshots.SelectedRows.Count > 0) return true;
            MsgWarn("Seleccione un snapshot de la lista."); return false;
        }
        private int SnapId() => Convert.ToInt32(dgvSnapshots.CurrentRow.Cells["id"].Value);
        private string SnapEtiqueta() => dgvSnapshots.CurrentRow.Cells["etiqueta"].Value?.ToString() ?? "";
        private string SnapFecha() => Convert.ToDateTime(dgvSnapshots.CurrentRow.Cells["fecha_creacion"].Value).ToString("dd/MM/yyyy HH:mm");

        // ── UI helpers ────────────────────────────────────────────────────────
        private void SetBusy(Button btn, string textoOriginal, string textoBusy, int maxProg = 1)
        {
            btn.Enabled = false; btn.Text = textoBusy;
            progressBar.Maximum = maxProg; progressBar.Value = 0; progressBar.Visible = true;
        }
        private void SetIdle(Button btn, string texto)
        { btn.Enabled = true; btn.Text = texto; progressBar.Visible = false; }

        private Button MakeBtn(string text, Color back, Point loc, Size size)
        {
            var b = new Button
            {
                Text = text,
                BackColor = back,
                ForeColor = Color.White,
                Location = loc,
                Size = size,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            b.FlatAppearance.BorderSize = 0;
            return b;
        }

        private void MsgWarn(string msg) => MessageBox.Show(msg, "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        private void MsgErr(string msg) => MessageBox.Show("❌ " + msg, "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
}