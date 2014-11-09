<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.Master" AutoEventWireup="true" CodeBehind="~/Paginas/Evento.aspx.cs" Inherits="UI.Web.EC.Paginas.Evento" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <sgi:MessageBox ID="messageBox" runat="server" />

    <div class="form-separator"></div>
    <h3>Cadastrar Evento</h3>


    <div class="form-in">
        <div class="row">
            <div class="column w100">
                Tipo de Evento:
            </div>
            <div class="column w250">
                <asp:DropDownList ID="ddlTipoEvento" runat="server"></asp:DropDownList>
            </div>
        </div>

        <div class="row">
            <div class="column w100">
                Nome:
            </div>
            <div class="column w250">
                <asp:TextBox ID="TxtNome" runat="server" Width="500px" TextMode="MultiLine" Rows="1"></asp:TextBox>
            </div>
        </div>

        <div class="row">
            <div class="column w100">
                Descrição:
            </div>
            <div class="column w250">
                <asp:TextBox ID="TxtDescricao" runat="server" Width="500px" TextMode="MultiLine" Rows="3"></asp:TextBox>
            </div>
        </div>

        <div class="row">
            <div class="column w100">
                Local:
            </div>
            <div class="column w250">
                <asp:TextBox ID="TxtLocal" runat="server" Width="500px" TextMode="MultiLine" Rows="1"></asp:TextBox>
            </div>
        </div>

        <div class="row">
            <div class="column w100">
                Responsável: 
            </div>
            <div class="column w250">
                <asp:DropDownList ID="ddlPessoa" runat="server"></asp:DropDownList>

            </div>
        </div>

        <div class="row">
            <div class="column w100">
                Inicio:
            </div>
            <div class="column w250">
                <asp:DropDownList ID="ddlDia" runat="server"></asp:DropDownList>
                <asp:DropDownList ID="ddlMes" runat="server"></asp:DropDownList>
                <asp:DropDownList ID="ddlAno" runat="server"></asp:DropDownList>
                <asp:DropDownList ID="ddlHora" runat="server"></asp:DropDownList>
                <asp:DropDownList ID="ddlMinuto" runat="server"></asp:DropDownList>
            </div>
        </div>

        <div class="row">
            <div class="column w100">
                Conclusão:
            </div>
            <div class="column w250">
                <asp:DropDownList ID="ddlDia1" runat="server"></asp:DropDownList>
                <asp:DropDownList ID="ddlMes1" runat="server"></asp:DropDownList>
                <asp:DropDownList ID="ddlAno1" runat="server"></asp:DropDownList>
                <asp:DropDownList ID="ddlHora1" runat="server"></asp:DropDownList>
                <asp:DropDownList ID="ddlMinuto1" runat="server"></asp:DropDownList>
            </div>
        </div>
    
        <div class="form-bottombuttons">
            <asp:Button ID="Button1" runat="server" Text="Salvar" OnClick="Button1_Click"/>
            <%--<sgi:Button ID="btnVoltar" runat="server" Text="Voltar" OnClick="btnVoltar_Click" />--%>
        </div>
    </div>
    
</asp:Content>






















