using System;
using System.Windows.Forms;
using CapaNegocio;

namespace CapaPresentacion
{
    /// <summary>
    /// FORMULARIO PARA REGISTRAR Y EDITAR CATEGORÍAS
    /// Permite agregar nuevas categorías o modificar existentes
    /// </summary>
    public partial class FrmRegistrarCategoria : Form
    {
        // =============================================
        // BANDERAS PARA INDICAR SI ES INSERCIÓN O EDICIÓN
        // =============================================
        public bool Insert = false;
        public bool Edit = false;

        public FrmRegistrarCategoria()
        {
            InitializeComponent();
        }

        /// <summary>
        /// EVENTO LOAD DEL FORMULARIO
        /// </summary>
        private void FrmRegistrarCategoria_Load(object sender, EventArgs e)
        {
            if (Insert)
            {
                lblTitulo.Text = "📝 Nueva Categoría";
                rbtnActivo.Checked = true;
            }
            else if (Edit)
            {
                lblTitulo.Text = "✏️ Editar Categoría";
            }
        }

        /// <summary>
        /// EVENTO CLICK DEL BOTÓN GUARDAR
        /// </summary>
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // VALIDACIONES
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("⚠️ Por favor, ingrese el nombre de la categoría",
                    "Sistema Veterinaria",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                txtNombre.Focus();
                return;
            }

            try
            {
                string nombre = txtNombre.Text.Trim();
                string descripcion = txtDescripcion.Text.Trim();
                string estado = rbtnActivo.Checked ? "ACTIVO" : "INACTIVO";

                string resultado;

                if (Insert)
                {
                    resultado = CN_Categoria.Guardar(nombre, descripcion, estado);

                    if (resultado == "OK")
                    {
                        MessageBox.Show($"✅ Categoría '{nombre}' registrada correctamente",
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
                    int idcategoria = Convert.ToInt32(txtIdCategoria.Text);
                    resultado = CN_Categoria.Editar(idcategoria, nombre, descripcion, estado);

                    if (resultado == "OK")
                    {
                        MessageBox.Show($"✅ Categoría '{nombre}' actualizada correctamente",
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
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        /// <summary>
        /// EVENTO KEYDOWN DEL FORMULARIO
        /// </summary>
        private void FrmRegistrarCategoria_KeyDown(object sender, KeyEventArgs e)
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