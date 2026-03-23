using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace CapaPresentacion
{
    partial class FrmTicketVenta
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
            this.panelCabecera = new Panel();
            this.lblVentaId = new Label();
            this.lblFecha = new Label();
            this.lblCliente = new Label();
            this.lblCajero = new Label();
            this.lblProductosTitulo = new Label();
            this.panelProductos = new Panel();
            this.panelTotales = new Panel();
            this.lblSubtotal = new Label();
            this.lblIva = new Label();
            this.lblTotal = new Label();
            this.btnImprimir = new Button();
            this.btnCerrar = new Button();

            this.panelCabecera.SuspendLayout();
            this.panelTotales.SuspendLayout();
            this.SuspendLayout();

            // lblTitulo
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            this.lblTitulo.ForeColor = Color.FromArgb(52, 73, 94);
            this.lblTitulo.Location = new Point(20, 15);
            this.lblTitulo.Text = "Ticket de Venta";

            // panelCabecera
            this.panelCabecera.BackColor = Color.White;
            this.panelCabecera.BorderStyle = BorderStyle.FixedSingle;
            this.panelCabecera.Location = new Point(15, 55);
            this.panelCabecera.Size = new Size(370, 95);
            this.panelCabecera.Controls.AddRange(new Control[] {
                this.lblVentaId, this.lblFecha, this.lblCliente, this.lblCajero });

            this.lblVentaId.Text = "Venta # -";
            this.lblVentaId.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            this.lblVentaId.ForeColor = Color.FromArgb(52, 152, 219);
            this.lblVentaId.Location = new Point(10, 8);
            this.lblVentaId.AutoSize = true;

            this.lblFecha.Text = "Fecha: -";
            this.lblFecha.Font = new Font("Segoe UI", 9F);
            this.lblFecha.ForeColor = Color.FromArgb(52, 73, 94);
            this.lblFecha.Location = new Point(10, 35);
            this.lblFecha.AutoSize = true;

            this.lblCliente.Text = "Cliente: -";
            this.lblCliente.Font = new Font("Segoe UI", 9F);
            this.lblCliente.ForeColor = Color.FromArgb(52, 73, 94);
            this.lblCliente.Location = new Point(10, 55);
            this.lblCliente.AutoSize = true;

            this.lblCajero.Text = "Cajero: -";
            this.lblCajero.Font = new Font("Segoe UI", 9F);
            this.lblCajero.ForeColor = Color.FromArgb(52, 73, 94);
            this.lblCajero.Location = new Point(210, 55);
            this.lblCajero.AutoSize = true;

            // lblProductosTitulo
            this.lblProductosTitulo.Text = "PRODUCTOS";
            this.lblProductosTitulo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblProductosTitulo.ForeColor = Color.FromArgb(149, 165, 166);
            this.lblProductosTitulo.Location = new Point(15, 158);
            this.lblProductosTitulo.AutoSize = true;

            // panelProductos
            this.panelProductos.BackColor = Color.FromArgb(248, 249, 250);
            this.panelProductos.BorderStyle = BorderStyle.FixedSingle;
            this.panelProductos.Location = new Point(15, 175);
            this.panelProductos.Size = new Size(370, 295);
            this.panelProductos.AutoScroll = true;
            this.panelProductos.AutoScrollMinSize = new Size(340, 0);

            // panelTotales
            this.panelTotales.BackColor = Color.White;
            this.panelTotales.BorderStyle = BorderStyle.FixedSingle;
            this.panelTotales.Location = new Point(15, 478);
            this.panelTotales.Size = new Size(370, 85);
            this.panelTotales.Controls.AddRange(new Control[] {
                this.lblSubtotal, this.lblIva, this.lblTotal });

            this.lblSubtotal.Text = "Subtotal:  $0.00";
            this.lblSubtotal.Font = new Font("Segoe UI", 9F);
            this.lblSubtotal.ForeColor = Color.FromArgb(52, 73, 94);
            this.lblSubtotal.Location = new Point(10, 8);
            this.lblSubtotal.AutoSize = true;

            this.lblIva.Text = "IVA (16%): $0.00";
            this.lblIva.Font = new Font("Segoe UI", 9F);
            this.lblIva.ForeColor = Color.FromArgb(52, 73, 94);
            this.lblIva.Location = new Point(10, 30);
            this.lblIva.AutoSize = true;

            this.lblTotal.Text = "TOTAL:     $0.00";
            this.lblTotal.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            this.lblTotal.ForeColor = Color.FromArgb(46, 204, 113);
            this.lblTotal.Location = new Point(10, 52);
            this.lblTotal.AutoSize = true;

            // btnImprimir
            this.btnImprimir.Text = "Imprimir Ticket";
            this.btnImprimir.Location = new Point(15, 575);
            this.btnImprimir.Size = new Size(180, 42);
            this.btnImprimir.BackColor = Color.FromArgb(52, 152, 219);
            this.btnImprimir.ForeColor = Color.White;
            this.btnImprimir.FlatStyle = FlatStyle.Flat;
            this.btnImprimir.FlatAppearance.BorderSize = 0;
            this.btnImprimir.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnImprimir.Cursor = Cursors.Hand;
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);

            // btnCerrar
            this.btnCerrar.Text = "Cerrar";
            this.btnCerrar.Location = new Point(205, 575);
            this.btnCerrar.Size = new Size(180, 42);
            this.btnCerrar.BackColor = Color.FromArgb(149, 165, 166);
            this.btnCerrar.ForeColor = Color.White;
            this.btnCerrar.FlatStyle = FlatStyle.Flat;
            this.btnCerrar.FlatAppearance.BorderSize = 0;
            this.btnCerrar.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnCerrar.Cursor = Cursors.Hand;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);

            // FrmTicketVenta
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = Color.FromArgb(236, 240, 241);
            this.ClientSize = new Size(400, 630);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Text = "Ticket de Venta";
            this.Controls.AddRange(new Control[] {
                this.lblTitulo, this.panelCabecera,
                this.lblProductosTitulo,
                this.panelProductos, this.panelTotales,
                this.btnImprimir, this.btnCerrar });
            this.Load += new System.EventHandler(this.FrmTicketVenta_Load);

            this.panelCabecera.ResumeLayout(false);
            this.panelCabecera.PerformLayout();
            this.panelTotales.ResumeLayout(false);
            this.panelTotales.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion

        private Label lblTitulo;
        private Panel panelCabecera;
        private Label lblVentaId, lblFecha, lblCliente, lblCajero;
        private Label lblProductosTitulo;
        private Panel panelProductos;
        private Panel panelTotales;
        private Label lblSubtotal, lblIva, lblTotal;
        private Button btnImprimir, btnCerrar;
    }
}