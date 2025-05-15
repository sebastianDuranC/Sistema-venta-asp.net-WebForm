using CapaDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_Login
    {
        private CD_Login CD_Datos = new CD_Login();

        public bool ValidarCredencialesUsuario(string usuario, string password)
        {
            try
            {
                if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(password))
                {
                    return false;
                }
                return CD_Datos.ValidarCredencialesUsuario(usuario, password);
            }
            catch (Exception ex)
            {
                throw new Exception ("Error al intentar iniciar sesión: " + ex);
            }
        }
        public bool UsuarioTienePermisoForm(string currentUsuario, string currentNombreForm)
        {
            CD_RolPermisos cD_RolPermisos = new CD_RolPermisos(); 
            try
            {
                return cD_RolPermisos.UsuarioTienePermisoForm(currentUsuario, currentNombreForm);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al intentar saber el permiso al form: " + ex);
            }
        }
    }
}
