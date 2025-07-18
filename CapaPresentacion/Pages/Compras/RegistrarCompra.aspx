<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RegistrarCompra.aspx.cs" Inherits="CapaPresentacion.Pages.Compras.RegistrarCompra" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <main class="w-full px-4 py-8">
        
        <!-- Encabezado Principal -->
        <div class="mb-6 flex items-center justify-between">
            <h1 class="text-3xl font-bold text-primary">Registrar Nueva Compra</h1>
            <asp:Button ID="btnCancelarCompra" runat="server" Text="Volver" OnClick="btnCancelarCompra_Click" 
                CssClass="cursor-pointer rounded-lg bg-secondary px-5 py-2.5 text-center text-sm font-medium text-white shadow-sm transition-all hover:bg-secondary/70" />
        </div>

        <!-- Contenedor de dos columnas -->
        <div class="grid grid-cols-1 gap-8 lg:grid-cols-5">

            <!-- Columna Izquierda: Selección de Insumos -->
            <div class="lg:col-span-3">
                <div class="rounded-xl border border-gray-200 bg-white p-6 shadow-lg">
                    <h2 class="mb-2 text-xl font-bold text-primary">1. Datos de la Compra</h2>
                    <p class="mb-6 text-sm text-gray-500">Selecciona el proveedor y añade los insumos que estás comprando.</p>
                    
                    <!-- Selección de Proveedor -->
                    <div class="mb-6">
                        <label for="<%= ddlProveedor.ClientID %>" class="mb-2 block text-sm font-medium text-gray-700">Proveedor:</label>
                        <asp:DropDownList ID="ddlProveedor" runat="server"
                            CssClass="block w-full rounded-md border-gray-300 px-3 py-2 text-gray-800 shadow-sm focus:border-primary focus:outline-none focus:ring-1 focus:ring-primary">
                        </asp:DropDownList>
                    </div>

                    <!-- Formulario para Añadir Insumo -->
                    <div class="rounded-lg border border-dashed border-gray-300 bg-gray-50 p-4">
                        <div class="grid grid-cols-1 gap-4 sm:grid-cols-12">
                            <div class="sm:col-span-5">
                                <label for="<%= ddlInsumos.ClientID %>" class="mb-1 block text-xs font-medium text-gray-600">Insumo:</label>
                                <asp:DropDownList ID="ddlInsumos" runat="server"
                                    CssClass="block w-full rounded-md border-gray-300 px-3 py-2 text-sm text-gray-800 shadow-sm focus:border-primary focus:outline-none focus:ring-1 focus:ring-primary">
                                </asp:DropDownList>
                            </div>
                            <div class="sm:col-span-3">
                                <label for="<%= txtCantidad.ClientID %>" class="mb-1 block text-xs font-medium text-gray-600">Cantidad:</label>
                                <asp:TextBox ID="txtCantidad" runat="server" TextMode="Number" step="0.01"
                                    CssClass="block w-full rounded-md border-gray-300 px-3 py-2 text-sm text-gray-800 shadow-sm focus:border-primary focus:outline-none focus:ring-1 focus:ring-primary" placeholder="0.00"></asp:TextBox>
                            </div>
                            <div class="sm:col-span-4">
                                <label for="<%= txtCosto.ClientID %>" class="mb-1 block text-xs font-medium text-gray-600">Costo Unitario (Bs.):</label>
                                <asp:TextBox ID="txtCosto" runat="server" TextMode="Number" step="0.01"
                                    CssClass="block w-full rounded-md border-gray-300 px-3 py-2 text-sm text-gray-800 shadow-sm focus:border-primary focus:outline-none focus:ring-1 focus:ring-primary" placeholder="0.00"></asp:TextBox>
                            </div>
                        </div>
                        <div class="mt-4 flex justify-end">
                            <asp:Button ID="btnAgregarInsumo" runat="server" Text="Añadir a la Compra" OnClick="btnAgregarInsumo_Click"
                                CssClass="cursor-pointer rounded-lg bg-primary px-4 py-2 text-center text-sm font-medium text-white shadow-sm transition-all hover:bg-primary/65" />
                        </div>
                    </div>
                </div>
            </div>

            <!-- Columna Derecha: Resumen de la Compra -->
            <div class="lg:col-span-2">
                <div class="rounded-xl border border-gray-200 bg-white p-6 shadow-lg">
                    <h2 class="mb-2 text-xl font-bold text-primary">2. Resumen de Compra</h2>
                    <p class="mb-6 text-sm text-gray-500">Aquí verás los insumos añadidos y el total a pagar.</p>
                    
                    <!-- Lista de Insumos Añadidos -->
                    <div class="min-h-[200px] space-y-3">
                        <asp:Repeater ID="rptDetalleCompra" runat="server" OnItemCommand="rptDetalleCompra_ItemCommand">
                            <ItemTemplate>
                                <div class="flex items-center justify-between border-b border-gray-200 pb-3">
                                    <div>
                                        <p class="font-semibold text-gray-800"><%# Eval("Insumo.Nombre") %></p>
                                        <p class="text-sm text-gray-500">
                                            <%# Eval("Cantidad") %> x Bs. <%# Eval("Costo", "{0:N2}") %>
                                        </p>
                                    </div>
                                    <div class="flex items-center space-x-2">
                                        <p class="font-semibold text-gray-900">
                                            Bs. <%# Convert.ToDecimal(Eval("Cantidad")) * Convert.ToDecimal(Eval("Costo")) %>
                                        </p>
                                        <asp:Button ID="btnEliminarItem" runat="server" Text="×"
                                            CommandName="Eliminar" CommandArgument='<%# Eval("InsumoId") %>'
                                            CssClass="flex h-6 w-6 items-center justify-center rounded-full bg-red-100 text-red-600 transition-colors hover:bg-red-200" />
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                        
                        <!-- Mensaje cuando no hay items -->
                        <asp:Panel ID="pnlNoItems" runat="server" CssClass="pt-10 text-center">
                            <p class="text-sm text-gray-400">Aún no has añadido insumos a la compra.</p>
                        </asp:Panel>
                    </div>

                    <!-- Total y Botones Finales -->
                    <div class="mt-6 border-t border-gray-200 pt-4">
                        <div class="flex justify-between text-lg font-bold">
                            <span>Total a Pagar:</span>
                            <asp:Label ID="lblTotalCompra" runat="server" Text="Bs. 0.00" CssClass="text-primary" />
                        </div>
                        <div class="mt-6">
                            <asp:Button ID="btnRegistrarCompra" runat="server" Text="Registrar Compra" OnClick="btnRegistrarCompra_Click"
                                CssClass="w-full cursor-pointer rounded-lg bg-primary px-5 py-3 text-center text-sm font-medium text-white shadow-sm transition-all hover:brightness-90" />
                        </div>
                    </div>
                </div>
            </div>

        </div>
    </main>
</asp:Content>
