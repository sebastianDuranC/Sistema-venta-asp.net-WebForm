using CapaEntidades;
using CapaNegocio;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CapaPresentacion.Pages.Ventas
{
    public partial class RegistrarVentas : System.Web.UI.Page
    {
        /// <summary>
        /// Propiedad de sesión para almacenar la lista de productos seleccionados en el carrito de compra.
        /// Utiliza la sesión para mantener el estado de los productos entre postbacks.
        /// </summary>
        private List<DetalleVenta> listaProductosSeleccionados
        {
            get
            {
                // Si la sesión no contiene la lista, inicializa una nueva lista vacía.
                return Session["ProductosSeleccionados"] as List<DetalleVenta> ?? new List<DetalleVenta>();
            }
            set
            {
                // Almacena la lista actual en la sesión.
                Session["ProductosSeleccionados"] = value;
            }
        }

        /// <summary>
        /// Evento que se dispara al cargar la página.
        /// Se encarga de inicializar los datos necesarios solo en la primera carga (no postback).
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarDatosIniciales();
                // Inicializa la lista de productos seleccionados en la sesión.
                // Esto asegura que la lista esté vacía al iniciar una nueva venta.
                listaProductosSeleccionados = new List<DetalleVenta>();
                // Actualiza el resumen del pedido para mostrar los totales iniciales (cero).
                ActualizarResumenPedido();
            }
        }

        /// <summary>
        /// Carga todos los datos iniciales necesarios para la página de ventas:
        /// productos, clientes y métodos de pago.
        /// </summary>
        private void CargarDatosIniciales()
        {
            // Cargar productos disponibles para la venta
            CargarProductosDisponibles();
            // Cargar la lista de clientes (comerciantes)
            CargarClientesYMetodosPago();
        }

        /// <summary>
        /// Carga la lista de todos los productos disponibles desde la capa de negocio
        /// y los enlaza al Repeater `rptProductos` para su visualización.
        /// </summary>
        private void CargarProductosDisponibles()
        {
            CN_Productos objProductos = new CN_Productos();
            List<Producto> lista = objProductos.ObtenerProductos();
            rptProductos.DataSource = lista;
            rptProductos.DataBind();
        }

        /// <summary>
        /// Carga la lista de clientes y los métodos de pago disponibles.
        /// Se agrupan porque ambos son DropDownLists que se cargan al inicio.
        /// </summary>
        private void CargarClientesYMetodosPago()
        {
            // Cargar clientes
            CN_Cliente cN_Cliente = new CN_Cliente();
            List<Cliente> listaClientes = cN_Cliente.ObtenerClientes();
            ddlCliente.DataSource = listaClientes;
            ddlCliente.DataTextField = "Nombre"; // Mostrar el nombre del cliente
            ddlCliente.DataValueField = "Id";    // Usar el ID del cliente como valor
            ddlCliente.DataBind();

            // Insertar un item por defecto si es necesario, o asegurar que haya un cliente seleccionado
            if (ddlCliente.Items.Count > 0)
            {
                ddlCliente.Items.Insert(0, new ListItem("-- Seleccione --", "0"));
                ddlCliente.SelectedValue = "0"; // Asegura que el valor por defecto sea el seleccionado
            }
            else if (ddlCliente.Items.Count == 0)
            {
                ddlCliente.Items.Add(new ListItem("-- No hay Clientes --", "0"));
            }

            // Cargar métodos de pago
            CN_MetodoPagos objMetodoPago = new CN_MetodoPagos();
            List<MetodoPago> listaMetodoPagos = objMetodoPago.ObtenerMetodoPagosParaVenta();
            ddlMetodoPago.DataSource = listaMetodoPagos;
            ddlMetodoPago.DataTextField = "Nombre"; // Mostrar el nombre del método de pago
            ddlMetodoPago.DataValueField = "Id";    // Usar el ID del método de pago como valor
            ddlMetodoPago.DataBind();

            // Insertar un item por defecto o asegurar una selección
            if (ddlMetodoPago.Items.Count > 0)
            {
                ddlMetodoPago.Items.Insert(0, new ListItem("-- Seleccione Método --", "0"));
                ddlMetodoPago.SelectedValue = "0";
            }
            else if (ddlMetodoPago.Items.Count == 0)
            {
                ddlMetodoPago.Items.Add(new ListItem("-- No hay Métodos de Pago --", "0"));
            }
        }

        /// <summary>
        /// Evento que se activa al hacer clic en el botón "Agregar Producto".
        /// Añade o incrementa la cantidad de un producto en el carrito de compra.
        /// </summary>
        protected void btnAgregarProducto_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            // Obtiene el ID del producto desde el atributo 'data-producto-id'.
            int productoId = Convert.ToInt32(btn.Attributes["data-producto-id"]);

            List<DetalleVenta> carrito = listaProductosSeleccionados;
            // Busca si el producto ya existe en el carrito.
            DetalleVenta productoEnCarrito = carrito.FirstOrDefault(p => p.ProductoId == productoId);

            if (productoEnCarrito != null)
            {
                // Si el producto ya está en el carrito, incrementa su cantidad y actualiza el subtotal.
                productoEnCarrito.Cantidad++;
                productoEnCarrito.SubTotal = productoEnCarrito.Cantidad * productoEnCarrito.Producto.Precio;
            }
            else
            {
                // Si el producto no está en el carrito, lo busca en la base de datos
                // y lo añade como un nuevo DetalleVenta.
                CN_Productos objProductos = new CN_Productos();
                Producto producto = objProductos.ObtenerProductoPorId(productoId);
                if (producto != null)
                {
                    carrito.Add(new DetalleVenta
                    {
                        ProductoId = productoId,
                        Cantidad = 1,
                        SubTotal = producto.Precio,
                        Producto = producto, // Se almacena el objeto Producto completo para mostrar sus detalles.
                        Estado = true
                    });
                }
            }
            listaProductosSeleccionados = carrito; // Actualiza la lista en la sesión
            ActualizarResumenPedido(); // Refresca el resumen del pedido en la UI.
        }

        /// <summary>
        /// Evento que se dispara al cambiar la selección del tipo de cliente (Normal/Comerciante).
        /// Muestra u oculta el panel de selección de cliente comerciante según la opción elegida.
        /// </summary>
        protected void rblCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Hace visible el panel de cliente comercial si se selecciona "Comerciante",
            // de lo contrario, lo oculta.
            pnlClienteComercial.Visible = rblCliente.SelectedValue == "Comerciante";
        }

        /// <summary>
        /// Evento que se dispara al seleccionar un cliente en el DropDownList de clientes comerciales.
        /// Muestra el número de local y pasillo del cliente seleccionado.
        /// </summary>
        protected void ddlCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (int.TryParse(ddlCliente.SelectedValue, out int clienteId) && clienteId > 0)
            {
                CN_Cliente cN_Cliente = new CN_Cliente();
                // Busca el cliente seleccionado en la lista completa de clientes.
                Cliente cliente = cN_Cliente.ObtenerClientes().FirstOrDefault(c => c.Id == clienteId);

                if (cliente != null)
                {
                    txtNumeroLocalCliente.Text = cliente.NumeroLocal;
                    txtPasilloCliente.Text = cliente.Pasillo;
                }
                else
                {
                    // Limpia los campos si no se encuentra el cliente o el ID es inválido.
                    txtNumeroLocalCliente.Text = string.Empty;
                    txtPasilloCliente.Text = string.Empty;
                }
            }
            else
            {
                // Limpia los campos si se selecciona la opción por defecto o un valor inválido.
                txtNumeroLocalCliente.Text = string.Empty;
                txtPasilloCliente.Text = string.Empty;
            }
        }

        /// <summary>
        /// Actualiza el Repeater de productos seleccionados y recalcula el subtotal y total de la venta.
        /// Se invoca después de cualquier modificación en el carrito de compra.
        /// </summary>
        private void ActualizarResumenPedido()
        {
            List<DetalleVenta> carrito = listaProductosSeleccionados;
            // Calcula el subtotal sumando los subtotales de cada producto en el carrito.
            decimal subtotalCalculado = carrito.Sum(p => p.SubTotal);
            decimal totalCalculado = subtotalCalculado;

            // Formatea y muestra el subtotal y total en los Labels correspondientes.
            // Se utiliza CultureInfo.InvariantCulture para asegurar un formato numérico consistente (con punto como decimal).
            lblSubtotal.Text = string.Format(CultureInfo.InvariantCulture, "Bs. {0:N2}", subtotalCalculado);
            lblTotal.Text = string.Format(CultureInfo.InvariantCulture, "Bs. {0:N2}", totalCalculado);

            // Enlaza la lista de productos seleccionados al Repeater para mostrar el resumen.
            rptProductosSeleccionados.DataSource = carrito;
            rptProductosSeleccionados.DataBind();

            // Vuelve a calcular el cambio en el lado del cliente después de un postback
            // para reflejar el total actualizado.
            ScriptManager.RegisterStartupScript(this, this.GetType(), "recalcularCambio", "calcularCambio();", true);
        }

        /// <summary>
        /// Maneja los comandos de modificar cantidad o eliminar productos del carrito.
        /// Los comandos son "AumentarCantidad", "DisminuirCantidad" y "EliminarProducto".
        /// </summary>
        protected void btnModificarCantidad_Command(object sender, CommandEventArgs e)
        {
            int productoId = Convert.ToInt32(e.CommandArgument);
            List<DetalleVenta> carrito = listaProductosSeleccionados;
            DetalleVenta producto = carrito.FirstOrDefault(p => p.ProductoId == productoId);

            if (producto != null)
            {
                switch (e.CommandName)
                {
                    case "AumentarCantidad":
                        producto.Cantidad++;
                        break;
                    case "DisminuirCantidad":
                        if (producto.Cantidad > 1)
                        {
                            producto.Cantidad--;
                        }
                        else
                        {
                            // Si la cantidad es 1 y se disminuye, se elimina el producto del carrito.
                            carrito.Remove(producto);
                        }
                        break;
                    case "EliminarProducto":
                        // Elimina el producto del carrito directamente.
                        carrito.Remove(producto);
                        break;
                }
                // Actualiza el subtotal del producto después de cambiar la cantidad.
                if (producto != null && carrito.Contains(producto)) // Asegura que el producto todavía existe en el carrito
                {
                    producto.SubTotal = producto.Cantidad * producto.Producto.Precio;
                }

                listaProductosSeleccionados = carrito; // Actualiza la sesión.
                ActualizarResumenPedido(); // Refresca la UI.
            }
        }

        /// <summary>
        /// Evento para completar y registrar la venta en el sistema.
        /// Realiza validaciones y llama a la capa de negocio para guardar la venta.
        /// </summary>
        protected void btnCompletarVenta_Click(object sender, EventArgs e)
        {
            try
            {
                // Validar que haya productos seleccionados
                List<DetalleVenta> detallesVenta = listaProductosSeleccionados;
                if (detallesVenta == null || detallesVenta.Count == 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Debe seleccionar al menos un producto para completar la venta.');", true);
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
                CN_Venta cnVenta = new CN_Venta(); // Se usa una instancia de CN_Venta para obtener el ID de usuario
                idUsuarioParaVenta = cnVenta.ObtenerIdUsuario(nombreUsuarioSesion);

                if (idUsuarioParaVenta == 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Error: No se pudo obtener la identificación del usuario actual.');", true);
                    return;
                }

                // Validar y obtener el ID del método de pago
                if (!int.TryParse(ddlMetodoPago.SelectedValue, out int metodoPagoId) || metodoPagoId == 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Debe seleccionar un método de pago válido.');", true);
                    return;
                }

                // Validar y obtener el ID del cliente
                int clienteId = 0; // Default para cliente normal o si no se seleccionó comerciante
                if (rblCliente.SelectedValue == "Comerciante")
                {
                    if (!int.TryParse(ddlCliente.SelectedValue, out clienteId) || clienteId == 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Debe seleccionar un cliente comerciante válido.');", true);
                        return;
                    }
                }
                else
                {
                    // Si es cliente normal
                    clienteId = 1; // No se requiere ID de cliente para ventas normales
                }

                // Determinar si la venta es para llevar (0) o local (1)
                int enLocal = rbLocal.SelectedValue == "Local" ? 1 : 0;

                // Registrar la venta
                bool resultado = cnVenta.RegistrarVentas(
                                            enLocal,
                                            clienteId,
                                            idUsuarioParaVenta,
                                            metodoPagoId,
                                            detallesVenta);

                if (resultado)
                {
                    // Limpiar la sesión y redirigir si la venta fue exitosa.
                    Session.Remove("ProductosSeleccionados");
                    Response.Redirect("/Pages/Ventas/Ventas.aspx", false); // false para evitar ThreadAbortException
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Error al registrar la venta. Por favor, intente de nuevo.');", true);
                }
            }
            catch (Exception ex)
            {
                // Registra el error en el log del sistema para depuración.
                System.Diagnostics.Trace.WriteLine($"Error en btnCompletarVenta_Click: {ex}");
                string mensajeError = "Ocurrió un error inesperado al completar la venta. Por favor, contacte a soporte.";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", $"alert('{mensajeError.Replace("'", "\\'")}');", true);
            }
        }

        /// <summary>
        /// Evento que se dispara al hacer clic en el botón "Cancelar Venta".
        /// Limpia la sesión de productos seleccionados y redirige a la página principal de Ventas.
        /// </summary>
        protected void cancelarVenta_Click(object sender, EventArgs e)
        {
            Session.Remove("ProductosSeleccionados");
            Response.Redirect("/Pages/Ventas/Ventas.aspx", false); // false para evitar ThreadAbortException
        }
    }
}