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
