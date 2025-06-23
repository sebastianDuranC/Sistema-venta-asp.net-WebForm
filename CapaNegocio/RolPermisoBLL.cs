using CapaDatos;
using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CapaNegocio
{
    public class RolPermisoBLL
    {
        RolPermisoDAL RolPermisos = new RolPermisoDAL();
        public void ActualizarRolPermisos(int rolId, int formId, bool check)
        {
            RolPermisoDAL cD_RolPermisos = new RolPermisoDAL();
            try
            {
                if (rolId != 0 && formId != 0)
                {
                    cD_RolPermisos.ActualizarRolPermisos(rolId, formId, check);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool ObtenerEsPermitidoForm(int rolId, int formId)
        {
            RolPermisoDAL cD_RolPermisos = new RolPermisoDAL();
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

        public List<string> ObtenerRutasPermitidasPorRol(int rolId)
        {
            RolPermisoDAL cD_RolPermisos = new RolPermisoDAL();
            try
            {
                return cD_RolPermisos.ObtenerRutasPermitidasPorRol(rolId);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
