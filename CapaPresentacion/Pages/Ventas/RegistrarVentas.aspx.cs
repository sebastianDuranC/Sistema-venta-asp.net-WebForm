using CapaEntidades;
using CapaNegocio;
using System;
using System.Collections.Generic;
using System.Data;
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

        VentaBLL cnVenta = new VentaBLL();
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
            // Cargar la lista de clientes
            CargarClientesYMetodosPago();
        }

        /// <summary>
        /// Carga la lista de todos los productos disponibles desde la capa de negocio
        /// y los enlaza al Repeater `rptProductos` para su visualización.
        /// </summary>
        private void CargarProductosDisponibles()
        {
            ProductoBLL objProductos = new ProductoBLL();
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
            ClienteBLL cN_Cliente = new ClienteBLL();
            List<Cliente> listaClientes = cN_Cliente.ObtenerClientes();

            // Filtrar usando LINQ
            List<Cliente> clientesFiltrados = new List<Cliente>();

            if (rblCliente.SelectedValue == "Comerciante")
            {
                clientesFiltrados = listaClientes.Where(c => c.EsComerciante).ToList();
            }
            else if (rblCliente.SelectedValue == "Normal")
            {
                clientesFiltrados = listaClientes.Where(c => !c.EsComerciante).ToList();
            }

            ddlCliente.DataSource = clientesFiltrados;
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
            MetodoPagoBLL objMetodoPago = new MetodoPagoBLL();
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
                ProductoBLL objProductos = new ProductoBLL();
                DataTable objetoProducto = objProductos.ObtenerProductoPorId(productoId);
                Producto producto = (from row in objetoProducto.AsEnumerable()
                                     select new Producto()
                                     {
                                         Id = Convert.ToInt32(row["Id"]),
                                         Nombre = row["Nombre"].ToString(),
                                         Precio = Convert.ToDecimal(row["Precio"]),
                                         Stock = row["Stock"] != DBNull.Value ? (int?)Convert.ToInt32(row["Stock"]) : null
                                     }).FirstOrDefault();
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
            filtrarClientes();
        }

        private void filtrarClientes()
        {
            ClienteBLL cN_Cliente = new ClienteBLL();
            List<Cliente> listaClientes = cN_Cliente.ObtenerClientes();

            // Filtrar usando LINQ
            List<Cliente> clientesFiltrados = new List<Cliente>();

            if (rblCliente.SelectedValue == "Comerciante")
            {
                clientesFiltrados = listaClientes.Where(c => c.EsComerciante).ToList();
            }
            else if (rblCliente.SelectedValue == "Normal")
            {
                clientesFiltrados = listaClientes.Where(c => !c.EsComerciante).ToList();
            }

            ddlCliente.DataSource = clientesFiltrados;
            ddlCliente.DataTextField = "Nombre";
            ddlCliente.DataValueField = "Id";
            ddlCliente.DataBind();

            if (ddlCliente.Items.Count > 0)
            {
                ddlCliente.Items.Insert(0, new ListItem("-- Seleccione --", "0"));
                ddlCliente.SelectedValue = "0"; // Asegura que el valor por defecto sea el seleccionado
            }
            else if (ddlCliente.Items.Count == 0)
            {
                ddlCliente.Items.Add(new ListItem("-- No hay Clientes --", "0"));
            }
        }

        protected void ddlCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (int.TryParse(ddlCliente.SelectedValue, out int clienteId) && clienteId > 0)
            {
                ClienteBLL cN_Cliente = new ClienteBLL();
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
                string nombreUsuarioSesion = Session["usuario"].ToString();

                // Registrar la venta
                Venta venta = new Venta
                {
                    EnLocal = rbLocal.SelectedValue == "Local",
                    ClienteId = int.TryParse(ddlCliente.SelectedValue, out int clienteId) ? clienteId : (int?)null,
                    UsuarioId = cnVenta.ObtenerIdUsuario(nombreUsuarioSesion),
                    MetodoPagoId = int.TryParse(ddlMetodoPago.SelectedValue, out int metodoPagoId) ? metodoPagoId : 0,
                    MontoRecibido = decimal.TryParse(txtPagoCliente.Text.Replace(',', '.'), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out decimal montoRecibido) ? montoRecibido : 0
                };

                List<DetalleVenta> detallesVenta = listaProductosSeleccionados;
                bool resultado = cnVenta.RegistrarVentas(venta,detallesVenta);
                if (resultado)
                {
                    // Limpiar la sesión y redirigir si la venta fue exitosa.
                    ShowToast("Venta exitosa","success");
                    Session.Remove("ProductosSeleccionados");
                    LimpiarInputs();
                }
                else
                {
                    ShowToast("Error al registrar la venta","error");
                }
            }
            catch (Exception ex)
            {
                ShowToast(ex.Message, "error");
            }
        }

        private void LimpiarInputs()
        {
            //limpiar tiodos los campos de entrada y controles de la página
            txtPagoCliente.Text = string.Empty;
            txtNumeroLocalCliente.Text = string.Empty;
            txtPasilloCliente.Text = string.Empty;
            ddlCliente.SelectedIndex = 0; // Selecciona el primer item por defecto
            ddlMetodoPago.SelectedIndex = 0; // Selecciona el primer item por defecto
            rblCliente.SelectedIndex = 0; // Selecciona el primer item por defecto
            rbLocal.SelectedIndex = 0; // Selecciona el primer item por defecto
            listaProductosSeleccionados = new List<DetalleVenta>(); // Reinicia la lista de productos seleccionados
            rptProductosSeleccionados.DataSource = null; // Limpia el Repeater de productos seleccionados
            rptProductosSeleccionados.DataBind(); // Vuelve a enlazar el Repeater para que esté vacío
            lblSubtotal.Text = "Bs. 0.00"; // Reinicia el subtotal
            lblTotal.Text = "Bs. 0.00"; // Reinicia el total
        }

        private void ShowToast(string titulo, string icono)
        {
            // Escapamos las comillas simples para evitar errores de JavaScript
            string safeTitle = titulo.Replace("'", "\\'");
            string script = $"Swal.fire({{ " +
                $"position: 'top-end'," +
                $" icon: '{icono}'," +
                $" title: '{safeTitle}'," +
                $" showConfirmButton: false," +
                $" timer: 2500," +
                $" toast: true}});";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowToastScript", script, true);
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