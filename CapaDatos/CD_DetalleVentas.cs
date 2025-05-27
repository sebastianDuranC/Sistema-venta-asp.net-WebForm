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
    public class CD_DetalleVentas
    {
        public DataTable ObtenerDetalleVenta(int ventaId)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conexionSql"].ToString()))
            {
                SqlCommand cmd = new SqlCommand("sp_ObtenerDetallesVenta", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@VentaId", ventaId);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                return dt;
            }
        }
    }
}
