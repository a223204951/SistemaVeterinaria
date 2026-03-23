using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace CapaPresentacion
{
    partial class FrmCompras
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblCompraId = new System.Windows.Forms.Label();
            this.panelCatalogo = new System.Windows.Forms.Panel();
            this.lblCatalogo = new System.Windows.Forms.Label();
            this.txtBuscarProducto = new System.Windows.Forms.TextBox();
            this.dgvProductos = new System.Windows.Forms.DataGridView();
            this.lblCantidad = new System.Windows.Forms.Label();
            this.nudCantidad = new System.Windows.Forms.NumericUpDown();
            this.lblPrecioCompra = new System.Windows.Forms.Label();
            this.nudPrecioCompra = new System.Windows.Forms.NumericUpDown();
            this.lblSubtotalPreview = new System.Windows.Forms.Label();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.panelCarrito = new System.Windows.Forms.Panel();
            this.lblCarritoTitulo = new System.Windows.Forms.Label();
            this.dgvCarrito = new System.Windows.Forms.DataGridView();
            this.btnQuitar = new System.Windows.Forms.Button();
            this.panelResumen = new System.Windows.Forms.Panel();
            this.lblProveedorLbl = new System.Windows.Forms.Label();
            this.cmbProveedor = new System.Windows.Forms.ComboBox();
            this.panelTotales = new System.Windows.Forms.Panel();
            this.lblSubtotal = new System.Windows.Forms.Label();
            this.lblIva = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.btnNuevaCompra = new System.Windows.Forms.Button();
            this.btnConfirmar = new System.Windows.Forms.Button();
            this.btnCancelarCompra = new System.Windows.Forms.Button();
            this.panelCatalogo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCantidad)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPrecioCompra)).BeginInit();
            this.panelCarrito.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCarrito)).BeginInit();
            this.panelResumen.SuspendLayout();
            this.panelTotales.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblTitulo.Location = new System.Drawing.Point(20, 15);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(323, 37);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "📥 Módulo de Compras";
            // 
            // lblCompraId
            // 
            this.lblCompraId.AutoSize = true;
            this.lblCompraId.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblCompraId.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(165)))), ((int)(((byte)(166)))));
            this.lblCompraId.Location = new System.Drawing.Point(20, 55);
            this.lblCompraId.Name = "lblCompraId";
            this.lblCompraId.Size = new System.Drawing.Size(169, 25);
            this.lblCompraId.TabIndex = 1;
            this.lblCompraId.Text = "Sin compra activa";
            // 
            // panelCatalogo
            // 
            this.panelCatalogo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panelCatalogo.BackColor = System.Drawing.Color.White;
            this.panelCatalogo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelCatalogo.Controls.Add(this.lblCatalogo);
            this.panelCatalogo.Controls.Add(this.txtBuscarProducto);
            this.panelCatalogo.Controls.Add(this.dgvProductos);
            this.panelCatalogo.Controls.Add(this.lblCantidad);
            this.panelCatalogo.Controls.Add(this.nudCantidad);
            this.panelCatalogo.Controls.Add(this.lblPrecioCompra);
            this.panelCatalogo.Controls.Add(this.nudPrecioCompra);
            this.panelCatalogo.Controls.Add(this.lblSubtotalPreview);
            this.panelCatalogo.Controls.Add(this.btnAgregar);
            this.panelCatalogo.Location = new System.Drawing.Point(15, 85);
            this.panelCatalogo.Name = "panelCatalogo";
            this.panelCatalogo.Size = new System.Drawing.Size(370, 495);
            this.panelCatalogo.TabIndex = 2;
            // 
            // lblCatalogo
            // 
            this.lblCatalogo.AutoSize = true;
            this.lblCatalogo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblCatalogo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblCatalogo.Location = new System.Drawing.Point(10, 10);
            this.lblCatalogo.Name = "lblCatalogo";
            this.lblCatalogo.Size = new System.Drawing.Size(221, 23);
            this.lblCatalogo.TabIndex = 0;
            this.lblCatalogo.Text = "📦 Catálogo de Productos";
            // 
            // txtBuscarProducto
            // 
            this.txtBuscarProducto.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBuscarProducto.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtBuscarProducto.ForeColor = System.Drawing.Color.Gray;
            this.txtBuscarProducto.Location = new System.Drawing.Point(10, 38);
            this.txtBuscarProducto.Name = "txtBuscarProducto";
            this.txtBuscarProducto.Size = new System.Drawing.Size(345, 30);
            this.txtBuscarProducto.TabIndex = 1;
            this.txtBuscarProducto.Text = "Buscar producto...";
            this.txtBuscarProducto.TextChanged += new System.EventHandler(this.txtBuscarProducto_TextChanged);
            this.txtBuscarProducto.GotFocus += new System.EventHandler(this.txtBuscarProducto_GotFocus);
            this.txtBuscarProducto.LostFocus += new System.EventHandler(this.txtBuscarProducto_LostFocus);
            // 
            // dgvProductos
            // 
            this.dgvProductos.AllowUserToAddRows = false;
            this.dgvProductos.AllowUserToDeleteRows = false;
            this.dgvProductos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvProductos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvProductos.BackgroundColor = System.Drawing.Color.White;
            this.dgvProductos.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.dgvProductos.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvProductos.ColumnHeadersHeight = 35;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvProductos.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvProductos.EnableHeadersVisualStyles = false;
            this.dgvProductos.Location = new System.Drawing.Point(10, 75);
            this.dgvProductos.Name = "dgvProductos";
            this.dgvProductos.ReadOnly = true;
            this.dgvProductos.RowHeadersVisible = false;
            this.dgvProductos.RowHeadersWidth = 51;
            this.dgvProductos.RowTemplate.Height = 30;
            this.dgvProductos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvProductos.Size = new System.Drawing.Size(345, 310);
            this.dgvProductos.TabIndex = 2;
            this.dgvProductos.SelectionChanged += new System.EventHandler(this.dgvProductos_SelectionChanged);
            this.dgvProductos.DoubleClick += new System.EventHandler(this.dgvProductos_DoubleClick);
            // 
            // lblCantidad
            // 
            this.lblCantidad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblCantidad.AutoSize = true;
            this.lblCantidad.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblCantidad.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblCantidad.Location = new System.Drawing.Point(10, 395);
            this.lblCantidad.Name = "lblCantidad";
            this.lblCantidad.Size = new System.Drawing.Size(75, 20);
            this.lblCantidad.TabIndex = 3;
            this.lblCantidad.Text = "Cantidad:";
            // 
            // nudCantidad
            // 
            this.nudCantidad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.nudCantidad.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.nudCantidad.Location = new System.Drawing.Point(10, 415);
            this.nudCantidad.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.nudCantidad.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudCantidad.Name = "nudCantidad";
            this.nudCantidad.Size = new System.Drawing.Size(70, 30);
            this.nudCantidad.TabIndex = 4;
            this.nudCantidad.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudCantidad.ValueChanged += new System.EventHandler(this.nudCantidad_ValueChanged);
            // 
            // lblPrecioCompra
            // 
            this.lblPrecioCompra.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblPrecioCompra.AutoSize = true;
            this.lblPrecioCompra.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblPrecioCompra.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblPrecioCompra.Location = new System.Drawing.Point(90, 395);
            this.lblPrecioCompra.Name = "lblPrecioCompra";
            this.lblPrecioCompra.Size = new System.Drawing.Size(138, 20);
            this.lblPrecioCompra.TabIndex = 5;
            this.lblPrecioCompra.Text = "Precio compra ($):";
            // 
            // nudPrecioCompra
            // 
            this.nudPrecioCompra.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.nudPrecioCompra.DecimalPlaces = 2;
            this.nudPrecioCompra.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.nudPrecioCompra.Location = new System.Drawing.Point(90, 415);
            this.nudPrecioCompra.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.nudPrecioCompra.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.nudPrecioCompra.Name = "nudPrecioCompra";
            this.nudPrecioCompra.Size = new System.Drawing.Size(100, 30);
            this.nudPrecioCompra.TabIndex = 6;
            this.nudPrecioCompra.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudPrecioCompra.ValueChanged += new System.EventHandler(this.nudPrecioCompra_ValueChanged);
            // 
            // lblSubtotalPreview
            // 
            this.lblSubtotalPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblSubtotalPreview.AutoSize = true;
            this.lblSubtotalPreview.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblSubtotalPreview.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(165)))), ((int)(((byte)(166)))));
            this.lblSubtotalPreview.Location = new System.Drawing.Point(200, 420);
            this.lblSubtotalPreview.Name = "lblSubtotalPreview";
            this.lblSubtotalPreview.Size = new System.Drawing.Size(116, 20);
            this.lblSubtotalPreview.TabIndex = 7;
            this.lblSubtotalPreview.Text = "Subtotal: $0.00";
            // 
            // btnAgregar
            // 
            this.btnAgregar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAgregar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnAgregar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAgregar.FlatAppearance.BorderSize = 0;
            this.btnAgregar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAgregar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnAgregar.ForeColor = System.Drawing.Color.White;
            this.btnAgregar.Location = new System.Drawing.Point(10, 455);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(345, 38);
            this.btnAgregar.TabIndex = 8;
            this.btnAgregar.Text = "➕ Agregar al Carrito";
            this.btnAgregar.UseVisualStyleBackColor = false;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // panelCarrito
            // 
            this.panelCarrito.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelCarrito.BackColor = System.Drawing.Color.White;
            this.panelCarrito.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelCarrito.Controls.Add(this.lblCarritoTitulo);
            this.panelCarrito.Controls.Add(this.dgvCarrito);
            this.panelCarrito.Controls.Add(this.btnQuitar);
            this.panelCarrito.Location = new System.Drawing.Point(391, 85);
            this.panelCarrito.Name = "panelCarrito";
            this.panelCarrito.Size = new System.Drawing.Size(310, 495);
            this.panelCarrito.TabIndex = 3;
            // 
            // lblCarritoTitulo
            // 
            this.lblCarritoTitulo.AutoSize = true;
            this.lblCarritoTitulo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblCarritoTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblCarritoTitulo.Location = new System.Drawing.Point(10, 10);
            this.lblCarritoTitulo.Name = "lblCarritoTitulo";
            this.lblCarritoTitulo.Size = new System.Drawing.Size(190, 23);
            this.lblCarritoTitulo.TabIndex = 0;
            this.lblCarritoTitulo.Text = "📋 Detalle de Compra";
            // 
            // dgvCarrito
            // 
            this.dgvCarrito.AllowUserToAddRows = false;
            this.dgvCarrito.AllowUserToDeleteRows = false;
            this.dgvCarrito.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvCarrito.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCarrito.BackgroundColor = System.Drawing.Color.White;
            this.dgvCarrito.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvCarrito.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvCarrito.ColumnHeadersHeight = 35;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvCarrito.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvCarrito.EnableHeadersVisualStyles = false;
            this.dgvCarrito.Location = new System.Drawing.Point(10, 38);
            this.dgvCarrito.Name = "dgvCarrito";
            this.dgvCarrito.ReadOnly = true;
            this.dgvCarrito.RowHeadersVisible = false;
            this.dgvCarrito.RowHeadersWidth = 51;
            this.dgvCarrito.RowTemplate.Height = 30;
            this.dgvCarrito.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCarrito.Size = new System.Drawing.Size(285, 405);
            this.dgvCarrito.TabIndex = 1;
            // 
            // btnQuitar
            // 
            this.btnQuitar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnQuitar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.btnQuitar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnQuitar.FlatAppearance.BorderSize = 0;
            this.btnQuitar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQuitar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnQuitar.ForeColor = System.Drawing.Color.White;
            this.btnQuitar.Location = new System.Drawing.Point(10, 455);
            this.btnQuitar.Name = "btnQuitar";
            this.btnQuitar.Size = new System.Drawing.Size(285, 38);
            this.btnQuitar.TabIndex = 2;
            this.btnQuitar.Text = "🗑️ Quitar seleccionado";
            this.btnQuitar.UseVisualStyleBackColor = false;
            this.btnQuitar.Click += new System.EventHandler(this.btnQuitar_Click);
            // 
            // panelResumen
            // 
            this.panelResumen.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelResumen.BackColor = System.Drawing.Color.White;
            this.panelResumen.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelResumen.Controls.Add(this.lblProveedorLbl);
            this.panelResumen.Controls.Add(this.cmbProveedor);
            this.panelResumen.Controls.Add(this.panelTotales);
            this.panelResumen.Controls.Add(this.btnNuevaCompra);
            this.panelResumen.Controls.Add(this.btnConfirmar);
            this.panelResumen.Controls.Add(this.btnCancelarCompra);
            this.panelResumen.Location = new System.Drawing.Point(707, 85);
            this.panelResumen.Name = "panelResumen";
            this.panelResumen.Size = new System.Drawing.Size(228, 495);
            this.panelResumen.TabIndex = 4;
            // 
            // lblProveedorLbl
            // 
            this.lblProveedorLbl.AutoSize = true;
            this.lblProveedorLbl.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblProveedorLbl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblProveedorLbl.Location = new System.Drawing.Point(10, 10);
            this.lblProveedorLbl.Name = "lblProveedorLbl";
            this.lblProveedorLbl.Size = new System.Drawing.Size(86, 20);
            this.lblProveedorLbl.TabIndex = 0;
            this.lblProveedorLbl.Text = "Proveedor:";
            // 
            // cmbProveedor
            // 
            this.cmbProveedor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbProveedor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProveedor.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cmbProveedor.Location = new System.Drawing.Point(10, 30);
            this.cmbProveedor.Name = "cmbProveedor";
            this.cmbProveedor.Size = new System.Drawing.Size(208, 28);
            this.cmbProveedor.TabIndex = 1;
            // 
            // panelTotales
            // 
            this.panelTotales.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelTotales.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.panelTotales.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelTotales.Controls.Add(this.lblSubtotal);
            this.panelTotales.Controls.Add(this.lblIva);
            this.panelTotales.Controls.Add(this.lblTotal);
            this.panelTotales.Location = new System.Drawing.Point(10, 75);
            this.panelTotales.Name = "panelTotales";
            this.panelTotales.Size = new System.Drawing.Size(208, 105);
            this.panelTotales.TabIndex = 2;
            // 
            // lblSubtotal
            // 
            this.lblSubtotal.AutoSize = true;
            this.lblSubtotal.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblSubtotal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblSubtotal.Location = new System.Drawing.Point(5, 8);
            this.lblSubtotal.Name = "lblSubtotal";
            this.lblSubtotal.Size = new System.Drawing.Size(106, 19);
            this.lblSubtotal.TabIndex = 0;
            this.lblSubtotal.Text = "Subtotal:  $0.00";
            // 
            // lblIva
            // 
            this.lblIva.AutoSize = true;
            this.lblIva.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblIva.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblIva.Location = new System.Drawing.Point(5, 38);
            this.lblIva.Name = "lblIva";
            this.lblIva.Size = new System.Drawing.Size(111, 19);
            this.lblIva.TabIndex = 1;
            this.lblIva.Text = "IVA (16%): $0.00";
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTotal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblTotal.Location = new System.Drawing.Point(5, 70);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(136, 23);
            this.lblTotal.TabIndex = 2;
            this.lblTotal.Text = "TOTAL:     $0.00";
            // 
            // btnNuevaCompra
            // 
            this.btnNuevaCompra.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNuevaCompra.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnNuevaCompra.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNuevaCompra.FlatAppearance.BorderSize = 0;
            this.btnNuevaCompra.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNuevaCompra.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnNuevaCompra.ForeColor = System.Drawing.Color.White;
            this.btnNuevaCompra.Location = new System.Drawing.Point(10, 195);
            this.btnNuevaCompra.Name = "btnNuevaCompra";
            this.btnNuevaCompra.Size = new System.Drawing.Size(208, 40);
            this.btnNuevaCompra.TabIndex = 3;
            this.btnNuevaCompra.Text = "🆕 Nueva";
            this.btnNuevaCompra.UseVisualStyleBackColor = false;
            this.btnNuevaCompra.Click += new System.EventHandler(this.btnNuevaCompra_Click);
            // 
            // btnConfirmar
            // 
            this.btnConfirmar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConfirmar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnConfirmar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnConfirmar.FlatAppearance.BorderSize = 0;
            this.btnConfirmar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfirmar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnConfirmar.ForeColor = System.Drawing.Color.White;
            this.btnConfirmar.Location = new System.Drawing.Point(10, 248);
            this.btnConfirmar.Name = "btnConfirmar";
            this.btnConfirmar.Size = new System.Drawing.Size(208, 40);
            this.btnConfirmar.TabIndex = 4;
            this.btnConfirmar.Text = "✅ Confirmar";
            this.btnConfirmar.UseVisualStyleBackColor = false;
            this.btnConfirmar.Click += new System.EventHandler(this.btnConfirmar_Click);
            // 
            // btnCancelarCompra
            // 
            this.btnCancelarCompra.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelarCompra.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.btnCancelarCompra.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelarCompra.FlatAppearance.BorderSize = 0;
            this.btnCancelarCompra.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelarCompra.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnCancelarCompra.ForeColor = System.Drawing.Color.White;
            this.btnCancelarCompra.Location = new System.Drawing.Point(10, 302);
            this.btnCancelarCompra.Name = "btnCancelarCompra";
            this.btnCancelarCompra.Size = new System.Drawing.Size(208, 40);
            this.btnCancelarCompra.TabIndex = 5;
            this.btnCancelarCompra.Text = "❌ Cancelar";
            this.btnCancelarCompra.UseVisualStyleBackColor = false;
            this.btnCancelarCompra.Click += new System.EventHandler(this.btnCancelarCompra_Click);
            // 
            // FrmCompras
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.ClientSize = new System.Drawing.Size(950, 600);
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.lblCompraId);
            this.Controls.Add(this.panelCatalogo);
            this.Controls.Add(this.panelCarrito);
            this.Controls.Add(this.panelResumen);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmCompras";
            this.Text = "Compras";
            this.Load += new System.EventHandler(this.FrmCompras_Load);
            this.panelCatalogo.ResumeLayout(false);
            this.panelCatalogo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCantidad)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPrecioCompra)).EndInit();
            this.panelCarrito.ResumeLayout(false);
            this.panelCarrito.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCarrito)).EndInit();
            this.panelResumen.ResumeLayout(false);
            this.panelResumen.PerformLayout();
            this.panelTotales.ResumeLayout(false);
            this.panelTotales.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private Label lblTitulo, lblCompraId, lblSubtotalPreview;
        private Panel panelCatalogo, panelCarrito, panelResumen, panelTotales;
        private Label lblCatalogo, lblCantidad, lblPrecioCompra, lblCarritoTitulo;
        private Label lblProveedorLbl, lblSubtotal, lblIva, lblTotal;
        private TextBox txtBuscarProducto;
        private DataGridView dgvProductos, dgvCarrito;
        private NumericUpDown nudCantidad, nudPrecioCompra;
        private ComboBox cmbProveedor;
        private Button btnAgregar, btnQuitar, btnNuevaCompra, btnConfirmar;
        private Button btnCancelarCompra;

        // ── Métodos de placeholder para txtBuscarProducto ─────────────────────
        private void txtBuscarProducto_GotFocus(object sender, System.EventArgs e)
        {
            if (this.txtBuscarProducto.Text == "Buscar producto...")
            {
                this.txtBuscarProducto.Text = "";
                this.txtBuscarProducto.ForeColor = System.Drawing.Color.Black;
            }
        }

        private void txtBuscarProducto_LostFocus(object sender, System.EventArgs e)
        {
            if (this.txtBuscarProducto.Text == "")
            {
                this.txtBuscarProducto.Text = "Buscar producto...";
                this.txtBuscarProducto.ForeColor = System.Drawing.Color.Gray;
            }
        }
    }
}