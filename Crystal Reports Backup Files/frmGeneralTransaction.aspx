<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/ProjectBM.Master" CodeBehind="frmGeneralTransaction.aspx.cs" Inherits="iBankingSolution.Transaction.frmGeneralTransaction" %>


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
                <h1 class="page-header">General Ledger Transaction</h1>
            </div>
            <!-- /.col-lg-12 -->
        </div>
        <div class="row">
            <div class="col-lg-12">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        General Ledger Transaction
                    </div>
                    <div class="panel-heading" runat="server" id="DivID" visible="false">
                        <asp:Label ID="Label1" runat="server" Visible="False" align="center" Font-Bold="True" Font-Italic="False" Font-Names="Verdana" ForeColor="White"></asp:Label>
                    </div>
                    <div class="panel-body">
                        <div class="row">

                            <div class="form-group">

                                <div class="col-md-8">
                                    <label style="margin-right: 20PX;">Entry type</label><asp:Label ID="lblDid" runat="server" BackColor="White" ForeColor="White"></asp:Label>
                                    <asp:DropDownList ID="cmbx_EntryType" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cmbx_EntryType_SelectedIndexChanged" EmptyMessage="Select Entry Type">
                                        <Items>
                                            <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                            <asp:ListItem Text="Receipt" Value="r"></asp:ListItem>
                                            <asp:ListItem Text="Payment" Value="p"></asp:ListItem>
                                        </Items>
                                    </asp:DropDownList>
                               </div>
                                <div class="col-md-4">
                                 <label style="margin-right: 20PX;">Date of Entry</label>
                                    <asp:TextBox ID="dtpkr_EntryDate" runat="server" onFocus="this.select()" CssClass="form-control input-sm BootDatepicker" autocomplete="off" AutoPostBack="True"></asp:TextBox>
                                    
                                   
                                </div>
                                
                                <div class="Clearfix"></div>

                                <div class="col-md-4">
                                    <label style="margin-right: 20PX;">Instrument Type</label>
                                    <asp:DropDownList ID="cmbx_InstrumentType" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                                <div class="col-md-4">
                                    <label style="margin-right: 20PX;">InstrumentNo</label>
                                    <asp:TextBox ID="txt_InstrumentNo" CssClass="form-control" runat="server" autocomplete="off"></asp:TextBox>
                                </div>
                                
                                <div class="col-md-4">
                                    <label style="margin-right: 20PX;">Date of Instrument</label>
                                    <asp:TextBox ID="dtpkr_DateOfInstrument" CssClass="form-control  input-sm BootDatepicker" runat="server" onFocus="this.select()" autocomplete="off"></asp:TextBox>
                                </div>
                               <div class="Clearfix"></div>
                            
                                <div class="col-md-4">
                                
                                    <asp:Label ID="lblCredited" runat="server" Text="Amount Credited"></asp:Label>
                                    <asp:TextBox ID="ntxt_AmountCredited" CssClass="form-control" runat="server" onFocus="this.select()" autocomplete="off" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                </div>
                               
                                <div class="col-md-8">
                                    <label style="margin-right: 20PX;">Pay to Ledger</label>
                                    <asp:DropDownList ID="cmbx_PayToLedger" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cmbx_PayToLedger_SelectedIndexChanged" EmptyMessage="Select Ledger">
 
                                    </asp:DropDownList>
                                    
                                </div>

                                   <div class="col-md-6">
                                    <label style="margin-right: 20PX;">Sub Ledger</label>
                                    <asp:DropDownList ID="cmbx_SubLedger" CssClass="form-control" runat="server" AutoPostBack="true" EmptyMessage="Select Sub Ledger">
 
                                    </asp:DropDownList>
                                    
                                </div>

                                <div class="col-md-2">
                                
                                    <asp:Label ID="lblDebited" runat="server" Text="Amount Debited"></asp:Label>
                                    <asp:TextBox ID="ntxt_AmountDebited" runat="server" onFocus="this.select()" CssClass="form-control" autocomplete="off" AutoPostBack="True"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-8">
                                <label style="margin-right: 20PX;">Comments</label>
                                <asp:TextBox ID="txt_Comments" runat="server" onFocus="this.select()" CssClass="form-control" autocomplete="off"></asp:TextBox>
                             
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
