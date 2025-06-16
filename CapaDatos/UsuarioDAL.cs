using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;

namespace CapaDatos
{
    public class UsuarioDAL
    {
        /// <summary>
        /// Obtiene la cadena de conexión desde el archivo de configuración (App.config o Web.config).
        /// </summary>
        private string ObtenerCadenaConexion()
        {
            // Reemplaza "conexionSql" con el nombre exacto de tu connectionString en el archivo de configuración.
            return ConfigurationManager.ConnectionStrings["conexionSql"].ConnectionString;
        }

       
        public List<Usuario> ObtenerUsuarios()
        {
            List<Usuario> listaUsuarios = new List<Usuario>();
            using (SqlConnection conexion = new SqlConnection(ObtenerCadenaConexion()))
            {
                try
                {
                    conexion.Open();
                    using (SqlCommand comando = new SqlCommand("sp_ObtenerUsuarios", conexion))
                    {
                        comando.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader lector = comando.ExecuteReader())
                        {
                            while (lector.Read())
                            {
                                listaUsuarios.Add(new Usuario
                                {
                                    Id = Convert.ToInt32(lector["Id"]),
                                    Nombre = lector["Nombre"].ToString(),
                                    Contra = lector["Contra"].ToString(),
                                    Estado = Convert.ToBoolean(lector["Estado"]),
                                    NegocioId = Convert.ToInt32(lector["NegocioId"]),
                                    Negocio = new Negocio
                                    {
                                        Id = Convert.ToInt32(lector["NegocioId"]),
                                        Nombre = lector["NombreNegocio"].ToString()
                                    },
                                    RolId = Convert.ToInt32(lector["RolId"]),
                                    Rol = new Rol
                                    {
                                        Id = Convert.ToInt32(lector["RolId"]),
                                        Nombre = lector["NombreRol"].ToString()
                                    }
                                });
                            }
                        }
                    }
                }
                catch (SqlException sqlEx)
                {
                    // Puedes registrar el error en un log (log4net, Serilog, etc.)
                    throw new Exception("Error SQL al obtener los usuarios.", sqlEx);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error inesperado al obtener los usuarios.", ex);
                }
            }
            return listaUsuarios;
        }

