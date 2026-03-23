using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace CapaPresentacion
{
    partial class FrmEtiquetaProducto
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
            this.lblTitulo = new Label();
            this.panelEtiqueta = new Panel();
            this.lblNombreProducto = new Label();
            this.lblPrecioProducto = new Label();
            this.picBarcode = new PictureBox();
            this.lblCodigoNum = new Label();
            this.lblEstadoCodigo = new Label();
            this.panelOpciones = new Panel();
            this.lblCopiasLbl = new Label();
            this.nudCopias = new NumericUpDown();
            this.btnImprimir = new Button();
            this.btnRegenerarCodigo = new Button();
            this.btnCerrar = new Button();

            ((ISupportInitialize)this.picBarcode).BeginInit();
            ((ISupportInitialize)this.nudCopias).BeginInit();
            this.panelEtiqueta.SuspendLayout();
            this.panelOpciones.SuspendLayout();
            this.SuspendLayout();

            // ── lblTitulo ─────────────────────────────────────────────────────
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            this.lblTitulo.ForeColor = Color.FromArgb(52, 73, 94);
            this.lblTitulo.Location = new Point(20, 15);
            this.lblTitulo.Text = "🔲 Etiqueta de Producto";

            // ── panelEtiqueta — preview de la etiqueta ────────────────────────
            this.panelEtiqueta.BackColor = Color.White;
            this.panelEtiqueta.BorderStyle = BorderStyle.FixedSingle;
            this.panelEtiqueta.Location = new Point(20, 60);
            this.panelEtiqueta.Size = new Size(340, 220);
            this.panelEtiqueta.Controls.AddRange(new Control[] {
                this.lblNombreProducto, this.lblPrecioProducto,
                this.picBarcode, this.lblCodigoNum });

            // Simula la etiqueta física
            this.lblNombreProducto.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            this.lblNombreProducto.ForeColor = Color.FromArgb(52, 73, 94);
            this.lblNombreProducto.Location = new Point(10, 12);
            this.lblNombreProducto.Size = new Size(315, 24);
            this.lblNombreProducto.Text = "Nombre del producto";

            this.lblPrecioProducto.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            this.lblPrecioProducto.ForeColor = Color.FromArgb(46, 204, 113);
            this.lblPrecioProducto.Location = new Point(10, 38);
            this.lblPrecioProducto.AutoSize = true;
            this.lblPrecioProducto.Text = "$0.00 MXN";

            this.picBarcode.Location = new Point(10, 78);
            this.picBarcode.Size = new Size(318, 100);
            this.picBarcode.SizeMode = PictureBoxSizeMode.StretchImage;
            this.picBarcode.BackColor = Color.White;
            this.picBarcode.BorderStyle = BorderStyle.None;

            this.lblCodigoNum.Font = new Font("Courier New", 11F, FontStyle.Bold);
            this.lblCodigoNum.ForeColor = Color.FromArgb(52, 73, 94);
            this.lblCodigoNum.Location = new Point(10, 183);
            this.lblCodigoNum.Size = new Size(318, 22);
            this.lblCodigoNum.TextAlign = ContentAlignment.MiddleCenter;
            this.lblCodigoNum.Text = "0000000000000";

            // ── panelOpciones ─────────────────────────────────────────────────
            this.panelOpciones.BackColor = Color.White;
            this.panelOpciones.BorderStyle = BorderStyle.FixedSingle;
            this.panelOpciones.Location = new Point(375, 60);
            this.panelOpciones.Size = new Size(200, 220);
            this.panelOpciones.Controls.AddRange(new Control[] {
                this.lblCopiasLbl, this.nudCopias,
                this.btnImprimir, this.btnRegenerarCodigo, this.btnCerrar });

            this.lblCopiasLbl.Text = "Número de copias:";
            this.lblCopiasLbl.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblCopiasLbl.ForeColor = Color.FromArgb(52, 73, 94);
            this.lblCopiasLbl.Location = new Point(10, 15);
            this.lblCopiasLbl.AutoSize = true;

            this.nudCopias.Location = new Point(10, 38);
            this.nudCopias.Size = new Size(80, 28);
            this.nudCopias.Minimum = 1;
            this.nudCopias.Maximum = 100;
            this.nudCopias.Value = 1;
            this.nudCopias.Font = new Font("Segoe UI", 11F);

            this.btnImprimir.Text = "🖨️ Imprimir";
            this.btnImprimir.Location = new Point(10, 82);
            this.btnImprimir.Size = new Size(178, 40);
            this.btnImprimir.BackColor = Color.FromArgb(52, 152, 219);
            this.btnImprimir.ForeColor = Color.White;
            this.btnImprimir.FlatStyle = FlatStyle.Flat;
            this.btnImprimir.FlatAppearance.BorderSize = 0;
            this.btnImprimir.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnImprimir.Cursor = Cursors.Hand;
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);

            this.btnRegenerarCodigo.Text = "🔄 Regenerar código";
            this.btnRegenerarCodigo.Location = new Point(10, 135);
            this.btnRegenerarCodigo.Size = new Size(178, 38);
            this.btnRegenerarCodigo.BackColor = Color.FromArgb(142, 68, 173);
            this.btnRegenerarCodigo.ForeColor = Color.White;
            this.btnRegenerarCodigo.FlatStyle = FlatStyle.Flat;
            this.btnRegenerarCodigo.FlatAppearance.BorderSize = 0;
            this.btnRegenerarCodigo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.btnRegenerarCodigo.Cursor = Cursors.Hand;
            this.btnRegenerarCodigo.Click += new System.EventHandler(this.btnRegenerarCodigo_Click);

            this.btnCerrar.Text = "✗ Cerrar";
            this.btnCerrar.Location = new Point(10, 182);
            this.btnCerrar.Size = new Size(178, 38);
            this.btnCerrar.BackColor = Color.FromArgb(149, 165, 166);
            this.btnCerrar.ForeColor = Color.White;
            this.btnCerrar.FlatStyle = FlatStyle.Flat;
            this.btnCerrar.FlatAppearance.BorderSize = 0;
            this.btnCerrar.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.btnCerrar.Cursor = Cursors.Hand;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);

            // ── lblEstadoCodigo ───────────────────────────────────────────────
            this.lblEstadoCodigo.AutoSize = true;
            this.lblEstadoCodigo.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            this.lblEstadoCodigo.ForeColor = Color.FromArgb(46, 204, 113);
            this.lblEstadoCodigo.Location = new Point(20, 292);
            this.lblEstadoCodigo.Text = "✅ Código EAN-13 válido";

            // ── FrmEtiquetaProducto ───────────────────────────────────────────
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = Color.FromArgb(236, 240, 241);
            this.ClientSize = new Size(595, 320);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Text = "Etiqueta de Producto";
            this.Controls.AddRange(new Control[] {
                this.lblTitulo, this.panelEtiqueta,
                this.panelOpciones, this.lblEstadoCodigo });
            this.Load += new System.EventHandler(this.FrmEtiquetaProducto_Load);

            ((ISupportInitialize)this.picBarcode).EndInit();
            ((ISupportInitialize)this.nudCopias).EndInit();
            this.panelEtiqueta.ResumeLayout(false);
            this.panelEtiqueta.PerformLayout();
            this.panelOpciones.ResumeLayout(false);
            this.panelOpciones.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion

        private Label lblTitulo;
        private Panel panelEtiqueta;
        private Label lblNombreProducto;
        private Label lblPrecioProducto;
        private PictureBox picBarcode;
        private Label lblCodigoNum;
        private Label lblEstadoCodigo;
        private Panel panelOpciones;
        private Label lblCopiasLbl;
        private NumericUpDown nudCopias;
        private Button btnImprimir;
        private Button btnRegenerarCodigo;
        private Button btnCerrar;
    }
}