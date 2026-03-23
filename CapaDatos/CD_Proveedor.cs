using System;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    /// <summary>
    /// CAPA DE DATOS — PROVEEDORES / DISTRIBUIDORES
    /// Adaptado al patrón del Sistema Veterinaria (CD_Conexion, using, try/catch)
    /// La tabla proveedor tiene: idproveedor, nombre, telefono, direccion, correo, estado
    /// </summary>
    public class CD_Proveedor
    {
        public int Idproveedor { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string Correo { get; set; }
        public string Estado { get; set; }
        public string Buscar { get; set; }

        // ── Listar todos ──────────────────────────────────────────────────────
        public DataTable Listar()
        {
            DataTable dt = new DataTable("proveedor");
            using (SqlConnection con = new SqlConnection(CD_Conexion.Conn))
            {
                SqlCommand cmd = new SqlCommand("dbo.sp_list_proveedor", con);
                cmd.CommandType = CommandType.StoredProcedure;
                new SqlDataAdapter(cmd).Fill(dt);
            }
            return dt;
        }

        // ── Guardar ───────────────────────────────────────────────────────────
        public string Guardar(CD_Proveedor prov)
        {
            using (SqlConnection con = new SqlConnection(CD_Conexion.Conn))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("dbo.sp_insert_proveedor", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@nombre", prov.Nombre);
                    cmd.Parameters.AddWithValue("@telefono", prov.Telefono ?? "");
                    cmd.Parameters.AddWithValue("@direccion", prov.Direccion ?? "");
                    cmd.Parameters.AddWithValue("@correo", prov.Correo ?? "");
                    cmd.Parameters.AddWithValue("@estado", prov.Estado);
                    object res = cmd.ExecuteScalar();
                    return res != null ? res.ToString() : "Error al guardar";
                }
                catch (Exception ex) { return ex.Message; }
            }
        }

        // ── Editar ────────────────────────────────────────────────────────────
        public string Editar(CD_Proveedor prov)
        {
            using (SqlConnection con = new SqlConnection(CD_Conexion.Conn))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("dbo.sp_update_proveedor", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idproveedor", prov.Idproveedor);
                    cmd.Parameters.AddWithValue("@nombre", prov.Nombre);
                    cmd.Parameters.AddWithValue("@telefono", prov.Telefono ?? "");
                    cmd.Parameters.AddWithValue("@direccion", prov.Direccion ?? "");
                    cmd.Parameters.AddWithValue("@correo", prov.Correo ?? "");
                    cmd.Parameters.AddWithValue("@estado", prov.Estado);
                    object res = cmd.ExecuteScalar();
                    return res != null ? res.ToString() : "Error al editar";
                }
                catch (Exception ex) { return ex.Message; }
            }
        }

        // ── Eliminar (lógico — marca INACTIVO) ────────────────────────────────
        public string Eliminar(CD_Proveedor prov)
        {
            using (SqlConnection con = new SqlConnection(CD_Conexion.Conn))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("dbo.sp_delete_proveedor", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idproveedor", prov.Idproveedor);
                    object res = cmd.ExecuteScalar();
                    return res != null ? res.ToString() : "Error al desactivar";
                }
                catch (Exception ex) { return ex.Message; }
            }
        }

        // ── Buscar por nombre ─────────────────────────────────────────────────
        public DataTable BuscarNombre(CD_Proveedor prov)
        {
            DataTable dt = new DataTable("proveedor");
            using (SqlConnection con = new SqlConnection(CD_Conexion.Conn))
            {
                SqlCommand cmd = new SqlCommand("dbo.sp_buscar_proveedor_nombre", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@buscar", prov.Buscar);
                new SqlDataAdapter(cmd).Fill(dt);
            }
            return dt;
        }

        // ── Historial de compras del proveedor ────────────────────────────────
        public DataTable HistorialCompras(int idproveedor)
        {
            DataTable dt = new DataTable("historial");
            using (SqlConnection con = new SqlConnection(CD_Conexion.Conn))
            {
                SqlCommand cmd = new SqlCommand("dbo.sp_historial_compras_proveedor", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idproveedor", idproveedor);
                new SqlDataAdapter(cmd).Fill(dt);
            }
            return dt;
        }

        // ── Listar activos (para ComboBox en FrmCompras) ──────────────────────
        public DataTable ListarActivos()
        {
            DataTable dt = new DataTable("proveedor");
            using (SqlConnection con = new SqlConnection(CD_Conexion.Conn))
            {
                SqlCommand cmd = new SqlCommand("dbo.sp_list_proveedores_activos", con);
                cmd.CommandType = CommandType.StoredProcedure;
                new SqlDataAdapter(cmd).Fill(dt);
            }
            return dt;
        }
    }
}