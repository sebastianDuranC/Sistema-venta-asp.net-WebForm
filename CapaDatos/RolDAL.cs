using CapaEntidades;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls.WebParts;

namespace CapaDatos
{
    public class RolDAL
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
        /// Obtiene una lista de todos los roles 
        /// </summary>
        public List<Rol> obtenerRol()
        {
            List<Rol> lista = new List<Rol>();
            string cadenaConexion = ObtenerCadenaConexion();

            using (SqlConnection con = new SqlConnection(cadenaConexion))
            {
                con.Open();
                using (SqlCommand comandoQuery = new SqlCommand("sp_ObtenerRoles", con))
                {
                    comandoQuery.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader leer = comandoQuery.ExecuteReader())
                    {
                        while (leer.Read())
                        {
                            lista.Add(new Rol
                            {
                                Id = Convert.ToInt32(leer["Id"]),
                                Nombre = leer["Nombre"].ToString()
                            });
                        }
                    }
                }
            }
            return lista;
        }

        /// <summary>
        /// Obtiene un rol específico por su ID.
        /// </summary>
        public Rol ObtenerRolPorId(int id)
        {
            Rol rol = null;
            string cadenaConexion = ObtenerCadenaConexion();

            using (SqlConnection con = new SqlConnection(cadenaConexion))
            {
                con.Open();
                using (SqlCommand comandoQuery = new SqlCommand("sp_ObtenerRolPorId", con))
                {
                    comandoQuery.CommandType = CommandType.StoredProcedure;
                    comandoQuery.Parameters.AddWithValue("@Id", id);

                    using (SqlDataReader leer = comandoQuery.ExecuteReader())
                    {
                        if (leer.HasRows)
                        {
                            leer.Read();
                            rol = new Rol
                            {
                                Id = Convert.ToInt32(leer["Id"]),
                                Nombre = leer["Nombre"].ToString(),
                                Estado = Convert.ToBoolean(leer["Estado"]),
                                RolesPermisos = new List<RolPermisos>()
                            };
                        }

                        // Si encontramos un rol, procedemos a leer sus permisos
                        if (rol != null)
                        {
                            // Avanzar al segundo resultado: IDs de Permisos Asignados
                            if (leer.NextResult())
                            {
                                while (leer.Read())
                                {
                                    // Por cada ID de permiso encontrado, creamos un objeto RolPermisos
                                    // y lo agregamos a la colección del rol.
                                    rol.RolesPermisos.Add(new RolPermisos
                                    {
                                        PermisosId = (int)leer["PermisosId"]
                                        // No es necesario llenar las otras propiedades como RolId,
                                        // solo necesitamos el ID del permiso para la lógica de la UI.
                                    });
                                }
                            }
                        }
                    }
                    return rol;
                }
            }
        }

        public int RegistrarRolConPermisos(Rol nombreRol, DataTable permisosIds)
        {
            int nuevoRolId = 0;
            using (SqlConnection con = new SqlConnection(ObtenerCadenaConexion()))
            {
                using (SqlCommand cmd = new SqlCommand("sp_RegistrarRol", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Parámetro de entrada para el nombre del rol
                    cmd.Parameters.AddWithValue("@Nombre", nombreRol.Nombre);

                    // Parámetro estructurado para la lista de IDs de permisos.
                    // Se utiliza el tipo de tabla 'dbo.IdListPermisos' que creamos en SQL.
                    SqlParameter permisosParam = cmd.Parameters.AddWithValue("@PermisosIds", permisosIds);
                    permisosParam.SqlDbType = SqlDbType.Structured;
                    permisosParam.TypeName = "IdListPermisos";

                    // Parámetro de salida para obtener el ID del nuevo rol creado.
                    SqlParameter nuevoRolIdParam = new SqlParameter("@NuevoRolId", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(nuevoRolIdParam);

                    con.Open();
                    cmd.ExecuteNonQuery();

                    // Asignar el valor de salida a nuestra variable.
                    nuevoRolId = (int)nuevoRolIdParam.Value;
                }
            }
            return nuevoRolId;
        }

        /// <summary>
        /// Actualiza un rol y su lista completa de permisos.
        /// </summary>
        public bool EditarRolConPermisos(int rolId, string nombre, DataTable permisosIds)
        {
            using (SqlConnection con = new SqlConnection(ObtenerCadenaConexion()))
            {
                using (SqlCommand cmd = new SqlCommand("sp_EditarRol", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@RolId", rolId);
                    cmd.Parameters.AddWithValue("@Nombre", nombre);

                    // Asegúrate de que el nombre del tipo coincida con el de tu BD
                    SqlParameter permisosParam = cmd.Parameters.AddWithValue("@PermisosIds", permisosIds);
                    permisosParam.SqlDbType = SqlDbType.Structured;
                    permisosParam.TypeName = "IdListPermisos";

                    con.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool EliminarRol(int id)
        {
            using (SqlConnection con = new SqlConnection(ObtenerCadenaConexion()))
            {
                using (SqlCommand cmd = new SqlCommand("sp_EliminarRol", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", id);
                    con.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
    }
}
