<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.Master" AutoEventWireup="true" CodeBehind="ProfessorQuestoes.aspx.cs" Inherits="UI.Web.EC.Paginas.Relatorios.ProfessorQuestoes" %>
<%@ Register assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <asp:Chart ID="chartProfessorQuestao" runat="server" Width="924px" Height="409px" >
        <Titles>
            <asp:Title Text="Questões Professor Utilizada em Prova da AMC" Font="Arial, 12pt, style=Bold" />
        </Titles>
        <Series>
            <asp:Series Name="ProfessorQuestao" XValueMember="Professor" YValueMembers="Qtde. Questões" ></asp:Series>
        </Series>
        <ChartAreas>
            <asp:ChartArea Name="ChartArea1">
                <AxisY Title="Questões"></AxisY>
                <AxisX Title="Professores" IsLabelAutoFit="True">
                    <LabelStyle Angle="30" Interval="1" />
                </AxisX>
                <Area3DStyle Enable3D="True" />
            </asp:ChartArea>
        </ChartAreas>
    </asp:Chart>





</asp:Content>
