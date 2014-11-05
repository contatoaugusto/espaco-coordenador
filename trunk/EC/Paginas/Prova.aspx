<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.Master" AutoEventWireup="true" CodeBehind="Prova.aspx.cs" Inherits="UI.Web.EC.Paginas.Prova" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <sgi:MessageBox ID="messageBox" runat="server" />
    <div class="form-separator"></div>

    <h3>Incluir Questão na prova da AMC</h3>
    <div class="form-in">
        <div class="row" id="divInstrucoesGeraProva" runat="server">
            A partir do banco de questões previamente criado, ao cliclar no botão Gerar Prova, 
            o sistema irá selecionar, de forma aleatória, 32 dessas questões pertencentes a AMC selecionada e 
            motar uma prova com seu gabarito correspondente.
        </div>


        <div class="row">
            <div class="column w150">
                AMC:
            </div>
            <div class="column w250">
                <sgi:DropDownList ID="ddlAmc" runat="server" equiredField="true"></sgi:DropDownList>
            </div>
        </div>
        
        </div>
        <%--<div class="row">--%>
        <%--<div class="column w350">
                <asp:ListBox 
                    ID="listBoxQuestoes" 
                    runat="server" 
                    DataTextField="DESCRICAO" 
                    DataValueField="ID_QUESTAO" 
                    DataSourceID="SqlDataSource_Questoes"
                    Width="100%"
                    SelectionMode="Multiple" Rows="10">
                
                </asp:ListBox>
            </div>
            <div class="column w10p">
                <center>
                    <uwc:Button ID="Button2" Text=">>" runat="server" /><br /><br />
                    <uwc:Button ID="Button1" Text="<<" runat="server" />
                
                </center>
            </div>--%>
        <%--<asp:ListBox 
                    ID="ListBox1" 
                    runat="server" 
                    Rows="10"
                    Width="100%"
                    ></asp:ListBox>--%>
        <%--<asp:CheckBoxList 
                    DataTextField="DESCRICAO" 
                    DataValueField="ID_QUESTAO" 
                    ID="CheckBoxList1" 
                    runat="server" 
                    DataSourceID="SqlDataSource_Questoes"
                    Width="100%">
               </asp:CheckBoxList>
        
            <asp:SqlDataSource 
                    ID="SqlDataSource_Questoes" 
                    runat="server" 
                    ConnectionString="<%$ ConnectionStrings:ECConnectionString %>" 
                    SelectCommand="SELECT * FROM [QUESTAO] WHERE ([ID_AMC] = @ID_AMC)">
                    <SelectParameters>
                        <asp:FormParameter FormField="idAmc" Name="ID_AMC" Type="Int32" />
                    </SelectParameters>
                </asp:SqlDataSource>--%>
        <%--</div>--%>
    </div>
    <div class="form-bottombuttons">
        <asp:Button ID="btnSalvar" runat="server" Text="Gerar Prova" OnClick="btnGerarProva_Click" />
        <%--<asp:Button ID="btnVoltar" runat="server" Text="Voltar" OnClick="btnVoltar_Click"  />--%>
    </div>
    <div class="form-separator"></div>

    <h3>AMCs Cadastradas</h3>
    <sgi:GridView ID="GridView1"
        runat="server"
        AutoGenerateColumns="False"
        DataKeyNames="ID_PROVA"
        DataSourceID="SqlDataSource"
        BackColor="#BFBFBF"
        BorderColor="#BFBFBF"
        BorderStyle="Solid"
        CssClass="gridview"
        GroupFooter="False"
        PageSize="50"
        ShowWhenEmpty="False"
        OnRowCommand="GridView1_RowCommand">
        <AlternatingRowStyle CssClass="gridviewrowalternating"></AlternatingRowStyle>
        <Columns>
            <%--<asp:BoundField DataField="ID_PROVA" HeaderText="ID_PROVA" InsertVisible="False" ReadOnly="True" SortExpression="ID_PROVA" />--%>
            <asp:BoundField DataField="DATA_CRIACAO" HeaderText="Data Criação" SortExpression="DATA_CRIACAO" />
            <%--<asp:BoundField DataField="ID_FUNCIONARIO_CADATRO" HeaderText="ID_FUNCIONARIO_CADATRO" SortExpression="ID_FUNCIONARIO_CADATRO" />--%>
            <%--<asp:BoundField DataField="DATA_RESULTADO" HeaderText="DATA_RESULTADO" SortExpression="DATA_RESULTADO" />--%>
            <asp:BoundField DataField="OBSERVACAO" HeaderText="Observação" SortExpression="OBSERVACAO" />
            <asp:BoundField DataField="QTDE_QUESTOES" HeaderText="Qtde Questões" SortExpression="QTDE_QUESTOES" />
            <asp:TemplateField HeaderText="Ações">
                <ItemTemplate>
                    <asp:LinkButton ID="HyperLink1" runat="server" CommandArgument='<%# Eval("ID_PROVA")%>' CommandName="Imprimir" Text="Imprimir" CausesValidation="false" />
                    |
                    <asp:LinkButton ID="HyperLink2" runat="server" CommandArgument='<%# Eval("ID_PROVA")%>' CommandName="Gabarito" Text="Gabarito" CausesValidation="false" />
                </ItemTemplate>
            </asp:TemplateField>

        </Columns>

        <HeaderStyle CssClass="gridviewrowheader"></HeaderStyle>

        <PagerStyle CssClass="gridviewpaging"></PagerStyle>

        <RowStyle CssClass="gridviewrow"></RowStyle>
    </sgi:GridView>
    <asp:SqlDataSource ID="SqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:ECConnectionString %>" SelectCommand="SELECT * FROM [PROVA]"></asp:SqlDataSource>
</asp:Content>
