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
    public class MetodoPagoDAL
    {
        private CD_Conexion conexion = new CD_Conexion();
        /// <summary>
        /// Obtiene la cadena de conexión desde el archivo de configuración (Web.config).
        /// </summary>
        private string ObtenerCadenaConexion()
        {
            return ConfigurationManager.ConnectionStrings["conexionSql"].ConnectionString;
        }

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

        /// <summary>
        /// Inserta un nuevo registro de la entidad en la base de datos.
        /// </summary>
        public bool RegistrarMetodoPago(MetodoPago metodoPago)
        {
            using (SqlConnection conexion = new SqlConnection(ObtenerCadenaConexion()))
            {
                try
                {
                    conexion.Open();
                    using (SqlCommand comando = new SqlCommand("sp_RegistrarMetodoPago", conexion))
                    {
                        comando.CommandType = CommandType.StoredProcedure;
                        comando.Parameters.AddWithValue("@Nombre", metodoPago.Nombre);

                        return comando.ExecuteNonQuery() > 0; // Ejecuta el comando y devuelve numero de filas afectadas
                    }
                }
                catch (SqlException sqlEx)
                {
                    throw new Exception($"Error SQL al insertar {typeof(MetodoPago).Name}.", sqlEx);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error inesperado al insertar {typeof(MetodoPago).Name}.", ex);
                }
            }
        }

        public MetodoPago ObtenerMetodoPagoPorId(int id)
        {
            MetodoPago metodoPago = null;
            SqlConnection con = conexion.AbrirBd();
            using (SqlCommand cmd = new SqlCommand("sp_ObtenerMetodoPagoPorId", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        metodoPago = new MetodoPago
                        {
                            Id = Convert.ToInt32(dr["Id"]),
                            Nombre = dr["Nombre"].ToString(),
                            Estado = Convert.ToBoolean(dr["Estado"])
                        };
                    }
                }
            }
            conexion.CerrarBd();
            return metodoPago;
        }

        public bool ActualizarMetodoPago(MetodoPago metodoPago)
        {
            using (SqlConnection conexion = new SqlConnection(ObtenerCadenaConexion()))
            {
                try
                {
                    conexion.Open();
                    using (SqlCommand comando = new SqlCommand("sp_EditarMetodoPago", conexion))
                    {
                        comando.CommandType = CommandType.StoredProcedure;
                        comando.Parameters.AddWithValue("@Id", metodoPago.Id);
                        comando.Parameters.AddWithValue("@Nombre", metodoPago.Nombre);
                        return comando.ExecuteNonQuery() > 0; // Ejecuta el comando y devuelve numero de filas afectadas
                    }
                }
                catch (SqlException sqlEx)
                {
                    throw new Exception($"Error SQL al actualizar {typeof(MetodoPago).Name}.", sqlEx);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error inesperado al actualizar {typeof(MetodoPago).Name}.", ex);
                }
            }
        }

        public bool EliminarMetodoPago(int id)
        {
            using (SqlConnection conexion = new SqlConnection(ObtenerCadenaConexion()))
            {
                try
                {
                    conexion.Open();
                    using (SqlCommand comando = new SqlCommand("sp_EliminarMetodoPago", conexion))
                    {
                        comando.CommandType = CommandType.StoredProcedure;
                        comando.Parameters.AddWithValue("@Id", id);
                        return comando.ExecuteNonQuery() > 0; // Ejecuta el comando y devuelve numero de filas afectadas
                    }
                }
                catch (SqlException sqlEx)
                {
                    throw new Exception($"Error SQL al actualizar {typeof(MetodoPago).Name}.", sqlEx);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error inesperado al actualizar {typeof(MetodoPago).Name}.", ex);
                }
            }
        }
    }
}
