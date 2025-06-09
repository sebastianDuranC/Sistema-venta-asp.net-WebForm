<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RegistrarPermisos.aspx.cs" Inherits="CapaPresentacion.Form.Index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="flex items-center justify-center">
        <div class="w-full max-w-md rounded-lg bg-white p-8 shadow-lg">
            <div class="mb-6 flex items-center justify-between">
                <h3 class="text-2xl font-bold text-gray-800">Registrar nuevo formularios</h3>
                <asp:Button ID="btnVolver" OnClick="btnVolver_Click" Text="Volver" runat="server" CssClass="rounded bg-primary px-4 py-2 font-semibold text-white transition-colors duration-200 hover:cursor-pointer hover:bg-primary/65" />
            </div>
            <div class="space-y-4">
                <div>
                    <asp:Label Text="Nombre del formulario" runat="server" CssClass="mb-1 block font-medium text-gray-700" />
                    <asp:TextBox ID="txtFormNombre" runat="server" CssClass="w-full rounded border border-gray-300 px-3 py-2 focus:ring-primary-500 focus:outline-none focus:ring-2" />
                </div>
                <div>
                    <asp:Label Text="Nombre de la ruta" runat="server" CssClass="mb-1 block font-medium text-gray-700" />
                    <asp:TextBox ID="txtformRuta" runat="server" CssClass="w-full rounded border border-gray-300 px-3 py-2 focus:ring-primary-500 focus:outline-none focus:ring-2" />
                </div>
                <div>
                    <asp:Button ID="btnRegistrarForm" runat="server" Text="Registrar formulario" OnClick="btnRegistrarForm_Click"
                        CssClass="w-full rounded bg-secondary px-4 py-2 font-semibold text-white transition-colors duration-200 hover:cursor-pointer hover:bg-secondary/70" />
                </div>
                <div>
                    <asp:Label ID="lblMensaje" runat="server" CssClass="block text-center font-medium text-red-600" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>