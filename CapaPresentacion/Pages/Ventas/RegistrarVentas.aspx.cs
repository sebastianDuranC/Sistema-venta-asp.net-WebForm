using CapaEntidades;
using CapaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CapaPresentacion.Pages.Ventas
{
    public partial class RegistrarVentas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarProductos();
                CargarMetodoPagos();
            }
        }

        protected void cancelarVenta_Click(object sender, EventArgs e)
        {
            //Volver a la vista de Ventas.asp
            Response.Redirect("/Pages/Ventas/Ventas.aspx");
        }

        private void CargarProductos()
        {
            CN_Productos objProductos = new CN_Productos();
            List<Producto> lista = objProductos.ObtenerProductos();

            rptProductos.DataSource = lista;
            rptProductos.DataBind();
            
        }

        private void CargarMetodoPagos()
        {
            CN_MetodoPagos objMetodoPago = new CN_MetodoPagos();
            List<MetodoPago> lista = objMetodoPago.ObtenerMetodoPagosParaVenta();

            ddlMetodoPago.DataSource = lista;
            ddlMetodoPago.DataTextField = "Nombre"; 
            ddlMetodoPago.DataValueField = "Id"; //SelectValue vamos a ovtener el id del metodo de pago
            ddlMetodoPago.DataBind();
        }

        protected void btnAgregarProducto_Click(object sender, EventArgs e)
        {
            int contador = 0;
            if (!string.IsNullOrEmpty(lblCantidadProducto.Text))
            {
                int.TryParse(lblCantidadProducto.Text, out contador);
            }
            contador++;
            lblCantidadProducto.Text = contador.ToString();
        }
    }
}