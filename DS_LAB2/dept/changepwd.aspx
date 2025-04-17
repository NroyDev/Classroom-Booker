<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/dept/DeptSite.Master" CodeBehind="changepwd.aspx.cs" Inherits="DS_LAB2.dept.changepwd" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div style="background-color:#e3e3e3;position:absolute;width:100vw;left:0px;top:80px">
         <div style="display:flex;justify-content:center;align-content:center;">
            <div style="background-color:#fff;height:50vh;width:22vw;text-align:center;border-radius: 10px;padding:10px;padding-top:30px">
                <h1 style="font-weight:bold;">變更密碼</h1>
                <div style="height:5px"><br /></div>
                <asp:TextBox ID="tbCurrentPwd" runat="server" Placeholder="請輸入舊密碼" type="password" CssClass="myinput_box" style="height: 50px;width:300px;"></asp:TextBox>
                <div style="height:5px"><br /></div>
                <asp:TextBox ID="tbNewPwd" runat="server" Placeholder="請輸入新密碼" type="password" CssClass="myinput_box" style="height: 50px;width:300px;"></asp:TextBox>
                <div style="height:5px"><br /></div>
                <asp:TextBox ID="tbNewPwdConfirm" runat="server"  Placeholder="確認新密碼" type="password" CssClass="myinput_box" style="height: 50px;width:300px;"></asp:TextBox>
                <div style="height:5px"><br /></div>
                <asp:Button ID="btnSubmit" runat="server" Text="更改密碼" onClick="btnSubmit_Click" CssClass="btn btn-primary" style="width:100%"/>
                <div style="height:5px"><br /></div>
                <asp:Label ID="LabelErrorMsg" runat="server" Text=""></asp:Label>
            </div>
        </div>
    </div>
    
</asp:Content>
