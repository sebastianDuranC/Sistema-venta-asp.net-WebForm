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
        // Instancia de la capa de negocio para manejar operaciones de Venta.
        private VentaBLL _ventaBLL = new VentaBLL();
        // Campo privado para almacenar el ID de la venta que se está editando.
        // Se inicializa en Page_Load y se mantiene en postbacks.
        private int _ventaId;

        // Propiedad de ViewState para mantener la lista de 'DetalleVenta' entre postbacks.
        // Esto es crucial porque el 'Repeater' no maneja automáticamente su estado como el 'GridView'.
        private List<DetalleVenta> DetallesVenta
        {
            get { return (List<DetalleVenta>)ViewState["DetallesVenta"] ?? new List<DetalleVenta>(); }
            set { ViewState["DetallesVenta"] = value; }
        }

        /// <summary>
        /// Se ejecuta cada vez que la página se carga (primera carga y postbacks).
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            // Verifica si es la primera vez que la página se carga.
            if (!IsPostBack)
            {
                // Intenta obtener el 'VentaId' de la cadena de consulta (URL).
                if (int.TryParse(Request.QueryString["VentaId"], out _ventaId))
                {
                    CargarCombos(); // Carga los DropDownLists (clientes, métodos de pago).
                    CargarDatosVenta(_ventaId); // Carga la información de la venta y sus detalles.
                }
                else
                {
                    // Si no se proporciona un 'VentaId' válido en la URL, redirige a la página de listado de ventas.
                    Response.Redirect("~/Pages/Ventas/Ventas.aspx");
                }
            }
            else
            {
                // En cada postback, aseguramos que '_ventaId' tenga el valor correcto,
                // ya que las variables privadas no se mantienen automáticamente.
                if (int.TryParse(Request.QueryString["VentaId"], out int id))
                {
                    _ventaId = id;
                }
            }
        }

        /// <summary>
        /// Carga los datos de los DropDownLists (Clientes y Métodos de Pago).
        /// </summary>
        private void CargarCombos()
        {
            // Carga de Clientes
            ClienteBLL cN_Cliente = new ClienteBLL();
            List<Cliente> listaClientes = cN_Cliente.ObtenerClientes();
            ddlClientes.DataSource = listaClientes;
            ddlClientes.DataTextField = "Nombre"; // Propiedad a mostrar en el DropDownList
            ddlClientes.DataValueField = "Id";   // Valor asociado a cada elemento del DropDownList
            ddlClientes.DataBind();

            // Carga de Métodos de Pago
            MetodoPagoBLL objMetodoPago = new MetodoPagoBLL();
            List<MetodoPago> listaMetodoPagos = objMetodoPago.ObtenerMetodoPagosParaVenta();
            ddlMetodoPago.DataSource = listaMetodoPagos;
            ddlMetodoPago.DataTextField = "Nombre";
            ddlMetodoPago.DataValueField = "Id";
            ddlMetodoPago.DataBind();
        }

        /// <summary>
        /// Carga los datos de una venta específica y sus detalles en los controles de la página.
        /// </summary>
        /// <param name="ventaId">El ID de la venta a cargar.</param>
        private void CargarDatosVenta(int ventaId)
        {
            // Obtiene los datos de la venta principal (cabecera) de la capa de negocio.
            DataTable dtVenta = _ventaBLL.ObtenerVentaPorId(ventaId);
            if (dtVenta != null && dtVenta.Rows.Count > 0)
            {
                DataRow rowVenta = dtVenta.Rows[0];

                // Asigna los valores de la venta a los controles de la interfaz.
                // Después de asignar el SelectedValue de ddlClientes, agrega este bloque:
                ddlClientes.SelectedValue = rowVenta["ClienteId"].ToString();

                int clienteId = Convert.ToInt32(rowVenta["ClienteId"]);
                if (clienteId == 1)
                {
                    rblCliente.SelectedValue = "Normal";
                    pnlClienteComercial.Visible = false;
                }
                else if (clienteId > 1)
                {
                    rblCliente.SelectedValue = "Comerciante";
                    pnlClienteComercial.Visible = true;
                }

                ddlMetodoPago.SelectedValue = rowVenta["MetodoPagoId"].ToString();
                // Determina si la venta fue "En Local" o "Para Llevar" y selecciona el RadioButton adecuado.
                rdbEnLocal.SelectedValue = Convert.ToBoolean(rowVenta["EnLocal"]) ? "Local" : "Llevar";
                lblTotal.Text = Convert.ToDecimal(rowVenta["Total"]).ToString("N2"); // Formatea el total a 2 decimales.
                lblFecha.Text = "Fecha: " + Convert.ToDateTime(rowVenta["Fecha"]).ToString("dd/MM/yyyy"); // Formatea la fecha.
            }

            // Obtiene los detalles de la venta (productos) de la capa de negocio.
            DataTable dtDetalles = _ventaBLL.ObtenerDetallesVentaPorVentaId(ventaId);

            // Convierte el DataTable de detalles a una lista de objetos 'DetalleVenta'.
            // Esto permite una manipulación más sencilla y un buen enlace con el 'Repeater'.
            List<DetalleVenta> detallesExistentes = (from row in dtDetalles.AsEnumerable()
                                                     select new DetalleVenta()
                                                     {
                                                         Id = Convert.ToInt32(row["Id"]), // ID del detalle de venta
                                                         VentaId = Convert.ToInt32(row["VentaId"]),
                                                         ProductoId = Convert.ToInt32(row["ProductoId"]),
                                                         Producto = new Producto // Crea una instancia de Producto para sus propiedades
                                                         {
                                                             Nombre = row["ProductoNombre"].ToString(),
                                                             Precio = Convert.ToDecimal(row["PrecioUnitario"])
                                                         },
                                                         Cantidad = Convert.ToInt32(row["Cantidad"]),
                                                         SubTotal = Convert.ToDecimal(row["SubTotal"])
                                                     }).ToList();

            // Almacena la lista de detalles en 'ViewState' para que persista entre postbacks.
            DetallesVenta = detallesExistentes;

            // Enlaza el 'Repeater' con la lista de detalles de venta.
            rptDetalleVenta.DataSource = DetallesVenta;
            rptDetalleVenta.DataBind();

            // Recalcula los totales después de cargar todos los detalles.
            CalcularTotales();
        }

        /// <summary>
        /// Maneja el evento 'TextChanged' del TextBox de cantidad dentro de cada fila del 'Repeater'.
        /// Se dispara cuando el usuario cambia la cantidad y sale del campo (debido a AutoPostBack="true").
        /// </summary>
        protected void txtCantidad_TextChanged(object sender, EventArgs e)
        {
            TextBox txtCantidad = (TextBox)sender;
            // Obtiene el 'RepeaterItem' (fila) que contiene el TextBox que disparó el evento.
            RepeaterItem item = (RepeaterItem)txtCantidad.NamingContainer;
            int itemIndex = item.ItemIndex; // Obtiene el índice de la fila en el Repeater.

            // Obtiene la lista actual de detalles de venta desde 'ViewState'.
            var detalles = DetallesVenta;
            int nuevaCantidad;

            // Valida que la cantidad ingresada sea un número válido y positivo.
            if (int.TryParse(txtCantidad.Text, out nuevaCantidad) && nuevaCantidad > 0)
            {
                // Actualiza la cantidad y recalcula el subtotal para el detalle de venta modificado.
                detalles[itemIndex].Cantidad = nuevaCantidad;
                detalles[itemIndex].SubTotal = nuevaCantidad * detalles[itemIndex].Producto.Precio;

                // Actualiza el 'ViewState' con la lista modificada.
                DetallesVenta = detalles;

                // Vuelve a enlazar el 'Repeater' para que los cambios se reflejen en la UI.
                rptDetalleVenta.DataSource = DetallesVenta;
                rptDetalleVenta.DataBind();

                // Recalcula los totales generales de la venta (Subtotal, Total).
                CalcularTotales();
            }
            else
            {
                // Muestra un mensaje de error si la cantidad no es válida.
                MostrarMensaje("Por favor ingrese una cantidad válida (número mayor que cero).", "error");
                // Opcional: Revertir la cantidad en el TextBox a su valor anterior si la entrada es inválida.
                // Esto evita que un valor no válido quede en el TextBox.
                txtCantidad.Text = detalles[itemIndex].Cantidad.ToString();
            }
        }

        /// <summary>
        /// Recalcula el subtotal y el total general de la venta basándose en los detalles de venta actuales.
        /// Este método se llama después de cada cambio en la cantidad de un producto o al eliminar un producto.
        /// </summary>
        private void CalcularTotales()
        {
            decimal subtotal = 0;
            // Itera sobre todos los detalles de venta para sumar sus subtotales.
            foreach (var detalle in DetallesVenta)
            {
                subtotal += detalle.SubTotal;
            }
            // Actualiza las etiquetas de subtotal y total en la UI.
            lblSubtotal.Text = subtotal.ToString("N2"); // Formato de moneda con 2 decimales.
            lblTotal.Text = subtotal.ToString("N2");

            // NOTA: El cálculo del cambio ahora se maneja completamente en el lado del cliente (JavaScript).
        }

        /// <summary>
        /// Maneja el evento 'Click' del botón "Guardar Cambios".
        /// </summary>
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                // Valida que haya al menos un producto en la venta antes de intentar guardar.
                if (DetallesVenta.Count == 0)
                {
                    MostrarMensaje("No se puede guardar una venta sin productos. Agregue al menos un producto.", "error");
                    return;
                }

                // Validar y obtener el ID del usuario actual
                int idUsuarioParaVenta;
                if (Session["usuario"] == null)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Error: Sesión de usuario no válida. Por favor, inicie sesión nuevamente.');", true);
                    return;
                }
                string nombreUsuarioSesion = Session["usuario"].ToString();
                VentaBLL cnVenta = new VentaBLL();
                idUsuarioParaVenta = cnVenta.ObtenerIdUsuario(nombreUsuarioSesion);

                if (idUsuarioParaVenta <= 0)
                {
                    MostrarMensaje("Error: No se pudo obtener el ID del usuario. Por favor, inicie sesión nuevamente.", "error");
                    return;
                }

                // Crea un objeto 'Venta' con los datos actuales de los controles de la página.
                Venta venta = new Venta
                {
                    Id = _ventaId,
                    ClienteId = rblCliente.SelectedValue == "Normal"
                                   ? 1
                                   : Convert.ToInt32(ddlClientes.SelectedValue),
                    UsuarioId = idUsuarioParaVenta,
                    MetodoPagoId = Convert.ToInt32(ddlMetodoPago.SelectedValue),
                    EnLocal = (rdbEnLocal.SelectedValue == "Local"),
                    Fecha = DateTime.Now,
                    Total = Convert.ToDecimal(lblTotal.Text.Replace("Bs.", "").Trim()),
                    Estado = true
                };

                // Llama al método en la capa de negocio para editar la venta y sus detalles.
                _ventaBLL.EditarVenta(venta, DetallesVenta);

                // Muestra un mensaje de éxito y redirige a la página de listado de ventas.
                MostrarMensaje("Venta editada exitosamente.", "success");
                Response.Redirect("~/Pages/Ventas/Ventas.aspx?successMessage=VentaEditada", false);
            }
            catch (ApplicationException ex)
            {
                // Captura errores específicos de la aplicación (como los RAISERROR de SQL)
                MostrarMensaje($"Error al guardar la venta: {ex.Message}", "error");
            }
            catch (Exception ex)
            {
                // Captura cualquier otra excepción no controlada
                MostrarMensaje($"Error inesperado al guardar la venta: {ex.Message}", "error");
            }
        }

        /// <summary>
        /// Maneja los comandos disparados desde los elementos del 'Repeater' (ej. el botón "Eliminar").
        /// </summary>
        protected void rptDetalleVenta_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            // Verifica si el comando es "EliminarDetalle".
            if (e.CommandName == "EliminarDetalle")
            {
                // Obtiene el índice de la fila del Repeater desde el CommandArgument.
                int index = Convert.ToInt32(e.CommandArgument);
                var detalles = DetallesVenta; // Obtiene la lista actual de detalles de venta.

                // Verifica que el índice sea válido para evitar errores.
                if (index >= 0 && index < detalles.Count)
                {
                    detalles.RemoveAt(index); // Elimina el detalle de venta de la lista.
                    DetallesVenta = detalles; // Actualiza la lista en 'ViewState'.

                    // Vuelve a enlazar el 'Repeater' para reflejar el cambio en la interfaz de usuario.
                    rptDetalleVenta.DataSource = DetallesVenta;
                    rptDetalleVenta.DataBind();

                    // Recalcula los totales de la venta después de eliminar un producto.
                    CalcularTotales();
                    MostrarMensaje("Producto eliminado de la venta.", "info");
                }
            }
        }

        /// <summary>
        /// Muestra un mensaje al usuario en un panel dedicado en la página.
        /// </summary>
        /// <param name="mensaje">El texto del mensaje a mostrar.</param>
        /// <param name="tipo">El tipo de mensaje ("success", "error", "info") para aplicar estilos CSS.</param>
        private void MostrarMensaje(string mensaje, string tipo)
        {
            lblMensaje.Text = mensaje; // Asigna el texto al Label dentro del panel.
            pnlMensaje.Visible = true; // Hace visible el panel de mensajes.

            // Asigna clases CSS al panel de mensajes para cambiar su apariencia según el tipo de mensaje.
            if (tipo == "success")
            {
                pnlMensaje.CssClass = "mt-6 p-4 rounded-md bg-green-100 text-green-700 border border-green-200";
            }
            else if (tipo == "error")
            {
                pnlMensaje.CssClass = "mt-6 p-4 rounded-md bg-red-100 text-red-700 border border-red-200";
            }
            else // Para tipo "info" o cualquier otro.
            {
                pnlMensaje.CssClass = "mt-6 p-4 rounded-md bg-blue-100 text-blue-700 border border-blue-200";
            }
        }

        protected void rblCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnlClienteComercial.Visible = rblCliente.SelectedValue == "Comerciante";
        }
    }
}