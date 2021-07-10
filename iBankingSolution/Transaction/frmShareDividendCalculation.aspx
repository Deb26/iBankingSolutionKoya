<%@ Page Language="C#" MasterPageFile="~/MasterPages/ProjectBM.Master" AutoEventWireup="true" CodeBehind="frmShareDividendCalculation.aspx.cs" Inherits="iBankingSolution.Transaction.frmShareDividendCalculation" %>
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
    <div class="clearfix"></div><br /><br />
     <asp:ScriptManager ID="scrpt1" runat="server"></asp:ScriptManager>
       <div class="row">
            <div class="col-lg-12">
                <div class="panel panel-primary">
                    <div class="panel-heading" style="font-size:larger;">
                        Share Dividend Calculation
                    </div>
                    <div class="panel-body">
                    <div class="row">

                        <div class="form-group">
                            <div class="col-md-3">
                                <label style="margin-right: 20PX;font-size:larger;">Percentage of Dividend</label>
                                <asp:TextBox ID="txtPercentage" runat="server" CssClass="form-control" placeholder="Percentage of Dividend" Font-Size="10" required="true"></asp:TextBox>
                            </div>
                             <div class="col-md-3">
                                <label style="margin-right: 20PX;font-size:larger;">Minimum Share Amount</label>
                                <asp:TextBox ID="txtMinAmt" runat="server" CssClass="form-control" placeholder="Enter Minimum Share Amount" Font-Size="10" required="true"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <label style="margin-right: 20PX;font-size:larger;">As On Date</label>
                                <asp:TextBox ID="dtpkr_DateAsOn" CssClass="form-control input-sm BootDatepicker" placeholder="dd/MM/yyyy" Font-Size="10" runat="server" onFocus="this.select()" autocomplete="off" required="required"></asp:TextBox>
                            </div>
                           
                            <div class="col-md-2">
                                <br />
                                <asp:Button ID="btnShow" runat="server" Text="Show Report" class="btn btn-primary"  OnClick="btnShow_Click" /> <%----%>
                            </div>
                             
                            <div class="clearfix"></div><br />
                            <div class="col-md-2">
                             <asp:DropDownList ID="ddlExportReport" runat="server" CssClass="form-control" class="styled-select" BackColor="White" ForeColor="Black">
                                    <asp:ListItem Value="0">WORD</asp:ListItem>
                                    <asp:ListItem Value="1">PDF</asp:ListItem>
                                    <asp:ListItem Value="2">EXCEL</asp:ListItem>
                                </asp:DropDownList>

                            </div>
                            <div class="col-md-3">
           
                                <asp:Button ID="btnDownload" runat="server" Text="Download" class="btn btn-primary"  /> <%--OnClick="btnDownload_Click"--%>

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
                    Dividend Calculation List               
                </div>

                <div>
                   
                    <div class="panel-body">
                         <div style ="height:350px; width:1235px; overflow:auto;">
                            <table width="100%" class="table table-striped table-bordered table-hover" id="dataTablesexample">
                                <thead>
                                    <tr>
                                        <th>Sr.</th>
                                        <th>A/c No</th>
                                        <th>Old Account No</th>
                                        <th>Name of the A/c</th>
                                        <th>Share Balance </th>
                                        <th>Divident Balance</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="RepCCListShare" runat="server">
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
                                                <td>

                                                <asp:Label ID="lbldateofopening" runat="server" Text='<%# Eval("BALANCE")%>'></asp:Label>

                                            </td>
                                                <td>

                                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("Divident_Amt")%>'></asp:Label>

                                            </td>

                                            
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>

                                </tbody>
                            </table>
                        </div>
          <%--<div style="overflow: scroll;" runat="server" id="GridFixedDtlList" align="center">
                            <asp:GridView ID="GridFixedDeposit" runat="server" AllowPaging="True" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" AutoGenerateColumns="False" AllowSorting="True" Caption="Details List" Font-Names="Verdana" Font-Size="11pt" ShowFooter="True" ControlStyle-CssClass="my-auto" CssClass="my-auto" EmptyDataText="Data Not found" OnPageIndexChanging="GridFixedDeposit_PageIndexChanging" PageSize="15">
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
                                    <asp:TemplateField HeaderText="Share Amount">

                                        <ItemTemplate>
                                            <asp:Label ID="Label4" runat="server" Text='<%# Eval("BALANCE") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Divident Amount">

                                        <ItemTemplate>
                                            <asp:Label ID="Label5" runat="server" Text='<%# Eval("Divident_Amt") %>'></asp:Label>
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


                        </div>--%>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    
      <%-- Scripting Section for calander --%>
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
