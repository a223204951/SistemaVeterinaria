using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace CapaNegocio
{
    /// <summary>
    /// UTILIDAD EAN-13 — Sin dependencias externas
    /// Genera códigos EAN-13 únicos por producto y los renderiza como Bitmap.
    ///
    /// ESTRUCTURA DEL CÓDIGO:
    ///   750  = Prefijo país México (GS1 México)
    ///   0001 = Código empresa (veterinaria) — puedes cambiarlo
    ///   XXXXX = idproducto con padding de 5 dígitos
    ///   D     = Dígito verificador calculado
    /// </summary>
    public static class EAN13Util
    {
        // ── Prefijos configurables ────────────────────────────────────────────
        private const string PREFIJO_PAIS = "750";   // México GS1
        private const string PREFIJO_EMPRESA = "0001";  // Código interno veterinaria

        // =====================================================================
        // GENERACIÓN DEL CÓDIGO
        // =====================================================================

        /// <summary>
        /// Genera un código EAN-13 único basado en el idproducto.
        /// Formato: 750 + 0001 + idproducto(5 dígitos) + dígito verificador
        /// </summary>
        public static string Generar(int idproducto)
        {
            // Parte de producto: 5 dígitos con padding
            string parteProducto = idproducto.ToString().PadLeft(5, '0');
            if (parteProducto.Length > 5)
                parteProducto = parteProducto.Substring(parteProducto.Length - 5);

            // 12 dígitos sin verificador
            string base12 = PREFIJO_PAIS + PREFIJO_EMPRESA + parteProducto;

            // Calcular dígito verificador
            int check = CalcularDigitoVerificador(base12);

            return base12 + check.ToString();
        }

        /// <summary>
        /// Calcula el dígito verificador EAN-13 estándar.
        /// </summary>
        public static int CalcularDigitoVerificador(string doceDigitos)
        {
            if (doceDigitos.Length != 12)
                throw new ArgumentException("Se requieren exactamente 12 dígitos");

            int suma = 0;
            for (int i = 0; i < 12; i++)
            {
                int d = int.Parse(doceDigitos[i].ToString());
                // Posiciones pares (0-based) = ×1, impares = ×3
                suma += (i % 2 == 0) ? d : d * 3;
            }
            return (10 - (suma % 10)) % 10;
        }

        /// <summary>
        /// Valida que un string sea un EAN-13 correcto.
        /// </summary>
        public static bool EsValido(string codigo)
        {
            if (string.IsNullOrWhiteSpace(codigo) || codigo.Length != 13)
                return false;

            foreach (char c in codigo)
                if (!char.IsDigit(c)) return false;

            int checkEsperado = CalcularDigitoVerificador(codigo.Substring(0, 12));
            return int.Parse(codigo[12].ToString()) == checkEsperado;
        }

        // =====================================================================
        // RENDERIZADO COMO BITMAP (sin librerías externas)
        // =====================================================================

        // Tablas de codificación EAN-13
        private static readonly string[] CODIGOS_L = {
            "0001101","0011001","0010011","0111101","0100011",
            "0110001","0101111","0111011","0110111","0001011"
        };
        private static readonly string[] CODIGOS_G = {
            "0100111","0110011","0011011","0100001","0011101",
            "0111001","0000101","0010001","0001001","0010111"
        };
        private static readonly string[] CODIGOS_R = {
            "1110010","1100110","1101100","1000010","1011100",
            "1001110","1010000","1000100","1001000","1110100"
        };

        // Primera cifra del EAN-13 define qué paridad usar en los primeros 6 dígitos
        private static readonly string[] PARIDAD = {
            "LLLLLL","LLGLGG","LLGGLG","LLGGGL","LGLLGG",
            "LGGLLG","LGGGLL","LGLGLG","LGLGGL","LGGLGL"
        };

        private const string GUARDIA_EXTERIOR = "101";
        private const string GUARDIA_CENTRAL = "01010";

        /// <summary>
        /// Genera un Bitmap del código de barras EAN-13 listo para mostrar en PictureBox.
        /// </summary>
        /// <param name="codigo">EAN-13 de 13 dígitos</param>
        /// <param name="ancho">Ancho del bitmap en píxeles (default 280)</param>
        /// <param name="alto">Alto del bitmap en píxeles (default 120)</param>
        /// <param name="mostrarNumero">Mostrar el número debajo de las barras</param>
        public static Bitmap GenerarImagen(string codigo, int ancho = 280, int alto = 120,
            bool mostrarNumero = true)
        {
            if (!EsValido(codigo))
                throw new ArgumentException($"Código EAN-13 inválido: {codigo}");

            // Construir la secuencia binaria completa
            string bits = ConstruirBits(codigo);

            Bitmap bmp = new Bitmap(ancho, alto, PixelFormat.Format32bppArgb);
            Graphics g = Graphics.FromImage(bmp);
            g.Clear(Color.White);

            int margenIzq = 18;
            int margenDer = 10;
            int altoBarras = mostrarNumero ? alto - 22 : alto - 8;
            int anchoDisp = ancho - margenIzq - margenDer;
            double barWidth = (double)anchoDisp / bits.Length;

            Brush negro = Brushes.Black;
            Brush blanco = Brushes.White;

            // Dibujar barras
            for (int i = 0; i < bits.Length; i++)
            {
                int x = margenIzq + (int)(i * barWidth);
                int w = Math.Max(1, (int)((i + 1) * barWidth) - (int)(i * barWidth));

                // Las guardias y barras de datos de diferente alto
                bool esGuardia = i < 3 || i >= bits.Length - 3 ||
                                 (i >= 45 && i <= 49);

                int h = esGuardia ? altoBarras + 5 : altoBarras;

                if (bits[i] == '1')
                    g.FillRectangle(negro, x, 4, w, h);
            }

            // Mostrar número EAN-13 debajo
            if (mostrarNumero)
            {
                Font fnt = new Font("Courier New", 7.5f, FontStyle.Regular);
                int yTexto = altoBarras + 8;

                // Primer dígito a la izquierda de las barras
                g.DrawString(codigo[0].ToString(), fnt, Brushes.Black, 4, yTexto);

                // Dígitos 2-7 bajo el primer grupo de barras
                string grupo1 = codigo.Substring(1, 6);
                g.DrawString(grupo1, fnt, Brushes.Black,
                    margenIzq + 2, yTexto);

                // Dígitos 8-13 bajo el segundo grupo
                string grupo2 = codigo.Substring(7, 6);
                g.DrawString(grupo2, fnt, Brushes.Black,
                    margenIzq + (int)(anchoDisp * 0.5) + 2, yTexto);

                fnt.Dispose();
            }

            g.Dispose();
            return bmp;
        }

        // ── Construcción de bits EAN-13 ───────────────────────────────────────
        private static string ConstruirBits(string codigo)
        {
            int primerDigito = int.Parse(codigo[0].ToString());
            string patronParidad = PARIDAD[primerDigito];

            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            // Guardia izquierda
            sb.Append(GUARDIA_EXTERIOR);

            // 6 dígitos izquierdos (dígitos 2-7 del código)
            for (int i = 0; i < 6; i++)
            {
                int d = int.Parse(codigo[i + 1].ToString());
                sb.Append(patronParidad[i] == 'L' ? CODIGOS_L[d] : CODIGOS_G[d]);
            }

            // Guardia central
            sb.Append(GUARDIA_CENTRAL);

            // 6 dígitos derechos (dígitos 8-13 del código)
            for (int i = 0; i < 6; i++)
            {
                int d = int.Parse(codigo[i + 7].ToString());
                sb.Append(CODIGOS_R[d]);
            }

            // Guardia derecha
            sb.Append(GUARDIA_EXTERIOR);

            return sb.ToString();
        }
    }
}