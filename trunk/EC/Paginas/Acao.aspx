<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.Master" AutoEventWireup="true" CodeBehind="~/Paginas/Acao.aspx.cs" Inherits="UI.Web.EC.Paginas.Acao" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style2 {
            width: 171px;
        }

        .auto-style4 {
            width: 61px;
        }

        .auto-style5 {
            width: 210px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <body>

        <h2 style="width: 100%; text-align: center; position: relative;">Dados da Ação</h2>

        <table style="width: 100%;">
            <tr>
                <td class="auto-style5"></td>
                <td class="auto-style4">Amc:</td>
                <td>
                    <asp:DropDownList ID="ddlAmc" runat="server"></asp:DropDownList></td>
                <td class="auto-style2">
            </tr>
            <tr>
                <td class="auto-style5"></td>
                <td class="auto-style4">Evento: </td>
                <td>
                    <asp:DropDownList ID="ddlEvento" runat="server"></asp:DropDownList></td>
                <td class="auto-style2">
            </tr>
            <tr>
                <td class="auto-style5"></td>
                <td class="auto-style4">Reunião:</td>
                <td>
                    <asp:DropDownList ID="ddlReuniao" runat="server"></asp:DropDownList></td>
                <td class="auto-style2">
            </tr>
            <tr>
                <td class="auto-style5"></td>
                <td class="auto-style4">Descrição</td>
                <td>
                    <asp:TextBox Wrap="True" ID="TxtTitulo" runat="server" Width="500px" TextMode="MultiLine" Rows="4"></asp:TextBox>
                    <td class="auto-style2">
            </tr>
            <tr>
                <td class="auto-style5"></td>
                <td class="auto-style4">
                    <asp:Label ID="Label4" runat="server" Text="Label">Inicio:</asp:Label>&nbsp&nbsp&nbsp</td>
                <td>
                    <asp:DropDownList ID="ddlDia" runat="server"></asp:DropDownList>
                    &nbsp&nbsp&nbsp
         <asp:DropDownList ID="ddlMes" runat="server"></asp:DropDownList>
                    &nbsp&nbsp&nbsp  
         <asp:DropDownList ID="ddlAno" runat="server"></asp:DropDownList>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;   
         <asp:Label ID="Label1" runat="server" Text="Label">Conclusão:</asp:Label>
                    &nbsp&nbsp&nbsp
         <asp:DropDownList ID="ddlDia1" runat="server"></asp:DropDownList>
                    &nbsp&nbsp&nbsp
         <asp:DropDownList ID="ddlMes1" runat="server"></asp:DropDownList>
                    &nbsp&nbsp&nbsp
         <asp:DropDownList ID="ddlAno1" runat="server"></asp:DropDownList>
                    &nbsp&nbsp&nbsp </td>
                <td class="auto-style2">
            </tr>
            <tr>
                <td class="auto-style5"></td>
                <td class="auto-style4">Responsável: </td>
                <td>
                    <asp:DropDownList ID="ddlPessoa" runat="server"></asp:DropDownList>
                </td>
                <td class="auto-style2">
            </tr>
            <tr>
                <td class="auto-style5"></td>
                <td class="auto-style4">
                    <asp:Label ID="lbPrioridade" runat="server" Text="Label">Prioridade:</asp:Label>
                    &nbsp</td>
                <td>
                    <asp:DropDownList ID="ddlPrioridade" runat="server"></asp:DropDownList>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
          &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Status&nbsp;&nbsp;
         <asp:DropDownList ID="ddlStatus" runat="server"></asp:DropDownList></td>
                <td class="auto-style2">
            </tr>
            <tr>
                <td class="auto-style5"></td>
                <td class="auto-style4">
                    <asp:Button ID="btSalvar" runat="server" Text="Salvar" OnClick="Button1_Click" Width="65px" />
                    <td class="auto-style2">
                    <asp:Button ID="btVoltar" runat="server" Text="Voltar"  Width="65px" OnClick="btVoltar_Click" />
                    </td>
            </tr>
            &nbsp;&nbsp;&nbsp;&nbsp;
        
        </table>

    </body>

</asp:Content>
