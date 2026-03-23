using System.Drawing;
using System.Windows.Forms;

namespace CapaPresentacion
{
    partial class FrmListadoProductos
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
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbCategoria = new System.Windows.Forms.ComboBox();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panelAlertas = new System.Windows.Forms.Panel();
            this.lblAlertaVencimiento = new System.Windows.Forms.Label();
            this.lblAlertaStock = new System.Windows.Forms.Label();
            this.dgvProductos = new System.Windows.Forms.DataGridView();
            this.panelBotones = new System.Windows.Forms.Panel();
            this.btnHistorialPrecios = new System.Windows.Forms.Button();
            this.btnEtiqueta = new System.Windows.Forms.Button();
            this.lblTotal = new System.Windows.Forms.Label();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnEditar = new System.Windows.Forms.Button();
            this.btnNuevo = new System.Windows.Forms.Button();

            this.panelBusqueda.SuspendLayout();
            this.panelAlertas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductos)).BeginInit();
            this.panelBotones.SuspendLayout();
            this.SuspendLayout();

            // ── label1 ────────────────────────────────────────────────────────
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(52, 73, 94);
            this.label1.Location = new System.Drawing.Point(20, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(316, 37);
            this.label1.TabIndex = 0;
            this.label1.Text = "📦 Gestión de Productos";

            // ── panelBusqueda ─────────────────────────────────────────────────
            this.panelBusqueda.BackColor = System.Drawing.Color.White;
            this.panelBusqueda.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelBusqueda.Controls.Add(this.btnLimpiar);
            this.panelBusqueda.Controls.Add(this.label3);
            this.panelBusqueda.Controls.Add(this.cmbCategoria);
            this.panelBusqueda.Controls.Add(this.txtBuscar);
            this.panelBusqueda.Controls.Add(this.label2);
            this.panelBusqueda.Location = new System.Drawing.Point(27, 70);
            this.panelBusqueda.Name = "panelBusqueda";
            this.panelBusqueda.Size = new System.Drawing.Size(1150, 100);
            this.panelBusqueda.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.panelBusqueda.TabIndex = 1;

            // btnLimpiar
            this.btnLimpiar.BackColor = System.Drawing.Color.FromArgb(149, 165, 166);
            this.btnLimpiar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLimpiar.FlatAppearance.BorderSize = 0;
            this.btnLimpiar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLimpiar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnLimpiar.ForeColor = System.Drawing.Color.White;
            this.btnLimpiar.Location = new System.Drawing.Point(540, 35);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(90, 35);
            this.btnLimpiar.TabIndex = 4;
            this.btnLimpiar.Text = "🔄 Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = false;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);

            // label3
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.FromArgb(52, 73, 94);
            this.label3.Location = new System.Drawing.Point(330, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 20);
            this.label3.TabIndex = 3;
            this.label3.Text = "Categoría:";

            // cmbCategoria
            this.cmbCategoria.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCategoria.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbCategoria.FormattingEnabled = true;
            this.cmbCategoria.Location = new System.Drawing.Point(334, 38);
            this.cmbCategoria.Name = "cmbCategoria";
            this.cmbCategoria.Size = new System.Drawing.Size(190, 31);
            this.cmbCategoria.TabIndex = 2;
            this.cmbCategoria.SelectedIndexChanged += new System.EventHandler(this.cmbCategoria_SelectedIndexChanged);

            // txtBuscar
            this.txtBuscar.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtBuscar.Location = new System.Drawing.Point(15, 38);
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(300, 30);
            this.txtBuscar.TabIndex = 1;
            this.txtBuscar.TextChanged += new System.EventHandler(this.txtBuscar_TextChanged);

            // label2
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.FromArgb(52, 73, 94);
            this.label2.Location = new System.Drawing.Point(11, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(141, 20);
            this.label2.TabIndex = 0;
            this.label2.Text = "Buscar producto:";

            // ── panelAlertas ──────────────────────────────────────────────────
            this.panelAlertas.BackColor = System.Drawing.Color.White;
            this.panelAlertas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelAlertas.Controls.Add(this.lblAlertaVencimiento);
            this.panelAlertas.Controls.Add(this.lblAlertaStock);
            this.panelAlertas.Location = new System.Drawing.Point(695, 70);
            this.panelAlertas.Name = "panelAlertas";
            this.panelAlertas.Size = new System.Drawing.Size(228, 100);
            this.panelAlertas.TabIndex = 2;

            // lblAlertaVencimiento
            this.lblAlertaVencimiento.AutoSize = true;
            this.lblAlertaVencimiento.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblAlertaVencimiento.ForeColor = System.Drawing.Color.FromArgb(231, 76, 60);
            this.lblAlertaVencimiento.Location = new System.Drawing.Point(10, 55);
            this.lblAlertaVencimiento.Name = "lblAlertaVencimiento";
            this.lblAlertaVencimiento.Size = new System.Drawing.Size(205, 20);
            this.lblAlertaVencimiento.TabIndex = 1;
            this.lblAlertaVencimiento.Text = "⚠️ 0 próximos a vencer";
            this.lblAlertaVencimiento.Visible = false;

            // lblAlertaStock
            this.lblAlertaStock.AutoSize = true;
            this.lblAlertaStock.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblAlertaStock.ForeColor = System.Drawing.Color.FromArgb(230, 126, 34);
            this.lblAlertaStock.Location = new System.Drawing.Point(10, 20);
            this.lblAlertaStock.Name = "lblAlertaStock";
            this.lblAlertaStock.Size = new System.Drawing.Size(162, 20);
            this.lblAlertaStock.TabIndex = 0;
            this.lblAlertaStock.Text = "⚠️ 0 con stock bajo";
            this.lblAlertaStock.Visible = false;

            // ── dgvProductos ──────────────────────────────────────────────────
            this.dgvProductos.AllowUserToAddRows = false;
            this.dgvProductos.AllowUserToDeleteRows = false;
            this.dgvProductos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvProductos.BackgroundColor = System.Drawing.Color.White;
            this.dgvProductos.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvProductos.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvProductos.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;

            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(52, 73, 94);
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(52, 73, 94);
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvProductos.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvProductos.ColumnHeadersHeight = 40;
            this.dgvProductos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(52, 73, 94);
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(52, 152, 219);
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvProductos.DefaultCellStyle = dataGridViewCellStyle2;

            this.dgvProductos.EnableHeadersVisualStyles = false;
            this.dgvProductos.GridColor = System.Drawing.Color.FromArgb(231, 231, 231);
            this.dgvProductos.Location = new System.Drawing.Point(27, 185);
            this.dgvProductos.Name = "dgvProductos";
            this.dgvProductos.ReadOnly = true;
            this.dgvProductos.RowHeadersVisible = false;
            this.dgvProductos.RowHeadersWidth = 51;
            this.dgvProductos.RowTemplate.Height = 35;
            this.dgvProductos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvProductos.Size = new System.Drawing.Size(1150, 300);
            this.dgvProductos.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.dgvProductos.TabIndex = 3;
            this.dgvProductos.DoubleClick += new System.EventHandler(this.dgvProductos_DoubleClick);

            // ── panelBotones ──────────────────────────────────────────────────
            this.panelBotones.Controls.Add(this.btnHistorialPrecios);
            this.panelBotones.Controls.Add(this.btnEtiqueta);
            this.panelBotones.Controls.Add(this.lblTotal);
            this.panelBotones.Controls.Add(this.btnEliminar);
            this.panelBotones.Controls.Add(this.btnEditar);
            this.panelBotones.Controls.Add(this.btnNuevo);
            this.panelBotones.Location = new System.Drawing.Point(27, 495);
            this.panelBotones.Name = "panelBotones";
            this.panelBotones.Size = new System.Drawing.Size(1150, 90);
            this.panelBotones.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.panelBotones.TabIndex = 4;

            // btnHistorialPrecios
            this.btnHistorialPrecios.BackColor = System.Drawing.Color.FromArgb(142, 68, 173);
            this.btnHistorialPrecios.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnHistorialPrecios.FlatAppearance.BorderSize = 0;
            this.btnHistorialPrecios.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHistorialPrecios.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnHistorialPrecios.ForeColor = System.Drawing.Color.White;
            this.btnHistorialPrecios.Location = new System.Drawing.Point(5, 45);
            this.btnHistorialPrecios.Name = "btnHistorialPrecios";
            this.btnHistorialPrecios.Size = new System.Drawing.Size(180, 38);
            this.btnHistorialPrecios.TabIndex = 4;
            this.btnHistorialPrecios.Text = "📊 Historial Precios";
            this.btnHistorialPrecios.UseVisualStyleBackColor = false;
            this.btnHistorialPrecios.Click += new System.EventHandler(this.btnHistorialPrecios_Click);

            // btnEtiqueta  ← NUEVO
            this.btnEtiqueta.BackColor = System.Drawing.Color.FromArgb(52, 152, 219);
            this.btnEtiqueta.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEtiqueta.FlatAppearance.BorderSize = 0;
            this.btnEtiqueta.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEtiqueta.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnEtiqueta.ForeColor = System.Drawing.Color.White;
            this.btnEtiqueta.Location = new System.Drawing.Point(195, 45);
            this.btnEtiqueta.Name = "btnEtiqueta";
            this.btnEtiqueta.Size = new System.Drawing.Size(150, 38);
            this.btnEtiqueta.TabIndex = 5;
            this.btnEtiqueta.Text = "🔲 Ver Etiqueta";
            this.btnEtiqueta.UseVisualStyleBackColor = false;
            this.btnEtiqueta.Click += new System.EventHandler(this.btnEtiqueta_Click);

            // lblTotal
            this.lblTotal.AutoSize = true;
            this.lblTotal.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTotal.ForeColor = System.Drawing.Color.FromArgb(52, 73, 94);
            this.lblTotal.Location = new System.Drawing.Point(5, 10);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(191, 23);
            this.lblTotal.TabIndex = 3;
            this.lblTotal.Text = "Total de productos: 0";

            // btnEliminar
            this.btnEliminar.BackColor = System.Drawing.Color.FromArgb(231, 76, 60);
            this.btnEliminar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEliminar.FlatAppearance.BorderSize = 0;
            this.btnEliminar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEliminar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnEliminar.ForeColor = System.Drawing.Color.White;
            this.btnEliminar.Location = new System.Drawing.Point(776, 7);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(110, 38);
            this.btnEliminar.TabIndex = 2;
            this.btnEliminar.Text = "🗑️ Eliminar";
            this.btnEliminar.UseVisualStyleBackColor = false;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);

            // btnEditar
            this.btnEditar.BackColor = System.Drawing.Color.FromArgb(241, 196, 15);
            this.btnEditar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEditar.FlatAppearance.BorderSize = 0;
            this.btnEditar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnEditar.ForeColor = System.Drawing.Color.White;
            this.btnEditar.Location = new System.Drawing.Point(670, 7);
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(100, 38);
            this.btnEditar.TabIndex = 1;
            this.btnEditar.Text = "✏️ Editar";
            this.btnEditar.UseVisualStyleBackColor = false;
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click);

            // btnNuevo
            this.btnNuevo.BackColor = System.Drawing.Color.FromArgb(46, 204, 113);
            this.btnNuevo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNuevo.FlatAppearance.BorderSize = 0;
            this.btnNuevo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNuevo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnNuevo.ForeColor = System.Drawing.Color.White;
            this.btnNuevo.Location = new System.Drawing.Point(560, 7);
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(104, 38);
            this.btnNuevo.TabIndex = 0;
            this.btnNuevo.Text = "➕ Nuevo";
            this.btnNuevo.UseVisualStyleBackColor = false;
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);

            // ── FrmListadoProductos ───────────────────────────────────────────
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(236, 240, 241);
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Controls.Add(this.panelBotones);
            this.Controls.Add(this.dgvProductos);
            this.Controls.Add(this.panelAlertas);
            this.Controls.Add(this.panelBusqueda);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmListadoProductos";
            this.Text = "Gestión de Productos";
            this.Load += new System.EventHandler(this.FrmListadoProductos_Load);

            this.panelBusqueda.ResumeLayout(false);
            this.panelBusqueda.PerformLayout();
            this.panelAlertas.ResumeLayout(false);
            this.panelAlertas.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductos)).EndInit();
            this.panelBotones.ResumeLayout(false);
            this.panelBotones.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panelBusqueda;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.ComboBox cmbCategoria;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Panel panelAlertas;
        private System.Windows.Forms.Label lblAlertaStock;
        private System.Windows.Forms.Label lblAlertaVencimiento;
        private System.Windows.Forms.DataGridView dgvProductos;
        private System.Windows.Forms.Panel panelBotones;
        private System.Windows.Forms.Button btnNuevo;
        private System.Windows.Forms.Button btnEditar;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Button btnHistorialPrecios;
        private System.Windows.Forms.Button btnEtiqueta;   // ← NUEVO
    }
}