        /// <summary>
        /// Valida las credenciales de un usuario contra la base de datos.
        /// </summary>
        public List<Usuario> ValidarCredencialesUsuario(string nombreUsuario)
        {
            List<Usuario> usuarios = new List<Usuario>();
            using (SqlConnection conexion = new SqlConnection(ObtenerCadenaConexion()))
            {
                try
                {
                    conexion.Open();
                    using (SqlCommand comando = new SqlCommand("sp_ObtenerUsuarioPorNombre", conexion))
                    {
                        comando.Parameters.AddWithValue("@Nombre", nombreUsuario);
                        comando.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader lector = comando.ExecuteReader())
                        {
                            while (lector.Read())
                            {
                                usuarios.Add(new Usuario
                                {
                                    Id = Convert.ToInt32(lector["Id"]),
                                    Nombre = lector["Nombre"].ToString(),
                                    Contra = lector["Contra"].ToString(),
                                    NegocioId = Convert.ToInt32(lector["NegocioId"]),
                                    RolId = Convert.ToInt32(lector["RolId"]),
                                    Estado = Convert.ToBoolean(lector["Estado"])
                                });
                            }
                        }
                    }
                }
                catch (SqlException sqlEx)
                {
                    // Puedes registrar el error en un log (log4net, Serilog, etc.)
                    throw new Exception($"Error SQL al obtener los datos del usuario '{nombreUsuario}'.", sqlEx);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error inesperado al obtener los datos del usuario '{nombreUsuario}'.", ex);
                }
            }
            return usuarios;
        }

        /// <summary>
        /// Obtiene el ID del rol de un usuario a partir de su nombre de usuario.
        /// </summary>
        public int ObtenerRolIdNombre(string nombreUsuario)
        {
            int rolId = -1; // Valor por defecto si no se encuentra el rol.

            using (SqlConnection conexion = new SqlConnection(ObtenerCadenaConexion()))
            {
                try
                {
                    conexion.Open();
                    using (SqlCommand comando = new SqlCommand("sp_ObtenerRolIdNombre", conexion))
                    {
                        comando.CommandType = CommandType.StoredProcedure;
                        comando.Parameters.AddWithValue("@Nombre", nombreUsuario);

                        object resultado = comando.ExecuteScalar();

                        if (resultado != null && resultado != DBNull.Value)
                        {
                            rolId = Convert.ToInt32(resultado);
                        }
                    }
                }
                catch (SqlException sqlEx)
                {
                    throw new Exception($"Error SQL al obtener el rol del usuario '{nombreUsuario}'.", sqlEx);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error inesperado al obtener el rol del usuario '{nombreUsuario}'.", ex);
                }
            }
            return rolId;
        }

        public bool RegistrarUsuario(Usuario usuario)
        {
            using (SqlConnection conexion = new SqlConnection(ObtenerCadenaConexion()))
            {
                try
                {
                    conexion.Open();
                    using (SqlCommand comando = new SqlCommand("sp_RegistrarUsuario", conexion))
                    {
                        comando.CommandType = CommandType.StoredProcedure;
                        comando.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                        comando.Parameters.AddWithValue("@Contra", usuario.Contra);
                        comando.Parameters.AddWithValue("@NegocioId", usuario.NegocioId);
                        comando.Parameters.AddWithValue("@RolId", usuario.RolId);
                        int filasAfectadas = comando.ExecuteNonQuery();
                        return filasAfectadas > 0; // Retorna true si se insertó al menos un registro.
                    }
                }
                catch (SqlException sqlEx)
                {
                    throw new Exception("Error SQL al registrar el usuario.", sqlEx);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error inesperado al registrar el usuario.", ex);
                }
            }
        }

        public bool ActualizarUsuario(Usuario usuario)
        {
            using (SqlConnection conexion = new SqlConnection(ObtenerCadenaConexion()))
            {
                try
                {
                    conexion.Open();
                    using (SqlCommand comando = new SqlCommand("sp_ActualizarUsuario", conexion))
                    {
                        comando.CommandType = CommandType.StoredProcedure;
                        comando.Parameters.AddWithValue("@Id", usuario.Id);
                        comando.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                        comando.Parameters.AddWithValue("@Contra", usuario.Contra);
                        comando.Parameters.AddWithValue("@NegocioId", usuario.NegocioId);
                        comando.Parameters.AddWithValue("@RolId", usuario.RolId);
                        int filasAfectadas = comando.ExecuteNonQuery();
                        return filasAfectadas > 0; // Retorna true si se actualizó al menos un registro.
                    }
                }
                catch (SqlException sqlEx)
                {
                    throw new Exception("Error SQL al actualizar el usuario.", sqlEx);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error inesperado al actualizar el usuario.", ex);
                }
            }
        }

        public bool EditarUsuario(Usuario usuario)
        {
            using (SqlConnection conexion = new SqlConnection(ObtenerCadenaConexion()))
            {
                conexion.Open();
                using (SqlCommand comando = new SqlCommand("sp_EditarUsuario", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@Id", usuario.Id);
                    comando.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                    comando.Parameters.AddWithValue("@Contra", usuario.Contra);
                    comando.Parameters.AddWithValue("@NegocioId", usuario.NegocioId);
                    comando.Parameters.AddWithValue("@RolId", usuario.RolId);
                    return comando.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool EliminarUsuario(int idUsuario)
        {
            using (SqlConnection conexion = new SqlConnection(ObtenerCadenaConexion()))
            {
                try
                {
                    conexion.Open();
                    using (SqlCommand comando = new SqlCommand("sp_EliminarUsuario", conexion))
                    {
                        comando.CommandType = CommandType.StoredProcedure;
                        comando.Parameters.AddWithValue("@Id", idUsuario);
                        int filasAfectadas = comando.ExecuteNonQuery();
                        return filasAfectadas > 0;
                    }
                }
                catch (SqlException sqlEx)
                {
                    throw new Exception("Error SQL al eliminar el usuario.", sqlEx);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error inesperado al eliminar el usuario.", ex);
                }
            }
        }
        public DataTable ObtenerUsarioPorId(int idUsuario)
        {
            DataTable dtUsuario = new DataTable();
            using (SqlConnection conexion = new SqlConnection(ObtenerCadenaConexion()))
            {
                try
                {
                    conexion.Open();
                    using (SqlCommand comando = new SqlCommand("sp_ObtenerUsuarioPorId", conexion))
                    {
                        comando.Parameters.AddWithValue("@Id", idUsuario);
                        comando.CommandType = CommandType.StoredProcedure;
                        using (SqlDataAdapter adaptador = new SqlDataAdapter(comando))
                        {
                            adaptador.Fill(dtUsuario);
                        }
                    }
                }
                catch (SqlException sqlEx)
                {
                    throw new Exception("Error SQL al obtener el usuario por ID.", sqlEx);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error inesperado al obtener el usuario por ID.", ex);
                }
            }
            return dtUsuario;
        }
    }
}
