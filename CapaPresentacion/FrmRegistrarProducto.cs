using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using CapaNegocio;

namespace CapaPresentacion
{
    /// <summary>
    /// FORMULARIO PARA REGISTRAR Y EDITAR PRODUCTOS
    /// Permite agregar nuevos productos o modificar existentes
    /// ACTUALIZADO: Usa categorías desde la tabla categoria_producto
    /// </summary>
    public partial class FrmRegistrarProducto : Form
    {
        // =============================================
        // BANDERAS PARA INDICAR SI ES INSERCIÓN O EDICIÓN
        // =============================================
        public bool Insert = false;
        public bool Edit = false;

        // =============================================
        // PROPIEDAD PARA ESTABLECER LA CATEGORÍA SELECCIONADA AL EDITAR
        // =============================================
        public int IdCategoriaSeleccionada { get; set; }

        public FrmRegistrarProducto()
        {
            InitializeComponent();
        }

        /// <summary>
        /// EVENTO LOAD DEL FORMULARIO
        /// </summary>
        private void FrmRegistrarProducto_Load(object sender, EventArgs e)
        {
            // CARGAR CATEGORÍAS
            CargarCategorias();

            // CONFIGURAR SEGÚN LA OPERACIÓN
            if (Insert)
            {
                lblTitulo.Text = "📝 Registrar Nuevo Producto";
                rbtnActivo.Checked = true;
                dtpVencimiento.Enabled = false;
                nudPrecio.Value = 1;
                nudStock.Value = 0;
            }
            else if (Edit)
            {
                lblTitulo.Text = "✏️ Editar Producto";

                // SELECCIONAR LA CATEGORÍA
                if (IdCategoriaSeleccionada > 0)
                {
                    cmbCategoria.SelectedValue = IdCategoriaSeleccionada;
                }
            }

            // CONFIGURAR TOOLTIPS
            ConfigurarTooltips();
        }

