using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class ProductoCategoriaDAL
    {
        /// <summary>
        /// Obtiene la cadena de conexión desde el archivo de configuración (Web.config).
        /// </summary>
        private string ObtenerCadenaConexion()
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["conexionSql"].ConnectionString;
        }

        public List<ProductoCategoria> obtenerCategoriasProducto()
        {
            List<ProductoCategoria> lista = new List<ProductoCategoria>();
            using (SqlConnection conexion = new SqlConnection(ObtenerCadenaConexion()))
            {
                conexion.Open();
                using (SqlCommand comando = new SqlCommand("sp_ObtenerProductoCategorias", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader leer = comando.ExecuteReader())
                    {
                        while (leer.Read())
                        {
                            lista.Add(new ProductoCategoria
                            {
                                Id = Convert.ToInt32(leer["Id"]),
                                Nombre = leer["Nombre"].ToString(),
                                Estado = Convert.ToBoolean(leer["Estado"])
                            });
                        }
                    }
                }
            }
            return lista;
        }

        public bool RegistrarProductoCategoria(ProductoCategoria categoria)
        {
            using (SqlConnection conexion = new SqlConnection(ObtenerCadenaConexion()))
            {
                conexion.Open();
                using (SqlCommand comando = new SqlCommand("sp_RegistrarProductoCategoria", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@Nombre", categoria.Nombre);

                    int filasAfectadas = comando.ExecuteNonQuery();
                    return filasAfectadas > 0;
                }
            }
        }

        public ProductoCategoria ObtenerProductoCategoriaPorId(int id)
        {
            ProductoCategoria categoria = null;
            using (SqlConnection conexion = new SqlConnection(ObtenerCadenaConexion()))
            {
                conexion.Open();
                using (SqlCommand comando = new SqlCommand("sp_ObtenerProductoCategoriasPorId", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@Id", id);
                    using (SqlDataReader leer = comando.ExecuteReader())
                    {
                        if (leer.Read())
                        {
                            categoria = new ProductoCategoria
                            {
                                Id = Convert.ToInt32(leer["Id"]),
                                Nombre = leer["Nombre"].ToString(),
                                Estado = Convert.ToBoolean(leer["Estado"])
                            };
                        }
                    }
                }
            }
            return categoria;
        }

        public bool EditarProductoCategoria(ProductoCategoria categoria)
        {
            using (SqlConnection conexion = new SqlConnection(ObtenerCadenaConexion()))
            {
                conexion.Open();
                using (SqlCommand comando = new SqlCommand("sp_EditarProductoCategoria", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@Id", categoria.Id);
                    comando.Parameters.AddWithValue("@Nombre", categoria.Nombre);
                    int filasAfectadas = comando.ExecuteNonQuery();
                    return filasAfectadas > 0;
                }
            }
        }

        public bool EliminarProductoCategoria(int id)
        {
            using (SqlConnection conexion = new SqlConnection(ObtenerCadenaConexion()))
            {
                conexion.Open();
                using (SqlCommand comando = new SqlCommand("sp_EliminarProductoCategoria", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@Id", id);
                    int filasAfectadas = comando.ExecuteNonQuery();
                    return filasAfectadas > 0;
                }
            }
        }
    }
}
