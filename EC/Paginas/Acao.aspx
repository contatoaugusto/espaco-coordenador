<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.Master" AutoEventWireup="true" CodeBehind="~/Paginas/Acao.aspx.cs" Inherits="UI.Web.EC.Paginas.Acao" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <link href="../EC.UI.WebControls/MessageBox/Styles/Outlook.css" rel="stylesheet" />
    <link href="../EC.UI.WebControls/MessageBox/Styles/UniCEUB.css" rel="stylesheet" />


    <sgi:MessageBox ID="messageBox" runat="server" />

    <div class="form-separator"></div>
    <h3>Incluir Ação</h3>



    <div class="form-in">
        <div class="row">
            <div class="column w100">
                AMC:
            </div>
            <div class="column w250">
                <sgi:DropDownList ID="ddlAmc" runat="server" RequiredField="true"></sgi:DropDownList>
            </div>
        </div>

        <div class="row">
            <div class="column w100">
                Evento:
            </div>
            <div class="column w250">
                <sgi:DropDownList ID="ddlEvento" runat="server"></sgi:DropDownList>
            </div>
        </div>

        <div class="row">
            <div class="column w100">
                Reunião:
            </div>
            <div class="column w250">
                <sgi:DropDownList ID="ddlReuniao" runat="server"></sgi:DropDownList>
            </div>
        </div>

        <div class="row">
            <div class="column w250">
                Titulo:
            </div>
            <sgi:TextArea ID="TxtTitulo" runat="server" Width="80%" Rows="1" MaxLength="3000" RequiredField="true" ErrorMessage="É obrigatorio informar o título"></sgi:TextArea>
        </div>

        <div class="row">
            <div class="column w100">
                Inicio: 
            </div>
            <div class="column w250">
                <sgi:DropDownList ID="ddlDia" runat="server"></sgi:DropDownList>
                <sgi:DropDownList ID="ddlMes" runat="server"></sgi:DropDownList>
                <sgi:DropDownList ID="ddlAno" runat="server"></sgi:DropDownList>
            </div>
        </div>
        <div class="row">
            <div class="column w100">
                Conclusão:
            </div>
            <div class="column w250">
                <sgi:DropDownList ID="ddlDia1" runat="server"></sgi:DropDownList>
                <sgi:DropDownList ID="ddlMes1" runat="server"></sgi:DropDownList>
                <sgi:DropDownList ID="ddlAno1" runat="server"></sgi:DropDownList>
            </div>
        </div>
        <div class="row">
            <div class="column w100">
                Responsável:
            </div>
            <div class="column w250">
                <sgi:DropDownList ID="ddlPessoa" runat="server"></sgi:DropDownList>
            </div>
        </div>

        <div class="row">
            <div class="column w100">
                Prioridade:
            </div>
            <div class="column w250">
                <sgi:DropDownList ID="ddlPrioridade" runat="server"></sgi:DropDownList>
            </div>
        </div>

        <div class="row">
            <div class="column w100">
                Status
            </div>
            <div class="column w250">
                <sgi:DropDownList ID="ddlStatus" runat="server"></sgi:DropDownList>
            </div>
        </div>

        <div class="form-bottombuttons">
            <sgi:Button ID="btSalvar" runat="server" Text="Salvar" OnClick="Button1_Click" Width="65px" />
            <sgi:Button ID="btnVoltar" runat="server" Text="Voltar" OnClick="btnVoltar_Click" />
        </div>
    </div>
     
</asp:Content>
