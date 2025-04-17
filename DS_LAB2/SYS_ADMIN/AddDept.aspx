<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddDept.aspx.cs" Inherits="DS_LAB2.SYS_ADMIN.AddDept" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
        <link rel="stylesheet" href="../css/MyMaster.css" />
        <link rel="stylesheet" href="../css/GridViewCss.css" />
        <webopt:bundlereference runat="server" path="~/Content/css" />
        <link href="~/favicon.ico" rel="shortcut icon" type="image/x-icon" />
        <script src="../Scripts/bootstrap.js"></script>
</head>
<body style="padding-left:40%;padding-top:40px">
    <form id="form1" runat="server">
        <div>
            <h2>新增系辦</h2>
            <asp:TextBox ID="tbName" runat="server" CssClass="myinput_box" placeholder="系辦名稱"></asp:TextBox>
            <div style="height:5px"><br /></div>
            <asp:TextBox ID="tbDeptAcco" runat="server" CssClass="myinput_box" placeholder="系辦帳號"></asp:TextBox>
            <div style="height:5px"><br /></div>
            <asp:TextBox ID="tbDeptPwd" runat="server"  CssClass="myinput_box" type="password"  placeholder="密碼"></asp:TextBox>
            <div style="height:5px"><br /></div>
            <asp:TextBox ID="tbDeptConfirmPwd" runat="server"  CssClass="myinput_box" type="password"  placeholder="確認密碼"></asp:TextBox>
            <div style="height:5px"><br /></div>
            <asp:TextBox ID="tbEmail" runat="server" CssClass="myinput_box" placeholder="系辦 Email"></asp:TextBox>
            <div style="height:5px"><br /></div>
            <asp:Button ID="btnSubmit" runat="server" Text="新增" OnClick="btnSubmit_Click" CssClass="btn btn-primary" style="width:200px"/>
        </div>

    </form>
</body>
</html>
