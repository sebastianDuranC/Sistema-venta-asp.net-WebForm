using CapaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CapaPresentacion.Pages.ProductoCategoria
{
    public partial class RegistrarProductoCategoria : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/ProductoCategoria/ProductoCategorias.aspx");
        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            ProductoCategoriaBLL productoCategoriaBLL = new ProductoCategoriaBLL();
            try
            {
                var categoria = new CapaEntidades.ProductoCategoria
                {
                    Nombre = txtNombre.Text,
                };

                bool resultado = productoCategoriaBLL.RegistrarProductoCategoria(categoria);
                if (resultado)
                {
                    ShowToast("Categoría registrada correctamente.", "success");
                }
                else
                {
                    ShowToast("Error al registrar la categoría.", "error");
                }
            }
            catch (Exception ex)
            {
                ShowToast($"Error: {ex.Message}", "warning");
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