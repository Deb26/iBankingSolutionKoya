<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/ProjectBM.Master" CodeBehind="frmEmployeeMaster.aspx.cs" Inherits="iBankingSolution.Master.frmEmployeeMaster" %>

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
        function IsValidEmail(email) {
            var expr = /^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/;
            return expr.test(email);
        };

        function ValidateBasicDetails() {
            //debugger;

            if ($('#MainContent_txtUserId').val() == '') {
                $('#MainContent_txtUserId').attr('required', true);

                return false;
            }
        }
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode != 46 && charCode > 31
            && (charCode < 48 || charCode > 57))
                return false;
            return true;
        }
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
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

     
        <asp:ScriptManager ID="Emp" runat="server"></asp:ScriptManager>
        <div class="row">
            <div class="col-lg-12">
                <div style="float: right; margin-top: 12px;">
                </div>
                <div style="float: right; margin-top: 12px;">
                    <asp:Button ID="Btn_Save" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="Btn_Save_Click" />
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" class="btn btn-primary" OnClick="btnCancel_Click" />
                    <a href="frmEmployeeMasterList.aspx" class="btn btn-default">Back to List</a>


                </div>
                <h1 class="page-header">Employee Master</h1>
            </div>
        </div>
        <div class="row">
            <div class="col-lg-12">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        Enter Details
                    </div>
                    <!-- /.panel-heading -->
                    <div class="panel-body">
                        <!-- Nav tabs -->
                        <ul class="nav nav-tabs">
                            <li class="active"><a href="#home" data-toggle="tab">Personal Details</a></li>
                        </ul>

                        <!-- Tab panes -->
                        <div class="tab-content">
                            <div class="tab-pane fade in active" id="home">


                                <div class="panel-body">
                                    <div class="row">
                                        <div class="form-group">
                                            <div class="col-md-6">
                                                
                                                <asp:Label ID="Label2" runat="server" Text="*" style="color: #FF3300"></asp:Label>
                                                <label>Name</label>
                                                <asp:TextBox runat="server" ID="txt_Name" CssClass="form-control" placeholder="Enter Name" Font-Size="10" required="required"></asp:TextBox>
                                                     <asp:Label ID="lblDid" runat="server" BackColor="White" ForeColor="White"></asp:Label>
                                            </div>

                                            <div class="col-md-3">
                                                <label style="margin-right: 20PX;">Photo Upload</label>
                                                <asp:FileUpload runat="server" ID="fu_Photo" />
                                                 <asp:Button ID="btnUpload" runat="server" Text="Upload" OnClick="btnUpload_Click" CausesValidation="False" />
                                                <asp:HiddenField ID="hiddenImgEmp" runat="server" />
                                            </div>
                                            <div class="col-md-3">
                                                <asp:Image ID="imgEmp" runat="server" Width="117" Height="117" CssClass="img-thumbnail" />

                                            </div>


                                            <div class="clearfix"></div>
                                        </div>
                                        <div class="clearfix"></div>
                                        <div class="form-group">

                                            <div class="col-md-12">
                                                <label>Father/Husband Name</label>
                                                <asp:TextBox runat="server" ID="txt_FathersName" placeholder="ENTER FATHER/HUSBAND NAME" Font-Size="10" CssClass="form-control"></asp:TextBox>
                                            </div>

                                            <div class="col-md-12">
                                                <label>Address</label>
                                                <asp:TextBox runat="server" ID="txt_Address" placeholder="ENTER ADDRESS" Font-Size="10" CssClass="form-control"></asp:TextBox>
                                            </div>

                                            <div class="col-md-12">
                                                <label>Educational Qualification</label>
                                                <asp:TextBox runat="server" ID="txt_EducationalQualification" placeholder="ENTER QUALIFICATION" Font-Size="10" CssClass="form-control"></asp:TextBox>
                                            </div>


                                            <div class="col-md-3">
                                                <label>Pin Code</label>
                                                <asp:TextBox runat="server" ID="txt_PinCode" placeholder="ENTER PINCODE" Font-Size="10" onkeypress="return isNumberKey(event)" CssClass="form-control"></asp:TextBox>

                                            </div>

                                            <div class="col-md-3">
                                                <label>Phone</label>
                                                <asp:TextBox runat="server" ID="txt_Phone" placeholder="ENTER PHONE" Font-Size="10" onkeypress="return isNumberKey(event)" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="col-md-3">
                                                <label>Sex</label>
                                                <asp:DropDownList ID="cmbx_Sex" runat="server" CssClass="form-control" Font-Size="10">
                                                    <asp:ListItem Value="0">--Select Type--</asp:ListItem>
                                                    <asp:ListItem Value="Female">Female</asp:ListItem>
                                                    <asp:ListItem Value="Male">Male</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-3">
                                                <label>Marital Status</label>
                                                <asp:DropDownList ID="cmbx_MaritalStatus" runat="server" CssClass="form-control" Font-Size="10">
                                                    <asp:ListItem Value="0">--Select Type--</asp:ListItem>
                                                    <asp:ListItem Value="Single">Single</asp:ListItem>
                                                    <asp:ListItem Value="Married">Married</asp:ListItem>
                                                    <asp:ListItem Value="Widow">Widow</asp:ListItem>
                                                    <asp:ListItem Value="Divorced">Divorced</asp:ListItem>
                                                    <asp:ListItem Value="Separated">Separated</asp:ListItem>

                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-3">
                                                <label>Department</label>
                                                <asp:TextBox runat="server" ID="txt_Department" placeholder="Enter Department" Font-Size="10" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="col-md-3">
                                                <label>Designation</label>
                                                <asp:TextBox runat="server" ID="txt_Designation" placeholder="Enter Designation" Font-Size="10" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="col-md-6">
                                                <label>Reports To</label>
                                                <asp:DropDownList ID="cmbx_ReportsTo" runat="server" CssClass="form-control" Font-Size="10"></asp:DropDownList>
                                            </div>





                                            <div class="col-md-4">
                                                <label>Date Of Birth</label>

                                                <asp:TextBox ID="dtpkr_DOB" CssClass="form-control input-sm BootDatepicker" runat="server" onFocus="this.select()" autocomplete="off" Font-Size="10"></asp:TextBox>
                                            </div>
                                            <div class="col-md-4">
                                                <label>Date Of Joining</label>
                                                <asp:TextBox ID="dtpkr_DOJ" CssClass="form-control input-sm BootDatepicker" runat="server" onFocus="this.select()" autocomplete="off" Font-Size="10"></asp:TextBox>
                                            </div>

                                            <div class="col-md-4">
                                                <label>Date of Retirment</label>
                                                <asp:TextBox ID="dtpkr_DOR" CssClass="form-control input-sm BootDatepicker" runat="server" onFocus="this.select()" autocomplete="off" Font-Size="10"></asp:TextBox>
                                            </div>


                                            <div class="clearfix"></div>
                                        </div>
                                        <div class="clearfix"></div>
                                        <div class="form-group">
                                            <div class="col-md-3">
                                                <label>ARCS Sanction</label>
                                                <asp:DropDownList ID="cmbx_ARCSSanction" runat="server" CssClass="form-control" Font-Size="10">
                                                    <asp:ListItem Value="0">--Select Type--</asp:ListItem>
                                                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                                    <asp:ListItem Value="No">No</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-4">
                                                <label>Sanction Date</label>
                                                <asp:TextBox ID="dtpkr_SanctionDate" CssClass="form-control input-sm BootDatepicker" placeholder="dd/MM/yyyy" runat="server" onFocus="this.select()" autocomplete="off" Font-Size="10"></asp:TextBox>
                                            </div>
                                            <div class="col-md-3">
                                                <label>Sanction No</label>
                                                <asp:TextBox runat="server" ID="txt_SanctionNo" CssClass="form-control" placeholder="ENTER SANCTION NO" Font-Size="10"></asp:TextBox>
                                            </div>


                                            <div class="clearfix"></div>
                                        </div>






                                        <div class="form-group">
                                            <div class="col-md-12 text-center">
                                                &nbsp;
                                                                        <%--<a href="frmUserMaster.aspx" class="btn btn-center btn-danger btn-sm">Reset</a>--%>
                                            </div>
                                            <div class="clearfix"></div>
                                        </div>
                                        <div class="clearfix"></div>
                                    </div>

                              
                                </div>

                            </div>

                        </div>
                    </div>
                </div>
                <!-- /.panel-body -->
            </div>
            <!-- /.panel -->
        </div>
        <%--   </div>--%>
 
</asp:Content>
