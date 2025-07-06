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
    public class ProductoDAL
    {
        /// <summary>
        /// Obtiene la cadena de conexión desde el archivo de configuración (Web.config).
        /// </summary>
        private string ObtenerCadenaConexion()
        {
            return ConfigurationManager.ConnectionStrings["conexionSql"].ConnectionString;
        }

        public DataTable ObtenerProductoPorId(int id)
        {
            DataTable producto = new DataTable();
            using (SqlConnection conexion = new SqlConnection(ObtenerCadenaConexion()))
            {
                conexion.Open();
                using (SqlCommand comando = new SqlCommand("sp_ObtenerProductoPorId", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@Id", id);
                    using (SqlDataAdapter adaptador = new SqlDataAdapter(comando))
                    {
                        adaptador.Fill(producto);
                    }
                }
            }
            return producto;
        }

        public List<Producto> ObtenerProductos()
        {
            List<Producto> lista = new List<Producto>();

            using (SqlConnection conexion = new SqlConnection(ObtenerCadenaConexion()))
            {
                conexion.Open();
                using (SqlCommand comando = new SqlCommand("sp_ObtenerProductos", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader leer = comando.ExecuteReader())
                    {
                        while (leer.Read())
                        {

                            lista.Add(new Producto
                            {
                                Id = Convert.ToInt32(leer["Id"]),
                                Nombre = leer["Nombre"].ToString(),
                                Precio = Convert.ToDecimal(leer["Precio"]),
                                Stock = leer["Stock"] != DBNull.Value ? (int?)Convert.ToInt32(leer["Stock"]) : null,
                                StockMinimo = leer["StockMinimo"] != DBNull.Value ? (int?)Convert.ToInt32(leer["StockMinimo"]) : null,
                                ProductoCategoriaId = Convert.ToInt32(leer["ProductoCategoriaId"]),
                                ProductoCategoria = new ProductoCategoria
                                {
                                    Id = Convert.ToInt32(leer["ProductoCategoriaId"]),
                                    Nombre = leer["NombreCategoria"].ToString()
                                },
                                FotoUrl = leer["FotoUrl"] != DBNull.Value ? leer["FotoUrl"].ToString() : null
                            });

                        }
                    }
                }
            }
            return lista;
        }

        public bool RegistrarProducto(Producto producto, DataTable productoInsumo)
        {
            using (SqlConnection conexion = new SqlConnection(ObtenerCadenaConexion()))
            {
                conexion.Open();
                using (SqlCommand comando = new SqlCommand("sp_RegistrarProducto", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    // --- Parámetros para el Producto ---
                    comando.Parameters.AddWithValue("@Nombre", producto.Nombre);
                    comando.Parameters.AddWithValue("@Precio", producto.Precio);
                    comando.Parameters.AddWithValue("@Stock", (object)producto.Stock ?? DBNull.Value);
                    comando.Parameters.AddWithValue("@StockMinimo", (object)producto.StockMinimo ?? DBNull.Value);
                    comando.Parameters.AddWithValue("@ProductoCategoriaId", producto.ProductoCategoriaId);
                    comando.Parameters.AddWithValue("@FotoUrl", (object)producto.FotoUrl ?? DBNull.Value);


                    // --- Parámetro para la lista de Insumos (Table-Valued Parameter) ---
                    SqlParameter parametroInsumos = new SqlParameter("@Insumos", SqlDbType.Structured);
                    parametroInsumos.Value = productoInsumo;
                    parametroInsumos.TypeName = "dbo.ProductoInsumoType"; // El nombre del tipo
                    comando.Parameters.Add(parametroInsumos);

                    return comando.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool EditarProducto(Producto producto, DataTable productoInsumo)
        {
            using (SqlConnection conexion = new SqlConnection(ObtenerCadenaConexion()))
            {
                conexion.Open();
                using (SqlCommand comando = new SqlCommand("sp_EditarProductos", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@Id", producto.Id);
                    comando.Parameters.AddWithValue("@Nombre", producto.Nombre);
                    comando.Parameters.AddWithValue("@Precio", producto.Precio);
                    comando.Parameters.AddWithValue("@Stock", (object)producto.Stock ?? DBNull.Value);
                    comando.Parameters.AddWithValue("@StockMinimo", (object)producto.StockMinimo ?? DBNull.Value);
                    comando.Parameters.AddWithValue("@ProductoCategoriaId", producto.ProductoCategoriaId);
                    comando.Parameters.AddWithValue("@FotoUrl", (object)producto.FotoUrl ?? DBNull.Value);

                    // Parámetro para la lista de insumos (Table-Valued Parameter)
                    SqlParameter parametroInsumos = new SqlParameter("@Insumos", SqlDbType.Structured);
                    parametroInsumos.Value = productoInsumo;
                    parametroInsumos.TypeName = "dbo.ProductoInsumoType";
                    comando.Parameters.Add(parametroInsumos);

                    return comando.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool EliminarProducto(int id)
        {
            using (SqlConnection conexion = new SqlConnection(ObtenerCadenaConexion()))
            {
                conexion.Open();
                using (SqlCommand comando = new SqlCommand("sp_EliminarProducto", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@Id", id);
                    return comando.ExecuteNonQuery() > 0;
                }
            }
        }
    }
}
