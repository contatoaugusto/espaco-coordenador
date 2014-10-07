<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.Master" AutoEventWireup="true" CodeBehind="Questao.aspx.cs" Inherits="EC.Paginas.Questao" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            height: 167px;
        }

        .auto-style2 {
            width: 202px;
            height: 167px;
        }

        .auto-style3 {
            width: 106px;
        }

        .auto-style4 {
            height: 167px;
            width: 106px;
        }
        .auto-style5 {
            width: 202px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div style="width: 1280px; height: 768px;">
        <h2 style="width: 100%; text-align: center; position: relative;">Incluir Questões</h2>


        <table style="width: 90%;">
            <a href="Questao.aspx">Questao.aspx</a>
            <tr>
                <td class="auto-style3"></td>
                <td class="auto-style5">AMC:</td>
                <td>
                    <asp:DropDownList ID="ddlAmc" runat="server"></asp:DropDownList></td>
                <td></td>
                <tr>
                    <td class="auto-style3"></td>
                    <td class="auto-style5">Disciplina:</td>
                    <td>
                        <asp:DropDownList ID="ddlDisciplina" runat="server"></asp:DropDownList></td>
                    <td></td>
                </tr>
                <tr>
                    <td class="auto-style3"></td>
                    <td class="auto-style5">Professor: </td>
                    <td>
                        <asp:DropDownList ID="ddlFuncionario" runat="server"></asp:DropDownList></td>
                    <td></td>
                </tr>

                <tr>
                    <td class="auto-style4"></td>
                    <td class="auto-style2">Pergunta:</td>
                    <td class="auto-style1">
                        <asp:TextBox ID="TxtDescricao" runat="server" Width="600px" TextMode="MultiLine" Rows="10" Style="margin-top: 0px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td></td>
                    <td class="auto-style5"></td>
                    <td>
                        <asp:FileUpload ID="upLoad" runat="server" /></td>
                    <td></td>
                </tr>
                <tr>
                    <td class="auto-style3"></td>
                    <td class="auto-style5">Resposta Correta:<asp:CheckBox ID="Correta1" runat="server" Checked="False" /></td>
                    <td>
                        <asp:TextBox ID="TxtEscolha1" runat="server" Width="600px" TextMode="MultiLine" Rows="2"></asp:TextBox>&nbsp</td>
                    <td></td>
                </tr>
                <tr>
                    <td class="auto-style3"></td>
                    <td class="auto-style5">Resposta Correta:<asp:CheckBox ID="Correta2" runat="server" Checked="False" /></td>
                    <td>
                     <asp:TextBox ID="TxtEscolha2" runat="server" Width="600px" TextMode="MultiLine" Rows="2"></asp:TextBox>&nbsp </td>
                </tr>
                <tr>
                    <td class="auto-style3"></td>
                    <td class="auto-style5">Resposta Correta:<asp:CheckBox ID="Correta3" runat="server" Checked="False" /></td>
                    <td>
                        <asp:TextBox ID="TxtEscolha3" runat="server" Width="600px" TextMode="MultiLine" Rows="2"></asp:TextBox>&nbsp </td>
                    <tr>
                        <td></td>
                        <td class="auto-style5">Resposta Correta:<asp:CheckBox ID="Correta4" runat="server" Checked="False" /></td>
          <td>  
                            <asp:TextBox ID="TxtEscolha4" runat="server" Width="600px" TextMode="MultiLine" Rows="2"></asp:TextBox></td>
                        <td>
                            &nbsp </td>
                    </tr>
                    <tr>
                        <td class="auto-style3"></td>
                        <td class="auto-style5">Resposta Correta:<asp:CheckBox ID="Correta5" runat="server" Checked="False" /> 
          </td>
            
                           
                          <td>  <asp:TextBox ID="TxtEscolha5" runat="server" Width="600px" TextMode="MultiLine" Rows="2"></asp:TextBox>&nbsp </td>
</td>
                    </tr>
                    <td></td>
                    <td class="auto-style5">
                        <asp:Button ID="Button1" runat="server" Text="Salvar" OnClick="Button1_Click1" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnVoltar" runat="server" Text="Voltar" OnClick="btnVoltar_Click"  /></td>
                    <td></td>
                </tr>
        </table>
    </div>
</asp:Content>
