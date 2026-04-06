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

        private static readonly HashSet<string> TablasProtegidas =
            new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            {
                "_vet_snapshots",
                "sesiones_usuario"
            };

        // Orden de restauración que respeta FK
        private static readonly List<string> OrdenRestauracion = new List<string>
        {
            "categoria_producto", "proveedor", "cliente", "empleado", "usuario",
            "permisos_rol", "mascota", "producto", "proveedor_producto",
            "cita", "consulta", "pago",
            "compra", "detalle_compra",
            "venta", "detalle_venta",
            "historial_precios", "movimiento_stock",
            "auditoria_cliente"
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
                Margin = new System.Windows.Forms.Padding(3, 4, 3, 4)
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
            foreach (System.Windows.Forms.Control c in flpModulos.Controls)
                if (c is CheckBox ck && ck.Checked && ck.Tag?.ToString() != "TODOS")
                    lista.Add(ck.Tag.ToString());
            return lista;
        }

        // ─────────────────────────────────────────────────────────────────────
        // FILTRO FECHAS
        // ─────────────────────────────────────────────────────────────────────
        // FIX: Este método estaba referenciado en el Designer pero faltaba la implementación
        private void rbtnFiltro_CheckedChanged(object sender, EventArgs e)
        {
            ActualizarEstadoFechas();
        }

        private void ActualizarEstadoFechas()
        {
            // rbtnDesdeHasta = "Desde fecha → hasta fecha"
            // rbtnDesdeHastaHoy = "Desde fecha → hasta ahora"
            bool necesitaFechaInicio = rbtnDesdeHasta.Checked || rbtnDesdeHastaHoy.Checked;
            bool necesitaFechaFin = rbtnDesdeHasta.Checked;

            lblFechaInicio.Enabled = necesitaFechaInicio;
            dtpFechaInicio.Enabled = necesitaFechaInicio;
            lblFechaFin.Visible = necesitaFechaFin;
            dtpFechaFin.Visible = necesitaFechaFin;
            lblFechaFin.Enabled = necesitaFechaFin;
            dtpFechaFin.Enabled = necesitaFechaFin;
        }

        // ═════════════════════════════════════════════════════════════════════
        // A) BACKUP SQL A ARCHIVO
        // ═════════════════════════════════════════════════════════════════════
        private void btnBackupSQL_Click(object sender, EventArgs e)
        {
            List<string> modulos = ObtenerModulosSeleccionados();
            if (modulos.Count == 0) { MsgWarn("Seleccione al menos un módulo."); return; }

            var dlg = new System.Windows.Forms.SaveFileDialog
            {
                Title = "Guardar respaldo SQL",
                Filter = "Archivo SQL|*.sql",
                FileName = $"VeterinariaBD_Backup_{DateTime.Now:yyyyMMdd_HHmm}.sql",
                DefaultExt = "sql"
            };
            if (dlg.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;

            try
            {
                SetBusy(btnBackupSQL, "💾 Backup SQL (archivo)", "⏳ Generando...", modulos.Count);
                var sbDet = new StringBuilder();
                int total = GenerarScriptCompleto(modulos, sbDet, out StringBuilder sb);
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
                int totalRegistros = GenerarScriptCompleto(modulos, detalle, out StringBuilder sbScript);
                string filtroDesc = ObtenerDescripcionFiltroFecha();
                string modulosStr = string.Join(", ", modulos);
                string scriptTexto = sbScript.ToString();

                using (var con = new SqlConnection(CD_Conexion.Conn))
                {
                    con.Open();
                    var cmd = new SqlCommand(
                        $"INSERT INTO [dbo].[{TABLA_SNAPSHOTS}] " +
                        "(etiqueta, modulos, filtro_fecha, total_registros, detalle_tablas, script_sql) " +
                        "VALUES (@et, @mod, @filtro, @total, @det, @sql)", con);
                    cmd.CommandTimeout = 300;

                    cmd.Parameters.Add("@et", SqlDbType.NVarChar, 100).Value = etiqueta;
                    cmd.Parameters.Add("@mod", SqlDbType.NVarChar, 500).Value = modulosStr;
                    cmd.Parameters.Add("@filtro", SqlDbType.NVarChar, 100).Value = filtroDesc;
                    cmd.Parameters.Add("@total", SqlDbType.Int).Value = totalRegistros;
                    cmd.Parameters.Add("@det", SqlDbType.NVarChar, -1).Value = detalle.ToString();
                    cmd.Parameters.Add("@sql", SqlDbType.NVarChar, -1).Value = scriptTexto;

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

        // ─────────────────────────────────────────────────────────────────────
        // GENERACIÓN DEL SCRIPT
        // Cada tabla genera un bloque delimitado por marcadores:
        //   -- INICIO_TABLA:nombre
        //   ... sentencias ...
        //   -- FIN_TABLA:nombre
        //
        // Esto permite que EjecutarScript divida y ejecute cada bloque completo
        // en lugar de intentar parsear línea a línea (que rompía con BEGIN/END).
        // ─────────────────────────────────────────────────────────────────────
        private int GenerarScriptCompleto(List<string> modulos,
            StringBuilder detalleTablas, out StringBuilder sbScript)
        {
            int totalGlobal = 0;
            sbScript = new StringBuilder();
            ObtenerRangoFechas(out DateTime? desde, out DateTime? hasta);

            sbScript.AppendLine($"-- BACKUP VeterinariaBD — {DateTime.Now:dd/MM/yyyy HH:mm:ss}");
            sbScript.AppendLine($"-- Usuario: {FrmLogin.UsuarioActual}");
            sbScript.AppendLine($"-- Filtro:  {ObtenerDescripcionFiltroFecha()}");
            sbScript.AppendLine("USE [VeterinariaBD];");
            sbScript.AppendLine("SET NOCOUNT ON;");
            sbScript.AppendLine();

            // Recopilar todas las tablas en orden FK-safe
            var tablasIncluidas = new List<string>();
            foreach (string modulo in modulos)
                if (ModulosTablas.TryGetValue(modulo, out string[] tablas))
                    foreach (string t in tablas)
                        if (!tablasIncluidas.Contains(t))
                            tablasIncluidas.Add(t);

            // Ordenar según OrdenRestauracion
            var tablasOrdenadas = new List<string>();
            foreach (string t in OrdenRestauracion)
                if (tablasIncluidas.Contains(t))
                    tablasOrdenadas.Add(t);
            foreach (string t in tablasIncluidas)
                if (!tablasOrdenadas.Contains(t))
                    tablasOrdenadas.Add(t);

            foreach (string tabla in tablasOrdenadas)
            {
                DataTable dt = ObtenerDatosTablaConFecha(tabla, desde, hasta);
                int n = dt?.Rows.Count ?? 0;
                detalleTablas.AppendLine($"{tabla}: {n} registros");
                totalGlobal += n;

                bool tieneIdentity = TieneColumnaIdentity(tabla);

                // Marcador de inicio (usado por EjecutarScript para dividir bloques)
                sbScript.AppendLine($"-- INICIO_TABLA:{tabla}");

                // Construir el bloque completo de la tabla como una sola transacción
                var bloque = new StringBuilder();
                bloque.AppendLine($"BEGIN TRY");
                bloque.AppendLine($"    BEGIN TRANSACTION;");
                bloque.AppendLine($"    DELETE FROM [dbo].[{tabla}];");

                if (n > 0 && dt != null)
                {
                    if (tieneIdentity)
                        bloque.AppendLine($"    SET IDENTITY_INSERT [dbo].[{tabla}] ON;");

                    foreach (DataRow row in dt.Rows)
                        bloque.AppendLine(GenerarInsertRow(tabla, dt.Columns, row));

                    if (tieneIdentity)
                        bloque.AppendLine($"    SET IDENTITY_INSERT [dbo].[{tabla}] OFF;");
                }

                bloque.AppendLine($"    COMMIT TRANSACTION;");
                bloque.AppendLine($"END TRY");
                bloque.AppendLine($"BEGIN CATCH");
                bloque.AppendLine($"    IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;");
                bloque.AppendLine($"    PRINT 'ERROR en {tabla}: ' + ERROR_MESSAGE();");
                bloque.AppendLine($"END CATCH");

                sbScript.Append(bloque);
                sbScript.AppendLine($"-- FIN_TABLA:{tabla}");
                sbScript.AppendLine();

                progressBar.Value = Math.Min(progressBar.Value + 1, progressBar.Maximum);
                System.Windows.Forms.Application.DoEvents();
            }

            sbScript.AppendLine($"-- FIN — Total registros: {totalGlobal}");
            return totalGlobal;
        }

        private string GenerarInsertRow(string tabla, DataColumnCollection cols, DataRow row)
        {
            var names = new List<string>();
            var vals = new List<string>();

            foreach (DataColumn col in cols)
            {
                names.Add($"[{col.ColumnName}]");
                vals.Add(FormatearValor(row[col], col));
            }

            return $"    INSERT INTO [dbo].[{tabla}] ({string.Join(", ", names)}) VALUES ({string.Join(", ", vals)});";
        }

        private string FormatearValor(object v, DataColumn col)
        {
            if (v == null || v == DBNull.Value) return "NULL";
            if (v is bool b) return b ? "1" : "0";
            if (v is DateTime dt) return $"'{dt:yyyy-MM-dd HH:mm:ss.fff}'";
            if (v is decimal || v is int || v is long || v is double || v is float)
                return Convert.ToString(v, System.Globalization.CultureInfo.InvariantCulture);
            return $"N'{v.ToString().Replace("'", "''")}'";
        }

        private bool TieneColumnaIdentity(string tabla)
        {
            try
            {
                using (var con = new SqlConnection(CD_Conexion.Conn))
                {
                    con.Open();
                    var cmd = new SqlCommand(
                        "SELECT COUNT(*) FROM sys.columns c " +
                        "INNER JOIN sys.tables t ON c.object_id = t.object_id " +
                        "WHERE t.name = @t AND c.is_identity = 1", con);
                    cmd.Parameters.AddWithValue("@t", tabla);
                    return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
                }
            }
            catch { return false; }
        }

        private string MostrarDialogoEtiqueta()
        {
            var dlg = new Form
            {
                Text = "Etiqueta del Snapshot",
                Size = new Size(430, 185),
                StartPosition = System.Windows.Forms.FormStartPosition.CenterParent,
                FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false,
                BackColor = Color.FromArgb(236, 240, 241)
            };
            var lbl = new System.Windows.Forms.Label
            {
                Text = "Título descriptivo del snapshot:",
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.FromArgb(52, 73, 94),
                Location = new Point(15, 18),
                Size = new Size(395, 20)
            };
            var txt = new System.Windows.Forms.TextBox
            {
                Font = new Font("Segoe UI", 10F),
                Location = new Point(15, 44),
                Size = new Size(395, 28),
                MaxLength = 90,
                Text = $"Snapshot {DateTime.Now:dd/MM/yyyy HH:mm}"
            };
            var btnOk = MakeBtn("✅ Guardar", Color.FromArgb(142, 68, 173), new Point(200, 96), new Size(100, 36));
            btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            var btnCx = MakeBtn("✗ Cancelar", Color.FromArgb(149, 165, 166), new Point(308, 96), new Size(102, 36));
            btnCx.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            dlg.Controls.AddRange(new System.Windows.Forms.Control[] { lbl, txt, btnOk, btnCx });
            dlg.AcceptButton = btnOk; dlg.CancelButton = btnCx;
            return dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK
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
                    $"DATALENGTH(script_sql)/1024 AS tamano_kb " +
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
                        $"SELECT etiqueta, fecha_creacion, modulos, filtro_fecha, total_registros, detalle_tablas " +
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
            catch (Exception ex) { AgregarLog($"⚠️ {ex.Message}"); LimpiarDetalleSnapshot(); }
        }

        private void LimpiarDetalleSnapshot()
        {
            lblDetEtiqueta.Text = "—"; lblDetFecha.Text = "—";
            lblDetModulos.Text = "—"; lblDetFiltro.Text = "—";
            lblDetRegistros.Text = "—"; rtbDetTablas.Text = "";
            panelDetalle.Visible = false;
        }

        // ─────────────────────────────────────────────────────────────────────
        // RESTAURAR SNAPSHOT
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
                    "• Los datos actuales serán REEMPLAZADOS por los del snapshot.\n" +
                    "• Esta operación no se puede deshacer. ¿Continuar?",
                    "Confirmar restauración completa",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != System.Windows.Forms.DialogResult.Yes) return;

            try
            {
                SetBusy(btnRestaurarSnapshot, "♻️ Restaurar", "⏳ Restaurando...");

                string script;
                using (var con = new SqlConnection(CD_Conexion.Conn))
                {
                    con.Open();
                    var cmd = new SqlCommand(
                        $"SELECT CAST(script_sql AS NVARCHAR(MAX)) FROM [dbo].[{TABLA_SNAPSHOTS}] WHERE id=@id", con);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.CommandTimeout = 120;
                    object res = cmd.ExecuteScalar();
                    script = res?.ToString() ?? "";
                }

                if (string.IsNullOrWhiteSpace(script))
                    throw new Exception("El snapshot está vacío o no se pudo leer.");

                EjecutarScript(script);

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

        // ─────────────────────────────────────────────────────────────────────
        // EJECUTAR SCRIPT  — NUEVA IMPLEMENTACIÓN
        //
        // En lugar de parsear línea a línea (que rompía con BEGIN/END anidados),
        // ahora divide el script usando los marcadores INICIO_TABLA / FIN_TABLA
        // que el generador ya coloca, y ejecuta cada bloque de una tabla como
        // una sola llamada a SqlCommand. Esto garantiza:
        //   • Las transacciones BEGIN/END TRY/CATCH funcionan íntegras.
        //   • SET IDENTITY_INSERT se aplica dentro de la misma conexión/bloque.
        //   • Los errores de una tabla no abortan las demás.
        // ─────────────────────────────────────────────────────────────────────
        private void EjecutarScript(string script)
        {
            // Dividir el script en bloques usando los marcadores de tabla
            var bloques = ExtraerBloquesPorTabla(script);

            progressBar.Maximum = Math.Max(1, bloques.Count);
            progressBar.Value = 0;
            progressBar.Visible = true;

            int ejecutados = 0, errores = 0;

            using (var con = new SqlConnection(CD_Conexion.Conn))
            {
                con.Open();
                con.InfoMessage += (s, ev) => AgregarLog($"   ℹ️ {ev.Message}");

                // Paso 1: deshabilitar todas las FK para evitar errores de orden
                try
                {
                    new SqlCommand(
                        "EXEC sp_MSforeachtable 'ALTER TABLE ? NOCHECK CONSTRAINT ALL';",
                        con)
                    { CommandTimeout = 60 }.ExecuteNonQuery();
                    AgregarLog("🔓 Restricciones FK deshabilitadas");
                }
                catch (Exception ex)
                {
                    AgregarLog($"⚠️ No se pudieron deshabilitar FK: {ex.Message}");
                }

                // Paso 2: ejecutar cada bloque de tabla
                foreach (var kvp in bloques)
                {
                    string nombreTabla = kvp.Key;
                    string bloque = kvp.Value;

                    try
                    {
                        var cmd = new SqlCommand(bloque, con) { CommandTimeout = 300 };
                        cmd.ExecuteNonQuery();
                        ejecutados++;
                        AgregarLog($"   ✅ {nombreTabla}");
                    }
                    catch (SqlException ex)
                    {
                        errores++;
                        AgregarLog($"   ❌ {nombreTabla}: {ex.Message.Substring(0, Math.Min(200, ex.Message.Length))}");
                    }

                    progressBar.Value = Math.Min(progressBar.Value + 1, progressBar.Maximum);
                    System.Windows.Forms.Application.DoEvents();
                }

                // Paso 3: re-habilitar FK
                try
                {
                    new SqlCommand(
                        "EXEC sp_MSforeachtable 'ALTER TABLE ? WITH CHECK CHECK CONSTRAINT ALL';",
                        con)
                    { CommandTimeout = 60 }.ExecuteNonQuery();
                    AgregarLog("🔒 Restricciones FK re-habilitadas");
                }
                catch (Exception ex)
                {
                    AgregarLog($"⚠️ Al re-habilitar FK: {ex.Message}");
                }
            }

            progressBar.Visible = false;
            AgregarLog($"✅ Script ejecutado: {ejecutados} tablas OK | {errores} con errores");

            if (errores > 0)
                MessageBox.Show(
                    $"La restauración finalizó con {errores} error(es).\n\n" +
                    "Revisa el log para más detalles. Algunas tablas pueden no haberse restaurado.",
                    "Restauración con advertencias", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary>
        /// Divide el script en pares (nombreTabla, bloqueSQL) usando los marcadores
        /// -- INICIO_TABLA:nombre  y  -- FIN_TABLA:nombre
        /// que GenerarScriptCompleto ya inserta.
        /// </summary>
        private List<KeyValuePair<string, string>> ExtraerBloquesPorTabla(string script)
        {
            var resultado = new List<KeyValuePair<string, string>>();
            string[] lineas = script.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);

            string tablaActual = null;
            var bloqueActual = new StringBuilder();

            foreach (string linea in lineas)
            {
                string trim = linea.Trim();

                if (trim.StartsWith("-- INICIO_TABLA:"))
                {
                    tablaActual = trim.Substring("-- INICIO_TABLA:".Length).Trim();
                    bloqueActual = new StringBuilder();
                }
                else if (trim.StartsWith("-- FIN_TABLA:") && tablaActual != null)
                {
                    string bloqueTexto = bloqueActual.ToString().Trim();
                    if (!string.IsNullOrWhiteSpace(bloqueTexto))
                        resultado.Add(new KeyValuePair<string, string>(tablaActual, bloqueTexto));
                    tablaActual = null;
                    bloqueActual = new StringBuilder();
                }
                else if (tablaActual != null)
                {
                    // Ignorar líneas de comentario puras dentro del bloque
                    // (se puede dejar; SQL las ignora igual)
                    bloqueActual.AppendLine(linea);
                }
            }

            // Si el script no tiene marcadores (archivo externo antiguo), 
            // caer en el modo legado línea por línea
            if (resultado.Count == 0)
            {
                AgregarLog("⚠️ Script sin marcadores de tabla — usando modo legado (línea a línea)");
                resultado.Add(new KeyValuePair<string, string>("(script completo)", script));
            }

            return resultado;
        }

        /// <summary>
        /// Modo legado: ejecuta el script completo línea a línea.
        /// Se usa solo para archivos .SQL exportados por versiones anteriores del sistema.
        /// </summary>
        private void EjecutarScriptLegado(string script, SqlConnection con)
        {
            string[] lineas = script.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);
            int ejecutadas = 0, errores = 0;

            // Agrupar por bloques GO
            var bloques = new List<string>();
            var bloque = new StringBuilder();

            foreach (string linea in lineas)
            {
                if (linea.Trim().Equals("GO", StringComparison.OrdinalIgnoreCase))
                {
                    string t = bloque.ToString().Trim();
                    if (!string.IsNullOrWhiteSpace(t)) bloques.Add(t);
                    bloque.Clear();
                }
                else
                    bloque.AppendLine(linea);
            }
            string ultimo = bloque.ToString().Trim();
            if (!string.IsNullOrWhiteSpace(ultimo)) bloques.Add(ultimo);

            foreach (string b in bloques)
            {
                if (string.IsNullOrWhiteSpace(b) || b.TrimStart().StartsWith("--")) continue;
                try
                {
                    new SqlCommand(b, con) { CommandTimeout = 120 }.ExecuteNonQuery();
                    ejecutadas++;
                }
                catch (SqlException ex)
                {
                    errores++;
                    AgregarLog($"  ⚠️ {ex.Message.Substring(0, Math.Min(150, ex.Message.Length))}");
                }
            }
            AgregarLog($"   Modo legado: {ejecutadas} bloques OK | {errores} errores");
        }

        // ─────────────────────────────────────────────────────────────────────
        // RENOMBRAR / ELIMINAR SNAPSHOT
        // ─────────────────────────────────────────────────────────────────────
        private void btnRenombrarSnapshot_Click(object sender, EventArgs e)
        {
            if (!VerificarSeleccion()) return;
            int id = SnapId();
            string etiquetaActual = SnapEtiqueta();

            var dlg = new Form
            {
                Text = "Renombrar Snapshot",
                Size = new Size(430, 155),
                StartPosition = System.Windows.Forms.FormStartPosition.CenterParent,
                FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false,
                BackColor = Color.FromArgb(236, 240, 241)
            };
            var lbl = new System.Windows.Forms.Label { Text = "Nueva etiqueta:", Font = new Font("Segoe UI", 9F), ForeColor = Color.FromArgb(52, 73, 94), Location = new Point(15, 18), Size = new Size(200, 20) };
            var txt = new System.Windows.Forms.TextBox { Font = new Font("Segoe UI", 10F), Location = new Point(15, 40), Size = new Size(395, 28), MaxLength = 90, Text = etiquetaActual };
            var btnOk = MakeBtn("✅ Guardar", Color.FromArgb(142, 68, 173), new Point(200, 80), new Size(100, 34)); btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            var btnCx = MakeBtn("✗ Cancelar", Color.FromArgb(149, 165, 166), new Point(308, 80), new Size(102, 34)); btnCx.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            dlg.Controls.AddRange(new System.Windows.Forms.Control[] { lbl, txt, btnOk, btnCx });
            dlg.AcceptButton = btnOk; dlg.CancelButton = btnCx;

            if (dlg.ShowDialog(this) != System.Windows.Forms.DialogResult.OK) return;
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
                != System.Windows.Forms.DialogResult.Yes) return;

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

            var dlg = new System.Windows.Forms.FolderBrowserDialog
            { Description = "Seleccione la carpeta destino", ShowNewFolderButton = true };
            if (dlg.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;

            try
            {
                SetBusy(btnBackupCSV, "📊 Exportar CSV", "⏳ Exportando...", modulos.Count);
                string carpeta = Path.Combine(dlg.SelectedPath, $"VetBackup_CSV_{DateTime.Now:yyyyMMdd_HHmm}");
                Directory.CreateDirectory(carpeta);
                ObtenerRangoFechas(out DateTime? desde, out DateTime? hasta);
                int total = 0, archivos = 0;

                foreach (string modulo in modulos)
                {
                    if (!ModulosTablas.TryGetValue(modulo, out string[] tablas)) continue;
                    foreach (string tabla in tablas)
                    {
                        DataTable dt = ObtenerDatosTablaConFecha(tabla, desde, hasta);
                        if (dt == null || dt.Rows.Count == 0) continue;
                        EscribirCSV(dt, Path.Combine(carpeta, $"{tabla}.csv"));
                        total += dt.Rows.Count; archivos++;
                    }
                    progressBar.Value++; System.Windows.Forms.Application.DoEvents();
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
            var dlg = new System.Windows.Forms.OpenFileDialog
            { Title = "Archivo de respaldo", Filter = "Archivo SQL|*.sql|Todos|*.*" };
            if (dlg.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;

            if (MessageBox.Show(
                    $"⚠️ La importación reemplazará datos existentes.\n\nArchivo: {Path.GetFileName(dlg.FileName)}\n\n¿Continuar?",
                    "Confirmar restauración", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
                != System.Windows.Forms.DialogResult.Yes) return;

            try
            {
                SetBusy(btnRestaurar, "📥 Importar .SQL", "⏳ Restaurando...");
                string contenido = File.ReadAllText(dlg.FileName, Encoding.UTF8);
                EjecutarScript(contenido);
                SetIdle(btnRestaurar, "📥 Importar .SQL");
                AgregarLog($"✅ Restaurado desde: {Path.GetFileName(dlg.FileName)}");
                MessageBox.Show(
                    $"✅ Restauración completada.\nArchivo: {Path.GetFileName(dlg.FileName)}",
                    "Restauración completada", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex) { SetIdle(btnRestaurar, "📥 Importar .SQL"); MsgErr(ex.Message); AgregarLog("❌ " + ex.Message); }
        }

        // ═════════════════════════════════════════════════════════════════════
        // E) IMPORTAR CSV
        // ═════════════════════════════════════════════════════════════════════
        private void btnImportarCSV_Click(object sender, EventArgs e)
        {
            var dlg = new System.Windows.Forms.OpenFileDialog
            { Title = "Importar CSV", Filter = "CSV|*.csv", Multiselect = true };
            if (dlg.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;

            if (MessageBox.Show(
                    $"Se importarán {dlg.FileNames.Length} archivo(s). Registros duplicados serán omitidos.\n\n¿Continuar?",
                    "Confirmar importación", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                != System.Windows.Forms.DialogResult.Yes) return;

            try
            {
                SetBusy(btnImportarCSV, "📤 Importar CSV", "⏳ Importando...", dlg.FileNames.Length);
                int total = 0;
                var errores = new List<string>();

                foreach (string archivo in dlg.FileNames)
                {
                    try { total += ImportarDesdeCSV(archivo); AgregarLog($"✅ {Path.GetFileName(archivo)}"); }
                    catch (Exception ex) { errores.Add($"{Path.GetFileName(archivo)}: {ex.Message}"); AgregarLog($"❌ {Path.GetFileName(archivo)}: {ex.Message}"); }
                    progressBar.Value++; System.Windows.Forms.Application.DoEvents();
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

        // FIX: parámetro hasta agregado para soportar rbtnDesdeHasta correctamente
        private DataTable ObtenerDatosTablaConFecha(string tabla, DateTime? desde, DateTime? hasta)
        {
            try
            {
                string where = "";
                if (desde.HasValue && ColumnasFecha.TryGetValue(tabla, out string col))
                {
                    where = $" WHERE [{col}] >= '{desde.Value:yyyy-MM-dd}'";
                    if (hasta.HasValue)
                        where += $" AND [{col}] <= '{hasta.Value:yyyy-MM-dd 23:59:59}'";
                }

                var dt = new DataTable(tabla);
                using (var con = new SqlConnection(CD_Conexion.Conn))
                using (var adapter = new SqlDataAdapter($"SELECT * FROM [dbo].[{tabla}]{where}", con)
                { SelectCommand = { CommandTimeout = 120 } })
                    adapter.Fill(dt);
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

        // FIX: firma actualizada para recibir tanto 'desde' como 'hasta'
        private void ObtenerRangoFechas(out DateTime? desde, out DateTime? hasta)
        {
            if (rbtnDesdeHastaHoy.Checked)
            {
                desde = dtpFechaInicio.Value.Date;
                hasta = null; // hasta hoy (sin límite superior)
            }
            else if (rbtnDesdeHasta.Checked)
            {
                desde = dtpFechaInicio.Value.Date;
                hasta = dtpFechaFin.Value.Date;
            }
            else
            {
                desde = null;
                hasta = null;
            }
        }

        private string ObtenerDescripcionFiltroFecha()
        {
            if (rbtnDesdeHastaHoy.Checked) return $"Desde {dtpFechaInicio.Value:dd/MM/yyyy} hasta ahora";
            if (rbtnDesdeHasta.Checked) return $"Desde {dtpFechaInicio.Value:dd/MM/yyyy} hasta {dtpFechaFin.Value:dd/MM/yyyy}";
            return "Todos los datos actuales";
        }

        private bool VerificarSeleccion()
        {
            if (dgvSnapshots.SelectedRows.Count > 0) return true;
            MsgWarn("Seleccione un snapshot de la lista."); return false;
        }
        private int SnapId() => Convert.ToInt32(dgvSnapshots.CurrentRow.Cells["id"].Value);
        private string SnapEtiqueta() => dgvSnapshots.CurrentRow.Cells["etiqueta"].Value?.ToString() ?? "";
        private string SnapFecha() => Convert.ToDateTime(dgvSnapshots.CurrentRow.Cells["fecha_creacion"].Value).ToString("dd/MM/yyyy HH:mm");

        private void SetBusy(System.Windows.Forms.Button btn, string textoOriginal, string textoBusy, int maxProg = 1)
        {
            btn.Enabled = false; btn.Text = textoBusy;
            progressBar.Maximum = maxProg; progressBar.Value = 0; progressBar.Visible = true;
        }
        private void SetIdle(System.Windows.Forms.Button btn, string texto)
        {
            btn.Enabled = true; btn.Text = texto; progressBar.Visible = false;
        }
        private System.Windows.Forms.Button MakeBtn(string text, Color back, Point loc, Size size)
        {
            var b = new System.Windows.Forms.Button
            {
                Text = text,
                BackColor = back,
                ForeColor = Color.White,
                Location = loc,
                Size = size,
                FlatStyle = System.Windows.Forms.FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Cursor = System.Windows.Forms.Cursors.Hand
            };
            b.FlatAppearance.BorderSize = 0;
            return b;
        }
        private void MsgWarn(string msg) => MessageBox.Show(msg, "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        private void MsgErr(string msg) => MessageBox.Show("❌ " + msg, "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
}