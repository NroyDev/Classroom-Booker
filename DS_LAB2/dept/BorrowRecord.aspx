<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/dept/DeptSite.Master" CodeBehind="BorrowRecord.aspx.cs" Inherits="DS_LAB2.dept.BorrowRecord" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div>
        <p>借閱紀錄關鍵字: </p>
        <asp:TextBox ID="tbBidKeyword" runat="server" Placeholder="借用編號(可為空)" CssClass="myinput_box"></asp:TextBox>
        <asp:TextBox ID="tbBeridKeyword" runat="server" Placeholder="借用人學號(可為空)" CssClass="myinput_box"></asp:TextBox>
        <asp:TextBox ID="tbCnameKeyword" runat="server" Placeholder="教室名稱關鍵字(可為空)" CssClass="myinput_box"></asp:TextBox>
        <asp:TextBox ID="tbDnameKeyword" runat="server" Placeholder="設備名稱關鍵字(可為空)" CssClass="myinput_box"></asp:TextBox>
        <div style="height:5px"><br /></div>

        <div style="display:flex;flex-direction: row;">
            <p style="margin-top:7.5px">指定查詢時間: </p>
            <p style="margin-top:7.5px">開始時間</p>
            <asp:TextBox ID="tbSearchStartDate" runat="server" Placeholder="開始日期" type="Date" CssClass="myinput_box" ></asp:TextBox>
            <p style="margin-top:7.5px">結束時間</p>
            <asp:TextBox ID="tbSearchEndDate" runat="server" Placeholder="結束日期" type="Date" CssClass="myinput_box" ></asp:TextBox>
        </div>
        <div style="height:5px"><br /></div>
        
        <div style="display:flex;flex-direction: row;">
            <p>其他搜尋限制</p>
            <asp:RadioButtonList ID="rblSearchConstraint" runat="server" style="margin-left:5px;">
                <asp:ListItem>無</asp:ListItem>
                <asp:ListItem>僅搜尋已歸還的紀錄</asp:ListItem>
                <asp:ListItem>僅搜尋未歸還的紀錄</asp:ListItem>
            </asp:RadioButtonList>
        </div>
        <div style="height:5px"><br /></div>

        <asp:Button ID="btnSearch" runat="server" Text="搜尋" OnClick="btnSearch_Click" CssClass="btn btn-primary"/>


        <br/>
        <hr/>
        <h3>教室借用紀錄清單</h3>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" ChildrenAsTriggers="true">
            <ContentTemplate>
                    <asp:GridView ID="gvBorrowClassroomList" runat="server" AutoGenerateColumns="false"
                    onrowcommand = "gvBorrowClassroomList_RowCommand" onRowDataBound="gvBorrowClassroomList_RowDataBound"
                    CssClass ="gridview" EmptyDataText="未搜尋到相符的借用紀錄資料!">
                    <Columns>
                        <asp:BoundField DataField="bid" HeaderText=" 借用編號"/>
                        <asp:BoundField DataField="cname" HeaderText=" 教室名稱"/>
                        <asp:BoundField DataField="bdate" HeaderText=" 借用日期" DataFormatString="{0:yyyy-MM-dd dddd}" />
                        <asp:BoundField DataField="starttime" HeaderText=" 開始時間" DataFormatString="{0:HH-mm}"/>
                        <asp:BoundField DataField="endtime" HeaderText=" 結束時間" DataFormatString="{0:HH-mm}"/>
                        <asp:BoundField DataField="berid" HeaderText=" 借用人"/>
                        <asp:BoundField DataField="usage" HeaderText=" 用途"/>
                        <asp:BoundField DataField="returned" HeaderText=" 是否已歸還"/>
                        <asp:ButtonField ButtonType="Button" CommandName="Toggle_return_status" text="確認歸還" ControlStyle-CssClass="btn btn-secondary" />
                    </Columns>
                </asp:GridView>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnSearch"/>
            </Triggers>
        </asp:UpdatePanel>
         
        <br />
        <h3>設備借用紀錄清單</h3>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server" ChildrenAsTriggers="true">
            <ContentTemplate>
                 <asp:GridView ID="gvBorrowDeviceList" runat="server" AutoGenerateColumns="false" 
                     onrowcommand = "gvBorrowDeviceList_RowCommand" onRowDataBound="gvBorrowDeviceList_RowDataBound"
                     CssClass ="gridview" EmptyDataText="未搜尋到相符的借用紀錄資料!">
                     <Columns>
                         <asp:BoundField DataField="bid" HeaderText=" 借用編號"/>
                         <asp:BoundField DataField="dname" HeaderText=" 教室名稱"/>
                         <asp:BoundField DataField="bdate" HeaderText=" 借用日期" DataFormatString="{0:yyyy-MM-dd dddd}" />
                         <asp:BoundField DataField="starttime" HeaderText=" 開始時間" DataFormatString="{0:HH-mm}"/>
                         <asp:BoundField DataField="endtime" HeaderText=" 結束時間" DataFormatString="{0:HH-mm}"/>
                         <asp:BoundField DataField="berid" HeaderText=" 借用人"/>
                         <asp:BoundField DataField="usage" HeaderText=" 用途"/>
                         <asp:BoundField DataField="returned" HeaderText=" 是否已歸還"/>
                         <asp:ButtonField ButtonType="Button" CommandName="Toggle_return_status" text="確認歸還" ControlStyle-CssClass="btn btn-secondary"/>
                     </Columns>
                 </asp:GridView>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnSearch"/>
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>


