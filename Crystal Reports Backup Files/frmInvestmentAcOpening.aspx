<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/ProjectBM.Master" CodeBehind="frmInvestmentAcOpening.aspx.cs" Inherits="iBankingSolution.Transaction.frmInvestmentAcOpening" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        $ = jQuery.noConflict();
        $(function () {
            $('.select2').select2()
        })
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <form id="formInitialSetting" runat="server">
        <asp:ScriptManager ID="scrpt1" runat="server"></asp:ScriptManager>
        <div class="row">
            <div class="col-lg-12">
                <div style="float: right; margin-top: 12px;">
                    <asp:Button ID="btnsubmit1" runat="server" Text="Save" class="btn btn-primary" OnClick="btnsubmit_Click" />
                     
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" class="btn btn-primary" OnClick="btnCancel_Click" />
                    <a href="frmInvestmentAccountOpeningList.aspx" class="btn btn-default">Back to List</a>
                </div>
                <h1 class="page-header">Investment Account Opening</h1>
            </div>
            <!-- /.col-lg-12 -->
        </div>
        <div class="row">
            <div class="col-lg-12">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        SHG Account Opening
                    </div>
                    <div class="panel-heading" runat="server" id="DivID" visible="false">
                        <asp:Label ID="Label1" runat="server" Visible="False" align="center" Font-Bold="True" Font-Italic="False" Font-Names="Verdana" ForeColor="White"></asp:Label>
                    </div>
                    <div class="panel-body">
                        <div class="row">

                            <div class="form-group">

                                <div class="col-md-3">
                                    <label style="margin-right: 20PX;">Dt of Open</label><asp:Label ID="lblDid" runat="server" BackColor="White" ForeColor="White"></asp:Label>
                                    <asp:TextBox ID="dtpkr_dateopen" CssClass="form-control input-sm BootDatepicker" runat="server" onFocus="this.select()" autocomplete="off" required="required"></asp:TextBox>
                                    

                                </div>
                                <div class="col-md-3">
                                    <label style="margin-right: 20PX;">Investment Type</label>
                                      <asp:Label ID="Label3" runat="server" BackColor="White" ForeColor="White"></asp:Label>
                                    <asp:DropDownList ID="cmbx_InvestmentType" runat="server" CssClass="form-control" required="required" AutoPostBack="True" OnSelectedIndexChanged="cmbx_InvestmentType_SelectedIndexChanged">

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
                                <div class="col-md-3">
                                    <label style="margin-right: 20PX;">Deposit Scheme</label>
                                    <asp:DropDownList ID="cmbx_DepositScheme" runat="server" CssClass="form-control" placeholder="Select Deposit Scheme"></asp:DropDownList>
                                </div>
                                <div class="col-md-3">
                                    <label style="margin-right: 20PX;">Effected Ac</label>
                                      <asp:DropDownList ID="cmbx_EffectedAcct" runat="server" CssClass="form-control" placeholder="Select Effected Ac">
                                           <asp:ListItem Value="0">--Select Type--</asp:ListItem>
                                        <asp:ListItem Value="Society">Society</asp:ListItem>
                                        <asp:ListItem Value="Stuff">Stuff</asp:ListItem>
                                         
                                      </asp:DropDownList>
                                </div>
                                <div class="col-md-6">
                                    <label style="margin-right: 20PX;">Total Cirtificate</label>
                                    <asp:TextBox ID="ntxt_TotCertificate" CssClass="form-control" runat="server" OnTextChanged="ntxt_TotCertificate_TextChanged" AutoPostBack="True" ReadOnly="True">1</asp:TextBox>
                                </div>
                              
                                <div class="col-md-12" title="Cirtificate Detail">
                                    <label style="margin-right: 20PX;">Cirtificate Detail</label>
                                    <asp:GridView ID="GVApplicantDtl" runat="server" AutoGenerateColumns="False"
                                        CssClass="table table-bordered table-striped table-hover" EmptyDataText="No Data Found" GridLines="Both">
                                        <Columns>
                                            <asp:TemplateField HeaderText="SL" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_SlNo" Text='<%# Bind("SlNo") %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Receipt No">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txt_ReceiptNo" CssClass="form-control input-sm" runat="server"
                                                        autocomplete="off" ForeColor="Black" Text='<%# Eval("ReceiptNo") %>' onFocus="this.select()" OnTextChanged="txt_ReceiptNo_TextChanged" AutoPostBack="True"></asp:TextBox>

                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Certificate No">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txt_CertificateNo" CssClass="form-control input-sm" runat="server"
                                                        autocomplete="off" ForeColor="Black" Text='<%# Eval("CertificateNo") %>' onFocus="this.select()"></asp:TextBox>

                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Date of Purchase">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="dtpkr_DateOfPurchase" CssClass="form-control input-sm BootDatepicker" runat="server"
                                                        autocomplete="off" ForeColor="Black" Text='<%# Eval("DateOfPurchase") %>' onFocus="this.select()"></asp:TextBox>

                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="A/C No">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txt_AcctNo" CssClass="form-control input-sm" runat="server"
                                                        autocomplete="off" ForeColor="Black" Text='<%# Eval("AcctNo") %>' onFocus="this.select()"></asp:TextBox>

                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Bank Name">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txt_BankName" CssClass="form-control input-sm" runat="server"
                                                        autocomplete="off" ForeColor="Black" Text='<%# Eval("BankName") %>' onFocus="this.select()"></asp:TextBox>

                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
 



                                        </Columns>
                                    </asp:GridView>

                                </div>
                                <div class="clearfix"></div>

                            </div>
                            <div class="form-group" title="Account Details">
                                 <label style="margin-right: 20PX;">Account Details</label>
                                
                                 <div class="form-group">
                                <div class="col-md-3">
                                    <label style="margin-right: 20PX;">Int Type</label>
                                    <asp:DropDownList ID="cmbx_IntType" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                                        <asp:ListItem Value="Normal" Selected="True">Normal</asp:ListItem>
                                        <asp:ListItem Value="Sr. Cirtizen">Sr. Cirtizen</asp:ListItem>
                                        <asp:ListItem Value="DBS">DBS</asp:ListItem>

                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-3">
                                    <label style="margin-right: 20PX;">Period in Month+days</label> 
                                    <asp:ImageButton ID="Image1" runat="server" Height="20px" ImageUrl="~/Content/images/refresh.png" AlternateText="Refresh" OnClick="Image1_Click" ValidationGroup="false"/>
                                    <br />
                                    <asp:TextBox ID="txt_PeriodsinMonth" runat="server" Width="116px" Height="35" OnTextChanged="txt_PeriodsinMonth_TextChanged" onkeypress="return isNumberKey(event)" AutoPostBack="True" MaxLength="3" />
                                    <asp:TextBox ID="txt_PeriodsInDays" Width="116px" Height="35" runat="server" AutoPostBack="True" OnTextChanged="txt_PeriodsInDays_TextChanged" onkeypress="return isNumberKey(event)" MaxLength="3" />
                                </div>
                                <div class="col-md-3">
                                    <label style="margin-right: 20PX;">ROI</label>
                                    <asp:TextBox ID="ntxt_ROI" runat="server" CssClass="form-control" AutoPostBack="True" required="required" />
                                </div>
                                <div class="col-md-3">
                                    <label style="margin-right: 20PX;">Deposit Amount</label>
                                    <asp:TextBox ID="ntxt_DepositAmt" runat="server" CssClass="form-control" onkeypress="return isNumberKey(event)" AutoPostBack="True" OnTextChanged="ntxt_DepositAmt_TextChanged" required="required" />
                                </div>

                                <div class="clearfix"></div>
                            </div>

                            <div class="form-group">
                                <div class="col-md-4">
                                    <label style="margin-right: 20PX;">Date of Maturity</label>
                                    <asp:TextBox ID="dtpkr_MaturityDate" CssClass="form-control input-sm BootDatepicker" runat="server" onFocus="this.select()" autocomplete="off" required="required" Enabled="False"></asp:TextBox>
                                </div>
                                <div class="col-md-4">
                                    <label style="margin-right: 20PX;">Maturity Amount</label>
                                    <asp:TextBox ID="ntxt_MaturityAmt" runat="server" CssClass="form-control" Enabled="False" />

                                </div>
                                


                                <div class="clearfix"></div>
                            </div>



                                <div class="clearfix"></div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>
        
        <div class="row">
            <div class="col-lg-12">
                <div style="float: right; margin-top: 12px;">
                    <asp:Button ID="btnsubmit" runat="server" Text="Save" class="btn btn-primary" OnClick="btnsubmit_Click" />
                    
                    <a href="#" class="btn btn-outline btn-danger">Cancel</a>

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


    </form>
</asp:Content>
