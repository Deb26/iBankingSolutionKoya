<%@ Page Language="C#" MasterPageFile="~/MasterPages/ProjectBM.Master" AutoEventWireup="true" CodeBehind="frmProfitLossAcReport.aspx.cs" Inherits="iBankingSolution.Report.frmProfitLossAcReport" %>

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
                        Profit and Loss A/C Report
                    </div>
                    <div class="panel-body">
                        <div class="row">

                            <div class="form-group">

                                <div class="clearfix"></div>
                                <div class="col-md-2">
                                    <label style="margin-right: 20PX;">Branch</label>

                                    <asp:DropDownList ID="cmbx_Branch" runat="server" CssClass="form-control" Font-Size="10"></asp:DropDownList>

                                </div>

                                <div class="col-md-2">
                                    <label style="margin-right: 20PX;">Date:</label>

                                    <asp:TextBox ID="dtpkr_frmDate" CssClass="form-control input-sm BootDatepicker" Font-Size="10" placeholder="dd/MM/yyyy" runat="server" onFocus="this.select()" autocomplete="off" required="required"></asp:TextBox>

                                </div>
                                 <div class="col-md-2">
                                    <label style="margin-right: 20PX;">View:</label>

                                     <asp:Button ID="btnShow" runat="server" Text="Show" class="btn btn-primary" OnClick="btnShow_Click" />

                                </div>

                        


                                
                                </div>
                                 <div class="clearfix"></div><br />
                               
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
                                    <th>Sr.No.</th> 
                                  
                                    <th>Expenditure</th>
                                      <th>Amount(Rs.)</th>
                                    <th>Current Year(Rs.)</th>
                                    <th>Previsous Year(Rs.)</th>

                                    <th>Sr.No.</th> 
                                   
                                    <th>Income</th>
                                     <th>Amount(Rs.)</th>
                                    <th>Current Year(Rs.)</th>
                                    <th>Previsous Year(Rs.)</th>
                                
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="RepCCList" runat="server" >
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblLsrL" runat="server" Text='<%# Eval("LSL")%>'></asp:Label>


                                            </td>

                                            <td>

                                                
                                                <asp:Label ID="lblLParticular" runat="server" Text='<%# Eval("LSL_HEAD")%>'></asp:Label>

                                            </td>

                                            <td>

                                                <asp:Label ID="LblLamt" runat="server" Text='<%# Eval("LAMT")%>'></asp:Label>

                                            </td>
                                            <td>

                                                <asp:Label ID="lblLCurYr" runat="server" Text='<%# Eval("LCURR")%>'></asp:Label>

                                            </td>
                                            <td>

                                          
                                                <asp:Label ID="lblLPreYr" runat="server" Text='<%# Eval("LPRE")%>'> </asp:Label>

                                            </td>

                                            <td>

                                                      <asp:Label ID="lblRsrL" runat="server" Text='<%# Eval("RSL")%>'></asp:Label>

                                            </td>
                                            <td>

                                            
                                                 <asp:Label ID="lblRParticular" runat="server" Text='<%# Eval("RSL_HEAD")%>'> </asp:Label>

                                            </td>
                                            <td>

                                                   <asp:Label ID="LblRamt" runat="server" Text='<%# Eval("RAMT")%>'> </asp:Label>

                                            </td>
                                            <td>

                                                <asp:Label ID="lblRCurYr" runat="server" Text='<%# Eval("RCURR")%>'> </asp:Label>

                                            </td>

                                             <td>

                                                <asp:Label ID="lblRPreYr" runat="server" Text='<%# Eval("RPRE")%>'> </asp:Label>

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
                                      <td>&nbsp;</td>
                                    <td>
                                        <strong></strong>

                                        <asp:Label ID="Label3" runat="server" Text="Profit Of The Year" style="font-weight: 700"></asp:Label>

                                    </td>
                                      <td>&nbsp;</td>
                                   
                                    <%--<td>&nbsp;</td>--%>
                                    <td><asp:Label ID="lblprofit" runat="server" Text="" style="font-weight: 700"></asp:Label></td>
                                   

                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                     <td>
                                        <strong></strong>

                                        <asp:Label ID="Label4" runat="server" Text="Loss Of The Year" style="font-weight: 700"></asp:Label>

                                    </td>
                        
                                     <td>&nbsp;</td>
                                    <td><strong><asp:Label ID="lblloss" runat="server" Text="" style="font-weight: 700"></asp:Label></strong></td>
                                    <td></td>
                                    
                                    

                                </tr>
                                <tr>
                                    <td>
                                        <strong></strong>
                                        <asp:Label ID="Label1" runat="server" Text="Total" style="font-weight: 700"></asp:Label>

                                    </td>

                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td><asp:Label ID="lblLeftTotal" runat="server" Text="" style="font-weight: 700"></asp:Label></td>
                                    <td>&nbsp;</td>

                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                     <td>&nbsp;</td>
                                     
                                    

                                    <td><strong><asp:Label ID="lblRightTotal" runat="server" Text="" style="font-weight: 700"></asp:Label></strong></td>
                                    <td></td>
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
