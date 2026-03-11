using System;
using System.Data;
using CapaDatos;

namespace CapaNegocio
{
    // =========================================================================
    // CN_VENTA — Lógica de negocio para ventas
    // =========================================================================
    public class CN_Venta
    {
        private static CD_Venta objDato = new CD_Venta();

        /// <summary>
        /// Inicia una venta nueva y devuelve el idventa.
        /// Devuelve -1 si hay error.
        /// </summary>
        public static int CrearVenta(int idcliente, string usuario)
        {
            if (string.IsNullOrWhiteSpace(usuario))
                return -1;

            CD_Venta obj = new CD_Venta
            {
                Idcliente = idcliente,
                Usuario = usuario
            };
            return objDato.CrearVenta(obj);
        }

        /// <summary>
        /// Agrega un producto al carrito de la venta activa.
        /// </summary>
        public static string AgregarProducto(int idventa, int idproducto, int cantidad)
        {
            if (idventa <= 0) return "Venta inválida";
            if (idproducto <= 0) return "Producto inválido";
            if (cantidad <= 0) return "La cantidad debe ser mayor a 0";

            return objDato.AgregarDetalle(idventa, idproducto, cantidad);
        }

        /// <summary>
        /// Quita un producto del carrito (devuelve stock).
        /// </summary>
        public static string QuitarProducto(int iddetalle)
        {
            if (iddetalle <= 0) return "Detalle inválido";
            return objDato.EliminarDetalle(iddetalle);
        }

        /// <summary>
        /// Confirma la venta: cierra, recalcula totales y aplica regla 10%.
        /// </summary>
        public static string ConfirmarVenta(int idventa, DataTable detalle)
        {
            if (idventa <= 0)
                return "Venta inválida";

            if (detalle == null || detalle.Rows.Count == 0)
                return "No puede confirmar una venta sin productos";

            return objDato.ConfirmarVenta(idventa);
        }

        /// <summary>
        /// Cancela la venta y devuelve el stock de todos sus productos.
        /// </summary>
        public static string CancelarVenta(int idventa)
        {
            if (idventa <= 0) return "Venta inválida";
            return objDato.CancelarVenta(idventa);
        }

        /// <summary>
        /// Lista ventas filtradas por rango de fechas y estado.
        /// </summary>
        public static DataTable Listar(DateTime fechaInicio, DateTime fechaFin, string estado = "TODAS")
        {
            if (fechaInicio > fechaFin)
                return new DataTable("Ventas");

            return objDato.Listar(fechaInicio, fechaFin.AddDays(1).AddSeconds(-1), estado);
        }

        /// <summary>
        /// Devuelve el detalle (carrito) de una venta.
        /// </summary>
        public static DataTable ObtenerDetalle(int idventa)
        {
            if (idventa <= 0) return new DataTable("DetalleVenta");
            return objDato.ObtenerDetalle(idventa);
        }

        /// <summary>
        /// Clientes activos para el ComboBox del formulario de venta.
        /// </summary>
        public static DataTable ObtenerClientes()
        {
            return objDato.ObtenerClientes();
        }

        /// <summary>
        /// Búsqueda de productos con stock disponible.
        /// </summary>
        public static DataTable BuscarProducto(string buscar)
        {
            return objDato.BuscarProducto(buscar);
        }

        /// <summary>
        /// Calcula el total del carrito actual (sin IVA / con IVA).
        /// </summary>
        public static (decimal subtotal, decimal iva, decimal total) CalcularTotales(DataTable detalle)
        {
            decimal subtotal = 0;
            if (detalle != null)
                foreach (DataRow row in detalle.Rows)
                    subtotal += Convert.ToDecimal(row["subtotal"]);

            decimal iva = Math.Round(subtotal * 0.16m, 2);
            decimal total = subtotal + iva;
            return (subtotal, iva, total);
        }
    }

    // =========================================================================
    // CN_COMPRA — Lógica de negocio para compras a proveedores
    // =========================================================================
    public class CN_Compra
    {
        private static CD_Compra objDato = new CD_Compra();

        /// <summary>
        /// Inicia una compra nueva y devuelve el idcompra.
        /// </summary>
        public static int CrearCompra(int idproveedor, string usuario)
        {
            if (string.IsNullOrWhiteSpace(usuario))
                return -1;
            if (idproveedor <= 0)
                return -1;

            CD_Compra obj = new CD_Compra
            {
                Idproveedor = idproveedor,
                Usuario = usuario
            };
            return objDato.CrearCompra(obj);
        }

        /// <summary>
        /// Agrega un producto a la compra activa.
        /// </summary>
        public static string AgregarProducto(int idcompra, int idproducto, int cantidad, decimal precioUnit)
        {
            if (idcompra <= 0) return "Compra inválida";
            if (idproducto <= 0) return "Producto inválido";
            if (cantidad <= 0) return "La cantidad debe ser mayor a 0";
            if (precioUnit <= 0) return "El precio unitario debe ser mayor a $0";

            return objDato.AgregarDetalle(idcompra, idproducto, cantidad, precioUnit);
        }

        /// <summary>
        /// Confirma la compra y cierra el documento.
        /// </summary>
        public static string ConfirmarCompra(int idcompra, DataTable detalle)
        {
            if (idcompra <= 0)
                return "Compra inválida";

            if (detalle == null || detalle.Rows.Count == 0)
                return "No puede confirmar una compra sin productos";

            return objDato.ConfirmarCompra(idcompra);
        }

        /// <summary>
        /// Lista de compras con filtro de fechas.
        /// </summary>
        public static DataTable Listar(DateTime fechaInicio, DateTime fechaFin)
        {
            if (fechaInicio > fechaFin)
                return new DataTable("Compras");

            return objDato.Listar(fechaInicio, fechaFin.AddDays(1).AddSeconds(-1));
        }

        /// <summary>
        /// Proveedores activos para el ComboBox.
        /// </summary>
        public static DataTable ObtenerProveedores()
        {
            return objDato.ObtenerProveedores();
        }

        /// <summary>
        /// Búsqueda de productos para agregar a la compra.
        /// </summary>
        public static DataTable BuscarProducto(string buscar)
        {
            return objDato.ObtenerProductos(buscar);
        }

        /// <summary>
        /// Historial de movimientos de stock.
        /// </summary>
        public static DataTable ListarMovimientos(DateTime fechaInicio, DateTime fechaFin, string tipo = "TODOS")
        {
            return objDato.ListarMovimientos(
                fechaInicio,
                fechaFin.AddDays(1).AddSeconds(-1),
                tipo);
        }

        /// <summary>
        /// Calcula el total de la compra actual.
        /// </summary>
        public static (decimal subtotal, decimal iva, decimal total) CalcularTotales(DataTable detalle)
        {
            decimal subtotal = 0;
            if (detalle != null)
                foreach (DataRow row in detalle.Rows)
                    subtotal += Convert.ToDecimal(row["subtotal"]);

            decimal iva = Math.Round(subtotal * 0.16m, 2);
            decimal total = subtotal + iva;
            return (subtotal, iva, total);
        }
    }
}