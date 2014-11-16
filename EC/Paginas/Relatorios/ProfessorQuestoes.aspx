<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.Master" AutoEventWireup="true" CodeBehind="~/Paginas/Relatorios/ProfessorQuestoes.aspx.cs" Inherits="UI.Web.EC.Paginas.Relatorios.ProfessorQuestoes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <asp:Chart ID="chartProfessorQuestao" runat="server" Width="924px" Height="356px" style="margin-top: 31px" OnDataBound="chartProfessorQuestao_DataBound">
        <%--<Titles>
            <asp:Title Text="Percentual de Questões Criadas por Professor na AMC" Font="Arial, 12pt, style=Bold" />
        </Titles>--%>
        <Series>
            <asp:Series Name="ProfessorQuestao" XValueMember="Professor" YValueMembers="Qtde. Questões" ChartType="Column" Url=""></asp:Series>
        </Series>
        <ChartAreas>
            <asp:ChartArea Name="ChartArea1">
                <AxisY Title="Questões (%)"></AxisY>
                <AxisX Title="Professores" IsLabelAutoFit="True">
                    <LabelStyle Angle="30" Interval="1" />
                </AxisX>
                <Area3DStyle Enable3D="True" />
            </asp:ChartArea>
        </ChartAreas>
    </asp:Chart>

    <asp:Chart ID="chartProfessorQuestao2" runat="server" Width="924px" Height="351px" style="margin-top: 25px" OnDataBound="chartProfessorQuestao2_DataBound">
        <%--<Titles>
            <asp:Title IsDockedInsideChartArea="true" Text="Percentual de Questões por Professor Utilizadas na Prova" Font="Arial, 12pt, style=Bold" />
        </Titles>--%>
        <Series>
            <asp:Series Name="ProfessorQuestao2" XValueMember="Professor" YValueMembers="Qtde. Questões" ></asp:Series>
        </Series>
        <ChartAreas>
            <asp:ChartArea Name="ChartArea2">
                <AxisY Title="Questões (%)"></AxisY>
                <AxisX Title="Professores" IsLabelAutoFit="True">
                    <LabelStyle Angle="30" Interval="1" />
                </AxisX>
                <Area3DStyle Enable3D="True" />
            </asp:ChartArea>
        </ChartAreas>
    </asp:Chart>



</asp:Content>
