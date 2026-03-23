using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using CapaNegocio;

namespace CapaPresentacion
{
    /// <summary>
    /// FORMULARIO LISTADO DE PROVEEDORES / DISTRIBUIDORES
    /// Patrón idéntico a FrmListadoProductos y FrmListadoClientes
    /// </summary>
    public partial class FrmListadoProveedores : Form
    {
        private CN_Usuario _cnUsuario = new CN_Usuario();

        public FrmListadoProveedores()
        {
            InitializeComponent();
        }

        // =====================================================================
        // LOAD
        // =====================================================================
        private void FrmListadoProveedores_Load(object sender, EventArgs e)
        {
            Mostrar();
            ConfigurarPermisos();
        }

        // =====================================================================
        // PERMISOS
        // =====================================================================
        private void ConfigurarPermisos()
        {
            string rol = FrmLogin.RolActual;
            btnNuevo.Visible = _cnUsuario.PuedeCrear(rol, "Proveedores");
            btnEditar.Visible = _cnUsuario.PuedeEditar(rol, "Proveedores");
            btnEliminar.Visible = _cnUsuario.PuedeEliminar(rol, "Proveedores");
            // Historial de compras solo ADMIN
            btnHistorial.Visible = (rol == "ADMINISTRADOR" || rol == "CAJERO");
        }

        // =====================================================================
        // MOSTRAR
        // =====================================================================
        public void Mostrar()
        {
            try
            {
                DataTable dt = CN_Proveedor.Listar();
                dgvProveedores.DataSource = dt;
                ConfigurarColumnas();
                ActualizarContador();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar proveedores: " + ex.Message,
                    "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurarColumnas()
        {
            if (dgvProveedores.Columns.Count == 0) return;

            if (dgvProveedores.Columns.Contains("idproveedor")) { dgvProveedores.Columns["idproveedor"].HeaderText = "ID"; dgvProveedores.Columns["idproveedor"].Width = 45; }
            if (dgvProveedores.Columns.Contains("nombre")) dgvProveedores.Columns["nombre"].HeaderText = "Nombre";
            if (dgvProveedores.Columns.Contains("telefono")) { dgvProveedores.Columns["telefono"].HeaderText = "Teléfono"; dgvProveedores.Columns["telefono"].Width = 120; }
            if (dgvProveedores.Columns.Contains("direccion")) dgvProveedores.Columns["direccion"].HeaderText = "Dirección";
            if (dgvProveedores.Columns.Contains("correo")) dgvProveedores.Columns["correo"].HeaderText = "Correo";
            if (dgvProveedores.Columns.Contains("estado")) { dgvProveedores.Columns["estado"].HeaderText = "Estado"; dgvProveedores.Columns["estado"].Width = 90; }
            if (dgvProveedores.Columns.Contains("total_compras")) { dgvProveedores.Columns["total_compras"].HeaderText = "# Compras"; dgvProveedores.Columns["total_compras"].Width = 85; }
            if (dgvProveedores.Columns.Contains("monto_total"))
            {
                dgvProveedores.Columns["monto_total"].HeaderText = "Total Comprado";
                dgvProveedores.Columns["monto_total"].Width = 120;
                dgvProveedores.Columns["monto_total"].DefaultCellStyle.Format = "C2";
            }

            // Colorear filas inactivas
            foreach (DataGridViewRow row in dgvProveedores.Rows)
            {
                if (row.Cells["estado"].Value?.ToString() == "INACTIVO")
                {
                    row.DefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);
                    row.DefaultCellStyle.ForeColor = Color.FromArgb(149, 165, 166);
                }
            }
        }

        private void ActualizarContador()
        {
            int total = dgvProveedores.Rows.Count;
            int activos = 0;
            foreach (DataGridViewRow row in dgvProveedores.Rows)
                if (row.Cells["estado"].Value?.ToString() == "ACTIVO") activos++;

            lblTotal.Text = $"Total: {total} proveedores | Activos: {activos}";
        }

        // =====================================================================
        // BÚSQUEDA
        // =====================================================================
        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtBuscar.Text))
                    Mostrar();
                else
                {
                    dgvProveedores.DataSource = CN_Proveedor.BuscarNombre(txtBuscar.Text);
                    ConfigurarColumnas();
                    ActualizarContador();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en búsqueda: " + ex.Message,
                    "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtBuscar.Clear();
            Mostrar();
        }

