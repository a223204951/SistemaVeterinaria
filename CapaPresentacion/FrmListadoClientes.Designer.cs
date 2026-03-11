using System;
using System.Windows.Forms;
using System.Drawing;

namespace CapaPresentacion
{
    partial class FrmListadoClientes
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
            System.Windows.Forms.DataGridViewCellStyle cs1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle cs2 = new System.Windows.Forms.DataGridViewCellStyle();

            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnbuscar = new System.Windows.Forms.Button();
            this.txtbuscar = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbtnidcliente = new System.Windows.Forms.RadioButton();
            this.rbtnnombre = new System.Windows.Forms.RadioButton();
            this.dlistado = new System.Windows.Forms.DataGridView();
            this.panelBotones = new System.Windows.Forms.Panel();
            this.lblTotal = new System.Windows.Forms.Label();
            this.btnnuevo = new System.Windows.Forms.Button();
            this.btneditar = new System.Windows.Forms.Button();
            this.btneliminar = new System.Windows.Forms.Button();

            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dlistado)).BeginInit();
            this.panelBotones.SuspendLayout();
            this.SuspendLayout();

            // label1
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(52, 73, 94);
            this.label1.Location = new System.Drawing.Point(20, 20);
            this.label1.Name = "label1";
            this.label1.TabIndex = 0;
            this.label1.Text = "📋 Gestión de Clientes";

            // panel1 (búsqueda)
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btnbuscar);
            this.panel1.Controls.Add(this.txtbuscar);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Location = new System.Drawing.Point(27, 70);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(896, 100);
            this.panel1.TabIndex = 1;

            // btnbuscar
            this.btnbuscar.BackColor = System.Drawing.Color.FromArgb(52, 152, 219);
            this.btnbuscar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnbuscar.FlatAppearance.BorderSize = 0;
            this.btnbuscar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnbuscar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnbuscar.ForeColor = System.Drawing.Color.White;
            this.btnbuscar.Location = new System.Drawing.Point(800, 35);
            this.btnbuscar.Name = "btnbuscar";
            this.btnbuscar.Size = new System.Drawing.Size(80, 35);
            this.btnbuscar.TabIndex = 2;
            this.btnbuscar.Text = "🔍 Buscar";
            this.btnbuscar.UseVisualStyleBackColor = false;
            this.btnbuscar.Click += new System.EventHandler(this.btnbuscar_Click);

            // txtbuscar
            this.txtbuscar.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtbuscar.Location = new System.Drawing.Point(266, 38);
            this.txtbuscar.Name = "txtbuscar";
            this.txtbuscar.Size = new System.Drawing.Size(520, 30);
            this.txtbuscar.TabIndex = 1;
            // Enlace para búsqueda en tiempo real
            this.txtbuscar.TextChanged += new System.EventHandler(this.txtbuscar_TextChanged);

            // groupBox1
            this.groupBox1.Controls.Add(this.rbtnidcliente);
            this.groupBox1.Controls.Add(this.rbtnnombre);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.groupBox1.ForeColor = System.Drawing.Color.FromArgb(52, 73, 94);
            this.groupBox1.Location = new System.Drawing.Point(15, 15);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(234, 70);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Buscar por:";

            // rbtnidcliente
            this.rbtnidcliente.AutoSize = true;
            this.rbtnidcliente.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbtnidcliente.Location = new System.Drawing.Point(117, 30);
            this.rbtnidcliente.Name = "rbtnidcliente";
            this.rbtnidcliente.Size = new System.Drawing.Size(95, 24);
            this.rbtnidcliente.TabIndex = 1;
            this.rbtnidcliente.TabStop = true;
            this.rbtnidcliente.Text = "ID Cliente";
            this.rbtnidcliente.UseVisualStyleBackColor = true;

            // rbtnnombre
            this.rbtnnombre.AutoSize = true;
            this.rbtnnombre.Checked = true;
            this.rbtnnombre.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbtnnombre.Location = new System.Drawing.Point(10, 30);
            this.rbtnnombre.Name = "rbtnnombre";
            this.rbtnnombre.Size = new System.Drawing.Size(85, 24);
            this.rbtnnombre.TabIndex = 0;
            this.rbtnnombre.TabStop = true;
            this.rbtnnombre.Text = "Nombre";
            this.rbtnnombre.UseVisualStyleBackColor = true;

            // dlistado
            this.dlistado.AllowUserToAddRows = false;
            this.dlistado.AllowUserToDeleteRows = false;
            this.dlistado.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dlistado.BackgroundColor = System.Drawing.Color.White;
            this.dlistado.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dlistado.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dlistado.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            cs1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            cs1.BackColor = System.Drawing.Color.FromArgb(52, 73, 94);
            cs1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            cs1.ForeColor = System.Drawing.Color.White;
            cs1.SelectionBackColor = System.Drawing.Color.FromArgb(52, 73, 94);
            cs1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            cs1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dlistado.ColumnHeadersDefaultCellStyle = cs1;
            this.dlistado.ColumnHeadersHeight = 40;
            this.dlistado.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            cs2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            cs2.BackColor = System.Drawing.Color.White;
            cs2.Font = new System.Drawing.Font("Segoe UI", 9F);
            cs2.ForeColor = System.Drawing.Color.FromArgb(52, 73, 94);
            cs2.SelectionBackColor = System.Drawing.Color.FromArgb(52, 152, 219);
            cs2.SelectionForeColor = System.Drawing.Color.White;
            cs2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dlistado.DefaultCellStyle = cs2;
            this.dlistado.EnableHeadersVisualStyles = false;
            this.dlistado.GridColor = System.Drawing.Color.FromArgb(231, 231, 231);
            this.dlistado.Location = new System.Drawing.Point(27, 185);
            this.dlistado.Name = "dlistado";
            this.dlistado.ReadOnly = true;
            this.dlistado.RowHeadersVisible = false;
            this.dlistado.RowHeadersWidth = 51;
            this.dlistado.RowTemplate.Height = 35;
            this.dlistado.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dlistado.Size = new System.Drawing.Size(896, 340);
            this.dlistado.TabIndex = 2;

            // panelBotones
            this.panelBotones.Controls.Add(this.lblTotal);
            this.panelBotones.Controls.Add(this.btnnuevo);
            this.panelBotones.Controls.Add(this.btneditar);
            this.panelBotones.Controls.Add(this.btneliminar);
            this.panelBotones.Location = new System.Drawing.Point(27, 535);
            this.panelBotones.Name = "panelBotones";
            this.panelBotones.Size = new System.Drawing.Size(896, 50);
            this.panelBotones.TabIndex = 3;

            // lblTotal
            this.lblTotal.AutoSize = true;
            this.lblTotal.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTotal.ForeColor = System.Drawing.Color.FromArgb(52, 73, 94);
            this.lblTotal.Location = new System.Drawing.Point(5, 15);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.TabIndex = 0;
            this.lblTotal.Text = "Total de clientes: 0";

            // btnnuevo
            this.btnnuevo.BackColor = System.Drawing.Color.FromArgb(46, 204, 113);
            this.btnnuevo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnnuevo.FlatAppearance.BorderSize = 0;
            this.btnnuevo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnnuevo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnnuevo.ForeColor = System.Drawing.Color.White;
            this.btnnuevo.Location = new System.Drawing.Point(576, 7);
            this.btnnuevo.Name = "btnnuevo";
            this.btnnuevo.Size = new System.Drawing.Size(95, 38);
            this.btnnuevo.TabIndex = 1;
            this.btnnuevo.Text = "➕ Nuevo";
            this.btnnuevo.UseVisualStyleBackColor = false;
            this.btnnuevo.Click += new System.EventHandler(this.btnnuevo_Click);

            // btneditar
            this.btneditar.BackColor = System.Drawing.Color.FromArgb(241, 196, 15);
            this.btneditar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btneditar.FlatAppearance.BorderSize = 0;
            this.btneditar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btneditar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btneditar.ForeColor = System.Drawing.Color.White;
            this.btneditar.Location = new System.Drawing.Point(678, 7);
            this.btneditar.Name = "btneditar";
            this.btneditar.Size = new System.Drawing.Size(95, 38);
            this.btneditar.TabIndex = 2;
            this.btneditar.Text = "✏️ Editar";
            this.btneditar.UseVisualStyleBackColor = false;
            this.btneditar.Click += new System.EventHandler(this.btneditar_Click);

            // btneliminar
            this.btneliminar.BackColor = System.Drawing.Color.FromArgb(231, 76, 60);
            this.btneliminar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btneliminar.FlatAppearance.BorderSize = 0;
            this.btneliminar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btneliminar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btneliminar.ForeColor = System.Drawing.Color.White;
            this.btneliminar.Location = new System.Drawing.Point(780, 7);
            this.btneliminar.Name = "btneliminar";
            this.btneliminar.Size = new System.Drawing.Size(110, 38);
            this.btneliminar.TabIndex = 3;
            this.btneliminar.Text = "🗑️ Eliminar";
            this.btneliminar.UseVisualStyleBackColor = false;
            this.btneliminar.Click += new System.EventHandler(this.btneliminar_Click);

            // FrmListadoClientes
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(236, 240, 241);
            this.ClientSize = new System.Drawing.Size(950, 600);
            this.Controls.Add(this.panelBotones);
            this.Controls.Add(this.dlistado);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmListadoClientes";
            this.Text = "FrmListadoCliente";
            this.Load += new System.EventHandler(this.FrmListadoCliente_Load);

            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dlistado)).EndInit();
            this.panelBotones.ResumeLayout(false);
            this.panelBotones.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbtnidcliente;
        private System.Windows.Forms.RadioButton rbtnnombre;
        private System.Windows.Forms.Button btnbuscar;
        private System.Windows.Forms.TextBox txtbuscar;
        private System.Windows.Forms.DataGridView dlistado;
        private System.Windows.Forms.Panel panelBotones;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Button btnnuevo;
        private System.Windows.Forms.Button btneditar;
        private System.Windows.Forms.Button btneliminar;
    }
}