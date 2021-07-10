<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/ProjectBM.Master" CodeBehind="frmStock_Update.aspx.cs" Inherits="iBankingSolution.Master.frmStock_Update" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
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
                    <asp:Button ID="btnsubmit1" runat="server" Text="UPDATE" class="btn btn-primary" OnClick="btnsubmit1_Click"/>
                  
                  
                    <a href="frmDepositMasterList.aspx" class="btn btn-default">Back to List</a>
                </div>
                <h1 class="page-header">Stock Update</h1>
            </div>
            <!-- /.col-lg-12 -->
        </div>
        <div class="row">
            <div class="col-lg-12">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                       Stock Details
                    </div>
                    <div class="panel-body">
                        <div class="row">

                            <div class="form-group">

                                <div class="col-md-6">
                                    <label style="margin-right: 20PX;">Select Item Class</label>
                                  <%--  <asp:TextBox ID="txt_itemclass" runat="server" placeholder="SELECT ITEM NUMBER" CssClass="form-control" />--%>
                                    <asp:DropDownList ID="cmbx_itemclass" runat="server" CssClass="form-control" Font-Size="10">
                                        <asp:ListItem Value="0">--Select Type--</asp:ListItem>
                                        <asp:ListItem Value="Wholesale Fertilizer">Wholesale Fertilizer</asp:ListItem>
                                        <asp:ListItem Value="Retail Fertilizer">Retail Fertilizer</asp:ListItem>
                                        <asp:ListItem Value="Wholesale Pesticides">Wholesale Pesticides</asp:ListItem>
                                        <asp:ListItem Value="Retail Pesticides">Retail Pesticides</asp:ListItem>
                                        <asp:ListItem Value="Wholesale Seeds">Wholesale Seeds</asp:ListItem>
                                        <asp:ListItem Value="PDS">PDS</asp:ListItem>
                                        <asp:ListItem Value="Non-PDS">Non-PDS</asp:ListItem>
                                        <asp:ListItem Value="Wholesale Cloth">Wholesale Cloth</asp:ListItem>
                                        <asp:ListItem Value="Retail Cloth">Retail Cloth</asp:ListItem>
                                        <asp:ListItem Value="Wholesale Finish Goods">Wholesale Finish Goods</asp:ListItem>
                                        <asp:ListItem Value="Retail Finish Goods">Retail Finish Goods</asp:ListItem>
                                        <asp:ListItem Value="Others">Others</asp:ListItem>

                                    </asp:DropDownList>
                                     
                                </div>
                                <div class="col-md-6">
                                    <label style="margin-right: 20PX;">AsOnDate</label>
                                    <asp:TextBox ID="txtasondt" runat="server" placeholder="dd/MM/yyyy" CssClass="form-control input-sm BootDatepicker" AutoPostBack="True" OnTextChanged="txtasondt_TextChanged"/>
                                    <br />
                                </div>
                                    
                                <center> 
                                <div class="col-md-12">
                                <asp:GridView ID="gv_stock" runat="server" AutoGenerateColumns="False" OnRowDataBound="gv_stock_RowDataBound" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" EmptyDataText="No Data Found" ForeColor="Black" GridLines="Vertical">
                                    <AlternatingRowStyle BackColor="#CCCCCC" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Item Code">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_code" runat="server" Text='<%# Bind("CODE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Item Name">
                                            <ItemTemplate>
                                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("NAME") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Stock Qty">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txt_qty" runat="server" Text='<%# Bind("TOT_QTY") %>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Rate">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txt_rate" runat="server" Text='<%# Bind("Stock_Rate") %>' OnTextChanged="txt_rate_TextChanged" AutoPostBack="True"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Value">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_value" runat="server" Text='<%# Bind("Stock_Value") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle BackColor="#CCCCCC" />
                                    <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" Height="40px" />
                                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                    <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                    <SortedAscendingHeaderStyle BackColor="#808080" />
                                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                    <SortedDescendingHeaderStyle BackColor="#383838" />
                                </asp:GridView>
                                    </div>
                               </center>
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
                    <asp:Button ID="btnsubmit" runat="server" Text="Save" class="btn btn-primary"  />
               
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


 
</asp:Content>

