<%@ Page Language="C#" MasterPageFile="~/MasterPages/ProjectBM.Master" AutoEventWireup="true" CodeBehind="frmVoucherAuthorization.aspx.cs" Inherits="iBankingSolution.Transaction.frmVoucherAuthorization" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.4.1/js/bootstrap-datepicker.min.js"></script>
         <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.4.1/css/bootstrap-datepicker3.css"/>
     <script type="text/javascript">
         $ = jQuery.noConflict();
         $(function () {
             $('.select2').select2()
         })

         function isNumberKey(evt) {
             var charCode = (evt.which) ? evt.which : event.keyCode
             if (charCode > 31 && (charCode < 48 || charCode > 57))
                 return false;
             return true;
         }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="scrpt1" runat="server" EnablePageMethods="true"></asp:ScriptManager>
    <div class="row">
        <div class="col-lg-12">
            <div style="float: right; margin-top: 12px;">
            </div>
            <h1 class="page-header">Voucher Authorization</h1>
        </div>
        <!-- /.col-lg-12 -->
    </div>
    <div class="row">
        <!-- /.col-lg-12 -->
        <div class="row">
            <div class="col-lg-12">
                <div class="panel panel-primary">
                    <div class="panel-heading" style="font-size:larger;">
                        Voucher Authorization
                    </div>
                    <div class="panel-heading" runat="server" id="DivID" visible="false">
                        <asp:Label ID="Label1" runat="server" Visible="False" align="center" Font-Bold="True" Font-Italic="False" Font-Names="Verdana" ForeColor="White"></asp:Label>
                    </div>
                    <div class="panel-body">
                        <div class="row">

                            <div class="form-group">

                                <div class="col-xs-2"  padding-right: 0;">

                                    <label style="margin-right: 20PX;font-size:larger;">Voucher Date:</label>

                                    <asp:TextBox ID="dtpkr_VoucherDate" CssClass="form-control input-sm BootDatepicker" runat="server" onFocus="this.select()" autocomplete="off" required="required"></asp:TextBox>
                                </div>


                                <div class="col-xs-3" style="width: 20%; padding-right: 0;">
                                    <label style="margin-right: 20PX;font-size:larger;">Voucher Type:</label>
                                    <asp:DropDownList ID="cmbx_VoucherType" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="cmbx_VoucherType_SelectedIndexChanged">
                                        <asp:ListItem Value="0">----Select-----</asp:ListItem>
                                        <asp:ListItem Value="Journal">Journal</asp:ListItem>
                                        <asp:ListItem Value="Cash">Cash</asp:ListItem>
                                        <%-- <asp:ListItem Value="3">Loan</asp:ListItem>--%>
                                    </asp:DropDownList>
                                </div>





                                <%--<div class="col-xs-3" style="width: 35%; padding-right: 0;">
                                    <label style="margin-right: 20PX;font-size:larger;">Select Voucher No:</label>
                                    <asp:DropDownList ID="cmbx_VoucherNo" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="cmbx_VoucherNo_SelectedIndexChanged">
                                        <asp:ListItem>-----Loading-----</asp:ListItem>

                                    </asp:DropDownList>
                                </div>--%>

                                <br />

                                <div class="col-xs-3" style="width: 15%;">

                                    <asp:Button ID="btn_Update" runat="server" Text="Authorised"  class="btn btn-primary" OnClick="click_btn_Update" Visible="false"/>
                                    <%--<asp:Button ID="btn_delete" runat="server" Text="Delete" OnClick="click_btn_delete" class="btn btn-primary"  />--%>
                                    
                                </div>
                                <div class="col-xs-3" style="width: 15%;">
                                </div>
                            </div>
                             <div align="center">
                                <div class="row">
                                    <div class="col-lg-12">
                                        <div style="float: right; margin-top: 12px;">
                                            <div class="horizontal-scroll-wrapper squares">
                                                <br />

                                                <br />

                                                <br />
                                                <br />
                                            </div>
                                        </div><br /><br /><br /><br /><br /><br />
                                        <asp:GridView ID="GVMTransaction" runat="server" AutoGenerateColumns="False" CellPadding="3" Height="188px" Width="1254px" ForeColor="#333333" CssClass="table table-bordered table-striped table-hover"
                                            EmptyDataText="No Data Found" GridLines="Both" 
                                             ShowFooter="True" OnRowDataBound="GVMTransaction_RowDataBound" >
                                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                            <Columns>
                                                

                                                <asp:TemplateField HeaderText="VOUCHER NUMBER" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txt_voucherNo" CssClass="form-control input-sm" runat="server" Text='<%# Bind("VouchNo") %>'></asp:TextBox>
                                                       <%-- <asp:Label ID="txt_voucherNo" runat="server" Text='<%# Bind("[VouchNo]") %>'></asp:Label>--%>
                                                    </ItemTemplate>
                                                     <ItemStyle HorizontalAlign="Left" />
                                                     <HeaderStyle HorizontalAlign="Left" />

                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="LDG CODE" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txt_Ldg_Code" CssClass="form-control input-sm" runat="server" Text='<%# Bind("LDG_CODE") %>'></asp:TextBox>
                                                        <%--<asp:TextBox ID="txt_Ldg_Code" runat="server" Text='<%# Bind("[LDG_CODE]") %>' CssClass="form-control input-sm" AutoPostBack="True" ></asp:TextBox>--%>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" />
                                                     <HeaderStyle HorizontalAlign="Left" />
                                                    <%--  <ItemTemplate>
                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("[Leadger Code]") %>'></asp:LabelOnTextChanged="txt_Ldg_Code_TextChanged" OnTextChanged="txt_Sl_Code_TextChanged">
                        </ItemTemplate>--%>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="SL CODE" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txt_Sl_Code" CssClass="form-control input-sm" runat="server" Text='<%# Bind("SL_CODE") %>'></asp:TextBox>
                                                        <%--<asp:TextBox ID="txt_Sl_Code" runat="server" CssClass="form-control input-sm" Text='<%# Bind("[SL_CODE]") %>' ></asp:TextBox>--%>
                                                    </ItemTemplate>
                                                     <ItemStyle HorizontalAlign="Left" />
                                                     <HeaderStyle HorizontalAlign="Left" />
                                                    <%-- <ItemTemplate>
                            <asp:Label ID="Label3" runat="server" Text='<%# Bind("[Subledger Code]") %>'></asp:Label>
                        </ItemTemplate>--%>
                                                </asp:TemplateField>



                                                <asp:TemplateField HeaderText="AMOUNT DEBIT" HeaderStyle-HorizontalAlign="Left">
                                                    <FooterTemplate>
                                                        <asp:Label ID="lbl_totdebit" runat="server" ></asp:Label>
                                                      
        
                                                        <asp:Label ID="txt_totdeb" runat="server" ></asp:Label>
                                                    </FooterTemplate>



                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txt_Amt_Debit" runat="server" CssClass="form-control input-sm" Text='<%# Bind("AMT_DEBIT") %>'></asp:TextBox>
                                                        <%--<asp:TextBox ID="txt_Amt_Debit" runat="server" CssClass="form-control input-sm" Text='<%# Bind("[AMT_DEBIT]") %>' AutoPostBack="True" ></asp:TextBox>--%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="AMOUNT CREDIT">
                                                    <FooterTemplate>
                                                        <asp:Label ID="lbl_totcredit" runat="server"></asp:Label>
                                                        
                                  
                                                        <asp:Label ID="txt_totcre" runat="server"></asp:Label>
                                                    </FooterTemplate>



                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txt_Amt_Credit" runat="server" CssClass="form-control input-sm" Text='<%# Bind("[AMT_CREDIT]") %>' AutoPostBack="True" ></asp:TextBox>
                                                    </ItemTemplate>

                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="VOUCHERDATE">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txt_VoucherDate" runat="server" CssClass="form-control input-sm BootDatepicker" Text='<%# Bind("[VoucherDate]") %>'></asp:TextBox>

                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="REMARKS">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txt_remarks" runat="server" CssClass="form-control input-sm" AutoPostBack="true" OnTextChanged="txt_Remers_TextChanged"></asp:TextBox>

                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="POSTED BY">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txt_empname" runat="server" CssClass="form-control input-sm" Text='<%# Bind("[employeeName]") %>' Enabled="false"></asp:TextBox>

                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="CCB CODE NEW" >
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtccbcodenew" runat="server" CssClass="form-control input-sm" Text='<%# Bind("CCBCodeNew") %>'></asp:TextBox>
                                                       
                                                    </ItemTemplate>
                                                    
                                                </asp:TemplateField>
                                                <%--  <ItemTemplate>
                            <asp:Label ID="Label6" runat="server" Text='<%# Bind("Date") %>'></asp:Label>
                        </ItemTemplate>--%>
                                               <%-- <asp:TemplateField HeaderText="HEADNAME">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txt_HeadName" runat="server" Enabled="false" CssClass="form-control input-sm" Text='<%# Bind("[HeadName]") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>

                                            </Columns>
                                            <EditRowStyle BackColor="#999999" />
                                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                        </asp:GridView>
                                    </div><br />
                                    <font color="red"> <b><asp:Label ID="lblStatus" runat="server" Text=""></asp:Label></b></font> 
                                </div>
                            </div>
                        </div>

                    </div>

                </div>

            </div>

        </div>

    </div>

    <asp:TextBox ID="txtnull" runat="server" Visible="false"></asp:TextBox>

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
