<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="CapaPresentacion._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="mb-6 grid grid-cols-1 gap-6 md:grid-cols-3">
        <div class="flex flex-col justify-between rounded-xl bg-white p-6 shadow">
            <span class="text-sm text-primary/70">Ventas del Día</span>
            <span class="text-3xl font-bold text-primary">Bs. 0</span>
        </div>
        <div class="flex flex-col justify-between rounded-xl bg-white p-6 shadow">
            <span class="text-sm text-primary/70">Total Ingreso del Mes</span>
            <span class="text-3xl font-bold text-primary">Bs. 0</span>
        </div>
        <div class="flex flex-col justify-between rounded-xl bg-white p-6 shadow">
            <span class="text-sm text-primary/70">Platos del Día</span>
            <span class="text-3xl font-bold text-primary">0</span>
        </div>
    </div>
    <div class="mb-6 grid grid-cols-1 gap-6 md:grid-cols-2">
        <div class="rounded-xl bg-white p-6 shadow">
            <span class="font-semibold text-primary">Ventas Diarias</span>
            <div class="flex h-32 items-center justify-center text-primary/20">[Gráfica]</div>
        </div>
        <div class="rounded-xl bg-white p-6 shadow">
            <span class="font-semibold text-primary">Top 5 Productos</span>
            <div class="flex h-32 items-center justify-center text-primary/20">[Lista de productos]</div>
        </div>
    </div>
    <div class="grid grid-cols-1 gap-6 md:grid-cols-2">
        <div class="rounded-xl bg-white p-6 shadow">
            <span class="font-semibold text-primary">Tipos de Venta</span>
            <div class="flex h-24 items-center justify-center text-primary/20">[Gráfica]</div>
        </div>
        <div class="rounded-xl bg-white p-6 shadow">
            <span class="font-semibold text-primary">Últimos Pedidos</span>
            <div class="overflow-x-auto">
                <table class="mt-2 min-w-full text-left text-sm">
                    <thead>
                        <tr>
                            <th class="font-bold text-primary">Cliente</th>
                            <th class="font-bold text-primary">Tipo</th>
                            <th class="font-bold text-primary">Total</th>
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
