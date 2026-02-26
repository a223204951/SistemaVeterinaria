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

        // EVENTO LOAD DEL FORMULARIO
        private void FrmSesiones_Load(object sender, EventArgs e)
        {
            // Configurar fechas por defecto
            dtpFechaInicio.Value = DateTime.Now.AddMonths(-1);
            dtpFechaFin.Value = DateTime.Now;

            // Cargar usuarios en el combo
            CargarUsuarios();

            // Cargar sesiones
            CargarSesiones();

            // Configurar apariencia del DataGridView
            dgvSesiones.BorderStyle = BorderStyle.None;
            dgvSesiones.BackgroundColor = Color.White;
            dgvSesiones.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(52, 73, 94);
            dgvSesiones.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvSesiones.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgvSesiones.DefaultCellStyle.Font = new Font("Segoe UI", 9F);
            dgvSesiones.DefaultCellStyle.SelectionBackColor = Color.FromArgb(52, 152, 219);
            dgvSesiones.EnableHeadersVisualStyles = false;

            // Configurar fechas y cargar datos
            dtpFechaInicio.Value = DateTime.Now.AddMonths(-1);
            dtpFechaFin.Value = DateTime.Now;

            CargarUsuarios();
            CargarSesiones();
        }

        // MÉTODO PARA CARGAR USUARIOS EN EL COMBOBOX
        private void CargarUsuarios()
        {
            cmbUsuario.Items.Clear();
            cmbUsuario.Items.Add("TODOS");

            // Obtener usuarios únicos de las sesiones
            DataTable datos = CN_Sesion.Listar("TODOS",
                DateTime.Now.AddYears(-1),
                DateTime.Now.AddDays(1));

            if (datos != null)
            {
                foreach (DataRow row in datos.Rows)
                {
                    string usuario = row["usuario"].ToString();
                    if (!cmbUsuario.Items.Contains(usuario))
                    {
                        cmbUsuario.Items.Add(usuario);
                    }
                }
            }

            cmbUsuario.SelectedIndex = 0;
        }

        // MÉTODO PARA CARGAR SESIONES
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

                    // Configurar columnas
                    ConfigurarDataGridView();

                    // Actualizar estadísticas
                    ActualizarEstadisticas(datos);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar sesiones: " + ex.Message,
                    "Sistema Veterinaria",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // MÉTODO PARA CONFIGURAR EL DATAGRIDVIEW
        private void ConfigurarDataGridView()
        {
            if (dgvSesiones.Columns.Count == 0) return;

            // Renombrar encabezados
            if (dgvSesiones.Columns.Contains("idsesion"))
                dgvSesiones.Columns["idsesion"].HeaderText = "ID Sesión";
            if (dgvSesiones.Columns.Contains("usuario"))
                dgvSesiones.Columns["usuario"].HeaderText = "Usuario";
            if (dgvSesiones.Columns.Contains("fecha_inicio"))
                dgvSesiones.Columns["fecha_inicio"].HeaderText = "Inicio de Sesión";
            if (dgvSesiones.Columns.Contains("fecha_fin"))
                dgvSesiones.Columns["fecha_fin"].HeaderText = "Fin de Sesión";
            if (dgvSesiones.Columns.Contains("duracion_minutos"))
                dgvSesiones.Columns["duracion_minutos"].HeaderText = "Duración (min)";
            if (dgvSesiones.Columns.Contains("duracion_actual"))
                dgvSesiones.Columns["duracion_actual"].HeaderText = "Duración Actual (min)";
            if (dgvSesiones.Columns.Contains("estado"))
                dgvSesiones.Columns["estado"].HeaderText = "Estado";

            // Colorear filas según estado
            foreach (DataGridViewRow row in dgvSesiones.Rows)
            {
                if (row.Cells["estado"].Value?.ToString() == "ACTIVA")
                {
                    // Verde claro para sesiones activas
                    row.DefaultCellStyle.BackColor = Color.LightGreen;
                }
                else
                {
                    // Blanco para sesiones cerradas
                    row.DefaultCellStyle.BackColor = Color.White;
                }
            }
        }

        // MÉTODO PARA ACTUALIZAR ESTADÍSTICAS
        private void ActualizarEstadisticas(DataTable datos)
        {
            int totalSesiones = datos.Rows.Count;
            double promedioMinutos = 0;

            if (totalSesiones > 0)
            {
                int suma = 0;
                int sesionesConDuracion = 0;

                foreach (DataRow row in datos.Rows)
                {
                    if (row["duracion_actual"] != DBNull.Value)
                    {
                        suma += Convert.ToInt32(row["duracion_actual"]);
                        sesionesConDuracion++;
                    }
                }

                if (sesionesConDuracion > 0)
                {
                    promedioMinutos = Math.Round((double)suma / sesionesConDuracion, 1);
                }
            }

            lblTotalSesiones.Text = $"Total de sesiones: {totalSesiones}";
            lblTiempoPromedio.Text = $"Tiempo promedio de conexión: {promedioMinutos} minutos";
        }

        // EVENTO CLICK DEL BOTÓN FILTRAR
        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            CargarSesiones();
        }

        // EVENTO CLICK DEL BOTÓN LIMPIAR
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            cmbUsuario.SelectedIndex = 0;
            dtpFechaInicio.Value = DateTime.Now.AddMonths(-1);
            dtpFechaFin.Value = DateTime.Now;
            CargarSesiones();
        }

        // EVENTO CLICK DEL BOTÓN CERRAR
        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}