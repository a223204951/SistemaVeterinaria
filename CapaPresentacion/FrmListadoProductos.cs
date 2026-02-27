using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using CapaNegocio;

namespace CapaPresentacion
{
    public partial class FrmListadoProductos : Form
    {
        private CN_Usuario cnUsuario = new CN_Usuario();

        // Flag para evitar que SelectedIndexChanged dispare durante CargarCategorias()
        private bool _cargandoCategorias = false;

        public FrmListadoProductos()
        {
            InitializeComponent();
        }

        private void FrmListadoProductos_Load(object sender, EventArgs e)
        {
            CargarCategorias();
            Mostrar();
            ConfigurarPermisos();
            VerificarAlertas();
        }

        private void ConfigurarPermisos()
        {
            string rol = FrmLogin.RolActual;
            bool esAdmin = (rol == "ADMINISTRADOR");

            btnNuevo.Visible = esAdmin || TryPerm(rol, "crear");
            btnEditar.Visible = esAdmin || TryPerm(rol, "editar");
            btnEliminar.Visible = esAdmin || TryPerm(rol, "eliminar");
            btnHistorialPrecios.Visible = esAdmin || rol == "CAJERO";
        }

        private bool TryPerm(string rol, string tipo)
        {
            try
            {
                switch (tipo)
                {
                    case "crear": return cnUsuario.PuedeCrear(rol, "Productos");
                    case "editar": return cnUsuario.PuedeEditar(rol, "Productos");
                    case "eliminar": return cnUsuario.PuedeEliminar(rol, "Productos");
                    default: return false;
                }
            }
            catch { return false; }
        }

