using CapaEntidades;
using CapaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;

namespace CapaPresentacion.Pages.Ventas
{
    public partial class RegistrarVentas : System.Web.UI.Page
    {
        private List<DetalleVenta> listaProductosSeleccionados
        {
            get { return (List<DetalleVenta>)Session["ProductosSeleccionados"] ?? new List<DetalleVenta>(); }
            set { Session["ProductosSeleccionados"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarProductos();
                CargarMetodoPagos();
                Session["ProductosSeleccionados"] = new List<DetalleVenta>();
                ActualizarResumenPedido();
                cargarClientes();
            }
        }

        protected void cancelarVenta_Click(object sender, EventArgs e)
        {
            Session.Remove("ProductosSeleccionados");
            Response.Redirect("/Pages/Ventas/Ventas.aspx");
        }

        private void CargarProductos()
        {
            CN_Productos objProductos = new CN_Productos();
            List<Producto> lista = objProductos.ObtenerProductos();
            rptProductos.DataSource = lista;
            rptProductos.DataBind();
        }

        //Metodo para obtener los clientes para generar venta
        private void cargarClientes()
        {
            CN_Cliente cN_Cliente = new CN_Cliente();
            List<Cliente> lista = cN_Cliente.ObtenerClientes();
            ddlCliente.DataSource = lista;
            ddlCliente.DataTextField = "Nombre";
            ddlCliente.DataValueField = "Id";
            ddlCliente.DataBind();
        }

        private void CargarMetodoPagos()
        {
            CN_MetodoPagos objMetodoPago = new CN_MetodoPagos();
            List<MetodoPago> lista = objMetodoPago.ObtenerMetodoPagosParaVenta();
            ddlMetodoPago.DataSource = lista;
            ddlMetodoPago.DataTextField = "Nombre";
            ddlMetodoPago.DataValueField = "Id";
            ddlMetodoPago.DataBind();
        }

        protected void btnAgregarProducto_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int productoId = Convert.ToInt32(btn.Attributes["data-producto-id"]);
            List<DetalleVenta> actualesSeleccionados = this.listaProductosSeleccionados;
            var productoExistente = actualesSeleccionados.FirstOrDefault(p => p.ProductoId == productoId);

            if (productoExistente != null)
            {
                productoExistente.Cantidad++;
                productoExistente.SubTotal = productoExistente.Cantidad * productoExistente.Producto.Precio;
            }
            else
            {
                CN_Productos objProductos = new CN_Productos();
                Producto producto = objProductos.ObtenerProductoPorId(productoId);
                if (producto != null)
                {
                    DetalleVenta nuevoDetalle = new DetalleVenta
                    {
                        ProductoId = productoId,
                        Cantidad = 1,
                        SubTotal = producto.Precio,
                        Producto = producto
                    };
                    actualesSeleccionados.Add(nuevoDetalle);
                }
            }
            this.listaProductosSeleccionados = actualesSeleccionados;
            ActualizarResumenPedido();
        }

        private void ActualizarResumenPedido()
        {
            List<DetalleVenta> actualesSeleccionados = this.listaProductosSeleccionados;
            decimal subtotalCalculado = actualesSeleccionados.Sum(p => p.SubTotal);
            decimal totalCalculado = subtotalCalculado;
            lblSubtotal.Text = string.Format(CultureInfo.InvariantCulture, "Bs. {0:N2}", subtotalCalculado);
            lblTotal.Text = string.Format(CultureInfo.InvariantCulture, "Bs. {0:N2}", totalCalculado);
            rptProductosSeleccionados.DataSource = actualesSeleccionados;
            rptProductosSeleccionados.DataBind();
        }

