<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditarVentas.aspx.cs" Inherits="CapaPresentacion.Pages.Ventas.EditarVentas" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mx-auto rounded-lg bg-white p-6 shadow-md">
        <h1 class="mb-6 text-3xl font-bold text-gray-800">Editar Venta</h1>

        <div class="mb-8 grid grid-cols-1 gap-6 md:grid-cols-2">
            <div>
                <label for="ddlClientes" class="mb-1 block text-sm font-medium text-gray-700">Cliente:</label>
                <asp:DropDownList ID="ddlClientes" runat="server" CssClass="w-full rounded-md border border-gray-300 px-3 py-2 shadow-sm focus:outline-none focus:ring-indigo-500 focus:border-indigo-500"></asp:DropDownList>
            </div>
            <div>
                <label for="ddlMetodoPago" class="mb-1 block text-sm font-medium text-gray-700">Método de Pago:</label>
                <asp:DropDownList ID="ddlMetodoPago" runat="server" CssClass="w-full rounded-md border border-gray-300 px-3 py-2 shadow-sm focus:outline-none focus:ring-indigo-500 focus:border-indigo-500"></asp:DropDownList>
            </div>
            <div class="md:col-span-2">
                <label class="mb-1 block text-sm font-medium text-gray-700">Tipo de Venta:</label>
                <asp:RadioButtonList ID="rdbEnLocal" runat="server" RepeatDirection="Horizontal" CssClass="flex space-x-4">
                    <asp:ListItem Text="En Local" Value="Local" CssClass="inline-flex items-center"></asp:ListItem>
                    <asp:ListItem Text="Para Llevar" Value="Llevar" CssClass="inline-flex items-center"></asp:ListItem>
                </asp:RadioButtonList>
            </div>
            <div class="md:col-span-2">
                <label for="lblFecha" class="mb-1 block text-sm font-medium text-gray-700">Fecha de Venta:</label>
                <asp:Label ID="lblFecha" runat="server" Text="Fecha: " CssClass="text-lg font-semibold text-gray-900"></asp:Label>
            </div>
        </div>

        <div class="mb-8">
            <h2 class="mb-4 text-2xl font-bold text-gray-800">Productos en la Venta</h2>
            <div class="overflow-hidden rounded-lg bg-white shadow-md">
                <table class="min-w-full leading-normal">
                    <thead>
                        <tr class="bg-gray-200 text-sm uppercase leading-normal text-gray-700">
                            <th class="px-6 py-3 text-left">Producto</th>
                            <th class="px-6 py-3 text-left">Cantidad</th>
                            <th class="px-6 py-3 text-left">Precio Unitario</th>
                            <th class="px-6 py-3 text-left">SubTotal</th>
                            <th class="px-6 py-3 text-left">Acciones</th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater ID="rptDetalleVenta" runat="server" OnItemCommand="rptDetalleVenta_ItemCommand">
                            <ItemTemplate>
                                <tr class="border-b border-gray-200 hover:bg-gray-100">
                                    <td class="whitespace-nowrap px-6 py-3 text-left">
                                        <asp:Label ID="lblProductoNombre" runat="server" Text='<%# Eval("Producto.Nombre") %>'></asp:Label>
                                        <asp:HiddenField ID="hdnProductoId" runat="server" Value='<%# Eval("ProductoId") %>' />
                                        <asp:HiddenField ID="hdnDetalleVentaId" runat="server" Value='<%# Eval("Id") %>' />
                                        <asp:HiddenField ID="hdnPrecioUnitario" runat="server" Value='<%# Eval("Producto.Precio") %>' />
                                    </td>
                                    <td class="px-6 py-3 text-left">
                                        <asp:TextBox ID="txtCantidad" runat="server" Text='<%# Eval("Cantidad") %>'
                                            CssClass="w-20 rounded border border-gray-300 px-2 py-1 text-center"
                                            AutoPostBack="true" OnTextChanged="txtCantidad_TextChanged"></asp:TextBox>
                                    </td>
                                    <td class="px-6 py-3 text-left">
                                        <asp:Label ID="lblPrecioUnitario" runat="server" Text='<%# Eval("Producto.Precio", "{0:C2}") %>'></asp:Label>
                                    </td>
                                    <td class="px-6 py-3 text-left">
                                        <asp:Label ID="lblSubTotalDetalle" runat="server" Text='<%# Eval("SubTotal", "{0:C2}") %>'></asp:Label>
                                    </td>
                                    <td class="px-6 py-3 text-left">
                                        <asp:LinkButton ID="btnEliminar" runat="server" CommandName="EliminarDetalle" CommandArgument='<%# Container.ItemIndex %>'
                                            CssClass="text-red-600 hover:text-red-900 font-bold">Eliminar</asp:LinkButton>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <AlternatingItemTemplate>
                                <tr class="border-b border-gray-200 bg-gray-50 hover:bg-gray-100">
                                    <td class="whitespace-nowrap px-6 py-3 text-left">
                                        <asp:Label ID="lblProductoNombre" runat="server" Text='<%# Eval("Producto.Nombre") %>'></asp:Label>
                                        <asp:HiddenField ID="hdnProductoId" runat="server" Value='<%# Eval("ProductoId") %>' />
                                        <asp:HiddenField ID="hdnDetalleVentaId" runat="server" Value='<%# Eval("Id") %>' />
                                        <asp:HiddenField ID="hdnPrecioUnitario" runat="server" Value='<%# Eval("Producto.Precio") %>' />
                                    </td>
                                    <td class="px-6 py-3 text-left">
                                        <asp:TextBox ID="txtCantidad" runat="server" Text='<%# Eval("Cantidad") %>'
                                            CssClass="w-20 rounded border border-gray-300 px-2 py-1 text-center"
                                            AutoPostBack="true" OnTextChanged="txtCantidad_TextChanged"></asp:TextBox>
                                    </td>
                                    <td class="px-6 py-3 text-left">
                                        <asp:Label ID="lblPrecioUnitario" runat="server" Text='<%# Eval("Producto.Precio", "{0:C2}") %>'></asp:Label>
                                    </td>
                                    <td class="px-6 py-3 text-left">
                                        <asp:Label ID="lblSubTotalDetalle" runat="server" Text='<%# Eval("SubTotal", "{0:C2}") %>'></asp:Label>
                                    </td>
                                    <td class="px-6 py-3 text-left">
                                        <asp:LinkButton ID="btnEliminar" runat="server" CommandName="EliminarDetalle" CommandArgument='<%# Container.ItemIndex %>'
                                            CssClass="text-red-600 hover:text-red-900 font-bold">Eliminar</asp:LinkButton>
                                    </td>
                                </tr>
                            </AlternatingItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>
            </div>
        </div>

        <div class="mb-8 grid grid-cols-1 gap-6 rounded-lg bg-gray-50 p-4 md:grid-cols-2">
            <div>
                <label for="lblSubtotal" class="mb-1 block text-sm font-medium text-gray-700">Subtotal:</label>
                <asp:Label ID="lblSubtotal" runat="server" Text="0.00" CssClass="text-xl font-bold text-gray-900"></asp:Label>
            </div>
            <div>
                <label for="lblTotal" class="mb-1 block text-sm font-medium text-gray-700">Total de Venta:</label>
                <asp:Label ID="lblTotal" runat="server" Text="0.00" CssClass="text-2xl font-bold text-indigo-600"></asp:Label>
            </div>
            <div>
                <label for="txtMontoCliente" class="mb-1 block text-sm font-medium text-gray-700">Monto Pagado por Cliente:</label>
                <asp:TextBox ID="txtMontoCliente" runat="server" TextMode="Number"
                    CssClass="w-full rounded-md border border-gray-300 px-3 py-2 shadow-sm focus:outline-none focus:ring-indigo-500 focus:border-indigo-500"
                    onkeyup="calcularCambio();" />
            </div>
            <div>
                <label for="lblCambio" class="mb-1 block text-sm font-medium text-gray-700">Cambio:</label>
                <asp:Label ID="lblCambio" runat="server" Text="0.00" CssClass="text-xl font-bold text-green-600"></asp:Label>
            </div>
        </div>

        <div class="flex justify-end space-x-4">
            <asp:Button ID="btnGuardar" runat="server" Text="Guardar Cambios" OnClick="btnGuardar_Click"
                CssClass="rounded-md bg-indigo-600 px-6 py-3 font-semibold text-white shadow-md hover:bg-indigo-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-indigo-500" />
            <a href="Ventas.aspx" class="rounded-md bg-gray-300 px-6 py-3 font-semibold text-gray-800 shadow-md hover:bg-gray-400 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-gray-500">Cancelar</a>
        </div>

        <asp:Button ID="btnCompletarVenta" runat="server" Text="Completar Venta" Visible="false"
            CssClass="hidden rounded-md bg-green-600 px-6 py-3 font-semibold text-white shadow-md hover:bg-green-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-green-500" />

        <asp:Panel ID="pnlMensaje" runat="server" Visible="false" CssClass="mt-6 rounded-md p-4 text-sm">
            <asp:Label ID="lblMensaje" runat="server" CssClass="font-medium"></asp:Label>
        </asp:Panel>
    </div>

    <script type="text/javascript">
        // Función para calcular el cambio a devolver al cliente
        function calcularCambio() {
            var totalElement = document.getElementById('<%= lblTotal.ClientID %>');
            var totalStrRaw = totalElement.textContent || totalElement.innerText;

            // Limpiar la cadena del total: eliminar "Bs.", espacios, y manejar el separador decimal
            var sTotalCleaned = totalStrRaw.replace(/Bs\.|\s/g, ''); // Elimina "Bs." y espacios
            var totalNumericStr;

            // Detectar y normalizar el formato de los números (coma como decimal o punto como decimal)
            // Esto es crucial para manejar diferentes configuraciones regionales
            if (sTotalCleaned.includes(',') && sTotalCleaned.includes('.')) {
                // Si ambos están presentes, asumimos formato europeo (punto como separador de miles, coma como decimal)
                if (sTotalCleaned.lastIndexOf(',') > sTotalCleaned.lastIndexOf('.')) {
                    totalNumericStr = sTotalCleaned.replace(/\./g, '').replace(',', '.'); // Elimina puntos de miles y cambia coma por punto
                } else {
                    // Si el punto está después de la coma, asumimos formato anglosajón
                    totalNumericStr = sTotalCleaned.replace(/,/g, ''); // Elimina comas de miles
                }
            } else if (sTotalCleaned.includes(',')) {
                // Si solo hay coma, asumimos coma como decimal
                totalNumericStr = sTotalCleaned.replace(',', '.');
            } else {
                // Si solo hay punto o ningún separador, se usa tal cual
                totalNumericStr = sTotalCleaned;
            }

            var total = parseFloat(totalNumericStr) || 0; // Convertir a número flotante

            var pagoStr = document.getElementById('<%= txtMontoCliente.ClientID %>').value;
            // Reemplazar coma por punto para asegurar un parseo correcto en JavaScript
            var sPagoCleaned = pagoStr.replace(',', '.'); 
            var pago = parseFloat(sPagoCleaned) || 0; // Convertir a número flotante

            var cambio = pago - total; // Calcular el cambio

            if (isNaN(cambio)) cambio = 0; // Si el resultado no es un número, establecerlo en 0

            // Mostrar el cambio formateado con 2 decimales y el prefijo "Bs. "
            document.getElementById('<%= lblCambio.ClientID %>').textContent = 'Bs. ' + cambio.toFixed(2);

            // Habilitar/deshabilitar botón de completar venta (si existiera en la página)
            // En EditarVentas, este botón podría no ser necesario, pero se mantiene para consistencia.
            var btnCompletar = document.getElementById('<%= btnCompletarVenta.ClientID %>');
            if (btnCompletar) {
                // El botón se habilita si el pago es suficiente y hay un total positivo en la venta
                btnCompletar.disabled = pago < total || total <= 0;
            }
        }

        // Llamar a calcularCambio al cargar la página para inicializar el cambio si ya hay valores
        document.addEventListener('DOMContentLoaded', function () {
            calcularCambio();
        });
    </script>
</asp:Content>