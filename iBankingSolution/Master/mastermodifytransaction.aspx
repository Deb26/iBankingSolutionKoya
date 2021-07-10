<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="mastermodifytransaction.aspx.cs" MasterPageFile="~/MasterPages/ProjectBM.Master" Inherits="iBankingSolution.Master.mastermodifytransaction" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
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
            <h1 class="page-header">Master ModifyTransaction</h1>
        </div>
        <!-- /.col-lg-12 -->
    </div>
    <div class="row">
        <!-- /.col-lg-12 -->
        <div class="row">
            <div class="col-lg-12">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        Modify Transaction Details
                    </div>
                    <div class="panel-heading" runat="server" id="DivID" visible="false">
                        <asp:Label ID="Label1" runat="server" Visible="False" align="center" Font-Bold="True" Font-Italic="False" Font-Names="Verdana" ForeColor="White"></asp:Label>
                    </div>
                    <div class="panel-body">
                        <div class="row">

                            <div class="form-group">

                                <div class="col-xs-3" style="width: 25%; padding-right: 0;">

                                    <label style="margin-right: 20PX;">Voucher Date:</label>

                                    <asp:TextBox ID="dtpkrr_VoucherDate" CssClass="form-control input-sm BootDatepicker" Font-Size="Medium" runat="server" onFocus="this.select()" autocomplete="off" required="required"></asp:TextBox>
                                </div>


                                <div class="col-xs-3" style="width: 20%; padding-right: 0;">
                                    <label style="margin-right: 20PX;">Voucher Type:</label>
                                    <asp:DropDownList ID="cmbxx_VoucherType" runat="server" Font-Size="Medium" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="cmbxx_VoucherType_SelectedIndexChanged">
                                        <asp:ListItem Value="0">----Select-----</asp:ListItem>
                                        <asp:ListItem Value="1">Journal</asp:ListItem>
                                        <asp:ListItem Value="2">Cash</asp:ListItem>
                                        <%-- <asp:ListItem Value="3">Loan</asp:ListItem>--%>
                                    </asp:DropDownList>
                                </div>





                                <div class="col-xs-3" style="width: 35%; padding-right: 0;">
                                    <label style="margin-right: 20PX;">Select Voucher No:</label>
                                    <asp:DropDownList ID="cmbxx_VoucherNo" runat="server" Font-Size="Medium" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="cmbx_VoucherNo_SelectedIndexChanged">
                                        <asp:ListItem>--Loading-----</asp:ListItem>

                                    </asp:DropDownList>
                                </div>

                                <br />

                                <div class="col-xs-3" style="width: 15%;">
                                    <asp:Button ID="txtsavecomm" runat="server" Text="Save Comment" class="btn btn-primary" OnClick="txtsavecomm_Click" />
                                    <br />
                                    <br />
                                </div>
                                
                            </div>
                          
                                <div class="col-md-12">
                                    <asp:TextBox ID="txtcoment" CssClass="form-control" TextMode="MultiLine" runat="server" Font-Size="Medium" Height="50px" Width="1000px" style="margin-left: 0px; margin-bottom: 0px;"></asp:TextBox>
                                     <div class="col-md-03">
                                   <div style="float: right; margin-top: 12px;">
                                       
                                       </div>
                                       </div>
                                    <br />
                                    <br />
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
                                        </div>
                                        <asp:GridView ID="GVMTTransaction" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" ShowFooter="True" Style="height: 104px" OnRowDataBound="GVMTransaction_RowDataBound" OnSelectedIndexChanged="GVMTransaction_SelectedIndexChanged" DataKeyNames="CCBCodeNew">
                                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="CCBCODE" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCCBCode" runat="server" Text='<%# Bind("[CCBCodeNew]") %>'></asp:Label>
                                                    </ItemTemplate>

                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="LDG_CODE">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txt_Ldg_Code" runat="server" Width="100" Enabled="false" Text='<%# Bind("[LDG_CODE]") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                    <%--  <ItemTemplate>
                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("[Leadger Code]") %>'></asp:Label>
                        </ItemTemplate>--%>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="SL_CODE">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txt_Sl_Code" runat="server" Width="100" Enabled="false" Text='<%# Bind("[SL_CODE]") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                    <%-- <ItemTemplate>
                            <asp:Label ID="Label3" runat="server" Text='<%# Bind("[Subledger Code]") %>'></asp:Label>
                        </ItemTemplate>--%>
                                                </asp:TemplateField>



                                                <asp:TemplateField HeaderText="AMT_DEBIT">
                                                    <FooterTemplate>
                                                        <asp:Label ID="lbl_totdebit" runat="server" Width="100" Enabled="false" Text="TOTAL_DEBIT"></asp:Label>
                                                        &nbsp;
                                    <asp:TextBox ID="txt_totdeb" runat="server"></asp:TextBox>
                                                    </FooterTemplate>



                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txt_Amt_Debit" runat="server" Width="100" Enabled="false" Text='<%# Bind("[AMT_DEBIT]") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="AMT_CREDIT">
                                                    <FooterTemplate>
                                                        <asp:Label ID="lbl_totcredit" runat="server" Width="100" Enabled="false" Text="TOTAL_CREDIT"></asp:Label>
                                                        &nbsp;
                            <asp:TextBox ID="txt_totcre" runat="server"></asp:TextBox>
                                                    </FooterTemplate>



                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txt_Amt_Credit" runat="server" Width="100" Enabled="false" Text='<%# Bind("[AMT_CREDIT]") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                    <%-- <ItemTemplate>
                            <asp:Label ID="Label5" runat="server" Text='<%# Bind("[Amount Credit]") %>'></asp:Label>
                        </ItemTemplate>--%>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="VOUCHERDATE">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txt_VoucherCode" runat="server" CssClass="form-control input-sm BootDatepicker" Text='<%# Bind("[VoucherDate]") %>'></asp:TextBox>

                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--  <ItemTemplate>
                            <asp:Label ID="Label6" runat="server" Text='<%# Bind("Date") %>'></asp:Label>
                        </ItemTemplate>--%>
                                                <asp:TemplateField HeaderText="HEADNAME">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txt_HeadName" runat="server"   Enabled="false" Width="200" Text='<%# Bind("[HeadName]") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

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
                                    </div>
                                </div>
                            </div>
                        </div>

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
