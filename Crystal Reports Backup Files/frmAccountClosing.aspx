<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/ProjectBM.Master" CodeBehind="frmAccountClosing.aspx.cs" Inherits="iBankingSolution.Transaction.frmAccountClosing" %>

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
                    <asp:Button ID="btnsubmit1" runat="server" Text="Calculate Maturity Amount" class="btn btn-primary" OnClick="btnsubmit_Click" />
                    <asp:Button ID="btnUpdate" runat="server" Text="CloseAccount" class="btn btn-primary" OnClick="btnsubmit2_Click" />
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" class="btn btn-primary" OnClick="btnCancel_Click" />
                    </div>
                <h1 class="page-header">Account Closing</h1>
            </div>
            <!-- /.col-lg-12 -->
        </div>
        <div class="row">
            <div class="col-lg-12">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        Account Closing Entry
                    </div>
                    <div class="panel-body">
                        <div class="row">

                            <div class="form-group">

                                <div class="col-md-4">
                                    <label style="margin-right: 20PX;">Old A/c No</label>
                                    <asp:TextBox ID="txtOldAcNo" runat="server" required="required" CssClass="form-control" />
                                </div>
                                <div class="col-md-4">
                                    <label style="margin-right: 20PX;">A/c No</label>
                                    <asp:TextBox ID="txtAcNo" runat="server" CssClass="form-control" />
                                </div>
                                <div class="col-md-4">
                                    <label style="margin-right: 20PX;">Date of Opening</label>
                                    <asp:TextBox ID="txtdtOpening" CssClass="form-control input-sm BootDatepicker" runat="server" onFocus="this.select()" autocomplete="off" required="required"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                    <label style="margin-right: 20PX;">Deposit Scheme</label>
                                    <asp:TextBox ID="txtdepositScheme" runat="server" CssClass="form-control" />
                                </div>
                                <div class="col-md-6">
                                    <label style="margin-right: 20PX;">Period of Deposit</label>
                                    M<asp:TextBox ID="txtM" runat="server" Width="46" />
                                    D<asp:TextBox ID="txtD" runat="server" Width="46" />
                                </div>

                                <div class="clearfix"></div>

                            </div>
                            <div class="form-group">
                                <div class="col-md-4">
                                    <label style="margin-right: 20PX;">Rate of Interest</label>
                                    <asp:TextBox ID="txtRateofint" runat="server" CssClass="form-control" />
                                </div>
                                <div class="col-md-4">
                                    <label style="margin-right: 20PX;">Penal Rate Of Int</label>
                                    <asp:TextBox ID="txtPenalRateInt" runat="server" CssClass="form-control" />
                                </div>
                                <div class="col-md-4">
                                    <label style="margin-right: 20PX;">Rate Of Int Appl</label>
                                    <asp:TextBox ID="txtRateofIntAppl" runat="server" required="required" CssClass="form-control" />
                                </div>

                                <div class="clearfix"></div>
                            </div>

                            <div class="form-group">
                                <div class="col-md-12">
                                    <label style="margin-right: 20PX;">Maturity Instruction</label>
                                    <asp:DropDownList ID="ddlMaturityInst" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                                <div class="col-md-4">
                                    <label style="margin-right: 20PX;">Take Interest</label>
                                    <asp:DropDownList ID="ddltakeIntr" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="0">Yes</asp:ListItem>
                                        <asp:ListItem Value="1">No</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-4">
                                    <label style="margin-right: 20PX;">Type</label>
                                    <asp:DropDownList ID="ddlAcTransfer" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="0">A/C Transfer</asp:ListItem>
                                        <asp:ListItem Value="1">No</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-4">
                                    <label style="margin-right: 20PX;">SB A/c No</label>
                                    <asp:DropDownList ID="ddlSBAcNo" runat="server" CssClass="form-control" palceholder="Select Ac No">
                                    </asp:DropDownList>
                                </div>

                                <div class="clearfix"></div>
                            </div>

                            <div class="form-group" title="Renewal Instruction">
                                <div class="col-md-3">
                                    <label style="margin-right: 20PX;">Renewal Date</label>
                                     <asp:TextBox ID="txtRenewalDt" CssClass="form-control input-sm BootDatepicker" runat="server" onFocus="this.select()" autocomplete="off" required="required"></asp:TextBox>
                                </div>
                                <div class="col-md-3">
                                    <label style="margin-right: 20PX;">Adjustment Date</label>
                                   <asp:TextBox ID="txtAdjDt" CssClass="form-control input-sm BootDatepicker" runat="server" onFocus="this.select()" autocomplete="off" required="required"></asp:TextBox>
                                </div>
                                <div class="col-md-3">
                                    <label style="margin-right: 20PX;">Periods in Month+Days</label>
                                      <asp:TextBox ID="txtMonth" runat="server"  Width="46"/>
                                      <asp:TextBox ID="txtDays" runat="server"  Width="46"/>
                                </div>
                                <div class="col-md-3">
                                    <label style="margin-right: 20PX;">ROI</label>
                                      <asp:TextBox ID="txtROi" runat="server"   CssClass="form-control"/>
                                </div>
                                  <div class="col-md-4">
                                    <label style="margin-right: 20PX;">Deposit Amount</label>
                                      <asp:TextBox ID="txtDepositamt" runat="server"   CssClass="form-control"/>
                                </div>
                                 <div class="col-md-4">
                                    <label style="margin-right: 20PX;">Date of Maturity</label>
                                      <asp:TextBox ID="txtdtofMaturiry" CssClass="form-control input-sm BootDatepicker" runat="server" onFocus="this.select()" autocomplete="off" required="required"></asp:TextBox>
                                </div>
                                 <div class="col-md-4">
                                    <label style="margin-right: 20PX;">Maturity Amount</label>
                                      <asp:TextBox ID="txtMaturityAmt" runat="server"   CssClass="form-control"/>
                                </div>
                                <div class="clearfix"></div>
                            </div>

                             <div class="form-group">
                                <div class="col-md-12">
                                    <label style="margin-right: 20PX;">Interest Issued From </label>
                                     <asp:TextBox ID="txtIntrissuedFr" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-md-12">
                                    <label style="margin-right: 20PX;">Penal Int. Rec. In</label>
                                   <asp:TextBox ID="txtpenalIntRec" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>

                            </div>
                             <div class="form-group" title="Collection Data">
                                <div class="col-md-4">
                                    <label style="margin-right: 20PX;">Interest credited till date</label>
                                      <asp:TextBox ID="txtIntcrdt" CssClass="form-control input-sm BootDatepicker" runat="server" onFocus="this.select()" autocomplete="off" required="required"></asp:TextBox>  
                                </div>
                                <div class="col-md-4">
                                    <label style="margin-right: 20PX;">Interest Adjusted</label>
                                      <asp:TextBox ID="txtIntAdj" runat="server"   CssClass="form-control"/>
                                </div>
                                  <div class="col-md-4">
                                    <label style="margin-right: 20PX;">Date of Maturity</label>
                                      <asp:TextBox ID="txtMaturityDt" CssClass="form-control input-sm BootDatepicker" runat="server" onFocus="this.select()" autocomplete="off" required="required"></asp:TextBox>  
                                </div>
                                 <div class="col-md-4">
                                    <label style="margin-right: 20PX;">Penal Interest</label>
                                      <asp:TextBox ID="txtPenalIntr" runat="server" CssClass="form-control"/>
                                </div>
                                 <div class="col-md-4">
                                    <label style="margin-right: 20PX;">WithdrwalDate</label>
                                      <asp:TextBox ID="txtwithdrwdt" CssClass="form-control input-sm BootDatepicker" runat="server" onFocus="this.select()" autocomplete="off" required="required"></asp:TextBox>  
                                </div>
                                 <div class="col-md-4">
                                    <label style="margin-right: 20PX;">Maturity Amount</label>
                                      <asp:TextBox ID="txtclMaturityamt" runat="server"   CssClass="form-control"/>
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
                    <asp:Button ID="btnsubmit" runat="server" Text="Calculate Maturity Amount" class="btn btn-primary" OnClick="btnsubmit_Click" />
                     <asp:Button ID="btnsubmit2" runat="server" Text="CloseAccount" class="btn btn-primary" OnClick="btnsubmit2_Click" />
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

