<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/ProjectBM.Master" AutoEventWireup="true" CodeBehind="frmJournaltransaciton.aspx.cs" Inherits="iBankingSolution.Transaction.frmJournaltransaciton" %>

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
    <form id="formInitialSetting" runat="server">
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
        <div class="row">
            <div class="col-lg-12">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        Journal Transaction
                    </div>
                    <div class="panel-heading" runat="server" id="DivID" visible="false">
                        <asp:Label ID="Label1" runat="server" Visible="False" align="center" Font-Bold="True" Font-Italic="False" Font-Names="Verdana" ForeColor="White"></asp:Label>
                    </div>
                    <div class="panel-body">
                        <div class="row">

                            <div class="form-group">


                                <div class="col-md-4">
                                    <label style="margin-right: 20PX;">Date of Entry</label>
                                    <asp:TextBox ID="dtpkr_EntryDate" runat="server" onFocus="this.select()" CssClass="form-control input-sm BootDatepicker" autocomplete="off" AutoPostBack="True"></asp:TextBox>


                                </div>

                                <div class="col-md-8">
                                    <label style="margin-right: 20PX;">Naration</label>
                                    <asp:TextBox ID="txt_Narration" runat="server" onFocus="this.select()" CssClass="form-control" autocomplete="off"></asp:TextBox>


                                </div>


                                <div class="col-md-12">

                                    <asp:GridView ID="rgv_JBEntryForm" runat="server" AutoGenerateColumns="False"
                                        CssClass="table table-bordered table-striped table-hover" EmptyDataText="No Data Found" GridLines="Both" OnRowDataBound="rgv_JBEntryForm_RowDataBound" ShowFooter="True">
                                        <Columns>
                                            <asp:TemplateField HeaderText="SL" HeaderStyle-HorizontalAlign="Left">
                                                
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_SlNo" Text='<%# Bind("SlNo") %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                               
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Ledger">
                                                <ItemTemplate>

                                                    <asp:DropDownList ID="cmbx_Ledger" CssClass="form-control" runat="server" OnSelectedIndexChanged="cmbx_Ledger_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Old A/C No">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txt_OldAcctNo" CssClass="form-control input-sm" runat="server"
                                                        autocomplete="off" ForeColor="Black" Text='<%# Eval("OldAcctNo") %>' onFocus="this.select()"></asp:TextBox>

                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label Text="Total Dr./Cr. :" runat="server" Width="100%" Font-Bold="true" Style="font-size: 12px !important; font-family: Arial,Helvetica,sans-serif !important;"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Sub Ledger">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="cmbx_SubLedger" runat="server" CssClass="form-control"></asp:DropDownList>

                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Dr. Amount">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="ntxt_DrAmount" CssClass="form-control input-sm" runat="server"
                                                        autocomplete="off" ForeColor="Black" Text='<%# Eval("DrAmount") %>' onFocus="this.select()"></asp:TextBox>

                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="ntxt_TotalDrAmount" runat="server" Width="100%">                                          
                                                    </asp:TextBox>

                                                    <asp:HiddenField ID="hiddenDrAmt" runat="server" />

                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Cr. Amount">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="ntxt_CrAmount" CssClass="form-control input-sm" runat="server"
                                                        autocomplete="off" ForeColor="Black" Text='<%# Eval("CrAmount") %>' onFocus="this.select()" AutoPostBack="True" OnTextChanged="ntxt_CrAmount_TextChanged"></asp:TextBox>

                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="ntxt_TotalCrAmount" runat="server" Width="100%">                                            
                                                    </asp:TextBox>

                                                    <asp:HiddenField ID="hiddenCrAmt" runat="server" />

                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                               <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Naration">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txt_Narration" CssClass="form-control input-sm" runat="server"
                                                        autocomplete="off" ForeColor="Black" Text='<%# Eval("Narration") %>' onFocus="this.select()"></asp:TextBox>

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
