<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/ProjectBM.Master" CodeBehind="frmDetailsDepositReport.aspx.cs" Inherits="iBankingSolution.Report.frmDetailsDepositReport" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $('#dataTablesexample').DataTable({
                responsive: true
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <form id="formInitialSetting" runat="server">
        <asp:ScriptManager ID="scrpt1" runat="server" EnablePageMethods="true"></asp:ScriptManager>
        <div class="row">
            <div class="col-lg-12">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        Daily Deposit Report
                    </div>
                    <div class="panel-body">
                        <div class="row">

                            <div class="form-group">

                                <%--  <div class="col-md-6" visible="false">
                                    <label style="margin-right: 20PX;">Select Branch</label>
                                    
                                       <asp:DropDownList ID="cmbx_Branch" runat="server" CssClass="form-control"> </asp:DropDownList>
                                </div>--%>
                                <div class="col-md-4">
                                    <label style="margin-right: 20PX;">Account Type</label>
                                    <asp:DropDownList ID="cmbx_AcctType" runat="server" CssClass="form-control" placeholder="Select Ac Type">
                                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                                        <asp:ListItem Value="s">Savings</asp:ListItem>
                                        <asp:ListItem Value="cc">Deposit Certificate</asp:ListItem>
                                        <asp:ListItem Value="fd">Fixed Deposite</asp:ListItem>
                                        <asp:ListItem Value="r">Recurring Deposite</asp:ListItem>
                                        <asp:ListItem Value="d">Home Savings</asp:ListItem>
                                        <asp:ListItem Value="jlg">JLG Deposite</asp:ListItem>
                                        <asp:ListItem Value="shg">SHG Deposite</asp:ListItem>
                                        <asp:ListItem Value="mis">MIS Deposite</asp:ListItem>
                                        <asp:ListItem Value="sus">Suspense Deposite</asp:ListItem>
                                        <asp:ListItem Value="nf">No Frill Deposite</asp:ListItem>



                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-4">
                                    <label style="margin-right: 20PX;">Select Ledger</label>
                                    <asp:DropDownList ID="cmbx_Ledger" runat="server" CssClass="form-control" placeholder="Select Ledger" AutoPostBack="True" OnSelectedIndexChanged="cmbx_Ledger_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                               
                                <div class="col-md-4">
                                    <label style="margin-right: 20PX;">As On Date</label>
                                    <asp:TextBox ID="dtpkr_DateAsOn" CssClass="form-control input-sm BootDatepicker" runat="server" onFocus="this.select()" autocomplete="off" required="required"></asp:TextBox>
                                </div>
                                 <div class="clearfix"></div>
                                <div class="col-md-2">
                                     <br />
                                  <asp:Button ID="btnShow" runat="server" Text="Show Report" class="btn btn-primary" OnClick="btnShow_Click" />
                                </div>
                               
                                <div  class="col-md-2">
                                    <br />
                                    
                                    <asp:DropDownList ID="ddlExportReport" runat="server" CssClass="form-control" class="styled-select"  BackColor="White" ForeColor="Black">
                                        <asp:ListItem Value="0">WORD</asp:ListItem>
                                        <asp:ListItem Value="1">PDF</asp:ListItem>
                                        <asp:ListItem Value="2">EXCEL</asp:ListItem>
                                    </asp:DropDownList>

                                </div>
                                <div  class="col-md-2"><br />
                                    <asp:Button ID="btnDownload" runat="server" Text="Download" CssClass="form-control" OnClick="btnDownload_Click" />

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
                <div class="panel panel-default">
                    <div class="panel-heading">
                        Daily Deposit List               
                    </div>
                    <div class="panel-body">

                        <%--<div style="overflow: scroll;">--%>
                        <table width="100%" class="table table-striped table-bordered table-hover" id="dataTablesexample">
                            <thead>
                                <tr>
                                    <th>Sr.</th>
                                    <th>A/c No</th>
                                    <th>Old Account No</th>
                                    <th>Name of the A/c</th>
                                    <th>Opening Date</th>
                                    <th>R.O.I(%)</th>
                                    <th>Duration </th>
                                    <th>Balance </th>
                                    <%-- <th>DEPOSIT AMOUNT</th>--%>
                                    <th>Maturity Amt</th>
                                    <th>Maturity Date</th>

                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="RepCCList" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblsrL" runat="server" Text='<%# (((RepeaterItem)Container).ItemIndex+1).ToString()%>'></asp:Label>


                                            </td>

                                            <td>

                                                <asp:Label ID="lblAcNo" runat="server" Text='<%# Eval("A/c No")%>'></asp:Label>

                                            </td>

                                            <td>

                                                <asp:Label ID="lblold_acno" runat="server" Text='<%# Eval("Old A/c No")%>'></asp:Label>

                                            </td>
                                            <td>

                                                <asp:Label ID="lblacctype" runat="server" Text='<%# Eval("Name of the A/c Holder")%>'></asp:Label>

                                            </td>
                                            <td>

                                                <asp:Label ID="lbldateofopening" runat="server" Text='<%# Eval("Opening Date")%>'></asp:Label>

                                            </td>

                                            <td>

                                                <asp:Label ID="LblROI" runat="server" Text='<%# Eval("ROI")%>'></asp:Label>

                                            </td>

                                            <td>

                                                <asp:Label ID="lblduration" runat="server" Text='<%# Eval("Duration")%>'></asp:Label>

                                            </td>

                                            <td>

                                                <asp:Label ID="lblBalance" runat="server" Text='<%# Eval("balance")%>'></asp:Label>

                                            </td>

                                            <%--<td>

                                                <asp:Label ID="lblDEPOSIT_AMOUNT" runat="server" Text='<%# Eval("DEPOSIT_AMOUNT")%>'></asp:Label>

                                            </td>--%>


                                            <td>

                                                <asp:Label ID="lblMaturityAmt" runat="server" Text='<%# Eval("Maturity Amt")%>'></asp:Label>

                                            </td>

                                            <td>

                                                <asp:Label ID="LblMaturityDate" runat="server" Text='<%# Eval("Maturity Date")%>'></asp:Label>

                                            </td>

                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>

                            </tbody>
                        </table>




                        <%-- </div>--%>
                    </div>
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

    </form>
</asp:Content>
