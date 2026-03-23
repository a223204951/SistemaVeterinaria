using System;
using System.Data;
using CapaDatos;

namespace CapaNegocio
{
    public class CN_Producto
    {
        private static CD_Producto objDato = new CD_Producto();

        // ── Listar ───────────────────────────────────────────────────────────
        public static DataTable Listar()
            => objDato.Listar();

        // ── Guardar (genera y asigna EAN-13 automáticamente) ─────────────────
        /// <summary>
        /// Guarda un nuevo producto y le asigna un código de barras EAN-13
        /// basado en el idproducto recién generado.
        /// </summary>
        public static string Guardar(string nombre, string descripcion, decimal precio,
            int stock, string estado, int idcategoria, bool esMedicamento,
            DateTime? fechaVencimiento, int? idproveedor = null)
        {
            if (string.IsNullOrWhiteSpace(nombre)) return "El nombre es obligatorio";
            if (precio <= 0) return "El precio debe ser mayor a $0";
            if (idcategoria <= 0) return "Seleccione una categoría";

            // Paso 1: Insertar sin código de barras para obtener el ID
            CD_Producto prod = new CD_Producto
            {
                Nombre = nombre.Trim(),
                Descripcion = descripcion?.Trim() ?? "",
                Precio = precio,
                Stock = stock,
                Estado = estado,
                Idcategoria = idcategoria,
                EsMedicamento = esMedicamento,
                FechaVencimiento = fechaVencimiento,
                CodigoBarras = null,  // se asignará en paso 2
                Idproveedor = idproveedor
            };

            string res = objDato.Guardar(prod);
            if (res != "OK") return res;

            // Paso 2: Obtener el idproducto recién insertado
            DataTable dt = objDato.BuscarNombre(new CD_Producto { Buscar = nombre.Trim() });
            if (dt == null || dt.Rows.Count == 0) return "OK"; // si falla el código no bloquea

            // Tomar el producto con mayor id (el recién creado)
            int idNuevo = 0;
            foreach (DataRow row in dt.Rows)
            {
                int id = Convert.ToInt32(row["idproducto"]);
                if (id > idNuevo) idNuevo = id;
            }

            if (idNuevo <= 0) return "OK";

            // Paso 3: Generar EAN-13 y guardarlo
            try
            {
                string codigoBarras = EAN13Util.Generar(idNuevo);
                objDato.GuardarCodigoBarras(idNuevo, codigoBarras);
            }
            catch { /* No bloquear si falla la generación del código */ }

            return "OK";
        }

        // ── Editar ───────────────────────────────────────────────────────────
        public static string Editar(int idproducto, string nombre, string descripcion,
            decimal precio, int stock, string estado, int idcategoria,
            bool esMedicamento, DateTime? fechaVencimiento, int? idproveedor = null)
        {
            if (string.IsNullOrWhiteSpace(nombre)) return "El nombre es obligatorio";
            if (precio <= 0) return "El precio debe ser mayor a $0";

            CD_Producto prod = new CD_Producto
            {
                Idproducto = idproducto,
                Nombre = nombre.Trim(),
                Descripcion = descripcion?.Trim() ?? "",
                Precio = precio,
                Stock = stock,
                Estado = estado,
                Idcategoria = idcategoria,
                EsMedicamento = esMedicamento,
                FechaVencimiento = fechaVencimiento,
                Idproveedor = idproveedor
            };
            return objDato.Editar(prod);
        }

        // ── Eliminar ─────────────────────────────────────────────────────────
        public static string Eliminar(int idproducto)
        {
            if (idproducto <= 0) return "Producto inválido";
            return objDato.Eliminar(new CD_Producto { Idproducto = idproducto });
        }

        // ── Asignar / regenerar código de barras ──────────────────────────────
        /// <summary>
        /// Genera y guarda un nuevo EAN-13 para el producto indicado.
        /// Útil para productos que ya existían antes de implementar esta funcionalidad.
        /// </summary>
        public static string RegenerarCodigoBarras(int idproducto)
        {
            if (idproducto <= 0) return "Producto inválido";
            try
            {
                string codigo = EAN13Util.Generar(idproducto);
                string res = objDato.GuardarCodigoBarras(idproducto, codigo);
                return res == "OK" ? codigo : res;
            }
            catch (Exception ex) { return ex.Message; }
        }

        // ── Buscar por código de barras ───────────────────────────────────────
        /// <summary>
        /// Busca un producto activo por su código de barras EAN-13.
        /// Devuelve DataTable con 1 fila si existe, 0 filas si no.
        /// </summary>
        public static DataTable BuscarPorCodigoBarras(string codigoBarras)
        {
            if (string.IsNullOrWhiteSpace(codigoBarras))
                return new DataTable();
            return objDato.BuscarPorCodigoBarras(codigoBarras);
        }

        // ── Búsquedas existentes ─────────────────────────────────────────────
        public static DataTable BuscarNombre(string buscar)
            => objDato.BuscarNombre(new CD_Producto { Buscar = buscar });

        public static DataTable BuscarCategoria(int idcategoria)
            => objDato.BuscarCategoria(idcategoria);

        // ── Precios dinámicos ────────────────────────────────────────────────
        public static string AjustarPreciosVenta(int idproductoVendido, int cantidadVendida)
            => objDato.AjustarPreciosVenta(idproductoVendido, cantidadVendida);

        public static string AjustarPreciosCompraMultiple(string idsProductos)
            => objDato.AjustarPreciosCompraMultiple(idsProductos);

        public static DataTable ObtenerHistorialPrecios(int idproducto)
            => objDato.ObtenerHistorialPrecios(idproducto);

        public static DataTable ObtenerProductosStockBajo()
            => objDato.ObtenerProductosStockBajo();

        public static DataTable ObtenerProductosProximosVencer()
            => objDato.ObtenerProductosProximosVencer();
    }
}