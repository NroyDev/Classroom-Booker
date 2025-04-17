<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="register.aspx.cs" Inherits="DS_LAB2.register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
        <link rel="stylesheet" href="css/register.css" />
        <title>註冊頁面</title>
    </head>
    <body>
        <form id="registerform" runat="server">
            <div class="register_container">
                <div id="left">
                    <img src="img/login_face.png" />
                    <h1 id="register_title">註冊</h1>
                    <asp:TextBox ID="userTextBox" runat="server" placeholder="學號或職位編號"/>
                    <asp:TextBox ID="passwordTextBox" runat="server" placeholder="密碼" type="password"/>
                    <asp:TextBox ID="passwordConfirmTextBox" runat="server" placeholder="確認密碼" type="password" CausesValidation="True" />
                    <asp:Label ID="ServerMSGLabel" runat="server" Text=""></asp:Label>
                </div>

                <div id="sep"></div>

                <div id="right">
                    <asp:TextBox ID="nameTextBox" runat="server" placeholder="姓名"/>
                    <asp:TextBox ID="phoneTextBox" runat="server" placeholder="電話"/>
                    <asp:TextBox ID="emailBox" runat="server" placeholder="E-Mail"/>
                    <asp:TextBox ID="roomidBox" runat="server" placeholder="實驗室編號或辦公室編號"/>
                    <p><a href="login">點擊此處登入</a></p>
   
                    <asp:Button ID="registerButton" runat="server" Text="註冊" OnClick="registerButton_Click" />
                </div>
            </div>
        </form>
    </body>
</html>
