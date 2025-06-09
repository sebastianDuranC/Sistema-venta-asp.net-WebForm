<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Permisos.aspx.cs" Inherits="CapaPresentacion.Pages.Permisos.Permisos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="flex w-full flex-col justify-between py-4">
        <div class="mb-4 flex items-center justify-between">
            <h2 class="text-3xl font-extrabold text-primary">Lista de permisos</h2>
            <asp:Button CssClass="mt-2 inline-block rounded bg-primary px-4 py-2 font-semibold text-white shadow transition-colors hover:cursor-pointer hover:bg-primary/85 focus:outline-none focus:ring-2 focus:ring-primary focus:ring-offset-2" Text="Crear permisos" runat="server" ID="btnRegistrarPermisos" OnClick="btnRegistrarPermisos_Click" />
        </div>
    </div>
    <div class="flex rounded-lg bg-white p-4">
        <div class="flex w-full flex-col justify-between">
            <%-- Lista de permisos --%>
            <asp:Repeater runat="server" ID="rptPermisos" OnItemCommand="rptPermisos_ItemCommand">
                <%-- Cabezera del table --%>
                <HeaderTemplate>
                    <table class="w-full table-auto border-collapse overflow-hidden rounded-lg shadow" id="tbPermisos" style="width: 100%">
                        <thead>
                            <tr class="bg-primary text-center text-white">
                                <th class="py-2">#</th>
                                <th class="py-2">Descripción</th>
                                <th class="py-2">Ruta</th>
                                <th class="py-2">Acciones</th>
                            </tr>
                        </thead>
                        <tbody>
                </HeaderTemplate>
                <%-- Cuerpo del table --%>
                <ItemTemplate>
                    <tr>
                        <td class="px-4 py-2"><%# Eval("Id") %>  </td>
                        <td class="px-4 py-2"><%# Eval("FormNombre") %> </td>
                        <td class="px-4 py-2"><%# Eval("FormRuta") %> </td>
                        <td class="px-4 py-2">
                            <div style="display: flex; justify-content: center; gap: 0.5rem;">
                                <asp:Button runat="server" Text="Editar" 
                                CommandName="Editar" CommandArgument='<%# Eval("Id") %>' 
                                CssClass="rounded bg-yellow-500 px-3 py-1 text-white transition-colors hover:bg-yellow-600 hover:cursor-pointer" />
                            </div>
                        </td>
                    </tr>
                </ItemTemplate>
                <%-- Pie del table --%>
                <FooterTemplate>
                    </tbody>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
        </div>
    </div>
<script>
    $(document).ready(function () {
        $('#tbPermisos').DataTable({

            // 1. Opciones de visualización y cantidad de registros
            "pageLength": 5, // Inicia mostrando 5 registros por página
            "lengthMenu": [
                [5, 10], // Valores internos de DataTables (-1 es 'Todos')
                ['5 registros', '10 registros'] // Texto que ve el usuario
            ],

            // 2. Traducción al español para todos los elementos de la tabla
            "language": {
                "url": "/Scripts/DataTables/es-ES.json", // Asegúrate que esta ruta es correcta
                // Opcional: puedes definir textos específicos aquí si el JSON no los tiene
                "lengthMenu": "Mostrar _MENU_", // Personaliza el texto del menú de longitud
                "search": "Buscar:", // Cambia el texto del label de búsqueda
                "info": "Mostrando _START_ a _END_ de _TOTAL_ registros"
            },

            // 3. Diseño y orden de los controles (La clave para el diseño que buscas)
            //    l -> lengthMenu (El selector de "Mostrar X registros")
            //    f -> filtering (El campo de "Buscar")
            //    t -> table (La tabla en sí)
            //    i -> info (El texto "Mostrando X a Y de Z registros")
            //    p -> pagination (Los botones de paginación)
            "dom": '<"flex justify-between items-center mb-4"lf>t<"flex justify-between items-center mt-4"ip>',

            // 4. Mantenemos el diseño responsivo
            "responsive": true
        });
    });
</script>
</asp:Content>