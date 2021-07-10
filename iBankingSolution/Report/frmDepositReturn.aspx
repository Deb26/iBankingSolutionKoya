<%@ Page Language="C#" MasterPageFile="~/MasterPages/ProjectBM.Master" AutoEventWireup="true" CodeBehind="frmDepositReturn.aspx.cs" Inherits="iBankingSolution.Report.frmDepositReturn" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
     <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.4.1/js/bootstrap-datepicker.min.js"></script>
         <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.4.1/css/bootstrap-datepicker3.css"/>
    <script type="text/javascript">
        $ = jQuery.noConflict();
        $(function () {
            $('.select2').select2()
        })
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server"><br />
    <asp:ScriptManager ID="scrpt1" runat="server" EnablePageMethods="true"></asp:ScriptManager>
    <div class="row">
        <div class="col-lg-12">
            <div class="panel panel-primary">
                <div class="panel-heading" style="font-size:larger;">
                    Deposit Return 
                </div>
                <div class="panel-body">
                    <div class="row">

                        <div class="form-group">

                            <div class="clearfix"></div>
                            <div class="col-md-2">
                                <label style="margin-right: 20PX;font-size:larger;">Select Report Type</label>

                                <asp:DropDownList ID="cmbx_ReportType" runat="server" CssClass="form-control" Font-Size="10" AutoPostBack="True" > <%--OnSelectedIndexChanged="cmbx_ReportType_SelectedIndexChanged"--%>
                                    <asp:ListItem Selected="True" Value="0">-- Select --</asp:ListItem>
                                    <asp:ListItem Value="BS">Balance Sheet</asp:ListItem>
                                    <asp:ListItem Value="PL">Profit &amp; Losss</asp:ListItem>
                                    <asp:ListItem Value="TR">Trading Account</asp:ListItem>


                                </asp:DropDownList>

                            </div>

                            <br />
                         

                            <div class="col-md-8">
                                <label style="margin-right: 20PX;"></label>
                                <%--<asp:Button ID="btnAddNew" runat="server" Text="Add New Record" class="btn btn-primary" OnClick="btnAddNew_Click" />--%>
                                <asp:Button ID="btnSave" runat="server" Text="Save Changes" class="btn btn-primary"  /> <%--OnClick="btnSave_Click"--%>
                                <%--<asp:Button ID="btnCancel" runat="server" Text="Cancel Changes" class="btn btn-primary" OnClick="btnCancel_Click" />--%>

                            </div>
                            <div class="clearfix"></div>

                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>
    <%--For Grid--%>
    <div class="row">
        <div class="col-lg-12">
            <div class="panel panel-primary">
                <div class="panel-heading" style="font-size:larger;">
                    <asp:Label ID="Label2" runat="server" Text="Deposit Return Setting"></asp:Label>
                </div>
                <div class="panel-body">
                    <div style ="height:350px; width:1250px; overflow:auto;">
                    <asp:GridView runat="server" ID="gvDetails" ShowFooter="false" AllowPaging="true" PageSize="50" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" OnRowDeleting="gvDetails_RowDeleting"
                        CssClass="table table-bordered table-striped table-hover" GridLines="Both" EmptyDataText="No Data Found">
                        <HeaderStyle CssClass="headerstyle" />
                        <Columns>
                            <asp:TemplateField HeaderText="SL" HeaderStyle-HorizontalAlign="Left">
                               <ItemTemplate>
                               <asp:Label ID="rowid" Text='<%# Bind("rowid") %>' runat="server"></asp:Label>
                               </ItemTemplate>
                               <HeaderStyle HorizontalAlign="Left" Width="20px"/>
                              </asp:TemplateField>
                           <%-- <asp:BoundField DataField="rowid" HeaderText="Sl" HeaderStyle-Width="20px" runat="server">
                                    <HeaderStyle Width="20px"></HeaderStyle>
                                </asp:BoundField>--%>
                        <%--<asp:BoundField DataField="rowid" HeaderText="Row Id" ReadOnly="true" Text='<%# Bind("rowid") %>'/>--%> <%--autocomplete="off" ForeColor="Black" Text='<%# Eval("nomn_name") %>' onFocus="this.select()"--%>
                       <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Type Of Deposit">
                           <ItemTemplate>
                           <asp:TextBox ID="txtName" CssClass="form-control input-sm" runat="server" Font-Size="10" placeholder="Type Of Deposit" Text='<%# Eval("productname") %>' onFocus="this.select()"
                             ></asp:TextBox>  
                          </ItemTemplate>
                          <ItemStyle HorizontalAlign="Left" />
                          <HeaderStyle HorizontalAlign="Left" />
                       </asp:TemplateField>
                        
                       <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Opening Balance">
                       <ItemTemplate>
                       <asp:TextBox ID="txtPrice" CssClass="form-control input-sm" runat="server" Font-Size="10" placeholder="Opening Balance"
                        ></asp:TextBox>  
                       </ItemTemplate>
                       <ItemStyle HorizontalAlign="Left" />
                       <HeaderStyle HorizontalAlign="Left" />
                       </asp:TemplateField>

                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Total Deposit For The Month">
                           <ItemTemplate>
                           <asp:TextBox ID="txtTotalDepositMonth" CssClass="form-control input-sm" runat="server" Font-Size="10" placeholder="Total Deposit For The Month"
                             ></asp:TextBox> 
                          </ItemTemplate>
                          <ItemStyle HorizontalAlign="Left" />
                          <HeaderStyle HorizontalAlign="Left" />
                       </asp:TemplateField>

                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Total Deposit">
                           <ItemTemplate>
                           <asp:TextBox ID="txtTotalDeposit" CssClass="form-control input-sm" runat="server" Font-Size="10" placeholder="Total Deposit"
                             ></asp:TextBox> 
                          </ItemTemplate>
                          <ItemStyle HorizontalAlign="Left" />
                          <HeaderStyle HorizontalAlign="Left" />
                       </asp:TemplateField>   


                       <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Total Withdrawl For The Month">
                           <ItemTemplate>
                           <asp:TextBox ID="txtWithdrawl" CssClass="form-control input-sm" runat="server" Font-Size="10" placeholder="Total Withdrawl For The Month"
                             ></asp:TextBox> 
                          </ItemTemplate>
                          <ItemStyle HorizontalAlign="Left" />
                          <HeaderStyle HorizontalAlign="Left" />
                       </asp:TemplateField>


                       <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Closing Balance">
                           <ItemTemplate>
                           <asp:TextBox ID="txtClosingBal" CssClass="form-control input-sm" runat="server" Font-Size="10" placeholder="Closing Balance"
                             ></asp:TextBox> 
                          </ItemTemplate>
                          <ItemStyle HorizontalAlign="Left" />
                          <HeaderStyle HorizontalAlign="Left" />
                       </asp:TemplateField>

                      <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="No. Of Accounts">
                           <ItemTemplate>
                           <asp:TextBox ID="txtAccounts" CssClass="form-control input-sm" runat="server" Font-Size="10" placeholder="No. Of Accounts"
                             ></asp:TextBox> 
                          </ItemTemplate>
                          <%--<FooterTemplate>
                            <asp:Button ID="btnAdd" runat="server" class="btn btn-success"  Text="+ Add" OnClick="btnAdd_Click" />
                          </FooterTemplate>--%>
                          <ItemStyle HorizontalAlign="Left" />
                          <HeaderStyle HorizontalAlign="Left" />
                       </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                             <asp:Button ID="btnAdd" runat="server" Text="+ Add" class="btn btn-success" OnClick="btnAdd_Click" Visible='<%# Eval("rowid").ToString() == "1" ? true : false %>' />
                             <asp:Button ID="btnDelete" ShowDeleteButton="true" runat="server" Text=" - " Class="btn btn-danger" OnClick="btnDelete_Click" Visible='<%# Eval("rowid").ToString() == "1" ? false : true %>' />
                        </ItemTemplate>
                         <ItemStyle HorizontalAlign="Left" />
                         <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>

                        <%--<asp:CommandField ShowDeleteButton="true" ButtonType="Button" ControlStyle-CssClass="btn btn-danger" DeleteText="-" ControlStyle-Font-Size="10px" ControlStyle-Font-Bold="true"/>--%>
                        </Columns>
                             
                            <FooterStyle BackColor="White" />
                            <HeaderStyle BackColor="White" Font-Bold="True" ForeColor="Black" />
                            <PagerStyle BackColor="White" ForeColor="White" HorizontalAlign="Left" />
                            <RowStyle BackColor="White" />
                            <SelectedRowStyle BackColor="White" Font-Bold="True" ForeColor="White" />
                            <SortedAscendingCellStyle BackColor="#F1F1F1" />
                            <SortedAscendingHeaderStyle BackColor="#808080" />
                            <SortedDescendingCellStyle BackColor="#CAC9C9" />
                            <SortedDescendingHeaderStyle BackColor="#383838" />
                             
                        </asp:GridView>


                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
