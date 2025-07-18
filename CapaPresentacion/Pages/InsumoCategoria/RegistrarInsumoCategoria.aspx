<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RegistrarInsumoCategoria.aspx.cs" Inherits="CapaPresentacion.Pages.InsumoCategoria.RegisrtarInsumoCategoria" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <main class="w-full px-4 py-12">

    <div class="mx-auto max-w-2xl rounded-xl border border-gray-200 bg-white shadow-lg">
        
        <div class="border-b border-gray-200 p-6">
            <h1 class="text-2xl font-bold text-primary">
                Registro de Categoria de Insumos
            </h1>
        </div>

        <div class="p-6">
    <div class="grid grid-cols-1 gap-x-6 gap-y-8 md:grid-cols-2">
        
        <div class="md:col-span-1">
            <label for="<%= txtNombre.ClientID %>" class="mb-2 block text-sm font-medium text-gray-700">Nombre:</label>
            <asp:TextBox ID="txtNombre" runat="server" 
                CssClass="block w-full rounded-md border-gray-300 px-3 py-2 text-gray-800 shadow-sm
                          transition-colors
                          placeholder:text-gray-400 focus:border-primary focus:outline-none focus:ring-1 focus:ring-primary"></asp:TextBox>
        </div>

    </div>
</div>

        <div class="flex items-center justify-end space-x-4 rounded-b-xl border-t border-gray-200 bg-gray-50 p-6">
            
            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" OnClick="btnCancelar_Click" 
                CssClass="cursor-pointer rounded-lg bg-secondary px-5 py-2.5 text-center text-sm font-medium text-white shadow-sm transition-all duration-200
                          hover:brightness-90 focus:outline-none focus:ring-2 focus:ring-secondary focus:ring-offset-2" />
            
            <asp:Button ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnGuardar_Click" 
                CssClass="cursor-pointer rounded-lg bg-primary px-5 py-2.5 text-center text-sm font-medium text-white shadow-sm transition-all
                          duration-200 hover:brightness-90 focus:outline-none focus:ring-2 focus:ring-primary focus:ring-offset-2" />
        </div>

    </div>
</main>
</asp:Content>
