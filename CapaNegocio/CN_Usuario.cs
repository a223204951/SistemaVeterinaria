using System;
using System.Data;
using CapaDatos;

namespace CapaNegocio
{
    /// <summary>
    /// CAPA DE NEGOCIO - GESTIÓN DE USUARIOS
    /// Contiene la lógica de negocio para autenticación y permisos
    /// </summary>
    public class CN_Usuario
    {
        // INSTANCIA DE LA CAPA DE DATOS
        private CD_Usuario objDato = new CD_Usuario();

        /// <summary>
        /// MÉTODO PARA VALIDAR USUARIO Y CONTRASEÑA
        /// </summary>
        /// <param name="user">Nombre de usuario</param>
        /// <param name="pass">Contraseña</param>
        /// <returns>True si las credenciales son válidas</returns>
        public bool ValidarUsuario(string user, string pass)
        {
            // VALIDAR QUE LOS CAMPOS NO ESTÉN VACÍOS
            if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(pass))
                return false;

            return objDato.Login(user, pass);
        }

        /// <summary>
        /// MÉTODO PARA OBTENER EL ROL DEL USUARIO
        /// </summary>
        /// <param name="user">Nombre de usuario</param>
        /// <returns>Rol del usuario</returns>
        public string ObtenerRol(string user)
        {
            if (string.IsNullOrEmpty(user))
                return "";

            return objDato.ObtenerRol(user);
        }

        /// <summary>
        /// MÉTODO PARA VERIFICAR SI UN USUARIO TIENE PERMISO PARA VER UN MÓDULO
        /// </summary>
        /// <param name="rol">Rol del usuario</param>
        /// <param name="modulo">Nombre del módulo</param>
        /// <returns>True si tiene permiso de ver</returns>
        public bool PuedeVer(string rol, string modulo)
        {
            DataRow permisos = objDato.ObtenerPermisos(rol, modulo);

            if (permisos != null)
            {
                return Convert.ToBoolean(permisos["puede_ver"]);
            }

            return false;
        }

        /// <summary>
        /// MÉTODO PARA VERIFICAR SI UN USUARIO TIENE PERMISO PARA CREAR EN UN MÓDULO
        /// </summary>
        public bool PuedeCrear(string rol, string modulo)
        {
            DataRow permisos = objDato.ObtenerPermisos(rol, modulo);

            if (permisos != null)
            {
                return Convert.ToBoolean(permisos["puede_crear"]);
            }

            return false;
        }

        /// <summary>
        /// MÉTODO PARA VERIFICAR SI UN USUARIO TIENE PERMISO PARA EDITAR EN UN MÓDULO
        /// </summary>
        public bool PuedeEditar(string rol, string modulo)
        {
            DataRow permisos = objDato.ObtenerPermisos(rol, modulo);

            if (permisos != null)
            {
                return Convert.ToBoolean(permisos["puede_editar"]);
            }

            return false;
        }

        /// <summary>
        /// MÉTODO PARA VERIFICAR SI UN USUARIO TIENE PERMISO PARA ELIMINAR EN UN MÓDULO
        /// </summary>
        public bool PuedeEliminar(string rol, string modulo)
        {
            DataRow permisos = objDato.ObtenerPermisos(rol, modulo);

            if (permisos != null)
            {
                return Convert.ToBoolean(permisos["puede_eliminar"]);
            }

            return false;
        }
    }
}