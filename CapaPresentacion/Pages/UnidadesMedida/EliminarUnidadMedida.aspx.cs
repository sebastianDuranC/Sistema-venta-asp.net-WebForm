using CapaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CapaPresentacion.Pages.UnidadesMedida
{
    public partial class EliminarUnidadMedida : System.Web.UI.Page
    {
        UnidadMedidaBLL UnidadMedidaBLL = new UnidadMedidaBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int id = Convert.ToInt32(Request.QueryString["Id"]);
                cargarDato(id);
            }
        }

        private void cargarDato(int id)
        {
            CapaEntidades.UnidadesMedida unidadesMedida = UnidadMedidaBLL.ObtenerUnidadMedidaPorId(id);
            rptUnidadMedidad.DataSource = new List<CapaEntidades.UnidadesMedida> { unidadesMedida };
            rptUnidadMedidad.DataBind();
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/UnidadesMedida/UnidadesMedida.aspx");
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

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                // Obtenemos el ID de la URL. Es seguro porque ya lo validamos en Page_Load.
                int id = Convert.ToInt32(Request.QueryString["Id"]);
                bool resultado = UnidadMedidaBLL.EliminarUnidadMedida(id);

                if (resultado)
                {
                    // Si la eliminación fue exitosa, mostramos un mensaje de éxito.
                    ShowToast("Éxito", "Unidad de medida eliminada correctamente.", "success", "UnidadesMedida.aspx");
                }
                else
                {
                    // Si no se pudo eliminar, mostramos un mensaje de error.
                    ShowToast("Error", "No se pudo eliminar la unidad de medida.", "error");
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores inesperados.
                ShowToast("Error Inesperado", ex.Message, "error");
            }
        }
    }
}