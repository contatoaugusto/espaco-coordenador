<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="UI.Web.EC.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="~/content/scripts/jquery-1.8.3.min.js" type="text/javascript"></script>
    <%--<link href="Content/EspacoCoordenador.css" rel="stylesheet" />--%>
    <%--<link href="Content/Page.css" rel="stylesheet" />--%>
    <title>Espaço Coordenador</title>


     <style type="text/css">
         .caixa
        {
            border: 1px solid #d5d5d5;
             position: relative;
            width: 304px;
             padding-right: 12px;
             display: inline;
             background: url(/Content/Images/caixa-bg.png) repeat-x left bottom;
             float: left;
            height: 89px;
             color: #6a6665;
             text-align: left;
             margin-top: 100px;
             top: -14px;
             left: -19px;
             padding-left: 20px;
             padding-top: 20px;
             padding-bottom: 20px;
         }
        .caixa .lt
        {
            position: absolute;
            padding-bottom: 0px;
            margin: 0px;
            padding-left: 0px;
            width: 9px;
            padding-right: 0px;
            display: block;
            background-repeat: no-repeat;
            background-position: left top;
            height: 9px;
            font-size: 1px;
            padding-top: 0px;
        }
        .caixa .rt
        {
            position: absolute;
            padding-bottom: 0px;
            margin: 0px;
            padding-left: 0px;
            width: 9px;
            padding-right: 0px;
            display: block;
            background-repeat: no-repeat;
            background-position: left top;
            height: 9px;
            font-size: 1px;
            padding-top: 0px;
        }
        .caixa .lb
        {
            position: absolute;
            padding-bottom: 0px;
            margin: 0px;
            padding-left: 0px;
            width: 9px;
            padding-right: 0px;
            display: block;
            background-repeat: no-repeat;
            background-position: left top;
            height: 9px;
            font-size: 1px;
            padding-top: 0px;
        }
        .caixa .rb
        {
            position: absolute;
            padding-bottom: 0px;
            margin: 0px;
            padding-left: 0px;
            width: 9px;
            padding-right: 0px;
            display: block;
            background-repeat: no-repeat;
            background-position: left top;
            height: 9px;
            font-size: 1px;
            padding-top: 0px;
        }
        .caixa .lt
        {
            background: url(/Content/Images/caixa-lt.png) no-repeat left top;
            top: -1px;
            left: -1px;
        }
        .caixa .rt
        {
            background: url(/Content/Images/caixa-rt.png) no-repeat right top;
            top: -1px;
            right: -1px;
            _right: -2px;
        }
        .caixa .lb
        {
            bottom: -1px;
            background: url(/Content/Images/caixa-lb.png) no-repeat left bottom;
            left: -1px;
        }
        .caixa .rb
        {
            bottom: -1px;
            background: url(/Content/Images/caixa-rb.png) no-repeat right bottom;
            right: -1px;
            _right: -2px;
        }
        
        .content
        {
            text-align: center;
        }
        #logobeta
        {
            background: url('data:image/png;base64,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') /*/Content/Images/loginbeta/logobeta.png*/ no-repeat;
            *background: url(/Content/Images/loginbeta/logobeta.png) no-repeat; /* For IE 6 and 7 */
            width: 213px;
            height: 219px;
            position: absolute;
            right: 220px;
            top: 34px;
            z-index: 100;
        }
        .textbox-beta 
        {
            border:dotted 1px #ccc;
            color:#999;
            font-size:150%;
            font-weight:bold;
            text-align:center;
            padding:3px;

        }
        .cabecalho
        {
            /*border-bottom: #d5d5d5 1px solid;
            border-left: #d5d5d5 1px solid;*/
            position: relative;
            padding: 20px;
            width: 350px;
            /*padding-right: 12px;*/
            /*display: inline;*/
            /*background: url(/Content/Images/caixa-bg.png) repeat-x left bottom;*/
            float: left;
            height: auto;
            /*color: #6a6665;*/
            /*border-top: #d5d5d5 1px solid;*/
            /*border-right: #d5d5d5 1px solid;*/
            text-align: center;
            margin-top: 50px;
             top: 97px;
             left: -43px;
         }

        
        /* Begin Button */
        /*
        label.button
        {
	        cursor: pointer;
	        text-align: center;
	        line-height: normal !important;
	        text-decoration: none;
	        vertical-align: top;
	        display: inline-block;
	        white-space: nowrap;
	        background: #f1f2f2;
	        border: solid 1px #dadada;
	        border-radius: 2px;
	        box-shadow: 0px 0px 0px #ccc;
	        width: auto;
	        height: auto;
	        margin-right: 5px;
        }*/

        .botao
        {
	        background: #fff;
	        border: solid 1px #ccc;
	        cursor: pointer;
	        color: #717272;
	        border-radius: 2px;
	        padding: 6px 8px;
	        font-size: 90%;
	        font-weight: bold;
	        min-width:50px;
        }

        .botao:hover
        {
	        background: #f1f1f1;
	        color: #494848;
        }

        label.button button
        {
	        background: #fff;
	        border: solid 1px #ccc;
	        cursor: pointer;
	        color: #717272;
	        border-radius: 2px;
	        padding: 6px 8px;
	        font-size: 90%;
	        font-weight: bold;
	        min-width:50px;
        }

        label.button button.red
        {
	        border: solid 1px #851115;
	        background: #af1d22;
	        color:#fff;
        }

        label.button button:hover
        {
	        background: #f1f1f1;
	        color: #494848;
        }

        label.button button.red:hover
        {
	        background: #851115;
	        color:#f1f1f1;
        }
        /* End Button */
    </style>

</head>
<body>
    <script type="text/javascript">
        $(document).ready(function () {

            $('#txtcoAcesso').keydown(function (e) {
                if (e.keyCode == 13)
                    $('#btnLogin').click()
            });
        });

    </script>

    <form id="form1" method="post" runat="server">
        <div style="top: 100px; width: 300px; margin: auto;">
            <div class="cabecalho" style="font-size: 110%; color: #990000; font-weight: bold;">Espaço Coordenador</div>
            <div id="login-container" class="caixa">
                <center>
                <div >
                    
                        <div>
                            Matrícula:
                            <sgi:TextBox ID="txtMatricula" runat="server" RequiredField="true" Width="180px" ErrorMessage="<br />A matrícula é obrigatória" CssClass="textbox-beta" CssBlur="textbox-beta" CssFocus="textbox-beta" />
                        </div>
                        <div>
                            Senha:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                           <sgi:TextBox ID="txtcoAcesso" runat="server" TextMode="Password" Width="180px" RequiredField="true"
                                ErrorMessage="<br />A senha é obrigatória" CssClass="textbox-beta" CssBlur="textbox-beta" CssFocus="textbox-beta" Text="tete" />
                        </div>
                </div>
                </center>
                </div>
           <div>
              
               <center>
                        <label class="button">
                            <sgi:Button ID="btnLogin" runat="server" Text="Ok" Style="margin-left: 10px;" CausesValidation="true" OnClick="btnLogin_Click" />
                        </label>

                    </center>
            </div>
        </div>
        <table class="w150">
            <tr>

                <td>
                    

                </td>
            </tr>

        </table>
        <div id="footer">Espaço Coordenador – UniCEUB © 2014</div>
        <sgi:MessageBox ID="messageBox" runat="server" />
        <sgi:AlertBox ID="alert" runat="server" AutoClose="true" Visible="false" Timeout="25" />
    </form>
</body>
</html>
