using CapaEntidades;
using CapaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CapaPresentacion.Pages.Insumos
{
    public partial class EliminarInsumo : System.Web.UI.Page
    {
        InsumoBLL InsumoBLL = new InsumoBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int id = Request.QueryString["id"] != null ? Convert.ToInt32(Request.QueryString["id"]) : 0;
                cargarDatos(id);
            }
        }

        private void cargarDatos(int id)
        {
            Insumo insumo = InsumoBLL.ObtenerInsumoPorId(id);
            rptInsumo.DataSource = new List<Insumo> { insumo };
            rptInsumo.DataBind();
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/Insumos/Insumos.aspx");
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                bool resultado = InsumoBLL.EliminarInsumo(Convert.ToInt32(Request.QueryString["Id"]));
                if (resultado)
                {
                    ShowToast("¡Eliminado!", "Insumo eliminado correctamente.", "success", "Insumos.aspx");
                }
                else
                {
                    ShowToast("Error", "No se pudo eliminar el Insumo.", "error");
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