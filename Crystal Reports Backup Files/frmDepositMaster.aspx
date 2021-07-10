<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/ProjectBM.Master" CodeBehind="frmDepositMaster.aspx.cs" Inherits="iBankingSolution.Master.frmDepositMaster" %>

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
                    <a href="frmDepositMasterList.aspx" class="btn btn-default">Back to List</a>
                </div>
                <h1 class="page-header">Deposit Master</h1>
            </div>
            <!-- /.col-lg-12 -->
        </div>
        <div class="row">
            <div class="col-lg-12">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        Deposit Master Entry
                    </div>
                    <div class="panel-body">
                        <div class="row">

                            <div class="form-group">

                                <div class="col-md-12">
                                    <label style="margin-right: 20PX;">Scheme Name</label>
                                    <asp:Label ID="lblDid" runat="server" BackColor="White" ForeColor="White"></asp:Label>
                                    <asp:TextBox ID="txt_SchemeName" runat="server" required="required" CssClass="form-control" />
                                </div>
                                <div class="col-md-12">
                                    <label style="margin-right: 20PX;">Scheme Type</label>
                                    <asp:DropDownList ID="cmbx_SchemeType" runat="server" CssClass="form-control">
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
                                <div class="col-md-4">
                                    <label style="margin-right: 20PX;">Interest Calc. Frequency</label>
                                    <asp:DropDownList ID="cmbx_InterestCalcFrequency" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                                        <asp:ListItem Value="Monthly">Monthly</asp:ListItem>
                                        <asp:ListItem Value="Quarterly">Quarterly</asp:ListItem>
                                        <asp:ListItem Value="Yearly">Yearly</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-4">
                                    <label style="margin-right: 20PX;">Interest Type</label>
                                    <asp:DropDownList ID="cmbx_InterestType" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                                        <asp:ListItem Value="Cumulative">Cumulative</asp:ListItem>
                                        <asp:ListItem Value="Non Cumulative">Non Cumulative</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-4">
                                    <label style="margin-right: 20PX;">Next Int. Calc. Date</label>
                                    <asp:TextBox ID="dtpkr_NextIntCalcDate" CssClass="form-control input-sm BootDatepicker" runat="server" onFocus="this.select()" autocomplete="off" ></asp:TextBox>
                                </div>
                                <div class="col-md-4">
                                    <label style="margin-right: 20PX;">Int. Calc. Based on</label>
                                    <asp:DropDownList ID="cmbx_IntCalcBasedOn" runat="server" CssClass="form-control">
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
                                    <asp:DropDownList ID="cmbx_Ledger" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                                <div class="col-md-4">
                                    <label style="margin-right: 20PX;">Cheque facility</label>
                                    <asp:CheckBox ID="chkbx_IsChequeFacility" runat="server" CssClass="form-control" />
                                </div>
                                <div class="col-md-4">
                                    <label style="margin-right: 20PX;">Min Int. Calc. Amount</label>
                                    <asp:TextBox ID="ntxt_MinIntCalcAmount" runat="server"   CssClass="form-control" />
                                </div>

                                <div class="clearfix"></div>
                            </div>

                            <div class="form-group">
                                <div class="col-md-3">
                                    <label style="margin-right: 20PX;">Minimum Balance</label>
                                    <asp:TextBox ID="ntxt_MinBalance" runat="server"   CssClass="form-control" />
                                </div>
                                <div class="col-md-3">
                                    <label style="margin-right: 20PX;">Max Withdraw/Month</label>
                                    <asp:TextBox ID="ntxt_MaxWithdraw" runat="server" CssClass="form-control" />
                                </div>
                                <div class="col-md-3">
                                    <label style="margin-right: 20PX;">Min Cash Deposit</label>
                                    <asp:TextBox ID="ntxt_MinCashDeposit" runat="server"   CssClass="form-control" />
                                </div>
                                <div class="col-md-3">
                                    <label style="margin-right: 20PX;">Inoperative After</label>
                                    Y<asp:TextBox ID="ntxt_InoperativeAfterYear" runat="server" width="40px"/>
                                    M<asp:TextBox ID="ntxt_InoperativeAfterMonth" runat="server" width="40px"/>

                                    <label style="margin-right: 20PX;">Unclaimed After</label>
                                    Y<asp:TextBox ID="ntxt_UnclaimedAfterYear" runat="server" width="40px"/>
                                    M<asp:TextBox ID="ntxt_UnclaimedAfterMonth" runat="server" width="40px"/>
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


