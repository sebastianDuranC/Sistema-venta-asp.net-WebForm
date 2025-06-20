using CapaEntidades;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class NegocioDAL
    {
        /// <summary>
        /// Obtiene la cadena de conexión desde el archivo de configuración (Web.config).
        /// </summary>
        private string ObtenerCadenaConexion()
        {
            return ConfigurationManager.ConnectionStrings["conexionSql"].ConnectionString;
        }

        public List<Negocio> ObtenerTodos()
        {
            List<Negocio> lista = new List<Negocio>();

            using (SqlConnection conexion = new SqlConnection(ObtenerCadenaConexion()))
            {
                try
                {
                    conexion.Open();
                    using (SqlCommand comando = new SqlCommand("sp_ObtenerNegocio", conexion))
                    {
                        comando.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader lector = comando.ExecuteReader())
                        {
                            while (lector.Read())
                            {
                                // Mapea los datos del SqlDataReader a un objeto de la entidad
                                lista.Add(new Negocio
                                {
                                    Id = Convert.ToInt32(lector["Id"]),
                                    Nombre = lector["Nombre"].ToString(),
                                    Direccion = lector["Direccion"].ToString(),
                                    LogoUrl = lector["LogoUrl"].ToString()
                                });
                            }
                        }
                    }
                }
                catch (SqlException sqlEx)
                {
                    // Manejo de errores específicos de SQL Server
                    throw new Exception($"Error SQL al obtener todos los registros de {typeof(Negocio).Name}.", sqlEx);
                }
                catch (Exception ex)
                {
                    // Manejo de otros errores inesperados
                    throw new Exception($"Error inesperado al obtener todos los registros de {typeof(Negocio).Name}.", ex);
                }
            }
            return lista;
        }

        public Negocio ObtenerPorId(int id)
        {
            Negocio negocio = null;
            using (SqlConnection conexion = new SqlConnection(ObtenerCadenaConexion()))
            {
                try
                {
                    conexion.Open();
                    using (SqlCommand comando = new SqlCommand("sp_ObtenerNegocioPorId", conexion))
                    {
                        comando.CommandType = CommandType.StoredProcedure;
                        comando.Parameters.AddWithValue("@Id", id);
                        using (SqlDataReader lector = comando.ExecuteReader())
                        {
                            if (lector.Read())
                            {
                                negocio = new Negocio
                                {
                                    Id = Convert.ToInt32(lector["Id"]),
                                    Nombre = lector["Nombre"].ToString(),
                                    Direccion = lector["Direccion"].ToString(),
                                    LogoUrl = lector["LogoUrl"].ToString()
                                };
                            }
                        }
                    }
                }
                catch (SqlException sqlEx)
                {
                    throw new Exception($"Error SQL al obtener el registro de {typeof(Negocio).Name} con ID {id}.", sqlEx);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error inesperado al obtener el registro de {typeof(Negocio).Name} con ID {id}.", ex);
                }
            }
            return negocio;
        }

        /// <summary>
        /// Actualiza un registro existente de la entidad en la base de datos.
        /// </summary>
        public bool EditaNegocio(Negocio negocio)
        {
            using (SqlConnection conexion = new SqlConnection(ObtenerCadenaConexion()))
            {
                try
                {
                    conexion.Open();
                    using (SqlCommand comando = new SqlCommand("sp_EditarNegocio", conexion))
                    {
                        comando.CommandType = CommandType.StoredProcedure;

                        comando.Parameters.AddWithValue("@Id", negocio.Id);
                        comando.Parameters.AddWithValue("@Nombre", negocio.Nombre);
                        comando.Parameters.AddWithValue("@Direccion", negocio.Direccion);
                        comando.Parameters.AddWithValue("@LogoUrl", negocio.LogoUrl);

                        return comando.ExecuteNonQuery() > 0; // Ejecuta el comando y devuelve numero de filas afectadas
                    }
                }
                catch (SqlException sqlEx)
                {
                    throw new Exception($"Error SQL al actualizar {typeof(Negocio).Name} con ID {negocio.Id}.", sqlEx);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error inesperado al actualizar {typeof(Negocio).Name} con ID {negocio.Id}.", ex);
                }
            }
        }
    }
}
