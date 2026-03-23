using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using CapaNegocio;

namespace CapaPresentacion
{
    /// <summary>
    /// FORMULARIO PARA REGISTRAR Y EDITAR PRODUCTOS
    /// ACTUALIZADO: Genera y muestra código de barras EAN-13 al crear/editar
    /// </summary>
    public partial class FrmRegistrarProducto : Form
    {
        public bool Insert = false;
        public bool Edit = false;
        public int IdCategoriaSeleccionada { get; set; }
        public int IdProveedorSeleccionado { get; set; }

        // Código de barras del producto actual (si ya existe o recién generado)
        private string _codigoBarrasActual = null;
        private int _idProductoActual = 0;

        public FrmRegistrarProducto()
        {
            InitializeComponent();
        }

        private void FrmRegistrarProducto_Load(object sender, EventArgs e)
        {
            CargarCategorias();
            CargarProveedores();

            if (Insert)
            {
                lblTitulo.Text = "📝 Registrar Nuevo Producto";
                rbtnActivo.Checked = true;
                dtpVencimiento.Enabled = false;
                nudPrecio.Value = 1;
                nudStock.Value = 0;

                // Previsualizar el código que se generará (estimado)
                lblCodigoBarrasNum.Text = "Se generará al guardar";
                picCodigoBarras.Visible = false;
                lblCodigoBarrasNum.ForeColor = Color.FromArgb(149, 165, 166);
            }
            else if (Edit)
            {
                lblTitulo.Text = "✏️ Editar Producto";
                if (IdCategoriaSeleccionada > 0)
                    cmbCategoria.SelectedValue = IdCategoriaSeleccionada;
                if (IdProveedorSeleccionado > 0)
                    cmbProveedor.SelectedValue = IdProveedorSeleccionado;

                // Mostrar código de barras existente si se cargó
                MostrarCodigoBarras(_codigoBarrasActual);
            }

            ConfigurarTooltips();
        }

        // ── Cargar código de barras desde fuera (FrmListadoProductos) ─────────
        public void SetCodigoBarras(string codigo, int idproducto)
        {
            _codigoBarrasActual = codigo;
            _idProductoActual = idproducto;
        }

        // ── Cargar proveedores ───────────────────────────────────────────────
        private void CargarProveedores()
        {
            try
            {
                DataTable prov = CN_Proveedor.ListarActivos();
                // Agregar opción "Sin proveedor"
                DataRow r = prov.NewRow();
                r["idproveedor"] = 0; r["nombre"] = "-- Sin proveedor --";
                prov.Rows.InsertAt(r, 0);
                cmbProveedor.DataSource = prov;
                cmbProveedor.DisplayMember = "nombre";
                cmbProveedor.ValueMember = "idproveedor";
                cmbProveedor.SelectedIndex = 0;
            }
            catch
            {
                // No bloquear si falla — proveedor es opcional
            }
        }

