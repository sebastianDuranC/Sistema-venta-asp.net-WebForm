<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="CapaPresentacion._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="mb-6 grid grid-cols-1 gap-6 md:grid-cols-3">
        <div class="flex flex-col justify-between rounded-xl bg-white p-6 shadow">
            <span class="text-primary/70 text-sm">Ventas del Día</span>
            <span class="text-primary text-3xl font-bold">Bs. 0</span>
        </div>
        <div class="flex flex-col justify-between rounded-xl bg-white p-6 shadow">
            <span class="text-primary/70 text-sm">Total Ingreso del Mes</span>
            <span class="text-primary text-3xl font-bold">Bs. 0</span>
        </div>
        <div class="flex flex-col justify-between rounded-xl bg-white p-6 shadow">
            <span class="text-primary/70 text-sm">Platos del Día</span>
            <span class="text-primary text-3xl font-bold">0</span>
        </div>
    </div>
    <div class="mb-6 grid grid-cols-1 gap-6 md:grid-cols-2">
        <div class="rounded-xl bg-white p-6 shadow">
            <span class="text-primary font-semibold">Ventas Diarias</span>
            <div class="text-primary/20 flex h-32 items-center justify-center">[Gráfica]</div>
        </div>
        <div class="rounded-xl bg-white p-6 shadow">
            <span class="text-primary font-semibold">Top 5 Productos</span>
            <div class="text-primary/20 flex h-32 items-center justify-center">[Lista de productos]</div>
        </div>
    </div>
    <div class="grid grid-cols-1 gap-6 md:grid-cols-2">
        <div class="rounded-xl bg-white p-6 shadow">
            <span class="text-primary font-semibold">Tipos de Venta</span>
            <div class="text-primary/20 flex h-24 items-center justify-center">[Gráfica]</div>
        </div>
        <div class="rounded-xl bg-white p-6 shadow">
            <span class="text-primary font-semibold">Últimos Pedidos</span>
            <div class="overflow-x-auto">
                <table class="mt-2 min-w-full text-left text-sm">
                    <thead>
                        <tr>
                            <th class="text-primary font-bold">Cliente</th>
                            <th class="text-primary font-bold">Tipo</th>
                            <th class="text-primary font-bold">Total</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr class="text-primary/30">
                            <td colspan="3">Sin datos</td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
</asp:Content>
