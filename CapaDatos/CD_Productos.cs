using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_Productos
    {
        private CD_Conexion conexion = new CD_Conexion();


        public Producto ObtenerProductoPorId(int id)
        {
            Producto producto = null;
            SqlConnection con = conexion.AbrirBd();
            using (SqlCommand cmd = new SqlCommand("SELECT Id, Nombre, Precio, Stock, StockMinimo, ProductoCategoriaId, FotoUrl, Estado FROM Producto WHERE Id = @id", con))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@id", id);

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        producto = new Producto
                        {
                            Id = Convert.ToInt32(dr["Id"]),
                            Nombre = dr["Nombre"].ToString(),
                            Precio = Convert.ToDecimal(dr["Precio"]),
                            Stock = dr["Stock"] != DBNull.Value ? Convert.ToInt32(dr["Stock"]) : (int?)null,
                            StockMinimo = dr["StockMinimo"] != DBNull.Value ? Convert.ToInt32(dr["StockMinimo"]) : (int?)null,
                            ProductoCategoriaId = Convert.ToInt32(dr["ProductoCategoriaId"]),
                            FotoUrl = dr["FotoUrl"].ToString(),
                            Estado = Convert.ToBoolean(dr["Estado"])
                        };
                    }
                }
            }
            conexion.CerrarBd();
            return producto;
        }

        public List<Producto> ObtenerProductosVenta()
        {
            List<Producto> lista = new List<Producto>();
            SqlConnection con = conexion.AbrirBd();
            using (SqlCommand cmd = new SqlCommand("sp_ObtenerProductosVentas", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new Producto
                        {
                            Id = Convert.ToInt32(dr["Id"]),
                            Nombre = dr["Nombre"].ToString(),
                            Precio = Convert.ToDecimal(dr["Precio"]),
                            FotoUrl = dr["FotoUrl"].ToString()
                        });
                    }
                }
            }
            conexion.CerrarBd();
            return lista;
        }
    }
}
