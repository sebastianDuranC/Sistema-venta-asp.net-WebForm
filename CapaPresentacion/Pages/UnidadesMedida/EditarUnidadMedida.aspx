<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditarUnidadMedida.aspx.cs" Inherits="CapaPresentacion.Pages.UnidadesMedida.EditarUnidadMedida" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <section class="w-full px-4 py-12">

        <div class="mx-auto max-w-2xl rounded-xl border border-gray-200 bg-white shadow-lg">

            <div class="flex items-center gap-1 border-b border-gray-200 p-6">
                <div>
                    <img src="/wwwroot/images/icons/icon-editar.png" alt="icon mas" class="mr-2 h-12 w-12 rounded-[50px] object-contain" />
                </div>
                <div>
                    <h1 class="text-primary text-2xl font-bold">Editar Unidad de Medida</h1>
                    <p class="mt-1 text-sm text-gray-500">
                        Modifica los campos que desees actualizar
                    </p>
                </div>
            </div>

            <div class="p-6">
                <div class="grid grid-cols-1 gap-x-6 gap-y-8 md:grid-cols-2">

                    <div class="md:col-span-1">
                        <label for="<%= txtNombre.ClientID %>" class="mb-2 block text-sm font-medium text-gray-700">Nombre de Unidad de Medida:</label>
                        <asp:TextBox ID="txtNombre" runat="server"
                            CssClass="block w-full rounded-md border-gray-300 px-3 py-2 text-gray-800 shadow-sm transition-colors
                          placeholder:text-gray-400
                          focus:border-primary focus:ring-primary focus:outline-none focus:ring-1"
                            placeholder="Ej. Kilogramos"></asp:TextBox>
                    </div>

                    <div class="md:col-span-1">
                        <label for="<%= txtAbreviacion.ClientID %>" class="mb-2 block text-sm font-medium text-gray-700">Abreviatura:</label>
                        <asp:TextBox ID="txtAbreviacion" runat="server"
                            CssClass="block w-full rounded-md border-gray-300 px-3 py-2 text-gray-800 shadow-sm transition-colors
                          placeholder:text-gray-400
                          focus:border-primary focus:ring-primary focus:outline-none focus:ring-1"
                            placeholder="Ej. Kg"></asp:TextBox>
                    </div>


                </div>
            </div>

            <div class="flex items-center justify-end space-x-4 rounded-b-xl border-t border-gray-200 bg-gray-50 p-6">

                <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" OnClick="btnCancelar_Click"
                    CssClass="bg-secondary cursor-pointer rounded-lg px-5 py-2.5 text-center text-sm font-medium text-white shadow-sm transition-all duration-200
                          hover:brightness-90 focus:ring-secondary focus:outline-none focus:ring-2 focus:ring-offset-2" />

                <span class="bg-primary inline-flex cursor-pointer items-center rounded-md px-3 py-1.5 text-white shadow-sm transition-colors focus-within:ring-primary focus-within:ring-2 focus-within:ring-offset-2 hover:bg-primary/85">
                    <img src="/wwwroot/images/icons/boton-guardar.png" class="mr-0.5 h-4 w-4" alt="Eliminar" />
                    <asp:Button ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnGuardar_Click"
                        CssClass="ml-2 cursor-pointer border-none bg-transparent p-0 py-1 text-sm font-semibold text-white shadow-none focus:outline-none" />
                </span>
            </div>

        </div>
    </section>
</asp:Content>
