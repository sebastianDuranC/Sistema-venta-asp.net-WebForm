using CapaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CapaPresentacion.Pages.Clientes
{
    public partial class RegistrarCliente : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/Clientes/Clientes.aspx");
        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            ClienteBLL clienteBLL = new ClienteBLL();
            CapaEntidades.Cliente nuevoCliente = new CapaEntidades.Cliente
            {
                Nombre = txtNombre.Text.Trim(),
                NumeroLocal = txtNumeroLocal.Text.Trim(),
                Pasillo = txtPasillo.Text.Trim(),
                EsComerciante = chkEsComerciante.SelectedValue == "1",
            };

            try
            {
                bool resultado = clienteBLL.RegistrarCliente(nuevoCliente);
                if (resultado)
                {
                    ShowToast("Cliente registrado correctamente", "success");
                }
                else
                {
                    ShowToast("Error al registrar el cliente", "warning");
                }
            }
            catch (Exception ex)
            {
                ShowToast(ex.Message, "error");
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