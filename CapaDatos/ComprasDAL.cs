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
    public class ComprasDAL
    {
        /// <summary>
        /// Obtiene la cadena de conexión desde el archivo de configuración (Web.config).
        /// </summary>
        private string ObtenerCadenaConexion()
        {
            return ConfigurationManager.ConnectionStrings["conexionSql"].ConnectionString;
        }

        public List<Compra> ObtenerCompras()
        {
            List<Compra> compras = new List<Compra>();
            using (SqlConnection conexion = new SqlConnection(ObtenerCadenaConexion()))
            {
                try
                {
                    conexion.Open();
                    using (SqlCommand comando = new SqlCommand("sp_ObtenerCompras", conexion))
                    {
                        comando.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader lector = comando.ExecuteReader())
                        {
                            while (lector.Read())
                            {
                                compras.Add(new Compra
                                {
                                    Id = Convert.ToInt32(lector["Id"]),
                                    Fecha = Convert.ToDateTime(lector["Fecha"]),
                                    Total = Convert.ToDecimal(lector["Total"]),
                                    UsuarioId = Convert.ToInt32(lector["UsuarioId"]),
                                    Usuario = new Usuario
                                    {
                                        Nombre = lector["NombreUsuario"].ToString()
                                    },
                                    ProveedorId = Convert.ToInt32(lector["ProveedorId"]),
                                    Proveedor = new Proveedor
                                    {
                                        Nombre = lector["NombreProveedor"].ToString()
                                    },
                                    Estado = Convert.ToBoolean(lector["Estado"]),
                                });
                            }
                        }
                    }
                }
                catch (SqlException sql)
                {
                    throw new Exception("Error de SQL al obtener las compras: " + sql.Message, sql);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al obtener las compras: " + ex.Message, ex);
                }
            }
            return compras;
        }

        public Compra ObtenerCompraPorId(int id)
        {
            Compra compra = null;
            using (SqlConnection conexion = new SqlConnection(ObtenerCadenaConexion()))
            {
                try
                {
                    conexion.Open();
                    using (SqlCommand comando = new SqlCommand("sp_ObtenerCompraPorId", conexion))
                    {
                        comando.CommandType = CommandType.StoredProcedure;
                        comando.Parameters.AddWithValue("@Id", id);

                        using (SqlDataReader lector = comando.ExecuteReader())
                        {
                            // Leer cabecera de la compra
                            if (lector.Read())
                            {
                                compra = new Compra
                                {
                                    Id = Convert.ToInt32(lector["Id"]),
                                    Fecha = Convert.ToDateTime(lector["Fecha"]),
                                    Total = Convert.ToDecimal(lector["Total"]),
                                    UsuarioId = Convert.ToInt32(lector["UsuarioId"]),
                                    Usuario = new Usuario
                                    {
                                        Nombre = lector["NombreUsuario"].ToString()
                                    },
                                    ProveedorId = Convert.ToInt32(lector["ProveedorId"]),
                                    Proveedor = new Proveedor
                                    {
                                        Nombre = lector["NombreProveedor"].ToString()
                                    },
                                    Estado = Convert.ToBoolean(lector["Estado"]),
                                    DetallesCompra = new List<DetalleCompra>()
                                };
                            }

                            // Avanzar al segundo result set (detalles)
                            if (compra != null && lector.NextResult())
                            {
                                while (lector.Read())
                                {
                                    var detalle = new DetalleCompra
                                    {
                                        Id = Convert.ToInt32(lector["Id"]),
                                        CompraId = Convert.ToInt32(lector["CompraId"]),
                                        InsumoId = Convert.ToInt32(lector["InsumoId"]),
                                        Insumo = new Insumo
                                        {
                                            Nombre = lector["NombreInsumo"].ToString()
                                        },
                                        Cantidad = Convert.ToDecimal(lector["Cantidad"]),
                                        Costo = Convert.ToDecimal(lector["Costo"])
                                    };
                                    compra.DetallesCompra.Add(detalle);
                                }
                            }
                        }
                    }
                }
                catch (SqlException sql)
                {
                    throw new Exception("Error de SQL al obtener la compra por ID: " + sql.Message, sql);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al obtener la compra por ID: " + ex.Message, ex);
                }
            }
            return compra;
        }

        public bool RegistrarCompra(Compra compra)
        {
            using (SqlConnection conexion = new SqlConnection(ObtenerCadenaConexion()))
            {
                try
                {
                    conexion.Open();
                    using (SqlCommand comando = new SqlCommand("sp_RegistrarCompra", conexion))
                    {
                        comando.CommandType = CommandType.StoredProcedure;
                        comando.Parameters.AddWithValue("@UsuarioId", compra.UsuarioId);
                        comando.Parameters.AddWithValue("@ProveedorId", compra.ProveedorId);

                        // Crear DataTable para el parámetro tipo tabla
                        var detallesTable = new DataTable();
                        detallesTable.Columns.Add("InsumoId", typeof(int));
                        detallesTable.Columns.Add("Cantidad", typeof(decimal));
                        detallesTable.Columns.Add("Costo", typeof(decimal));

                        if (compra.DetallesCompra != null)
                        {
                            foreach (var detalle in compra.DetallesCompra)
                            {
                                detallesTable.Rows.Add(detalle.InsumoId, detalle.Cantidad, detalle.Costo);
                            }
                        }

                        var detallesParam = comando.Parameters.AddWithValue("@Detalles", detallesTable);
                        detallesParam.SqlDbType = SqlDbType.Structured;
                        detallesParam.TypeName = "DetalleCompraType";

                        int filasAfectadas = comando.ExecuteNonQuery();
                        return filasAfectadas > 0;
                    }
                }
                catch (SqlException sql)
                {
                    throw new Exception("Error de SQL al registrar la compra: " + sql.Message, sql);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al registrar la compra: " + ex.Message, ex);
                }
            }
        }
    }
}
