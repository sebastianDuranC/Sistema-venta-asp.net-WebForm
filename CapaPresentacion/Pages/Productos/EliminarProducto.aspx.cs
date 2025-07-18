using CapaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CapaPresentacion.Pages.Productos
{
    public partial class EliminarProducto1 : System.Web.UI.Page
    {
        ProductoBLL ProductoBLL = new ProductoBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int id = Convert.ToInt32(Request.QueryString["Id"]);
                cardarDatos(id);
            }
        }

        private void cardarDatos(int id)
        {
            rptProducto.DataSource = ProductoBLL.ObtenerProductoPorId(id);
            rptProducto.DataBind();
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/Productos/Productos.aspx");
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                bool resultado = ProductoBLL.EliminarProducto(Convert.ToInt32(Request.QueryString["Id"]));
                if (resultado)
                {
                    ShowToast("¡Eliminado!", "Producto eliminado correctamente.", "success", "Productos.aspx");
                }
                else
                {
                    ShowToast("Error", "No se pudo eliminar el Producto.", "error");
                }
            }
            catch (Exception ex)
            {
                ShowToast("Error Inesperado", ex.Message, "warning");
            }
        }

        private void ShowToast(string titulo, string mensaje, string icono, string redirectUrl = "")
        {
            // Usamos $ para la interpolación de cadenas y @ para un bloque de cadena literal.
            string script = $@"
        Swal.fire({{
            title: '{titulo.Replace("'", "\\'")}',
            text: '{mensaje.Replace("'", "\\'")}',
            icon: '{icono}',
            showConfirmButton: false,
            timer: 2500,
            timerProgressBar: true
        }}).then(() => {{
            if ('{redirectUrl}') {{
                window.location.href = '{redirectUrl}';
            }}
        }});";

            // Registra el script para que se ejecute en el cliente.
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowSweetAlert", script, true);
        }
    }
}