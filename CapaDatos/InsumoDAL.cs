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
    public class InsumoDAL
    {
        /// <summary>
        /// Obtiene la cadena de conexión desde el archivo de configuración (Web.config).
        /// </summary>
        private string ObtenerCadenaConexion()
        {
            return ConfigurationManager.ConnectionStrings["conexionSql"].ConnectionString;
        }

        public List<Insumo> ObtenerTodos()
        {
            List<Insumo> lista = new List<Insumo>();

            using (SqlConnection conexion = new SqlConnection(ObtenerCadenaConexion()))
            {
                try
                {
                    conexion.Open();
                    using (SqlCommand comando = new SqlCommand("sp_ObtenerInsumos", conexion))
                    {
                        comando.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader lector = comando.ExecuteReader())
                        {
                            while (lector.Read())
                            {
                                lista.Add(new Insumo
                                {
                                    Id = Convert.ToInt32(lector["Id"]),
                                    Nombre = lector["Nombre"].ToString(),
                                    Costo = lector["Costo"] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(lector["Costo"]),
                                    Stock = lector["Stock"] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(lector["Stock"]),
                                    StockMinimo = lector["StockMinimo"] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(lector["StockMinimo"]),
                                    InsumoCategoriaId = Convert.ToInt32(lector["InsumoCategoriaId"]),
                                    InsumoCategoria = new InsumoCategoria
                                    {
                                        Id = Convert.ToInt32(lector["InsumoCategoriaId"]),
                                        Nombre = lector["NombreCategoria"].ToString()
                                    },
                                    ProveedorId = Convert.ToInt32(lector["ProveedorId"]),
                                    Proveedor = new Proveedor
                                    {
                                        Id = Convert.ToInt32(lector["ProveedorId"]),
                                        Nombre = lector["NombreProveedor"].ToString()
                                    },
                                    FotoUrl = lector["FotoUrl"] == DBNull.Value ? null : lector["FotoUrl"].ToString(),
                                    UnidadesMedidaId = Convert.ToInt32(lector["UnidadesMedidaId"]),
                                    UnidadesMedida = new UnidadesMedida
                                    {
                                        Id = Convert.ToInt32(lector["UnidadesMedidaId"]),
                                        Nombre = lector["NombreMedidas"].ToString()
                                    },
                                });
                            }
                        }
                    }
                }
                catch (SqlException sqlEx)
                {
                    throw new Exception($"Error SQL al obtener todos los registros de {typeof(Insumo).Name}.", sqlEx);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error inesperado al obtener todos los registros de {typeof(Insumo).Name}.", ex);
                }
            }
            return lista;
        }

        public Insumo ObtenerInsumoPorId(int id)
        {
            using (SqlConnection conexion = new SqlConnection(ObtenerCadenaConexion()))
            {
                try
                {
                    conexion.Open();
                    using (SqlCommand comando = new SqlCommand("sp_ObtenerInsumoPorId", conexion))
                    {
                        comando.CommandType = CommandType.StoredProcedure;
                        comando.Parameters.AddWithValue("@Id", id);
                        using (SqlDataReader lector = comando.ExecuteReader())
                        {
                            if (lector.Read())
                            {
                                return new Insumo
                                {
                                    Id = Convert.ToInt32(lector["Id"]),
                                    Nombre = lector["Nombre"].ToString(),
                                    Costo = lector["Costo"] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(lector["Costo"]),
                                    Stock = lector["Stock"] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(lector["Stock"]),
                                    StockMinimo = lector["StockMinimo"] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(lector["StockMinimo"]),
                                    InsumoCategoriaId = Convert.ToInt32(lector["InsumoCategoriaId"]),
                                    InsumoCategoria = new InsumoCategoria
                                    {
                                        Id = Convert.ToInt32(lector["InsumoCategoriaId"]),
                                        Nombre = lector["NombreCategoria"].ToString()
                                    },
                                    ProveedorId = Convert.ToInt32(lector["ProveedorId"]),
                                    Proveedor = new Proveedor
                                    {
                                        Id = Convert.ToInt32(lector["ProveedorId"]),
                                        Nombre = lector["NombreProveedor"].ToString()
                                    },
                                    FotoUrl = lector["FotoUrl"] == DBNull.Value ? null : lector["FotoUrl"].ToString(),
                                    UnidadesMedidaId = Convert.ToInt32(lector["UnidadesMedidaId"]),
                                    UnidadesMedida = new UnidadesMedida
                                    {
                                        Id = Convert.ToInt32(lector["UnidadesMedidaId"]),
                                        Nombre = lector["NombreMedidas"].ToString()
                                    },
                                };
                            }
                            else
                            {
                                return null; // No se encontró el insumo con el ID especificado.
                            }
                        }
                    }
                }
                catch (SqlException sqlEx)
                {
                    throw new Exception($"Error SQL al obtener el insumo con ID {id}.", sqlEx);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error inesperado al obtener el insumo con ID {id}.", ex);
                }
            }
        }

        public bool RegistrarInsumo(Insumo insumo)
        {
            using (SqlConnection conexion = new SqlConnection(ObtenerCadenaConexion()))
            {
                try
                {
                    conexion.Open();
                    using (SqlCommand comando = new SqlCommand("sp_RegistrarInsumo", conexion))
                    {
                        comando.CommandType = CommandType.StoredProcedure;
                        comando.Parameters.AddWithValue("@Nombre", insumo.Nombre);
                        comando.Parameters.AddWithValue("@Costo", insumo.Costo ?? (object)DBNull.Value);
                        comando.Parameters.AddWithValue("@Stock", insumo.Stock ?? (object)DBNull.Value);
                        comando.Parameters.AddWithValue("@StockMinimo", insumo.StockMinimo ?? (object)DBNull.Value);
                        comando.Parameters.AddWithValue("@InsumoCategoriaId", insumo.InsumoCategoriaId);
                        comando.Parameters.AddWithValue("@ProveedorId", insumo.ProveedorId);
                        comando.Parameters.AddWithValue("@FotoUrl", string.IsNullOrEmpty(insumo.FotoUrl) ? (object)DBNull.Value : insumo.FotoUrl);
                        comando.Parameters.AddWithValue("@UnidadesMedidaId", insumo.UnidadesMedidaId);
                        int filasAfectadas = comando.ExecuteNonQuery();
                        return filasAfectadas > 0;
                    }
                }
                catch (SqlException sqlEx)
                {
                    throw new Exception($"Error SQL al registrar el insumo {insumo.Nombre}.", sqlEx);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error inesperado al registrar el insumo {insumo.Nombre}.", ex);
                }
            }
        }

        public bool EditarInsumo(Insumo insumo)
        {
            using (SqlConnection conexion = new SqlConnection(ObtenerCadenaConexion()))
            {
                try
                {
                    conexion.Open();
                    using (SqlCommand comando = new SqlCommand("sp_EditarInsumo", conexion))
                    {
                        comando.CommandType = CommandType.StoredProcedure;
                        comando.Parameters.AddWithValue("@Id", insumo.Id);
                        comando.Parameters.AddWithValue("@Nombre", insumo.Nombre);
                        comando.Parameters.AddWithValue("@Costo", insumo.Costo ?? (object)DBNull.Value);
                        comando.Parameters.AddWithValue("@Stock", insumo.Stock ?? (object)DBNull.Value);
                        comando.Parameters.AddWithValue("@StockMinimo", insumo.StockMinimo ?? (object)DBNull.Value);
                        comando.Parameters.AddWithValue("@InsumoCategoriaId", insumo.InsumoCategoriaId);
                        comando.Parameters.AddWithValue("@ProveedorId", insumo.ProveedorId);
                        comando.Parameters.AddWithValue("@FotoUrl", string.IsNullOrEmpty(insumo.FotoUrl) ? (object)DBNull.Value : insumo.FotoUrl);
                        comando.Parameters.AddWithValue("@UnidadesMedidaId", insumo.UnidadesMedidaId);
                        int filasAfectadas = comando.ExecuteNonQuery();
                        return filasAfectadas > 0;
                    }
                }
                catch (SqlException sqlEx)
                {
                    throw new Exception($"Error SQL al editar el insumo {insumo.Nombre}.", sqlEx);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error inesperado al editar el insumo {insumo.Nombre}.", ex);
                }
            }
        }

        public bool EliminarInsumo(int id)
        {
            using (SqlConnection conexion = new SqlConnection(ObtenerCadenaConexion()))
            {
                try
                {
                    conexion.Open();
                    using (SqlCommand comando = new SqlCommand("sp_EliminarInsumo", conexion))
                    {
                        comando.CommandType = CommandType.StoredProcedure;
                        comando.Parameters.AddWithValue("@Id", id);
                        int filasAfectadas = comando.ExecuteNonQuery();
                        return filasAfectadas > 0;
                    }
                }
                catch (SqlException sqlEx)
                {
                    throw new Exception($"Error SQL al eliminar el insumo con ID {id}.", sqlEx);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error inesperado al eliminar el insumo con ID {id}.", ex);
                }
            }
        }
    }
}
