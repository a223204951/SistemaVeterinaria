using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using CapaNegocio;

namespace CapaPresentacion
{
    /// <summary>
    /// HISTORIAL DE COMPRAS POR PROVEEDOR
    /// Muestra todas las compras realizadas a un proveedor con sus totales.
    /// Equivalente a FrmHistorialPrecios pero para compras.
    /// </summary>
    public partial class FrmHistorialComprasProveedor : Form
    {
        private readonly int _idProveedor;
        private readonly string _nombreProveedor;

        public FrmHistorialComprasProveedor(int idProveedor, string nombreProveedor)
        {
            InitializeComponent();
            _idProveedor = idProveedor;
            _nombreProveedor = nombreProveedor;
        }

        private void FrmHistorialComprasProveedor_Load(object sender, EventArgs e)
        {
            lblNombreProveedor.Text = $"Proveedor: {_nombreProveedor}";
            CargarHistorial();
        }

        private void CargarHistorial()
        {
            try
            {
                DataTable dt = CN_Proveedor.HistorialCompras(_idProveedor);

                // Garantizar orden por fecha descendente en C# (más reciente primero)
                dt.DefaultView.Sort = "idcompra DESC";
                dgvHistorial.DataSource = dt.DefaultView.ToTable();

                ConfigurarColumnas();
                CalcularResumen(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar historial: " + ex.Message,
                    "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurarColumnas()
        {
            if (dgvHistorial.Columns.Count == 0) return;

            if (dgvHistorial.Columns.Contains("idcompra"))
            {
                dgvHistorial.Columns["idcompra"].HeaderText = "# Compra";
                dgvHistorial.Columns["idcompra"].Width = 80;
                dgvHistorial.Columns["idcompra"].HeaderCell.SortGlyphDirection = SortOrder.Descending;
            }
            if (dgvHistorial.Columns.Contains("fecha"))
            {
                dgvHistorial.Columns["fecha"].HeaderText = "Fecha";
                dgvHistorial.Columns["fecha"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
            }
            if (dgvHistorial.Columns.Contains("comprador")) dgvHistorial.Columns["comprador"].HeaderText = "Registró";
            if (dgvHistorial.Columns.Contains("subtotal")) { dgvHistorial.Columns["subtotal"].HeaderText = "Subtotal"; dgvHistorial.Columns["subtotal"].DefaultCellStyle.Format = "C2"; }
            if (dgvHistorial.Columns.Contains("iva")) { dgvHistorial.Columns["iva"].HeaderText = "IVA"; dgvHistorial.Columns["iva"].DefaultCellStyle.Format = "C2"; }
            if (dgvHistorial.Columns.Contains("total")) { dgvHistorial.Columns["total"].HeaderText = "Total"; dgvHistorial.Columns["total"].DefaultCellStyle.Format = "C2"; }
            if (dgvHistorial.Columns.Contains("num_productos")) { dgvHistorial.Columns["num_productos"].HeaderText = "Productos"; dgvHistorial.Columns["num_productos"].Width = 85; }
        }

        private void CalcularResumen(DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0)
            {
                lblResumen.Text = "Sin compras registradas para este proveedor.";
                return;
            }

            decimal totalAcumulado = 0;
            foreach (DataRow row in dt.Rows)
                if (row["total"] != DBNull.Value)
                    totalAcumulado += Convert.ToDecimal(row["total"]);

            lblResumen.Text = $"Total de compras: {dt.Rows.Count}  |  Monto total acumulado: {totalAcumulado:C2}";
        }

        private void btnCerrar_Click(object sender, EventArgs e)
            => this.Close();
    }
}