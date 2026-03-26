using System.Drawing;
using System.Windows.Forms;

namespace CapaPresentacion
{
    partial class FrmListadoMascotas
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.panelBusqueda = new System.Windows.Forms.Panel();
            this.groupBoxBuscar = new System.Windows.Forms.GroupBox();
            this.rbtnNombreDueno = new System.Windows.Forms.RadioButton();
            this.rbtnNombreMascota = new System.Windows.Forms.RadioButton();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.dgvMascotas = new System.Windows.Forms.DataGridView();
            this.panelBotones = new System.Windows.Forms.Panel();
            this.lblTotal = new System.Windows.Forms.Label();
            this.btneliminar = new System.Windows.Forms.Button();
            this.btneditar = new System.Windows.Forms.Button();
            this.btnnuevo = new System.Windows.Forms.Button();
            this.panelBusqueda.SuspendLayout();
            this.groupBoxBuscar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMascotas)).BeginInit();
            this.panelBotones.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.label1.Location = new System.Drawing.Point(20, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(327, 37);
            this.label1.TabIndex = 0;
            this.label1.Text = "🐾 Gestión de Mascotas";
            // 
            // panelBusqueda
            // 
            this.panelBusqueda.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelBusqueda.BackColor = System.Drawing.Color.White;
            this.panelBusqueda.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelBusqueda.Controls.Add(this.groupBoxBuscar);
            this.panelBusqueda.Controls.Add(this.txtBuscar);
            this.panelBusqueda.Controls.Add(this.btnBuscar);
            this.panelBusqueda.Location = new System.Drawing.Point(27, 70);
            this.panelBusqueda.Name = "panelBusqueda";
            this.panelBusqueda.Size = new System.Drawing.Size(1150, 100);
            this.panelBusqueda.TabIndex = 1;
            // 
            // groupBoxBuscar
            // 
            this.groupBoxBuscar.Controls.Add(this.rbtnNombreDueno);
            this.groupBoxBuscar.Controls.Add(this.rbtnNombreMascota);
            this.groupBoxBuscar.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.groupBoxBuscar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.groupBoxBuscar.Location = new System.Drawing.Point(15, 12);
            this.groupBoxBuscar.Name = "groupBoxBuscar";
            this.groupBoxBuscar.Size = new System.Drawing.Size(256, 72);
            this.groupBoxBuscar.TabIndex = 0;
            this.groupBoxBuscar.TabStop = false;
            this.groupBoxBuscar.Text = "Buscar por:";
            // 
            // rbtnNombreDueno
            // 
            this.rbtnNombreDueno.AutoSize = true;
            this.rbtnNombreDueno.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbtnNombreDueno.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.rbtnNombreDueno.Location = new System.Drawing.Point(130, 28);
            this.rbtnNombreDueno.Name = "rbtnNombreDueno";
            this.rbtnNombreDueno.Size = new System.Drawing.Size(74, 24);
            this.rbtnNombreDueno.TabIndex = 1;
            this.rbtnNombreDueno.Text = "Dueño";
            this.rbtnNombreDueno.UseVisualStyleBackColor = true;
            // 
            // rbtnNombreMascota
            // 
            this.rbtnNombreMascota.AutoSize = true;
            this.rbtnNombreMascota.Checked = true;
            this.rbtnNombreMascota.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbtnNombreMascota.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.rbtnNombreMascota.Location = new System.Drawing.Point(10, 28);
            this.rbtnNombreMascota.Name = "rbtnNombreMascota";
            this.rbtnNombreMascota.Size = new System.Drawing.Size(85, 24);
            this.rbtnNombreMascota.TabIndex = 0;
            this.rbtnNombreMascota.TabStop = true;
            this.rbtnNombreMascota.Text = "Nombre";
            this.rbtnNombreMascota.UseVisualStyleBackColor = true;
            // 
            // txtBuscar
            // 
            this.txtBuscar.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtBuscar.Location = new System.Drawing.Point(282, 38);
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(764, 30);
            this.txtBuscar.TabIndex = 1;
            this.txtBuscar.TextChanged += new System.EventHandler(this.txtBuscar_TextChanged);
            // 
            // btnBuscar
            // 
            this.btnBuscar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnBuscar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBuscar.FlatAppearance.BorderSize = 0;
            this.btnBuscar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnBuscar.ForeColor = System.Drawing.Color.White;
            this.btnBuscar.Location = new System.Drawing.Point(1052, 36);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(80, 35);
            this.btnBuscar.TabIndex = 2;
            this.btnBuscar.Text = "🔍 Buscar";
            this.btnBuscar.UseVisualStyleBackColor = false;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // dgvMascotas
            // 
            this.dgvMascotas.AllowUserToAddRows = false;
            this.dgvMascotas.AllowUserToDeleteRows = false;
            this.dgvMascotas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvMascotas.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvMascotas.BackgroundColor = System.Drawing.Color.White;
            this.dgvMascotas.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvMascotas.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvMascotas.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvMascotas.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvMascotas.ColumnHeadersHeight = 40;
            this.dgvMascotas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvMascotas.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvMascotas.EnableHeadersVisualStyles = false;
            this.dgvMascotas.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(231)))), ((int)(((byte)(231)))));
            this.dgvMascotas.Location = new System.Drawing.Point(27, 185);
            this.dgvMascotas.Name = "dgvMascotas";
            this.dgvMascotas.ReadOnly = true;
            this.dgvMascotas.RowHeadersVisible = false;
            this.dgvMascotas.RowHeadersWidth = 51;
            this.dgvMascotas.RowTemplate.Height = 35;
            this.dgvMascotas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMascotas.Size = new System.Drawing.Size(1150, 340);
            this.dgvMascotas.TabIndex = 2;
            this.dgvMascotas.DoubleClick += new System.EventHandler(this.dgvMascotas_DoubleClick);
            // 
            // panelBotones
            // 
            this.panelBotones.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelBotones.Controls.Add(this.lblTotal);
            this.panelBotones.Controls.Add(this.btneliminar);
            this.panelBotones.Controls.Add(this.btneditar);
            this.panelBotones.Controls.Add(this.btnnuevo);
            this.panelBotones.Location = new System.Drawing.Point(27, 535);
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
            this.lblTotal.Size = new System.Drawing.Size(172, 23);
            this.lblTotal.TabIndex = 3;
            this.lblTotal.Text = "Total de mascotas: 0";
            // 
            // btneliminar
            // 
            this.btneliminar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.btneliminar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btneliminar.FlatAppearance.BorderSize = 0;
            this.btneliminar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btneliminar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btneliminar.ForeColor = System.Drawing.Color.White;
            this.btneliminar.Location = new System.Drawing.Point(1023, 7);
            this.btneliminar.Name = "btneliminar";
            this.btneliminar.Size = new System.Drawing.Size(110, 38);
            this.btneliminar.TabIndex = 2;
            this.btneliminar.Text = "🗑️ Eliminar";
            this.btneliminar.UseVisualStyleBackColor = false;
            this.btneliminar.Click += new System.EventHandler(this.btneliminar_Click);
            // 
            // btneditar
            // 
            this.btneditar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(196)))), ((int)(((byte)(15)))));
            this.btneditar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btneditar.FlatAppearance.BorderSize = 0;
            this.btneditar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btneditar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btneditar.ForeColor = System.Drawing.Color.White;
            this.btneditar.Location = new System.Drawing.Point(917, 7);
            this.btneditar.Name = "btneditar";
            this.btneditar.Size = new System.Drawing.Size(100, 38);
            this.btneditar.TabIndex = 1;
            this.btneditar.Text = "✏️ Editar";
            this.btneditar.UseVisualStyleBackColor = false;
            this.btneditar.Click += new System.EventHandler(this.btneditar_Click);
            // 
            // btnnuevo
            // 
            this.btnnuevo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnnuevo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnnuevo.FlatAppearance.BorderSize = 0;
            this.btnnuevo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnnuevo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnnuevo.ForeColor = System.Drawing.Color.White;
            this.btnnuevo.Location = new System.Drawing.Point(807, 7);
            this.btnnuevo.Name = "btnnuevo";
            this.btnnuevo.Size = new System.Drawing.Size(104, 38);
            this.btnnuevo.TabIndex = 0;
            this.btnnuevo.Text = "➕ Nuevo";
            this.btnnuevo.UseVisualStyleBackColor = false;
            this.btnnuevo.Click += new System.EventHandler(this.btnnuevo_Click);
            // 
            // FrmListadoMascotas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Controls.Add(this.panelBotones);
            this.Controls.Add(this.dgvMascotas);
            this.Controls.Add(this.panelBusqueda);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmListadoMascotas";
            this.Text = "Gestión de Mascotas";
            this.Load += new System.EventHandler(this.FrmListadoMascotas_Load);
            this.panelBusqueda.ResumeLayout(false);
            this.panelBusqueda.PerformLayout();
            this.groupBoxBuscar.ResumeLayout(false);
            this.groupBoxBuscar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMascotas)).EndInit();
            this.panelBotones.ResumeLayout(false);
            this.panelBotones.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panelBusqueda;
        private System.Windows.Forms.GroupBox groupBoxBuscar;
        private System.Windows.Forms.RadioButton rbtnNombreMascota;
        private System.Windows.Forms.RadioButton rbtnNombreDueno;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.DataGridView dgvMascotas;
        private System.Windows.Forms.Panel panelBotones;
        private System.Windows.Forms.Button btnnuevo;
        private System.Windows.Forms.Button btneditar;
        private System.Windows.Forms.Button btneliminar;
        private System.Windows.Forms.Label lblTotal;
    }
}