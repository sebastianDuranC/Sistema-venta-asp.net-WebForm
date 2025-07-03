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
    public class InsumoDAL
    {
        /// <summary>
        /// Obtiene la cadena de conexión desde el archivo de configuración (Web.config).
        /// </summary>
        private string ObtenerCadenaConexion()
        {
            return ConfigurationManager.ConnectionStrings["conexionSql"].ConnectionString;
        }

        public List<Insumo> ObtenerTodos()
        {
            List<Insumo> lista = new List<Insumo>();

            using (SqlConnection conexion = new SqlConnection(ObtenerCadenaConexion()))
            {
                try
                {
                    conexion.Open();
                    using (SqlCommand comando = new SqlCommand("sp_ObtenerInsumos", conexion))
                    {
                        comando.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader lector = comando.ExecuteReader())
                        {
                            while (lector.Read())
                            {
                                lista.Add(new Insumo
                                {
                                    Id = Convert.ToInt32(lector["Id"]),
                                    Nombre = lector["Nombre"].ToString(),
                                    Stock = lector["Stock"] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(lector["Stock"]),
                                    StockMinimo = lector["StockMinimo"] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(lector["StockMinimo"]),
                                    InsumoCategoriaId = Convert.ToInt32(lector["InsumoCategoriaId"]),
                                    InsumoCategoria = new InsumoCategoria
                                    {
                                        Id = Convert.ToInt32(lector["InsumoCategoriaId"]),
                                        Nombre = lector["NombreCategoria"].ToString()
                                    },
                                    ProveedorId = Convert.ToInt32(lector["ProveedorId"]),
                                    Proveedor = new Proveedor
                                    {
                                        Id = Convert.ToInt32(lector["ProveedorId"]),
                                        Nombre = lector["NombreProveedor"].ToString()
                                    },
                                    FotoUrl = lector["FotoUrl"] == DBNull.Value ? null : lector["FotoUrl"].ToString(),
                                    UnidadesMedidaId = Convert.ToInt32(lector["UnidadesMedidaId"]),
                                    UnidadesMedida = new UnidadesMedida
                                    {
                                        Id = Convert.ToInt32(lector["UnidadesMedidaId"]),
                                        Nombre = lector["NombreMedidas"].ToString()
                                    },
                                });
                            }
                        }
                    }
                }
                catch (SqlException sqlEx)
                {
                    throw new Exception($"Error SQL al obtener todos los registros de {typeof(Insumo).Name}.", sqlEx);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error inesperado al obtener todos los registros de {typeof(Insumo).Name}.", ex);
                }
            }
            return lista;
        }

        //public bool RegistrarInsumo(Insumo insumo)
        //{

        //}
    }
}
