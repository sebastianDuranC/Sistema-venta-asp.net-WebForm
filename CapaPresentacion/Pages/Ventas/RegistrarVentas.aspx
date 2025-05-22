<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RegistrarVentas.aspx.cs" Inherits="CapaPresentacion.Pages.Ventas.RegistrarVentas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .rbl input[type="radio"] {
            margin-left: 5px;
        }
    </style>
    <div>
        <div class="flex items-center justify-between py-2">
            <h1 class="text-2xl font-bold">Nueva Venta</h1>
            <asp:Button Text="Cancelar" runat="server" ID="cancelarVenta" class="cursor-pointer rounded-lg bg-primary p-2 text-white hover:bg-gray-700" OnClick="cancelarVenta_Click"
                />
        </div>
        <div class="flex flex-col gap-4 lg:flex-row">
            <div class="w-full rounded bg-white p-4 shadow lg:w-2/3">
                <h2 class="mb-4 text-xl font-semibold">Productos</h2>
                <!-- Busqueda por filtro -->
                <div class="mb-4">
                    <input type="text" id="buscarProductos" placeholder="Buscar productos..." class="w-full rounded-md border px-3 py-2" onkeyup="filtrarProductos()">
                </div>
                <!-- Categorias -->
                <div class="mb-4 flex space-x-2">
                    <button class="rounded-md bg-blue-500 px-4 py-2 text-white">Todos</button>
                    <button class="rounded-md bg-gray-200 px-4 py-2">Platos</button>
                    <button class="rounded-md bg-gray-200 px-4 py-2">Bebidas</button>
                    <button class="rounded-md bg-gray-200 px-4 py-2">Complementos</button>
                </div>
                <!-- Productos -->
                <div class="max-h-[calc(100vh-350px)] grid grid-cols-2 gap-4 overflow-y-auto md:grid-cols-3 lg:grid-cols-4" id="listaProductos">
                    <asp:Repeater ID="rptProductos" runat="server">
                        <ItemTemplate>
                            <div class="producto rounded-lg border p-4 text-center">
                                <img src='<%# Eval("FotoUrl") %>' alt='<%# Eval("Nombre") %>' class="mx-auto h-32 rounded-lg bg-contentbg" />
                                <h3 class="font-semibold"><%# Eval("Nombre") %></h3>
                                <p class="text-gray-600">Bs. <%# Eval("Precio", "{0:N2}") %></p>
                                <asp:Button Text="+" runat="server" class="mt-2 rounded-full border px-3 py-1" data-producto-id='<%# Eval("Id") %>' ID="btnAgregarProducto" OnClick="btnAgregarProducto_Click"/>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>

            <!-- Resumen de Pedido -->
            <div class="w-full rounded bg-white p-4 shadow lg:w-1/3">
                <h2 class="mb-4 text-xl font-semibold">Resumen de Pedido</h2>
                <!-- Local o para llevar -->
                <div class="mb-4">
                    <label for="rblCliente" class="block text-lg font-medium text-gray-700">Cliente</label>
                    <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal" CssClass="rbl">
                        <asp:ListItem Text="Llevar" Value="Llevar"></asp:ListItem>
                        <asp:ListItem Text="Local" Value="Local" Selected="True"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
                <!-- Cliente -->
                <div class="mb-4">
                    <label for="rblCliente" class="block text-lg font-medium text-gray-700">Cliente</label>
                    <asp:RadioButtonList ID="rblCliente" runat="server" RepeatDirection="Horizontal" CssClass="rbl">
                        <asp:ListItem Text="Normal" Value="Normal"></asp:ListItem>
                        <asp:ListItem Text="Comerciante" Value="Comerciante" Selected="True"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
                <!-- Metodo de pago -->
                <div class="mb-4">
                    <label for="ddlMetodoPago" class="block text-lg font-medium text-gray-700">Método de pago</label>
                    <asp:DropDownList ID="ddlMetodoPago" runat="server" CssClass="form-select w-full rounded-md border px-3 py-2 text-sm text-primary focus:ring-primary">
                    </asp:DropDownList>
                </div>
                <!-- Productos -->
                <div class="mb-4 space-y-4">
                    <!-- Lista de productos -->
                    <div class="flex items-center justify-between border-b pb-2">
                        <div>
                            <p class="font-semibold">1/2 Pollo</p>
                            <p class="text-sm text-gray-600">Bs. 29.90 x <asp:Label Text="" runat="server" ID="lblCantidadProducto"/> </p>
                        </div>
                        <div class="flex items-center space-x-2">
                            <button class="rounded border px-2 py-1">-</button>
                            <span>2</span>
                            <button class="rounded border px-2 py-1">+</button>
                            <button class="text-red-500"><i class="fas fa-trash"></i></button>
                        </div>
                    </div>
                </div>
                <!-- Total -->
                <div class="mb-4 space-y-2">
                    <div class="flex justify-between">
                        <span>Subtotal</span>
                        <span>Bs. 115.70</span>
                    </div>
                    <div class="flex justify-between font-bold">
                        <span>Total</span>
                        <span>Bs. 136.53</span>
                    </div>
                </div>
                <div class="flex flex-col space-y-2">
                    <button class="rounded-md bg-primary py-2 text-white">Completar Venta</button>
                    <button class="rounded-md bg-contentbg py-2 text-gray-800">Imprimir Comanda</button>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        function filtrarProductos() {
            var input = document.getElementById('buscarProductos');
            var filtro = input.value.toLowerCase();
            var productos = document.querySelectorAll('#listaProductos .producto');
            productos.forEach(function (producto) {
                var nombre = producto.querySelector('h3').textContent.toLowerCase();
                if (nombre.indexOf(filtro) > -1) {
                    producto.style.display = "";
                } else {
                    producto.style.display = "none";
                }
            });
        }
    </script>
</asp:Content>
