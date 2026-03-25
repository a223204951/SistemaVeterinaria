using System;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    /// <summary>
    /// CAPA DE DATOS — GESTIÓN DE EMPLEADOS
    /// Maneja las operaciones CRUD para empleados (veterinarios, cajeros, asistentes, etc.)
    /// Patrón idéntico al resto de la capa de datos del sistema.
    /// </summary>
    public class CD_Empleado
    {
        // =============================================
        // PROPIEDADES
        // =============================================
        public int Idempleado { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string Correo { get; set; }
        public string Estado { get; set; }
        public string TipoEmpleado { get; set; }
        public string CedulaProfesional { get; set; }
        public string Especialidad { get; set; }
        public string Buscar { get; set; }

        // ── Listar todos ──────────────────────────────────────────────────────
        public DataTable Listar()
        {
            DataTable dt = new DataTable("Empleado");
            using (SqlConnection con = new SqlConnection(CD_Conexion.Conn))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("dbo.sp_list_empleado_completo", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    new SqlDataAdapter(cmd).Fill(dt);
                }
                catch (Exception ex) { throw ex; }
            }
            return dt;
        }

        // ── Guardar nuevo empleado ────────────────────────────────────────────
        public string Guardar(CD_Empleado emp)
        {
            using (SqlConnection con = new SqlConnection(CD_Conexion.Conn))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("dbo.sp_insert_empleado_v2", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@nombre", emp.Nombre);
                    cmd.Parameters.AddWithValue("@apellidos", emp.Apellidos);
                    cmd.Parameters.AddWithValue("@telefono", emp.Telefono ?? "");
                    cmd.Parameters.AddWithValue("@direccion", emp.Direccion ?? "");
                    cmd.Parameters.AddWithValue("@correo", emp.Correo ?? "");
                    cmd.Parameters.AddWithValue("@estado", emp.Estado);
                    cmd.Parameters.AddWithValue("@tipo_empleado", emp.TipoEmpleado);
                    cmd.Parameters.AddWithValue("@cedula_profesional",
                        string.IsNullOrEmpty(emp.CedulaProfesional)
                            ? (object)DBNull.Value : emp.CedulaProfesional);
                    cmd.Parameters.AddWithValue("@especialidad",
                        string.IsNullOrEmpty(emp.Especialidad)
                            ? (object)DBNull.Value : emp.Especialidad);

                    object res = cmd.ExecuteScalar();
                    return res != null ? res.ToString() : "Error al guardar empleado";
                }
                catch (Exception ex) { return ex.Message; }
            }
        }

        // ── Editar empleado existente ─────────────────────────────────────────
        public string Editar(CD_Empleado emp)
        {
            using (SqlConnection con = new SqlConnection(CD_Conexion.Conn))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("dbo.sp_update_empleado_v2", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idempleado", emp.Idempleado);
                    cmd.Parameters.AddWithValue("@nombre", emp.Nombre);
                    cmd.Parameters.AddWithValue("@apellidos", emp.Apellidos);
                    cmd.Parameters.AddWithValue("@telefono", emp.Telefono ?? "");
                    cmd.Parameters.AddWithValue("@direccion", emp.Direccion ?? "");
                    cmd.Parameters.AddWithValue("@correo", emp.Correo ?? "");
                    cmd.Parameters.AddWithValue("@estado", emp.Estado);
                    cmd.Parameters.AddWithValue("@tipo_empleado", emp.TipoEmpleado);
                    cmd.Parameters.AddWithValue("@cedula_profesional",
                        string.IsNullOrEmpty(emp.CedulaProfesional)
                            ? (object)DBNull.Value : emp.CedulaProfesional);
                    cmd.Parameters.AddWithValue("@especialidad",
                        string.IsNullOrEmpty(emp.Especialidad)
                            ? (object)DBNull.Value : emp.Especialidad);

                    object res = cmd.ExecuteScalar();
                    return res != null ? res.ToString() : "Error al actualizar empleado";
                }
                catch (Exception ex) { return ex.Message; }
            }
        }

        // ── Eliminar lógico (marca INACTIVO) ──────────────────────────────────
        // No se elimina físicamente: el empleado puede tener usuario, citas y ventas ligadas.
        public string Eliminar(CD_Empleado emp)
        {
            using (SqlConnection con = new SqlConnection(CD_Conexion.Conn))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(
                        "UPDATE empleado SET estado = 'INACTIVO' WHERE idempleado = @id", con);
                    cmd.Parameters.AddWithValue("@id", emp.Idempleado);
                    cmd.ExecuteNonQuery();
                    return "OK";
                }
                catch (Exception ex) { return ex.Message; }
            }
        }

        // ── Buscar por nombre o apellidos ─────────────────────────────────────
        public DataTable BuscarNombre(CD_Empleado emp)
        {
            DataTable dt = new DataTable("Empleado");
            using (SqlConnection con = new SqlConnection(CD_Conexion.Conn))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("dbo.sp_buscar_empleado_nombre", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@buscar", emp.Buscar ?? "");
                    new SqlDataAdapter(cmd).Fill(dt);
                }
                catch (Exception ex) { throw ex; }
            }
            return dt;
        }

        // ── Buscar por ID ─────────────────────────────────────────────────────
        public DataTable BuscarId(CD_Empleado emp)
        {
            DataTable dt = new DataTable("Empleado");
            using (SqlConnection con = new SqlConnection(CD_Conexion.Conn))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("dbo.sp_buscar_empleado_id", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@buscar", emp.Buscar ?? "");
                    new SqlDataAdapter(cmd).Fill(dt);
                }
                catch (Exception ex) { throw ex; }
            }
            return dt;
        }

        // ── Listar activos (para ComboBox en módulo de Usuarios, etc.) ────────
        public DataTable ListarActivos()
        {
            DataTable dt = new DataTable("EmpleadoActivo");
            using (SqlConnection con = new SqlConnection(CD_Conexion.Conn))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(
                        "SELECT idempleado, " +
                        "       nombre + ' ' + apellidos AS nombre_completo, " +
                        "       tipo_empleado " +
                        "FROM empleado " +
                        "WHERE estado = 'ACTIVO' " +
                        "ORDER BY nombre", con);
                    cmd.CommandType = CommandType.Text;
                    new SqlDataAdapter(cmd).Fill(dt);
                }
                catch { /* silencioso: uso interno */ }
            }
            return dt;
        }
    }
}