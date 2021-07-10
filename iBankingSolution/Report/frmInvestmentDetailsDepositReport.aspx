<%@ Page Language="C#" MasterPageFile="~/MasterPages/ProjectBM.Master" AutoEventWireup="true" CodeBehind="frmInvestmentDetailsDepositReport.aspx.cs" Inherits="iBankingSolution.Report.frmInvestmentDetailsDepositReport" %>
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

    <script type="text/javascript">
        $ = jQuery.noConflict();
        $(function () {
            $('.select2').select2()
        })
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="clearfix"></div><br /><br />
    <div class="row">
        <div class="col-lg-12">
            <div class="panel panel-primary">
                <div class="panel-heading" style="font-size:larger;">
                    Investment Detail List
                </div>
                <div class="panel-body">
                    <div class="row">

                        <div class="form-group">

                            <div class="col-md-4" style="font-size:larger;">
                                <label style="margin-right: 20PX;">Account Type</label>
                                <asp:DropDownList ID="cmbx_AcctType" runat="server" CssClass="form-control" placeholder="Select Ac Type" Font-Size="9" AutoPostBack="True" >  <%--OnSelectedIndexChanged="cmbx_AcctType_SelectedIndexChanged"--%>
                                  <asp:ListItem Value="0">--Select Type--</asp:ListItem>
                                        <asp:ListItem Value="fd">Fixed Deposit</asp:ListItem>
                                        <asp:ListItem Value="r">Recurring Deposit</asp:ListItem>
                                        <asp:ListItem Value="cc">Deposit Certificate</asp:ListItem>
                                        <asp:ListItem Value="s">Savings</asp:ListItem>
                                        <asp:ListItem Value="c">Current</asp:ListItem>
                                        <asp:ListItem Value="mis">MIS Deposit</asp:ListItem>
                                        <asp:ListItem Value="sh">Share</asp:ListItem>
                                        <asp:ListItem Value="Others">Others</asp:ListItem>
                                </asp:DropDownList>
                                </div>
                            <div class="col-md-4">
                                    <label style="margin-right: 20PX; font-size:larger;">Effected Ac</label>
                                      <asp:DropDownList ID="cmbx_EffectedAcct" runat="server" Font-Size="9" CssClass="form-control" placeholder="Select Effected Ac" >
                                           <asp:ListItem Value="0">--Select Type--</asp:ListItem>
                                        <asp:ListItem Value="Society">Society</asp:ListItem>
                                        <asp:ListItem Value="Stuff">Stuff</asp:ListItem>
                                         
                                      </asp:DropDownList>
                                </div>
                            <div class="col-md-4">
                                <label style="margin-right: 20PX;font-size:larger;">As On Date</label>
                                <asp:TextBox ID="dtpkr_DateAsOn" CssClass="form-control input-sm BootDatepicker" placeholder="dd/MM/yyyy" Font-Size="10" runat="server" onFocus="this.select()" autocomplete="off" required="required"></asp:TextBox>
                            </div>
                            <div class="clearfix"></div>
                            <div class="col-md-2">
                                <br />
                                <asp:Button ID="btnShow" runat="server" Text="Show Report" class="btn btn-primary"  OnClick="btnShow_Click" />  <%----%>
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
                                <asp:Button ID="btnDownload" runat="server" Text="Download" class="btn btn-primary" OnClick="btnDownload_Click" /> <%----%>

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
                <div class="panel-heading" style="font-size:larger;">
                    Detail List               
                </div>

                <div>
                    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" />
                    <div class="panel-body">
                        <div style="overflow: scroll;" runat="server" id="GridFixedDtlList" align="center">
                            <asp:GridView ID="GridFixedDeposit" runat="server" AllowPaging="True" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" AutoGenerateColumns="False" AllowSorting="True" Caption="Details List" Font-Names="Verdana" Font-Size="11pt" ShowFooter="True" ControlStyle-CssClass="my-auto" CssClass="my-auto" EmptyDataText="Data Not found"  PageSize="25">  <%--OnPageIndexChanging="GridFixedDeposit_PageIndexChanging"--%>
                                <Columns>
                                   <asp:TemplateField HeaderText="A/C No.">

                                         <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("A/C NO") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Height="20px" Wrap="False" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Date Of Opening">
                                        <ItemTemplate>
                                            <asp:Label ID="Label16" runat="server" Text='<%# Eval("DATE_OF_OPENING") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Face Value">
                                        <ItemTemplate>
                                            <asp:Label ID="Label15" runat="server" Text='<%# Eval("FACE_VALUE") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Rate">

                                        <ItemTemplate>
                                            <asp:Label ID="Label7" runat="server" Text='<%# Eval("ROI") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Duration">

                                        <ItemTemplate>
                                            <asp:Label ID="Label8" runat="server"  HorizontalAlign="Center"  Text='<%# Eval("DURATION") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Maturity Amount">

                                        <ItemTemplate>
                                            <asp:Label ID="Label9" runat="server" Text='<%# Eval("MATURITY_AMT") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Maturity Date" ItemStyle-CssClass="my-auto" HeaderStyle-CssClass="my-auto1" HeaderStyle-Height="20" HeaderStyle-HorizontalAlign="Center" HeaderStyle-BackColor="#003366">

                                        <ItemTemplate>
                                            <asp:Label ID="Label10" runat="server"  HorizontalAlign="Center"  Text='<%# Eval("MATURITY DATE") %>'></asp:Label>
                                        </ItemTemplate>

                                        <HeaderStyle HorizontalAlign="Center" BackColor="#006699" CssClass="my-auto1" Height="20px"></HeaderStyle>

                                        <ItemStyle CssClass="my-auto"></ItemStyle>

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Interest Accured">
                                         <ItemTemplate>
                                             <asp:Label ID="Label14" runat="server" Width="2%"  HorizontalAlign="Center"  Text='<%# Eval("Interest") %>'></asp:Label>
                                         </ItemTemplate>
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
                </div>
            </div>

    <%-- Scripting Section for calender--%>
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
