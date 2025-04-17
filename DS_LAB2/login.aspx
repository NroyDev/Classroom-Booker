<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="DS_LAB2.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
        <link rel="stylesheet" href="css/login.css" />
        <title>登入頁面</title>
    </head>
    <body>
        <form id="loginform" runat="server">
            <div class="login_container">
                <img src="./img/login_face.png" />

                <h1 id="login_title">登入</h1>
                <asp:TextBox ID="userTextBox" runat="server" placeholder="學號或職位編號"></asp:TextBox>
                <p></p>
                <asp:TextBox ID="passwordTextBox" runat="server" type="password" placeholder="密碼"></asp:TextBox>
                <p><a href="register">點擊此處註冊</a></p>
                <asp:Label ID="ServerMSGLabel" runat="server" Text=""></asp:Label>
                <asp:Button ID="loginButton" runat="server" Text="登入" OnClick="loginBtn_Click" />
            </div>
        </form>
    </body>
</html>
