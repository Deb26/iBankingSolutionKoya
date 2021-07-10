<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/ProjectBM.Master" CodeBehind="frmOpeningBalanceUp.aspx.cs" Inherits="iBankingSolution.Master.frmOpeningBalanceUp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        $ = jQuery.noConflict();
        $(function () {
            $('.select2').select2()
        })
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
        <asp:ScriptManager ID="scrpt1" runat="server"></asp:ScriptManager>
        <div class="row">
            <div class="col-lg-12">
                <div style="float: right; margin-top: 12px;">
                    <asp:Button ID="btnsubmit1" runat="server" Text="Add" class="btn btn-primary" OnClick="btnsubmit_Click" />
                    <asp:Button ID="btnUpdate" runat="server" Text="Update" class="btn btn-primary" OnClick="btnUpdate_Click" />
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" class="btn btn-primary" OnClick="btnCancel_Click" />
                    <a href="frmDepositMasterList.aspx" class="btn btn-default">Back to List</a>
                </div>
                <h1 class="page-header">Opening Balance Update</h1>
            </div>
            <!-- /.col-lg-12 -->
        </div>
        <div class="row">
            <div class="col-lg-12">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        Entry
                    </div>
                    <div class="panel-body">
                        <div class="row">

                            <div class="form-group">

                                <div class="col-md-6">
                                    <label style="margin-right: 20PX;">Old Ac Number</label>
                                    <asp:TextBox ID="txtAcNo" runat="server" placeholder="Enter Old Ac number" CssClass="form-control" />
                                </div>
                                <div class="col-md-6">
                                    <label style="margin-right: 20PX;">New L/P Number</label>
                                    <asp:TextBox ID="txtLPNo" runat="server" placeholder="Enter New L/P Number" CssClass="form-control" />
                                </div>

                                <div class="col-md-4">
                                    <label style="margin-right: 20PX;">AC Number</label>
                                    <asp:TextBox ID="TextBox2" runat="server" placeholder=" Enter Ac number" CssClass="form-control" />
                                </div>

                                <div class="col-md-4">
                                    <label style="margin-right: 20PX;">Credit</label>
                                    <asp:TextBox ID="txtCreditamt" runat="server" placeholder=" Enter Credit Amount" CssClass="form-control" />
                                </div>

                                 <div class="col-md-4">
                                    <label style="margin-right: 20PX;">Debit</label>
                                    <asp:TextBox ID="txtdebitamt" runat="server" placeholder=" Enter Debit Amount" CssClass="form-control" />
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


 
</asp:Content>