        // =====================================================================
        // CRUD
        // =====================================================================
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            if (!_cnUsuario.PuedeCrear(FrmLogin.RolActual, "Proveedores"))
            {
                MessageBox.Show("No tiene permisos para registrar proveedores.",
                    "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            FrmRegistrarProveedor form = new FrmRegistrarProveedor { Insert = true };
            if (form.ShowDialog() == DialogResult.OK)
                Mostrar();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (!_cnUsuario.PuedeEditar(FrmLogin.RolActual, "Proveedores"))
            {
                MessageBox.Show("No tiene permisos para editar proveedores.",
                    "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dgvProveedores.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione un proveedor para editar.",
                    "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow row = dgvProveedores.CurrentRow;
            FrmRegistrarProveedor form = new FrmRegistrarProveedor { Edit = true };

            form.txtIdProveedor.Text = row.Cells["idproveedor"].Value.ToString();
            form.txtNombre.Text = row.Cells["nombre"].Value?.ToString() ?? "";
            form.txtTelefono.Text = row.Cells["telefono"].Value?.ToString() ?? "";
            form.txtDireccion.Text = row.Cells["direccion"].Value?.ToString() ?? "";
            form.txtCorreo.Text = row.Cells["correo"].Value?.ToString() ?? "";

            string estado = row.Cells["estado"].Value?.ToString() ?? "ACTIVO";
            if (estado == "ACTIVO") form.rbtnActivo.Checked = true;
            else form.rbtnInactivo.Checked = true;

            if (form.ShowDialog() == DialogResult.OK)
                Mostrar();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (!_cnUsuario.PuedeEliminar(FrmLogin.RolActual, "Proveedores"))
            {
                MessageBox.Show("No tiene permisos para desactivar proveedores.",
                    "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dgvProveedores.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione un proveedor para desactivar.",
                    "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string nombre = dgvProveedores.CurrentRow.Cells["nombre"].Value?.ToString() ?? "";

            if (MessageBox.Show(
                    $"¿Desactivar el proveedor?\n\nProveedor: {nombre}\n\nEl proveedor quedará INACTIVO pero se conservará su historial de compras.",
                    "Sistema Veterinaria", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                == DialogResult.Yes)
            {
                try
                {
                    int id = Convert.ToInt32(dgvProveedores.CurrentRow.Cells["idproveedor"].Value);
                    string resultado = CN_Proveedor.Eliminar(id);

                    if (resultado == "OK")
                    {
                        MessageBox.Show("✅ Proveedor desactivado correctamente.",
                            "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Mostrar();
                    }
                    else
                        MessageBox.Show("❌ " + resultado,
                            "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message,
                        "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // ── Historial de compras del proveedor seleccionado ───────────────────
        private void btnHistorial_Click(object sender, EventArgs e)
        {
            if (dgvProveedores.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione un proveedor para ver su historial.",
                    "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int id = Convert.ToInt32(dgvProveedores.CurrentRow.Cells["idproveedor"].Value);
            string nombre = dgvProveedores.CurrentRow.Cells["nombre"].Value?.ToString() ?? "";

            FrmHistorialComprasProveedor frm = new FrmHistorialComprasProveedor(id, nombre);
            frm.ShowDialog(this);
        }

        // ── Doble clic = editar ───────────────────────────────────────────────
        private void dgvProveedores_DoubleClick(object sender, EventArgs e)
        {
            if (_cnUsuario.PuedeEditar(FrmLogin.RolActual, "Proveedores"))
                btnEditar_Click(sender, e);
        }
    }
}