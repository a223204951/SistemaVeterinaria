using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using CapaNegocio;

namespace CapaPresentacion
{
    /// <summary>
    /// FORMULARIO DE LISTADO DE PRODUCTOS
    /// Muestra todos los productos con sistema de precios dinámicos
    /// Control de permisos según el rol del usuario
    /// ACTUALIZADO: Usa categorías desde la tabla categoria_producto
    /// </summary>
    public partial class FrmListadoProductos : Form
    {
        // =============================================
        // INSTANCIA DE LA CAPA DE NEGOCIO PARA VERIFICAR PERMISOS
        // =============================================
        private CN_Usuario cnUsuario = new CN_Usuario();

        public FrmListadoProductos()
        {
            InitializeComponent();
        }

        /// <summary>
        /// EVENTO LOAD DEL FORMULARIO
        /// Configura permisos y carga los datos iniciales
        /// </summary>
        private void FrmListadoProductos_Load(object sender, EventArgs e)
        {
            // CARGAR TODAS LOS PRODUCTOS
            Mostrar();

            // CONFIGURAR PERMISOS SEGÚN EL ROL
            ConfigurarPermisos();

            // CARGAR CATEGORÍAS EN EL COMBOBOX
            CargarCategorias();

            // VERIFICAR ALERTAS
            VerificarAlertas();
        }

        /// <summary>
        /// MÉTODO PARA CONFIGURAR PERMISOS SEGÚN EL ROL
        /// </summary>
        private void ConfigurarPermisos()
        {
            string rol = FrmLogin.RolActual;

            // OBTENER PERMISOS
            bool puedeCrear = cnUsuario.PuedeCrear(rol, "Productos");
            bool puedeEditar = cnUsuario.PuedeEditar(rol, "Productos");
            bool puedeEliminar = cnUsuario.PuedeEliminar(rol, "Productos");

            // MOSTRAR U OCULTAR BOTONES
            btnNuevo.Visible = puedeCrear;
            btnEditar.Visible = puedeEditar;
            btnEliminar.Visible = puedeEliminar;

            // BOTONES ESPECIALES
            // Solo ADMIN y CAJERO pueden ver historial de precios
            btnHistorialPrecios.Visible = (rol == "ADMINISTRADOR" || rol == "CAJERO");

            // SI NO TIENE PERMISOS DE EDICIÓN, HACER EL GRID DE SOLO LECTURA
            if (!puedeEditar && !puedeEliminar)
            {
                dgvProductos.ReadOnly = true;
            }
        }

