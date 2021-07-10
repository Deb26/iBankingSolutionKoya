<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/ProjectBM.Master"  CodeBehind="LoanAcctDetails.aspx.cs" Inherits="iBankingSolution.Report.LoanAcctDetails" %>
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
                        Loan Account Detail
                    </div>
                    <div class="panel-body">
                        <div class="row">

                            <div class="form-group">
 
                                <div class="col-md-4">
                                    <label style="margin-right: 20PX;">Loan A/c No.</label>
                                    <asp:DropDownList ID="cmbx_LoanAcNo" runat="server" CssClass="form-control" Font-Size="10"></asp:DropDownList>
                                </div>
                                    <div class="col-md-4">
                                    <label style="margin-right: 20PX;">As On Date</label>
                                     <asp:TextBox ID="dtpkr_DateAsOn" CssClass="form-control input-sm BootDatepicker" placeholder="dd/MM/yyyy" Font-Size="10" runat="server" onFocus="this.select()" autocomplete="off" required="required"></asp:TextBox>
                                </div>
                               
                              
                                 <div class="clearfix"></div>
                                <div class="col-md-2">
                                     <br />
                                  <asp:Button ID="btnDownload" runat="server" Text="Download" class="btn btn-primary" OnClick="btnDownload_Click" />
                                </div>
                               
                                <div  class="col-md-2">
                                    <br />
                                    
                                    <asp:DropDownList ID="ddlExportReport" runat="server" CssClass="form-control" class="styled-select"  BackColor="White" ForeColor="Black" Font-Size="10">
                                        <asp:ListItem Value="0">WORD</asp:ListItem>
                                        <asp:ListItem Value="1">PDF</asp:ListItem>
                                        <asp:ListItem Value="2">EXCEL</asp:ListItem>
                                    </asp:DropDownList>

                                </div>
                                
                                <div class="clearfix"></div>
                                <hr />
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>

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