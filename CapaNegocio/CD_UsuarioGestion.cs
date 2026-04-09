using System;
using System.Data;
using CapaDatos;

namespace CapaNegocio
{
    /// <summary>
    /// CAPA DE NEGOCIO — GESTIÓN COMPLETA DE USUARIOS
    /// Validaciones y lógica de negocio para CRUD de usuarios
    /// </summary>
    public class CN_UsuarioGestion
    {
        private static CD_UsuarioGestion objDato = new CD_UsuarioGestion();

        // Niveles de acceso permitidos
        private static readonly string[] NivelesAcceso =
            { "ADMINISTRADOR", "VETERINARIO", "CAJERO", "ASISTENTE" };

        // ── Listar ────────────────────────────────────────────────────────────
        public static DataTable Listar()
            => objDato.Listar();

        // ── Guardar nuevo usuario ─────────────────────────────────────────────
        public static string Guardar(string usuario, string pass, string passConfirm,
            string acceso, string estado, int idempleado)
        {
            // Validaciones
            if (string.IsNullOrWhiteSpace(usuario))
                return "El nombre de usuario es obligatorio";
            if (usuario.Trim().Length < 3)
                return "El usuario debe tener al menos 3 caracteres";
            if (usuario.Trim().Length > 20)
                return "El usuario no puede exceder 20 caracteres";
            if (usuario.Contains(" "))
                return "El usuario no puede contener espacios";
            if (string.IsNullOrWhiteSpace(pass))
                return "La contraseña es obligatoria";
            if (pass.Length < 4)
                return "La contraseña debe tener al menos 4 caracteres";
            if (pass.Length > 20)
                return "La contraseña no puede exceder 20 caracteres";
            if (pass != passConfirm)
                return "Las contraseñas no coinciden";
            if (string.IsNullOrWhiteSpace(acceso))
                return "Seleccione un nivel de acceso";
            if (idempleado <= 0)
                return "Debe vincular el usuario a un empleado";

            var obj = new CD_UsuarioGestion
            {
                Usuario = usuario.Trim(),
                Pass = pass,
                Acceso = acceso,
                Estado = estado,
                Idempleado = idempleado
            };
            return objDato.Guardar(obj);
        }

        // ── Editar usuario existente ──────────────────────────────────────────
        public static string Editar(int idusuario, string usuario, string pass,
            string passConfirm, string acceso, string estado, int idempleado,
            bool cambiarPassword)
        {
            if (idusuario <= 0) return "ID de usuario inválido";
            if (string.IsNullOrWhiteSpace(usuario))
                return "El nombre de usuario es obligatorio";
            if (usuario.Trim().Length < 3)
                return "El usuario debe tener al menos 3 caracteres";
            if (usuario.Trim().Length > 20)
                return "El usuario no puede exceder 20 caracteres";
            if (usuario.Contains(" "))
                return "El usuario no puede contener espacios";
            if (cambiarPassword)
            {
                if (string.IsNullOrWhiteSpace(pass))
                    return "La contraseña es obligatoria";
                if (pass.Length < 4)
                    return "La contraseña debe tener al menos 4 caracteres";
                if (pass.Length > 20)
                    return "La contraseña no puede exceder 20 caracteres";
                if (pass != passConfirm)
                    return "Las contraseñas no coinciden";
            }
            if (string.IsNullOrWhiteSpace(acceso))
                return "Seleccione un nivel de acceso";
            if (idempleado <= 0)
                return "Debe vincular el usuario a un empleado";

            var obj = new CD_UsuarioGestion
            {
                Idusuario = idusuario,
                Usuario = usuario.Trim(),
                Pass = cambiarPassword ? pass : null,
                Acceso = acceso,
                Estado = estado,
                Idempleado = idempleado
            };

            // Si no cambia password, obtener la actual primero
            if (!cambiarPassword)
            {
                string passActual = ObtenerPasswordActual(idusuario);
                obj.Pass = passActual;
            }

            return objDato.Editar(obj);
        }

        // ── Obtener password actual (para no sobreescribir) ───────────────────
        private static string ObtenerPasswordActual(int idusuario)
        {
            try
            {
                using (var con = new System.Data.SqlClient.SqlConnection(
                    CapaDatos.CD_Conexion.Conn))
                {
                    con.Open();
                    var cmd = new System.Data.SqlClient.SqlCommand(
                        "SELECT pass FROM usuario WHERE idusuario = @id", con);
                    cmd.Parameters.AddWithValue("@id", idusuario);
                    object res = cmd.ExecuteScalar();
                    return res?.ToString() ?? "";
                }
            }
            catch { return ""; }
        }

        // ── Eliminar (baja lógica) ────────────────────────────────────────────
        public static string Eliminar(int idusuario)
        {
            if (idusuario <= 0) return "ID de usuario inválido";
            return objDato.Eliminar(idusuario);
        }

        // ── Resetear contraseña ───────────────────────────────────────────────
        public static string ResetPassword(int idusuario, string nuevaPass, string confirmar)
        {
            if (idusuario <= 0) return "ID de usuario inválido";
            if (string.IsNullOrWhiteSpace(nuevaPass)) return "La contraseña es obligatoria";
            if (nuevaPass.Length < 4) return "Mínimo 4 caracteres";
            if (nuevaPass.Length > 20) return "Máximo 20 caracteres";
            if (nuevaPass != confirmar) return "Las contraseñas no coinciden";
            return objDato.ResetPassword(idusuario, nuevaPass);
        }

        // ── Búsqueda ──────────────────────────────────────────────────────────
        public static DataTable BuscarNombre(string texto)
            => objDato.Buscar(texto);

        // ── Empleados para ComboBox ───────────────────────────────────────────
        public static DataTable ListarEmpleadosSinUsuario(int idUsuarioExcluir = 0)
            => objDato.ListarEmpleadosSinUsuario(idUsuarioExcluir);

        public static DataTable ListarTodosEmpleadosActivos()
            => objDato.ListarTodosEmpleadosActivos();

        // ── Niveles de acceso disponibles ─────────────────────────────────────
        public static string[] GetNivelesAcceso() => NivelesAcceso;
    }
}