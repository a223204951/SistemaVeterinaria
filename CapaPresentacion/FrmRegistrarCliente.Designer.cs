using System;
using System.Drawing;
using System.Windows.Forms;

namespace CapaPresentacion
{
    partial class FrmRegistrarCliente
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtnombre = new System.Windows.Forms.TextBox();
            this.txtdireccion = new System.Windows.Forms.TextBox();
            this.txttelefono = new System.Windows.Forms.TextBox();
            this.rbtnactivo = new System.Windows.Forms.RadioButton();
            this.rbtninactivo = new System.Windows.Forms.RadioButton();
            this.btnguardar = new System.Windows.Forms.Button();
            this.txtidcliente = new System.Windows.Forms.TextBox();
            this.panelFormulario = new System.Windows.Forms.Panel();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.panelFormulario.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.label1.Location = new System.Drawing.Point(30, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(367, 37);
            this.label1.TabIndex = 0;
            this.label1.Text = "📝 Registrar Nuevo Cliente";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.label2.Location = new System.Drawing.Point(30, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(162, 23);
            this.label2.TabIndex = 1;
            this.label2.Text = "Nombre completo:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.label5.Location = new System.Drawing.Point(460, 40);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(90, 23);
            this.label5.TabIndex = 4;
            this.label5.Text = "Dirección:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.label6.Location = new System.Drawing.Point(30, 130);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(83, 23);
            this.label6.TabIndex = 5;
            this.label6.Text = "Teléfono:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.label7.Location = new System.Drawing.Point(30, 230);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(68, 23);
            this.label7.TabIndex = 6;
            this.label7.Text = "Estado:";
            // 
            // txtnombre
            // 
            this.txtnombre.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtnombre.Location = new System.Drawing.Point(34, 70);
            this.txtnombre.Name = "txtnombre";
            this.txtnombre.Size = new System.Drawing.Size(400, 30);
            this.txtnombre.TabIndex = 7;
            // 
            // txtdireccion
            // 
            this.txtdireccion.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtdireccion.Location = new System.Drawing.Point(464, 70);
            this.txtdireccion.Multiline = true;
            this.txtdireccion.Name = "txtdireccion";
            this.txtdireccion.Size = new System.Drawing.Size(380, 80);
            this.txtdireccion.TabIndex = 10;
            // 
            // txttelefono
            // 
            this.txttelefono.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txttelefono.Location = new System.Drawing.Point(34, 160);
            this.txttelefono.Name = "txttelefono";
            this.txttelefono.Size = new System.Drawing.Size(250, 30);
            this.txttelefono.TabIndex = 11;
            // 
            // rbtnactivo
            // 
            this.rbtnactivo.AutoSize = true;
            this.rbtnactivo.Checked = true;
            this.rbtnactivo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbtnactivo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.rbtnactivo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.rbtnactivo.Location = new System.Drawing.Point(34, 265);
            this.rbtnactivo.Name = "rbtnactivo";
            this.rbtnactivo.Size = new System.Drawing.Size(109, 27);
            this.rbtnactivo.TabIndex = 12;
            this.rbtnactivo.TabStop = true;
            this.rbtnactivo.Text = "✓ ACTIVO";
            this.rbtnactivo.UseVisualStyleBackColor = true;
            // 
            // rbtninactivo
            // 
            this.rbtninactivo.AutoSize = true;
            this.rbtninactivo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbtninactivo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.rbtninactivo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.rbtninactivo.Location = new System.Drawing.Point(160, 265);
            this.rbtninactivo.Name = "rbtninactivo";
            this.rbtninactivo.Size = new System.Drawing.Size(128, 27);
            this.rbtninactivo.TabIndex = 13;
            this.rbtninactivo.Text = "✗ INACTIVO";
            this.rbtninactivo.UseVisualStyleBackColor = true;
            // 
            // btnguardar
            // 
            this.btnguardar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnguardar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnguardar.FlatAppearance.BorderSize = 0;
            this.btnguardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnguardar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnguardar.ForeColor = System.Drawing.Color.White;
            this.btnguardar.Location = new System.Drawing.Point(666, 471);
            this.btnguardar.Name = "btnguardar";
            this.btnguardar.Size = new System.Drawing.Size(120, 45);
            this.btnguardar.TabIndex = 15;
            this.btnguardar.Text = "💾 Guardar";
            this.btnguardar.UseVisualStyleBackColor = false;
            this.btnguardar.Click += new System.EventHandler(this.btnguardar_Click);
            // 
            // txtidcliente
            // 
            this.txtidcliente.Location = new System.Drawing.Point(37, 40);
            this.txtidcliente.Name = "txtidcliente";
            this.txtidcliente.Size = new System.Drawing.Size(100, 22);
            this.txtidcliente.TabIndex = 16;
            this.txtidcliente.Visible = false;
            // 
            // panelFormulario
            // 
            this.panelFormulario.BackColor = System.Drawing.Color.White;
            this.panelFormulario.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelFormulario.Controls.Add(this.label2);
            this.panelFormulario.Controls.Add(this.txtnombre);
            this.panelFormulario.Controls.Add(this.label5);
            this.panelFormulario.Controls.Add(this.txtdireccion);
            this.panelFormulario.Controls.Add(this.label6);
            this.panelFormulario.Controls.Add(this.txttelefono);
            this.panelFormulario.Controls.Add(this.label7);
            this.panelFormulario.Controls.Add(this.rbtnactivo);
            this.panelFormulario.Controls.Add(this.rbtninactivo);
            this.panelFormulario.Location = new System.Drawing.Point(37, 90);
            this.panelFormulario.Name = "panelFormulario";
            this.panelFormulario.Size = new System.Drawing.Size(876, 350);
            this.panelFormulario.TabIndex = 1;
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.btnCancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelar.FlatAppearance.BorderSize = 0;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnCancelar.ForeColor = System.Drawing.Color.White;
            this.btnCancelar.Location = new System.Drawing.Point(793, 471);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(120, 45);
            this.btnCancelar.TabIndex = 25;
            this.btnCancelar.Text = "✗ Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // FrmRegistrarCliente
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.ClientSize = new System.Drawing.Size(950, 600);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.txtidcliente);
            this.Controls.Add(this.btnguardar);
            this.Controls.Add(this.panelFormulario);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "FrmRegistrarCliente";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmRegistrarCliente";
            this.Load += new System.EventHandler(this.FrmRegistrarCliente_Load);
            this.panelFormulario.ResumeLayout(false);
            this.panelFormulario.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnguardar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Panel panelFormulario;
        public System.Windows.Forms.TextBox txtnombre;
        public System.Windows.Forms.TextBox txtdireccion;
        public System.Windows.Forms.TextBox txttelefono;
        public System.Windows.Forms.RadioButton rbtnactivo;
        public System.Windows.Forms.RadioButton rbtninactivo;
        public System.Windows.Forms.TextBox txtidcliente;
    }
}