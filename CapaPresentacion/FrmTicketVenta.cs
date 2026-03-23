using System;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using CapaNegocio;

namespace CapaPresentacion
{
    /// <summary>
    /// TICKET DE VENTA
    /// Muestra el resumen de la venta confirmada con código de barras
    /// de cada producto. Permite imprimir o cerrar.
    ///
    /// USO: Abrir después de confirmar una venta en FrmVentas:
    ///   FrmTicketVenta ticket = new FrmTicketVenta(idventa);
    ///   ticket.ShowDialog(this);
    /// </summary>
    public partial class FrmTicketVenta : Form
    {
        private readonly int _idVenta;
        private DataTable _detalle;
        private DataTable _cabecera;

        public FrmTicketVenta(int idVenta)
        {
            InitializeComponent();
            _idVenta = idVenta;
        }

        private void FrmTicketVenta_Load(object sender, EventArgs e)
        {
            CargarDatos();
        }

        // ── Cargar datos de la venta ──────────────────────────────────────────
        private void CargarDatos()
        {
            try
            {
                // Detalle de la venta
                _detalle = CN_Venta.ObtenerDetalle(_idVenta);

                // Cabecera (fecha, cliente, cajero, totales)
                _cabecera = ObtenerCabecera(_idVenta);

                if (_cabecera == null || _cabecera.Rows.Count == 0)
                {
                    MessageBox.Show("No se encontraron datos de la venta.",
                        "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DataRow cab = _cabecera.Rows[0];

                // Encabezado del ticket
                lblVentaId.Text = $"Venta # {_idVenta}";
                lblFecha.Text = $"Fecha: {Convert.ToDateTime(cab["fecha"]):dd/MM/yyyy HH:mm}";
                lblCliente.Text = $"Cliente: {cab["cliente"]}";
                lblCajero.Text = $"Cajero:  {cab["cajero"]}";

                // Totales
                lblSubtotal.Text = $"Subtotal:  ${Convert.ToDecimal(cab["subtotal"]):N2}";
                lblIva.Text = $"IVA (16%): ${Convert.ToDecimal(cab["iva"]):N2}";
                lblTotal.Text = $"TOTAL:     ${Convert.ToDecimal(cab["total"]):N2}";

                // Renderizar productos con sus códigos de barras
                RenderizarProductos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar ticket: " + ex.Message,
                    "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ── Obtener cabecera de la venta ──────────────────────────────────────
        private DataTable ObtenerCabecera(int idventa)
        {
            // Reutiliza CN_Venta.Listar con rango amplio para encontrar la venta
            DataTable todas = CN_Venta.Listar(
                DateTime.Now.AddYears(-1), DateTime.Now, "TODAS");

            DataTable resultado = todas.Clone();
            foreach (DataRow row in todas.Rows)
                if (Convert.ToInt32(row["idventa"]) == idventa)
                    resultado.ImportRow(row);

            return resultado;
        }

        // ── Renderizar panel de productos con imagen de código de barras ──────
        private void RenderizarProductos()
        {
            panelProductos.Controls.Clear();

            if (_detalle == null || _detalle.Rows.Count == 0)
            {
                Label lblVacio = new Label
                {
                    Text = "Sin productos",
                    Location = new Point(10, 10),
                    AutoSize = true,
                    ForeColor = Color.Gray
                };
                panelProductos.Controls.Add(lblVacio);
                return;
            }

            int yPos = 5;

            foreach (DataRow row in _detalle.Rows)
            {
                string nombre = row["producto"].ToString();
                int cantidad = Convert.ToInt32(row["cantidad"]);
                decimal precioUnit = Convert.ToDecimal(row["precio_unit"]);
                decimal subtotal = Convert.ToDecimal(row["subtotal"]);
                int idprod = Convert.ToInt32(row["idproducto"]);

                // Obtener código de barras del producto
                string codigo = ObtenerCodigoBarras(idprod);

                // ── Panel por producto ─────────────────────────────────────────
                Panel pnlItem = new Panel
                {
                    Location = new Point(5, yPos),
                    Size = new Size(panelProductos.Width - 25, 100),
                    BackColor = Color.White,
                    BorderStyle = BorderStyle.FixedSingle
                };

                // Nombre + cantidad + precio
                Label lblNombre = new Label
                {
                    Text = $"{nombre}",
                    Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(52, 73, 94),
                    Location = new Point(8, 6),
                    Size = new Size(250, 18)
                };

                Label lblDetalle = new Label
                {
                    Text = $"{cantidad} x ${precioUnit:N2} = ${subtotal:N2}",
                    Font = new Font("Segoe UI", 8.5F),
                    ForeColor = Color.FromArgb(52, 152, 219),
                    Location = new Point(8, 26),
                    Size = new Size(250, 16)
                };

                // Imagen del código de barras
                PictureBox picItem = new PictureBox
                {
                    Location = new Point(8, 44),
                    Size = new Size(200, 45),
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    BackColor = Color.White,
                    BorderStyle = BorderStyle.None
                };

                Label lblCodItem = new Label
                {
                    Font = new Font("Courier New", 7.5F),
                    ForeColor = Color.FromArgb(100, 100, 100),
                    Location = new Point(8, 90),
                    Size = new Size(200, 14),
                    TextAlign = ContentAlignment.MiddleCenter
                };

                if (EAN13Util.EsValido(codigo))
                {
                    try
                    {
                        picItem.Image = EAN13Util.GenerarImagen(codigo, 200, 45, false);
                        lblCodItem.Text = codigo;
                    }
                    catch
                    {
                        lblCodItem.Text = "Sin código";
                    }
                }
                else
                {
                    lblCodItem.Text = "Sin código de barras";
                    lblCodItem.ForeColor = Color.Gray;
                }

                pnlItem.Controls.AddRange(new Control[] {
                    lblNombre, lblDetalle, picItem, lblCodItem });

                panelProductos.Controls.Add(pnlItem);
                yPos += 110;
            }

            // Ajustar alto del panel para scroll
            panelProductos.AutoScrollMinSize = new Size(0, yPos + 10);
        }

        // ── Obtener código de barras de un producto ───────────────────────────
        private string ObtenerCodigoBarras(int idproducto)
        {
            try
            {
                // Buscar en la BD con el SP de búsqueda inversa
                // Como no hay SP directo para obtener por ID, calculamos el EAN-13
                return EAN13Util.Generar(idproducto);
            }
            catch { return ""; }
        }

        // ── Imprimir ticket ───────────────────────────────────────────────────
        private void btnImprimir_Click(object sender, EventArgs e)
        {
            PrintDocument pd = new PrintDocument();
            pd.DefaultPageSettings.PaperSize =
                new PaperSize("Ticket", 315, 1000); // 8cm de ancho, alto dinámico
            pd.PrintPage += ImprimirTicket;

            PrintPreviewDialog preview = new PrintPreviewDialog
            {
                Document = pd,
                Width = 500,
                Height = 700,
                StartPosition = FormStartPosition.CenterParent,
                Text = $"Ticket Venta #{_idVenta}"
            };

            try { preview.ShowDialog(this); }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message,
                    "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ImprimirTicket(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            int margen = 10;
            int ancho = e.PageBounds.Width - margen * 2;
            int y = margen;

            Font fntTitulo = new Font("Segoe UI", 12F, FontStyle.Bold);
            Font fntNormal = new Font("Segoe UI", 8F);
            Font fntBold = new Font("Segoe UI", 8F, FontStyle.Bold);
            Font fntCodigo = new Font("Courier New", 7F);
            Font fntTotal = new Font("Segoe UI", 10F, FontStyle.Bold);

            // ── Encabezado ────────────────────────────────────────────────────
            g.DrawString("VETERINARIA", fntTitulo, Brushes.Black,
                margen + (ancho - g.MeasureString("VETERINARIA", fntTitulo).Width) / 2, y);
            y += 20;

            g.DrawLine(Pens.Black, margen, y, margen + ancho, y); y += 5;

            g.DrawString($"Venta #: {_idVenta}", fntNormal, Brushes.Black, margen, y); y += 14;
            if (_cabecera?.Rows.Count > 0)
            {
                DataRow cab = _cabecera.Rows[0];
                g.DrawString($"Fecha:   {Convert.ToDateTime(cab["fecha"]):dd/MM/yyyy HH:mm}", fntNormal, Brushes.Black, margen, y); y += 14;
                g.DrawString($"Cliente: {cab["cliente"]}", fntNormal, Brushes.Black, margen, y); y += 14;
                g.DrawString($"Cajero:  {cab["cajero"]}", fntNormal, Brushes.Black, margen, y); y += 14;
            }

            g.DrawLine(Pens.Black, margen, y, margen + ancho, y); y += 5;

            // ── Productos ─────────────────────────────────────────────────────
            g.DrawString("CANT  PRODUCTO              SUBTOTAL", fntBold, Brushes.Black, margen, y); y += 14;
            g.DrawLine(Pens.Black, margen, y, margen + ancho, y); y += 4;

            if (_detalle != null)
            {
                foreach (DataRow row in _detalle.Rows)
                {
                    string nombre = row["producto"].ToString();
                    int cantidad = Convert.ToInt32(row["cantidad"]);
                    decimal subtotal = Convert.ToDecimal(row["subtotal"]);
                    int idprod = Convert.ToInt32(row["idproducto"]);

                    // Nombre recortado para ticket angosto
                    string nombreCorto = nombre.Length > 20
                        ? nombre.Substring(0, 20) : nombre.PadRight(20);

                    g.DrawString(
                        $"{cantidad,3}   {nombreCorto}  ${subtotal,7:N2}",
                        fntNormal, Brushes.Black, margen, y);
                    y += 13;

                    // Código de barras pequeño por producto
                    string codigo = EAN13Util.Generar(idprod);
                    if (EAN13Util.EsValido(codigo))
                    {
                        try
                        {
                            Bitmap bmp = EAN13Util.GenerarImagen(codigo, ancho, 35, false);
                            g.DrawImage(bmp, margen, y, ancho, 35);
                            y += 36;
                            g.DrawString(codigo, fntCodigo, Brushes.Black,
                                margen + (ancho - g.MeasureString(codigo, fntCodigo).Width) / 2, y);
                            y += 12;
                            bmp.Dispose();
                        }
                        catch { y += 5; }
                    }

                    y += 3; // separación entre productos
                }
            }

            g.DrawLine(Pens.Black, margen, y, margen + ancho, y); y += 5;

            // ── Totales ───────────────────────────────────────────────────────
            if (_cabecera?.Rows.Count > 0)
            {
                DataRow cab = _cabecera.Rows[0];
                g.DrawString($"Subtotal: ${Convert.ToDecimal(cab["subtotal"]):N2}", fntNormal, Brushes.Black, margen, y); y += 14;
                g.DrawString($"IVA 16%:  ${Convert.ToDecimal(cab["iva"]):N2}", fntNormal, Brushes.Black, margen, y); y += 14;
                g.DrawString($"TOTAL:    ${Convert.ToDecimal(cab["total"]):N2}", fntTotal, Brushes.Black, margen, y); y += 20;
            }

            g.DrawLine(Pens.Black, margen, y, margen + ancho, y); y += 5;
            g.DrawString("¡Gracias por su preferencia!", fntNormal, Brushes.Black,
                margen + (ancho - g.MeasureString("¡Gracias por su preferencia!", fntNormal).Width) / 2, y);

            // Liberar fuentes
            fntTitulo.Dispose(); fntNormal.Dispose(); fntBold.Dispose();
            fntCodigo.Dispose(); fntTotal.Dispose();

            e.HasMorePages = false;
        }

        private void btnCerrar_Click(object sender, EventArgs e) => this.Close();
    }
}