        protected void btnCompletarVenta_Click(object sender, EventArgs e)
        {
            try
            {
                List<DetalleVenta> detallesVenta = this.listaProductosSeleccionados;

                if (detallesVenta == null || detallesVenta.Count == 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Debe seleccionar al menos un producto.');", true);
                    return;
                }

                // --- Obtener UsuarioId a partir de Session["usuario"] (nombre de usuario) ---
                int idUsuarioParaVenta;
                if (Session["usuario"] == null)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Error: Sesión de usuario no válida. Por favor, inicie sesión nuevamente.');", true);
                    return;
                }
                string nombreUsuarioSesion = Session["usuario"].ToString();

                CN_Venta cnLogin = new CN_Venta(); 
                idUsuarioParaVenta = cnLogin.ObtenerIdUsuario(nombreUsuarioSesion);

                if (idUsuarioParaVenta == 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Error: No se pudo obtener la identificación del usuario actual.');", true);
                    return;
                }
                // --- Fin de obtener UsuarioId ---

                if (ddlMetodoPago.SelectedValue == null || !int.TryParse(ddlMetodoPago.SelectedValue, out int metodoPagoId))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Debe seleccionar un método de pago válido.');", true);
                    return;
                }

                if (ddlCliente.SelectedValue == null || !int.TryParse(ddlCliente.SelectedValue, out int clienteId))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Debe seleccionar un cliente valido.');", true);
                    return;
                }

                int enLocal = RadioButtonList1.SelectedValue == "Local" ? 1 : 0;
                CN_Venta objVentas = new CN_Venta();

                bool resultado = objVentas.RegistrarVentas(
                                        enLocal,
                                        clienteId,
                                        idUsuarioParaVenta,
                                        metodoPagoId,
                                        detallesVenta);

                if (resultado)
                {
                    Session.Remove("ProductosSeleccionados");
                    Response.Redirect("/Pages/Ventas/Ventas.aspx");
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Error al registrar la venta. Por favor, intente de nuevo.');", true);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine($"Error en btnCompletarVenta_Click: {ex.ToString()}");
                string mensajeError = "Ocurrió un error inesperado. Por favor, contacte a soporte.";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", $"alert('{mensajeError.Replace("'", "\\'")}');", true);
            }
        }

        protected void btnModificarCantidad_Command(object sender, CommandEventArgs e)
        {
            int productoId = Convert.ToInt32(e.CommandArgument);
            List<DetalleVenta> actualesSeleccionados = this.listaProductosSeleccionados;
            var producto = actualesSeleccionados.FirstOrDefault(p => p.ProductoId == productoId);

            if (producto != null)
            {
                switch (e.CommandName)
                {
                    case "AumentarCantidad":
                        producto.Cantidad++;
                        producto.SubTotal = producto.Cantidad * producto.Producto.Precio;
                        break;
                    case "DisminuirCantidad":
                        if (producto.Cantidad > 1)
                        {
                            producto.Cantidad--;
                            producto.SubTotal = producto.Cantidad * producto.Producto.Precio;
                        }
                        else
                        {
                            actualesSeleccionados.Remove(producto);
                        }
                        break;
                    case "EliminarProducto":
                        actualesSeleccionados.Remove(producto);
                        break;
                }
                this.listaProductosSeleccionados = actualesSeleccionados;
                ActualizarResumenPedido();
            }
        }

        protected void rblCliente_SelectedIndexChanged1(object sender, EventArgs e)
        {
            if (rblCliente.SelectedValue == "Comerciante")
            {
                pnlClienteComercial.Visible = true;
            }
            else
            {
                pnlClienteComercial.Visible = false;
            }
        }

        protected void ddlCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCliente.SelectedValue != null && int.TryParse(ddlCliente.SelectedValue, out int clienteId))
            {
                CN_Cliente cN_Cliente = new CN_Cliente();
                Cliente cliente = cN_Cliente.ObtenerClientes().FirstOrDefault(c => c.Id == clienteId);

                //- Si el cliente es comerciante, mostrar los campos de número de local y pasillo
                if (cliente != null)
                {
                    txtNumeroLocalCliente.Text = cliente.NumeroLocal;
                    txtPasilloCliente.Text = cliente.Pasillo;
                }
                else
                {
                    txtNumeroLocalCliente.Text = string.Empty;
                    txtPasilloCliente.Text = string.Empty;
                }
            }
        }
    }
}