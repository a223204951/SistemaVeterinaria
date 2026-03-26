using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace CapaPresentacion
{
    /// <summary>
    /// DESIGNER — FrmBackup
    ///
    /// LAYOUT — TabControl con 2 pestañas:
    ///
    ///   Tab 1 "💾 Backup / Importar"  (1165 × 560)
    ///     ┌─ panelConfig (285 px) ──┬─ panelAcciones (340 px) ──┬─ panelLog (fill) ─┐
    ///     │ checkboxes módulos      │ A) Backup SQL              │ log terminal       │
    ///     │ filtro de fechas        │ B) Snapshot Auto           │                    │
    ///     │                         │ C) Export CSV              │                    │
    ///     │                         │ ── sep ──                  │                    │
    ///     │                         │ D) Importar SQL            │                    │
    ///     │                         │ E) Importar CSV            │                    │
    ///     └─────────────────────────┴────────────────────────────┴────────────────────┘
    ///
    ///   Tab 2 "📸 Snapshots Automáticos"  (1165 × 560)
    ///     ┌─ panelSnapBotones (barra superior con 4 botones) ─────────────────────────┐
    ///     ├─ dgvSnapshots (grid, fill vertical) ─────────────────────────────────────┤
    ///     ├─ panelDetalle (panel inferior con info del snapshot seleccionado) ─────────┤
    ///     └───────────────────────────────────────────────────────────────────────────┘
    ///
    /// Todos los botones de la sección Snapshots son morado (142,68,173) excepto:
    ///   Eliminar → rojo (231,76,60)
    ///   Volver a la Actualidad → verde (39,174,96)
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
            // Estilos reutilizables para los dos DataGridView
            DataGridViewCellStyle csHeader = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(52, 73, 94),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                SelectionBackColor = Color.FromArgb(52, 73, 94),
                Alignment = DataGridViewContentAlignment.MiddleLeft
            };
            DataGridViewCellStyle csCell = new DataGridViewCellStyle
            {
                BackColor = Color.White,
                ForeColor = Color.FromArgb(52, 73, 94),
                Font = new Font("Segoe UI", 9F),
                SelectionBackColor = Color.FromArgb(142, 68, 173),
                SelectionForeColor = Color.White
            };

            // ── Declaraciones ─────────────────────────────────────────────────
            this.lblTitulo = new Label();
            this.lblSubtitulo = new Label();
            this.tabControl = new TabControl();
            this.tabBackup = new TabPage();
            this.tabSnapshots = new TabPage();
            this.progressBar = new ProgressBar();

            // Tab 1 controles
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
            this.lblBackupTitulo = new Label();
            this.lblBackupDesc = new Label();
            this.btnBackupSQL = new Button();
            this.lblBackupSQLDesc = new Label();
            this.btnSnapshotAuto = new Button();
            this.lblSnapshotDesc = new Label();
            this.btnBackupCSV = new Button();
            this.lblBackupCSVDesc = new Label();
            this.panelSep = new Panel();
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

            // Tab 2 controles
            this.panelSnapBotones = new Panel();
            this.lblSnapTitulo = new Label();
            this.btnRestaurarSnapshot = new Button();
            this.btnRenombrarSnapshot = new Button();
            this.btnEliminarSnapshot = new Button();
            this.btnVolverActualidad = new Button();
            this.dgvSnapshots = new DataGridView();
            this.panelDetalle = new Panel();
            this.lblDetTitulo = new Label();
            this.lblDetEtiquetaLbl = new Label();
            this.lblDetEtiqueta = new Label();
            this.lblDetFechaLbl = new Label();
            this.lblDetFecha = new Label();
            this.lblDetFiltroLbl = new Label();
            this.lblDetFiltro = new Label();
            this.lblDetModulosLbl = new Label();
            this.lblDetModulos = new Label();
            this.lblDetRegistrosLbl = new Label();
            this.lblDetRegistros = new Label();
            this.lblDetTablasLbl = new Label();
            this.rtbDetTablas = new RichTextBox();

            // ── Suspend ───────────────────────────────────────────────────────
            this.tabControl.SuspendLayout();
            this.tabBackup.SuspendLayout();
            this.tabSnapshots.SuspendLayout();
            this.panelConfig.SuspendLayout();
            this.panelModulos.SuspendLayout();
            this.panelFechas.SuspendLayout();
            this.panelAcciones.SuspendLayout();
            this.panelLog.SuspendLayout();
            this.panelSnapBotones.SuspendLayout();
            ((ISupportInitialize)this.dgvSnapshots).BeginInit();
            this.panelDetalle.SuspendLayout();
            this.SuspendLayout();

            // ══════════════════════════════════════════════════════════════════
            // ENCABEZADO DEL FORMULARIO
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
            this.lblSubtitulo.Text = "Respaldo segmentado por módulo y fecha  •  Snapshots automáticos en BD  •  Exportar / Importar";

            // ══════════════════════════════════════════════════════════════════
            // TAB CONTROL
            // ══════════════════════════════════════════════════════════════════
            this.tabControl.Location = new Point(15, 78);
            this.tabControl.Size = new Size(1168, 608);
            this.tabControl.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.tabControl.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.tabControl.TabPages.Add(this.tabBackup);
            this.tabControl.TabPages.Add(this.tabSnapshots);

            this.tabBackup.Text = "💾 Backup / Importar";
            this.tabBackup.BackColor = Color.FromArgb(236, 240, 241);
            this.tabBackup.Controls.AddRange(new Control[]
                { this.panelConfig, this.panelAcciones, this.panelLog });

            this.tabSnapshots.Text = "📸 Snapshots Automáticos";
            this.tabSnapshots.BackColor = Color.FromArgb(236, 240, 241);
            this.tabSnapshots.Controls.AddRange(new Control[]
                { this.panelSnapBotones, this.dgvSnapshots, this.panelDetalle });

            // ══════════════════════════════════════════════════════════════════
            // TAB 1 — PANEL CONFIG (izquierdo, 285 px)
            // ══════════════════════════════════════════════════════════════════
            this.panelConfig.BackColor = Color.White;
            this.panelConfig.BorderStyle = BorderStyle.FixedSingle;
            this.panelConfig.Location = new Point(8, 8);
            this.panelConfig.Size = new Size(285, 570);
            this.panelConfig.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Bottom;
            this.panelConfig.Controls.Add(this.panelModulos);
            this.panelConfig.Controls.Add(this.panelFechas);

            // Módulos
            this.panelModulos.BackColor = Color.Transparent;
            this.panelModulos.Location = new Point(10, 10);
            this.panelModulos.Size = new Size(262, 310);
            this.panelModulos.Controls.Add(this.lblModulosTitulo);
            this.panelModulos.Controls.Add(this.flpModulos);

            this.lblModulosTitulo.Text = "📦 Módulos a respaldar";
            this.lblModulosTitulo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblModulosTitulo.ForeColor = Color.FromArgb(52, 73, 94);
            this.lblModulosTitulo.Location = new Point(0, 0);
            this.lblModulosTitulo.AutoSize = true;

            this.flpModulos.Location = new Point(0, 25);
            this.flpModulos.Size = new Size(262, 284);
            this.flpModulos.AutoScroll = true;
            this.flpModulos.FlowDirection = FlowDirection.TopDown;
            this.flpModulos.WrapContents = false;
            this.flpModulos.BackColor = Color.Transparent;

            // Filtro fechas
            this.panelFechas.BackColor = Color.FromArgb(248, 249, 250);
            this.panelFechas.BorderStyle = BorderStyle.FixedSingle;
            this.panelFechas.Location = new Point(10, 330);
            this.panelFechas.Size = new Size(262, 175);
            this.panelFechas.Controls.AddRange(new Control[]
            {
                this.lblFechasTitulo,
                this.rbtnSinFiltro, this.rbtnHastaHoy, this.rbtnRangoFechas,
                this.lblFechaInicio, this.dtpFechaInicio,
                this.lblFechaFin,   this.dtpFechaFin
            });

            this.lblFechasTitulo.Text = "📅 Filtro de fecha"; this.lblFechasTitulo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblFechasTitulo.ForeColor = Color.FromArgb(52, 73, 94); this.lblFechasTitulo.Location = new Point(10, 8); this.lblFechasTitulo.AutoSize = true;

            this.rbtnSinFiltro.Text = "Sin filtro (todos los datos)"; this.rbtnSinFiltro.Font = new Font("Segoe UI", 8.5F);
            this.rbtnSinFiltro.ForeColor = Color.FromArgb(52, 73, 94); this.rbtnSinFiltro.Location = new Point(10, 32);
            this.rbtnSinFiltro.AutoSize = true; this.rbtnSinFiltro.Checked = true; this.rbtnSinFiltro.Cursor = Cursors.Hand;
            this.rbtnSinFiltro.CheckedChanged += new System.EventHandler(this.rbtnSinFiltro_CheckedChanged);

            this.rbtnHastaHoy.Text = "Desde fecha → hasta hoy"; this.rbtnHastaHoy.Font = new Font("Segoe UI", 8.5F);
            this.rbtnHastaHoy.ForeColor = Color.FromArgb(52, 73, 94); this.rbtnHastaHoy.Location = new Point(10, 54);
            this.rbtnHastaHoy.AutoSize = true; this.rbtnHastaHoy.Cursor = Cursors.Hand;
            this.rbtnHastaHoy.CheckedChanged += new System.EventHandler(this.rbtnHastaHoy_CheckedChanged);

            this.rbtnRangoFechas.Text = "Rango específico"; this.rbtnRangoFechas.Font = new Font("Segoe UI", 8.5F);
            this.rbtnRangoFechas.ForeColor = Color.FromArgb(52, 73, 94); this.rbtnRangoFechas.Location = new Point(10, 76);
            this.rbtnRangoFechas.AutoSize = true; this.rbtnRangoFechas.Cursor = Cursors.Hand;
            this.rbtnRangoFechas.CheckedChanged += new System.EventHandler(this.rbtnRangoFechas_CheckedChanged);

            this.lblFechaInicio.Text = "Inicio:"; this.lblFechaInicio.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            this.lblFechaInicio.ForeColor = Color.FromArgb(52, 73, 94); this.lblFechaInicio.Location = new Point(10, 102);
            this.lblFechaInicio.AutoSize = true; this.lblFechaInicio.Enabled = false;

            this.dtpFechaInicio.Font = new Font("Segoe UI", 9F); this.dtpFechaInicio.Format = DateTimePickerFormat.Short;
            this.dtpFechaInicio.Location = new Point(58, 99); this.dtpFechaInicio.Size = new Size(110, 25); this.dtpFechaInicio.Enabled = false;

            this.lblFechaFin.Text = "Fin:"; this.lblFechaFin.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            this.lblFechaFin.ForeColor = Color.FromArgb(52, 73, 94); this.lblFechaFin.Location = new Point(10, 136);
            this.lblFechaFin.AutoSize = true; this.lblFechaFin.Enabled = false;

            this.dtpFechaFin.Font = new Font("Segoe UI", 9F); this.dtpFechaFin.Format = DateTimePickerFormat.Short;
            this.dtpFechaFin.Location = new Point(58, 133); this.dtpFechaFin.Size = new Size(110, 25); this.dtpFechaFin.Enabled = false;

            // ══════════════════════════════════════════════════════════════════
            // TAB 1 — PANEL ACCIONES (central, 340 px)
            // ══════════════════════════════════════════════════════════════════
            this.panelAcciones.BackColor = Color.White;
            this.panelAcciones.BorderStyle = BorderStyle.FixedSingle;
            this.panelAcciones.Location = new Point(305, 8);
            this.panelAcciones.Size = new Size(340, 570);
            this.panelAcciones.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Bottom;

            this.lblBackupTitulo.Text = "💾 Generar Respaldo"; this.lblBackupTitulo.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            this.lblBackupTitulo.ForeColor = Color.FromArgb(52, 73, 94); this.lblBackupTitulo.Location = new Point(15, 12); this.lblBackupTitulo.AutoSize = true;

            this.lblBackupDesc.Text = "Exporta los módulos seleccionados\nsegún el filtro de fecha configurado.";
            this.lblBackupDesc.Font = new Font("Segoe UI", 8.5F, FontStyle.Italic); this.lblBackupDesc.ForeColor = Color.FromArgb(127, 140, 141);
            this.lblBackupDesc.Location = new Point(15, 38); this.lblBackupDesc.AutoSize = true;

            // A) Backup SQL → morado
            this.btnBackupSQL.Text = "💾 Backup SQL (archivo)"; this.btnBackupSQL.Location = new Point(15, 76);
            this.btnBackupSQL.Size = new Size(305, 46); this.btnBackupSQL.BackColor = Color.FromArgb(142, 68, 173);
            this.btnBackupSQL.ForeColor = Color.White; this.btnBackupSQL.FlatStyle = FlatStyle.Flat;
            this.btnBackupSQL.FlatAppearance.BorderSize = 0; this.btnBackupSQL.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnBackupSQL.Cursor = Cursors.Hand; this.btnBackupSQL.Click += new System.EventHandler(this.btnBackupSQL_Click);

            this.lblBackupSQLDesc.Text = "Genera un archivo .SQL para guardar externamente.";
            this.lblBackupSQLDesc.Font = new Font("Segoe UI", 8F); this.lblBackupSQLDesc.ForeColor = Color.FromArgb(142, 68, 173);
            this.lblBackupSQLDesc.Location = new Point(15, 127); this.lblBackupSQLDesc.AutoSize = true;

            // B) Snapshot Automático → verde
            this.btnSnapshotAuto.Text = "📸 Snapshot Automático"; this.btnSnapshotAuto.Location = new Point(15, 148);
            this.btnSnapshotAuto.Size = new Size(305, 46); this.btnSnapshotAuto.BackColor = Color.FromArgb(39, 174, 96);
            this.btnSnapshotAuto.ForeColor = Color.White; this.btnSnapshotAuto.FlatStyle = FlatStyle.Flat;
            this.btnSnapshotAuto.FlatAppearance.BorderSize = 0; this.btnSnapshotAuto.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnSnapshotAuto.Cursor = Cursors.Hand; this.btnSnapshotAuto.Click += new System.EventHandler(this.btnSnapshotAuto_Click);

            this.lblSnapshotDesc.Text = "Guarda el backup en la BD (sin archivo externo).\nConsulta y gestiona en la pestaña '📸 Snapshots'.";
            this.lblSnapshotDesc.Font = new Font("Segoe UI", 8F); this.lblSnapshotDesc.ForeColor = Color.FromArgb(39, 174, 96);
            this.lblSnapshotDesc.Location = new Point(15, 199); this.lblSnapshotDesc.Size = new Size(305, 32);

            // C) CSV → azul
            this.btnBackupCSV.Text = "📊 Exportar CSV"; this.btnBackupCSV.Location = new Point(15, 238);
            this.btnBackupCSV.Size = new Size(305, 46); this.btnBackupCSV.BackColor = Color.FromArgb(52, 152, 219);
            this.btnBackupCSV.ForeColor = Color.White; this.btnBackupCSV.FlatStyle = FlatStyle.Flat;
            this.btnBackupCSV.FlatAppearance.BorderSize = 0; this.btnBackupCSV.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnBackupCSV.Cursor = Cursors.Hand; this.btnBackupCSV.Click += new System.EventHandler(this.btnBackupCSV_Click);

            this.lblBackupCSVDesc.Text = "Exporta cada tabla como .CSV en una carpeta.";
            this.lblBackupCSVDesc.Font = new Font("Segoe UI", 8F); this.lblBackupCSVDesc.ForeColor = Color.FromArgb(41, 128, 185);
            this.lblBackupCSVDesc.Location = new Point(15, 289); this.lblBackupCSVDesc.AutoSize = true;

            // Separador
            this.panelSep.BackColor = Color.FromArgb(220, 220, 220); this.panelSep.Location = new Point(15, 315);
            this.panelSep.Size = new Size(305, 1);

            // Importar
            this.lblImportTitulo.Text = "📥 Importar / Restaurar"; this.lblImportTitulo.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            this.lblImportTitulo.ForeColor = Color.FromArgb(52, 73, 94); this.lblImportTitulo.Location = new Point(15, 327); this.lblImportTitulo.AutoSize = true;

            this.lblImportDesc.Text = "Restaura datos desde un respaldo previo.\nNo sobrescribe registros con el mismo ID.";
            this.lblImportDesc.Font = new Font("Segoe UI", 8.5F, FontStyle.Italic); this.lblImportDesc.ForeColor = Color.FromArgb(127, 140, 141);
            this.lblImportDesc.Location = new Point(15, 352); this.lblImportDesc.AutoSize = true;

            // D) Importar SQL → morado
            this.btnRestaurar.Text = "📥 Importar .SQL"; this.btnRestaurar.Location = new Point(15, 386);
            this.btnRestaurar.Size = new Size(305, 46); this.btnRestaurar.BackColor = Color.FromArgb(142, 68, 173);
            this.btnRestaurar.ForeColor = Color.White; this.btnRestaurar.FlatStyle = FlatStyle.Flat;
            this.btnRestaurar.FlatAppearance.BorderSize = 0; this.btnRestaurar.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnRestaurar.Cursor = Cursors.Hand; this.btnRestaurar.Click += new System.EventHandler(this.btnRestaurar_Click);

            this.lblRestaurarDesc.Text = "Carga y ejecuta un archivo .SQL generado por este sistema.";
            this.lblRestaurarDesc.Font = new Font("Segoe UI", 8F); this.lblRestaurarDesc.ForeColor = Color.FromArgb(142, 68, 173);
            this.lblRestaurarDesc.Location = new Point(15, 437); this.lblRestaurarDesc.AutoSize = true;

            // E) Importar CSV → naranja
            this.btnImportarCSV.Text = "📤 Importar CSV(s)"; this.btnImportarCSV.Location = new Point(15, 458);
            this.btnImportarCSV.Size = new Size(305, 42); this.btnImportarCSV.BackColor = Color.FromArgb(230, 126, 34);
            this.btnImportarCSV.ForeColor = Color.White; this.btnImportarCSV.FlatStyle = FlatStyle.Flat;
            this.btnImportarCSV.FlatAppearance.BorderSize = 0; this.btnImportarCSV.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnImportarCSV.Cursor = Cursors.Hand; this.btnImportarCSV.Click += new System.EventHandler(this.btnImportarCSV_Click);

            this.lblImportarCSVDesc.Text = "Importa archivos .CSV exportados por este sistema.";
            this.lblImportarCSVDesc.Font = new Font("Segoe UI", 8F); this.lblImportarCSVDesc.ForeColor = Color.FromArgb(230, 126, 34);
            this.lblImportarCSVDesc.Location = new Point(15, 505); this.lblImportarCSVDesc.AutoSize = true;

            this.panelAcciones.Controls.AddRange(new Control[]
            {
                this.lblBackupTitulo, this.lblBackupDesc,
                this.btnBackupSQL, this.lblBackupSQLDesc,
                this.btnSnapshotAuto, this.lblSnapshotDesc,
                this.btnBackupCSV, this.lblBackupCSVDesc,
                this.panelSep,
                this.lblImportTitulo, this.lblImportDesc,
                this.btnRestaurar, this.lblRestaurarDesc,
                this.btnImportarCSV, this.lblImportarCSVDesc
            });

            // ══════════════════════════════════════════════════════════════════
            // TAB 1 — PANEL LOG (derecho, fill)
            // ══════════════════════════════════════════════════════════════════
            this.panelLog.BackColor = Color.White;
            this.panelLog.BorderStyle = BorderStyle.FixedSingle;
            this.panelLog.Location = new Point(657, 8);
            this.panelLog.Size = new Size(498, 570);
            this.panelLog.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;

            this.lblLogTitulo.Text = "📋 Registro de operaciones"; this.lblLogTitulo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblLogTitulo.ForeColor = Color.FromArgb(52, 73, 94); this.lblLogTitulo.Location = new Point(10, 10); this.lblLogTitulo.AutoSize = true;

            this.btnLimpiarLog.Text = "🗑️ Limpiar"; this.btnLimpiarLog.Location = new Point(390, 5); this.btnLimpiarLog.Size = new Size(100, 28);
            this.btnLimpiarLog.BackColor = Color.FromArgb(149, 165, 166); this.btnLimpiarLog.ForeColor = Color.White;
            this.btnLimpiarLog.FlatStyle = FlatStyle.Flat; this.btnLimpiarLog.FlatAppearance.BorderSize = 0;
            this.btnLimpiarLog.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold); this.btnLimpiarLog.Cursor = Cursors.Hand;
            this.btnLimpiarLog.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.btnLimpiarLog.Click += new System.EventHandler(this.btnLimpiarLog_Click);

            this.rtbLog.Location = new Point(10, 38); this.rtbLog.Size = new Size(480, 526);
            this.rtbLog.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
            this.rtbLog.BackColor = Color.FromArgb(30, 30, 30); this.rtbLog.ForeColor = Color.FromArgb(180, 220, 180);
            this.rtbLog.Font = new Font("Consolas", 8.5F); this.rtbLog.ReadOnly = true;
            this.rtbLog.BorderStyle = BorderStyle.None; this.rtbLog.ScrollBars = RichTextBoxScrollBars.Vertical;

            this.panelLog.Controls.AddRange(new Control[] { this.lblLogTitulo, this.btnLimpiarLog, this.rtbLog });

            // ══════════════════════════════════════════════════════════════════
            // TAB 2 — BARRA DE BOTONES (parte superior, altura fija 68 px)
            // ══════════════════════════════════════════════════════════════════
            this.panelSnapBotones.BackColor = Color.White;
            this.panelSnapBotones.BorderStyle = BorderStyle.FixedSingle;
            this.panelSnapBotones.Location = new Point(8, 8);
            this.panelSnapBotones.Size = new Size(1147, 68);
            this.panelSnapBotones.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            this.lblSnapTitulo.Text = "📸 Snapshots guardados en la base de datos";
            this.lblSnapTitulo.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.lblSnapTitulo.ForeColor = Color.FromArgb(52, 73, 94);
            this.lblSnapTitulo.Location = new Point(12, 10);
            this.lblSnapTitulo.AutoSize = true;

            // ── Los 4 botones se colocan a la DERECHA del título ──────────────
            //    Se anclan a Top|Right y se ubican desde la derecha hacia la izquierda.

            // Volver a la Actualidad → verde (más a la derecha)
            this.btnVolverActualidad.Text = "🔄 Volver a la Actualidad";
            this.btnVolverActualidad.Location = new Point(808, 12);
            this.btnVolverActualidad.Size = new Size(200, 42);
            this.btnVolverActualidad.BackColor = Color.FromArgb(39, 174, 96);
            this.btnVolverActualidad.ForeColor = Color.White;
            this.btnVolverActualidad.FlatStyle = FlatStyle.Flat;
            this.btnVolverActualidad.FlatAppearance.BorderSize = 0;
            this.btnVolverActualidad.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.btnVolverActualidad.Cursor = Cursors.Hand;
            this.btnVolverActualidad.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.btnVolverActualidad.Click += new System.EventHandler(this.btnVolverActualidad_Click);

            // Eliminar → rojo
            this.btnEliminarSnapshot.Text = "🗑️ Eliminar";
            this.btnEliminarSnapshot.Location = new Point(1016, 12);
            this.btnEliminarSnapshot.Size = new Size(120, 42);
            this.btnEliminarSnapshot.BackColor = Color.FromArgb(231, 76, 60);
            this.btnEliminarSnapshot.ForeColor = Color.White;
            this.btnEliminarSnapshot.FlatStyle = FlatStyle.Flat;
            this.btnEliminarSnapshot.FlatAppearance.BorderSize = 0;
            this.btnEliminarSnapshot.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.btnEliminarSnapshot.Cursor = Cursors.Hand;
            this.btnEliminarSnapshot.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.btnEliminarSnapshot.Click += new System.EventHandler(this.btnEliminarSnapshot_Click);

            // Renombrar → morado
            this.btnRenombrarSnapshot.Text = "✏️ Renombrar";
            this.btnRenombrarSnapshot.Location = new Point(662, 12);
            this.btnRenombrarSnapshot.Size = new Size(138, 42);
            this.btnRenombrarSnapshot.BackColor = Color.FromArgb(142, 68, 173);
            this.btnRenombrarSnapshot.ForeColor = Color.White;
            this.btnRenombrarSnapshot.FlatStyle = FlatStyle.Flat;
            this.btnRenombrarSnapshot.FlatAppearance.BorderSize = 0;
            this.btnRenombrarSnapshot.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.btnRenombrarSnapshot.Cursor = Cursors.Hand;
            this.btnRenombrarSnapshot.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.btnRenombrarSnapshot.Click += new System.EventHandler(this.btnRenombrarSnapshot_Click);

            // Cargar / Restaurar → morado
            this.btnRestaurarSnapshot.Text = "♻️ Cargar / Restaurar";
            this.btnRestaurarSnapshot.Location = new Point(508, 12);
            this.btnRestaurarSnapshot.Size = new Size(146, 42);
            this.btnRestaurarSnapshot.BackColor = Color.FromArgb(142, 68, 173);
            this.btnRestaurarSnapshot.ForeColor = Color.White;
            this.btnRestaurarSnapshot.FlatStyle = FlatStyle.Flat;
            this.btnRestaurarSnapshot.FlatAppearance.BorderSize = 0;
            this.btnRestaurarSnapshot.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.btnRestaurarSnapshot.Cursor = Cursors.Hand;
            this.btnRestaurarSnapshot.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.btnRestaurarSnapshot.Click += new System.EventHandler(this.btnRestaurarSnapshot_Click);

            this.panelSnapBotones.Controls.AddRange(new Control[]
            {
                this.lblSnapTitulo,
                this.btnRestaurarSnapshot,
                this.btnRenombrarSnapshot,
                this.btnVolverActualidad,
                this.btnEliminarSnapshot
            });

            // ══════════════════════════════════════════════════════════════════
            // TAB 2 — GRID DE SNAPSHOTS
            // ══════════════════════════════════════════════════════════════════
            this.dgvSnapshots.AllowUserToAddRows = false;
            this.dgvSnapshots.AllowUserToDeleteRows = false;
            this.dgvSnapshots.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSnapshots.BackgroundColor = Color.White;
            this.dgvSnapshots.BorderStyle = BorderStyle.None;
            this.dgvSnapshots.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvSnapshots.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            this.dgvSnapshots.ColumnHeadersDefaultCellStyle = csHeader;
            this.dgvSnapshots.ColumnHeadersHeight = 40;
            this.dgvSnapshots.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvSnapshots.DefaultCellStyle = csCell;
            this.dgvSnapshots.EnableHeadersVisualStyles = false;
            this.dgvSnapshots.GridColor = Color.FromArgb(231, 231, 231);
            this.dgvSnapshots.Location = new Point(8, 84);
            this.dgvSnapshots.Size = new Size(1147, 280);
            this.dgvSnapshots.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.dgvSnapshots.ReadOnly = true;
            this.dgvSnapshots.RowHeadersVisible = false;
            this.dgvSnapshots.RowTemplate.Height = 38;
            this.dgvSnapshots.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvSnapshots.MultiSelect = false;

            // ══════════════════════════════════════════════════════════════════
            // TAB 2 — PANEL DETALLE (parte inferior, muestra info del snapshot)
            // ══════════════════════════════════════════════════════════════════
            this.panelDetalle.BackColor = Color.White;
            this.panelDetalle.BorderStyle = BorderStyle.FixedSingle;
            this.panelDetalle.Location = new Point(8, 374);
            this.panelDetalle.Size = new Size(1147, 205);
            this.panelDetalle.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.panelDetalle.Visible = false;   // se muestra al seleccionar una fila

            // Título del panel detalle
            this.lblDetTitulo.Text = "📋 Detalle del snapshot seleccionado";
            this.lblDetTitulo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblDetTitulo.ForeColor = Color.FromArgb(142, 68, 173);
            this.lblDetTitulo.Location = new Point(12, 10);
            this.lblDetTitulo.AutoSize = true;

            // ── Columna izquierda: campos de metadatos ──────────────────────
            int xL = 12, xV = 105, yStart = 36, yStep = 28;

            this.lblDetEtiquetaLbl.Text = "Etiqueta:"; this.lblDetEtiquetaLbl.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            this.lblDetEtiquetaLbl.ForeColor = Color.FromArgb(100, 100, 100); this.lblDetEtiquetaLbl.Location = new Point(xL, yStart); this.lblDetEtiquetaLbl.AutoSize = true;
            this.lblDetEtiqueta.Text = "—"; this.lblDetEtiqueta.Font = new Font("Segoe UI", 8.5F);
            this.lblDetEtiqueta.ForeColor = Color.FromArgb(52, 73, 94); this.lblDetEtiqueta.Location = new Point(xV, yStart); this.lblDetEtiqueta.Size = new Size(330, 20);

            this.lblDetFechaLbl.Text = "Creado:"; this.lblDetFechaLbl.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            this.lblDetFechaLbl.ForeColor = Color.FromArgb(100, 100, 100); this.lblDetFechaLbl.Location = new Point(xL, yStart + yStep); this.lblDetFechaLbl.AutoSize = true;
            this.lblDetFecha.Text = "—"; this.lblDetFecha.Font = new Font("Segoe UI", 8.5F);
            this.lblDetFecha.ForeColor = Color.FromArgb(52, 73, 94); this.lblDetFecha.Location = new Point(xV, yStart + yStep); this.lblDetFecha.Size = new Size(200, 20);

            this.lblDetFiltroLbl.Text = "Filtro fecha:"; this.lblDetFiltroLbl.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            this.lblDetFiltroLbl.ForeColor = Color.FromArgb(100, 100, 100); this.lblDetFiltroLbl.Location = new Point(xL, yStart + yStep * 2); this.lblDetFiltroLbl.AutoSize = true;
            this.lblDetFiltro.Text = "—"; this.lblDetFiltro.Font = new Font("Segoe UI", 8.5F);
            this.lblDetFiltro.ForeColor = Color.FromArgb(52, 73, 94); this.lblDetFiltro.Location = new Point(xV, yStart + yStep * 2); this.lblDetFiltro.Size = new Size(330, 20);

            this.lblDetModulosLbl.Text = "Módulos:"; this.lblDetModulosLbl.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            this.lblDetModulosLbl.ForeColor = Color.FromArgb(100, 100, 100); this.lblDetModulosLbl.Location = new Point(xL, yStart + yStep * 3); this.lblDetModulosLbl.AutoSize = true;
            this.lblDetModulos.Text = "—"; this.lblDetModulos.Font = new Font("Segoe UI", 8.5F);
            this.lblDetModulos.ForeColor = Color.FromArgb(52, 73, 94); this.lblDetModulos.Location = new Point(xV, yStart + yStep * 3); this.lblDetModulos.Size = new Size(330, 40);

            this.lblDetRegistrosLbl.Text = "Total:"; this.lblDetRegistrosLbl.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            this.lblDetRegistrosLbl.ForeColor = Color.FromArgb(100, 100, 100); this.lblDetRegistrosLbl.Location = new Point(xL, yStart + yStep * 4 + 14); this.lblDetRegistrosLbl.AutoSize = true;
            this.lblDetRegistros.Text = "—"; this.lblDetRegistros.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblDetRegistros.ForeColor = Color.FromArgb(142, 68, 173); this.lblDetRegistros.Location = new Point(xV, yStart + yStep * 4 + 14); this.lblDetRegistros.AutoSize = true;

            // ── Columna derecha: detalle tabla por tabla ──────────────────────
            int xRight = 460;
            this.lblDetTablasLbl.Text = "Detalle de tablas incluidas:"; this.lblDetTablasLbl.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            this.lblDetTablasLbl.ForeColor = Color.FromArgb(100, 100, 100); this.lblDetTablasLbl.Location = new Point(xRight, yStart - 2); this.lblDetTablasLbl.AutoSize = true;

            this.rtbDetTablas.Location = new Point(xRight, yStart + 20);
            this.rtbDetTablas.Size = new Size(675, 160);
            this.rtbDetTablas.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
            this.rtbDetTablas.BackColor = Color.FromArgb(248, 249, 250);
            this.rtbDetTablas.ForeColor = Color.FromArgb(52, 73, 94);
            this.rtbDetTablas.Font = new Font("Consolas", 8.5F);
            this.rtbDetTablas.ReadOnly = true;
            this.rtbDetTablas.BorderStyle = BorderStyle.None;
            this.rtbDetTablas.ScrollBars = RichTextBoxScrollBars.Vertical;

            this.panelDetalle.Controls.AddRange(new Control[]
            {
                this.lblDetTitulo,
                this.lblDetEtiquetaLbl, this.lblDetEtiqueta,
                this.lblDetFechaLbl,    this.lblDetFecha,
                this.lblDetFiltroLbl,   this.lblDetFiltro,
                this.lblDetModulosLbl,  this.lblDetModulos,
                this.lblDetRegistrosLbl,this.lblDetRegistros,
                this.lblDetTablasLbl,   this.rtbDetTablas
            });

            // ── ProgressBar (fuera del TabControl, en la base del form) ───────
            this.progressBar.Location = new Point(15, 695);
            this.progressBar.Size = new Size(1168, 10);
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
            this.ClientSize = new Size(1200, 715);
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
            this.panelConfig.ResumeLayout(false); this.panelConfig.PerformLayout();
            this.panelModulos.ResumeLayout(false); this.panelModulos.PerformLayout();
            this.panelFechas.ResumeLayout(false); this.panelFechas.PerformLayout();
            this.panelAcciones.ResumeLayout(false); this.panelAcciones.PerformLayout();
            this.panelLog.ResumeLayout(false); this.panelLog.PerformLayout();
            this.panelSnapBotones.ResumeLayout(false); this.panelSnapBotones.PerformLayout();
            ((ISupportInitialize)this.dgvSnapshots).EndInit();
            this.panelDetalle.ResumeLayout(false); this.panelDetalle.PerformLayout();
            this.tabBackup.ResumeLayout(false);
            this.tabSnapshots.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        // ── Declaraciones de campos ───────────────────────────────────────────
        // Encabezado
        private Label lblTitulo, lblSubtitulo;
        // TabControl
        private TabControl tabControl;
        private TabPage tabBackup, tabSnapshots;
        // ProgressBar global
        private ProgressBar progressBar;

        // Tab 1 — Config
        private Panel panelConfig, panelModulos, panelFechas;
        private Label lblModulosTitulo, lblFechasTitulo;
        private FlowLayoutPanel flpModulos;
        private RadioButton rbtnSinFiltro, rbtnHastaHoy, rbtnRangoFechas;
        private Label lblFechaInicio, lblFechaFin;
        private DateTimePicker dtpFechaInicio, dtpFechaFin;

        // Tab 1 — Acciones
        private Panel panelAcciones, panelSep;
        private Label lblBackupTitulo, lblBackupDesc;
        private Button btnBackupSQL; private Label lblBackupSQLDesc;
        private Button btnSnapshotAuto; private Label lblSnapshotDesc;
        private Button btnBackupCSV; private Label lblBackupCSVDesc;
        private Label lblImportTitulo, lblImportDesc;
        private Button btnRestaurar; private Label lblRestaurarDesc;
        private Button btnImportarCSV; private Label lblImportarCSVDesc;

        // Tab 1 — Log
        private Panel panelLog;
        private Label lblLogTitulo;
        private RichTextBox rtbLog;
        private Button btnLimpiarLog;

        // Tab 2 — Snapshots
        private Panel panelSnapBotones;
        private Label lblSnapTitulo;
        private Button btnRestaurarSnapshot;
        private Button btnRenombrarSnapshot;
        private Button btnEliminarSnapshot;
        private Button btnVolverActualidad;
        private DataGridView dgvSnapshots;

        // Tab 2 — Panel detalle del snapshot seleccionado
        private Panel panelDetalle;
        private Label lblDetTitulo;
        private Label lblDetEtiquetaLbl, lblDetEtiqueta;
        private Label lblDetFechaLbl, lblDetFecha;
        private Label lblDetFiltroLbl, lblDetFiltro;
        private Label lblDetModulosLbl, lblDetModulos;
        private Label lblDetRegistrosLbl, lblDetRegistros;
        private Label lblDetTablasLbl;
        private RichTextBox rtbDetTablas;
    }
}