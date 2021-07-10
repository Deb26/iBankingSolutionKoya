<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/ProjectBM.Master" CodeBehind="frmGeneralLedgerReport.aspx.cs" Inherits="iBankingSolution.Report.frmGeneralLedgerReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <%--<script type="text/javascript">
        $(document).ready(function () {
            $('#dataTablesexample').DataTable({
                responsive: true
            });
        });
    </script>--%>
    <%--<style type="text/css">
        .auto-style1 {
            width: 146px;
        }
        .auto-style2 {
            width: 70px;
        }
        .auto-style3 {
            width: 178px;
        }
        .auto-style4 {
            width: 71px;
        }
    </style>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

     
        <asp:ScriptManager ID="scrpt1" runat="server" EnablePageMethods="true"></asp:ScriptManager>
        <div class="row">
            <div class="col-lg-12">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        General Ledger Report
                    </div>
                    <div class="panel-body">
                        <div class="row">

                            <div class="form-group">

                                <%--  <div class="col-md-6" visible="false">
                                    <label style="margin-right: 20PX;">Select Branch</label>
                                    
                                       <asp:DropDownList ID="cmbx_Branch" runat="server" CssClass="form-control"> </asp:DropDownList>
                                </div>--%>
                                <div class="col-md-2">
                                    <label style="margin-right: 20PX;">Ledger Code</label>
                                    <asp:DropDownList ID="cmbx_LedgerCode" runat="server" CssClass="form-control" Font-Size="10" placeholder="Select Ledger Code" AutoPostBack="True" OnSelectedIndexChanged="cmbx_LedgerCode_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>


                                <div class="col-md-6">
                                    <label style="margin-right: 20PX;">Ledger Name</label>
                                    <asp:DropDownList ID="Cmbx_LedgerName" runat="server" Font-Size="10" CssClass="form-control" placeholder="Select Ledger Name" AutoPostBack="True" OnSelectedIndexChanged="cmbx_LedgerName_SelectedIndexChanged">
                                    </asp:DropDownList>

                                </div>
                                 <div class="col-md-3">
                                     <label style="margin-right: 20px;">Branch</label>
                                     <asp:DropDownList ID="cmbx_Branch" runat="server" Font-Size =" 10" CssClass="form-control" placeholder="Branch name"></asp:DropDownList>
                                 </div>

                                  
                                    <div class="clearfix"></div><br />

                                    <div class="col-md-2">
                                        <label style="margin-right: 20PX;">From Date:</label>

                                         <asp:TextBox ID="dtpkr_frmDate" CssClass="form-control input-sm BootDatepicker" placeholder="dd/MM/yyyy" Font-Size="10" runat="server" onFocus="this.select()" autocomplete="off" required="required"></asp:TextBox>

                                    </div>

                                         <div class="col-md-2">
                                        <label style="margin-right: 20PX;">To Date:</label>

                                         <asp:TextBox ID="dtpkr_toDate" CssClass="form-control input-sm BootDatepicker" placeholder="dd/MM/yyyy" Font-Size="10"  runat="server" onFocus="this.select()" autocomplete="off" required="required"></asp:TextBox>

                                    </div>

                                    <div class="col-md-2">
                                        <br />
                                        
                                         <asp:Button ID="btnShow" runat="server" Text="Show" class="btn btn-primary" OnClick="btnShow_Click" />
                                         
                                    </div>
                                    <div class="col-md-2"></div>
                                     <div class="col-md-2">
                                        <br />

                                        <asp:DropDownList ID="ddlExportReport" runat="server" CssClass="form-control" class="styled-select" BackColor="White" ForeColor="Black" Font-Size="10">
                                            <asp:ListItem Value="0">WORD</asp:ListItem>
                                            <asp:ListItem Value="1">PDF</asp:ListItem>
                                            <asp:ListItem Value="2">EXCEL</asp:ListItem>
                                        </asp:DropDownList>

                                    </div>
                                    <div class="col-md-2"><br />
                                        <asp:Button ID="btnDownload" runat="server" Text="Download" class="btn btn-primary" OnClick="btnDownload_Click"  Visible="true"/>
                                    </div>

                              

                                    <div class="clearfix"></div>
                                    <hr />
                              <%--  <h4>Transaction Detils</h4>--%>
                                <div class="col-md-3" runat="server" id="Opening" visible="false">
                                    
                                      <label style="margin-right: 20PX;">Opening Balance</label>

                                         <asp:TextBox ID="txtOpeningBal" CssClass="form-control" placeholder=" OPENING BALANCE" Font-Size="10" runat="server" onFocus="this.select()" autocomplete="off" required="required" Enabled="False"></asp:TextBox>
                                 </div>
                                <div class="col-md-3" runat="server" id="debit" visible="false">
                                    
                                      <label style="margin-right: 20PX;">Debits</label>

                                         <asp:TextBox ID="txtDebit" CssClass="form-control" runat="server" placeholder="ENTER DEBIT BALANCE" Font-Size="10" onFocus="this.select()" autocomplete="off" required="required" Enabled="False"></asp:TextBox>
                                 </div>
                                  <div class="col-md-3" runat="server" id="Credit" visible="false">
                                    
                                      <label style="margin-right: 20PX;">Credits</label>

                                         <asp:TextBox ID="txtCredit" CssClass="form-control" runat="server" placeholder="ENTER CREDIT BALANCE" Font-Size="10" onFocus="this.select()" autocomplete="off" required="required" Enabled="False"></asp:TextBox>
                                 </div>

                                <div class="col-md-3" runat="server" id="ClBal" visible="false">
                                    
                                      <label style="margin-right: 20PX;">Closing Balance</label>

                                         <asp:TextBox ID="txtClosingBal" CssClass="form-control" placeholder="ENTER CLOSING BALANCE" Font-Size="10" runat="server" onFocus="this.select()" autocomplete="off" required="required" Enabled="False"></asp:TextBox>
                                 </div>

                                   

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
                        Statement               
                    </div>
                    
                    <br />
                    <table align="center" width="100%" height="50%">

                             
                          
                         <tr><td>&nbsp;</td> </tr>
                             <tr>
                                  <td></td>
                                  <%--<td align="left" class="auto-style2"><asp:Label ID="Label7" runat="server" Text="Adhar No:" style="font-weight: 700"></asp:Label></td>
                                 <td>
                                        <asp:Label ID="lblAdharNo" runat="server" Text="Adhar No:"></asp:Label> </td><td>&nbsp;&nbsp;&nbsp; </td>
                                  <td></td>--%>
                                 <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                     &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                     &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                     &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                 </td>
                                 <td align="left" class="auto-style1"><asp:Label ID="lblope" runat="server" Text="Opening Balance:" style="font-weight: 700; font-size:larger;"></asp:Label>
                                 <td>
                                        <asp:Label ID="lblopeningBal" runat="server"></asp:Label></td><td></td>
                                    <td></td><br />
                                 <td align="left" class="auto-style2"><asp:Label ID="lblcl" runat="server" Text="Closing Balance:" style="font-weight: 700; font-size:larger;"></asp:Label></td>
                                 <td>
                                        <asp:Label ID="lblClosingBal" runat="server" ></asp:Label> </td><td>&nbsp;&nbsp;&nbsp; </td>
                                  <td></td>
                                    <%--<td align="right" class="auto-style4"><asp:Label ID="Label9" runat="server" Text="Ac Status:" style="color: #FFFFFF"></asp:Label></td>
                                 <td>
                                        &nbsp;</td>--%>
  
                                </tr>
                    </table>
                    
                    <hr />
                    <div class="panel-body">

                        <%--<div style="overflow: scroll;">--%>
                        <table width="100%" class="table table-striped table-bordered table-hover" id="dataTables-example">
                            <thead>
                                <tr>
                                    <th>Sr.</th>
                                    <th>Dt Of Trans</th>
                                    <th>Payment/Debit</th>
                                    <th>Receipt/Credit</th>
                                    <th>Balance </th>
                                    <th></th>
                                    <%--<th>Closing Balance</th>--%>
                                   <%-- <th>Balance</th>
                                    <th>Dr/Cr</th>--%>
                                    <%--<th>Duration </th>
                                    <th>Balance </th>
                                    <%-- <th>DEPOSIT AMOUNT</th>
                                    <th>Maturity Amt</th>
                                    <th>Maturity Date</th> --%>

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

                                                <%--<asp:Label ID="lblTransDate" runat="server" Text='<%# Eval("date_from", "{0:dd MMM yyyy}")%>'></asp:Label>--%>
                                                <asp:Label ID="lblTransDate" runat="server" Text='<%# Eval("DATE_FROM", "{0:dd MMM yyyy}")%>'></asp:Label>

                                            </td>

                                            <td>

                                                <asp:Label ID="lblpayment" runat="server" Text='<%# Eval("payment")%>'></asp:Label>

                                            </td>
                                            <td>

                                                <asp:Label ID="lblreceipt" runat="server" Text='<%# Eval("RECEIPT")%>'></asp:Label>

                                            </td>
                                            <td>

                                                <asp:Label ID="lblbalance" runat="server" Text='<%# Eval("Balance")%>'></asp:Label>

                                            </td>
                                            <td>

                                                <asp:Label ID="Label1"  runat="server" Visible="false"  Text='<%# Eval("ClosingBalance")%>'></asp:Label>

                                            </td>
                                             <%--<td>

                                                <asp:Label ID="lblDrCr" runat="server" Text='<%# Eval("DrCr")%>'> </asp:Label>

                                            </td>--%>
                                            
                                            <%--<td>

                                                <asp:Label ID="lblbalance" runat="server" Text='<%# Eval("Balance")%>'></asp:Label>

                                            </td>

                                            <td>

                                                <asp:Label ID="lblDrCr" runat="server" Text='<%# Eval("DrCr")%>'> </asp:Label>

                                            </td>--%>

 

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
                                   
                                    

                                </tr>
                                <tr>
                                    <td></td>
                                    <td>
                                        <asp:Label ID="Label1" runat="server" Text="Total" style="font-weight: 700"></asp:Label>

                                    </td>
                                           <td><asp:Label ID="lblPaymenttotal" runat="server" Text="" style="font-weight: 700"></asp:Label></td>
                                       
                                        
                                      <td><asp:Label ID="lblReceipttotal" runat="server" Text="" style="font-weight: 700"></asp:Label></td>
                                    <td></td>
                                    <td></td>
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
