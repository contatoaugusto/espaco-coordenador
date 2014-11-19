<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.Master" AutoEventWireup="true" CodeBehind="~/Paginas/AtaReuniao.aspx.cs" Inherits="UI.Web.EC.Paginas.AtaReunião" MaintainScrollPositionOnPostback="true"%>
<%@ Import Namespace="UI.Web.EC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <sgi:MessageBox ID="messageBox" runat="server" />

    <div class="form-separator"></div>
    <h3>Cadastrar Ata de Reunião</h3>


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
            <div class="form-subtitle">
                Identificação da Reunião
            </div>

            <div class="row">
                <div class="column w80">
                    Pauta nº:
                </div>
                <div class="column w250">
                    <asp:Label ID="lblNumeroReunião" runat="server"/>
                </div>
            </div>
            <div class="row">
                <div class="column w90">
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
            <div class="form-separator"></div>
            <sgi:GridView 
                ID="grdParticipantesReuniao" 
                runat="server" 
                AutoGenerateColumns="False"
                EmptyDataText="Nenhum registro encontrado">
                <Columns>
                    <asp:TemplateField HeaderText="Nome">
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("PESSOA.NOME") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="E-mail">
                        <ItemTemplate>
                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("PESSOA.EMAIL") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Telefone">
                        <ItemTemplate>
                            <asp:Label ID="Label3" runat="server" Text='<%# Bind("PESSOA.TELEFONE") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Cargo/Função">
                        <ItemTemplate>
                            <asp:Label ID="Label4" runat="server" Text='<%# Utils.GetCargos(Eval("ID_PESSOA").ToInt32()) %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="Presente" DataField="PRESENCA"/>
                </Columns>
            </sgi:GridView>
            <br/>
            
            <div class="form-subtitle">
                Pauta de Reunião
            </div>

            <div class="row">
                <div class="column">
                    <asp:Label ID="lblPauta" runat="server"/>
                </div>
            </div>
            
            <div class="form-subtitle">
                Pendências Anteriores
            </div>
            <div class="row">
                <div class="column w150">
                    <sgi:Button ID="btntransferirPendencias" runat="server" Text="Transferir Pendências"/>
                </div>
            </div>
            <sgi:GridView ID="grdPendencias" runat="server" AutoGenerateColumns="False" EmptyDataText="Nenhum registro encontrado">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:BoundField HeaderText="Nº" DataField="ITEM" />
                    <asp:BoundField HeaderText="Descricão" DataField="DESCRICAO" />
                    <asp:BoundField DataField="TIPO_ASSUNTO.DESCRICAO" HeaderText="Tipo" />
                </Columns>
            </sgi:GridView>

            <br />
            <div class="form-subtitle">
                Assuntos Tratados
            </div>
            <div class="row">
                <div class="column w450">
                    Descrição:
                </div>
                <div class="column w150">
                    Tipo
                </div>
            </div>
            <div class="row">
                <div class="column w450">
                    <asp:TextBox ID="TxtAssunto" runat="server" Width="440px" TextMode="MultiLine" Rows="2"></asp:TextBox>
                </div>
                <div class="column w150">
                    <asp:DropDownList ID="ddlTipoAssunto" runat="server"></asp:DropDownList>
                     <asp:RequiredFieldValidator ID="rfv0" runat="server" ControlToValidate="ddlTipoAssunto" Font-Bold="true" ErrorMessage="<br />Campo obrigatório"></asp:RequiredFieldValidator>
                    <asp:ImageButton ID="btnIncluirAssunto" runat="server" ImageUrl="~/Imagens/adicionar.jpg" Width="23px" OnClick="btnIncluirAssunto_Click" ToolTip="Adiciona novo assunto tratado" CausesValidation="true"/>
                </div>
            </div>
            <sgi:GridView ID="grdAssunto" runat="server" AutoGenerateColumns="False" EmptyDataText="Nenhum registro encontrado" OnRowCommand="grdAssunto_RowCommand">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:BoundField HeaderText="Nº" DataField="ITEM" />
                    <asp:BoundField HeaderText="Descricão" DataField="DESCRICAO" />
                    <asp:TemplateField HeaderText="Tipo">
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("TIPO_ASSUNTO_TRATADO.DESCRICAO") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Ações">
                        <ItemTemplate>
                            <asp:LinkButton ID="HyperLink1" runat="server" CommandArgument='<%# Eval("ITEM")%>' CommandName="Excluir" Text="Excluir" CausesValidation="false" />
                            <%--|
                            <asp:LinkButton ID="HyperLink2" runat="server" CommandArgument='<%# Eval("ITEM")%>' CommandName="Excluir" Text="Excluir" CausesValidation="false" />--%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                </Columns>
            </sgi:GridView>

            <br />
            <div class="form-subtitle">
                Compromissos
            </div>
            
            <div class="row">
                <div class="column w450">
                    Descrição:
                </div>
                <div class="column w200">
                    Responsável
                </div>
                 <div class="column w200">
                    Data
                </div>
            </div>
            <div class="row">
                <div class="column w450">
                    <asp:TextBox ID="TxtCompromisso" runat="server" Width="440px" TextMode="MultiLine" Rows="2" ></asp:TextBox>
                </div>
                <div class="column w200">
                    <asp:DropDownList ID="ddlPessoa" runat="server"></asp:DropDownList>
                </div>
                 <div class="column w200">
                    <asp:DropDownList ID="ddlDia" runat="server"></asp:DropDownList>
                    <asp:DropDownList ID="ddlMes" runat="server"></asp:DropDownList>
                    <asp:DropDownList ID="ddlAno" runat="server"></asp:DropDownList>

                    <asp:ImageButton ID="btnIncluircompromisso" runat="server" ImageUrl="~/Imagens/adicionar.jpg" Width="23px" OnClick="btnIncluircompromisso_Click1" ToolTip="Adiciona nova ação futura" />
                </div>
            </div>
            <sgi:GridView ID="grdCompromisso" runat="server" AutoGenerateColumns="False" OnRowCommand="grdCompromisso_RowCommand" EmptyDataText="Nenhum registro encontrado">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:BoundField HeaderText="Nº" DataField="ITEM" />
                    <asp:BoundField HeaderText="Descricão" DataField="DESCRICAO" />
                    <asp:BoundField HeaderText="Data" DataField="DATA"/>
                    <asp:TemplateField HeaderText="Responsável">
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("PESSOA.NOME") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Ações">
                        <ItemTemplate>
                            <asp:LinkButton ID="HyperLink1" runat="server" CommandArgument='<%# Eval("ITEM")%>' CommandName="Excluir" Text="Excluir" CausesValidation="false" />
                            <%--|
                            <asp:LinkButton ID="HyperLink2" runat="server" CommandArgument='<%# Eval("ITEM")%>' CommandName="Excluir" Text="Excluir" CausesValidation="false" />--%>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </sgi:GridView>

           <br />
            <div class="form-subtitle">
                Identificação do Registro
            </div>
            <div class="row">
                <div class="column w150">
                    Ata elaborada por:
                </div>
                <div class="column w200">
                    <asp:Label ID="lblResponsavelAta" runat="server"></asp:Label>
                    <asp:HiddenField ID="hddResponsavelAta" runat="server" />
                </div>
                <div class="column w150">
                    Data do fechamento
                </div>
                <div class="column w200">
                    <asp:DropDownList ID="ddlDiaFechamento" runat="server"></asp:DropDownList>
                    <asp:DropDownList ID="ddlMesFechamento" runat="server"></asp:DropDownList>
                    <asp:DropDownList ID="ddlAnoFechamento" runat="server"></asp:DropDownList>
                </div>
            </div>

            <div class="form-separator"></div>
            
            <div class="form-bottombuttons">
                <sgi:Button ID="btnSalvarReuniao" runat="server" Text="Salvar" OnClick="btnSalvarReuniao_Click" />
                <sgi:Button ID="btnEnviaEmail" runat="server" Text="Enviar E-mail" OnClick="btnSalvarReuniao_Click" />
                <sgi:Button ID="btnImprimir" runat="server" Text="Imprimir" />
                <sgi:Button ID="btnVoltar" runat="server" Text="Voltar" OnClick="btnVoltar_Click" />
            </div>
        </asp:Panel>
    </div>
</asp:Content>
