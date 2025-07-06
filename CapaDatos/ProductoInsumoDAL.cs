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
    public class ProductoInsumoDAL
    {
        private string ObtenerCadenaConexion()
        {
            return ConfigurationManager.ConnectionStrings["conexionSql"].ConnectionString;
        }

        public DataTable ObtenerRecetaPorProductoId(int productoId)
        {
            DataTable dt = new DataTable();

            using (SqlConnection conexion = new SqlConnection(ObtenerCadenaConexion()))
            {
                try
                {
                    conexion.Open();
                    using (SqlCommand comando = new SqlCommand("sp_ObtenerProductoInsumoPorId", conexion))
                    {
                        comando.CommandType = CommandType.StoredProcedure;
                        comando.Parameters.AddWithValue("@ProductoId", productoId);

                        using (SqlDataAdapter adapter = new SqlDataAdapter(comando))
                        {
                            adapter.Fill(dt);
                        }
                    }
                }
                catch (SqlException sqlEx)
                {
                    throw new Exception($"Error SQL al obtener los insumos del producto.", sqlEx);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error inesperado al obtener los insumos del producto.", ex);
                }
            }
            return dt;
        }
    }
}
