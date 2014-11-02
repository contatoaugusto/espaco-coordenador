<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.Master" AutoEventWireup="true" CodeBehind="~/Paginas/AgendaReuniao.aspx.cs" Inherits="UI.Web.EC.Reuniao.AgendaReuniao" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   
   <div class="form-separator"></div>

    <h3>Manter Reunião</h3>
    <div class="form-in">
        <div class="row">
            <div class="column w100">
                Tipo de Reuniao:
            </div>
            <div class="column w200">
                <sgi:DropDownList ID="ddlTipoReuniao" runat="server"></sgi:DropDownList>
            </div>
            <div class="column w100">
                Semestre:
            </div>
            <div class="column w200">
                <sgi:DropDownList ID="ddlSemestre" runat="server">
                    <asp:ListItem Value="1">01</asp:ListItem>
                    <asp:ListItem Value="2">02</asp:ListItem>
                </sgi:DropDownList>
            </div>
        </div>
        
        <div class="row">
            <div class="column w100">
                Data:
            </div>
            <div class="column w200">
                <sgi:DropDownList ID="ddlDia" runat="server"></sgi:DropDownList>&nbsp&nbsp&nbsp
                <sgi:DropDownList ID="ddlMes" runat="server"></sgi:DropDownList>&nbsp&nbsp&nbsp   
                <sgi:DropDownList ID="ddlAno" runat="server"></sgi:DropDownList>
            </div>
        
            <div class="column w100">
                Hora:
            </div>
            <div class="column w200">
                <sgi:DropDownList ID="ddlHora" runat="server"></sgi:DropDownList>&nbsp&nbsp&nbsp
                <sgi:DropDownList ID="ddlMinuto" runat="server"></sgi:DropDownList>
            </div>
        </div>

        <div class="row">
            <div class="column w100">
                Título:
            </div>
            <div class="column w500">
                <asp:TextBox ID="txtTitulo" runat="server" Width="500px" TextMode="MultiLine" Rows="1"></asp:TextBox>
            </div>
        </div>
       <div class="row">    
            <div class="column w100">
                Local:
            </div>
            <div class="column w500">
                <asp:TextBox ID="txtLocal" runat="server" Width="500px" TextMode="MultiLine" Rows="1"></asp:TextBox>
            </div>
        </div>
        
        <div class="row">    
            <div class="column w100">
                Pauta:
            </div>
            <div class="column w500">
                <asp:TextBox ID="TxtPauta" runat="server" Width="500px" TextMode="MultiLine" Rows="3" Style="margin-left: 0px"></asp:TextBox>
                    <asp:ImageButton ID="btnIncluirauta" runat="server" ImageUrl="~/Imagens/adicionar.jpg" OnClick="ImageButton1_Click" Width="23px" />
            </div>
        </div>
    </div>
            

        <asp:GridView ID="grdPauta" runat="server" AutoGenerateColumns="False" Width="70%" Style="margin-left: 15%; margin-right: 15%;">
            <Columns>
                <asp:BoundField HeaderText="Nº" DataField="ITEM" />
                <asp:BoundField HeaderText="Descricão" DataField="DESCRICAO" />
            </Columns>
        </asp:GridView>

    <div class="form-bottombuttons">
        <asp:Button ID="btnSalvarReuniao" runat="server" Text="Salvar" Width="65px" OnClick="btnSalvarReuniao_Click" />
        <asp:Button ID="btnVoltar" runat="server" Text="Voltar" Width="65px" OnClick="btnVoltar_Click" />
    </div >    
    

</asp:Content>
