<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="CapaPresentacion.Pages.rolpermisosmapping.Index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h3>Formulario de acceso - Bit Forms</h3>
    <div>
        <h3>Manejo de permisos</h3>
        <asp:Repeater runat="server" ID="rptRoles" OnItemDataBound="rptRoles_ItemDataBound">
            <ItemTemplate>
                <div class="card-header">
                    <h4><%# Eval("Nombre") %> </h4>
                </div>
                <div class="card-body">
                    <asp:Repeater runat="server" ID="rptForm" OnItemDataBound="rptForm_ItemDataBound">
                        <ItemTemplate>
                            <div>
                                <asp:CheckBox runat="server" ID="checkFormPermisos" />
                                <asp:Label ID="Label1" runat="server"> <%# DataBinder.Eval(Container.DataItem, "FormNombre") %></asp:Label>
                            </div>
                            <asp:HiddenField runat="server" ID="hdnFormId" Value='<%# DataBinder.Eval(Container.DataItem, "Id") %>' />
                            <asp:Label runat="server"> <%# DataBinder.Eval(Container.DataItem, "FormRuta") %> </asp:Label>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
                <div class="card-footer">
                    <asp:Label runat="server" ID="lblRolId" Visible="false" Text=' <%# Eval("Id") %> '> </asp:Label>
                </div>
            </ItemTemplate>
        </asp:Repeater>
        <asp:Button runat="server" class="" ID="btnRolPermisos" Text="Guardar Permisos" OnClick="btnRolPermisos_Click"/>
        <asp:Label runat="server" ID="lblMensaje"></asp:Label>
    </div>
</asp:Content>
