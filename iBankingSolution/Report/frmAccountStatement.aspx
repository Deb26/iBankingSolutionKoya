<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/ProjectBM.Master"  CodeBehind="frmAccountStatement.aspx.cs" Inherits="iBankingSolution.Report.frmAccountStatement" %>

 
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $('#dataTablesexample').DataTable({
                responsive: true
            });
        });
    </script>
    <style type="text/css">
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
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

     
        <asp:ScriptManager ID="scrpt1" runat="server" EnablePageMethods="true"></asp:ScriptManager>
        <div class="row">
            <div class="col-lg-12">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        Account Statement Report
                    </div>
                    <div class="panel-body">
                        <div class="row">

                            <div class="form-group">

                             

                                <div class="col-md-3">
                                    
                                      <label style="margin-right: 20PX;">OLD A/C No.</label>

                                          <asp:TextBox ID="txt_OldAcctNo" runat="server" placeholder="OLD NO." Font-Size="10" CssClass="form-control" AutoPostBack="True" OnTextChanged="txt_OldAcctNo_TextChanged"></asp:TextBox>
                                 </div>

                                    <div class="col-md-3">
                                    
                                      <label style="margin-right: 20PX;"> A/C No.</label>

                                         <asp:TextBox ID="txt_AcctNo" runat="server" placeholder="ENTER AC NO." Font-Size="10" CssClass="form-control" AutoPostBack="false" OnTextChanged="txt_AcctNo_TextChanged"></asp:TextBox>
                                 </div>
                               
                              <div class="col-md-2">
                                        <label style="margin-right: 20PX;">From Date:</label>

                                         <asp:TextBox ID="dtpkr_frmDate" CssClass="form-control input-sm BootDatepicker" placeholder="dd/MM/yyyy" Font-Size="10" runat="server" onFocus="this.select()" autocomplete="off"></asp:TextBox>

                                    </div>

                                         <div class="col-md-2">
                                        <label style="margin-right: 20PX;">To Date:</label>

                                         <asp:TextBox ID="dtpkr_toDate" CssClass="form-control input-sm BootDatepicker" placeholder="dd/MM/yyyy" Font-Size="10"  runat="server" onFocus="this.select()" autocomplete="off"></asp:TextBox>

                                    </div>
                                  <div class="col-md-2">
                                     <label style="margin-right: 10PX; top: 150px;"><font color="white"> Options:</font></label>   
                                  <asp:Button ID="btnShow" runat="server" Text="Show" class="btn btn-primary" OnClick="btnShow_Click" />
                                </div>
                               
                                <div class="clearfix"></div>
                                 
                                 
                                    <div  class="col-md-3">
                                    <label style="margin-right: 20PX;">Download Options:</label>               
                                    <asp:DropDownList ID="ddlExportReport" runat="server" CssClass="form-control" class="styled-select" Font-Size="10" BackColor="White" ForeColor="Black">
                                        <asp:ListItem Value="0">WORD</asp:ListItem>
                                        <asp:ListItem Value="1">PDF</asp:ListItem>
                                        <asp:ListItem Value="2">EXCEL</asp:ListItem>
                                    </asp:DropDownList>

                                </div>
                                
                                
                                <div class="col-md-2">
                                    <label style="margin-right: 20PX;"><font color="white"> Download Options:</font></label>   
                                  <asp:Button ID="btnDownload" runat="server" Text="Download" class="btn btn-primary" OnClick="btnDownload_Click"  Visible="false"/>
                                </div>
                               
                                
                                
                               
                            </div>
                             <hr />
                        </div>
                    </div>
                </div>
            </div>
     

         <div class="row">
            <div class="col-lg-12">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                      Account Statement Report              
                    </div>
                    <br />
                    <br />
                    <table align="center" width="100%" height="50%">

                             <tr>
                                 <td></td>
                                 <td align="left" class="auto-style2"><asp:Label ID="Label1" runat="server" Text="OldAcNo:" style="font-weight: 700"></asp:Label></td>
                                 <td>
                                     
                                        <asp:Label ID="lblOldAcNo" runat="server" Text="OldAcNo:"></asp:Label> </td><td>&nbsp;&nbsp;&nbsp; </td>

                                 <td></td>

                                 <td class="auto-style1"><asp:Label ID="Label2" runat="server" Text=" Account No:" style="font-weight: 700"></asp:Label></td>
  
                                 <td align="left" class="auto-style3">
                                        <asp:Label ID="lblSlcode" runat="server"></asp:Label></td><td>&nbsp;&nbsp;&nbsp; </td>

                                     <td></td>

                                     <td align="right" class="auto-style4"><asp:Label ID="Label3" runat="server" Text="Ac Status:" style="font-weight: 700"></asp:Label></td>
                                 <td>
                                        <asp:Label ID="lblAcStatus" runat="server"></asp:Label> </td><td>&nbsp;&nbsp;&nbsp; </td>
  
                                </tr>
                          <tr><td>&nbsp;</td> </tr>
                             <tr>
                                 <td></td>
                                 <td align="left" class="auto-style2"><asp:Label ID="Label4" runat="server" Text="Ac Type:" style="font-weight: 700"></asp:Label></td>
                                 <td>
                                        <asp:Label ID="lblActype" runat="server" Text="Ac Type:"></asp:Label> </td><td>&nbsp;&nbsp;&nbsp; </td>
                                    <td></td>
                                    <td class="auto-style1"><asp:Label ID="Label5" runat="server" Text="Membership No:" style="font-weight: 700"></asp:Label></td>
                                 <td align="left" class="auto-style3">
                                        <asp:Label ID="lblMemberNo" runat="server"></asp:Label></td><td>&nbsp;&nbsp;&nbsp; </td>
                                    
                                 <td></td>
                                    
                                 <td align="right" class="auto-style4"><asp:Label ID="Label6" runat="server" Text="Pan No:" style="font-weight: 700"></asp:Label></td>
                                 <td>
                                        <asp:Label ID="lblPanNo" runat="server"></asp:Label> </td><td>&nbsp;&nbsp;&nbsp; </td>
  
                                </tr>
                         <tr><td>&nbsp;</td> </tr>
                             <tr>
                                  <td></td>
                                  <td align="left" class="auto-style2"><asp:Label ID="Label7" runat="server" Text="Adhar No:" style="font-weight: 700"></asp:Label></td>
                                 <td>
                                        <asp:Label ID="lblAdharNo" runat="server" Text="Adhar No:"></asp:Label> </td><td>&nbsp;&nbsp;&nbsp; </td>
                                  <td></td>
                                 <td class="auto-style1"><asp:Label ID="Label8" runat="server" Text="Account Holder Name:" style="font-weight: 700"></asp:Label></td>
  
                                 <td class="auto-style3">
                                        <asp:Label ID="lblAcHolderName" runat="server"></asp:Label></td><td>&nbsp;&nbsp;&nbsp; </td>
                                    <td></td>
                                    <td align="right" class="auto-style1"><asp:Label ID="Label9" runat="server" Text="Opening Balance: " style="font-weight: 700"></asp:Label></td>
                                 
                                     <td class="auto-style3">
                                        <asp:Label ID="lblbalance" runat="server"></asp:Label> </td><td>&nbsp;&nbsp;&nbsp; </td>
                                     <td></td>
                                 
                                        <td>&nbsp;</td>
                                 <%--<td class="auto-style3">
                                     <asp:Label ID="lblbalance" runat="server"></asp:Label></td><td>&nbsp;&nbsp;&nbsp; </td>
                                 <td></td>
                                    <td align="right" class="auto-style4"><asp:Label ID="Label10" runat="server" Text="Ac Balance:" style="color: #FFFFFF"></asp:Label></td>
                                 <td>
                                     &nbsp;
                                 </td>--%>
                                </tr>
                    </table>
                    
                    <hr />
                    <div class="panel-body">

                        <div style="overflow: scroll;" runat="server" id="RepeaterControls"> 
                         <table width="100%" class="table table-striped table-bordered table-hover" id="dataTablesexample">


                            <thead>
                                <tr>
                                     <th>Sr.</th>
                                    <th>Trans.Date</th>
                                    <th>Trans.Type</th>
                                    <th>Trans.Details</th>
                                    <th>Ref.No</th>
                                    <th>Receipt Amount</th>
                                     <th>Payment Amount </th> 
                                    <th>Balance Amount </th>
                             
                                 
                                    <%--<th>Int.Overdue</th>
                                     <th>Int.Demand</th> 
                                     <th>Print Out</th>  --%>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="RepCCList" runat="server" OnItemDataBound="RepCCList_ItemDataBound">
                                    <ItemTemplate>
                                        <tr>
                                             <td>
                                                <asp:Label ID="lblsrL" runat="server" Text='<%# (((RepeaterItem)Container).ItemIndex+1).ToString()%>'></asp:Label>


                                            </td> 

                                            <td>

                                                <asp:Label ID="lblParticular" runat="server" Text='<%# Eval("CCB_DATE" ,"{0:dd/MM/yyyy}")%>'></asp:Label>

                                            </td>

                                            <td>

                                                <asp:Label ID="lblLdgCode" runat="server" Text='<%# Eval("TYPE")%>'></asp:Label>

                                            </td>
                                            <td>

                                                <asp:Label ID="lblSubLedger" runat="server" Text='<%# Eval("NARRATION2")%>'></asp:Label>

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
                                            
                                           <td>

                                                <asp:Label ID="lblbalance" runat="server" Text='<%# Eval("Balance")%>'></asp:Label>

                                            </td>
                                           <%-- <td>

                                                <asp:Label ID="Label1" runat="server" ></asp:Label>

                                            </td>--%>

                                            
                                          

     <%--                                    <td>

                                                <asp:Label ID="lblBalanceclosing" runat="server" Text='<%# Eval("total")%>'></asp:Label>

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
