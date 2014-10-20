<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/Default/Content.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="UI.Web.EC.Coordenador.Aluno_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPHTitle" Runat="Server">
Aluno
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHContent" Runat="Server">
    <ul class="list">
        <li><a href="dadospessoais.aspx">Meus Dados</a></li>
        <li><a href="senhabiblioteca.aspx">Alterar Senha da Biblioteca</a></li>
        <li><a href="senhaespacoaluno.aspx">Alterar Senha do Espaço Aluno</a></li>
    </ul>
</asp:Content>

