<%@ Page Language="C#" MasterPageFile="~/MasterPages/ProjectBM.Master" AutoEventWireup="true" CodeBehind="frmShareAccountClosing.aspx.cs" Inherits="iBankingSolution.Transaction.frmShareAccountClosing" %>
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
               
                <%--<asp:Button ID="btnsubmit1" runat="server" Text="Calculate Maturity Amount" class="btn btn-primary"  Visible="false" /> --%><%--OnClick="btnsubmit_Click"--%>
                <asp:Button ID="btn_CloseAccount" runat="server" Text="CloseAccount" class="btn btn-primary"  Visible="false" OnClick="btn_CloseAccount_Click"/><%-- --%>
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" class="btn btn-primary" /> <%--OnClick="btnCancel_Click"--%> 
            </div>
            <h1 class="page-header">Share Account Closing</h1>
        </div>
    </div>
    <div class="row">
        <div class="col-lg-8">
            <div class="panel panel-primary">
                <div class="panel-heading" style="font-size:larger;">
                    Account Closing Entry
                </div>
                <div class="panel-body">
                    <div class="row">

                        <div class="form-group">
                            <div class="col-md-12">
                                <div class="panel panel-primary">

                                    <div class="panel-heading text-center" style="font-size:larger;">
                                        <b>Accounts Details</b>

                                    </div>
                                </div>
                            </div>
                            <div class="col-md-1">
                                <asp:Label ID="lblSession" runat="server" BackColor="White" ForeColor="White"></asp:Label>
                            </div>
                            <div class="clearfix"></div>
                            <div class="col-md-2">
                                <label style="margin-right: 20PX;font-size:larger;">Old A/c No</label>
                                <asp:TextBox ID="txtOldAcNo" placeholder="ENTER OLD A/C NO" Font-Size="10" runat="server" CssClass="form-control" AutoPostBack="True" />  <%--OnTextChanged="txtOldAcNo_TextChanged"--%>
                            </div>
                            <div class="col-md-2">
                                <label style="margin-right: 20PX;font-size:larger;">A/c No</label>
                                <asp:TextBox ID="txtAcNo" placeholder="ENTER A/C NO" Font-Size="10" runat="server" CssClass="form-control" AutoPostBack="True"  Width="100px" OnTextChanged="txtAcNo_TextChanged"/> <%----%>
                            </div>
                            <div class="col-md-2">
                                <label style="margin-right: 20PX;font-size:larger;">Opening DT</label>
                                <asp:TextBox ID="txtdtOpening" placeholder="DD/MM/YYYY" Font-Size="10" ReadOnly="true" CssClass="form-control" runat="server" onFocus="this.select()" autocomplete="off" Width="120px"></asp:TextBox>

                            </div>
                            <%-- Scripting Section --%>
                            <div class="col-md-3">
                                <label style="margin-right: 20PX;font-size:larger;">AcType</label>
                                <asp:TextBox ID="txtacctype" placeholder="Accont Type" Font-Size="10" ReadOnly="true" CssClass="form-control" runat="server"  AutoPostBack="false"></asp:TextBox> <%--OnTextChanged="txtacctype_TextChanged"--%>

                            </div>
                            <div class="col-md-3">
                                <label style="margin-right: 20PX;font-size:larger;">Last Trans.Dt</label>
                                <asp:TextBox ID="txtdtLastTrnsDt" placeholder="DD/MM/YYYY" Font-Size="10" ReadOnly="true" CssClass="form-control" runat="server" onFocus="this.select()" autocomplete="off"></asp:TextBox>

                            </div>
                           
               <div class="clearfix"></div><br /><br />
                        <asp:Panel runat="server" ID="maturityPanel" Visible="false">
                            <div class="form-group">
                                <div class="col-md-12">
                                    <div class="panel panel-primary">

                                        <div class="panel-heading text-center" style="font-size:larger;">
                                            <b>Collection Data</b>

                                        </div>
                                    </div>
                                </div>
                                
                                <div class="col-md-3">
                                     <label style="margin-right: 20PX;font-size:larger;">Maturity Instruction</label>
                                    <asp:DropDownList ID="ddlMaturityInst" runat="server" Filter="Contains" MarkFirstMatch="true" EmptyMessage="Select" CssClass="form-control" AutoPostBack="True"  Width="250px" OnSelectedIndexChanged="cmbx_Type_SelectedIndexChanged"> <%----%>
                                  
                                     <asp:ListItem Value="select">-- Select Maturity Instruction --</asp:ListItem>
                                     <asp:ListItem Value="CASH WITHDRAWL">Cash Withdrawl</asp:ListItem>
                                     <asp:ListItem Value="A/C TRANSFER">A/C Transfer</asp:ListItem>
                                     </asp:DropDownList>
                                 </div>
                                
                                <div class="col-md-1"></div>
                                <div class="col-md-3">
                                    <label style="margin-right: 20px;font-size:larger;">Transfer Account</label>
                                    <asp:TextBox ID="txtTransferAccount" runat="server" placeholder="Enter Transfer Account Number" Font-Size="10"  CssClass="form-control" Width="250px" />
                                    
                                </div>
                                <div class="col-md-1"></div>
                                <div class="col-md-3">
                                    <label style="margin-right: 20PX;font-size:larger;">MaturityAmt</label>
                                    <asp:TextBox ID="txt_MaturityAmt" runat="server" placeholder="MATURITY AMT" ReadOnly="true" Font-Size="10" CssClass="form-control" Width="250px" />
                                </div>
                                <div class="clearfix"></div><br />
                                <br />
                                <%--<div class="col-md-2">
                                    <label style="margin-right: 20px;font-size:larger;">TDS</label>
                                    <asp:TextBox ID="txttds" runat="server" placeholder="ENTER TDS" CssClass="form-control" Font-Size="10" width="250px"/>
                                </div>--%>
                                <%--<div class="col-md-2"></div>
                                <div class="col-md-3">
                                    <label style="margin-right: 20PX;font-size:larger;">MaturityAmt</label>
                                    <asp:TextBox ID="txt_MaturityAmt" runat="server" placeholder="MATURITY AMT" ReadOnly="true" Font-Size="10" CssClass="form-control" Width="250px" />
                                </div>--%>
                                <%--<div class="col-md-1"></div>--%>
                                <div class="col-md-2">
                                    <label style="margin-right: 20PX;font-size:larger;">WithdrawalDt.</label>
                                    <asp:TextBox ID="dtpkr_WithdrawlDate" runat="server" placeholder="Withdrawal Dt" Font-Size="10" CssClass="form-control input-sm BootDatepicker" Width="250px" />
                                </div>
                                
                                </div>
                      
                    </asp:Panel>
                </div>
            </div>
            </div>
                </div>
            </div>
        <div class="row">
            <div class="col-lg-4">
                <div class="panel panel-primary">
                    <div class="panel-heading" style="font-size:larger;">
                        Account Closing Entry
                    </div>
                    <div class="panel-body">



                        <asp:ListView ID="lv_AcctHolders" runat="server" RenderMode="Classic"
                            ItemPlaceholderID="AcctHoldersContainer" >  <%--OnSelectedIndexChanged="lv_AcctHolder_SelectedIndexChanged"--%>
                            <LayoutTemplate>
                                <fieldset class="layoutFieldset ">
                                    <legend>Account Holders</legend>
                                    <asp:PlaceHolder ID="AcctHoldersContainer" runat="server"></asp:PlaceHolder>
                                </fieldset>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <table>
                                    <tr>
                                        <td style="width: 75%; vertical-align: top;">
                                            <table style="width: 100%">
                                                <tr>
                                                    <td style="width: 40%; font-weight: bold;">CUST ID:</td>
                                                    <td style="width: 60%; padding-left: 2px"><%#Eval("CUST_ID")%></td>
                                                </tr>
                                                <tr>
                                                    <td style="font-weight: bold;">NAME:</td>
                                                    <td><%#Eval("NAME")%></td>
                                                </tr>
                                                <tr>
                                                    <td style="font-weight: bold;">GURDIAN NAME:</td>
                                                    <td><%# Eval("GUARDIAN_NAME")%></td>
                                                </tr>
                                                <tr>
                                                    <td style="font-weight: bold;">VILLAGE:</td>
                                                    <td><%# Eval("VILL_CODE")%></td>
                                                </tr>
                                                <tr>
                                                    <td style="font-weight: bold;">BLOCK:</td>
                                                    <td><%#Eval("BLK_CODE")%></td>
                                                </tr>
                                                <tr>
                                                    <td style="font-weight: bold;">DISTRICT:</td>
                                                    <td><%#Eval("DIS_CODE")%></td>
                                                </tr>
                                                <tr>
                                                </tr>
                                            </table>
                                        </td>
                                        <td class="contactPhoto" style="width: 25%">
                                            <asp:Image ID="Photo" ImageUrl="~/Images/Empty_DP.jpg" Shape="Square" runat="server" Height="120"></asp:Image>
                                            <asp:Button ID="btnShowPhoto" runat="server" Text="ShowPhoto" />
                                            <asp:Image ID="Sign" ImageUrl="~/Images/logo_sigclub.png" Shape="Wide" runat="server" Width="120" BackColor="WhiteSmoke"></asp:Image>
                                            <asp:Button ID="btnShowSign" runat="server" Text="ShowSign" />

                                        </td>
                                    </tr>

                                </table>


                            </ItemTemplate>
                        </asp:ListView>

                    </div>
                </div>
            </div>
        </div>

        </div>
    <div class="row">
        <div class="col-lg-5">
            <div style="float: right; margin-top: 12px;">
                <%--<asp:Button ID="btnsubmit" runat="server" Text="Calculate Maturity Amount" class="btn btn-primary"  Visible="false"/> --%>  <%--OnClick="btnsubmit_Click"--%>
                <asp:Button ID="btn_CloseAccount2" runat="server" Text="CloseAccount" class="btn btn-primary"  Visible="false" OnClick="btn_CloseAccount2_Click"/>       <%----%>
                <a href="frmShareAccountClosing.aspx" class="btn btn-outline btn-danger">Cancel</a>

            </div>
        </div>
    </div>

    <%-- Scripting Section Calander --%>
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