        // ── Cargar categorías ─────────────────────────────────────────────────
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
                    MessageBox.Show("⚠️ No hay categorías registradas. Registre al menos una.",
                        "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar categorías: " + ex.Message,
                    "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ── Mostrar código de barras como imagen + número ─────────────────────
        private void MostrarCodigoBarras(string codigo)
        {
            if (string.IsNullOrEmpty(codigo) || !EAN13Util.EsValido(codigo))
            {
                picCodigoBarras.Visible = false;
                lblCodigoBarrasNum.Text = "Sin código de barras";
                lblCodigoBarrasNum.ForeColor = Color.FromArgb(149, 165, 166);
                btnRegenerarCodigo.Visible = _idProductoActual > 0;
                return;
            }

            try
            {
                // Generar imagen EAN-13
                Bitmap bmp = EAN13Util.GenerarImagen(codigo,
                    picCodigoBarras.Width, picCodigoBarras.Height, mostrarNumero: false);

                picCodigoBarras.Image = bmp;
                picCodigoBarras.Visible = true;

                lblCodigoBarrasNum.Text = codigo;
                lblCodigoBarrasNum.ForeColor = Color.FromArgb(52, 73, 94);

                btnRegenerarCodigo.Visible = true;
                _codigoBarrasActual = codigo;
            }
            catch (Exception ex)
            {
                lblCodigoBarrasNum.Text = $"Error al renderizar: {ex.Message}";
                lblCodigoBarrasNum.ForeColor = Color.Red;
                picCodigoBarras.Visible = false;
            }
        }

        // ── Guardar ───────────────────────────────────────────────────────────
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // Validaciones
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("⚠️ Por favor, ingrese el nombre del producto",
                    "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombre.Focus(); return;
            }
            if (cmbCategoria.SelectedIndex < 0)
            {
                MessageBox.Show("⚠️ Por favor, seleccione una categoría",
                    "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbCategoria.Focus(); return;
            }
            if (nudPrecio.Value <= 0)
            {
                MessageBox.Show("⚠️ El precio debe ser mayor a $0",
                    "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                nudPrecio.Focus(); return;
            }
            if (chkEsMedicamento.Checked && !dtpVencimiento.Enabled)
            {
                MessageBox.Show("⚠️ Los medicamentos deben tener fecha de vencimiento",
                    "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string nombre = txtNombre.Text.Trim();
                string descripcion = txtDescripcion.Text.Trim();
                decimal precio = nudPrecio.Value;
                int stock = Convert.ToInt32(nudStock.Value);
                int idcategoria = Convert.ToInt32(cmbCategoria.SelectedValue);
                bool esMedicamento = chkEsMedicamento.Checked;
                DateTime? fechaVencimiento = esMedicamento && dtpVencimiento.Enabled
                    ? dtpVencimiento.Value : (DateTime?)null;
                string estado = rbtnActivo.Checked ? "ACTIVO" : "INACTIVO";

                if (Insert)
                {
                    // CN_Producto.Guardar genera el EAN-13 automáticamente
                    string resultado = CN_Producto.Guardar(nombre, descripcion, precio,
                        stock, estado, idcategoria, esMedicamento, fechaVencimiento);

                    if (resultado == "OK")
                    {
                        // Obtener el código recién generado para mostrarlo
                        DataTable dt = CN_Producto.BuscarNombre(nombre);
                        string codigoGenerado = "";
                        int idGenerado = 0;

                        if (dt != null && dt.Rows.Count > 0)
                        {
                            // Tomar el de mayor ID
                            foreach (DataRow row in dt.Rows)
                            {
                                int id = Convert.ToInt32(row["idproducto"]);
                                if (id > idGenerado)
                                {
                                    idGenerado = id;
                                    codigoGenerado = dt.Columns.Contains("codigo_barras")
                                        ? row["codigo_barras"]?.ToString() ?? ""
                                        : EAN13Util.Generar(id);
                                }
                            }
                        }

                        // Mostrar código de barras generado en un diálogo de confirmación
                        MostrarDialogoCodigoBarras(nombre, codigoGenerado, precio, stock);

                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("❌ " + resultado,
                            "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else if (Edit)
                {
                    int idproducto = Convert.ToInt32(txtIdProducto.Text);
                    string resultado = CN_Producto.Editar(idproducto, nombre, descripcion,
                        precio, stock, estado, idcategoria, esMedicamento, fechaVencimiento);

                    if (resultado == "OK")
                    {
                        MessageBox.Show($"✅ Producto actualizado correctamente\n\n" +
                            $"Nombre: {nombre}\nPrecio: ${precio:N2}\nStock: {stock}",
                            "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("❌ " + resultado,
                            "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Error al guardar: " + ex.Message,
                    "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ── Diálogo de confirmación con imagen del código de barras ───────────
        private void MostrarDialogoCodigoBarras(string nombre, string codigo,
            decimal precio, int stock)
        {
            Form dlg = new Form
            {
                Text = "✅ Producto registrado",
                Size = new Size(360, 320),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false,
                BackColor = Color.White
            };

            Label lblInfo = new Label
            {
                Text = $"✅ Producto registrado correctamente\n\n" +
                             $"Nombre: {nombre}\nPrecio: ${precio:N2}\nStock: {stock}\n\n" +
                             $"Código de barras generado:",
                Location = new Point(15, 10),
                Size = new Size(320, 100),
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.FromArgb(52, 73, 94)
            };

            PictureBox pic = new PictureBox
            {
                Location = new Point(30, 110),
                Size = new Size(290, 100),
                SizeMode = PictureBoxSizeMode.StretchImage,
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };

            Label lblCod = new Label
            {
                Text = codigo,
                Location = new Point(30, 215),
                Size = new Size(290, 20),
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Courier New", 11F, FontStyle.Bold),
                ForeColor = Color.FromArgb(52, 73, 94)
            };

            Button btnOk = new Button
            {
                Text = "OK",
                Location = new Point(130, 245),
                Size = new Size(90, 35),
                BackColor = Color.FromArgb(46, 204, 113),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                DialogResult = DialogResult.OK
            };
            btnOk.FlatAppearance.BorderSize = 0;

            dlg.Controls.AddRange(new Control[] { lblInfo, pic, lblCod, btnOk });
            dlg.AcceptButton = btnOk;

            // Renderizar imagen
            if (!string.IsNullOrEmpty(codigo) && EAN13Util.EsValido(codigo))
            {
                try
                {
                    pic.Image = EAN13Util.GenerarImagen(codigo, 290, 100, mostrarNumero: false);
                }
                catch { lblCod.Text = codigo; }
            }

            dlg.ShowDialog(this);
            dlg.Dispose();
        }

        // ── Regenerar código de barras manualmente ────────────────────────────
        private void btnRegenerarCodigo_Click(object sender, EventArgs e)
        {
            if (_idProductoActual <= 0)
            {
                MessageBox.Show("Solo se puede regenerar en modo edición.",
                    "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("¿Regenerar el código de barras de este producto?\n\nEl código anterior quedará inválido.",
                    "Sistema Veterinaria", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                == DialogResult.Yes)
            {
                string nuevo = CN_Producto.RegenerarCodigoBarras(_idProductoActual);
                if (EAN13Util.EsValido(nuevo))
                {
                    MostrarCodigoBarras(nuevo);
                    MessageBox.Show($"✅ Nuevo código generado:\n{nuevo}",
                        "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    MessageBox.Show("❌ " + nuevo,
                        "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ── Cancelar ─────────────────────────────────────────────────────────
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtNombre.Text) || cmbCategoria.SelectedIndex >= 0)
            {
                if (MessageBox.Show("¿Desea cancelar? Los cambios se perderán.",
                        "Sistema Veterinaria", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    == DialogResult.Yes)
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

        // ── Eventos existentes (sin cambios) ──────────────────────────────────
        private void ConfigurarTooltips()
        {
            ToolTip tt = new ToolTip();
            tt.SetToolTip(txtNombre, "Nombre del producto (obligatorio)");
            tt.SetToolTip(nudPrecio, "Precio en pesos mexicanos (MXN)");
            tt.SetToolTip(nudStock, "Cantidad en inventario");
            tt.SetToolTip(cmbCategoria, "Categoría del producto");
            tt.SetToolTip(chkEsMedicamento, "Marcar si es un medicamento");
            tt.SetToolTip(picCodigoBarras, "Código de barras EAN-13 del producto");
            tt.SetToolTip(btnRegenerarCodigo, "Generar un nuevo código de barras para este producto");
        }

        private void chkEsMedicamento_CheckedChanged(object sender, EventArgs e)
        {
            dtpVencimiento.Enabled = chkEsMedicamento.Checked;
            if (chkEsMedicamento.Checked)
            {
                dtpVencimiento.Value = DateTime.Now.AddYears(1);
                lblVencimiento.ForeColor = Color.FromArgb(231, 76, 60);
            }
            else
                lblVencimiento.ForeColor = Color.FromArgb(149, 165, 166);
        }

        private void nudPrecio_ValueChanged(object sender, EventArgs e)
        {
            if (nudPrecio.Value < 10) { lblInfoPrecio.Text = "💰 Precio bajo"; lblInfoPrecio.ForeColor = Color.FromArgb(52, 152, 219); }
            else if (nudPrecio.Value >= 10 && nudPrecio.Value < 100) { lblInfoPrecio.Text = "💰 Precio moderado"; lblInfoPrecio.ForeColor = Color.FromArgb(46, 204, 113); }
            else if (nudPrecio.Value >= 100 && nudPrecio.Value < 500) { lblInfoPrecio.Text = "💰 Precio medio-alto"; lblInfoPrecio.ForeColor = Color.FromArgb(241, 196, 15); }
            else { lblInfoPrecio.Text = "💰 Precio premium"; lblInfoPrecio.ForeColor = Color.FromArgb(142, 68, 173); }
        }

        private void nudStock_ValueChanged(object sender, EventArgs e)
        {
            if (nudStock.Value == 0) { lblInfoStock.Text = "⚠️ Sin stock"; lblInfoStock.ForeColor = Color.FromArgb(231, 76, 60); }
            else if (nudStock.Value <= 10) { lblInfoStock.Text = "⚠️ Stock bajo"; lblInfoStock.ForeColor = Color.FromArgb(230, 126, 34); }
            else if (nudStock.Value <= 30) { lblInfoStock.Text = "📦 Stock medio"; lblInfoStock.ForeColor = Color.FromArgb(241, 196, 15); }
            else { lblInfoStock.Text = "✅ Stock suficiente"; lblInfoStock.ForeColor = Color.FromArgb(46, 204, 113); }
        }

        private void FrmRegistrarProducto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !btnCancelar.Focused)
            { btnGuardar_Click(sender, e); e.Handled = true; }
            if (e.KeyCode == Keys.Escape)
            { btnCancelar_Click(sender, e); e.Handled = true; }
        }
    }
}