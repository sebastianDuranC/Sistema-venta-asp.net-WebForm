<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditarVentas.aspx.cs" Inherits="CapaPresentacion.Pages.Ventas.EditarVentas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        /* Para controles asp que no tomen tailwind */
        .asp-grid th {
            background: #111;
            color: #fff;
            font-weight: 600;
            padding: 0.75rem 1rem;
        }
        .asp-grid td {
            padding: 0.75rem 1rem;
            vertical-align: middle;
        }
    </style>

    <div class="mt-6 rounded-lg bg-white p-8 shadow-md">
        <div class="mb-8 flex items-center justify-between">
            <h2 class="text-2xl font-bold">Editar Venta</h2>
            <asp:HyperLink ID="lnkCancelar" runat="server" NavigateUrl="~/Pages/Ventas/Ventas.aspx"
                CssClass="rounded bg-black px-4 py-2 font-semibold text-white transition hover:bg-gray-800" Text="Cancelar" />
        </div>

        <div class="mb-8">
            <asp:Label Text="" CssClass="mb-2 block text-base font-medium text-gray-700" runat="server" ID="lblFecha"/>
            <div class="flex flex-col md:flex-row md:gap-6">
                <div class="mb-4 flex-1 md:mb-0">
                    <label class="mb-1 block text-sm font-medium text-gray-700">Cliente:</label>
                    <asp:DropDownList ID="ddlClientes" runat="server" CssClass="w-full rounded border border-gray-300 px-3 py-2 focus:outline-none focus:ring focus:border-black" />
                </div>
                <div class="mb-4 flex-1 md:mb-0">
                    <label class="mb-1 block text-sm font-medium text-gray-700">Método de Pago:</label>
                    <asp:DropDownList ID="ddlMetodoPago" runat="server" CssClass="w-full rounded border border-gray-300 px-3 py-2 focus:outline-none focus:ring focus:border-black" />
                </div>
                <div class="flex-1">
                    <label class="mb-1 block text-sm font-medium text-gray-700">Tipo de venta:</label>
                    <asp:RadioButtonList ID="rdbEnLocal" runat="server" RepeatDirection="Horizontal" CssClass="flex gap-6">
                        <asp:ListItem Text="Llevar" Value="Llevar"></asp:ListItem>
                        <asp:ListItem Text="Local" Value="Local"></asp:ListItem>
                     </asp:RadioButtonList>
                </div>
            </div>
        </div>

        <div class="mb-8">
            <h3 class="mb-4 text-lg font-semibold text-gray-700">Detalle de venta</h3>
            <asp:GridView ID="gvDetalleVenta" runat="server" AutoGenerateColumns="False" CssClass="asp-grid w-full overflow-hidden rounded-lg border text-sm" OnRowCommand="gvDetalleVenta_RowCommand" OnRowDataBound="gvDetalleVenta_RowDataBound">
                <Columns>
                    <asp:BoundField DataField="Producto.Nombre" HeaderText="Producto" />
                    <asp:TemplateField HeaderText="Cantidad">
                        <ItemTemplate>
                            <asp:TextBox ID="txtCantidad" runat="server" Text='<%# Eval("Cantidad") %>' CssClass="w-16 rounded border border-gray-300 px-2 py-1 text-center" AutoPostBack="true" OnTextChanged="txtCantidad_TextChanged" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Producto.Precio" HeaderText="Precio unitario" />
                    <asp:BoundField DataField="SubTotal" HeaderText="SubTotal" DataFormatString="{0:C}" />
                    <asp:TemplateField HeaderText="Acciones">
                        <ItemTemplate>
                            <asp:LinkButton ID="btnEliminar" runat="server" CommandName="EliminarFila" CommandArgument='<%# Container.DataItemIndex %>' CssClass="bg-secondary p-1" Text="Eliminar" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>

        <div class="mb-8 grid grid-cols-1 gap-6 md:grid-cols-4">
            <div>
                <p class="text-lg font-semibold text-gray-700">Subtotal:</p>
                <asp:Label ID="lblSubtotal" runat="server" CssClass="text-xl font-bold text-blue-600" />
            </div>
            <div>
                <p class="text-lg font-semibold text-gray-700">Total:</p>
                <asp:Label ID="lblTotal" runat="server" CssClass="text-xl font-bold text-green-600" />
            </div>
            <div>
                <label class="mb-1 block text-sm font-medium text-gray-700">Monto de cliente:</label>
                <asp:TextBox ID="txtMontoCliente" runat="server" CssClass="w-full rounded border border-gray-300 px-3 py-2 focus:outline-none focus:ring focus:border-black" AutoPostBack="true" OnTextChanged="txtMontoCliente_TextChanged" />
            </div>
            <div>
                <label class="mb-1 block text-sm font-medium text-gray-700">Cambio: </label>
                <asp:Label Text="" ID="lblCambio" runat="server" CssClass="block w-full rounded border border-gray-300 bg-gray-100 px-3 py-2" />
            </div>
        </div>

        <div class="mt-6 flex items-center gap-4">
            <asp:Button ID="btnGuardar" runat="server" Text="Guardar Cambios"
                CssClass="rounded bg-red-600 px-4 py-2 font-semibold text-white transition hover:bg-red-700" OnClick="btnGuardar_Click" />
        </div>
    </div>
</asp:Content>