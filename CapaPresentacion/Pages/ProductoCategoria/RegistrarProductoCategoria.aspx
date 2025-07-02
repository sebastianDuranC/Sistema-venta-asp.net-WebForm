<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RegistrarProductoCategoria.aspx.cs" Inherits="CapaPresentacion.Pages.ProductoCategoria.RegistrarProductoCategoria" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
        <main class="container mx-auto px-4 py-8">
        <div class="mx-auto max-w-3xl rounded-lg bg-white p-8 shadow-xl">
            <h1 class="mb-6 text-2xl font-bold text-gray-800">Registrar Nuevo Elemento</h1>

            <div class="grid grid-cols-1 gap-6 md:grid-cols-2">
                <div>
                    <div class="mb-4">
                        <label for="<%= txtNombre.ClientID %>" class="block text-gray-700 text-sm font-bold mb-2">Nombre de categoria:</label>
                        <asp:TextBox ID="txtNombre" runat="server" CssClass="w-full appearance-none rounded border px-3 py-2 leading-tight text-gray-700 shadow focus:outline-none focus:ring-2 focus:ring-blue-500" placeholder="Ej. Postre"></asp:TextBox>
                    </div>
                </div>
            </div>

            <div class="mt-8 flex justify-end space-x-4">
                <asp:Button ID="btnVolver" runat="server" Text="Volver" OnClick="btnVolver_Click" CssClass="rounded-lg bg-gray-500 px-4 py-2 font-bold text-white transition duration-300 hover:bg-gray-600" />
                <asp:Button ID="btnRegistrar" runat="server" Text="Registrar" OnClick="btnRegistrar_Click" CssClass="rounded-lg bg-blue-600 px-4 py-2 font-bold text-white transition duration-300 hover:bg-blue-700" />
            </div>
        </div>
    </main>
</asp:Content>
