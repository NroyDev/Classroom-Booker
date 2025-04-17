<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/dept/DeptSite.Master" CodeBehind="AllDevice.aspx.cs" Inherits="DS_LAB2.dept.AllDevice" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        
        <div class="modal fade" id="exampleModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
            <div class="modal-dialog modal-xl">
            <div class="modal-content">
                <div class="modal-header">
                <h5 class="modal-title" id="exampleModalLabel">設備借用狀況</h5>
                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>

                <div class="modal-body">
                   <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <br />
                            <asp:GridView ID="gvBorrow_status" runat="server" CssClass="gridview" ></asp:GridView>
                            <br />
                        </ContentTemplate>

                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="gvDevice"/>
                        </Triggers>
                    </asp:UpdatePanel>
                </div>

                <div class="modal-footer">
                <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">關閉</button>
                </div>
            </div>
            </div>
        </div>
        

        
        <div class="modal fade" id="exampleModal2" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
            <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                <h5 class="modal-title">變更設備可借用時間</h5>
                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>

                <div class="modal-body">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <br />
                        <asp:Label ID="Label_ava_did" runat="server" Text="" Visible="false"></asp:Label>
                        <asp:GridView ID="gvDeviceAvaliable" runat="server" OnRowDataBound="gvDeviceAvaliable_RowDataBound" AutoGenerateColumns="true"    CssClass="gridview"></asp:GridView>
                        <br />
                    </ContentTemplate>

                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="gvDevice"/>
                    </Triggers>
                </asp:UpdatePanel>
                </div>

                <div class="modal-footer">
                    <asp:Button ID="BtnSaveChange" runat="server" class="btn btn-primary" Text="保存" onClick="BtnSaveChange_Click"/>
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">關閉</button>
                </div>
            </div>
            </div>
        </div>

        
        
        <div class="modal fade" id="imageModal" tabindex="-1" aria-labelledby="imageModal" aria-hidden="true">
            <div class="modal-dialog modal-lg" >
            <div class="modal-content" >
                <div class="modal-body" style="padding:0;">
                    
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:Image ID="imgDevice" runat="server" style="width:100%;" />
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="gvDevice"/>
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
            </div>
        </div>




        
        <h2 style="font-weight:bold">設備借用情形查詢</h2>
        <asp:Panel ID="pAreaSearch" runat="server" DefaultButton="btnSearch">
            <asp:TextBox ID="tbDnameKeyword" runat="server" Placeholder="教室關鍵字(可為空)" CssClass="myinput_box"></asp:TextBox>
            <asp:Button ID="btnSearch" runat="server" Text="搜尋" OnClick="btnSearch_Click" CssClass="btn btn-primary"/>
        </asp:Panel>
        <div style="height:5px"><br /></div>
        
        <div style="display:flex;flex-direction: row;">
            <p style="margin-top:7.5px;">開始時間</p>
            <asp:TextBox ID="tbSearchStartDate" runat="server" Placeholder="開始日期" type="Date"  CssClass="myinput_box" ></asp:TextBox>
            <p style="margin-top:7.5px;margin-left:5px;">結束時間</p>
            <asp:TextBox ID="tbSearchEndDate" runat="server" Placeholder="結束日期" type="Date"  CssClass="myinput_box" ></asp:TextBox>
        </div>

        <hr />

        <asp:GridView ID="gvDevice" runat="server" AutoGenerateColumns="false" EmptyDataText="查無資料!" CssClass="gridview">
            <Columns>
                <asp:BoundField DataField="did" HeaderText="設備編號" />
                <asp:BoundField DataField="dname" HeaderText="設備名稱" />
                <asp:TemplateField HeaderText="" HeaderStyle-Width="70px">
                    <ItemTemplate>
                        <div style="width:100%;display:flex;justify-content:center;">
                            <asp:Button ID="btnImage" runat="server" Text="照片" OnClick="btnImage_Click" class="btn btn-outline-secondary" data-bs-toggle="modal" data-bs-target="#imageModal"/>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="變更可借用時段" HeaderStyle-Width="135px">
                    <ItemTemplate>
                        <div style="width:100%;display:flex;justify-content:center;">
                             <asp:Button ID="ChangeAvaliable" runat="server" Text="變更" OnClick="ChangeAvaliable_Click"  class="btn btn-secondary" data-bs-toggle="modal" data-bs-target="#exampleModal2"/>
                        </div>
                       </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="查詢借用狀況" HeaderStyle-Width="120px">
                    <ItemTemplate>
                        <div style="width:100%;display:flex;justify-content:center;">
                            <asp:Button ID="SearchAvaliable" runat="server" Text="查詢" OnClick="btnSearchAvaliable"  class="btn btn-success" data-bs-toggle="modal" data-bs-target="#exampleModal" />
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>


    </div>
</asp:Content>
