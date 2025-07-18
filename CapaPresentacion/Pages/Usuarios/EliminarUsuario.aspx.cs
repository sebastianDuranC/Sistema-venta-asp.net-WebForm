using CapaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CapaPresentacion.Pages.Usuarios
{
    public partial class EliminarUsuario : System.Web.UI.Page
    {
        UsuarioBLL usuarioBLL = new UsuarioBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cargarDatos();
            }
        }

        private void cargarDatos()
        {
            int idUsuario = Convert.ToInt32(Request.QueryString["Id"]);
            rptUsuario.DataSource = usuarioBLL.ObtenerUsuarioPorId(idUsuario);
            rptUsuario.DataBind();
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/Usuarios/Usuarios.aspx");
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                bool resultado = usuarioBLL.EliminarUsuario(Convert.ToInt32(Request.QueryString["Id"]));
                if (resultado)
                {
                    ShowToast("¡Eliminado!", "Usuario eliminado correctamente.", "success", "Usuarios.aspx");
                }
                else
                {
                    ShowToast("Error", "No se pudo eliminar el Usuario.", "error");
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