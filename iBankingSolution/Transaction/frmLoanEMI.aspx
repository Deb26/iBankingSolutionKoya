<%@ Page Language="C#" MasterPageFile="~/MasterPages/ProjectBM.Master" AutoEventWireup="true" CodeBehind="frmLoanEMI.aspx.cs" Inherits="iBankingSolution.Transaction.frmLoanEMI" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
     <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.4.1/js/bootstrap-datepicker.min.js"></script>
         <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.4.1/css/bootstrap-datepicker3.css"/>
    <script type="text/javascript">
        $ = jQuery.noConflict();
        $(function () {
            $('.select2').select2()
        })
    </script>
    <style type="text/css">
        .form-control {}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="scrpt1" runat="server"></asp:ScriptManager>
    <div class="row">
        <div class="col-lg-12">
            <div style="float: right; margin-top: 12px;">
               
                 
                <%--<asp:Button ID="btnSave" runat="server" Text="Save" class="btn btn-primary"  Visible="true" />   
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" class="btn btn-primary"  /> --%> <%--OnClick="btnCancel_Click"--%>
            </div>
            <h1 class="page-header">Loan EMI</h1>
        </div>

    </div>
    <div class="row">
            <div class="col-lg-12" >
                <div class="panel panel-primary" style="font-size:larger;">
                    <div class="panel-heading text-center">
                        Loan EMI
                    </div>
                    <div class="panel-body">
                        <div class="row">

                            <div class="form-group">

                                <div class="col-md-5">
                                    
                                      <label style="margin-right: 20PX;">Loan Account Number</label>

                                <asp:DropDownList ID="cmbx_AccountNo" runat="server" CssClass="form-control" Font-Size="10" AutoPostBack="true" OnSelectedIndexChanged="cmbx_AccountNo_SelectedIndexChanged">
                                </asp:DropDownList>  <%--OnTextChanged="txt_OldAcctNo_TextChanged"--%>
                                 </div>
                                 <div class="col-md-2">
                                     <label style="margin-right: 20PX;"><font color="white"> Download Options:</font></label>   
                                  <asp:Button ID="Button1" runat="server" Text="Show" class="btn btn-primary"   Visible="true"/>   <%--OnClick="btnShow_Click" top: 150px;--%>
                                </div>
                                <div  class="col-md-3">
                                    <label style="margin-right: 20PX;">Download Options:</label>               
                                    <asp:DropDownList ID="ddlExportReport" runat="server" CssClass="form-control" class="styled-select" Font-Size="10" BackColor="White" ForeColor="Black">
                                        <asp:ListItem Value="0">WORD</asp:ListItem>
                                        <asp:ListItem Value="1">PDF</asp:ListItem>
                                        <asp:ListItem Value="2">EXCEL</asp:ListItem>
                                    </asp:DropDownList>

                                </div>
                                
                                
                                <div class="col-md-2">
                                    <label style="margin-right: 20PX;"><font color="white"> Download Options:</font></label>   
                                  <asp:Button ID="btnDownload" runat="server" Text="Download" class="btn btn-primary"   Visible="true"/> <%--OnClick="btnDownload_Click"--%>
                                </div>
                                <div class="clearfix"></div><br />
                                <div class="col-md-12">
                                    <div class="panel panel-primary">

                                        <div class="panel-heading text-center">
                                            <b>Loan Account Details</b>
                                        </div>
                                    </div>
                                </div>
                                  <div class="col-md-2">
                                      <label style="margin-right: 20px;">Loan Amount</label>
                                      <asp:TextBox ID="txtLAc" runat="server" placeholder="Loan Amount " Font-Size="10" CssClass="form-control" ></asp:TextBox>
                                  </div>
                                  <div class="col-md-2">
                                      <label style="margin-right: 20px;">ROI</label>
                                      <asp:TextBox ID="txtRoi" runat="server" placeholder="ROI" Font-Size="10" CssClass="form-control" ></asp:TextBox>
                                  </div>
                                  <div class="col-md-2">
                                      <label style="margin-right: 20px;">Period</label>
                                      <asp:TextBox ID="txtPeriod" runat="server" placeholder="Loan Period" Font-Size="10" CssClass="form-control" ></asp:TextBox>
                                  </div>
                                 <div class="col-md-3">
                                      <label style="margin-right: 20px;">Repay Mode</label>
                                      <asp:TextBox ID="txtRepayMode" runat="server" placeholder="Repay Mode" Font-Size="10" CssClass="form-control" ></asp:TextBox>
                                  </div>
                                 <div class="col-md-3">
                                     <label style="margin-right: 20px;">Loan Opening Date</label>
                                     <asp:TextBox ID="txtopeningdt" runat="server" placeholder="Opening Date" Font-Size="10" CssClass="form-control"></asp:TextBox>
                                 </div>
                                  
                                 <div class="clearfix"></div><br />
                                  
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
        </div>
       <div class="row">
            <div class="col-lg-12">
                <div class="panel panel-primary" style="font-size:larger;">
                    <div class="panel-heading text-center">
                        Loan EMI Report              
                    </div>
                    <div class="panel-body">
                        <div style="overflow: scroll;" runat="server" id="RepeaterControls"> 
                        <table width="100%" class="table table-striped table-bordered table-hover" id="dataTablesexample">


                            <thead>
                                <tr>
                                     <th>Srl.</th>
                                    <th>Date</th>
                                    <th>EMI Amount</th>
                                    <th>Principal</th>
                                    <th>Interest</th>
                                    <th>Balance</th>
                             
                                 
                                    <%--<th>Int.Overdue</th>
                                     <th>Int.Demand</th> 
                                     <th>Print Out</th>  --%>
                                </tr>
                            </thead>
                            <tbody>
                                <%--<asp:Repeater ID="RepCCList" runat="server" OnItemDataBound="RepCCList_ItemDataBound">
                                    <ItemTemplate>
                                        <tr>
                                             <td>
                                                <asp:Label ID="lblsrL" runat="server" Text='<%# (((RepeaterItem)Container).ItemIndex+1).ToString()%>'></asp:Label>


                                            </td> 

                                            <td>

                                                <asp:Label ID="lbldt" runat="server" Text='<%# Eval("Date" ,"{0:dd/MM/yyyy}")%>'></asp:Label>

                                            </td>

                                            <td>

                                                <asp:Label ID="lblEmiAmount" runat="server" Text='<%# Eval("EMI Amount")%>'></asp:Label>

                                            </td>
                                            <td>

                                                <asp:Label ID="lblPrincipal" runat="server" Text='<%# Eval("Principal")%>'></asp:Label>

                                            </td>
                                            <td>

                                                <asp:Label ID="lblInterest" runat="server" Text='<%# Eval("Interest")%>'></asp:Label>

                                            </td>

                                            <td>

                                                <asp:Label ID="lblBalance" runat="server" Text='<%# Eval("Balance")%>'></asp:Label>

                                            </td>

                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>--%>

                            </tbody>
                        </table>
                        </div>
                       </div>
                    </div>
                </div>
           </div>
     <div class="row">
        <div class="col-lg-6">
            <div style="float: right; margin-top: 12px;">
                <%--<asp:Button ID="btnSave2" runat="server" Text="Save" class="btn btn-primary"  Visible="true" />  --%><%--OnClick="btn_CloseAccount2_Click"--%>
                <a href="frmLoanEMI.aspx" class="btn btn-outline btn-danger">Cancel</a>

            </div>
        </div>
   
    </div><br /><br /><br />
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
