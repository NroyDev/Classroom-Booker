<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="printForm.aspx.cs" Inherits="DS_LAB2.printForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link rel="stylesheet" href="css/printform.css" />
</head>
<body>
    <form id="form1" runat="server">
        
         <script type="text/javascript">
             function print() {
                 window.print();
             }
         </script>
        <div>
            <div id="bg">
            <img id="bgImg" src="img/nsysu_logo.svg" />
        </div>
        <div id="header_text">
            <h1>國立中山大學-教室與設備借用申請表</h1>
        </div>
        <br />
        <table>
            <tbody>
              <tr>
                <th>借用教室或設備名稱與編號</th>
                <td>
                  <asp:Label ID="Label_id_name" runat="server" Text=""></asp:Label>
                </td>
                <th>申請日期</th>
                <td colspan="3">
                  <asp:Label ID="Label_Today" runat="server" Text="____ / __ / __"></asp:Label>
                </td>
              </tr>
              <tr>
                <th>用途</th>
                <td colspan="3" style="height:50px">
                    <asp:Label ID="Label_usage" runat="server" Text=""></asp:Label>
                </td>
              </tr>
              <tr>
                <th rowspan="2">借用時間</th>
                <td>
                    <asp:Label ID="Label_start" runat="server" Text="自 ____ 年 __ 月 __ 日 __ 時 __ 分"></asp:Label>
                </td>
                <th rowspan="2">類別</th>
                <td rowspan="2"> 
                    <asp:CheckBox ID="cbClassroom" runat="server" text="借用教室"/>
                    <br />
                    <asp:CheckBox ID="cbDevice" runat="server" text="借用設備"/>
                </td>
              </tr>
              <tr>
                <td>
                  <asp:Label ID="Label_end" runat="server" Text="至 ____ 年 __ 月 __ 日 __ 時 __ 分"></asp:Label>
                </td>
              </tr>

              <tr>
                <th>相關證明</th>
              </tr>

              <tr>
                <th rowspan="3">申請人</th>
                <td rowspan="3">簽名</td>
                <th>學號</th>
                <td>
                    <asp:Label ID="Label_berid" runat="server" Text=""></asp:Label>
                </td>
              </tr>
              <tr>
                <th>手機</th>
                <td>
                    <asp:Label ID="Label_phone" runat="server" Text=""></asp:Label>
                </td>
              </tr>
              <tr>
                <th>Email</th>
                <td>
                    <asp:Label ID="Label_email" runat="server" Text=""></asp:Label>
                </td>
              </tr>
              <tr>
                <th style="height:50px">場地借用負責人簽章</th>
                <td></td>
                <th>審核日期</th>
                <td> ____ / __ / __ </td>
              </tr>
              <tr>
                <td colspan="4">
                    <div>
                        <div style="height:50px"><p>審核結果:</p></div>
                        <asp:CheckBox ID="cbAgree" runat="server" text="同意借用，應繳金額為: __________"/>
                        <asp:CheckBox ID="cbDisagree" runat="server" text="不同意借用 原因: __________"/>
                    </div>
                </td>
              </tr>
            </tbody>
          </table>
        </div>
    </form>
</body>
</html>
