<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/ProjectBM.Master" CodeBehind="frmDepositMaster.aspx.cs" Inherits="iBankingSolution.Master.frmDepositMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        $ = jQuery.noConflict();
        $(function () {
            $('.select2').select2()
        })
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode != 46 && charCode > 31
            && (charCode < 48 || charCode > 57))
                return false;
            return true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
   
        <asp:ScriptManager ID="scrpt1" runat="server"></asp:ScriptManager>
        <div class="row">
            <div class="col-lg-12">
                <div style="float: right; margin-top: 12px;">
                    <asp:Button ID="btnsubmit1" runat="server" Text="Save" class="btn btn-primary" OnClick="btnsubmit_Click" />
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" class="btn btn-primary" OnClick="btnCancel_Click" />
                    <a href="frmDepositMasterList.aspx" class="btn btn-default">Back to List</a>
                </div>
                <h1 class="page-header">Deposit Master</h1>
            </div>
            <!-- /.col-lg-12 -->
        </div>
        <div class="row">
            <div class="col-lg-12">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        Deposit Master Entry
                    </div>
                    <div class="panel-body">
                        <div class="row">

                            <div class="form-group">

                                <div class="col-md-12">
                                    
                                    <asp:Label ID="Label2" runat="server" Text="*" style="color: #FF3300"></asp:Label>
                                    <label style="margin-right: 20PX;">Scheme Name</label>
                                    <asp:Label ID="lblDid" runat="server" BackColor="White" ForeColor="White"></asp:Label>
                                    <asp:TextBox ID="txt_SchemeName" runat="server" required="required" CssClass="form-control" placeholder="ENTER SCHEME NAME" Font-Size="10"/>
                                </div>
                                <div class="col-md-12">
                                    
                                    <asp:Label ID="Label1" runat="server" Text="*" style="color: #FF3300"></asp:Label>
                                    <label style="margin-right: 20PX;">Scheme Type</label>
                                    <asp:DropDownList ID="cmbx_SchemeType" runat="server" CssClass="form-control" Font-Size="10">
                                        <asp:ListItem Value="0">--Select Type--</asp:ListItem>
                                        <asp:ListItem Value="s">Savings</asp:ListItem>
                                        <asp:ListItem Value="cc">Deposit Certificate</asp:ListItem>
                                        <asp:ListItem Value="fd">Fixed Deposite</asp:ListItem>
                                        <asp:ListItem Value="r">Recurring Deposite</asp:ListItem>
                                        <asp:ListItem Value="d">Home Savings</asp:ListItem>
                                        <asp:ListItem Value="jlg">JLG Deposite</asp:ListItem>
                                        <asp:ListItem Value="shg">SHG Deposite</asp:ListItem>
                                        <asp:ListItem Value="mis">MIS Deposite</asp:ListItem>
                                        <asp:ListItem Value="sus">Suspense Deposite</asp:ListItem>
                                        <asp:ListItem Value="i">Investment</asp:ListItem>
                                        <asp:ListItem Value="nf">No Frill Deposite</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-3">
                                    <label style="margin-right: 20PX;">Interest Calc. Frequency</label>
                                    <asp:DropDownList ID="cmbx_InterestCalcFrequency" runat="server" CssClass="form-control" Font-Size="10">
                                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                                        <asp:ListItem Value="Monthly">Monthly</asp:ListItem>
                                        <asp:ListItem Value="Quarterly">Quarterly</asp:ListItem>
                                        <asp:ListItem Value="Yearly">Yearly</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-3">
                                    <label style="margin-right: 20PX;">Interest Type</label>
                                    <asp:DropDownList ID="cmbx_InterestType" runat="server" CssClass="form-control" Font-Size="10">
                                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                                        <asp:ListItem Value="Cumulative">Cumulative</asp:ListItem>
                                        <asp:ListItem Value="Non Cumulative">Non Cumulative</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-3">
                                    <label style="margin-right: 20PX;">Next Int. Calc. Date</label>
                                    <asp:TextBox ID="dtpkr_NextIntCalcDate" placeholder="dd/MM/yyyy" Font-Size="10" CssClass="form-control input-sm BootDatepicker" runat="server" onFocus="this.select()" autocomplete="off" ></asp:TextBox>
                                </div>
                                <div class="col-md-3">
                                    <label style="margin-right: 20PX;">Int. Calc. Based on</label>
                                    <asp:DropDownList ID="cmbx_IntCalcBasedOn" runat="server" CssClass="form-control" Font-Size="10">
                                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                                        <asp:ListItem Value="As Specified">As Specified</asp:ListItem>
                                        <asp:ListItem Value="Date Of Opening">Date Of Opening</asp:ListItem>
                                        
                                    </asp:DropDownList>

                                </div>




                                <div class="clearfix"></div>

                            </div>
                            <div class="form-group">
                                <div class="col-md-4">
                                    <label style="margin-right: 20PX;">Ledger</label>
                                    <asp:DropDownList ID="cmbx_Ledger" runat="server" CssClass="form-control" Font-Size="10"></asp:DropDownList>
                                </div>
                                <div class="col-md-4">
                                    <label style="margin-right: 20PX;">Cheque facility</label>
                                    <asp:CheckBox ID="chkbx_IsChequeFacility" runat="server" CssClass="form-control"/>
                                </div>
                                <div class="col-md-4">
                                    <label style="margin-right: 20PX;">Min Int. Calc. Amount</label>
                                    <asp:TextBox ID="ntxt_MinIntCalcAmount" runat="server" placeholder="ENTER INT AMT" Font-Size="10" CssClass="form-control" />
                                </div>

                                <div class="clearfix"></div>
                            </div>

                            <div class="form-group">
                                <div class="col-md-3">
                                    <label style="margin-right: 20PX;">Minimum Balance</label>
                                    <asp:TextBox ID="ntxt_MinBalance" runat="server" CssClass="form-control" placeholder="XX-XXX" Font-Size="10" onkeypress="return isNumberKey(event)"/>
                                </div>
                                <div class="col-md-3">
                                    <label style="margin-right: 20PX;">Max Withdraw/Month</label>
                                    <asp:TextBox ID="ntxt_MaxWithdraw" runat="server" CssClass="form-control" placeholder="XX-XXX" Font-Size="10" onkeypress="return isNumberKey(event)"/>
                                </div>
                                <div class="col-md-2">
                                    <label style="margin-right: 20PX;">Min Cash Deposit</label>
                                    <asp:TextBox ID="ntxt_MinCashDeposit" runat="server" CssClass="form-control" placeholder="XX-XXX" Font-Size="10" onkeypress="return isNumberKey(event)"/>
                                </div>
                                <div class="col-md-2">
                                    <label style="margin-right: 20PX;">Inoperative After</label>
                                    <asp:TextBox ID="ntxt_InoperativeAfterYear" placeholder="YEAR" Font-Size="10" runat="server" width="70px" Height="35px"/>
                                    <asp:TextBox ID="ntxt_InoperativeAfterMonth" placeholder="MONTH" Font-Size="10" runat="server" width="70px" Height="35px"/>
                                    </div>
                                 <div class="col-md-2">
                                    <label style="margin-right: 20PX;">Unclaimed After</label>
                                    <asp:TextBox ID="ntxt_UnclaimedAfterYear" placeholder="YEAR" Font-Size="10" runat="server" width="70px" Height="35px"/>
                                    <asp:TextBox ID="ntxt_UnclaimedAfterMonth" placeholder="MONTH" Font-Size="10" runat="server" width="70px" Height="35px"/>
                                </div>

                                <div class="clearfix"></div>
                          


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


 
</asp:Content>


