<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditarNegocio.aspx.cs" Inherits="CapaPresentacion.Pages.Negocio.EditarNegocio" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<div class="container mx-auto max-w-2xl p-4 sm:p-6 lg:p-8">
        
    
            <div class="mb-6 flex items-center justify-between pb-4">
                <h2 class="text-2xl font-bold text-gray-800">Editar Datos del Negocio</h2>
                <asp:Button ID="btnVolver" runat="server" OnClick="btnVolver_Click" Text="Volver"
                    CssClass="bg-secondary inline-flex cursor-pointer items-center rounded-lg px-4 py-2 text-sm font-semibold text-white transition-colors hover:bg-secondary/75 focus:ring-secondary focus:outline-none focus:ring-2 focus:ring-offset-2" />
            </div>
        <div class="rounded-2xl bg-white p-6 shadow-xl">

            <asp:HiddenField ID="hfLogoUrlActual" runat="server" />

            <div class="grid grid-cols-1 gap-6 md:grid-cols-3">
                
                <div class="md:col-span-1">
                    <label class="mb-2 block text-sm font-medium text-gray-700">Logo Actual</label>
                    <asp:Image ID="imgPreview" runat="server" CssClass="h-32 w-32 rounded-lg border object-cover shadow-sm" />
                </div>

                <div class="space-y-6 md:col-span-2">
                    <div>
                        <label for="<%= txtNombreNegocio.ClientID %>" class="mb-2 block text-sm font-medium text-gray-700">Nombre del negocio</label>
                        <asp:TextBox ID="txtNombreNegocio" runat="server" CssClass="w-full rounded-lg border border-gray-300 p-3 shadow-sm transition focus:border-primary focus:ring-primary/50 focus:ring-2"></asp:TextBox>
                    </div>
                    <div>
                        <label for="<%= txtDireccionNegocio.ClientID %>" class="mb-2 block text-sm font-medium text-gray-700">Dirección del negocio</label>
                        <asp:TextBox ID="txtDireccionNegocio" runat="server" CssClass="w-full rounded-lg border border-gray-300 p-3 shadow-sm transition focus:border-primary focus:ring-primary/50 focus:ring-2"></asp:TextBox>
                    </div>
                     <div>
                        <label for="<%= fileUploadNuevo.ClientID %>" class="mb-2 block text-sm font-medium text-gray-700">Cambiar Logo (Opcional)</label>
                        <asp:FileUpload ID="fileUploadNuevo" runat="server" CssClass="block w-full text-sm text-gray-500 file:bg-primary/10 file:text-primary file:mr-4 file:rounded-full file:border-0 file:px-4 file:py-2 file:text-sm file:font-semibold hover:file:bg-primary/20" />
                    </div>
                </div>
            </div>

            <div class="mt-8 border-t border-gray-200 pt-6 text-right">
                <asp:Button Text="Guardar Cambios" runat="server" ID="btnEditar" OnClick="btnEditar_Click"
                    CssClass="bg-primary cursor-pointer rounded-lg px-6 py-3 text-base font-semibold text-white shadow-sm transition-colors duration-300 hover:bg-primary/85 focus:ring-primary focus:outline-none focus:ring-2 focus:ring-offset-2" />
            </div>
        </div>
    </div>
</asp:Content>
