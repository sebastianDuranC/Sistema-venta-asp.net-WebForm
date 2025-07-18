using CapaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CapaPresentacion.Pages.InsumoCategoria
{
    public partial class InsumoCategorias : System.Web.UI.Page
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
            InsumoCategoriaBLL insumoCategorias = new InsumoCategoriaBLL();
            rptInsumoCategoria.DataSource = insumoCategorias.ObtenerInsumoCategorias();
            rptInsumoCategoria.DataBind();
        }

        protected void rptInsumoCategoria_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int idInsumoCategoria = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "Ver")
            {
                Response.Redirect($"~/Pages/InsumoCategoria/VerInsumoCategoria.aspx?Id={idInsumoCategoria}");
            }
            else if (e.CommandName == "Editar")
            {
                Response.Redirect($"~/Pages/InsumoCategoria/EditarInsumoCategoria.aspx?Id={idInsumoCategoria}");
            }
            else if (e.CommandName == "Eliminar")
            {
                Response.Redirect($"~/Pages/InsumoCategoria/EliminarInsumoCategoria.aspx?Id={idInsumoCategoria}");
            }
        }

        protected void btnCrearNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/InsumoCategoria/RegistrarInsumoCategoria.aspx");
        }
    }
}