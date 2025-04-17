<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="DS_LAB2.index" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div>


        <h1>當前教室借用申請</h1>
        <asp:GridView ID="gvBorrowClassrom" runat="server" AutoGenerateColumns="false" CssClass="gridview" EmptyDataText="目前沒有借用教室的申請">
            <columns>
                <asp:boundfield datafield="bid" headertext="借用編號" />
                <asp:boundfield datafield="cname" headertext="教室名稱" />
                <asp:boundfield datafield="bdate" headertext="借用日期" dataformatstring="{0:yyyy/MM/dd}" />
                <asp:boundfield datafield="starttime" headertext="開始時間" dataformatstring="{0:hh:mm}"/>
                <asp:boundfield datafield="endtime" headertext="結束時間" dataformatstring="{0:hh:mm}"/>
                <asp:boundfield datafield="usage" headertext="用途" />
                <asp:TemplateField HeaderText="" HeaderStyle-Width="100px">
                    <ItemTemplate>
                        <asp:Button ID="btnPrintClassroom" runat="server" Text="列印申請單" OnClick="btnPrintClassroom_click" CssClass="btn btn-outline-secondary" />
                    </ItemTemplate>
                </asp:TemplateField>
            </columns>
        </asp:GridView>

        <br />
        <h1>當前設備借用申請</h1>
        <asp:GridView ID="gvBorrowDevice" runat="server" AutoGenerateColumns="false" CssClass="gridview" EmptyDataText="目前沒有借用設備的申請">
            <Columns>
                <asp:BoundField DataField="bid" HeaderText="借用編號" />
                <asp:BoundField DataField="dname" HeaderText="設備名稱" />
                <asp:BoundField DataField="bdate" HeaderText="借用日期" DataFormatString="{0:yyyy/MM/dd}" />
                <asp:BoundField DataField="starttime" HeaderText="開始時間" DataFormatString="{0:HH:mm}"/>
                <asp:BoundField DataField="endtime" HeaderText="結束時間" DataFormatString="{0:HH:mm}"/>
                <asp:BoundField DataField="usage" HeaderText="用途" />
                <asp:TemplateField HeaderText="" HeaderStyle-Width="100px">
                    <ItemTemplate>
                        <asp:Button ID="btnPrintDevice" runat="server" Text="列印申請單" OnClick="btnPrintDevice_click"  CssClass="btn btn-outline-secondary" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>






    </div>
</asp:Content>
