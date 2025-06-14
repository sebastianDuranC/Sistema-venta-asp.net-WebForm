﻿using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class VentaDAL
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

        public bool RegistrarVentas(Venta venta, DataTable detalles)
        {
            using (SqlConnection conexion = new SqlConnection(ObtenerCadenaConexion()))
            {
                using (SqlCommand comando = new SqlCommand("sp_RegistrarVenta", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;

                    // Parámetros escalares
                    comando.Parameters.AddWithValue("@EnLocal", venta.EnLocal);
                    comando.Parameters.AddWithValue("@ClienteId", venta.ClienteId);
                    comando.Parameters.AddWithValue("@UsuarioId", venta.UsuarioId);
                    comando.Parameters.AddWithValue("@MetodoPagoId", venta.MetodoPagoId);
                    comando.Parameters.AddWithValue("@MontoRecibido", venta.MontoRecibido);

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

        public DataTable ObtenerVentaId(int ventaId)
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection conexion = new SqlConnection(ObtenerCadenaConexion()))
            {
                conexion.Open();
                using (SqlCommand comando = new SqlCommand("sp_ObtenerVentaId", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@Id", ventaId);

                    using (SqlDataAdapter adaptador = new SqlDataAdapter(comando))
                    {
                        adaptador.Fill(dataTable);
                    }
                }
            }
            return dataTable;
        }

        public void EditarVenta(Venta venta, List<DetalleVenta> detalles)
        {
            using (SqlConnection conexion = new SqlConnection(ObtenerCadenaConexion()))
            using (SqlCommand comando = new SqlCommand("sp_EditarVenta", conexion))
            {
                comando.CommandType = CommandType.StoredProcedure;

                // Parámetros escalares
                comando.Parameters.AddWithValue("@VentaId", venta.Id);
                comando.Parameters.AddWithValue("@ClienteId", venta.ClienteId);
                comando.Parameters.AddWithValue("@MetodoPagoId", venta.MetodoPagoId);
                comando.Parameters.AddWithValue("@EnLocal", venta.EnLocal);
                comando.Parameters.AddWithValue("@Total", venta.Total);
                comando.Parameters.AddWithValue("@UsuarioEditaId", venta.UsuarioId);
                comando.Parameters.AddWithValue("@MontoRecibido", venta.MontoRecibido);

                // Crear DataTable para el parámetro tipo tabla
                DataTable dtDetalles = new DataTable();
                dtDetalles.Columns.Add("ProductoId", typeof(int));
                dtDetalles.Columns.Add("Cantidad", typeof(int));
                dtDetalles.Columns.Add("SubTotal", typeof(decimal));

                foreach (var d in detalles)
                {
                    dtDetalles.Rows.Add(d.ProductoId, d.Cantidad, d.SubTotal);
                }

                var paramDetalles = comando.Parameters.AddWithValue("@Detalles", dtDetalles);
                paramDetalles.SqlDbType = SqlDbType.Structured;
                paramDetalles.TypeName = "DetalleVentaTipo";

                try
                {
                    conexion.Open();
                    comando.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    // Capturar errores específicos de RAISERROR
                    if (ex.Class == 16)  // Errores de usuario
                        throw new ApplicationException(ex.Message);
                    throw;
                }
            }
        }

        public bool EliminarVenta(int ventaId)
        {
            using (SqlConnection conexion = new SqlConnection(ObtenerCadenaConexion()))
            {
                using (SqlCommand comando = new SqlCommand("sp_EliminarVenta", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@VentaId", ventaId);
                    conexion.Open();
                    try
                    {
                        int filasAfectadas = comando.ExecuteNonQuery();
                        return filasAfectadas > 0;
                    }
                    catch (SqlException ex)
                    {
                        // Capturar errores específicos de RAISERROR
                        if (ex.Class == 16)  // Errores de usuario
                            throw new ApplicationException(ex.Message);

                        throw;
                    }
                }
            }
        }

    }
}