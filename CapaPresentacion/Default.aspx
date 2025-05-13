<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="CapaPresentacion._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="mb-6 grid grid-cols-1 gap-6 md:grid-cols-3">
        <div class="flex flex-col justify-between rounded-xl bg-white p-6 shadow">
            <span class="text-sm text-gray-500">Ventas del Día</span>
            <span class="text-3xl font-bold text-[#212f3d]">Bs. 0</span>
        </div>
        <div class="flex flex-col justify-between rounded-xl bg-white p-6 shadow">
            <span class="text-sm text-gray-500">Total Ingreso del Mes</span>
            <span class="text-3xl font-bold text-[#212f3d]">Bs. 0</span>
        </div>
        <div class="flex flex-col justify-between rounded-xl bg-white p-6 shadow">
            <span class="text-sm text-gray-500">Platos del Día</span>
            <span class="text-3xl font-bold text-[#212f3d]">0</span>
        </div>
    </div>
    <div class="mb-6 grid grid-cols-1 gap-6 md:grid-cols-2">
        <div class="rounded-xl bg-white p-6 shadow">
            <span class="font-semibold">Ventas Diarias</span>
            <div class="flex h-32 items-center justify-center text-gray-300">[Gráfica]</div>
        </div>
        <div class="rounded-xl bg-white p-6 shadow">
            <span class="font-semibold">Top 5 Productos</span>
            <div class="flex h-32 items-center justify-center text-gray-300">[Lista de productos]</div>
        </div>
    </div>
    <div class="grid grid-cols-1 gap-6 md:grid-cols-2">
        <div class="rounded-xl bg-white p-6 shadow">
            <span class="font-semibold">Tipos de Venta</span>
            <div class="flex h-24 items-center justify-center text-gray-300">[Gráfica]</div>
        </div>
        <div class="rounded-xl bg-white p-6 shadow">
            <span class="font-semibold">Últimos Pedidos</span>
            <div class="overflow-x-auto">
                <table class="mt-2 min-w-full text-left text-sm">
                    <thead>
                        <tr>
                            <th class="font-bold">Cliente</th>
                            <th class="font-bold">Tipo</th>
                            <th class="font-bold">Total</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr class="text-gray-400">
                            <td colspan="3">Sin datos</td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
</asp:Content>
