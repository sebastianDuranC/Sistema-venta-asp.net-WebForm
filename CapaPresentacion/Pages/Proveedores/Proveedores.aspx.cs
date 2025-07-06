using CapaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CapaPresentacion.Pages.Proveedores
{
    public partial class Proveedores : System.Web.UI.Page
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
            ProveedorBLL proveedorBLL = new ProveedorBLL();
            rptProveedores.DataSource = proveedorBLL.ObtenerProveedores();
            rptProveedores.DataBind();
        }

        protected void btnCrearNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/Proveedores/RegistrarProveedor.aspx");
        }

        protected void rptProveedores_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int idProveedor = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "Ver")
            {
                Response.Redirect($"~/Pages/Proveedores/VerProveedor?Id={idProveedor}");
            }
            if (e.CommandName == "Editar")
            {
                Response.Redirect($"~/Pages/Proveedores/EditarProveedor?Id={idProveedor}");
            }
            if (e.CommandName == "Eliminar")
            {
                Response.Redirect($"~/Pages/Proveedores/EliminarProveedor?Id={idProveedor}");
            }
        }
    }
}