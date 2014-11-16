<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.Master" AutoEventWireup="true" CodeBehind="~/Paginas/Relatorios/QuestaoNota.aspx.cs" Inherits="UI.Web.EC.Paginas.Relatorios.QuestaoNota" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <asp:Chart ID="chart1" runat="server" Width="924px" Height="409px" OnDataBound="chart1_DataBound">
        <Titles>
            <asp:Title Text="Quantidade Menções" Font="Arial, 12pt, style=Bold" />
        </Titles>
        <Series>
            <asp:Series Name="serie1" XValueMember="Menção" YValueMembers="Quantidade" ></asp:Series>
        </Series>
        <ChartAreas>
            <asp:ChartArea Name="ChartArea1">
                <AxisY Title="Notas"></AxisY>
                <AxisX Title="Questões" IsLabelAutoFit="True">
                    <LabelStyle Angle="30" Interval="1" />
                </AxisX>
                <Area3DStyle Enable3D="True" />
            </asp:ChartArea>
        </ChartAreas>
    </asp:Chart>





</asp:Content>
