<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.Master" AutoEventWireup="true" CodeBehind="~/Paginas/ConsultarAta.aspx.cs" Inherits="UI.Web.EC.Paginas.ConsultarAta" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <%--<script src="../Includes/ea.modal.js" type="text/javascript"></script>--%>
  <br />
    <h3>Consultar Ata de Reunião</h3>
    
    <div class="form-in">
        <div class="row">
            <div class="column w100">
                Reuniao: 
            </div>
            <div class="column w250">
                <asp:DropDownList ID="ddlReuniao" runat="server" OnSelectedIndexChanged="ddlReuniao_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
            </div>
        </div>
    
       <%-- <div class="row">
            <div class="column w100">
                AMC:
            </div>
            <div class="column w250">
                <asp:DropDownList ID="ddlAmc" runat="server"></asp:DropDownList>
            </div>
        </div>--%>
        
        <%--<div class="form-bottombuttons">
            <asp:Button ID="btnConsultar" runat="server" Text="Consultar" OnClick="btnConsultar_Click" Width="65px" />
            <asp:Button ID="btnNovo" runat="server" Text="Novo" Width="65px" OnClick="btnNovo_Click" />
        </div>--%>
    
        <asp:Panel ID="pnlAta" runat="server" Visible="false">
            <div class="form-subtitle">
                Ata Reuniao: 
            </div>
        
            <div class="row">
                <div class="column w100">
                    Reuniao: 
                </div>
                <div class="column w250">
                    <asp:Label ID="lblReuniao" runat="server"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="column w100">
                    Responsável: 
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
        <div class="form-separator"></div>

        <%--<sgi:GridView 
            ID="grdReuniaoAta" 
            runat="server" 
            AutoGenerateColumns="False" 
            OnRowCommand=""
            AllowPaging="true" PageSize="10"
            OnPageIndexChanging="grdItens_PageIndexChanging"
            EmptyDataText="Nenhum registro encontrado">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:BoundField DataField="REUNIAO.TITULO" HeaderText="Reunião" HeaderStyle-Width="55%" />
                <asp:TemplateField HeaderText="Responsável">
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("FUNCIONARIO.PESSOA.NOME") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Data Fechamento">
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("DATA_FECHAMENTO") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Ações">
                    <ItemTemplate>
                        <asp:LinkButton ID="HyperLink1" runat="server" CommandArgument='<%# Eval("ID_ATA")%>' CommandName="Editar" Text="Editar" CausesValidation="false" />
                        |
                        <asp:LinkButton ID="HyperLink2" runat="server" CommandArgument='<%# Eval("ID_ATA")%>' CommandName="Excluir" Text="Excluir" CausesValidation="false" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </sgi:GridView>--%>


</asp:Content>
