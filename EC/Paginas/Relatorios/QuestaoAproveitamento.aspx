<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.Master" AutoEventWireup="true" CodeBehind="~/Paginas/Relatorios/QuestaoAproveitamento.aspx.cs" Inherits="UI.Web.EC.Paginas.Relatorios.QuestaoAproveitamento" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <asp:Chart ID="chart1" runat="server" Width="1008px" Height="327px" style="margin-top: 31px" OnDataBound="chart1_DataBound">
        <%--<Titles>
            <asp:Title Text="Percentual de Questões Criadas por Professor na AMC" Font="Arial, 12pt, style=Bold" />
        </Titles>--%>
        <Series>
            <asp:Series Name="QuestaoAproveitamento" XValueMember="Questão" YValueMembers="Qtde. Acertos" ChartType="Column" Url="" ></asp:Series>
            <%--<asp:Series Name="QuestaoAproveitamentoErros" XValueMember="Questão" YValueMembers="Qtde. Acertos" ChartType="Column" Url="" ></asp:Series>--%>
        </Series>
        <ChartAreas>
            <asp:ChartArea Name="ChartArea1" BackColor="#ccffff">
                <AxisY Title="Nº Acertos"></AxisY>
                <AxisX Title="Questão" IsLabelAutoFit="True">
                    <LabelStyle Interval="1" />  
                </AxisX>
                <%--<AxisY  Minimum="-32" Maximum="32"> </AxisY>--%>
                <Area3DStyle Enable3D="true" />
            </asp:ChartArea>
        </ChartAreas> 
    </asp:Chart>

    <asp:Chart ID="chart2" runat="server" Width="1008px" Height="372px" style="margin-top: 31px" OnDataBound="chart1_DataBound">
        <%--<Titles>
            <asp:Title Text="Percentual de Questões Criadas por Professor na AMC" Font="Arial, 12pt, style=Bold" />
        </Titles>--%>
        <Series>
            <asp:Series Name="QuestaoAproveitamento" XValueMember="Questão" YValueMembers="Qtde. Erros" Url="" ></asp:Series>
            <%--<asp:Series Name="QuestaoAproveitamentoErros" XValueMember="Questão" YValueMembers="Qtde. Acertos" ChartType="Column" Url="" ></asp:Series>--%>
        </Series>
        <ChartAreas>
            <asp:ChartArea Name="ChartArea1" BackColor="#ff9966">
                <AxisY Title="Nº Erros"></AxisY>
                <AxisX Title="Questão" IsLabelAutoFit="True">
                    <LabelStyle Interval="1" />  
                </AxisX>
                <Area3DStyle Enable3D="true" />
            </asp:ChartArea>
        </ChartAreas> 
    </asp:Chart>
   
</asp:Content>
