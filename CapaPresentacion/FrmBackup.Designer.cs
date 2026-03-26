using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace CapaPresentacion
{
    /// <summary>
    /// DESIGNER — FrmBackup
    ///
    /// LAYOUT (todo en TabControl con 2 pestañas):
    ///   Pestaña 1 — "💾 Backup / Importar"
    ///     • Panel izquierdo : módulos checkboxes + filtro de fechas
    ///     • Panel central   : botones de acción (Backup SQL, Snapshot, CSV, Importar)
    ///     • Panel derecho   : log de operaciones
    ///
    ///   Pestaña 2 — "📸 Snapshots Automáticos"
    ///     • Grid con lista de snapshots guardados en BD
    ///     • Botones: Restaurar Snapshot, Eliminar, Volver a la Actualidad
    ///
    /// Colores admin (morado): Color.FromArgb(142, 68, 173)
    /// Consistentes con el resto del sistema.
    /// </summary>
    partial class FrmBackup
    {
        private IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            DataGridViewCellStyle csH = new DataGridViewCellStyle();
            DataGridViewCellStyle csC = new DataGridViewCellStyle();
            csH.BackColor = Color.FromArgb(52, 73, 94); csH.ForeColor = Color.White;
            csH.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            csH.SelectionBackColor = Color.FromArgb(52, 73, 94);
            csH.Alignment = DataGridViewContentAlignment.MiddleLeft;
            csC.BackColor = Color.White; csC.ForeColor = Color.FromArgb(52, 73, 94);
            csC.Font = new Font("Segoe UI", 9F);
            csC.SelectionBackColor = Color.FromArgb(142, 68, 173);
            csC.SelectionForeColor = Color.White;

            // ── Controles raíz ────────────────────────────────────────────────
            this.lblTitulo = new Label();
            this.lblSubtitulo = new Label();
            this.tabControl = new TabControl();
            this.tabBackup = new TabPage();
            this.tabSnapshots = new TabPage();
            this.progressBar = new ProgressBar();

            // ── TAB 1: Backup / Importar ──────────────────────────────────────
            this.panelConfig = new Panel();
            this.panelModulos = new Panel();
            this.lblModulosTitulo = new Label();
            this.flpModulos = new FlowLayoutPanel();
            this.panelFechas = new Panel();
            this.lblFechasTitulo = new Label();
            this.rbtnSinFiltro = new RadioButton();
            this.rbtnHastaHoy = new RadioButton();
            this.rbtnRangoFechas = new RadioButton();
            this.lblFechaInicio = new Label();
            this.dtpFechaInicio = new DateTimePicker();
            this.lblFechaFin = new Label();
            this.dtpFechaFin = new DateTimePicker();

            this.panelAcciones = new Panel();
            // Sección Backup
            this.lblBackupTitulo = new Label();
            this.lblBackupDesc = new Label();
            this.btnBackupSQL = new Button();
            this.lblBackupSQLDesc = new Label();
            this.btnSnapshotAuto = new Button();
            this.lblSnapshotDesc = new Label();
            this.btnBackupCSV = new Button();
            this.lblBackupCSVDesc = new Label();
            // Separador
            this.panelSep = new Panel();
            // Sección Importar
            this.lblImportTitulo = new Label();
            this.lblImportDesc = new Label();
            this.btnRestaurar = new Button();
            this.lblRestaurarDesc = new Label();
            this.btnImportarCSV = new Button();
            this.lblImportarCSVDesc = new Label();

            this.panelLog = new Panel();
            this.lblLogTitulo = new Label();
            this.rtbLog = new RichTextBox();
            this.btnLimpiarLog = new Button();

            // ── TAB 2: Snapshots Automáticos ──────────────────────────────────
            this.panelSnapshotTop = new Panel();
            this.lblSnapTitulo = new Label();
            this.lblSnapDesc = new Label();
            this.btnRestaurarSnapshot = new Button();
            this.btnEliminarSnapshot = new Button();
            this.btnVolverActualidad = new Button();
            this.dgvSnapshots = new DataGridView();

            // ── Suspend ───────────────────────────────────────────────────────
            this.tabControl.SuspendLayout();
            this.tabBackup.SuspendLayout();
            this.tabSnapshots.SuspendLayout();
            this.panelConfig.SuspendLayout();
            this.panelModulos.SuspendLayout();
            this.panelFechas.SuspendLayout();
            this.panelAcciones.SuspendLayout();
            this.panelLog.SuspendLayout();
            this.panelSnapshotTop.SuspendLayout();
            ((ISupportInitialize)this.dgvSnapshots).BeginInit();
            this.SuspendLayout();

            // ══════════════════════════════════════════════════════════════════
            // ENCABEZADO DEL FORM
            // ══════════════════════════════════════════════════════════════════
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            this.lblTitulo.ForeColor = Color.FromArgb(52, 73, 94);
            this.lblTitulo.Location = new Point(20, 18);
            this.lblTitulo.Text = "🗄️ Backup y Restauración";

            this.lblSubtitulo.AutoSize = true;
            this.lblSubtitulo.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            this.lblSubtitulo.ForeColor = Color.FromArgb(127, 140, 141);
            this.lblSubtitulo.Location = new Point(24, 55);
            this.lblSubtitulo.Text = "Respaldo segmentado por módulo y fecha  •  Snapshots automáticos  •  Exportar / Importar";

            // ══════════════════════════════════════════════════════════════════
            // TAB CONTROL
            // ══════════════════════════════════════════════════════════════════
            this.tabControl.Location = new Point(15, 78);
            this.tabControl.Size = new Size(1165, 590);
            this.tabControl.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.tabControl.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.tabControl.TabPages.Add(this.tabBackup);
            this.tabControl.TabPages.Add(this.tabSnapshots);

            // ── Tab 1 ─────────────────────────────────────────────────────────
            this.tabBackup.Text = "💾 Backup / Importar";
            this.tabBackup.BackColor = Color.FromArgb(236, 240, 241);
            this.tabBackup.Controls.AddRange(new Control[]
            {
                this.panelConfig, this.panelAcciones, this.panelLog
            });

            // ── Tab 2 ─────────────────────────────────────────────────────────
            this.tabSnapshots.Text = "📸 Snapshots Automáticos";
            this.tabSnapshots.BackColor = Color.FromArgb(236, 240, 241);
            this.tabSnapshots.Controls.AddRange(new Control[]
            {
                this.panelSnapshotTop, this.dgvSnapshots
            });

            // ══════════════════════════════════════════════════════════════════
            // TAB 1 — PANEL CONFIG (izquierdo)
            // ══════════════════════════════════════════════════════════════════
            this.panelConfig.BackColor = Color.White;
            this.panelConfig.BorderStyle = BorderStyle.FixedSingle;
            this.panelConfig.Location = new Point(10, 8);
            this.panelConfig.Size = new Size(285, 555);
            this.panelConfig.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Bottom;
            this.panelConfig.Controls.Add(this.panelModulos);
            this.panelConfig.Controls.Add(this.panelFechas);

            // ── Módulos ───────────────────────────────────────────────────────
            this.panelModulos.BackColor = Color.Transparent;
            this.panelModulos.Location = new Point(10, 10);
            this.panelModulos.Size = new Size(262, 298);
            this.panelModulos.Controls.Add(this.lblModulosTitulo);
            this.panelModulos.Controls.Add(this.flpModulos);

            this.lblModulosTitulo.Text = "📦 Módulos a respaldar";
            this.lblModulosTitulo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblModulosTitulo.ForeColor = Color.FromArgb(52, 73, 94);
            this.lblModulosTitulo.Location = new Point(0, 0);
            this.lblModulosTitulo.AutoSize = true;

            this.flpModulos.Location = new Point(0, 25);
            this.flpModulos.Size = new Size(262, 272);
            this.flpModulos.AutoScroll = true;
            this.flpModulos.FlowDirection = FlowDirection.TopDown;
            this.flpModulos.WrapContents = false;
            this.flpModulos.BackColor = Color.Transparent;

            // ── Fechas ────────────────────────────────────────────────────────
            this.panelFechas.BackColor = Color.FromArgb(248, 249, 250);
            this.panelFechas.BorderStyle = BorderStyle.FixedSingle;
            this.panelFechas.Location = new Point(10, 318);
            this.panelFechas.Size = new Size(262, 170);
            this.panelFechas.Controls.AddRange(new Control[]
            {
                this.lblFechasTitulo,
                this.rbtnSinFiltro, this.rbtnHastaHoy, this.rbtnRangoFechas,
                this.lblFechaInicio, this.dtpFechaInicio,
                this.lblFechaFin, this.dtpFechaFin
            });

            this.lblFechasTitulo.Text = "📅 Filtro de fecha";
            this.lblFechasTitulo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblFechasTitulo.ForeColor = Color.FromArgb(52, 73, 94);
            this.lblFechasTitulo.Location = new Point(10, 8);
            this.lblFechasTitulo.AutoSize = true;

            this.rbtnSinFiltro.Text = "Sin filtro (todos los datos)";
            this.rbtnSinFiltro.Font = new Font("Segoe UI", 8.5F);
            this.rbtnSinFiltro.ForeColor = Color.FromArgb(52, 73, 94);
            this.rbtnSinFiltro.Location = new Point(10, 30);
            this.rbtnSinFiltro.AutoSize = true;
            this.rbtnSinFiltro.Checked = true;
            this.rbtnSinFiltro.Cursor = Cursors.Hand;
            this.rbtnSinFiltro.CheckedChanged += new System.EventHandler(this.rbtnSinFiltro_CheckedChanged);

            this.rbtnHastaHoy.Text = "Desde fecha → hasta hoy";
            this.rbtnHastaHoy.Font = new Font("Segoe UI", 8.5F);
            this.rbtnHastaHoy.ForeColor = Color.FromArgb(52, 73, 94);
            this.rbtnHastaHoy.Location = new Point(10, 52);
            this.rbtnHastaHoy.AutoSize = true;
            this.rbtnHastaHoy.Cursor = Cursors.Hand;
            this.rbtnHastaHoy.CheckedChanged += new System.EventHandler(this.rbtnHastaHoy_CheckedChanged);

            this.rbtnRangoFechas.Text = "Rango específico";
            this.rbtnRangoFechas.Font = new Font("Segoe UI", 8.5F);
            this.rbtnRangoFechas.ForeColor = Color.FromArgb(52, 73, 94);
            this.rbtnRangoFechas.Location = new Point(10, 74);
            this.rbtnRangoFechas.AutoSize = true;
            this.rbtnRangoFechas.Cursor = Cursors.Hand;
            this.rbtnRangoFechas.CheckedChanged += new System.EventHandler(this.rbtnRangoFechas_CheckedChanged);

            this.lblFechaInicio.Text = "Inicio:";
            this.lblFechaInicio.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            this.lblFechaInicio.ForeColor = Color.FromArgb(52, 73, 94);
            this.lblFechaInicio.Location = new Point(10, 100);
            this.lblFechaInicio.AutoSize = true;
            this.lblFechaInicio.Enabled = false;

            this.dtpFechaInicio.Font = new Font("Segoe UI", 9F);
            this.dtpFechaInicio.Format = DateTimePickerFormat.Short;
            this.dtpFechaInicio.Location = new Point(55, 97);
            this.dtpFechaInicio.Size = new Size(110, 25);
            this.dtpFechaInicio.Enabled = false;

            this.lblFechaFin.Text = "Fin:";
            this.lblFechaFin.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            this.lblFechaFin.ForeColor = Color.FromArgb(52, 73, 94);
            this.lblFechaFin.Location = new Point(10, 132);
            this.lblFechaFin.AutoSize = true;
            this.lblFechaFin.Enabled = false;

            this.dtpFechaFin.Font = new Font("Segoe UI", 9F);
            this.dtpFechaFin.Format = DateTimePickerFormat.Short;
            this.dtpFechaFin.Location = new Point(55, 129);
            this.dtpFechaFin.Size = new Size(110, 25);
            this.dtpFechaFin.Enabled = false;

            // ══════════════════════════════════════════════════════════════════
            // TAB 1 — PANEL ACCIONES (central)
            // ══════════════════════════════════════════════════════════════════
            this.panelAcciones.BackColor = Color.White;
            this.panelAcciones.BorderStyle = BorderStyle.FixedSingle;
            this.panelAcciones.Location = new Point(307, 8);
            this.panelAcciones.Size = new Size(340, 555);
            this.panelAcciones.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Bottom;

            // ── Sección Backup ────────────────────────────────────────────────
            this.lblBackupTitulo.Text = "💾 Generar Respaldo";
            this.lblBackupTitulo.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            this.lblBackupTitulo.ForeColor = Color.FromArgb(52, 73, 94);
            this.lblBackupTitulo.Location = new Point(15, 12);
            this.lblBackupTitulo.AutoSize = true;

            this.lblBackupDesc.Text = "Exporta los módulos seleccionados\nsegún el filtro de fecha configurado.";
            this.lblBackupDesc.Font = new Font("Segoe UI", 8.5F, FontStyle.Italic);
            this.lblBackupDesc.ForeColor = Color.FromArgb(127, 140, 141);
            this.lblBackupDesc.Location = new Point(15, 38);
            this.lblBackupDesc.AutoSize = true;

            // Botón Backup SQL → morado (color admin)
            this.btnBackupSQL.Text = "💾 Backup SQL (archivo)";
            this.btnBackupSQL.Location = new Point(15, 78);
            this.btnBackupSQL.Size = new Size(305, 46);
            this.btnBackupSQL.BackColor = Color.FromArgb(142, 68, 173);
            this.btnBackupSQL.ForeColor = Color.White;
            this.btnBackupSQL.FlatStyle = FlatStyle.Flat;
            this.btnBackupSQL.FlatAppearance.BorderSize = 0;
            this.btnBackupSQL.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnBackupSQL.Cursor = Cursors.Hand;
            this.btnBackupSQL.Click += new System.EventHandler(this.btnBackupSQL_Click);

            this.lblBackupSQLDesc.Text = "Genera un archivo .SQL para guardar externamente.";
            this.lblBackupSQLDesc.Font = new Font("Segoe UI", 8F);
            this.lblBackupSQLDesc.ForeColor = Color.FromArgb(142, 68, 173);
            this.lblBackupSQLDesc.Location = new Point(15, 129);
            this.lblBackupSQLDesc.AutoSize = true;

            // Botón Snapshot Automático → verde (acción positiva)
            this.btnSnapshotAuto.Text = "📸 Snapshot Automático";
            this.btnSnapshotAuto.Location = new Point(15, 152);
            this.btnSnapshotAuto.Size = new Size(305, 46);
            this.btnSnapshotAuto.BackColor = Color.FromArgb(39, 174, 96);
            this.btnSnapshotAuto.ForeColor = Color.White;
            this.btnSnapshotAuto.FlatStyle = FlatStyle.Flat;
            this.btnSnapshotAuto.FlatAppearance.BorderSize = 0;
            this.btnSnapshotAuto.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnSnapshotAuto.Cursor = Cursors.Hand;
            this.btnSnapshotAuto.Click += new System.EventHandler(this.btnSnapshotAuto_Click);

            this.lblSnapshotDesc.Text = "Guarda el backup directo en la BD — sin archivo externo.\nGestiona los snapshots en la pestaña '📸 Snapshots'.";
            this.lblSnapshotDesc.Font = new Font("Segoe UI", 8F);
            this.lblSnapshotDesc.ForeColor = Color.FromArgb(39, 174, 96);
            this.lblSnapshotDesc.Location = new Point(15, 203);
            this.lblSnapshotDesc.Size = new Size(305, 32);

            // Botón CSV
            this.btnBackupCSV.Text = "📊 Exportar CSV";
            this.btnBackupCSV.Location = new Point(15, 242);
            this.btnBackupCSV.Size = new Size(305, 46);
            this.btnBackupCSV.BackColor = Color.FromArgb(52, 152, 219);
            this.btnBackupCSV.ForeColor = Color.White;
            this.btnBackupCSV.FlatStyle = FlatStyle.Flat;
            this.btnBackupCSV.FlatAppearance.BorderSize = 0;
            this.btnBackupCSV.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnBackupCSV.Cursor = Cursors.Hand;
            this.btnBackupCSV.Click += new System.EventHandler(this.btnBackupCSV_Click);

            this.lblBackupCSVDesc.Text = "Exporta cada tabla como .CSV en una carpeta.";
            this.lblBackupCSVDesc.Font = new Font("Segoe UI", 8F);
            this.lblBackupCSVDesc.ForeColor = Color.FromArgb(41, 128, 185);
            this.lblBackupCSVDesc.Location = new Point(15, 293);
            this.lblBackupCSVDesc.AutoSize = true;

            // Separador
            this.panelSep.BackColor = Color.FromArgb(220, 220, 220);
            this.panelSep.Location = new Point(15, 322);
            this.panelSep.Size = new Size(305, 1);

            // ── Sección Importar ──────────────────────────────────────────────
            this.lblImportTitulo.Text = "📥 Importar / Restaurar";
            this.lblImportTitulo.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            this.lblImportTitulo.ForeColor = Color.FromArgb(52, 73, 94);
            this.lblImportTitulo.Location = new Point(15, 334);
            this.lblImportTitulo.AutoSize = true;

            this.lblImportDesc.Text = "Restaura datos desde un respaldo previo.\nNo sobrescribe registros con el mismo ID.";
            this.lblImportDesc.Font = new Font("Segoe UI", 8.5F, FontStyle.Italic);
            this.lblImportDesc.ForeColor = Color.FromArgb(127, 140, 141);
            this.lblImportDesc.Location = new Point(15, 358);
            this.lblImportDesc.AutoSize = true;

            this.btnRestaurar.Text = "📥 Importar .SQL";
            this.btnRestaurar.Location = new Point(15, 392);
            this.btnRestaurar.Size = new Size(305, 46);
            this.btnRestaurar.BackColor = Color.FromArgb(142, 68, 173);
            this.btnRestaurar.ForeColor = Color.White;
            this.btnRestaurar.FlatStyle = FlatStyle.Flat;
            this.btnRestaurar.FlatAppearance.BorderSize = 0;
            this.btnRestaurar.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnRestaurar.Cursor = Cursors.Hand;
            this.btnRestaurar.Click += new System.EventHandler(this.btnRestaurar_Click);

            this.lblRestaurarDesc.Text = "Carga y ejecuta un archivo .SQL generado por este sistema.";
            this.lblRestaurarDesc.Font = new Font("Segoe UI", 8F);
            this.lblRestaurarDesc.ForeColor = Color.FromArgb(142, 68, 173);
            this.lblRestaurarDesc.Location = new Point(15, 443);
            this.lblRestaurarDesc.AutoSize = true;

            this.btnImportarCSV.Text = "📤 Importar CSV(s)";
            this.btnImportarCSV.Location = new Point(15, 464);
            this.btnImportarCSV.Size = new Size(305, 42);
            this.btnImportarCSV.BackColor = Color.FromArgb(230, 126, 34);
            this.btnImportarCSV.ForeColor = Color.White;
            this.btnImportarCSV.FlatStyle = FlatStyle.Flat;
            this.btnImportarCSV.FlatAppearance.BorderSize = 0;
            this.btnImportarCSV.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnImportarCSV.Cursor = Cursors.Hand;
            this.btnImportarCSV.Click += new System.EventHandler(this.btnImportarCSV_Click);

            this.lblImportarCSVDesc.Text = "Importa archivos .CSV exportados por este sistema.";
            this.lblImportarCSVDesc.Font = new Font("Segoe UI", 8F);
            this.lblImportarCSVDesc.ForeColor = Color.FromArgb(230, 126, 34);
            this.lblImportarCSVDesc.Location = new Point(15, 511);
            this.lblImportarCSVDesc.AutoSize = true;

            this.panelAcciones.Controls.AddRange(new Control[]
            {
                this.lblBackupTitulo, this.lblBackupDesc,
                this.btnBackupSQL,    this.lblBackupSQLDesc,
                this.btnSnapshotAuto, this.lblSnapshotDesc,
                this.btnBackupCSV,    this.lblBackupCSVDesc,
                this.panelSep,
                this.lblImportTitulo, this.lblImportDesc,
                this.btnRestaurar,    this.lblRestaurarDesc,
                this.btnImportarCSV,  this.lblImportarCSVDesc
            });

            // ══════════════════════════════════════════════════════════════════
            // TAB 1 — PANEL LOG (derecho)
            // ══════════════════════════════════════════════════════════════════
            this.panelLog.BackColor = Color.White;
            this.panelLog.BorderStyle = BorderStyle.FixedSingle;
            this.panelLog.Location = new Point(659, 8);
            this.panelLog.Size = new Size(492, 555);
            this.panelLog.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;

            this.lblLogTitulo.Text = "📋 Registro de operaciones";
            this.lblLogTitulo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblLogTitulo.ForeColor = Color.FromArgb(52, 73, 94);
            this.lblLogTitulo.Location = new Point(10, 10);
            this.lblLogTitulo.AutoSize = true;

            this.btnLimpiarLog.Text = "🗑️ Limpiar";
            this.btnLimpiarLog.Location = new Point(385, 5);
            this.btnLimpiarLog.Size = new Size(100, 28);
            this.btnLimpiarLog.BackColor = Color.FromArgb(149, 165, 166);
            this.btnLimpiarLog.ForeColor = Color.White;
            this.btnLimpiarLog.FlatStyle = FlatStyle.Flat;
            this.btnLimpiarLog.FlatAppearance.BorderSize = 0;
            this.btnLimpiarLog.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            this.btnLimpiarLog.Cursor = Cursors.Hand;
            this.btnLimpiarLog.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.btnLimpiarLog.Click += new System.EventHandler(this.btnLimpiarLog_Click);

            this.rtbLog.Location = new Point(10, 38);
            this.rtbLog.Size = new Size(474, 510);
            this.rtbLog.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
            this.rtbLog.BackColor = Color.FromArgb(30, 30, 30);
            this.rtbLog.ForeColor = Color.FromArgb(180, 220, 180);
            this.rtbLog.Font = new Font("Consolas", 8.5F);
            this.rtbLog.ReadOnly = true;
            this.rtbLog.BorderStyle = BorderStyle.None;
            this.rtbLog.ScrollBars = RichTextBoxScrollBars.Vertical;

            this.panelLog.Controls.AddRange(new Control[]
            {
                this.lblLogTitulo, this.btnLimpiarLog, this.rtbLog
            });

            // ══════════════════════════════════════════════════════════════════
            // TAB 2 — SNAPSHOTS
            // ══════════════════════════════════════════════════════════════════

            // Panel superior con descripción y botones de acción
            this.panelSnapshotTop.BackColor = Color.White;
            this.panelSnapshotTop.BorderStyle = BorderStyle.FixedSingle;
            this.panelSnapshotTop.Location = new Point(10, 8);
            this.panelSnapshotTop.Size = new Size(1135, 95);
            this.panelSnapshotTop.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            this.lblSnapTitulo.Text = "📸 Snapshots guardados en la base de datos";
            this.lblSnapTitulo.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            this.lblSnapTitulo.ForeColor = Color.FromArgb(52, 73, 94);
            this.lblSnapTitulo.Location = new Point(15, 10);
            this.lblSnapTitulo.AutoSize = true;

            this.lblSnapDesc.Text = "Los snapshots se almacenan internamente. Selecciona uno y usa los botones para restaurarlo o eliminarlo.";
            this.lblSnapDesc.Font = new Font("Segoe UI", 8.5F, FontStyle.Italic);
            this.lblSnapDesc.ForeColor = Color.FromArgb(127, 140, 141);
            this.lblSnapDesc.Location = new Point(15, 36);
            this.lblSnapDesc.Size = new Size(600, 18);

            // Botón Restaurar Snapshot → morado
            this.btnRestaurarSnapshot.Text = "♻️ Restaurar Snapshot";
            this.btnRestaurarSnapshot.Location = new Point(620, 12);
            this.btnRestaurarSnapshot.Size = new Size(195, 40);
            this.btnRestaurarSnapshot.BackColor = Color.FromArgb(142, 68, 173);
            this.btnRestaurarSnapshot.ForeColor = Color.White;
            this.btnRestaurarSnapshot.FlatStyle = FlatStyle.Flat;
            this.btnRestaurarSnapshot.FlatAppearance.BorderSize = 0;
            this.btnRestaurarSnapshot.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.btnRestaurarSnapshot.Cursor = Cursors.Hand;
            this.btnRestaurarSnapshot.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.btnRestaurarSnapshot.Click += new System.EventHandler(this.btnRestaurarSnapshot_Click);

            // Botón Volver a la Actualidad → verde (acción clave)
            this.btnVolverActualidad.Text = "🔄 Volver a la Actualidad";
            this.btnVolverActualidad.Location = new Point(823, 12);
            this.btnVolverActualidad.Size = new Size(210, 40);
            this.btnVolverActualidad.BackColor = Color.FromArgb(39, 174, 96);
            this.btnVolverActualidad.ForeColor = Color.White;
            this.btnVolverActualidad.FlatStyle = FlatStyle.Flat;
            this.btnVolverActualidad.FlatAppearance.BorderSize = 0;
            this.btnVolverActualidad.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.btnVolverActualidad.Cursor = Cursors.Hand;
            this.btnVolverActualidad.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.btnVolverActualidad.Click += new System.EventHandler(this.btnVolverActualidad_Click);

            // Botón Eliminar Snapshot → rojo
            this.btnEliminarSnapshot.Text = "🗑️ Eliminar";
            this.btnEliminarSnapshot.Location = new Point(1040, 12);
            this.btnEliminarSnapshot.Size = new Size(85, 40);
            this.btnEliminarSnapshot.BackColor = Color.FromArgb(231, 76, 60);
            this.btnEliminarSnapshot.ForeColor = Color.White;
            this.btnEliminarSnapshot.FlatStyle = FlatStyle.Flat;
            this.btnEliminarSnapshot.FlatAppearance.BorderSize = 0;
            this.btnEliminarSnapshot.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.btnEliminarSnapshot.Cursor = Cursors.Hand;
            this.btnEliminarSnapshot.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.btnEliminarSnapshot.Click += new System.EventHandler(this.btnEliminarSnapshot_Click);

            // Nota informativa de Volver a la Actualidad
            Label lblNotaActualidad = new Label
            {
                Text = "💡 'Volver a la Actualidad' aplica el snapshot más reciente (o el seleccionado) como estado actual del sistema.",
                Font = new Font("Segoe UI", 8F, FontStyle.Italic),
                ForeColor = Color.FromArgb(39, 174, 96),
                Location = new Point(620, 58),
                Size = new Size(505, 28)
            };

            this.panelSnapshotTop.Controls.AddRange(new Control[]
            {
                this.lblSnapTitulo, this.lblSnapDesc,
                this.btnRestaurarSnapshot,
                this.btnVolverActualidad,
                this.btnEliminarSnapshot,
                lblNotaActualidad
            });

            // DataGridView de snapshots
            this.dgvSnapshots.AllowUserToAddRows = false;
            this.dgvSnapshots.AllowUserToDeleteRows = false;
            this.dgvSnapshots.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSnapshots.BackgroundColor = Color.White;
            this.dgvSnapshots.BorderStyle = BorderStyle.None;
            this.dgvSnapshots.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvSnapshots.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            this.dgvSnapshots.ColumnHeadersDefaultCellStyle = csH;
            this.dgvSnapshots.ColumnHeadersHeight = 40;
            this.dgvSnapshots.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvSnapshots.DefaultCellStyle = csC;
            this.dgvSnapshots.EnableHeadersVisualStyles = false;
            this.dgvSnapshots.GridColor = Color.FromArgb(231, 231, 231);
            this.dgvSnapshots.Location = new Point(10, 113);
            this.dgvSnapshots.Size = new Size(1135, 430);
            this.dgvSnapshots.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.dgvSnapshots.ReadOnly = true;
            this.dgvSnapshots.RowHeadersVisible = false;
            this.dgvSnapshots.RowTemplate.Height = 38;
            this.dgvSnapshots.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // ── ProgressBar (parte inferior del form, fuera del TabControl) ───
            this.progressBar.Location = new Point(15, 678);
            this.progressBar.Size = new Size(1163, 10);
            this.progressBar.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.progressBar.Visible = false;
            this.progressBar.Style = ProgressBarStyle.Continuous;
            this.progressBar.ForeColor = Color.FromArgb(142, 68, 173);

            // ══════════════════════════════════════════════════════════════════
            // FORMULARIO
            // ══════════════════════════════════════════════════════════════════
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = Color.FromArgb(236, 240, 241);
            this.ClientSize = new Size(1200, 700);
            this.FormBorderStyle = FormBorderStyle.None;
            this.Name = "FrmBackup";
            this.Text = "Backup y Restauración";
            this.Controls.AddRange(new Control[]
            {
                this.lblTitulo, this.lblSubtitulo,
                this.tabControl,
                this.progressBar
            });
            this.Load += new System.EventHandler(this.FrmBackup_Load);

            // ── Resume ────────────────────────────────────────────────────────
            this.panelConfig.ResumeLayout(false);
            this.panelConfig.PerformLayout();
            this.panelModulos.ResumeLayout(false);
            this.panelModulos.PerformLayout();
            this.panelFechas.ResumeLayout(false);
            this.panelFechas.PerformLayout();
            this.panelAcciones.ResumeLayout(false);
            this.panelAcciones.PerformLayout();
            this.panelLog.ResumeLayout(false);
            this.panelLog.PerformLayout();
            this.panelSnapshotTop.ResumeLayout(false);
            this.panelSnapshotTop.PerformLayout();
            ((ISupportInitialize)this.dgvSnapshots).EndInit();
            this.tabBackup.ResumeLayout(false);
            this.tabSnapshots.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        // ── Declaraciones ─────────────────────────────────────────────────────
        // Encabezado
        private Label lblTitulo;
        private Label lblSubtitulo;

        // TabControl
        private TabControl tabControl;
        private TabPage tabBackup;
        private TabPage tabSnapshots;

        // ProgressBar global
        private ProgressBar progressBar;

        // Tab 1 — Panel config
        private Panel panelConfig;
        private Panel panelModulos;
        private Label lblModulosTitulo;
        private FlowLayoutPanel flpModulos;
        private Panel panelFechas;
        private Label lblFechasTitulo;
        private RadioButton rbtnSinFiltro;
        private RadioButton rbtnHastaHoy;
        private RadioButton rbtnRangoFechas;
        private Label lblFechaInicio;
        private DateTimePicker dtpFechaInicio;
        private Label lblFechaFin;
        private DateTimePicker dtpFechaFin;

        // Tab 1 — Panel acciones
        private Panel panelAcciones;
        private Label lblBackupTitulo;
        private Label lblBackupDesc;
        private Button btnBackupSQL;
        private Label lblBackupSQLDesc;
        private Button btnSnapshotAuto;
        private Label lblSnapshotDesc;
        private Button btnBackupCSV;
        private Label lblBackupCSVDesc;
        private Panel panelSep;
        private Label lblImportTitulo;
        private Label lblImportDesc;
        private Button btnRestaurar;
        private Label lblRestaurarDesc;
        private Button btnImportarCSV;
        private Label lblImportarCSVDesc;

        // Tab 1 — Panel log
        private Panel panelLog;
        private Label lblLogTitulo;
        private RichTextBox rtbLog;
        private Button btnLimpiarLog;

        // Tab 2 — Snapshots
        private Panel panelSnapshotTop;
        private Label lblSnapTitulo;
        private Label lblSnapDesc;
        private Button btnRestaurarSnapshot;
        private Button btnVolverActualidad;
        private Button btnEliminarSnapshot;
        private DataGridView dgvSnapshots;
    }
}