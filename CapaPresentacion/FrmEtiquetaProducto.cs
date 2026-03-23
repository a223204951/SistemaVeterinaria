using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using CapaNegocio;

namespace CapaPresentacion
{
    /// <summary>
    /// FORMULARIO DE ETIQUETA DE PRODUCTO
    /// Muestra el código de barras EAN-13 con preview de impresión.
    /// Permite imprimir 1 etiqueta o múltiples copias.
    /// </summary>
    public partial class FrmEtiquetaProducto : Form
    {
        private readonly int _idProducto;
        private readonly string _nombre;
        private readonly decimal _precio;
        private readonly string _codigo;
        private int _copias = 1;

        public FrmEtiquetaProducto(int idProducto, string nombre, decimal precio, string codigo)
        {
            InitializeComponent();
            _idProducto = idProducto;
            _nombre = nombre;
            _precio = precio;
            _codigo = codigo;
        }

        private void FrmEtiquetaProducto_Load(object sender, EventArgs e)
        {
            lblNombreProducto.Text = _nombre;
            lblPrecioProducto.Text = $"${_precio:N2} MXN";
            lblCodigoNum.Text = _codigo;

            if (EAN13Util.EsValido(_codigo))
            {
                try
                {
                    picBarcode.Image = EAN13Util.GenerarImagen(
                        _codigo, picBarcode.Width, picBarcode.Height, mostrarNumero: false);
                    lblEstadoCodigo.Text = "✅ Código EAN-13 válido";
                    lblEstadoCodigo.ForeColor = Color.FromArgb(46, 204, 113);
                }
                catch (Exception ex)
                {
                    lblEstadoCodigo.Text = "❌ Error al renderizar: " + ex.Message;
                    lblEstadoCodigo.ForeColor = Color.Red;
                }
            }
            else
            {
                lblEstadoCodigo.Text = "⚠️ Código de barras no disponible";
                lblEstadoCodigo.ForeColor = Color.FromArgb(230, 126, 34);
                btnImprimir.Enabled = false;
            }

            nudCopias.Value = 1;
        }

        // ── Imprimir ──────────────────────────────────────────────────────────
        private void btnImprimir_Click(object sender, EventArgs e)
        {
            _copias = Convert.ToInt32(nudCopias.Value);

            PrintDocument pd = new PrintDocument();
            pd.DefaultPageSettings.PaperSize =
                new PaperSize("Etiqueta", 315, 189); // 11cm x 6.6cm en centésimas de pulgada
            pd.DefaultPageSettings.Landscape = false;
            pd.PrintPage += PrintPage;

            PrintPreviewDialog preview = new PrintPreviewDialog
            {
                Document = pd,
                Width = 700,
                Height = 500,
                StartPosition = FormStartPosition.CenterParent,
                Text = "Vista previa de etiqueta"
            };

            try { preview.ShowDialog(this); }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar vista previa: " + ex.Message,
                    "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            int margen = 15;
            int yPos = margen;
            int ancho = e.PageBounds.Width - margen * 2;

            // ── Nombre del producto ───────────────────────────────────────────
            Font fntNombre = new Font("Segoe UI", 10F, FontStyle.Bold);
            string nombreRecortado = _nombre.Length > 28
                ? _nombre.Substring(0, 28) + "…" : _nombre;

            g.DrawString(nombreRecortado, fntNombre, Brushes.Black,
                new RectangleF(margen, yPos, ancho, 22));
            yPos += 24;

            // ── Precio ────────────────────────────────────────────────────────
            Font fntPrecio = new Font("Segoe UI", 14F, FontStyle.Bold);
            g.DrawString($"${_precio:N2}", fntPrecio, Brushes.Black, margen, yPos);
            yPos += 28;

            // ── Imagen código de barras ───────────────────────────────────────
            int altoBarra = 70;
            Bitmap bmp = EAN13Util.GenerarImagen(_codigo, ancho, altoBarra, mostrarNumero: false);
            g.DrawImage(bmp, margen, yPos, ancho, altoBarra);
            yPos += altoBarra + 2;

            // ── Número EAN-13 ─────────────────────────────────────────────────
            Font fntCod = new Font("Courier New", 9F);
            SizeF tamCod = g.MeasureString(_codigo, fntCod);
            g.DrawString(_codigo, fntCod, Brushes.Black,
                margen + (ancho - tamCod.Width) / 2, yPos);

            // Liberar
            fntNombre.Dispose(); fntPrecio.Dispose(); fntCod.Dispose(); bmp.Dispose();

            // ── Múltiples copias: repetir en la misma hoja si caben ───────────
            e.HasMorePages = false; // PrintDocument maneja las copias
        }

        private void btnCerrar_Click(object sender, EventArgs e) => this.Close();

        // ── Regenerar código si no existe ─────────────────────────────────────
        private void btnRegenerarCodigo_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Generar un nuevo código de barras para este producto?",
                    "Sistema Veterinaria", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                != DialogResult.Yes) return;

            string nuevo = CN_Producto.RegenerarCodigoBarras(_idProducto);

            if (EAN13Util.EsValido(nuevo))
            {
                picBarcode.Image = EAN13Util.GenerarImagen(nuevo,
                    picBarcode.Width, picBarcode.Height, mostrarNumero: false);
                lblCodigoNum.Text = nuevo;
                lblEstadoCodigo.Text = "✅ Nuevo código generado";
                lblEstadoCodigo.ForeColor = Color.FromArgb(46, 204, 113);
                btnImprimir.Enabled = true;

                MessageBox.Show($"✅ Código generado:\n{nuevo}",
                    "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("❌ " + nuevo,
                    "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}