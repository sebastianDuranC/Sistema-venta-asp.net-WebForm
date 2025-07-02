using CapaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CapaPresentacion.Pages.MetodosPago
{
    public partial class RegistrarMetodoPago : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/MetodosPago/MetodosPago.aspx");
        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            MetodoPagoBLL metodoPagoBLL = new MetodoPagoBLL();
            try
            {
                string nombreMetodoPago = txtNombre.Text.Trim();
                var metodoPago = new CapaEntidades.MetodoPago
                {
                    Nombre = nombreMetodoPago
                };
                bool resultado = metodoPagoBLL.RegistrarMetodoPago(metodoPago);
                if (resultado)
                {
                    ShowToast("Método de pago registrado correctamente.", "success");
                }
                else
                {
                    ShowToast("Error al registrar el método de pago.", "error");
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
    }
}