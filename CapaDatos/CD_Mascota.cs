using System;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CD_Mascota
    {
        public int Idmascota { get; set; }
        public string Nombre { get; set; }
        public string Especie { get; set; }
        public string Raza { get; set; }
        public string Sexo { get; set; }
        public int Edad { get; set; }
        public decimal Peso { get; set; }
        public string Color { get; set; }
        public string Estado { get; set; }
        public int Idcliente { get; set; }
        public string Buscar { get; set; }

        // ── Listar ────────────────────────────────────────────────────────────

        public DataTable Listar()
        {
            DataTable resultado = new DataTable("Mascota");
            using (SqlConnection con = new SqlConnection(CD_Conexion.Conn))
            {
                SqlCommand cmd = new SqlCommand("dbo.sp_list_mascota", con);
                cmd.CommandType = CommandType.StoredProcedure;
                new SqlDataAdapter(cmd).Fill(resultado);
            }
            return resultado;
        }

        // ── Guardar ───────────────────────────────────────────────────────────
        // sp_insert_mascota usa SET NOCOUNT ON → ExecuteNonQuery devuelve -1 SIEMPRE.
        // Solución: usar ExecuteScalar con OUTPUT o intentar con @@ROWCOUNT.
        // La forma más simple y compatible: try/catch; si no lanza excepción = OK.

        public string Guardar(CD_Mascota mas)
        {
            using (SqlConnection con = new SqlConnection(CD_Conexion.Conn))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("dbo.sp_insert_mascota", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@nombre", mas.Nombre);
                    cmd.Parameters.AddWithValue("@especie", mas.Especie);
                    cmd.Parameters.AddWithValue("@raza", mas.Raza);
                    cmd.Parameters.AddWithValue("@sexo", mas.Sexo);
                    cmd.Parameters.AddWithValue("@edad", mas.Edad);
                    cmd.Parameters.AddWithValue("@peso", mas.Peso);
                    cmd.Parameters.AddWithValue("@color", mas.Color);
                    cmd.Parameters.AddWithValue("@estado", mas.Estado);
                    cmd.Parameters.AddWithValue("@idcliente", mas.Idcliente);
                    cmd.ExecuteNonQuery();  // NOCOUNT ON → no verificar filas afectadas
                    return "OK";
                }
                catch (Exception ex) { return ex.Message; }
            }
        }

        // ── Editar ────────────────────────────────────────────────────────────
        // sp_update_mascota también tiene SET NOCOUNT ON → mismo patrón.

        public string Editar(CD_Mascota mas)
        {
            using (SqlConnection con = new SqlConnection(CD_Conexion.Conn))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("dbo.sp_update_mascota", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idmascota", mas.Idmascota);
                    cmd.Parameters.AddWithValue("@nombre", mas.Nombre);
                    cmd.Parameters.AddWithValue("@especie", mas.Especie);
                    cmd.Parameters.AddWithValue("@raza", mas.Raza);
                    cmd.Parameters.AddWithValue("@sexo", mas.Sexo);
                    cmd.Parameters.AddWithValue("@edad", mas.Edad);
                    cmd.Parameters.AddWithValue("@peso", mas.Peso);
                    cmd.Parameters.AddWithValue("@color", mas.Color);
                    cmd.Parameters.AddWithValue("@estado", mas.Estado);
                    cmd.Parameters.AddWithValue("@idcliente", mas.Idcliente);
                    cmd.ExecuteNonQuery();
                    return "OK";
                }
                catch (Exception ex) { return ex.Message; }
            }
        }

        // ── Eliminar REAL ─────────────────────────────────────────────────────
        // *** CORRECCIÓN: DELETE directo en lugar de sp_delete_mascota que solo
        //     marca INACTIVO. También elimina citas y consultas relacionadas. ***

        public string Eliminar(CD_Mascota mas)
        {
            using (SqlConnection con = new SqlConnection(CD_Conexion.Conn))
            {
                try
                {
                    con.Open();
                    SqlTransaction tx = con.BeginTransaction();

                    try
                    {
                        // 1. Eliminar consultas ligadas a citas de esta mascota
                        SqlCommand cmd1 = new SqlCommand(
                            @"DELETE FROM consulta WHERE idcita IN
                              (SELECT idcita FROM cita WHERE idmascota = @id)", con, tx);
                        cmd1.Parameters.AddWithValue("@id", mas.Idmascota);
                        cmd1.ExecuteNonQuery();

                        // 2. Eliminar pagos ligados a citas de esta mascota
                        SqlCommand cmd2 = new SqlCommand(
                            @"DELETE FROM pago WHERE idcita IN
                              (SELECT idcita FROM cita WHERE idmascota = @id)", con, tx);
                        cmd2.Parameters.AddWithValue("@id", mas.Idmascota);
                        cmd2.ExecuteNonQuery();

                        // 3. Eliminar citas
                        SqlCommand cmd3 = new SqlCommand(
                            "DELETE FROM cita WHERE idmascota = @id", con, tx);
                        cmd3.Parameters.AddWithValue("@id", mas.Idmascota);
                        cmd3.ExecuteNonQuery();

                        // 4. Eliminar la mascota
                        SqlCommand cmd4 = new SqlCommand(
                            "DELETE FROM mascota WHERE idmascota = @id", con, tx);
                        cmd4.Parameters.AddWithValue("@id", mas.Idmascota);
                        cmd4.ExecuteNonQuery();

                        tx.Commit();
                        return "OK";
                    }
                    catch
                    {
                        tx.Rollback();
                        throw;
                    }
                }
                catch (Exception ex) { return ex.Message; }
            }
        }

        // ── Búsquedas ─────────────────────────────────────────────────────────

        public DataTable BuscarNombre(CD_Mascota mas)
        {
            DataTable dt = new DataTable("Mascota");
            using (SqlConnection con = new SqlConnection(CD_Conexion.Conn))
            {
                SqlCommand cmd = new SqlCommand("dbo.sp_buscar_mascota_nombre", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@buscar", mas.Buscar);
                new SqlDataAdapter(cmd).Fill(dt);
            }
            return dt;
        }

        public DataTable BuscarPorCliente(int idcliente)
        {
            DataTable dt = new DataTable("Mascota");
            using (SqlConnection con = new SqlConnection(CD_Conexion.Conn))
            {
                SqlCommand cmd = new SqlCommand("dbo.sp_buscar_mascota_cliente", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idcliente", idcliente);
                new SqlDataAdapter(cmd).Fill(dt);
            }
            return dt;
        }

        public DataTable BuscarPorNombreCliente(CD_Mascota mas)
        {
            DataTable dt = new DataTable("Mascota");
            using (SqlConnection con = new SqlConnection(CD_Conexion.Conn))
            {
                SqlCommand cmd = new SqlCommand("dbo.sp_buscar_mascota_nombre_cliente", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@buscar", mas.Buscar);
                new SqlDataAdapter(cmd).Fill(dt);
            }
            return dt;
        }

        public DataTable ObtenerClientes()
        {
            DataTable dt = new DataTable("Clientes");
            using (SqlConnection con = new SqlConnection(CD_Conexion.Conn))
            {
                SqlCommand cmd = new SqlCommand(
                    "SELECT idcliente, nombre FROM cliente WHERE estado='ACTIVO' ORDER BY nombre", con);
                cmd.CommandType = CommandType.Text;
                new SqlDataAdapter(cmd).Fill(dt);
            }
            return dt;
        }
    }
}