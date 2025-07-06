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
    public class UnidadMedidaDAL
    {
        /// <summary>
        /// Obtiene la cadena de conexión desde el archivo de configuración (Web.config).
        /// </summary>
        private string ObtenerCadenaConexion()
        {
            return ConfigurationManager.ConnectionStrings["conexionSql"].ConnectionString;
        }

        public List<UnidadesMedida> ObtenerUnidadesMedida()
        {
            List<UnidadesMedida> lista = new List<UnidadesMedida>();

            using (SqlConnection conexion = new SqlConnection(ObtenerCadenaConexion()))
            {
                try
                {
                    conexion.Open();
                    using (SqlCommand comando = new SqlCommand("sp_ObtenerUnidadesMedida", conexion))
                    {
                        comando.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader lector = comando.ExecuteReader())
                        {
                            while (lector.Read())
                            {
                                // Mapea los datos del SqlDataReader a un objeto de la entidad
                                lista.Add(new UnidadesMedida
                                {
                                    Id = Convert.ToInt32(lector["Id"]),
                                    Nombre = lector["Nombre"].ToString(),
                                    Abreviatura = lector["Abreviatura"].ToString(),
                                });
                            }
                        }
                    }
                }
                catch (SqlException sqlEx)
                {
                    // Manejo de errores específicos de SQL Server
                    throw new Exception($"Error SQL al obtener todos los registros de {typeof(UnidadesMedida).Name}.", sqlEx);
                }
                catch (Exception ex)
                {
                    // Manejo de otros errores inesperados
                    throw new Exception($"Error inesperado al obtener todos los registros de {typeof(UnidadesMedida).Name}.", ex);
                }
            }
            return lista;
        }

        public UnidadesMedida ObtenerUnidadMedidaPorId(int id)
        {
            UnidadesMedida unidadMedida = null;
            using (SqlConnection conexion = new SqlConnection(ObtenerCadenaConexion()))
            {
                try
                {
                    conexion.Open();
                    using (SqlCommand comando = new SqlCommand("sp_ObtenerUnidadMedidaPorId", conexion))
                    {
                        comando.CommandType = CommandType.StoredProcedure;
                        comando.Parameters.AddWithValue("@Id", id);
                        using (SqlDataReader lector = comando.ExecuteReader())
                        {
                            if (lector.Read())
                            {
                                unidadMedida = new UnidadesMedida
                                {
                                    Id = Convert.ToInt32(lector["Id"]),
                                    Nombre = lector["Nombre"].ToString(),
                                    Abreviatura = lector["Abreviatura"].ToString(),
                                    Estado = Convert.ToBoolean(lector["Estado"])
                                };
                            }
                        }
                    }
                }
                catch (SqlException sqlEx)
                {
                    throw new Exception($"Error SQL al obtener el registro de {typeof(UnidadesMedida).Name} por ID.", sqlEx);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error inesperado al obtener el registro de {typeof(UnidadesMedida).Name} por ID.", ex);
                }
            }
            return unidadMedida;
        }

        public bool RegistrarUnidadMedida(UnidadesMedida unidadMedida)
        {
            using (SqlConnection conexion = new SqlConnection(ObtenerCadenaConexion()))
            {
                try
                {
                    conexion.Open();
                    using (SqlCommand comando = new SqlCommand("sp_RegistrarUnidadMedida", conexion))
                    {
                        comando.CommandType = CommandType.StoredProcedure;
                        comando.Parameters.AddWithValue("@Nombre", unidadMedida.Nombre);
                        comando.Parameters.AddWithValue("@Abreviatura", unidadMedida.Abreviatura);
                        int filasAfectadas = comando.ExecuteNonQuery();
                        return filasAfectadas > 0;
                    }
                }
                catch (SqlException sqlEx)
                {
                    throw new Exception($"Error SQL al registrar el registro de {typeof(UnidadesMedida).Name}.", sqlEx);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error inesperado al registrar el registro de {typeof(UnidadesMedida).Name}.", ex);
                }
            }
        }

        public bool EditarUnidadMedida(UnidadesMedida unidadMedida)
        {
            using (SqlConnection conexion = new SqlConnection(ObtenerCadenaConexion()))
            {
                try
                {
                    conexion.Open();
                    using (SqlCommand comando = new SqlCommand("sp_EditarUnidadMedida", conexion))
                    {
                        comando.CommandType = CommandType.StoredProcedure;
                        comando.Parameters.AddWithValue("@Id", unidadMedida.Id);
                        comando.Parameters.AddWithValue("@Nombre", unidadMedida.Nombre);
                        comando.Parameters.AddWithValue("@Abreviatura", unidadMedida.Abreviatura);
                        int filasAfectadas = comando.ExecuteNonQuery();
                        return filasAfectadas > 0;
                    }
                }
                catch (SqlException sqlEx)
                {
                    throw new Exception($"Error SQL al editar el registro de {typeof(UnidadesMedida).Name}.", sqlEx);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error inesperado al editar el registro de {typeof(UnidadesMedida).Name}.", ex);
                }
            }
        }

        public bool EliminarUnidadMedida(int id)
        {
            using (SqlConnection conexion = new SqlConnection(ObtenerCadenaConexion()))
            {
                try
                {
                    conexion.Open();
                    using (SqlCommand comando = new SqlCommand("sp_EliminarUnidadMedida", conexion))
                    {
                        comando.CommandType = CommandType.StoredProcedure;
                        comando.Parameters.AddWithValue("@Id", id);
                        int filasAfectadas = comando.ExecuteNonQuery();
                        return filasAfectadas > 0;
                    }
                }
                catch (SqlException sqlEx)
                {
                    throw new Exception($"Error SQL al eliminar el registro de {typeof(UnidadesMedida).Name}.", sqlEx);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error inesperado al eliminar el registro de {typeof(UnidadesMedida).Name}.", ex);
                }
            }
        }
    }
}
