using System;
using System.Data;
using CapaDatos;

namespace CapaNegocio
{
    /// <summary>
    /// CAPA DE NEGOCIO - GESTIÓN DE MASCOTAS
    /// Contiene la lógica de negocio y validaciones para mascotas
    /// </summary>
    public class CN_Mascota
    {
        // INSTANCIA DE LA CAPA DE DATOS
        private static CD_Mascota objDato = new CD_Mascota();

        /// <summary>
        /// MÉTODO PARA LISTAR TODAS LAS MASCOTAS
        /// </summary>
        public static DataTable Listar()
        {
            return objDato.Listar();
        }

        /// <summary>
        /// MÉTODO PARA GUARDAR UNA NUEVA MASCOTA
        /// Incluye validaciones de negocio
        /// </summary>
        public static string Guardar(string nombre, string especie, string raza, string sexo,
                                     int edad, decimal peso, string color, string estado, int idcliente)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                return "El nombre de la mascota es obligatorio";

            if (string.IsNullOrWhiteSpace(especie))
                return "La especie es obligatoria";

            if (edad < 0 || edad > 30)
                return "La edad debe estar entre 0 y 30 años";

            if (peso <= 0 || peso > 200)
                return "El peso debe estar entre 0.1 y 200 kg";

            if (idcliente <= 0)
                return "Debe seleccionar un dueño para la mascota";

            CD_Mascota obj = new CD_Mascota
            {
                Nombre = nombre,
                Especie = especie,
                Raza = raza,
                Sexo = sexo,
                Edad = edad,
                Peso = peso,
                Color = color,
                Estado = estado,
                Idcliente = idcliente
            };

            return objDato.Guardar(obj);
        }

        /// <summary>
        /// MÉTODO PARA EDITAR UNA MASCOTA EXISTENTE
        /// </summary>
        public static string Editar(int idmascota, string nombre, string especie, string raza,
                                   string sexo, int edad, decimal peso, string color, string estado, int idcliente)
        {
            if (idmascota <= 0)
                return "ID de mascota inválido";

            if (string.IsNullOrWhiteSpace(nombre))
                return "El nombre de la mascota es obligatorio";

            if (string.IsNullOrWhiteSpace(especie))
                return "La especie es obligatoria";

            if (edad < 0 || edad > 30)
                return "La edad debe estar entre 0 y 30 años";

            if (peso <= 0 || peso > 200)
                return "El peso debe estar entre 0.1 y 200 kg";

            if (idcliente <= 0)
                return "Debe seleccionar un dueño para la mascota";

            CD_Mascota obj = new CD_Mascota
            {
                Idmascota = idmascota,
                Nombre = nombre,
                Especie = especie,
                Raza = raza,
                Sexo = sexo,
                Edad = edad,
                Peso = peso,
                Color = color,
                Estado = estado,
                Idcliente = idcliente
            };

            return objDato.Editar(obj);
        }

        /// <summary>
        /// MÉTODO PARA ELIMINAR UNA MASCOTA (cambio de estado a INACTIVO)
        /// </summary>
        public static string Eliminar(int idmascota)
        {
            if (idmascota <= 0)
                return "ID de mascota inválido";

            CD_Mascota obj = new CD_Mascota
            {
                Idmascota = idmascota
            };

            return objDato.Eliminar(obj);
        }

        /// <summary>
        /// MÉTODO PARA BUSCAR MASCOTAS POR NOMBRE DE MASCOTA
        /// </summary>
        public static DataTable BuscarNombre(string nombre)
        {
            CD_Mascota obj = new CD_Mascota
            {
                Buscar = nombre
            };

            return objDato.BuscarNombre(obj);
        }

        /// <summary>
        /// MÉTODO PARA BUSCAR MASCOTAS POR ID DE CLIENTE
        /// (se mantiene por compatibilidad con otros módulos)
        /// </summary>
        public static DataTable BuscarPorCliente(int idcliente)
        {
            return objDato.BuscarPorCliente(idcliente);
        }

        /// <summary>
        /// MÉTODO PARA BUSCAR MASCOTAS POR NOMBRE DEL DUEÑO (búsqueda parcial)
        /// Usado por la nueva SearchBar de "Buscar por dueño"
        /// </summary>
        public static DataTable BuscarPorNombreCliente(string nombreCliente)
        {
            if (string.IsNullOrWhiteSpace(nombreCliente))
                return Listar();

            CD_Mascota obj = new CD_Mascota
            {
                Buscar = nombreCliente
            };

            return objDato.BuscarPorNombreCliente(obj);
        }

        /// <summary>
        /// MÉTODO PARA OBTENER LISTA DE CLIENTES
        /// </summary>
        public static DataTable ObtenerClientes()
        {
            return objDato.ObtenerClientes();
        }
    }
}