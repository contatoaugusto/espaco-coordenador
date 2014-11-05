<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.Master" AutoEventWireup="true" CodeBehind="~/Paginas/Lancamento.aspx.cs" Inherits="UI.Web.EC.Paginas.Lancamento" %>



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
    <sgi:MessageBox ID="messageBox" runat="server" />

    <h3>Incluir Lançamento</h3>


    <div class="form-in">
        <div class="row">
            <div class="column w100">
                Tipo Lancamento:
            </div>
            <div class="column w200">
                <asp:DropDownList ID="ddlTipolancamento" runat="server"></asp:DropDownList>
            </div>
        </div>

        <div class="row">
            <div class="column w100">
                Professor
            </div>
            <div class="column w200">
                <asp:DropDownList ID="ddlPessoa" runat="server"></asp:DropDownList>
            </div>
        </div>

        <div class="row">
            <div class="column w100">
                Turma:
            </div>
            <div class="column w200">
                <asp:DropDownList ID="ddlTurma" runat="server"></asp:DropDownList>
            </div>
        </div>

        <div class="row">
            <div class="column w100">
                Data
            </div>
            <div class="column w200">
                <div class="column w200">
                    <asp:DropDownList ID="ddlDia" runat="server"></asp:DropDownList>
                    &nbsp&nbsp&nbsp
                <asp:DropDownList ID="ddlMes" runat="server"></asp:DropDownList>
                    &nbsp&nbsp&nbsp 
                 <asp:DropDownList ID="ddlAno" runat="server"></asp:DropDownList>
                </div>
            </div>

            <div class="row">
                <div class="column w100">
                    Justificativa
                </div>
                <div class="column w200">
                    <asp:TextBox ID="TxtJustificativa" runat="server" Width="500px" TextMode="MultiLine" Rows="3"></asp:TextBox>
                </div>
            </div>

            <div class="row">
                <div class="column w100">
                    Providências
                </div>
                <div class="column w200">
                    <asp:TextBox ID="TxtProvidencias" runat="server" Width="500px" TextMode="MultiLine" Rows="3"></asp:TextBox>
                </div>
            </div>
            <div class="form-bottombuttons">
                <asp:Button ID="btnSalvar" runat="server" Text="Salvar" OnClick="Button1_Click" Width="65px" />
                <asp:Button ID="btnVoltar" runat="server" Text="Voltar" Width="65px" OnClick="btnVoltar_Click" />

            </div>
        </div>
    </div>
  
</asp:Content>
