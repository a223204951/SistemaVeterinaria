using System.Drawing;
using System.Windows.Forms;

namespace CapaPresentacion
{
    partial class FrmSesiones
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
            this.lblUsuario = new System.Windows.Forms.Label();
            this.cmbUsuario = new System.Windows.Forms.ComboBox();
            this.lblFechaInicio = new System.Windows.Forms.Label();
            this.dtpFechaInicio = new System.Windows.Forms.DateTimePicker();
            this.lblFechaFin = new System.Windows.Forms.Label();
            this.dtpFechaFin = new System.Windows.Forms.DateTimePicker();
            this.btnFiltrar = new System.Windows.Forms.Button();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.dgvSesiones = new System.Windows.Forms.DataGridView();
            this.panelInfo = new System.Windows.Forms.Panel();
            this.lblTotalSesiones = new System.Windows.Forms.Label();
            this.lblTiempoPromedio = new System.Windows.Forms.Label();
            this.panelFiltros.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSesiones)).BeginInit();
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
            this.lblTitulo.Size = new System.Drawing.Size(326, 37);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "🔐 Registro de Sesiones";
            // 
            // panelFiltros
            // 
            this.panelFiltros.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelFiltros.BackColor = System.Drawing.Color.White;
            this.panelFiltros.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelFiltros.Controls.Add(this.lblUsuario);
            this.panelFiltros.Controls.Add(this.cmbUsuario);
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
            // lblUsuario
            // 
            this.lblUsuario.AutoSize = true;
            this.lblUsuario.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblUsuario.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblUsuario.Location = new System.Drawing.Point(15, 15);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Size = new System.Drawing.Size(67, 20);
            this.lblUsuario.TabIndex = 0;
            this.lblUsuario.Text = "Usuario:";
            // 
            // cmbUsuario
            // 
            this.cmbUsuario.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUsuario.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbUsuario.FormattingEnabled = true;
            this.cmbUsuario.Location = new System.Drawing.Point(15, 38);
            this.cmbUsuario.Name = "cmbUsuario";
            this.cmbUsuario.Size = new System.Drawing.Size(244, 31);
            this.cmbUsuario.TabIndex = 0;
            // 
            // lblFechaInicio
            // 
            this.lblFechaInicio.AutoSize = true;
            this.lblFechaInicio.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblFechaInicio.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblFechaInicio.Location = new System.Drawing.Point(284, 15);
            this.lblFechaInicio.Name = "lblFechaInicio";
            this.lblFechaInicio.Size = new System.Drawing.Size(95, 20);
            this.lblFechaInicio.TabIndex = 1;
            this.lblFechaInicio.Text = "Fecha Inicio:";
            // 
            // dtpFechaInicio
            // 
            this.dtpFechaInicio.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpFechaInicio.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaInicio.Location = new System.Drawing.Point(284, 38);
            this.dtpFechaInicio.Name = "dtpFechaInicio";
            this.dtpFechaInicio.Size = new System.Drawing.Size(140, 30);
            this.dtpFechaInicio.TabIndex = 1;
            // 
            // lblFechaFin
            // 
            this.lblFechaFin.AutoSize = true;
            this.lblFechaFin.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblFechaFin.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblFechaFin.Location = new System.Drawing.Point(439, 15);
            this.lblFechaFin.Name = "lblFechaFin";
            this.lblFechaFin.Size = new System.Drawing.Size(78, 20);
            this.lblFechaFin.TabIndex = 2;
            this.lblFechaFin.Text = "Fecha Fin:";
            // 
            // dtpFechaFin
            // 
            this.dtpFechaFin.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpFechaFin.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaFin.Location = new System.Drawing.Point(439, 38);
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
            this.btnFiltrar.Location = new System.Drawing.Point(700, 35);
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
            this.btnLimpiar.Location = new System.Drawing.Point(798, 35);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(90, 35);
            this.btnLimpiar.TabIndex = 4;
            this.btnLimpiar.Text = "🔄 Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = false;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // dgvSesiones
            // 
            this.dgvSesiones.AllowUserToAddRows = false;
            this.dgvSesiones.AllowUserToDeleteRows = false;
            this.dgvSesiones.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvSesiones.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSesiones.BackgroundColor = System.Drawing.Color.White;
            this.dgvSesiones.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvSesiones.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvSesiones.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.dgvSesiones.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvSesiones.ColumnHeadersHeight = 40;
            this.dgvSesiones.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvSesiones.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvSesiones.EnableHeadersVisualStyles = false;
            this.dgvSesiones.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(231)))), ((int)(((byte)(231)))));
            this.dgvSesiones.Location = new System.Drawing.Point(27, 185);
            this.dgvSesiones.Name = "dgvSesiones";
            this.dgvSesiones.ReadOnly = true;
            this.dgvSesiones.RowHeadersVisible = false;
            this.dgvSesiones.RowHeadersWidth = 51;
            this.dgvSesiones.RowTemplate.Height = 35;
            this.dgvSesiones.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSesiones.Size = new System.Drawing.Size(1150, 335);
            this.dgvSesiones.TabIndex = 2;
            // 
            // panelInfo
            // 
            this.panelInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelInfo.Controls.Add(this.lblTotalSesiones);
            this.panelInfo.Controls.Add(this.lblTiempoPromedio);
            this.panelInfo.Location = new System.Drawing.Point(20, 540);
            this.panelInfo.Name = "panelInfo";
            this.panelInfo.Size = new System.Drawing.Size(1150, 50);
            this.panelInfo.TabIndex = 3;
            // 
            // lblTotalSesiones
            // 
            this.lblTotalSesiones.AutoSize = true;
            this.lblTotalSesiones.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTotalSesiones.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblTotalSesiones.Location = new System.Drawing.Point(5, 10);
            this.lblTotalSesiones.Name = "lblTotalSesiones";
            this.lblTotalSesiones.Size = new System.Drawing.Size(163, 23);
            this.lblTotalSesiones.TabIndex = 0;
            this.lblTotalSesiones.Text = "Total de sesiones: 0";
            // 
            // lblTiempoPromedio
            // 
            this.lblTiempoPromedio.AutoSize = true;
            this.lblTiempoPromedio.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTiempoPromedio.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblTiempoPromedio.Location = new System.Drawing.Point(280, 10);
            this.lblTiempoPromedio.Name = "lblTiempoPromedio";
            this.lblTiempoPromedio.Size = new System.Drawing.Size(245, 23);
            this.lblTiempoPromedio.TabIndex = 1;
            this.lblTiempoPromedio.Text = "Tiempo promedio: 0 minutos";
            // 
            // FrmSesiones
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Controls.Add(this.panelInfo);
            this.Controls.Add(this.dgvSesiones);
            this.Controls.Add(this.panelFiltros);
            this.Controls.Add(this.lblTitulo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmSesiones";
            this.Text = "Sesiones";
            this.Load += new System.EventHandler(this.FrmSesiones_Load);
            this.panelFiltros.ResumeLayout(false);
            this.panelFiltros.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSesiones)).EndInit();
            this.panelInfo.ResumeLayout(false);
            this.panelInfo.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Panel panelFiltros;
        private System.Windows.Forms.Label lblUsuario;
        private System.Windows.Forms.ComboBox cmbUsuario;
        private System.Windows.Forms.Label lblFechaInicio;
        private System.Windows.Forms.DateTimePicker dtpFechaInicio;
        private System.Windows.Forms.Label lblFechaFin;
        private System.Windows.Forms.DateTimePicker dtpFechaFin;
        private System.Windows.Forms.Button btnFiltrar;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.DataGridView dgvSesiones;
        private System.Windows.Forms.Panel panelInfo;
        private System.Windows.Forms.Label lblTotalSesiones;
        private System.Windows.Forms.Label lblTiempoPromedio;
    }
}