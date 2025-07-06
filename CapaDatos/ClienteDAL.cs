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
    public class ClienteDAL
    {
        private string ObtenerCadenaConexion()
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["conexionSql"].ConnectionString;
        }
        public DataTable ListarClientes()
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(ObtenerCadenaConexion()))
            {
                using (SqlCommand cmd = new SqlCommand("sp_ObtenerClientes", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            return dt;
        }

        public DataTable ObtenerClientes()
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(ObtenerCadenaConexion()))
            {
                using (SqlCommand cmd = new SqlCommand("sp_ObtenerClientesComerciantes", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            return dt;
        }

        public bool RegistrarCliente(Cliente cliente)
        {
            using (SqlConnection con = new SqlConnection(ObtenerCadenaConexion()))
            {
                using (SqlCommand cmd = new SqlCommand("sp_RegistrarClientes", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Nombre", cliente.Nombre);
                    cmd.Parameters.AddWithValue("@NumeroLocal", cliente.NumeroLocal);
                    cmd.Parameters.AddWithValue("@Pasillo", cliente.Pasillo);
                    cmd.Parameters.AddWithValue("@EsComerciante", cliente.EsComerciante);
                    con.Open();
                    int filasAfectadas = cmd.ExecuteNonQuery();
                    return filasAfectadas > 0;
                }
            }
        }

        public Cliente ObtenerClientePorId(int id)
        {
            Cliente cliente = null;
            using (SqlConnection con = new SqlConnection(ObtenerCadenaConexion()))
            {
                using (SqlCommand cmd = new SqlCommand("sp_ObtenerClientePorId", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", id);
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            cliente = new Cliente
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Nombre = reader["Nombre"].ToString(),
                                NumeroLocal = reader["NumeroLocal"].ToString(),
                                Pasillo = reader["Pasillo"].ToString(),
                                EsComerciante = Convert.ToBoolean(reader["EsComerciante"]),
                            };
                        }
                    }
                }
            }
            return cliente;
        }

        public bool EditarCliente(Cliente cliente)
        {
            using (SqlConnection con = new SqlConnection(ObtenerCadenaConexion()))
            {
                using (SqlCommand cmd = new SqlCommand("sp_EditarCliente", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", cliente.Id);
                    cmd.Parameters.AddWithValue("@Nombre", cliente.Nombre);
                    cmd.Parameters.AddWithValue("@NumeroLocal", cliente.NumeroLocal);
                    cmd.Parameters.AddWithValue("@Pasillo", cliente.Pasillo);
                    con.Open();
                    int filasAfectadas = cmd.ExecuteNonQuery();
                    return filasAfectadas > 0;
                }
            }
        }

        public bool EliminarCliente(int id)
        {
            using (SqlConnection con = new SqlConnection(ObtenerCadenaConexion()))
            {
                using (SqlCommand cmd = new SqlCommand("sp_ElimiarCliente", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", id);
                    con.Open();
                    int filasAfectadas = cmd.ExecuteNonQuery();
                    return filasAfectadas > 0;
                }
            }
        }
    }
}
