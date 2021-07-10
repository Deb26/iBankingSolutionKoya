<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/ProjectBM.Master" CodeBehind="frmProjectUser.aspx.cs" Inherits="iBankingSolution.Master.frmProjectUser" %>

 
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
                    <a href="frmProjectUserList.aspx" class="btn btn-default">Back to List</a>
                </div>
                <h1 class="page-header">Project User Master</h1>
            </div>
            <!-- /.col-lg-12 -->
        </div>
        <div class="row">
            <div class="col-lg-12">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        Project User
                    </div>
                    <div class="panel-body">
                        <div class="row">

                            <div class="form-group">

                                <div class="col-md-6">
                                    <label style="margin-right: 20PX;">Name</label>
                                 <asp:DropDownList ID="ddlUserName" runat="server" CssClass="form-control"></asp:DropDownList>
                                    <asp:Label ID="lblDid" runat="server" BackColor="White" ForeColor="White"></asp:Label>
                                </div>
                                <div class="col-md-6">
                                    <label style="margin-right: 20PX;">Department</label>
                                  <asp:DropDownList ID="cmbx_Department" runat="server" CssClass="form-control">
                                         <asp:ListItem Value="0">--Select Type--</asp:ListItem>
                                        <asp:ListItem Value="Accounts">Accounts</asp:ListItem>
                                        <asp:ListItem Value="Backoffice">Operations</asp:ListItem>
                                        <asp:ListItem Value="Loan">Loan</asp:ListItem>

                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-6">
                                    <label style="margin-right: 20PX;">Designation</label>
                                     <asp:TextBox ID="txtDesignation" runat="server"   CssClass="form-control" />
                                </div>
                                <div class="col-md-6">
                                    <label style="margin-right: 20PX;">Address</label>
                                    <asp:TextBox ID="txtaddress" runat="server"   CssClass="form-control" />
                                </div>
                                <div class="col-md-6">
                                    <label style="margin-right: 20PX;">EMail</label>
                                     <asp:TextBox ID="txtEmail" runat="server"  CssClass="form-control" />
                                </div>
                                <div class="col-md-6">
                                    <label style="margin-right: 20PX;">Phone</label>
                                  
                                     <asp:TextBox ID="txtPhone" runat="server"   CssClass="form-control" />
                                </div>




                                <div class="clearfix"></div>

                            </div>
                            <div class="form-group">
                                <div class="col-md-4">
                                    <label style="margin-right: 20PX;">User Name</label>
                                    <asp:TextBox ID="txtUserID" runat="server"  placeholder="Enter User ID"  CssClass="form-control" />
                                </div>
                                <div class="col-md-4">
                                    <label style="margin-right: 20PX;">Password</label>
                                    <asp:TextBox ID="txtpassword" runat="server"  CssClass="form-control" TextMode="Password" />
                                </div>
                                <div class="col-md-4">
                                    <label style="margin-right: 20PX;">Confirm Password</label>
                                   <asp:TextBox ID="txtRePassword" runat="server"  placeholder="Re enter Password"  CssClass="form-control" TextMode="Password" />
                                </div>

                                <div class="clearfix"></div>
                            </div>

                           <div class="form-group">
                                <div class="col-md-4">
                                    <label style="margin-right: 20PX;">User Level</label>
                                    <asp:DropDownList ID="ddlUserLevel" runat="server" CssClass="form-control">

                                         <asp:ListItem Value="0">--Select Type--</asp:ListItem>
                                        <asp:ListItem Value="Admin">Admin</asp:ListItem>
                                        <asp:ListItem Value="User">User</asp:ListItem>
                                         

                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-4">
                                    <label style="margin-right: 20PX;">Valid Till</label>
                                     <asp:TextBox ID="txtvalidtill" CssClass="form-control input-sm BootDatepicker" runat="server" onFocus="this.select()" autocomplete="off" required="required"></asp:TextBox>
                                </div>
                           <div class="col-md-4">
                                    <label style="margin-right: 20PX;">Branch</label>
                                     <asp:DropDownList ID="cmbx_Branch" runat="server" CssClass="form-control"> </asp:DropDownList>
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
 
