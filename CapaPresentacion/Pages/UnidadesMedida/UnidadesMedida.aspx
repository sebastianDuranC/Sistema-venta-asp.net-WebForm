<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UnidadesMedida.aspx.cs" Inherits="CapaPresentacion.Pages.UnidadesMedida.UnidadesMedida" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%-- Encabezado de la página: Título y Botón de Crear --%>
    <div class="flex w-full flex-col justify-between py-4">
        <div class="mb-4 flex items-center justify-between">
            <h2 class="text-primary text-3xl font-extrabold">Gestion de Unidades de Medida</h2>
            <asp:Button
                ID="btnCrearNuevo"
                runat="server"
                Text="Crear Unidad Medida"
                OnClick="btnCrearNuevo_Click"
                CssClass="bg-primary mt-2 inline-block rounded px-4 py-2 font-semibold text-white shadow transition-colors hover:bg-primary/85 hover:cursor-pointer focus:ring-primary focus:outline-none focus:ring-2 focus:ring-offset-2" />
        </div>
    </div>

    <%-- Contenedor principal de la tabla --%>
    <div class="flex rounded-lg bg-white p-4">
        <div class="flex w-full flex-col justify-between">
            <asp:UpdatePanel ID="updUnidadesMedida" runat="server">
                <ContentTemplate>
                    <asp:Repeater runat="server" ID="rpttbUnidadMedida" OnItemCommand="rpttbUnidadMedida_ItemCommand">
                        <%-- Cabecera de la tabla --%>
                        <HeaderTemplate>
                            <table class="w-full table-auto border-collapse overflow-hidden rounded-lg shadow" id="tbUnidadMedida" style="width: 100%">
                                <thead>
                                    <tr class="bg-primary text-white">
                                        <th class="py-2">#</th>
                                        <th class="py-2">Nombre</th>
                                        <th class="py-2">Abreviatura</th>
                                        <th class="py-2">Acciones</th>
                                    </tr>
                                </thead>
                                <tbody>
                        </HeaderTemplate>

                        <%-- Cuerpo de la tabla (filas de datos) --%>
                        <ItemTemplate>
                            <tr class=" transition-colors hover:bg-gray-100">
                                <td class="px-4 py-2"><%# Eval("Id") %></td>
                                <td class="px-4 py-2"><%# Eval("Nombre") %></td>
                                <td class="px-4 py-2"><%# Eval("Abreviatura") %></td>

                                <td class="px-4 py-2">
                                    <div style="display: flex; gap: 0.5rem;">

                                        <asp:Button runat="server" Text="Ver"
                                            CommandName="Ver" CommandArgument='<%# Eval("Id") %>'
                                            CssClass="rounded bg-blue-500 px-3 py-1 text-white transition-colors hover:bg-blue-600 hover:cursor-pointer" />

                                        <asp:Button runat="server" Text="Editar"
                                            CommandName="Editar" CommandArgument='<%# Eval("Id") %>'
                                            CssClass="rounded bg-yellow-500 px-3 py-1 text-white transition-colors hover:bg-yellow-600 hover:cursor-pointer" />
                                        <asp:Button runat="server" Text="Eliminar"
                                            CommandName="Eliminar" CommandArgument='<%# Eval("Id") %>'
                                            CssClass="rounded bg-red-500 px-3 py-1 text-white transition-colors hover:bg-red-600 hover:cursor-pointer" />

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
        <script>
            // Función para inicializar DataTables en nuestra tabla
            $(document).ready(function () {
                $('#tbUnidadMedida').DataTable({

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
    </div>
</asp:Content>
