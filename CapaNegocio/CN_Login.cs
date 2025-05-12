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
        private CD_Login datos = new CD_Login();

        public bool UsuarioDatos(string usuario, string password)
        {
            try
            {
                if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(password))
                {
                    return false;
                }
                return datos.LoginUsuario(usuario, password);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool TienePermiso(string currentUsuario, string currentNombreForm)
        {
            try
            {
                return datos.TienePermiso(currentUsuario, currentNombreForm);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
