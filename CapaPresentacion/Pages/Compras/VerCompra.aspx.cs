using CapaEntidades;
using CapaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CapaPresentacion.Pages.Compras
{
    public partial class VerCompra : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Usamos int.TryParse para una forma más segura de convertir el ID de la URL.
                if (int.TryParse(Request.QueryString["Id"], out int idCompra))
                {
                    cargarDato(idCompra);
                }
                else
                {
                    // Si el ID no es válido o no existe, redirigimos a la lista principal.
                    Response.Redirect("~/Pages/Compras/Compras.aspx");
                }
            }
        }

        private void cargarDato(int idCompra)
        {
            // 1. Instanciamos la capa de negocio.
            ComprasBLL comprasBLL = new ComprasBLL();

            // 2. Llamamos al método que obtiene la compra y todos sus detalles.
            Compra compra = comprasBLL.ObtenerCompraPorId(idCompra);

            // 3. Verificamos si la compra fue encontrada.
            if (compra != null)
            {
                // 4. Poblamos los controles de la cabecera "manualmente".
                lblCompraId.Text = compra.Id.ToString("D4"); // Formato "0001"
                lblFecha.Text = compra.Fecha.ToString("dd/MM/yyyy HH:mm 'hrs.'");
                lblProveedor.Text = compra.Proveedor.Nombre;
                lblUsuario.Text = compra.Usuario.Nombre;
                lblTotalCompra.Text = $"Bs. {compra.Total:N2}";

                // 5. Enlazamos la lista de detalles de la compra al Repeater.
                rptDetalleCompra.DataSource = compra.DetallesCompra;
                rptDetalleCompra.DataBind();
            }
            else
            {
                // Si el método devuelve null, significa que la compra no existe.
                // Redirigimos para evitar errores.
                Response.Redirect("~/Pages/Compras/Compras.aspx");
            }
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/Compras/Compras.aspx");
        }
    }
}