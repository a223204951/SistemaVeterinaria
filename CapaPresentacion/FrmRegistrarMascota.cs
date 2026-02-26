using CapaDatos;
using CapaNegocio;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace CapaPresentacion
{
    /// <summary>
    /// FORMULARIO PARA REGISTRAR Y EDITAR MASCOTAS
    /// Permite agregar nuevas mascotas o modificar las existentes
    /// Incluye validaciones completas y selección de dueño (cliente)
    /// </summary>
    public partial class FrmRegistrarMascota : Form
    {
        // =============================================
        // BANDERAS PARA INDICAR SI ES INSERCIÓN O EDICIÓN
        // =============================================
        public bool Insert = false;
        public bool Edit = false;

        // =============================================
        // PROPIEDAD PARA ESTABLECER EL CLIENTE SELECCIONADO AL EDITAR
        // =============================================
        public int IdClienteSeleccionado { get; set; }

        public FrmRegistrarMascota()
        {
            InitializeComponent();
        }

        /// <summary>
        /// EVENTO LOAD DEL FORMULARIO
        /// Configura el formulario según si es inserción o edición
        /// </summary>
        private void FrmRegistrarMascota_Load(object sender, EventArgs e)
        {
            // CARGAR CLIENTES EN EL COMBOBOX
            CargarClientes();

            // CAMBIAR EL TÍTULO SEGÚN LA OPERACIÓN
            if (Insert)
            {
                lblTitulo.Text = "📝 Registrar Nueva Mascota";
                rbtnPerro.Checked = true;
                rbtnMacho.Checked = true;
                rbtnActivo.Checked = true;
            }
            else if (Edit)
            {
                lblTitulo.Text = "✏️ Editar Mascota";

                // SELECCIONAR EL CLIENTE EN EL COMBOBOX
                if (IdClienteSeleccionado > 0)
                {
                    cmbCliente.SelectedValue = IdClienteSeleccionado;
                }
            }

            // CONFIGURAR TOOLTIPS PARA AYUDA
            ConfigurarTooltips();
        }

        /// <summary>
        /// MÉTODO PARA CARGAR CLIENTES EN EL COMBOBOX
        /// Solo muestra clientes activos
        /// </summary>
        private void CargarClientes()
        {
            try
            {
                DataTable clientes = CN_Mascota.ObtenerClientes();

                if (clientes != null && clientes.Rows.Count > 0)
                {
                    cmbCliente.DataSource = clientes;
                    cmbCliente.DisplayMember = "nombre";
                    cmbCliente.ValueMember = "idcliente";
                    cmbCliente.SelectedIndex = -1;
                }
                else
                {
                    MessageBox.Show("⚠️ No hay clientes registrados en el sistema.\n\n" +
                        "Por favor, registre al menos un cliente antes de agregar mascotas.",
                        "Sistema Veterinaria",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar clientes: " + ex.Message,
                    "Sistema Veterinaria",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// MÉTODO PARA CONFIGURAR TOOLTIPS (AYUDAS EMERGENTES)
        /// </summary>
        private void ConfigurarTooltips()
        {
            ToolTip tooltip = new ToolTip();
            tooltip.SetToolTip(txtNombre, "Nombre de la mascota (obligatorio)");
            tooltip.SetToolTip(txtRaza, "Raza o cruce (ej: Labrador, Mestizo)");
            tooltip.SetToolTip(nudEdad, "Edad en años completos");
            tooltip.SetToolTip(nudPeso, "Peso en kilogramos");
            tooltip.SetToolTip(txtColor, "Color predominante del pelaje");
            tooltip.SetToolTip(cmbCliente, "Seleccione el dueño de la mascota");
        }

        /// <summary>
        /// EVENTO CLICK DEL BOTÓN GUARDAR
        /// Valida y guarda los datos de la mascota
        /// </summary>
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // =============================================
            // VALIDACIONES DE CAMPOS OBLIGATORIOS
            // =============================================

            // VALIDAR NOMBRE
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("⚠️ Por favor, ingrese el nombre de la mascota",
                    "Sistema Veterinaria",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                txtNombre.Focus();
                return;
            }

            // VALIDAR SELECCIÓN DE CLIENTE
            if (cmbCliente.SelectedIndex < 0)
            {
                MessageBox.Show("⚠️ Por favor, seleccione el dueño de la mascota",
                    "Sistema Veterinaria",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                cmbCliente.Focus();
                return;
            }

            // VALIDAR RAZA
            if (string.IsNullOrWhiteSpace(txtRaza.Text))
            {
                MessageBox.Show("⚠️ Por favor, ingrese la raza de la mascota",
                    "Sistema Veterinaria",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                txtRaza.Focus();
                return;
            }

            // VALIDAR EDAD
            if (nudEdad.Value < 0 || nudEdad.Value > 30)
            {
                MessageBox.Show("⚠️ La edad debe estar entre 0 y 30 años",
                    "Sistema Veterinaria",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                nudEdad.Focus();
                return;
            }

            // VALIDAR PESO
            if (nudPeso.Value <= 0 || nudPeso.Value > 200)
            {
                MessageBox.Show("⚠️ El peso debe estar entre 0.1 y 200 kg",
                    "Sistema Veterinaria",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                nudPeso.Focus();
                return;
            }

            try
            {
                // =============================================
                // OBTENER VALORES DE LOS CONTROLES
                // =============================================

                string nombre = txtNombre.Text.Trim();
                string raza = txtRaza.Text.Trim();
                int edad = Convert.ToInt32(nudEdad.Value);
                decimal peso = nudPeso.Value;
                string color = txtColor.Text.Trim();
                int idcliente = Convert.ToInt32(cmbCliente.SelectedValue);

                // DETERMINAR ESPECIE
                string especie = "";
                if (rbtnPerro.Checked)
                    especie = "Perro";
                else if (rbtnGato.Checked)
                    especie = "Gato";
                else if (rbtnOtro.Checked)
                    especie = "Otro";

                // DETERMINAR SEXO
                string sexo = rbtnMacho.Checked ? "Macho" : "Hembra";

                // DETERMINAR ESTADO
                string estado = rbtnActivo.Checked ? "ACTIVO" : "INACTIVO";

                // =============================================
                // GUARDAR O EDITAR SEGÚN LA OPERACIÓN
                // =============================================

                string resultado;

                if (Insert)
                {
                    // =============================================
                    // INSERTAR NUEVA MASCOTA
                    // =============================================
                    resultado = CN_Mascota.Guardar(nombre, especie, raza, sexo, edad, peso, color, estado, idcliente);

                    if (resultado == "OK")
                    {
                        MessageBox.Show("✅ Mascota registrada correctamente\n\n" +
                            $"Nombre: {nombre}\n" +
                            $"Especie: {especie}\n" +
                            $"Dueño: {cmbCliente.Text}",
                            "Sistema Veterinaria",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

                        // ESTABLECER RESULTADO Y CERRAR
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
                    // EDITAR MASCOTA EXISTENTE
                    // =============================================
                    int idmascota = Convert.ToInt32(txtIdMascota.Text);
                    resultado = CN_Mascota.Editar(idmascota, nombre, especie, raza, sexo, edad, peso, color, estado, idcliente);

                    if (resultado == "OK")
                    {
                        MessageBox.Show("✅ Mascota actualizada correctamente\n\n" +
                            $"Nombre: {nombre}\n" +
                            $"Especie: {especie}\n" +
                            $"Dueño: {cmbCliente.Text}",
                            "Sistema Veterinaria",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

                        // ESTABLECER RESULTADO Y CERRAR
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
                else
                {
                    MessageBox.Show("⚠️ No se ha definido la operación a realizar",
                        "Sistema Veterinaria",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Error al guardar: " + ex.Message + "\n\n" + ex.StackTrace,
                    "Sistema Veterinaria",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// EVENTO CLICK DEL BOTÓN CANCELAR
        /// Cierra el formulario sin guardar cambios
        /// </summary>
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            // CONFIRMAR CANCELACIÓN SI HAY DATOS INGRESADOS
            if (!string.IsNullOrWhiteSpace(txtNombre.Text) || cmbCliente.SelectedIndex >= 0)
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

        // =============================================
        // EVENTOS DE VALIDACIÓN EN TIEMPO REAL
        // =============================================

        /// <summary>
        /// EVENTO KEYPRESS DEL TEXTBOX NOMBRE
        /// Permite solo letras y espacios
        /// </summary>
        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            // PERMITIR SOLO LETRAS, ESPACIOS Y TECLAS DE CONTROL
            if (!char.IsLetter(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                System.Media.SystemSounds.Beep.Play();
            }
        }

        /// <summary>
        /// EVENTO KEYPRESS DEL TEXTBOX COLOR
        /// Permite solo letras y espacios
        /// </summary>
        private void txtColor_KeyPress(object sender, KeyPressEventArgs e)
        {
            // PERMITIR SOLO LETRAS, ESPACIOS Y TECLAS DE CONTROL
            if (!char.IsLetter(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                System.Media.SystemSounds.Beep.Play();
            }
        }

        /// <summary>
        /// EVENTO CHECKEDCHANGED DE LOS RADIOBUTTONS DE ESPECIE
        /// Actualiza el emoji según la especie seleccionada
        /// </summary>
        private void rbtnEspecie_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnPerro.Checked)
            {
                lblEmojiEspecie.Text = "🐕";
            }
            else if (rbtnGato.Checked)
            {
                lblEmojiEspecie.Text = "🐈";
            }
            else if (rbtnOtro.Checked)
            {
                lblEmojiEspecie.Text = "🐾";
            }
        }

        /// <summary>
        /// EVENTO VALUECHANGED DEL NUMERICUPDOWN DE EDAD
        /// Muestra mensaje si la mascota es muy joven o muy vieja
        /// </summary>
        private void nudEdad_ValueChanged(object sender, EventArgs e)
        {
            if (nudEdad.Value == 0)
            {
                lblEdadInfo.Text = "(Cachorro/Cría)";
                lblEdadInfo.ForeColor = Color.FromArgb(46, 204, 113);
            }
            else if (nudEdad.Value >= 1 && nudEdad.Value <= 7)
            {
                lblEdadInfo.Text = "(Adulto joven)";
                lblEdadInfo.ForeColor = Color.FromArgb(52, 152, 219);
            }
            else if (nudEdad.Value >= 8 && nudEdad.Value <= 12)
            {
                lblEdadInfo.Text = "(Adulto mayor)";
                lblEdadInfo.ForeColor = Color.FromArgb(241, 196, 15);
            }
            else if (nudEdad.Value > 12)
            {
                lblEdadInfo.Text = "(Senior)";
                lblEdadInfo.ForeColor = Color.FromArgb(230, 126, 34);
            }
        }

        /// <summary>
        /// EVENTO VALUECHANGED DEL NUMERICUPDOWN DE PESO
        /// Muestra mensaje según el peso de la mascota
        /// </summary>
        private void nudPeso_ValueChanged(object sender, EventArgs e)
        {
            if (nudPeso.Value < 5)
            {
                lblPesoInfo.Text = "(Muy pequeño)";
                lblPesoInfo.ForeColor = Color.FromArgb(52, 152, 219);
            }
            else if (nudPeso.Value >= 5 && nudPeso.Value < 15)
            {
                lblPesoInfo.Text = "(Pequeño)";
                lblPesoInfo.ForeColor = Color.FromArgb(46, 204, 113);
            }
            else if (nudPeso.Value >= 15 && nudPeso.Value < 30)
            {
                lblPesoInfo.Text = "(Mediano)";
                lblPesoInfo.ForeColor = Color.FromArgb(241, 196, 15);
            }
            else if (nudPeso.Value >= 30 && nudPeso.Value < 50)
            {
                lblPesoInfo.Text = "(Grande)";
                lblPesoInfo.ForeColor = Color.FromArgb(230, 126, 34);
            }
            else if (nudPeso.Value >= 50)
            {
                lblPesoInfo.Text = "(Muy grande)";
                lblPesoInfo.ForeColor = Color.FromArgb(231, 76, 60);
            }
        }

        /// <summary>
        /// EVENTO KEYDOWN DEL FORMULARIO
        /// Permite usar Enter para guardar y ESC para cancelar
        /// </summary>
        private void FrmRegistrarMascota_KeyDown(object sender, KeyEventArgs e)
        {
            // ENTER: Guardar (si no está en el botón cancelar)
            if (e.KeyCode == Keys.Enter && !btnCancelar.Focused)
            {
                btnGuardar_Click(sender, e);
                e.Handled = true;
            }

            // ESC: Cancelar
            if (e.KeyCode == Keys.Escape)
            {
                btnCancelar_Click(sender, e);
                e.Handled = true;
            }
        }
    }
}