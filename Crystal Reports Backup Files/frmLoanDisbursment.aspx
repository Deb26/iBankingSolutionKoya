<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/ProjectBM.Master" CodeBehind="frmLoanDisbursment.aspx.cs" Inherits="iBankingSolution.Transaction.frmLoanDisbursment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        $ = jQuery.noConflict();
        $(function () {
            $('.select2').select2()
        })

        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;
            return true;
        }

    </script>



</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <form id="formInitialSetting" runat="server">
        <asp:ScriptManager ID="scrpt1" runat="server" EnablePageMethods="true"></asp:ScriptManager>
        <div class="row">
            <div class="col-lg-12">
                <div style="float: right; margin-top: 12px;">
                    <asp:Button ID="btnsubmit1" runat="server" Text="Save" class="btn btn-primary" OnClick="btnsubmit_Click" />

                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" class="btn btn-primary" OnClick="btnCancel_Click" />
                    <a href="frmLoanRepaymentList.aspx" class="btn btn-default">Back to List</a>
                </div>
                <h1 class="page-header">Loan Disbursment</h1>
            </div>
            <!-- /.col-lg-12 -->
        </div>
        <div class="row">
            <div class="col-lg-12">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        Disbursment
                    </div>
                    <div class="panel-heading" runat="server" id="DivID" visible="false">
                        <asp:Label ID="Label1" runat="server" Visible="False" align="center" Font-Bold="True" Font-Italic="False" Font-Names="Verdana" ForeColor="White"></asp:Label>
                    </div>
                    <div class="panel-body">
                        <div class="row">

                            <div class="form-group">

                                <div class="col-md-6">
                                    <label style="margin-right: 10PX;">A/c Head.</label>
                                    <asp:Label ID="lblDid" runat="server" BackColor="White" ForeColor="White"></asp:Label>

                                    <asp:TextBox ID="txt_AcctHead" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                    <label style="margin-right: 20PX;">Loan A/c No</label>
                                    <asp:DropDownList ID="cmbx_AcctNo" runat="server" CssClass="form-control" EmptyMessage="Select A/c No" AutoPostBack="true"
                                        OnSelectedIndexChanged="cmbx_AcctNo_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>

                                <div class="clearfix"></div>
                                <div class="panel-heading" font-family: Arial,Helvetica,sans-serif !important;">
                                     Sanction Details 
                                    </div>
                                <div class="col-md-3">
                                    
                                    <label style="margin-right: 20PX;">Total</label>
                                    <asp:TextBox ID="ntxt_SancTotal" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>

                                <div class="col-md-3">
                                    <label style="margin-right: 20PX;">Cash</label>
                                    <asp:TextBox ID="ntxt_SancCash" runat="server" CssClass="form-control"></asp:TextBox>

                                </div>

                                <div class="col-md-3">
                                    <label style="margin-right: 20PX;">Kind</label>
                                    <asp:TextBox ID="ntxt_SancKind" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-md-3">
                                    <label style="margin-right: 20PX;">Insurance</label>
                                    <asp:TextBox ID="ntxt_SancInsurance" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>

                                <div class="clearfix"></div>
                              
                                 <div class="panel-heading" font-family: Arial,Helvetica,sans-serif !important;">
                                     Previous Disbursment Details
                                     </div>

                                <div class="col-md-3">
                                      
                                    <label style="margin-right: 20PX;">Total</label>
                                    <asp:TextBox ID="ntxt_PreDisbTotal" CssClass="form-control" runat="server" onFocus="this.select()"></asp:TextBox>
                                </div>
                                <div class="col-md-3">
                                    <label style="margin-right: 20PX;">Cash</label>
                                    <asp:TextBox ID="ntxt_PreDisbCash" CssClass="form-control" runat="server" onFocus="this.select()"></asp:TextBox>
                                </div>
                                <div class="col-md-3">
                                    <label style="margin-right: 20PX;">Kind</label>
                                    <asp:TextBox ID="txt_PreDisbKind" runat="server" onFocus="this.select()" CssClass="form-control"></asp:TextBox>
                                </div>

                                <div class="col-md-3">
                                    <label style="margin-right: 20PX;">Insurance</label>
                                    <asp:TextBox ID="txt_PreDisbInsurance" runat="server" onFocus="this.select()" CssClass="form-control"></asp:TextBox>
                                </div>


                                <div class="clearfix"></div>
                                
                                <div class="panel-heading" font-family: Arial,Helvetica,sans-serif !important;">
                                    Crop Insurance Premium Ledger 
                                     </div>

                                <div class="col-md-3">
                                  
                                    <asp:TextBox ID="RadTextBox9" runat="server" onFocus="this.select()" CssClass="form-control" autocomplete="off"></asp:TextBox>
                                </div>
                                <div class="col-md-9">

                                    <asp:TextBox ID="RadTextBox10" runat="server" onFocus="this.select()" CssClass="form-control" autocomplete="off"></asp:TextBox>

                                </div>


                                <div class="clearfix"></div>
                                <div class="form-group">

                                    <div class="col-md-4">
                                        <asp:RadioButtonList ID="rdobtn_Type" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" onchange="return rdobtn_Type_ClientSelectedIndexChanged();" AutoPostBack="True" OnSelectedIndexChanged="rdobtn_Type_SelectedIndexChanged">
                                            <asp:ListItem Text="Cash Withdraw" Value="Cash" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="A/c Transfer" Value="A/c Trans"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>

                                    <div class="col-md-4">
                                        <label style="margin-right: 20PX;">&nbsp;&nbsp;</label>
                                        <asp:TextBox ID="RadTextBox11" runat="server" onFocus="this.select()" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-md-4">
                                        <label style="margin-right: 20PX;">&nbsp;&nbsp;</label>
                                        <asp:TextBox ID="RadTextBox12" runat="server" CssClass="form-control" />

                                    </div>
                                     <div class="form-group" runat="server" id="fs_CashWithdrawl">
                                    <div class="col-md-4">
                                        <label style="margin-right: 20PX;">Date</label>
                                        <asp:TextBox ID="dtpkr_DisbDate" runat="server" CssClass="form-control input-sm BootDatepicker " />
                                    </div>
                                    <div class="col-md-4">
                                        <label style="margin-right: 20PX;">Voucher No</label>
                                        <asp:TextBox ID="txt_VoucherNo" runat="server" CssClass="form-control" />
                                    </div>
                                    <div class="col-md-4">
                                        <label style="margin-right: 20PX;">Cash Book</label>
                                        <asp:TextBox ID="txt_CashBook" runat="server" CssClass="form-control" />
                                    </div>
                                    </div>

                                    <div class="clearfix"></div>

                                    <div class="form-group" runat="server" visible="false" id="fs_AcTransfer">
                                        <div class="col-md-12">
                                            <asp:CheckBox ID="CheckBox1" runat="server" Text="Disburse Kind Component As Cash To Savings A/c" />
                                        </div>
                                        <div class="col-md-4">
                                            <label style="margin-right: 20PX;">Journal Date</label>
                                            <asp:TextBox ID="dtpkr_JournalDate" runat="server" CssClass="form-control input-sm BootDatepicker " />
                                        </div>

                                        <div class="col-md-4">
                                            <label style="margin-right: 20PX;">Voucher No</label>
                                            <asp:TextBox ID="RadTextBox13" runat="server" CssClass="form-control" />
                                        </div>

                                        <div class="col-md-4">
                                            <label style="margin-right: 20PX;">Savings A/c No</label>
                                            <asp:DropDownList ID="cmbx_TransferAcNo" runat="server" CssClass="form-control" EmptyMessage="Select A/c No" />
                                        </div>


                                    </div>
                                    <%----hidden div--%>
                                    <div class="form-group">

                                        <div class="col-md-3">
                                            <label style="margin-right: 20PX;">Net Disb</label>
                                            <asp:TextBox ID="ntxt_NetDisb" runat="server" CssClass="form-control" />
                                        </div>

                                        <div class="col-md-3">
                                            <label style="margin-right: 20PX;">Cash</label>
                                            <asp:TextBox ID="ntxt_NetCash" runat="server" CssClass="form-control" />
                                        </div>

                                        <div class="col-md-3">
                                            <label style="margin-right: 20PX;">Kind</label>
                                            <asp:TextBox ID="txt_NetKind" runat="server" CssClass="form-control" />
                                        </div>
                                        <div class="col-md-3">
                                            <label style="margin-right: 20PX;">Insurance</label>
                                            <asp:TextBox ID="txt_NetInsurance" runat="server" CssClass="form-control" />
                                        </div>
                                    </div>
                                    <div class="clearfix"></div>
                                    <div class="form-group">
                                        <div class="col-md-12">
                                            <asp:CheckBox ID="chkbx_NewDisburse" runat="server" Text="Click Here For New Disbursement" />
                                        </div>
                                        <div class="col-md-3">
                                            <asp:Label Text="Due Repay Date" runat="server"></asp:Label>
                                            <asp:TextBox ID="dtpkr_NewRepayDate" runat="server" CssClass="form-control input-sm BootDatepicker"></asp:TextBox>

                                        </div>
                                         <div class="col-md-3">
                                            <asp:Label Text="ROI" runat="server"></asp:Label>
                                            <asp:TextBox ID="ntxt_NewROI" runat="server" CssClass="form-control input-sm BootDatepicker"></asp:TextBox>

                                        </div>

                                         <div class="col-md-3">
                                            <asp:Label Text="Sanction Amt" runat="server"></asp:Label>
                                            <asp:TextBox ID="ntxt_NewSancAmt" runat="server" CssClass="form-control"></asp:TextBox>

                                        </div>

                                        <div class="col-md-3">
                                            <asp:Label Text="OD ROI" runat="server"></asp:Label>
                                            <asp:TextBox ID="ntxt_NewODROI" runat="server" CssClass="form-control"></asp:TextBox>

                                        </div>



                                    </div>


                                </div>
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
