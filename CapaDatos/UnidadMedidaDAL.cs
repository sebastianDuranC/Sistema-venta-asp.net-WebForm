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
    public class UnidadMedidaDAL
    {
        /// <summary>
        /// Obtiene la cadena de conexión desde el archivo de configuración (Web.config).
        /// </summary>
        private string ObtenerCadenaConexion()
        {
            return ConfigurationManager.ConnectionStrings["conexionSql"].ConnectionString;
        }

        public List<UnidadesMedida> ObtenerUnidadesMedida()
        {
            List<UnidadesMedida> lista = new List<UnidadesMedida>();

            using (SqlConnection conexion = new SqlConnection(ObtenerCadenaConexion()))
            {
                try
                {
                    conexion.Open();
                    using (SqlCommand comando = new SqlCommand("sp_ObtenerUnidadesMedida", conexion))
                    {
                        comando.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader lector = comando.ExecuteReader())
                        {
                            while (lector.Read())
                            {
                                // Mapea los datos del SqlDataReader a un objeto de la entidad
                                lista.Add(new UnidadesMedida
                                {
                                    Id = Convert.ToInt32(lector["Id"]),
                                    Nombre = lector["Nombre"].ToString(),
                                    Abreviatura = lector["Abreviatura"].ToString(),
                                });
                            }
                        }
                    }
                }
                catch (SqlException sqlEx)
                {
                    // Manejo de errores específicos de SQL Server
                    throw new Exception($"Error SQL al obtener todos los registros de {typeof(UnidadesMedida).Name}.", sqlEx);
                }
                catch (Exception ex)
                {
                    // Manejo de otros errores inesperados
                    throw new Exception($"Error inesperado al obtener todos los registros de {typeof(UnidadesMedida).Name}.", ex);
                }
            }
            return lista;
        }
    }
}
