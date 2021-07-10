<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/ProjectBM.Master" CodeBehind="frmTrailBalance.aspx.cs" Inherits="iBankingSolution.Report.frmTrailBalance" %>

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
                        Trail Balance
                    </div>
                    <div class="panel-body">
                        <div class="row">

                            <div class="form-group">

                                <div class="clearfix"></div>
                                <div class="col-md-2">
                                    <label style="margin-right: 20PX;">Branch</label>

                                    <asp:DropDownList ID="cmbx_Branch" runat="server" CssClass="form-control"></asp:DropDownList>

                                </div>

                                <div class="col-md-2">
                                    <label style="margin-right: 20PX;">From Date:</label>

                                    <asp:TextBox ID="dtpkr_frmDate" CssClass="form-control input-sm BootDatepicker" runat="server" onFocus="this.select()" autocomplete="off" required="required"></asp:TextBox>

                                </div>

                                <div class="col-md-2">

                                    <label style="margin-right: 20PX;">To Date:</label>

                                    <asp:TextBox ID="dtpkr_toDate" CssClass="form-control input-sm BootDatepicker" runat="server" onFocus="this.select()" autocomplete="off" required="required"></asp:TextBox>

                                </div>


                                <div class="col-md-4">
                                    <label style="margin-right: 20PX;"></label>
                                    <asp:Button ID="btnShow" runat="server" Text="Liabilities/Income" class="btn btn-primary" OnClick="btnShow_Click" />
                                     <asp:Button ID="btnShow1" runat="server" Text="Assets/Expenditure" class="btn btn-primary" OnClick="btnShow1_Click" />

                                </div>
                                 <div class="clearfix"></div>
                               
                                <div class="col-md-2">


                                    <asp:DropDownList ID="ddlExportReport" runat="server" CssClass="form-control" class="styled-select" BackColor="White" ForeColor="Black">
                                        <asp:ListItem Value="0">WORD</asp:ListItem>
                                        <asp:ListItem Value="1">PDF</asp:ListItem>
                                        <asp:ListItem Value="2">EXCEL</asp:ListItem>
                                    </asp:DropDownList>

                                </div>
                                 <div class="col-md-2">
                                    <label style="margin-right: 20PX;"></label>
                                    <asp:Button ID="btnDownload" runat="server" Text="Download" class="btn btn-primary" OnClick="btnDownload_Click" />
                                </div>

                               
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
                        <asp:Label ID="Label2" runat="server" Text="Statement"></asp:Label>           
                    </div>
                    <div class="panel-body">

                        <%--<div style="overflow: scroll;">--%>
                        <table width="100%" class="table table-striped table-bordered table-hover" id="dataTablesexample">
                            <thead>
                                <tr>
                                   <%-- <th>Sr.</th>--%>
                                    <th>Ldgcode</th>
                                    <th>LedgerName</th>
                                    <th>Opening Balance</th>
                                     <th>Total Debit</th>
                                    <th>Total Credit</th>
                                  
                                    <th>Closing Balance </th>
                                    <%-- <th>Balance </th>
                                    <%-- <th>DEPOSIT AMOUNT</th>
                                    <th>Maturity Amt</th>
                                    <th>Maturity Date</th> --%>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="RepCCList" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <%--<td>
                                                <asp:Label ID="lblsrL" runat="server" Text='<%# (((RepeaterItem)Container).ItemIndex+1).ToString()%>'></asp:Label>


                                            </td>--%>

                                            <td>

                                                <asp:Label ID="LblLdgcode" runat="server" Text='<%# Eval("LDG_CODE")%>'></asp:Label>

                                            </td>

                                            <td>

                                                <asp:Label ID="lblLedger" runat="server" Text='<%# Eval("NOMENCLATURE")%>'></asp:Label>

                                            </td>
                                            <td>

                                                <asp:Label ID="lblOpBal" runat="server" Text='<%# Eval("OPENINGBAL")%>'></asp:Label>

                                            </td>
                                            <td>

                                          
                                                <asp:Label ID="lblPayment" runat="server" Text='<%# Eval("PAYMENT")%>'> </asp:Label>

                                            </td>

                                            <td>

                                                      <asp:Label ID="lblReceipt" runat="server" Text='<%# Eval("Receipt")%>'></asp:Label>

                                            </td>
                                            <td>

                                                <asp:Label ID="lblclosing" runat="server"> </asp:Label>

                                            </td>



                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>

                                 <tr>
                                    <td>
                                        &nbsp;</td>

                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>

                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>

                                </tr>
                                <tr>
                                    <td>
                                        <strong>Grand</strong>
                                        <asp:Label ID="Label1" runat="server" Text="Total" style="font-weight: 700"></asp:Label>

                                    </td>

                                    <td></td>
                                    <td><asp:Label ID="lblOpeningTot" runat="server" Text="" style="font-weight: 700"></asp:Label></td>
                                    <td><asp:Label ID="lblPaymentTotal" runat="server" Text="" style="font-weight: 700"></asp:Label></td>
                                    <td><asp:Label ID="lblReceiptTotal" runat="server" Text="" style="font-weight: 700"></asp:Label></td>

                                    <td><strong><asp:Label ID="lblClosingTot" runat="server" Text="" style="font-weight: 700"></asp:Label></strong></td>
                                    <td>&nbsp;</td>
                                     <td>&nbsp;</td>
                                    

                                </tr>
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

 
</asp:Content>
