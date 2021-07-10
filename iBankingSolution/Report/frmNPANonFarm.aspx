<%@ Page Language="C#" MasterPageFile="~/MasterPages/ProjectBM.Master" AutoEventWireup="true" CodeBehind="frmNPANonFarm.aspx.cs" Inherits="iBankingSolution.Report.frmNPANonFarm" %>
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
                        NPA Non Farm Report 
                    </div>
                     <div class="panel-body">
                    <div class="row">

                        <div class="form-group">

                            <div class="col-md-4">
                                <label style="margin-right: 20PX; font-size:larger;">Select Scheme</label>

                          
                                <asp:DropDownList ID="cmbx_SelectScheme" runat="server" CssClass="form-control" AutoPostBack="false" ></asp:DropDownList> <%--OnSelectedIndexChanged="cmbx_CrropName_SelectedIndexChanged"--%>
                            </div>

                            <div class="col-md-2">
                                <label style="margin-right: 20PX;font-size:larger;">AsonDate.</label>
                                <asp:TextBox ID="dtpkr_AsonDate" CssClass="form-control input-sm BootDatepicker" placeholder="DD/MM/YYYY" Font-Size="10" runat="server" onFocus="this.select()" autocomplete="off" required="required"></asp:TextBox>
                        
                            </div>
                            <div class="col-md-2"><br />
                                <asp:Button ID="btnShow" runat="server" Text="Show Report" class="btn btn-primary" OnClick="btnShow_Click" /> <%----%>
                            </div>
                         
                            <div class="col-md-2" align="left"> <br />

                                <asp:DropDownList ID="ddlExportReport" runat="server" CssClass="form-control" class="styled-select" Font-Size="10" BackColor="White" ForeColor="Black">
                                    <asp:ListItem Value="0">WORD</asp:ListItem>
                                    <asp:ListItem Value="1">PDF</asp:ListItem>
                                    <asp:ListItem Value="2">EXCEL</asp:ListItem>
                                </asp:DropDownList>

                            </div>
                            <div class="col-md-2"><br />
                                <asp:Button ID="btnDownload" runat="server" Text="Download Report" class="btn btn-primary" OnClick="btnDownload_Click" />  <%----%>
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
                    <div class="panel-heading" style=" font-size:larger;">
                        NPA Non Farm Overdue List              
                    </div>
                    <%--<hr />--%>
                    <div class="panel-body">
                        <div style ="height:350px; width:1235px; overflow:auto;">
                        <%--<div style="overflow: scroll;" runat="server" id="RepeaterControls">--%>
                            <table width="100%" class="table table-striped table-bordered table-hover" id="dataTablesexample">


                                <thead>
                                    <tr>

                                        <th>Sr.</th>
                                        <th>Loan A/C No.</th>
                                        <%--<th>A/c No.</th>--%>
                                        <th>Name Of Borower</th>
                                        <th>Date Of Issue Loan</th>
                                        <th>Amount Of Loan Issue</th>
                                        <%--<th>Repayment Schedule</th>--%>
                                        <th>Repayment Mode </th>
                                        <th>Kisti Amt </th>
                                        <th>Date Of Overdue</th>
                                        <th>Balance Outstanding AsOn</th>
                                        <th>Principal</th>
                                        <th>Interest</th>
                                        <th>Overdue Interest</th>
                                        <th>Realisable Value Of Security</th>
                                        <%--<th>Standard</th>
                                        <th>Sub Standard</th>
                                        <th>Doubtfull Catagory</th>--%>
                                        <th>Current Overdue Upto 90 Days</th>
                                        <th>> 90Days upto3yrs</th>
                                        <th>D1 Secured > 3 Yrs < 4 Yrs</th>
                                        <th>D2 Secured > 4 Yrs < 6 Yrs</th>
                                        <th>D3 Secured > 6 Yrs</th>
                                        <th>Un-Secured</th>
                                        <th>Loss Assets</th>
                                        <th>Reason For Loss Assets</th>
                                       <%-- <th>Doubtfull Catagory of Unsecure</th>
                                        <th>Father/Husband</th>--%>
                                       
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="RepCCList" runat="server" > <%--OnItemDataBound="RepCCList_ItemDataBound"--%>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblsrL" runat="server" Text='<%# (((RepeaterItem)Container).ItemIndex+1).ToString()%>'></asp:Label>


                                                </td>

                                                <td>

                                                    <asp:Label ID="lblLF_NO" runat="server" Text='<%# Eval("AcNo")%>'></asp:Label>

                                                </td>

                     
                                                <td>

                                                    <asp:Label ID="lblName" runat="server" Text='<%# Eval("Name")%>'></asp:Label>

                                                </td>

                                                <td>

                                                    <asp:Label ID="lblsanc_dt" runat="server" Text='<%# Eval("ISSUEDT","{0:d}")%>'></asp:Label>

                                                </td>
                                                <td>

                                                    <asp:Label ID="lblnet_loan" runat="server" Text='<%# Eval("BALANCE")%>'></asp:Label>

                                                </td>


                                                <td>

                                                    <asp:label id="lblinst_amount" runat="server" text='<%# Eval("RepayMode")%>'></asp:label>

                                                </td>

                                                <td>

                                                    <asp:Label ID="Lbllast_rep_dt" runat="server" Text='<%# Eval("KISTI")%>'></asp:Label>

                                                </td>
                                                <td>

                                                    <asp:Label ID="lblBalance" runat="server" Text='<%# Eval("overduedt","{0:d}")%>'></asp:Label>

                                                </td>




                                                <td>

                                                    <asp:Label ID="lblloancurrint" runat="server" Text='<%# Eval("OutstandingAmt")%>'></asp:Label>

                                                </td>


                                                <td>

                                                    <asp:Label ID="lblvfloanodint" runat="server" Text='<%# Eval("OutstandingAmt")%>'></asp:Label>

                                                </td>
                                                <td>
                                                     <asp:Label ID="Label1" runat="server" Text='<%# Eval("Interest")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("drIntOverDue")%>'></asp:Label>
                                                </td>
                                                <td>
                                                 <asp:Label ID="Label6" runat="server"></asp:Label>
                                            </td>

                                             <td>

                                                <asp:Label ID="lblUpto90Days" runat="server" Text='<%# Eval("UptoDays") %>'></asp:Label>

                                            </td>

                                             <td>

                                                <asp:Label ID="lbl90Daysupto3yrs" runat="server" Text='<%# Eval("uptothree") %>'></asp:Label>

                                            </td>

                                             <td>
                                                <asp:Label ID="lbl3yrs4yrs" runat="server" Text='<%# Eval("D1") %>'></asp:Label>
                                            </td>

                                            <td>
                                                <asp:Label ID="lbl4yrs6yrs" runat="server" Text='<%# Eval("D2") %>'></asp:Label>
                                            </td>
                                            <td>

                                                <asp:Label ID="lblMorethan6yrs" runat="server" Text='<%# Eval("D3") %>'></asp:Label>

                                            </td>
                                            <td>
                                                 <asp:Label ID="Label3" runat="server"></asp:Label>
                                            </td>
                                            <td>
                                                 <asp:Label ID="Label4" runat="server"></asp:Label>
                                            </td>
                                            <td>
                                                 <asp:Label ID="Label5" runat="server"></asp:Label>
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
