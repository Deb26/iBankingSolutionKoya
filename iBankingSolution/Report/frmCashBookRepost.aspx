<%@ Page  Language="C#" MasterPageFile="~/MasterPages/ProjectBM.Master" AutoEventWireup="true" CodeBehind="frmCashBookRepost.aspx.cs" Inherits="iBankingSolution.Report.frmCashBookRepost" %>

<%@ Register TagPrefix="Ajax" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.4.1/js/bootstrap-datepicker.min.js"></script>
         <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.4.1/css/bootstrap-datepicker3.css"/>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#dataTablesexample').DataTable({
                responsive: true
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="clearfix"></div>
    <br /><br />
    <asp:ScriptManager ID="scrpt1" runat="server" EnablePageMethods="true"></asp:ScriptManager>
        <div class="row">
            <div class="col-lg-12">
                <div class="panel panel-primary">
                    <div class="panel-heading" style="font-size:larger;">
                        Cash Book Report
                    </div>
                    <div class="panel-body">
                        <div class="row">

                            <div class="form-group">
                                <div class="col-md-4">
                                    <label style="margin-right: 20PX;font-size:larger;">Branch</label>
                                    <asp:DropDownList ID="cmbx_Branch" runat="server" CssClass="form-control" placeholder="Select Branch Name" Font-Size="10">
                                </asp:DropDownList>
                                </div>
 
                               
                                <div class="col-md-4">
                                    <label style="margin-right: 20PX; font-size:larger;">Date</label>
                                    <asp:TextBox ID="dtpkr_DateAsOn" CssClass="form-control input-sm BootDatepicker" placeholder="dd/MM/yyyy" runat="server" onFocus="this.select()" autocomplete="off" required="required" SelectedDate="<%# DateTime.Today %>"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                     <br />
                                     <asp:Button ID="btnShow" runat="server" Text="Show" class="btn btn-primary" OnClick="btnShow_Click" /> <%----%>
                                  <asp:Button ID="btnDownload" runat="server" Text="Download" class="btn btn-primary" OnClick="btnDownload_Click"/>  <%----%>
                                </div>
                                <div  class="col-md-2">
                                    <br />
                                    
                                    <asp:DropDownList ID="ddlExportReport" runat="server" CssClass="form-control" class="styled-select"  BackColor="White" ForeColor="Black" Font-Size="10">
                                        <asp:ListItem Value="0">WORD</asp:ListItem>
                                        <asp:ListItem Value="1">PDF</asp:ListItem>
                                        <asp:ListItem Value="2">EXCEL</asp:ListItem>
                                    </asp:DropDownList>

                                </div>

                            </div>
                        </div>
                    </div>
                    </div>
                </div>
            <br />
            <br />
            <div class="row">
            <div class="col-lg-12">
                <br /><br />
                <div class="panel panel-primary">
                    <div class="panel-heading" style="font-size:larger">
                      Cash Book Report              
                    </div>
                    <div class="panel-body">

                        <div style="overflow: scroll;" runat="server" id="RepeaterControls"> 
                         <table width="100%" class="table table-striped table-bordered table-hover" id="dataTablesexample">
                            <thead>
                                <tr>
                                    <%--<th>Sr.</th>--%>
                                    <th>Particulars</th>
                                    <th>Ledger Folio</th>
                                    <th>SubLedger Folio</th>
                                    <th>Voucher No</th>
                                    <th>Receipt Amount</th>
                                    <th>Payment Amount </th> 
                                    <th></th>
                                    <th></th>
                                    <%--<th>total</th>--%>
                                    
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="RepCCList" runat="server" OnItemDataBound="RepCCList_ItemDataBound">  
                                    <ItemTemplate>
                                        <tr>
                                            

                                            <td>

                                                <asp:Label ID="lblParticular" runat="server" Text='<%# Eval("NOMENCLATURE")%>'></asp:Label>

                                            </td>

                                            <td>

                                                <asp:Label ID="lblLdgCode" runat="server" Text='<%# Eval("Ldg_code")%>'></asp:Label>

                                            </td>
                                            <td>

                                                <asp:Label ID="lblSubLedger" runat="server" Text='<%# Eval("Sl_Code")%>'></asp:Label>

                                            </td>
                                            <td>

                                                <asp:Label ID="lblVOUCHER_NO" runat="server" Text='<%# Eval("VOUCHER_NO")%>'></asp:Label>

                                            </td>

                                            <td>

                                                <asp:Label ID="LblAMT_Payment" runat="server" Text='<%# Eval("AMT_CREDIT")%>'></asp:Label>

                                            </td>

                                               <td>

                                                <asp:Label ID="lblTrReceipt" runat="server" Text='<%# Eval("AMT_DEBIT")%>'></asp:Label>

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
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>

                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label1" runat="server" Text="Total" style="font-weight: 700"></asp:Label>

                                    </td>

                                    <td></td>
                                    <td></td>
                                    <td></td>
                                      <td><asp:Label ID="lblReceipttotal" runat="server" Text="" style="font-weight: 700"></asp:Label></td>

                                    <td><asp:Label ID="lblPaymenttotal" runat="server" Text="" style="font-weight: 700"></asp:Label></td>
                                    <%--<td><strong><asp:Label ID="lbltrrecAmt" runat="server" Text="" style="font-weight: 700"></asp:Label></strong></td>
                                     <td><strong><asp:Label ID="lbltrPayAmt" runat="server" Text="" style="font-weight: 700"></asp:Label></strong></td>
                                    --%>

                                </tr>
                                <tr>
                                    <td>
                                        <strong>Opening/Closing</strong></td>

                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td><asp:Label ID="lblopeningbal" runat="server" Text='<%# Eval("OpeningBal")%>' style="font-weight: 700"></asp:Label></td>

                                     <td><asp:Label ID="lblclosingBal" runat="server" Text='<%# Eval("ClosingBal")%>' style="font-weight: 700"></asp:Label></td>
                                   <%-- <td>&nbsp;</td>
                                    <td>&nbsp;</td>--%>

                                </tr>
                                <tr>
                                    <td>
                                        <strong>Grand Total</strong></td>

                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                     <td><asp:Label ID="lblGRTReceipt" runat="server"  style="font-weight: 700"></asp:Label></td>

                                     <td><asp:Label ID="lblGRTPayment" runat="server"  style="font-weight: 700"></asp:Label></td>
                                   <%-- <td>&nbsp;</td>
                                    <td>&nbsp;</td>--%>

                                </tr>
                            </tbody>
                        </table> 
                        </div>
                        </div>
                    </div>
                </div>
         
            </div>

    <%--Scripting For Calender--%>
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
