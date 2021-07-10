<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/ProjectBM.Master" CodeBehind="frmMasterLedger.aspx.cs" Inherits="iBankingSolution.Master.frmMasterLedger" %>

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
                    <a href="frmLedgerList.aspx" class="btn btn-default">Back to List</a>
                </div>
                <h1 class="page-header">Ledger Master</h1>
            </div>
            <!-- /.col-lg-12 -->
        </div>
        <div class="row">
            <div class="col-lg-12">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        Ledger Master Entry
                    </div>
                    <div class="panel-body">
                        <div class="row">

                            <div class="form-group">

                                <div class="col-md-12">
                                    
                                    <asp:Label ID="Label2" runat="server" Text="*" style="color: #FF3300"></asp:Label>
                                    <label style="margin-right: 20PX;Font-Size :larger;">Ledger Name</label>
                                    <asp:Label ID="lblDid" runat="server" BackColor="White" ForeColor="White"></asp:Label>
                                    <asp:TextBox ID="txt_LedgerName" runat="server" required="required" CssClass="form-control" Style="text-transform: uppercase;" />
                                </div>
                                <div class="col-md-2">
                                     <br />
                                    <asp:CheckBox ID="chkbx_IsSubledger" runat="server"  text="Sub Ledger" Font-Size="larger"/>
                                </div>
                                <div class="col-md-6">
                                    <br />
                                    <asp:RadioButtonList ID="rdobtn_LedgerType" Style="border: 1px solid whitesmoke;Font-Size :larger;" runat="server" CellPadding="4" CellSpacing="4" RepeatDirection="Horizontal" Width="100%">


                                        <asp:ListItem Text="Cash" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Bank" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="Service Branch" Value="3"></asp:ListItem>

                                        <asp:ListItem Text="None" Value="0" Selected="True"></asp:ListItem>

                                    </asp:RadioButtonList>
                                </div>

                                <div class="col-md-4" style="font-weight: bold;Font-Size :larger;">
                                  <br />
                                      Appropriation<asp:DropDownList ID="cmbx_approporation" runat="server" style="font-weight: bold;">

                                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                                        <asp:ListItem Selected="True">No</asp:ListItem>
                                        <asp:ListItem>Yes</asp:ListItem>
                                    </asp:DropDownList>
                                </div>

                                <div class="clearfix"></div>
                                <hr />
                            </div>
                            <div class="form-group">
                                <div class="col-md-4">

                                    
                                    <asp:Label ID="Label1" runat="server" Text="*" style="color: #FF3300"></asp:Label>
                                    <label style="margin-right: 20PX;Font-Size :larger;">Group</label>
                                    <asp:DropDownList ID="cmbx_Group" runat="server" required="required" CssClass="form-control"></asp:DropDownList>
                                </div>
                                <div class="col-md-4">
                                    <label style="margin-right: 20PX;Font-Size :larger;">Type</label>
                                    <asp:DropDownList ID="cmbx_Type" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="0">--SELECT TYPE--</asp:ListItem>
                                        <asp:ListItem>Deposit</asp:ListItem>
                                        <asp:ListItem>General</asp:ListItem>
                                        <asp:ListItem>Loan</asp:ListItem>
                                        <asp:ListItem>Share</asp:ListItem>
                                        <asp:ListItem>Nominal Share</asp:ListItem>
                                        <asp:ListItem>Investment</asp:ListItem>
                                        <asp:ListItem>Borrowing</asp:ListItem>
                                        <asp:ListItem>Meterial</asp:ListItem>
                                        <asp:ListItem>Payble</asp:ListItem>
                                        <asp:ListItem>Receivable</asp:ListItem>
                                        <asp:ListItem>Paid</asp:ListItem>
                                        <asp:ListItem>Receive</asp:ListItem>
                                        <asp:ListItem>Forms</asp:ListItem>
                                        <asp:ListItem>Supplier</asp:ListItem>
                                        <asp:ListItem>Insurence</asp:ListItem>
                                        <asp:ListItem>Purchase</asp:ListItem>
                                        <asp:ListItem>Sales</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-4">
                                    <asp:Label ID="Labe31" runat="server" Text="*" style="color: #FF3300"></asp:Label>
                                    <label style="margin-right: 20PX;Font-Size :larger;">Ledger Code</label>
                                    <asp:TextBox ID="txt_LedgerCode" placeholder="ENTER LEDGER CODE" Font-Size="10" runat="server" required="required" CssClass="form-control" AutoPostBack="True" OnTextChanged="txt_LedgerCode_TextChanged"/>
                                </div>


                                <div class="clearfix"></div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-6">
                                    <label style="margin-right: 20PX;Font-Size :larger;">Interest Issued/Received Form</label>
                                    <asp:DropDownList ID="cmbx_InterestIssued" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                                <div class="col-md-6">
                                    <label style="margin-right: 20PX;Font-Size :larger;">Interest Payble/Receivable Form</label>

                                    <asp:DropDownList ID="cmbx_InterestPayble" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>

                                <div class="col-md-12">
                                 <%---   <label style="margin-right: 20PX;">Add With</label>
                                    <asp:TextBox ID="txt_AddWith" runat="server" CssClass="form-control" /> ---%>

                                </div>
                                <div class="col-md-3">
                                    <br />
                                    <label style="margin-right: 10PX;Font-Size :larger;">Debit Opening Balance</label>
                                    <asp:TextBox ID="ntxt_Debit" runat="server" CssClass="form-control" placeholder="XX-XXX" onkeypress="return isNumberKey(event)"/>
                                </div>
                               
                                
                                <div class="col-md-3">
                                    <br />
                                    <label style="margin-right: 10PX;Font-Size :larger;">Credit Opening Balance</label>
                                    <asp:TextBox ID="ntxt_Credit" runat="server" CssClass="form-control" placeholder="XX-XXX" onkeypress="return isNumberKey(event)" />

                                </div>


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
                    <asp:Button ID="btnsubmit" runat="server" Text="Save" class="btn btn-primary" OnClick="btnsubmit_Click" />
                    <a href="frmMasterLedger.aspx" class="btn btn-outline btn-danger">Cancel</a>

                </div>
            </div>
            <!-- /.col-lg-12 -->
        </div>

        <%-- Scripting Section --%>
  
</asp:Content>
