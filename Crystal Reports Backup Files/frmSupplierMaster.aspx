<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/ProjectBM.Master" CodeBehind="frmSupplierMaster.aspx.cs" Inherits="iBankingSolution.Master.frmSupplierMaster" %>

 <asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        $ = jQuery.noConflict();
        $(function () {
            $('.select2').select2()
        })
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <form id="formInitialSetting" runat="server">
        <asp:ScriptManager ID="scrpt1" runat="server"></asp:ScriptManager>
        <div class="row">
            <div class="col-lg-12">
                <div style="float: right; margin-top: 12px;">
                    <asp:Button ID="btnsubmit1" runat="server" Text="Add" class="btn btn-primary" OnClick="btnsubmit_Click" />
                    <asp:Button ID="btnUpdate" runat="server" Text="Update" class="btn btn-primary" OnClick="btnUpdate_Click" />
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" class="btn btn-primary" OnClick="btnCancel_Click" />
                    <a href="frmDepositMasterList.aspx" class="btn btn-default">Back to List</a>
                </div>
                <h1 class="page-header">Supplier Master</h1>
            </div>
            <!-- /.col-lg-12 -->
        </div>
        <div class="row">
            <div class="col-lg-12">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        Entry
                    </div>
                    <div class="panel-body">
                        <div class="row">

                            <div class="form-group">

                                <div class="col-md-12">
                                    <label style="margin-right: 20PX;">Supplier Name</label>
                                 <asp:TextBox ID="txtsupplierName" runat="server" placeholder="Enter Supplier Name"   CssClass="form-control" />
                                </div>
                                <div class="col-md-12">
                                    <label style="margin-right: 20PX;">Ledger</label>
                                   <asp:DropDownList ID="ddlLedger" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                                <div class="col-md-12">
                                    <label style="margin-right: 20PX;">Address</label>
                                  <asp:TextBox ID="txtAddress" runat="server"  CssClass="form-control" TextMode="MultiLine"/>
                                </div>
                                <div class="col-md-4">
                                    <label style="margin-right: 20PX;">Opening Balance</label>
                                    <asp:TextBox ID="txtOpeningBalance" runat="server"  CssClass="form-control" />
                                </div>
                                <div class="col-md-4">
                                    <label style="margin-right: 20PX;">Debit</label>
                                    <asp:RadioButton ID="rbDebit" runat="server" CssClass="form-control"/>
                                </div>
                                <div class="col-md-4">
                                    <label style="margin-right: 20PX;">Credit</label>
                                  
                                      <asp:RadioButton ID="rbCredit" runat="server" CssClass="form-control"/>
                                </div>
                              



                                <div class="clearfix"></div>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-lg-12">
                <div class="panel panel-default">
                    <div class="panel-heading">
                       Supplier Master Details
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="form-group">
                                <div class="col-md-12">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <asp:GridView ID="GVSupplier" runat="server" AutoGenerateColumns="False"
                                                CssClass="table table-bordered table-striped table-hover" EmptyDataText="No Data Found" GridLines="Both" OnRowDeleting="GVSupplier_RowDeleting" OnRowEditing="GVSupplier_RowEditing" OnRowUpdating="GVSupplier_RowUpdating">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="SL." HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex + 1 %>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>

                                                      <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Item code">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtsupplierCode" CssClass="form-control input-sm" runat="server"
                                                                autocomplete="off" ForeColor="Black" Text='<%# Eval("supplierCode") %>' onFocus="this.select()"></asp:TextBox>

                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Supplier Name">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtUserName" CssClass="form-control input-sm" runat="server"
                                                                autocomplete="off" ForeColor="Black" Text='<%# Eval("supplierName") %>' onFocus="this.select()"></asp:TextBox>

                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="50px" HeaderText="ACTION">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="itbnEdit" runat="server" CausesValidation="false" Height="18"
                                                                ImageUrl="~/Content/images/edit.png" Width="18" OnClick="itbnEdit_Click" />
                                                            <asp:ImageButton ID="itbndelete" runat="server" CausesValidation="false"
                                                                CommandName="Delete" Height="18" ImageUrl="~/Content/images/delete.png" Width="18" OnClick="itbndelete_Click" />
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" Width="50px" />
                                                    </asp:TemplateField>


                                                </Columns>
                                            </asp:GridView>
                                        </ContentTemplate>
                                        <%--  <Triggers>
                                            <asp:PostBackTrigger ControlID="btnsubmit" />
                                        </Triggers>--%>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="clearfix"></div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-lg-12">
                <div style="float: right; margin-top: 12px;">
                    <asp:Button ID="btnsubmit" runat="server" Text="Save" class="btn btn-primary" OnClick="btnsubmit_Click" />
                    <asp:Button ID="btnUpdate1" runat="server" Text="Update" class="btn btn-primary" OnClick="btnUpdate_Click" />
                    <a href="#" class="btn btn-outline btn-danger">Cancel</a>

                </div>
            </div>
            <!-- /.col-lg-12 -->
        </div>

        <%-- Scripting Section --%>
        <script type="text/javascript">
            $(function () {
                $('.BootDatepicker').datepicker({
                    changeMonth: true,
                    changeYear: true,
                    format: "dd/mm/yyyy",
                    language: "tr",
                    autoclose: true,
                    todayHighlight: true
                });
            });
        </script>


    </form>
</asp:Content>