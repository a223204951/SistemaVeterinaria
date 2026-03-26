using System.Drawing;
using System.Windows.Forms;

namespace CapaPresentacion
{
    partial class FrmAuditoria
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.panelFiltros = new System.Windows.Forms.Panel();
            this.lblOperacion = new System.Windows.Forms.Label();
            this.cmbFiltroOperacion = new System.Windows.Forms.ComboBox();
            this.lblFechaInicio = new System.Windows.Forms.Label();
            this.dtpFechaInicio = new System.Windows.Forms.DateTimePicker();
            this.lblFechaFin = new System.Windows.Forms.Label();
            this.dtpFechaFin = new System.Windows.Forms.DateTimePicker();
            this.btnFiltrar = new System.Windows.Forms.Button();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.dgvAuditoria = new System.Windows.Forms.DataGridView();
            this.panelInfo = new System.Windows.Forms.Panel();
            this.lblTotal = new System.Windows.Forms.Label();
            this.panelFiltros.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAuditoria)).BeginInit();
            this.panelInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblTitulo.Location = new System.Drawing.Point(20, 20);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(332, 37);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "🔍 Auditoría de Clientes";
            // 
            // panelFiltros
            // 
            this.panelFiltros.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelFiltros.BackColor = System.Drawing.Color.White;
            this.panelFiltros.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelFiltros.Controls.Add(this.lblOperacion);
            this.panelFiltros.Controls.Add(this.cmbFiltroOperacion);
            this.panelFiltros.Controls.Add(this.lblFechaInicio);
            this.panelFiltros.Controls.Add(this.dtpFechaInicio);
            this.panelFiltros.Controls.Add(this.lblFechaFin);
            this.panelFiltros.Controls.Add(this.dtpFechaFin);
            this.panelFiltros.Controls.Add(this.btnFiltrar);
            this.panelFiltros.Controls.Add(this.btnLimpiar);
            this.panelFiltros.Location = new System.Drawing.Point(27, 70);
            this.panelFiltros.Name = "panelFiltros";
            this.panelFiltros.Size = new System.Drawing.Size(1150, 100);
            this.panelFiltros.TabIndex = 1;
            // 
            // lblOperacion
            // 
            this.lblOperacion.AutoSize = true;
            this.lblOperacion.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblOperacion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblOperacion.Location = new System.Drawing.Point(15, 17);
            this.lblOperacion.Name = "lblOperacion";
            this.lblOperacion.Size = new System.Drawing.Size(84, 20);
            this.lblOperacion.TabIndex = 0;
            this.lblOperacion.Text = "Operación:";
            // 
            // cmbFiltroOperacion
            // 
            this.cmbFiltroOperacion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFiltroOperacion.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbFiltroOperacion.FormattingEnabled = true;
            this.cmbFiltroOperacion.Items.AddRange(new object[] {
            "TODAS",
            "INSERT",
            "UPDATE",
            "DELETE"});
            this.cmbFiltroOperacion.Location = new System.Drawing.Point(15, 40);
            this.cmbFiltroOperacion.Name = "cmbFiltroOperacion";
            this.cmbFiltroOperacion.Size = new System.Drawing.Size(217, 31);
            this.cmbFiltroOperacion.TabIndex = 0;
            // 
            // lblFechaInicio
            // 
            this.lblFechaInicio.AutoSize = true;
            this.lblFechaInicio.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblFechaInicio.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblFechaInicio.Location = new System.Drawing.Point(275, 18);
            this.lblFechaInicio.Name = "lblFechaInicio";
            this.lblFechaInicio.Size = new System.Drawing.Size(95, 20);
            this.lblFechaInicio.TabIndex = 1;
            this.lblFechaInicio.Text = "Fecha Inicio:";
            // 
            // dtpFechaInicio
            // 
            this.dtpFechaInicio.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpFechaInicio.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaInicio.Location = new System.Drawing.Point(275, 41);
            this.dtpFechaInicio.Name = "dtpFechaInicio";
            this.dtpFechaInicio.Size = new System.Drawing.Size(140, 30);
            this.dtpFechaInicio.TabIndex = 1;
            // 
            // lblFechaFin
            // 
            this.lblFechaFin.AutoSize = true;
            this.lblFechaFin.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblFechaFin.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblFechaFin.Location = new System.Drawing.Point(460, 18);
            this.lblFechaFin.Name = "lblFechaFin";
            this.lblFechaFin.Size = new System.Drawing.Size(78, 20);
            this.lblFechaFin.TabIndex = 2;
            this.lblFechaFin.Text = "Fecha Fin:";
            // 
            // dtpFechaFin
            // 
            this.dtpFechaFin.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpFechaFin.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaFin.Location = new System.Drawing.Point(460, 41);
            this.dtpFechaFin.Name = "dtpFechaFin";
            this.dtpFechaFin.Size = new System.Drawing.Size(140, 30);
            this.dtpFechaFin.TabIndex = 2;
            // 
            // btnFiltrar
            // 
            this.btnFiltrar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnFiltrar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFiltrar.FlatAppearance.BorderSize = 0;
            this.btnFiltrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFiltrar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnFiltrar.ForeColor = System.Drawing.Color.White;
            this.btnFiltrar.Location = new System.Drawing.Point(940, 36);
            this.btnFiltrar.Name = "btnFiltrar";
            this.btnFiltrar.Size = new System.Drawing.Size(90, 35);
            this.btnFiltrar.TabIndex = 3;
            this.btnFiltrar.Text = "🔍 Filtrar";
            this.btnFiltrar.UseVisualStyleBackColor = false;
            this.btnFiltrar.Click += new System.EventHandler(this.btnFiltrar_Click);
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(165)))), ((int)(((byte)(166)))));
            this.btnLimpiar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLimpiar.FlatAppearance.BorderSize = 0;
            this.btnLimpiar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLimpiar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnLimpiar.ForeColor = System.Drawing.Color.White;
            this.btnLimpiar.Location = new System.Drawing.Point(1038, 36);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(90, 35);
            this.btnLimpiar.TabIndex = 4;
            this.btnLimpiar.Text = "🔄 Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = false;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // dgvAuditoria
            // 
            this.dgvAuditoria.AllowUserToAddRows = false;
            this.dgvAuditoria.AllowUserToDeleteRows = false;
            this.dgvAuditoria.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvAuditoria.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvAuditoria.BackgroundColor = System.Drawing.Color.White;
            this.dgvAuditoria.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvAuditoria.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvAuditoria.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.dgvAuditoria.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvAuditoria.ColumnHeadersHeight = 40;
            this.dgvAuditoria.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvAuditoria.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvAuditoria.EnableHeadersVisualStyles = false;
            this.dgvAuditoria.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(231)))), ((int)(((byte)(231)))));
            this.dgvAuditoria.Location = new System.Drawing.Point(27, 185);
            this.dgvAuditoria.Name = "dgvAuditoria";
            this.dgvAuditoria.ReadOnly = true;
            this.dgvAuditoria.RowHeadersVisible = false;
            this.dgvAuditoria.RowHeadersWidth = 51;
            this.dgvAuditoria.RowTemplate.Height = 35;
            this.dgvAuditoria.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAuditoria.Size = new System.Drawing.Size(1150, 350);
            this.dgvAuditoria.TabIndex = 2;
            // 
            // panelInfo
            // 
            this.panelInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelInfo.Controls.Add(this.lblTotal);
            this.panelInfo.Location = new System.Drawing.Point(20, 540);
            this.panelInfo.Name = "panelInfo";
            this.panelInfo.Size = new System.Drawing.Size(1150, 50);
            this.panelInfo.TabIndex = 3;
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTotal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblTotal.Location = new System.Drawing.Point(5, 10);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(169, 23);
            this.lblTotal.TabIndex = 0;
            this.lblTotal.Text = "Total de registros: 0";
            // 
            // FrmAuditoria
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Controls.Add(this.panelInfo);
            this.Controls.Add(this.dgvAuditoria);
            this.Controls.Add(this.panelFiltros);
            this.Controls.Add(this.lblTitulo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmAuditoria";
            this.Text = "Auditoría";
            this.Load += new System.EventHandler(this.FrmAuditoria_Load);
            this.panelFiltros.ResumeLayout(false);
            this.panelFiltros.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAuditoria)).EndInit();
            this.panelInfo.ResumeLayout(false);
            this.panelInfo.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Panel panelFiltros;
        private System.Windows.Forms.Label lblOperacion;
        private System.Windows.Forms.ComboBox cmbFiltroOperacion;
        private System.Windows.Forms.Label lblFechaInicio;
        private System.Windows.Forms.DateTimePicker dtpFechaInicio;
        private System.Windows.Forms.Label lblFechaFin;
        private System.Windows.Forms.DateTimePicker dtpFechaFin;
        private System.Windows.Forms.Button btnFiltrar;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.DataGridView dgvAuditoria;
        private System.Windows.Forms.Panel panelInfo;
        private System.Windows.Forms.Label lblTotal;
    }
}