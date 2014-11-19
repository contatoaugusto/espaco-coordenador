<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.Master" AutoEventWireup="true" CodeBehind="RelatorioAMC.aspx.cs" Inherits="UI.Web.EC.Paginas.RelatorioAMC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--<script src="../Includes/ea.modal.js" type="text/javascript"></script>--%>
    

    <h3>Emitir Relatórios da AMC</h3>

    <div class="form-in">

        <div class="row">
            <div class="column w100">
                AMC:
            </div>
            <div class="column w250">
                <asp:DropDownList ID="ddlAmc" runat="server"></asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlAmc" ErrorMessage="<br />Campo obrigatório"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="row">
            <div class="column w100">
                Tipo de Relatório:
            </div>
            <div class="column w250">
                <asp:DropDownList ID="ddlTipoRelatorio" runat="server" OnSelectedIndexChanged="ddlTipoRelatorio_SelectedIndexChanged">
                    <asp:ListItem Value="0">Selecione</asp:ListItem>
                    <asp:ListItem Value="1">Participação de professores na AMC</asp:ListItem>
                    <asp:ListItem Value="2">Desempenho de Alunos por questão</asp:ListItem>
                    <asp:ListItem Value="3">Aproveitamento por Questão</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlTipoRelatorio" InitialValue="0" ErrorMessage="<br />Campo obrigatório"></asp:RequiredFieldValidator>
                
            </div>
        </div>

        <div class="form-separator"></div>

        <div class="form-bottombuttons">
            <asp:Button ID="btConsultar" runat="server" Text="Gerar Relatório" OnClick="btConsultar_Click" CausesValidation="true"/>
            
        </div>
        
    </div>
</asp:Content>
