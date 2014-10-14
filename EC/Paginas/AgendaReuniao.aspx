<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.Master" AutoEventWireup="true" CodeBehind="~/Paginas/AgendaReuniao.aspx.cs" Inherits="UI.Web.EC.Reuniao.AgendaReuniao" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style5 {
            width: 177px;
        }

        .auto-style9 {
            width: 830px;
        }

        .auto-style10 {
            width: 740px;
        }

        .auto-style11 {
            width: 252px;
        }

        .auto-style12 {
            width: 935px;
        }

        .auto-style13 {
            width: 480px;
        }

        .auto-style16 {
            width: 179px;
        }
        .auto-style18 {
            width: 170px;
        }
        .auto-style19 {
            width: 265px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <div style="width: 1280px; height: 100%";>

        <h2 style="width: 100%; text-align: center; position: relative; top: 0px; left: 0px; height: 56px;">Dados da Reunião</h2>


        <table style="width: 100%;">

            <tr>
                <td class="auto-style13"></td>
                <td class="auto-style18">Tipo de Reuniao:<td class="auto-style10">
                    <asp:DropDownList ID="ddlTipoReuniao" runat="server"></asp:DropDownList>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
        &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
  Semestre:&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<asp:DropDownList ID="ddlSemestre" runat="server">
      <asp:ListItem Value="1">01</asp:ListItem>
      <asp:ListItem Value="2">02</asp:ListItem>
  </asp:DropDownList>
                </td>
                    <td class="auto-style5"></td>
            </tr>
            <tr>
                <td class="auto-style13"></td>
                <td class="auto-style18">
                    <asp:Label ID="lbData" runat="server" Text="Label">Data:</asp:Label></td>
                <td class="auto-style10">
                    <asp:DropDownList ID="ddlDia" runat="server"></asp:DropDownList>&nbsp&nbsp&nbsp
     <asp:DropDownList ID="ddlMes" runat="server"></asp:DropDownList>&nbsp&nbsp&nbsp   
     <asp:DropDownList ID="ddlAno" runat="server"></asp:DropDownList>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp     
     <asp:Label ID="lbHora" runat="server" Text="Label"> Hora:</asp:Label>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
     <asp:DropDownList ID="ddlHora" runat="server"></asp:DropDownList>&nbsp&nbsp&nbsp
     <asp:DropDownList ID="ddlMinuto" runat="server"></asp:DropDownList>&nbsp&nbsp&nbsp </td>
                <td class="auto-style5"></td>
            </tr>
            <tr>
                <td class="auto-style13"></td>
                <td class="auto-style18">
                    <asp:Label ID="lbTitulo" runat="server" Text="Label">Titulo:</asp:Label>
                </td>
                <td class="auto-style10">
                    <asp:TextBox ID="TxtTitulo" runat="server" Width="500px" TextMode="MultiLine" Rows="1"></asp:TextBox>
                </td>
                <td class="auto-style5"></td>
            </tr>
            <tr>
                <td class="auto-style13"></td>
                <td class="auto-style18">
                    <asp:Label ID="lbLocal" runat="server" Text="Label">Local:</asp:Label></td>
                <td class="auto-style10">
                    <asp:TextBox ID="TxtLocal" runat="server" Width="500px" TextMode="MultiLine" Rows="1"></asp:TextBox>&nbsp&nbsp&nbsp&nbsp</td>
                <td class="auto-style5"></td>
            </tr>
            <tr>

                <td class="auto-style13"></td>
                <td class="auto-style18">
                    <asp:Label ID="Label1" runat="server" Text="Label">Pauta:</asp:Label></td>
                <td class="auto-style12">
                    <asp:TextBox ID="TxtPauta" runat="server" Width="500px" TextMode="MultiLine" Rows="3" Style="margin-left: 0px"></asp:TextBox>
                    <asp:ImageButton ID="btnIncluirauta" runat="server" ImageUrl="~/Imagens/adicionar.jpg" OnClick="ImageButton1_Click" Width="23px" />
                </td>
                <td class="auto-style11">&nbsp;</td>
            </tr>


        </table>

        &nbsp;<asp:GridView ID="grdPauta" runat="server" AutoGenerateColumns="False" Width="70%" Style="margin-left: 15%; margin-right: 15%;">
            <Columns>
                <asp:BoundField HeaderText="Nº" DataField="ITEM" />
                <asp:BoundField HeaderText="Descricão" DataField="DESCRICAO" />
            </Columns>
        </asp:GridView>

        <table style="width: 100%;">
            <tr>
                <td class="auto-style19"></td>
                <td class="auto-style9">&nbsp;<asp:Button ID="btnSalvarReuniao" runat="server" Text="Salvar" Width="65px" OnClick="btnSalvarReuniao_Click" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
                     <asp:Button ID="btnVoltar" runat="server" Text="Voltar" Width="65px" OnClick="btnVoltar_Click" /></td>
                <td></td>
                <td class="auto-style16"></td>
            </tr>
        </table>

    </div>


</asp:Content>
