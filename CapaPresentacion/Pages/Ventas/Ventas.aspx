<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Ventas.aspx.cs" Inherits="CapaPresentacion.Pages.Ventas.Ventas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mx-auto px-4 py-8">
        <div class="flex items-center justify-between">
            <h1 class="mb-6 mt-6 text-3xl font-bold text-gray-800">Gestión de Ventas</h1>
            <asp:Button Text="Crear venta" runat="server" class="rounded-lg bg-primary p-2 text-white" ID="crearVentas" OnClick="crearVentas_Click" />
        </div>
        <div class="rounded-lg bg-white p-6 shadow-lg">
            <table id="tablaVentas" class="display compact" style="width: 100%">
                <thead class="rounded-lg bg-primary p-1 text-white">
                    <tr>
                        <th>№ venta</th>
                        <th>Fecha</th>
                        <th>Cliente</th>
                        <th>Total</th>
                        <th>Método Pago</th>
                        <th>Vendedor</th>
                        <th>Acciones</th>
                    </tr>
                </thead>
                <tbody>
                    <%-- DataTables llenará esto con AJAX --%>
                </tbody>
            </table>
        </div>
    </div>

    <script>
        $(document).ready(function () {
            $('#tablaVentas').DataTable({
                processing: true,
                ajax: {
                    url: "VentaDataHandler.ashx",
                    type: "POST"
                },
                columns: [
                    { data: "VentaId" },
                    {
                        data: "Fecha",
                        render: function (data) {
                            if (data) {
                                var date = new Date(parseInt(data.substr(6)));
                                return ('0' + date.getDate()).slice(-2) + '/' +
                                    ('0' + (date.getMonth() + 1)).slice(-2) + '/' +
                                    date.getFullYear() + ' ' +
                                    ('0' + date.getHours()).slice(-2) + ':' +
                                    ('0' + date.getMinutes()).slice(-2);
                            }
                            return "";
                        }
                    },
                    { data: "Cliente" },
                    {
                        data: "Total",
                        render: $.fn.dataTable.render.number(',', '.', 2, 'Bs ')
                    },
                    { data: "MetodoPago" },
                    { data: "Vendedor" },
                    {
                        data: "VentaId",
                        render: function (data) {
                            return `
                                <div class="flex space-x-2">
                                    <a href='/Pages/Ventas/DetalleVentas.aspx?VentaId=${data}' 
                                       class="rounded bg-blue-500 px-3 py-1 text-white hover:bg-blue-600">
                                        <i class="fas fa-eye"></i> Ver
                                    </a>
                                    <a href='/Pages/Ventas/EditarVentas.aspx?VentaId=${data}' 
                                       class="rounded bg-yellow-500 px-3 py-1 text-black hover:bg-yellow-600">
                                        <i class="fas fa-edit"></i> Editar
                                    </a>
                                    <button type="button" onclick="confirmarEliminacion(${data})" 
                                       class="rounded bg-red-500 px-3 py-1 text-white hover:bg-red-600">
                                        <i class="fas fa-trash"></i> Eliminar
                                    </button>
                                </div>`;
                        },
                        orderable: false
                    }
                ],
                language: {
                    url: '/Scripts/DataTables/es-ES.json',
                    decimal: ","
                },
                dom: '<"flex justify-between items-center mb-4"<"botonesExportar"B><"filtroBusqueda"f>>rtip',
                buttons: [
                    { //EXCEL
                        extend: 'excelHtml5',
                        text: 'Exportar a Excel',
                        filename: 'Reporte de ventas',
                        exportOptions: {
                            columns: ':not(:last-child)'
                        },
                        customize: function (xlsx) {
                            var sheet = xlsx.xl.worksheets['sheet1.xml'];

                            // Aplicar estilo a todas las celdas
                            $('row c', sheet).attr('s', '55'); // '55' es un estilo predefinido que incluye envoltura de texto

                            // Centrar texto en la primera fila (cabecera)
                            $('row:eq(0) c', sheet).attr('s', '51'); // '51' es un estilo predefinido para centrado y negrita
                        }
                    },
                    { //PDF
                        extend: 'pdf',
                        text: 'Exportar a PDF',
                        filename: 'Reporte de ventas',
                        exportOptions: { columns: ':not(:last-child)' },
                        className: 'bg-red-500 hover:bg-red-600 text-white px-3 py-1 rounded',
                        pageSize: 'A4',
                        orientation: 'portrait',
                        customize: function (doc) {
                            // Establecer márgenes de la página: [izquierda, arriba, derecha, abajo]
                            doc.pageMargins = [0, 20, 0, 10];


                            // Estilo para los encabezados de la tabla
                            doc.styles.tableHeader = {
                                bold: true,
                                fontSize: 12,
                                color: 'white',
                                fillColor: '#EF4444', // Color primario
                                alignment: 'center',
                                margin: [0, 5, 0, 5] // Espaciado interno: [izquierda, arriba, derecha, abajo]
                            };

                            // Estilo para las filas de la tabla
                            doc.styles.tableBodyOdd = {
                                margin: [0, 5, 0, 5]
                            };
                            doc.styles.tableBodyEven = {
                                margin: [0, 5, 0, 5]
                            };

                            // Centrar la tabla utilizando columnas vacías a los lados
                            var table = doc.content[1];
                            doc.content[1] = {
                                columns: [
                                    { width: '*', text: '' },
                                    {
                                        width: 'auto',
                                        table: table.table,
                                        layout: table.layout
                                    },
                                    { width: '*', text: '' }
                                ]
                            };
                        }
                    },
                    {
                        extend: 'pageLength',
                        className: 'bg-gray-200 hover:bg-gray-300 px-3 py-1 rounded'
                    }
                ],
                lengthMenu: [[1, 5, 10, -1], ["1 registro", "5 registros", "10 registros", "Todos"]],
                pageLength: 5
            });
        });

        function confirmarEliminacion(ventaId) {
            Swal.fire({
                title: '¿Estás seguro?',
                text: "Esta acción no se puede deshacer.",
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#d33',
                cancelButtonColor: '#3085d6',
                confirmButtonText: 'Sí, eliminar',
                cancelButtonText: 'Cancelar',
                allowOutsideClick: false,
                allowEscapeKey: false,
                backdrop: true
            }).then((result) => {
                if (result.isConfirmed) {
                    eliminarVenta(ventaId);
                }
            });
        }

        function eliminarVenta(ventaId) {
            $.ajax({
                type: "POST",
                url: "/Pages/Ventas/EliminarVentaHandler.ashx",
                data: { ventaId: ventaId },
                dataType: "json",
                success: function (response) {
                    if (response.success) {
                        Swal.fire({
                            title: 'Eliminado',
                            text: response.message,
                            icon: 'success',
                            timer: 2000
                        });
                        $('#tablaVentas').DataTable().ajax.reload();
                    } else {
                        Swal.fire('Error', response.message, 'error');
                    }
                },
                error: function (xhr, status, error) {
                    Swal.fire('Error', 'Error de conexión: ' + error, 'error');
                }
            });
        }
    </script>
</asp:Content>
