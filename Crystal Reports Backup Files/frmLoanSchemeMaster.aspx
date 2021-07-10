<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/ProjectBM.Master" CodeBehind="frmLoanSchemeMaster.aspx.cs" Inherits="iBankingSolution.Master.frmLoanSchemeMaster" %>

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
                    <a href="frmLoanSchemeMasterList.aspx" class="btn btn-default">Back to List</a>
                </div>
                <h1 class="page-header">Loan Scheme Master</h1>
            </div>
            <!-- /.col-lg-12 -->
        </div>
        <div class="row">
            <div class="col-lg-12">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        Loan Scheme Master Entry
                    </div>
                    <div class="panel-body">
                        <div class="row">

                            <div class="form-group">

                                <div class="col-md-6">
                                    <label style="margin-right: 20PX;">Scheme Name</label>
                                    <asp:TextBox ID="txt_SchemeName" runat="server" required="required" CssClass="form-control" />
                                    <asp:Label ID="lblDid" runat="server" BackColor="White" ForeColor="White"></asp:Label>
                                </div>
                                <div class="col-md-6">
                                    <label style="margin-right: 20PX;">Loan Type</label>
                                    <asp:DropDownList ID="cmbx_LoanType" runat="server" CssClass="form-control">
                                         <asp:ListItem Value="0">--Select Type--</asp:ListItem>
                                        <asp:ListItem Value="Farm">Farm</asp:ListItem>
                                        <asp:ListItem Value="General">General</asp:ListItem>
                                        <asp:ListItem Value="Shg">Shg</asp:ListItem>

                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-12">
                                    <label style="margin-right: 20PX;">Adjustment Ledger</label>
                                    <asp:DropDownList ID="cmbx_AdjustmentLedger" runat="server" CssClass="form-control">

                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-4">
                                    <label style="margin-right: 20PX;">NPA Aplicable</label>
                                    <asp:DropDownList ID="cmbx_NPAAplicable" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="0">--Select Type--</asp:ListItem>
                                        <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                        <asp:ListItem Value="No">No</asp:ListItem>
                                        

                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-4">
                                    <label style="margin-right: 20PX;">OD Int. Aplicable</label>
                                     <asp:DropDownList ID="cmbx_ODIntAplicable" runat="server" CssClass="form-control">

                                         <asp:ListItem Value="0">--Select Type--</asp:ListItem>
                                        <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                        <asp:ListItem Value="No">No</asp:ListItem>
                                     </asp:DropDownList>
                                </div>
                                <div class="col-md-4">
                                    <label style="margin-right: 20PX;">Sanction By</label>
                                   <%-- <asp:TextBox ID="txtIntcalBasedOn" CssClass="form-control input-sm BootDatepicker" runat="server" onFocus="this.select()" autocomplete="off" required="required"></asp:TextBox>--%>
                                     <asp:DropDownList ID="cmbx_SanctionBy" runat="server" CssClass="form-control">

                                         <asp:ListItem Value="0">--Select Type--</asp:ListItem>
                                        <asp:ListItem Value="Board">Board</asp:ListItem>
                                        <asp:ListItem Value="Manager">Manager</asp:ListItem>
                                     </asp:DropDownList>
                                </div>




                                <div class="clearfix"></div>

                            </div>
                            <div class="form-group">
                                <div class="col-md-12">
                                    <label style="margin-right: 20PX;">Int. Received Ldg.</label>
                                    <asp:DropDownList ID="cmbx_IntReceivedLdg" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                                <div class="col-md-12">
                                    <label style="margin-right: 20PX;">ODInt. Ldg.</label>
                                    <asp:DropDownList ID="cmbx_ODIntLdg" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                                <div class="col-md-12">
                                    <label style="margin-right: 20PX;">ODInt Received Ldg</label>
                                     <asp:DropDownList ID="cmbx_ODIntReceivedLdg" runat="server" CssClass="form-control"></asp:DropDownList>
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
