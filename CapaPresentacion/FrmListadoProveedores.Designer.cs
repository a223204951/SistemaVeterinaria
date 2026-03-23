using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace CapaPresentacion
{
    partial class FrmListadoProveedores
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
            DataGridViewCellStyle style1 = new DataGridViewCellStyle();
            DataGridViewCellStyle style2 = new DataGridViewCellStyle();

            this.lblTitulo = new Label();
            this.panelBusqueda = new Panel();
            this.lblBuscar = new Label();
            this.txtBuscar = new TextBox();
            this.btnLimpiar = new Button();
            this.dgvProveedores = new DataGridView();
            this.panelBotones = new Panel();
            this.lblTotal = new Label();
            this.btnNuevo = new Button();
            this.btnEditar = new Button();
            this.btnEliminar = new Button();
            this.btnHistorial = new Button();

            this.panelBusqueda.SuspendLayout();
            ((ISupportInitialize)this.dgvProveedores).BeginInit();
            this.panelBotones.SuspendLayout();
            this.SuspendLayout();

            // lblTitulo
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            this.lblTitulo.ForeColor = Color.FromArgb(52, 73, 94);
            this.lblTitulo.Location = new Point(20, 20);
            this.lblTitulo.Text = "🏭 Gestión de Proveedores";

            // panelBusqueda
            this.panelBusqueda.BackColor = Color.White;
            this.panelBusqueda.BorderStyle = BorderStyle.FixedSingle;
            this.panelBusqueda.Location = new Point(27, 70);
            this.panelBusqueda.Size = new Size(1150, 90);
            this.panelBusqueda.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.panelBusqueda.Controls.AddRange(new Control[] {
                this.lblBuscar, this.txtBuscar, this.btnLimpiar });

            this.lblBuscar.Text = "Buscar proveedor:";
            this.lblBuscar.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblBuscar.ForeColor = Color.FromArgb(52, 73, 94);
            this.lblBuscar.Location = new Point(12, 14);
            this.lblBuscar.AutoSize = true;

            this.txtBuscar.Font = new Font("Segoe UI", 10F);
            this.txtBuscar.Location = new Point(12, 36);
            this.txtBuscar.Size = new Size(300, 30);
            this.txtBuscar.TextChanged += new System.EventHandler(this.txtBuscar_TextChanged);

            this.btnLimpiar.Text = "🔄 Limpiar";
            this.btnLimpiar.Location = new Point(324, 33);
            this.btnLimpiar.Size = new Size(90, 35);
            this.btnLimpiar.BackColor = Color.FromArgb(149, 165, 166);
            this.btnLimpiar.ForeColor = Color.White;
            this.btnLimpiar.FlatStyle = FlatStyle.Flat;
            this.btnLimpiar.FlatAppearance.BorderSize = 0;
            this.btnLimpiar.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.btnLimpiar.Cursor = Cursors.Hand;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);

            // dgvProveedores
            this.dgvProveedores.AllowUserToAddRows = false;
            this.dgvProveedores.AllowUserToDeleteRows = false;
            this.dgvProveedores.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvProveedores.BackgroundColor = Color.White;
            this.dgvProveedores.BorderStyle = BorderStyle.None;
            this.dgvProveedores.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvProveedores.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;

            style1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            style1.BackColor = Color.FromArgb(52, 73, 94);
            style1.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            style1.ForeColor = Color.White;
            style1.SelectionBackColor = Color.FromArgb(52, 73, 94);
            style1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.dgvProveedores.ColumnHeadersDefaultCellStyle = style1;
            this.dgvProveedores.ColumnHeadersHeight = 40;
            this.dgvProveedores.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            style2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            style2.BackColor = Color.White;
            style2.Font = new Font("Segoe UI", 9F);
            style2.ForeColor = Color.FromArgb(52, 73, 94);
            style2.SelectionBackColor = Color.FromArgb(52, 152, 219);
            style2.SelectionForeColor = Color.White;
            this.dgvProveedores.DefaultCellStyle = style2;
            this.dgvProveedores.EnableHeadersVisualStyles = false;
            this.dgvProveedores.GridColor = Color.FromArgb(231, 231, 231);
            this.dgvProveedores.Location = new Point(27, 175);
            this.dgvProveedores.ReadOnly = true;
            this.dgvProveedores.RowHeadersVisible = false;
            this.dgvProveedores.RowTemplate.Height = 35;
            this.dgvProveedores.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvProveedores.Size = new Size(1150, 295);
            this.dgvProveedores.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.dgvProveedores.DoubleClick += new System.EventHandler(this.dgvProveedores_DoubleClick);

            // panelBotones
            this.panelBotones.Location = new Point(27, 480);
            this.panelBotones.Size = new Size(1150, 90);
            this.panelBotones.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.panelBotones.Controls.AddRange(new Control[] {
                this.lblTotal, this.btnNuevo, this.btnEditar,
                this.btnEliminar, this.btnHistorial });

            this.lblTotal.AutoSize = true;
            this.lblTotal.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.lblTotal.ForeColor = Color.FromArgb(52, 73, 94);
            this.lblTotal.Location = new Point(5, 10);
            this.lblTotal.Text = "Total: 0 proveedores";

            // btnHistorial (morado — igual que btnHistorialPrecios en productos)
            this.btnHistorial.Text = "📋 Historial Compras";
            this.btnHistorial.Location = new Point(5, 45);
            this.btnHistorial.Size = new Size(185, 38);
            this.btnHistorial.BackColor = Color.FromArgb(142, 68, 173);
            this.btnHistorial.ForeColor = Color.White;
            this.btnHistorial.FlatStyle = FlatStyle.Flat;
            this.btnHistorial.FlatAppearance.BorderSize = 0;
            this.btnHistorial.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.btnHistorial.Cursor = Cursors.Hand;
            this.btnHistorial.Click += new System.EventHandler(this.btnHistorial_Click);

            // btnNuevo
            this.btnNuevo.Text = "➕ Nuevo";
            this.btnNuevo.Location = new Point(578, 7);
            this.btnNuevo.Size = new Size(104, 38);
            this.btnNuevo.BackColor = Color.FromArgb(46, 204, 113);
            this.btnNuevo.ForeColor = Color.White;
            this.btnNuevo.FlatStyle = FlatStyle.Flat;
            this.btnNuevo.FlatAppearance.BorderSize = 0;
            this.btnNuevo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.btnNuevo.Cursor = Cursors.Hand;
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);

            // btnEditar
            this.btnEditar.Text = "✏️ Editar";
            this.btnEditar.Location = new Point(688, 7);
            this.btnEditar.Size = new Size(100, 38);
            this.btnEditar.BackColor = Color.FromArgb(241, 196, 15);
            this.btnEditar.ForeColor = Color.White;
            this.btnEditar.FlatStyle = FlatStyle.Flat;
            this.btnEditar.FlatAppearance.BorderSize = 0;
            this.btnEditar.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.btnEditar.Cursor = Cursors.Hand;
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click);

            // btnEliminar
            this.btnEliminar.Text = "🗑️ Desactivar";
            this.btnEliminar.Location = new Point(794, 7);
            this.btnEliminar.Size = new Size(100, 38);
            this.btnEliminar.BackColor = Color.FromArgb(231, 76, 60);
            this.btnEliminar.ForeColor = Color.White;
            this.btnEliminar.FlatStyle = FlatStyle.Flat;
            this.btnEliminar.FlatAppearance.BorderSize = 0;
            this.btnEliminar.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.btnEliminar.Cursor = Cursors.Hand;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);

            // FrmListadoProveedores
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = Color.FromArgb(236, 240, 241);
            this.ClientSize = new Size(1200, 700);
            this.FormBorderStyle = FormBorderStyle.None;
            this.Name = "FrmListadoProveedores";
            this.Text = "Gestión de Proveedores";
            this.Controls.AddRange(new Control[] {
                this.lblTitulo, this.panelBusqueda,
                this.dgvProveedores, this.panelBotones });
            this.Load += new System.EventHandler(this.FrmListadoProveedores_Load);

            this.panelBusqueda.ResumeLayout(false);
            this.panelBusqueda.PerformLayout();
            ((ISupportInitialize)this.dgvProveedores).EndInit();
            this.panelBotones.ResumeLayout(false);
            this.panelBotones.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion

        private Label lblTitulo;
        private Panel panelBusqueda;
        private Label lblBuscar;
        private TextBox txtBuscar;
        private Button btnLimpiar;
        private DataGridView dgvProveedores;
        private Panel panelBotones;
        private Label lblTotal;
        private Button btnNuevo;
        private Button btnEditar;
        private Button btnEliminar;
        private Button btnHistorial;
    }
}