using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using CapaNegocio;

namespace CapaPresentacion
{
    /// <summary>
    /// FORMULARIO PARA VER EL HISTORIAL DE CAMBIOS DE PRECIO DE UN PRODUCTO
    /// Muestra todos los ajustes de precio realizados por el sistema dinámico
    /// </summary>
    public partial class FrmHistorialPrecios : Form
    {
        private int idProducto;
        private string nombreProducto;

        /// <summary>
        /// CONSTRUCTOR CON PARÁMETROS
        /// </summary>
        public FrmHistorialPrecios(int idProducto, string nombreProducto)
        {
            InitializeComponent();
            this.idProducto = idProducto;
            this.nombreProducto = nombreProducto;
        }

        /// <summary>
        /// EVENTO LOAD DEL FORMULARIO
        /// </summary>
        private void FrmHistorialPrecios_Load(object sender, EventArgs e)
        {
            // CONFIGURAR TÍTULO
            lblTitulo.Text = $"📊 Historial de Precios - {nombreProducto}";

            // CARGAR HISTORIAL
            CargarHistorial();

            // CONFIGURAR DATAGRIDVIEW
            ConfigurarColumnas();
        }

        /// <summary>
        /// MÉTODO PARA CARGAR EL HISTORIAL DE PRECIOS
        /// </summary>
        private void CargarHistorial()
        {
            try
            {
                DataTable historial = CN_Producto.ObtenerHistorialPrecios(idProducto);

                if (historial != null && historial.Rows.Count > 0)
                {
                    dgvHistorial.DataSource = historial;
                    lblTotal.Text = $"Total de cambios registrados: {historial.Rows.Count}";

                    // CALCULAR ESTADÍSTICAS
                    CalcularEstadisticas(historial);
                }
                else
                {
                    lblTotal.Text = "No hay historial de cambios de precio para este producto";
                    lblEstadisticas.Text = "💡 El historial se generará automáticamente cuando el producto sea vendido";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar historial: " + ex.Message,
                    "Sistema Veterinaria",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// MÉTODO PARA CONFIGURAR COLUMNAS DEL DATAGRIDVIEW
        /// </summary>
        private void ConfigurarColumnas()
        {
            if (dgvHistorial.Columns.Count > 0)
            {
                // OCULTAR COLUMNA ID
                if (dgvHistorial.Columns.Contains("idhistorial"))
                    dgvHistorial.Columns["idhistorial"].Visible = false;

                // RENOMBRAR ENCABEZADOS
                if (dgvHistorial.Columns.Contains("precio_anterior"))
                {
                    dgvHistorial.Columns["precio_anterior"].HeaderText = "Precio Anterior";
                    dgvHistorial.Columns["precio_anterior"].DefaultCellStyle.Format = "C2";
                }
                if (dgvHistorial.Columns.Contains("precio_nuevo"))
                {
                    dgvHistorial.Columns["precio_nuevo"].HeaderText = "Precio Nuevo";
                    dgvHistorial.Columns["precio_nuevo"].DefaultCellStyle.Format = "C2";
                }
                if (dgvHistorial.Columns.Contains("motivo"))
                    dgvHistorial.Columns["motivo"].HeaderText = "Motivo";
                if (dgvHistorial.Columns.Contains("fecha"))
                {
                    dgvHistorial.Columns["fecha"].HeaderText = "Fecha y Hora";
                    dgvHistorial.Columns["fecha"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss";
                }
                if (dgvHistorial.Columns.Contains("porcentaje_cambio"))
                {
                    dgvHistorial.Columns["porcentaje_cambio"].HeaderText = "Cambio %";
                    dgvHistorial.Columns["porcentaje_cambio"].DefaultCellStyle.Format = "0.00'%'";
                }

                // AJUSTAR ANCHOS
                dgvHistorial.Columns["precio_anterior"].Width = 120;
                dgvHistorial.Columns["precio_nuevo"].Width = 120;
                dgvHistorial.Columns["motivo"].Width = 150;
                dgvHistorial.Columns["porcentaje_cambio"].Width = 100;

                // APLICAR COLORES SEGÚN EL MOTIVO Y CAMBIO
                AplicarColores();
            }
        }

        /// <summary>
        /// MÉTODO PARA APLICAR COLORES SEGÚN EL TIPO DE CAMBIO
        /// </summary>
        private void AplicarColores()
        {
            foreach (DataGridViewRow row in dgvHistorial.Rows)
            {
                if (row.Cells["motivo"].Value != null)
                {
                    string motivo = row.Cells["motivo"].Value.ToString();

                    // COLOREAR SEGÚN EL MOTIVO
                    switch (motivo)
                    {
                        case "VENTA":
                            row.DefaultCellStyle.BackColor = Color.FromArgb(232, 248, 245);
                            row.Cells["motivo"].Style.ForeColor = Color.FromArgb(22, 160, 133);
                            row.Cells["motivo"].Value = "✅ VENTA";
                            break;
                        case "NO_VENTA":
                            row.DefaultCellStyle.BackColor = Color.FromArgb(255, 230, 230);
                            row.Cells["motivo"].Style.ForeColor = Color.FromArgb(192, 57, 43);
                            row.Cells["motivo"].Value = "⬇️ NO VENTA";
                            break;
                        case "COMPRA_MULTIPLE":
                            row.DefaultCellStyle.BackColor = Color.FromArgb(240, 230, 255);
                            row.Cells["motivo"].Style.ForeColor = Color.FromArgb(142, 68, 173);
                            row.Cells["motivo"].Value = "🛒 COMPRA MÚLTIPLE";
                            break;
                    }
                }

                // COLOREAR PORCENTAJE DE CAMBIO
                if (row.Cells["porcentaje_cambio"].Value != null)
                {
                    decimal cambio = Convert.ToDecimal(row.Cells["porcentaje_cambio"].Value);

                    if (cambio > 0)
                        row.Cells["porcentaje_cambio"].Style.ForeColor = Color.FromArgb(46, 204, 113);
                    else if (cambio < 0)
                        row.Cells["porcentaje_cambio"].Style.ForeColor = Color.FromArgb(231, 76, 60);
                }
            }
        }

        /// <summary>
        /// MÉTODO PARA CALCULAR ESTADÍSTICAS DEL HISTORIAL
        /// </summary>
        private void CalcularEstadisticas(DataTable historial)
        {
            if (historial.Rows.Count == 0)
                return;

            int totalCambios = historial.Rows.Count;
            int aumentos = 0;
            int disminuciones = 0;
            decimal precioInicial = 0;
            decimal precioActual = 0;
            decimal mayorPrecio = 0;
            decimal menorPrecio = decimal.MaxValue;

            foreach (DataRow row in historial.Rows)
            {
                decimal cambio = Convert.ToDecimal(row["porcentaje_cambio"]);
                decimal precioNuevo = Convert.ToDecimal(row["precio_nuevo"]);

                if (cambio > 0)
                    aumentos++;
                else if (cambio < 0)
                    disminuciones++;

                if (precioNuevo > mayorPrecio)
                    mayorPrecio = precioNuevo;

                if (precioNuevo < menorPrecio)
                    menorPrecio = precioNuevo;
            }

            // EL PRECIO INICIAL ES EL PRECIO ANTERIOR DEL PRIMER CAMBIO
            precioInicial = Convert.ToDecimal(historial.Rows[historial.Rows.Count - 1]["precio_anterior"]);

            // EL PRECIO ACTUAL ES EL PRECIO NUEVO DEL ÚLTIMO CAMBIO
            precioActual = Convert.ToDecimal(historial.Rows[0]["precio_nuevo"]);

            // CALCULAR CAMBIO TOTAL
            decimal cambioTotal = ((precioActual - precioInicial) / precioInicial) * 100;

            // MOSTRAR ESTADÍSTICAS
            lblEstadisticas.Text = $"📊 ESTADÍSTICAS:\n\n" +
                $"🔢 Total de cambios: {totalCambios}\n" +
                $"⬆️ Aumentos: {aumentos}\n" +
                $"⬇️ Disminuciones: {disminuciones}\n\n" +
                $"💰 Precio inicial: ${precioInicial:N2} MXN\n" +
                $"💰 Precio actual: ${precioActual:N2} MXN\n" +
                $"📈 Cambio total: {cambioTotal:N2}%\n\n" +
                $"🔝 Precio más alto: ${mayorPrecio:N2} MXN\n" +
                $"🔻 Precio más bajo: ${menorPrecio:N2} MXN";

            // COLOREAR EL CAMBIO TOTAL
            if (cambioTotal > 0)
                lblEstadisticas.ForeColor = Color.FromArgb(46, 204, 113);
            else if (cambioTotal < 0)
                lblEstadisticas.ForeColor = Color.FromArgb(231, 76, 60);
            else
                lblEstadisticas.ForeColor = Color.FromArgb(52, 73, 94);
        }

        /// <summary>
        /// EVENTO CLICK DEL BOTÓN CERRAR
        /// </summary>
        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// EVENTO CLICK DEL BOTÓN EXPORTAR
        /// </summary>
        private void btnExportar_Click(object sender, EventArgs e)
        {
            try
            {
                // CREAR SAVEFILEDIALOG
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Archivo CSV|*.csv|Archivo de texto|*.txt";
                saveFileDialog.Title = "Exportar Historial de Precios";
                saveFileDialog.FileName = $"Historial_{nombreProducto.Replace(" ", "_")}_{DateTime.Now:yyyyMMdd}.csv";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // CREAR ARCHIVO CSV
                    using (System.IO.StreamWriter sw = new System.IO.StreamWriter(saveFileDialog.FileName))
                    {
                        // ESCRIBIR ENCABEZADOS
                        sw.WriteLine($"Historial de Precios - {nombreProducto}");
                        sw.WriteLine($"Generado: {DateTime.Now:dd/MM/yyyy HH:mm:ss}");
                        sw.WriteLine();
                        sw.WriteLine("Precio Anterior,Precio Nuevo,Cambio %,Motivo,Fecha");

                        // ESCRIBIR DATOS
                        foreach (DataGridViewRow row in dgvHistorial.Rows)
                        {
                            if (!row.IsNewRow)
                            {
                                string precioAnterior = row.Cells["precio_anterior"].Value.ToString();
                                string precioNuevo = row.Cells["precio_nuevo"].Value.ToString();
                                string cambio = row.Cells["porcentaje_cambio"].Value.ToString();
                                string motivo = row.Cells["motivo"].Value.ToString();
                                string fecha = row.Cells["fecha"].Value.ToString();

                                sw.WriteLine($"{precioAnterior},{precioNuevo},{cambio},{motivo},{fecha}");
                            }
                        }
                    }

                    MessageBox.Show("✅ Historial exportado correctamente\n\n" +
                        $"Ubicación: {saveFileDialog.FileName}",
                        "Sistema Veterinaria",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al exportar: " + ex.Message,
                    "Sistema Veterinaria",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}