using CapaNegocio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CapaPresentacion.Pages.Rol
{
    public partial class EliminarRol : System.Web.UI.Page
    {
        RolBLL rolBLL = new RolBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["Id"] != null && int.TryParse(Request.QueryString["Id"], out int id))
                {
                    cargarDatos(id);
                }
                else
                {
                    Response.Redirect("Rol.aspx");
                }
            }
        }

        private void cargarDatos(int id)
        {
            List<object> rol = new List<object> { rolBLL.ObtenerRolPorId(id) };
            rptRol.DataSource = rol;
            rptRol.DataBind();
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/Rol/Rol.aspx");
        }
    }
}