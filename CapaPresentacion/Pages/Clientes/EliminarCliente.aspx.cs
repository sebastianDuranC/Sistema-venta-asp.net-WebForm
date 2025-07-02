using CapaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CapaPresentacion.Pages.Clientes
{
    public partial class EliminarCliente : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int idCliente = Convert.ToInt32(Request.QueryString["Id"]);
                cargarDatos(idCliente);
            }
        }

        private void cargarDatos(int idCliente)
        {
            ClienteBLL clienteBLL = new ClienteBLL();
            CapaEntidades.Cliente cliente = clienteBLL.ObtenerClientePorId(idCliente);
            rptCliente.DataSource = new List<CapaEntidades.Cliente> { cliente };
            rptCliente.DataBind();
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/Clientes/Clientes.aspx");
        }
    }
}