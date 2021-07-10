<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/ProjectBM.Master" CodeBehind="frmBRSReport.aspx.cs" Inherits="iBankingSolution.Report.frmBRSReport" %>

 <asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
      <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.4.1/js/bootstrap-datepicker.min.js"></script>
         <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.4.1/css/bootstrap-datepicker3.css"/>
    <script type="text/javascript">
        $ = jQuery.noConflict();
        $(function () {
            $('.select2').select2()
        })
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
   
         <asp:ScriptManager ID="scrpt1" runat="server"></asp:ScriptManager>
        <div class="row">
            <div class="col-lg-12">
                <div style="float: right; margin-top: 12px;">
                    <%--<asp:Button ID="btnsubmit1" runat="server" Text="Add" class="btn btn-primary" />--%>
           
                </div>
                <h1 class="page-header">BRS</h1>
            </div>
            <!-- /.col-lg-12 -->
        </div>
        <div class="row">
            <div class="col-lg-12">
                <div class="panel panel-primary">
                    <div class="panel-heading" style="font-size:larger;">
                         Bank Reconciliation Statement
                    </div>
                    <div class="panel-body">

                        <div class="row">

                            <div class="form-group">

                                <%--  <div class="col-md-6" visible="false">
                                    <label style="margin-right: 20PX;">Select Branch</label>
                                    
                                       <asp:DropDownList ID="cmbx_Branch" runat="server" CssClass="form-control"> </asp:DropDownList>
                                </div>--%>
                                <div class="col-md-2">
                                    <label style="margin-right: 20PX;font-size:larger;">Ledger Code</label>
                                    <asp:DropDownList ID="cmbx_LedgerCode" runat="server" CssClass="form-control" Font-Size="10" placeholder="Select Ledger Code" AutoPostBack="True" OnSelectedIndexChanged="cmbx_LedgerCode_SelectedIndexChanged" > <%----%>
                                    </asp:DropDownList>
                                </div>


                                <div class="col-md-6">
                                    <label style="margin-right: 20PX;font-size:larger;">Ledger Name</label>
                                    <asp:DropDownList ID="Cmbx_LedgerName" runat="server" Font-Size="10" CssClass="form-control" placeholder="Select Ledger Name" AutoPostBack="True" OnSelectedIndexChanged="cmbx_LedgerName_SelectedIndexChanged">   <%----%>
                                    </asp:DropDownList>

                                </div>
                                 <div class="col-md-3">
                                     <label style="margin-right: 20px;font-size:larger;">Branch</label>
                                     <asp:DropDownList ID="cmbx_Branch" runat="server" Font-Size =" 10" CssClass="form-control" placeholder="Branch name"></asp:DropDownList>
                                 </div>

                                  
                                    <div class="clearfix"></div><br />

                                    <div class="col-md-2">
                                        <label style="margin-right: 20PX;font-size:larger;">From Date:</label>

                                         <asp:TextBox ID="dtpkr_frmDate" CssClass="form-control input-sm BootDatepicker" placeholder="dd/MM/yyyy" Font-Size="10" runat="server" onFocus="this.select()" autocomplete="off" required="required"></asp:TextBox>

                                    </div>

                                         <div class="col-md-2">
                                        <label style="margin-right: 20PX;font-size:larger;">To Date:</label>

                                         <asp:TextBox ID="dtpkr_toDate" CssClass="form-control input-sm BootDatepicker" placeholder="dd/MM/yyyy" Font-Size="10"  runat="server" onFocus="this.select()" autocomplete="off" required="required"></asp:TextBox>

                                    </div>

                                    <div class="col-md-2">
                                        <br />
                                        
                                         <asp:Button ID="btnShow" runat="server" Text="Show" class="btn btn-primary" OnClick="btnShow_Click"/>   <%----%>
                                         
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
                                        <asp:Button ID="btnDownload" runat="server" Text="Download" class="btn btn-primary"   Visible="true"/> <%-- OnClick="btnDownload_Click"--%>
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
      
         <div class="col-lg-12">
            <div class="panel panel-primary">
                <div class="panel-heading" style="font-size:larger;">
                   BRS           
                </div>

                <div>
                    <div class="panel-body">


                        <%--<div style="overflow: scroll;" runat="server" id="RepeaterControls">--%>
                         <div style ="height:350px; width:1195px; overflow:auto;">
                            <table width="100%" class="table table-striped table-bordered table-hover" id="dataTablesexample">
                                <thead>
                                    <tr>
                                        <th>DT.OF TRANS</th>
                                        <th>TYPE OF TRANS</th>
                                        <th>TRANS DETAILS</th>
                                        <th>DEBIT</th>
                                        <th>CREDIT</th>
                                        <th>BALANCE</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="RepCCList" runat="server">
                                        <ItemTemplate>
                                            <tr>
                                                
                                                <td>

                                                    <asp:Label ID="lblAcNo" runat="server" Text='<%# Eval("DT")%>'></asp:Label>

                                                </td>

                                                <td>

                                                    <asp:Label ID="lblold_acno" runat="server" Text='<%# Eval("TYPE_OF_TRANS")%>'></asp:Label>

                                                </td>
                                                <td>

                                                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("narration")%>'></asp:Label>

                                                </td>
                                                <td>

                                                    <asp:Label ID="lblacctype" runat="server" Text='<%# Eval("DEBIT")%>'></asp:Label>

                                                </td>
                                                <td>

                                                <asp:Label ID="lbldateofopening" runat="server" Text='<%# Eval("CREDIT")%>'></asp:Label>

                                               </td>
                                                <td>

                                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("BALANCE")%>'></asp:Label>

                                               </td>

                                            
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>

                                </tbody>
                            </table>
                        </div>
                        


                        </div>
                    </div>
                </div>
            </div>
        <div class="row">
            <div class="col-lg-12">
                <div style="float: right; margin-top: 12px;">
                     
                    <a href="frmBRSReport.aspx" class="btn btn-outline btn-danger">Cancel</a>
                   
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