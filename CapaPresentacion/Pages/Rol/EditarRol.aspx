<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditarRol.aspx.cs" Inherits="CapaPresentacion.Pages.Rol.EditarRol" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="mx-auto max-w-5xl px-4 py-8 sm:px-6 lg:px-8">

        <%-- Campo oculto para guardar el ID del Rol que estamos editando --%>
        <asp:HiddenField ID="hfRolId" runat="server" />

        <%-- ENCABEZADO DE LA PÁGINA --%>
        <div class="mb-6 flex items-center justify-between">
            <h1 class="text-3xl font-bold text-gray-800">Editar Rol</h1>
            <div>
                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Pages/Rol/Rol.aspx"
                    CssClass="border-primary bg-primary cursor-pointer rounded-md border px-4 py-4 text-sm font-medium text-white shadow-sm hover:bg-primary/75 focus:ring-primary focus:outline-none focus:ring-2 focus:ring-offset-2">Volver</asp:HyperLink>
            </div>
        </div>

        <%-- CONTENEDOR PRINCIPAL --%>
        <div class="overflow-hidden rounded-xl bg-white shadow-lg">
            <div class="p-6 sm:p-8">
                <div class="grid grid-cols-1 md:grid-cols-12 md:gap-12">

                    <%-- COLUMNA IZQUIERDA --%>
                    <div class="md:col-span-5">
                        <h2 class="text-xl font-semibold text-gray-900">Datos del Rol</h2>
                        <p class="mt-1 text-sm text-gray-600">Modifique el nombre y los permisos asociados.</p>

                        <div class="mt-6">
                            <asp:Label ID="lblNombreRol" runat="server" Text="Nombre del Rol" CssClass="block text-sm font-medium text-gray-700" />
                            <asp:TextBox ID="txtNombreRol" runat="server"
                                CssClass="mt-1 block w-full rounded-md border border-gray-300 bg-white px-3 py-2 placeholder-gray-400 shadow-sm sm:text-sm focus:border-indigo-500 focus:outline-none focus:ring-indigo-500"></asp:TextBox>
                        </div>

                        <div class="mt-8">
                            <asp:Button ID="btnGuardarCambios" runat="server" Text="Guardar Cambios"
                                OnClick="btnGuardarCambios_Click"
                                CssClass="bg-secondary flex w-full cursor-pointer justify-center rounded-md border border-transparent px-4 py-2.5 text-sm font-medium text-white shadow-sm hover:bg-secondary/85 focus:ring-secondary focus:outline-none focus:ring-2 focus:ring-offset-2" />
                        </div>
                    </div>

                    <%-- COLUMNA DERECHA: PERMISOS --%>
                    <div class="mt-8 md:col-span-7 md:mt-0">
                        <h2 class="text-xl font-semibold text-gray-900">Permisos del Rol</h2>
                        <p class="mt-1 text-sm text-gray-600">Seleccione los permisos que tendrá este rol.</p>

                        <div class="mt-6 max-h-96 space-y-3 overflow-y-auto rounded-lg border border-gray-200 bg-gray-50 p-4">
                            <asp:Repeater ID="rptPermisos" runat="server" OnItemDataBound="rptPermisos_ItemDataBound">
                                <ItemTemplate>
                                    <div class="block rounded-md border bg-white p-4 transition-colors duration-150 hover:bg-indigo-50">
                                        <asp:HiddenField ID="hfPermisoId" runat="server" Value='<%# Eval("Id") %>' />
                                        <label class="flex cursor-pointer items-center space-x-3">
                                            <asp:CheckBox ID="chkPermiso" runat="server"
                                                CssClass="h-5 w-5 rounded border-gray-300 text-indigo-600 focus:ring-indigo-500" />
                                            <span class="flex flex-col">
                                                <span class="font-medium text-gray-800"><%# Eval("FormNombre") %></span>
                                                <span class="text-xs text-gray-500"><%# Eval("FormRuta") %></span>
                                            </span>
                                        </label>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
