using CapaEntidades;
using CapaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CapaPresentacion.Pages.Proveedores
{
    public partial class RegistrarProveedor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/Proveedores/Proveedores.aspx");
        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            ProveedorBLL proveedorBLL = new ProveedorBLL();
            try
            {
                Proveedor proveedor = new Proveedor
                {
                    Nombre = txtNombre.Text.Trim(),
                    Contacto = txtContacto.Text.Trim()
                };
                bool resultado = proveedorBLL.RegistrarProveedor(proveedor);
                if (resultado)
                {
                    ShowToast("Registro exitoso", "success");
                }
                else
                {
                    ShowToast("Error al registrar el proveedor", "error");
                }
                limnpiarInputs();
            }
            catch (Exception ex)
            {
                ShowToast(ex.Message, "warning");   
            }
        }

        private void limnpiarInputs()
        {
            txtNombre.Text = string.Empty;
            txtContacto.Text = string.Empty;
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