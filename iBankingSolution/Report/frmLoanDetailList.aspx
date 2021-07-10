<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/ProjectBM.Master" CodeBehind="frmLoanDetailList.aspx.cs" Inherits="iBankingSolution.Report.frmLoanDetailList" %>

 

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

     
        <asp:ScriptManager ID="scrpt1" runat="server" EnablePageMethods="true"></asp:ScriptManager>
        <div class="row">
            <div class="col-lg-12">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        Loan Detail List Report
                    </div>
                    <div class="panel-body">
                        <div class="row">

                            <div class="form-group">

                                <%--  <div class="col-md-6" visible="false">
                                    <label style="margin-right: 20PX;">Select Branch</label>
                                    
                                       <asp:DropDownList ID="cmbx_Branch" runat="server" CssClass="form-control"> </asp:DropDownList>
                                </div>--%>
                                 
                                <div class="col-md-3">
                                    <label style="margin-right: 20PX;">Select Loan Scheme</label>
                                    <asp:DropDownList ID="cmbx_Ledger" runat="server" CssClass="form-control" placeholder="Select Scheme" AutoPostBack="True" OnSelectedIndexChanged="cmbx_Ledger_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                                <div class="col-md-3">
                                    <label style="margin-right: 20PX;">Ledger Code</label>
                                    <asp:TextBox ID="txtLedgCode" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>

                               
                                <div class="col-md-3">
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
                                    <asp:Button ID="btnDownload" runat="server" Text="Download" class="btn btn-primary" OnClick="btnDownload_Click" />

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
                <div class="panel panel-primary">
                    <div class="panel-heading">
                       Detail List               
                    </div>
                    <div class="panel-body">

                        <div style="overflow: scroll;" runat="server" id="RepeaterControls"> 
                         <table width="100%" class="table table-striped table-bordered table-hover" id="dataTablesexample">
                            <thead>
                                <tr>
                                    <th>Sr.</th>
                                    <th>A/c No</th>
                                    <th>Old A/C No</th>
                                    <th>Name</th>
                                    <th>Disbursment Date</th>
                                    <th>Disbursment Amount</th>
                                    <th>Principal Out.</th>
                                    <th>Current Interest</th>
                                    <th>Od Interest</th>
                                    <th>Current Principal</th>
                                    <th>OD Principal</th>
                                  <%--  <th>Prin.Current </th> 
                                    <th>Prin.Overdue </th>
                                    <th>Prin.Demand </th>
                                    <th>Int.Current</th> 
                                     <th>Int.Overdue</th>
                                    <th>Int.Demand</th> 
                                     <th>Print Out</th> --%>
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

                                                <asp:Label ID="lblAcNo" runat="server" Text='<%# Eval("AcNo")%>'></asp:Label>

                                            </td>

                                            <td>

                                                <asp:Label ID="lblold_acno" runat="server" Text='<%# Eval("OldAcNo")%>'></asp:Label>

                                            </td>
                                            <td>

                                                <asp:Label ID="lblacctype" runat="server" Text='<%# Eval("Name")%>'></asp:Label>

                                            </td>
                                            <td>

                                                <asp:Label ID="lbldateofopening" runat="server" Text='<%# Eval("Disb_Date")%>'></asp:Label>

                                            </td>

                                            <td>

                                                <asp:Label ID="LblROI" runat="server" Text='<%# Eval("OutstandingAmt")%>'></asp:Label>

                                            </td>

                                              <%--<td>

                                                <asp:Label ID="lblduration" runat="server" Text='<%# Eval("Duration")%>'></asp:Label>

                                            </td>--%>

                                            <td>

                                                <asp:Label ID="lblBalance" runat="server" Text='<%# Eval("balance")%>'></asp:Label>

                                            </td>
                                            <td>

                                                <asp:Label ID="lblInterest" runat="server" Text='<%# Eval("cur_interest")%>'></asp:Label>

                                            </td>
                                            <td>

                                                <asp:Label ID="odint" runat="server" Text='<%# Eval("od_interest")%>'></asp:Label>

                                            </td>
                                            <td>

                                                <asp:Label ID="lblcurprin" runat="server" Text='<%# Eval("cur_prin")%>'></asp:Label>

                                            </td>
                                            <td>

                                                <asp:Label ID="lblodprin" runat="server" Text='<%# Eval("od_prin")%>'></asp:Label>

                                            </td>
                                             <%--<td>

                                                <asp:Label ID="lblInterest" runat="server" Text='<%# Eval("interest")%>'></asp:Label>

                                            </td>

                                            <td>

                                                <asp:Label ID="lblBalanceclosing" runat="server" Text='<%# Eval("ClosingBalance")%>'></asp:Label>

                                            </td> 


                                          <td>

                                                <asp:Label ID="lblMaturityAmt" runat="server" Text='<%# Eval("Maturity Amt")%>'></asp:Label>

                                            </td>

                                            <td>

                                                <asp:Label ID="LblMaturityDate" runat="server" Text='<%# Eval("Maturity Date")%>'></asp:Label>

                                            </td>--%>

                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>

                            </tbody>
                        </table> 
                        </div>
                           
            </div>
        </div>
        <%-- Scripting Section --%>
                </div>
            </div>
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
