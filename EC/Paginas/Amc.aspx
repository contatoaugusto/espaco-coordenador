<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.Master" AutoEventWireup="true" CodeBehind="Amc.aspx.cs" Inherits="UI.Web.EC.Paginas.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   
    <sgi:MessageBox ID="messageBox" runat="server" />
<%--<sgi:UpdatePanel ID="pnlAmc" runat="server">--%>
    <div class="form-separator"></div>

    <h3>Criar AMC</h3>
  
    <asp:Panel ID="pnlAmc" runat="server">
      
        <div class="row">
            <div class="column w150">
                Semestre corrente:
            </div>
            <div class="column w250">
                <asp:Label ID="lblSemestreCorrente" runat="server"></asp:Label>
            </div>
        </div>  

        <div class="row">
            <div class="column w150">
                Data Aplicação da AMC:
            </div>
            <div class="column w250">
               <asp:TextBox ID="dtAplicacao" runat="server" ></asp:TextBox>
               <actk:CalendarExtender 
                   runat="server" 
                   ID="dtAplicacaoExtender" 
                   TargetControlID="dtAplicacao" 
                   Format="MMMM/dd/yyyy" 
                   CssClass="MyCalendar" 
                   PopupPosition="Left"></actk:CalendarExtender>
            </div>
            
        </div>
        <br />
        <div class="form-bottombuttons">
            <asp:Button ID="btnSalvar" runat="server" Text="Salvar" OnClick="Salvar_Click1" />
            <%--<asp:Button ID="btnVoltar" runat="server" Text="Voltar" OnClick="btnVoltar_Click"  />--%>
        </div>
    
       <%-- <asp:ObjectDataSource ID="ObjectDataSource1" runat="server"></asp:ObjectDataSource>
                <uwc:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="#BFBFBF" BorderColor="#BFBFBF" BorderStyle="Solid" CssClass="gridview" DataSourceID="EntityDataSource1" GroupFooter="False" PageSize="50" ShowWhenEmpty="False">
                </uwc:GridView>
                <asp:EntityDataSource ID="EntityDataSource1" runat="server">
                </asp:EntityDataSource>--%>
        <br />
        <br />
  </asp:Panel>
    <div class="form-separator"></div>
        <hr class="line" />
    <h3>AMCs Cadastradas</h3>
    
    <sgi:GridView ID="GridView_AMC" runat="server" AutoGenerateColumns="False" DataKeyNames="ID_AMC" Width="474px" BackColor="#BFBFBF" BorderColor="#BFBFBF" 
        BorderStyle="Solid" CssClass="gridview" GroupFooter="False" PageSize="50" ShowWhenEmpty="False" >
        <AlternatingRowStyle CssClass="gridviewrowalternating"></AlternatingRowStyle>
        <Columns>
            <%--<asp:BoundField DataField="ID_AMC" HeaderText="ID" ReadOnly="True" SortExpression="ID_AMC" />--%>   
            <%--<asp:BoundField DataField="SEMESTRE" HeaderText="Semestre" SortExpression="SEMESTRE" />--%>
            
            <asp:TemplateField HeaderText="Semestre/Ano">
                <ItemTemplate>
                    <asp:Label ID="lblSemestereAno" runat="server" Text='<%# Bind("SEMESTRE.SEMESTRE1") %>'></asp:Label>º sem/ <asp:Label ID="Label1" runat="server" Text='<%# Bind("SEMESTRE.ANO") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>

            <%--<asp:BoundField DataField="ANO" HeaderText="Ano" SortExpression="ANO" />--%>
            <asp:BoundField DataField="DATA_APLICACAO" HeaderText="Data Aplicação da prova" SortExpression="DATA_APLICACAO" />
            
            <asp:CommandField DeleteImageUrl="~/Imagens/close.png" ShowDeleteButton="True" DeleteText="Excluir" />
            
        </Columns>

        <HeaderStyle CssClass="gridviewrowheader"></HeaderStyle>

        <PagerStyle CssClass="gridviewpaging"></PagerStyle>

        <RowStyle CssClass="gridviewrow"></RowStyle>
        </sgi:GridView>

    


    <asp:EntityDataSource ID="EntityDataSource1" runat="server" ConnectionString="name=ECEntities" DefaultContainerName="ECEntities" EnableDelete="True" EnableFlattening="False" EnableUpdate="True" EntitySetName="AMC">
    </asp:EntityDataSource>

    
    <sgi:MessageBox ID="messageBox1" runat="server" />
    <sgi:AlertBox ID="alert" runat="server" AutoClose="true" Visible="false" Timeout="25" />
    <%--</sgi:UpdatePanel>--%>
</asp:Content>
