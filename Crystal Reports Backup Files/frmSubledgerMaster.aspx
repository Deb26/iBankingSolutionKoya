<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/MasterPages/ProjectBM.Master"CodeBehind ="frmSubledgerMaster.aspx.cs" Inherits="iBankingSolution.Master.frmSubledgerMaster" %>

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
                    <asp:Button ID="btnsubmit1" runat="server" Text="Save" class="btn btn-primary" OnClick="btnsubmit_Click" />
                   
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" class="btn btn-primary" OnClick="btnCancel_Click" />
                    <a href="frmSubledgerList.aspx" class="btn btn-default">Back to List</a>
                </div>
                <h1 class="page-header">Sub Ledger Master</h1>
            </div>
            <!-- /.col-lg-12 -->
        </div>
        <div class="row">
            <div class="col-lg-12">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        Sub Ledger Master Entry
                    </div>
                    <div class="panel-body">
                        <div class="row">
 
                            <div class="form-group">

                                <div class="col-md-12">
                                    <label style="margin-right: 20PX;">Sub Ledger Name</label>
                                    <asp:Label ID="lblDid" runat="server" BackColor="White" ForeColor="White"></asp:Label>
                                    <asp:TextBox ID="txt_SubLedgerName" runat="server" required="required" CssClass="form-control" />
                                </div>
                                <div class="col-md-12">
                                      <label style="margin-right: 20PX;"> Ledger Name</label>
                                    <asp:DropDownList ID="cmbx_Ledger" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                                <div class="col-md-6">
                                      <label style="margin-right: 20PX;">MemsID</label>
                                     <asp:TextBox ID="txt_MemsID" runat="server"  CssClass="form-control" />
                                </div>
                                 <div class="col-md-6">
                                      <label style="margin-right: 20PX;">GSTIN NO</label>
                                   <asp:TextBox ID="txt_GSTINNo" runat="server"  CssClass="form-control" />
                                </div>
                                <div class="col-md-12">
                                      <label style="margin-right: 20PX;">Address</label>
                                     <asp:TextBox ID="txt_Address" runat="server" CssClass="form-control" />
                                </div>
                                <div class="col-md-3">
                                      <label style="margin-right: 20PX;">Opening Balance</label>
                                  
                                </div>
                                 <div class="col-md-3">
                                      <label style="margin-right: 20PX;">Debit</label>
                                   <asp:TextBox ID="ntxt_Debit" runat="server" required="required" CssClass="form-control" />
                                </div>
                                 <div class="col-md-3">
                                      <label style="margin-right: 20PX;">Credit</label>
                                   <asp:TextBox ID="ntxt_Credit" runat="server" required="required" CssClass="form-control" />
                                </div>
 

                                <div class="clearfix"></div>
                                <hr />
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
                    <a href="#" class="btn btn-outline btn-danger">Cancel</a>

                </div>
            </div>
            <!-- /.col-lg-12 -->
        </div>

        <%-- Scripting Section --%>

 

    </form>
</asp:Content>