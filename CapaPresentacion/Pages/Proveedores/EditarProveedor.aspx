<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditarProveedor.aspx.cs" Inherits="CapaPresentacion.Pages.Proveedores.EditarProveedor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="w-full px-4 py-12">

        <div class="mx-auto max-w-2xl rounded-xl border border-gray-200 bg-white shadow-lg">

            <div class="border-b border-gray-200 p-6">
                <h1 class="text-2xl font-bold text-primary">Editar Proveedor
                </h1>
            </div>

            <div class="p-6">
                <div class="space-y-6">

                    <div>
                        <label for="<%= txtNombre.ClientID %>" class="mb-2 block text-sm font-medium text-gray-700">Nombre del Proveedor:</label>
                        <asp:TextBox ID="txtNombre" runat="server"
                            CssClass="block w-full rounded-md border-gray-300 px-3 py-2 text-gray-800 shadow-sm transition-colors
                                  placeholder:text-gray-400
                                  focus:border-primary focus:outline-none focus:ring-1 focus:ring-primary"></asp:TextBox>
                    </div>
                    <div>
                        <label for="<%= txtContacto.ClientID %>" class="mb-2 block text-sm font-medium text-gray-700">Nombre del Método:</label>
                        <asp:TextBox ID="txtContacto" runat="server" TextMode="Number"
                            CssClass="block w-full rounded-md border-gray-300 px-3 py-2 text-gray-800 shadow-sm transition-colors
                                placeholder:text-gray-400
                                focus:border-primary focus:outline-none focus:ring-1 focus:ring-primary"></asp:TextBox>
                    </div>

                </div>
            </div>

            <div class="flex items-center justify-end space-x-4 rounded-b-xl border-t border-gray-200 bg-gray-50 p-6">

                <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" OnClick="btnCancelar_Click"
                    CssClass="rounded-lg bg-secondary px-5 py-2.5 text-center text-sm font-medium text-white shadow-sm transition-all duration-200
                          hover:brightness-90 focus:outline-none focus:ring-2 focus:ring-secondary focus:ring-offset-2" />

                <asp:Button ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnGuardar_Click"
                    CssClass="rounded-lg bg-primary px-5 py-2.5 text-center text-sm font-medium text-white shadow-sm transition-all duration-200
                          hover:brightness-90 focus:outline-none focus:ring-2 focus:ring-primary focus:ring-offset-2" />
            </div>

        </div>
    </div>
</asp:Content>
