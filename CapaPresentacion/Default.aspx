<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="CapaPresentacion._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mx-auto px-4 py-8">

        <div class="mb-8 grid grid-cols-1 gap-6 md:grid-cols-2 lg:grid-cols-3">
            <div class="flex items-center rounded-lg bg-white p-6 shadow-md">
                <div class="mr-4 rounded-full bg-blue-500 p-4 text-white">
                    <img src="/wwwroot/images/icons8-porcentaje-100.png" alt="icon venta" class="h-8 w-8"/>
                </div>
                <div>
                    <p class="text-sm text-gray-500">Ventas del Mes</p>
                    <asp:Label ID="lblVentasMes" runat="server" Text="Bs 0.00" CssClass="text-2xl font-bold text-gray-800"></asp:Label>
                </div>
            </div>
            <div class="flex items-center rounded-lg bg-white p-6 shadow-md">
                <div class="mr-4 rounded-full bg-green-500 p-4 text-white">
                    <img src="/wwwroot/images/icons8-cheeseburger-100.png" alt="icon venta" class="h-8 w-8"/>
                </div>
                <div>
                    <p class="text-sm text-gray-500">Unidades Vendidas (Mes)</p>
                    <asp:Label ID="lblProductosVendidos" runat="server" Text="0" CssClass="text-2xl font-bold text-gray-800"></asp:Label>
                </div>
            </div>
            <div class="flex items-center rounded-lg bg-white p-6 shadow-md">
                <div class="mr-4 rounded-full bg-yellow-500 p-4 text-white">
                     <img src="/wwwroot/images/icons8-inventarios-100.png" alt="icon venta" class="h-8 w-8"/>
                </div>
                <div>
                    <p class="text-sm text-gray-500">Items en Inventario</p>
                    <asp:Label ID="lblItemsInventario" runat="server" Text="0" CssClass="text-2xl font-bold text-gray-800"></asp:Label>
                </div>
            </div>
        </div>

        <div class="grid grid-cols-1 gap-8 lg:grid-cols-2">
            <div class="rounded-lg bg-white p-6 shadow-md">
                <h2 class="mb-4 text-xl font-semibold text-gray-700">Resumen de Ventas (Últimos 7 Días)</h2>
                <div style="height: 350px;">
                    <canvas id="graficoVentasSemana"></canvas>
                </div>
            </div>
            <div class="rounded-lg bg-white p-6 shadow-md">
                <h2 class="mb-4 text-xl font-semibold text-gray-700">Top 5 Productos Más Vendidos</h2>
                <div style="height: 350px;">
                    <canvas id="graficoTopProductos"></canvas>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        document.addEventListener('DOMContentLoaded', function () {

            // --- Cargar datos para la Gráfica 1: Ventas de la Semana ---
            fetch('DashboardHandler.ashx?grafica=ventasSemana') // Llamada al Handler
                .then(response => response.json()) // Convertir la respuesta a JSON
                .then(data => {
                    // Ya no hay propiedad ".d", los datos vienen directamente
                    renderizarGraficoVentas(data);
                })
                .catch(error => console.error('Error al cargar datos de ventas:', error));


            // --- Cargar datos para la Gráfica 2: Top Productos Vendidos ---
            fetch('DashboardHandler.ashx?grafica=topProductos') // Llamada al Handler
                .then(response => response.json())
                .then(data => {
                    renderizarGraficoProductos(data);
                })
                .catch(error => console.error('Error al cargar datos de productos:', error));

        });

        // --- Funciones para renderizar las gráficas (sin cambios) ---
        function renderizarGraficoVentas(dataVentas) {
            const ctx = document.getElementById('graficoVentasSemana').getContext('2d');
            new Chart(ctx, {
                type: 'bar',
                data: {
                    labels: dataVentas.Labels,
                    datasets: [{ label: 'Ventas en Bs', data: dataVentas.Data, backgroundColor: 'rgba(59, 130, 246, 0.5)', borderColor: 'rgba(59, 130, 246, 1)', borderWidth: 1 }]
                },
                options: { responsive: true, maintainAspectRatio: false, scales: { y: { beginAtZero: true } } }
            });
        }

        function renderizarGraficoProductos(dataProductos) {
            const ctx = document.getElementById('graficoTopProductos').getContext('2d');
            new Chart(ctx, {
                type: 'doughnut',
                data: {
                    labels: dataProductos.Labels,
                    datasets: [{
                        label: 'Cantidad Vendida',
                        data: dataProductos.Data,
                        backgroundColor: ['rgba(255, 99, 132, 0.7)', 'rgba(54, 162, 235, 0.7)', 'rgba(255, 206, 86, 0.7)', 'rgba(75, 192, 192, 0.7)', 'rgba(153, 102, 255, 0.7)'],
                        hoverOffset: 4
                    }]
                },
                options: { responsive: true, maintainAspectRatio: false, plugins: { legend: { position: 'top' } } }
            });
        }
    </script>
</asp:Content>
