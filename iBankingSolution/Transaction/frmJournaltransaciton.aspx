<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/ProjectBM.Master" AutoEventWireup="true" CodeBehind="frmJournaltransaciton.aspx.cs" Inherits="iBankingSolution.Transaction.frmJournaltransaciton" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        $ = jQuery.noConflict();



        $(document).ready(function () {

            $('[id*="MainContent_rgv_JBEntryForm_cmbx_Ledger_"]').select2({

                placeholder: "Select Item",
                matcher: function (params, data) {
                    return matchStart(params, data);
                },
                allowClear: true

            });

           

        });

        function matchStart(params, data) {
            params.term = params.term || '';
            if (data.text.toUpperCase().indexOf(params.term.toUpperCase()) == 0) {
                return data;
            }
            return false;
        }

        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;
                return true;
        }
    </script>
     <script type="text/javascript">
        function ShowMessage(message, messagetype) {
            var cssclass;
            debugger;
            switch (messagetype) {
                case 'Success':
                    cssclass = 'alert-success'
                    break;
                case 'Error':
                    cssclass = 'alert-danger'
                    break;
                case 'Warning':
                    cssclass = 'alert-warning'
                    break;
                default:
                    cssclass = 'alert-info'
            }
            $('#alert_container').append('<div id="alert_div" style="margin: 0 -37.5%;width: 99%;-webkit-box-shadow: 3px 4px 6px #999;text-align: center;" class="alert fade in ' + cssclass + '"><a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a><strong>' + messagetype + '!</strong> <span>' + message + '</span></div>').fadeOut(20000);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <asp:ScriptManager ID="scrpt1" runat="server" EnablePageMethods="true"></asp:ScriptManager>
    <div class="row">
        <div class="col-lg-12">
            <div style="float: right; margin-top: 12px;">
                <asp:Button ID="btnsubmit1" runat="server" Text="Save" class="btn btn-primary" OnClick="btnsubmit_Click" />

                <asp:Button ID="btnCancel" runat="server" Text="Cancel" class="btn btn-primary" OnClick="btnCancel_Click" />
                <%--<a href="#" class="btn btn-default">Back to List</a>--%>
            </div>
            <h1 class="page-header">Journal Transaction</h1>
        </div>
        <!-- /.col-lg-12 -->
    </div>
<%--<asp:UpdatePanel runat="server" ID="ser1">
 <ContentTemplate>--%>

    <div class="row">
        <div class="col-lg-12">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    Journal Transaction
                </div>
                <div class="panel-heading" runat="server" id="DivID" visible="false">
                    <asp:Label ID="Label1" runat="server" Visible="False" align="center" Font-Bold="True" Font-Italic="False" Font-Names="Verdana" ForeColor="White"></asp:Label>
                    <asp:Label ID="lblSession" runat="server" BackColor="White" ForeColor="White"></asp:Label>
                </div>
                <div class="panel-body">
                    <div class="row">

                        <div class="form-group">


                            <div class="col-md-4">
                                <label style="margin-right: 20PX;">Date of Entry</label>
                                <asp:TextBox ID="dtpkr_EntryDate" runat="server" onFocus="this.select()" placeholder="dd/MM/yyyy" Font-Size="10" CssClass="form-control input-sm BootDatepicker" autocomplete="off" AutoPostBack="True"></asp:TextBox>


                            </div>

                            <div class="col-md-8">
                                <label style="margin-right: 20PX;">Naration</label>
                                <asp:TextBox ID="txt_Narration" runat="server" placeholder="ENTER NARATION" Font-Size="10" onFocus="this.select()" CssClass="form-control" autocomplete="off"></asp:TextBox>

                                <br />
                            </div>
                            <div class="clearfix"></div>
                            <div class="col-md-4"></div>
                            <div class="col-md-3">
                                <label style="margin-right: 20px;">Name</label>
                                <asp:TextBox ID="txtName" runat="server" placeholder="Account Holder Name" Font-Size="10"  onFocus="this.select()" CssClass="form-control" autocomplete="off" Enabled="false"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <label style="margin-right: 20px;">Gurdian Name</label>
                                <asp:TextBox ID="txtGurdianName" runat="server" placeholder="Account Holder Gurdian Name" Font-Size="10"  onFocus="this.select()" CssClass="form-control" autocomplete="off" Enabled="false"></asp:TextBox>
                            </div>
                            <div class="clearfix"></div><br />
                            <div class="col-md-8">
                                <asp:GridView ID="gv_accholder" runat="server" AutoGenerateColumns="False" CellPadding="3" Height="128px" Width="334px" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" EmptyDataText="No Records Found!">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Cust_Id">
                                            <ItemTemplate>
                                                <asp:Label ID="iblcustid" runat="server" Text='<%# Bind("cust_id") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Name">
                                            <ItemTemplate>
                                                <asp:Label ID="iblname" runat="server" Text='<%# Bind("name") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Balance">
                                            <ItemTemplate>
                                                <asp:Label ID="lblbalance" runat="server" Text='<%# Bind("balanceoo") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle BackColor="White" ForeColor="#000066" />
                                    <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" Height="30px" />
                                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                    <RowStyle ForeColor="#000066" />
                                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                    <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                    <SortedDescendingHeaderStyle BackColor="#00547E" />
                                </asp:GridView>
                                <br />
                            </div>

                            <div class="col-md-12">

                                <asp:GridView ID="rgv_JBEntryForm" runat="server" AutoGenerateColumns="False"
                                    CssClass="table table-bordered table-striped table-hover" EmptyDataText="No Data Found" GridLines="Both" OnRowDataBound="rgv_JBEntryForm_RowDataBound" ShowFooter="True" OnSelectedIndexChanged="rgv_JBEntryForm_SelectedIndexChanged">
                                    <Columns>
                                        <asp:TemplateField HeaderText="SL" HeaderStyle-HorizontalAlign="Left">

                                            <ItemTemplate>
                                                <asp:Label ID="lbl_SlNo" Text='<%# Bind("SlNo") %>' runat="server"></asp:Label>
                                            </ItemTemplate>

                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Ledger">
                                            <ItemTemplate>

                                                <asp:DropDownList ID="cmbx_Ledger" Width="280" CssClass="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cmbx_LedgerSelectedIndexChanged"></asp:DropDownList>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Old A/C No">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txt_OldAcctNo" CssClass="form-control input-sm" runat="server" placeholder="ENTER OLD AC NO" Font-Size="10" Enabled="false"
                                                    autocomplete="off" ForeColor="Black" Width="80" Text='<%# Eval("OldAcctNo") %>' onFocus="this.select()" AutoPostBack="True" OnTextChanged="txt_OldAcctNo_TextChanged"></asp:TextBox>

                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label Text="Total Dr./Cr. :" runat="server" Width="100%" Font-Bold="true" Style="font-size: 12px !important; font-family: Arial,Helvetica,sans-serif !important;"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                      
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Account No">
                                                <ItemStyle HorizontalAlign="Left" />
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtslcode" CssClass="form-control input-sm" Enabled="false" runat="server" Width="100" placeholder="ENTER SL_CODE" ForeColor="Black" Font-Size="10" 
                                                        AutoPostBack="True" onkeypress="return isNumberKey(event)" OnTextChanged="txtslcode_TextChanged" Text='<%# Eval("sl_code") %>'></asp:TextBox>
                                                    <asp:DropDownList ID="cmbx_subledger" Width="100" CssClass="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cmbx_SubLedgerSelectedIndexChanged"
                                                        visible="false"></asp:DropDownList>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Available Balance">
                                                <ItemStyle HorizontalAlign="Left" />
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtBalance" CssClass="form-control input-sm" runat="server" Width="100" Enabled="false" placeholder="AVAILABLE BALANCE"  ForeColor="Black" Font-Size="10" 
                                                        AutoPostBack="True" onkeypress="return isNumberKey(event)" Text='<%# Eval("Balance") %>'></asp:TextBox>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                                         
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Dr. Amount">
                                            <ItemTemplate>
                                                <asp:TextBox ID="ntxt_DrAmount" CssClass="form-control input-sm" runat="server"  
                                                    autocomplete="off" ForeColor="Black" Text='<%# Eval("DrAmount") %>' onFocus="this.select()" onkeypress="return isNumberKey(event)"  AutoPostBack="True" OnTextChanged="ntxt_DrAmount_TextChanged"></asp:TextBox>

                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="ntxt_TotalDrAmount" runat="server" Width="100%" Enabled="false">                                          
                                                </asp:TextBox>

                                                <asp:HiddenField ID="hiddenDrAmt" runat="server" />

                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Cr. Amount">
                                            <ItemTemplate>
                                                <asp:TextBox ID="ntxt_CrAmount" CssClass="form-control input-sm" runat="server"
                                                    autocomplete="off" ForeColor="Black" Text='<%# Eval("CrAmount") %>' onFocus="this.select()" onkeypress="return isNumberKey(event)"  AutoPostBack="True" OnTextChanged="ntxt_CrAmount_TextChanged"></asp:TextBox>

                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="ntxt_TotalCrAmount" runat="server" Width="100%" Enabled="false">                                            
                                                </asp:TextBox>

                                                <asp:HiddenField ID="hiddenCrAmt" runat="server" />

                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Naration">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txt_Narration" CssClass="form-control input-sm" runat="server"
                                                    autocomplete="off" ForeColor="Black" Text='<%# Eval("Narration") %>' onFocus="this.select()" Width="200"></asp:TextBox>

                                            </ItemTemplate>

                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="50px" HeaderText="ACTION">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="itbnNew" runat="server" CausesValidation="false" Height="18"
                                                    ImageUrl="~/Content/images/add.png" Width="18" OnClick="itNew_Click" />
                                                <%-- <asp:ImageButton ID="itbndelete" runat="server" CausesValidation="false"
                                                                CommandName="Delete" Height="18" ImageUrl="~/Content/images/delete.png" Width="18" OnClick="itbndelete_Click" />--%>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" Width="50px" />
                                        </asp:TemplateField>

                                    </Columns>
                                    <PagerStyle BorderColor="#3333CC" />
                                </asp:GridView>

                            </div>


                        </div>



                    </div>

                </div>
            </div>
        </div>
    </div>
<%--</ContentTemplate>
</asp:UpdatePanel>--%>

    <div class="row">
        <div class="col-lg-12">
            <div style="float: right; margin-top: 12px;">
                <asp:Button ID="btnsubmit" runat="server" Text="Save" class="btn btn-primary" OnClick="btnsubmit_Click" />
                <a href="frmJournaltransaciton.aspx" class="btn btn-outline btn-danger">Cancel</a>

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