        /// <summary>
        /// MÉTODO PÚBLICO PARA MOSTRAR TODOS LOS PRODUCTOS
        /// </summary>
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
                    "Sistema Veterinaria",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// MÉTODO PARA CONFIGURAR APARIENCIA DE COLUMNAS
        /// </summary>
        private void ConfigurarColumnas()
        {
            if (dgvProductos.Columns.Count > 0)
            {
                // OCULTAR COLUMNAS INNECESARIAS
                if (dgvProductos.Columns.Contains("precio_base"))
                    dgvProductos.Columns["precio_base"].Visible = false;
                if (dgvProductos.Columns.Contains("precio_minimo"))
                    dgvProductos.Columns["precio_minimo"].Visible = false;
                if (dgvProductos.Columns.Contains("total_vendido"))
                    dgvProductos.Columns["total_vendido"].Visible = false;
                if (dgvProductos.Columns.Contains("fecha_ultimo_ajuste"))
                    dgvProductos.Columns["fecha_ultimo_ajuste"].Visible = false;
                if (dgvProductos.Columns.Contains("fecha_creacion"))
                    dgvProductos.Columns["fecha_creacion"].Visible = false;
                if (dgvProductos.Columns.Contains("idcategoria"))
                    dgvProductos.Columns["idcategoria"].Visible = false;

                // RENOMBRAR ENCABEZADOS
                if (dgvProductos.Columns.Contains("idproducto"))
                    dgvProductos.Columns["idproducto"].HeaderText = "ID";
                if (dgvProductos.Columns.Contains("nombre"))
                    dgvProductos.Columns["nombre"].HeaderText = "Nombre";
                if (dgvProductos.Columns.Contains("descripcion"))
                    dgvProductos.Columns["descripcion"].HeaderText = "Descripción";
                if (dgvProductos.Columns.Contains("precio"))
                {
                    dgvProductos.Columns["precio"].HeaderText = "Precio";
                    dgvProductos.Columns["precio"].DefaultCellStyle.Format = "C2"; // Formato moneda
                }
                if (dgvProductos.Columns.Contains("stock"))
                    dgvProductos.Columns["stock"].HeaderText = "Stock";
                if (dgvProductos.Columns.Contains("estado"))
                    dgvProductos.Columns["estado"].HeaderText = "Estado";
                if (dgvProductos.Columns.Contains("categoria"))
                    dgvProductos.Columns["categoria"].HeaderText = "Categoría";
                if (dgvProductos.Columns.Contains("es_medicamento"))
                    dgvProductos.Columns["es_medicamento"].HeaderText = "Medicamento";
                if (dgvProductos.Columns.Contains("fecha_vencimiento"))
                {
                    dgvProductos.Columns["fecha_vencimiento"].HeaderText = "Vencimiento";
                    dgvProductos.Columns["fecha_vencimiento"].DefaultCellStyle.Format = "dd/MM/yyyy";
                }
                if (dgvProductos.Columns.Contains("nivel_stock"))
                    dgvProductos.Columns["nivel_stock"].HeaderText = "Nivel Stock";
                if (dgvProductos.Columns.Contains("porcentaje_cambio"))
                {
                    dgvProductos.Columns["porcentaje_cambio"].HeaderText = "Cambio %";
                    dgvProductos.Columns["porcentaje_cambio"].DefaultCellStyle.Format = "0.00'%'";
                }

                // AJUSTAR ANCHOS
                dgvProductos.Columns["idproducto"].Width = 50;
                dgvProductos.Columns["precio"].Width = 100;
                dgvProductos.Columns["stock"].Width = 70;
                dgvProductos.Columns["estado"].Width = 90;
                dgvProductos.Columns["categoria"].Width = 120;
                dgvProductos.Columns["es_medicamento"].Width = 100;
                dgvProductos.Columns["nivel_stock"].Width = 120;
            }
        }

