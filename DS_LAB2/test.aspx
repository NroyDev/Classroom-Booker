<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="test.aspx.cs" Inherits="DS_LAB2.test" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:ScriptManager EnablePartialRendering="true"
            ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <script src="Scripts/bootstrap-5.3.3/bootstrap.js"></script>
            <link rel="stylesheet" href="css/bootstrap-5.3.3/bootstrap.css" />
            <script>    <!-- Bootstrap 的 Modal -->
                var myModal = document.getElementById('myModal')
                var myInput = document.getElementById('myInput')

                myModal.addEventListener('shown.bs.modal', function () {
                    myInput.focus()
                })
            </script>
            
            <div class="modal fade" id="exampleModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
                <div class="modal-dialog modal-xl">
                <div class="modal-content">
                    <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel">教室借用狀況</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
          
                    <div class="modal-body">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <br />
                            <asp:GridView ID="gvBorrow_status" runat="server" CssClass="table" ></asp:GridView>
                            <br />
                        </ContentTemplate>

                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="gvClassroom"/>
                        </Triggers>
                    </asp:UpdatePanel>
                    </div>


                    <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">關閉</button>
                    </div>
                </div>
                </div>
            </div>
            
            <asp:Panel ID="pAreaSearch" runat="server" DefaultButton="btnSearch">
                <asp:TextBox ID="tbCnameKeyword" runat="server" Placeholder="教室關鍵字(可為空)"></asp:TextBox>
                <asp:Button ID="btnSearch" runat="server" Text="搜尋" OnClick="btnSearch_Click" />
            </asp:Panel>

            <asp:GridView ID="gvClassroom" runat="server" AutoGenerateColumns="false" EmptyDataText="查無資料!" CssClass="table">
                <Columns>
                    <asp:BoundField DataField="cid" HeaderText="教室編號" />
                    <asp:BoundField DataField="cname" HeaderText="教室名稱" />
                    <asp:BoundField DataField="location" HeaderText="教室位置" />
                    <asp:BoundField DataField="capacity" HeaderText="可容納人數" />
                    <asp:BoundField DataField="dept_name" HeaderText="負責管理的系辦" />
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="SearchAvaliable" runat="server" Text="搜尋近7日可借用時間" OnClick="btnSearchAvaliable"  class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#exampleModal"/>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>


        </div>
    </form>
</body>
</html>
