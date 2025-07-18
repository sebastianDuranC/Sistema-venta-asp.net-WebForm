using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class MovimientoInventarioDAL
    {
        private string ObtenerCadenaConexion()
        {
            return ConfigurationManager.ConnectionStrings["conexionSql"].ConnectionString;
        }

        public List<MovimientoInventario> ObtenerMovimientoInventario()
        {
            List<MovimientoInventario> movimientos = new List<MovimientoInventario>();
            using (SqlConnection conexion = new SqlConnection(ObtenerCadenaConexion()))
            {
                conexion.Open();
                using (SqlCommand comando = new SqlCommand("sp_ObtenerMovimientoInventario", conexion))
                {
                    using (var lector = comando.ExecuteReader())
                    {
                        while (lector.Read())
                        {
                            MovimientoInventario movimiento = new MovimientoInventario
                            {
                                Id = Convert.ToInt32(lector["Id"]),
                                Fecha = Convert.ToDateTime(lector["Fecha"]),
                                InsumoId = Convert.ToInt32(lector["InsumoId"]),
                                Insumo = new Insumo
                                {
                                    Nombre = lector["NombreInsumo"].ToString(),
                                },
                                TipoMovimiento = lector["TipoMovimiento"].ToString(),
                                Cantidad = Convert.ToInt32(lector["Cantidad"]),
                                Observacion = lector["Observacion"].ToString(),
                                UsuarioId = Convert.ToInt32(lector["UsuarioId"]),
                                Usuario = new Usuario
                                {
                                    Nombre = lector["NombreUsuario"].ToString(),
                                },
                                Estado = Convert.ToBoolean(lector["Estado"])
                            };
                            movimientos.Add(movimiento);
                        }
                    }
                }
            }
            return movimientos;
        }
    }
}
