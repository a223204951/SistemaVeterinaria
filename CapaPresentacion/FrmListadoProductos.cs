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
        private bool _cargandoCategorias = false;

        // ── Tooltip visual de código de barras ────────────────────────────────
        private Form _popupBarcode = null;
        private int _ultimaFilaTooltip = -1;

        public FrmListadoProductos()
        {
            InitializeComponent();
        }

        // =====================================================================
        // LOAD
        // =====================================================================
        private void FrmListadoProductos_Load(object sender, EventArgs e)
        {
            Mostrar();
            ConfigurarPermisos();
            CargarCategorias();
            VerificarAlertas();

            // Registrar eventos del tooltip de código de barras
            dgvProductos.CellMouseEnter += dgvProductos_CellMouseEnter;
            dgvProductos.CellMouseLeave += dgvProductos_CellMouseLeave;
        }

        // =====================================================================
        // PERMISOS
        // =====================================================================
        private void ConfigurarPermisos()
        {
            string rol = FrmLogin.RolActual;

            bool puedeCrear = cnUsuario.PuedeCrear(rol, "Productos");
            bool puedeEditar = cnUsuario.PuedeEditar(rol, "Productos");
            bool puedeEliminar = cnUsuario.PuedeEliminar(rol, "Productos");

            btnNuevo.Visible = puedeCrear;
            btnEditar.Visible = puedeEditar;
            btnEliminar.Visible = puedeEliminar;

            btnHistorialPrecios.Visible = (rol == "ADMINISTRADOR" || rol == "CAJERO");
            btnEtiqueta.Visible = true; // visible para todos los roles

            if (!puedeEditar && !puedeEliminar)
                dgvProductos.ReadOnly = true;
        }

        // =====================================================================
        // MOSTRAR / CONFIGURAR GRID
        // =====================================================================
        public void Mostrar()
        {
            try
            {
                DataTable datos = CN_Producto.Listar();
                datos.DefaultView.Sort = "idproducto ASC";
                dgvProductos.DataSource = datos.DefaultView.ToTable();

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

            // ── Ocultar ───────────────────────────────────────────────────────
            string[] ocultas = { "precio_base", "precio_minimo", "total_vendido",
                                  "fecha_ultimo_ajuste", "fecha_creacion", "idcategoria", "idproveedor" };
            foreach (string col in ocultas)
                if (dgvProductos.Columns.Contains(col))
                    dgvProductos.Columns[col].Visible = false;

            // ── Encabezados ───────────────────────────────────────────────────
            if (dgvProductos.Columns.Contains("idproducto")) dgvProductos.Columns["idproducto"].HeaderText = "ID";
            if (dgvProductos.Columns.Contains("nombre")) dgvProductos.Columns["nombre"].HeaderText = "Nombre";
            if (dgvProductos.Columns.Contains("descripcion")) dgvProductos.Columns["descripcion"].HeaderText = "Descripción";
            if (dgvProductos.Columns.Contains("precio"))
            {
                dgvProductos.Columns["precio"].HeaderText = "Precio";
                dgvProductos.Columns["precio"].DefaultCellStyle.Format = "C2";
            }
            if (dgvProductos.Columns.Contains("stock")) dgvProductos.Columns["stock"].HeaderText = "Stock";
            if (dgvProductos.Columns.Contains("estado")) dgvProductos.Columns["estado"].HeaderText = "Estado";
            if (dgvProductos.Columns.Contains("categoria")) dgvProductos.Columns["categoria"].HeaderText = "Categoría";
            if (dgvProductos.Columns.Contains("es_medicamento")) dgvProductos.Columns["es_medicamento"].HeaderText = "Medicamento";
            if (dgvProductos.Columns.Contains("fecha_vencimiento"))
            {
                dgvProductos.Columns["fecha_vencimiento"].HeaderText = "Vencimiento";
                dgvProductos.Columns["fecha_vencimiento"].DefaultCellStyle.Format = "dd/MM/yyyy";
            }
            if (dgvProductos.Columns.Contains("nivel_stock")) dgvProductos.Columns["nivel_stock"].HeaderText = "Nivel Stock";
            if (dgvProductos.Columns.Contains("porcentaje_cambio"))
            {
                dgvProductos.Columns["porcentaje_cambio"].HeaderText = "Cambio %";
                dgvProductos.Columns["porcentaje_cambio"].DefaultCellStyle.Format = "0.00'%'";
            }
            if (dgvProductos.Columns.Contains("codigo_barras"))
            {
                dgvProductos.Columns["codigo_barras"].HeaderText = "Cód. Barras";
                dgvProductos.Columns["codigo_barras"].Width = 130;
            }
            if (dgvProductos.Columns.Contains("proveedor"))
            {
                dgvProductos.Columns["proveedor"].HeaderText = "Proveedor";
                dgvProductos.Columns["proveedor"].Width = 140;
            }

            // ── Anchos ────────────────────────────────────────────────────────
            if (dgvProductos.Columns.Contains("idproducto")) dgvProductos.Columns["idproducto"].Width = 50;
            if (dgvProductos.Columns.Contains("precio")) dgvProductos.Columns["precio"].Width = 100;
            if (dgvProductos.Columns.Contains("stock")) dgvProductos.Columns["stock"].Width = 70;
            if (dgvProductos.Columns.Contains("estado")) dgvProductos.Columns["estado"].Width = 90;
            if (dgvProductos.Columns.Contains("categoria")) dgvProductos.Columns["categoria"].Width = 120;
            if (dgvProductos.Columns.Contains("es_medicamento")) dgvProductos.Columns["es_medicamento"].Width = 100;
            if (dgvProductos.Columns.Contains("nivel_stock")) dgvProductos.Columns["nivel_stock"].Width = 120;
        }

        private void AplicarColoresStock()
        {
            foreach (DataGridViewRow row in dgvProductos.Rows)
            {
                if (dgvProductos.Columns.Contains("nivel_stock") &&
                    row.Cells["nivel_stock"].Value != null)
                {
                    switch (row.Cells["nivel_stock"].Value.ToString())
                    {
                        case "SIN STOCK":
                            row.DefaultCellStyle.BackColor = Color.FromArgb(255, 230, 230);
                            row.DefaultCellStyle.ForeColor = Color.FromArgb(192, 57, 43);
                            break;
                        case "STOCK BAJO":
                            row.DefaultCellStyle.BackColor = Color.FromArgb(255, 243, 224);
                            row.DefaultCellStyle.ForeColor = Color.FromArgb(230, 126, 34);
                            break;
                        case "STOCK MEDIO":
                            row.DefaultCellStyle.BackColor = Color.FromArgb(255, 250, 205);
                            row.DefaultCellStyle.ForeColor = Color.FromArgb(241, 196, 15);
                            break;
                        case "STOCK SUFICIENTE":
                            row.DefaultCellStyle.BackColor = Color.FromArgb(232, 248, 245);
                            row.DefaultCellStyle.ForeColor = Color.FromArgb(22, 160, 133);
                            break;
                    }
                }

                if (dgvProductos.Columns.Contains("porcentaje_cambio") &&
                    row.Cells["porcentaje_cambio"].Value != null)
                {
                    decimal cambio = Convert.ToDecimal(row.Cells["porcentaje_cambio"].Value);
                    row.Cells["porcentaje_cambio"].Style.ForeColor =
                        cambio > 0 ? Color.FromArgb(46, 204, 113) :
                        cambio < 0 ? Color.FromArgb(231, 76, 60) : Color.Gray;
                }
            }
        }

        private void ActualizarContador()
        {
            int total = dgvProductos.Rows.Count;
            int stockBajo = 0, sinStock = 0;

            foreach (DataGridViewRow row in dgvProductos.Rows)
            {
                if (!dgvProductos.Columns.Contains("nivel_stock") ||
                    row.Cells["nivel_stock"].Value == null) continue;
                string nivel = row.Cells["nivel_stock"].Value.ToString();
                if (nivel == "STOCK BAJO") stockBajo++;
                if (nivel == "SIN STOCK") sinStock++;
            }

            lblTotal.Text = $"Total: {total} productos | Stock bajo: {stockBajo} | Sin stock: {sinStock}";
        }

        // =====================================================================
        // CATEGORÍAS Y ALERTAS
        // =====================================================================
        private void CargarCategorias()
        {
            try
            {
                DataTable dt = CN_Categoria.ListarActivas().Copy();
                DataRow row = dt.NewRow();
                row["idcategoria"] = 0;
                row["nombre"] = "-- Todas las categorías --";
                dt.Rows.InsertAt(row, 0);

                cmbCategoria.SelectedIndexChanged -= cmbCategoria_SelectedIndexChanged;
                cmbCategoria.DataSource = null;
                cmbCategoria.DataSource = dt;
                cmbCategoria.DisplayMember = "nombre";
                cmbCategoria.ValueMember = "idcategoria";
                cmbCategoria.SelectedIndex = 0;
                cmbCategoria.SelectedIndexChanged += cmbCategoria_SelectedIndexChanged;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar categorías: " + ex.Message,
                    "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void VerificarAlertas()
        {
            DataTable stockBajo = CN_Producto.ObtenerProductosStockBajo();
            if (stockBajo != null && stockBajo.Rows.Count > 0)
            {
                lblAlertaStock.Text = $"⚠️ {stockBajo.Rows.Count} producto(s) con stock bajo";
                lblAlertaStock.ForeColor = Color.FromArgb(230, 126, 34);
                lblAlertaStock.Visible = true;
            }
            else lblAlertaStock.Visible = false;

            DataTable proxVencer = CN_Producto.ObtenerProductosProximosVencer();
            if (proxVencer != null && proxVencer.Rows.Count > 0)
            {
                lblAlertaVencimiento.Text = $"⚠️ {proxVencer.Rows.Count} producto(s) próximo(s) a vencer";
                lblAlertaVencimiento.ForeColor = Color.FromArgb(231, 76, 60);
                lblAlertaVencimiento.Visible = true;
            }
            else lblAlertaVencimiento.Visible = false;
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
                    DataTable datos = CN_Producto.BuscarNombre(txtBuscar.Text);
                    dgvProductos.DataSource = datos;
                    ConfigurarColumnas();
                    AplicarColoresStock();
                    ActualizarContador();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en la búsqueda: " + ex.Message,
                    "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_cargandoCategorias || cmbCategoria.SelectedIndex < 0) return;
            try
            {
                DataRowView drv = cmbCategoria.SelectedItem as DataRowView;
                if (drv == null) return;
                int idcategoria = Convert.ToInt32(drv["idcategoria"]);

                DataTable datos = idcategoria == 0
                    ? CN_Producto.Listar()
                    : CN_Producto.BuscarCategoria(idcategoria);

                dgvProductos.DataSource = datos;
                ConfigurarColumnas();
                AplicarColoresStock();
                ActualizarContador();
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
            cmbCategoria.SelectedIndex = 0;
            Mostrar();
        }

        // =====================================================================
        // TOOLTIP VISUAL — POPUP CON IMAGEN DEL CÓDIGO DE BARRAS
        // =====================================================================
        private void dgvProductos_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex == _ultimaFilaTooltip) return;
            _ultimaFilaTooltip = e.RowIndex;
            CerrarPopupBarcode();

            if (!dgvProductos.Columns.Contains("codigo_barras")) return;

            string codigo = dgvProductos.Rows[e.RowIndex]
                .Cells["codigo_barras"].Value?.ToString() ?? "";

            if (!EAN13Util.EsValido(codigo)) return;

            Rectangle cellRect = dgvProductos.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
            Point screenPos = dgvProductos.PointToScreen(new Point(cellRect.Right + 5, cellRect.Top));

            _popupBarcode = new Form
            {
                FormBorderStyle = FormBorderStyle.None,
                StartPosition = FormStartPosition.Manual,
                Location = screenPos,
                Size = new Size(230, 95),
                BackColor = Color.White,
                ShowInTaskbar = false,
                TopMost = true
            };

            PictureBox pic = new PictureBox
            {
                Dock = DockStyle.Fill,
                SizeMode = PictureBoxSizeMode.StretchImage,
                Image = EAN13Util.GenerarImagen(codigo, 230, 95, mostrarNumero: true)
            };

            _popupBarcode.Controls.Add(pic);
            _popupBarcode.Show();
        }

        private void dgvProductos_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            _ultimaFilaTooltip = -1;
            CerrarPopupBarcode();
        }

        private void CerrarPopupBarcode()
        {
            if (_popupBarcode == null) return;
            _popupBarcode.Close();
            _popupBarcode.Dispose();
            _popupBarcode = null;
        }

        // =====================================================================
        // BOTONES CRUD
        // =====================================================================
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            if (!cnUsuario.PuedeCrear(FrmLogin.RolActual, "Productos"))
            {
                MessageBox.Show("No tiene permisos para agregar productos",
                    "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            FrmRegistrarProducto form = new FrmRegistrarProducto();
            form.Insert = true;

            if (form.ShowDialog() == DialogResult.OK)
            {
                Mostrar();
                VerificarAlertas();
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (!cnUsuario.PuedeEditar(FrmLogin.RolActual, "Productos"))
            {
                MessageBox.Show("No tiene permisos para editar productos",
                    "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

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

            string estado = row.Cells["estado"].Value.ToString();
            if (estado == "ACTIVO") form.rbtnActivo.Checked = true;
            else form.rbtnInactivo.Checked = true;

            if (row.Cells["fecha_vencimiento"].Value != DBNull.Value)
            {
                form.dtpVencimiento.Value = Convert.ToDateTime(row.Cells["fecha_vencimiento"].Value);
                form.dtpVencimiento.Enabled = true;
            }

            // Pasar código de barras al formulario de edición
            int idprod = Convert.ToInt32(row.Cells["idproducto"].Value);
            string codigo = dgvProductos.Columns.Contains("codigo_barras")
                ? row.Cells["codigo_barras"].Value?.ToString() ?? ""
                : "";
            form.SetCodigoBarras(codigo, idprod);
            // Pasar proveedor si la columna existe
            if (dgvProductos.Columns.Contains("idproveedor") && row.Cells["idproveedor"].Value != DBNull.Value && row.Cells["idproveedor"].Value != null)
                form.IdProveedorSeleccionado = Convert.ToInt32(row.Cells["idproveedor"].Value);

            if (form.ShowDialog() == DialogResult.OK)
            {
                Mostrar();
                VerificarAlertas();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (!cnUsuario.PuedeEliminar(FrmLogin.RolActual, "Productos"))
            {
                MessageBox.Show("No tiene permisos para eliminar productos",
                    "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dgvProductos.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione un producto para eliminar",
                    "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string nombre = dgvProductos.CurrentRow.Cells["nombre"].Value.ToString();

            if (MessageBox.Show(
                    $"¿Desactivar el producto?\n\nProducto: {nombre}\n\nEl producto quedará INACTIVO y no aparecerá en ventas.",
                    "Sistema Veterinaria", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                == DialogResult.Yes)
            {
                try
                {
                    int idproducto = Convert.ToInt32(dgvProductos.CurrentRow.Cells["idproducto"].Value);
                    string resultado = CN_Producto.Eliminar(idproducto);

                    if (resultado == "OK")
                    {
                        MessageBox.Show("✅ Producto desactivado correctamente",
                            "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Mostrar();
                        VerificarAlertas();
                    }
                    else
                        MessageBox.Show(resultado,
                            "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar: " + ex.Message,
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

            int idproducto = Convert.ToInt32(dgvProductos.CurrentRow.Cells["idproducto"].Value);
            string nombreProducto = dgvProductos.CurrentRow.Cells["nombre"].Value.ToString();
            FrmHistorialPrecios form = new FrmHistorialPrecios(idproducto, nombreProducto);
            form.ShowDialog();
        }

        // ── Botón Etiqueta ────────────────────────────────────────────────────
        private void btnEtiqueta_Click(object sender, EventArgs e)
        {
            if (dgvProductos.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione un producto para ver su etiqueta.",
                    "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow row = dgvProductos.CurrentRow;
            int idproducto = Convert.ToInt32(row.Cells["idproducto"].Value);
            string nombre = row.Cells["nombre"].Value.ToString();
            decimal precio = Convert.ToDecimal(row.Cells["precio"].Value);
            string codigo = dgvProductos.Columns.Contains("codigo_barras")
                ? row.Cells["codigo_barras"].Value?.ToString() ?? ""
                : EAN13Util.Generar(idproducto);

            FrmEtiquetaProducto frm = new FrmEtiquetaProducto(idproducto, nombre, precio, codigo);
            frm.ShowDialog(this);
        }

        private void dgvProductos_DoubleClick(object sender, EventArgs e)
        {
            if (cnUsuario.PuedeEditar(FrmLogin.RolActual, "Productos"))
                btnEditar_Click(sender, e);
        }
    }
}