using System.Drawing;
using System.Windows.Forms;

namespace CapaPresentacion
{
    partial class FrmListadoEmpleados
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
            // ── Estilos de cabecera y celda compartidos ────────────────────────
            System.Windows.Forms.DataGridViewCellStyle csHeader = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle csCell = new System.Windows.Forms.DataGridViewCellStyle();

            csHeader.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            csHeader.BackColor = System.Drawing.Color.FromArgb(52, 73, 94);
            csHeader.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            csHeader.ForeColor = System.Drawing.Color.White;
            csHeader.SelectionBackColor = System.Drawing.Color.FromArgb(52, 73, 94);
            csHeader.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            csHeader.WrapMode = System.Windows.Forms.DataGridViewTriState.True;

            csCell.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            csCell.BackColor = System.Drawing.Color.White;
            csCell.Font = new System.Drawing.Font("Segoe UI", 9F);
            csCell.ForeColor = System.Drawing.Color.FromArgb(52, 73, 94);
            csCell.SelectionBackColor = System.Drawing.Color.FromArgb(52, 152, 219);
            csCell.SelectionForeColor = System.Drawing.Color.White;
            csCell.WrapMode = System.Windows.Forms.DataGridViewTriState.False;

            // ── Controles ────────────────────────────────────────────────────────
            this.lblTitulo = new System.Windows.Forms.Label();
            this.panelBusqueda = new System.Windows.Forms.Panel();
            this.groupBoxBuscar = new System.Windows.Forms.GroupBox();
            this.rbtNombre = new System.Windows.Forms.RadioButton();
            this.rbtId = new System.Windows.Forms.RadioButton();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.lblLeyenda = new System.Windows.Forms.Label();
            this.dgvEmpleados = new System.Windows.Forms.DataGridView();
            this.panelBotones = new System.Windows.Forms.Panel();
            this.lblTotal = new System.Windows.Forms.Label();
            this.btnNuevo = new System.Windows.Forms.Button();
            this.btnEditar = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();

