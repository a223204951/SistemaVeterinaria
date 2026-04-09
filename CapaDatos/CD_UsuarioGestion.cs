using System;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    /// <summary>
    /// CAPA DE DATOS — GESTIÓN COMPLETA DE USUARIOS
    /// Maneja CRUD de usuarios vinculados a empleados
    /// </summary>
    public class CD_UsuarioGestion
    {
        public int Idusuario { get; set; }
        public string Usuario { get; set; }
        public string Pass { get; set; }
        public string Acceso { get; set; }
        public string Estado { get; set; }
        public int Idempleado { get; set; }

        // ── Listar todos los usuarios ─────────────────────────────────────────
        public DataTable Listar()
        {
            DataTable dt = new DataTable("Usuario");
            using (var con = new SqlConnection(CD_Conexion.Conn))
            {
                try
                {
                    var cmd = new SqlCommand("dbo.sp_list_usuario", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    new SqlDataAdapter(cmd).Fill(dt);
                }
                catch (Exception ex) { throw ex; }
            }
            return dt;
        }

        // ── Guardar nuevo usuario ─────────────────────────────────────────────
        public string Guardar(CD_UsuarioGestion u)
        {
            using (var con = new SqlConnection(CD_Conexion.Conn))
            {
                try
                {
                    con.Open();
                    var cmd = new SqlCommand("dbo.sp_insert_usuario_v2", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@usuario", u.Usuario);
                    cmd.Parameters.AddWithValue("@pass", u.Pass);
                    cmd.Parameters.AddWithValue("@acceso", u.Acceso);
                    cmd.Parameters.AddWithValue("@estado", u.Estado);
                    cmd.Parameters.AddWithValue("@idempleado", u.Idempleado);
                    object res = cmd.ExecuteScalar();
                    return res?.ToString() ?? "Error al guardar usuario";
                }
                catch (Exception ex) { return ex.Message; }
            }
        }

        // ── Editar usuario existente ──────────────────────────────────────────
        public string Editar(CD_UsuarioGestion u)
        {
            using (var con = new SqlConnection(CD_Conexion.Conn))
            {
                try
                {
                    con.Open();
                    var cmd = new SqlCommand("dbo.sp_update_usuario", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idusuario", u.Idusuario);
                    cmd.Parameters.AddWithValue("@usuario", u.Usuario);
                    cmd.Parameters.AddWithValue("@pass", u.Pass);
                    cmd.Parameters.AddWithValue("@acceso", u.Acceso);
                    cmd.Parameters.AddWithValue("@estado", u.Estado);
                    cmd.Parameters.AddWithValue("@idempleado", u.Idempleado);
                    object res = cmd.ExecuteScalar();
                    return res?.ToString() ?? "Error al actualizar usuario";
                }
                catch (Exception ex) { return ex.Message; }
            }
        }

        // ── Eliminar (baja lógica) ────────────────────────────────────────────
        public string Eliminar(int idusuario)
        {
            using (var con = new SqlConnection(CD_Conexion.Conn))
            {
                try
                {
                    con.Open();
                    var cmd = new SqlCommand("dbo.sp_delete_usuario_v2", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idusuario", idusuario);
                    object res = cmd.ExecuteScalar();
                    return res?.ToString() ?? "Error al eliminar usuario";
                }
                catch (Exception ex) { return ex.Message; }
            }
        }

        // ── Resetear contraseña ───────────────────────────────────────────────
        public string ResetPassword(int idusuario, string nuevaPass)
        {
            using (var con = new SqlConnection(CD_Conexion.Conn))
            {
                try
                {
                    con.Open();
                    var cmd = new SqlCommand("dbo.sp_reset_password", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idusuario", idusuario);
                    cmd.Parameters.AddWithValue("@nueva_pass", nuevaPass);
                    object res = cmd.ExecuteScalar();
                    return res?.ToString() ?? "Error al resetear contraseña";
                }
                catch (Exception ex) { return ex.Message; }
            }
        }

        // ── Buscar por nombre de usuario o empleado ───────────────────────────
        public DataTable Buscar(string texto)
        {
            DataTable dt = new DataTable("Usuario");
            using (var con = new SqlConnection(CD_Conexion.Conn))
            {
                try
                {
                    var cmd = new SqlCommand("dbo.sp_buscar_usuario_nombre", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@buscar", texto ?? "");
                    new SqlDataAdapter(cmd).Fill(dt);
                }
                catch (Exception ex) { throw ex; }
            }
            return dt;
        }

        // ── Listar empleados activos sin usuario activo asignado (para ComboBox) ──
        public DataTable ListarEmpleadosSinUsuario(int idUsuarioExcluir = 0)
        {
            DataTable dt = new DataTable("Empleados");
            using (var con = new SqlConnection(CD_Conexion.Conn))
            {
                try
                {
                    string sql = @"
                        SELECT e.idempleado,
                               e.nombre + ' ' + e.apellidos AS nombre_completo,
                               e.tipo_empleado
                        FROM empleado e
                        WHERE e.estado = 'ACTIVO'
                          AND (
                              NOT EXISTS (
                                  SELECT 1 FROM usuario u
                                  WHERE u.idempleado = e.idempleado
                                    AND u.estado = 'ACTIVO'
                                    AND (@excluir = 0 OR u.idusuario <> @excluir)
                              )
                          )
                        ORDER BY e.nombre";
                    var cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@excluir", idUsuarioExcluir);
                    new SqlDataAdapter(cmd).Fill(dt);
                }
                catch { /* devuelve vacío si falla */ }
            }
            return dt;
        }

        // ── Listar todos los empleados activos (para edición) ─────────────────
        public DataTable ListarTodosEmpleadosActivos()
        {
            DataTable dt = new DataTable("Empleados");
            using (var con = new SqlConnection(CD_Conexion.Conn))
            {
                try
                {
                    var cmd = new SqlCommand(
                        @"SELECT idempleado,
                                 nombre + ' ' + apellidos AS nombre_completo,
                                 tipo_empleado
                          FROM empleado
                          WHERE estado = 'ACTIVO'
                          ORDER BY nombre", con);
                    new SqlDataAdapter(cmd).Fill(dt);
                }
                catch { }
            }
            return dt;
        }
    }
}