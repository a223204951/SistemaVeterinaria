using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace CapaPresentacion
{
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
            // ── Estilos de celda ──────────────────────────────────────────────
            var dgvHeaderStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(52, 73, 94),
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.White,
                SelectionBackColor = Color.FromArgb(52, 73, 94),
                Alignment = DataGridViewContentAlignment.MiddleLeft
            };
            var dgvCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.White,
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.FromArgb(52, 73, 94),
                SelectionBackColor = Color.FromArgb(142, 68, 173),
                SelectionForeColor = Color.White,
                WrapMode = DataGridViewTriState.False
            };

            // ── Instanciación ─────────────────────────────────────────────────
            this.lblTitulo = new Label();
            this.lblSubtitulo = new Label();
            this.tabControl = new TabControl();

            // Tab 1
            this.tabBackup = new TabPage();
            this.panelConfig = new Panel();
            this.panelModulos = new Panel();
            this.lblModulosTitulo = new Label();
            this.flpModulos = new FlowLayoutPanel();
            this.panelFechas = new Panel();
            this.lblFechasTitulo = new Label();
            this.rbtnTodosDatos = new RadioButton();
            this.rbtnDesdeHastaHoy = new RadioButton();     // Desde X hasta HOY (ahora mismo)
            this.rbtnDesdeHasta = new RadioButton();         // Desde X hasta Y (rango fijo)
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
            this.btnLimpiarLog = new Button();
            this.rtbLog = new RichTextBox();

            // Tab 2
            this.tabSnapshots = new TabPage();
            this.panelSnapshotTop = new Panel();
            this.lblSnapTitulo = new Label();
            this.lblSnapDesc = new Label();
            this.btnRestaurarSnapshot = new Button();
            this.btnRenombrarSnapshot = new Button();
            this.btnEliminarSnapshot = new Button();
            this.dgvSnapshots = new DataGridView();

            // Panel detalle
            this.panelDetalle = new Panel();
            this.lblDetTituloInterno = new Label();
            this.lblDetEtqLabel = new Label();
            this.lblDetEtiqueta = new Label();
            this.lblDetFecLabel = new Label();
            this.lblDetFecha = new Label();
            this.lblDetModLabel = new Label();
            this.lblDetModulos = new Label();
            this.lblDetFiltLabel = new Label();
            this.lblDetFiltro = new Label();
            this.lblDetRegLabel = new Label();
            this.lblDetRegistros = new Label();
            this.lblDetTablasLabel = new Label();
            this.rtbDetTablas = new RichTextBox();

            this.progressBar = new ProgressBar();

            // Suspender layouts
            this.tabControl.SuspendLayout();
            this.tabBackup.SuspendLayout();
            this.panelConfig.SuspendLayout();
            this.panelModulos.SuspendLayout();
            this.panelFechas.SuspendLayout();
            this.panelAcciones.SuspendLayout();
            this.panelLog.SuspendLayout();
            this.tabSnapshots.SuspendLayout();
            this.panelSnapshotTop.SuspendLayout();
            this.panelDetalle.SuspendLayout();
            ((ISupportInitialize)this.dgvSnapshots).BeginInit();
            this.SuspendLayout();

            // ══════════════════════════════════════════════════════════════════
            // ENCABEZADO
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
            this.tabControl.Anchor = (AnchorStyles)(AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
            this.tabControl.Controls.Add(this.tabBackup);
            this.tabControl.Controls.Add(this.tabSnapshots);
            this.tabControl.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.tabControl.Location = new Point(15, 78);
            this.tabControl.Size = new Size(1165, 590);
            this.tabControl.SelectedIndex = 0;

            // ── Tab 1 ─────────────────────────────────────────────────────────
            this.tabBackup.BackColor = Color.FromArgb(236, 240, 241);
            this.tabBackup.Controls.AddRange(new Control[] { this.panelConfig, this.panelAcciones, this.panelLog });
            this.tabBackup.Location = new Point(4, 32);
            this.tabBackup.Size = new Size(1157, 554);
            this.tabBackup.Text = "💾 Backup / Importar";

            // panelConfig
            this.panelConfig.Anchor = (AnchorStyles)(AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left);
            this.panelConfig.BackColor = Color.White;
            this.panelConfig.BorderStyle = BorderStyle.FixedSingle;
            this.panelConfig.Controls.Add(this.panelModulos);
            this.panelConfig.Controls.Add(this.panelFechas);
            this.panelConfig.Location = new Point(10, 8);
            this.panelConfig.Size = new Size(285, 538);

            // panelModulos
            this.panelModulos.BackColor = Color.Transparent;
            this.panelModulos.Controls.Add(this.lblModulosTitulo);
            this.panelModulos.Controls.Add(this.flpModulos);
            this.panelModulos.Location = new Point(10, 10);
            this.panelModulos.Size = new Size(262, 270);

            this.lblModulosTitulo.AutoSize = true;
            this.lblModulosTitulo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblModulosTitulo.ForeColor = Color.FromArgb(52, 73, 94);
            this.lblModulosTitulo.Location = new Point(0, 0);
            this.lblModulosTitulo.Text = "📦 Módulos a respaldar";

            this.flpModulos.AutoScroll = true;
            this.flpModulos.BackColor = Color.Transparent;
            this.flpModulos.FlowDirection = FlowDirection.TopDown;
            this.flpModulos.Location = new Point(0, 25);
            this.flpModulos.Size = new Size(262, 244);
            this.flpModulos.WrapContents = false;

            // ── panelFechas — TRES opciones de filtro ─────────────────────────
            // Altura aumentada a 210 para acomodar 3 radios + 2 pares de fecha
            this.panelFechas.BackColor = Color.FromArgb(248, 249, 250);
            this.panelFechas.BorderStyle = BorderStyle.FixedSingle;
            this.panelFechas.Controls.AddRange(new Control[] {
                this.lblFechasTitulo,
                this.rbtnTodosDatos,
                this.rbtnDesdeHastaHoy,
                this.rbtnDesdeHasta,
                this.lblFechaInicio,
                this.dtpFechaInicio,
                this.lblFechaFin,
                this.dtpFechaFin });
            this.panelFechas.Location = new Point(10, 290);
            this.panelFechas.Size = new Size(262, 238);

            this.lblFechasTitulo.AutoSize = true;
            this.lblFechasTitulo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblFechasTitulo.ForeColor = Color.FromArgb(52, 73, 94);
            this.lblFechasTitulo.Location = new Point(10, 8);
            this.lblFechasTitulo.Text = "📅 Alcance del snapshot";

            // Opción 1: Todos los datos actuales
            this.rbtnTodosDatos.AutoSize = true;
            this.rbtnTodosDatos.Checked = true;
            this.rbtnTodosDatos.Cursor = Cursors.Hand;
            this.rbtnTodosDatos.Font = new Font("Segoe UI", 8.5F);
            this.rbtnTodosDatos.ForeColor = Color.FromArgb(52, 73, 94);
            this.rbtnTodosDatos.Location = new Point(10, 30);
            this.rbtnTodosDatos.Text = "Todos los datos actuales";
            this.rbtnTodosDatos.CheckedChanged += new System.EventHandler(this.rbtnFiltro_CheckedChanged);

            // Opción 2: Desde fecha → hasta AHORA (momento del clic)
            this.rbtnDesdeHastaHoy.AutoSize = true;
            this.rbtnDesdeHastaHoy.Cursor = Cursors.Hand;
            this.rbtnDesdeHastaHoy.Font = new Font("Segoe UI", 8.5F);
            this.rbtnDesdeHastaHoy.ForeColor = Color.FromArgb(52, 73, 94);
            this.rbtnDesdeHastaHoy.Location = new Point(10, 54);
            this.rbtnDesdeHastaHoy.Text = "Desde fecha → hasta ahora";
            this.rbtnDesdeHastaHoy.CheckedChanged += new System.EventHandler(this.rbtnFiltro_CheckedChanged);

            // Opción 3: Desde fecha → hasta fecha (rango fijo)
            this.rbtnDesdeHasta.AutoSize = true;
            this.rbtnDesdeHasta.Cursor = Cursors.Hand;
            this.rbtnDesdeHasta.Font = new Font("Segoe UI", 8.5F);
            this.rbtnDesdeHasta.ForeColor = Color.FromArgb(52, 73, 94);
            this.rbtnDesdeHasta.Location = new Point(10, 78);
            this.rbtnDesdeHasta.Text = "Desde fecha → hasta fecha";
            this.rbtnDesdeHasta.CheckedChanged += new System.EventHandler(this.rbtnFiltro_CheckedChanged);

            // Fecha Inicio
            this.lblFechaInicio.AutoSize = true;
            this.lblFechaInicio.Enabled = false;
            this.lblFechaInicio.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            this.lblFechaInicio.ForeColor = Color.FromArgb(52, 73, 94);
            this.lblFechaInicio.Location = new Point(10, 106);
            this.lblFechaInicio.Text = "Desde:";

            this.dtpFechaInicio.Enabled = false;
            this.dtpFechaInicio.Font = new Font("Segoe UI", 9F);
            this.dtpFechaInicio.Format = DateTimePickerFormat.Short;
            this.dtpFechaInicio.Location = new Point(10, 122);
            this.dtpFechaInicio.Size = new Size(182, 27);

            // Fecha Fin (solo visible para opción 3)
            this.lblFechaFin.AutoSize = true;
            this.lblFechaFin.Enabled = false;
            this.lblFechaFin.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            this.lblFechaFin.ForeColor = Color.FromArgb(52, 73, 94);
            this.lblFechaFin.Location = new Point(10, 158);
            this.lblFechaFin.Text = "Hasta:";
            this.lblFechaFin.Visible = false;

            this.dtpFechaFin.Enabled = false;
            this.dtpFechaFin.Font = new Font("Segoe UI", 9F);
            this.dtpFechaFin.Format = DateTimePickerFormat.Short;
            this.dtpFechaFin.Location = new Point(10, 174);
            this.dtpFechaFin.Size = new Size(182, 27);
            this.dtpFechaFin.Visible = false;

            // panelAcciones
            this.panelAcciones.Anchor = (AnchorStyles)(AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left);
            this.panelAcciones.BackColor = Color.White;
            this.panelAcciones.BorderStyle = BorderStyle.FixedSingle;
            this.panelAcciones.Controls.AddRange(new Control[] {
                this.lblBackupTitulo, this.lblBackupDesc,
                this.btnBackupSQL,    this.lblBackupSQLDesc,
                this.btnSnapshotAuto, this.lblSnapshotDesc,
                this.btnBackupCSV,    this.lblBackupCSVDesc,
                this.panelSep,
                this.lblImportTitulo, this.lblImportDesc,
                this.btnRestaurar,    this.lblRestaurarDesc,
                this.btnImportarCSV,  this.lblImportarCSVDesc });
            this.panelAcciones.Location = new Point(301, 8);
            this.panelAcciones.Size = new Size(521, 538);

            SetLabel(this.lblBackupTitulo, "💾 Generar Respaldo", 11F, bold: true, loc: new Point(15, 13));
            SetLabel(this.lblBackupDesc, "Exporta los módulos seleccionados según el alcance configurado.", 8.5F, italic: true, loc: new Point(15, 38));
            SetBtn(this.btnBackupSQL, "💾 Backup SQL (archivo)", Color.FromArgb(142, 68, 173), new Point(15, 66), this.btnBackupSQL_Click);
            SetLabel(this.lblBackupSQLDesc, "Genera un archivo .SQL para guardar externamente.", 8F, color: Color.FromArgb(142, 68, 173), loc: new Point(15, 115));
            SetBtn(this.btnSnapshotAuto, "📸 Snapshot Automático", Color.FromArgb(39, 174, 96), new Point(15, 152), this.btnSnapshotAuto_Click);
            SetLabel(this.lblSnapshotDesc, "Guarda el backup directo en la BD — sin archivo externo.", 8F, color: Color.FromArgb(39, 174, 96), loc: new Point(15, 203));
            SetBtn(this.btnBackupCSV, "📊 Exportar CSV", Color.FromArgb(52, 152, 219), new Point(15, 228), this.btnBackupCSV_Click);
            SetLabel(this.lblBackupCSVDesc, "Exporta cada tabla como .CSV en una carpeta.", 8F, color: Color.FromArgb(41, 128, 185), loc: new Point(16, 277));

            this.panelSep.BackColor = Color.FromArgb(220, 220, 220);
            this.panelSep.Location = new Point(15, 322);
            this.panelSep.Size = new Size(305, 1);

            SetLabel(this.lblImportTitulo, "📥 Importar / Restaurar", 11F, bold: true, loc: new Point(15, 329));
            SetLabel(this.lblImportDesc, "Restaura datos desde un respaldo previo.", 8.5F, italic: true, loc: new Point(15, 354), size: new Size(490, 20));
            SetBtn(this.btnRestaurar, "📥 Importar .SQL", Color.FromArgb(142, 68, 173), new Point(15, 392), this.btnRestaurar_Click);
            SetLabel(this.lblRestaurarDesc, "Carga y ejecuta un archivo .SQL con restauración.", 8F, color: Color.FromArgb(142, 68, 173), loc: new Point(15, 443));
            SetBtn(this.btnImportarCSV, "📤 Importar CSV(s)", Color.FromArgb(230, 126, 34), new Point(15, 464), this.btnImportarCSV_Click);
            SetLabel(this.lblImportarCSVDesc, "Importa archivos .CSV exportados por este sistema.", 8F, color: Color.FromArgb(230, 126, 34), loc: new Point(15, 511));

            // panelLog
            this.panelLog.Anchor = (AnchorStyles)(AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
            this.panelLog.BackColor = Color.White;
            this.panelLog.BorderStyle = BorderStyle.FixedSingle;
            this.panelLog.Controls.AddRange(new Control[] { this.lblLogTitulo, this.btnLimpiarLog, this.rtbLog });
            this.panelLog.Location = new Point(828, 8);
            this.panelLog.Size = new Size(323, 538);

            this.lblLogTitulo.AutoSize = true;
            this.lblLogTitulo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblLogTitulo.ForeColor = Color.FromArgb(52, 73, 94);
            this.lblLogTitulo.Location = new Point(10, 10);
            this.lblLogTitulo.Text = "📋 Registro de operaciones";

            this.btnLimpiarLog.Anchor = (AnchorStyles)(AnchorStyles.Top | AnchorStyles.Right);
            this.btnLimpiarLog.BackColor = Color.FromArgb(149, 165, 166);
            this.btnLimpiarLog.Cursor = Cursors.Hand;
            this.btnLimpiarLog.FlatAppearance.BorderSize = 0;
            this.btnLimpiarLog.FlatStyle = FlatStyle.Flat;
            this.btnLimpiarLog.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            this.btnLimpiarLog.ForeColor = Color.White;
            this.btnLimpiarLog.Location = new Point(216, 5);
            this.btnLimpiarLog.Size = new Size(100, 28);
            this.btnLimpiarLog.Text = "🗑️ Limpiar";
            this.btnLimpiarLog.UseVisualStyleBackColor = false;
            this.btnLimpiarLog.Click += new System.EventHandler(this.btnLimpiarLog_Click);

            this.rtbLog.Anchor = (AnchorStyles)(AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
            this.rtbLog.BackColor = Color.FromArgb(30, 30, 30);
            this.rtbLog.BorderStyle = BorderStyle.None;
            this.rtbLog.Font = new Font("Consolas", 8.5F);
            this.rtbLog.ForeColor = Color.FromArgb(180, 220, 180);
            this.rtbLog.Location = new Point(10, 38);
            this.rtbLog.ReadOnly = true;
            this.rtbLog.ScrollBars = RichTextBoxScrollBars.Vertical;
            this.rtbLog.Size = new Size(305, 493);
            this.rtbLog.Text = "";

            // ── Tab 2 — Snapshots ─────────────────────────────────────────────
            this.tabSnapshots.BackColor = Color.FromArgb(236, 240, 241);
            this.tabSnapshots.Controls.AddRange(new Control[] {
                this.panelSnapshotTop, this.dgvSnapshots, this.panelDetalle });
            this.tabSnapshots.Location = new Point(4, 32);
            this.tabSnapshots.Size = new Size(1157, 554);
            this.tabSnapshots.Text = "📸 Snapshots Automáticos";

            // panelSnapshotTop
            this.panelSnapshotTop.Anchor = (AnchorStyles)(AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
            this.panelSnapshotTop.BackColor = Color.White;
            this.panelSnapshotTop.BorderStyle = BorderStyle.FixedSingle;
            this.panelSnapshotTop.Controls.AddRange(new Control[] {
                this.lblSnapTitulo, this.lblSnapDesc,
                this.btnRestaurarSnapshot, this.btnRenombrarSnapshot, this.btnEliminarSnapshot });
            this.panelSnapshotTop.Location = new Point(10, 8);
            this.panelSnapshotTop.Size = new Size(1135, 98);

            this.lblSnapTitulo.AutoSize = true;
            this.lblSnapTitulo.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            this.lblSnapTitulo.ForeColor = Color.FromArgb(52, 73, 94);
            this.lblSnapTitulo.Location = new Point(15, 10);
            this.lblSnapTitulo.Text = "📸 Snapshots guardados en la base de datos";

            this.lblSnapDesc.Font = new Font("Segoe UI", 8.5F, FontStyle.Italic);
            this.lblSnapDesc.ForeColor = Color.FromArgb(127, 140, 141);
            this.lblSnapDesc.Location = new Point(15, 35);
            this.lblSnapDesc.Size = new Size(620, 18);
            this.lblSnapDesc.Text = "Selecciona un snapshot → Restaurar (elige modo), Renombrar o Eliminar.";

            SetBtnSnap(this.btnRestaurarSnapshot, "♻️ Restaurar", Color.FromArgb(142, 68, 173), new Point(648, 12), new Size(155, 40), this.btnRestaurarSnapshot_Click);
            SetBtnSnap(this.btnRenombrarSnapshot, "✏️ Renombrar", Color.FromArgb(52, 152, 219), new Point(811, 12), new Size(150, 40), this.btnRenombrarSnapshot_Click);
            SetBtnSnap(this.btnEliminarSnapshot, "🗑️ Eliminar", Color.FromArgb(231, 76, 60), new Point(969, 12), new Size(150, 40), this.btnEliminarSnapshot_Click);

            // ── btnDeshacerUltima ─────────────────────────────────────
            this.btnDeshacerUltima = new System.Windows.Forms.Button();
            SetBtnSnap(this.btnDeshacerUltima, "↩️ Deshacer última operación",
                      Color.FromArgb(230, 126, 34),
                      new Point(648, 58),
                      new Size(471, 28),
                      this.btnDeshacerUltima_Click);
            this.btnDeshacerUltima.Visible = false;   // se activa automáticamente
            this.panelSnapshotTop.Controls.Add(this.btnDeshacerUltima);
            
            // dgvSnapshots
            this.dgvSnapshots.AllowUserToAddRows = false;
            this.dgvSnapshots.AllowUserToDeleteRows = false;
            this.dgvSnapshots.Anchor = (AnchorStyles)(AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left);
            this.dgvSnapshots.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSnapshots.BackgroundColor = Color.White;
            this.dgvSnapshots.BorderStyle = BorderStyle.None;
            this.dgvSnapshots.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvSnapshots.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            this.dgvSnapshots.ColumnHeadersDefaultCellStyle = dgvHeaderStyle;
            this.dgvSnapshots.ColumnHeadersHeight = 40;
            this.dgvSnapshots.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvSnapshots.DefaultCellStyle = dgvCellStyle;
            this.dgvSnapshots.EnableHeadersVisualStyles = false;
            this.dgvSnapshots.GridColor = Color.FromArgb(231, 231, 231);
            this.dgvSnapshots.Location = new Point(10, 108);
            this.dgvSnapshots.ReadOnly = true;
            this.dgvSnapshots.RowHeadersVisible = false;
            this.dgvSnapshots.RowTemplate.Height = 38;
            this.dgvSnapshots.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvSnapshots.Size = new Size(735, 435);

            // ── panelDetalle ──────────────────────────────────────────────────
            this.panelDetalle.Anchor = (AnchorStyles)(AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right);
            this.panelDetalle.BackColor = Color.White;
            this.panelDetalle.BorderStyle = BorderStyle.FixedSingle;
            this.panelDetalle.Location = new Point(753, 108);
            this.panelDetalle.Size = new Size(392, 435);
            this.panelDetalle.Visible = false;

            this.lblDetTituloInterno.AutoSize = true;
            this.lblDetTituloInterno.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.lblDetTituloInterno.ForeColor = Color.FromArgb(52, 73, 94);
            this.lblDetTituloInterno.Location = new Point(12, 10);
            this.lblDetTituloInterno.Text = "📄 Detalle del snapshot";

            var lineaSep = new Panel
            {
                BackColor = Color.FromArgb(220, 220, 220),
                Location = new Point(12, 32),
                Size = new Size(364, 1)
            };
            this.panelDetalle.Controls.Add(lineaSep);

            int yDet = 40;
            void AgregarParDetalle(Label lKey, string keyTxt, Label lVal, string valDef, int extraAlto = 0)
            {
                lKey.AutoSize = true;
                lKey.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
                lKey.ForeColor = Color.FromArgb(127, 140, 141);
                lKey.Location = new Point(12, yDet);
                lKey.Text = keyTxt;
                lVal.AutoSize = false;
                lVal.Font = new Font("Segoe UI", 9F);
                lVal.ForeColor = Color.FromArgb(52, 73, 94);
                lVal.Location = new Point(12, yDet + 16);
                lVal.Size = new Size(364, 20 + extraAlto);
                lVal.Text = valDef;
                yDet += 44 + extraAlto;
            }

            AgregarParDetalle(this.lblDetEtqLabel, "ETIQUETA", this.lblDetEtiqueta, "—");
            AgregarParDetalle(this.lblDetFecLabel, "FECHA CREACIÓN", this.lblDetFecha, "—");
            AgregarParDetalle(this.lblDetModLabel, "MÓDULOS", this.lblDetModulos, "—", extraAlto: 16);
            AgregarParDetalle(this.lblDetFiltLabel, "ALCANCE", this.lblDetFiltro, "—");
            AgregarParDetalle(this.lblDetRegLabel, "REGISTROS", this.lblDetRegistros, "—");

            this.lblDetTablasLabel.AutoSize = true;
            this.lblDetTablasLabel.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            this.lblDetTablasLabel.ForeColor = Color.FromArgb(127, 140, 141);
            this.lblDetTablasLabel.Location = new Point(12, yDet);
            this.lblDetTablasLabel.Text = "DETALLE POR TABLA";

            this.rtbDetTablas.BackColor = Color.FromArgb(248, 249, 250);
            this.rtbDetTablas.BorderStyle = BorderStyle.FixedSingle;
            this.rtbDetTablas.Font = new Font("Consolas", 8.5F);
            this.rtbDetTablas.ForeColor = Color.FromArgb(52, 73, 94);
            this.rtbDetTablas.Location = new Point(12, yDet + 18);
            this.rtbDetTablas.ReadOnly = true;
            this.rtbDetTablas.ScrollBars = RichTextBoxScrollBars.Vertical;
            this.rtbDetTablas.Size = new Size(364, 90);
            this.rtbDetTablas.Text = "";

            this.panelDetalle.Controls.AddRange(new Control[] {
                this.lblDetTituloInterno,
                this.lblDetEtqLabel,  this.lblDetEtiqueta,
                this.lblDetFecLabel,  this.lblDetFecha,
                this.lblDetModLabel,  this.lblDetModulos,
                this.lblDetFiltLabel, this.lblDetFiltro,
                this.lblDetRegLabel,  this.lblDetRegistros,
                this.lblDetTablasLabel, this.rtbDetTablas
            });

            // ── ProgressBar ───────────────────────────────────────────────────
            this.progressBar.Anchor = (AnchorStyles)(AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
            this.progressBar.ForeColor = Color.FromArgb(142, 68, 173);
            this.progressBar.Location = new Point(15, 678);
            this.progressBar.Size = new Size(1163, 10);
            this.progressBar.Style = ProgressBarStyle.Continuous;
            this.progressBar.Visible = false;

            // ── FrmBackup ─────────────────────────────────────────────────────
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.FromArgb(236, 240, 241);
            this.ClientSize = new Size(1200, 700);
            this.Controls.AddRange(new Control[] {
                this.lblTitulo, this.lblSubtitulo, this.tabControl, this.progressBar });
            this.FormBorderStyle = FormBorderStyle.None;
            this.Name = "FrmBackup";
            this.Text = "Backup y Restauración";
            this.Load += new System.EventHandler(this.FrmBackup_Load);

            this.panelDetalle.ResumeLayout(false);
            this.panelDetalle.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.tabBackup.ResumeLayout(false);
            this.panelConfig.ResumeLayout(false);
            this.panelModulos.ResumeLayout(false);
            this.panelModulos.PerformLayout();
            this.panelFechas.ResumeLayout(false);
            this.panelFechas.PerformLayout();
            this.panelAcciones.ResumeLayout(false);
            this.panelAcciones.PerformLayout();
            this.panelLog.ResumeLayout(false);
            this.panelLog.PerformLayout();
            this.tabSnapshots.ResumeLayout(false);
            this.panelSnapshotTop.ResumeLayout(false);
            this.panelSnapshotTop.PerformLayout();
            ((ISupportInitialize)this.dgvSnapshots).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        // ── Helpers ───────────────────────────────────────────────────────────
        private void SetLabel(Label lbl, string text, float fontSize,
            bool bold = false, bool italic = false,
            Color? color = null, Point loc = default, Size size = default)
        {
            lbl.Text = text;
            lbl.Font = new Font("Segoe UI", fontSize,
                (bold ? FontStyle.Bold : FontStyle.Regular) |
                (italic ? FontStyle.Italic : FontStyle.Regular));
            lbl.ForeColor = color ?? Color.FromArgb(52, 73, 94);
            lbl.Location = loc;
            if (size.IsEmpty) lbl.AutoSize = true;
            else { lbl.AutoSize = false; lbl.Size = size; }
        }

        private void SetBtn(Button btn, string text, Color backColor, Point loc, System.EventHandler onClick)
        {
            btn.Text = text;
            btn.BackColor = backColor;
            btn.ForeColor = Color.White;
            btn.Location = loc;
            btn.Size = new Size(490, 46);
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btn.Cursor = Cursors.Hand;
            btn.UseVisualStyleBackColor = false;
            btn.Click += onClick;
        }

        private void SetBtnSnap(Button btn, string text, Color backColor, Point loc, Size size, System.EventHandler onClick)
        {
            btn.Text = text;
            btn.BackColor = backColor;
            btn.ForeColor = Color.White;
            btn.Location = loc;
            btn.Size = size;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btn.Cursor = Cursors.Hand;
            btn.UseVisualStyleBackColor = false;
            btn.Click += onClick;
        }

        #endregion

        // ── Declaraciones ─────────────────────────────────────────────────────
        private Label lblTitulo;
        private Label lblSubtitulo;
        private TabControl tabControl;
        private TabPage tabBackup;
        private TabPage tabSnapshots;
        private ProgressBar progressBar;

        // Tab 1 — configuración
        private Panel panelConfig;
        private Panel panelModulos;
        private Label lblModulosTitulo;
        private FlowLayoutPanel flpModulos;
        private Panel panelFechas;
        private Label lblFechasTitulo;
        private RadioButton rbtnTodosDatos;
        private RadioButton rbtnDesdeHastaHoy;   // ← Desde X hasta AHORA
        private RadioButton rbtnDesdeHasta;       // ← Desde X hasta Y
        private Label lblFechaInicio;
        private DateTimePicker dtpFechaInicio;
        private Label lblFechaFin;               // ← NUEVO
        private DateTimePicker dtpFechaFin;      // ← NUEVO

        // Tab 1 — acciones
        private Panel panelAcciones;
        private Label lblBackupTitulo, lblBackupDesc;
        private Button btnBackupSQL;
        private Label lblBackupSQLDesc;
        private Button btnSnapshotAuto;
        private Label lblSnapshotDesc;
        private Button btnBackupCSV;
        private Label lblBackupCSVDesc;
        private Panel panelSep;
        private Label lblImportTitulo, lblImportDesc;
        private Button btnRestaurar;
        private Label lblRestaurarDesc;
        private Button btnImportarCSV;
        private Label lblImportarCSVDesc;

        // Tab 1 — log
        private Panel panelLog;
        private Label lblLogTitulo;
        private RichTextBox rtbLog;
        private Button btnLimpiarLog;

        // Tab 2 — snapshots
        private Panel panelSnapshotTop;
        private Label lblSnapTitulo, lblSnapDesc;
        private Button btnRestaurarSnapshot;
        private Button btnRenombrarSnapshot;
        private Button btnEliminarSnapshot;
        private DataGridView dgvSnapshots;
        private Button btnDeshacerUltima;

        // Tab 2 — panel detalle
        private Panel panelDetalle;
        private Label lblDetTituloInterno;
        private Label lblDetEtqLabel;
        private Label lblDetEtiqueta;
        private Label lblDetFecLabel;
        private Label lblDetFecha;
        private Label lblDetModLabel;
        private Label lblDetModulos;
        private Label lblDetFiltLabel;
        private Label lblDetFiltro;
        private Label lblDetRegLabel;
        private Label lblDetRegistros;
        private Label lblDetTablasLabel;
        private RichTextBox rtbDetTablas;
    }
}