<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/Default/Content.master"
    AutoEventWireup="true" CodeFile="DadosPessoais.aspx.cs" MaintainScrollPositionOnPostback="true" Inherits="UI.Web.EC.Coordenador.Aluno_DadosPessoais" %>

<%@ Register src="DadosPessoais.ascx" tagname="DadosPessoais" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPHTitle" runat="Server">
    Dados do Aluno
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHContent" runat="Server">

    <uc1:DadosPessoais ID="dadosPessoais" runat="server" />
    
</asp:Content>
