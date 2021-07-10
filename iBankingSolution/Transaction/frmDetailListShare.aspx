<%@ Page Language="C#" MasterPageFile="~/MasterPages/ProjectBM.Master" AutoEventWireup="true" CodeBehind="frmDetailListShare.aspx.cs" Inherits="iBankingSolution.Transaction.frmDetailListShare" %>
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
    <div class="row">
        <div class="col-lg-12">
            <div class="panel panel-primary">
                <div class="panel-heading" style="font-size:larger;">
                    Daily Share Report
                </div>
                <div class="panel-body">
                    <div class="row">

                        <div class="form-group">
                            <div class="col-md-3">
                                <label style="margin-right: 20PX;">Account Type</label>
                                <asp:TextBox ID="txtAcType" runat="server" CssClass="form-control" Text="Share" Enabled="false"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <label style="margin-right: 20PX;">As On Date</label>
                                <asp:TextBox ID="dtpkr_DateAsOn" CssClass="form-control input-sm BootDatepicker" placeholder="dd/MM/yyyy" Font-Size="10" runat="server" onFocus="this.select()" autocomplete="off" required="required"></asp:TextBox>
                            </div>
                             <div class="col-md-2">
                                <br />
                                <asp:Button ID="btnShow" runat="server" Text="Show Report" class="btn btn-primary" OnClick="btnShow_Click"  /> <%----%>
                            </div>
                            <div class="col-md-3">
                             <asp:DropDownList ID="ddlExportReport" runat="server" CssClass="form-control" class="styled-select" BackColor="White" ForeColor="Black">
                                    <asp:ListItem Value="0">WORD</asp:ListItem>
                                    <asp:ListItem Value="1">PDF</asp:ListItem>
                                    <asp:ListItem Value="2">EXCEL</asp:ListItem>
                                </asp:DropDownList>

                            </div>
                            <div class="col-md-3">
                                <br />
                                <asp:Button ID="btnDownload" runat="server" Text="Download" class="btn btn-primary" OnClick="btnDownload_Click" /> <%----%>

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
                   Share Detail List               
                </div>

                <div>
                    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" />
                    <div class="panel-body">


                        <%--<div style="overflow: scroll;" runat="server" id="RepeaterControls">--%>
                         <div style ="height:350px; width:1265px; overflow:auto;">
                            <table width="100%" class="table table-striped table-bordered table-hover" id="dataTablesexample">
                                <thead>
                                    <tr>
                                        <th>Sr.</th>
                                        <th>A/c No</th>
                                        <th>Old Account No</th>
                                        <th>Name of the A/c</th>
                                        <th>Share Balance </th>
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
                                                <td>

                                                <asp:Label ID="lbldateofopening" runat="server" Text='<%# Eval("ClosingBalance")%>'></asp:Label>

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
            </div><r /><br /><br />
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
