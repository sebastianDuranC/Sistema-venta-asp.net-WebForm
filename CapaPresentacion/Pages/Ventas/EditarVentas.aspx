<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditarVentas.aspx.cs" Inherits="CapaPresentacion.Pages.Ventas.EditarVentas" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mx-auto rounded-lg bg-white p-4 shadow">
        <div class="mb-4 flex items-center justify-between">
            <h1 class="text-2xl font-bold text-primary">Editar Venta</h1>
            <a href="Ventas.aspx" class="rounded border border-secondary px-4 py-2 font-semibold text-secondary transition hover:bg-secondary hover:text-white">Cancelar</a>
        </div>

        <div class="mb-4 grid grid-cols-1 gap-4 md:grid-cols-2">
            <div>
                <label class="mb-1 block text-sm font-medium text-primary">Tipo de cliente</label>
                <asp:RadioButtonList ID="rblCliente" runat="server" RepeatDirection="Horizontal" CssClass="flex gap-2 text-primary" OnSelectedIndexChanged="rblCliente_SelectedIndexChanged" AutoPostBack="true">
                    <asp:ListItem Text="Normal" Value="Normal"></asp:ListItem>
                    <asp:ListItem Text="Comerciante" Value="Comerciante"></asp:ListItem>
                </asp:RadioButtonList>
            </div>
            <asp:Panel ID="pnlClienteComercial" runat="server" Visible="false" CssClass="mb-2 rounded-lg border border-gray-200 bg-gray-50 p-2">
                <div class="flex flex-col md:flex-row md:gap-2">
                    <div class="mb-2 flex-1 md:mb-0">
                        <label for="ddlClientes" class="mb-1 block text-xs font-medium text-primary">Cliente:</label>
                        <asp:DropDownList ID="ddlClientes" runat="server" CssClass="w-full rounded-md border border-gray-300 px-2 py-1 shadow-sm focus:outline-none focus:ring-primary focus:border-primary"></asp:DropDownList>
                    </div>
                </div>
            </asp:Panel>
            <div>
                <label for="ddlMetodoPago" class="mb-1 block text-xs font-medium text-primary">Método de Pago:</label>
                <asp:DropDownList ID="ddlMetodoPago" runat="server" CssClass="w-full rounded-md border border-gray-300 px-2 py-1 shadow-sm focus:outline-none focus:ring-primary focus:border-primary"></asp:DropDownList>
            </div>
        </div>
        <div class="mb-4 grid grid-cols-1 gap-4 md:grid-cols-2">
            <div>
                <label class="mb-1 block text-xs font-medium text-primary">Tipo de Venta:</label>
                <asp:RadioButtonList ID="rdbEnLocal" runat="server" RepeatDirection="Horizontal" CssClass="flex gap-2 text-primary">
                    <asp:ListItem Text="En Local" Value="Local" CssClass="inline-flex items-center"></asp:ListItem>
                    <asp:ListItem Text="Para Llevar" Value="Llevar" CssClass="inline-flex items-center"></asp:ListItem>
                </asp:RadioButtonList>
            </div>
            <div>
                <label for="lblFecha" class="mb-1 block text-xs font-medium text-primary">Fecha de Venta:</label>
                <asp:Label ID="lblFecha" runat="server" Text="Fecha: " CssClass="text-base font-semibold text-primary"></asp:Label>
            </div>
        </div>

        <div class="mb-4 rounded-lg bg-white shadow">
            <table class="min-w-full text-sm">
                <thead>
                    <tr class="bg-primary uppercase text-white">
                        <th class="px-3 py-2">Producto</th>
                        <th class="px-3 py-2">Cantidad</th>
                        <th class="px-3 py-2">Precio Unitario</th>
                        <th class="px-3 py-2">SubTotal</th>
                        <th class="px-3 py-2">Acciones</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rptDetalleVenta" runat="server" OnItemCommand="rptDetalleVenta_ItemCommand">
                        <ItemTemplate>
                            <tr class="border-b hover:bg-gray-50">
                                <td class="px-3 py-2">
                                    <asp:Label ID="lblProductoNombre" runat="server" Text='<%# Eval("Producto.Nombre") %>'></asp:Label>
                                    <asp:HiddenField ID="hdnProductoId" runat="server" Value='<%# Eval("ProductoId") %>' />
                                    <asp:HiddenField ID="hdnDetalleVentaId" runat="server" Value='<%# Eval("Id") %>' />
                                    <asp:HiddenField ID="hdnPrecioUnitario" runat="server" Value='<%# Eval("Producto.Precio") %>' />
                                </td>
                                <td class="px-3 py-2">
                                    <asp:TextBox ID="txtCantidad" runat="server" Text='<%# Eval("Cantidad") %>'
                                        CssClass="w-16 rounded border border-gray-300 px-1 py-1 text-center focus:border-primary focus:ring-primary"
                                        AutoPostBack="true" OnTextChanged="txtCantidad_TextChanged"></asp:TextBox>
                                </td>
                                <td class="px-3 py-2">
                                    <asp:Label ID="lblPrecioUnitario" runat="server" Text='<%# Eval("Producto.Precio", "{0:C2}") %>'></asp:Label>
                                </td>
                                <td class="px-3 py-2">
                                    <asp:Label ID="lblSubTotalDetalle" runat="server" Text='<%# Eval("SubTotal", "{0:C2}") %>'></asp:Label>
                                </td>
                                <td class="px-3 py-2">
                                    <asp:LinkButton ID="btnEliminar" runat="server" CommandName="EliminarDetalle" CommandArgument='<%# Container.ItemIndex %>'
                                        CssClass="text-secondary hover:underline font-bold">Eliminar</asp:LinkButton>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <tr class="border-b bg-gray-50 hover:bg-gray-100">
                                <td class="px-3 py-2">
                                    <asp:Label ID="lblProductoNombre" runat="server" Text='<%# Eval("Producto.Nombre") %>'></asp:Label>
                                    <asp:HiddenField ID="hdnProductoId" runat="server" Value='<%# Eval("ProductoId") %>' />
                                    <asp:HiddenField ID="hdnDetalleVentaId" runat="server" Value='<%# Eval("Id") %>' />
                                    <asp:HiddenField ID="hdnPrecioUnitario" runat="server" Value='<%# Eval("Producto.Precio") %>' />
                                </td>
                                <td class="px-3 py-2">
                                    <asp:TextBox ID="txtCantidad" runat="server" Text='<%# Eval("Cantidad") %>'
                                        CssClass="w-16 rounded border border-gray-300 px-1 py-1 text-center focus:border-primary focus:ring-primary"
                                        AutoPostBack="true" OnTextChanged="txtCantidad_TextChanged"></asp:TextBox>
                                </td>
                                <td class="px-3 py-2">
                                    <asp:Label ID="lblPrecioUnitario" runat="server" Text='<%# Eval("Producto.Precio", "{0:C2}") %>'></asp:Label>
                                </td>
                                <td class="px-3 py-2">
                                    <asp:Label ID="lblSubTotalDetalle" runat="server" Text='<%# Eval("SubTotal", "{0:C2}") %>'></asp:Label>
                                </td>
                                <td class="px-3 py-2">
                                    <asp:LinkButton ID="btnEliminar" runat="server" CommandName="EliminarDetalle" CommandArgument='<%# Container.ItemIndex %>'
                                        CssClass="text-secondary hover:underline font-bold">Eliminar</asp:LinkButton>
                                </td>
                            </tr>
                        </AlternatingItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
        </div>

        <div class="mb-4 grid grid-cols-2 gap-4">
            <div class="rounded bg-gray-50 p-3">
                <label for="lblSubtotal" class="block text-xs text-primary">Subtotal:</label>
                <asp:Label ID="lblSubtotal" runat="server" Text="0.00" CssClass="text-lg font-bold text-primary"></asp:Label>
            </div>
            <div class="rounded bg-gray-50 p-3">
                <label for="lblTotal" class="block text-xs text-primary">Total de Venta:</label>
                <asp:Label ID="lblTotal" runat="server" Text="0.00" CssClass="text-xl font-bold text-primary"></asp:Label>
            </div>
            <div class="rounded bg-gray-50 p-3">
                <label for="txtMontoCliente" class="block text-xs text-primary">Monto Pagado por Cliente:</label>
                <asp:TextBox ID="txtMontoCliente" runat="server" TextMode="Number"
                    CssClass="w-full rounded-md border border-gray-300 px-2 py-1 shadow-sm focus:outline-none focus:ring-primary focus:border-primary"
                    onkeyup="calcularCambio();" />
            </div>
            <div class="rounded bg-gray-50 p-3">
                <label for="lblCambio" class="block text-xs text-primary">Cambio:</label>
                <asp:Label ID="lblCambio" runat="server" Text="0.00" CssClass="text-lg font-bold text-secondary"></asp:Label>
            </div>
        </div>

        <div class="flex justify-end gap-2">
            <asp:Button ID="btnGuardar" runat="server" Text="Guardar Cambios" OnClick="btnGuardar_Click"
                CssClass="rounded bg-primary px-6 py-2 font-semibold text-white shadow transition hover:bg-gray-900" />
        </div>

        <asp:Button ID="btnCompletarVenta" runat="server" Text="Completar Venta" Visible="false"
            CssClass="hidden rounded bg-green-600 px-6 py-2 font-semibold text-white shadow hover:bg-green-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-green-500" />

        <asp:Panel ID="pnlMensaje" runat="server" Visible="false" CssClass="mt-4 rounded-md p-2 text-sm">
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
