<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.Master" AutoEventWireup="true" CodeBehind="~/Paginas/ConsultarAta.aspx.cs" Inherits="UI.Web.EC.Paginas.ConsultarAta" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

  <br />
    
    <h3>Consultar Ata de Reunião</h3>
    
    <div class="form-in">
        <div class="row">
            <div class="column w100">
                Reunião: 
            </div>
            <div class="column w250">
                <asp:DropDownList ID="ddlReuniao" runat="server" OnSelectedIndexChanged="ddlReuniao_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
            </div>
        </div>
    
        <asp:Panel ID="pnlAta" runat="server" Visible="false">
            <div class="form-subtitle">
                Identificação da Ata de Reunião: 
            </div>
        
            <div class="row">
                <div class="column w100">
                    Data da Reunião: 
                </div>
                <div class="column w250">
                    <asp:Label ID="lblReuniao" runat="server"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="column w100">
                    Ata elaborada por: 
                </div>
                <div class="column w250">
                    <asp:Label ID="lblResponsavel" runat="server"></asp:Label>
                </div>
                <div class="column w100">
                    Data Fechamento: 
                </div>
                <div class="column w250">
                    <asp:Label ID="lblFechamento" runat="server"></asp:Label>
                </div>
            </div>
            <div class="form-bottombuttons">
                <asp:Button ID="btnEditar" runat="server" Text="Editar" OnClick="btnEditar_Click" />
                <asp:HiddenField ID="hddIdAta" runat="server" />
            </div>    
        </asp:Panel>
    </div>
    
</asp:Content>
