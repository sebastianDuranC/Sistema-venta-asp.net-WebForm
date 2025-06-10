<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="DetalleVentas.aspx.cs" Inherits="CapaPresentacion.Pages.Ventas.DetalleVentas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mx-auto px-4 py-8">
        <!-- Encabezado -->
        <div class="mb-6 flex items-center justify-between">
            <h2 class="text-3xl font-bold text-primary">📋 Detalle de Venta<asp:Literal ID="litVentaId" runat="server" /></h2>
            <asp:Button ID="btnVolver" runat="server" Text="← Volver a Ventas"
                CssClass="rounded-lg bg-primary px-6 py-3 font-medium text-white shadow-md transition-colors duration-200 hover:bg-gray-800"
                OnClick="btnVolver_Click" />
        </div>

        <!-- Contenedor de la tabla -->
        <div class="overflow-hidden rounded-xl bg-white shadow-lg">
            <asp:GridView ID="gvDetalle" runat="server" AutoGenerateColumns="false"
                CssClass="w-full"
                GridLines="None"
                HeaderStyle-CssClass="bg-primary px-6 py-4 text-sm font-semibold uppercase tracking-wider text-white"
                RowStyle-CssClass="border-b border-gray-200 transition-colors duration-150 hover:bg-gray-50"
                AlternatingRowStyle-CssClass="bg-gray-50">
                <Columns>
                    <asp:BoundField DataField="VentaId" HeaderText="№ venta"
                        ItemStyle-CssClass="px-6 py-4 text-center font-medium text-gray-500" />
                    <asp:BoundField DataField="Fecha" HeaderText="Fecha"
                        ItemStyle-CssClass="px-6 py-4 text-center font-medium text-gray-500" />
                    <asp:BoundField DataField="TipoVenta" HeaderText="Tipo de venta"
                        ItemStyle-CssClass="px-6 py-4 text-center font-medium text-gray-500" />
                    <asp:BoundField DataField="UsuarioNombre" HeaderText="Usuario"
                        ItemStyle-CssClass="px-6 py-4 text-center font-medium text-gray-500" />
                    <asp:BoundField DataField="MetodoPagoNombre" HeaderText="Metodo de pago"
                        ItemStyle-CssClass="px-6 py-4 text-center font-medium text-gray-500" />
                    <asp:BoundField DataField="ProductoNombre" HeaderText="Producto"
                        ItemStyle-CssClass="px-6 py-4 font-medium text-gray-900" />
                    <asp:BoundField DataField="PrecioUnitario" HeaderText="Precio Unitario"
                        DataFormatString="{0:C}"
                        ItemStyle-CssClass="px-6 py-4 text-right" />
                    <asp:BoundField DataField="Cantidad" HeaderText="Cantidad"
                        ItemStyle-CssClass="px-6 py-4 text-center" />
                    <asp:BoundField DataField="SubTotal" HeaderText="Subtotal"
                        DataFormatString="{0:C}"
                        ItemStyle-CssClass="px-6 py-4 text-right font-semibold text-green-600" />
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
