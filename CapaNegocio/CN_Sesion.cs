using System;
using System.Data;
using CapaDatos;

namespace CapaNegocio
{
    public class CN_Sesion
    {
        private static CD_Sesion objDato = new CD_Sesion();

        // MÉTODO PARA INICIAR SESIÓN
        public static int IniciarSesion(string usuario)
        {
            if (string.IsNullOrEmpty(usuario))
                return 0;

            return objDato.IniciarSesion(usuario);
        }

        // MÉTODO PARA CERRAR SESIÓN
        public static string CerrarSesion(int idSesion)
        {
            if (idSesion <= 0)
                return "ID de sesión inválido";

            return objDato.CerrarSesion(idSesion);
        }

        // MÉTODO PARA LISTAR SESIONES
        public static DataTable Listar(string usuario, DateTime fechaInicio, DateTime fechaFin)
        {
            return objDato.Listar(usuario, fechaInicio, fechaFin);
        }
    }
}