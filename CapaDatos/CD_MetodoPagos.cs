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
    public class CD_MetodoPagos
    {
        private CD_Conexion conexion = new CD_Conexion();

        public List<MetodoPago> ObtenerMetodoVenta()
        {
            List<MetodoPago> lista = new List<MetodoPago>();
            SqlConnection con = conexion.AbrirBd();
            using (SqlCommand cmd = new SqlCommand("sp_ObtenerMetodosPagoVenta", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new MetodoPago
                        {
                            Id = Convert.ToInt32(dr["Id"]),
                            Nombre = dr["Nombre"].ToString()
                        });
                    }
                }
            }
            conexion.CerrarBd();
            return lista;
        }
    }
}
