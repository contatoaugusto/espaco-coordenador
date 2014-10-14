<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.Master" AutoEventWireup="true" CodeBehind="~/Paginas/Evento.aspx.cs" Inherits="UI.Web.EC.Paginas.Evento" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <div style="width: 1280px; height: 768px;">

        <h2 style="width: 100%; text-align: center; position: relative; top: 0px; left: 0px; height: 9px;">Dados do Evento</h2>

        <table style="width: 90%;">
            <tr>
                <td class="auto-style2"></td>
                <td class="auto-style3">
                    <asp:Label ID="lbTipoEvento" runat="server" Text="Label">Tipo de Evento:</asp:Label>
                    &nbsp
                </td>
                <td class="auto-style4">
                    <asp:DropDownList ID="ddlTipoEvento" runat="server"></asp:DropDownList>
                </td>
                <td class="auto-style1"></td>   </tr>

            <tr>
                <td class="auto-style2"></td>
                <td class="auto-style3">
                    <asp:Label ID="lbNome" runat="server" Text="Label">Nome:</asp:Label>
                    &nbsp</td>
                <td class="auto-style4">
                    <asp:TextBox ID="TxtNome" runat="server" Width="500px" TextMode="MultiLine" Rows="1"></asp:TextBox> <td></td>
                </td>

                <tr>
                    <td class="auto-style2"></td>
                    <td class="auto-style3">
                        <asp:Label ID="lbDescricao" runat="server" Text="Label">Descrição:</asp:Label>
                        &nbsp
                    </td>
                    <td class="auto-style4">
                        <asp:TextBox ID="TxtDescricao" runat="server" Width="500px" TextMode="MultiLine" Rows="3"></asp:TextBox><td></td>
                    </td>
                    <br />
                    <tr>
                        <td class="auto-style2"></td>
                        <td class="auto-style3">
                            <asp:Label ID="lbLocal" runat="server" Text="Label">Local:</asp:Label>
                            &nbsp
                        </td>
                        <td class="auto-style4">
                            <asp:TextBox ID="TxtLocal" runat="server" Width="500px" TextMode="MultiLine" Rows="1"></asp:TextBox><td></td>
                        </td>
                        <br />

                        <tr>
                            <td class="auto-style2"></td>
                            <td class="auto-style3">Responsável: </td>
                            <td class="auto-style4">
                                <asp:DropDownList ID="ddlPessoa" runat="server"></asp:DropDownList><td></td>
                            </td>

                        </tr>
                        <br />
                        <tr>
                            <td class="auto-style2"></td>
                            <td class="auto-style3">
                                <asp:Label ID="Label4" runat="server" Text="Label">Inicio:</asp:Label>&nbsp&nbsp&nbsp
                            </td>
                            <td class="auto-style4">
                                <asp:DropDownList ID="ddlDia" runat="server">
                                </asp:DropDownList>&nbsp&nbsp&nbsp
            <asp:DropDownList ID="ddlMes" runat="server">
            </asp:DropDownList>&nbsp&nbsp&nbsp
           <asp:DropDownList ID="ddlAno" runat="server"></asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp; &nbsp;
           <asp:Label ID="Label1" runat="server" Text="Label">Conclusão:</asp:Label>&nbsp&nbsp&nbsp
           <asp:DropDownList ID="ddlDia1" runat="server">
           </asp:DropDownList>&nbsp&nbsp&nbsp
           <asp:DropDownList ID="ddlMes1" runat="server">
           </asp:DropDownList>&nbsp&nbsp&nbsp
           <asp:DropDownList ID="ddlAno1" runat="server"></asp:DropDownList><td></td>
                            </td>

                        </tr>
                        <tr>
                            <td class="auto-style2"></td>
                            <td class="auto-style3">
                                <asp:Button ID="Button1" runat="server" Text="Salvar" OnClick="Button1_Click" Width="60px" />
                            </td>
                        </tr>
        </table>

        <div>
</asp:Content>











<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="head">
    <style type="text/css">
        .auto-style1 {
            width: 478px;
        }
        .auto-style2 {
            width: 597px;
        }
        .auto-style3 {
            width: 170px;
        }
        .auto-style4 {
            width: 566px;
        }
    </style>
</asp:Content>












