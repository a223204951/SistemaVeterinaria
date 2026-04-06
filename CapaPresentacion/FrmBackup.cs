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
    ///     • Botones          : Restaurar, Renombrar, Eliminar
    ///
    /// FILTROS DE SNAPSHOT (simplificados):
    ///   • Todos los datos actuales  → captura el 100% de cada tabla sin restricción de fecha.
    ///   • Desde fecha → hasta hoy   → solo registros desde la fecha indicada hasta ahora.
    ///     Las tablas sin columna de fecha siempre se incluyen completas en ambos casos.
    ///
    /// RESTAURACIÓN DESTRUCTIVA:
    ///   Al restaurar un snapshot la BD queda igual que en el momento de la captura:
    ///     • Registros ausentes en el snapshot → se ELIMINAN de la BD.
    ///     • Registros presentes pero distintos → se ACTUALIZAN.
    ///     • Registros nuevos (no existen en BD) → se INSERTAN.
    ///     • Registros idénticos → no se tocan.
    ///   Las tablas _vet_snapshots y sesiones_usuario están protegidas.
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
                { "Productos",   new[] { "categoria_producto", "producto", "historial_precios", "movimiento_stock" } },
                { "Proveedores", new[] { "proveedor", "proveedor_producto" } },
                { "Ventas",      new[] { "venta",     "detalle_venta" } },
                { "Compras",     new[] { "compra",    "detalle_compra" } },
                { "Citas",       new[] { "cita",      "consulta", "pago" } },
            };

        // Tablas que nunca se deben eliminar/modificar durante una restauración.
        private static readonly HashSet<string> TablasProtegidas =
            new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            {
                "_vet_snapshots",
                "sesiones_usuario"
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

            CargarModulos();
            ActualizarEstadoFechas();
            GarantizarTablaSnapshots();
            RefrescarListaSnapshots();

            dgvSnapshots.SelectionChanged += DgvSnapshots_SelectionChanged;
        }

        // ─────────────────────────────────────────────────────────────────────
        // MÓDULOS
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
            flpModulos.Controls.Add(new Panel
            {
                Width = 220,
                Height = 1,
                BackColor = Color.FromArgb(220, 220, 220),
                Margin = new Padding(3, 4, 3, 4)
            });

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
        // FILTRO DE FECHAS (simplificado)
        //   rbtnTodosDatos → sin filtro de fecha
        //   rbtnDesdeHasta → desde la fecha elegida hasta hoy
        // ─────────────────────────────────────────────────────────────────────
        private void rbtnTodosDatos_CheckedChanged(object sender, EventArgs e) => ActualizarEstadoFechas();
        private void rbtnDesdeHasta_CheckedChanged(object sender, EventArgs e) => ActualizarEstadoFechas();

        private void ActualizarEstadoFechas()
        {
            bool necesitaFecha = rbtnDesdeHasta.Checked;
            lblFechaInicio.Enabled = necesitaFecha;
            dtpFechaInicio.Enabled = necesitaFecha;
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
                var sbDet = new StringBuilder();
                int total = GenerarScriptConDetalle(modulos, sbDet, out StringBuilder sb);
                File.WriteAllText(dlg.FileName, sb.ToString(), Encoding.UTF8);
                SetIdle(btnBackupSQL, "💾 Backup SQL (archivo)");
                AgregarLog($"✅ Backup SQL: {Path.GetFileName(dlg.FileName)} | Módulos: {modulos.Count} | Registros: {total}");
                MessageBox.Show(
                    $"✅ Respaldo SQL generado.\n\nArchivo: {Path.GetFileName(dlg.FileName)}\n" +
                    $"Módulos: {modulos.Count} | Registros: {total}\nTamaño: {new FileInfo(dlg.FileName).Length / 1024.0:N1} KB",
                    "Backup completado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                SetIdle(btnBackupSQL, "💾 Backup SQL (archivo)");
                MsgErr(ex.Message); AgregarLog("❌ " + ex.Message);
            }
        }

        // ═════════════════════════════════════════════════════════════════════
        // B) SNAPSHOT AUTOMÁTICO
        // ═════════════════════════════════════════════════════════════════════

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
                            [id]              INT IDENTITY(1,1) PRIMARY KEY,
                            [etiqueta]        NVARCHAR(100) NOT NULL,
                            [fecha_creacion]  DATETIME      NOT NULL DEFAULT GETDATE(),
                            [modulos]         NVARCHAR(500) NOT NULL,
                            [filtro_fecha]    NVARCHAR(100) NOT NULL DEFAULT 'Todos los datos actuales',
                            [total_registros] INT           NOT NULL DEFAULT 0,
                            [detalle_tablas]  NVARCHAR(MAX) NULL,
                            [script_sql]      NVARCHAR(MAX) NOT NULL
                        );", con).ExecuteNonQuery();

                    AgregarColumnasSiNoExisten(con);
                }
            }
            catch (Exception ex) { AgregarLog($"⚠️ GarantizarTablaSnapshots: {ex.Message}"); }
        }

        private void AgregarColumnasSiNoExisten(SqlConnection con)
        {
            var columnas = new[]
            {
                ("filtro_fecha",    "NVARCHAR(100) NOT NULL DEFAULT 'Todos los datos actuales'"),
                ("total_registros", "INT NOT NULL DEFAULT 0"),
                ("detalle_tablas",  "NVARCHAR(MAX) NULL"),
            };
            foreach (var (col, def) in columnas)
                new SqlCommand($@"IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS
                                  WHERE TABLE_NAME='{TABLA_SNAPSHOTS}' AND COLUMN_NAME='{col}')
                                  ALTER TABLE [dbo].[{TABLA_SNAPSHOTS}] ADD [{col}] {def};",
                    con).ExecuteNonQuery();
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

                var detalle = new StringBuilder();
                int totalRegistros = GenerarScriptConDetalle(modulos, detalle, out StringBuilder sbScript);
                string filtroDesc = ObtenerDescripcionFiltroFecha();
                string modulosStr = string.Join(", ", modulos);

                using (var con = new SqlConnection(CD_Conexion.Conn))
                {
                    con.Open();
                    var cmd = new SqlCommand(
                        $"INSERT INTO [dbo].[{TABLA_SNAPSHOTS}] " +
                        "(etiqueta, modulos, filtro_fecha, total_registros, detalle_tablas, script_sql) " +
                        "VALUES (@et,@mod,@filtro,@total,@det,@sql)", con)
                    { CommandTimeout = 180 };
                    cmd.Parameters.AddWithValue("@et", etiqueta);
                    cmd.Parameters.AddWithValue("@mod", modulosStr);
                    cmd.Parameters.AddWithValue("@filtro", filtroDesc);
                    cmd.Parameters.AddWithValue("@total", totalRegistros);
                    cmd.Parameters.AddWithValue("@det", detalle.ToString());
                    cmd.Parameters.AddWithValue("@sql", sbScript.ToString());
                    cmd.ExecuteNonQuery();
                }

                SetIdle(btnSnapshotAuto, "📸 Snapshot Automático");
                AgregarLog($"✅ Snapshot \"{etiqueta}\" | Módulos: {modulos.Count} | Registros: {totalRegistros}");
                RefrescarListaSnapshots();

                MessageBox.Show(
                    $"✅ Snapshot guardado en la base de datos.\n\n" +
                    $"Etiqueta:  {etiqueta}\nMódulos:   {modulosStr}\n" +
                    $"Filtro:    {filtroDesc}\nRegistros: {totalRegistros}\n\n" +
                    "Consúltalo en la pestaña '📸 Snapshots'.",
                    "Snapshot completado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                SetIdle(btnSnapshotAuto, "📸 Snapshot Automático");
                AgregarLog("❌ " + ex.Message); MsgErr(ex.Message);
            }
        }

        // ── Generación del script ─────────────────────────────────────────────

        /// <summary>
        /// Genera el script SQL de respaldo y rellena detalleTablas.
        /// El script utiliza sentencias IF NOT EXISTS / ELSE IF para soportar
        /// restauración destructiva (INSERT nuevos, UPDATE cambiados).
        /// Los marcadores -- INICIO_TABLA:x / -- FIN_TABLA:x permiten luego
        /// identificar qué tablas cubre el script y eliminar los sobrantes.
        /// </summary>
        private int GenerarScriptConDetalle(List<string> modulos,
            StringBuilder detalleTablas, out StringBuilder sbScript)
        {
            int totalGlobal = 0;
            sbScript = new StringBuilder();
            ObtenerRangoFechas(out DateTime? desde);

            sbScript.AppendLine($"-- BACKUP VeterinariaBD — {DateTime.Now:dd/MM/yyyy HH:mm:ss}");
            sbScript.AppendLine($"-- Usuario: {FrmLogin.UsuarioActual}");
            sbScript.AppendLine($"-- Filtro: {ObtenerDescripcionFiltroFecha()}");
            sbScript.AppendLine("USE [VeterinariaBD]");
            sbScript.AppendLine("GO");
            sbScript.AppendLine("SET NOCOUNT ON;");
            sbScript.AppendLine();

            foreach (string modulo in modulos)
            {
                if (!ModulosTablas.TryGetValue(modulo, out string[] tablas)) continue;
                sbScript.AppendLine($"-- MÓDULO: {modulo.ToUpper()}");

                foreach (string tabla in tablas)
                {
                    DataTable dt = ObtenerDatosTablaConFecha(tabla, desde);
                    int n = dt?.Rows.Count ?? 0;
                    detalleTablas.AppendLine($"{tabla}: {n} registros");
                    totalGlobal += n;

                    sbScript.AppendLine($"-- INICIO_TABLA:{tabla}");

                    if (n == 0)
                    {
                        // Tabla vacía en el snapshot: al restaurar se vaciará
                        sbScript.AppendLine($"-- (sin registros)");
                    }
                    else
                    {
                        sbScript.AppendLine($"-- [{tabla}] — {n} registros");
                        foreach (DataRow row in dt.Rows)
                            sbScript.Append(GenerarUpsertRow(tabla, dt.Columns, row));
                        sbScript.AppendLine("GO");
                    }

                    sbScript.AppendLine($"-- FIN_TABLA:{tabla}");
                    sbScript.AppendLine();
                }

                progressBar.Value = Math.Min(progressBar.Value + 1, progressBar.Maximum);
                Application.DoEvents();
            }

            sbScript.AppendLine($"-- FIN — Total registros: {totalGlobal}");
            return totalGlobal;
        }

        /// <summary>
        /// Genera una sentencia de INSERT-si-no-existe / UPDATE-si-cambió para una fila.
        /// Formato:
        ///   IF NOT EXISTS ... BEGIN IDENTITY_INSERT ON; INSERT; IDENTITY_INSERT OFF; END
        ///   ELSE IF (algún campo cambió) BEGIN UPDATE ... END
        /// </summary>
        private string GenerarUpsertRow(string tabla, DataColumnCollection cols, DataRow row)
        {
            string pkCol = cols[0].ColumnName;
            string pkValStr = FormatearValor(row[cols[0]], cols[0]);

            var names = new List<string>();
            var vals = new List<string>();
            var setCols = new List<string>();
            var difConds = new List<string>();

            foreach (DataColumn col in cols)
            {
                string valStr = FormatearValor(row[col], col);
                names.Add($"[{col.ColumnName}]");
                vals.Add(valStr);

                if (col.ColumnName != pkCol)
                {
                    setCols.Add($"[{col.ColumnName}] = {valStr}");
                    if (valStr == "NULL")
                        difConds.Add($"([{col.ColumnName}] IS NOT NULL)");
                    else
                        difConds.Add($"(ISNULL(CAST([{col.ColumnName}] AS NVARCHAR(MAX)),'') <> ISNULL(CAST({valStr} AS NVARCHAR(MAX)),'' ))");
                }
            }

            var sb = new StringBuilder();
            sb.AppendLine($"IF NOT EXISTS (SELECT 1 FROM [dbo].[{tabla}] WHERE [{pkCol}]={pkValStr})");
            sb.AppendLine($"BEGIN");
            sb.AppendLine($"    SET IDENTITY_INSERT [dbo].[{tabla}] ON;");
            sb.AppendLine($"    INSERT INTO [dbo].[{tabla}] ({string.Join(",", names)}) VALUES ({string.Join(",", vals)});");
            sb.AppendLine($"    SET IDENTITY_INSERT [dbo].[{tabla}] OFF;");
            sb.AppendLine($"END");

            if (setCols.Count > 0 && difConds.Count > 0)
            {
                sb.AppendLine($"ELSE IF ({string.Join(" OR ", difConds)})");
                sb.AppendLine($"    UPDATE [dbo].[{tabla}] SET {string.Join(", ", setCols)} WHERE [{pkCol}]={pkValStr};");
            }

            return sb.ToString();
        }

        private string FormatearValor(object v, DataColumn col)
        {
            if (v == null || v == DBNull.Value) return "NULL";
            if (v is bool b) return b ? "1" : "0";
            if (v is DateTime dt) return $"'{dt:yyyy-MM-dd HH:mm:ss}'";
            if (v is decimal || v is int || v is long || v is double || v is float)
                return Convert.ToString(v, System.Globalization.CultureInfo.InvariantCulture);
            return $"N'{v.ToString().Replace("'", "''")}'";
        }

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
            var lbl = new Label { Text = "Título descriptivo (los metadatos se guardarán automáticamente):", Font = new Font("Segoe UI", 9F), ForeColor = Color.FromArgb(52, 73, 94), Location = new Point(15, 18), Size = new Size(395, 20) };
            var txt = new TextBox { Font = new Font("Segoe UI", 10F), Location = new Point(15, 44), Size = new Size(395, 28), MaxLength = 90, Text = $"Snapshot {DateTime.Now:dd/MM/yyyy HH:mm}" };
            Button btnOk = MakeBtn("✅ Guardar", Color.FromArgb(142, 68, 173), new Point(200, 96), new Size(100, 36)); btnOk.DialogResult = DialogResult.OK;
            Button btnCx = MakeBtn("✗ Cancelar", Color.FromArgb(149, 165, 166), new Point(308, 96), new Size(102, 36)); btnCx.DialogResult = DialogResult.Cancel;
            dlg.Controls.AddRange(new Control[] { lbl, txt, btnOk, btnCx });
            dlg.AcceptButton = btnOk; dlg.CancelButton = btnCx;
            return dlg.ShowDialog(this) == DialogResult.OK
                ? (string.IsNullOrWhiteSpace(txt.Text) ? $"Snapshot {DateTime.Now:dd/MM/yyyy HH:mm}" : txt.Text.Trim())
                : null;
        }

        // ─────────────────────────────────────────────────────────────────────
        // GRID DE SNAPSHOTS
        // ─────────────────────────────────────────────────────────────────────
        private void RefrescarListaSnapshots()
        {
            LimpiarDetalleSnapshot();
            try
            {
                var dt = new DataTable();
                using (var con = new SqlConnection(CD_Conexion.Conn))
                using (var adapter = new SqlDataAdapter(
                    $"SELECT id, etiqueta, fecha_creacion, modulos, filtro_fecha, total_registros, " +
                    $"LEN(script_sql)/1024 AS tamano_kb " +
                    $"FROM [dbo].[{TABLA_SNAPSHOTS}] ORDER BY fecha_creacion DESC", con))
                {
                    adapter.Fill(dt);
                }
                dgvSnapshots.DataSource = dt;
                ConfigurarColumnasSnapshots();
            }
            catch (Exception ex) { AgregarLog($"⚠️ RefrescarListaSnapshots: {ex.Message}"); }
        }

        private void ConfigurarColumnasSnapshots()
        {
            if (dgvSnapshots.Columns.Count == 0) return;
            if (dgvSnapshots.Columns.Contains("id")) dgvSnapshots.Columns["id"].Visible = false;
            if (dgvSnapshots.Columns.Contains("etiqueta")) { dgvSnapshots.Columns["etiqueta"].HeaderText = "Etiqueta"; dgvSnapshots.Columns["etiqueta"].FillWeight = 30; }
            if (dgvSnapshots.Columns.Contains("fecha_creacion")) { dgvSnapshots.Columns["fecha_creacion"].HeaderText = "Creación"; dgvSnapshots.Columns["fecha_creacion"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm"; dgvSnapshots.Columns["fecha_creacion"].FillWeight = 18; }
            if (dgvSnapshots.Columns.Contains("modulos")) { dgvSnapshots.Columns["modulos"].HeaderText = "Módulos"; dgvSnapshots.Columns["modulos"].FillWeight = 25; }
            if (dgvSnapshots.Columns.Contains("filtro_fecha")) { dgvSnapshots.Columns["filtro_fecha"].HeaderText = "Filtro"; dgvSnapshots.Columns["filtro_fecha"].FillWeight = 18; }
            if (dgvSnapshots.Columns.Contains("total_registros")) { dgvSnapshots.Columns["total_registros"].HeaderText = "Regs."; dgvSnapshots.Columns["total_registros"].FillWeight = 10; }
            if (dgvSnapshots.Columns.Contains("tamano_kb")) { dgvSnapshots.Columns["tamano_kb"].HeaderText = "KB"; dgvSnapshots.Columns["tamano_kb"].FillWeight = 7; }
        }

        private void DgvSnapshots_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvSnapshots.SelectedRows.Count == 0) { LimpiarDetalleSnapshot(); return; }
            try
            {
                int id = Convert.ToInt32(dgvSnapshots.CurrentRow.Cells["id"].Value);
                using (var con = new SqlConnection(CD_Conexion.Conn))
                {
                    con.Open();
                    var cmd = new SqlCommand(
                        $"SELECT etiqueta,fecha_creacion,modulos,filtro_fecha,total_registros,detalle_tablas " +
                        $"FROM [dbo].[{TABLA_SNAPSHOTS}] WHERE id=@id", con);
                    cmd.Parameters.AddWithValue("@id", id);
                    using (var r = cmd.ExecuteReader())
                    {
                        if (!r.Read()) { LimpiarDetalleSnapshot(); return; }
                        lblDetEtiqueta.Text = r["etiqueta"]?.ToString() ?? "";
                        lblDetFecha.Text = Convert.ToDateTime(r["fecha_creacion"]).ToString("dd/MM/yyyy HH:mm:ss");
                        lblDetModulos.Text = r["modulos"]?.ToString() ?? "";
                        lblDetFiltro.Text = r["filtro_fecha"]?.ToString() ?? "Todos los datos actuales";
                        lblDetRegistros.Text = $"{r["total_registros"]} registros totales";
                        rtbDetTablas.Text = r["detalle_tablas"]?.ToString() ?? "(sin detalle)";
                    }
                }
                panelDetalle.Visible = true;
            }
            catch (Exception ex) { AgregarLog($"⚠️ DgvSnapshots_SelectionChanged: {ex.Message}"); LimpiarDetalleSnapshot(); }
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
        // BOTONES DEL GRID
        // ─────────────────────────────────────────────────────────────────────

        private void btnRestaurarSnapshot_Click(object sender, EventArgs e)
        {
            if (!VerificarSeleccion()) return;
            int id = SnapId();
            string etiqueta = SnapEtiqueta();
            string fecha = SnapFecha();

            if (MessageBox.Show(
                    $"⚠️ RESTAURACIÓN COMPLETA — ¿Continuar?\n\n" +
                    $"Snapshot: \"{etiqueta}\"\nFecha:    {fecha}\n\n" +
                    "• Los registros AUSENTES en el snapshot serán ELIMINADOS.\n" +
                    "• Los registros con CAMBIOS serán ACTUALIZADOS.\n" +
                    "• Los registros NUEVOS serán INSERTADOS.\n" +
                    "• Los registros IDÉNTICOS no se tocarán.\n\n" +
                    "Esta operación no se puede deshacer. ¿Continuar?",
                    "Confirmar restauración completa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
                != DialogResult.Yes) return;

            try
            {
                SetBusy(btnRestaurarSnapshot, "♻️ Restaurar", "⏳ Restaurando...");

                string script;
                using (var con = new SqlConnection(CD_Conexion.Conn))
                {
                    con.Open();
                    var cmd = new SqlCommand(
                        $"SELECT script_sql FROM [dbo].[{TABLA_SNAPSHOTS}] WHERE id=@id", con);
                    cmd.Parameters.AddWithValue("@id", id);
                    script = cmd.ExecuteScalar()?.ToString() ?? "";
                }

                if (string.IsNullOrWhiteSpace(script))
                    throw new Exception("El snapshot está vacío o no se encontró.");

                var tablasEnSnapshot = ExtraerTablasDelScript(script);
                EjecutarRestauracionDestructiva(script, tablasEnSnapshot);

                SetIdle(btnRestaurarSnapshot, "♻️ Restaurar");
                AgregarLog($"✅ Restauración completa: \"{etiqueta}\" ({fecha})");
                MessageBox.Show(
                    $"✅ Snapshot \"{etiqueta}\" restaurado correctamente.\n\n" +
                    "La base de datos refleja ahora el estado del snapshot.",
                    "Restauración completada", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                SetIdle(btnRestaurarSnapshot, "♻️ Restaurar");
                AgregarLog("❌ " + ex.Message); MsgErr(ex.Message);
            }
        }

        // ── Restauración destructiva ──────────────────────────────────────────

        private List<string> ExtraerTablasDelScript(string script)
        {
            var tablas = new List<string>();
            foreach (string linea in script.Split('\n'))
            {
                string l = linea.Trim();
                if (l.StartsWith("-- INICIO_TABLA:"))
                    tablas.Add(l.Replace("-- INICIO_TABLA:", "").Trim());
            }
            return tablas;
        }

        /// <summary>
        /// Restauración completa en dos pasos dentro de una transacción:
        ///   1. Aplica los INSERT (nuevos) y UPDATE (cambiados) del script.
        ///   2. Elimina de la BD los registros cuya PK no aparece en el snapshot.
        /// Las tablas protegidas se saltan siempre.
        /// </summary>
        private void EjecutarRestauracionDestructiva(string script, List<string> tablasEnSnapshot)
        {
            using (var con = new SqlConnection(CD_Conexion.Conn))
            {
                con.Open();
                using (var tx = con.BeginTransaction())
                {
                    try
                    {
                        progressBar.Maximum = tablasEnSnapshot.Count * 2 + 1;
                        progressBar.Value = 0;

                        // PASO 1 — INSERT nuevos + UPDATE cambiados
                        string[] bloques = script.Split(
                            new[] { "\r\nGO\r\n", "\nGO\n", "\r\nGO\n", "\nGO\r\n", "\r\nGO", "\nGO" },
                            StringSplitOptions.RemoveEmptyEntries);

                        foreach (string bloque in bloques)
                        {
                            string sql = bloque.Trim();
                            if (string.IsNullOrWhiteSpace(sql) || sql.StartsWith("--")) continue;
                            try
                            {
                                new SqlCommand(sql, con, tx) { CommandTimeout = 120 }.ExecuteNonQuery();
                            }
                            catch (SqlException ex)
                            {
                                if (ex.Number != 8101 && ex.Number != 2627 && ex.Number != 2601)
                                    AgregarLog($"  ⚠️ {ex.Message}");
                            }
                        }

                        progressBar.Value = Math.Min(progressBar.Value + 1, progressBar.Maximum);
                        Application.DoEvents();

                        // PASO 2 — DELETE de registros no presentes en el snapshot
                        foreach (string tabla in tablasEnSnapshot)
                        {
                            if (TablasProtegidas.Contains(tabla))
                            {
                                AgregarLog($"  🔒 [{tabla}] protegida — omitida.");
                                progressBar.Value = Math.Min(progressBar.Value + 1, progressBar.Maximum);
                                Application.DoEvents();
                                continue;
                            }

                            string pkCol = ObtenerPkColumna(con, tx, tabla);
                            if (string.IsNullOrEmpty(pkCol))
                            {
                                AgregarLog($"  ⚠️ [{tabla}] sin PK identificable — omitida.");
                                progressBar.Value = Math.Min(progressBar.Value + 1, progressBar.Maximum);
                                Application.DoEvents();
                                continue;
                            }

                            var idsEnSnapshot = ExtraerIdsDelScript(script, tabla, pkCol);

                            try
                            {
                                int eliminados;
                                if (idsEnSnapshot.Count == 0)
                                {
                                    // El snapshot dice que la tabla debía estar vacía
                                    eliminados = new SqlCommand(
                                        $"DELETE FROM [dbo].[{tabla}]", con, tx)
                                    { CommandTimeout = 60 }.ExecuteNonQuery();
                                }
                                else
                                {
                                    string idsCSV = string.Join(",", idsEnSnapshot);
                                    eliminados = new SqlCommand(
                                        $"DELETE FROM [dbo].[{tabla}] WHERE [{pkCol}] NOT IN ({idsCSV})",
                                        con, tx)
                                    { CommandTimeout = 60 }.ExecuteNonQuery();
                                }

                                if (eliminados > 0)
                                    AgregarLog($"  🗑️ [{tabla}]: {eliminados} registro(s) eliminado(s).");
                            }
                            catch (Exception ex)
                            {
                                AgregarLog($"  ⚠️ Error al limpiar [{tabla}]: {ex.Message}");
                            }

                            progressBar.Value = Math.Min(progressBar.Value + 1, progressBar.Maximum);
                            Application.DoEvents();
                        }

                        tx.Commit();
                        progressBar.Value = progressBar.Maximum;
                        Application.DoEvents();
                    }
                    catch
                    {
                        tx.Rollback();
                        throw;
                    }
                }
            }
        }

        private string ObtenerPkColumna(SqlConnection con, SqlTransaction tx, string tabla)
        {
            try
            {
                var cmd = new SqlCommand(@"
                    SELECT TOP 1 c.COLUMN_NAME
                    FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS tc
                    JOIN INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE c
                         ON tc.CONSTRAINT_NAME = c.CONSTRAINT_NAME
                    WHERE tc.TABLE_NAME = @t AND tc.CONSTRAINT_TYPE = 'PRIMARY KEY'",
                    con, tx);
                cmd.Parameters.AddWithValue("@t", tabla);
                return cmd.ExecuteScalar()?.ToString() ?? "";
            }
            catch { return ""; }
        }

        /// <summary>
        /// Extrae los valores de PK del script para una tabla concreta.
        /// Lee entre los marcadores -- INICIO_TABLA:x / -- FIN_TABLA:x
        /// y busca las líneas "WHERE [pkCol]=valor" generadas por GenerarUpsertRow.
        /// </summary>
        private List<string> ExtraerIdsDelScript(string script, string tabla, string pkCol)
        {
            var ids = new List<string>();
            bool enTabla = false;
            string marcIni = $"-- INICIO_TABLA:{tabla}";
            string marcFin = $"-- FIN_TABLA:{tabla}";
            string patron = $"WHERE [{pkCol}]=";

            foreach (string linea in script.Split('\n'))
            {
                string l = linea.Trim();
                if (l.Equals(marcIni, StringComparison.OrdinalIgnoreCase)) { enTabla = true; continue; }
                if (l.Equals(marcFin, StringComparison.OrdinalIgnoreCase)) { enTabla = false; continue; }
                if (!enTabla) continue;

                int idx = l.IndexOf(patron, StringComparison.OrdinalIgnoreCase);
                if (idx < 0) continue;

                string resto = l.Substring(idx + patron.Length).TrimEnd(';', ' ', '\r');
                if (!string.IsNullOrWhiteSpace(resto))
                    ids.Add(resto);
            }
            return ids;
        }

        // ── Renombrar / Eliminar snapshot ─────────────────────────────────────

        private void btnRenombrarSnapshot_Click(object sender, EventArgs e)
        {
            if (!VerificarSeleccion()) return;
            int id = SnapId();
            string etiquetaActual = SnapEtiqueta();

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
            var lbl = new Label { Text = "Nueva etiqueta:", Font = new Font("Segoe UI", 9F), ForeColor = Color.FromArgb(52, 73, 94), Location = new Point(15, 18), Size = new Size(200, 20) };
            var txt = new TextBox { Font = new Font("Segoe UI", 10F), Location = new Point(15, 40), Size = new Size(395, 28), MaxLength = 90, Text = etiquetaActual };
            Button btnOk = MakeBtn("✅ Guardar", Color.FromArgb(142, 68, 173), new Point(200, 80), new Size(100, 34)); btnOk.DialogResult = DialogResult.OK;
            Button btnCx = MakeBtn("✗ Cancelar", Color.FromArgb(149, 165, 166), new Point(308, 80), new Size(102, 34)); btnCx.DialogResult = DialogResult.Cancel;
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
                AgregarLog($"✏️ Renombrado: \"{etiquetaActual}\" → \"{nueva}\"");
                RefrescarListaSnapshots();
            }
            catch (Exception ex) { MsgErr(ex.Message); }
        }

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
        // C) EXPORTAR CSV
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
                ObtenerRangoFechas(out DateTime? desde);
                int total = 0, archivos = 0;

                foreach (string modulo in modulos)
                {
                    if (!ModulosTablas.TryGetValue(modulo, out string[] tablas)) continue;
                    foreach (string tabla in tablas)
                    {
                        DataTable dt = ObtenerDatosTablaConFecha(tabla, desde);
                        if (dt == null || dt.Rows.Count == 0) continue;
                        EscribirCSV(dt, Path.Combine(carpeta, $"{tabla}.csv"));
                        total += dt.Rows.Count; archivos++;
                    }
                    progressBar.Value++; Application.DoEvents();
                }

                SetIdle(btnBackupCSV, "📊 Exportar CSV");
                AgregarLog($"✅ CSV: {archivos} archivos | {total} registros");
                MessageBox.Show($"✅ Exportación CSV completada.\n\nArchivos: {archivos} | Registros: {total}\nCarpeta: {carpeta}",
                    "CSV exportado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                System.Diagnostics.Process.Start("explorer.exe", carpeta);
            }
            catch (Exception ex) { SetIdle(btnBackupCSV, "📊 Exportar CSV"); MsgErr(ex.Message); AgregarLog("❌ " + ex.Message); }
        }

        // ═════════════════════════════════════════════════════════════════════
        // D) RESTAURAR DESDE ARCHIVO SQL
        // ═════════════════════════════════════════════════════════════════════
        private void btnRestaurar_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog { Title = "Archivo de respaldo", Filter = "Archivo SQL|*.sql|Todos|*.*" };
            if (dlg.ShowDialog() != DialogResult.OK) return;

            if (MessageBox.Show($"⚠️ La importación reemplazará datos existentes.\n\nArchivo: {Path.GetFileName(dlg.FileName)}\n\n¿Continuar?",
                    "Confirmar restauración", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;

            try
            {
                SetBusy(btnRestaurar, "📥 Importar .SQL", "⏳ Restaurando...");
                string contenido = File.ReadAllText(dlg.FileName, Encoding.UTF8);
                var tablasEnScript = ExtraerTablasDelScript(contenido);

                if (tablasEnScript.Count > 0)
                    EjecutarRestauracionDestructiva(contenido, tablasEnScript);
                else
                    EjecutarScriptSimple(contenido);

                SetIdle(btnRestaurar, "📥 Importar .SQL");
                AgregarLog($"✅ Restaurado desde: {Path.GetFileName(dlg.FileName)}");
                MessageBox.Show($"✅ Restauración completada.\nArchivo: {Path.GetFileName(dlg.FileName)}",
                    "Restauración completada", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex) { SetIdle(btnRestaurar, "📥 Importar .SQL"); MsgErr(ex.Message); AgregarLog("❌ " + ex.Message); }
        }

        private void EjecutarScriptSimple(string script)
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
                    catch (SqlException ex) { if (ex.Number != 8101 && ex.Number != 2627 && ex.Number != 2601) AgregarLog($"⚠️ {ex.Message}"); }
                    progressBar.Value = Math.Min(progressBar.Value + 1, progressBar.Maximum);
                    Application.DoEvents();
                }
            }
        }

        // ═════════════════════════════════════════════════════════════════════
        // E) IMPORTAR CSV
        // ═════════════════════════════════════════════════════════════════════
        private void btnImportarCSV_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog { Title = "Importar CSV", Filter = "CSV|*.csv", Multiselect = true };
            if (dlg.ShowDialog() != DialogResult.OK) return;

            if (MessageBox.Show($"Se importarán {dlg.FileNames.Length} archivo(s). Registros duplicados serán omitidos.\n\n¿Continuar?",
                    "Confirmar importación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

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
        // HELPERS
        // ═════════════════════════════════════════════════════════════════════

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

        private DataTable ObtenerDatosTablaConFecha(string tabla, DateTime? desde)
        {
            try
            {
                string where = "";
                if (desde.HasValue && ColumnasFecha.TryGetValue(tabla, out string col))
                    where = $" WHERE [{col}] >= '{desde.Value:yyyy-MM-dd}'";

                var dt = new DataTable(tabla);
                using (var con = new SqlConnection(CD_Conexion.Conn))
                using (var adapter = new SqlDataAdapter(
                    $"SELECT * FROM [dbo].[{tabla}]{where}", con)
                { SelectCommand = { CommandTimeout = 120 } })
                {
                    adapter.Fill(dt);
                }
                return dt;
            }
            catch { return null; }
        }

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
            foreach (var kvp in ModulosTablas)
                foreach (string t in kvp.Value)
                    if (t.Equals(tabla, StringComparison.OrdinalIgnoreCase)) { valida = true; break; }
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

        // ── Filtro de fechas ──────────────────────────────────────────────────
        private void ObtenerRangoFechas(out DateTime? desde)
        {
            desde = rbtnDesdeHasta.Checked ? dtpFechaInicio.Value.Date : (DateTime?)null;
        }

        private string ObtenerDescripcionFiltroFecha()
        {
            if (rbtnDesdeHasta.Checked) return $"Desde {dtpFechaInicio.Value:dd/MM/yyyy} hasta hoy";
            return "Todos los datos actuales";
        }

        // ── Atajos grid ───────────────────────────────────────────────────────
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
        {
            btn.Enabled = true; btn.Text = texto; progressBar.Visible = false;
        }
        private Button MakeBtn(string text, Color back, Point loc, Size size)
        {
            var b = new Button { Text = text, BackColor = back, ForeColor = Color.White, Location = loc, Size = size, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 9F, FontStyle.Bold), Cursor = Cursors.Hand };
            b.FlatAppearance.BorderSize = 0; return b;
        }
        private void MsgWarn(string msg) => MessageBox.Show(msg, "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        private void MsgErr(string msg) => MessageBox.Show("❌ " + msg, "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
}