            this.panelBusqueda.SuspendLayout();
            this.groupBoxBuscar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEmpleados)).BeginInit();
            this.panelBotones.SuspendLayout();
            this.SuspendLayout();

            // ── lblTitulo ─────────────────────────────────────────────────────
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.FromArgb(52, 73, 94);
            this.lblTitulo.Location = new System.Drawing.Point(20, 20);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "👥 Gestión de Empleados";

            // ── panelBusqueda ─────────────────────────────────────────────────
            this.panelBusqueda.BackColor = System.Drawing.Color.White;
            this.panelBusqueda.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelBusqueda.Location = new System.Drawing.Point(27, 70);
            this.panelBusqueda.Name = "panelBusqueda";
            this.panelBusqueda.Size = new System.Drawing.Size(1150, 100);
            this.panelBusqueda.Anchor = System.Windows.Forms.AnchorStyles.Top
                                           | System.Windows.Forms.AnchorStyles.Left
                                           | System.Windows.Forms.AnchorStyles.Right;
            this.panelBusqueda.TabIndex = 1;
            this.panelBusqueda.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.groupBoxBuscar, this.txtBuscar, this.btnBuscar,
                this.btnLimpiar, this.lblLeyenda });

            // ── groupBoxBuscar ────────────────────────────────────────────────
            this.groupBoxBuscar.Controls.Add(this.rbtNombre);
            this.groupBoxBuscar.Controls.Add(this.rbtId);
            this.groupBoxBuscar.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.groupBoxBuscar.ForeColor = System.Drawing.Color.FromArgb(52, 73, 94);
            this.groupBoxBuscar.Location = new System.Drawing.Point(15, 12);
            this.groupBoxBuscar.Name = "groupBoxBuscar";
            this.groupBoxBuscar.Size = new System.Drawing.Size(245, 72);
            this.groupBoxBuscar.TabIndex = 0;
            this.groupBoxBuscar.TabStop = false;
            this.groupBoxBuscar.Text = "Buscar por:";

            // rbtNombre
            this.rbtNombre.AutoSize = true;
            this.rbtNombre.Checked = true;
            this.rbtNombre.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbtNombre.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.rbtNombre.Location = new System.Drawing.Point(10, 30);
            this.rbtNombre.Name = "rbtNombre";
            this.rbtNombre.Text = "Nombre / Apellido";
            this.rbtNombre.TabIndex = 0;
            this.rbtNombre.TabStop = true;

            // rbtId
            this.rbtId.AutoSize = true;
            this.rbtId.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbtId.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.rbtId.Location = new System.Drawing.Point(150, 30);
            this.rbtId.Name = "rbtId";
            this.rbtId.Text = "ID";
            this.rbtId.TabIndex = 1;

            // txtBuscar
            this.txtBuscar.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtBuscar.Location = new System.Drawing.Point(272, 38);
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(480, 30);
            this.txtBuscar.TabIndex = 1;
            this.txtBuscar.TextChanged += new System.EventHandler(this.txtBuscar_TextChanged);

            // btnBuscar
            this.btnBuscar.BackColor = System.Drawing.Color.FromArgb(52, 152, 219);
            this.btnBuscar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBuscar.FlatAppearance.BorderSize = 0;
            this.btnBuscar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnBuscar.ForeColor = System.Drawing.Color.White;
            this.btnBuscar.Location = new System.Drawing.Point(765, 35);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(85, 35);
            this.btnBuscar.TabIndex = 2;
            this.btnBuscar.Text = "🔍 Buscar";
            this.btnBuscar.UseVisualStyleBackColor = false;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);

            // btnLimpiar
            this.btnLimpiar.BackColor = System.Drawing.Color.FromArgb(149, 165, 166);
            this.btnLimpiar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLimpiar.FlatAppearance.BorderSize = 0;
            this.btnLimpiar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLimpiar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnLimpiar.ForeColor = System.Drawing.Color.White;
            this.btnLimpiar.Location = new System.Drawing.Point(858, 35);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(85, 35);
            this.btnLimpiar.TabIndex = 3;
            this.btnLimpiar.Text = "🔄 Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = false;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);

            // lblLeyenda — leyenda de colores
            this.lblLeyenda.AutoSize = true;
            this.lblLeyenda.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Italic);
            this.lblLeyenda.ForeColor = System.Drawing.Color.FromArgb(127, 140, 141);
            this.lblLeyenda.Location = new System.Drawing.Point(272, 74);
            this.lblLeyenda.Name = "lblLeyenda";
            this.lblLeyenda.Text = "🟢 Veterinario   🔵 Cajero   🟠 Asistente   🟣 Administrador   ⬜ Inactivo";

            // ── dgvEmpleados ──────────────────────────────────────────────────
            this.dgvEmpleados.AllowUserToAddRows = false;
            this.dgvEmpleados.AllowUserToDeleteRows = false;
            this.dgvEmpleados.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvEmpleados.BackgroundColor = System.Drawing.Color.White;
            this.dgvEmpleados.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvEmpleados.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvEmpleados.ColumnHeadersBorderStyle =
                System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvEmpleados.ColumnHeadersDefaultCellStyle = csHeader;
            this.dgvEmpleados.ColumnHeadersHeight = 40;
            this.dgvEmpleados.ColumnHeadersHeightSizeMode =
                System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvEmpleados.DefaultCellStyle = csCell;
            this.dgvEmpleados.EnableHeadersVisualStyles = false;
            this.dgvEmpleados.GridColor = System.Drawing.Color.FromArgb(231, 231, 231);
            this.dgvEmpleados.Location = new System.Drawing.Point(27, 185);
            this.dgvEmpleados.Name = "dgvEmpleados";
            this.dgvEmpleados.ReadOnly = true;
            this.dgvEmpleados.RowHeadersVisible = false;
            this.dgvEmpleados.RowHeadersWidth = 51;
            this.dgvEmpleados.RowTemplate.Height = 35;
            this.dgvEmpleados.SelectionMode =
                System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvEmpleados.Size = new System.Drawing.Size(1150, 330);
            this.dgvEmpleados.Anchor = System.Windows.Forms.AnchorStyles.Top
                                     | System.Windows.Forms.AnchorStyles.Bottom
                                     | System.Windows.Forms.AnchorStyles.Left
                                     | System.Windows.Forms.AnchorStyles.Right;
            this.dgvEmpleados.TabIndex = 2;
            this.dgvEmpleados.DoubleClick += new System.EventHandler(this.dgvEmpleados_DoubleClick);

            // ── panelBotones ──────────────────────────────────────────────────
            this.panelBotones.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblTotal, this.btnNuevo, this.btnEditar, this.btnEliminar });
            this.panelBotones.Location = new System.Drawing.Point(27, 525);
            this.panelBotones.Name = "panelBotones";
            this.panelBotones.Size = new System.Drawing.Size(1150, 50);
            this.panelBotones.Anchor = System.Windows.Forms.AnchorStyles.Bottom
                                       | System.Windows.Forms.AnchorStyles.Left
                                       | System.Windows.Forms.AnchorStyles.Right;
            this.panelBotones.TabIndex = 3;

            // lblTotal
            this.lblTotal.AutoSize = true;
            this.lblTotal.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTotal.ForeColor = System.Drawing.Color.FromArgb(52, 73, 94);
            this.lblTotal.Location = new System.Drawing.Point(5, 15);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.TabIndex = 0;
            this.lblTotal.Text = "Total: 0 empleados";

            // btnNuevo
            this.btnNuevo.BackColor = System.Drawing.Color.FromArgb(46, 204, 113);
            this.btnNuevo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNuevo.FlatAppearance.BorderSize = 0;
            this.btnNuevo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNuevo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnNuevo.ForeColor = System.Drawing.Color.White;
            this.btnNuevo.Location = new System.Drawing.Point(576, 7);
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(104, 38);
            this.btnNuevo.TabIndex = 1;
            this.btnNuevo.Text = "➕ Nuevo";
            this.btnNuevo.UseVisualStyleBackColor = false;
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);

            // btnEditar
            this.btnEditar.BackColor = System.Drawing.Color.FromArgb(241, 196, 15);
            this.btnEditar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEditar.FlatAppearance.BorderSize = 0;
            this.btnEditar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnEditar.ForeColor = System.Drawing.Color.White;
            this.btnEditar.Location = new System.Drawing.Point(686, 7);
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(100, 38);
            this.btnEditar.TabIndex = 2;
            this.btnEditar.Text = "✏️ Editar";
            this.btnEditar.UseVisualStyleBackColor = false;
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click);

            // btnEliminar
            this.btnEliminar.BackColor = System.Drawing.Color.FromArgb(231, 76, 60);
            this.btnEliminar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEliminar.FlatAppearance.BorderSize = 0;
            this.btnEliminar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEliminar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnEliminar.ForeColor = System.Drawing.Color.White;
            this.btnEliminar.Location = new System.Drawing.Point(792, 7);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(110, 38);
            this.btnEliminar.TabIndex = 3;
            this.btnEliminar.Text = "🗑️ Dar de Baja";
            this.btnEliminar.UseVisualStyleBackColor = false;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);

            // ── FrmListadoEmpleados ───────────────────────────────────────────
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(236, 240, 241);
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblTitulo, this.panelBusqueda, this.dgvEmpleados, this.panelBotones });
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmListadoEmpleados";
            this.Text = "Gestión de Empleados";
            this.Load += new System.EventHandler(this.FrmListadoEmpleados_Load);

            this.panelBusqueda.ResumeLayout(false);
            this.panelBusqueda.PerformLayout();
            this.groupBoxBuscar.ResumeLayout(false);
            this.groupBoxBuscar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEmpleados)).EndInit();
            this.panelBotones.ResumeLayout(false);
            this.panelBotones.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        // ── Declaraciones ────────────────────────────────────────────────────
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Panel panelBusqueda;
        private System.Windows.Forms.GroupBox groupBoxBuscar;
        private System.Windows.Forms.RadioButton rbtNombre;
        private System.Windows.Forms.RadioButton rbtId;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Label lblLeyenda;
        private System.Windows.Forms.DataGridView dgvEmpleados;
        private System.Windows.Forms.Panel panelBotones;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Button btnNuevo;
        private System.Windows.Forms.Button btnEditar;
        private System.Windows.Forms.Button btnEliminar;
    }
}