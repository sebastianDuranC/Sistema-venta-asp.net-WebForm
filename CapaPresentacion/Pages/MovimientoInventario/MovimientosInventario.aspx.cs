using CapaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CapaPresentacion.Pages.MovimientoInventario
{
    public partial class MovimientosInventario : System.Web.UI.Page
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
            MovimientoInventarioBLL movimientoInventarioBLL = new MovimientoInventarioBLL();
            rptMoviientoInventario.DataSource = movimientoInventarioBLL.ObtenerMovimientoInventario();
            rptMoviientoInventario.DataBind();
        }

        protected void rptMoviientoInventario_ItemCommand(object source, RepeaterCommandEventArgs e)
        {

        }

        protected void btnCrearNuevo_Click(object sender, EventArgs e)
        {

        }
    }
}