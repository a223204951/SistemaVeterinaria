using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        // EVENTO LOAD DEL FORMULARIO
        private void FrmAuditoria_Load(object sender, EventArgs e)
        {
            // Configurar fechas por defecto (último mes)
            dtpFechaInicio.Value = DateTime.Now.AddMonths(-1);
            dtpFechaFin.Value = DateTime.Now;

            // Seleccionar "TODAS" por defecto
            cmbFiltroOperacion.SelectedIndex = 0;

            // Cargar auditoría completa
            CargarAuditoria();

            // Configurar apariencia del DataGridView
            ConfigurarDataGridView();
        }

        // MÉTODO PARA CARGAR LA AUDITORÍA
        private void CargarAuditoria()
        {
            try
            {
                string operacion = cmbFiltroOperacion.SelectedItem?.ToString();
                DateTime fechaInicio = dtpFechaInicio.Value.Date;
                DateTime fechaFin = dtpFechaFin.Value.Date.AddDays(1).AddSeconds(-1);

                DataTable datos = CN_Auditoria.Listar(operacion, fechaInicio, fechaFin);
                dgvAuditoria.DataSource = datos;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar auditoría: " + ex.Message,
                    "Sistema Veterinaria",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // MÉTODO PARA CONFIGURAR LA APARIENCIA DEL DATAGRIDVIEW
        private void ConfigurarDataGridView()
        {
            if (dgvAuditoria.Columns.Count > 0)
            {
                // Ocultar columnas que no son necesarias visualmente
                if (dgvAuditoria.Columns.Contains("nombre_anterior"))
                    dgvAuditoria.Columns["nombre_anterior"].Visible = false;
                if (dgvAuditoria.Columns.Contains("telefono_anterior"))
                    dgvAuditoria.Columns["telefono_anterior"].Visible = false;
                if (dgvAuditoria.Columns.Contains("direccion_anterior"))
                    dgvAuditoria.Columns["direccion_anterior"].Visible = false;
                if (dgvAuditoria.Columns.Contains("estado_anterior"))
                    dgvAuditoria.Columns["estado_anterior"].Visible = false;
                if (dgvAuditoria.Columns.Contains("nombre_nuevo"))
                    dgvAuditoria.Columns["nombre_nuevo"].Visible = false;
                if (dgvAuditoria.Columns.Contains("telefono_nuevo"))
                    dgvAuditoria.Columns["telefono_nuevo"].Visible = false;
                if (dgvAuditoria.Columns.Contains("direccion_nuevo"))
                    dgvAuditoria.Columns["direccion_nuevo"].Visible = false;
                if (dgvAuditoria.Columns.Contains("estado_nuevo"))
                    dgvAuditoria.Columns["estado_nuevo"].Visible = false;

                // Renombrar encabezados
                if (dgvAuditoria.Columns.Contains("idauditoria"))
                    dgvAuditoria.Columns["idauditoria"].HeaderText = "ID Auditoría";
                if (dgvAuditoria.Columns.Contains("idcliente"))
                    dgvAuditoria.Columns["idcliente"].HeaderText = "ID Cliente";
                if (dgvAuditoria.Columns.Contains("operacion"))
                    dgvAuditoria.Columns["operacion"].HeaderText = "Operación";
                if (dgvAuditoria.Columns.Contains("usuario"))
                    dgvAuditoria.Columns["usuario"].HeaderText = "Usuario";
                if (dgvAuditoria.Columns.Contains("fecha"))
                    dgvAuditoria.Columns["fecha"].HeaderText = "Fecha";
                if (dgvAuditoria.Columns.Contains("descripcion"))
                    dgvAuditoria.Columns["descripcion"].HeaderText = "Descripción";
            }
        }

        // EVENTO CLICK DEL BOTÓN FILTRAR
        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            CargarAuditoria();
        }

        // EVENTO CLICK DEL BOTÓN LIMPIAR
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            // Resetear filtros
            cmbFiltroOperacion.SelectedIndex = 0;
            dtpFechaInicio.Value = DateTime.Now.AddMonths(-1);
            dtpFechaFin.Value = DateTime.Now;

            // Recargar todo
            CargarAuditoria();
        }

        // EVENTO CLICK DEL BOTÓN CERRAR
        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}