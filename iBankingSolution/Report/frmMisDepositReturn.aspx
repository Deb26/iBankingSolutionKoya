<%@ Page Language="C#" MasterPageFile="~/MasterPages/ProjectBM.Master" AutoEventWireup="true" CodeBehind="frmMisDepositReturn.aspx.cs" Inherits="iBankingSolution.Report.frmMisDepositReturn" %>
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
                </div>
                <h1 class="page-header">Deposit Return</h1>
            </div>
            <!-- /.col-lg-12 -->
        </div>
        <div class="row">
            <div class="col-lg-12">
                <div class="panel panel-primary">
                    <div class="panel-heading" style="font-size:larger;">
                         Deposit Return 
                    </div>
                    <div class="panel-body">

                        <div class="row">

                            <div class="form-group">
  
                                    <div class="clearfix"></div><br />

                                    <div class="col-md-2">
                                        <label style="margin-right: 20PX;font-size:larger;">Select Month:</label>

                                        <asp:DropDownList ID="cmbx_selectmonth" runat="server" CssClass="form-control" class="styled-select" BackColor="White" ForeColor="Black" Font-Size="10" AutoPostBack="true" OnSelectedIndexChanged="cmbx_SelectMonths_SelectedIndexChanged">
                                            <asp:ListItem Value="0">-- Select Months --</asp:ListItem>
                                            <asp:ListItem Value="1">January</asp:ListItem>
                                            <asp:ListItem Value="2">February</asp:ListItem>
                                            <asp:ListItem Value="3">March</asp:ListItem>
                                            <asp:ListItem Value="4">April</asp:ListItem>
                                            <asp:ListItem Value="5">May</asp:ListItem>
                                            <asp:ListItem Value="6">June</asp:ListItem>
                                            <asp:ListItem Value="7">July</asp:ListItem>
                                            <asp:ListItem Value="8">August</asp:ListItem>
                                            <asp:ListItem Value="9">September</asp:ListItem>
                                            <asp:ListItem Value="10">October</asp:ListItem>
                                            <asp:ListItem Value="11">November</asp:ListItem>
                                            <asp:ListItem Value="12">December</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                         <div class="col-md-2">
                                        <label style="margin-right: 20PX;font-size:larger;">To Date:</label>

                                         <asp:TextBox ID="dtpkr_frmDate" CssClass="form-control input-sm BootDatepicker" placeholder="dd/MM/yyyy" Font-Size="10"  runat="server" onFocus="this.select()" autocomplete="off" required="required"></asp:TextBox>

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
                                        <asp:Button ID="btnDownload" runat="server" Text="Download" class="btn btn-primary"  OnClick="btnDownload_Click"  Visible="true"/> <%-- --%>
                                    </div>

                              

                                    <div class="clearfix"></div>
                                    <hr />
                              <%--  <h4>Transaction Detils</h4>--%>
                                
                        </div>
                         
                        </div>
                    </div>
                </div>
            </div>
      </div>
         <div class="col-lg-12">
            <div class="panel panel-primary">
                <div class="panel-heading" style="font-size:larger;">
                    Deposit Return Report          
                </div>

                <div>
                    <div class="panel-body">


                        <%--<div style="overflow: scroll;" runat="server" id="RepeaterControls">--%>
                         <div style ="height:350px; width:1195px; overflow:auto;">
                            <table width="100%" class="table table-striped table-bordered table-hover" id="dataTablesexample">
                                <thead>
                                    <tr>
                                        <th>SL.</th>
                                        <th>LDG CODE</th>
                                        <th>OPENIGN BALANCE</th>
                                        <th>RECIPT</th>
                                        <th>PAYMENT</th>
                                        <th>CLOSING BALANCE</th>
                                        <th>NO OF AC</th>
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

                                                    <asp:Label ID="lblold_acno" runat="server" Text='<%# Eval("LDG_CODE")%>'></asp:Label>

                                                </td>
                                                <td>

                                                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("OPENING_BAL")%>'></asp:Label>

                                                </td>
                                                <td>

                                                    <asp:Label ID="lblacctype" runat="server" Text='<%# Eval("RECIPT")%>'></asp:Label>

                                                </td>
                                                <td>

                                                <asp:Label ID="lbldateofopening" runat="server" Text='<%# Eval("PAYMENT")%>'></asp:Label>

                                               </td>
                                            
                                                 <td>

                                                <asp:Label ID="Label3" runat="server" Text='<%# Eval("CLOSING_BAL")%>'></asp:Label>

                                               </td>
                                                 <td>

                                                <asp:Label ID="Label4" runat="server" Text='<%# Eval("NO_OF_AC")%>'></asp:Label>

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
            <div class="col-lg-7">
                <div style="float: right; margin-top: 12px;">
                     
                    <a href="frmMisDepositReturn.aspx" class="btn btn-outline btn-danger">Cancel</a>
                   
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
