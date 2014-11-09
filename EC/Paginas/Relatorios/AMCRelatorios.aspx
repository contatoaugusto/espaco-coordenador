<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.Master" AutoEventWireup="true" CodeBehind="AMCRelatorios.aspx.cs" Inherits="UI.Web.EC.Paginas.Relatorios.AMCRelatorios" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <div class="form-separator"></div>
    <div class="form-in">
        <div class="row">
            <div class="column">
                <h3><a href='<%= Library.ResolveClientUrl("~/Paginas/Relatorios/ProfessorQuestoes.aspx")%>'><span>Qtde. Quesões por Professor</span></a></h3>
            </div>
        </div>
     </div>

</asp:Content>
