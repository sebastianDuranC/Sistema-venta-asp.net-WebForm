using CapaDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class UsuarioBLL
    {
        private UsuarioDAL usuarioDal = new UsuarioDAL();

        public bool ValidarCredencialesUsuario(string usuario, string password)
        {
            try
            {
                if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(password))
                {
                    return false;
                }
                return usuarioDal.ValidarCredencialesUsuario(usuario, password);
            }
            catch (Exception ex)
            {
                throw new Exception ("Error al intentar iniciar sesión: " + ex);
            }
        }

        public bool UsuarioTienePermisoForm(int currentUsuario, int currentNombreForm)
        {
            RolPermisoDAL cD_RolPermisos = new RolPermisoDAL(); 
            try
            {
                return cD_RolPermisos.UsuarioTienePermisoForm(currentUsuario, currentNombreForm);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al intentar saber el permiso al form: " + ex);
            }
        }

        public int ObtenerRolIdNombre(string currentUsuario)
        {
            try
            {
                return usuarioDal.ObtenerRolIdNombre(currentUsuario);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al intentar obtener el rol del usuario: " + ex);
            }
        }
    }
}
