<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Ventas.aspx.cs" Inherits="CapaPresentacion.Pages.Ventas.Ventas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="flex w-full flex-col justify-between py-4">
        <div class="mb-4 flex items-center justify-between">
            <h2 class="text-primary text-3xl font-extrabold">Gestion de ventas</h2>
            <asp:Button CssClass="bg-primary mt-2 inline-block rounded px-4 py-2 font-semibold text-white shadow transition-colors hover:bg-primary/85 hover:cursor-pointer focus:ring-primary focus:outline-none focus:ring-2 focus:ring-offset-2" Text="Crear venta" runat="server" ID="btnRegistrarVenta" OnClick="btnRegistrarVenta_Click" />
        </div>
    </div>
    <div class="flex rounded-lg bg-white p-4">
        <div class="flex w-full flex-col justify-between">
            <%-- Lista --%>
            <asp:Repeater runat="server" ID="rptVentas" OnItemCommand="rptVentas_ItemCommand">
                <%-- Cabezera del table --%>
                <HeaderTemplate>
                    <table class="w-full table-auto border-collapse overflow-hidden rounded-lg shadow" id="tbVentas" style="width: 100%">
                        <thead>
                            <tr class="bg-primary text-center text-white">
                                <th class="py-2">№ venta</th>
                                <th class="py-2">Fecha</th>
                                <th class="py-2">Total</th>
                                <th class="py-2">Tipo venta</th>
                                <th class="py-2">Cliente</th>
                                <th class="py-2">Pago cliente</th>
                                <th class="py-2">Cambio cliente</th>
                                <th class="py-2">Vendedor</th>
                                <th class="py-2">Método Pago</th>
                                <th class="py-2">Acciones</th>
                            </tr>
                        </thead>
                        <tbody>
                </HeaderTemplate>
                <%-- Cuerpo del table --%>
                <ItemTemplate>
                    <tr>
                        <td class="px-4 py-2"><%# Eval("VentaId") %>  </td>
                        <td class="px-4 py-2"><%# Eval("Fecha") %> </td>
                        <td class="px-4 py-2"><%# Eval("Total") %> </td>
                        <td class="px-4 py-2"><%# Eval("TipoVenta") %> </td>
                        <td class="px-4 py-2"><%# Eval("Cliente") %> </td>
                        <td class="px-4 py-2"><%# Eval("MontoRecibido") %> </td>
                        <td class="px-4 py-2"><%# Eval("CambioDevuelto") %> </td>
                        <td class="px-4 py-2"><%# Eval("Vendedor") %> </td>
                        <td class="px-4 py-2"><%# Eval("MetodoPago") %> </td>
                        <td class="px-4 py-2">
                            <div style="display: flex; gap: 0.5rem;">
                                <asp:Button runat="server" Text="Ver"
                                    CommandName="Ver" CommandArgument='<%# Eval("VentaId") %>'
                                    CssClass="rounded bg-blue-600 px-3 py-1 text-white transition-colors hover:bg-blue-600/70/ hover:cursor-pointer" />
                                <asp:Button runat="server" Text="Editar"
                                    CommandName="Editar" CommandArgument='<%# Eval("VentaId") %>'
                                    CssClass="rounded bg-yellow-500 px-3 py-1 text-white transition-colors hover:bg-yellow-600 hover:cursor-pointer" />
                                <asp:Button runat="server" Text="Eliminar"
                                    CommandName="Eliminar" CommandArgument='<%# Eval("VentaId") %>'
                                    CssClass="rounded bg-secondary px-3 py-1 text-white transition-colors hover:bg-secondary/85 hover:cursor-pointer" />
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
            $('#tbVentas').DataTable({

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
