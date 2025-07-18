using CapaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CapaPresentacion.Pages.InsumoCategoria
{
    public partial class RegisrtarInsumoCategoria : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/InsumoCategoria/InsumoCategorias.aspx");    
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            InsumoCategoriaBLL insumoCategoriaBLL = new InsumoCategoriaBLL();
            CapaEntidades.InsumoCategoria insumoCategoria = new CapaEntidades.InsumoCategoria
            {
                Nombre = txtNombre.Text.Trim()
            };

            try
            {
                bool resultado = insumoCategoriaBLL.RegistrarInsumoCategoria(insumoCategoria); 
                if (resultado)
                {
                    ShowToast("Registro exitoso", "success");
                    txtNombre.Text = string.Empty;
                }
                else
                {
                    ShowToast("Error al registrar", "error");
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