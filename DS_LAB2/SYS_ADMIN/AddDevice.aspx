<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddDevice.aspx.cs" Inherits="DS_LAB2.SYS_ADMIN.AddDevice" %>

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
            <h2>新增設備</h2>
            <asp:TextBox ID="tbDname" runat="server"  CssClass="myinput_box" placeholder="設備名稱"></asp:TextBox>
            <div style="height:5px"><br /></div>
            <asp:DropDownList ID="ddlDevice" runat="server" CssClass="myinput_box" style="width:190px"></asp:DropDownList>
            <div style="height:5px"><br /></div>
            <asp:FileUpload id="FileUpload_DeviceImg" CssClass="myinput_box" runat="server" style="width:190px"></asp:FileUpload>
            <div style="height:5px"><br /></div>
            <asp:Button ID="btnAddDevice" runat="server" Text="新增" OnClick="btnAddDevice_Click" CssClass="btn btn-primary" style="width:200px" />

        </div>
    </form>
</body>
</html>
