using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using CapaNegocio;

namespace CapaPresentacion
{
    public partial class FrmAuditoria : Form
    {
        public FrmAuditoria()
        {
            InitializeComponent();
        }

        private void FrmAuditoria_Load(object sender, EventArgs e)
        {
            dtpFechaInicio.Value = DateTime.Now.AddMonths(-1);
            dtpFechaFin.Value = DateTime.Now;
            cmbFiltroOperacion.SelectedIndex = 0;
            CargarAuditoria();
            ConfigurarDataGridView();
        }

        private void CargarAuditoria()
        {
            try
            {
                string operacion = cmbFiltroOperacion.SelectedItem?.ToString();
                DateTime fechaInicio = dtpFechaInicio.Value.Date;
                DateTime fechaFin = dtpFechaFin.Value.Date.AddDays(1).AddSeconds(-1);

                DataTable datos = CN_Auditoria.Listar(operacion, fechaInicio, fechaFin);
                dgvAuditoria.DataSource = datos;
                ConfigurarDataGridView();

                lblTotal.Text = $"Total de registros: {dgvAuditoria.Rows.Count}";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar auditoría: " + ex.Message,
                    "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurarDataGridView()
        {
            if (dgvAuditoria.Columns.Count == 0) return;

            foreach (string col in new[] {
                "nombre_anterior","telefono_anterior","direccion_anterior","estado_anterior",
                "nombre_nuevo","telefono_nuevo","direccion_nuevo","estado_nuevo" })
                if (dgvAuditoria.Columns.Contains(col))
                    dgvAuditoria.Columns[col].Visible = false;

            void R(string c, string h) { if (dgvAuditoria.Columns.Contains(c)) dgvAuditoria.Columns[c].HeaderText = h; }
            R("idauditoria", "ID"); R("idcliente", "ID Cliente");
            R("operacion", "Operación"); R("usuario", "Usuario");
            R("fecha", "Fecha"); R("descripcion", "Descripción");

            // Colorear filas por operación
            foreach (DataGridViewRow row in dgvAuditoria.Rows)
            {
                if (row.Cells["operacion"].Value == null) continue;
                switch (row.Cells["operacion"].Value.ToString())
                {
                    case "INSERT":
                        row.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(232, 248, 245);
                        row.DefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(22, 160, 133); break;
                    case "UPDATE":
                        row.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(255, 250, 205);
                        row.DefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(180, 140, 0); break;
                    case "DELETE":
                        row.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(255, 230, 230);
                        row.DefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(192, 57, 43); break;
                }
            }
        }

        private void btnFiltrar_Click(object sender, EventArgs e) => CargarAuditoria();

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            cmbFiltroOperacion.SelectedIndex = 0;
            dtpFechaInicio.Value = DateTime.Now.AddMonths(-1);
            dtpFechaFin.Value = DateTime.Now;
            CargarAuditoria();
        }
    }
}