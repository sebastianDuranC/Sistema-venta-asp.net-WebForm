using CapaDatos;
using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_Form
    {
        CD_Form form = new CD_Form();

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

        public List<Form> obtenerForm()
        {
            try
            {
                return form.ObtenerForm();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la lista de formularios", ex);
            }
        }
    }
}
