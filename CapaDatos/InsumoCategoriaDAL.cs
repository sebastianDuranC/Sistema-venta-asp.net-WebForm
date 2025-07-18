using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class InsumoCategoriaDAL
    {
        /// <summary>
        /// Obtiene la cadena de conexión desde el archivo de configuración (Web.config).
        /// </summary>
        private string ObtenerCadenaConexion()
        {
            return ConfigurationManager.ConnectionStrings["conexionSql"].ConnectionString;
        }

        public List<InsumoCategoria> ObtenerTodos()
        {
            List<InsumoCategoria> lista = new List<InsumoCategoria>();

            using (SqlConnection conexion = new SqlConnection(ObtenerCadenaConexion()))
            {
                try
                {
                    conexion.Open();
                    using (SqlCommand comando = new SqlCommand("sp_ObtenerInsumoCategoria", conexion))
                    {
                        comando.CommandType = System.Data.CommandType.StoredProcedure;

                        using (SqlDataReader lector = comando.ExecuteReader())
                        {
                            while (lector.Read())
                            {
                                lista.Add(new InsumoCategoria
                                {
                                    Id = Convert.ToInt32(lector["Id"]),
                                    Nombre = lector["Nombre"].ToString(),
                                    Estado = Convert.ToBoolean(lector["Estado"])
                                });
                            }
                        }
                    }
                }
                catch (SqlException sqlEx)
                {
                    throw new Exception($"Error SQL al obtener todos los registros de {typeof(InsumoCategoria).Name}.", sqlEx);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error inesperado al obtener todos los registros de {typeof(InsumoCategoria).Name}.", ex);
                }
            }
            return lista;
        }

        public bool RegistrarInsumoCategoria(InsumoCategoria insumoCategoria)
        {
            using (SqlConnection conexion = new SqlConnection(ObtenerCadenaConexion()))
            {
                try
                {
                    conexion.Open();
                    using (SqlCommand comando = new SqlCommand("sp_RegistrarInsumoCategoria", conexion))
                    {
                        comando.CommandType = System.Data.CommandType.StoredProcedure;
                        comando.Parameters.AddWithValue("@Nombre", insumoCategoria.Nombre);
                        int filasAfectadas = comando.ExecuteNonQuery();
                        return filasAfectadas > 0;
                    }
                }
                catch (SqlException sqlEx)
                {
                    throw new Exception($"Error SQL al registrar {typeof(InsumoCategoria).Name}.", sqlEx);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error inesperado al registrar {typeof(InsumoCategoria).Name}.", ex);
                }
            }
        }

        public InsumoCategoria ObtenerInsumoCategoriaPorId(int id)
        {
            InsumoCategoria insumoCategoria = null;
            using (SqlConnection conexion = new SqlConnection(ObtenerCadenaConexion()))
            {
                try
                {
                    conexion.Open();
                    using (SqlCommand comando = new SqlCommand("sp_ObtenerInsumoCategoriaPorId", conexion))
                    {
                        comando.CommandType = System.Data.CommandType.StoredProcedure;
                        comando.Parameters.AddWithValue("@Id", id);
                        using (SqlDataReader lector = comando.ExecuteReader())
                        {
                            while (lector.Read())
                            {
                                insumoCategoria = new InsumoCategoria
                                {
                                    Id = Convert.ToInt32(lector["Id"]),
                                    Nombre = lector["Nombre"].ToString(),
                                    Estado = Convert.ToBoolean(lector["Estado"])
                                };
                            }
                        }
                    }
                }
                catch (SqlException sqlEx)
                {
                    throw new Exception($"Error SQL al obtener todos los registros de {typeof(InsumoCategoria).Name}.", sqlEx);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error inesperado al obtener todos los registros de {typeof(InsumoCategoria).Name}.", ex);
                }
                return insumoCategoria;
            }
        }

        public bool EditarInsumoCategoria(InsumoCategoria insumoCategoria)
        {
            using (SqlConnection conexion = new SqlConnection(ObtenerCadenaConexion()))
            {
                try
                {
                    conexion.Open();
                    using (SqlCommand comando = new SqlCommand("sp_EditarInsumoCategoria", conexion))
                    {
                        comando.CommandType = System.Data.CommandType.StoredProcedure;
                        comando.Parameters.AddWithValue("@Id", insumoCategoria.Id);
                        comando.Parameters.AddWithValue("@Nombre", insumoCategoria.Nombre);
                        int filasAfectadas = comando.ExecuteNonQuery();
                        return filasAfectadas > 0;
                    }
                }
                catch (SqlException sqlEx)
                {
                    throw new Exception($"Error SQL al editar {typeof(InsumoCategoria).Name}.", sqlEx);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error inesperado al editar {typeof(InsumoCategoria).Name}.", ex);
                }
            }
        }   

        public bool EliminarInsumoCategoria(int id)
        {
            using (SqlConnection conexion = new SqlConnection(ObtenerCadenaConexion()))
            {
                try
                {
                    conexion.Open();
                    using (SqlCommand comando = new SqlCommand("sp_EliminarInsumoCategoria", conexion))
                    {
                        comando.CommandType = System.Data.CommandType.StoredProcedure;
                        comando.Parameters.AddWithValue("@Id", id);
                        int filasAfectadas = comando.ExecuteNonQuery();
                        return filasAfectadas > 0;
                    }
                }
                catch (SqlException sqlEx)
                {
                    throw new Exception($"Error SQL al eliminar {typeof(InsumoCategoria).Name}.", sqlEx);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error inesperado al eliminar {typeof(InsumoCategoria).Name}.", ex);
                }
            }
        }
    }
}