        /// <summary>
        /// MÉTODO PARA CARGAR CATEGORÍAS
        /// ACTUALIZADO: Carga desde la tabla categoria_producto
        /// </summary>
        private void CargarCategorias()
        {
            try
            {
                DataTable categorias = CN_Categoria.ListarActivas();

                if (categorias != null && categorias.Rows.Count > 0)
                {
                    cmbCategoria.DataSource = categorias;
                    cmbCategoria.DisplayMember = "nombre";
                    cmbCategoria.ValueMember = "idcategoria";
                    cmbCategoria.SelectedIndex = -1;
                }
                else
                {
                    MessageBox.Show("⚠️ No hay categorías registradas en el sistema.\n\n" +
                        "Por favor, registre al menos una categoría antes de agregar productos.",
                        "Sistema Veterinaria",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
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
        /// MÉTODO PARA CONFIGURAR TOOLTIPS
        /// </summary>
        private void ConfigurarTooltips()
        {
            ToolTip tooltip = new ToolTip();
            tooltip.SetToolTip(txtNombre, "Nombre del producto (obligatorio)");
            tooltip.SetToolTip(nudPrecio, "Precio en pesos mexicanos (MXN)");
            tooltip.SetToolTip(nudStock, "Cantidad en inventario");
            tooltip.SetToolTip(cmbCategoria, "Categoría del producto");
            tooltip.SetToolTip(chkEsMedicamento, "Marcar si es un medicamento (requiere fecha de vencimiento)");
            tooltip.SetToolTip(dtpVencimiento, "Fecha de vencimiento (obligatorio para medicamentos)");

            // TOOLTIP ESPECIAL PARA PRECIOS DINÁMICOS
            ToolTip tooltipPrecio = new ToolTip();
            tooltipPrecio.SetToolTip(lblInfoPrecio,
                "⚠️ SISTEMA DE PRECIOS DINÁMICOS:\n" +
                "• Cuando se vende: +10% precio\n" +
                "• Productos no vendidos: -10% precio\n" +
                "• Precio mínimo: $1.00 MXN\n" +
                "• Compra múltiple: todos +10%");
        }

        /// <summary>
        /// EVENTO CLICK DEL BOTÓN GUARDAR
        /// </summary>
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // =============================================
            // VALIDACIONES
            // =============================================

            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("⚠️ Por favor, ingrese el nombre del producto",
                    "Sistema Veterinaria",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                txtNombre.Focus();
                return;
            }

            if (cmbCategoria.SelectedIndex < 0)
            {
                MessageBox.Show("⚠️ Por favor, seleccione una categoría",
                    "Sistema Veterinaria",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                cmbCategoria.Focus();
                return;
            }

            if (nudPrecio.Value <= 0)
            {
                MessageBox.Show("⚠️ El precio debe ser mayor a $0",
                    "Sistema Veterinaria",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                nudPrecio.Focus();
                return;
            }

            if (chkEsMedicamento.Checked && !dtpVencimiento.Enabled)
            {
                MessageBox.Show("⚠️ Los medicamentos deben tener fecha de vencimiento",
                    "Sistema Veterinaria",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // =============================================
                // OBTENER VALORES
                // =============================================
                string nombre = txtNombre.Text.Trim();
                string descripcion = txtDescripcion.Text.Trim();
                decimal precio = nudPrecio.Value;
                int stock = Convert.ToInt32(nudStock.Value);
                int idcategoria = Convert.ToInt32(cmbCategoria.SelectedValue);
                bool esMedicamento = chkEsMedicamento.Checked;
                DateTime? fechaVencimiento = esMedicamento && dtpVencimiento.Enabled ? dtpVencimiento.Value : (DateTime?)null;
                string estado = rbtnActivo.Checked ? "ACTIVO" : "INACTIVO";

                string resultado;

                if (Insert)
                {
                    // =============================================
                    // INSERTAR NUEVO PRODUCTO
                    // =============================================
                    resultado = CN_Producto.Guardar(nombre, descripcion, precio, stock, estado,
                                                   idcategoria, esMedicamento, fechaVencimiento);

                    if (resultado == "OK")
                    {
                        MessageBox.Show("✅ Producto registrado correctamente\n\n" +
                            $"Nombre: {nombre}\n" +
                            $"Precio inicial: ${precio:N2} MXN\n" +
                            $"Stock: {stock} unidades\n" +
                            $"Categoría: {cmbCategoria.Text}\n\n" +
                            $"💡 El precio se ajustará automáticamente según las ventas",
                            "Sistema Veterinaria",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("❌ " + resultado,
                            "Sistema Veterinaria",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                }
                else if (Edit)
                {
                    // =============================================
                    // EDITAR PRODUCTO EXISTENTE
                    // =============================================
                    int idproducto = Convert.ToInt32(txtIdProducto.Text);
                    resultado = CN_Producto.Editar(idproducto, nombre, descripcion, precio, stock,
                                                  estado, idcategoria, esMedicamento, fechaVencimiento);

                    if (resultado == "OK")
                    {
                        MessageBox.Show("✅ Producto actualizado correctamente\n\n" +
                            $"Nombre: {nombre}\n" +
                            $"Precio actual: ${precio:N2} MXN\n" +
                            $"Stock: {stock} unidades\n" +
                            $"Categoría: {cmbCategoria.Text}",
                            "Sistema Veterinaria",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("❌ " + resultado,
                            "Sistema Veterinaria",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Error al guardar: " + ex.Message,
                    "Sistema Veterinaria",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// EVENTO CLICK DEL BOTÓN CANCELAR
        /// </summary>
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtNombre.Text) || cmbCategoria.SelectedIndex >= 0)
            {
                DialogResult resultado = MessageBox.Show(
                    "¿Está seguro que desea cancelar?\n\n" +
                    "Los cambios no guardados se perderán.",
                    "Sistema Veterinaria",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (resultado == DialogResult.Yes)
                {
                    this.DialogResult = DialogResult.Cancel;
                    this.Close();
                }
            }
            else
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }

        /// <summary>
        /// EVENTO CHECKEDCHANGED DEL CHECKBOX ES MEDICAMENTO
        /// Habilita/deshabilita la fecha de vencimiento
        /// </summary>
        private void chkEsMedicamento_CheckedChanged(object sender, EventArgs e)
        {
            dtpVencimiento.Enabled = chkEsMedicamento.Checked;

            if (chkEsMedicamento.Checked)
            {
                dtpVencimiento.Value = DateTime.Now.AddYears(1);
                lblVencimiento.ForeColor = Color.FromArgb(231, 76, 60);
            }
            else
            {
                lblVencimiento.ForeColor = Color.FromArgb(149, 165, 166);
            }
        }

        /// <summary>
        /// EVENTO VALUECHANGED DEL NUMERICUPDOWN DE PRECIO
        /// Muestra información sobre el precio
        /// </summary>
        private void nudPrecio_ValueChanged(object sender, EventArgs e)
        {
            if (nudPrecio.Value < 10)
            {
                lblInfoPrecio.Text = "💰 Precio bajo";
                lblInfoPrecio.ForeColor = Color.FromArgb(52, 152, 219);
            }
            else if (nudPrecio.Value >= 10 && nudPrecio.Value < 100)
            {
                lblInfoPrecio.Text = "💰 Precio moderado";
                lblInfoPrecio.ForeColor = Color.FromArgb(46, 204, 113);
            }
            else if (nudPrecio.Value >= 100 && nudPrecio.Value < 500)
            {
                lblInfoPrecio.Text = "💰 Precio medio-alto";
                lblInfoPrecio.ForeColor = Color.FromArgb(241, 196, 15);
            }
            else
            {
                lblInfoPrecio.Text = "💰 Precio premium";
                lblInfoPrecio.ForeColor = Color.FromArgb(142, 68, 173);
            }
        }

        /// <summary>
        /// EVENTO VALUECHANGED DEL NUMERICUPDOWN DE STOCK
        /// </summary>
        private void nudStock_ValueChanged(object sender, EventArgs e)
        {
            if (nudStock.Value == 0)
            {
                lblInfoStock.Text = "⚠️ Sin stock";
                lblInfoStock.ForeColor = Color.FromArgb(231, 76, 60);
            }
            else if (nudStock.Value <= 10)
            {
                lblInfoStock.Text = "⚠️ Stock bajo";
                lblInfoStock.ForeColor = Color.FromArgb(230, 126, 34);
            }
            else if (nudStock.Value <= 30)
            {
                lblInfoStock.Text = "📦 Stock medio";
                lblInfoStock.ForeColor = Color.FromArgb(241, 196, 15);
            }
            else
            {
                lblInfoStock.Text = "✅ Stock suficiente";
                lblInfoStock.ForeColor = Color.FromArgb(46, 204, 113);
            }
        }

        /// <summary>
        /// EVENTO KEYDOWN DEL FORMULARIO
        /// Permite usar Enter y ESC
        /// </summary>
        private void FrmRegistrarProducto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !btnCancelar.Focused)
            {
                btnGuardar_Click(sender, e);
                e.Handled = true;
            }

            if (e.KeyCode == Keys.Escape)
            {
                btnCancelar_Click(sender, e);
                e.Handled = true;
            }
        }
    }
}