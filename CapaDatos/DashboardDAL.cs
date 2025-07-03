using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class DashboardDAL
    {
        private string ObtenerCadenaConexion()
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["conexionSql"].ConnectionString;
        }

        public decimal ObtenerVentasTotalesMes()
        {
            decimal totalVentas = 0;

            try
            {
                // Usar using para asegurar la liberación de recursos de la conexión y el comando
                using (var oconexion = new SqlConnection(ObtenerCadenaConexion()))
                using (var cmd = new SqlCommand("sp_Dashboard_VentasTotalesMes", oconexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    oconexion.Open();

                    object resultado = cmd.ExecuteScalar();
                    if (resultado != null && resultado != DBNull.Value)
                    {
                        totalVentas = Convert.ToDecimal(resultado);
                    }
                }
            }
            catch (Exception ex)
            {
                // Aquí se podría registrar el error en un log si se desea
                totalVentas = 0;
            }

            return totalVentas;
        }

        public int ObtenerProductosVendidosMes()
        {
            int totalProductos = 0;
            try
            {
                using (var oconexion = new SqlConnection(ObtenerCadenaConexion()))
                using (var cmd = new SqlCommand("sp_Dashboard_ProductosVendidosMes", oconexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    oconexion.Open();
                    object resultado = cmd.ExecuteScalar();
                    if (resultado != null && resultado != DBNull.Value)
                    {
                        totalProductos = Convert.ToInt32(resultado);
                    }
                }
            }
            catch (Exception ex)
            {
                totalProductos = 0;
            }
            return totalProductos;
        }

        public int ObtenerTotalItemsInventario()
        {
            int totalItems = 0;
            try
            {
                using (var oconexion = new SqlConnection(ObtenerCadenaConexion()))
                using (var cmd = new SqlCommand("sp_Dashboard_TotalItemsInventario", oconexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    oconexion.Open();
                    object resultado = cmd.ExecuteScalar();
                    if (resultado != null && resultado != DBNull.Value)
                    {
                        totalItems = Convert.ToInt32(resultado);
                    }
                }
            }
            catch (Exception ex)
            {
                totalItems = 0;
            }
            return totalItems;
        }

        public List<Dictionary<string, object>> ObtenerVentasSemana()
        {
            var ventas = new List<Dictionary<string, object>>();
            try
            {
                using (var oconexion = new SqlConnection(ObtenerCadenaConexion()))
                using (var cmd = new SqlCommand("sp_Dashboard_VentasSemana", oconexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            ventas.Add(new Dictionary<string, object>
                            {
                                { "Dia", Convert.ToDateTime(dr["Dia"]).ToString("dd MMM", new CultureInfo("es-ES")) },
                                { "Total", Convert.ToDecimal(dr["Total"]) }
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ventas = new List<Dictionary<string, object>>();
            }
            return ventas;
        }

        public List<Dictionary<string, object>> ObtenerTopProductosVendidos()
        {
            var productos = new List<Dictionary<string, object>>();
            try
            {
                using (var oconexion = new SqlConnection(ObtenerCadenaConexion()))
                using (var cmd = new SqlCommand("sp_Dashboard_TopProductosVendidos", oconexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            productos.Add(new Dictionary<string, object>
                            {
                                { "Producto", dr["Producto"].ToString() },
                                { "Cantidad", Convert.ToInt32(dr["CantidadVendida"]) }
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                productos = new List<Dictionary<string, object>>();
            }
            return productos;
        }
    }
}
