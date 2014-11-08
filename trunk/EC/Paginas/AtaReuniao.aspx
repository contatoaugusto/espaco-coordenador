<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.Master" AutoEventWireup="true" CodeBehind="~/Paginas/AtaReuniao.aspx.cs" Inherits="UI.Web.EC.Paginas.AtaReunião" %>
<%@ Import Namespace="UI.Web.EC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <sgi:MessageBox ID="messageBox" runat="server" />

    <div class="form-separator"></div>
    <h3>Incluir Ata de Reunião</h3>


    <div class="form-in">
        
        
        <div class="row">
            <div class="column w100">
                Reuniao: 
            </div>
            <div class="column w250">
                <asp:DropDownList ID="ddlReuniao" runat="server" OnSelectedIndexChanged="ddlReuniao_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
            </div>
        </div>
        <div class="form-separator"></div>
    
        <asp:Panel ID="pnlAta" runat="server" Visible="false">

            <h4>Identificação da Reunião</h4>

            <div class="row">
                <div class="column w100">
                    Pauta nº:
                </div>
                <div class="column w250">
                    <asp:Label ID="lblNumeroReunião" runat="server"/>
                </div>
            </div>
            <div class="row">
                <div class="column w80">
                    Data:
                </div>
                <div class="column w100">
                    <asp:Label ID="lblDataTeuniao" runat="server"/>
                </div>
                <div class="column w80">
                    Hora:
                </div>
                <div class="column w100">
                    <asp:Label ID="lblHoraReuniao" runat="server"/>
                </div>
                <div class="column w80">
                    Local:
                </div>
                <div class="column w150">
                    <asp:Label ID="lblLocalReuniao" runat="server"/>
                </div>
            </div>

            <sgi:GridView 
                ID="grdParticipantesReuniao" 
                runat="server" 
                AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField HeaderText="Nome" DataField="NOME" />
                    <asp:BoundField HeaderText="E-mail" DataField="EMAIL" />
                    <asp:BoundField HeaderText="Telefone" DataField="TELEFONE"/>
                    <asp:TemplateField HeaderText="Cargo/Função">
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Utils.GetCargos(Eval("ID_PESSOA").ToInt32()) %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="Presente" DataField="ID_PESSOA"/>
                </Columns>
            </sgi:GridView>

            <div class="row">
                <div class="column">
                    <asp:Label ID="lblPauta" runat="server"/>
                </div>
            </div>

            <div class="row">
                <div class="column w100">
                    Pendências:
                </div>
                <div class="column w250">
                    <sgi:Button ID="btntransferirPendencias" runat="server" Text="Transferir Pendências"/>
                </div>
            </div>
            <sgi:GridView ID="grdPendencias" runat="server" AutoGenerateColumns="False">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:BoundField HeaderText="Nº" DataField="ITEM" />
                    <asp:BoundField HeaderText="Descricão" DataField="DESCRICAO" />
                    <asp:BoundField DataField="TIPO_ASSUNTO.DESCRICAO" HeaderText="Tipo" />
                </Columns>
            </sgi:GridView>

            <div class="form-separator"></div>

            <div class="row">
                <div class="column w100">
                    Assuntos Tratados:
                </div>
                <div class="column w250">
                    <asp:TextBox ID="TxtAssunto" runat="server" Width="500px" TextMode="MultiLine" Rows="2" Style="margin-left: 0px"></asp:TextBox>
                </div>
            </div>

            <div class="row">
                <div class="column w100">
                    Tipo:
                </div>
                <div class="column w250">
                    <asp:DropDownList ID="ddlTipoAssunto" runat="server"></asp:DropDownList>
                    <asp:ImageButton ID="btnIncluirAssunto" runat="server" ImageUrl="~/Imagens/adicionar.jpg" Width="23px" OnClick="btnIncluirAssunto_Click" /></td>
                </div>
            </div>

            <sgi:GridView ID="grdAssunto" runat="server" AutoGenerateColumns="False">
                <AlternatingRowStyle BackColor="White" />
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
            </sgi:GridView>

            <div class="form-separator"></div>

            <div class="row">
                <div class="column w100">
                    Compromissos:
                </div>
                <div class="column w250">

                    <asp:TextBox ID="TxtCompromisso" runat="server" Width="500px" TextMode="MultiLine" Rows="2" Style="margin-left: 0px"></asp:TextBox>
                </div>
            </div>

            <div class="row">
                <div class="column w100">
                    Responsável:
                </div>
                <div class="column w250">

                    <asp:DropDownList ID="ddlPessoa" runat="server"></asp:DropDownList>
                </div>
            </div>

            <div class="row">
                <div class="column w100">
                    Data
                </div>
                <div class="column w250">
                    <asp:DropDownList ID="ddlDia" runat="server">
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddlMes" runat="server">
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddlAno" runat="server"></asp:DropDownList>

                    <asp:ImageButton ID="btnIncluircompromisso" runat="server" ImageUrl="~/Imagens/adicionar.jpg" Width="23px" OnClick="btnIncluircompromisso_Click1" />
                </div>
            </div>

            <sgi:GridView ID="grdCompromisso" runat="server" AutoGenerateColumns="False">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:BoundField HeaderText="Nº" DataField="ITEM" />
                    <asp:BoundField HeaderText="Descricão" DataField="DESCRICAO" />

                    <asp:BoundField HeaderText="Data " />
                    <asp:BoundField DataField="ID_PESSOA" HeaderText="Responsável" />

                </Columns>
            </sgi:GridView>

            <div class="form-separator"></div>

            <div class="form-bottombuttons">
                <sgi:Button ID="btnSalvarReuniao" runat="server" Text="Salvar" OnClick="btnSalvarReuniao_Click" />
                <sgi:Button ID="btnVoltar" runat="server" Text="Voltar" OnClick="btnVoltar_Click" />
            </div>
        </asp:Panel>
    </div>
</asp:Content>
