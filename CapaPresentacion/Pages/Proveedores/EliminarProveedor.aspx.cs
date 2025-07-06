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
    public partial class EliminarProveedor : System.Web.UI.Page
    {
        ProveedorBLL ProveedorBLL = new ProveedorBLL();
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
            Proveedor proveeodor = ProveedorBLL.ObtenerProveedorPorId(id);
            rptProveeodr.DataSource = new List<Proveedor> { proveeodor };
            rptProveeodr.DataBind();
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/Proveedores/Proveedores.aspx");
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                // Obtenemos el ID de la URL. Es seguro porque ya lo validamos en Page_Load.
                int id = Convert.ToInt32(Request.QueryString["Id"]);

                // Llamamos a la capa de negocio para eliminar.
                bool exito = ProveedorBLL.EliminarProveedor(id);

                if (exito)
                {
                    // Si se eliminó, mostramos un mensaje de éxito y redirigimos.
                    ShowToast("¡Eliminado!", "Proveedor eliminado correctamente.", "success", "Proveedores.aspx");
                }
                else
                {
                    ShowToast("Error", "No se pudo eliminar el proveedor.", "error");
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores inesperados.
                ShowToast("Error Inesperado", ex.Message, "error");
            }
        }

        private void ShowToast(string titulo, string mensaje, string icono, string redirectUrl = "")
        {
            string script = $@"
                Swal.fire({{
                    title: '{titulo.Replace("'", "\\'")}',
                    text: '{mensaje.Replace("'", "\\'")}',
                    icon: '{icono}',
                    showConfirmButton: false,
                    timer: 2000
                }}).then(() => {{
                    if ('{redirectUrl}') {{
                        window.location.href = '{redirectUrl}';
                    }}
                }});";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowToastScript", script, true);
        }
    }
}