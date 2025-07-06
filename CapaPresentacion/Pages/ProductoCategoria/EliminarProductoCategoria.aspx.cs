using CapaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CapaPresentacion.Pages.ProductoCategoria
{
    public partial class EliminarProductoCategoria : System.Web.UI.Page
    {
        ProductoCategoriaBLL productoCategoriaBLL = new ProductoCategoriaBLL();
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
            CapaEntidades.ProductoCategoria pCategoria = productoCategoriaBLL.ObtenerCategoriaPorId(id);
            rptCategoriaProductos.DataSource = new List<CapaEntidades.ProductoCategoria> { pCategoria };
            rptCategoriaProductos.DataBind();
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/ProductoCategoria/ProductoCategorias.aspx");
        }
    }
}