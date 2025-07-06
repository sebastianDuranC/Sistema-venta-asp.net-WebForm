<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditarProducto.aspx.cs" Inherits="CapaPresentacion.Pages.Productos.EditarProducto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<div class="container mx-auto flex h-[90vh] max-h-[90vh] flex-col p-4 md:p-6 lg:p-8">
        <div class="sticky top-0 z-10 mb-6 flex items-center justify-between py-4">
            <h2 class="text-3xl font-extrabold text-gray-900">Editar Producto</h2>
            <asp:Button CssClass="inline-flex items-center rounded-md bg-red-600 px-5 py-2.5 text-base font-semibold text-white shadow-sm hover:bg-red-700"
                Text="Cancelar" runat="server" ID="btnVolverProductos" OnClick="btnVolverProductos_Click" />
        </div>

        <div class="w-full flex-1 overflow-y-auto rounded-2xl bg-white p-4 shadow-xl md:p-8">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:HiddenField ID="hfProductoId" runat="server" />
                    <asp:HiddenField ID="hfFotoUrlActual" runat="server" />

                    <div class="grid grid-cols-1 gap-x-12 lg:grid-cols-2">
                        <div class="flex flex-col">
                            <h2 class="mb-4 border-b pb-2 text-xl font-semibold text-gray-700">1. Datos del Producto</h2>
                            
                            <div class="mb-6">
                                <label class="mb-1 block text-sm font-medium text-gray-600">Foto del Producto</label>
                                <div class="relative flex h-48 w-full items-center justify-center rounded-lg border-2 border-dashed border-gray-300 p-4 text-center">
                                    <asp:Image ID="imgPreview" runat="server" CssClass="absolute inset-0 h-full w-full rounded-md object-contain p-2" />
                                    <div id="divPlaceholder" runat="server" class="text-gray-500">
                                        <p class="mt-2 text-sm">Selecciona una nueva imagen para cambiarla</p>
                                    </div>
                                    <asp:FileUpload ID="fotoUrl" runat="server" CssClass="absolute inset-0 h-full w-full cursor-pointer opacity-0" onchange="previewImage(this);" />
                                </div>
                            </div>

                            <div class="grid grid-cols-1 gap-4 sm:grid-cols-2">
                                <div class="sm:col-span-2">
                                    <label for="<%= txtNombre.ClientID %>" class="block text-sm font-medium text-gray-600 mb-1">Nombre</label>
                                    <asp:TextBox ID="txtNombre" runat="server" CssClass="w-full rounded-lg border p-3" />
                                </div>
                                <div>
                                    <label for="<%= txtPrecio.ClientID %>" class="block text-sm font-medium text-gray-600 mb-1">Precio</label>
                                    <asp:TextBox ID="txtPrecio" runat="server" TextMode="Number" CssClass="w-full rounded-lg border p-3" />
                                </div>
                                <div>
                                    <label for="<%= ddlCategoria.ClientID %>" class="block text-sm font-medium text-gray-600 mb-1">Categoría</label>
                                    <asp:DropDownList ID="ddlCategoria" runat="server" CssClass="w-full rounded-lg border bg-white p-3" />
                                </div>
                                <div>
                                    <label for="<%= txtStock.ClientID %>" class="block text-sm font-medium text-gray-600 mb-1">Stock</label>
                                    <asp:TextBox ID="txtStock" runat="server" TextMode="Number" CssClass="w-full rounded-lg border p-3" />
                                </div>
                                <div>
                                    <label for="<%= txtStockMinimo.ClientID %>" class="block text-sm font-medium text-gray-600 mb-1">Stock Mínimo</label>
                                    <asp:TextBox ID="txtStockMinimo" runat="server" TextMode="Number" CssClass="w-full rounded-lg border p-3" />
                                </div>
                            </div>
                        </div>

                        <div class="mt-8 flex flex-col lg:mt-0">
                            <h2 class="mb-4 border-b pb-2 text-xl font-semibold text-gray-700">2. Receta / Insumos</h2>
                            <div class="rounded-lg border bg-gray-50 p-4">
                                <div class="grid grid-cols-1 items-end gap-4 md:grid-cols-2">
                                     <div class="md:col-span-2">
                                        <label for="<%= ddlInsumo.ClientID %>" class="block text-sm font-medium text-gray-600 mb-1">Seleccionar Insumo</label>
                                        <asp:DropDownList ID="ddlInsumo" runat="server" CssClass="w-full rounded-lg border bg-white p-3"></asp:DropDownList>
                                    </div>
                                    <div>
                                        <label for="<%= txtInsumoCantidad.ClientID %>" class="block text-sm font-medium text-gray-600 mb-1">Cantidad</label>
                                        <asp:TextBox ID="txtInsumoCantidad" runat="server" TextMode="Number" step="0.01" CssClass="w-full rounded-lg border p-3"></asp:TextBox>
                                    </div>
                                    <div>
                                        <label for="<%= ddlInsumoTipo.ClientID %>" class="block text-sm font-medium text-gray-600 mb-1">Tipo</label>
                                        <asp:DropDownList ID="ddlInsumoTipo" runat="server" CssClass="w-full rounded-lg border bg-white p-3">
                                            <asp:ListItem>Comestible</asp:ListItem>
                                            <asp:ListItem>Descartable</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <asp:Button ID="btnAgregarInsumo" runat="server" Text="Añadir Insumo a la Receta" OnClick="btnAgregarInsumo_Click1"
                                    CssClass="mt-4 w-full rounded-lg bg-red-600 py-3 font-semibold text-white" />
                            </div>

                            <div class="mt-6">
                                <asp:Repeater ID="rptInsumosAgregados" runat="server" OnItemCommand="rptInsumosAgregados_ItemCommand">
                                    <HeaderTemplate>
                                        <h3 class="mb-2 font-semibold text-gray-700">Insumos Agregados</h3>
                                        <div class="max-h-60 overflow-y-auto rounded-lg border">
                                            <table class="min-w-full bg-white">
                                                <thead class="sticky top-0 bg-gray-50">
                                                    <tr>
                                                        <th class="px-4 py-3 text-left text-sm font-semibold uppercase text-gray-600">Insumo</th>
                                                        <th class="px-4 py-3 text-left text-sm font-semibold uppercase text-gray-600">Cantidad</th>
                                                        <th class="px-4 py-3 text-left text-sm font-semibold uppercase text-gray-600">Tipo</th>
                                                        <th class="px-4 py-3 text-right text-sm font-semibold uppercase text-gray-600">Acción</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr class="border-t">
                                            <td class="px-4 py-3 text-sm text-gray-700"><%# Eval("InsumoNombre") %></td>
                                            <td class="px-4 py-3 text-sm text-gray-700"><%# Eval("Cantidad", "{0:N2}") %></td>
                                            <td class="px-4 py-3 text-sm"><span><%# Eval("Tipo") %></span></td>
                                            <td class="px-4 py-3 text-right">
                                                <asp:Button ID="btnEliminarInsumo" runat="server" Text="Eliminar" CommandName="Eliminar" CommandArgument='<%# Eval("InsumoId") %>' CssClass="text-red-500 hover:text-red-700 font-medium text-sm bg-transparent border-none p-0 cursor-pointer" />
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                                </tbody>
                                            </table>
                                        </div>
                                    </FooterTemplate>
                                </asp:Repeater>
                                <asp:Panel ID="pnlNoInsumos" runat="server" Visible="false" CssClass="mt-2 rounded-lg border py-6 text-center text-gray-500">
                                    Aún no se han agregado insumos a la receta.
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                     <div class="mt-8 border-t pt-6 text-right">
                        <asp:Button ID="btnGuardarCambios" runat="server" Text="Guardar Cambios" OnClick="btnGuardarCambios_Click" CssClass="cursor-pointer rounded-lg bg-primary px-6 py-3 text-base font-semibold text-white shadow-sm hover:bg-primary/85" />
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnGuardarCambios" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
