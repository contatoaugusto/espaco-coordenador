<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.Master" AutoEventWireup="true" CodeBehind="~/Paginas/Lancamento.aspx.cs" Inherits="UI.Web.EC.Paginas.Lancamento" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <%--<script src="../Includes/ea.modal.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#closeModalpopup").click(function () {
                document.location = "../default.aspx";
            });
        });
    </script>--%>
    <sgi:MessageBox ID="messageBox" runat="server" />
    
    <div class="form-separator"></div>
    <h3>Cadastrar Lançamento</h3>

    <div class="form-in">
        <div class="row">
            <div class="column w100">
                Tipo Lancamento:
            </div>
            <div class="column w200">
                <asp:DropDownList ID="ddlTipolancamento" runat="server"></asp:DropDownList>
            </div>
            <div class="column w100">
                Semestre:
            </div>
            <div class="column w200">
                <asp:HiddenField ID="hddSemestreCorrente" runat="server"></asp:HiddenField>
                <asp:Label ID="lblSemestreCorrente" runat="server"></asp:Label>
            </div>
        </div>

        <div class="row">
            <div class="column w100">
                Disciplina:
            </div>
            <div class="column w250">
                <sgi:DropDownList ID="ddlDisciplina" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlDisciplina_SelectedIndexChanged"></sgi:DropDownList>
            </div>
        </div>

        <div class="row">
            <div class="column w100">
                Professor
            </div>
            <div class="column w200">
                <asp:DropDownList ID="ddlFuncionario" runat="server"></asp:DropDownList>
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
                <asp:DropDownList ID="ddlDia" runat="server"/>
                <asp:DropDownList ID="ddlMes" runat="server"/>
                <asp:DropDownList ID="ddlAno" runat="server"/>
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
        <div class="form-separator"></div>
        <div class="form-bottombuttons">
            <asp:Button ID="btnSalvar" runat="server" Text="Salvar" OnClick="Button1_Click"/>
            <asp:Button ID="btnVoltar" runat="server" Text="Voltar" OnClick="btnVoltar_Click" />

        </div>
    </div>
  
</asp:Content>
