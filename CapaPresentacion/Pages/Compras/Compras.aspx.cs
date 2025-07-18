using CapaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CapaPresentacion.Pages.Compras
{
    public partial class Compras : System.Web.UI.Page
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
            ComprasBLL comprasBLL = new ComprasBLL();
            rptCompras.DataSource = comprasBLL.ObtenerCompras();
            rptCompras.DataBind();
        }

        protected void btnCrearNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/Compras/RegistrarCompra.aspx");
        }

        protected void rptCompras_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "Ver")
            {
                Response.Redirect($"~/Pages/Compras/VerCompra?Id={id}");
            }
            if (e.CommandName == "Editar")
            {
                Response.Redirect($"~/Pages/Compras/EditarCompra?Id={id}");
            }
            if (e.CommandName == "Eliminar")
            {
                Response.Redirect($"~/Pages/Compras/EliminarCompra?Id={id}");
            }
        }
    }
}