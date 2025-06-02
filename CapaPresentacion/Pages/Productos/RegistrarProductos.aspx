<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RegistrarProductos.aspx.cs" Inherits="CapaPresentacion.Pages.Productos.RegistrarProductos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mx-auto p-4 md:p-6 lg:p-8">
        <div class="mb-6 flex items-center justify-between py-4">
            <h2 class="text-3xl font-extrabold text-gray-900">Registro de Productos</h2>
            <asp:Button CssClass="inline-flex items-center rounded-md bg-red-600 px-5 py-2.5 text-base font-semibold text-white shadow-sm transition-colors duration-200 hover:cursor-pointer hover:bg-red-700 focus:outline-none focus:ring-2 focus:ring-red-500 focus:ring-offset-2"
                Text="Volver" runat="server" ID="btnVolverProductos" OnClick="btnVolverProductos_Click1" />
        </div>

        <div class="mx-auto rounded-xl bg-white p-6 shadow-xl md:p-8 lg:p-10">
            <div class="grid grid-cols-1 gap-6 md:grid-cols-2 md:gap-8">
                <div class="flex flex-col items-center justify-center rounded-lg border-2 border-dashed border-gray-300 bg-gray-50 p-6 text-center transition-all duration-200 hover:border-blue-500 hover:bg-gray-100">
                    <label for="fuFoto" class="cursor-pointer">
                        <svg class="mx-auto h-16 w-16 text-gray-400" fill="none" viewBox="0 0 24 24" stroke="currentColor" aria-hidden="true">
                            <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M7 16a4 4 0 01-.88-7.903A5 5 0 0115.9 6L16 6a5 5 0 011 9.9M15 13l-3-3m0 0l-3 3m3-3v12"></path>
                        </svg>
                        <span class="mt-2 block text-sm font-medium text-gray-600">Selecciona o arrastra una imagen</span>
                        <p class="text-xs text-gray-500">PNG, JPG, GIF hasta 10MB</p>
                    </label>
                    <asp:FileUpload ID="fotoUrl" runat="server" CssClass="" />
                </div>

                <div class="space-y-5">
                    <div>
                        <label for="txtNombre" class="mb-1 block text-sm font-medium text-gray-700">Nombre del producto</label>
                        <asp:TextBox ID="txtNombre" runat="server" CssClass="w-full rounded-md border border-gray-300 px-4 py-2.5 text-gray-900 shadow-sm transition-all duration-200 focus:border-blue-500 focus:ring-blue-500"/>
                    </div>
                    <div>
                        <label for="ddlCategoria" class="mb-1 block text-sm font-medium text-gray-700">Categoría</label>
                        <asp:DropDownList ID="ddlCategoria" runat="server" CssClass="w-full rounded-md border border-gray-300 bg-white px-4 py-2.5 text-gray-900 shadow-sm transition-all duration-200 focus:border-blue-500 focus:ring-blue-500">
                            <asp:ListItem Text="Seleccione una categoría" Value="" Selected="True" Disabled="True" />
                        </asp:DropDownList>
                    </div>
                    <div class="grid grid-cols-1 gap-4 sm:grid-cols-2">
                        <div>
                            <label for="txtPrecio" class="mb-1 block text-sm font-medium text-gray-700">Precio</label>
                            <asp:TextBox ID="txtPrecio" runat="server" TextMode="Number" CssClass="w-full rounded-md border border-gray-300 px-4 py-2.5 text-gray-900 shadow-sm transition-all duration-200 focus:border-blue-500 focus:ring-blue-500" placeholder="0.00" />
                        </div>
                        <div>
                            <label for="txtStock" class="mb-1 block text-sm font-medium text-gray-700">Stock</label>
                            <asp:TextBox ID="txtStock" runat="server" TextMode="Number" CssClass="w-full rounded-md border border-gray-300 px-4 py-2.5 text-gray-900 shadow-sm transition-all duration-200 focus:border-blue-500 focus:ring-blue-500" placeholder="0" />
                        </div>
                    </div>
                    <div>
                        <label for="txtStockMinimo" class="mb-1 block text-sm font-medium text-gray-700">Stock Mínimo</label>
                        <asp:TextBox ID="txtStockMinimo" runat="server" TextMode="Number" CssClass="w-full rounded-md border border-gray-300 px-4 py-2.5 text-gray-900 shadow-sm transition-all duration-200 focus:border-blue-500 focus:ring-blue-500" placeholder="0" />
                    </div>
                </div>
            </div>
            <div class="mt-8 flex justify-end">
                <asp:Button ID="btnRegistrar" runat="server" Text="Registrar Producto"
                    CssClass="inline-flex items-center rounded-md bg-primary px-6 py-3 text-base font-semibold text-white shadow-sm transition-colors duration-200 hover:cursor-pointer hover:bg-primary/85 focus:outline-none focus:ring-2 focus:ring-primary focus:ring-offset-2"
                    OnClick="btnRegistrar_Click" />
            </div>
        </div>
    </div>
</asp:Content>