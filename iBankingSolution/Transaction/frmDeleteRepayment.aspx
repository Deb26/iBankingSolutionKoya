<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/ProjectBM.Master" CodeBehind="frmDeleteRepayment.aspx.cs" Inherits="iBankingSolution.Transaction.frmDeleteRepayment" %>

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
<style type="text/css">
    .auto-style1 {
        color: #990000;
    }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <asp:ScriptManager ID="scrpt1" runat="server" EnablePageMethods="true"></asp:ScriptManager>
    <div class="row">
        <div class="col-lg-12">
            <div style="float: right; margin-top: 12px;">
                <%--<a href="#" class="btn btn-default">Back to List</a>--%>
            </div>
            <h1 class="page-header">Delete Transactions</h1>
        </div>
        <!-- /.col-lg-12 -->
    </div>
    <div class="row">
        <div class="col-lg-12">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    Delete Repayment/Disbursment
                </div>

                <div class="panel-body">
                    <div class="row">

                        <div class="form-group">

                            <div class="col-md-2">
                                <label style="margin-right: 20PX;">Entry type</label><asp:Label ID="lblDid" runat="server" BackColor="White" ForeColor="White"></asp:Label>
                                <asp:DropDownList ID="cmbx_EntryType" CssClass="form-control" runat="server" Font-Size="10" EmptyMessage="Select Entry Type">
                                    <Items>
                                        <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                        <asp:ListItem Text="Repayment" Value="Repayment"></asp:ListItem>
                                        <asp:ListItem Text="Disbursement" Value="Disbursement"></asp:ListItem>
                                    </Items>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-2">
                                <label style="margin-right: 20PX;">Select Date</label>
                                <asp:TextBox ID="dtpkr_EntryDate" runat="server" onFocus="this.select()" placeholder="dd/MM/yyyy" Font-Size="10" CssClass="form-control input-sm BootDatepicker" autocomplete="off" AutoPostBack="True"></asp:TextBox>
                            </div>



                            <div class="col-md-2">
                                <label style="margin-right: 20PX;"></label><br />
                                <asp:Button ID="btnshow" runat="server" Text="Show Details" class="btn btn-success" OnClick="btnshow_Click" />

                            </div>
                            <div class="col-md-2">
                                <label style="margin-right: 20PX;" runat="server" text="Delete?" id="lbldel"></label>
                              <asp:Button ID="btnDelete" runat="server" Text="Delete Transaction?"  class="btn btn-danger" OnClick="btnDelete_Click"  OnClientClick="return confirm('Are you sure you want to delete this Vouchers?');"/>
                                
                            </div>
                            <div class="clearfix"></div>
                            <hr />
                          
                            <center>
                                 <div class="col-md-12">
                                       <br /> <asp:Label runat="server" ID="lblcnt" Text="" style="color: #000099; font-weight: 700"></asp:Label>
                                     <br />
                                       <div class="clearfix"></div>
                                     <asp:GridView ID="GVTransDetails" runat="server" AutoGenerateColumns="False" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2" Height="238px" Width="967px" OnRowDataBound="GVTransDetails_RowDataBound">
                                         <Columns>
                                             
                                                <asp:TemplateField HeaderText="Select">
                                                
                                                 <ItemTemplate>
                                                  <asp:CheckBox ID="chkSelect" runat="server" CssClass="form-control"></asp:CheckBox>
                                                 </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                             </asp:TemplateField>

                                             <asp:TemplateField HeaderText="VoucherNo" ItemStyle-HorizontalAlign="Center">
                                                
                                                 <ItemTemplate>
                                                     <asp:Label ID="lblVoucherNo" runat="server" Text='<%# Eval("VoucherNo") %>'></asp:Label>
                                                 </ItemTemplate>
                                             </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Account No" ItemStyle-HorizontalAlign="Center">
                                                 
                                                 <ItemTemplate>
                                                     <asp:Label ID="lblSLcode" runat="server" Text='<%# Eval("SLCode") %>'></asp:Label>
                                                 </ItemTemplate>
                                             </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Disbursement Amount" ItemStyle-HorizontalAlign="Left">
                                                
                                                 <ItemTemplate>
                                                     <asp:Label ID="lbldisbAmt" runat="server" Text='<%# Eval("DisbAmt") %>'></asp:Label>
                                                 </ItemTemplate>
                                             </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Principal" ItemStyle-HorizontalAlign="Right">
                                              
                                                 <ItemTemplate>
                                                     <asp:Label ID="lblPrincipal" runat="server" Text='<%# Eval("principal") %>'></asp:Label>
                                                 </ItemTemplate>
                                             </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Interest" ItemStyle-HorizontalAlign="Right">
                                                 
                                                 <ItemTemplate>
                                                     <asp:Label ID="lblInterest" runat="server" Text='<%# Eval("Interest") %>'></asp:Label>
                                                 </ItemTemplate>
                                             </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Name" ItemStyle-HorizontalAlign="Center">
                                               
                                                 <ItemTemplate>
                                                     <asp:Label ID="lblName" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                                                 </ItemTemplate>
                                             </asp:TemplateField>
                                         </Columns>
                                         <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
                                         <HeaderStyle BackColor="#175599" ForeColor="White" Font-Names="Verdana" Font-Size="10pt" />
                                         <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
                                         <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
                                         <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
                                         <SortedAscendingCellStyle BackColor="#FFF1D4" />
                                         <SortedAscendingHeaderStyle BackColor="#B95C30" />
                                         <SortedDescendingCellStyle BackColor="#F1E5CE" />
                                         <SortedDescendingHeaderStyle BackColor="#93451F" />
                                     </asp:GridView>
                                </div>

                                <hr />
                               

                            </center>












                        </div>
    </div>
                </div>
            </div>
        </div>


        <div class="row">
            <div class="col-lg-12">
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
