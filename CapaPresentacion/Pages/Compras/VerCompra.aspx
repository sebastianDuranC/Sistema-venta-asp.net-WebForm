<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="VerCompra.aspx.cs" Inherits="CapaPresentacion.Pages.Compras.VerCompra" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<main class="w-full px-4 py-12">
        <div class="mx-auto max-w-3xl rounded-xl border border-gray-200 bg-white shadow-lg">
            
            <!-- Encabezado con Datos Principales -->
            <div class="border-b border-gray-200 p-6">
                <div class="flex items-center justify-between">
                    <div>
                        <h1 class="text-2xl font-bold text-primary">Detalle de Compra</h1>
                        <p class="mt-1 text-sm text-gray-500">
                            Compra Nº: 
                            <asp:Label ID="lblCompraId" runat="server" Text="000" CssClass="font-mono font-semibold" />
                        </p>
                    </div>
                    <div class="text-right">
                        <p class="text-sm font-semibold text-gray-700">Fecha de Compra</p>
                        <asp:Label ID="lblFecha" runat="server" Text="dd/mm/yyyy" CssClass="text-sm text-gray-500" />
                    </div>
                </div>
            </div>

            <!-- Información de Proveedor y Usuario -->
            <div class="grid grid-cols-1 gap-6 border-b border-gray-200 bg-gray-50/50 p-6 md:grid-cols-2">
                <div>
                    <h3 class="mb-1 text-xs font-semibold uppercase tracking-wider text-gray-500">Proveedor</h3>
                    <asp:Label ID="lblProveedor" runat="server" Text="[Nombre del Proveedor]" CssClass="text-base font-medium text-gray-800" />
                </div>
                <div>
                    <h3 class="mb-1 text-xs font-semibold uppercase tracking-wider text-gray-500">Registrado por</h3>
                    <asp:Label ID="lblUsuario" runat="server" Text="[Nombre del Usuario]" CssClass="text-base font-medium text-gray-800" />
                </div>
            </div>

            <!-- Tabla con Detalles de la Compra -->
            <div class="p-6">
                <h3 class="mb-4 text-lg font-semibold text-primary">Insumos Comprados</h3>
                <div class="overflow-x-auto">
                    <table class="min-w-full text-left text-sm">
                        <thead class="border-b bg-gray-100 text-xs uppercase text-gray-600">
                            <tr>
                                <th scope="col" class="px-4 py-3">Insumo</th>
                                <th scope="col" class="px-4 py-3 text-center">Cantidad</th>
                                <th scope="col" class="px-4 py-3 text-right">Costo Unitario</th>
                                <th scope="col" class="px-4 py-3 text-right">Subtotal</th>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Repeater ID="rptDetalleCompra" runat="server">
                                <ItemTemplate>
                                    <tr class="border-b transition-colors hover:bg-gray-50">
                                        <td class="px-4 py-3 font-medium text-gray-900"><%# Eval("Insumo.Nombre") %></td>
                                        <td class="px-4 py-3 text-center"><%# Eval("Cantidad") %></td>
                                        <td class="px-4 py-3 text-right">Bs. <%# Eval("Costo", "{0:N2}") %></td>
                                        <td class="px-4 py-3 text-right font-semibold">Bs. <%# (Convert.ToDecimal(Eval("Cantidad")) * Convert.ToDecimal(Eval("Costo"))).ToString("N2") %></td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </tbody>
                        <tfoot>
                            <tr class="font-bold text-primary">
                                <td colspan="3" class="px-4 py-3 text-right text-base">Total Pagado:</td>
                                <td class="px-4 py-3 text-right text-base">
                                    <asp:Label ID="lblTotalCompra" runat="server" Text="Bs. 0.00" />
                                </td>
                            </tr>
                        </tfoot>
                    </table>
                </div>
            </div>

            <!-- Pie de Página con Acciones -->
            <div class="flex items-center justify-end space-x-4 rounded-b-xl border-t border-gray-200 bg-gray-50 p-6">
                <asp:Button ID="btnVolver" runat="server" Text="Volver a la Lista" OnClick="btnVolver_Click" 
                    CssClass="cursor-pointer rounded-lg bg-secondary px-5 py-2.5 text-center text-sm font-medium text-white shadow-sm transition-all duration-200
                              hover:brightness-90 focus:outline-none focus:ring-2 focus:ring-secondary focus:ring-offset-2" />
           </div>
        </div>
    </main>
</asp:Content>