        public void Mostrar()
        {
            try
            {
                DataTable datos = CN_Producto.Listar();
                dgvProductos.DataSource = datos;
                ConfigurarColumnas();
                AplicarColoresStock();
                ActualizarContador();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar productos: " + ex.Message,
                    "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurarColumnas()
        {
            if (dgvProductos.Columns.Count == 0) return;

            foreach (string col in new[] { "precio_base", "precio_minimo", "total_vendido",
                                           "fecha_ultimo_ajuste", "fecha_creacion", "idcategoria" })
                if (dgvProductos.Columns.Contains(col))
                    dgvProductos.Columns[col].Visible = false;

            void Rename(string c, string h) { if (dgvProductos.Columns.Contains(c)) dgvProductos.Columns[c].HeaderText = h; }
            void W(string c, int w) { if (dgvProductos.Columns.Contains(c)) dgvProductos.Columns[c].Width = w; }

            Rename("idproducto", "ID");
            Rename("nombre", "Nombre");
            Rename("descripcion", "Descripción");
            Rename("precio", "Precio");
            Rename("stock", "Stock");
            Rename("estado", "Estado");
            Rename("categoria", "Categoría");
            Rename("es_medicamento", "Medicamento");
            Rename("fecha_vencimiento", "Vencimiento");
            Rename("nivel_stock", "Nivel Stock");
            Rename("porcentaje_cambio", "Cambio %");

            if (dgvProductos.Columns.Contains("precio"))
                dgvProductos.Columns["precio"].DefaultCellStyle.Format = "C2";
            if (dgvProductos.Columns.Contains("fecha_vencimiento"))
                dgvProductos.Columns["fecha_vencimiento"].DefaultCellStyle.Format = "dd/MM/yyyy";
            if (dgvProductos.Columns.Contains("porcentaje_cambio"))
                dgvProductos.Columns["porcentaje_cambio"].DefaultCellStyle.Format = "0.00'%'";

            W("idproducto", 50); W("precio", 100);
            W("stock", 70); W("estado", 90);
            W("categoria", 120); W("nivel_stock", 120);
        }

        private void AplicarColoresStock()
        {
            bool tieneNivel = dgvProductos.Columns.Contains("nivel_stock");
            bool tieneCambio = dgvProductos.Columns.Contains("porcentaje_cambio");

            foreach (DataGridViewRow row in dgvProductos.Rows)
            {
                if (tieneNivel && row.Cells["nivel_stock"].Value != null)
                {
                    switch (row.Cells["nivel_stock"].Value.ToString())
                    {
                        case "SIN STOCK":
                            row.DefaultCellStyle.BackColor = Color.FromArgb(255, 230, 230);
                            row.DefaultCellStyle.ForeColor = Color.FromArgb(192, 57, 43); break;
                        case "STOCK BAJO":
                            row.DefaultCellStyle.BackColor = Color.FromArgb(255, 243, 224);
                            row.DefaultCellStyle.ForeColor = Color.FromArgb(230, 126, 34); break;
                        case "STOCK MEDIO":
                            row.DefaultCellStyle.BackColor = Color.FromArgb(255, 250, 205);
                            row.DefaultCellStyle.ForeColor = Color.FromArgb(241, 196, 15); break;
                        case "STOCK SUFICIENTE":
                            row.DefaultCellStyle.BackColor = Color.FromArgb(232, 248, 245);
                            row.DefaultCellStyle.ForeColor = Color.FromArgb(22, 160, 133); break;
                    }
                }
                if (tieneCambio && row.Cells["porcentaje_cambio"].Value != null)
                {
                    decimal cambio = Convert.ToDecimal(row.Cells["porcentaje_cambio"].Value);
                    row.Cells["porcentaje_cambio"].Style.ForeColor = cambio > 0
                        ? Color.FromArgb(46, 204, 113) : Color.FromArgb(231, 76, 60);
                }
            }
        }

        private void ActualizarContador()
        {
            int total = dgvProductos.Rows.Count, bajo = 0, sinStock = 0;

            if (dgvProductos.Columns.Contains("nivel_stock"))
                foreach (DataGridViewRow row in dgvProductos.Rows)
                    if (row.Cells["nivel_stock"].Value != null)
                    {
                        string n = row.Cells["nivel_stock"].Value.ToString();
                        if (n == "STOCK BAJO") bajo++;
                        if (n == "SIN STOCK") sinStock++;
                    }

            lblTotal.Text = $"Total: {total} productos | Stock bajo: {bajo} | Sin stock: {sinStock}";
        }

        private void CargarCategorias()
        {
            try
            {
                _cargandoCategorias = true;

                DataTable categorias = CN_Categoria.ListarActivas();
                DataRow filaTodas = categorias.NewRow();
                filaTodas["idcategoria"] = 0;
                filaTodas["nombre"] = "-- Todas las categorías --";
                categorias.Rows.InsertAt(filaTodas, 0);

                cmbCategoria.DataSource = categorias;
                cmbCategoria.DisplayMember = "nombre";
                cmbCategoria.ValueMember = "idcategoria";
                cmbCategoria.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar categorías: " + ex.Message,
                    "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _cargandoCategorias = false;
            }
        }

        private void VerificarAlertas()
        {
            DataTable bajo = CN_Producto.ObtenerProductosStockBajo();
            lblAlertaStock.Visible = (bajo != null && bajo.Rows.Count > 0);
            if (lblAlertaStock.Visible)
                lblAlertaStock.Text = $"⚠️ {bajo.Rows.Count} producto(s) con stock bajo";

            DataTable vencer = CN_Producto.ObtenerProductosProximosVencer();
            lblAlertaVencimiento.Visible = (vencer != null && vencer.Rows.Count > 0);
            if (lblAlertaVencimiento.Visible)
                lblAlertaVencimiento.Text = $"⚠️ {vencer.Rows.Count} producto(s) próximo(s) a vencer";
        }

        // ── BÚSQUEDA ───────────────────────────────────────────────────────────

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtBuscar.Text))
                    Mostrar();
                else
                {
                    dgvProductos.DataSource = CN_Producto.BuscarNombre(txtBuscar.Text);
                    ConfigurarColumnas(); AplicarColoresStock(); ActualizarContador();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en búsqueda: " + ex.Message,
                    "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            // No disparar durante la carga inicial del ComboBox
            if (_cargandoCategorias) return;
            if (cmbCategoria.SelectedItem == null) return;

            try
            {
                // *** FIX DEFINITIVO: cuando DataSource es DataTable, SelectedValue devuelve
                //     DataRowView aunque ValueMember esté configurado. Leer siempre desde
                //     SelectedItem casteado a DataRowView para obtener el valor seguro. ***
                DataRowView drv = (DataRowView)cmbCategoria.SelectedItem;
                int idcategoria = Convert.ToInt32(drv["idcategoria"]);

                if (idcategoria == 0)
                    Mostrar();
                else
                {
                    dgvProductos.DataSource = CN_Producto.BuscarCategoria(idcategoria);
                    ConfigurarColumnas(); AplicarColoresStock(); ActualizarContador();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al filtrar: " + ex.Message,
                    "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtBuscar.Clear();
            _cargandoCategorias = true;
            cmbCategoria.SelectedIndex = 0;
            _cargandoCategorias = false;
            Mostrar();
        }

        // ── CRUD ───────────────────────────────────────────────────────────────

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            FrmRegistrarProducto form = new FrmRegistrarProducto();
            form.Insert = true;
            if (form.ShowDialog() == DialogResult.OK) { Mostrar(); VerificarAlertas(); }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvProductos.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione un producto para editar",
                    "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            FrmRegistrarProducto form = new FrmRegistrarProducto();
            form.Edit = true;
            DataGridViewRow row = dgvProductos.CurrentRow;

            form.txtIdProducto.Text = row.Cells["idproducto"].Value.ToString();
            form.txtNombre.Text = row.Cells["nombre"].Value.ToString();
            form.txtDescripcion.Text = row.Cells["descripcion"].Value.ToString();
            form.nudPrecio.Value = Convert.ToDecimal(row.Cells["precio"].Value);
            form.nudStock.Value = Convert.ToDecimal(row.Cells["stock"].Value);
            form.IdCategoriaSeleccionada = Convert.ToInt32(row.Cells["idcategoria"].Value);
            form.chkEsMedicamento.Checked = Convert.ToBoolean(row.Cells["es_medicamento"].Value);

            if (row.Cells["estado"].Value.ToString() == "ACTIVO")
                form.rbtnActivo.Checked = true;
            else
                form.rbtnInactivo.Checked = true;

            if (row.Cells["fecha_vencimiento"].Value != DBNull.Value)
            {
                form.dtpVencimiento.Value = Convert.ToDateTime(row.Cells["fecha_vencimiento"].Value);
                form.dtpVencimiento.Enabled = true;
            }

            if (form.ShowDialog() == DialogResult.OK) { Mostrar(); VerificarAlertas(); }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvProductos.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione un producto para eliminar",
                    "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string nombre = dgvProductos.CurrentRow.Cells["nombre"].Value.ToString();
            if (MessageBox.Show($"¿Dar de baja '{nombre}'?\nSerá marcado como INACTIVO.",
                "Sistema Veterinaria", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    int id = Convert.ToInt32(dgvProductos.CurrentRow.Cells["idproducto"].Value);
                    string res = CN_Producto.Eliminar(id);
                    if (res == "OK")
                    {
                        MessageBox.Show("✅ Producto dado de baja correctamente",
                            "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Mostrar(); VerificarAlertas();
                    }
                    else
                        MessageBox.Show(res, "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message,
                        "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnHistorialPrecios_Click(object sender, EventArgs e)
        {
            if (dgvProductos.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione un producto para ver su historial",
                    "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int id = Convert.ToInt32(dgvProductos.CurrentRow.Cells["idproducto"].Value);
            string nombre = dgvProductos.CurrentRow.Cells["nombre"].Value.ToString();
            new FrmHistorialPrecios(id, nombre).ShowDialog();
        }

        private void dgvProductos_DoubleClick(object sender, EventArgs e)
            => btnEditar_Click(sender, e);
    }
}