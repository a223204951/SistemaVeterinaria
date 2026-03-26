using System.Drawing;
using System.Windows.Forms;

namespace CapaPresentacion
{
    partial class FrmHistorialPrecios
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.dgvHistorial = new System.Windows.Forms.DataGridView();
            this.panelEstadisticas = new System.Windows.Forms.Panel();
            this.lblEstadisticas = new System.Windows.Forms.Label();
            this.panelBotones = new System.Windows.Forms.Panel();
            this.lblTotal = new System.Windows.Forms.Label();
            this.btnExportar = new System.Windows.Forms.Button();
            this.btnCerrar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistorial)).BeginInit();
            this.panelEstadisticas.SuspendLayout();
            this.panelBotones.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblTitulo.Location = new System.Drawing.Point(20, 20);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(277, 32);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "📊 Historial de Precios";
            // 
            // dgvHistorial
            // 
            this.dgvHistorial.AllowUserToAddRows = false;
            this.dgvHistorial.AllowUserToDeleteRows = false;
            this.dgvHistorial.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvHistorial.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvHistorial.BackgroundColor = System.Drawing.Color.White;
            this.dgvHistorial.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvHistorial.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvHistorial.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvHistorial.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvHistorial.ColumnHeadersHeight = 40;
            this.dgvHistorial.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvHistorial.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvHistorial.EnableHeadersVisualStyles = false;
            this.dgvHistorial.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(231)))), ((int)(((byte)(231)))));
            this.dgvHistorial.Location = new System.Drawing.Point(26, 70);
            this.dgvHistorial.Name = "dgvHistorial";
            this.dgvHistorial.ReadOnly = true;
            this.dgvHistorial.RowHeadersVisible = false;
            this.dgvHistorial.RowHeadersWidth = 51;
            this.dgvHistorial.RowTemplate.Height = 35;
            this.dgvHistorial.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvHistorial.Size = new System.Drawing.Size(884, 400);
            this.dgvHistorial.TabIndex = 1;
            // 
            // panelEstadisticas
            // 
            this.panelEstadisticas.BackColor = System.Drawing.Color.White;
            this.panelEstadisticas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelEstadisticas.Controls.Add(this.lblEstadisticas);
            this.panelEstadisticas.Location = new System.Drawing.Point(916, 70);
            this.panelEstadisticas.Name = "panelEstadisticas";
            this.panelEstadisticas.Size = new System.Drawing.Size(260, 400);
            this.panelEstadisticas.TabIndex = 2;
            // 
            // lblEstadisticas
            // 
            this.lblEstadisticas.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblEstadisticas.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblEstadisticas.Location = new System.Drawing.Point(15, 15);
            this.lblEstadisticas.Name = "lblEstadisticas";
            this.lblEstadisticas.Size = new System.Drawing.Size(230, 370);
            this.lblEstadisticas.TabIndex = 0;
            this.lblEstadisticas.Text = "📊 Estadísticas";
            // 
            // panelBotones
            // 
            this.panelBotones.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelBotones.Controls.Add(this.lblTotal);
            this.panelBotones.Controls.Add(this.btnExportar);
            this.panelBotones.Controls.Add(this.btnCerrar);
            this.panelBotones.Location = new System.Drawing.Point(26, 480);
            this.panelBotones.Name = "panelBotones";
            this.panelBotones.Size = new System.Drawing.Size(1150, 50);
            this.panelBotones.TabIndex = 3;
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTotal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblTotal.Location = new System.Drawing.Point(5, 15);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(260, 23);
            this.lblTotal.TabIndex = 2;
            this.lblTotal.Text = "Total de cambios registrados: 0";
            // 
            // btnExportar
            // 
            this.btnExportar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnExportar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExportar.FlatAppearance.BorderSize = 0;
            this.btnExportar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnExportar.ForeColor = System.Drawing.Color.White;
            this.btnExportar.Location = new System.Drawing.Point(907, 7);
            this.btnExportar.Name = "btnExportar";
            this.btnExportar.Size = new System.Drawing.Size(110, 38);
            this.btnExportar.TabIndex = 1;
            this.btnExportar.Text = "📁 Exportar";
            this.btnExportar.UseVisualStyleBackColor = false;
            this.btnExportar.Click += new System.EventHandler(this.btnExportar_Click);
            // 
            // btnCerrar
            // 
            this.btnCerrar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(165)))), ((int)(((byte)(166)))));
            this.btnCerrar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCerrar.FlatAppearance.BorderSize = 0;
            this.btnCerrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCerrar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnCerrar.ForeColor = System.Drawing.Color.White;
            this.btnCerrar.Location = new System.Drawing.Point(1027, 7);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(110, 38);
            this.btnCerrar.TabIndex = 0;
            this.btnCerrar.Text = "✗ Cerrar";
            this.btnCerrar.UseVisualStyleBackColor = false;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // FrmHistorialPrecios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Controls.Add(this.panelBotones);
            this.Controls.Add(this.panelEstadisticas);
            this.Controls.Add(this.dgvHistorial);
            this.Controls.Add(this.lblTitulo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmHistorialPrecios";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Historial de Precios - Sistema Veterinaria";
            this.Load += new System.EventHandler(this.FrmHistorialPrecios_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistorial)).EndInit();
            this.panelEstadisticas.ResumeLayout(false);
            this.panelBotones.ResumeLayout(false);
            this.panelBotones.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.DataGridView dgvHistorial;
        private System.Windows.Forms.Panel panelEstadisticas;
        private System.Windows.Forms.Label lblEstadisticas;
        private System.Windows.Forms.Panel panelBotones;
        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.Button btnExportar;
        private System.Windows.Forms.Label lblTotal;
    }
}