using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class RolPermisoDAL
    {
        /// <summary>
        /// Obtiene la cadena de conexión desde el archivo de configuración (App.config/Web.config).
        /// </summary>
        private string ObtenerCadenaConexion()
        {
            return ConfigurationManager.ConnectionStrings["conexionSql"].ConnectionString;
        }

        /// <summary>
        /// Actualiza el estado de un permiso específico para un rol y un formulario.
        /// </summary>
        public bool ActualizarRolPermisos(int rolId, int formId, bool check)
        {
            using (SqlConnection conexion = new SqlConnection(ObtenerCadenaConexion()))
            {
                try
                {
                    conexion.Open();
                    using (SqlCommand comando = new SqlCommand("sp_ActualizarRolPermisos", conexion))
                    {
                        comando.CommandType = CommandType.StoredProcedure;
                        comando.Parameters.AddWithValue("@IsAllowed", check);
                        comando.Parameters.AddWithValue("@RolId", rolId);
                        comando.Parameters.AddWithValue("@FormId", formId);

                        return comando.ExecuteNonQuery() > 0; // Devuelve true si se afectó al menos una fila.
                    }
                }
                catch (SqlException sqlEx)
                {
                    // Puedes registrar el error aquí si lo deseas (log4net, Serilog, etc.)
                    throw new Exception($"Error SQL al actualizar el permiso para el Rol ID {rolId} y Form ID {formId}.", sqlEx);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error inesperado al actualizar el permiso del rol.", ex);
                }
            }
        }

        /// <summary>
        /// Obtiene el estado de un permiso para un rol y formulario específico.
        /// </summary>
        public bool ObtenerEsPermitidoForm(int rolId, int formId)
        {
            bool esPermitido = false;
            using (SqlConnection conexion = new SqlConnection(ObtenerCadenaConexion()))
            {
                try
                {
                    conexion.Open();
                    using (SqlCommand comando = new SqlCommand("sp_ObtenerEsPermitidoForm", conexion))
                    {
                        comando.CommandType = CommandType.StoredProcedure;
                        comando.Parameters.AddWithValue("@RolId", rolId);
                        comando.Parameters.AddWithValue("@FormId", formId);

                        using (SqlDataReader lector = comando.ExecuteReader())
                        {
                            if (lector.Read())
                            {
                                // Se asume que la columna "Estado" es de tipo BIT o INT (1 o 0) en la BD.
                                esPermitido = Convert.ToBoolean(lector["Estado"]);
                            }
                        }
                    }
                }
                catch (SqlException sqlEx)
                {
                    throw new Exception($"Error SQL al obtener el permiso del formulario para el Rol ID {rolId}.", sqlEx);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error inesperado al obtener el permiso del formulario.", ex);
                }
            }
            return esPermitido;
        }

        /// <summary>
        /// Verifica si un rol tiene al menos un permiso activo para un formulario.
        /// Utiliza ExecuteScalar para mayor eficiencia.
        /// </summary>
        public bool UsuarioTienePermisoForm(int rolId, int formularioId)
        {
            if (rolId <= 0 || formularioId <= 0)
            {
                return false;
            }

            using (SqlConnection conexion = new SqlConnection(ObtenerCadenaConexion()))
            {
                try
                {
                    conexion.Open();
                    using (SqlCommand comando = new SqlCommand("sp_ObtenerNumeroPermisos", conexion))
                    {
                        comando.CommandType = CommandType.StoredProcedure;
                        comando.Parameters.AddWithValue("@RolId", rolId);
                        comando.Parameters.AddWithValue("@FormularioId", formularioId);

                        // ExecuteScalar es ideal para consultas que devuelven un único valor.
                        object resultado = comando.ExecuteScalar();

                        // Si el resultado no es nulo y es mayor que 0, tiene permiso.
                        return (resultado != null && Convert.ToInt32(resultado) > 0);
                    }
                }
                catch (SqlException sqlEx)
                {
                    throw new Exception($"Error SQL al verificar el permiso para el Rol ID {rolId} en el Formulario ID {formularioId}.", sqlEx);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error inesperado al verificar el permiso del usuario.", ex);
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de todas las rutas de formularios permitidas para un rol específico.
        /// </summary>
        public List<string> ObtenerRutasPermitidasPorRol(int rolId)
        {
            List<string> rutasPermitidas = new List<string>();

            using (SqlConnection conexion = new SqlConnection(ObtenerCadenaConexion()))
            {
                try
                {
                    conexion.Open();
                    using (SqlCommand comando = new SqlCommand("sp_ObtenerRutasPermitidasPorRol", conexion))
                    {
                        comando.CommandType = CommandType.StoredProcedure;
                        comando.Parameters.AddWithValue("@RolId", rolId);

                        using (SqlDataReader lector = comando.ExecuteReader())
                        {
                            while (lector.Read())
                            {
                                rutasPermitidas.Add(lector["FormRuta"].ToString());
                            }
                        }
                    }
                }
                catch (SqlException sqlEx)
                {
                    throw new Exception($"Error SQL al obtener las rutas permitidas para el Rol ID {rolId}.", sqlEx);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error inesperado al obtener las rutas permitidas por rol.", ex);
                }
            }
            return rutasPermitidas;
        }
    }
}
