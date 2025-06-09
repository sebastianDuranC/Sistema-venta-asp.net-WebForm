<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditarProducto.aspx.cs" Inherits="CapaPresentacion.Pages.Productos.EditarProducto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mx-auto p-4 md:p-6 lg:p-8">
        <div class="mb-6 flex items-center justify-between py-4">
            <h2 class="text-3xl font-extrabold text-primary">Editar producto</h2>
            <asp:Button CssClass="inline-flex items-center rounded-md bg-red-600 px-5 py-2.5 text-base font-semibold text-white shadow-sm transition-colors duration-200 hover:cursor-pointer hover:bg-red-700 focus:outline-none focus:ring-2 focus:ring-red-500 focus:ring-offset-2"
                Text="Cancelar" runat="server" ID="btnVolverProductos" OnClick="btnVolverProductos_Click" />
        </div>

        <div class="mx-auto rounded-xl bg-white p-6 shadow-xl md:p-8 lg:p-10">
            <div class="grid grid-cols-1 gap-8 md:grid-cols-2">
                <!-- Columna izquierda: Imagen actual y FileUpload -->
                <div class="flex flex-col items-center space-y-6">
                    <!-- Tarjeta de imagen actual -->
                    <div class="flex w-full flex-col items-center rounded-lg border-2 border-gray-300 bg-gray-50 p-6">
                        <label class="mb-4 block text-lg font-semibold text-gray-700">Imagen actual</label>
                        <!-- Imagen del producto -->
                        <asp:Image ID="imgProducto" runat="server" CssClass="mx-auto h-44 w-64 border border-gray-200 bg-white object-contain" AlternateText="Imagen del producto" />
                    </div>
                    <!-- Tarjeta de FileUpload -->
                    <div class="flex w-full flex-col items-center rounded-lg border-2 border-dashed border-gray-300 bg-gray-50 p-6">
                        <label for="fotoUrl" class="mb-4 block text-lg font-semibold text-gray-700">Cambiar imagen</label>
                        <!-- Input para subir nueva imagen -->
                        <asp:FileUpload ID="fotoUrl" runat="server" CssClass="mb-2" />
                        <span class="text-xs text-gray-500">Formatos permitidos: PNG, JPG, GIF. Máx 10MB.</span>
                    </div>
                </div>
                <!-- Columna derecha: Formulario de datos -->
                <div class="space-y-5">
                    <asp:HiddenField runat="server" ID="idProducto" />
                    <label for="txtNombre" class="mb-1 block text-sm font-medium text-gray-700">Nombre del producto</label>
                    <asp:TextBox ID="txtNombre" runat="server" CssClass="w-full rounded-md border border-gray-300 px-4 py-2.5 text-gray-900 shadow-sm transition-all duration-200 focus:border-blue-500 focus:ring-blue-500" />
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
                <asp:Button ID="btnEditar" runat="server" Text="Editar Producto"
                    CssClass="inline-flex items-center rounded-md bg-primary px-6 py-3 text-base font-semibold text-white shadow-sm transition-colors duration-200 hover:cursor-pointer hover:bg-primary/85 focus:outline-none focus:ring-2 focus:ring-primary focus:ring-offset-2"
                    OnClick="btnEditar_Click" />
            </div>
        </div>
    </div>
</asp:Content>
