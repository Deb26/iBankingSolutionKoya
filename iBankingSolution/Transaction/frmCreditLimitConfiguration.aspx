<%@ Page Language="C#" MasterPageFile="~/MasterPages/ProjectBM.Master" AutoEventWireup="true" CodeBehind="frmCreditLimitConfiguration.aspx.cs" Inherits="iBankingSolution.Transaction.frmCreditLimitConfiguration" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
     <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.4.1/js/bootstrap-datepicker.min.js"></script>
         <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.4.1/css/bootstrap-datepicker3.css"/>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="scrpt1" runat="server"></asp:ScriptManager>
        <div class="row">
            <div class="col-lg-12">
                <div style="float: right; margin-top: 12px;">
                 </div>
                <h1 class="page-header">Credit Limit Configaration</h1>
            </div>
        </div>
        <div class="row">
            <div class="col-lg-12">
                <div class="panel panel-primary">
                    <div class="panel-heading" style="font-size:larger;">
                        Credit Limit Configaration
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="form-group">
                                <div class="col-md-3">
                                </div>
                                <div class="col-md-6">
                                    <asp:Label ID="Label2" runat="server" Text="*" Style="color: #FF3300"></asp:Label>
                                    <label style="margin-right: 20PX;Font-Size :larger;">Select Loan Scheme</label>
                                    <asp:DropDownList ID="cmbx_LoanScheme" CssClass="form-control" placeholder="SELECT LOAN SCHEME" Font-Size="10" runat="server" AutoPostBack="false"  EmptyMessage="Select Deposit Scheme"  required="true">
                                    </asp:DropDownList>
                               </div>
                                <div class="clearfix"></div><br />
                                <div class="col-md-3"></div>
                                <div class="col-md-3">
                                    <label style="margin-right: 20px;font-size:larger;">Per Decimal</label>
                                    <asp:TextBox ID="txtperdecimal" CssClass="form-control"  Font-Size="10" runat="server" autocomplete="off" ReadOnly="false"></asp:TextBox>
                                </div>
                                <div class="col-md-3">
                                    <label style="margin-right: 20px;font-size:larger;">Land Less Amount</label>
                                    <asp:TextBox ID="txtlandlessamount" CssClass="form-control"  Font-Size="10" runat="server" autocomplete="off" ReadOnly="false"></asp:TextBox>
                                </div>
                                <div class="clearfix"></div><br />
                                <div class="col-md-3"></div>
                                <div class="col-md-3">
                                    <label style="margin-right: 20px;font-size:larger;">Share %</label>
                                    <asp:TextBox ID="txtshare" CssClass="form-control"  Font-Size="10" runat="server" autocomplete="off" ReadOnly="false"></asp:TextBox>
                                </div>
                                <div class="col-md-3">
                                    <label style="margin-right: 20px;font-size:larger;">Insurance %</label>
                                    <asp:TextBox ID="txtinsurance" CssClass="form-control"  Font-Size="10" runat="server" autocomplete="off" ReadOnly="false"></asp:TextBox>
                                </div>
                                <div class="clearfix"></div><br />
                                <div class="col-md-3"></div>
                                <div class="col-md-3">
                                    <label style="margin-right: 20px;font-size:larger;">GAP</label>
                                    <asp:TextBox ID="txtgap" CssClass="form-control"  Font-Size="10" runat="server" autocomplete="off" ReadOnly="false"></asp:TextBox>
                                </div>
                                <div class="col-md-3">
                                    <label style="margin-right: 20px;font-size:larger;">ROI</label>
                                    <asp:TextBox ID="txtroi" CssClass="form-control"  Font-Size="10" runat="server" autocomplete="off" ReadOnly="false"></asp:TextBox>
                                </div>
                                <div class="clearfix"></div><br />
                                <div class="col-md-3"></div>
                                <div class="col-md-3">
                                    <label style="margin-right: 20px;font-size:larger;">OD ROI</label>
                                    <asp:TextBox ID="txtodroi" CssClass="form-control"  Font-Size="10" runat="server" autocomplete="off" ReadOnly="false"></asp:TextBox>
                                </div>
                                <div class="col-md-3">
                                    <label style="margin-right: 20px;font-size:larger;">Defaultter Allowed</label>
                                    <asp:TextBox ID="txtdefaulterallowed" CssClass="form-control"  Font-Size="10" runat="server" autocomplete="off" ReadOnly="false"></asp:TextBox>
                                </div>
                                <div class="clearfix"></div><br />
                                <div class="col-md-3"></div>
                                <div class="col-md-6">
                                    <label style="margin-right: 20px; font-size:larger;">Due Date</label>
                                    <asp:TextBox ID="txtduedate" CssClass="form-control input-sm BootDatepicker"  Font-Size="10" runat="server" autocomplete="off" ReadOnly="false"></asp:TextBox>
                                </div>
                            </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                    <div class="col-lg-7">
                        <div style="float: right; margin-top: 12px;">
                            <asp:Button ID="btnsubmit1" runat="server" Text="Save" class="btn btn-primary" OnClick="btnsubmit1_Click" />
                            <a href="frmCreditLimitConfiguration.aspx" class="btn btn-outline btn-danger">Reset</a>
                        </div>
                    </div>
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
