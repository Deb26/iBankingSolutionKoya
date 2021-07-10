<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/ProjectBM.Master" CodeBehind="FrmItemMaster.aspx.cs" Inherits="iBankingSolution.Master.FrmItemMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
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
                <div class="panel panel-primary">
                    <div class="panel-heading" style="font-size:larger;">
                        Entry
                    </div>
                    <div class="panel-body">
                        <div class="row">

                            <div class="form-group">

                                <div class="col-md-12">
                                    
                                    <asp:Label ID="Label2" runat="server" Text="*" style="color: #FF3300"></asp:Label>
                                    <label style="margin-right: 20PX;font-size:larger;">Item Name</label>
                                    <asp:TextBox ID="txt_ItemName" runat="server" placeholder="Enter Item Name" Font-Size="10" CssClass="form-control" required="required"/>
                                    <asp:Label ID="lblDid" runat="server" BackColor="White" ForeColor="White"></asp:Label>

                                </div>
                                <div class="clearfix"></div><br />
                                <div class="col-md-12">
                                    
                                <asp:Label ID="Label1" runat="server" Text="*" style="color: #FF3300"></asp:Label>
                                    <label style="margin-right: 20PX;font-size:larger;">Purchase Ledger</label>
                                    <asp:DropDownList ID="cmbx_PurchaseLedger" runat="server" CssClass="form-control" placeholder="ENTER PURCHASE LEDGER" Font-Size="10"></asp:DropDownList>
                                </div>
                                <div class="clearfix"></div><br />
                                <div class="col-md-12">
                                    <label style="margin-right: 20PX;font-size:larger;">Sales Ledger</label>
                                    <asp:DropDownList ID="cmbx_SaleLedger" runat="server" CssClass="form-control" placeholder="ENTER SALES LEDGER" Font-Size="10"></asp:DropDownList>
                                </div>
                                <div class="clearfix"></div><br />
                                <div class="col-md-4">
                                    <label style="margin-right: 20PX;font-size:larger;">Opening Stock</label>
                                    <asp:TextBox ID="ntxt_OpeningSTock" runat="server" CssClass="form-control" placeholder="ENTER OPENING STOCK" Font-Size="10"/>
                                </div>
                                <div class="col-md-4">
                                    <label style="margin-right: 20PX;font-size:larger;">Item Sales Value</label>
                                    <asp:TextBox ID="ntxt_ItemSaleValue" runat="server" CssClass="form-control" placeholder="ENTER ITEM VALUE" Font-Size="10"/>
                                </div>
                                <div class="col-md-4">
                                    <label style="margin-right: 20PX;font-size:larger;">Value of Stock</label>
                                    <asp:TextBox ID="ntxt_StockValue" runat="server" CssClass="form-control" placeholder="ENTER STOCK VALUE" Font-Size="10"/>
                                </div>

                                <div class="clearfix"></div><br />
                                <div class="col-md-4">
                                    <label style="margin-right: 20PX;font-size:larger;">Measuring Unit</label>

                                    <asp:DropDownList ID="cmbx_UoM" runat="server" CssClass="form-control" Font-Size="10">

                                        <asp:ListItem Value="0">-- Select Measuring Unit --</asp:ListItem>
                                        <asp:ListItem Value="KG">KG</asp:ListItem>
                                        <asp:ListItem Value="MT">MT</asp:ListItem>
                                        <asp:ListItem Value="PCS">PCS</asp:ListItem>
                                        <asp:ListItem Value="UNIT">UNIT</asp:ListItem>
                                        <asp:ListItem Value="LITRE">LITRE</asp:ListItem>
                                        <asp:ListItem Value="QUINTAL">QUINTAL</asp:ListItem>
                                        <asp:ListItem Value="GRAM">GRAM</asp:ListItem>
                                        
                                        <%--<asp:ListItem Value="Bag">Bag</asp:ListItem>--%>
                                        
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-4">
                                    <label style="margin-right: 20PX;font-size:larger;">Item Class</label>

                                    <asp:DropDownList ID="cmbx_ItemClass" runat="server" CssClass="form-control" Font-Size="10">
<%--                                    <asp:ListItem Value="0">--Select Type--</asp:ListItem>
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
                                        <asp:ListItem Value="Others">Others</asp:ListItem>--%>

                                    </asp:DropDownList>
                                </div>
                                 <div class="col-md-4">
                                    <label style="margin-right: 20PX;font-size:larger;">HSN No</label>
                                    <asp:TextBox ID="txt_HSNNO" runat="server" CssClass="form-control" placeholder="ENTER HSN NO" Font-Size="10"/>
                                </div>

                                <div class="clearfix"></div><br />
                                  <div class="col-md-6">
                                    <label style="margin-right: 20PX;font-size:larger;" >CGST %</label>
                                    <asp:TextBox ID="ntxt_CGST" onkeypress="return isNumberKey(event)" runat="server" CssClass="form-control" placeholder="Enter CGST Value" Font-Size="10"/>
                                </div>
                                <div class="col-md-6">
                                    <label style="margin-right: 20PX;font-size:larger;"  >SGST %</label>
                                    <asp:TextBox ID="ntxt_SGST" onkeypress="return isNumberKey(event)" runat="server" CssClass="form-control" placeholder="Enter SGST Value" Font-Size="10"/>
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



</asp:Content>



