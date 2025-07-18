using CapaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CapaPresentacion.Pages.InsumoCategoria
{
    public partial class EliminarInsumoCategoria : System.Web.UI.Page
    {
        InsumoCategoriaBLL insumoCategoriaBLL = new InsumoCategoriaBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int id = Convert.ToInt32(Request.QueryString["Id"]);
                cargarDatos(id);
            }
        }

        private void cargarDatos(int id)
        {
            CapaEntidades.InsumoCategoria insumoCategoria = insumoCategoriaBLL.obtenerInsumoCategoriaPorId(id);
            rptInsumoCategorias.DataSource = new List<CapaEntidades.InsumoCategoria> { insumoCategoria };
            rptInsumoCategorias.DataBind();
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/InsumoCategoria/InsumoCategorias.aspx");
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(Request.QueryString["Id"]);
                bool resultado = insumoCategoriaBLL.EliminarInsumoCategoria(id);
                if (resultado)
                {
                    ShowToast("¡Eliminado!", "Categoria eliminado correctamente.", "success", "InsumoCategorias.aspx");
                }
                else
                {
                    ShowToast("Error", "No se pudo eliminar la categoria.", "error");
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