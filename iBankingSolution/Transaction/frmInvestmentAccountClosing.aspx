<%@ Page  Language="C#" MasterPageFile="~/MasterPages/ProjectBM.Master" AutoEventWireup="true" CodeBehind="frmInvestmentAccountClosing.aspx.cs" Inherits="iBankingSolution.Transaction.frmInvestmentAccountClosing" %>
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


     <asp:ScriptManager ID="scrpt1" runat="server"></asp:ScriptManager>
    <div class="row">
        <div class="col-lg-12">
            <div style="float: right; margin-top: 12px;">
                <asp:Button ID="btnsubmit1" runat="server" Text="Calculate Maturity Amount" class="btn btn-primary" OnClick="btnsubmit_Click" Visible="False" />   <%--OnClick="txtAcNo_TextChanged"--%>
                <asp:Button ID="btn_CloseAccount" runat="server" Text="CloseAccount" class="btn btn-primary"  Visible="False" OnClick="btn_CloseAccount2_Click" />   <%--OnClick="btn_CloseAccount_Click"--%>
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" class="btn btn-primary" OnClick="btnCancel_Click" />    <%----%>
            </div>
            <h1 class="page-header">Investment Account Closing</h1>
        </div>

    </div>
    <div class="row">
        <div class="col-lg-12">
            <div class="panel panel-primary">
                <div class="panel-heading" style="font-size:larger;">
                     Investment Account Closing Entry
                </div>
                <div class="panel-body">
                    <div class="row">

                        <div class="form-group">
                            <div class="col-md-12">
                                <div class="panel panel-primary">

                                    <div class="panel-heading text-center" style="font-size:larger;">
                                        <b>Investment Account Details</b>

                                    </div>
                                </div>
                            </div>
                            <div class="col-md-1">
                                <asp:Label ID="lblSession" runat="server" BackColor="White" ForeColor="White"></asp:Label>
                            </div>
                            <div class="clearfix"></div>
                            <div class="col-md-2">
                                <label style="margin-right: 20PX;font-size:larger;">Old A/c No</label>
                                <asp:TextBox ID="txtOldAcNo" placeholder="ENTER OLD A/C NO" Font-Size="10" runat="server" CssClass="form-control" AutoPostBack="True"  OnTextChanged="txtOldAcNo_TextChanged"/>   <%----%>
                            </div>
                            <div class="col-md-2">
                                <label style="margin-right: 20PX;font-size:larger;">A/c No</label>
                                <asp:TextBox ID="txtAcNo" placeholder="ENTER A/C NO" Font-Size="10" runat="server" CssClass="form-control" AutoPostBack="True"  Width="100px" OnTextChanged="txtAcNo_TextChanged"/>   <%----%>
                            </div>
                            <div class="col-md-2">
                                <label style="margin-right: 20PX;font-size:larger;">Opening DT</label>
                                <asp:TextBox ID="txtdtOpening" placeholder="DD/MM/YYYY" Font-Size="10" ReadOnly="true" CssClass="form-control" runat="server" onFocus="this.select()" autocomplete="off" Width="120px"></asp:TextBox>

                            </div>
                           
                            <div class="col-md-3">
                                <label style="margin-right: 20PX;font-size:larger;">AcType</label>
                                <asp:TextBox ID="txtacctype" placeholder="Accont Type" Font-Size="10" ReadOnly="true" CssClass="form-control" runat="server"  AutoPostBack="True" OnTextChanged="txtacctype_TextChanged"></asp:TextBox>  <%--OnTextChanged="txtacctype_TextChanged"--%>

                            </div>
                            <div class="col-md-3">
                                <label style="margin-right: 20PX;font-size:larger;">Last Trans.Dt</label>
                                <asp:TextBox ID="txtdtLastTrnsDt" placeholder="DD/MM/YYYY" Font-Size="10" ReadOnly="true" CssClass="form-control" runat="server" onFocus="this.select()" autocomplete="off"></asp:TextBox>

                            </div>
                            <div class="clearfix"></div>
                        </div>
                        <div class="clearfix"></div>
                        <asp:Panel runat="server" ID="PanelCertificateTwo" Visible="false">
                            <div class="form-group">
                                <div class="col-md-12">
                                    <div class="panel panel-primary">

                                        <div class="panel-heading text-center" style="font-size:larger;">
                                            <b>Certificate Details</b>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <label style="margin-right: 20PX;font-size:larger;">ROI</label>
                                    <asp:TextBox ID="ntxt_RenewalROI" runat="server" placeholder="ENTER ROI" Font-Size="10" CssClass="form-control" AutoPostBack="True" required="required"  Enable="false"/>

                                </div>
                                <div class="col-md-3">
                                    <label style="margin-right: 20px;font-size:larger;">Maturity Date</label>
                                    <asp:TextBox ID="dtpkr_RenewalMatDt" placeholder="DD/MM/YYYY" Font-Size="10" CssClass="form-control input-sm BootDatepicker" runat="server" onFocus="this.select()" autocomplete="off" required="required" Enabled="false"></asp:TextBox>
                                </div>
                                <div class="col-md-3">
                                    <label style="margin-right: 20px;font-size:larger;">Period(MM+DD)</label>
                                    <asp:TextBox ID="txt_RePrdinMonth" runat="server" Width="58px" Height="30" Font-Size="10" onkeypress="return isNumberKey(event)" AutoPostBack="True" MaxLength="2" Enabled="false"></asp:TextBox>
                                    <asp:TextBox ID="txt_RePrdInDays" runat="server" Width="58px" Height="30" Font-Size="10" AutoPostBack="True" onkeypress="return isNumberKey(event)" MaxLength="2" Enabled="false"></asp:TextBox>
                                </div>
                                 <div class="col-md-3">
                                    <label style="margin-right: 20PX;font-size:larger;">Maturity-Amount</label>
                                    <asp:TextBox ID="ntxt_RMaturityAmt" runat="server" placeholder="MATURITY AMT" Font-Size="10" CssClass="form-control" Enabled="False" />
                                    <br />
                                </div>
                                <div class="clearfix"></div>
                                <br />
                                <div class="col-md-3">
                                    <label style="margin-right: 20PX;font-size:larger;">Dep.Amt</label>
                                    <asp:TextBox ID="txt_RenewalDepotAmt" runat="server" placeholder="DEPOSITE AMT" Font-Size="10" CssClass="form-control" onkeypress="return isNumberKey(event)" AutoPostBack="True" required="required" Enabled="false" />
                                </div>
                               <div class="col-md-3">
                                <label style="margin-right: 20PX;font-size:larger;">Interest Issued From</label>
                                <asp:TextBox ID="txtIntrissuedFrCode"  runat="server" Enabled="False" Height="28"></asp:TextBox>
                                
                            </div>
                                <div class="col-md-6">
                                    <label style="margin-right: 20px;font-size:larger;">Interested Issued From (Ledger Name)</label>
                                    <asp:TextBox ID="txtIntrissuedFrLedgerName" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                                </div>
                             
                            </div>
                        </asp:Panel> 

                        <div class="clearfix"></div><br />
                        <asp:Panel runat="server" ID="maturityPanel" Visible="false">
                            <div class="form-group">
                                <div class="col-md-12">
                                    <div class="panel panel-primary">

                                        <div class="panel-heading text-center" style="font-size:larger;">
                                            <b>Collection Data</b>

                                        </div>
                                    </div>
                                </div>
                                
                                <div class="col-md-3">
                                     <label style="margin-right: 20PX;font-size:larger;">Maturity Instruction</label>
                                    <asp:DropDownList ID="ddlMaturityInst" runat="server" Filter="Contains" MarkFirstMatch="true" EmptyMessage="Select" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="cmbx_Type_SelectedIndexChanged" Width="250px">
                                     
                                     <asp:ListItem Value="Select">-- Select Maturity Instruction --</asp:ListItem>
                                     
                                     <asp:ListItem Value="CASH WITHDRAWL">Cash Withdrawl</asp:ListItem>
                                     <asp:ListItem Value="A/C TRANSFER">A/C Transfer</asp:ListItem>
                                     </asp:DropDownList>
                                 </div>
                                <div class="col-md-1"></div>
                                <div class="col-md-3">
                                    <label style="margin-right: 20px;font-size:larger;">Transfer Account</label>
                                    <asp:DropDownList ID="cmbx_IntTransferAccountTo" runat="server" placeholder="Account Transfer" Font-Size="10" Width="250px" CssClass="form-control" Enabled="true"></asp:DropDownList>
                                    
                                </div>
                                <div class="col-md-1"></div>
                                <div class="col-md-3">
                                    <label style="margin-right: 20PX;font-size:larger;">Interest Adjusted</label>
                                    <asp:TextBox ID="txtIntAdj" runat="server" placeholder="INT ADJUSTED" Font-Size="10" ReadOnly="true" CssClass="form-control" Width="250px" />
                                </div>
                                <div class="clearfix"></div>
                                <br />
                                <div class="col-md-2">
                                    <label style="margin-right: 20px;font-size:larger;">TDS</label>
                                    <asp:TextBox ID="txttds" runat="server" placeholder="ENTER TDS" CssClass="form-control" Font-Size="10" width="250px"/>
                                </div>
                                <div class="col-md-2"></div>
                                <div class="col-md-3">
                                    <label style="margin-right: 20PX;font-size:larger;">MaturityAmt</label>
                                    <asp:TextBox ID="txt_MaturityAmt" runat="server" placeholder="MATURITY AMT" ReadOnly="true" Font-Size="10" CssClass="form-control" Width="250px" />
                                </div>
                                <div class="col-md-1"></div>
                                <div class="col-md-2">
                                    <label style="margin-right: 20PX;font-size:larger;">WithdrawalDt.</label>
                                    <asp:TextBox ID="dtpkr_WithdrawlDate" runat="server" placeholder="Withdrawal Dt" Font-Size="10" CssClass="form-control input-sm BootDatepicker" Width="250px" />
                                </div>


                            </div>
                        </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
       <div class="row">
        <div class="col-lg-12">
            <div style="float: right; margin-top: 12px;">
                <asp:Button ID="btnsubmit" runat="server" Text="Calculate Maturity Amount" class="btn btn-primary" OnClick="btnsubmit_Click" Visible="False"/> <%----%>
                <asp:Button ID="btn_CloseAccount2" runat="server" Text="CloseAccount" class="btn btn-primary"  Visible="false" OnClick="btn_CloseAccount2_Click" />   <%----%>
                <a href="frmInvestmentAccountClosing.aspx" class="btn btn-outline btn-danger">Cancel</a>

            </div>
        </div>
        <!-- /.col-lg-12 -->
    </div>
    



     <%-- Scripting Section for calender--%>
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
