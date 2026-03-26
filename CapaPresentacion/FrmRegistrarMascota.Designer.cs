using System.Drawing;
using System.Windows.Forms;

namespace CapaPresentacion
{
    partial class FrmRegistrarMascota
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.lblTitulo = new System.Windows.Forms.Label();
            this.panelFormulario = new System.Windows.Forms.Panel();
            this.lblEmojiEspecie = new System.Windows.Forms.Label();
            this.lblPesoInfo = new System.Windows.Forms.Label();
            this.lblEdadInfo = new System.Windows.Forms.Label();
            this.groupBoxEstado = new System.Windows.Forms.GroupBox();
            this.rbtnInactivo = new System.Windows.Forms.RadioButton();
            this.rbtnActivo = new System.Windows.Forms.RadioButton();
            this.groupBoxSexo = new System.Windows.Forms.GroupBox();
            this.rbtnHembra = new System.Windows.Forms.RadioButton();
            this.rbtnMacho = new System.Windows.Forms.RadioButton();
            this.groupBoxEspecie = new System.Windows.Forms.GroupBox();
            this.rbtnOtro = new System.Windows.Forms.RadioButton();
            this.rbtnGato = new System.Windows.Forms.RadioButton();
            this.rbtnPerro = new System.Windows.Forms.RadioButton();
            this.label8 = new System.Windows.Forms.Label();
            this.cmbCliente = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtColor = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.nudPeso = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.nudEdad = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.txtRaza = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.txtIdMascota = new System.Windows.Forms.TextBox();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.panelFormulario.SuspendLayout();
            this.groupBoxEstado.SuspendLayout();
            this.groupBoxSexo.SuspendLayout();
            this.groupBoxEspecie.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPeso)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudEdad)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblTitulo.Location = new System.Drawing.Point(30, 20);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(384, 37);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "📝 Registrar Nueva Mascota";
            // 
            // panelFormulario
            // 
            this.panelFormulario.BackColor = System.Drawing.Color.White;
            this.panelFormulario.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelFormulario.Controls.Add(this.lblEmojiEspecie);
            this.panelFormulario.Controls.Add(this.lblPesoInfo);
            this.panelFormulario.Controls.Add(this.lblEdadInfo);
            this.panelFormulario.Controls.Add(this.groupBoxEstado);
            this.panelFormulario.Controls.Add(this.groupBoxSexo);
            this.panelFormulario.Controls.Add(this.groupBoxEspecie);
            this.panelFormulario.Controls.Add(this.label8);
            this.panelFormulario.Controls.Add(this.cmbCliente);
            this.panelFormulario.Controls.Add(this.label7);
            this.panelFormulario.Controls.Add(this.txtColor);
            this.panelFormulario.Controls.Add(this.label6);
            this.panelFormulario.Controls.Add(this.nudPeso);
            this.panelFormulario.Controls.Add(this.label5);
            this.panelFormulario.Controls.Add(this.nudEdad);
            this.panelFormulario.Controls.Add(this.label3);
            this.panelFormulario.Controls.Add(this.txtRaza);
            this.panelFormulario.Controls.Add(this.label2);
            this.panelFormulario.Controls.Add(this.txtNombre);
            this.panelFormulario.Location = new System.Drawing.Point(30, 70);
            this.panelFormulario.Name = "panelFormulario";
            this.panelFormulario.Size = new System.Drawing.Size(740, 480);
            this.panelFormulario.TabIndex = 1;
            // 
            // lblEmojiEspecie
            // 
            this.lblEmojiEspecie.AutoSize = true;
            this.lblEmojiEspecie.Font = new System.Drawing.Font("Segoe UI", 48F);
            this.lblEmojiEspecie.Location = new System.Drawing.Point(602, 180);
            this.lblEmojiEspecie.Name = "lblEmojiEspecie";
            this.lblEmojiEspecie.Size = new System.Drawing.Size(155, 106);
            this.lblEmojiEspecie.TabIndex = 17;
            this.lblEmojiEspecie.Text = "🐕";
            // 
            // lblPesoInfo
            // 
            this.lblPesoInfo.AutoSize = true;
            this.lblPesoInfo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.lblPesoInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(165)))), ((int)(((byte)(166)))));
            this.lblPesoInfo.Location = new System.Drawing.Point(230, 275);
            this.lblPesoInfo.Name = "lblPesoInfo";
            this.lblPesoInfo.Size = new System.Drawing.Size(0, 20);
            this.lblPesoInfo.TabIndex = 16;
            // 
            // lblEdadInfo
            // 
            this.lblEdadInfo.AutoSize = true;
            this.lblEdadInfo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.lblEdadInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(165)))), ((int)(((byte)(166)))));
            this.lblEdadInfo.Location = new System.Drawing.Point(230, 215);
            this.lblEdadInfo.Name = "lblEdadInfo";
            this.lblEdadInfo.Size = new System.Drawing.Size(0, 20);
            this.lblEdadInfo.TabIndex = 15;
            // 
            // groupBoxEstado
            // 
            this.groupBoxEstado.Controls.Add(this.rbtnInactivo);
            this.groupBoxEstado.Controls.Add(this.rbtnActivo);
            this.groupBoxEstado.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.groupBoxEstado.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.groupBoxEstado.Location = new System.Drawing.Point(380, 350);
            this.groupBoxEstado.Name = "groupBoxEstado";
            this.groupBoxEstado.Size = new System.Drawing.Size(330, 100);
            this.groupBoxEstado.TabIndex = 14;
            this.groupBoxEstado.TabStop = false;
            this.groupBoxEstado.Text = "Estado";
            // 
            // rbtnInactivo
            // 
            this.rbtnInactivo.AutoSize = true;
            this.rbtnInactivo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbtnInactivo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.rbtnInactivo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.rbtnInactivo.Location = new System.Drawing.Point(170, 40);
            this.rbtnInactivo.Name = "rbtnInactivo";
            this.rbtnInactivo.Size = new System.Drawing.Size(128, 27);
            this.rbtnInactivo.TabIndex = 1;
            this.rbtnInactivo.Text = "✗ INACTIVO";
            this.rbtnInactivo.UseVisualStyleBackColor = true;
            // 
            // rbtnActivo
            // 
            this.rbtnActivo.AutoSize = true;
            this.rbtnActivo.Checked = true;
            this.rbtnActivo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbtnActivo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.rbtnActivo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.rbtnActivo.Location = new System.Drawing.Point(30, 40);
            this.rbtnActivo.Name = "rbtnActivo";
            this.rbtnActivo.Size = new System.Drawing.Size(109, 27);
            this.rbtnActivo.TabIndex = 0;
            this.rbtnActivo.TabStop = true;
            this.rbtnActivo.Text = "✓ ACTIVO";
            this.rbtnActivo.UseVisualStyleBackColor = true;
            // 
            // groupBoxSexo
            // 
            this.groupBoxSexo.Controls.Add(this.rbtnHembra);
            this.groupBoxSexo.Controls.Add(this.rbtnMacho);
            this.groupBoxSexo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.groupBoxSexo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.groupBoxSexo.Location = new System.Drawing.Point(30, 350);
            this.groupBoxSexo.Name = "groupBoxSexo";
            this.groupBoxSexo.Size = new System.Drawing.Size(330, 100);
            this.groupBoxSexo.TabIndex = 13;
            this.groupBoxSexo.TabStop = false;
            this.groupBoxSexo.Text = "Sexo";
            // 
            // rbtnHembra
            // 
            this.rbtnHembra.AutoSize = true;
            this.rbtnHembra.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbtnHembra.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.rbtnHembra.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.rbtnHembra.Location = new System.Drawing.Point(170, 40);
            this.rbtnHembra.Name = "rbtnHembra";
            this.rbtnHembra.Size = new System.Drawing.Size(126, 27);
            this.rbtnHembra.TabIndex = 1;
            this.rbtnHembra.Text = "♀ HEMBRA";
            this.rbtnHembra.UseVisualStyleBackColor = true;
            // 
            // rbtnMacho
            // 
            this.rbtnMacho.AutoSize = true;
            this.rbtnMacho.Checked = true;
            this.rbtnMacho.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbtnMacho.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.rbtnMacho.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.rbtnMacho.Location = new System.Drawing.Point(30, 40);
            this.rbtnMacho.Name = "rbtnMacho";
            this.rbtnMacho.Size = new System.Drawing.Size(121, 27);
            this.rbtnMacho.TabIndex = 0;
            this.rbtnMacho.TabStop = true;
            this.rbtnMacho.Text = "♂ MACHO";
            this.rbtnMacho.UseVisualStyleBackColor = true;
            // 
            // groupBoxEspecie
            // 
            this.groupBoxEspecie.Controls.Add(this.rbtnOtro);
            this.groupBoxEspecie.Controls.Add(this.rbtnGato);
            this.groupBoxEspecie.Controls.Add(this.rbtnPerro);
            this.groupBoxEspecie.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.groupBoxEspecie.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.groupBoxEspecie.Location = new System.Drawing.Point(380, 130);
            this.groupBoxEspecie.Name = "groupBoxEspecie";
            this.groupBoxEspecie.Size = new System.Drawing.Size(220, 180);
            this.groupBoxEspecie.TabIndex = 12;
            this.groupBoxEspecie.TabStop = false;
            this.groupBoxEspecie.Text = "Especie";
            // 
            // rbtnOtro
            // 
            this.rbtnOtro.AutoSize = true;
            this.rbtnOtro.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbtnOtro.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.rbtnOtro.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(126)))), ((int)(((byte)(34)))));
            this.rbtnOtro.Location = new System.Drawing.Point(30, 120);
            this.rbtnOtro.Name = "rbtnOtro";
            this.rbtnOtro.Size = new System.Drawing.Size(103, 27);
            this.rbtnOtro.TabIndex = 2;
            this.rbtnOtro.Text = "🐾 OTRO";
            this.rbtnOtro.UseVisualStyleBackColor = true;
            this.rbtnOtro.CheckedChanged += new System.EventHandler(this.rbtnEspecie_CheckedChanged);
            // 
            // rbtnGato
            // 
            this.rbtnGato.AutoSize = true;
            this.rbtnGato.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbtnGato.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.rbtnGato.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(68)))), ((int)(((byte)(173)))));
            this.rbtnGato.Location = new System.Drawing.Point(30, 80);
            this.rbtnGato.Name = "rbtnGato";
            this.rbtnGato.Size = new System.Drawing.Size(102, 27);
            this.rbtnGato.TabIndex = 1;
            this.rbtnGato.Text = "🐈 GATO";
            this.rbtnGato.UseVisualStyleBackColor = true;
            this.rbtnGato.CheckedChanged += new System.EventHandler(this.rbtnEspecie_CheckedChanged);
            // 
            // rbtnPerro
            // 
            this.rbtnPerro.AutoSize = true;
            this.rbtnPerro.Checked = true;
            this.rbtnPerro.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbtnPerro.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.rbtnPerro.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.rbtnPerro.Location = new System.Drawing.Point(30, 40);
            this.rbtnPerro.Name = "rbtnPerro";
            this.rbtnPerro.Size = new System.Drawing.Size(111, 27);
            this.rbtnPerro.TabIndex = 0;
            this.rbtnPerro.TabStop = true;
            this.rbtnPerro.Text = "🐕 PERRO";
            this.rbtnPerro.UseVisualStyleBackColor = true;
            this.rbtnPerro.CheckedChanged += new System.EventHandler(this.rbtnEspecie_CheckedChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.label8.Location = new System.Drawing.Point(380, 20);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(153, 23);
            this.label8.TabIndex = 11;
            this.label8.Text = "Dueño (Cliente): *";
            // 
            // cmbCliente
            // 
            this.cmbCliente.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCliente.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbCliente.FormattingEnabled = true;
            this.cmbCliente.Location = new System.Drawing.Point(384, 50);
            this.cmbCliente.Name = "cmbCliente";
            this.cmbCliente.Size = new System.Drawing.Size(326, 31);
            this.cmbCliente.TabIndex = 10;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.label7.Location = new System.Drawing.Point(26, 300);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 23);
            this.label7.TabIndex = 9;
            this.label7.Text = "Color:";
            // 
            // txtColor
            // 
            this.txtColor.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtColor.Location = new System.Drawing.Point(30, 330);
            this.txtColor.Name = "txtColor";
            this.txtColor.Size = new System.Drawing.Size(330, 30);
            this.txtColor.TabIndex = 8;
            this.txtColor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtColor_KeyPress);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.label6.Location = new System.Drawing.Point(26, 240);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(101, 23);
            this.label6.TabIndex = 7;
            this.label6.Text = "Peso (kg): *";
            // 
            // nudPeso
            // 
            this.nudPeso.DecimalPlaces = 2;
            this.nudPeso.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.nudPeso.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.nudPeso.Location = new System.Drawing.Point(30, 270);
            this.nudPeso.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.nudPeso.Name = "nudPeso";
            this.nudPeso.Size = new System.Drawing.Size(180, 30);
            this.nudPeso.TabIndex = 6;
            this.nudPeso.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nudPeso.ValueChanged += new System.EventHandler(this.nudPeso_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.label5.Location = new System.Drawing.Point(26, 180);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(121, 23);
            this.label5.TabIndex = 5;
            this.label5.Text = "Edad (años): *";
            // 
            // nudEdad
            // 
            this.nudEdad.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.nudEdad.Location = new System.Drawing.Point(30, 210);
            this.nudEdad.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.nudEdad.Name = "nudEdad";
            this.nudEdad.Size = new System.Drawing.Size(180, 30);
            this.nudEdad.TabIndex = 4;
            this.nudEdad.ValueChanged += new System.EventHandler(this.nudEdad_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.label3.Location = new System.Drawing.Point(26, 100);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 23);
            this.label3.TabIndex = 3;
            this.label3.Text = "Raza: *";
            // 
            // txtRaza
            // 
            this.txtRaza.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtRaza.Location = new System.Drawing.Point(30, 130);
            this.txtRaza.Name = "txtRaza";
            this.txtRaza.Size = new System.Drawing.Size(330, 30);
            this.txtRaza.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.label2.Location = new System.Drawing.Point(26, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 23);
            this.label2.TabIndex = 1;
            this.label2.Text = "Nombre: *";
            // 
            // txtNombre
            // 
            this.txtNombre.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtNombre.Location = new System.Drawing.Point(30, 50);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(330, 30);
            this.txtNombre.TabIndex = 0;
            this.txtNombre.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNombre_KeyPress);
            // 
            // btnGuardar
            // 
            this.btnGuardar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnGuardar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGuardar.FlatAppearance.BorderSize = 0;
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnGuardar.ForeColor = System.Drawing.Color.White;
            this.btnGuardar.Location = new System.Drawing.Point(511, 565);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(120, 45);
            this.btnGuardar.TabIndex = 3;
            this.btnGuardar.Text = "💾 Guardar";
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // txtIdMascota
            // 
            this.txtIdMascota.Location = new System.Drawing.Point(30, 565);
            this.txtIdMascota.Name = "txtIdMascota";
            this.txtIdMascota.Size = new System.Drawing.Size(100, 22);
            this.txtIdMascota.TabIndex = 4;
            this.txtIdMascota.Visible = false;
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.btnCancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelar.FlatAppearance.BorderSize = 0;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnCancelar.ForeColor = System.Drawing.Color.White;
            this.btnCancelar.Location = new System.Drawing.Point(650, 565);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(120, 45);
            this.btnCancelar.TabIndex = 24;
            this.btnCancelar.Text = "✗ Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // FrmRegistrarMascota
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.ClientSize = new System.Drawing.Size(800, 630);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.txtIdMascota);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.panelFormulario);
            this.Controls.Add(this.lblTitulo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmRegistrarMascota";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Registrar Mascota - Sistema Veterinaria";
            this.Load += new System.EventHandler(this.FrmRegistrarMascota_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmRegistrarMascota_KeyDown);
            this.panelFormulario.ResumeLayout(false);
            this.panelFormulario.PerformLayout();
            this.groupBoxEstado.ResumeLayout(false);
            this.groupBoxEstado.PerformLayout();
            this.groupBoxSexo.ResumeLayout(false);
            this.groupBoxSexo.PerformLayout();
            this.groupBoxEspecie.ResumeLayout(false);
            this.groupBoxEspecie.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPeso)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudEdad)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Panel panelFormulario;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox txtRaza;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.NumericUpDown nudEdad;
        private System.Windows.Forms.Label label6;
        public System.Windows.Forms.NumericUpDown nudPeso;
        private System.Windows.Forms.Label label7;
        public System.Windows.Forms.TextBox txtColor;
        private System.Windows.Forms.Label label8;
        public System.Windows.Forms.ComboBox cmbCliente;
        private System.Windows.Forms.GroupBox groupBoxEspecie;
        public System.Windows.Forms.RadioButton rbtnPerro;
        public System.Windows.Forms.RadioButton rbtnGato;
        public System.Windows.Forms.RadioButton rbtnOtro;
        private System.Windows.Forms.GroupBox groupBoxSexo;
        public System.Windows.Forms.RadioButton rbtnMacho;
        public System.Windows.Forms.RadioButton rbtnHembra;
        private System.Windows.Forms.GroupBox groupBoxEstado;
        public System.Windows.Forms.RadioButton rbtnActivo;
        public System.Windows.Forms.RadioButton rbtnInactivo;
        private System.Windows.Forms.Button btnGuardar;
        public System.Windows.Forms.TextBox txtIdMascota;
        private System.Windows.Forms.Label lblEdadInfo;
        private System.Windows.Forms.Label lblPesoInfo;
        private System.Windows.Forms.Label lblEmojiEspecie;
        private Button btnCancelar;
    }
}