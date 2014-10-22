<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.Master" AutoEventWireup="true" CodeBehind="Prova.aspx.cs" Inherits="UI.Web.EC.Paginas.Prova" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ListBox ID="ListBox1" runat="server" DataSourceID="EntityDataSource_Questoes" DataTextField="DESCRICAO" DataValueField="ID_QUESTAO"></asp:ListBox>
    <asp:EntityDataSource ID="EntityDataSource_Questoes" runat="server" ConnectionString="name=ECEntities" DefaultContainerName="ECEntities" EnableFlattening="False" EntitySetName="QUESTAO">
    </asp:EntityDataSource>
</asp:Content>
