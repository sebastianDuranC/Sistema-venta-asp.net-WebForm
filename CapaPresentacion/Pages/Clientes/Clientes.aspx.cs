using CapaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CapaPresentacion.Pages.Clientes
{
    public partial class Clientes : System.Web.UI.Page
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
            ClienteBLL clienteBLL = new ClienteBLL();
            rptCliente.DataSource = clienteBLL.ListarClientes();
            rptCliente.DataBind();
        }

        protected void btnCrearNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/Clientes/RegistrarCliente.aspx");
        }

        protected void rptCliente_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int idCliente = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "Editar")
            {
                Response.Redirect($"~/Pages/Clientes/EditarCliente.aspx?Id={idCliente}");
            }
            else if (e.CommandName == "Ver")
            {
                Response.Redirect($"~/Pages/Clientes/VerCliente.aspx?Id={idCliente}");
            }
            else if (e.CommandName == "Eliminar")
            {
                Response.Redirect($"~/Pages/Clientes/EliminarCliente.aspx?Id={idCliente}");
            }
        }
    }
}