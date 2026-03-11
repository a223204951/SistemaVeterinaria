using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using CapaNegocio;

namespace CapaPresentacion
{
    /// <summary>
    /// FORMULARIO DE HISTORIAL DE CAJA
    /// Muestra ventas pasadas y movimientos de stock con filtros.
    /// </summary>
    public partial class FrmHistorialVentas : Form
    {
        public FrmHistorialVentas()
        {
            InitializeComponent();
        }

        private void FrmHistorialVentas_Load(object sender, EventArgs e)
        {
            dtpInicio.Value = DateTime.Now.AddMonths(-1);
            dtpFin.Value = DateTime.Now;
            cmbEstado.SelectedIndex = 0; // TODAS
            cmbTipoMov.SelectedIndex = 0; // TODOS
            CargarVentas();
            CargarMovimientos();
        }

        // ── Cargar grids ──────────────────────────────────────────────────────
        private void CargarVentas()
        {
            try
            {
                string estado = cmbEstado.SelectedItem?.ToString() ?? "TODAS";
                DataTable dt = CN_Venta.Listar(dtpInicio.Value, dtpFin.Value, estado);
                dgvVentas.DataSource = dt;
                ConfigurarVentas();
                lblTotalVentas.Text = $"Ventas encontradas: {dgvVentas.Rows.Count}";
                ColorizarVentas();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message,
                    "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurarVentas()
        {
            if (dgvVentas.Columns.Count == 0) return;
            dgvVentas.Columns["idventa"].HeaderText = "ID";
            dgvVentas.Columns["fecha"].HeaderText = "Fecha";
            dgvVentas.Columns["cliente"].HeaderText = "Cliente";
            dgvVentas.Columns["cajero"].HeaderText = "Cajero";
            dgvVentas.Columns["subtotal"].HeaderText = "Subtotal";
            dgvVentas.Columns["iva"].HeaderText = "IVA";
            dgvVentas.Columns["total"].HeaderText = "Total";
            dgvVentas.Columns["estado"].HeaderText = "Estado";
            dgvVentas.Columns["num_productos"].HeaderText = "# Productos";

            dgvVentas.Columns["subtotal"].DefaultCellStyle.Format = "C2";
            dgvVentas.Columns["iva"].DefaultCellStyle.Format = "C2";
            dgvVentas.Columns["total"].DefaultCellStyle.Format = "C2";
            dgvVentas.Columns["fecha"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";

            dgvVentas.Columns["idventa"].Width = 50;
            dgvVentas.Columns["num_productos"].Width = 90;
        }

        private void ColorizarVentas()
        {
            foreach (DataGridViewRow row in dgvVentas.Rows)
            {
                if (row.Cells["estado"].Value == null) continue;
                switch (row.Cells["estado"].Value.ToString())
                {
                    case "CONFIRMADA":
                        row.DefaultCellStyle.BackColor = Color.FromArgb(232, 248, 245);
                        row.Cells["estado"].Style.ForeColor = Color.FromArgb(22, 160, 133); break;
                    case "CANCELADA":
                        row.DefaultCellStyle.BackColor = Color.FromArgb(255, 230, 230);
                        row.Cells["estado"].Style.ForeColor = Color.FromArgb(192, 57, 43); break;
                    case "ACTIVA":
                        row.DefaultCellStyle.BackColor = Color.FromArgb(255, 250, 205);
                        row.Cells["estado"].Style.ForeColor = Color.FromArgb(180, 140, 0); break;
                }
            }
        }

        private void CargarMovimientos()
        {
            try
            {
                string tipo = cmbTipoMov.SelectedItem?.ToString() ?? "TODOS";
                DataTable dt = CN_Compra.ListarMovimientos(dtpInicio.Value, dtpFin.Value, tipo);
                dgvMovimientos.DataSource = dt;

                if (dgvMovimientos.Columns.Count > 0)
                {
                    dgvMovimientos.Columns["idmovimiento"].Visible = false;
                    dgvMovimientos.Columns["fecha"].HeaderText = "Fecha";
                    dgvMovimientos.Columns["tipo"].HeaderText = "Tipo";
                    dgvMovimientos.Columns["cantidad"].HeaderText = "Cantidad";
                    dgvMovimientos.Columns["motivo"].HeaderText = "Motivo";
                    dgvMovimientos.Columns["producto"].HeaderText = "Producto";
                    dgvMovimientos.Columns["referencia"].HeaderText = "Referencia";
                    dgvMovimientos.Columns["fecha"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";

                    foreach (DataGridViewRow row in dgvMovimientos.Rows)
                    {
                        if (row.Cells["tipo"].Value?.ToString() == "ENTRADA")
                            row.DefaultCellStyle.BackColor = Color.FromArgb(232, 248, 245);
                        else
                            row.DefaultCellStyle.BackColor = Color.FromArgb(255, 230, 230);
                    }
                }

                lblTotalMov.Text = $"Movimientos encontrados: {dgvMovimientos.Rows.Count}";
            }
            catch { }
        }

        // ── Eventos botones ───────────────────────────────────────────────────
        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            CargarVentas();
            CargarMovimientos();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            dtpInicio.Value = DateTime.Now.AddMonths(-1);
            dtpFin.Value = DateTime.Now;
            cmbEstado.SelectedIndex = 0;
            cmbTipoMov.SelectedIndex = 0;
            CargarVentas();
            CargarMovimientos();
        }
    }
}