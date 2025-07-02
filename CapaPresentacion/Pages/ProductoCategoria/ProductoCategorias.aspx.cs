using CapaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CapaPresentacion.Pages.ProductoCategoria
{
    public partial class ProductoCategorias : System.Web.UI.Page
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
            ProductoCategoriaBLL bllProductoCategoria = new ProductoCategoriaBLL();
            rptPCategoria.DataSource = bllProductoCategoria.ObtenerCategoriasProducto();
            rptPCategoria.DataBind();
        }

        protected void btnCrearNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/ProductoCategoria/RegistrarProductoCategoria.aspx");
        }

        protected void rptPCategoria_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int idCategoria = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "Ver")
            {
                Response.Redirect($"~/Pages/ProductoCategoria/VerProductoCategorias.aspx?Id={idCategoria}");
            }
            if (e.CommandName == "Editar")
            {
                Response.Redirect($"~/Pages/ProductoCategoria/EditarProductoCategorias.aspx?Id={idCategoria}");
            }
            if (e.CommandName == "Eliminar")
            {
                Response.Redirect($"~/Pages/ProductoCategoria/EliminarProductoCategorias.aspx?Id={idCategoria}");
            }
        }
    }
}