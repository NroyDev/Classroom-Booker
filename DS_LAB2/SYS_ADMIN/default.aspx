<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="DS_LAB2.SYS_ADMIN._default" enableEventValidation="false" %>

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
        <style>
            .hidden{
                display:none;
            }
        </style>

    </head>
    <body class="bg" style="margin:0px;padding:0px;">
        <form id="form1" runat="server">
            <asp:ScriptManager runat="server">
                <Scripts>
                    <asp:ScriptReference Name="MsAjaxBundle" />
                    <asp:ScriptReference Name="jquery" />
                    <asp:ScriptReference Name="WebForms.js" Assembly="System.Web" Path="~/Scripts/WebForms/WebForms.js" />
                    <asp:ScriptReference Name="WebUIValidation.js" Assembly="System.Web" Path="~/Scripts/WebForms/WebUIValidation.js" />
                    <asp:ScriptReference Name="MenuStandards.js" Assembly="System.Web" Path="~/Scripts/WebForms/MenuStandards.js" />
                    <asp:ScriptReference Name="GridView.js" Assembly="System.Web" Path="~/Scripts/WebForms/GridView.js" />
                    <asp:ScriptReference Name="DetailsView.js" Assembly="System.Web" Path="~/Scripts/WebForms/DetailsView.js" />
                    <asp:ScriptReference Name="TreeView.js" Assembly="System.Web" Path="~/Scripts/WebForms/TreeView.js" />
                    <asp:ScriptReference Name="WebParts.js" Assembly="System.Web" Path="~/Scripts/WebForms/WebParts.js" />
                    <asp:ScriptReference Name="Focus.js" Assembly="System.Web" Path="~/Scripts/WebForms/Focus.js" />
                    <asp:ScriptReference Name="WebFormsBundle" />
                </Scripts>
            </asp:ScriptManager>


            <div style="padding:0px">

                
                <%--####################################### Modal #######################################--%>

                <asp:Panel ID="Panel3" runat="server" DefaultButton="btnChangeUserPwd">
                    <div class="modal fade" id="changpwdUserModal" tabindex="-1" aria-labelledby="changpwdUserModalLabel" aria-hidden="true">
                        <div class="modal-dialog modal-sm"><div class="modal-content">
                            <div class="modal-header">
                            <h5 class="modal-title" id="changpwdUserModalLabel">變更使用者密碼</h5>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                            </div>

                            <div class="modal-body" >
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:Label ID="LabelUserid" runat="server" Text="" Visible="false"></asp:Label>
                                        <asp:TextBox ID="tbPwdUser" runat="server" CssClass="myinput_box" type="password" placeholder="新密碼" style="width:250px"></asp:TextBox>
                                        <div style="height:5px"><br /></div>
                                        <asp:TextBox ID="tbPwdUserConfirm" runat="server"  CssClass="myinput_box" type="password" placeholder="確認新密碼" style="width:250px"></asp:TextBox>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="gvUser"/>
                                    </Triggers>
                                </asp:UpdatePanel>
            
                            </div>

                            <div class="modal-footer">
                                <asp:Button ID="btnChangeUserPwd" runat="server" Text="變更" OnClick="btnChangeUserPwd_Click" CssClass="btn btn-primary" />
                                <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">關閉</button>
                            </div>
                        </div></div>
                    </div>
                </asp:Panel>
                

                <asp:Panel ID="Panel4" runat="server" DefaultButton="btnChangeDeptPwd">
                     <div class="modal fade" id="changpwdDeptModal" tabindex="-1" aria-labelledby="changpwdDeptModalLabel" aria-hidden="true">
                        <div class="modal-dialog modal-sm"><div class="modal-content">
                            <div class="modal-header">
                            <h5 class="modal-title" id="changpwdDeptModalLabel">變更系辦帳戶密碼</h5>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                            </div>

                            <div class="modal-body">
             
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:Label ID="LabelDeptid" runat="server" Text="" Visible="false"></asp:Label>
                                        <asp:TextBox ID="tbPwdDept" runat="server"  CssClass="myinput_box" type="password"  placeholder="新密碼" style="width:250px"></asp:TextBox>
                                        <div style="height:5px"><br /></div>
                                        <asp:TextBox ID="tbPwdDeptConfirm" runat="server"  CssClass="myinput_box" type="password"  placeholder="確認新密碼" style="width:250px"></asp:TextBox>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="gvDept"/>
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>

                            <div class="modal-footer">
                                <asp:Button ID="btnChangeDeptPwd" runat="server" Text="變更" OnClick="btnChangeDeptPwd_Click" CssClass="btn btn-primary" />
                                <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">關閉</button>
                            </div>
                        </div></div>
                    </div>
                </asp:Panel>

                
                <asp:Panel ID="Panel5" runat="server" DefaultButton="btnChangeDeptInfo">
                     <div class="modal fade" id="changeInfoDeptModal" tabindex="-1" aria-labelledby="changeInfoDeptModalLabel" aria-hidden="true">
                        <div class="modal-dialog"><div class="modal-content">
                            <div class="modal-header">
                            <h5 class="modal-title" id="changeInfoDeptModalLabel">變更系辦帳戶資料</h5>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                            </div>

                            <div class="modal-body">
             
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <div style="display:flex;flex-direction:row">
                                            <p>系辦編號:　　</p>
                                            <asp:Label ID="LabelDeptid_ChangeInfo" runat="server" Text=""></asp:Label>
                                        </div>
                                        <div style="height:5px"><br /></div>
                                        <div style="display:flex;flex-direction:row">
                                            <p>系辦名稱:　　</p>
                                            <asp:TextBox ID="tbDeptNewName" runat="server"  CssClass="myinput_box" placeholder="名稱" style="width:400px"></asp:TextBox>
                                        </div>
                                        
                                        <div style="height:5px"><br /></div>
                                        <div style="display:flex;flex-direction:row">
                                            <p>系辦帳號:　　</p>
                                            <asp:TextBox ID="tbDeptNewAcco" runat="server"  CssClass="myinput_box" placeholder="帳號" style="width:400px"></asp:TextBox>
                                        </div>
                                        <div style="height:5px"><br /></div>
                                        <div style="display:flex;flex-direction:row">
                                            <p>系辦Email: 　 </p>
                                        <asp:TextBox ID="tbDeptNewEmail" runat="server"  CssClass="myinput_box" placeholder="Email" style="width:400px"></asp:TextBox>
                                        </div>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="gvDept"/>
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>

                            <div class="modal-footer">
                                <asp:Button ID="btnChangeDeptInfo" runat="server" Text="變更" OnClick="btnChangeDeptInfo_Click" CssClass="btn btn-primary" />
                                <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">關閉</button>
                            </div>
                        </div></div>
                    </div>
                </asp:Panel>
               
                <%--####################################### NAV BAR #######################################--%>

                <nav class="mynavbar" style="margin:0px">
                    <img class="mynavbar_logo" src="../img/nsysu_logo.svg" />
                    <a href="#" class="mynavbar_logo_text">系統管理員
                    </a>
                    <div style="width:70%;">
                        <ul class="mynav_lists">
                            <li ><a href="default">首頁</a></li>
                            <li ><a href="AddClassroom">新增教室</a></li>
                            <li ><a href="AddDevice">新增設備</a></li>
                            <li ><a href="AddDept">新增系辦</a></li>
                        </ul>
                    </div>
                    <div style="width: 30%; display: flex;justify-content: flex-end;">
                        <asp:Button ID="btnLogout" runat="server" Text="Logout" onClick="btnLogout_Click" CssClass="btn btn-outline-secondary" />
                    </div>
                </nav>
                
                <%--####################################### MAIN #######################################--%>

                <div class="myMainContainer">
                    <h2>變更使用者密碼</h2>
                     <asp:TextBox ID="tbKeyword" runat="server" CssClass="myinput_box" placeholder="帳號關鍵字"></asp:TextBox>
                     <asp:Button ID="btnSearch" runat="server" Text="搜尋" OnClick="btnSearch_Click" CssClass="btn btn-primary" />
                     <div style="height:5px"><br /></div>
                     <div style="display:flex;flex-direction: row;">
                         <asp:RadioButtonList ID="rblSearchConstraint" runat="server" style="display:flex;flex-direction: row;margin-left:5px;">
                             <asp:ListItem>搜尋借用者</asp:ListItem>
                             <asp:ListItem>搜尋系辦</asp:ListItem>
                         </asp:RadioButtonList>
                     </div>
                     <hr />

                    <asp:Panel ID="Panel1" runat="server" Visible="false">
                        <h2>借用人</h2>
                        <asp:GridView ID="gvUser" runat="server" AutoGenerateColumns="false" CssClass="gridview" EmptyDataText="查無相關資料">
                            <Columns>
                                <asp:BoundField DataField="id" HeaderText="學號(帳號)" />
                                <asp:BoundField DataField="realname" HeaderText="姓名" />
                                <%--<asp:BoundField DataField="email" HeaderText="Email" />
                                <asp:BoundField DataField="phone" HeaderText="連絡電話" />
                                <asp:BoundField DataField="roomid" HeaderText="實驗室或辦公室編號" />--%>
                                <asp:TemplateField HeaderText="密碼" HeaderStyle-Width="90px">
                                    <ItemTemplate>
                                        <div style="width:100%;display:flex;justify-content:center;">
                                            <asp:Button ID="changepwd_user" runat="server" Text="更改" OnClick="changepwd_user_Click" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#changpwdUserModal"/>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </asp:Panel>

                    <asp:Panel ID="Panel2" runat="server" Visible="false">
                        <h2>系辦</h2>
                        <asp:GridView ID="gvDept" runat="server" AutoGenerateColumns="false" CssClass="gridview" EmptyDataText="查無相關資料">
                            <Columns>
                                <asp:BoundField DataField="dept_id" HeaderText="系辦編號" />
                                <asp:BoundField DataField="dept_name" HeaderText="系辦名稱" />
                                <asp:BoundField DataField="account" HeaderText="系辦帳號" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                                <asp:BoundField DataField="email" HeaderText="Email" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                                <asp:TemplateField HeaderText="資料" HeaderStyle-Width="90px">
                                    <ItemTemplate>
                                        <div style="width:100%;display:flex;justify-content:center;">
                                            <asp:Button ID="changeInfo_dept" runat="server" Text="更改" OnClick="changeInfo_dept_Click" class="btn btn-outline-secondary" data-bs-toggle="modal" data-bs-target="#changeInfoDeptModal"/>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="密碼" HeaderStyle-Width="90px">
                                    <ItemTemplate>
                                        <div style="width:100%;display:flex;justify-content:center;">
                                            <asp:Button ID="changepwd_dept" runat="server" Text="更改" OnClick="changepwd_dept_Click" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#changpwdDeptModal"/>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </asp:Panel>
                </div>


                
                
            </div>
        </form>
    </body>
</html>
