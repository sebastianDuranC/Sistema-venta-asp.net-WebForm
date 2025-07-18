using CapaEntidades;
using CapaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CapaPresentacion.Pages.UnidadesMedida
{
    public partial class UnidadesMedida : System.Web.UI.Page
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
            UnidadMedidaBLL unidadMedidaBLL = new UnidadMedidaBLL();
            rpttbUnidadMedida.DataSource = unidadMedidaBLL.ObtenerUnidadesMedida();
            rpttbUnidadMedida.DataBind();
        }

        protected void btnCrearNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/UnidadesMedida/RegistrarUnidadMedida.aspx");
        }

        protected void rpttbUnidadMedida_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int unidadMedidaId = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "Ver")
            {
                UnidadMedidaBLL unidadMedidaBLL = new UnidadMedidaBLL();
                CapaEntidades.UnidadesMedida oUnidadMedida = unidadMedidaBLL.ObtenerUnidadMedidaPorId(unidadMedidaId);
                if (oUnidadMedida != null)
                {
                    var detalles = new Dictionary<string, object>
                    {
                        { "ID", oUnidadMedida.Id },
                        { "Nombre de Unidad", oUnidadMedida.Nombre },
                        { "Abreviatura", oUnidadMedida.Abreviatura }
                    };

                    MostrarDetallesEnPopup("Detalles de la Unidad", detalles);
                }
            }
            if (e.CommandName == "Editar")
            {
                Response.Redirect($"~/Pages/UnidadesMedida/EditarUnidadMedida.aspx?Id={unidadMedidaId}");
            }
            if (e.CommandName == "Eliminar")
            {
                Response.Redirect($"~/Pages/UnidadesMedida/EliminarUnidadMedida.aspx?Id={unidadMedidaId}");
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
    }
}