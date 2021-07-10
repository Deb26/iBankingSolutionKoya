<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/ProjectBM.Master" CodeBehind="frmCashAccount.aspx.cs" Inherits="iBankingSolution.Report.frmCashAccount" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

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
                        Cash Account
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

                                         <asp:TextBox ID="dtpkr_frmDate" CssClass="form-control input-sm BootDatepicker" placeholder="dd/MM/yyyy" Font-Size="10" runat="server" onFocus="this.select()" autocomplete="off" required="required"></asp:TextBox>

                                    </div>

                                         <div class="col-md-2">

                                        <label style="margin-right: 20PX;">To Date:</label>

                                         <asp:TextBox ID="dtpkr_toDate" CssClass="form-control input-sm BootDatepicker" placeholder="dd/MM/yyyy" Font-Size="10" runat="server" onFocus="this.select()" autocomplete="off" required="required"></asp:TextBox>

                                    </div>
                         
                                  
                                <div class="col-md-2">
                                     <label style="margin-right: 20PX;">    </label><br />
                                     <asp:Button ID="btnShow" runat="server" Text="Show" class="btn btn-primary" OnClick="btnShow_Click" />
                                  <%--<asp:Button ID="btnDownload" runat="server" Text="Download" class="btn btn-primary" OnClick="btnDownload_Click" />--%>
                                </div>
                               
                                <div  class="col-md-2"><br />
                                   
                                    
                                    <asp:DropDownList ID="ddlExportReport" runat="server" CssClass="form-control" class="styled-select"  BackColor="White" ForeColor="Black" Font-Size="10">
                                        <asp:ListItem Value="0">WORD</asp:ListItem>
                                        <asp:ListItem Value="1">TEXT</asp:ListItem>
                                        <asp:ListItem Value="2">HTML</asp:ListItem>
                                    </asp:DropDownList>

                                </div>
                                <div class="col-md-2"><br />
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
        <div>
            <asp:GridView ID="GridView1" runat="server" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" Visible="False">
                <AlternatingRowStyle BackColor="#CCCCCC" />
                <FooterStyle BackColor="#CCCCCC" />
                <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                <SortedAscendingHeaderStyle BackColor="#808080" />
                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                <SortedDescendingHeaderStyle BackColor="#383838" />
            </asp:GridView>

            

        </div>


     <div class="row">
            <div class="col-lg-12">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        Cash Account Result
                    </div>
        <div class="panel-body">

                        <div style="overflow: scroll;" runat="server" id="RepeaterControls"> 
                         <table width="100%" class="table table-striped table-bordered table-hover" id="dataTablesexample">


                            <thead>
                                <tr>
                                    <%-- <th>Sr.</th>--%>
                                    <th>Code</th>
                                    <th>Ledger</th>
                                    <th>Receipt Amtount</th>
                                    <th>Code</th>
                                    <th>Ledger</th>
                                    <th>Payment Amount </th>
                                    <th></th>
                                    <th></th>
                                    
 
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="RepCCList" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <%-- <td>
                                                <asp:Label ID="lblsrL" runat="server" Text='<%# (((RepeaterItem)Container).ItemIndex+1).ToString()%>'></asp:Label>


                                            </td> --%>

                                            <td>
                                                

                                                <asp:Label ID="lblParticular" runat="server" Text='<%# Eval("ldg_code")%>'></asp:Label>

                                            </td>

                                            <td>

                                                <asp:Label ID="lblLdgCode" runat="server" Text='<%# Eval("nomenclature")%>'></asp:Label>

                                            </td>
                                            
                                            <td>

                                                <asp:Label ID="lblrecipt" runat="server" Text='<%# Eval("receipt")%>'></asp:Label>

                                            </td>
                                            <td>

                                                <asp:Label ID="lblVOUCHER_NO" runat="server" Text='<%# Eval("ldg_code")%>'></asp:Label>

                                            </td>

                                            <td>

                                                <asp:Label ID="LblAMT_Payment" runat="server" Text='<%# Eval("nomenclature")%>'></asp:Label>

                                            </td>
                                            <td>

                                                <asp:Label ID="lbl_payment" runat="server" Text='<%# Eval("payment")%>'></asp:Label>

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
                                   
                                    

                                </tr>
                                <tr>
                                    <td></td>
                                    <td>
                                        <asp:Label ID="Label1" runat="server" Text="Total" style="font-weight: 700"></asp:Label>

                                    </td>

                                   <%-- <td></td>--%>
                                   <%-- <td></td>--%>
                                      <td><asp:Label ID="lblPaymenttotal" runat="server" Text="" style="font-weight: 700"></asp:Label></td>
                                       <td></td>
                                        <td>
                                            <asp:Label ID="Label2" runat="server" Text="Total" style="font-weight: 700"></asp:Label>

                                        </td>
                                       <%--<td></td>--%>
                                      <td><asp:Label ID="lblReceipttotal" runat="server" Text="" style="font-weight: 700"></asp:Label></td>

                                    
                                   <%-- <td><strong><asp:Label ID="lbltrrecAmt" runat="server" Text="" style="font-weight: 700"></asp:Label></strong></td>
                                     <td><strong><asp:Label ID="lbltrPayAmt" runat="server" Text="" style="font-weight: 700"></asp:Label></strong></td>--%>
                                    

                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td>
                                        <strong>Opening</strong></td>

                                   <%-- <td>&nbsp;</td>--%>
                                    <%--<td>&nbsp;</td>--%>
                                    <%--<td>&nbsp;</td>--%>
                                    <td><asp:Label ID="lblopeningbal" runat="server" Text='<%# Eval("OpeningBal")%>' style="font-weight: 700"></asp:Label></td>
                                    <td>&nbsp;</td>
                                    <td>
                                        <strong>Closing</strong></td>
                                    <%--<td>&nbsp;</td>--%>
                                     <td><asp:Label ID="lblclosingBal" runat="server" Text='<%# Eval("ClosingBal")%>' style="font-weight: 700"></asp:Label></td>
                                   <%-- <td>&nbsp;</td>
                                    <td>&nbsp;</td>--%>

                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td>
                                        <strong>Grand Total</strong></td>

                                    <%--<td>&nbsp;</td>
                                    <td>&nbsp;</td>--%>
                                    <%--<td>&nbsp;</td>--%>
                                    <td><asp:Label ID="lblGRTReceipt" runat="server"  style="font-weight: 700"></asp:Label></td>
                                   
                                    <td>&nbsp;</td>
                                     <td>
                                        <strong>Grand Total</strong></td>
                                   <%-- <td>&nbsp;</td>--%>
                                     
                                     <td><asp:Label ID="lblGRTPayment" runat="server"  style="font-weight: 700"></asp:Label></td>

                                     
                                    <%--<td>&nbsp;</td>
                                    <td>&nbsp;</td>--%>

                                </tr>


                            </tbody>
                        </table> 
                        </div>
                           
            </div>
                    </div>
                </div></div>
    

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
