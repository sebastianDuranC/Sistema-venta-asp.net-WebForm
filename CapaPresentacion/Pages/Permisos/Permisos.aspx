<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Permisos.aspx.cs" Inherits="CapaPresentacion.Form.Index" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    Formularios
     <h3 class=""> Registrar nuevo formularios</h3>
    <div>
        <asp:Label Text="Nombre del formulario" runat="server" />
        <asp:TextBox ID="txtFormNombre" runat="server"></asp:TextBox>
        <asp:Label Text="Nombre de la ruta" runat="server" />
        <asp:TextBox ID="txtformRuta" runat="server"></asp:TextBox>
        <asp:Button ID="btnRegistrarForm" runat="server" Text="Registrar formulario" OnClick="btnRegistrarForm_Click"/>
    </div>
    <div>
        <asp:Label ID="lblMensaje" runat="server" />
    </div>
</asp:Content>
