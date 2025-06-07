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
    public class UsuarioDAL
    {
        private CD_Conexion conexion = new CD_Conexion();
        SqlDataReader leer;
        SqlCommand comandoQuery = new SqlCommand();

        public bool ValidarCredencialesUsuario(string usuario, string contrasena)
        {
            bool existe = false;
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
            conexion.CerrarBd();
            return existe;
        }

        //De la tabla Usuario
        public int ObtenerRolIdNombre(string currentUsuario)
        {
            comandoQuery.Connection = conexion.AbrirBd();
            comandoQuery.CommandText = "sp_ObtenerRolIdNombre";
            comandoQuery.CommandType = CommandType.StoredProcedure;
            comandoQuery.Parameters.Clear();

            comandoQuery.Parameters.AddWithValue("@Nombre", currentUsuario);
            object resultado = comandoQuery.ExecuteScalar();
            if (resultado != null && resultado != DBNull.Value)
            {
                return Convert.ToInt32(resultado);
            }
            conexion.CerrarBd();
            return -1;
        }
    }
}