        /// <summary>
        /// MÉTODO PARA APLICAR COLORES SEGÚN EL NIVEL DE STOCK
        /// </summary>
        private void AplicarColoresStock()
        {
            foreach (DataGridViewRow row in dgvProductos.Rows)
            {
                if (row.Cells["nivel_stock"].Value != null)
                {
                    string nivelStock = row.Cells["nivel_stock"].Value.ToString();

                    switch (nivelStock)
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
        /// MÉTODO PARA ACTUALIZAR CONTADOR DE PRODUCTOS
        /// </summary>
        private void ActualizarContador()
        {
            int total = dgvProductos.Rows.Count;
            int stockBajo = 0;
            int sinStock = 0;

            foreach (DataGridViewRow row in dgvProductos.Rows)
            {
                if (row.Cells["nivel_stock"].Value != null)
                {
                    string nivel = row.Cells["nivel_stock"].Value.ToString();
                    if (nivel == "STOCK BAJO") stockBajo++;
                    if (nivel == "SIN STOCK") sinStock++;
                }
            }

            lblTotal.Text = $"Total: {total} productos | Stock bajo: {stockBajo} | Sin stock: {sinStock}";
        }

        /// <summary>
        /// MÉTODO PARA CARGAR CATEGORÍAS EN EL COMBOBOX
        /// ACTUALIZADO: Carga desde la tabla categoria_producto
        /// </summary>
        private void CargarCategorias()
        {
            try
            {
                DataTable categorias = CN_Categoria.ListarActivas();

                // CREAR UNA COPIA Y AGREGAR "TODAS"
                DataTable dt = categorias.Copy();
                DataRow row = dt.NewRow();
                row["idcategoria"] = 0;
                row["nombre"] = "-- Todas las categorías --";
                dt.Rows.InsertAt(row, 0);

                cmbCategoria.DataSource = dt;
                cmbCategoria.DisplayMember = "nombre";
                cmbCategoria.ValueMember = "idcategoria";
                cmbCategoria.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar categorías: " + ex.Message,
                    "Sistema Veterinaria",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// MÉTODO PARA VERIFICAR ALERTAS DE STOCK Y VENCIMIENTO
        /// </summary>
        private void VerificarAlertas()
        {
            // VERIFICAR STOCK BAJO
            DataTable stockBajo = CN_Producto.ObtenerProductosStockBajo();
            if (stockBajo != null && stockBajo.Rows.Count > 0)
            {
                lblAlertaStock.Text = $"⚠️ {stockBajo.Rows.Count} producto(s) con stock bajo";
                lblAlertaStock.ForeColor = Color.FromArgb(230, 126, 34);
                lblAlertaStock.Visible = true;
            }
            else
            {
                lblAlertaStock.Visible = false;
            }

            // VERIFICAR PRODUCTOS PRÓXIMOS A VENCER
            DataTable proximosVencer = CN_Producto.ObtenerProductosProximosVencer();
            if (proximosVencer != null && proximosVencer.Rows.Count > 0)
            {
                lblAlertaVencimiento.Text = $"⚠️ {proximosVencer.Rows.Count} producto(s) próximo(s) a vencer";
                lblAlertaVencimiento.ForeColor = Color.FromArgb(231, 76, 60);
                lblAlertaVencimiento.Visible = true;
            }
            else
            {
                lblAlertaVencimiento.Visible = false;
            }
        }

        // =============================================
        // EVENTOS DE BÚSQUEDA
        // =============================================

        /// <summary>
        /// EVENTO TEXTCHANGED PARA BÚSQUEDA EN TIEMPO REAL
        /// </summary>
        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            BuscarProducto();
        }

        /// <summary>
        /// MÉTODO PARA BUSCAR PRODUCTOS
        /// </summary>
        private void BuscarProducto()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtBuscar.Text))
                {
                    Mostrar();
                }
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
                    "Sistema Veterinaria",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// EVENTO SELECTEDINDEXCHANGED DEL COMBOBOX DE CATEGORÍAS
        /// ACTUALIZADO: Usa el ID de categoría
        /// </summary>
        private void cmbCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCategoria.SelectedIndex < 0)
                return;

            try
            {
                int idcategoria = Convert.ToInt32(cmbCategoria.SelectedValue);

                if (idcategoria == 0)
                {
                    Mostrar();
                }
                else
                {
                    DataTable datos = CN_Producto.BuscarCategoria(idcategoria);
                    dgvProductos.DataSource = datos;
                    ConfigurarColumnas();
                    AplicarColoresStock();
                    ActualizarContador();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al filtrar: " + ex.Message,
                    "Sistema Veterinaria",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// EVENTO CLICK DEL BOTÓN LIMPIAR
        /// </summary>
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtBuscar.Clear();
            cmbCategoria.SelectedIndex = 0;
            Mostrar();
        }

        // =============================================
        // EVENTOS DE BOTONES CRUD
        // =============================================

        /// <summary>
        /// EVENTO CLICK DEL BOTÓN NUEVO
        /// </summary>
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            if (!cnUsuario.PuedeCrear(FrmLogin.RolActual, "Productos"))
            {
                MessageBox.Show("No tiene permisos para agregar productos",
                    "Sistema Veterinaria",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
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

        /// <summary>
        /// EVENTO CLICK DEL BOTÓN EDITAR
        /// </summary>
        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (!cnUsuario.PuedeEditar(FrmLogin.RolActual, "Productos"))
            {
                MessageBox.Show("No tiene permisos para editar productos",
                    "Sistema Veterinaria",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            if (dgvProductos.SelectedRows.Count > 0)
            {
                FrmRegistrarProducto form = new FrmRegistrarProducto();
                form.Edit = true;

                // CARGAR DATOS DEL PRODUCTO SELECCIONADO
                form.txtIdProducto.Text = dgvProductos.CurrentRow.Cells["idproducto"].Value.ToString();
                form.txtNombre.Text = dgvProductos.CurrentRow.Cells["nombre"].Value.ToString();
                form.txtDescripcion.Text = dgvProductos.CurrentRow.Cells["descripcion"].Value.ToString();
                form.nudPrecio.Value = Convert.ToDecimal(dgvProductos.CurrentRow.Cells["precio"].Value);
                form.nudStock.Value = Convert.ToDecimal(dgvProductos.CurrentRow.Cells["stock"].Value);

                // SELECCIONAR CATEGORÍA POR ID
                int idcategoria = Convert.ToInt32(dgvProductos.CurrentRow.Cells["idcategoria"].Value);
                form.IdCategoriaSeleccionada = idcategoria;

                form.chkEsMedicamento.Checked = Convert.ToBoolean(dgvProductos.CurrentRow.Cells["es_medicamento"].Value);

                // CONFIGURAR ESTADO
                string estado = dgvProductos.CurrentRow.Cells["estado"].Value.ToString();
                if (estado == "ACTIVO")
                    form.rbtnActivo.Checked = true;
                else
                    form.rbtnInactivo.Checked = true;

                // CONFIGURAR FECHA DE VENCIMIENTO
                if (dgvProductos.CurrentRow.Cells["fecha_vencimiento"].Value != DBNull.Value)
                {
                    form.dtpVencimiento.Value = Convert.ToDateTime(dgvProductos.CurrentRow.Cells["fecha_vencimiento"].Value);
                    form.dtpVencimiento.Enabled = true;
                }

                if (form.ShowDialog() == DialogResult.OK)
                {
                    Mostrar();
                    VerificarAlertas();
                }
            }
            else
            {
                MessageBox.Show("Seleccione un producto para editar",
                    "Sistema Veterinaria",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// EVENTO CLICK DEL BOTÓN ELIMINAR
        /// </summary>
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (!cnUsuario.PuedeEliminar(FrmLogin.RolActual, "Productos"))
            {
                MessageBox.Show("No tiene permisos para eliminar productos",
                    "Sistema Veterinaria",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            if (dgvProductos.SelectedRows.Count > 0)
            {
                string nombreProducto = dgvProductos.CurrentRow.Cells["nombre"].Value.ToString();

                DialogResult opcion = MessageBox.Show(
                    $"¿Está seguro que desea dar de baja el producto?\n\n" +
                    $"Producto: {nombreProducto}\n\n" +
                    $"Nota: El producto será marcado como INACTIVO.",
                    "Sistema Veterinaria",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (opcion == DialogResult.Yes)
                {
                    try
                    {
                        int idproducto = Convert.ToInt32(dgvProductos.CurrentRow.Cells["idproducto"].Value);
                        string resultado = CN_Producto.Eliminar(idproducto);

                        if (resultado == "OK")
                        {
                            MessageBox.Show("✅ Producto dado de baja correctamente",
                                "Sistema Veterinaria",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                            Mostrar();
                            VerificarAlertas();
                        }
                        else
                        {
                            MessageBox.Show(resultado,
                                "Sistema Veterinaria",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al eliminar: " + ex.Message,
                            "Sistema Veterinaria",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Seleccione un producto para eliminar",
                    "Sistema Veterinaria",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// EVENTO CLICK DEL BOTÓN HISTORIAL DE PRECIOS
        /// </summary>
        private void btnHistorialPrecios_Click(object sender, EventArgs e)
        {
            if (dgvProductos.SelectedRows.Count > 0)
            {
                int idproducto = Convert.ToInt32(dgvProductos.CurrentRow.Cells["idproducto"].Value);
                string nombreProducto = dgvProductos.CurrentRow.Cells["nombre"].Value.ToString();

                FrmHistorialPrecios form = new FrmHistorialPrecios(idproducto, nombreProducto);
                form.ShowDialog();
            }
            else
            {
                MessageBox.Show("Seleccione un producto para ver su historial",
                    "Sistema Veterinaria",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// EVENTO DOUBLECLICK EN EL DATAGRIDVIEW
        /// </summary>
        private void dgvProductos_DoubleClick(object sender, EventArgs e)
        {
            if (cnUsuario.PuedeEditar(FrmLogin.RolActual, "Productos"))
            {
                btnEditar_Click(sender, e);
            }
        }
    }
}