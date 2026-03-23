using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace CapaPresentacion
{
    partial class FrmHistorialComprasProveedor
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
            this.lblNombreProveedor = new Label();
            this.dgvHistorial = new DataGridView();
            this.lblResumen = new Label();
            this.btnCerrar = new Button();

            ((ISupportInitialize)this.dgvHistorial).BeginInit();
            this.SuspendLayout();

            // lblTitulo
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            this.lblTitulo.ForeColor = Color.FromArgb(52, 73, 94);
            this.lblTitulo.Location = new Point(20, 18);
            this.lblTitulo.Text = "📋 Historial de Compras";

            // lblNombreProveedor
            this.lblNombreProveedor.AutoSize = true;
            this.lblNombreProveedor.Font = new Font("Segoe UI", 10F, FontStyle.Italic);
            this.lblNombreProveedor.ForeColor = Color.FromArgb(52, 152, 219);
            this.lblNombreProveedor.Location = new Point(20, 50);
            this.lblNombreProveedor.Text = "Proveedor: -";

            // dgvHistorial
            this.dgvHistorial.AllowUserToAddRows = false;
            this.dgvHistorial.AllowUserToDeleteRows = false;
            this.dgvHistorial.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvHistorial.BackgroundColor = Color.White;
            this.dgvHistorial.BorderStyle = BorderStyle.None;
            this.dgvHistorial.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvHistorial.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;

            style1.BackColor = Color.FromArgb(52, 73, 94);
            style1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            style1.ForeColor = Color.White;
            style1.SelectionBackColor = Color.FromArgb(52, 73, 94);
            style1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            this.dgvHistorial.ColumnHeadersDefaultCellStyle = style1;
            this.dgvHistorial.ColumnHeadersHeight = 38;
            this.dgvHistorial.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            style2.BackColor = Color.White;
            style2.Font = new Font("Segoe UI", 9F);
            style2.ForeColor = Color.FromArgb(52, 73, 94);
            style2.SelectionBackColor = Color.FromArgb(52, 152, 219);
            style2.SelectionForeColor = Color.White;
            this.dgvHistorial.DefaultCellStyle = style2;
            this.dgvHistorial.EnableHeadersVisualStyles = false;
            this.dgvHistorial.GridColor = Color.FromArgb(231, 231, 231);
            this.dgvHistorial.Location = new Point(20, 80);
            this.dgvHistorial.ReadOnly = true;
            this.dgvHistorial.RowHeadersVisible = false;
            this.dgvHistorial.RowTemplate.Height = 32;
            this.dgvHistorial.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvHistorial.Size = new Size(720, 340);

            // lblResumen
            this.lblResumen.AutoSize = true;
            this.lblResumen.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblResumen.ForeColor = Color.FromArgb(46, 204, 113);
            this.lblResumen.Location = new Point(20, 430);
            this.lblResumen.Text = "Cargando...";

            // btnCerrar
            this.btnCerrar.Text = "✗ Cerrar";
            this.btnCerrar.Location = new Point(590, 460);
            this.btnCerrar.Size = new Size(150, 40);
            this.btnCerrar.BackColor = Color.FromArgb(149, 165, 166);
            this.btnCerrar.ForeColor = Color.White;
            this.btnCerrar.FlatStyle = FlatStyle.Flat;
            this.btnCerrar.FlatAppearance.BorderSize = 0;
            this.btnCerrar.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnCerrar.Cursor = Cursors.Hand;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);

            // FrmHistorialComprasProveedor
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = Color.FromArgb(236, 240, 241);
            this.ClientSize = new Size(760, 515);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Text = "Historial de Compras del Proveedor";
            this.Controls.AddRange(new Control[] {
                this.lblTitulo, this.lblNombreProveedor,
                this.dgvHistorial, this.lblResumen, this.btnCerrar });
            this.Load += new System.EventHandler(this.FrmHistorialComprasProveedor_Load);

            ((ISupportInitialize)this.dgvHistorial).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion

        private Label lblTitulo;
        private Label lblNombreProveedor;
        private DataGridView dgvHistorial;
        private Label lblResumen;
        private Button btnCerrar;
    }
}