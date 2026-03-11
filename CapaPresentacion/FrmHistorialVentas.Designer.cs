using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace CapaPresentacion
{
    partial class FrmHistorialVentas
    {
        private System.ComponentModel.IContainer components = null;
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
            csC.SelectionBackColor = Color.FromArgb(52, 152, 219); csC.SelectionForeColor = Color.White;

            this.lblTitulo = new Label();
            this.panelFiltros = new Panel();
            this.lblFecIni = new Label(); this.dtpInicio = new DateTimePicker();
            this.lblFecFin = new Label(); this.dtpFin = new DateTimePicker();
            this.lblEstadoLbl = new Label(); this.cmbEstado = new ComboBox();
            this.btnFiltrar = new Button(); this.btnLimpiar = new Button();
            this.tabControl = new TabControl();
            this.tabVentas = new TabPage();
            this.dgvVentas = new DataGridView();
            this.lblTotalVentas = new Label();
            this.tabMovimientos = new TabPage();
            this.lblTipoMovLbl = new Label(); this.cmbTipoMov = new ComboBox();
            this.dgvMovimientos = new DataGridView();
            this.lblTotalMov = new Label();

            this.panelFiltros.SuspendLayout();
            ((ISupportInitialize)this.dgvVentas).BeginInit();
            ((ISupportInitialize)this.dgvMovimientos).BeginInit();
            this.tabControl.SuspendLayout();
            this.SuspendLayout();

            // lblTitulo
            this.lblTitulo.AutoSize = true; this.lblTitulo.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            this.lblTitulo.ForeColor = Color.FromArgb(52, 73, 94); this.lblTitulo.Location = new Point(20, 15);
            this.lblTitulo.Text = "📊 Historial de Caja";

            // panelFiltros
            this.panelFiltros.BackColor = Color.White; this.panelFiltros.BorderStyle = BorderStyle.FixedSingle;
            this.panelFiltros.Location = new Point(15, 60); this.panelFiltros.Size = new Size(915, 70);
            this.panelFiltros.Controls.AddRange(new Control[] {
                this.lblFecIni, this.dtpInicio, this.lblFecFin, this.dtpFin,
                this.lblEstadoLbl, this.cmbEstado, this.btnFiltrar, this.btnLimpiar });

            this.lblFecIni.Text = "Desde:"; this.lblFecIni.AutoSize = true;
            this.lblFecIni.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblFecIni.ForeColor = Color.FromArgb(52, 73, 94); this.lblFecIni.Location = new Point(10, 12);

            this.dtpInicio.Font = new Font("Segoe UI", 10F); this.dtpInicio.Format = DateTimePickerFormat.Short;
            this.dtpInicio.Location = new Point(60, 10); this.dtpInicio.Size = new Size(130, 30);

            this.lblFecFin.Text = "Hasta:"; this.lblFecFin.AutoSize = true;
            this.lblFecFin.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblFecFin.ForeColor = Color.FromArgb(52, 73, 94); this.lblFecFin.Location = new Point(205, 12);

            this.dtpFin.Font = new Font("Segoe UI", 10F); this.dtpFin.Format = DateTimePickerFormat.Short;
            this.dtpFin.Location = new Point(255, 10); this.dtpFin.Size = new Size(130, 30);

            this.lblEstadoLbl.Text = "Estado venta:"; this.lblEstadoLbl.AutoSize = true;
            this.lblEstadoLbl.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblEstadoLbl.ForeColor = Color.FromArgb(52, 73, 94); this.lblEstadoLbl.Location = new Point(400, 12);

            this.cmbEstado.DropDownStyle = ComboBoxStyle.DropDownList; this.cmbEstado.Font = new Font("Segoe UI", 9F);
            this.cmbEstado.Items.AddRange(new object[] { "TODAS", "CONFIRMADA", "CANCELADA", "ACTIVA" });
            this.cmbEstado.Location = new Point(500, 8); this.cmbEstado.Size = new Size(130, 30);

            this.btnFiltrar.Text = "🔍 Filtrar"; this.btnFiltrar.Location = new Point(660, 10); this.btnFiltrar.Size = new Size(110, 38);
            this.btnFiltrar.BackColor = Color.FromArgb(52, 152, 219); this.btnFiltrar.ForeColor = Color.White;
            this.btnFiltrar.FlatStyle = FlatStyle.Flat; this.btnFiltrar.FlatAppearance.BorderSize = 0;
            this.btnFiltrar.Font = new Font("Segoe UI", 9F, FontStyle.Bold); this.btnFiltrar.Cursor = Cursors.Hand;
            this.btnFiltrar.Click += new System.EventHandler(this.btnFiltrar_Click);

            this.btnLimpiar.Text = "🔄 Limpiar"; this.btnLimpiar.Location = new Point(780, 10); this.btnLimpiar.Size = new Size(110, 38);
            this.btnLimpiar.BackColor = Color.FromArgb(149, 165, 166); this.btnLimpiar.ForeColor = Color.White;
            this.btnLimpiar.FlatStyle = FlatStyle.Flat; this.btnLimpiar.FlatAppearance.BorderSize = 0;
            this.btnLimpiar.Font = new Font("Segoe UI", 9F, FontStyle.Bold); this.btnLimpiar.Cursor = Cursors.Hand;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);

            // tabControl
            this.tabControl.Location = new Point(15, 140); this.tabControl.Size = new Size(915, 440);
            this.tabControl.TabPages.Add(this.tabVentas);
            this.tabControl.TabPages.Add(this.tabMovimientos);
            this.tabControl.Font = new Font("Segoe UI", 10F);

            // tabVentas
            this.tabVentas.Text = "📋 Ventas"; this.tabVentas.BackColor = Color.FromArgb(236, 240, 241);
            this.tabVentas.Controls.AddRange(new Control[] { this.dgvVentas, this.lblTotalVentas });

            this.dgvVentas.Location = new Point(5, 5); this.dgvVentas.Size = new Size(900, 370);
            this.dgvVentas.AllowUserToAddRows = false; this.dgvVentas.AllowUserToDeleteRows = false;
            this.dgvVentas.ReadOnly = true; this.dgvVentas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvVentas.RowHeadersVisible = false; this.dgvVentas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvVentas.ColumnHeadersDefaultCellStyle = csH; this.dgvVentas.DefaultCellStyle = csC;
            this.dgvVentas.EnableHeadersVisualStyles = false; this.dgvVentas.ColumnHeadersHeight = 35;
            this.dgvVentas.RowTemplate.Height = 30; this.dgvVentas.BackgroundColor = Color.White;
            this.dgvVentas.BorderStyle = BorderStyle.None;

            this.lblTotalVentas.Text = "Ventas encontradas: 0"; this.lblTotalVentas.AutoSize = true;
            this.lblTotalVentas.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.lblTotalVentas.ForeColor = Color.FromArgb(52, 73, 94); this.lblTotalVentas.Location = new Point(5, 385);

            // tabMovimientos
            this.tabMovimientos.Text = "📦 Movimientos de Stock"; this.tabMovimientos.BackColor = Color.FromArgb(236, 240, 241);
            this.tabMovimientos.Controls.AddRange(new Control[] {
                this.lblTipoMovLbl, this.cmbTipoMov, this.dgvMovimientos, this.lblTotalMov });

            this.lblTipoMovLbl.Text = "Tipo:"; this.lblTipoMovLbl.AutoSize = true;
            this.lblTipoMovLbl.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblTipoMovLbl.ForeColor = Color.FromArgb(52, 73, 94); this.lblTipoMovLbl.Location = new Point(5, 8);

            this.cmbTipoMov.DropDownStyle = ComboBoxStyle.DropDownList; this.cmbTipoMov.Font = new Font("Segoe UI", 9F);
            this.cmbTipoMov.Items.AddRange(new object[] { "TODOS", "ENTRADA", "SALIDA" });
            this.cmbTipoMov.Location = new Point(45, 5); this.cmbTipoMov.Size = new Size(120, 30);

            this.dgvMovimientos.Location = new Point(5, 38); this.dgvMovimientos.Size = new Size(900, 345);
            this.dgvMovimientos.AllowUserToAddRows = false; this.dgvMovimientos.AllowUserToDeleteRows = false;
            this.dgvMovimientos.ReadOnly = true; this.dgvMovimientos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvMovimientos.RowHeadersVisible = false; this.dgvMovimientos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvMovimientos.ColumnHeadersDefaultCellStyle = csH; this.dgvMovimientos.DefaultCellStyle = csC;
            this.dgvMovimientos.EnableHeadersVisualStyles = false; this.dgvMovimientos.ColumnHeadersHeight = 35;
            this.dgvMovimientos.RowTemplate.Height = 30; this.dgvMovimientos.BackgroundColor = Color.White;
            this.dgvMovimientos.BorderStyle = BorderStyle.None;

            this.lblTotalMov.Text = "Movimientos encontrados: 0"; this.lblTotalMov.AutoSize = true;
            this.lblTotalMov.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.lblTotalMov.ForeColor = Color.FromArgb(52, 73, 94); this.lblTotalMov.Location = new Point(5, 390);

            // FrmHistorialVentas
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = Color.FromArgb(236, 240, 241);
            this.ClientSize = new Size(950, 600);
            this.FormBorderStyle = FormBorderStyle.None;
            this.Name = "FrmHistorialVentas";
            this.Text = "Historial de Caja";
            this.Controls.AddRange(new Control[] { this.lblTitulo, this.panelFiltros, this.tabControl });
            this.Load += new System.EventHandler(this.FrmHistorialVentas_Load);

            this.panelFiltros.ResumeLayout(false); this.panelFiltros.PerformLayout();
            ((ISupportInitialize)this.dgvVentas).EndInit();
            ((ISupportInitialize)this.dgvMovimientos).EndInit();
            this.tabControl.ResumeLayout(false);
            this.ResumeLayout(false); this.PerformLayout();
        }
        #endregion

        private Label lblTitulo;
        private Panel panelFiltros;
        private Label lblFecIni, lblFecFin, lblEstadoLbl, lblTipoMovLbl;
        private DateTimePicker dtpInicio, dtpFin;
        private ComboBox cmbEstado, cmbTipoMov;
        private Button btnFiltrar, btnLimpiar;
        private TabControl tabControl;
        private TabPage tabVentas, tabMovimientos;
        private DataGridView dgvVentas, dgvMovimientos;
        private Label lblTotalVentas, lblTotalMov;
    }
}