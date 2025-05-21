<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Ventas.aspx.cs" Inherits="CapaPresentacion.Pages.Ventas.Ventas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mx-auto px-4 py-8">
        <div class="flex items-center justify-between">
            <h1 class="mb-6 mt-6 text-3xl font-bold text-gray-800">Gestión de Ventas</h1>
            <asp:Button Text="Crear venta" runat="server"  class="rounded-lg bg-primary p-2 text-white" ID="crearVentas" OnClick="crearVentas_Click"/>
        </div>
        <div class="rounded-lg bg-white p-6 shadow-lg">
            <table id="tablaVentas" class="display compact" style="width: 100%">
                <thead class="rounded-lg bg-primary p-1 text-white">
                    <tr>
                        <th>#</th>
                        <th>Fecha</th>
                        <th>Cliente</th>
                        <th>Total</th>
                        <th>Método Pago</th>
                        <th>Vendedor</th>
                        <th>Acciones</th>
                    </tr>
                </thead>
                <asp:Repeater ID="rptVentas" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td><%# Eval("VentaId") %></td>
                            <td><%# Eval("Fecha", "{0:dd/MM/yyyy HH:mm}") %></td>
                            <td><%# Eval("Cliente") %></td>
                            <td><%# Eval("Total", "{0:C}") %></td>
                            <td><%# Eval("MetodoPago") %></td>
                            <td><%# Eval("Vendedor") %></td>
                            <td>
                                <asp:LinkButton ID="lnkVerDetalle" runat="server"
                                    CssClass="btn btn-info btn-sm"
                                    CommandName="VerDetalle"
                                    CommandArgument='<%# Eval("Id") %>'
                                    Text="<i class='fas fa-search'></i> Ver"></asp:LinkButton>
                                <%-- Otros botones para Editar/Eliminar (con sus CommandName/Argument) --%>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <tbody>
                    <%-- El Repeater generará las filas aquí si se usa para data inicial.
             Si es full server-side con DataTables.js, este tbody puede estar vacío inicialmente
             y DataTables lo poblará vía AJAX. --%>
                </tbody>
            </table>

        </div>
    </div>
    <script>
        $(document).ready(function () {
            $('#tablaVentas').DataTable({
                processing: true, // Muestra un indicador de "Procesando..."
                ajax: {
                    url: "VentaDataHandler.ashx", // Un handler HTTP que crearemos
                    type: "POST" // O "GET", según cómo diseñes tu handler
                },
                columns: [ // Define las columnas que DataTables espera y cómo mapearlas desde la respuesta del servidor
                    { data: "VentaId" },
                    {
                        data: "Fecha", render: function (data) {
                            if (data) {
                                // Formatear la fecha (DataTables recibe el string JSON de fecha)
                                var date = new Date(parseInt(data.substr(6)));
                                return ('0' + date.getDate()).slice(-2) + '/' + ('0' + (date.getMonth() + 1)).slice(-2) + '/' + date.getFullYear();
                            }
                            return "";
                        }
                    },
                    { data: "Cliente" },
                    { data: "Total" },
                    { data: "MetodoPago" },
                    { data: "Vendedor" },
                    {
                        data: "Id", // Usamos el Id para el CommandArgument
                        render: function (data, type, row) {
                            // Importante: Los botones aquí NO usarán postbacks de ASP.NET directamente.
                            // Si necesitas postbacks, tendrías que registrar los scripts después de cada dibujo de la tabla.
                            // Alternativamente, usar AJAX para las acciones o navegar a otra página.
                            // Para "Ver Detalle", podrías usar un simple enlace o un botón que llame JS.
                            return `<a href='VentaDetallePage.aspx?VentaId=${data}' class='btn btn-info btn-sm'><i class='fas fa-search'></i> Ver</a>`;
                            // O un botón que llame a una función JS:
                            // return `<button onclick='mostrarDetalle(${data})' class='btn btn-info btn-sm'>Ver Detalle</button>`;
                        },
                        orderable: false // Columna de acciones no ordenable
                    }
                ],
                language: {
                    url: '//cdn.datatables.net/plug-ins/1.13.10/i18n/es-ES.json'
                },
                dom: 'Bfrtip', // 'B' para los botones de exportación
                buttons: [
                    'excel', 'pdf'
                ]
            });
        });
    </script>
</asp:Content>
