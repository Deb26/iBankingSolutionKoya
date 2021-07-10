<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/ProjectBM.Master" CodeBehind="frmStockTransaction.aspx.cs" Inherits="iBankingSolution.stock.frmStockTransaction" %>

<%@ Register TagPrefix="Ajax" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>
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
            <h1 class="page-header">Stock Transaction</h1>
        </div>

    </div>
    <div class="row">
        <div class="col-lg-8">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    Stock Transaction Entry
                </div>
                <div class="panel-body">
                    <div class="row">

                        <div class="form-group">
                            <div class="col-md-12">
                                <div class="panel panel-primary">

                                    <div class="panel-heading text-center">
                                        <b>Stock Details</b>

                                    </div>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <label style="margin-right: 20PX;">Select Entry Type</label>
                                <asp:DropDownList ID="cmbx_entrytype" runat="server" CssClass="form-control" Font-Size="10" AutoPostBack="True" OnSelectedIndexChanged="cmbx_entrytype_SelectedIndexChanged">
                                    <asp:ListItem Value="0">--Select--</asp:ListItem>
                                    <asp:ListItem Value="Purchase">Purchase</asp:ListItem>
                                    <asp:ListItem Value="Sale">Sale</asp:ListItem>

                                </asp:DropDownList>

                            </div>

                            <div class="col-md-3">
                                <label style="margin-right: 20PX;">Entry Date</label>
                                <asp:TextBox ID="txt_entrydt" placeholder="DD/MM/YYYY" Font-Size="10" CssClass="form-control input-sm BootDatepicker" runat="server" required="required"></asp:TextBox>

                            </div>
                           <%-- <div class="col-md-4">
                                <label style="margin-right: 20PX;">Voucher No</label>
                                <asp:TextBox ID="txt_voucherno" placeholder="VOUCHER NO" Font-Size="10" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>--%>

                    
                            <div class="col-md-6">
                                <label style="margin-right: 20PX;"><b>Category Name</b></label>
                                <asp:DropDownList ID="cmbx_cateName" runat="server" CssClass="form-control" Font-Size="10" required="required" AutoPostBack="True" OnSelectedIndexChanged="cmbx_cateName_SelectedIndexChanged">
                                </asp:DropDownList>
                            
                            </div>
                            <div class="clearfix"></div><br />
                             <div class="col-md-3">
                                <label style="margin-right: 20PX;"><b>Trans.Type</b></label>
                                <asp:DropDownList ID="cmbs_transType" runat="server" CssClass="form-control" Font-Size="10" required="required" AutoPostBack="True" OnSelectedIndexChanged="cmbs_transType_SelectedIndexChanged">
                                    <asp:ListItem>--Select--</asp:ListItem>
                                    <asp:ListItem>Cash</asp:ListItem>
                                    <asp:ListItem>Credit</asp:ListItem>
                                </asp:DropDownList>
                                
                            </div>

                            <div class="col-md-5">
                                <label style="margin-right: 20PX;"><b>Sundry Dr./Sundry Cr.</b></label>
                                 <asp:DropDownList ID="cmbx_SundryDRCR" runat="server" CssClass="form-control" Font-Size="10"  AutoPostBack="True" OnSelectedIndexChanged="cmbx_SundryDRCR_SelectedIndexChanged">
                                   
                                </asp:DropDownList>
                                
                            </div>
                              <div class="col-md-4">
                                <%--<label style="margin-right: 20PX;"><b>Sales Ledger.</b></label>
                                 <asp:DropDownList ID="cmbx_SaleLedger" runat="server" CssClass="form-control" Font-Size="10"  AutoPostBack="True">
                                   
                                </asp:DropDownList>--%>
                                
                            </div>

                            <div class="col-md-2">
                                <label style="margin-right: 20PX;"><b>Code.</b></label>
                  
                                <asp:TextBox ID="txtCode" runat="server" width="90px" Enabled="false"></asp:TextBox>
                                
                            </div>

                        <div class="clearfix"></div>
                       
                      

                         <%--   <div class="col-md-3">
                                <label style="margin-right: 20PX;">Purchase Rate</label>
                                <asp:TextBox ID="txt_purchaserate" placeholder="PURCHASE(RS)" Font-Size="10" Height="25" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>

                            <div class="col-md-3">
                                <label style="margin-right: 20PX;">Basic Rate</label>
                                <asp:TextBox ID="txt_basicrate" placeholder="BASIC(RS)" Font-Size="10" Height="25" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>--%>


                        </div>

                        <div class="clearfix"></div>
                         <div class="form-group">
                              <div class="col-md-4">
                                <label style="margin-right: 20PX;">NAME</label>
                                <asp:TextBox ID="txt_name" placeholder=" ENTER NAME" Font-Size="10" CssClass="form-control" runat="server" Style="text-transform: uppercase;" AutoPostBack="True" OnTextChanged="txt_name_TextChanged"></asp:TextBox>
                                    <ajax:autocompleteextender ServiceMethod="SearchCustomer"
                                                        MinimumPrefixLength="2"
                                                        CompletionInterval="50" EnableCaching="false" CompletionSetCount="3"
                                                        TargetControlID="txt_name"
                                                        ID="AutoCompleteExtender7" runat="server" FirstRowSelected="false">
                                                    </ajax:autocompleteextender>
                            </div>
                            <div class="col-md-4">
                                <label style="margin-right: 20PX;">Address</label>
                                <asp:TextBox ID="txt_address" placeholder=" ENTER ADDRESS" Font-Size="10" CssClass="form-control" runat="server"></asp:TextBox>
                                <br />
                            </div>
                            <div class="col-md-2">
                                <label style="margin-right: 20PX;">GST No</label>
                                <asp:TextBox ID="txt_gstno" placeholder=" GST NO" Font-Size="10" Height="25" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-2">
                                <label style="margin-right: 20PX;">IDcard No</label>
                                <asp:TextBox ID="txt_idcardno" placeholder=" ENTER CODE" Font-Size="10" Height="25" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>

                    </div>





                </div>
            </div>
        </div>



        <div class="row">
            <div class="col-lg-4">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        Stock Basic Entry
                    </div>
                    <div class="panel-body">

                        <%-- <div class="col-md-6">
                            <label style="margin-right: 20PX;">Edit Voucher</label>
                            <asp:TextBox ID="txt_editvoucher" placeholder="ENTER INT CREDIT DATE" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>

                        <div class="col-md-6">
                            <label style="margin-right: 20PX;">Entry Type</label>
                            <asp:TextBox ID="txt_entrytype" placeholder="ENTER INT CREDIT DATE" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-md-6">
                            <label style="margin-right: 20PX;">Session</label>
                            <asp:TextBox ID="txt_session" placeholder="ENTER INT CREDIT DATE" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-md-6">
                            <label style="margin-right: 20PX;">Mail ID</label>
                            <asp:TextBox ID="txt_mailid" placeholder="ENTER INT CREDIT DATE" CssClass="form-control" runat="server" ></asp:TextBox>
                            <br />
                        </div>--%>


                        <div class="form-group">
                            <div class="col-md-12">
                                <div class="panel panel-primary">



                                    <div class="panel-heading" align="center">
                                        <asp:Button ID="btnVoucher" runat="server" class="btn btn-warning" Text="Create Voucher" OnClick="btnVoucher_Click" />


                                    </div>
                                </div>
                            </div>
                            <asp:Panel ID="panelVoucher" runat="server" Visible="false">
                                    <div class="col-md-6">
                                 
                                        <label style="margin-right: 20PX;">Taxable Amount</label>
                                        <asp:TextBox ID="txttaxableamtt" placeholder="TAXABLE AMOUNT" Font-Size ="10" runat="server" CssClass="form-control"></asp:TextBox>
                                
                                        <br />
                                    </div>
                       
                                    <div class="col-md-6">
                                        <label style="margin-right: 20PX;">Discount(%)</label>
                                        <asp:TextBox ID="txtdiscountper" placeholder="ENTER DISCOUNT IN PERCENTAGE" runat="server" Font-Size="10" CssClass="form-control" AutoPostBack="True" OnTextChanged="txtdiscountper_TextChanged"></asp:TextBox>
                                        <br />
                                    </div>
                                  <div class="col-md-6">
                                        <label style="margin-right: 20PX;">Discount Amt.</label>
                                        <asp:TextBox ID="txtDisAmt" placeholder="Discount Amt" runat="server" Font-Size="10" CssClass="form-control" AutoPostBack="True" OnTextChanged="txtDisAmt_TextChanged"></asp:TextBox>
                                        <br />
                                    </div>
                                    <div class="col-md-6">
                                        <label style="margin-right: 20PX;">TotalBill Amount</label>
                                        <asp:TextBox ID="totbillamtt" placeholder="TOTALBILL" Font-Size="10" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                      <asp:Button ID="btnsubmit1" runat="server" Text="Save" class="btn btn-primary" OnClick="btnsubmit1_Click" />
                                     <%--<center> <asp:Button ID="btnsubmit1" runat="server" Text="Save" class="btn btn-primary" OnClick="btnsubmit1_Click" /></center> --%>
                            </asp:Panel>
                        </div>


                    </div>


                </div>
            </div>
        </div>

    </div>


    <%-- gridview for --%>

    <div class="row">
        <div class="col-lg-12">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    Stock Entry Details
                </div>
                <div class="panel-body">
                    <div>

                        <asp:Panel ID="gv_panel" runat="server" ScrollBars="Vertical">
                            <%--<asp:UpdatePanel ID="UpdatePanel11" runat="server">--%>
                                <%--<ContentTemplate>--%>

                                    <asp:GridView ID="GV_GSTVIEW" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped table-hover" EmptyDataText="No Data Found" OnRowDeleting="GV_GSTVIEW_RowDeleting" ShowFooter="True">
                                        <Columns>
                                            <asp:TemplateField HeaderText="SL. NO" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <%--<%# Container.DataItemIndex + 1 %>--%>
                                                    <asp:Label ID="lbl_SlNo" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Item Name">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="cmbx_itemname" runat="server"  AutoPostBack="True" OnSelectedIndexChanged="cmbx_itemname_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Current Stock">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtcurrstatus" Width="50px" runat="server" Text='<%# Bind("CurStock") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Qty">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtQty"  Width="50px" runat="server" Text='<%# Bind("MU") %>' AutoPostBack="True" OnTextChanged="txtQty_TextChanged"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            
                                              <asp:TemplateField HeaderText="Unit">
                                                <ItemTemplate>
                                              
                                                 <asp:TextBox ID="txt_Unit"  Width="50px" Enabled="false" runat="server"  Text='<%# Bind("MU") %>' AutoPostBack="True" OnTextChanged="txtrateunit_TextChanged"></asp:TextBox>
                                                    <asp:DropDownList ID="cmbxUnit" runat="server">
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Rate">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtrateunit"  Width="50px" runat="server"  Text='<%# Bind("Rate") %>' AutoPostBack="True" OnTextChanged="txtrateunit_TextChanged"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="TotalAmt">
                                                <FooterTemplate>
                                                    <asp:Label ID="lblGrossAmt" runat="server" Text="Total"></asp:Label>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txttotamt"  Width="100px" Enabled="false" runat="server" Text='<%# Bind("totAmt") %>'></asp:TextBox>
                                                </ItemTemplate>

                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="CGST%">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtcgst" runat="server" Enabled="false" Width="50px" Text='<%# Bind("CGST") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="SGST%">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtsgst" runat="server" Enabled="false" Width="50px" Text='<%# Bind("SGST") %>' AutoPostBack="True" OnTextChanged="txtsgst_TextChanged"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="CGSTAMT">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtcgstamt" runat="server"  Enabled="false"  Width="50px" Text='<%# Bind("cgstAmt") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="SGSTAMT">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtsgstamt" runat="server" Enabled="false"  Width="50px" Text='<%# Bind("sgstAmt") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="TOTALAMT">
                                                <FooterTemplate>
                                                    <asp:Label ID="lblNetAmt" runat="server" Text="Total"></asp:Label>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txttotalamt" runat="server" Enabled="false" Width="50px" Text='<%# Bind("grandTotal") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                          <asp:TemplateField HeaderText="BatchNo">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtbatchno"  Width="100px" runat="server" Text='<%# Bind("Batch_no") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ExpDate">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtexpdate" placeholder="DD/MM/YYYY" CssClass="form-control input-sm BootDatepicker"  Width="150px" runat="server" Text='<%# Bind("EXP_DT", "{0:dd/MM/yyyy}") %>'></asp:TextBox>
                                                </ItemTemplate>

                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="HSNCODE">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txthsnno"  Width="100px" runat="server" Text='<%# Bind("HSNNO") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                              <asp:TemplateField HeaderText="PurLDG" Visible="true">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtPurLdg"  enabled="false" Width="100px" runat="server" Text='<%# Bind("Pur_Ldg") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="SaleLDG" Visible="true">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtSaleLdg" enabled="false" Width="100px" runat="server" Text='<%# Bind("Sale_ldg") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="itbnNew" runat="server" CausesValidation="false" Height="18"
                                                        ImageUrl="~/Content/images/add.png" Width="18" OnClick="itbnNew_Click" />
                                                    <asp:ImageButton ID="itbndelete" runat="server" CausesValidation="false"
                                                        CommandName="Delete" Height="18" ImageUrl="~/Content/images/delete.png" Width="18" OnClick="itbndelete_Click" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:GridView>




                                <%--</ContentTemplate>--%>

                            <%--</asp:UpdatePanel>--%>

                        </asp:Panel>
                    </div>

                </div>
            </div>
        </div>
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
