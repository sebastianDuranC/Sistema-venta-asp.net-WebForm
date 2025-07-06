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
    public class ProveedorDAL
    {
        /// <summary>
        /// Obtiene la cadena de conexión desde el archivo de configuración (Web.config).
        /// </summary>
        private string ObtenerCadenaConexion()
        {
            return ConfigurationManager.ConnectionStrings["conexionSql"].ConnectionString;
        }

        public List<Proveedor> ObtenerProveedores()
        {
            List<Proveedor> proveedores = new List<Proveedor>();
            using (SqlConnection conexion = new SqlConnection(ObtenerCadenaConexion()))
            {
                try
                {
                    conexion.Open();
                    using (SqlCommand comando = new SqlCommand("sp_ObtenerProveedores", conexion))
                    {
                        comando.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader lector = comando.ExecuteReader())
                        {
                            while (lector.Read())
                            {
                                proveedores.Add(new Proveedor
                                {
                                    Id = Convert.ToInt32(lector["Id"]),
                                    Nombre = lector["Nombre"].ToString(),
                                    Contacto = lector["Contacto"].ToString(),
                                });
                            }
                        }
                    }
                }
                catch (SqlException sqlEx)
                {
                    throw new Exception($"Error SQL al obtener todos los registros de {typeof(Proveedor).Name}.", sqlEx);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error inesperado al obtener todos los registros de {typeof(Proveedor).Name}.", ex);
                }
            }
            return proveedores;
        }

        public Proveedor ObtenerProveedorPorId(int id)
        {
            Proveedor proveedor = null;
            using (SqlConnection conexion = new SqlConnection(ObtenerCadenaConexion()))
            {
                try
                {
                    conexion.Open();
                    using (SqlCommand comando = new SqlCommand("sp_ObtenerProveedorPorId", conexion))
                    {
                        comando.CommandType = CommandType.StoredProcedure;
                        comando.Parameters.AddWithValue("@Id", id);
                        using (SqlDataReader lector = comando.ExecuteReader())
                        {
                            if (lector.Read())
                            {
                                proveedor = new Proveedor
                                {
                                    Id = Convert.ToInt32(lector["Id"]),
                                    Nombre = lector["Nombre"].ToString(),
                                    Contacto = lector["Contacto"].ToString(),
                                };
                            }
                        }
                    }
                }
                catch (SqlException sqlEx)
                {
                    throw new Exception($"Error SQL al obtener el registro de {typeof(Proveedor).Name} con ID {id}.", sqlEx);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error inesperado al obtener el registro de {typeof(Proveedor).Name} con ID {id}.", ex);
                }
            }
            return proveedor;
        }

        public bool RegistrarProveedor(Proveedor proveedor)
        {
            using (SqlConnection conexion = new SqlConnection(ObtenerCadenaConexion()))
            {
                try
                {
                    conexion.Open();
                    using (SqlCommand comando = new SqlCommand("sp_RegistrarProveedor", conexion))
                    {
                        comando.CommandType = CommandType.StoredProcedure;
                        comando.Parameters.AddWithValue("@Nombre", proveedor.Nombre);
                        comando.Parameters.AddWithValue("@Contacto", proveedor.Contacto);
                        int filasAfectadas = comando.ExecuteNonQuery();
                        return filasAfectadas > 0;
                    }
                }
                catch (SqlException sqlEx)
                {
                    throw new Exception($"Error SQL al registrar el proveedor {proveedor.Nombre}.", sqlEx);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error inesperado al registrar el proveedor {proveedor.Nombre}.", ex);
                }
            }
        }

        public bool EditarProveedor(Proveedor proveedor)
        {
            using (SqlConnection conexion = new SqlConnection(ObtenerCadenaConexion()))
            {
                try
                {
                    conexion.Open();
                    using (SqlCommand comando = new SqlCommand("sp_EditarProveedor", conexion))
                    {
                        comando.CommandType = CommandType.StoredProcedure;
                        comando.Parameters.AddWithValue("@Id", proveedor.Id);
                        comando.Parameters.AddWithValue("@Nombre", proveedor.Nombre);
                        comando.Parameters.AddWithValue("@Contacto", proveedor.Contacto);
                        int filasAfectadas = comando.ExecuteNonQuery();
                        return filasAfectadas > 0;
                    }
                }
                catch (SqlException sqlEx)
                {
                    throw new Exception($"Error SQL al editar el proveedor {proveedor.Nombre}.", sqlEx);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error inesperado al editar el proveedor {proveedor.Nombre}.", ex);
                }
            }
        }

        public bool EliminarProveedor(int id)
        {
            using (SqlConnection conexion = new SqlConnection(ObtenerCadenaConexion()))
            {
                try
                {
                    conexion.Open();
                    using (SqlCommand comando = new SqlCommand("sp_EliminarProveedor", conexion))
                    {
                        comando.CommandType = CommandType.StoredProcedure;
                        comando.Parameters.AddWithValue("@Id", id);
                        int filasAfectadas = comando.ExecuteNonQuery();
                        return filasAfectadas > 0;
                    }
                }
                catch (SqlException sqlEx)
                {
                    throw new Exception($"Error SQL al eliminar el proveedor con ID {id}.", sqlEx);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error inesperado al eliminar el proveedor con ID {id}.", ex);
                }
            }
        }
    }
}
