<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="AllClassroom.aspx.cs" Inherits="DS_LAB2.AllClassroom" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div>
            
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
                        <asp:GridView ID="gvBorrow_status" runat="server" CssClass="gridview" ></asp:GridView>
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
            
        
        <div class="modal fade" id="imageModal" tabindex="-1" aria-labelledby="imageModal" aria-hidden="true">
            <div class="modal-dialog modal-lg" >
            <div class="modal-content" >
                <div class="modal-body" style="padding:0;">
                    
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:Image ID="imgClassroom" runat="server" style="width:100%;" />
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="gvClassroom"/>
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
            </div>
        </div>



        <h2 style="font-weight:bold;">所有教室一覽</h2>
        <asp:Panel ID="pAreaSearch" runat="server" DefaultButton="btnSearch">
            根據條件篩選
            <asp:TextBox ID="tbCnameKeyword" runat="server" Placeholder="教室關鍵字(可為空)" CssClass="myinput_box"></asp:TextBox>
            <asp:Button ID="btnSearch" runat="server" Text="搜尋" OnClick="btnSearch_Click" CssClass="btn btn-primary"/>
        </asp:Panel>

        <hr />

        <asp:GridView ID="gvClassroom" runat="server" AutoGenerateColumns="false" EmptyDataText="查無資料!" CssClass="gridview">
            <Columns>
                <asp:BoundField DataField="cid" HeaderText="教室編號" />
                <asp:BoundField DataField="cname" HeaderText="教室名稱" />
                <asp:BoundField DataField="location" HeaderText="教室位置" />
                <asp:BoundField DataField="capacity" HeaderText="可容納人數" />
                <asp:BoundField DataField="dept_name" HeaderText="負責管理的系辦" />
                <asp:TemplateField HeaderText="" HeaderStyle-Width="70px">
                    <ItemTemplate>
                        <div style="width:100%;display:flex;justify-content:center;">
                            <asp:Button ID="btnImage" runat="server" Text="照片" OnClick="btnImage_Click" class="btn btn-outline-secondary" data-bs-toggle="modal" data-bs-target="#imageModal"/>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="近7日可借用時間" HeaderStyle-Width="140px">
                    <ItemTemplate>
                        <div style="width:100%;display:flex;justify-content:center;">
                            <asp:Button ID="SearchAvaliable" runat="server" Text="查詢" OnClick="btnSearchAvaliable" class="btn btn-success" data-bs-toggle="modal" data-bs-target="#exampleModal"/>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        
    </div>
</asp:Content>
