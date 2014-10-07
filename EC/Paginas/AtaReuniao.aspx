<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.Master" AutoEventWireup="true" CodeBehind="AtaReuniao.aspx.cs" Inherits="EC.Paginas.AtaReunião" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        
        .auto-style26 {
            width: 117px;
        }
        .auto-style27 {
            width: 280px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <div style="width: 1280px; height: 100%; margin-left: 1%; margin-right: 1%;">

        <h2 style="width: 100%; text-align: center; position: relative; top: 0px; left: 0px; height: 34px;">Dados da Ata</h2>


        <table style="width: 90%;">
            <tr>
                <td class="auto-style27"></td>
                <td class="auto-style26">Reuniao: </td>
                <td>
                    <asp:DropDownList ID="ddlReuniao" runat="server"></asp:DropDownList>
                    <asp:Button ID="btntransferirPendencias" runat="server" Text="Transferir Pendências" Width="143px" /></td>
                <td></td>
            </tr>
        </table>
        <asp:GridView ID="grdPendencias" runat="server" AutoGenerateColumns="False" Width="68%" Style="margin-left: 15%; margin-right: 15%;">
            <Columns>
                <asp:BoundField HeaderText="Nº" DataField="ITEM" />
                <asp:BoundField HeaderText="Descricão" DataField="DESCRICAO" />
                <asp:BoundField DataField="TIPO_ASSUNTO.DESCRICAO" HeaderText="Tipo" />
            </Columns>
        </asp:GridView>

        <table style="width: 90%;">
            <tr>
                <td class="auto-style27"></td>
                <td class="auto-style26">
                    <asp:Label ID="Label1" runat="server" Text="Label">Assuntos Tratados:</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="TxtAssunto" runat="server" Width="500px" TextMode="MultiLine" Rows="2" Style="margin-left: 0px"></asp:TextBox>
                    <tr>
                        <td class="auto-style27"></td>
                        <td class="auto-style26">Tipo:</td>
                        <td>
                            <asp:DropDownList ID="ddlTipoAssunto" runat="server"></asp:DropDownList>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

                            <asp:ImageButton ID="btnIncluirAssunto" runat="server" ImageUrl="~/Imagens/adicionar.jpg" Width="23px" OnClick="btnIncluirAssunto_Click" /></td>
                    </tr>
        </table>

        <asp:GridView ID="grdAssunto" runat="server" AutoGenerateColumns="False" Width="68%" Style="margin-left: 15%; margin-right: 15%;">
            <Columns>
                <asp:BoundField HeaderText="Nº" DataField="ITEM" />
                <asp:BoundField HeaderText="Descricão" DataField="DESCRICAO" />
                <asp:TemplateField HeaderText="Tipo">
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("ID_TIPOASSTRATADO") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:ButtonField Text="Alterar" />
                <asp:ButtonField Text="Excluir" />
            </Columns>
        </asp:GridView>

        <table style="width: 90%;">
            <tr>
                <td class="auto-style27"></td>
                <td class="auto-style26">
                    <asp:Label ID="Label2" runat="server" Text="Label">Compromissos:</asp:Label></td>
                <td>
                    <asp:TextBox ID="TxtCompromisso" runat="server" Width="500px" TextMode="MultiLine" Rows="2" Style="margin-left: 0px"></asp:TextBox>
                    <td></td>
            </tr>

            <tr>
                <td class="auto-style27"></td>
                <td class="auto-style26">
                    <asp:Label ID="Label3" runat="server" Text="Label">Responsável:</asp:Label></td>
                <td>
                    <asp:DropDownList ID="ddlPessoa" runat="server"></asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
          
            Data&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:DropDownList ID="ddlDia" runat="server">
                    </asp:DropDownList>&nbsp&nbsp&nbsp
            <asp:DropDownList ID="ddlMes" runat="server">
            </asp:DropDownList>&nbsp&nbsp&nbsp
           <asp:DropDownList ID="ddlAno" runat="server"></asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:ImageButton ID="btnIncluircompromisso" runat="server" ImageUrl="~/Imagens/adicionar.jpg" Width="23px" OnClick="btnIncluircompromisso_Click1" />
            </tr>
        </table>

        <asp:GridView ID="grdCompromisso" runat="server" AutoGenerateColumns="False" Width="68%" Style="margin-left: 15%; margin-right: 15%;">
            <Columns>
                <asp:BoundField HeaderText="Nº" DataField="ITEM" />
                <asp:BoundField HeaderText="Descricão" DataField="DESCRICAO" />

                <asp:BoundField HeaderText="Data " />
                <asp:BoundField DataField="ID_PESSOA" HeaderText="Responsável" />

            </Columns>
        </asp:GridView>
        <table style="width: 90%;">
            <tr>
                <td class="auto-style27"></td>
                <td class="auto-style9">&nbsp;<asp:Button ID="btnSalvarReuniao" runat="server" Text="Salvar" Width="65px" OnClick="btnSalvarReuniao_Click" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                    <asp:Button ID="btnVoltar" runat="server" Text="Voltar" Width="65px" OnClick="btnVoltar_Click" /></td>
                <td></td>
                <td class="auto-style16"></td>
            </tr>
        </table>
    </div>


</asp:Content>
