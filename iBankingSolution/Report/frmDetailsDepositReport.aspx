<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/ProjectBM.Master" CodeBehind="frmDetailsDepositReport.aspx.cs" Inherits="iBankingSolution.Report.frmDetailsDepositReport" %>



<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
   
    <!-- YUI Dependencies -->

    

    <script type="text/javascript">
        $(document).ready(function () {
            $('#dataTablesexample').DataTable({
                responsive: true
            });
        });


        

    </script>

 
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">


    <div class="clearfix"></div><br /><br />
    <div class="row">
        <div class="col-lg-12">
            <div class="panel panel-primary">
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
                            <div class="col-md-2">
                                <label style="margin-right: 20PX;">Account Type</label>
                                <asp:DropDownList ID="cmbx_AcctType" runat="server" CssClass="form-control" placeholder="Select Ac Type" Font-Size="10" AutoPostBack="True" OnSelectedIndexChanged="cmbx_AcctType_SelectedIndexChanged">
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
                                    <asp:ListItem Value="sh">Share</asp:ListItem>


                                </asp:DropDownList>
                            </div>
                            <div class="col-md-4">
                                <label style="margin-right: 20PX;">Select Scheme</label>
                                <asp:DropDownList ID="cmbx_Ledger" runat="server" CssClass="form-control" placeholder="Select Scheme" Font-Size="10" AutoPostBack="True" OnSelectedIndexChanged="cmbx_Ledger_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                            <div class="col-md-3">
                                <label style="margin-right: 20PX;">Ledger Code</label>
                                <asp:TextBox ID="txtLedgCode" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>


                            <div class="col-md-3">
                                <label style="margin-right: 20PX;">As On Date</label>
                                <asp:TextBox ID="dtpkr_DateAsOn" CssClass="form-control input-sm BootDatepicker" placeholder="dd/MM/yyyy" Font-Size="10" runat="server" onFocus="this.select()" autocomplete="off" required="required"></asp:TextBox>
                            </div>
                            <div class="clearfix"></div>
                            <div class="col-md-2">
                                <br />
                                <asp:Button ID="btnShow" runat="server" Text="Show Report" class="btn btn-primary"  OnClick="btnShow_Click" />
                            </div>

                            <div class="col-md-2">
                                <br />

                                <asp:DropDownList ID="ddlExportReport" runat="server" CssClass="form-control" class="styled-select" BackColor="White" ForeColor="Black">
                                    <asp:ListItem Value="0">WORD</asp:ListItem>
                                    <asp:ListItem Value="1">PDF</asp:ListItem>
                                    <asp:ListItem Value="2">EXCEL</asp:ListItem>
                                </asp:DropDownList>

                            </div>
                            <div class="col-md-2">
                                <br />
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

                <div>
                    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" />
                    <div class="panel-body">


                        <div style="overflow: scroll;" runat="server" id="RepeaterControls">
                            <table width="100%" class="table table-striped table-bordered table-hover" id="dataTablesexample">
                                <thead>
                                    <tr>
                                        <th>Sr.</th>
                                        <th>A/c No</th>
                                        <th>Old Account No</th>
                                        <th>Name of the A/c</th>
                                        <%--<th>Opening Date</th>
                                    <th>R.O.I(%)</th>
                                    <th>Duration </th>--%>
                                        <th>Opening Balance </th>
                                        <th>Interest </th>
                                        <th>Closing Balance</th>
                                        <%--<th>Maturity Amt</th>
                                    <th>Maturity Date</th>--%>
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
                                                <%--<td>

                                                <asp:Label ID="lbldateofopening" runat="server" Text='<%# Eval("Opening Date")%>'></asp:Label>

                                            </td>

                                            <td>

                                                <asp:Label ID="LblROI" runat="server" Text='<%# Eval("ROI")%>'></asp:Label>

                                            </td>

                                            <td>

                                                <asp:Label ID="lblduration" runat="server" Text='<%# Eval("Duration")%>'></asp:Label>

                                            </td>--%>

                                                <td>

                                                    <asp:Label ID="lblBalance" runat="server" Text='<%# Eval("balance")%>'></asp:Label>

                                                </td>
                                                <td>

                                                    <asp:Label ID="lblInterest" runat="server" Text='<%# Eval("interest")%>'></asp:Label>

                                                </td>

                                                <td>

                                                    <asp:Label ID="lblBalanceclosing" runat="server" Text='<%# Eval("ClosingBalance")%>'></asp:Label>

                                                </td>


                                                <%--  <td>

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
                        <div style="overflow: scroll;" runat="server" id="GridFixedDtlList" align="center">
                            <asp:GridView ID="GridFixedDeposit" runat="server" AllowPaging="True" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" AutoGenerateColumns="False" AllowSorting="True" Caption="Details List" Font-Names="Verdana" Font-Size="11pt" ShowFooter="True" ControlStyle-CssClass="my-auto" CssClass="my-auto" EmptyDataText="Data Not found" OnPageIndexChanging="GridFixedDeposit_PageIndexChanging" PageSize="25">
                                <Columns>
                                    <asp:TemplateField HeaderText="A/C No.">

                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("A/C NO") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Height="20px" Wrap="False" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Old A/C">

                                        <ItemTemplate>
                                            <asp:Label ID="Label2" runat="server" Text='<%# Eval("OLD A/C NO") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="A/C Holder Name">

                                        <ItemTemplate>
                                            <asp:Label ID="Label3" runat="server" Text='<%# Eval("NAME OF THE A/C HOLDER") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total Amount">

                                        <ItemTemplate>
                                            <asp:Label ID="Label4" runat="server" Text='<%# Eval("BALANCE") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="INST_AMT">

                                        <ItemTemplate>
                                            <asp:Label ID="Labe22" runat="server" Text='<%# Eval("BALANCE") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Interest">

                                        <ItemTemplate>
                                            <asp:Label ID="Label5" runat="server" Text='<%# Eval("Interest") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Op.Date">

                                        <ItemTemplate>
                                            <asp:Label ID="Label6" runat="server" Text='<%# Eval("Opening Date") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Rate">

                                        <ItemTemplate>
                                            <asp:Label ID="Label7" runat="server" Text='<%# Eval("ROI") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Duration">

                                        <ItemTemplate>
                                            <asp:Label ID="Label8" runat="server" Text='<%# Eval("DURATION") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Maturity Amt">

                                        <ItemTemplate>
                                            <asp:Label ID="Label9" runat="server" Text='<%# Eval("MATURITY AMT") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Maturity Date" ItemStyle-CssClass="my-auto" HeaderStyle-CssClass="my-auto1" HeaderStyle-Height="20" HeaderStyle-HorizontalAlign="Center" HeaderStyle-BackColor="#003366">

                                        <ItemTemplate>
                                            <asp:Label ID="Label10" runat="server" Text='<%# Eval("MATURITY DATE") %>'></asp:Label>
                                        </ItemTemplate>

                                        <HeaderStyle HorizontalAlign="Center" BackColor="#006699" CssClass="my-auto1" Height="20px"></HeaderStyle>

                                        <ItemStyle CssClass="my-auto"></ItemStyle>
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle BackColor="White" ForeColor="#000066" />
                                <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" CssClass="my-auto" />
                                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                <RowStyle ForeColor="#000066" />
                                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                <SortedDescendingHeaderStyle BackColor="#00547E" />
                            </asp:GridView>


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
