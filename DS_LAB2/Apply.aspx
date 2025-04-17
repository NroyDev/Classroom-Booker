<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Apply.aspx.cs" Inherits="DS_LAB2.Apply" %>



<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        td input[type='checkbox'] {
            -webkit-appearance:none;
            width:100%;
            height:40px;
            margin:0px;
            border-radius:5px;
            border:0.5px solid #e7e7e7;
        }
        td input[type='checkbox']:checked {
            background: #0060ff;
        }
        td{
            padding:0px !important; 
        }
        .td span{
            padding-left:10px;
            text-align:center;
        }
    </style>
    <div>

        <h2 style="font-weight: bold;">申請借用</h2>
        <asp:Panel ID="pAreaSearch" runat="server" DefaultButton="btnSearch">
            <asp:TextBox ID="tbDate" runat="server" type="Date" CssClass="myinput_box"></asp:TextBox>
            <asp:TextBox ID="tbCnameKeyword" runat="server" Placeholder="教室關鍵字(可為空)"  CssClass="myinput_box"></asp:TextBox>
            <asp:Button ID="btnSearch" runat="server" Text="查詢" onClick="btnSearch_Click"  CssClass="btn btn-primary"/>
            <div style="height:5px"><br /></div>
            <asp:CheckBox ID="cbNeedDevice" runat="server" Text="需要借用設備" />
            <asp:TextBox ID="tbDnameKeyword" runat="server" Placeholder="設備關鍵字(可為空)"  CssClass="myinput_box"></asp:TextBox>
        </asp:Panel>

        <asp:Panel ID="pAfterSearch" runat="server" Visible="false">
            <hr />
            <h2>選擇要借用的教室與時間</h2>
            <asp:GridView ID="gvClassroom" runat="server" CssClass="gridview"
              OnRowDataBound="gvClassroom_RowDataBound" EmptyDataText="查無資料!" >
            </asp:GridView>
            <br />
            <h2>選擇要借用的設備與時間</h2>
            <asp:GridView ID="gvDevice" runat="server" CssClass="gridview"
                OnRowDataBound="gvDevice_RowDataBound" EmptyDataText="查無資料">
            </asp:GridView>
            <br/>

            
            <asp:Panel ID="pSubmit" runat="server" DefaultButton="btnSubmit">
                <asp:TextBox ID="tbUsage" runat="server" Placeholder="請輸入用途" CssClass="myinput_box"></asp:TextBox>
                <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click"  CssClass="btn btn-primary"/>
                <br/>
            </asp:Panel>
        </asp:Panel>
    </div>
</asp:Content>
