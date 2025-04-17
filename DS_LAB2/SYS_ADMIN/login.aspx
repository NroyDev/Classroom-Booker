<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="DS_LAB2.SYS_ADMIN.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
        <link rel="stylesheet" href="/css/login.css" />
        <title>登入頁面</title>
    </head>
    <body style="background: url('/img/login_bg5.jpg') no-repeat;background-size: cover">
        <form id="loginform" runat="server">
            <div class="login_container">
                <img src="/img/login_face.png" />

                <h1 id="login_title">管理員登入</h1>
                <asp:TextBox ID="userTextBox" runat="server" placeholder="帳號"></asp:TextBox>
                <p></p>
                <asp:TextBox ID="passwordTextBox" runat="server" type="password" placeholder="密碼"></asp:TextBox>
                <asp:Label ID="ServerMSGLabel" runat="server" Text=""></asp:Label>
                <asp:Button ID="loginButton" runat="server" Text="登入" OnClick="loginBtn_Click" />
            </div>
        </form>
    </body>
</html>
