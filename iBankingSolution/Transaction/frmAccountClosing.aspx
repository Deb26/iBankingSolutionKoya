<%@ Page Language="C#" AutoEventWireup="true" MaintainScrollPositionOnPostback="true" MasterPageFile="~/MasterPages/ProjectBM.Master" CodeBehind="frmAccountClosing.aspx.cs" Inherits="iBankingSolution.Transaction.frmAccountClosing" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
     <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.4.1/js/bootstrap-datepicker.min.js"></script>
         <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.4.1/css/bootstrap-datepicker3.css"/>
    <script type="text/javascript">
        $ = jQuery.noConflict();
        $(function () {
            $('.select2').select2()
        })
    </script>
    <style type="text/css">
        .form-control {}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="scrpt1" runat="server"></asp:ScriptManager>
    <div class="row">
        <div class="col-lg-12">
            <div style="float: right; margin-top: 12px;">
               
                <asp:Button ID="btnsubmit1" runat="server" Text="Calculate Maturity Amount" class="btn btn-primary" OnClick="btnsubmit_Click" Visible="False" />
                <asp:Button ID="btn_CloseAccount" runat="server" Text="CloseAccount" class="btn btn-primary" OnClick="btn_CloseAccount_Click" Visible="False" />
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" class="btn btn-primary" OnClick="btnCancel_Click" />
            </div>
            <h1 class="page-header">Account Closing</h1>
        </div>

    </div>
    <div class="row">
        <div class="col-lg-8">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    Account Closing Entry
                </div>
                <div class="panel-body">
                    <div class="row">

                        <div class="form-group">
                            <div class="col-md-12">
                                <div class="panel panel-primary">

                                    <div class="panel-heading text-center">
                                        <b>Accounts Details</b>

                                    </div>
                                </div>
                            </div>
                            <div class="clearfix"></div><br />
                            <div class="col-md-1">
                                <asp:Label ID="lblSession" runat="server" BackColor="White" ForeColor="White"></asp:Label>
                            </div>
                            <div class="clearfix"></div>
                            
                            <div class="col-md-2">
                                <label style="margin-right: 20PX;">Old A/c No</label>
                                <asp:TextBox ID="txtOldAcNo" placeholder="ENTER OLD A/C NO" Font-Size="10" runat="server" CssClass="form-control" AutoPostBack="True" OnTextChanged="txtOldAcNo_TextChanged" />
                            </div>
                            <div class="col-md-2">
                                <label style="margin-right: 20PX;">A/c No</label>
                                <asp:TextBox ID="txtAcNo" placeholder="ENTER A/C NO" Font-Size="10" runat="server" CssClass="form-control" AutoPostBack="True" OnTextChanged="txtAcNo_TextChanged" Width="100px" />
                            </div>
                            <div class="col-md-2">
                                <label style="margin-right: 20PX;">Opening DT</label>
                                <asp:TextBox ID="txtdtOpening" placeholder="DD/MM/YYYY" Font-Size="10" ReadOnly="true" CssClass="form-control" runat="server" onFocus="this.select()" autocomplete="off" Width="120px"></asp:TextBox>

                            </div>
                            <%-- Scripting Section --%>
                            <div class="col-md-3">
                                <label style="margin-right: 20PX;">AcType</label>
                                <asp:TextBox ID="txtacctype" placeholder="Accont Type" Font-Size="10" ReadOnly="true" CssClass="form-control" runat="server" OnTextChanged="txtacctype_TextChanged" AutoPostBack="True"></asp:TextBox>

                            </div>
                            <div class="col-md-3">
                                <label style="margin-right: 20PX;">Last Trans.Dt</label>
                                <asp:TextBox ID="txtdtLastTrnsDt" placeholder="DD/MM/YYYY" Font-Size="10" ReadOnly="true" CssClass="form-control" runat="server" onFocus="this.select()" autocomplete="off"></asp:TextBox>

                            </div>
                            <div class="clearfix"></div>
                        </div>
                        <%--Panel Certificate Two --%>
                        <div class="clearfix"></div>
                        <asp:Panel runat="server" ID="PanelCertificateTwo" Visible="false">
                            <div class="form-group">
                                <div class="col-md-12">
                                    <div class="panel panel-primary">

                                        <div class="panel-heading text-center">
                                            <b>Certificate Details</b>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <label style="margin-right: 20PX;">ROI</label>
                                    <asp:TextBox ID="ntxt_RenewalROI" runat="server" placeholder="ENTER ROI" Font-Size="10" CssClass="form-control" AutoPostBack="True" required="required"  Enable="false"/>

                                </div>
                                <div class="col-md-3">
                                    <label style="margin-right: 20px;">Maturity Date</label>
                                    <asp:TextBox ID="dtpkr_RenewalMatDt" placeholder="DD/MM/YYYY" Font-Size="10" CssClass="form-control input-sm BootDatepicker" runat="server" onFocus="this.select()" autocomplete="off" required="required" Enabled="false"></asp:TextBox>
                                </div>
                                <div class="col-md-3">
                                    <label style="margin-right: 20px;">Period(MM+DD)</label>
                                    <asp:TextBox ID="txt_RePrdinMonth" runat="server" Width="58px" Height="30" Font-Size="10" onkeypress="return isNumberKey(event)" AutoPostBack="True" MaxLength="2" Enabled="false"></asp:TextBox>
                                    <asp:TextBox ID="txt_RePrdInDays" runat="server" Width="58px" Height="30" Font-Size="10" AutoPostBack="True" onkeypress="return isNumberKey(event)" MaxLength="2" Enabled="false"></asp:TextBox>
                                </div>
                                 <div class="col-md-3">
                                    <label style="margin-right: 20PX;">Maturity-Amount</label>
                                    <asp:TextBox ID="ntxt_RMaturityAmt" runat="server" placeholder="MATURITY AMT" Font-Size="10" CssClass="form-control" Enabled="False" />
                                    <br />
                                </div>
                                <div class="clearfix"></div>
                                <br />
                                <div class="col-md-3">
                                    <label style="margin-right: 20PX;">Dep.Amt</label>
                                    <asp:TextBox ID="txt_RenewalDepotAmt" runat="server" placeholder="DEPOSITE AMT" Font-Size="10" CssClass="form-control" onkeypress="return isNumberKey(event)" AutoPostBack="True" required="required" Enabled="false" />
                                </div>
                               <div class="col-md-3">
                                <label style="margin-right: 20PX;">Interest Issued From</label>
                                <asp:TextBox ID="txtIntrissuedFrCode"  runat="server" Enabled="False" Height="28"></asp:TextBox>
                                <%--<asp:TextBox ID="txtIntrissuedFrLedgerName"  runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>--%>
                            </div>
                                <div class="col-md-6">
                                    <label style="margin-right: 20px;">Interested Issued From (Ledger Name)</label>
                                    <asp:TextBox ID="txtIntrissuedFrLedgerName" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                                </div>
                             
                            </div>
                        </asp:Panel> 
                        <br />
                        <div class="clearfix"></div><br />

                        <%-- Panel for homesavings--%>
                        <asp:Panel runat="server" ID="PANELHOMESAVINGS" Visible="false">
                            <div class="form-group">
                                <div class="col-md-12">
                                    <div class="panel panel-primary">

                                        <div class="panel-heading text-center">
                                            <b>Certificate Details</b>

                                        </div>
                                    </div>
                                </div>
                                
                                 <div class="col-md-3">
                                    <label style="margin-right: 20PX;">ROI</label>
                                    <asp:TextBox ID="ntxt_RenewalROII" runat="server" placeholder="ENTER ROI" Font-Size="10" CssClass="form-control" AutoPostBack="True" required="required" ReadOnly="true" OnTextChanged="txtapplHROI_TextChanged"/>

                                </div>
                                <div class="col-md-3">
                                    <label style="margin-right: 20px;">Maturity Date</label>
                                    <asp:TextBox ID="dtpkr_RenewalMatDtt" placeholder="DD/MM/YYYY" Font-Size="10" CssClass="form-control input-sm BootDatepicker" runat="server" onFocus="this.select()" autocomplete="off" required="required" Enabled="false"></asp:TextBox>
                                </div>
                                <div class="col-md-3">
                                    <label style="margin-right: 20px;">Period(MM+DD)</label>
                                    <asp:TextBox ID="txt_RePrdinMonths" runat="server" Width="58px" Height="30" Font-Size="10" onkeypress="return isNumberKey(event)" AutoPostBack="True" MaxLength="2" Enabled="false"></asp:TextBox>
                                    <asp:TextBox ID="txt_RePrdInDay" runat="server" Width="58px" Height="30" Font-Size="10" AutoPostBack="True" onkeypress="return isNumberKey(event)" MaxLength="2" Enabled="false"></asp:TextBox>
                                </div>
                                 <div class="col-md-3">
                                    <label style="margin-right: 20PX;">Maturity-Amount</label>
                                    <asp:TextBox ID="ntxt_RMaturityAmtt" runat="server" placeholder="MATURITY AMT" Font-Size="10" CssClass="form-control" Enabled="False" />
                                    <br />
                                </div>
                                <div class="clearfix"></div>
                                <div class="col-md-3">
                                    <label style="margin-right: 20PX;">Depo-Amt</label>
                                    <asp:TextBox ID="txtdepoamtt" placeholder="ENTER DEPO AMT" ReadOnly="true" runat="server" Font-Size="10" CssClass="form-control" />
                                </div>
                                <div class="col-md-3">
                                <label style="margin-right: 20PX;">Interest Issued From</label>
                                <asp:TextBox ID="txtIntrissuedFrCodee"  runat="server" Enabled="False" Height="28"></asp:TextBox>
                                
                                </div>
                                <div class="col-md-6">
                                    <label style="margin-right: 20px;">Interested Issued From (Ledger Name)</label>
                                    <asp:TextBox ID="txtIntrissuedFrLedgerNamee" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                                </div>


                            </div>
                        </asp:Panel>
                        <div class="clearfix"></div>
                        <br />
                        <%--Renewal Instruction Panel--%>
                        <div class="clearfix"></div>
                        <asp:Panel runat="server" ID="PanelRenewalInstruction" Visible="false">
                            <div class="form-group">
                                <div class="col-md-12">
                                    <div class="panel panel-primary">

                                        <div class="panel-heading text-center">
                                            <b>Renewal Instruction</b>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <label style="margin-right: 20PX;">ROI</label>
                                    <asp:TextBox ID="txtRateofint" placeholder="ENTER ROI" ReadOnly="true" runat="server" Font-Size="10" CssClass="form-control" Enable="false"/>
                                </div>
                                <div class="col-md-2">
                                    <label style="margin-right: 20PX;">Appl ROI</label>
                                    <asp:TextBox ID="txtapplROI" placeholder="ENTER ROI APPL" runat="server" Font-Size="10" required="required" CssClass="form-control" AutoPostBack="True" OnTextChanged="txtapplROI_TextChanged" />
                                </div>
                                <div class="col-md-2">
                                    <label style="margin-right: 20PX;">Depo-Amt</label>
                                    <asp:TextBox ID="txtdepoamt" placeholder="ENTER DEPO AMT" ReadOnly="true" runat="server" Font-Size="10" CssClass="form-control" />
                                </div>
                                <div class="col-md-3">

                                    <label style="margin-right: 20PX;">Deposit Per(M+d)</label>
                                    <asp:TextBox ID="txtM" runat="server" ReadOnly="true" placeholder="MONTH" Width="58px" Height="30" Font-Size="10" />
                                    <asp:TextBox ID="txtD" runat="server" ReadOnly="true" placeholder="DAY" Width="58px" Height="30" Font-Size="10" />
                                </div>
                                <div class="col-md-3">
                                    <label style="margin-right: 20PX;">MaturityDt</label>
                                    <asp:TextBox ID="txtdtofMaturiry" placeholder="DD/MM/YYYY" Font-Size="10" ReadOnly="true" CssClass="form-control" runat="server" onFocus="this.select()" autocomplete="off" required="required"></asp:TextBox>
                                </div>
                                <div class="clearfix"></div>
                                <br />
                                 <div class="col-md-3">
                                    <label style="margin-right: 20PX;">Take Interest</label>
                                    <asp:DropDownList ID="cmbx_TakeInterest" runat="server" Filter="Contains" MarkFirstMatch="true" EmptyMessage="Select" Enabled="True" CssClass="form-control">

                                        <asp:ListItem>Yes</asp:ListItem>
                                        <asp:ListItem>No</asp:ListItem>

                                    </asp:DropDownList>
                                </div>
                                 <div class="col-md-3">
                                    <label style="margin-right: 20PX;">SB A/c No</label>
                                    <asp:DropDownList ID="cmbx_TransferAcNo" runat="server" Filter="Contains" MarkFirstMatch="true" EmptyMessage="Select A/c No" CssClass="form-control"
                                        DataTextField="SL_CODE" DataValueField="SL_CODE" Enabled="False">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-3">
                                <label style="margin-right: 20PX;">Renewal Date</label>
                                <asp:TextBox ID="dtpkr_RenewalDate" placeholder="DD/MM/YYYY" CssClass="form-control input-sm BootDatepicker" runat="server" onFocus="this.select()" autocomplete="off" required="required"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <label style="margin-right: 20PX;">Adjustment Date</label>
                                <asp:TextBox ID="dtpkr_RenewalAdjustmentDate" placeholder="DD/MM/YYYY" CssClass="form-control input-sm BootDatepicker" runat="server" onFocus="this.select()" autocomplete="off" required="required"></asp:TextBox>
                            </div>
 
                                </div>
                            </asp:Panel>

                        <div class="clearfix"></div>
                        <br />
                        <%--First Panel Certificate --%>
                        <asp:Panel runat="server" ID="PanelCertificate" Visible="false">
                            <div class="form-group">
                                <div class="col-md-12">
                                    <div class="panel panel-primary">



                                        <div class="panel-heading text-center">
                                            <b>Certificate Details</b>

                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <label style="margin-right: 20PX;">Deposit Scheme</label>
                                    <asp:TextBox ID="txtdepositScheme" placeholder="ENTER DEPO SCHEME" ReadOnly="true" Font-Size="10" runat="server" CssClass="form-control" />
                                    <br />
                                </div>
                                <%--<div class="col-md-2">
                                    <label style="margin-right: 20PX;">ROI</label>
                                    <asp:TextBox ID="txtRateofint" placeholder="ENTER ROI" ReadOnly="true" runat="server" Font-Size="10" CssClass="form-control" />
                                </div>
                                <div class="col-md-2">
                                    <label style="margin-right: 20PX;">Appl ROI</label>
                                    <asp:TextBox ID="txtapplROI" placeholder="ENTER ROI APPL" runat="server" Font-Size="10" required="required" CssClass="form-control" AutoPostBack="True" OnTextChanged="txtapplROI_TextChanged" />
                                </div>
                                <div class="col-md-2">
                                    <label style="margin-right: 20PX;">Depo-Amt</label>
                                    <asp:TextBox ID="txtdepoamt" placeholder="ENTER DEPO AMT" ReadOnly="true" runat="server" Font-Size="10" CssClass="form-control" />
                                </div>
                                <div class="col-md-3">

                                    <label style="margin-right: 20PX;">Deposit Per(M+d)</label>
                                    <asp:TextBox ID="txtM" runat="server" ReadOnly="true" placeholder="MONTH" Width="58px" Height="30" Font-Size="10" />
                                    <asp:TextBox ID="txtD" runat="server" ReadOnly="true" placeholder="DAY" Width="58px" Height="30" Font-Size="10" />
                                </div>
                                <div class="col-md-3">
                                    <label style="margin-right: 20PX;">MaturityDt</label>
                                    <asp:TextBox ID="txtdtofMaturiry" placeholder="DD/MM/YYYY" Font-Size="10" ReadOnly="true" CssClass="form-control" runat="server" onFocus="this.select()" autocomplete="off" required="required"></asp:TextBox>
                                </div>
 --%>                               <%-- <div class="col-md-2">
                                    <label style="margin-right: 20PX;">MaturityAmt</label>
                                      <asp:TextBox ID="txtMaturityAmt" runat="server" ReadOnly="true" placeholder="ENTER MATURITY AMT" Font-Size="10"  CssClass="form-control"/>
                                </div>--%>
                                <%--<div class="clearfix"></div>
                                  <br />
                                  <div class="col-md-3">
                                    <label style="margin-right: 20PX;">Maturity Instruction</label>
                                    <asp:DropDownList ID="ddlMaturityInst" runat="server" CssClass="form-control" Font-Size="10">
                                        <asp:ListItem>Renew Principle</asp:ListItem>
                                        <asp:ListItem>Renew principle with Intrest</asp:ListItem>
                                        <asp:ListItem>Cash Withdrawl</asp:ListItem>
                                        <asp:ListItem>A/C Transfer</asp:ListItem>
                                    </asp:DropDownList>
                                </div>--%>


                               <%-- <div class="col-md-3">
                                    <label style="margin-right: 20PX;">Take Interest</label>
                                    <asp:DropDownList ID="cmbx_TakeInterest" runat="server" Filter="Contains" MarkFirstMatch="true" EmptyMessage="Select" Enabled="True" CssClass="form-control">

                                        <asp:ListItem>Yes</asp:ListItem>
                                        <asp:ListItem>No</asp:ListItem>

                                    </asp:DropDownList>
                                </div>--%>
                                <%--Not Required --%>
                               <%-- <div class="col-md-3">
                                    <label style="margin-right: 20PX;">Type</label>
                                    <asp:DropDownList ID="cmbx_Type" runat="server" Filter="Contains" MarkFirstMatch="true" EmptyMessage="Select" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="cmbx_Type_SelectedIndexChanged">

                                        <asp:ListItem> Cash Withdrawl </asp:ListItem>
                                        <asp:ListItem> A/c Transfer</asp:ListItem>

                                    </asp:DropDownList>
                                </div>--%>
                               <%-- <div class="col-md-3">
                                    <label style="margin-right: 20PX;">SB A/c No</label>
                                    <asp:DropDownList ID="cmbx_TransferAcNo" runat="server" Filter="Contains" MarkFirstMatch="true" EmptyMessage="Select A/c No" CssClass="form-control"
                                        DataTextField="SL_CODE" DataValueField="SL_CODE" Enabled="False">
                                    </asp:DropDownList>
                                </div>--%>

                            </div>
                        </asp:Panel>


                        <div class="clearfix"></div>
                        <%--PANNEL CERTIFICATE TWO --%>
                        <%-- <asp:Panel runat="server" ID="PanelCertificateTwo" Visible="false">
                            <div class="form-group">
                                <div class="col-md-12">
                                    <div class="panel panel-primary">
                                        <div class="panel-heading text-center">
                                                            <b>Certificate Details</b>

                                         </div>
                                        </div>
                                    </div>
                                                <div class="clearfix"></div>
                                                  <br />
                                                  <div class="col-md-3">
                                                    <label style="margin-right: 20PX;">Maturity Instruction</label>
                                                    <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control" Font-Size="10" Width="250px">
                                                       <asp:ListItem>Renew Principle</asp:ListItem>
                                                        <asp:ListItem>Renew principle with Intrest</asp:ListItem>
                                                        <asp:ListItem>Cash Withdrawl</asp:ListItem>
                                                        <asp:ListItem>A/C Transfer</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                
                                <div class="col-md-5">
                                    <label style="margin-right: 60px;">Transfer Account</label>
                                    <asp:TextBox ID="txt_TransferAc" runat="server" placeholder="Transfer Account" Font-Size="10" CssClass="form-control" Enabled="False" />
                                </div>


                                

                            </div>
                        </asp:Panel>--%>
                        <div class="clearfix"></div>

         <fieldset id="fs_RenewalInstraction" runat="server">
                        <asp:Panel runat="server" ID="PanelRenew" Visible="false">
                            <div class="form-group">
                                <div class="col-md-12">
                                    <div class="panel panel-primary">

                                        <div class="panel-heading text-center">
                                            <b>Renewal Instruction</b>

                                        </div>
                                    </div>
                                </div>
                              
                                <div class="clearfix"></div>
                                <%--<div class="col-md-3">
                                     <label style="margin-right: 20PX;">Maturity Instruction</label>
                                     <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control" Font-Size="10" Width="250px">
                                     <asp:ListItem>Cash Withdrawl</asp:ListItem>
                                     <asp:ListItem>A/C Transfer</asp:ListItem>
                                     </asp:DropDownList>
                                 </div>
                                <div class="col-md-3">
                                    <label style="margin-right: 60px;">Transfer Account</label>
                                    <asp:TextBox ID="txt_TransferAc" runat="server" placeholder="Transfer Account" Font-Size="10" CssClass="form-control" Enabled="False" />
                                </div>--%>
                                 <%--<div class="col-md-3">
                                <label style="margin-right: 20PX;">Renewal Date</label>
                                <asp:TextBox ID="dtpkr_RenewalDate" placeholder="DD/MM/YYYY" CssClass="form-control input-sm BootDatepicker" runat="server" onFocus="this.select()" autocomplete="off" required="required"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <label style="margin-right: 20PX;">Adjustment Date</label>
                                <asp:TextBox ID="dtpkr_RenewalAdjustmentDate" placeholder="DD/MM/YYYY" CssClass="form-control input-sm BootDatepicker" runat="server" onFocus="this.select()" autocomplete="off" required="required"></asp:TextBox>
                            </div>--%>
                                <%--<div class="col-md-3">
                                    <label style="margin-right: 20PX;">Period(MM+DD)</label><br />
                                    <asp:TextBox ID="txt_RePrdinMonth" runat="server" Width="58px" Height="30" Font-Size="10" onkeypress="return isNumberKey(event)" AutoPostBack="True" MaxLength="2" />
                                    <asp:TextBox ID="txt_RePrdInDays" Width="58px" Height="30" runat="server" Font-Size="10" AutoPostBack="True" onkeypress="return isNumberKey(event)" MaxLength="2" />
                                </div>--%>
                                <%--<div class="col-md-3">
                                    <label style="margin-right: 20PX;">ROI</label>
                                    <asp:TextBox ID="ntxt_RenewalROI" runat="server" placeholder="ENTER ROI" Font-Size="10" CssClass="form-control" AutoPostBack="True" required="required" />

                                </div>--%>

                                <%--<div class="col-md-3">
                                    <label style="margin-right: 20PX;">Dep.Amt</label>
                                    <asp:TextBox ID="txt_RenewalDepotAmt" runat="server" placeholder="DEPOSITE AMT" Font-Size="10" CssClass="form-control" onkeypress="return isNumberKey(event)" AutoPostBack="True" required="required" />
                                </div>--%>

                               <%-- <div class="col-md-2">
                                    <label style="margin-right: 20PX;">MaturityDt</label>
                                    <asp:TextBox ID="dtpkr_RenewalMatDt" placeholder="DD/MM/YYYY" Font-Size="10" CssClass="form-control input-sm BootDatepicker" runat="server" onFocus="this.select()" autocomplete="off" required="required" Enabled="False"></asp:TextBox>
                                </div>--%>
                               <%-- <div class="col-md-3">
                                    <label style="margin-right: 20PX;">Matur-Amt</label>
                                    <asp:TextBox ID="ntxt_RMaturityAmt" runat="server" placeholder="MATURITY AMT" Font-Size="10" CssClass="form-control" Enabled="False" />
                                    <br />
                                </div>--%>
                            </div>
                        </asp:Panel>
                        </fieldset>

         <fieldset id="fs_InterestIssued" runat="server" visible="false">
                        <div class="form-group">
                            <%--<div class="col-md-12">
                                <label style="margin-right: 20PX;">Interest Issued From </label>
                                <asp:TextBox ID="txtIntrissuedFrCode"  runat="server" Enabled="False"></asp:TextBox>
                                <asp:TextBox ID="txtIntrissuedFrLedgerName"  runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                            </div>--%>
                            <%--<div class="col-md-12">
                                <label style="margin-right: 20PX;">Penal Int. Rec. In</label>
                                <asp:TextBox ID="txtpenalIntRecCode" runat="server" Enabled="False"></asp:TextBox>
                                <asp:TextBox ID="txtpenalIntRecLedgerName"  runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                            </div>--%>

                        </div>
     </fieldset>
                     
                        <div class="clearfix"></div>
                        <asp:Panel runat="server" ID="maturityPanel" Visible="false">
                            <div class="form-group">
                                <div class="col-md-12">
                                    <div class="panel panel-primary">

                                        <div class="panel-heading text-center">
                                            <b>Collection Data</b>

                                        </div>
                                    </div>
                                </div>
                                <%--   <div class="col-md-3">
                                    <label style="margin-right: 20PX;">Instruction Details</label>
                                    <asp:DropDownList ID="ddlMaturityInst" runat="server" CssClass="form-control" Font-Size="10">
                                        <asp:ListItem>Cash Withdrawl</asp:ListItem>
                                        <asp:ListItem>A/C Transfer</asp:ListItem>
                                       
                                    </asp:DropDownList>
                                </div>--%>
                                <%--<div class="col-md-3">
                                    <label style="margin-right: 20px"></label>
                                    <asp:DropDownList ID="DropDownList1" runat="server" Filter="Contains" MarkFirstMatch="true" EmptyMessage="Select" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="cmbx_Type_SelectedIndexChanged" Width="250px">
                                        <asp:ListItem>-- Select Maturity Instruction --</asp:ListItem>
                                        <asp:ListItem Value="r">Renew Principle</asp:ListItem>
                                        <asp:ListItem Value="rp">Renew Principle With Interest</asp:ListItem>
                                    </asp:DropDownList>
                                </div>--%>
                                <div class="col-md-3">
                                     <label style="margin-right: 20PX;">Maturity Instruction</label>
                                    <asp:DropDownList ID="ddlMaturityInst" runat="server" Filter="Contains" MarkFirstMatch="true" EmptyMessage="Select" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="cmbx_Type_SelectedIndexChanged" Width="250px">
                                     <%--<asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control" Font-Size="10" Width="250px">--%>
                                     <asp:ListItem>-- Select Maturity Instruction --</asp:ListItem>
                                     <asp:ListItem Value="R">Renew Principle</asp:ListItem>
                                     <asp:ListItem Value="RP">Renew principle with Interest</asp:ListItem>
                                     <asp:ListItem Value="CASH WITHDRAWL">Cash Withdrawl</asp:ListItem>
                                     <asp:ListItem Value="A/C TRANSFER">A/C Transfer</asp:ListItem>
                                     </asp:DropDownList>
                                 </div>
                                
                                <div class="col-md-1"></div>
                                <div class="col-md-3">
                                    <label style="margin-right: 20px;">Transfer Account</label>
                                    <asp:DropDownList ID="cmbx_IntTransferAccountTo" runat="server" placeholder="Account Transfer" Font-Size="10" Width="250px" CssClass="form-control"></asp:DropDownList>
                                    <%--<asp:TextBox ID="txtTransferAccountTo" runat="server" placeholder="Account Transfer" Font-Size="10" Width="250px" CssClass="form-control"></asp:TextBox>--%>
                                    <%--<asp:TextBox ID="txt_TransferAc" runat="server" placeholder="Transfer Account" Font-Size="10" Enabled="false" CssClass="form-control" width="250px"/>--%>
                                </div>
                                <div class="col-md-1"></div>
                                <div class="col-md-3">
                                    <label style="margin-right: 20PX;">Interest Adjusted</label>
                                    <asp:TextBox ID="txtIntAdj" runat="server" placeholder="INT ADJUSTED" Font-Size="10" ReadOnly="FALSE" CssClass="form-control" Width="250px" />
                                </div>
                                <div class="clearfix"></div>
                                <br />
                                <div class="col-md-2">
                                    <label style="margin-right: 20px;">TDS</label>
                                    <asp:TextBox ID="txttds" runat="server" placeholder="ENTER TDS" CssClass="form-control" Font-Size="10" width="250px"/>
                                </div>
                                <div class="col-md-2"></div>
                                <div class="col-md-3">
                                    <label style="margin-right: 20PX;">MaturityAmt</label>
                                    <asp:TextBox ID="txt_MaturityAmt" runat="server" placeholder="MATURITY AMT" ReadOnly="FALSE" Font-Size="10" CssClass="form-control" Width="250px" />
                                </div>
                                <div class="col-md-1"></div>
                                <div class="col-md-2">
                                    <label style="margin-right: 20PX;">WithdrawalDt.</label>
                                    <asp:TextBox ID="dtpkr_WithdrawlDate" runat="server" placeholder="Withdrawal Dt" Font-Size="10" CssClass="form-control input-sm BootDatepicker" Width="250px" />
                                </div>


                            </div>
                        </asp:Panel>

                       <%-- PANEL FOR HOME SAVINGS--%>
                        <div class="clearfix"></div><br />
                         <asp:Panel runat="server" ID="panelhomesavingmaturity" Visible="false">
                            <div class="form-group">
                                <div class="col-md-12">
                                    <div class="panel panel-primary">

                                        <div class="panel-heading text-center">
                                            <b>Collection Data</b>

                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                     <label style="margin-right: 20PX;">Maturity Instruction</label>
                                    <asp:DropDownList ID="cmbx_dropdown" runat="server" Filter="Contains" MarkFirstMatch="true" EmptyMessage="Select" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="cmbx_Typehome_SelectedIndexChanged" Width="250px">
                                     <asp:ListItem>-- Select Maturity Instruction --</asp:ListItem>
                                     <asp:ListItem Value="CASH WITHDRAWL">Cash Withdrawl</asp:ListItem>
                                     <asp:ListItem Value="A/C TRANSFER">A/C Transfer</asp:ListItem>
                                     </asp:DropDownList>
                                 </div>
                                
                                <div class="col-md-1"></div>
                                <div class="col-md-3">
                                    <label style="margin-right: 20px;">Transfer Account</label>
                                    <asp:DropDownList ID="cmbx_transfer" runat="server" placeholder="Account Transfer" Font-Size="10" Width="250px" CssClass="form-control"></asp:DropDownList>
                        
                                </div>
                                <div class="col-md-1"></div>
                                <div class="col-md-3">
                                    <label style="margin-right: 20PX;">Interest Adjusted</label>
                                    <asp:TextBox ID="txtiadjested" runat="server" placeholder="INT ADJUSTED" Font-Size="10" ReadOnly="FALSE" CssClass="form-control" Width="250px" AutoPostBack="true"  OnTextChanged="intadj_selectedvalue"/>
                                </div>
                                <div class="clearfix"></div>
                                <br />
                                <div class="col-md-2">
                                    <label style="margin-right: 20px;">TDS</label>
                                    <asp:TextBox ID="txtt" runat="server" placeholder="ENTER TDS" CssClass="form-control" Font-Size="10" width="250px"/>
                                </div>
                                <div class="col-md-2"></div>
                                <div class="col-md-3">
                                    <label style="margin-right: 20PX;">MaturityAmt</label>
                                    <asp:TextBox ID="txtmamt" runat="server" placeholder="MATURITY AMT" ReadOnly="true" Font-Size="10" CssClass="form-control" Width="250px" />
                                </div>
                                <div class="col-md-1"></div>
                                <div class="col-md-2">
                                    <label style="margin-right: 20PX;">WithdrawalDt.</label>
                                    <asp:TextBox ID="txtwdt" runat="server" placeholder="Withdrawal Dt" Font-Size="10" CssClass="form-control input-sm BootDatepicker" Width="250px" />
                                </div>


                            </div>
                        </asp:Panel>
                         


                      


 
                        <div class="clearfix"></div>


                    </div>


        
                   

                  
                </div>
            </div>
        </div>



        <div class="row">
            <div class="col-lg-4">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        Account Closing Entry
                    </div>
                    <div class="panel-body">



                        <asp:ListView ID="lv_AcctHolders" runat="server" RenderMode="Classic"
                            ItemPlaceholderID="AcctHoldersContainer" OnSelectedIndexChanged="lv_AcctHolder_SelectedIndexChanged">
                            <LayoutTemplate>
                                <fieldset class="layoutFieldset ">
                                    <legend>Account Holders</legend>
                                    <asp:PlaceHolder ID="AcctHoldersContainer" runat="server"></asp:PlaceHolder>
                                </fieldset>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <table>
                                    <tr>
                                        <td style="width: 75%; vertical-align: top;">
                                            <table style="width: 100%">
                                                <tr>
                                                    <td style="width: 40%; font-weight: bold;">CUST ID:</td>
                                                    <td style="width: 60%; padding-left: 2px"><%#Eval("CUST_ID")%></td>
                                                </tr>
                                                <tr>
                                                    <td style="font-weight: bold;">NAME:</td>
                                                    <td><%#Eval("NAME")%></td>
                                                </tr>
                                                <tr>
                                                    <td style="font-weight: bold;">GURDIAN NAME:</td>
                                                    <td><%# Eval("GUARDIAN_NAME")%></td>
                                                </tr>
                                                <tr>
                                                    <td style="font-weight: bold;">VILLAGE:</td>
                                                    <td><%# Eval("VILL_CODE")%></td>
                                                </tr>
                                                <tr>
                                                    <td style="font-weight: bold;">BLOCK:</td>
                                                    <td><%#Eval("BLK_CODE")%></td>
                                                </tr>
                                                <tr>
                                                    <td style="font-weight: bold;">DISTRICT:</td>
                                                    <td><%#Eval("DIS_CODE")%></td>
                                                </tr>
                                                <tr>
                                                </tr>
                                            </table>
                                        </td>
                                        <td class="contactPhoto" style="width: 25%">
                                            <asp:Image ID="Photo" ImageUrl="~/Images/Empty_DP.jpg" Shape="Square" runat="server" Height="120"></asp:Image>
                                            <asp:Button ID="btnShowPhoto" runat="server" Text="ShowPhoto" />
                                            <asp:Image ID="Sign" ImageUrl="~/Images/logo_sigclub.png" Shape="Wide" runat="server" Width="120" BackColor="WhiteSmoke"></asp:Image>
                                            <asp:Button ID="btnShowSign" runat="server" Text="ShowSign" />

                                        </td>
                                    </tr>

                                </table>


                            </ItemTemplate>
                        </asp:ListView>

                    </div>
                </div>
            </div>
        </div>






        <%--<div class="col-lg-4">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        Account Holder Info
                    </div>
                     <div class="panel-body">
                        <div class="row">
                         <div class="form-group">
                             <div class="col-md-12">
                                 <asp:Image ID="Image1" runat="server" />
                                 <asp:ImageButton ID="ImageButton1" runat="server" />
                                <br />
                             </div>
                             <div class="col-md-12">
                                    <label style="margin-right: 20PX;">SL_CODE</label>
                                      <asp:TextBox ID="txt_SLcode" runat="server" placeholder="ENTER A/C NO" ReadOnly="true" Font-Size="10" CssClass="form-control"/>
                                 <br />
                                </div>
                             <div class="col-md-12">
                                    <label style="margin-right: 20PX;">Cust Id</label>
                                      <asp:TextBox ID="txt_custid" runat="server" placeholder="ENTER A/C Type" ReadOnly="true" Font-Size="10" CssClass="form-control"/>
                                 <br />
                                </div>
                             <div class="col-md-12">
                                    <label style="margin-right: 20PX;">Name</label>
                                      <asp:TextBox ID="txt_name" runat="server" placeholder="A/C Status" ReadOnly="true" Font-Size="10" CssClass="form-control"/>
                                 <br />
                                </div>
                             <div class="col-md-12">
                                    <label style="margin-right: 20PX;">Guardian Name</label>
                                      <asp:TextBox ID="txtGuarname" runat="server" placeholder="dd/mm/yyyy" ReadOnly="true" Font-Size="10" CssClass="form-control"/>
                                 <br />
                                </div>
                             <div class="form-group">
                              <div class="col-md-10">
                                    <label style="margin-right: 20PX;">Village Code</label>
                                      <asp:TextBox ID="txtvillcode" runat="server" placeholder="Enter Total Balance" ReadOnly="true" Font-Size="10" CssClass="form-control"/>
                                </div>
                                 <div class="col-md-2">
                                    <label style="margin-right: 20PX;"></label>
                                      <asp:TextBox ID="TextBox8" runat="server" placeholder="" Font-Size="10" ReadOnly="true" CssClass="form-control"/>
                                     <br />
                                </div>
                             </div>
                             <div class="form-group">
                              <div class="col-md-10">
                                    <label style="margin-right: 20PX;">Block Code</label>
                                      <asp:TextBox ID="txtblkcode" runat="server" placeholder="ENTER ACTUAL BALANCE" ReadOnly="true" Font-Size="10" CssClass="form-control"/>
                                </div>
                                  <div class="col-md-2">
                                    <label style="margin-right: 20PX;"></label>
                                      <asp:TextBox ID="TextBox9" runat="server" placeholder="" ReadOnly="true" Font-Size="10" CssClass="form-control"/>
                                      <br />
                                </div>
                                 </div>
                             <div class="form-group">
                              <div class="col-md-10">
                                    <label style="margin-right: 20PX;">Dist Code</label>
                                      <asp:TextBox ID="txtdistcode" runat="server" placeholder="ENTER SHADOW BALANCE" Font-Size="10" ReadOnly="true" CssClass="form-control"/>
                                </div>
                                  <div class="col-md-2">
                                    <label style="margin-right: 20PX;"></label>
                                      <asp:TextBox ID="TextBox10" runat="server" placeholder="" Font-Size="10" CssClass="form-control" ReadOnly="true"/>
                                </div>
                                 </div>
                         </div>
                            <div class="form-group">
                                <div class="col-md-12">
                                    
                                </div>
                            </div>
                            
                               
                        </div>

                     </div>
                </div>
            </div>--%>
    </div>
    <div class="row">
        <div class="col-lg-12">
            <div style="float: right; margin-top: 12px;">
                <asp:Button ID="btnsubmit" runat="server" Text="Calculate Maturity Amount" class="btn btn-primary" OnClick="btnsubmit_Click" Visible="False"/>
                <asp:Button ID="btn_CloseAccount2" runat="server" Text="CloseAccount" class="btn btn-primary" OnClick="btn_CloseAccount2_Click" Visible="false" />
                <a href="frmAccountClosing.aspx" class="btn btn-outline btn-danger">Cancel</a>

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

