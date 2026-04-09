<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Usuarios.aspx.cs" Inherits="CapaPresentacion.Pages.Usuarios.Usuarios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%-- Encabezado de la página: Título y Botón de Crear --%>
    <div class="flex w-full flex-col justify-between py-4">
        <div class="mb-4 flex items-center justify-between">
            <h2 class="text-primary text-3xl font-extrabold">Gestión de Usuarios</h2>

            <span class="bg-primary inline-flex cursor-pointer items-center rounded-md px-4 py-2 text-white shadow-sm transition-colors focus-within:ring-primary focus-within:ring-2 focus-within:ring-offset-2 hover:bg-primary/85">

                <span class="mr-2 text-xl font-bold">+</span>

                <asp:Button
                    ID="btnCrearNuevo"
                    runat="server"
                    Text="Nuevo Usuario"
                    OnClick="btnCrearNuevo_Click"
                    CssClass="cursor-pointer border-none bg-transparent p-0 font-semibold text-white shadow-none focus:outline-none"
                    UseSubmitBehavior="false" />

            </span>
        </div>
    </div>

    <%-- Contenedor principal de la tabla --%>
    <div class="flex rounded-lg bg-white p-4">
        <div class="flex w-full flex-col justify-between">
            <asp:UpdatePanel ID="UpdatePanel" runat="server">
                <ContentTemplate>
                    <asp:Repeater runat="server" ID="rptUsuarios" OnItemCommand="rptUsuarios_ItemCommand">
                        <%-- Cabecera de la tabla --%>
                        <HeaderTemplate>
                            <table class="w-full table-auto border-collapse overflow-hidden rounded-lg shadow" id="tbUsuarios" style="width: 100%">
                                <thead>
                                    <tr class="bg-primary text-white">
                                        <th class="py-2">#</th>
                                        <th class="py-2">Nombre</th>
                                        <th class="py-2">Contraseña</th>
                                        <th class="py-2">Acciones</th>
                                    </tr>
                                </thead>
                                <tbody>
                        </HeaderTemplate>

                        <%-- Cuerpo de la tabla (filas de datos) --%>
                        <ItemTemplate>
                            <tr class="transition-colors hover:bg-gray-100">
                                <td class="px-4 py-2"><%# Eval("Id") %></td>
                                <td class="px-4 py-2"><%# Eval("Nombre") %></td>
                                <td class="px-4 py-2">***********</td>

                                <td class="px-4 py-2">
                                    <div class="flex items-center gap-3">

                                        <span class="inline-flex cursor-pointer items-center rounded-md bg-blue-500 px-3 py-1.5 text-white shadow-sm transition-colors focus-within:ring-2 focus-within:ring-blue-500 focus-within:ring-offset-2 hover:bg-blue-600">
                                            <img src="/wwwroot/images/icons/boton-ver.png" class="h-4 w-4" alt="Ver" />
                                            <asp:Button runat="server" Text="Ver"
                                                CommandName="Ver" CommandArgument='<%# Eval("Id") %>'
                                                CssClass="ml-2 cursor-pointer border-none bg-transparent p-0 text-sm font-semibold text-white shadow-none focus:outline-none" />
                                        </span>

                                        <span class="inline-flex cursor-pointer items-center rounded-md bg-yellow-500 px-3 py-1.5 text-white shadow-sm transition-colors focus-within:ring-2 focus-within:ring-yellow-500 focus-within:ring-offset-2 hover:bg-yellow-600">
                                            <img src="/wwwroot/images/icons/boton-editar.png" class="h-4 w-4" alt="Editar" />
                                            <asp:Button runat="server" Text="Editar"
                                                CommandName="Editar" CommandArgument='<%# Eval("Id") %>'
                                                CssClass="ml-2 cursor-pointer border-none bg-transparent p-0 text-sm font-semibold text-white shadow-none focus:outline-none" />
                                        </span>

                                        <span class="inline-flex cursor-pointer items-center rounded-md bg-red-500 px-3 py-1.5 text-white shadow-sm transition-colors focus-within:ring-2 focus-within:ring-red-500 focus-within:ring-offset-2 hover:bg-red-600">
                                            <img src="/wwwroot/images/icons/boton-eliminar.png" class="h-4 w-4" alt="Eliminar" />
                                            <asp:Button runat="server" Text="Eliminar"
                                                CommandName="Eliminar" CommandArgument='<%# Eval("Id") %>'
                                                CssClass="ml-2 cursor-pointer border-none bg-transparent p-0 text-sm font-semibold text-white shadow-none focus:outline-none" />
                                        </span>

                                    </div>
                                </td>
                            </tr>
                        </ItemTemplate>

                        <%-- Pie de la tabla --%>
                        <FooterTemplate>
                            </tbody>
                    </table>
               
                        </FooterTemplate>
                    </asp:Repeater>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <script>
        // Función para inicializar DataTables
        function InicializarDataTable() {
            // Obtenemos una referencia a la tabla
            var tabla = $('#tbUsuarios');

            // Destruir cualquier instancia anterior de DataTables.
            // Esto evita errores si la tabla ya fue inicializada.
            if ($.fn.DataTable.isDataTable(tabla)) {
                tabla.DataTable().destroy();
            }

            // Inicializamos DataTables con una configuración
            tabla.DataTable({
                "pageLength": 5,
                "lengthMenu": [
                    [5, 10],
                    ['5 registros', '10 registros']
                ],
                "language": {
                    "url": "/Scripts/DataTables/es-ES.json",
                    "lengthMenu": "Mostrar _MENU_",
                    "search": "Buscar:",
                    "info": "Mostrando _START_ a _END_ de _TOTAL_ registros"
                },
                "dom": '<"flex justify-between items-center mb-4"lf>t<"flex justify-between items-center mt-4"ip>',
                "responsive": true
            });
        }

        // Se ejecuta la función en la carga inicial de la página
        $(document).ready(function () {
            InicializarDataTable();
        });

        // Le decimos al ScriptManager de ASP.NET que vuelva a ejecutar
        // nuestra función CADA VEZ que un UpdatePanel termine de actualizarse.
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {
                if (e.get_error() == null) {
                    InicializarDataTable();
                }
            });
        }
    </script>
</asp:Content>
