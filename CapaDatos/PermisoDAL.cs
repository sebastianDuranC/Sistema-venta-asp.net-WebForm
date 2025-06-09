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
    public class PermisoDAL
    {
        /// <summary>
        /// Obtiene la cadena de conexión desde el archivo de configuración (App.config o Web.config).
        /// </summary>
        private string ObtenerCadenaConexion()
        {
            // Asegúrate de que el nombre "conexionSql" coincida con el de tu archivo de configuración.
            return ConfigurationManager.ConnectionStrings["conexionSql"].ConnectionString;
        }

        /// <summary>
        /// Registra un nuevo formulario de permiso en la base de datos.
        /// </summary>
        public bool RegistrarForm(string nombreForm, string formRuta)
        {
            using (SqlConnection conexion = new SqlConnection(ObtenerCadenaConexion()))
            {
                conexion.Open();
                using (SqlCommand comando = new SqlCommand("sp_RegistrarPermisos", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@FormNombre", nombreForm);
                    comando.Parameters.AddWithValue("@FormRuta", formRuta);

                    // ExecuteNonQuery devuelve el número de filas afectadas.
                    // Si es mayor a 0, la operación fue exitosa.
                    return comando.ExecuteNonQuery() > 0;
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de todos los permisos de formulario activos.
        /// </summary>
        public List<Permisos> ObtenerForm()
        {
            List<Permisos> listaPermisos = new List<Permisos>();
            using (SqlConnection conexion = new SqlConnection(ObtenerCadenaConexion()))
            {
                conexion.Open();
                using (SqlCommand comando = new SqlCommand("sp_ObtenerPermisos", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader reader = comando.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            listaPermisos.Add(new Permisos
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                FormNombre = reader["FormNombre"].ToString(),
                                FormRuta = reader["FormRuta"].ToString(),
                                Estado = Convert.ToBoolean(reader["Estado"])
                            });
                        }
                    }
                }
            }
            return listaPermisos;
        }

        public Permisos ObtenerFormPorId(int id)
        {
            Permisos permiso = null;
            using (SqlConnection conexion = new SqlConnection(ObtenerCadenaConexion()))
            {
                conexion.Open();
                using (SqlCommand comando = new SqlCommand("sp_ObtenerPermisoPorId", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@Id", id);

                    using (SqlDataReader reader = comando.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            permiso = new Permisos
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                FormNombre = reader["FormNombre"].ToString(),
                                FormRuta = reader["FormRuta"].ToString(),
                                Estado = Convert.ToBoolean(reader["Estado"])
                            };
                        }
                    }
                }
            }
            return permiso;
        }

        /// <summary>
        /// Obtiene el ID de un formulario a partir de su nombre.
        /// </summary>
        public int ObtenerFormularioIdNombre(string currentNombreForm)
        {
            int formularioId = -1; // Valor por defecto si no se encuentra.
            using (SqlConnection conexion = new SqlConnection(ObtenerCadenaConexion()))
            {
                conexion.Open();
                using (SqlCommand comando = new SqlCommand("sp_ObtenerPermisosIdPorNombre", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@FormRuta", currentNombreForm);

                    object resultado = comando.ExecuteScalar();
                    if (resultado != null && resultado != DBNull.Value)
                    {
                        formularioId = Convert.ToInt32(resultado);
                    }
                }
            }
            return formularioId;
        }

        /// <summary>
        /// Edita los datos de un permiso existente.
        /// </summary>
        public bool EditarPermiso(Permisos permiso)
        {
            using (SqlConnection conexion = new SqlConnection(ObtenerCadenaConexion()))
            {
                conexion.Open();
                using (SqlCommand comando = new SqlCommand("sp_EditarPermisos", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@Id", permiso.Id);
                    comando.Parameters.AddWithValue("@FormNombre", permiso.FormNombre);

                    return comando.ExecuteNonQuery() > 0;
                }
            }
        }
        /// <summary>
        /// Elimina un permiso por su Id utilizando una consulta SQL directa (no SP).
        /// </summary>
        public bool EliminarPermiso(int id)
        {
            using (SqlConnection conexion = new SqlConnection(ObtenerCadenaConexion()))
            {
                conexion.Open();
                string query = "UPDATE Permisos SET Estado = 0 WHERE Id = @Id";
                using (SqlCommand comando = new SqlCommand(query, conexion))
                {
                    comando.Parameters.AddWithValue("@Id", id);
                    return comando.ExecuteNonQuery() > 0;
                }
            }
        }

    }
}
