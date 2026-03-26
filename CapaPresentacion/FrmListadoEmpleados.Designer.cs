using System.Drawing;
using System.Windows.Forms;

namespace CapaPresentacion
{
    partial class FrmListadoEmpleados
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.panelBusqueda = new System.Windows.Forms.Panel();
            this.groupBoxBuscar = new System.Windows.Forms.GroupBox();
            this.rbtNombre = new System.Windows.Forms.RadioButton();
            this.rbtId = new System.Windows.Forms.RadioButton();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.panelFiltros = new System.Windows.Forms.Panel();
            this.lblFiltrosTitulo = new System.Windows.Forms.Label();
            this.chkVeterinario = new System.Windows.Forms.CheckBox();
            this.chkCajero = new System.Windows.Forms.CheckBox();
            this.chkAsistente = new System.Windows.Forms.CheckBox();
            this.chkAdministrador = new System.Windows.Forms.CheckBox();
            this.chkInactivo = new System.Windows.Forms.CheckBox();
            this.dgvEmpleados = new System.Windows.Forms.DataGridView();
            this.panelBotones = new System.Windows.Forms.Panel();
            this.lblTotal = new System.Windows.Forms.Label();
            this.btnNuevo = new System.Windows.Forms.Button();
            this.btnEditar = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.panelBusqueda.SuspendLayout();
            this.groupBoxBuscar.SuspendLayout();
            this.panelFiltros.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEmpleados)).BeginInit();
            this.panelBotones.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblTitulo.Location = new System.Drawing.Point(20, 20);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(346, 37);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "👥 Gestión de Empleados";
            // 
            // panelBusqueda
            // 
            this.panelBusqueda.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelBusqueda.BackColor = System.Drawing.Color.White;
            this.panelBusqueda.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelBusqueda.Controls.Add(this.groupBoxBuscar);
            this.panelBusqueda.Controls.Add(this.txtBuscar);
            this.panelBusqueda.Controls.Add(this.btnBuscar);
            this.panelBusqueda.Controls.Add(this.btnLimpiar);
            this.panelBusqueda.Location = new System.Drawing.Point(27, 70);
            this.panelBusqueda.Name = "panelBusqueda";
            this.panelBusqueda.Size = new System.Drawing.Size(1150, 100);
            this.panelBusqueda.TabIndex = 1;
            // 
            // groupBoxBuscar
            // 
            this.groupBoxBuscar.Controls.Add(this.rbtNombre);
            this.groupBoxBuscar.Controls.Add(this.rbtId);
            this.groupBoxBuscar.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.groupBoxBuscar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.groupBoxBuscar.Location = new System.Drawing.Point(15, 12);
            this.groupBoxBuscar.Name = "groupBoxBuscar";
            this.groupBoxBuscar.Size = new System.Drawing.Size(184, 72);
            this.groupBoxBuscar.TabIndex = 0;
            this.groupBoxBuscar.TabStop = false;
            this.groupBoxBuscar.Text = "Buscar por:";
            // 
            // rbtNombre
            // 
            this.rbtNombre.AutoSize = true;
            this.rbtNombre.Checked = true;
            this.rbtNombre.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbtNombre.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.rbtNombre.Location = new System.Drawing.Point(10, 30);
            this.rbtNombre.Name = "rbtNombre";
            this.rbtNombre.Size = new System.Drawing.Size(85, 24);
            this.rbtNombre.TabIndex = 0;
            this.rbtNombre.TabStop = true;
            this.rbtNombre.Text = "Nombre";
            this.rbtNombre.UseVisualStyleBackColor = true;
            // 
            // rbtId
            // 
            this.rbtId.AutoSize = true;
            this.rbtId.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbtId.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.rbtId.Location = new System.Drawing.Point(121, 30);
            this.rbtId.Name = "rbtId";
            this.rbtId.Size = new System.Drawing.Size(45, 24);
            this.rbtId.TabIndex = 1;
            this.rbtId.Text = "ID";
            this.rbtId.UseVisualStyleBackColor = true;
            // 
            // txtBuscar
            // 
            this.txtBuscar.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtBuscar.Location = new System.Drawing.Point(205, 38);
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(734, 30);
            this.txtBuscar.TabIndex = 1;
            this.txtBuscar.TextChanged += new System.EventHandler(this.txtBuscar_TextChanged);
            // 
            // btnBuscar
            // 
            this.btnBuscar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnBuscar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBuscar.FlatAppearance.BorderSize = 0;
            this.btnBuscar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnBuscar.ForeColor = System.Drawing.Color.White;
            this.btnBuscar.Location = new System.Drawing.Point(945, 35);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(85, 35);
            this.btnBuscar.TabIndex = 2;
            this.btnBuscar.Text = "🔍 Buscar";
            this.btnBuscar.UseVisualStyleBackColor = false;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(165)))), ((int)(((byte)(166)))));
            this.btnLimpiar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLimpiar.FlatAppearance.BorderSize = 0;
            this.btnLimpiar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLimpiar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnLimpiar.ForeColor = System.Drawing.Color.White;
            this.btnLimpiar.Location = new System.Drawing.Point(1038, 35);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(85, 35);
            this.btnLimpiar.TabIndex = 3;
            this.btnLimpiar.Text = "🔄 Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = false;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // panelFiltros
            // 
            this.panelFiltros.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelFiltros.BackColor = System.Drawing.Color.White;
            this.panelFiltros.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelFiltros.Controls.Add(this.lblFiltrosTitulo);
            this.panelFiltros.Controls.Add(this.chkVeterinario);
            this.panelFiltros.Controls.Add(this.chkCajero);
            this.panelFiltros.Controls.Add(this.chkAsistente);
            this.panelFiltros.Controls.Add(this.chkAdministrador);
            this.panelFiltros.Controls.Add(this.chkInactivo);
            this.panelFiltros.Location = new System.Drawing.Point(27, 182);
            this.panelFiltros.Name = "panelFiltros";
            this.panelFiltros.Size = new System.Drawing.Size(1150, 52);
            this.panelFiltros.TabIndex = 2;
            // 
            // lblFiltrosTitulo
            // 
            this.lblFiltrosTitulo.AutoSize = true;
            this.lblFiltrosTitulo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblFiltrosTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblFiltrosTitulo.Location = new System.Drawing.Point(15, 16);
            this.lblFiltrosTitulo.Name = "lblFiltrosTitulo";
            this.lblFiltrosTitulo.Size = new System.Drawing.Size(115, 20);
            this.lblFiltrosTitulo.TabIndex = 0;
            this.lblFiltrosTitulo.Text = "Filtrar por tipo:";
            // 
            // chkVeterinario
            // 
            this.chkVeterinario.AutoSize = true;
            this.chkVeterinario.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkVeterinario.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.chkVeterinario.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(160)))), ((int)(((byte)(133)))));
            this.chkVeterinario.Location = new System.Drawing.Point(163, 14);
            this.chkVeterinario.Name = "chkVeterinario";
            this.chkVeterinario.Size = new System.Drawing.Size(134, 24);
            this.chkVeterinario.TabIndex = 1;
            this.chkVeterinario.Text = "🟢 Veterinario";
            this.chkVeterinario.UseVisualStyleBackColor = true;
            this.chkVeterinario.CheckedChanged += new System.EventHandler(this.chkFiltro_CheckedChanged);
            // 
            // chkCajero
            // 
            this.chkCajero.AutoSize = true;
            this.chkCajero.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkCajero.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.chkCajero.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.chkCajero.Location = new System.Drawing.Point(316, 13);
            this.chkCajero.Name = "chkCajero";
            this.chkCajero.Size = new System.Drawing.Size(101, 24);
            this.chkCajero.TabIndex = 2;
            this.chkCajero.Text = "🔵 Cajero";
            this.chkCajero.UseVisualStyleBackColor = true;
            this.chkCajero.CheckedChanged += new System.EventHandler(this.chkFiltro_CheckedChanged);
            // 
            // chkAsistente
            // 
            this.chkAsistente.AutoSize = true;
            this.chkAsistente.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkAsistente.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.chkAsistente.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(126)))), ((int)(((byte)(34)))));
            this.chkAsistente.Location = new System.Drawing.Point(446, 14);
            this.chkAsistente.Name = "chkAsistente";
            this.chkAsistente.Size = new System.Drawing.Size(123, 24);
            this.chkAsistente.TabIndex = 3;
            this.chkAsistente.Text = "🟠 Asistente";
            this.chkAsistente.UseVisualStyleBackColor = true;
            this.chkAsistente.CheckedChanged += new System.EventHandler(this.chkFiltro_CheckedChanged);
            // 
            // chkAdministrador
            // 
            this.chkAdministrador.AutoSize = true;
            this.chkAdministrador.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkAdministrador.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.chkAdministrador.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(68)))), ((int)(((byte)(173)))));
            this.chkAdministrador.Location = new System.Drawing.Point(592, 14);
            this.chkAdministrador.Name = "chkAdministrador";
            this.chkAdministrador.Size = new System.Drawing.Size(159, 24);
            this.chkAdministrador.TabIndex = 4;
            this.chkAdministrador.Text = "🟣 Administrador";
            this.chkAdministrador.UseVisualStyleBackColor = true;
            this.chkAdministrador.CheckedChanged += new System.EventHandler(this.chkFiltro_CheckedChanged);
            // 
            // chkInactivo
            // 
            this.chkInactivo.AutoSize = true;
            this.chkInactivo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkInactivo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.chkInactivo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(165)))), ((int)(((byte)(166)))));
            this.chkInactivo.Location = new System.Drawing.Point(766, 14);
            this.chkInactivo.Name = "chkInactivo";
            this.chkInactivo.Size = new System.Drawing.Size(105, 24);
            this.chkInactivo.TabIndex = 5;
            this.chkInactivo.Text = "⬜ Inactivo";
            this.chkInactivo.UseVisualStyleBackColor = true;
            this.chkInactivo.CheckedChanged += new System.EventHandler(this.chkFiltro_CheckedChanged);
            // 
            // dgvEmpleados
            // 
            this.dgvEmpleados.AllowUserToAddRows = false;
            this.dgvEmpleados.AllowUserToDeleteRows = false;
            this.dgvEmpleados.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvEmpleados.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvEmpleados.BackgroundColor = System.Drawing.Color.White;
            this.dgvEmpleados.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvEmpleados.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvEmpleados.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvEmpleados.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvEmpleados.ColumnHeadersHeight = 40;
            this.dgvEmpleados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvEmpleados.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvEmpleados.EnableHeadersVisualStyles = false;
            this.dgvEmpleados.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(231)))), ((int)(((byte)(231)))));
            this.dgvEmpleados.Location = new System.Drawing.Point(27, 246);
            this.dgvEmpleados.Name = "dgvEmpleados";
            this.dgvEmpleados.ReadOnly = true;
            this.dgvEmpleados.RowHeadersVisible = false;
            this.dgvEmpleados.RowHeadersWidth = 51;
            this.dgvEmpleados.RowTemplate.Height = 35;
            this.dgvEmpleados.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvEmpleados.Size = new System.Drawing.Size(1150, 285);
            this.dgvEmpleados.TabIndex = 3;
            this.dgvEmpleados.DoubleClick += new System.EventHandler(this.dgvEmpleados_DoubleClick);
            // 
            // panelBotones
            // 
            this.panelBotones.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelBotones.Controls.Add(this.lblTotal);
            this.panelBotones.Controls.Add(this.btnNuevo);
            this.panelBotones.Controls.Add(this.btnEditar);
            this.panelBotones.Controls.Add(this.btnEliminar);
            this.panelBotones.Location = new System.Drawing.Point(27, 540);
            this.panelBotones.Name = "panelBotones";
            this.panelBotones.Size = new System.Drawing.Size(1150, 50);
            this.panelBotones.TabIndex = 4;
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTotal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblTotal.Location = new System.Drawing.Point(5, 15);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(161, 23);
            this.lblTotal.TabIndex = 0;
            this.lblTotal.Text = "Total: 0 empleados";
            // 
            // btnNuevo
            // 
            this.btnNuevo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnNuevo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNuevo.FlatAppearance.BorderSize = 0;
            this.btnNuevo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNuevo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnNuevo.ForeColor = System.Drawing.Color.White;
            this.btnNuevo.Location = new System.Drawing.Point(794, 7);
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(104, 38);
            this.btnNuevo.TabIndex = 1;
            this.btnNuevo.Text = "➕ Nuevo";
            this.btnNuevo.UseVisualStyleBackColor = false;
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // btnEditar
            // 
            this.btnEditar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(196)))), ((int)(((byte)(15)))));
            this.btnEditar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEditar.FlatAppearance.BorderSize = 0;
            this.btnEditar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnEditar.ForeColor = System.Drawing.Color.White;
            this.btnEditar.Location = new System.Drawing.Point(904, 7);
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(100, 38);
            this.btnEditar.TabIndex = 2;
            this.btnEditar.Text = "✏️ Editar";
            this.btnEditar.UseVisualStyleBackColor = false;
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click);
            // 
            // btnEliminar
            // 
            this.btnEliminar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.btnEliminar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEliminar.FlatAppearance.BorderSize = 0;
            this.btnEliminar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEliminar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnEliminar.ForeColor = System.Drawing.Color.White;
            this.btnEliminar.Location = new System.Drawing.Point(1010, 7);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(120, 38);
            this.btnEliminar.TabIndex = 3;
            this.btnEliminar.Text = "🗑️ Eliminar";
            this.btnEliminar.UseVisualStyleBackColor = false;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // FrmListadoEmpleados
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.panelBusqueda);
            this.Controls.Add(this.panelFiltros);
            this.Controls.Add(this.dgvEmpleados);
            this.Controls.Add(this.panelBotones);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmListadoEmpleados";
            this.Text = "Gestión de Empleados";
            this.Load += new System.EventHandler(this.FrmListadoEmpleados_Load);
            this.panelBusqueda.ResumeLayout(false);
            this.panelBusqueda.PerformLayout();
            this.groupBoxBuscar.ResumeLayout(false);
            this.groupBoxBuscar.PerformLayout();
            this.panelFiltros.ResumeLayout(false);
            this.panelFiltros.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEmpleados)).EndInit();
            this.panelBotones.ResumeLayout(false);
            this.panelBotones.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        // ── Declaraciones ─────────────────────────────────────────────────────
        private System.Windows.Forms.Label lblTitulo;

        // Panel búsqueda
        private System.Windows.Forms.Panel panelBusqueda;
        private System.Windows.Forms.GroupBox groupBoxBuscar;
        private System.Windows.Forms.RadioButton rbtNombre;
        private System.Windows.Forms.RadioButton rbtId;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.Button btnLimpiar;

        // Panel filtros por tipo/estado
        private System.Windows.Forms.Panel panelFiltros;
        private System.Windows.Forms.Label lblFiltrosTitulo;
        private System.Windows.Forms.CheckBox chkVeterinario;
        private System.Windows.Forms.CheckBox chkCajero;
        private System.Windows.Forms.CheckBox chkAsistente;
        private System.Windows.Forms.CheckBox chkAdministrador;
        private System.Windows.Forms.CheckBox chkInactivo;

        // Grid y botones
        private System.Windows.Forms.DataGridView dgvEmpleados;
        private System.Windows.Forms.Panel panelBotones;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Button btnNuevo;
        private System.Windows.Forms.Button btnEditar;
        private System.Windows.Forms.Button btnEliminar;
    }
}