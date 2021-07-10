<%@ Language="C#" MasterPageFile="~/MasterPages/ProjectBM.Master" AutoEventWireup="true" CodeBehind="frmMISINTDetails.aspx.cs" Inherits="iBankingSolution.Transaction.frmMISINTDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.4.1/js/bootstrap-datepicker.min.js"></script>
         <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.4.1/css/bootstrap-datepicker3.css"/>
    <script type="text/javascript">
        $ = jQuery.noConflict();
        $(function () {
            $('.select2').select2()
        })
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode != 46 && charCode > 31
            && (charCode < 48 || charCode > 57))
                return false;
            return true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="scrpt1" runat="server"></asp:ScriptManager>
        <div class="row">
            <div class="col-lg-12">
                <div style="float: right; margin-top: 12px;">
                   
                </div>
                <h1 class="page-header">MIS Interest Details</h1>
            </div>
        </div>
           <div class="row">
            <div class="col-lg-12">
                <div class="panel panel-primary">
                    <div class="panel-heading" style="font-size:larger;">
                        
                    </div>
                    <div class="panel-body">
                            <div class="form-group">
                                 <div class="col-xs-2" padding-right: 0;">

                                    <label style="margin-right: 20PX;font-size:larger;">Show Date:</label>

                                    <asp:TextBox ID="dtpkr_EntryDate" CssClass="form-control input-sm BootDatepicker" runat="server" onFocus="this.select()" autocomplete="off" required="required"></asp:TextBox>
                                </div>
                                <div class="col-xs-2" padding-right: 0;">

                                    <label style="margin-right: 20PX;font-size:larger;">Transfer Date:</label>

                                    <asp:TextBox ID="dtpkr_TransferDt" CssClass="form-control input-sm BootDatepicker" runat="server" onFocus="this.select()" autocomplete="off"></asp:TextBox>
                                </div><br />
                                <%--<asp:TextBox ID="dtpkr_EntryDate" CssClass="form-control input-sm BootDatepicker" runat="server" Visible="FALSE"></asp:TextBox><br />--%>
                                    <asp:Button ID="btnshow" runat="server" Text="Show" class="btn btn-primary"  OnClick="button_Show"/>  <%----%>
                                    <asp:Button ID="btntransfer" runat="server" Text="Transfer" class="btn btn-success" OnClick="btnTransfer_Click" /> <%-- --%>
                                </div>
                                <div style ="height:350px; width:1240px; overflow:auto;">
                            
                                <asp:GridView ID="GVMTransaction" runat="server" AutoGenerateColumns="False" CellPadding="3" Height="188px" Width="1254px" ForeColor="#333333" CssClass="table table-bordered table-striped table-hover"
                                            EmptyDataText="No Data Found" GridLines="Both" 
                                             ShowFooter="True" OnRowDataBound="GVMTransaction_RowDataBound">
                                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                            <Columns>
                                                
                     
                                                <asp:TemplateField HeaderText="ACCOUNT NUMBER" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <%--<asp:Label ID="txt_accountno" runat="server" Text='<%# Bind("[SL_CODE]") %>'></asp:Label>--%>
                                                        <asp:TextBox ID="txt_accountno" runat="server" Text='<%# Bind("[SL_CODE]") %>' CssClass="form-control input-sm" ENABLED="FALSE"></asp:TextBox>
                                                    </ItemTemplate>
                                                     <ItemStyle HorizontalAlign="Left" />
                                                     <HeaderStyle HorizontalAlign="Left" />

                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="NAME" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txt_name" runat="server" Text='<%# Bind("[NAME]") %>' CssClass="form-control input-sm" ENABLED="FALSE"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" />
                                                     <HeaderStyle HorizontalAlign="Left" />
                                                   
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="INTEREST AMOUNT" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txt_intamt" runat="server" CssClass="form-control input-sm" Text='<%# Bind("[INT_AMT]") %>' Enabled="FALSE"></asp:TextBox>
                                                    </ItemTemplate>
                                                     <ItemStyle HorizontalAlign="Left" />
                                                     <HeaderStyle HorizontalAlign="Left" />
                                                   
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="INTEREST GOES TO" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txt_intgoesto" runat="server" CssClass="form-control input-sm" Text='<%# Bind("[INT_GOES_TO]") %>' Enabled="FALSE"></asp:TextBox>
                                                    </ItemTemplate>
                                                     <ItemStyle HorizontalAlign="Left" />
                                                     <HeaderStyle HorizontalAlign="Left" />
                                                   
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="ACTION" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkSelect" runat="server" CssClass="form-control"></asp:CheckBox>
                                                    </ItemTemplate>
                                                     <ItemStyle HorizontalAlign="Left" />
                                                     <HeaderStyle HorizontalAlign="Left" />
                                                   
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
