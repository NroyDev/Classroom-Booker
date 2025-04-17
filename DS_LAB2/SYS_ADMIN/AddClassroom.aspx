<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddClassroom.aspx.cs" Inherits="DS_LAB2.SYS_ADMIN.AddClassroom" %>

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
            <h2>新增教室</h2>
            <asp:TextBox ID="tbCname" runat="server"  CssClass="myinput_box" placeholder="教室名稱"></asp:TextBox>
            <div style="height:5px"><br /></div>
            <asp:TextBox ID="tbLocation" runat="server" CssClass="myinput_box" placeholder="教室位置"></asp:TextBox>
            <div style="height:5px"><br /></div>
            <asp:TextBox ID="tbCapacity" runat="server"  CssClass="myinput_box" placeholder="教室容量"></asp:TextBox>
            <div style="height:5px"><br /></div>
            <asp:DropDownList ID="ddlDept" CssClass="myinput_box" runat="server" style="width:190px"></asp:DropDownList>
            <div style="height:5px"><br /></div>
            <asp:FileUpload id="FileUpload_Classroom" CssClass="myinput_box" runat="server" style="width:190px"></asp:FileUpload>
            <div style="height:5px"><br /></div>
            <asp:Button ID="btnAddClassroom" runat="server" Text="新增" OnClick="btnAddClassroom_Click" CssClass="btn btn-primary" style="width:200px" />

        </div>
    </form>
</body>
</html>
