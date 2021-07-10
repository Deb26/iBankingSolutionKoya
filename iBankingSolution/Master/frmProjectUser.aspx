<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/ProjectBM.Master" CodeBehind="frmProjectUser.aspx.cs" Inherits="iBankingSolution.Master.frmProjectUser" %>

 
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
     <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.4.1/js/bootstrap-datepicker.min.js"></script>
         <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.4.1/css/bootstrap-datepicker3.css"/>
    <script type="text/javascript">
        $ = jQuery.noConflict();
        $(function () {
            $('.select2').select2()
        })
    </script>
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
                    <a href="frmProjectUserList.aspx" class="btn btn-default">Back to List</a>
                </div>
                <h1 class="page-header">Project User Master</h1>
            </div>
            <!-- /.col-lg-12 -->
        </div>
        <div class="row">
            <div class="col-lg-12">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        Project User
                    </div>
                    <div class="panel-body">
                        <div class="row">

                            <div class="form-group">

                                <div class="col-md-6">
                                    
                                       <asp:Label ID="Label2" runat="server" Text="*" style="color: #FF3300"></asp:Label>
                                    <label style="margin-right: 20PX;">Name</label>
                                 <asp:DropDownList ID="ddlUserName" runat="server" CssClass="form-control" Font-Size="10" AutoPostBack="true" OnSelectedIndexChanged="cmbx_UserDetails_SelectedIndexChanged"></asp:DropDownList>
                                    <asp:Label ID="lblDid" runat="server" BackColor="White" ForeColor="White"></asp:Label>
                                </div>
                                <div class="col-md-6">
                                    <label style="margin-right: 20PX;">Department</label>
                                        <asp:TextBox ID="txt_Department" runat="server"   CssClass="form-control" placeholder="ENTER DEPARTMENT" Font-Size="10" Enabled="FALSE"/>

                                  <%--<asp:DropDownList ID="cmbx_Department" runat="server" CssClass="form-control" Font-Size="10">
                                         <asp:ListItem Value="0">--Select Type--</asp:ListItem>
                                        <asp:ListItem Value="Accounts">Accounts</asp:ListItem>
                                        <asp:ListItem Value="Backoffice">Operations</asp:ListItem>
                                        <asp:ListItem Value="Loan">Loan</asp:ListItem>

                                    </asp:DropDownList>--%>
                                </div>
                                <div class="col-md-6">
                                    <label style="margin-right: 20PX;">Designation</label>
                                     <asp:TextBox ID="txtDesignation" runat="server"   CssClass="form-control" placeholder="ENTER DESIGNATION" Font-Size="10" Enabled="false"/>
                                </div>
                                <div class="col-md-6">
                                    <label style="margin-right: 20PX;">Address</label>
                                    <asp:TextBox ID="txtaddress" runat="server"   CssClass="form-control" placeholder="ENTER ADDRESS" Font-Size="10" Enabled="false"/>
                                </div>
                                <div class="col-md-6">
                                    <label style="margin-right: 20PX;">EMail</label>
                                     <asp:TextBox ID="txtEmail" runat="server"  CssClass="form-control" placeholder="ENTER MAIL ID" Font-Size="10" />
                                </div>
                                <div class="col-md-6">
                                    <label style="margin-right: 20PX;">Phone</label>
                                  
                                     <asp:TextBox ID="txtPhone" runat="server"  CssClass="form-control" placeholder="XX-XXX-XXX-XX" Font-Size="10" onkeypress="return isNumberKey(event)" Enabled="false"/>
                                </div>




                                <div class="clearfix"></div>

                            </div>
                            <div class="form-group">
                                <div class="col-md-4">
                                    <label style="margin-right: 20PX;">User Name</label>
                                    <asp:TextBox ID="txtUserID" runat="server"  placeholder="Enter User ID"  CssClass="form-control" Font-Size="10" />
                                </div>
                                <div class="col-md-4">
                                    <label style="margin-right: 20PX;">Password</label>
                                    <asp:TextBox ID="txtpassword" runat="server" placeholder="XX-XXX-XXX-XX"  CssClass="form-control"   Font-Size="10"/>
                                </div>
                                <div class="col-md-4">
                                    <label style="margin-right: 20PX;">Confirm Password</label>
                                   <asp:TextBox ID="txtRePassword" runat="server"  placeholder="XX-XXX-XXX-XX" Font-Size="10"  CssClass="form-control" />
                                </div>

                                <div class="clearfix"></div>
                            </div>

                           <div class="form-group">
                                <div class="col-md-3">
                                    <label style="margin-right: 20PX;">User Level</label>
                                    <asp:DropDownList ID="ddlUserLevel" runat="server" CssClass="form-control" Font-Size="10" AutoPostBack="true" OnSelectedIndexChanged="cmbx_Auth_SelectedIndexChanged">

                                         <asp:ListItem Value="0">--Select Type--</asp:ListItem>
                                        <asp:ListItem Value="Admin">Admin</asp:ListItem>
                                        <asp:ListItem Value="User">User</asp:ListItem>
                                         

                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-3">
                                    <label style="margin-right: 20PX;">Valid Till</label>
                                     <asp:TextBox ID="txtvalidtill" placeholder="DD/MM/YYYY" CssClass="form-control input-sm BootDatepicker" runat="server" onFocus="this.select()" autocomplete="off" required="required" Font-Size="10"></asp:TextBox>
                                </div>
                           <div class="col-md-3">
                                    <label style="margin-right: 20PX;">Branch</label>
                                     <asp:DropDownList ID="cmbx_Branch" runat="server" CssClass="form-control" Font-Size="10"> </asp:DropDownList>
                                </div>
                               <div class="col-md-3">
                                    <label style="margin-right: 20PX;">Authorization</label>
                                    <asp:DropDownList ID="cmbx_Authorization" runat="server" CssClass="form-control" Font-Size="10" Enabled="false">

                                         <asp:ListItem Value="0">--Select Type--</asp:ListItem>
                                        <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                        <asp:ListItem Value="No">No</asp:ListItem>
                                         

                                    </asp:DropDownList>
                                </div>

                                <div class="clearfix"></div>
                            </div>

                             <asp:TextBox ID="txtemp" runat="server" Font-Size="10"  CssClass="form-control" Visible="false"/>


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

        <%-- Scripting Section for Calander--%>
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
 
