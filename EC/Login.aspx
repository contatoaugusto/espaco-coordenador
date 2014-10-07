<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="EC.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="~/content/login.min.css" rel="stylesheet" />
    
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table class="form">
                <tr>
                    <td class="column-label w100">
                        <label for="txtcoAcesso">Matr&#237;cula:</label>
                    </td>
                    <td>
                        <sgi:TextBox ID="txtnuRA" runat="server" RequiredField="true" Width="180px" ErrorMessage="<br />O RA é obrigatório" CssClass="textbox-beta" CssBlur="textbox-beta" CssFocus="textbox-beta" />
                        <input id="txtcoAcesso" name="txtcoAcesso" type="text" class="textbox w150" />
                    </td>
                </tr>
                <tr>
                    <td class="column-label w100">
                        <label for="txtcoAcesso">Senha:</label>
                    </td>
                    <td>
                        <input id="txtcoSenha" name="txtcoSenha" type="password" maxlength="30" class="textbox w150" />
                    </td>
                </tr>

        </table>
    </div>
    </form>
</body>
</html>
