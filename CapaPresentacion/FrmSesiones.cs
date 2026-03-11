using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using CapaNegocio;

namespace CapaPresentacion
{
    public partial class FrmSesiones : Form
    {
        public FrmSesiones()
        {
            InitializeComponent();
        }

        private void FrmSesiones_Load(object sender, EventArgs e)
        {
            dtpFechaInicio.Value = DateTime.Now.AddMonths(-1);
            dtpFechaFin.Value = DateTime.Now;
            CargarUsuarios();
            CargarSesiones();
        }

        private void CargarUsuarios()
        {
            cmbUsuario.Items.Clear();
            cmbUsuario.Items.Add("TODOS");

            DataTable datos = CN_Sesion.Listar("TODOS",
                DateTime.Now.AddYears(-1), DateTime.Now.AddDays(1));

            if (datos != null)
                foreach (DataRow row in datos.Rows)
                {
                    string u = row["usuario"].ToString();
                    if (!cmbUsuario.Items.Contains(u)) cmbUsuario.Items.Add(u);
                }

            cmbUsuario.SelectedIndex = 0;
        }

        private void CargarSesiones()
        {
            try
            {
                string usuario = cmbUsuario.SelectedItem?.ToString() ?? "TODOS";
                DateTime fechaInicio = dtpFechaInicio.Value.Date;
                DateTime fechaFin = dtpFechaFin.Value.Date.AddDays(1).AddSeconds(-1);

                DataTable datos = CN_Sesion.Listar(usuario, fechaInicio, fechaFin);

                if (datos != null)
                {
                    dgvSesiones.DataSource = datos;
                    ConfigurarDataGridView();
                    ActualizarEstadisticas(datos);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar sesiones: " + ex.Message,
                    "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurarDataGridView()
        {
            if (dgvSesiones.Columns.Count == 0) return;

            void R(string c, string h) { if (dgvSesiones.Columns.Contains(c)) dgvSesiones.Columns[c].HeaderText = h; }
            R("idsesion", "ID Sesión");
            R("usuario", "Usuario");
            R("fecha_inicio", "Inicio de Sesión");
            R("fecha_fin", "Fin de Sesión");
            R("duracion_minutos", "Duración (min)");
            R("duracion_actual", "Duración Actual (min)");
            R("estado", "Estado");

            foreach (DataGridViewRow row in dgvSesiones.Rows)
            {
                if (row.Cells["estado"].Value?.ToString() == "ACTIVA")
                    row.DefaultCellStyle.BackColor = Color.FromArgb(232, 248, 245);
                else
                    row.DefaultCellStyle.BackColor = Color.White;
            }
        }

        private void ActualizarEstadisticas(DataTable datos)
        {
            int total = datos.Rows.Count, suma = 0, conDuracion = 0;

            foreach (DataRow row in datos.Rows)
                if (row["duracion_actual"] != DBNull.Value)
                { suma += Convert.ToInt32(row["duracion_actual"]); conDuracion++; }

            double promedio = conDuracion > 0 ? Math.Round((double)suma / conDuracion, 1) : 0;

            lblTotalSesiones.Text = $"Total de sesiones: {total}";
            lblTiempoPromedio.Text = $"Tiempo promedio de conexión: {promedio} minutos";
        }

        private void btnFiltrar_Click(object sender, EventArgs e) => CargarSesiones();

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            cmbUsuario.SelectedIndex = 0;
            dtpFechaInicio.Value = DateTime.Now.AddMonths(-1);
            dtpFechaFin.Value = DateTime.Now;
            CargarSesiones();
        }
    }
}