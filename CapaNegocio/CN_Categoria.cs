using System;
using System.Data;
using CapaDatos;

namespace CapaNegocio
{
    /// <summary>
    /// CAPA DE NEGOCIO - GESTIÓN DE CATEGORÍAS
    /// Contiene la lógica de negocio y validaciones para categorías
    /// </summary>
    public class CN_Categoria
    {
        // INSTANCIA DE LA CAPA DE DATOS
        private static CD_Categoria objDato = new CD_Categoria();

        /// <summary>
        /// MÉTODO PARA LISTAR TODAS LAS CATEGORÍAS
        /// </summary>
        public static DataTable Listar()
        {
            return objDato.Listar();
        }

        /// <summary>
        /// MÉTODO PARA LISTAR SOLO CATEGORÍAS ACTIVAS
        /// Útil para ComboBox
        /// </summary>
        public static DataTable ListarActivas()
        {
            return objDato.ListarActivas();
        }

        /// <summary>
        /// MÉTODO PARA GUARDAR UNA NUEVA CATEGORÍA
        /// Incluye validaciones de negocio
        /// </summary>
        public static string Guardar(string nombre, string descripcion, string estado)
        {
            // VALIDACIONES
            if (string.IsNullOrWhiteSpace(nombre))
                return "El nombre de la categoría es obligatorio";

            if (nombre.Length < 3)
                return "El nombre debe tener al menos 3 caracteres";

            if (nombre.Length > 50)
                return "El nombre no puede exceder 50 caracteres";

            // CREAR OBJETO Y GUARDAR
            CD_Categoria obj = new CD_Categoria
            {
                Nombre = nombre.Trim(),
                Descripcion = descripcion?.Trim() ?? "",
                Estado = estado
            };

            return objDato.Guardar(obj);
        }

        /// <summary>
        /// MÉTODO PARA EDITAR UNA CATEGORÍA EXISTENTE
        /// </summary>
        public static string Editar(int idcategoria, string nombre, string descripcion, string estado)
        {
            // VALIDACIONES
            if (idcategoria <= 0)
                return "ID de categoría inválido";

            if (string.IsNullOrWhiteSpace(nombre))
                return "El nombre de la categoría es obligatorio";

            if (nombre.Length < 3)
                return "El nombre debe tener al menos 3 caracteres";

            if (nombre.Length > 50)
                return "El nombre no puede exceder 50 caracteres";

            // CREAR OBJETO Y EDITAR
            CD_Categoria obj = new CD_Categoria
            {
                Idcategoria = idcategoria,
                Nombre = nombre.Trim(),
                Descripcion = descripcion?.Trim() ?? "",
                Estado = estado
            };

            return objDato.Editar(obj);
        }

        /// <summary>
        /// MÉTODO PARA ELIMINAR UNA CATEGORÍA
        /// No permite eliminar si tiene productos asociados
        /// </summary>
        public static string Eliminar(int idcategoria)
        {
            if (idcategoria <= 0)
                return "ID de categoría inválido";

            CD_Categoria obj = new CD_Categoria
            {
                Idcategoria = idcategoria
            };

            return objDato.Eliminar(obj);
        }
    }
}