<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.Master" AutoEventWireup="true" CodeBehind="~/Paginas/ConsultarAta.aspx.cs" Inherits="UI.Web.EC.Paginas.ConsultarAta" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <%--<script src="../Includes/ea.modal.js" type="text/javascript"></script>--%>
  
    <h3>Consultar Ata de Reunião</h3>

    <div class="row">
        <div class="column w100">
            Reuniao: 
        </div>
        <div class="column w250">
            <asp:DropDownList ID="ddlReuniao" runat="server"></asp:DropDownList>
        </div>
    </div>

        <div class="form-separator"></div>

        <div class="form-bottombuttons">
            <asp:Button ID="btConsultar" runat="server" Text="Consultar" />
            <asp:Button ID="btNovo" runat="server" Text="Novo" />
        </div>   
    </div>
</asp:Content>
