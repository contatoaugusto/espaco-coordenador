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
            <div class="column w100">
                AMC:
            </div>
            <div class="column w500">
                <sgi:DropDownList ID="ddlAmc" runat="server" OnSelectedIndexChanged="ddlAmc_SelectedIndexChanged" AutoPostBack="true"></sgi:DropDownList>
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
        <asp:Button ID="btnSalvar" runat="server" Text="Gerar Prova" OnClick="btnGerarProva_Click"/>
        <%--<asp:Button ID="btnVoltar" runat="server" Text="Voltar" OnClick="btnVoltar_Click"  />--%>
    </div>
</asp:Content>
