using System;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    /// <summary>
    /// CAPA DE DATOS - GESTIÓN DE USUARIOS
    /// Maneja las operaciones de autenticación y permisos de usuarios
    /// </summary>
    public class CD_Usuario
    {
        /// <summary>
        /// MÉTODO PARA VALIDAR CREDENCIALES DE USUARIO
        /// Verifica que el usuario y contraseña sean correctos y que el usuario esté activo
        /// </summary>
        /// <param name="user">Nombre de usuario</param>
        /// <param name="pass">Contraseña</param>
        /// <returns>True si las credenciales son válidas, False en caso contrario</returns>
        public bool Login(string user, string pass)
        {
            using (SqlConnection conexion = new SqlConnection(CD_Conexion.Conn))
            {
                conexion.Open();

                // CONSULTA PARA VERIFICAR CREDENCIALES Y ESTADO ACTIVO
                string query = "SELECT Count(*) FROM usuario WHERE usuario = @usuario AND pass = @pass AND estado = 'ACTIVO'";

                SqlCommand cmd = new SqlCommand(query, conexion);
                cmd.Parameters.AddWithValue("@usuario", user);
                cmd.Parameters.AddWithValue("@pass", pass);

                int result = (int)cmd.ExecuteScalar();

                return result > 0;
            }
        }

        /// <summary>
        /// MÉTODO PARA OBTENER EL ROL/ACCESO DEL USUARIO
        /// Retorna el nivel de acceso del usuario (ADMINISTRADOR, VETERINARIO, CAJERO, ASISTENTE)
        /// </summary>
        /// <param name="user">Nombre de usuario</param>
        /// <returns>Rol del usuario como string</returns>
        public string ObtenerRol(string user)
        {
            string rol = "";

            using (SqlConnection conexion = new SqlConnection(CD_Conexion.Conn))
            {
                try
                {
                    conexion.Open();

                    // CONSULTA PARA OBTENER EL ROL DEL USUARIO
                    string query = @"SELECT u.acceso 
                                   FROM usuario u 
                                   WHERE u.usuario = @usuario AND u.estado = 'ACTIVO'";

                    SqlCommand cmd = new SqlCommand(query, conexion);
                    cmd.Parameters.AddWithValue("@usuario", user);

                    object resultado = cmd.ExecuteScalar();

                    if (resultado != null)
                    {
                        rol = resultado.ToString();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al obtener rol del usuario: " + ex.Message);
                }
            }

            return rol;
        }

        /// <summary>
        /// MÉTODO PARA OBTENER LOS PERMISOS DE UN ROL PARA UN MÓDULO ESPECÍFICO
        /// </summary>
        /// <param name="rol">Rol del usuario</param>
        /// <param name="modulo">Nombre del módulo</param>
        /// <returns>DataRow con los permisos (puede_ver, puede_crear, puede_editar, puede_eliminar)</returns>
        public DataRow ObtenerPermisos(string rol, string modulo)
        {
            DataTable dt = new DataTable();

            using (SqlConnection conexion = new SqlConnection(CD_Conexion.Conn))
            {
                try
                {
                    conexion.Open();

                    // CONSULTA PARA OBTENER PERMISOS ESPECÍFICOS
                    string query = @"SELECT puede_ver, puede_crear, puede_editar, puede_eliminar 
                                   FROM permisos_rol 
                                   WHERE rol = @rol AND modulo = @modulo";

                    SqlCommand cmd = new SqlCommand(query, conexion);
                    cmd.Parameters.AddWithValue("@rol", rol);
                    cmd.Parameters.AddWithValue("@modulo", modulo);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        return dt.Rows[0];
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al obtener permisos: " + ex.Message);
                }
            }

            return null;
        }
    }
}