using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace CapaPresentacion
{
    partial class FrmVentas
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
            this.lblVentaId = new System.Windows.Forms.Label();
            this.panelCatalogo = new System.Windows.Forms.Panel();
            this.lblCatalogo = new System.Windows.Forms.Label();
            this.txtBuscarProducto = new System.Windows.Forms.TextBox();
            this.txtScanner = new System.Windows.Forms.TextBox();
            this.lblScannerInfo = new System.Windows.Forms.Label();
            this.dgvProductos = new System.Windows.Forms.DataGridView();
            this.lblCantidad = new System.Windows.Forms.Label();
            this.nudCantidad = new System.Windows.Forms.NumericUpDown();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.panelCarrito = new System.Windows.Forms.Panel();
            this.lblCarrito = new System.Windows.Forms.Label();
            this.dgvCarrito = new System.Windows.Forms.DataGridView();
            this.btnQuitar = new System.Windows.Forms.Button();
            this.panelResumen = new System.Windows.Forms.Panel();
            this.lblClienteLbl = new System.Windows.Forms.Label();
            this.cmbCliente = new System.Windows.Forms.ComboBox();
            this.panelTotales = new System.Windows.Forms.Panel();
            this.lblSubtotal = new System.Windows.Forms.Label();
            this.lblIva = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.btnNuevaVenta = new System.Windows.Forms.Button();
            this.btnConfirmar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.lblInfoPrecio = new System.Windows.Forms.Label();
            this.panelCatalogo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCantidad)).BeginInit();
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
            this.lblTitulo.Size = new System.Drawing.Size(294, 37);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "🛒 Módulo de Ventas";
            // 
            // lblVentaId
            // 
            this.lblVentaId.AutoSize = true;
            this.lblVentaId.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblVentaId.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(165)))), ((int)(((byte)(166)))));
            this.lblVentaId.Location = new System.Drawing.Point(20, 55);
            this.lblVentaId.Name = "lblVentaId";
            this.lblVentaId.Size = new System.Drawing.Size(150, 25);
            this.lblVentaId.TabIndex = 1;
            this.lblVentaId.Text = "Sin venta activa";
            // 
            // panelCatalogo
            // 
            this.panelCatalogo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panelCatalogo.BackColor = System.Drawing.Color.White;
            this.panelCatalogo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelCatalogo.Controls.Add(this.lblCatalogo);
            this.panelCatalogo.Controls.Add(this.txtBuscarProducto);
            this.panelCatalogo.Controls.Add(this.txtScanner);
            this.panelCatalogo.Controls.Add(this.lblScannerInfo);
            this.panelCatalogo.Controls.Add(this.dgvProductos);
            this.panelCatalogo.Controls.Add(this.lblCantidad);
            this.panelCatalogo.Controls.Add(this.nudCantidad);
            this.panelCatalogo.Controls.Add(this.btnAgregar);
            this.panelCatalogo.Location = new System.Drawing.Point(15, 85);
            this.panelCatalogo.Name = "panelCatalogo";
            this.panelCatalogo.Size = new System.Drawing.Size(364, 490);
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
            this.txtBuscarProducto.Size = new System.Drawing.Size(339, 30);
            this.txtBuscarProducto.TabIndex = 1;
            this.txtBuscarProducto.Text = "Buscar producto...";
            this.txtBuscarProducto.TextChanged += new System.EventHandler(this.txtBuscarProducto_TextChanged);
            this.txtBuscarProducto.GotFocus += new System.EventHandler(this.txtBuscarProducto_GotFocus);
            this.txtBuscarProducto.LostFocus += new System.EventHandler(this.txtBuscarProducto_LostFocus);
            // 
            // txtScanner
            // 
            this.txtScanner.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtScanner.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtScanner.ForeColor = System.Drawing.Color.Gray;
            this.txtScanner.Location = new System.Drawing.Point(10, 72);
            this.txtScanner.Name = "txtScanner";
            this.txtScanner.Size = new System.Drawing.Size(339, 27);
            this.txtScanner.TabIndex = 2;
            this.txtScanner.Text = "🔲 Escanear código de barras...";
            this.txtScanner.GotFocus += new System.EventHandler(this.txtScanner_GotFocus);
            this.txtScanner.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtScanner_KeyDown);
            this.txtScanner.LostFocus += new System.EventHandler(this.txtScanner_LostFocus);
            // 
            // lblScannerInfo
            // 
            this.lblScannerInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblScannerInfo.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Italic);
            this.lblScannerInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(68)))), ((int)(((byte)(173)))));
            this.lblScannerInfo.Location = new System.Drawing.Point(10, 100);
            this.lblScannerInfo.Name = "lblScannerInfo";
            this.lblScannerInfo.Size = new System.Drawing.Size(339, 16);
            this.lblScannerInfo.TabIndex = 3;
            this.lblScannerInfo.Text = "Escanea o presiona Enter para agregar";
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
            this.dgvProductos.Location = new System.Drawing.Point(10, 118);
            this.dgvProductos.Name = "dgvProductos";
            this.dgvProductos.ReadOnly = true;
            this.dgvProductos.RowHeadersVisible = false;
            this.dgvProductos.RowHeadersWidth = 51;
            this.dgvProductos.RowTemplate.Height = 30;
            this.dgvProductos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvProductos.Size = new System.Drawing.Size(339, 330);
            this.dgvProductos.TabIndex = 4;
            this.dgvProductos.DoubleClick += new System.EventHandler(this.dgvProductos_DoubleClick);
            // 
            // lblCantidad
            // 
            this.lblCantidad.AutoSize = true;
            this.lblCantidad.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblCantidad.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblCantidad.Location = new System.Drawing.Point(10, 415);
            this.lblCantidad.Name = "lblCantidad";
            this.lblCantidad.Size = new System.Drawing.Size(75, 20);
            this.lblCantidad.TabIndex = 5;
            this.lblCantidad.Text = "Cantidad:";
            // 
            // nudCantidad
            // 
            this.nudCantidad.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.nudCantidad.Location = new System.Drawing.Point(10, 438);
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
            this.nudCantidad.Size = new System.Drawing.Size(80, 30);
            this.nudCantidad.TabIndex = 6;
            this.nudCantidad.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
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
            this.btnAgregar.Location = new System.Drawing.Point(100, 435);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(249, 38);
            this.btnAgregar.TabIndex = 7;
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
            this.panelCarrito.Controls.Add(this.lblCarrito);
            this.panelCarrito.Controls.Add(this.dgvCarrito);
            this.panelCarrito.Controls.Add(this.btnQuitar);
            this.panelCarrito.Location = new System.Drawing.Point(385, 85);
            this.panelCarrito.Name = "panelCarrito";
            this.panelCarrito.Size = new System.Drawing.Size(503, 490);
            this.panelCarrito.TabIndex = 3;
            // 
            // lblCarrito
            // 
            this.lblCarrito.AutoSize = true;
            this.lblCarrito.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblCarrito.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblCarrito.Location = new System.Drawing.Point(10, 10);
            this.lblCarrito.Name = "lblCarrito";
            this.lblCarrito.Size = new System.Drawing.Size(170, 23);
            this.lblCarrito.TabIndex = 0;
            this.lblCarrito.Text = "🛒 Carrito de Venta";
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
            this.dgvCarrito.Size = new System.Drawing.Size(478, 395);
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
            this.btnQuitar.Location = new System.Drawing.Point(10, 440);
            this.btnQuitar.Name = "btnQuitar";
            this.btnQuitar.Size = new System.Drawing.Size(478, 38);
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
            this.panelResumen.Controls.Add(this.lblClienteLbl);
            this.panelResumen.Controls.Add(this.cmbCliente);
            this.panelResumen.Controls.Add(this.panelTotales);
            this.panelResumen.Controls.Add(this.btnNuevaVenta);
            this.panelResumen.Controls.Add(this.btnConfirmar);
            this.panelResumen.Controls.Add(this.btnCancelar);
            this.panelResumen.Controls.Add(this.lblInfoPrecio);
            this.panelResumen.Location = new System.Drawing.Point(894, 85);
            this.panelResumen.Name = "panelResumen";
            this.panelResumen.Size = new System.Drawing.Size(274, 490);
            this.panelResumen.TabIndex = 4;
            // 
            // lblClienteLbl
            // 
            this.lblClienteLbl.AutoSize = true;
            this.lblClienteLbl.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblClienteLbl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblClienteLbl.Location = new System.Drawing.Point(10, 10);
            this.lblClienteLbl.Name = "lblClienteLbl";
            this.lblClienteLbl.Size = new System.Drawing.Size(61, 20);
            this.lblClienteLbl.TabIndex = 0;
            this.lblClienteLbl.Text = "Cliente:";
            // 
            // cmbCliente
            // 
            this.cmbCliente.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbCliente.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCliente.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cmbCliente.Location = new System.Drawing.Point(10, 30);
            this.cmbCliente.Name = "cmbCliente";
            this.cmbCliente.Size = new System.Drawing.Size(254, 28);
            this.cmbCliente.TabIndex = 1;
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
            this.panelTotales.Size = new System.Drawing.Size(254, 100);
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
            this.lblIva.Location = new System.Drawing.Point(5, 35);
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
            this.lblTotal.Location = new System.Drawing.Point(5, 65);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(136, 23);
            this.lblTotal.TabIndex = 2;
            this.lblTotal.Text = "TOTAL:     $0.00";
            // 
            // btnNuevaVenta
            // 
            this.btnNuevaVenta.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnNuevaVenta.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNuevaVenta.FlatAppearance.BorderSize = 0;
            this.btnNuevaVenta.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNuevaVenta.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnNuevaVenta.ForeColor = System.Drawing.Color.White;
            this.btnNuevaVenta.Location = new System.Drawing.Point(10, 190);
            this.btnNuevaVenta.Name = "btnNuevaVenta";
            this.btnNuevaVenta.Size = new System.Drawing.Size(254, 40);
            this.btnNuevaVenta.TabIndex = 3;
            this.btnNuevaVenta.Text = "🆕 Nueva Venta";
            this.btnNuevaVenta.UseVisualStyleBackColor = false;
            this.btnNuevaVenta.Click += new System.EventHandler(this.btnNuevaVenta_Click);
            // 
            // btnConfirmar
            // 
            this.btnConfirmar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnConfirmar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnConfirmar.FlatAppearance.BorderSize = 0;
            this.btnConfirmar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfirmar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnConfirmar.ForeColor = System.Drawing.Color.White;
            this.btnConfirmar.Location = new System.Drawing.Point(10, 245);
            this.btnConfirmar.Name = "btnConfirmar";
            this.btnConfirmar.Size = new System.Drawing.Size(254, 40);
            this.btnConfirmar.TabIndex = 4;
            this.btnConfirmar.Text = "✅ Confirmar Venta";
            this.btnConfirmar.UseVisualStyleBackColor = false;
            this.btnConfirmar.Click += new System.EventHandler(this.btnConfirmar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.btnCancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelar.FlatAppearance.BorderSize = 0;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnCancelar.ForeColor = System.Drawing.Color.White;
            this.btnCancelar.Location = new System.Drawing.Point(10, 300);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(254, 40);
            this.btnCancelar.TabIndex = 5;
            this.btnCancelar.Text = "✗ Cancelar Venta";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // lblInfoPrecio
            // 
            this.lblInfoPrecio.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Italic);
            this.lblInfoPrecio.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(68)))), ((int)(((byte)(173)))));
            this.lblInfoPrecio.Location = new System.Drawing.Point(10, 355);
            this.lblInfoPrecio.Name = "lblInfoPrecio";
            this.lblInfoPrecio.Size = new System.Drawing.Size(229, 60);
            this.lblInfoPrecio.TabIndex = 6;
            this.lblInfoPrecio.Text = "ℹ️ Al confirmar:\n• Vendido +10%\n• Otros -10%";
            // 
            // FrmVentas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.lblVentaId);
            this.Controls.Add(this.panelCatalogo);
            this.Controls.Add(this.panelCarrito);
            this.Controls.Add(this.panelResumen);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmVentas";
            this.Text = "Ventas";
            this.Load += new System.EventHandler(this.FrmVentas_Load);
            this.panelCatalogo.ResumeLayout(false);
            this.panelCatalogo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCantidad)).EndInit();
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

        // ── Métodos de placeholder para txtScanner ────────────────────────────
        private void txtScanner_GotFocus(object sender, System.EventArgs e)
        {
            if (this.txtScanner.Text == "🔲 Escanear código de barras...")
            {
                this.txtScanner.Text = "";
                this.txtScanner.ForeColor = System.Drawing.Color.FromArgb(142, 68, 173);
            }
        }

        private void txtScanner_LostFocus(object sender, System.EventArgs e)
        {
            if (this.txtScanner.Text == "")
            {
                this.txtScanner.Text = "🔲 Escanear código de barras...";
                this.txtScanner.ForeColor = System.Drawing.Color.Gray;
            }
        }

        #endregion

        private Label lblTitulo, lblVentaId;
        private Panel panelCatalogo, panelCarrito, panelResumen, panelTotales;
        private Label lblCatalogo, lblCantidad, lblCarrito;
        private Label lblClienteLbl, lblSubtotal, lblIva, lblTotal, lblInfoPrecio;
        private TextBox txtBuscarProducto;
        private DataGridView dgvProductos, dgvCarrito;
        private NumericUpDown nudCantidad;
        private ComboBox cmbCliente;
        private Button btnAgregar, btnQuitar, btnNuevaVenta, btnConfirmar, btnCancelar;
        private TextBox txtScanner;
        private Label lblScannerInfo;
    }
}