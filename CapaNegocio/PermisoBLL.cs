using CapaDatos;
using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class PermisoBLL
    {
        PermisoDAL form = new PermisoDAL();

        public bool RegistrarForm(string nombreForm, string formRuta)
        {
            try
            {
                // Validar que los parámetros no sean nulos o vacíos
                if (!string.IsNullOrEmpty(nombreForm) && !string.IsNullOrEmpty(formRuta))
                {
                    return form.RegistrarForm(nombreForm, formRuta);
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

        public List<Permisos> obtenerForm()
        {
            try
            {
                return form.ObtenerForm();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Permisos ObtenerFormPorId(int id)
        {
            try
            {
                // Validar que el ID sea mayor a 0
                if (id > 0)
                {
                    return form.ObtenerFormPorId(id);
                }
                else
                {
                    return new Permisos(); // O un valor que indique error
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int ObtenerFormularioIdNombre(string currentNombreForm)
        {
            try
            {
                // Validar que el nombre del formulario no sea nulo o vacío
                if (!string.IsNullOrEmpty(currentNombreForm))
                {
                    return form.ObtenerFormularioIdNombre(currentNombreForm);
                }
                else
                {
                    return -1; // O un valor que indique error
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool EditarForm(Permisos permisos)
        {
            try
            {
                // Validar que el ID sea mayor a 0
                if (permisos.Id > 0)
                {
                    return form.EditarPermiso(permisos);
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

        public bool EliminarPermiso(int id)
        {
            try
            {
                // Validar que el ID sea mayor a 0
                if (id > 0)
                {
                    return form.EliminarPermiso(id);
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
