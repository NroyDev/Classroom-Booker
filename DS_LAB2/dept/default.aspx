<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/dept/DeptSite.Master" CodeBehind="default.aspx.cs" Inherits="DS_LAB2.dept._default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        
        <div>
            <asp:Label ID="Label_BorrowClassroomToday" runat="server" Text="今天有 0 筆 借用教室的申請"></asp:Label>
            <br />
            <asp:Label ID="Label_ClassroomOverdue" runat="server" Text="有 0 筆 逾期的借用教室申請"></asp:Label>
            <hr />
            <asp:Label ID="Label_BorrowDeviceToday" runat="server" Text="今天有 0 筆 借用設備的申請"></asp:Label>
            <br />
            <asp:Label ID="Label_DeviceOverdue" runat="server" Text="有 0 筆 逾期的借用設備申請"></asp:Label>
        </div>
        <br />

        <h2>當前逾期未還的教室</h2>
        <asp:GridView ID="gvClassroomOverude" runat="server" AutoGenerateColumns="false" CssClass="table" EmptyDataText="目前無逾期未還的教室" >
            <Columns>
                <asp:BoundField DataField="bid" HeaderText="借用編號" />
                <asp:BoundField DataField="cid" HeaderText="教室編號" />
                <asp:BoundField DataField="cname" HeaderText="教室名稱" />
                <asp:BoundField DataField="bdate" HeaderText="借用日期" DataFormatString="{0:yyyy-MM-dd}" />
                <asp:BoundField DataField="starttime" HeaderText="開始時間" DataFormatString="{0:HH:mm}" />
                <asp:BoundField DataField="endtime" HeaderText="結束時間" DataFormatString="{0:HH:mm}" />
                <asp:BoundField DataField="berid" HeaderText="借用者" />
                <asp:BoundField DataField="usage" HeaderText="用途" />
                <asp:BoundField DataField="email" HeaderText="電子郵件" />
                <asp:BoundField DataField="phone" HeaderText="電話" />
            </Columns>
        </asp:GridView>

        
        <h2>當前逾期未還的設備</h2>
        <asp:GridView ID="gvDeviceOverude" runat="server" AutoGenerateColumns="false" CssClass="table" EmptyDataText="目前無逾期未還的設備">
            <Columns>
                <asp:BoundField DataField="bid" HeaderText="借用編號" />
                <asp:BoundField DataField="did" HeaderText="設備編號" />
                <asp:BoundField DataField="dname" HeaderText="設備名稱" />
                <asp:BoundField DataField="bdate" HeaderText="借用日期" DataFormatString="{0:yyyy-MM-dd}" />
                <asp:BoundField DataField="starttime" HeaderText="開始時間" DataFormatString="{0:HH:mm}" />
                <asp:BoundField DataField="endtime" HeaderText="結束時間" DataFormatString="{0:HH:mm}" />
                <asp:BoundField DataField="berid" HeaderText="借用者" />
                <asp:BoundField DataField="usage" HeaderText="用途" />
                <asp:BoundField DataField="email" HeaderText="電子郵件" />
                <asp:BoundField DataField="phone" HeaderText="電話" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
