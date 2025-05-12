using CapaDatos;
using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_RolPermisos
    {
        CD_RolPermisos RolPermisos = new CD_RolPermisos();
        public void ActualizarRolPermisos(int rolId, int formId, bool check)
        {
            RolPermisos.actualizarRolPermisos(rolId, formId, check);
        }

        public bool ObtenerEsPermitidoForm(int rolId, int formId)
        {
            CD_RolPermisos cD_RolPermisos = new CD_RolPermisos();
            try
            {
                if (rolId != 0 && formId != 0)
                {
                    return cD_RolPermisos.ObtenerEsPermitidoForm(rolId, formId);
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
