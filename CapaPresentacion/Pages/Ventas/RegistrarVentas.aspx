<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RegistrarVentas.aspx.cs" Inherits="CapaPresentacion.Pages.Ventas.RegistrarVentas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .rbl input[type="radio"] {
            accent-color: #2563eb;
            width: 1.1em;
            height: 1.1em;
            margin-right: 0.5em;
            vertical-align: middle;
        }

        .rbl label {
            font-size: 1rem;
            color: #374151;
            margin-right: 1.5em;
            cursor: pointer;
        }
    </style>
    <div>
        <div class="flex items-center justify-between py-2">
            <h1 class="text-2xl font-bold">Nueva Venta</h1>
            <asp:Button Text="Volver" runat="server" ID="cancelarVenta" class="cursor-pointer rounded-lg bg-primary p-2 text-white hover:bg-gray-700" OnClick="cancelarVenta_Click" />
        </div>
        <div class="flex flex-col gap-4 lg:flex-row">
            <div class="w-full rounded bg-white p-4 shadow lg:w-2/3">
                <h2 class="mb-4 text-xl font-semibold">Productos</h2>
                <div class="mb-4">
                    <input type="text" id="buscarProductos" placeholder="Buscar productos..." class="w-full rounded-md border px-3 py-2" onkeyup="filtrarProductos()">
                </div>
                <%--<div class="mb-4 flex space-x-2">
                    <button class="rounded-md bg-secondary px-4 py-2 text-white">Todos</button>
                    <button class="rounded-md bg-gray-200 px-4 py-2">Platos</button>
                    <button class="rounded-md bg-gray-200 px-4 py-2">Bebidas</button>
                    <button class="rounded-md bg-gray-200 px-4 py-2">Complementos</button>
                </div>--%>
                <div class="max-h-[calc(100vh-350px)] grid grid-cols-2 gap-4 overflow-y-auto md:grid-cols-3 lg:grid-cols-4" id="listaProductos">
                    <asp:Repeater ID="rptProductos" runat="server">
                        <ItemTemplate>
                            <div class="producto rounded-lg border p-4 text-center">
                                <asp:HiddenField ID="idProducto" runat="server" Value='<%# Eval("Id") %>' />
                                <img src='<%# Eval("FotoUrl") %>' alt='<%# Eval("Nombre") %>' class="mx-auto h-32 rounded-lg bg-contentbg" />
                                <h3 class="font-semibold"><%# Eval("Nombre") %></h3>
                                <p class="text-gray-600">Bs. <%# Eval("Precio", "{0:N2}") %></p>
                                <asp:Button Text="+" runat="server" class="mt-2 rounded-full border px-3 py-1" data-producto-id='<%# Eval("Id") %>' ID="btnAgregarProducto" OnClick="btnAgregarProducto_Click" />
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
            <div class="w-full rounded bg-white p-4 shadow lg:w-1/3">
                <h2 class="mb-4 text-xl font-semibold">Resumen de Pedido</h2>
                <div class="mb-4 flex flex-col gap-4 md:flex-row md:items-end">
                    <div class="flex-1">
                        <label class="mb-1 block text-lg font-medium text-gray-700">Tipo de pedido</label>
                        <asp:RadioButtonList ID="rbLocal" runat="server" RepeatDirection="Horizontal" CssClass="rbl flex gap-4">
                            <asp:ListItem Text="Llevar" Value="Llevar"></asp:ListItem>
                            <asp:ListItem Text="Local" Value="Local" Selected="True"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                    <div class="flex-1">
                        <label class="mb-1 block text-lg font-medium text-gray-700">Tipo de cliente</label>
                        <asp:RadioButtonList ID="rblCliente" runat="server" RepeatDirection="Horizontal" CssClass="rbl flex gap-4" OnSelectedIndexChanged="rblCliente_SelectedIndexChanged" AutoPostBack="true">
                            <asp:ListItem Text="Normal" Value="Normal" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="Comerciante" Value="Comerciante"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                </div>
                <asp:Panel ID="pnlClienteComercial" runat="server" Visible="true" CssClass="mb-4 rounded-lg border border-gray-200 bg-gray-50 p-4">
                    <div class="flex flex-col md:flex-row md:gap-4">
                        <div class="mb-3 flex-1 md:mb-0">
                            <label class="mb-1 block text-sm font-medium text-gray-700">Nombre Cliente:</label>
                            <asp:DropDownList ID="ddlCliente" runat="server" CssClass="form-select w-full rounded-md border px-3 py-2 text-sm text-primary focus:ring-primary" OnSelectedIndexChanged="ddlCliente_SelectedIndexChanged" AutoPostBack="true">
                            </asp:DropDownList>
                        </div>
                        <div class="flex items-end gap-3">
                            <div>
                                <label class="mb-1 block text-sm font-medium text-gray-700" for="<%= txtNumeroLocalCliente.ClientID %>">Número Local:</label>
                                <asp:TextBox ID="txtNumeroLocalCliente" runat="server" CssClass="form-control w-20 rounded-md border px-2 py-1 text-center" MaxLength="2" placeholder="1-20" ReadOnly="true"></asp:TextBox>
                            </div>
                            <div>
                                <label class="mb-1 block text-sm font-medium text-gray-700" for="<%= txtPasilloCliente.ClientID %>">Pasillo:</label>
                                <asp:TextBox ID="txtPasilloCliente" runat="server" CssClass="form-control w-16 rounded-md border px-2 py-1 text-center uppercase" MaxLength="1" placeholder="A-Z" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
                <div class="mb-4">
                    <label for="<%= ddlMetodoPago.ClientID %>" class="block text-lg font-medium text-gray-700">Método de pago</label>
                    <asp:DropDownList ID="ddlMetodoPago" runat="server" CssClass="form-select w-full rounded-md border px-3 py-2 text-sm text-primary focus:ring-primary">
                    </asp:DropDownList>
                </div>
                <div class="mb-4 space-y-4">
                    <asp:Repeater ID="rptProductosSeleccionados" runat="server">
                        <ItemTemplate>
                            <div class="flex items-center justify-between border-b pb-2">
                                <div>
                                    <p class="font-semibold"><%# Eval("Producto.Nombre") %></p>
                                    <p class="text-sm text-gray-600">Bs. <%# Eval("Producto.Precio", "{0:N2}") %> x <%# Eval("Cantidad") %></p>
                                </div>
                                <div class="flex items-center space-x-2">
                                    <asp:Button Text="-" runat="server" CssClass="rounded border px-2 py-1"
                                        CommandName="DisminuirCantidad"
                                        CommandArgument='<%# Eval("ProductoId") %>'
                                        OnCommand="btnModificarCantidad_Command" />
                                    <span><%# Eval("Cantidad") %></span>
                                    <asp:Button Text="+" runat="server" CssClass="rounded border px-2 py-1"
                                        CommandName="AumentarCantidad"
                                        CommandArgument='<%# Eval("ProductoId") %>'
                                        OnCommand="btnModificarCantidad_Command" />
                                    <asp:Button Text="×" runat="server" CssClass="text-red-500"
                                        CommandName="EliminarProducto"
                                        CommandArgument='<%# Eval("ProductoId") %>'
                                        OnCommand="btnModificarCantidad_Command" />
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
                <div class="mb-4 space-y-2">
                    <div class="flex justify-between">
                        <span>Subtotal</span>
                        <asp:Label ID="lblSubtotal" runat="server" Text="Bs. 0.00" />
                    </div>
                    <div class="flex justify-between font-bold">
                        <span>Total</span>
                        <asp:Label ID="lblTotal" runat="server" Text="Bs. 0.00" />
                    </div>
                </div>
                <div class="mb-4 space-y-2">
                    <div class="flex items-center justify-between">
                        <span>Pago del Cliente</span>
                        <asp:TextBox ID="txtPagoCliente" runat="server" CssClass="w-32 rounded-md border px-3 py-2 text-right"
                            onkeyup="calcularCambio()" autocomplete="off" />
                    </div>
                    <div class="flex items-center justify-between">
                        <span>Cambio</span>
                        <asp:Label ID="lblCambio" runat="server" Text="Bs. 0.00" CssClass="font-bold" />
                    </div>
                </div>
                <div class="flex flex-col space-y-2">
                    <asp:Button ID="btnCompletarVenta" runat="server" Text="Completar Venta"
                        CssClass="rounded-md bg-primary py-2 text-white"
                        OnClick="btnCompletarVenta_Click" />
                    <asp:Button ID="btnImprimirComanda" runat="server" Text="Imprimir Comanda"
                        CssClass="rounded-md bg-contentbg py-2 text-gray-800" />
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        // Función para filtrar productos en la lista
        function filtrarProductos() {
            var input = document.getElementById('buscarProductos');
            var filtro = input.value.toLowerCase();
            var productosContainer = document.getElementById('listaProductos');
            if (productosContainer) {
                var productos = productosContainer.querySelectorAll('.producto');
                productos.forEach(function (producto) {
                    var nombreElement = producto.querySelector('h3');
                    if (nombreElement) {
                        var nombre = nombreElement.textContent.toLowerCase();
                        if (nombre.includes(filtro)) {
                            producto.style.display = "";
                        } else {
                            producto.style.display = "none";
                        }
                    }
                });
            }
        }

        // Función para calcular el cambio a devolver al cliente
        function calcularCambio() {
            var totalElement = document.getElementById('<%= lblTotal.ClientID %>');
            var totalStrRaw = totalElement.textContent || totalElement.innerText;

            // Limpiar la cadena del total y convertir a un formato numérico estándar
            var sTotalCleaned = totalStrRaw.replace(/Bs\.|\s/g, '');
            var totalNumericStr;

            // Detectar el formato de los números (coma como decimal o punto como decimal)
            if (sTotalCleaned.includes(',') && sTotalCleaned.includes('.')) {
                if (sTotalCleaned.lastIndexOf(',') > sTotalCleaned.lastIndexOf('.')) {
                    totalNumericStr = sTotalCleaned.replace(/\./g, '').replace(',', '.');
                } else {
                    totalNumericStr = sTotalCleaned.replace(/,/g, '');
                }
            } else if (sTotalCleaned.includes(',')) {
                totalNumericStr = sTotalCleaned.replace(',', '.');
            } else {
                totalNumericStr = sTotalCleaned;
            }

            var total = parseFloat(totalNumericStr) || 0;

            var pagoStr = document.getElementById('<%= txtPagoCliente.ClientID %>').value;
            var sPagoCleaned = pagoStr.replace(',', '.');
            var pago = parseFloat(sPagoCleaned) || 0;

            var cambio = pago - total;

            if (isNaN(cambio)) cambio = 0;

            document.getElementById('<%= lblCambio.ClientID %>').textContent = 'Bs. ' + cambio.toFixed(2);

            // Habilitar/deshabilitar botón de completar venta
            var btnCompletar = document.getElementById('<%= btnCompletarVenta.ClientID %>');
            if (btnCompletar) {
                btnCompletar.disabled = pago < total || total <= 0;
            }
        }
    </script>
</asp:Content>
