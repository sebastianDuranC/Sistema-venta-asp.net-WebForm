using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;

namespace CapaDatos
{
    public class CD_Login
    {
        private CD_Conexion conexion = new CD_Conexion();
        SqlDataReader leer;
        SqlCommand comandoQuery = new SqlCommand(); //Obj para utilizar las sentencias sql (insert, update, delete, select, procedure stored)

        public bool LoginUsuario(string usuario, string contrasena)
        {
            bool existe = false;
            try
            {
                comandoQuery.Connection = conexion.AbrirBd();
                comandoQuery.CommandText = "sp_ObtenerDatosUsuario";
                comandoQuery.CommandType = CommandType.StoredProcedure;
                comandoQuery.Parameters.Clear();
                comandoQuery.Parameters.AddWithValue("@Nombre", usuario);
                comandoQuery.Parameters.AddWithValue("@Contra", contrasena);

                leer = comandoQuery.ExecuteReader();
                if (leer.Read())
                {
                    existe = true;
                }
                leer.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al intentar iniciar sesión: " + ex.Message);
            }
            finally
            {
                conexion.CerrarBd();
            }
            return existe;
        }

        // dE LA TABLA RolPermisosMapping
        public bool TienePermiso(string currentUsuario, string currentNombreForm)
        {
            int rolId = ObtenerRolIdNombre(currentUsuario);
            int formularioId = ObtenerFormularioIdNombre(currentNombreForm); //ERROR, OBTENEMOS EL ID DEL FORMULARIO -1 Y POR ESONOS LLEVA A ACCESO DENEGADO/---COLSUIONAR
            if (rolId != -1 && formularioId != -1)
            {
                try
                {
                    comandoQuery.Connection = conexion.AbrirBd();
                    comandoQuery.CommandText = "sp_ObtenerNumeroPermisos";
                    comandoQuery.CommandType = CommandType.StoredProcedure;
                    comandoQuery.Parameters.Clear();
                    // Comparamos si el rolID tiene permisos en el formularioID
                    comandoQuery.Parameters.AddWithValue("@RolId", rolId);
                    comandoQuery.Parameters.AddWithValue("@FormularioId", formularioId);
                    leer = comandoQuery.ExecuteReader();
                    int tienePermiso = 0;
                    if (leer.Read())
                    {
                        tienePermiso = Convert.ToInt32(comandoQuery.ExecuteScalar()); //Si es 1 el rol tiene permisos al formulario si es 0 entonces no
                    }
                    leer.Close();
                    return tienePermiso > 0;
                }
                catch (Exception)
                {

                    throw;
                }
                finally
                {
                    conexion.CerrarBd();
                }
            }
            return false;
        }

        // dE LA TABLA Form
        private int ObtenerFormularioIdNombre(string currentNombreForm)
        {
            try
            {
                comandoQuery.Connection = conexion.AbrirBd();
                comandoQuery.CommandText = "sp_ObtenerFormularioIdNombre";
                comandoQuery.CommandType = CommandType.StoredProcedure;
                comandoQuery.Parameters.Clear();

                comandoQuery.Parameters.AddWithValue("@FormNombre", currentNombreForm);
                object resultado = comandoQuery.ExecuteScalar();
                //VALIDAMOS QUE NOSEA NULL
                if (resultado != null && resultado !=DBNull.Value)
                {
                    return Convert.ToInt32(resultado);
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conexion.CerrarBd();
            }
            return -1; // Si no se encuentra el formulario, devolvemos -1
        }

        //De la tabla Usuario
        private int ObtenerRolIdNombre(string currentUsuario)
        {
            try
            {
                comandoQuery.Connection = conexion.AbrirBd();
                comandoQuery.CommandText = "sp_ObtenerRolIdNombre";
                comandoQuery.CommandType = CommandType.StoredProcedure;
                comandoQuery.Parameters.Clear();

                comandoQuery.Parameters.AddWithValue("@Nombre", currentUsuario);
                object resultado = comandoQuery.ExecuteScalar();
                //VALIDAMOS QUE NOSEA NULL
                if (resultado != null && resultado != DBNull.Value)
                {
                    return Convert.ToInt32(resultado);
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conexion.CerrarBd();
            }
            return -1;
        }
    }
}
