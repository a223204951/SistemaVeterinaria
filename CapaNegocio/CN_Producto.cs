using System;
using System.Data;
using CapaDatos;

namespace CapaNegocio
{
    /// <summary>
    /// CAPA DE NEGOCIO - GESTIÓN DE PRODUCTOS
    /// Incluye validaciones y lógica de negocio para el sistema de precios dinámicos
    /// </summary>
    public class CN_Producto
    {
        // INSTANCIA DE LA CAPA DE DATOS
        private static CD_Producto objDato = new CD_Producto();

        /// <summary>
        /// MÉTODO PARA LISTAR TODOS LOS PRODUCTOS
        /// </summary>
        public static DataTable Listar()
        {
            return objDato.Listar();
        }

        /// <summary>
        /// MÉTODO PARA GUARDAR UN NUEVO PRODUCTO
        /// Incluye validaciones de negocio
        /// </summary>
        public static string Guardar(string nombre, string descripcion, decimal precio, int stock,
                                     string estado, string categoria, bool esMedicamento, DateTime? fechaVencimiento)
        {
            // =============================================
            // VALIDACIONES DE NEGOCIO
            // =============================================

            // VALIDAR NOMBRE
            if (string.IsNullOrWhiteSpace(nombre))
                return "El nombre del producto es obligatorio";

            if (nombre.Length < 3)
                return "El nombre debe tener al menos 3 caracteres";

            // VALIDAR PRECIO
            if (precio <= 0)
                return "El precio debe ser mayor a $0";

            if (precio > 999999.99m)
                return "El precio no puede exceder $999,999.99";

            // VALIDAR STOCK
            if (stock < 0)
                return "El stock no puede ser negativo";

            if (stock > 999999)
                return "El stock no puede exceder 999,999 unidades";

            // VALIDAR CATEGORÍA
            if (string.IsNullOrWhiteSpace(categoria))
                return "Debe seleccionar una categoría";

            // VALIDAR FECHA DE VENCIMIENTO PARA MEDICAMENTOS
            if (esMedicamento)
            {
                if (!fechaVencimiento.HasValue)
                    return "Los medicamentos deben tener fecha de vencimiento";

                if (fechaVencimiento.Value < DateTime.Now.Date)
                    return "La fecha de vencimiento no puede ser anterior a hoy";
            }

            // =============================================
            // CREAR OBJETO Y GUARDAR
            // =============================================
            CD_Producto obj = new CD_Producto
            {
                Nombre = nombre.Trim(),
                Descripcion = descripcion?.Trim() ?? "",
                Precio = precio,
                Stock = stock,
                Estado = estado,
                Categoria = categoria,
                EsMedicamento = esMedicamento,
                FechaVencimiento = fechaVencimiento
            };

            return objDato.Guardar(obj);
        }

        /// <summary>
        /// MÉTODO PARA EDITAR UN PRODUCTO EXISTENTE
        /// </summary>
        public static string Editar(int idproducto, string nombre, string descripcion, decimal precio,
                                   int stock, string estado, string categoria, bool esMedicamento, DateTime? fechaVencimiento)
        {
            // VALIDACIONES
            if (idproducto <= 0)
                return "ID de producto inválido";

            if (string.IsNullOrWhiteSpace(nombre))
                return "El nombre del producto es obligatorio";

            if (precio <= 0)
                return "El precio debe ser mayor a $0";

            if (stock < 0)
                return "El stock no puede ser negativo";

            if (string.IsNullOrWhiteSpace(categoria))
                return "Debe seleccionar una categoría";

            if (esMedicamento && !fechaVencimiento.HasValue)
                return "Los medicamentos deben tener fecha de vencimiento";

            // CREAR OBJETO Y EDITAR
            CD_Producto obj = new CD_Producto
            {
                Idproducto = idproducto,
                Nombre = nombre.Trim(),
                Descripcion = descripcion?.Trim() ?? "",
                Precio = precio,
                Stock = stock,
                Estado = estado,
                Categoria = categoria,
                EsMedicamento = esMedicamento,
                FechaVencimiento = fechaVencimiento
            };

            return objDato.Editar(obj);
        }

