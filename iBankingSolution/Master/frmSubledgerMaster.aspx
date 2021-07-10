<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/MasterPages/ProjectBM.Master"CodeBehind ="frmSubledgerMaster.aspx.cs" Inherits="iBankingSolution.Master.frmSubledgerMaster" %>

 <asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        $ = jQuery.noConflict();
        $(function () {
            $('.select2').select2()
        })

        document.onreadystatechange = function () {
            var state = document.readyState
            if (state == 'interactive') {
                document.getElementById('contents').style.visibility = "hidden";
            } else if (state == 'complete') {
                setTimeout(function () {
                    document.getElementById('interactive');
                    document.getElementById('load').style.visibility = "hidden";
                    document.getElementById('contents').style.visibility = "visible";

                }, 2000);
            }
        }

        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode != 46 && charCode > 31
            && (charCode < 48 || charCode > 57))
                return false;
            return true;
        }
    </script>
     <style>
            #load {
            width : 80%;
            height : 80%;
            position : fixed;
            z-index : 9999;
            background : url("circle.gif") no-repeat center /*center rgba(255,255,255,255)*/
           }
     </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <div id="load"></div>
        <asp:ScriptManager ID="scrpt1" runat="server"></asp:ScriptManager>
        <div class="row">
            <div class="col-lg-12">
                <div style="float: right; margin-top: 12px;">
                    <asp:Button ID="btnsubmit1" runat="server" Text="Save" class="btn btn-primary" OnClick="btnsubmit_Click" />
                   
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" class="btn btn-primary" OnClick="btnCancel_Click" />
                    <a href="frmSubledgerList.aspx" class="btn btn-default">Back to List</a>
                </div>
                <h1 class="page-header">Sub Ledger Master</h1>
            </div>
            <!-- /.col-lg-12 -->
        </div>
        <div class="row">
            <div class="col-lg-12">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        Sub Ledger Master Entry
                    </div>
                    <div class="panel-body">
                        <div class="row">
 
                            <div class="form-group">

                                <div class="col-md-12">
                                    
                                    <asp:Label ID="Label2" runat="server" Text="*" style="color: #FF3300"></asp:Label>
                                    <label style="margin-right: 20PX;">Sub Ledger Name</label>
                                    <asp:Label ID="lblDid" runat="server" BackColor="White" ForeColor="White"></asp:Label>
                                    <asp:TextBox ID="txt_SubLedgerName" runat="server" placeholder="ENTER SUBLDGNAME" Font-Size="10" required="required" CssClass="form-control" />
                                </div>
                                <div class="clearfix"></div>
                                <br />
                                <div class="col-md-12">
                                    
                                        <asp:Label ID="Label1" runat="server" Text="*" style="color: #FF3300"></asp:Label>
                                      <label style="margin-right: 20PX;"> Ledger Name</label>
                                    <asp:DropDownList ID="cmbx_Ledger" runat="server" placeholder="ENTER LDG NAME" Font-Size="10" CssClass="form-control"></asp:DropDownList>
                                </div>
                                <div class="clearfix"></div>
                                <br />
                                <div class="col-md-6">
                                      <label style="margin-right: 20PX;">MemsID</label>
                                     <asp:TextBox ID="txt_MemsID" runat="server"  placeholder="ENTER MemsID" Font-Size="10" CssClass="form-control" />
                                </div>
                                 <div class="col-md-6">
                                      <label style="margin-right: 20PX;">GSTIN NO</label>
                                   <asp:TextBox ID="txt_GSTINNo" runat="server" placeholder="ENTER GSTINO" Font-Size="10" CssClass="form-control" />
                                </div>
                                <div class="clearfix"></div>
                                <br />
                                <div class="col-md-12">
                                      <label style="margin-right: 20PX;">Address</label>
                                     <asp:TextBox ID="txt_Address" placeholder="ENTER ADDRESS" Font-Size="10" runat="server" CssClass="form-control" />
                                </div>
                                <%--<div class="col-md-3">
                                      <label style="margin-right: 20PX;">Opening Balance</label>
                                  
                                </div>--%>
                                <div class="clearfix"></div>
                                <br />
                                 <div class="col-md-3">
                                      <label style="margin-right: 20PX;">Opening Debit Balance </label>
                                   <asp:TextBox ID="ntxt_Debit" runat="server" CssClass="form-control" Font-Size="10" placeholder="0.00" onkeypress="return isNumberKey(event)"/>
                                </div>
                                 <div class="col-md-3">
                                      <label style="margin-right: 20PX;">Opening Credit Balance</label>
                                   <asp:TextBox ID="ntxt_Credit" runat="server" CssClass="form-control" Font-Size="10" placeholder="0.00" onkeypress="return isNumberKey(event)"/>
                                </div>
 
                                <hr />
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
                    <a href="frmSubledgerMaster.aspx" class="btn btn-outline btn-danger">Cancel</a>

                </div>
            </div>
            <!-- /.col-lg-12 -->
        </div>

        <%-- Scripting Section --%>

 
 
</asp:Content>