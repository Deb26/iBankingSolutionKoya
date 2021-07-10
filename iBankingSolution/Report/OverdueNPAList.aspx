<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/ProjectBM.Master" CodeBehind="OverdueNPAList.aspx.cs" Inherits="iBankingSolution.Report.OverdueNPAList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.4.1/js/bootstrap-datepicker.min.js"></script>
         <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.4.1/css/bootstrap-datepicker3.css"/>
    <script type="text/javascript">
        $ = jQuery.noConflict();
        $(function () {
            $('.select2').select2()
        })
    </script>
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
                    Overdue NPA Report
                </div>
                <div class="panel-body">
                    <div class="row">

                        <div class="form-group">

                            <div class="col-md-2">
                                <label style="margin-right: 20PX;">Select Name of the Crop.</label>

                          
                                <asp:DropDownList ID="cmbx_CrropName" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="cmbx_CrropName_SelectedIndexChanged"></asp:DropDownList>
                            </div>

                
                           
                            <div class="col-md-2">
                                <label style="margin-right: 20PX;">Type.</label>
                                <asp:TextBox ID="txt_type" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                            </div>
                           

                            <div class="col-md-2">
                                <label style="margin-right: 20PX;">AsonDate.</label>
                                <asp:TextBox ID="dtpkr_AsonDate" CssClass="form-control input-sm BootDatepicker" placeholder="DD/MM/YYYY" Font-Size="10" runat="server" onFocus="this.select()" autocomplete="off" required="required"></asp:TextBox>
                        
                            </div>
                            <div class="col-md-2">
                                <asp:Button ID="btnShow" runat="server" Text="Show" class="btn btn-primary" OnClick="btnShow_Click" />
                            </div>
                         
                            <div class="col-md-2" align="left">

                                <asp:DropDownList ID="ddlExportReport" runat="server" CssClass="form-control" class="styled-select" Font-Size="10" BackColor="White" ForeColor="Black">
                                    <asp:ListItem Value="0">WORD</asp:ListItem>
                                    <asp:ListItem Value="1">PDF</asp:ListItem>
                                    <asp:ListItem Value="2">EXCEL</asp:ListItem>
                                </asp:DropDownList>

                            </div>

                            <div class="col-md-2">
                                <asp:Button ID="btnDownload" runat="server" Text="Download" class="btn btn-primary" OnClick="btnDownload_Click" />
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
                        NPA Overdue List              
                    </div>
                    <hr />
                    <div class="panel-body">

                        <div style="overflow: scroll;" runat="server" id="RepeaterControls">
                            <table width="100%" class="table table-striped table-bordered table-hover" id="dataTablesexample">


                                <thead>
                                    <tr>
                                        <th>Sr.</th>
                                        <th>Loan A/C No.</th>
                                        <th>A/c No.</th>
                                        <th>Borower Name</th>
                                        <th>IssueDt.</th>
                                        <th>LoanAmt</th>
                                        <th>Repayment Mode </th>
                                        <th>Kisti Amt </th>
                                        <th>Overdue Dt.</th>
                                        <th>Prin.Out</th>
                                        <th>Intr.Out </th>
                                        <th>Overdue Int. </th>
                                        <th>Upto90Days</th>
                                        <th>90Days upto3yrs</th>
                                        <th>3 yrs- 4 yrs</th>
                                        <th>4 yrs- 6 yrs</th>
                                        <th>More than6 yrs</th>

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

                                                    <asp:Label ID="lblLF_NO" runat="server" Text='<%# Eval("LF_NO")%>'></asp:Label>

                                                </td>

                                                <td>

                                                    <asp:Label ID="lblslcode" runat="server" Text='<%# Eval("sl_code")%>'></asp:Label>

                                                </td>
                                                <td>

                                                    <asp:Label ID="lblName" runat="server" Text='<%# Eval("name")%>'></asp:Label>

                                                </td>

                                                <td>

                                                    <asp:Label ID="lblsanc_dt" runat="server" Text='<%# Eval("sanc_dt","{0:d}")%>'></asp:Label>

                                                </td>
                                                <td>

                                                    <asp:Label ID="lblnet_loan" runat="server" Text='<%# Eval("net_loan")%>'></asp:Label>

                                                </td>

                                                <td>

                                                    <asp:Label ID="Lblrepay_mode" runat="server" Text='<%# Eval("repay_mode")%>'></asp:Label>

                                                </td>

                                                <td>

                                                    <asp:Label ID="lblinst_amount" runat="server" Text='<%# Eval("inst_amount")%>'></asp:Label>

                                                </td>

                                                <td>
                                                <asp:Label ID="Lbllast_rep_dt" runat="server" Text='<%# Eval("last_rep_dt")%>'></asp:Label>

                                                </td>
                                                <td>

                                                    <asp:Label ID="lblBalance" runat="server" Text='<%# Eval("Balance")%>'></asp:Label>

                                                </td>




                                                <td>

                                                    <asp:Label ID="lblloancurrint" runat="server" Text='<%# Eval("vfloancurrint")%>'></asp:Label>

                                                </td>


                                                <td>

                                                    <asp:Label ID="lblvfloanodint" runat="server" Text='<%# Eval("vfloanodint")%>'></asp:Label>

                                                </td>

                                             <td>

                                                <asp:Label ID="lblUpto90Days" runat="server"></asp:Label>

                                            </td>

                                             <td>

                                                <asp:Label ID="lbl90Daysupto3yrs" runat="server"></asp:Label>

                                            </td>

                                             <td>
                                                <asp:Label ID="lbl3yrs4yrs" runat="server"></asp:Label>
                                            </td>

                                            <td>
                                                <asp:Label ID="lbl4yrs6yrs" runat="server"></asp:Label>
                                            </td>
                                            <td>

                                                <asp:Label ID="lblMorethan6yrs" runat="server"></asp:Label>

                                            </td>
  
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
        </div>
</asp:Content>

