<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/ProjectBM.Master" CodeBehind="FrmItemMaster.aspx.cs" Inherits="iBankingSolution.Master.FrmItemMaster" %>

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
                    <a href="frmItemMasterList.aspx" class="btn btn-default">Back to List</a>
                </div>
                <h1 class="page-header">Item Master</h1>
            </div>
            <!-- /.col-lg-12 -->
        </div>
        <div class="row">
            <div class="col-lg-12">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        Entry
                    </div>
                    <div class="panel-body">
                        <div class="row">

                            <div class="form-group">

                                <div class="col-md-12">
                                    <label style="margin-right: 20PX;">Item Name</label>
                                    <asp:TextBox ID="txt_ItemName" runat="server" placeholder="Enter Item Name" CssClass="form-control" />
                                    <asp:Label ID="lblDid" runat="server" BackColor="White" ForeColor="White"></asp:Label>

                                </div>
                                <div class="col-md-12">
                                    <label style="margin-right: 20PX;">Purchase Ledger</label>
                                    <asp:DropDownList ID="cmbx_PurchaseLedger" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                                <div class="col-md-12">
                                    <label style="margin-right: 20PX;">Sales Ledger</label>
                                    <asp:DropDownList ID="cmbx_SaleLedger" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                                <div class="col-md-4">
                                    <label style="margin-right: 20PX;">Opening Stock</label>
                                    <asp:TextBox ID="ntxt_OpeningSTock" runat="server" CssClass="form-control" />
                                </div>
                                <div class="col-md-4">
                                    <label style="margin-right: 20PX;">Item Sales Value</label>
                                    <asp:TextBox ID="ntxt_ItemSaleValue" runat="server" CssClass="form-control" />
                                </div>
                                <div class="col-md-4">
                                    <label style="margin-right: 20PX;">Value of Stock</label>
                                    <asp:TextBox ID="ntxt_StockValue" runat="server" CssClass="form-control" />
                                </div>


                                <div class="col-md-4">
                                    <label style="margin-right: 20PX;">Measuring Unit</label>

                                    <asp:DropDownList ID="cmbx_UoM" runat="server" CssClass="form-control">

                                        <asp:ListItem Value="0">--Select Type--</asp:ListItem>
                                        <asp:ListItem Value="Kg">Kilogram</asp:ListItem>
                                        <asp:ListItem Value="g">Gram</asp:ListItem>
                                        <asp:ListItem Value="Ltr">Litre</asp:ListItem>
                                        <asp:ListItem Value="MT">MT</asp:ListItem>
                                        <asp:ListItem Value="Bag">Bag</asp:ListItem>
                                        <asp:ListItem Value="Pcs">Pcs</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-4">
                                    <label style="margin-right: 20PX;">Item Class</label>

                                    <asp:DropDownList ID="cmbx_ItemClass" runat="server" CssClass="form-control">
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
                                 <div class="col-md-4">
                                    <label style="margin-right: 20PX;">HSN No</label>
                                    <asp:TextBox ID="txt_HSNNO" runat="server" CssClass="form-control" />
                                </div>

                                <div class="clearfix"></div>
                                  <div class="col-md-6">
                                    <label style="margin-right: 20PX;" >CGST %</label>
                                    <asp:TextBox ID="ntxt_CGST" runat="server" CssClass="form-control" placeholder="Enter CGST Value" />
                                </div>
                                <div class="col-md-6">
                                    <label style="margin-right: 20PX;"  >SGST %</label>
                                    <asp:TextBox ID="ntxt_SGST" runat="server" CssClass="form-control" placeholder="Enter SGST Value"/>
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



