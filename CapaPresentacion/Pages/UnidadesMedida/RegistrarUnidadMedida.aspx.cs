using CapaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CapaPresentacion.Pages.UnidadesMedida
{
    public partial class RegistrarUnidadMedida : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/UnidadesMedida/UnidadesMedida.aspx");
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            UnidadMedidaBLL unidadMedidaBLL = new UnidadMedidaBLL();
            CapaEntidades.UnidadesMedida unidadesMedida = new CapaEntidades.UnidadesMedida
            {
                Nombre = txtNombre.Text.Trim(),
                Abreviatura = txtAbreviatura.Text.Trim()
            };

            try
            {
                bool resultado = unidadMedidaBLL.RegistrarUnidadMedida(unidadesMedida);
                if (resultado)
                {
                    ShowToast("Registrado exitosamente", "success", "UnidadesMedida.aspx");
                }
                else
                {
                    ShowToast("Error al registrar la unidad de medida", "error");
                }
            }
            catch (Exception ex)
            {
                ShowToast(ex.Message, "warning");
            }
        }

        private void ShowToast(string titulo, string icono)
        {
            // Escapamos las comillas simples para evitar errores de JavaScript
            string safeTitle = titulo.Replace("'", "\\'");
            string script = $"Swal.fire({{ " +
                $"position: 'top-end'," +
                $" icon: '{icono}'," +
                $" title: '{safeTitle}'," +
                $" showConfirmButton: false," +
                $" timer: 2500," +
                $" toast: true}});";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowToastScript", script, true);
        }

        private void ShowToast(string titulo, string icono, string redirectUrl)
        {
            // 1. Prepara el objeto de configuración para SweetAlert
            string swalConfig = $@"{{
                position: 'top-end',
                icon: '{icono}',
                title: '{titulo.Replace("'", "\\'")}',
                showConfirmButton: false,
                timer: 2000,
                toast: true
            }}";

            // 2. Llama a Swal.fire() y LUEGO, usando .then(), ejecuta la redirección.
            string script = $"Swal.fire({swalConfig}).then(() => {{ window.location.href = '{redirectUrl}'; }});";

            ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowToastScript", script, true);
        }
    }
}