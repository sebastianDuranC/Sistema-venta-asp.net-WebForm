using CapaEntidades;
using CapaNegocio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CapaPresentacion.Pages.Usuarios
{
    public partial class Usuarios : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cargarDatos();
            }
        }

        private void cargarDatos()
        {
            UsuarioBLL usuarioBLL = new UsuarioBLL();
            rptUsuarios.DataSource = usuarioBLL.ObtenerUsuarios();
            rptUsuarios.DataBind();
        }

        protected void rptUsuarios_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int idUsuario = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "Ver")
            {
                UsuarioBLL usuarioBLL = new UsuarioBLL();
                var usuario = usuarioBLL.ObtenerUsuarioPorId(idUsuario).AsEnumerable().Select(row => new Usuario
                {
                    Id = row.Field<int>("ID"),
                    Nombre = row.Field<string>("Nombre"),
                    Contra = "*********"
                }).FirstOrDefault();

                if (usuario != null)
                {
                    var detalles = new Dictionary<string, object>
                    {
                        { "Id", usuario.Id },
                        { "Nombre usuario", usuario.Nombre },
                        { "Contraseña cifrada", usuario.Contra}
                    };

                    MostrarDetallesEnPopup("Detalles de Usuario", detalles);
                }
            }
            if (e.CommandName == "Editar")
            {
                Response.Redirect($"~/Pages/Usuarios/EditarUsuario.aspx?Id={idUsuario}");
            }
            if (e.CommandName == "Eliminar")
            {
                Response.Redirect($"~/Pages/Usuarios/EliminarUsuario.aspx?Id={idUsuario}");
            }
        }

        private void MostrarDetallesEnPopup(string titulo, Dictionary<string, object> detalles)
        {
            var sbHtml = new StringBuilder();

            // CABEZERA
            sbHtml.Append($@"
        <div class='flex items-center p-4 bg-primary text-white rounded-t-lg'>
            <h2 class='text-xl font-bold'>{titulo}</h2>
        </div>
    ");

            // CUERPO POPUP
            sbHtml.Append("<div class='p-6'>");
            foreach (var detalle in detalles)
            {
                sbHtml.Append($@"
            <div class='flex justify-between items-center border-b border-gray-200 py-3'>
                <span class='text-sm font-medium text-gray-600'>{detalle.Key}:</span>
                <span class='text-base font-semibold text-gray-900 text-right'>{detalle.Value}</span>
            </div>
        ");
            }
            sbHtml.Append("</div>");

            // COMPORTAMIENTO POPUP SWEETALERT
            string script = $@"
    Swal.fire({{
        html: `{sbHtml.ToString()}`,
        showConfirmButton: true,
        confirmButtonText: 'Cerrar',
        confirmButtonColor: '#111827',
        width: '600px',
        padding: '0',
        background: '#fff',
        customClass: {{
            popup: 'rounded-lg'
        }}
    }});";

            // Registrar el script
            ScriptManager.RegisterStartupScript(this, this.GetType(), "MostrarDetallesPopup", script, true);
        }

        protected void btnCrearNuevo_Click1(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/Usuarios/RegistrarUsuarios.aspx");
        }

        protected void btnCrearNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/Usuarios/RegistrarUsuarios.aspx");
        }
    }
}