        /// <summary>
        /// MÉTODO PARA ELIMINAR UN PRODUCTO
        /// </summary>
        public static string Eliminar(int idproducto)
        {
            if (idproducto <= 0)
                return "ID de producto inválido";

            CD_Producto obj = new CD_Producto
            {
                Idproducto = idproducto
            };

            return objDato.Eliminar(obj);
        }

        /// <summary>
        /// MÉTODO PARA BUSCAR PRODUCTOS POR NOMBRE
        /// </summary>
        public static DataTable BuscarNombre(string nombre)
        {
            CD_Producto obj = new CD_Producto
            {
                Buscar = nombre
            };

            return objDato.BuscarNombre(obj);
        }

        /// <summary>
        /// MÉTODO PARA BUSCAR PRODUCTOS POR CATEGORÍA
        /// </summary>
        public static DataTable BuscarCategoria(string categoria)
        {
            return objDato.BuscarCategoria(categoria);
        }

        /// <summary>
        /// MÉTODO PARA AJUSTAR PRECIOS DESPUÉS DE UNA VENTA
        /// Implementa la lógica de precios dinámicos
        /// </summary>
        public static string AjustarPreciosVenta(int idproductoVendido, int cantidadVendida)
        {
            if (idproductoVendido <= 0)
                return "ID de producto inválido";

            if (cantidadVendida <= 0)
                return "La cantidad vendida debe ser mayor a 0";

            return objDato.AjustarPreciosVenta(idproductoVendido, cantidadVendida);
        }

        /// <summary>
        /// MÉTODO PARA AJUSTAR PRECIOS EN COMPRA MÚLTIPLE
        /// Todos los productos comprados suben 10%
        /// </summary>
        public static string AjustarPreciosCompraMultiple(string idsProductos)
        {
            if (string.IsNullOrWhiteSpace(idsProductos))
                return "No se proporcionaron IDs de productos";

            return objDato.AjustarPreciosCompraMultiple(idsProductos);
        }

        /// <summary>
        /// MÉTODO PARA OBTENER HISTORIAL DE PRECIOS DE UN PRODUCTO
        /// </summary>
        public static DataTable ObtenerHistorialPrecios(int idproducto)
        {
            if (idproducto <= 0)
                return null;

            return objDato.ObtenerHistorialPrecios(idproducto);
        }

        /// <summary>
        /// MÉTODO PARA OBTENER LISTA DE CATEGORÍAS DISPONIBLES
        /// </summary>
        public static string[] ObtenerCategorias()
        {
            return new string[]
            {
                "Alimentos",
                "Antiparasitarios",
                "Vacunas",
                "Medicamentos",
                "Higiene",
                "Accesorios",
                "Juguetes",
                "Suplementos",
                "Camas y Transportadoras",
                "Otros"
            };
        }

        /// <summary>
        /// MÉTODO PARA VALIDAR PRODUCTOS PRÓXIMOS A VENCER
        /// Retorna productos que vencen en los próximos 30 días
        /// </summary>
        public static DataTable ObtenerProductosProximosVencer()
        {
            DataTable todos = objDato.Listar();
            DataTable proximosVencer = todos.Clone(); // Clonar estructura

            DateTime fechaLimite = DateTime.Now.AddDays(30);

            foreach (DataRow row in todos.Rows)
            {
                if (row["fecha_vencimiento"] != DBNull.Value)
                {
                    DateTime fechaVenc = Convert.ToDateTime(row["fecha_vencimiento"]);

                    if (fechaVenc <= fechaLimite && fechaVenc >= DateTime.Now)
                    {
                        proximosVencer.ImportRow(row);
                    }
                }
            }

            return proximosVencer;
        }

        /// <summary>
        /// MÉTODO PARA OBTENER PRODUCTOS CON STOCK BAJO (<=10)
        /// </summary>
        public static DataTable ObtenerProductosStockBajo()
        {
            DataTable todos = objDato.Listar();
            DataTable stockBajo = todos.Clone();

            foreach (DataRow row in todos.Rows)
            {
                int stock = Convert.ToInt32(row["stock"]);

                if (stock <= 10)
                {
                    stockBajo.ImportRow(row);
                }
            }

            return stockBajo;
        }
    }
}