<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/ProjectBM.Master" CodeBehind="frmLoanRepayment.aspx.cs" Inherits="iBankingSolution.Transaction.frmLoanRepayment" %>

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
                <h1 class="page-header">Loan Repayment</h1>
            </div>
            <!-- /.col-lg-12 -->
        </div>
        <div class="row">
            <div class="col-lg-12">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        Loan Repayment
                    </div>
                    <div class="panel-heading" runat="server" id="DivID" visible="false">
                        <asp:Label ID="Label1" runat="server" Visible="False" align="center" Font-Bold="True" Font-Italic="False" Font-Names="Verdana" ForeColor="White"></asp:Label>
                    </div>
                    <div class="panel-body">
                        <div class="row">

                            <div class="form-group">

                                <div class="col-md-6">
                                    <label style="margin-right: 10PX;">Loan A/c No.</label>
                                    <asp:Label ID="lblDid" runat="server" BackColor="White" ForeColor="White"></asp:Label>

                                    <asp:DropDownList ID="cmbx_AccountNo" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="cmbx_AccountNo_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-6">
                                    <label style="margin-right: 20PX;">Collection Date</label>
                                    <asp:TextBox ID="dtpkr_CollectionDate" CssClass="form-control input-sm BootDatepicker" runat="server" onFocus="this.select()" autocomplete="off" required="required" AutoPostBack="True" OnTextChanged="dtpkr_CollectionDate_TextChanged"></asp:TextBox>
                                </div>
                                
                                <div class="clearfix"></div>

                                 <div class="col-md-4">
                                    <label style="margin-right: 20PX;">Received Type</label>
                                    <asp:RadioButtonList ID="rdobtn_ReceivedType" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rdobtn_ReceivedType_SelectedIndexChanged">
                                        <asp:ListItem Text="Cash Received" Value="Cash" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="A/c Adjustment" Value="Adjustment"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                               
                                <div class="col-md-12">
                                     <asp:CheckBox ID="chkbx_IsBank" runat="server" Text="Bank" Enabled="false" />
                                    <asp:DropDownList ID="cmbx_BankLedger" runat="server" CssClass="form-control" placeholder="Select Ledger" AutoPostBack="True">
                                    </asp:DropDownList>
                                </div>

                                <div class="col-md-4">
                                    <label style="margin-right: 20PX;">Total Collection</label>
                                    <asp:TextBox ID="ntxt_TotalCollection" CssClass="form-control" runat="server" onFocus="this.select()" autocomplete="off" AutoPostBack="True" OnTextChanged="ntxt_TotalCollection_TextChanged"></asp:TextBox>
                                </div>
                                <div class="col-md-4">
                                    <label style="margin-right: 20PX;">Actual Balance</label>
                                    <asp:TextBox ID="ntxt_ActualBalance" CssClass="form-control" runat="server" onFocus="this.select()" autocomplete="off"></asp:TextBox>
                                </div>
                                <div class="col-md-4">
                                    <label style="margin-right: 20PX;">Cash Book</label>
                                    <asp:TextBox ID="txt_CashBook" CssClass="form-control" runat="server" onFocus="this.select()" autocomplete="off"></asp:TextBox>
                                </div>
                                <div class="clearfix"></div>

                                <div class="col-md-4">
                                    <label style="margin-right: 20PX;">Principal Outstanding</label>
                                    <asp:TextBox ID="ntxt_DemandPrincipalOutstanding" CssClass="form-control" runat="server" onFocus="this.select()" autocomplete="off"></asp:TextBox>
                                </div>
                                <div class="col-md-4">
                                    <label style="margin-right: 20PX;">Principal Overdue</label>
                                    <asp:TextBox ID="ntxt_DemandPrincipalOverdue" CssClass="form-control" runat="server" onFocus="this.select()" autocomplete="off"></asp:TextBox>
                                </div>
                                <div class="col-md-4">
                                    <label style="margin-right: 20PX;">Principal Current</label>
                                    <asp:TextBox ID="ntxt_DemandPrincipalCurrent" runat="server" onFocus="this.select()" CssClass="form-control" autocomplete="off"></asp:TextBox>
                                </div>


                                <div class="clearfix"></div>
                                <div class="col-md-4">
                                    <label style="margin-right: 20PX;">Due Interest</label>
                                    <asp:TextBox ID="ntxt_DemandDueInterest" runat="server" onFocus="this.select()" CssClass="form-control" autocomplete="off"></asp:TextBox>
                                </div>
                                <div class="col-md-4">
                                    <label style="margin-right: 20PX;">Overdue Interest</label>
                                    <asp:TextBox ID="ntxt_DemandOverdueInterest" runat="server" onFocus="this.select()" CssClass="form-control" autocomplete="off"></asp:TextBox>

                                </div>
                                <div class="col-md-4">
                                    <label style="margin-right: 20PX;">Current Interest</label>
                                    <asp:TextBox ID="ntxt_DemandCurrentInterest" runat="server" onFocus="this.select()" CssClass="form-control" autocomplete="off" AutoPostBack="True"></asp:TextBox>

                                </div>

                                <div class="clearfix"></div>
                                <div class="form-group">
                                    
                            <div class="col-md-4">
                                <label style="margin-right: 20PX;">Principal Outstanding</label>
                                <asp:TextBox ID="ntxt_CollectionPrincipalOutstanding" runat="server" onFocus="this.select()" CssClass="form-control" autocomplete="off" AutoPostBack="True"></asp:TextBox>
                            </div>

                                    <div class="col-md-4">
                                        <label style="margin-right: 20PX;">Principal Overdue</label>
                                        <asp:TextBox ID="ntxt_CollectionPrincipalOverdue" runat="server" onFocus="this.select()" CssClass="form-control" autocomplete="off" AutoPostBack="True"></asp:TextBox>
                                    </div>
                                    <div class="col-md-4">
                                        <label style="margin-right: 20PX;">Principal Current</label><br />
                                        <asp:TextBox ID="ntxt_CollectionPrincipalCurrent" runat="server" CssClass="form-control" />

                                    </div>
                                    <div class="col-md-4">
                                        <label style="margin-right: 20PX;">Due Interest</label>
                                        <asp:TextBox ID="ntxt_CollectionDueInterest" runat="server" CssClass="form-control" AutoPostBack="True" required="required" />
                                    </div>
                                    <div class="col-md-4">
                                        <label style="margin-right: 20PX;">Overdue Interest</label>
                                        <asp:TextBox ID="ntxt_CollectionOverdueInterest" runat="server" CssClass="form-control" />
                                    </div>
                                    <div class="col-md-4">
                                        <label style="margin-right: 20PX;">Current Interest</label>
                                        <asp:TextBox ID="ntxt_CollectionCurrentInterest" runat="server" CssClass="form-control" onkeypress="return isNumberKey(event)" />
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
