<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.Master" AutoEventWireup="true" CodeBehind="~/Paginas/ConsultarAcao.aspx.cs" Inherits="UI.Web.EC.Paginas.ConsultarAcao" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <link href="../EC.UI.WebControls/MessageBox/Styles/Outlook.css" rel="stylesheet" />
    <link href="../EC.UI.WebControls/MessageBox/Styles/UniCEUB.css" rel="stylesheet" />

    <script src="../Includes/ea.modal.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#closeModalpopup").click(function () {
                document.location = "../default.aspx";
            });
        });
    </script>


    <h3>Consultar Ação</h3>

    <div class="form-in">
        <div class="row">
            <div class="column w100">
                Responsável:
            </div>
            <div class="column w250">
                <asp:DropDownList ID="ddlPessoa" runat="server" Style="margin-left: 0px"></asp:DropDownList>
            </div>
        </div>

        <div class="row">
            <div class="column w100">
                Status:
            </div>

            <div class="column w250">
                <asp:DropDownList ID="ddlStatus" runat="server" Style="margin-left: 0px"></asp:DropDownList>
            </div>
        </div>

        <div class="row">
            <div class="column w100">
                Prioridade:
            </div>
            <div class="column w250">
                <asp:DropDownList ID="ddlPrioridade" runat="server"></asp:DropDownList>

            </div>
        </div>

        <div class="form-bottombuttons">
            <asp:Button ID="btnConsultar" runat="server" Text="Pesquisar" OnClick="btnConsultar_Click" Width="65px" />
            <asp:Button ID="btnNovo" runat="server" Text="Novo" Width="65px" OnClick="btnNovo_Click" />
        </div>
    </div>

    <div class="form-separator"></div>

    <h3>Ações</h3>
    <sgi:GridView ID="grdAcao" runat="server" AutoGenerateColumns="False">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:BoundField DataField="ID_ACAO" HeaderText="Nº"></asp:BoundField>
            <asp:BoundField DataField="TITULO" HeaderText="Titulo"></asp:BoundField>
            <asp:BoundField DataField="INICIO" HeaderText="Data" />
            <asp:BoundField DataField="ID_PESSOA" HeaderText="Responsável" />

        </Columns>

    </sgi:GridView>


</asp:Content>
