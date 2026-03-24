namespace CapaPresentacion
{
    partial class FrmMenuPrincipal
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.panelSuperior = new System.Windows.Forms.Panel();
            this.btnCerrarSesion = new System.Windows.Forms.Button();
            this.lblRol = new System.Windows.Forms.Label();
            this.lblUsuario = new System.Windows.Forms.Label();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.panelLateral = new System.Windows.Forms.Panel();
            this.btnSalir = new System.Windows.Forms.Button();
            this.panelAdministracion = new System.Windows.Forms.Panel();
            this.btnCategorias = new System.Windows.Forms.Button();
            this.btnSesiones = new System.Windows.Forms.Button();
            this.btnAuditoria = new System.Windows.Forms.Button();
            this.lblAdministracion = new System.Windows.Forms.Label();
            this.panelCaja = new System.Windows.Forms.Panel();
            this.btnPagos = new System.Windows.Forms.Button();
            this.btnCompras = new System.Windows.Forms.Button();
            this.btnVentas = new System.Windows.Forms.Button();
            this.lblCaja = new System.Windows.Forms.Label();
            this.panelVeterinario = new System.Windows.Forms.Panel();
            this.btnConsultas = new System.Windows.Forms.Button();
            this.btnCitas = new System.Windows.Forms.Button();
            this.lblVeterinario = new System.Windows.Forms.Label();
            this.panelGestion = new System.Windows.Forms.Panel();
            this.btnProveedores = new System.Windows.Forms.Button();
            this.btnProductos = new System.Windows.Forms.Button();
            this.btnEmpleados = new System.Windows.Forms.Button();
            this.btnMascotas = new System.Windows.Forms.Button();
            this.btnClientes = new System.Windows.Forms.Button();
            this.lblGestion = new System.Windows.Forms.Label();
            this.panelContenedor = new System.Windows.Forms.Panel();
            this.panelInicio = new System.Windows.Forms.Panel();
            this.lblBienvenida = new System.Windows.Forms.Label();
            this.panelNotifBox = new System.Windows.Forms.Panel();
            this.lblNotifTitulo = new System.Windows.Forms.Label();
            this.lblNotifBadge = new System.Windows.Forms.Label();
            this.panelNotificaciones = new System.Windows.Forms.Panel();
            this.panelSuperior.SuspendLayout();
            this.panelLateral.SuspendLayout();
            this.panelAdministracion.SuspendLayout();
            this.panelCaja.SuspendLayout();
            this.panelVeterinario.SuspendLayout();
            this.panelGestion.SuspendLayout();
            this.panelContenedor.SuspendLayout();
            this.panelInicio.SuspendLayout();
            this.panelNotifBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelSuperior
            // 
            this.panelSuperior.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.panelSuperior.Controls.Add(this.btnCerrarSesion);
            this.panelSuperior.Controls.Add(this.lblRol);
            this.panelSuperior.Controls.Add(this.lblUsuario);
            this.panelSuperior.Controls.Add(this.lblTitulo);
            this.panelSuperior.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSuperior.Location = new System.Drawing.Point(0, 0);
            this.panelSuperior.Name = "panelSuperior";
            this.panelSuperior.Size = new System.Drawing.Size(1278, 80);
            this.panelSuperior.TabIndex = 0;
            // 
            // btnCerrarSesion
            // 
            this.btnCerrarSesion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.btnCerrarSesion.FlatAppearance.BorderSize = 0;
            this.btnCerrarSesion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCerrarSesion.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnCerrarSesion.ForeColor = System.Drawing.Color.White;
            this.btnCerrarSesion.Location = new System.Drawing.Point(1118, 25);
            this.btnCerrarSesion.Name = "btnCerrarSesion";
            this.btnCerrarSesion.Size = new System.Drawing.Size(130, 40);
            this.btnCerrarSesion.TabIndex = 3;
            this.btnCerrarSesion.Text = "Cerrar Sesión";
            this.btnCerrarSesion.UseVisualStyleBackColor = false;
            this.btnCerrarSesion.Click += new System.EventHandler(this.btnCerrarSesion_Click);
            // 
            // lblRol
            // 
            this.lblRol.AutoSize = true;
            this.lblRol.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblRol.ForeColor = System.Drawing.Color.White;
            this.lblRol.Location = new System.Drawing.Point(925, 48);
            this.lblRol.Name = "lblRol";
            this.lblRol.Size = new System.Drawing.Size(50, 20);
            this.lblRol.TabIndex = 2;
            this.lblRol.Text = "Rol: --";
            // 
            // lblUsuario
            // 
            this.lblUsuario.AutoSize = true;
            this.lblUsuario.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblUsuario.ForeColor = System.Drawing.Color.White;
            this.lblUsuario.Location = new System.Drawing.Point(925, 25);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Size = new System.Drawing.Size(91, 23);
            this.lblUsuario.TabIndex = 1;
            this.lblUsuario.Text = "Usuario: --";
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(20, 20);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(340, 41);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "🐾 Sistema Veterinaria";
            // 
            // panelLateral
            // 
            this.panelLateral.AutoScroll = true;
            this.panelLateral.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.panelLateral.Controls.Add(this.btnSalir);
            this.panelLateral.Controls.Add(this.panelAdministracion);
            this.panelLateral.Controls.Add(this.panelCaja);
            this.panelLateral.Controls.Add(this.panelVeterinario);
            this.panelLateral.Controls.Add(this.panelGestion);
            this.panelLateral.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelLateral.Location = new System.Drawing.Point(0, 80);
            this.panelLateral.Name = "panelLateral";
            this.panelLateral.Size = new System.Drawing.Size(250, 682);
            this.panelLateral.TabIndex = 1;
            // 
            // btnSalir
            // 
            this.btnSalir.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(57)))), ((int)(((byte)(43)))));
            this.btnSalir.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSalir.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnSalir.FlatAppearance.BorderSize = 0;
            this.btnSalir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSalir.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSalir.ForeColor = System.Drawing.Color.White;
            this.btnSalir.Location = new System.Drawing.Point(0, 945);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(229, 50);
            this.btnSalir.TabIndex = 4;
            this.btnSalir.Text = "❌ Salir del Sistema";
            this.btnSalir.UseVisualStyleBackColor = false;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // panelAdministracion
            // 
            this.panelAdministracion.Controls.Add(this.btnCategorias);
            this.panelAdministracion.Controls.Add(this.btnSesiones);
            this.panelAdministracion.Controls.Add(this.btnAuditoria);
            this.panelAdministracion.Controls.Add(this.lblAdministracion);
            this.panelAdministracion.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelAdministracion.Location = new System.Drawing.Point(0, 725);
            this.panelAdministracion.Name = "panelAdministracion";
            this.panelAdministracion.Size = new System.Drawing.Size(229, 220);
            this.panelAdministracion.TabIndex = 3;
            // 
            // btnCategorias
            // 
            this.btnCategorias.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(68)))), ((int)(((byte)(173)))));
            this.btnCategorias.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCategorias.FlatAppearance.BorderSize = 0;
            this.btnCategorias.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCategorias.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnCategorias.ForeColor = System.Drawing.Color.White;
            this.btnCategorias.Location = new System.Drawing.Point(20, 160);
            this.btnCategorias.Name = "btnCategorias";
            this.btnCategorias.Size = new System.Drawing.Size(186, 45);
            this.btnCategorias.TabIndex = 3;
            this.btnCategorias.Text = "📂 Categorías";
            this.btnCategorias.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCategorias.UseVisualStyleBackColor = false;
            this.btnCategorias.Click += new System.EventHandler(this.btnCategorias_Click);
            // 
            // btnSesiones
            // 
            this.btnSesiones.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(68)))), ((int)(((byte)(173)))));
            this.btnSesiones.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSesiones.FlatAppearance.BorderSize = 0;
            this.btnSesiones.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSesiones.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnSesiones.ForeColor = System.Drawing.Color.White;
            this.btnSesiones.Location = new System.Drawing.Point(20, 105);
            this.btnSesiones.Name = "btnSesiones";
            this.btnSesiones.Size = new System.Drawing.Size(186, 45);
            this.btnSesiones.TabIndex = 2;
            this.btnSesiones.Text = "⏱️ Sesiones";
            this.btnSesiones.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSesiones.UseVisualStyleBackColor = false;
            this.btnSesiones.Click += new System.EventHandler(this.btnSesiones_Click);
            // 
            // btnAuditoria
            // 
            this.btnAuditoria.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(68)))), ((int)(((byte)(173)))));
            this.btnAuditoria.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAuditoria.FlatAppearance.BorderSize = 0;
            this.btnAuditoria.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAuditoria.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnAuditoria.ForeColor = System.Drawing.Color.White;
            this.btnAuditoria.Location = new System.Drawing.Point(20, 50);
            this.btnAuditoria.Name = "btnAuditoria";
            this.btnAuditoria.Size = new System.Drawing.Size(186, 45);
            this.btnAuditoria.TabIndex = 1;
            this.btnAuditoria.Text = "🔍 Auditoría";
            this.btnAuditoria.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAuditoria.UseVisualStyleBackColor = false;
            this.btnAuditoria.Click += new System.EventHandler(this.btnAuditoria_Click);
            // 
            // lblAdministracion
            // 
            this.lblAdministracion.AutoSize = true;
            this.lblAdministracion.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblAdministracion.ForeColor = System.Drawing.Color.White;
            this.lblAdministracion.Location = new System.Drawing.Point(15, 15);
            this.lblAdministracion.Name = "lblAdministracion";
            this.lblAdministracion.Size = new System.Drawing.Size(147, 25);
            this.lblAdministracion.TabIndex = 0;
            this.lblAdministracion.Text = "Administración";
            // 
            // panelCaja
            // 
            this.panelCaja.Controls.Add(this.btnPagos);
            this.panelCaja.Controls.Add(this.btnCompras);
            this.panelCaja.Controls.Add(this.btnVentas);
            this.panelCaja.Controls.Add(this.lblCaja);
            this.panelCaja.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelCaja.Location = new System.Drawing.Point(0, 501);
            this.panelCaja.Name = "panelCaja";
            this.panelCaja.Size = new System.Drawing.Size(229, 224);
            this.panelCaja.TabIndex = 2;
            // 
            // btnPagos
            // 
            this.btnPagos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(160)))), ((int)(((byte)(133)))));
            this.btnPagos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPagos.FlatAppearance.BorderSize = 0;
            this.btnPagos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPagos.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnPagos.ForeColor = System.Drawing.Color.White;
            this.btnPagos.Location = new System.Drawing.Point(20, 160);
            this.btnPagos.Name = "btnPagos";
            this.btnPagos.Size = new System.Drawing.Size(186, 45);
            this.btnPagos.TabIndex = 3;
            this.btnPagos.Text = "💳 Pagos";
            this.btnPagos.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPagos.UseVisualStyleBackColor = false;
            this.btnPagos.Click += new System.EventHandler(this.btnPagos_Click);
            // 
            // btnCompras
            // 
            this.btnCompras.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(160)))), ((int)(((byte)(133)))));
            this.btnCompras.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCompras.FlatAppearance.BorderSize = 0;
            this.btnCompras.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCompras.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnCompras.ForeColor = System.Drawing.Color.White;
            this.btnCompras.Location = new System.Drawing.Point(20, 105);
            this.btnCompras.Name = "btnCompras";
            this.btnCompras.Size = new System.Drawing.Size(186, 45);
            this.btnCompras.TabIndex = 2;
            this.btnCompras.Text = "🛒 Compras";
            this.btnCompras.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCompras.UseVisualStyleBackColor = false;
            this.btnCompras.Click += new System.EventHandler(this.btnCompras_Click);
            // 
            // btnVentas
            // 
            this.btnVentas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(160)))), ((int)(((byte)(133)))));
            this.btnVentas.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnVentas.FlatAppearance.BorderSize = 0;
            this.btnVentas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVentas.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnVentas.ForeColor = System.Drawing.Color.White;
            this.btnVentas.Location = new System.Drawing.Point(20, 50);
            this.btnVentas.Name = "btnVentas";
            this.btnVentas.Size = new System.Drawing.Size(186, 45);
            this.btnVentas.TabIndex = 1;
            this.btnVentas.Text = "💰 Ventas";
            this.btnVentas.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnVentas.UseVisualStyleBackColor = false;
            this.btnVentas.Click += new System.EventHandler(this.btnVentas_Click);
            // 
            // lblCaja
            // 
            this.lblCaja.AutoSize = true;
            this.lblCaja.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblCaja.ForeColor = System.Drawing.Color.White;
            this.lblCaja.Location = new System.Drawing.Point(15, 15);
            this.lblCaja.Name = "lblCaja";
            this.lblCaja.Size = new System.Drawing.Size(49, 25);
            this.lblCaja.TabIndex = 0;
            this.lblCaja.Text = "Caja";
            // 
            // panelVeterinario
            // 
            this.panelVeterinario.Controls.Add(this.btnConsultas);
            this.panelVeterinario.Controls.Add(this.btnCitas);
            this.panelVeterinario.Controls.Add(this.lblVeterinario);
            this.panelVeterinario.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelVeterinario.Location = new System.Drawing.Point(0, 333);
            this.panelVeterinario.Name = "panelVeterinario";
            this.panelVeterinario.Size = new System.Drawing.Size(229, 168);
            this.panelVeterinario.TabIndex = 1;
            // 
            // btnConsultas
            // 
            this.btnConsultas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(126)))), ((int)(((byte)(34)))));
            this.btnConsultas.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnConsultas.FlatAppearance.BorderSize = 0;
            this.btnConsultas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConsultas.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnConsultas.ForeColor = System.Drawing.Color.White;
            this.btnConsultas.Location = new System.Drawing.Point(20, 105);
            this.btnConsultas.Name = "btnConsultas";
            this.btnConsultas.Size = new System.Drawing.Size(186, 45);
            this.btnConsultas.TabIndex = 2;
            this.btnConsultas.Text = "🩺 Consultas";
            this.btnConsultas.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnConsultas.UseVisualStyleBackColor = false;
            this.btnConsultas.Click += new System.EventHandler(this.btnConsultas_Click);
            // 
            // btnCitas
            // 
            this.btnCitas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(126)))), ((int)(((byte)(34)))));
            this.btnCitas.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCitas.FlatAppearance.BorderSize = 0;
            this.btnCitas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCitas.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnCitas.ForeColor = System.Drawing.Color.White;
            this.btnCitas.Location = new System.Drawing.Point(20, 50);
            this.btnCitas.Name = "btnCitas";
            this.btnCitas.Size = new System.Drawing.Size(186, 45);
            this.btnCitas.TabIndex = 1;
            this.btnCitas.Text = "📅 Citas";
            this.btnCitas.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCitas.UseVisualStyleBackColor = false;
            this.btnCitas.Click += new System.EventHandler(this.btnCitas_Click);
            // 
            // lblVeterinario
            // 
            this.lblVeterinario.AutoSize = true;
            this.lblVeterinario.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblVeterinario.ForeColor = System.Drawing.Color.White;
            this.lblVeterinario.Location = new System.Drawing.Point(15, 15);
            this.lblVeterinario.Name = "lblVeterinario";
            this.lblVeterinario.Size = new System.Drawing.Size(111, 25);
            this.lblVeterinario.TabIndex = 0;
            this.lblVeterinario.Text = "Veterinario";
            // 
            // panelGestion
            // 
            this.panelGestion.Controls.Add(this.btnProveedores);
            this.panelGestion.Controls.Add(this.btnProductos);
            this.panelGestion.Controls.Add(this.btnEmpleados);
            this.panelGestion.Controls.Add(this.btnMascotas);
            this.panelGestion.Controls.Add(this.btnClientes);
            this.panelGestion.Controls.Add(this.lblGestion);
            this.panelGestion.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelGestion.Location = new System.Drawing.Point(0, 0);
            this.panelGestion.Name = "panelGestion";
            this.panelGestion.Size = new System.Drawing.Size(229, 333);
            this.panelGestion.TabIndex = 0;
            // 
            // btnProveedores
            // 
            this.btnProveedores.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.btnProveedores.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnProveedores.FlatAppearance.BorderSize = 0;
            this.btnProveedores.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnProveedores.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnProveedores.ForeColor = System.Drawing.Color.White;
            this.btnProveedores.Location = new System.Drawing.Point(20, 215);
            this.btnProveedores.Name = "btnProveedores";
            this.btnProveedores.Size = new System.Drawing.Size(186, 45);
            this.btnProveedores.TabIndex = 5;
            this.btnProveedores.Text = "🏭 Proveedores";
            this.btnProveedores.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnProveedores.UseVisualStyleBackColor = false;
            this.btnProveedores.Click += new System.EventHandler(this.btnProveedores_Click);
            // 
            // btnProductos
            // 
            this.btnProductos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.btnProductos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnProductos.FlatAppearance.BorderSize = 0;
            this.btnProductos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnProductos.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnProductos.ForeColor = System.Drawing.Color.White;
            this.btnProductos.Location = new System.Drawing.Point(20, 160);
            this.btnProductos.Name = "btnProductos";
            this.btnProductos.Size = new System.Drawing.Size(186, 45);
            this.btnProductos.TabIndex = 4;
            this.btnProductos.Text = "📦 Productos";
            this.btnProductos.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnProductos.UseVisualStyleBackColor = false;
            this.btnProductos.Click += new System.EventHandler(this.btnProductos_Click);
            // 
            // btnEmpleados
            // 
            this.btnEmpleados.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.btnEmpleados.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEmpleados.FlatAppearance.BorderSize = 0;
            this.btnEmpleados.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEmpleados.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnEmpleados.ForeColor = System.Drawing.Color.White;
            this.btnEmpleados.Location = new System.Drawing.Point(20, 270);
            this.btnEmpleados.Name = "btnEmpleados";
            this.btnEmpleados.Size = new System.Drawing.Size(186, 45);
            this.btnEmpleados.TabIndex = 3;
            this.btnEmpleados.Text = "👥 Empleados";
            this.btnEmpleados.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEmpleados.UseVisualStyleBackColor = false;
            this.btnEmpleados.Click += new System.EventHandler(this.btnEmpleados_Click);
            // 
            // btnMascotas
            // 
            this.btnMascotas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.btnMascotas.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMascotas.FlatAppearance.BorderSize = 0;
            this.btnMascotas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMascotas.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnMascotas.ForeColor = System.Drawing.Color.White;
            this.btnMascotas.Location = new System.Drawing.Point(20, 105);
            this.btnMascotas.Name = "btnMascotas";
            this.btnMascotas.Size = new System.Drawing.Size(186, 45);
            this.btnMascotas.TabIndex = 2;
            this.btnMascotas.Text = "🐾 Mascotas";
            this.btnMascotas.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMascotas.UseVisualStyleBackColor = false;
            this.btnMascotas.Click += new System.EventHandler(this.btnMascotas_Click);
            // 
            // btnClientes
            // 
            this.btnClientes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.btnClientes.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClientes.FlatAppearance.BorderSize = 0;
            this.btnClientes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClientes.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnClientes.ForeColor = System.Drawing.Color.White;
            this.btnClientes.Location = new System.Drawing.Point(20, 50);
            this.btnClientes.Name = "btnClientes";
            this.btnClientes.Size = new System.Drawing.Size(186, 45);
            this.btnClientes.TabIndex = 1;
            this.btnClientes.Text = "👤 Clientes";
            this.btnClientes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClientes.UseVisualStyleBackColor = false;
            this.btnClientes.Click += new System.EventHandler(this.btnClientes_Click);
            // 
            // lblGestion
            // 
            this.lblGestion.AutoSize = true;
            this.lblGestion.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblGestion.ForeColor = System.Drawing.Color.White;
            this.lblGestion.Location = new System.Drawing.Point(15, 15);
            this.lblGestion.Name = "lblGestion";
            this.lblGestion.Size = new System.Drawing.Size(80, 25);
            this.lblGestion.TabIndex = 0;
            this.lblGestion.Text = "Gestión";
            // 
            // panelContenedor
            // 
            this.panelContenedor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.panelContenedor.Controls.Add(this.panelInicio);
            this.panelContenedor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContenedor.Location = new System.Drawing.Point(250, 80);
            this.panelContenedor.Name = "panelContenedor";
            this.panelContenedor.Size = new System.Drawing.Size(1028, 682);
            this.panelContenedor.TabIndex = 2;
            // 
            // panelInicio
            // 
            this.panelInicio.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.panelInicio.Controls.Add(this.lblBienvenida);
            this.panelInicio.Controls.Add(this.panelNotifBox);
            this.panelInicio.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelInicio.Location = new System.Drawing.Point(0, 0);
            this.panelInicio.Name = "panelInicio";
            this.panelInicio.Size = new System.Drawing.Size(1028, 682);
            this.panelInicio.TabIndex = 0;
            // 
            // lblBienvenida
            // 
            this.lblBienvenida.AutoSize = true;
            this.lblBienvenida.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.lblBienvenida.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblBienvenida.Location = new System.Drawing.Point(40, 40);
            this.lblBienvenida.Name = "lblBienvenida";
            this.lblBienvenida.Size = new System.Drawing.Size(504, 54);
            this.lblBienvenida.TabIndex = 0;
            this.lblBienvenida.Text = "Bienvenido al Sistema 🐾";
            // 
            // panelNotifBox
            // 
            this.panelNotifBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panelNotifBox.BackColor = System.Drawing.Color.White;
            this.panelNotifBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelNotifBox.Controls.Add(this.lblNotifTitulo);
            this.panelNotifBox.Controls.Add(this.lblNotifBadge);
            this.panelNotifBox.Controls.Add(this.panelNotificaciones);
            this.panelNotifBox.Location = new System.Drawing.Point(49, 140);
            this.panelNotifBox.Name = "panelNotifBox";
            this.panelNotifBox.Size = new System.Drawing.Size(495, 233);
            this.panelNotifBox.TabIndex = 1;
            // 
            // lblNotifTitulo
            // 
            this.lblNotifTitulo.AutoSize = true;
            this.lblNotifTitulo.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblNotifTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblNotifTitulo.Location = new System.Drawing.Point(12, 12);
            this.lblNotifTitulo.Name = "lblNotifTitulo";
            this.lblNotifTitulo.Size = new System.Drawing.Size(229, 28);
            this.lblNotifTitulo.TabIndex = 0;
            this.lblNotifTitulo.Text = "🔔 Alertas del Sistema";
            // 
            // lblNotifBadge
            // 
            this.lblNotifBadge.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.lblNotifBadge.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.lblNotifBadge.ForeColor = System.Drawing.Color.White;
            this.lblNotifBadge.Location = new System.Drawing.Point(264, 14);
            this.lblNotifBadge.Name = "lblNotifBadge";
            this.lblNotifBadge.Size = new System.Drawing.Size(26, 26);
            this.lblNotifBadge.TabIndex = 1;
            this.lblNotifBadge.Text = "0";
            this.lblNotifBadge.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblNotifBadge.Visible = false;
            // 
            // panelNotificaciones
            // 
            this.panelNotificaciones.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelNotificaciones.AutoScroll = true;
            this.panelNotificaciones.BackColor = System.Drawing.Color.White;
            this.panelNotificaciones.Location = new System.Drawing.Point(0, 48);
            this.panelNotificaciones.Name = "panelNotificaciones";
            this.panelNotificaciones.Size = new System.Drawing.Size(493, 183);
            this.panelNotificaciones.TabIndex = 2;
            // 
            // FrmMenuPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1278, 762);
            this.Controls.Add(this.panelContenedor);
            this.Controls.Add(this.panelLateral);
            this.Controls.Add(this.panelSuperior);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmMenuPrincipal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Menú Principal - Sistema Veterinaria";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmMenuPrincipal_Load);
            this.panelSuperior.ResumeLayout(false);
            this.panelSuperior.PerformLayout();
            this.panelLateral.ResumeLayout(false);
            this.panelAdministracion.ResumeLayout(false);
            this.panelAdministracion.PerformLayout();
            this.panelCaja.ResumeLayout(false);
            this.panelCaja.PerformLayout();
            this.panelVeterinario.ResumeLayout(false);
            this.panelVeterinario.PerformLayout();
            this.panelGestion.ResumeLayout(false);
            this.panelGestion.PerformLayout();
            this.panelContenedor.ResumeLayout(false);
            this.panelInicio.ResumeLayout(false);
            this.panelInicio.PerformLayout();
            this.panelNotifBox.ResumeLayout(false);
            this.panelNotifBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelSuperior;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblUsuario;
        private System.Windows.Forms.Label lblRol;
        private System.Windows.Forms.Button btnCerrarSesion;
        private System.Windows.Forms.Panel panelLateral;
        private System.Windows.Forms.Panel panelGestion;
        private System.Windows.Forms.Label lblGestion;
        private System.Windows.Forms.Button btnClientes;
        private System.Windows.Forms.Button btnMascotas;
        private System.Windows.Forms.Button btnEmpleados;
        private System.Windows.Forms.Button btnProductos;
        private System.Windows.Forms.Button btnProveedores;
        private System.Windows.Forms.Panel panelVeterinario;
        private System.Windows.Forms.Label lblVeterinario;
        private System.Windows.Forms.Button btnCitas;
        private System.Windows.Forms.Button btnConsultas;
        private System.Windows.Forms.Panel panelCaja;
        private System.Windows.Forms.Label lblCaja;
        private System.Windows.Forms.Button btnVentas;
        private System.Windows.Forms.Button btnCompras;
        private System.Windows.Forms.Button btnPagos;
        private System.Windows.Forms.Panel panelAdministracion;
        private System.Windows.Forms.Label lblAdministracion;
        private System.Windows.Forms.Button btnAuditoria;
        private System.Windows.Forms.Button btnSesiones;
        private System.Windows.Forms.Button btnCategorias;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.Panel panelContenedor;
        // Pantalla inicio
        private System.Windows.Forms.Panel panelInicio;
        private System.Windows.Forms.Label lblBienvenida;
        private System.Windows.Forms.Panel panelNotifBox;
        private System.Windows.Forms.Label lblNotifTitulo;
        private System.Windows.Forms.Label lblNotifBadge;
        internal System.Windows.Forms.Panel panelNotificaciones;  // internal para que CargarNotificaciones pueda poblarlo
    }
}