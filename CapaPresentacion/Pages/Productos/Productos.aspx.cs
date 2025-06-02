using CapaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CapaPresentacion.Pages.Productos
{
    public partial class Productos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarProductos();
            }
        }

        public void CargarProductos()
        {
            ProductoBLL productoBLL = new ProductoBLL();
            rptProductos.DataSource = productoBLL.ObtenerProductos();
            rptProductos.DataBind();
        }

        protected void btnRegistrarProducto_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/Productos/RegistrarProductos.aspx");  
        }

        protected void rptProductos_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);
            switch (e.CommandName)
            {
                case "Editar":
                    Response.Redirect($"~/Pages/Productos/EditarProducto.aspx?Id={id}");
                    break;
                case "Eliminar":
                    Response.Redirect($"~/Pages/Productos/EliminarProducto.aspx?Id={id}");
                    break;
            }
        }
    }
}