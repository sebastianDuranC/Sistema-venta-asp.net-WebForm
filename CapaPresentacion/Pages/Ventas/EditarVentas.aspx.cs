using CapaEntidades;
using CapaNegocio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CapaPresentacion.Pages.Ventas
{
    public partial class EditarVentas : System.Web.UI.Page
    {
        private CN_Venta _ventaBLL = new CN_Venta();
        private int _ventaId;

        private List<DetalleVenta> DetallesVenta
        {
            get { return (List<DetalleVenta>)ViewState["DetallesVenta"] ?? new List<DetalleVenta>(); }
            set { ViewState["DetallesVenta"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (int.TryParse(Request.QueryString["VentaId"], out _ventaId))
                {
                    CargarCombos(); // Cargar los DropDownLists primero
                    CargarDatosVenta(_ventaId);
                }
                else
                {
                    Response.Redirect("~/Pages/Ventas/Ventas.aspx");
                }
            }
            else
            {
                // En cada postback, aseguramos que _ventaId esté disponible
                if (int.TryParse(Request.QueryString["VentaId"], out int id))
                {
                    _ventaId = id;
                }
            }
        }
       
        private void CargarCombos()
        {
            CN_Cliente cN_Cliente = new CN_Cliente();
            List<Cliente> listaClientes = cN_Cliente.ObtenerClientes();
            ddlClientes.DataSource = listaClientes;
            ddlClientes.DataTextField = "Nombre";
            ddlClientes.DataValueField = "Id";
            ddlClientes.DataBind();

            CN_MetodoPagos objMetodoPago = new CN_MetodoPagos();
            List<MetodoPago> listaMetodoPagos = objMetodoPago.ObtenerMetodoPagosParaVenta();
            ddlMetodoPago.DataSource = listaMetodoPagos;
            ddlMetodoPago.DataTextField = "Nombre";
            ddlMetodoPago.DataValueField = "Id";
            ddlMetodoPago.DataBind();
        }

        private void CargarDatosVenta(int ventaId)
        {
            // Obtener datos de la venta desde la capa de negocio
            DataTable dtVenta = _ventaBLL.ObtenerVentaPorId(ventaId);
            List<Venta> ventaExistente = (from row in dtVenta.AsEnumerable()
                                          select new Venta()
                                          {
                                              Id = Convert.ToInt32(row["VentaId"]),
                                              Fecha = Convert.ToDateTime(row["Fecha"]),
                                              Total = Convert.ToDecimal(row["Total"]),
                                              EnLocal = Convert.ToBoolean(row["EnLocal"]),
                                              ClienteId = Convert.ToInt32(row["ClienteId"].ToString()),
                                              UsuarioId = Convert.ToInt32(row["VendedorId"].ToString()),
                                              MetodoPagoId = Convert.ToInt32(row["MetodoPagoId"].ToString()),
                                              Estado = Convert.ToBoolean(row["Estado"])
                                          }).ToList();
            if (ventaExistente != null)
            {
                foreach (var venta in ventaExistente)
                {
                    // Seleccionar el cliente y método de pago correctos
                    ddlClientes.SelectedValue = venta.ClienteId.ToString();
                    ddlMetodoPago.SelectedValue = venta.MetodoPagoId.ToString();
                    rdbEnLocal.SelectedValue = venta.EnLocal ? "Local" : "Llevar";
                    lblTotal.Text = venta.Total.ToString();
                    lblFecha.Text = "Fecha: " + venta.Fecha.ToString("dd/MM/yyyy");
                }
            }

            // Para los detalles
            DataTable dtDetalles = _ventaBLL.ObtenerDetallesVentaPorVentaId(ventaId);
            List<DetalleVenta> detallesExistentes = (from row in dtDetalles.AsEnumerable()
                                                     select new DetalleVenta()
                                                     {
                                                         Id = Convert.ToInt32(row["Id"]),
                                                         VentaId = Convert.ToInt32(row["VentaId"]),
                                                         ProductoId = Convert.ToInt32(row["ProductoId"]),
                                                         Producto = new Producto
                                                         {
                                                             Nombre = row["ProductoNombre"].ToString(),
                                                             Precio = Convert.ToDecimal(row["PrecioUnitario"])
                                                         },
                                                         Cantidad = Convert.ToDecimal(row["Cantidad"]),
                                                         SubTotal = Convert.ToDecimal(row["SubTotal"]),
                                                         Estado = Convert.ToBoolean(row["Estado"])
                                                     }).ToList();

            // Cargar GridView y almacenar en ViewState
            if (detallesExistentes != null)
            {
                DetallesVenta = detallesExistentes;
                CalcularTotales();
                gvDetalleVenta.DataSource = DetallesVenta;
                gvDetalleVenta.DataBind();
            }
        }

        protected void gvDetalles_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Eliminar")
            {
                // Obtener el ProductoId del CommandArgument
                if (int.TryParse(e.CommandArgument.ToString(), out int productoIdAEliminar))
                {
                    
                    //CalcularTotales(); // Recalcular totales después de eliminar
                }
            }
        }

        protected void gvDetalles_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Asegúrate de que el TextBox txtCantidad esté en la columna correcta (índice 1 en este caso)
                TextBox txtCantidad = (TextBox)e.Row.FindControl("txtCantidad");
                if (txtCantidad != null)
                {
                    // Asignar el atributo onchange para llamar a la función JavaScript
                    // txtCantidad.Attributes.Add("onchange", "CalcularSubtotal(this);"); // Ya no necesitamos la función JS

                    // Si quieres manejar el cálculo en el servidor directamente
                    txtCantidad.AutoPostBack = true;
                    txtCantidad.TextChanged += new EventHandler(txtCantidad_TextChanged);
                }
            }
        }

        protected void txtCantidad_TextChanged(object sender, EventArgs e)
        {
            TextBox txtCantidad = (TextBox)sender;
            GridViewRow row = (GridViewRow)txtCantidad.NamingContainer;
            int rowIndex = row.RowIndex;
            var detalles = DetallesVenta;
            int cantidad;
            if (int.TryParse(txtCantidad.Text, out cantidad) && cantidad > 0)
            {
                detalles[rowIndex].Cantidad = cantidad;
                detalles[rowIndex].SubTotal = cantidad * detalles[rowIndex].Producto.Precio;
                DetallesVenta = detalles;
                gvDetalleVenta.DataSource = DetallesVenta;
                gvDetalleVenta.DataBind();
                CalcularTotales();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Por favor ingrese una cantidad válida.');", true);
            }
        }

        private void CalcularTotales()
        {
            decimal subtotal = 0;
            foreach (var detalle in DetallesVenta)
            {
                subtotal += detalle.SubTotal;
            }
            lblSubtotal.Text = subtotal.ToString("N2");
            lblTotal.Text = subtotal.ToString("N2");
            CalcularCambio();
        }

        protected void txtMontoCliente_TextChanged(object sender, EventArgs e)
        {
            CalcularCambio();
        }

        private void CalcularCambio()
        {
            decimal montoCliente;
            decimal totalVenta = 0;
            decimal.TryParse(lblTotal.Text.Replace("$", "").Replace("€", "").Replace(",", "").Trim(), out totalVenta);
            if (decimal.TryParse(txtMontoCliente.Text, out montoCliente))
            {
                decimal cambio = montoCliente - totalVenta;
                lblCambio.Text = cambio.ToString("N2");
            }
            else
            {
                lblCambio.Text = "0.00";
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                Venta venta = new Venta
                {
                    Id = _ventaId,
                    ClienteId = Convert.ToInt32(ddlClientes.SelectedValue),
                    MetodoPagoId = Convert.ToInt32(ddlMetodoPago.SelectedValue),
                    EnLocal = Convert.ToBoolean(rdbEnLocal.SelectedValue == "Local" ? 1 : 0),
                    Fecha = DateTime.Now,
                    Estado = true
                };
                _ventaBLL.EditarVenta(venta, DetallesVenta);
                Response.Redirect("~/Pages/Ventas/Ventas.aspx");
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "error",
                    $"alert('Error al guardar la venta: {ex.Message}');", true);
            }
        }

        protected void gvDetalleVenta_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EliminarFila")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                var detalles = DetallesVenta;
                if (index >= 0 && index < detalles.Count)
                {
                    detalles.RemoveAt(index);
                    DetallesVenta = detalles;
                    gvDetalleVenta.DataSource = DetallesVenta;
                    gvDetalleVenta.DataBind();
                    CalcularTotales();
                }
            }
        }

        protected void gvDetalleVenta_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TextBox txtCantidad = new TextBox();
                txtCantidad.ID = "txtCantidad";
                txtCantidad.CssClass = "w-16 rounded border border-gray-300 px-2 py-1 text-center";
                txtCantidad.AutoPostBack = true;
                txtCantidad.TextChanged += txtCantidad_TextChanged;
                txtCantidad.Text = DataBinder.Eval(e.Row.DataItem, "Cantidad").ToString();
                e.Row.Cells[1].Controls.Add(txtCantidad);
            }
        }
    }
}