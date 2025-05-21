using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CD_Ventas
    {
        private string ObtenerCadenaConexion()
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["conexionSql"].ConnectionString;
        }

        public DataTable ObtenerVentas()
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection conexion = new SqlConnection(ObtenerCadenaConexion()))
            {
                try
                {
                    conexion.Open();
                    using (SqlCommand comando = new SqlCommand("sp_ObtenerVentas", conexion))
                    {
                        comando.CommandType = CommandType.StoredProcedure;
                        using (SqlDataAdapter adaptador = new SqlDataAdapter(comando))
                        {
                            adaptador.Fill(dataTable);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Aquí podrías implementar un sistema de logging
                    throw new Exception("Error al obtener las ventas: " + ex.Message);
                }
            }

            return dataTable;
        }

        public bool RegistrarVentas(int enLocal, int cliente, int usuario, int metodoPago, DataTable detalles)
        {
            using (SqlConnection conexion = new SqlConnection(ObtenerCadenaConexion()))
            {
                using (SqlCommand comando = new SqlCommand("sp_RegistrarVenta", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;

                    // Parámetros escalares
                    comando.Parameters.AddWithValue("@EnLocal", enLocal);
                    comando.Parameters.AddWithValue("@ClienteId", cliente);
                    comando.Parameters.AddWithValue("@UsuarioId", usuario);
                    comando.Parameters.AddWithValue("@MetodoPagoId", metodoPago);

                    // Parámetro tipo tabla
                    SqlParameter tvpDetalle = comando.Parameters.AddWithValue("@Detalles", detalles);
                    tvpDetalle.SqlDbType = SqlDbType.Structured;
                    tvpDetalle.TypeName = "DetalleVentaType";

                    conexion.Open();
                    int filasAfectadas = comando.ExecuteNonQuery();

                    return filasAfectadas > 0;
                }
            }
        }

    }
}