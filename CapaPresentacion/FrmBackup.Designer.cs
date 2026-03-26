using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace CapaPresentacion
{
    /// <summary>
    /// DESIGNER — FrmBackup
    ///
    /// CORRECCIONES:
    ///   • lblNotaActualidad ahora es campo de clase (no variable local) para que
    ///     el analizador del diseñador no lance NullReferenceException.
    ///   • Se eliminaron referencias a columnas inexistentes en la tabla _vet_snapshots
    ///     (filtro_fecha, total_registros, detalle_tablas).
    ///
    /// LAYOUT (TabControl con 2 pestañas):
    ///   Pestaña 1 — "💾 Backup / Importar"
    ///     • Panel izquierdo : módulos checkboxes + filtro de fechas
    ///     • Panel central   : botones de acción
    ///     • Panel derecho   : log de operaciones
    ///
    ///   Pestaña 2 — "📸 Snapshots Automáticos"
    ///     • Grid con lista de snapshots guardados en BD
    ///     • Botones: Restaurar, Eliminar, Volver a la Actualidad
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblSubtitulo = new System.Windows.Forms.Label();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabBackup = new System.Windows.Forms.TabPage();
            this.panelConfig = new System.Windows.Forms.Panel();
            this.panelModulos = new System.Windows.Forms.Panel();
            this.lblModulosTitulo = new System.Windows.Forms.Label();
            this.flpModulos = new System.Windows.Forms.FlowLayoutPanel();
            this.panelFechas = new System.Windows.Forms.Panel();
            this.lblFechasTitulo = new System.Windows.Forms.Label();
            this.rbtnSinFiltro = new System.Windows.Forms.RadioButton();
            this.rbtnHastaHoy = new System.Windows.Forms.RadioButton();
            this.rbtnRangoFechas = new System.Windows.Forms.RadioButton();
            this.lblFechaInicio = new System.Windows.Forms.Label();
            this.dtpFechaInicio = new System.Windows.Forms.DateTimePicker();
            this.lblFechaFin = new System.Windows.Forms.Label();
            this.dtpFechaFin = new System.Windows.Forms.DateTimePicker();
            this.panelAcciones = new System.Windows.Forms.Panel();
            this.lblBackupTitulo = new System.Windows.Forms.Label();
            this.lblBackupDesc = new System.Windows.Forms.Label();
            this.btnBackupSQL = new System.Windows.Forms.Button();
            this.lblBackupSQLDesc = new System.Windows.Forms.Label();
            this.btnSnapshotAuto = new System.Windows.Forms.Button();
            this.lblSnapshotDesc = new System.Windows.Forms.Label();
            this.btnBackupCSV = new System.Windows.Forms.Button();
            this.lblBackupCSVDesc = new System.Windows.Forms.Label();
            this.panelSep = new System.Windows.Forms.Panel();
            this.lblImportTitulo = new System.Windows.Forms.Label();
            this.lblImportDesc = new System.Windows.Forms.Label();
            this.btnRestaurar = new System.Windows.Forms.Button();
            this.lblRestaurarDesc = new System.Windows.Forms.Label();
            this.btnImportarCSV = new System.Windows.Forms.Button();
            this.lblImportarCSVDesc = new System.Windows.Forms.Label();
            this.panelLog = new System.Windows.Forms.Panel();
            this.lblLogTitulo = new System.Windows.Forms.Label();
            this.btnLimpiarLog = new System.Windows.Forms.Button();
            this.rtbLog = new System.Windows.Forms.RichTextBox();
            this.tabSnapshots = new System.Windows.Forms.TabPage();
            this.panelSnapshotTop = new System.Windows.Forms.Panel();
            this.lblSnapTitulo = new System.Windows.Forms.Label();
            this.lblSnapDesc = new System.Windows.Forms.Label();
            this.btnRestaurarSnapshot = new System.Windows.Forms.Button();
            this.btnVolverActualidad = new System.Windows.Forms.Button();
            this.btnEliminarSnapshot = new System.Windows.Forms.Button();
            this.lblNotaActualidad = new System.Windows.Forms.Label();
            this.dgvSnapshots = new System.Windows.Forms.DataGridView();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.tabControl.SuspendLayout();
            this.tabBackup.SuspendLayout();
            this.panelConfig.SuspendLayout();
            this.panelModulos.SuspendLayout();
            this.panelFechas.SuspendLayout();
            this.panelAcciones.SuspendLayout();
            this.panelLog.SuspendLayout();
            this.tabSnapshots.SuspendLayout();
            this.panelSnapshotTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSnapshots)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblTitulo.Location = new System.Drawing.Point(20, 18);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(350, 37);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "🗄️ Backup y Restauración";
            // 
            // lblSubtitulo
            // 
            this.lblSubtitulo.AutoSize = true;
            this.lblSubtitulo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.lblSubtitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(140)))), ((int)(((byte)(141)))));
            this.lblSubtitulo.Location = new System.Drawing.Point(24, 55);
            this.lblSubtitulo.Name = "lblSubtitulo";
            this.lblSubtitulo.Size = new System.Drawing.Size(591, 20);
            this.lblSubtitulo.TabIndex = 1;
            this.lblSubtitulo.Text = "Respaldo segmentado por módulo y fecha  •  Snapshots automáticos  •  Exportar / I" +
    "mportar";
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.tabBackup);
            this.tabControl.Controls.Add(this.tabSnapshots);
            this.tabControl.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.tabControl.Location = new System.Drawing.Point(15, 78);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1165, 590);
            this.tabControl.TabIndex = 2;
            // 
            // tabBackup
            // 
            this.tabBackup.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.tabBackup.Controls.Add(this.panelConfig);
            this.tabBackup.Controls.Add(this.panelAcciones);
            this.tabBackup.Controls.Add(this.panelLog);
            this.tabBackup.Location = new System.Drawing.Point(4, 32);
            this.tabBackup.Name = "tabBackup";
            this.tabBackup.Size = new System.Drawing.Size(1157, 554);
            this.tabBackup.TabIndex = 0;
            this.tabBackup.Text = "💾 Backup / Importar";
            // 
            // panelConfig
            // 
            this.panelConfig.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panelConfig.BackColor = System.Drawing.Color.White;
            this.panelConfig.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelConfig.Controls.Add(this.panelModulos);
            this.panelConfig.Controls.Add(this.panelFechas);
            this.panelConfig.Location = new System.Drawing.Point(10, 8);
            this.panelConfig.Name = "panelConfig";
            this.panelConfig.Size = new System.Drawing.Size(285, 555);
            this.panelConfig.TabIndex = 0;
            // 
            // panelModulos
            // 
            this.panelModulos.BackColor = System.Drawing.Color.Transparent;
            this.panelModulos.Controls.Add(this.lblModulosTitulo);
            this.panelModulos.Controls.Add(this.flpModulos);
            this.panelModulos.Location = new System.Drawing.Point(10, 10);
            this.panelModulos.Name = "panelModulos";
            this.panelModulos.Size = new System.Drawing.Size(262, 298);
            this.panelModulos.TabIndex = 0;
            // 
            // lblModulosTitulo
            // 
            this.lblModulosTitulo.AutoSize = true;
            this.lblModulosTitulo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblModulosTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblModulosTitulo.Location = new System.Drawing.Point(0, 0);
            this.lblModulosTitulo.Name = "lblModulosTitulo";
            this.lblModulosTitulo.Size = new System.Drawing.Size(177, 20);
            this.lblModulosTitulo.TabIndex = 0;
            this.lblModulosTitulo.Text = "📦 Módulos a respaldar";
            // 
            // flpModulos
            // 
            this.flpModulos.AutoScroll = true;
            this.flpModulos.BackColor = System.Drawing.Color.Transparent;
            this.flpModulos.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpModulos.Location = new System.Drawing.Point(0, 25);
            this.flpModulos.Name = "flpModulos";
            this.flpModulos.Size = new System.Drawing.Size(262, 272);
            this.flpModulos.TabIndex = 1;
            this.flpModulos.WrapContents = false;
            // 
            // panelFechas
            // 
            this.panelFechas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.panelFechas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelFechas.Controls.Add(this.lblFechasTitulo);
            this.panelFechas.Controls.Add(this.rbtnSinFiltro);
            this.panelFechas.Controls.Add(this.rbtnHastaHoy);
            this.panelFechas.Controls.Add(this.rbtnRangoFechas);
            this.panelFechas.Controls.Add(this.lblFechaInicio);
            this.panelFechas.Controls.Add(this.dtpFechaInicio);
            this.panelFechas.Controls.Add(this.lblFechaFin);
            this.panelFechas.Controls.Add(this.dtpFechaFin);
            this.panelFechas.Location = new System.Drawing.Point(10, 318);
            this.panelFechas.Name = "panelFechas";
            this.panelFechas.Size = new System.Drawing.Size(262, 170);
            this.panelFechas.TabIndex = 1;
            // 
            // lblFechasTitulo
            // 
            this.lblFechasTitulo.AutoSize = true;
            this.lblFechasTitulo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblFechasTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblFechasTitulo.Location = new System.Drawing.Point(10, 8);
            this.lblFechasTitulo.Name = "lblFechasTitulo";
            this.lblFechasTitulo.Size = new System.Drawing.Size(135, 20);
            this.lblFechasTitulo.TabIndex = 0;
            this.lblFechasTitulo.Text = "📅 Filtro de fecha";
            // 
            // rbtnSinFiltro
            // 
            this.rbtnSinFiltro.AutoSize = true;
            this.rbtnSinFiltro.Checked = true;
            this.rbtnSinFiltro.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbtnSinFiltro.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.rbtnSinFiltro.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.rbtnSinFiltro.Location = new System.Drawing.Point(10, 30);
            this.rbtnSinFiltro.Name = "rbtnSinFiltro";
            this.rbtnSinFiltro.Size = new System.Drawing.Size(202, 24);
            this.rbtnSinFiltro.TabIndex = 1;
            this.rbtnSinFiltro.TabStop = true;
            this.rbtnSinFiltro.Text = "Sin filtro (todos los datos)";
            this.rbtnSinFiltro.CheckedChanged += new System.EventHandler(this.rbtnSinFiltro_CheckedChanged);
            // 
            // rbtnHastaHoy
            // 
            this.rbtnHastaHoy.AutoSize = true;
            this.rbtnHastaHoy.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbtnHastaHoy.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.rbtnHastaHoy.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.rbtnHastaHoy.Location = new System.Drawing.Point(10, 52);
            this.rbtnHastaHoy.Name = "rbtnHastaHoy";
            this.rbtnHastaHoy.Size = new System.Drawing.Size(196, 24);
            this.rbtnHastaHoy.TabIndex = 2;
            this.rbtnHastaHoy.Text = "Desde fecha → hasta hoy";
            this.rbtnHastaHoy.CheckedChanged += new System.EventHandler(this.rbtnHastaHoy_CheckedChanged);
            // 
            // rbtnRangoFechas
            // 
            this.rbtnRangoFechas.AutoSize = true;
            this.rbtnRangoFechas.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbtnRangoFechas.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.rbtnRangoFechas.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.rbtnRangoFechas.Location = new System.Drawing.Point(10, 74);
            this.rbtnRangoFechas.Name = "rbtnRangoFechas";
            this.rbtnRangoFechas.Size = new System.Drawing.Size(144, 24);
            this.rbtnRangoFechas.TabIndex = 3;
            this.rbtnRangoFechas.Text = "Rango específico";
            this.rbtnRangoFechas.CheckedChanged += new System.EventHandler(this.rbtnRangoFechas_CheckedChanged);
            // 
            // lblFechaInicio
            // 
            this.lblFechaInicio.AutoSize = true;
            this.lblFechaInicio.Enabled = false;
            this.lblFechaInicio.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.lblFechaInicio.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblFechaInicio.Location = new System.Drawing.Point(10, 100);
            this.lblFechaInicio.Name = "lblFechaInicio";
            this.lblFechaInicio.Size = new System.Drawing.Size(49, 19);
            this.lblFechaInicio.TabIndex = 4;
            this.lblFechaInicio.Text = "Inicio:";
            // 
            // dtpFechaInicio
            // 
            this.dtpFechaInicio.Enabled = false;
            this.dtpFechaInicio.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpFechaInicio.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaInicio.Location = new System.Drawing.Point(55, 97);
            this.dtpFechaInicio.Name = "dtpFechaInicio";
            this.dtpFechaInicio.Size = new System.Drawing.Size(182, 27);
            this.dtpFechaInicio.TabIndex = 5;
            // 
            // lblFechaFin
            // 
            this.lblFechaFin.AutoSize = true;
            this.lblFechaFin.Enabled = false;
            this.lblFechaFin.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.lblFechaFin.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblFechaFin.Location = new System.Drawing.Point(10, 132);
            this.lblFechaFin.Name = "lblFechaFin";
            this.lblFechaFin.Size = new System.Drawing.Size(32, 19);
            this.lblFechaFin.TabIndex = 6;
            this.lblFechaFin.Text = "Fin:";
            // 
            // dtpFechaFin
            // 
            this.dtpFechaFin.Enabled = false;
            this.dtpFechaFin.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpFechaFin.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaFin.Location = new System.Drawing.Point(55, 129);
            this.dtpFechaFin.Name = "dtpFechaFin";
            this.dtpFechaFin.Size = new System.Drawing.Size(182, 27);
            this.dtpFechaFin.TabIndex = 7;
            // 
            // panelAcciones
            // 
            this.panelAcciones.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panelAcciones.BackColor = System.Drawing.Color.White;
            this.panelAcciones.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelAcciones.Controls.Add(this.lblBackupTitulo);
            this.panelAcciones.Controls.Add(this.lblBackupDesc);
            this.panelAcciones.Controls.Add(this.btnBackupSQL);
            this.panelAcciones.Controls.Add(this.lblBackupSQLDesc);
            this.panelAcciones.Controls.Add(this.btnSnapshotAuto);
            this.panelAcciones.Controls.Add(this.lblSnapshotDesc);
            this.panelAcciones.Controls.Add(this.btnBackupCSV);
            this.panelAcciones.Controls.Add(this.lblBackupCSVDesc);
            this.panelAcciones.Controls.Add(this.panelSep);
            this.panelAcciones.Controls.Add(this.lblImportTitulo);
            this.panelAcciones.Controls.Add(this.lblImportDesc);
            this.panelAcciones.Controls.Add(this.btnRestaurar);
            this.panelAcciones.Controls.Add(this.lblRestaurarDesc);
            this.panelAcciones.Controls.Add(this.btnImportarCSV);
            this.panelAcciones.Controls.Add(this.lblImportarCSVDesc);
            this.panelAcciones.Location = new System.Drawing.Point(301, 8);
            this.panelAcciones.Name = "panelAcciones";
            this.panelAcciones.Size = new System.Drawing.Size(521, 555);
            this.panelAcciones.TabIndex = 1;
            // 
            // lblBackupTitulo
            // 
            this.lblBackupTitulo.AutoSize = true;
            this.lblBackupTitulo.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblBackupTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblBackupTitulo.Location = new System.Drawing.Point(15, 13);
            this.lblBackupTitulo.Name = "lblBackupTitulo";
            this.lblBackupTitulo.Size = new System.Drawing.Size(197, 25);
            this.lblBackupTitulo.TabIndex = 0;
            this.lblBackupTitulo.Text = "💾 Generar Respaldo";
            // 
            // lblBackupDesc
            // 
            this.lblBackupDesc.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Italic);
            this.lblBackupDesc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(140)))), ((int)(((byte)(141)))));
            this.lblBackupDesc.Location = new System.Drawing.Point(15, 38);
            this.lblBackupDesc.Name = "lblBackupDesc";
            this.lblBackupDesc.Size = new System.Drawing.Size(490, 24);
            this.lblBackupDesc.TabIndex = 1;
            this.lblBackupDesc.Text = "Exporta los módulos seleccionados según el filtro de fecha configurado.";
            // 
            // btnBackupSQL
            // 
            this.btnBackupSQL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(68)))), ((int)(((byte)(173)))));
            this.btnBackupSQL.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBackupSQL.FlatAppearance.BorderSize = 0;
            this.btnBackupSQL.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBackupSQL.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnBackupSQL.ForeColor = System.Drawing.Color.White;
            this.btnBackupSQL.Location = new System.Drawing.Point(15, 66);
            this.btnBackupSQL.Name = "btnBackupSQL";
            this.btnBackupSQL.Size = new System.Drawing.Size(490, 46);
            this.btnBackupSQL.TabIndex = 2;
            this.btnBackupSQL.Text = "💾 Backup SQL (archivo)";
            this.btnBackupSQL.UseVisualStyleBackColor = false;
            this.btnBackupSQL.Click += new System.EventHandler(this.btnBackupSQL_Click);
            // 
            // lblBackupSQLDesc
            // 
            this.lblBackupSQLDesc.AutoSize = true;
            this.lblBackupSQLDesc.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblBackupSQLDesc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(68)))), ((int)(((byte)(173)))));
            this.lblBackupSQLDesc.Location = new System.Drawing.Point(15, 115);
            this.lblBackupSQLDesc.Name = "lblBackupSQLDesc";
            this.lblBackupSQLDesc.Size = new System.Drawing.Size(327, 19);
            this.lblBackupSQLDesc.TabIndex = 3;
            this.lblBackupSQLDesc.Text = "Genera un archivo .SQL para guardar externamente.";
            // 
            // btnSnapshotAuto
            // 
            this.btnSnapshotAuto.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(174)))), ((int)(((byte)(96)))));
            this.btnSnapshotAuto.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSnapshotAuto.FlatAppearance.BorderSize = 0;
            this.btnSnapshotAuto.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSnapshotAuto.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSnapshotAuto.ForeColor = System.Drawing.Color.White;
            this.btnSnapshotAuto.Location = new System.Drawing.Point(15, 152);
            this.btnSnapshotAuto.Name = "btnSnapshotAuto";
            this.btnSnapshotAuto.Size = new System.Drawing.Size(490, 46);
            this.btnSnapshotAuto.TabIndex = 4;
            this.btnSnapshotAuto.Text = "📸 Snapshot Automático";
            this.btnSnapshotAuto.UseVisualStyleBackColor = false;
            this.btnSnapshotAuto.Click += new System.EventHandler(this.btnSnapshotAuto_Click);
            // 
            // lblSnapshotDesc
            // 
            this.lblSnapshotDesc.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblSnapshotDesc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(174)))), ((int)(((byte)(96)))));
            this.lblSnapshotDesc.Location = new System.Drawing.Point(15, 203);
            this.lblSnapshotDesc.Name = "lblSnapshotDesc";
            this.lblSnapshotDesc.Size = new System.Drawing.Size(490, 22);
            this.lblSnapshotDesc.TabIndex = 5;
            this.lblSnapshotDesc.Text = "Guarda el backup directo en la BD — sin archivo externo.\r\n";
            // 
            // btnBackupCSV
            // 
            this.btnBackupCSV.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnBackupCSV.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBackupCSV.FlatAppearance.BorderSize = 0;
            this.btnBackupCSV.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBackupCSV.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnBackupCSV.ForeColor = System.Drawing.Color.White;
            this.btnBackupCSV.Location = new System.Drawing.Point(15, 228);
            this.btnBackupCSV.Name = "btnBackupCSV";
            this.btnBackupCSV.Size = new System.Drawing.Size(490, 46);
            this.btnBackupCSV.TabIndex = 6;
            this.btnBackupCSV.Text = "📊 Exportar CSV";
            this.btnBackupCSV.UseVisualStyleBackColor = false;
            this.btnBackupCSV.Click += new System.EventHandler(this.btnBackupCSV_Click);
            // 
            // lblBackupCSVDesc
            // 
            this.lblBackupCSVDesc.AutoSize = true;
            this.lblBackupCSVDesc.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblBackupCSVDesc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.lblBackupCSVDesc.Location = new System.Drawing.Point(16, 277);
            this.lblBackupCSVDesc.Name = "lblBackupCSVDesc";
            this.lblBackupCSVDesc.Size = new System.Drawing.Size(289, 19);
            this.lblBackupCSVDesc.TabIndex = 7;
            this.lblBackupCSVDesc.Text = "Exporta cada tabla como .CSV en una carpeta.";
            // 
            // panelSep
            // 
            this.panelSep.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.panelSep.Location = new System.Drawing.Point(15, 322);
            this.panelSep.Name = "panelSep";
            this.panelSep.Size = new System.Drawing.Size(305, 1);
            this.panelSep.TabIndex = 8;
            // 
            // lblImportTitulo
            // 
            this.lblImportTitulo.AutoSize = true;
            this.lblImportTitulo.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblImportTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblImportTitulo.Location = new System.Drawing.Point(15, 329);
            this.lblImportTitulo.Name = "lblImportTitulo";
            this.lblImportTitulo.Size = new System.Drawing.Size(223, 25);
            this.lblImportTitulo.TabIndex = 9;
            this.lblImportTitulo.Text = "📥 Importar / Restaurar";
            // 
            // lblImportDesc
            // 
            this.lblImportDesc.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Italic);
            this.lblImportDesc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(140)))), ((int)(((byte)(141)))));
            this.lblImportDesc.Location = new System.Drawing.Point(15, 358);
            this.lblImportDesc.Name = "lblImportDesc";
            this.lblImportDesc.Size = new System.Drawing.Size(490, 40);
            this.lblImportDesc.TabIndex = 10;
            this.lblImportDesc.Text = "Restaura datos desde un respaldo previo. No sobrescribe registros con el mismo ID" +
    ".";
            // 
            // btnRestaurar
            // 
            this.btnRestaurar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(68)))), ((int)(((byte)(173)))));
            this.btnRestaurar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRestaurar.FlatAppearance.BorderSize = 0;
            this.btnRestaurar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRestaurar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnRestaurar.ForeColor = System.Drawing.Color.White;
            this.btnRestaurar.Location = new System.Drawing.Point(15, 392);
            this.btnRestaurar.Name = "btnRestaurar";
            this.btnRestaurar.Size = new System.Drawing.Size(490, 46);
            this.btnRestaurar.TabIndex = 11;
            this.btnRestaurar.Text = "📥 Importar .SQL";
            this.btnRestaurar.UseVisualStyleBackColor = false;
            this.btnRestaurar.Click += new System.EventHandler(this.btnRestaurar_Click);
            // 
            // lblRestaurarDesc
            // 
            this.lblRestaurarDesc.AutoSize = true;
            this.lblRestaurarDesc.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblRestaurarDesc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(68)))), ((int)(((byte)(173)))));
            this.lblRestaurarDesc.Location = new System.Drawing.Point(15, 443);
            this.lblRestaurarDesc.Name = "lblRestaurarDesc";
            this.lblRestaurarDesc.Size = new System.Drawing.Size(372, 19);
            this.lblRestaurarDesc.TabIndex = 12;
            this.lblRestaurarDesc.Text = "Carga y ejecuta un archivo .SQL generado por este sistema.";
            // 
            // btnImportarCSV
            // 
            this.btnImportarCSV.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(126)))), ((int)(((byte)(34)))));
            this.btnImportarCSV.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnImportarCSV.FlatAppearance.BorderSize = 0;
            this.btnImportarCSV.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImportarCSV.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnImportarCSV.ForeColor = System.Drawing.Color.White;
            this.btnImportarCSV.Location = new System.Drawing.Point(15, 464);
            this.btnImportarCSV.Name = "btnImportarCSV";
            this.btnImportarCSV.Size = new System.Drawing.Size(490, 42);
            this.btnImportarCSV.TabIndex = 13;
            this.btnImportarCSV.Text = "📤 Importar CSV(s)";
            this.btnImportarCSV.UseVisualStyleBackColor = false;
            this.btnImportarCSV.Click += new System.EventHandler(this.btnImportarCSV_Click);
            // 
            // lblImportarCSVDesc
            // 
            this.lblImportarCSVDesc.AutoSize = true;
            this.lblImportarCSVDesc.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblImportarCSVDesc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(126)))), ((int)(((byte)(34)))));
            this.lblImportarCSVDesc.Location = new System.Drawing.Point(15, 511);
            this.lblImportarCSVDesc.Name = "lblImportarCSVDesc";
            this.lblImportarCSVDesc.Size = new System.Drawing.Size(323, 19);
            this.lblImportarCSVDesc.TabIndex = 14;
            this.lblImportarCSVDesc.Text = "Importa archivos .CSV exportados por este sistema.";
            // 
            // panelLog
            // 
            this.panelLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelLog.BackColor = System.Drawing.Color.White;
            this.panelLog.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelLog.Controls.Add(this.lblLogTitulo);
            this.panelLog.Controls.Add(this.btnLimpiarLog);
            this.panelLog.Controls.Add(this.rtbLog);
            this.panelLog.Location = new System.Drawing.Point(828, 8);
            this.panelLog.Name = "panelLog";
            this.panelLog.Size = new System.Drawing.Size(323, 555);
            this.panelLog.TabIndex = 2;
            // 
            // lblLogTitulo
            // 
            this.lblLogTitulo.AutoSize = true;
            this.lblLogTitulo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblLogTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblLogTitulo.Location = new System.Drawing.Point(10, 10);
            this.lblLogTitulo.Name = "lblLogTitulo";
            this.lblLogTitulo.Size = new System.Drawing.Size(203, 20);
            this.lblLogTitulo.TabIndex = 0;
            this.lblLogTitulo.Text = "📋 Registro de operaciones";
            // 
            // btnLimpiarLog
            // 
            this.btnLimpiarLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLimpiarLog.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(165)))), ((int)(((byte)(166)))));
            this.btnLimpiarLog.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLimpiarLog.FlatAppearance.BorderSize = 0;
            this.btnLimpiarLog.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLimpiarLog.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.btnLimpiarLog.ForeColor = System.Drawing.Color.White;
            this.btnLimpiarLog.Location = new System.Drawing.Point(216, 5);
            this.btnLimpiarLog.Name = "btnLimpiarLog";
            this.btnLimpiarLog.Size = new System.Drawing.Size(100, 28);
            this.btnLimpiarLog.TabIndex = 1;
            this.btnLimpiarLog.Text = "🗑️ Limpiar";
            this.btnLimpiarLog.UseVisualStyleBackColor = false;
            this.btnLimpiarLog.Click += new System.EventHandler(this.btnLimpiarLog_Click);
            // 
            // rtbLog
            // 
            this.rtbLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbLog.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.rtbLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbLog.Font = new System.Drawing.Font("Consolas", 8.5F);
            this.rtbLog.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(220)))), ((int)(((byte)(180)))));
            this.rtbLog.Location = new System.Drawing.Point(10, 38);
            this.rtbLog.Name = "rtbLog";
            this.rtbLog.ReadOnly = true;
            this.rtbLog.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.rtbLog.Size = new System.Drawing.Size(305, 510);
            this.rtbLog.TabIndex = 2;
            this.rtbLog.Text = "";
            // 
            // tabSnapshots
            // 
            this.tabSnapshots.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.tabSnapshots.Controls.Add(this.panelSnapshotTop);
            this.tabSnapshots.Controls.Add(this.dgvSnapshots);
            this.tabSnapshots.Location = new System.Drawing.Point(4, 32);
            this.tabSnapshots.Name = "tabSnapshots";
            this.tabSnapshots.Size = new System.Drawing.Size(1157, 554);
            this.tabSnapshots.TabIndex = 1;
            this.tabSnapshots.Text = "📸 Snapshots Automáticos";
            // 
            // panelSnapshotTop
            // 
            this.panelSnapshotTop.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelSnapshotTop.BackColor = System.Drawing.Color.White;
            this.panelSnapshotTop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelSnapshotTop.Controls.Add(this.lblSnapTitulo);
            this.panelSnapshotTop.Controls.Add(this.lblSnapDesc);
            this.panelSnapshotTop.Controls.Add(this.btnRestaurarSnapshot);
            this.panelSnapshotTop.Controls.Add(this.btnVolverActualidad);
            this.panelSnapshotTop.Controls.Add(this.btnEliminarSnapshot);
            this.panelSnapshotTop.Controls.Add(this.lblNotaActualidad);
            this.panelSnapshotTop.Location = new System.Drawing.Point(10, 8);
            this.panelSnapshotTop.Name = "panelSnapshotTop";
            this.panelSnapshotTop.Size = new System.Drawing.Size(1135, 95);
            this.panelSnapshotTop.TabIndex = 0;
            // 
            // lblSnapTitulo
            // 
            this.lblSnapTitulo.AutoSize = true;
            this.lblSnapTitulo.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblSnapTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblSnapTitulo.Location = new System.Drawing.Point(15, 10);
            this.lblSnapTitulo.Name = "lblSnapTitulo";
            this.lblSnapTitulo.Size = new System.Drawing.Size(404, 25);
            this.lblSnapTitulo.TabIndex = 0;
            this.lblSnapTitulo.Text = "📸 Snapshots guardados en la base de datos";
            // 
            // lblSnapDesc
            // 
            this.lblSnapDesc.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Italic);
            this.lblSnapDesc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(140)))), ((int)(((byte)(141)))));
            this.lblSnapDesc.Location = new System.Drawing.Point(15, 36);
            this.lblSnapDesc.Name = "lblSnapDesc";
            this.lblSnapDesc.Size = new System.Drawing.Size(600, 18);
            this.lblSnapDesc.TabIndex = 1;
            this.lblSnapDesc.Text = "Los snapshots se almacenan internamente. Selecciona uno y usa los botones para re" +
    "staurarlo o eliminarlo.";
            // 
            // btnRestaurarSnapshot
            // 
            this.btnRestaurarSnapshot.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRestaurarSnapshot.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(68)))), ((int)(((byte)(173)))));
            this.btnRestaurarSnapshot.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRestaurarSnapshot.FlatAppearance.BorderSize = 0;
            this.btnRestaurarSnapshot.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRestaurarSnapshot.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnRestaurarSnapshot.ForeColor = System.Drawing.Color.White;
            this.btnRestaurarSnapshot.Location = new System.Drawing.Point(620, 12);
            this.btnRestaurarSnapshot.Name = "btnRestaurarSnapshot";
            this.btnRestaurarSnapshot.Size = new System.Drawing.Size(195, 40);
            this.btnRestaurarSnapshot.TabIndex = 2;
            this.btnRestaurarSnapshot.Text = "♻️ Restaurar Snapshot";
            this.btnRestaurarSnapshot.UseVisualStyleBackColor = false;
            this.btnRestaurarSnapshot.Click += new System.EventHandler(this.btnRestaurarSnapshot_Click);
            // 
            // btnVolverActualidad
            // 
            this.btnVolverActualidad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnVolverActualidad.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(174)))), ((int)(((byte)(96)))));
            this.btnVolverActualidad.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnVolverActualidad.FlatAppearance.BorderSize = 0;
            this.btnVolverActualidad.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVolverActualidad.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnVolverActualidad.ForeColor = System.Drawing.Color.White;
            this.btnVolverActualidad.Location = new System.Drawing.Point(823, 12);
            this.btnVolverActualidad.Name = "btnVolverActualidad";
            this.btnVolverActualidad.Size = new System.Drawing.Size(210, 40);
            this.btnVolverActualidad.TabIndex = 3;
            this.btnVolverActualidad.Text = "🔄 Volver a la Actualidad";
            this.btnVolverActualidad.UseVisualStyleBackColor = false;
            this.btnVolverActualidad.Click += new System.EventHandler(this.btnVolverActualidad_Click);
            // 
            // btnEliminarSnapshot
            // 
            this.btnEliminarSnapshot.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEliminarSnapshot.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.btnEliminarSnapshot.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEliminarSnapshot.FlatAppearance.BorderSize = 0;
            this.btnEliminarSnapshot.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEliminarSnapshot.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnEliminarSnapshot.ForeColor = System.Drawing.Color.White;
            this.btnEliminarSnapshot.Location = new System.Drawing.Point(1040, 12);
            this.btnEliminarSnapshot.Name = "btnEliminarSnapshot";
            this.btnEliminarSnapshot.Size = new System.Drawing.Size(85, 40);
            this.btnEliminarSnapshot.TabIndex = 4;
            this.btnEliminarSnapshot.Text = "🗑️ Eliminar";
            this.btnEliminarSnapshot.UseVisualStyleBackColor = false;
            this.btnEliminarSnapshot.Click += new System.EventHandler(this.btnEliminarSnapshot_Click);
            // 
            // lblNotaActualidad
            // 
            this.lblNotaActualidad.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Italic);
            this.lblNotaActualidad.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(174)))), ((int)(((byte)(96)))));
            this.lblNotaActualidad.Location = new System.Drawing.Point(620, 58);
            this.lblNotaActualidad.Name = "lblNotaActualidad";
            this.lblNotaActualidad.Size = new System.Drawing.Size(505, 28);
            this.lblNotaActualidad.TabIndex = 5;
            this.lblNotaActualidad.Text = "💡 \'Volver a la Actualidad\' aplica el snapshot más reciente (o el seleccionado) c" +
    "omo estado actual del sistema.";
            // 
            // dgvSnapshots
            // 
            this.dgvSnapshots.AllowUserToAddRows = false;
            this.dgvSnapshots.AllowUserToDeleteRows = false;
            this.dgvSnapshots.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvSnapshots.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSnapshots.BackgroundColor = System.Drawing.Color.White;
            this.dgvSnapshots.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvSnapshots.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvSnapshots.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.dgvSnapshots.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvSnapshots.ColumnHeadersHeight = 40;
            this.dgvSnapshots.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(68)))), ((int)(((byte)(173)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvSnapshots.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgvSnapshots.EnableHeadersVisualStyles = false;
            this.dgvSnapshots.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(231)))), ((int)(((byte)(231)))));
            this.dgvSnapshots.Location = new System.Drawing.Point(10, 113);
            this.dgvSnapshots.Name = "dgvSnapshots";
            this.dgvSnapshots.ReadOnly = true;
            this.dgvSnapshots.RowHeadersVisible = false;
            this.dgvSnapshots.RowHeadersWidth = 51;
            this.dgvSnapshots.RowTemplate.Height = 38;
            this.dgvSnapshots.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSnapshots.Size = new System.Drawing.Size(1135, 430);
            this.dgvSnapshots.TabIndex = 1;
            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(68)))), ((int)(((byte)(173)))));
            this.progressBar.Location = new System.Drawing.Point(15, 678);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(1163, 10);
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar.TabIndex = 3;
            this.progressBar.Visible = false;
            // 
            // FrmBackup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.lblSubtitulo);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.progressBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmBackup";
            this.Text = "Backup y Restauración";
            this.Load += new System.EventHandler(this.FrmBackup_Load);
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
            ((System.ComponentModel.ISupportInitialize)(this.dgvSnapshots)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        // ── Declaraciones de campos de clase ─────────────────────────────────

        // Encabezado del formulario
        private Label lblTitulo;
        private Label lblSubtitulo;

        // TabControl con dos pestañas
        private TabControl tabControl;
        private TabPage tabBackup;
        private TabPage tabSnapshots;

        // Barra de progreso global
        private ProgressBar progressBar;

        // Tab 1 — Panel de configuración (izquierdo)
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

        // Tab 1 — Panel de acciones (central)
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

        // Tab 1 — Panel de log (derecho)
        private Panel panelLog;
        private Label lblLogTitulo;
        private RichTextBox rtbLog;
        private Button btnLimpiarLog;

        // Tab 2 — Snapshots automáticos
        private Panel panelSnapshotTop;
        private Label lblSnapTitulo;
        private Label lblSnapDesc;
        private Button btnRestaurarSnapshot;
        private Button btnVolverActualidad;
        private Button btnEliminarSnapshot;
        private Label lblNotaActualidad;    // ← CAMPO DE CLASE (corrección clave)
        private DataGridView dgvSnapshots;
    }
}