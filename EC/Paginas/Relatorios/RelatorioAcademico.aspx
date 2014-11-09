<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.Master" AutoEventWireup="true" CodeBehind="RelatorioAcademico.aspx.cs" Inherits="UI.Web.EC.Paginas.RelatorioAcademico" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--<script src="../Includes/ea.modal.js" type="text/javascript"></script>--%>
    
    <h3>Emitir Relatórios Acadêmicos</h3>

    <div class="form-in">

        <div class="form-in">
            <div class="row">
                <div class="column w100">
                    Semestre:
                </div>
                <div class="column w250">
                    <asp:DropDownList ID="ddlSemestre" runat="server"></asp:DropDownList>
                </div>
            </div>
            <div class="row">
                <div class="column w100">
                    Tipo de Relatório:
                </div>
                <div class="column w250">
                    <asp:DropDownList ID="ddlTipoRelatorio" runat="server">
                        <asp:ListItem Value="01">Selecione</asp:ListItem>
                        <asp:ListItem Value="02">Atrasos e Faltas</asp:ListItem>
                        <asp:ListItem Value="03">Entrega de Pautas</asp:ListItem>
                        <asp:ListItem Value="04">Entrega de Diários</asp:ListItem>
                        <asp:ListItem Value="05">Atraso na Entrega de Pautas </asp:ListItem>
                        <asp:ListItem Value="06">Atraso na Entrega de Diários</asp:ListItem>
                        <asp:ListItem Value="07">Atraso no Lançamento de Pautas </asp:ListItem>
                        <asp:ListItem Value="08">Atraso no Lançamento de Diários</asp:ListItem>
                        <asp:ListItem Value="09">Abandono de Curso por Aluno</asp:ListItem>
                        <asp:ListItem Value="10">Ocupação de Alunos por Turmas</asp:ListItem>
                        <asp:ListItem Value="11">Avaliação de Proefessores </asp:ListItem>
                        </asp:DropDownList>
                </div>
            </div>

            <div class="form-separator"></div>

            <div class="form-bottombuttons">
                <asp:Button ID="btConsultar" runat="server" Text="Gerar Relatório" />

            </div>

        </div>

</asp:Content>
