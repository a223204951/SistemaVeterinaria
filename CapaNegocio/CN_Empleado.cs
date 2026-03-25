using System;
using System.Data;
using CapaDatos;

namespace CapaNegocio
{
    /// <summary>
    /// CAPA DE NEGOCIO — GESTIÓN DE EMPLEADOS
    /// Contiene validaciones y lógica de negocio.
    /// Patrón estático idéntico al resto del sistema (CN_Cliente, CN_Proveedor, etc.)
    /// </summary>
    public class CN_Empleado
    {
        private static CD_Empleado objDato = new CD_Empleado();

        // ── Listar todos ──────────────────────────────────────────────────────
        public static DataTable Listar()
            => objDato.Listar();

        // ── Listar solo activos (para ComboBox en otros módulos) ──────────────
        public static DataTable ListarActivos()
            => objDato.ListarActivos();

        // ── Guardar nuevo empleado ────────────────────────────────────────────
        public static string Guardar(string nombre, string apellidos, string telefono,
            string direccion, string correo, string estado, string tipoEmpleado,
            string cedulaProfesional, string especialidad)
        {
            // Validaciones obligatorias
            if (string.IsNullOrWhiteSpace(nombre))
                return "El nombre es obligatorio";

            if (string.IsNullOrWhiteSpace(apellidos))
                return "Los apellidos son obligatorios";

            if (nombre.Trim().Length < 2)
                return "El nombre debe tener al menos 2 caracteres";

            if (apellidos.Trim().Length < 2)
                return "Los apellidos deben tener al menos 2 caracteres";

            if (string.IsNullOrWhiteSpace(tipoEmpleado))
                return "El tipo de empleado es obligatorio";

            // Validar correo si se proporcionó
            if (!string.IsNullOrWhiteSpace(correo) && !correo.Contains("@"))
                return "El correo electrónico no es válido";

            // Validar que los veterinarios tengan cédula
            if (tipoEmpleado == "VETERINARIO" && string.IsNullOrWhiteSpace(cedulaProfesional))
                return "Los veterinarios deben tener cédula profesional";

            CD_Empleado obj = new CD_Empleado
            {
                Nombre = nombre.Trim(),
                Apellidos = apellidos.Trim(),
                Telefono = telefono?.Trim() ?? "",
                Direccion = direccion?.Trim() ?? "",
                Correo = correo?.Trim() ?? "",
                Estado = estado,
                TipoEmpleado = tipoEmpleado,
                CedulaProfesional = cedulaProfesional?.Trim() ?? "",
                Especialidad = especialidad?.Trim() ?? ""
            };

            return objDato.Guardar(obj);
        }

        // ── Editar empleado existente ─────────────────────────────────────────
        public static string Editar(int idempleado, string nombre, string apellidos,
            string telefono, string direccion, string correo, string estado,
            string tipoEmpleado, string cedulaProfesional, string especialidad)
        {
            if (idempleado <= 0)
                return "ID de empleado inválido";

            if (string.IsNullOrWhiteSpace(nombre))
                return "El nombre es obligatorio";

            if (string.IsNullOrWhiteSpace(apellidos))
                return "Los apellidos son obligatorios";

            if (nombre.Trim().Length < 2)
                return "El nombre debe tener al menos 2 caracteres";

            if (apellidos.Trim().Length < 2)
                return "Los apellidos deben tener al menos 2 caracteres";

            if (string.IsNullOrWhiteSpace(tipoEmpleado))
                return "El tipo de empleado es obligatorio";

            if (!string.IsNullOrWhiteSpace(correo) && !correo.Contains("@"))
                return "El correo electrónico no es válido";

            if (tipoEmpleado == "VETERINARIO" && string.IsNullOrWhiteSpace(cedulaProfesional))
                return "Los veterinarios deben tener cédula profesional";

            CD_Empleado obj = new CD_Empleado
            {
                Idempleado = idempleado,
                Nombre = nombre.Trim(),
                Apellidos = apellidos.Trim(),
                Telefono = telefono?.Trim() ?? "",
                Direccion = direccion?.Trim() ?? "",
                Correo = correo?.Trim() ?? "",
                Estado = estado,
                TipoEmpleado = tipoEmpleado,
                CedulaProfesional = cedulaProfesional?.Trim() ?? "",
                Especialidad = especialidad?.Trim() ?? ""
            };

            return objDato.Editar(obj);
        }

        // ── Eliminar (baja lógica) ────────────────────────────────────────────
        public static string Eliminar(int idempleado)
        {
            if (idempleado <= 0)
                return "ID de empleado inválido";

            return objDato.Eliminar(new CD_Empleado { Idempleado = idempleado });
        }

        // ── Búsqueda por nombre / apellidos ───────────────────────────────────
        public static DataTable BuscarNombre(string texto)
        {
            return objDato.BuscarNombre(new CD_Empleado { Buscar = texto });
        }

        // ── Búsqueda por ID ───────────────────────────────────────────────────
        public static DataTable BuscarId(string id)
        {
            return objDato.BuscarId(new CD_Empleado { Buscar = id });
        }
    }
}