<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditarInsumo.aspx.cs" Inherits="CapaPresentacion.Pages.Insumos.EditarInsumo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="w-full px-4 py-12">

        <div class="mx-auto max-w-2xl rounded-xl border border-gray-200 bg-white shadow-lg">

            <div class="border-b border-gray-200 p-6">
                <h1 class="text-2xl font-bold text-primary">Editar Insumo
            </h1>
            </div>

            <div class="p-6">
                <div class="space-y-6">

                    <div>
                        <label for="<%= txtNombre.ClientID %>" class="mb-2 block text-sm font-medium text-gray-700">Nombre de Insumo:</label>
                        <asp:TextBox ID="txtNombre" runat="server"
                            CssClass="block w-full rounded-md border-gray-300 px-3 py-2 text-gray-800 shadow-sm
                                  transition-colors
                                  placeholder:text-gray-400 focus:border-primary focus:outline-none focus:ring-1 focus:ring-primary"></asp:TextBox>
                    </div>
                    <div>
                        <label for="<%= txtCosto.ClientID %>" class="mb-2 block text-sm font-medium text-gray-700">Costo:</label>
                        <asp:TextBox ID="txtCosto" runat="server" TextMode="Number"
                            CssClass="block w-full rounded-md border-gray-300 px-3 py-2 text-gray-800 shadow-sm
                                 transition-colors
                                placeholder:text-gray-400 focus:border-primary focus:outline-none focus:ring-1 focus:ring-primary"></asp:TextBox>
                    </div>
                    <div>
                        <label for="<%= txtStock.ClientID %>" class="mb-2 block text-sm font-medium text-gray-700">Stock:</label>
                        <asp:TextBox ID="txtStock" runat="server" TextMode="Number"
                            CssClass="block w-full rounded-md border-gray-300 px-3 py-2 text-gray-800 shadow-sm
                                 transition-colors
                                 placeholder:text-gray-400 focus:border-primary focus:outline-none focus:ring-1 focus:ring-primary"></asp:TextBox>
                    </div>
                    <div>
                        <label for="<%= txtStockMinimo.ClientID %>" class="mb-2 block text-sm font-medium text-gray-700">Stock minimo:</label>
                        <asp:TextBox ID="txtStockMinimo" runat="server" TextMode="Number"
                            CssClass="block w-full rounded-md border-gray-300 px-3 py-2 text-gray-800 shadow-sm
                                 transition-colors
                                 placeholder:text-gray-400 focus:border-primary focus:outline-none focus:ring-1 focus:ring-primary"></asp:TextBox>
                    </div>
                    <div>
                        <label for="<%= imgFoto.ClientID %>" class="mb-2 block text-sm font-medium text-gray-700">Foto actual:</label>
                        <asp:Image runat="server" ID="imgFoto" CssClass="h-20 w-20 object-cover"/>
                    </div>
                    <div>
                        <label for="<%= flpFotoNueva.ClientID %>" class="mb-2 block text-sm font-medium text-gray-700">Foto nueva:</label>
                        <asp:FileUpload runat="server" ID="flpFotoNueva"/>
                    </div>
                    <div>
                        <label for="<%= InsumoCategoriaId.ClientID %>" class="mb-2 block text-sm font-medium text-gray-700">Categoria:</label>
                        <asp:DropDownList ID="InsumoCategoriaId" runat="server"></asp:DropDownList>
                    </div>
                    <div>
                        <label for="<%= ProveedorId.ClientID %>" class="mb-2 block text-sm font-medium text-gray-700">Proveedor:</label>
                        <asp:DropDownList ID="ProveedorId" runat="server"></asp:DropDownList>
                    </div>
                    <div>
                        <label for="<%= UnidadDeMedidaId.ClientID %>" class="mb-2 block text-sm font-medium text-gray-700">Unidad de Medida:</label>
                        <asp:DropDownList ID="UnidadDeMedidaId" runat="server"></asp:DropDownList>
                    </div>

                </div>
            </div>

            <div class="flex items-center justify-end space-x-4 rounded-b-xl border-t border-gray-200 bg-gray-50 p-6">

                <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" OnClick="btnCancelar_Click"
                    CssClass="cursor-pointer rounded-lg bg-secondary px-5 py-2.5 text-center text-sm font-medium text-white shadow-sm transition-all
                          duration-200 hover:brightness-90 focus:outline-none focus:ring-2 focus:ring-secondary focus:ring-offset-2" />

                <asp:Button ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnGuardar_Click"
                    CssClass="cursor-pointer rounded-lg bg-primary px-5 py-2.5 text-center text-sm font-medium text-white shadow-sm transition-all
                          duration-200 hover:brightness-90 focus:outline-none focus:ring-2 focus:ring-primary focus:ring-offset-2" />
            </div>

        </div>
    </div>
</asp:Content>
