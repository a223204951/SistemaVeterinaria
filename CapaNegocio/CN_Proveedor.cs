using System;
using System.Data;
using CapaDatos;

namespace CapaNegocio
{
    /// <summary>
    /// CAPA DE NEGOCIO — PROVEEDORES / DISTRIBUIDORES
    /// Métodos estáticos, igual que CN_Producto, CN_Cliente, etc.
    /// </summary>
    public class CN_Proveedor
    {
        private static CD_Proveedor objDato = new CD_Proveedor();

        // ── Listar ────────────────────────────────────────────────────────────
        public static DataTable Listar()
            => objDato.Listar();

        public static DataTable ListarActivos()
            => objDato.ListarActivos();

        // ── Guardar ───────────────────────────────────────────────────────────
        public static string Guardar(string nombre, string telefono,
            string direccion, string correo, string estado)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                return "El nombre del proveedor es obligatorio";

            if (!string.IsNullOrWhiteSpace(correo) && !correo.Contains("@"))
                return "El correo electrónico no es válido";

            CD_Proveedor prov = new CD_Proveedor
            {
                Nombre = nombre.Trim(),
                Telefono = telefono?.Trim() ?? "",
                Direccion = direccion?.Trim() ?? "",
                Correo = correo?.Trim() ?? "",
                Estado = estado
            };
            return objDato.Guardar(prov);
        }

        // ── Editar ────────────────────────────────────────────────────────────
        public static string Editar(int idproveedor, string nombre, string telefono,
            string direccion, string correo, string estado)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                return "El nombre del proveedor es obligatorio";
            if (idproveedor <= 0)
                return "Proveedor inválido";

            if (!string.IsNullOrWhiteSpace(correo) && !correo.Contains("@"))
                return "El correo electrónico no es válido";

            CD_Proveedor prov = new CD_Proveedor
            {
                Idproveedor = idproveedor,
                Nombre = nombre.Trim(),
                Telefono = telefono?.Trim() ?? "",
                Direccion = direccion?.Trim() ?? "",
                Correo = correo?.Trim() ?? "",
                Estado = estado
            };
            return objDato.Editar(prov);
        }

        // ── Eliminar (lógico) ─────────────────────────────────────────────────
        public static string Eliminar(int idproveedor)
        {
            if (idproveedor <= 0) return "Proveedor inválido";
            return objDato.Eliminar(new CD_Proveedor { Idproveedor = idproveedor });
        }

        // ── Búsqueda ──────────────────────────────────────────────────────────
        public static DataTable BuscarNombre(string texto)
            => objDato.BuscarNombre(new CD_Proveedor { Buscar = texto });

        // ── Historial de compras ──────────────────────────────────────────────
        public static DataTable HistorialCompras(int idproveedor)
            => objDato.HistorialCompras(idproveedor);
    }
}