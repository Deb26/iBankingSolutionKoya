<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/ProjectBM.Master" CodeBehind="frmMasterKYC.aspx.cs" Inherits="iBankingSolution.MasterPages.frmMasterKYC" %>

<%@ Register TagPrefix="Ajax" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>
<%--<link rel="stylesheet" href="http://code.jquery.com/ui/1.10.3/themes/smoothness/jquery-ui.css"/>
          <script src="http://code.jquery.com/jquery-1.9.1.js"></script>
          <script src="http://code.jquery.com/ui/1.10.3/jquery-ui.js"></script>--%>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
        <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.4.1/js/bootstrap-datepicker.min.js"></script>
         <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.4.1/css/bootstrap-datepicker3.css"/>
    <style>
            #load {
            width : 80%;
            height : 80%;
            position : fixed;
            z-index : 9999;
            background : url("circle.gif") no-repeat center /*center rgba(255,255,255,255)*/
            }
     </style>
    <style>
        .popover {
            max-width: 1200px;
            left: 820px !important;
        }

        .btn-group {
            width: 270px;
        }

            .btn-group a {
                margin-right: 7px;
            }
    </style>

    <script type="text/javascript">
        $(document).ready(function () {
            $('#dataTables-example').DataTable({
                responsive: true
            });
        });
        function IsValidEmail(email) {
            var expr = /^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/;
            return expr.test(email);
        };

        $(document).ready(function () {
            $("#txtFullName").blur(function () {
                var FirstName = $("#txtFirstName").text();
               
                var MiddleName = $("#txtMiddleName");
                var LastName = $("#txtLastName");
                var FullName = $("#txtFullName");
                var Full = FirstName + " " + MiddleName + LastName;
                FullName.text(Full);
            });
        });

        
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

            
            

        <%--$(function(){
            $("#<%=txtDOB.ClientID%>").datepicker({
                dateFormate: "dd-MM-yyyy",
                changeMonth: true,
                changeYear: true,
                yearRange: '1980:2030'
                //yearRange: "-20: +20",

                //changeYear: true;
                //minDate: new Date(01,01, 1947)

            });
        });--%>
 
        <%--
        function PreviewPhoto(sender) {
            debugger;
            $("#div_Photo").html("");
            var regex = /^([a-zA-Z0-9\s_\\.\-:])+(.jpg|.jpeg|.gif|.png|.bmp)$/;
            debugger;
            if (regex.test($(sender).val().toLowerCase())) {
                {
                    if (typeof (FileReader) != "undefined") {
                        $("#div_Photo").show();
                        $("#div_Photo").append("<img />");
                        var reader = new FileReader();
                        reader.onload = function (e) {
                            var height = this.height;
                            var width = this.width;
                            if (height < 100 || width < 150) {
                                alert("At least you can upload a 1100*750 photo size.");
                                return false;
                            } else {
                                alert("Uploaded image has valid Height and Width.");
                                return true;
                            }
                            $("#div_Photo img").attr("src", e.target.result);
                            $("#div_Photo img").attr("id", "img_Photo");
                        }
                        reader.readAsDataURL($(sender)[0].files[0]);
                    } else {
                        alert("This browser does not support FileReader.");
                    }
                }
            } else {
                alert("Please upload a valid image file.");
            }
        }

        function Previewsignature(sender) {
            debugger;
            $("#div_Sign").html("");
            var regex = /^([a-zA-Z0-9\s_\\.\-:])+(.jpg|.jpeg|.gif|.png|.bmp)$/;
            debugger;
            if (regex.test($(sender).val().toLowerCase())) {
                {
                    if (typeof (FileReader) != "undefined") {
                        $("#div_Sign").show();
                        $("#div_Sign").append("<img />");
                        var reader = new FileReader();
                        reader.onload = function (e) {
                            var height = this.height;
                            var width = this.width;
                            if (height < 1100 || width < 750) {
                                alert("At least you can upload a 1100*750 photo size.");
                                return false;
                            } else {
                                alert("Uploaded image has valid Height and Width.");
                                return true;
                            }
                            $("#div_Sign img").attr("src", e.target.result);
                            $("#div_Sign img").attr("id", "img_Sign");
                        }
                        reader.readAsDataURL($(sender)[0].files[0]);
                    } else {
                        alert("This browser does not support FileReader.");
                    }
                }
            } else {
                alert("Please upload a valid image file.");
            }
        } ---%>

        //function PreviewPhoto() {
        //    var fileUpload = document.getElementById("fileUpload");
        //    var regex = new RegExp("([a-zA-Z0-9\s_\\.\-:])+(.jpg|.png|.gif)$");
        //    if (regex.test(fileUpload.value.toLowerCase())) {
        //        if (typeof (fileUpload.files) != "undefined") {
        //            var reader = new FileReader();
        //            reader.readAsDataURL(fileUpload.files[0]);
        //            reader.onload = function (e) {
        //                var image = new Image();
        //                image.src = e.target.result;
        //                image.onload = function () {
        //                    var height = this.height;
        //                    var width = this.width;
        //                    if (height > 100 || width > 100) {
        //                        alert("Height and Width must not exceed 100px.");
        //                        return false;
        //                    }
        //                    alert("Uploaded image has valid Height and Width.");
        //                    return true;
        //                };

        //            }
        //        } else {
        //            alert("This browser does not support HTML5.");
        //            return false;
        //        }
        //    } else {
        //        alert("Please select a valid Image file.");
        //        return false;
        //    }
        //}
        function PreviewPhoto(sender) {
            debugger;
            $("#div_Photo").html("");
            var regex = /^([a-zA-Z0-9\s_\\.\-:])+(.jpg|.jpeg|.gif|.png|.bmp)$/;
            debugger;
            if (regex.test($(sender).val().toLowerCase())) {
                {
                    if (typeof (FileReader) != "undefined") {
                        $("#div_Photo").show();
                        $("#div_Photo").append("<img />");
                        var reader = new FileReader();
                        reader.onload = function (e)
                        {
       
                                alert("Uploaded image has valid Height and Width.");
                                $("#div_Photo img").attr("src", e.target.result);
                                $("#div_Photo img").attr("id", "img_Photo");
                   
                        }
                        reader.readAsDataURL($(sender)[0].files[0]);
                    } else {
                        alert("This browser does not support FileReader.");
                    }
                }
            } else {
                alert("Please upload a valid image file.");
            }
        }

        function Previewsignature(sender) {
            debugger;
            $("#div_Sign").html("");
            var regex = /^([a-zA-Z0-9\s_\\.\-:])+(.jpg|.jpeg|.gif|.png|.bmp)$/;
            debugger;
            if (regex.test($(sender).val().toLowerCase())) {
                {
                    if (typeof (FileReader) != "undefined") {
                        $("#div_Sign").show();
                        $("#div_Sign").append("<img />");
                        var reader = new FileReader();
                        reader.onload = function (e)
                        {
                            var height = this.height;
                            var width = this.weidth;
                            if (height < 200 || width < 60)
                            {
                                
                                alert("Upload image has valid Height and Width.");
                                $("#div_Sign img").attr("src", e.target.result);
                                $("#div_Sign img").attr("id", "img_Sign");
                                return true;
                            }
                            else
                            {
                                alert("At least you can upload a 1100*750 photo size.");
                                return false;
                            }
                            
                        }
                        reader.readAsDataURL($(sender)[0].files[0]);
                    } else {
                        alert("This browser does not support FileReader.");
                    }
                }
            } else {
                alert("Please upload a valid image file.");
            }
        }

        function CalculateAge(sender, eventArgs) {
            debugger;
            var dtpkr_DOB = $find("<%= txtDOB.ClientID %>");
            var ntxt_Age = $find("<%= txtAge.ClientID %>");
            var date = dtpkr_DOB.get_selectedDate();
            var CurrentDate = new Date(); 7
            var timeDiff = Math.abs(CurrentDate.getTime() - date.getTime());
            var diffDays = Math.ceil(timeDiff / (1000 * 3600 * 24));
            if (dtpkr_DOB.isEmpty())
                date = new Date();
            ntxt_Age.set_value(diffDays / 365);
        };

        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode != 46 && charCode > 31
            && (charCode < 48 || charCode > 57))
                return false;
            return true;
        }



        function isNumberandDecimalKey(evt, element) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57) && !(charCode == 46 || charCode == 8))
                return false;
            else {
                var len = $(element).val().length;
                var index = $(element).val().indexOf('.');
                if (index > 0 && charCode == 46) {
                    return false;
                }
                if (index > 0) {
                    var CharAfterdot = (len + 1) - index;
                    if (CharAfterdot > 3) {
                        return false;
                    }
                }

            }
            return true;
        }

        function isNumericKey(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode != 46 && charCode > 31
            && (charCode < 48 || charCode > 57))
                return true;
            return false;
        }

        //$('document').ready(function () {

        //    $("#MainContent_txtDOB").datepicker({
        //        numberOfMonths: 1

        //    });
        //});
        //$('document').ready(function () {

        //    $("#MainContent_txtDateSinceCustomer").datepicker({
        //        numberOfMonths: 1
        //    });
        //});
        //$('document').ready(function () {

        //    $("#MainContent_dtpkr_CloseDate").datepicker({
        //        numberOfMonths: 1
        //    });
        //});

        $('#renderButton').on('click', function () {
            $('#myTab a[href="#EMD"]').tab('show');
        });


    </script>
    <style type="text/css">
        .auto-style1 {
            color: #990000;
        }

        #Table1 {
            font-style: italic;
        }
    </style>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="load"></div>
    <div class="row">
        <div class="col-lg-12">
            <div style="float: right; margin-top: 12px;">
                    <%--<a href="~/Master/frmMasterKYC.aspx" id="add" runat="server" class="btn btn-primary">Add New</a>--%>
                     <a href="frmKYCList.aspx" class="btn btn-default">Back to List</a>
                    <button type="button" class="btn btn-warning" data-toggle="modal" data-target="#myModal">Edit KYC</button>
                       <div class="modal" id="myModal">
    <div class="modal-dialog modal-sm">
      <div class="modal-content">
      
        <!-- Modal Header -->
        <div class="modal-header">
          <h4 class="modal-title">Search KYC</h4>
          <button type="button" class="close" data-dismiss="modal">&times;</button>
        </div>
        
        <!-- Modal body -->
        <div class="modal-body">
            <h5>Search By:</h5>
            <table><tr>   
                <td>         <asp:DropDownList ID="cmbx_ddlsearch" runat="server" CssClass="form-control" Width="120" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                <%--<asp:ListItem Value="0">--Select--</asp:ListItem>--%>
                <asp:ListItem Value="1">CUST_ID</asp:ListItem>
                <%--<asp:ListItem Value="2">NAME</asp:ListItem>--%>

            </asp:DropDownList></td>
                <td>&nbsp;&nbsp;&nbsp;</td>
            <td><asp:TextBox ID="txtsearchkyc" runat="server" Width="140px" CssClass="form-control" AutoPostBack="True" OnTextChanged="txtsearchkyc_TextChanged"></asp:TextBox></td>
                </tr>
</table>
        </div>
        
        <!-- Modal footer -->
        <div class="modal-footer">
          <button type="button" class="btn btn-success" data-dismiss="modal">Search</button>
        </div>
        
      </div>
    </div>
  </div>
                </div>
            <div style="float: right; margin-top: 12px;">
            </div>
            <div style="float: right; margin-top: 12px;">
                <asp:Button ID="btnSave" Visible="false" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="btnSave_Click" />
               <%-- <a href="frmKYCList.aspx" class="btn btn-default">Back to List</a>--%>
            </div>
            <h1 class="page-header">KYC Form</h1>
        </div>
    </div>
    
    <div class="row">
        <div class="col-lg-12">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    Enter Details
                </div>
                <div class="panel-heading" runat="server" id="DivID" visible="false">
                    <asp:Label ID="Label1" runat="server" Visible="False" align="center" Font-Bold="True" Font-Italic="False" Font-Names="Verdana" ForeColor="White"></asp:Label>
                </div>
                <!-- /.panel-heading -->
                <div class="panel-body">

                    <table width="100%" align="center">
                        <tr>
                                <asp:Button Text="Personal Details" BorderStyle="None" ID="Tab1" CssClass="Initial" runat="server"
                                    OnClick="Tab1_Click" Font-Bold="True" Font-Names="Verdana" />
                                <asp:Button Text="KYC Details" BorderStyle="None" ID="Tab2" CssClass="Initial" runat="server"
                                    OnClick="Tab2_Click" Font-Bold="True" Font-Names="Verdana" />
                                <asp:Button Text="Address" BorderStyle="None" ID="Tab3" CssClass="Initial" runat="server"
                                    OnClick="Tab3_Click" Font-Bold="True" Font-Names="Verdana" />
                                <asp:Button Text="Photo Upload" BorderStyle="None" ID="Tab4" CssClass="Initial" runat="server"
                                    OnClick="Tab4_Click" Font-Bold="True" Font-Names="Verdana" />

                                <asp:MultiView ID="MainView" runat="server">
                                    <asp:View ID="View1" runat="server">
                                        <h4 class="auto-style1"><em>Personal Details</em></h4>
                                        <table style="width: 100%; border-width: 0px; border-color: #666; border-style: solid">
                                            <tr>
                                                <td>

                                                  
                                                    <div class="col-md-4">
                                                        <asp:Label ID="Label2" runat="server" Text="*" Style="color: #FF3300"></asp:Label>
                                                        <label>First name</label>
                                                        <asp:Label ID="lblDid" runat="server" BackColor="White" ForeColor="White"></asp:Label>
                                                        <asp:Label ID="lblSession" runat="server" BackColor="White" ForeColor="White"></asp:Label>
                                                        <asp:TextBox runat="server" ID="txtFirstName" placeholder="EnterFirstName" Font-Size="10" Style="text-transform: uppercase;" CssClass="form-control" required="required" OnTextChanged="txtFirstName_TextChanged" autoComplete="off"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <label>Middle Name</label>
                                                        <asp:TextBox runat="server" ID="txtMiddleName" placeholder="EnterMiddleName" Font-Size="10" Style="text-transform: uppercase;" CssClass="form-control" OnTextChanged="txtMiddleName_TextChanged"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:Label ID="Label3" runat="server" Text="*" Style="color: #FF3300"></asp:Label>
                                                        <label>Last Name</label>
                                                        <asp:TextBox runat="server" ID="txtLastName" placeholder="EnterLastName" Font-Size="10" Style="text-transform: uppercase;" CssClass="form-control" requied="required" AutoPostBack="True" OnTextChanged="txtLastName_TextChanged"></asp:TextBox>
                                                    </div>


                                                    <div class="clearfix"></div>
                                                    <div class="form-group">
                                                        <div class="col-md-12">
                                                            <br />
                                                            <label>Full Name</label>
                                                            <asp:TextBox runat="server" ID="txtFullName" Font-Size="10" placeholder="ENTER FULL NAME" Style="text-transform: uppercase;" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-12">
                                                            <br />
                                                            <label>Father/Husband Name</label>
                                                            <asp:TextBox runat="server" ID="txtFatherHusName" Font-Size="10" placeholder="EnterFatherName" Style="text-transform: uppercase;" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <br />
                                                            <asp:Label ID="Label4" runat="server" Text="*" Style="color: #FF3300"></asp:Label>
                                                            <label>Date Of Birth</label>
                                                            <asp:TextBox runat="server" ID="txtDOB" Font-Size="10" placeholder="dd/mm/yyyy" CssClass="form-control input-sm BootDatepicker" required="required" OnClick="CalculateAge" AutoPostBack="True" OnTextChanged="txtDOB_TextChanged"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <br />
                                                            <label>Age</label>
                                                            <asp:TextBox runat="server" ID="txtAge" Font-Size="10" placeholder="XX" onkeypress="return isNumberKey(event)" CssClass="form-control" AutoPostBack="True" OnTextChanged="txtAge_TextChanged"></asp:TextBox>
                                                        </div>

                                                        <div class="col-md-3">
                                                            <br />
                                                            <asp:Label ID="Label5" runat="server" Text="*" Style="color: #FF3300"></asp:Label>
                                                            <label>Gender</label>
                                                            <asp:DropDownList ID="cmbx_Gender" runat="server" Font-Size="10" required="required" CssClass="form-control">
                                                                <asp:ListItem Value="0">--SELECT GENDER--</asp:ListItem>
                                                                <asp:ListItem Value="Male">Male</asp:ListItem> 
                                                                <asp:ListItem Value="Female">Female</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="Genderr" runat="server" ControlToValidate="cmbx_Gender"
                                                                InitialValue="0" ErrorMessage="Please Select Item"></asp:RequiredFieldValidator>
                                                        </div>

                                                        <div class="col-md-3">
                                                            <br />
                                                            <label>Religion</label>
                                                            <asp:DropDownList ID="cmbx_Religion" runat="server" Font-Size="10" CssClass="form-control">
                                                                <asp:ListItem Value="0">--SELECT RELIGION--</asp:ListItem>
                                                                <asp:ListItem Value="1">HINDU</asp:ListItem>
                                                                <asp:ListItem Value="2">MUSLIM</asp:ListItem>
                                                                <asp:ListItem Value="3">CHRISTIAN</asp:ListItem>
                                                                <asp:ListItem Value="4">OTHER</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div class="clearfix"></div>
                                                    </div>
                                                    <div class="clearfix"></div>
                                                    <div class="form-group">
                                                        <div class="col-md-3">
                                                            <asp:Label ID="Label6" runat="server" Text="*" Style="color: #FF3300"></asp:Label>
                                                            <label>Category</label>
                                                            <asp:DropDownList ID="cmbx_Category" runat="server" Font-Size="10" required="required" CssClass="form-control">
                                                                <asp:ListItem Value="0">--SELECT CATEGORY--</asp:ListItem>
                                                                <asp:ListItem Value="1">AI/CO-OP SOCIETY</asp:ListItem>
                                                                <asp:ListItem Value="2">MEN</asp:ListItem>
                                                                <asp:ListItem Value="3">LCF</asp:ListItem>
                                                                <asp:ListItem Value="4">MIXED</asp:ListItem>
                                                                <asp:ListItem Value="5">OTHERS</asp:ListItem>
                                                                <asp:ListItem Value="6">WOMEN</asp:ListItem>
                                                                <asp:ListItem Value="7">OB CLASS</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="Category" runat="server" ControlToValidate="cmbx_Category"
                                                                InitialValue="0" ErrorMessage="Please Select Item"></asp:RequiredFieldValidator>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <asp:Label ID="Label7" runat="server" Text="*" Style="color: #FF3300"></asp:Label>
                                                            <label>Spcial Category</label>
                                                            <asp:DropDownList ID="cmbx_SpecialCategory" Font-Size="10" runat="server" required="required" CssClass="form-control">
                                                                <asp:ListItem Value="0">--SPECIAL CATEGORY--</asp:ListItem>
                                                                <asp:ListItem Value="1">GENERAL</asp:ListItem>
                                                                <asp:ListItem Value="2">MINORITY</asp:ListItem>
                                                                <asp:ListItem Value="3">OB CLASS</asp:ListItem>
                                                                <asp:ListItem Value="4">OTHERS</asp:ListItem>
                                                                <asp:ListItem Value="5">PHYSICALLY HANDICAPED</asp:ListItem>
                                                                <asp:ListItem Value="6">SC</asp:ListItem>
                                                                <asp:ListItem Value="7">ST</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="SpecialCategory" runat="server" ControlToValidate="cmbx_SpecialCategory"
                                                                InitialValue="0" ErrorMessage="Please Select Item"></asp:RequiredFieldValidator>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <label>Membership No</label>
                                                            <asp:TextBox runat="server" ID="txt_MembershipNo" Font-Size="10" placeholder="ENTERMEMBERNUMBER" style="text-transform: uppercase;" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <label>Membership Status</label>
                                                            <asp:DropDownList ID="cmbx_MembershipStatus" Font-Size="10" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="cmbx_MembershipStatus_SelectIndex">
                                                                <asp:ListItem Value="0">--SELECT STATUS--</asp:ListItem>
                                                                <asp:ListItem Value="Active">Active</asp:ListItem>
                                                                <asp:ListItem Value="Close">Close</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>

                                                        <div class="clearfix"></div>
                                                    </div>
                                                    <div class="clearfix"></div>
                                                    <div class="form-group">
                                                        <div class="col-md-3">
                                                            <label>Client Type</label>
                                                            <asp:DropDownList ID="cmbx_ClientType" Font-Size="10" runat="server" CssClass="form-control">
                                                                <asp:ListItem Value="0">--SELECT TYPE--</asp:ListItem>
                                                                <asp:ListItem Value="APL">APL</asp:ListItem>
                                                                <asp:ListItem Value="BPL">BPL</asp:ListItem>
                                                            </asp:DropDownList>

                                                        </div>
                                                        <div class="col-md-3">
                                                            <asp:Label ID="Label15" runat="server" Text="*" Style="color: #FF3300"></asp:Label>
                                                            <label>Client Status</label>
                                                            <asp:DropDownList ID="cmbx_ClientStatus" Font-Size="10" runat="server" CssClass="form-control" required="required">
                                                                <asp:ListItem Value="0">--SELECT STATUS--</asp:ListItem>
                                                                <asp:ListItem Value="m">MEMBER</asp:ListItem>
                                                                <asp:ListItem Value="n">NON MEMBER</asp:ListItem>
                                                                <asp:ListItem Value="nm">NOMINAL MEMBER</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div class="col-md-3">

                                                            <label>Dt.Since Customer</label>
                                                            <asp:TextBox runat="server" ID="txtDateSinceCustomer" Font-Size="10" placeholder="Enter DSCoustomer" CssClass="form-control input-sm BootDatepicker" autoComplete="off"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <label>Phone</label>
                                                            <asp:TextBox runat="server" ID="txt_Telephone" MaxLength="10" required="true" Font-Size="10" onkeypress="return isNumberKey(event)" placeholder="EnterPhoneNumber" CssClass="form-control"></asp:TextBox>
                                                        </div>


                                                        <div class="clearfix"></div>
                                                    </div>
                                                    <div class="clearfix"></div>
                                                    <div class="form-group">
                                                        <div class="col-md-3">
                                                            <label>Close Date</label>
                                                            <asp:TextBox runat="server" ID="dtpkr_CloseDate" Font-Size="10" placeholder="DD/MM/YYYY" CssClass="form-control input-sm BootDatepicker" autoComplete="off" Enabled="false"></asp:TextBox>

                                                        </div>

                                                        <div class="col-md-9">
                                                            <label>Cause of Close</label>
                                                            <asp:TextBox runat="server" ID="txt_CloseCause" placeholder="EnterCause" Font-Size="10" Style="text-transform: uppercase;" CssClass="form-control" autoComplete="off" Enabled="false"></asp:TextBox>
                                                        </div>

                                                        <div class="clearfix"></div>

                                                    </div>





                                                    <hr />
                                                    <%--Start Grid--%>
                                                    <h4 class="auto-style1"><em>Education and Professional Details</em></h4>
                                                    <br />
                                                    <div class="row">
                                                        <div class="form-group">
                                                            <div class="col-md-3">
                                                                <label>Educational Qualification</label>

                                                                <asp:DropDownList ID="cmbEducationalQualification" runat="server" Font-Size="10" CssClass="form-control">
                                                                    <asp:ListItem Value="0">--SELECT EDUCATION--</asp:ListItem>
                                                                    <asp:ListItem Value="1">MATRICULATION</asp:ListItem>
                                                                    <asp:ListItem Value="2">HIGHER SECONDARY</asp:ListItem>
                                                                    <asp:ListItem Value="3">GRADUATION</asp:ListItem>
                                                                    <asp:ListItem Value="4">POST GRADUATE</asp:ListItem>
                                                                </asp:DropDownList>

                                                            </div>

                                                            <div class="col-md-3">
                                                                <label>Profession</label>
                                                                <asp:DropDownList ID="cmbx_Profession" runat="server" Font-Size="10" CssClass="form-control">
                                                                    <asp:ListItem Value="0">--SELECT PROFESSION--</asp:ListItem>
                                                                    <asp:ListItem Value="1">BUSINESS</asp:ListItem>
                                                                    <asp:ListItem Value="2">SERVICE</asp:ListItem>
                                                                    <asp:ListItem Value="3">FARMAR</asp:ListItem>
                                                                    <asp:ListItem Value="4">HOUSE WIFE</asp:ListItem>
                                                                    <asp:ListItem Value="5">DOCTOR</asp:ListItem>
                                                                    <asp:ListItem Value="6">LAWER</asp:ListItem>
                                                                    <asp:ListItem Value="7">OTHER</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>

                                                            <div class="col-md-3">
                                                                <label>Monthly Income</label>
                                                                <asp:TextBox runat="server" ID="ntxt_MonthlyIncome" placeholder="ENTER MONTHLY INCOME" onkeypress="return isNumberKey(event)" Font-Size="10" CssClass="form-control"></asp:TextBox>
                                                            </div>
                                                            <div class="col-md-3">
                                                                <label>School certificate</label>
                                                                <asp:TextBox runat="server" ID="txt_SchoolCer" placeholder="ENTER SCHOOL CERT.NO." Font-Size="10" Style="text-transform: uppercase;" CssClass="form-control"></asp:TextBox>
                                                            </div>

                                                            <div class="clearfix"></div>
                                                            <br />
                                                            <br />
                                                            <br />
                                                            <br />
                                                            <table align="center">
                                                                <tr>
                                                                    <td>
                                                                        <a href="frmKYCList.aspx" class="btn btn-default">Back to List</a>
                                                                        <asp:Button ID="btnNext2" runat="server" Text="Next" CssClass="btn btn-primary" OnClick="btnNext2_Click" />
                                                                        <a href="frmMasterKYC.aspx" class="btn btn-danger">Reset</a>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>

                                                        <%--End Grid--%>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:View>
                                    <asp:View ID="View2" runat="server">
                                        <table style="width: 100%; border-width: 0px; border-color: #666; border-style: solid">
                                            <tr>
                                                <td>

                                                    <h4 class="auto-style1"><em>KYC Details</em></h4>

                                                    <div class="panel-body">
                                                        <div class="row">
                                                            <div class="form-group">
                                                                <div class="col-md-3">
                                                                    <asp:Label ID="Label9" runat="server" Text="" Style="color: #FF3300"></asp:Label>
                                                                    <label>KCC Card No</label>
                                                                    <asp:TextBox runat="server" ID="txt_KCCCardNo" Font-Size="10" placeholder="ENTER KCC CARD NO" Style="text-transform: uppercase;"  CssClass="form-control"></asp:TextBox>
                                                                </div>

                                                                <div class="col-md-3">
                                                                    <asp:Label ID="Label8" runat="server" Text="*" Style="color: #FF3300"></asp:Label>
                                                                    <label>Land Less </label>
                                                                    <asp:DropDownList ID="cmbx_LandLess" Font-Size="10" runat="server" CssClass="form-control" required="required">
                                                                        <asp:ListItem Value="0">--LAND INFO--</asp:ListItem>
                                                                        <asp:ListItem Value="1">YES</asp:ListItem>
                                                                        <asp:ListItem Value="2">NO</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                                <div class="col-md-3">
                                                                    <label>Land Acre </label>
                                                                    <asp:TextBox runat="server" ID="txt_LandHolding" Font-Size="10" placeholder="LAND IN ACRE" Style="text-transform: uppercase;" onkeypress="return isNumberandDecimalKey(event,this)" CssClass="form-control"></asp:TextBox>
                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txt_LandHolding" ErrorMessage="Accept Numbers" ForeColor="#990000" ValidationExpression="^[1-9]\d*(\.\d+)?$"></asp:RegularExpressionValidator>
                                                                </div>

                                                                <div class="col-md-3">
                                                                    <%--<asp:Label ID="Label10" runat="server" Text="*" Style="color: #FF3300"></asp:Label>--%>
                                                                    <label>Aadhaar No </label>
                                                                    <asp:TextBox runat="server" ID="txt_AdharNo" MaxLength="12" Font-Size="10" placeholder="ENTER AADHAAR NO" Style="text-transform: uppercase;"   CssClass="form-control"></asp:TextBox>
                                                                </div>

                                                                <div class="clearfix"></div>
                                                            </div>
                                                            <div class="clearfix"></div>
                                                            <div class="form-group">
                                                                <div class="col-md-3">
                                                                    <label>Voter Card No </label>
                                                                    <asp:TextBox runat="server" ID="txt_Votar" Font-Size="10" placeholder="ENTER VOTER NO" Style="text-transform: uppercase;" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                                <div class="col-md-3">
                                                                    <label>Ration Card No</label>
                                                                    <asp:TextBox runat="server" ID="txt_RationCardNo" Font-Size="10" placeholder="ENTER RATION NO" Style="text-transform: uppercase;" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                                <div class="col-md-3">
                                                                    <label>Passport No</label>
                                                                    <asp:TextBox runat="server" ID="txt_Passport" Font-Size="10" placeholder="ENTER PASSPORT NO" Style="text-transform: uppercase;" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                                <div class="col-md-3">

                                                                    <label>PAN Card No</label>
                                                                    <asp:TextBox runat="server" ID="txt_PAN" Font-Size="10" placeholder="ENTER PAN NO" Style="text-transform: uppercase;" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                                <div class="clearfix"></div>
                                                            </div>
                                                            <div class="clearfix"></div>

                                                            <div class="form-group">
                                                                <div class="col-md-12">
                                                                    <label>BPL No</label>
                                                                    <asp:TextBox runat="server" ID="txt_BPLNo" Font-Size="10" placeholder="ENTER BPL NO" Style="text-transform: uppercase;" CssClass="form-control"></asp:TextBox>

                                                                </div>

                                                                <div class="clearfix"></div>
                                                            </div>


                                                            <div class="clearfix">
                                                                <br />
                                                                <br />

                                                                <table align="center">
                                                                    <tr>
                                                                        <td>
                                                                              <a href="frmKYCList.aspx" class="btn btn-default">Back to List</a>
                                                                            <asp:Button ID="btnNext3" runat="server" CssClass="btn btn-primary" OnClick="btnNext3_Click" Text="Next" />
                                                                            <a href="frmMasterKYC.aspx" class="btn btn-danger">Reset</a>
                                                                        </td>
                                                                    </tr>
                                                                </table>

                                                            </div>
                                                        </div>
                                                    </div>

                                                </td>
                                            </tr>
                                        </table>
                                    </asp:View>
                                    <asp:View ID="View3" runat="server">
                                        <br />
                                        <h4 class="auto-style1"><em>Address</em></h4>
                                        <table style="width: 100%; border-width: 0px; border-color: #666; border-style: solid">

                                            
                                                <div class="col-md-3">
                                                    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
                                                    </asp:ScriptManager>
                                                    <asp:Label ID="Label11" runat="server" Text="*" Style="color: #FF3300"></asp:Label>
                                                    <label>Village</label>
                                                    <asp:TextBox ID="txt_Village" runat="server" Font-Size="10" placeholder="ENTER VILLAGE NAME"  style="text-transform: uppercase;" CssClass="form-control" required="required" />
                                                    <Ajax:AutoCompleteExtender ServiceMethod="SearchVillage"
                                                        MinimumPrefixLength="2"
                                                        CompletionInterval="50" EnableCaching="false" CompletionSetCount="3"
                                                        TargetControlID="txt_Village"
                                                        ID="AutoCompleteExtender1" runat="server" FirstRowSelected="false">
                                                    </Ajax:AutoCompleteExtender>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:Label ID="Label12" runat="server" Text="*" Style="color: #FF3300"></asp:Label>
                                                    <label>Gram Panchayet(Muni)</label>
                                                    <asp:TextBox ID="txt_Municipality" runat="server" Font-Size="10" placeholder="ENTER PANCHAYAT NAME" style="text-transform: uppercase;" CssClass="form-control" required="required" />
                                                    <Ajax:AutoCompleteExtender ServiceMethod="SearchMunicipalty"
                                                        MinimumPrefixLength="2"
                                                        CompletionInterval="50" EnableCaching="false" CompletionSetCount="3"
                                                        TargetControlID="txt_Municipality"
                                                        ID="AutoCompleteExtender2" runat="server" FirstRowSelected="false">
                                                    </Ajax:AutoCompleteExtender>
                                               </div>
                                              
                                                <div class="col-md-3">
                                                    <asp:Label ID="Label13" runat="server" Text="*" Style="color: #FF3300"></asp:Label>
                                                    <label>Post Office</label>
                                                    <asp:TextBox ID="txt_PostOffice" Font-Size="10" placeholder="ENTER POST OFFICE" style="text-transform: uppercase;" runat="server"   CssClass="form-control" required="required" />
                                                    <Ajax:AutoCompleteExtender ServiceMethod="SearchPost"
                                                        MinimumPrefixLength="2"
                                                        CompletionInterval="50" EnableCaching="false" CompletionSetCount="3"
                                                        TargetControlID="txt_PostOffice"
                                                        ID="AutoCompleteExtender3" runat="server" FirstRowSelected="false">
                                                    </Ajax:AutoCompleteExtender>
                                                 </div>
                                                 <div class="col-md-3">
                                                    <label>Police Station</label>
                                                    <asp:TextBox ID="txt_PoliceStation" placeholder="ENTER POLICE STATION" style="text-transform: uppercase;" runat="server"  CssClass="form-control" />
                                                    <Ajax:AutoCompleteExtender ServiceMethod="SearchPoliceSt"
                                                        MinimumPrefixLength="2"
                                                        CompletionInterval="50" EnableCaching="false" CompletionSetCount="3"
                                                        TargetControlID="txt_PoliceStation"
                                                        ID="AutoCompleteExtender4" runat="server" FirstRowSelected="false">
                                                    </Ajax:AutoCompleteExtender>
                                                 </div>
                                                
                                            <div class="clearfix"></div>
                                            <br />
                                                  <div class="col-md-3">
                                                    <label>Sub Division</label>
                                                    <asp:TextBox ID="txt_SubDivision" Font-Size="10"  placeholder="ENTER SUB DIVISSION" style="text-transform: uppercase;" runat="server" CssClass="form-control" />
                                                    <Ajax:AutoCompleteExtender ServiceMethod="SearchDivision"
                                                        MinimumPrefixLength="2"
                                                        CompletionInterval="50" EnableCaching="false" CompletionSetCount="3"
                                                        TargetControlID="txt_SubDivision"
                                                        ID="AutoCompleteExtender5" runat="server" FirstRowSelected="false">
                                                    </Ajax:AutoCompleteExtender>
                                                </div>

                                                <div class="col-md-3">
                                                    <label>Block</label>
                                                    <asp:TextBox ID="txt_Block" runat="server" Font-Size="10" placeholder="ENTER BLOCK" style="text-transform: uppercase;" CssClass="form-control" />
                                                    <Ajax:AutoCompleteExtender ServiceMethod="SearchBlock"
                                                        MinimumPrefixLength="2"
                                                        CompletionInterval="50" EnableCaching="false" CompletionSetCount="3"
                                                        TargetControlID="txt_Block"
                                                        ID="AutoCompleteExtender6" runat="server" FirstRowSelected="false">
                                                    </Ajax:AutoCompleteExtender>
                                                </div>

                                                <div class="col-md-3">
                                                    <label>District</label>
                                                    <asp:TextBox ID="txt_District" runat="server" Font-Size="10"  placeholder="ENTER DISTRICY NAME" Style="text-transform: uppercase;" CssClass="form-control" />
                                                    <Ajax:AutoCompleteExtender ServiceMethod="SearchDistrict"
                                                        MinimumPrefixLength="2"
                                                        CompletionInterval="50" EnableCaching="false" CompletionSetCount="3"
                                                        TargetControlID="txt_District"
                                                        ID="AutoCompleteExtender7" runat="server" FirstRowSelected="false">
                                                    </Ajax:AutoCompleteExtender>
                                               </div>
                                            

                                        </table>
                                        <div class="clearfix">


                                            <table align="center">
                                                <br />
                                                <br />
                                                <br />
                                                <tr>
                                                    <td>
                                                         <a href="frmKYCList.aspx" class="btn btn-default">Back to List</a>
                                                        <asp:Button ID="btnNext4" runat="server" CssClass="btn btn-primary" OnClick="btnNext4_Click" Text="Next" />
                                                        <a href="frmMasterKYC.aspx" class="btn btn-danger">Reset</a>
                                                    </td>
                                                </tr>
                                            </table>

                                        </div>
                                    </asp:View>
                                    <%--- For Image And Signature---%>
                                    <asp:View ID="View4" runat="server">
                                        <br />
                                        <h4 class="auto-style1"><em>Photo Upload</em></h4>
                                        <table>
                                            <br />
                                            <tr>
                                                <td>

                                                    <label style="margin-right: 30PX;">Photo Upload</label>
                                                    <asp:FileUpload runat="server" ID="ProjectFileUpload" onchange="PreviewPhoto(this)" />
                                                    <asp:Button ID="btnUpload" runat="server" Text="Upload" CausesValidation="False" OnClick="btnUpload_Click" Visible="False" />
                                                    
                                                    <asp:HiddenField ID="hiddenImgEmp" runat="server" />
                                                    <%--<label style="margin-right: 30px;">Photo Upload</label>
                                                    <asp:FileUpload ID="ProjectFileUpload" runat="server" />
                                                    <br />
                                                    <br />
                                                    <asp:Button ID="Button1" runat="server" Text="Upload Image" OnClick="btnUpload_Click"></asp:Button>
                                                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                                    <asp:Image ID="Image1" runat="server" Height="164px" ImageUrl="~/Images/images.jpg" />--%>

                                                </td>
                                               <%-- <td>
                                                     <asp:FileUpload ID="FileUpload1" runat="server" />
                                                     <asp:Button ID="Button3" runat="server" Text="Upload Image" OnClick="Button1_Click"></asp:Button>
                                                      &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                                     <asp:Image ID="Image2" runat="server" Height="164px" ImageUrl="~/Images/images.jpg" />
                                                </td>--%>
                                                <td>
                                                    <div class="col-xs-12" id="div_Photo" style="height: 200px;">
                                                        <asp:Image ID="imgEmp" runat="server" Width="117" Height="117" CssClass="img-thumbnail" ImageUrl="~/Images/images.jpg" />
                                                    </div>

                                                </td>

                                                <td colspan="4">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                                </td>
                                                <td></td>




                                                <td>


                                                    <label style="margin-right: 30px;">Signature Upload</label>     
                                                    <asp:FileUpload ID="FileSignatureUpload" runat="server" onchange="Previewsignature(this)" ClientIDMode="Static" />
                                                    <asp:Button ID="btnUpload1" runat="server" Text="Upload" OnClick="btnUpload1_Click" CausesValidation="False" Visible="False" />
                                                    <asp:HiddenField ID="hiddenImgsign" runat="server" />
                                                    <%--<label style="margin-right: 30px;">Signature Upload</label>
                                                    <asp:FileUpload ID="FileUpload2" runat="server" />
                                                    <br />
                                                    <br />
                                                    <asp:Button ID="Button2" runat="server" Text="Upload Signature" OnClick="btnUpload1_Click"></asp:Button>
                                                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;--%>

                                                </td>


                                                <td>
                                                    <div class="col-xs-12" id="div_Sign" style="height: 200px;">
                                                        <asp:Image ID="ImgSig" runat="server" CssClass="img-thumbnail" Height="117" ImageUrl="~/Images/No_Signature.jpg" Width="237" />
                                                    </div>
                                                </td>
                                            </tr>

                                        </table>

                                        <div class="clearfix">
                                            <table align="center">
                                                <tr>
                                                    <td>
                                                        <a href="frmKYCList.aspx" class="btn btn-default">Back to List</a>
                                                        <asp:Button ID="btnSave1" Visible="false" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="btnSave_Click" />
                                                        <a href="frmMasterKYC.aspx" class="btn btn-danger">Reset</a>
                                                    </td>
                                                </tr>

                                            </table>
                                        </div>
                                    </asp:View>
                                    <%--</asp:MultiView>--%>
                                    <%--</td>--%>
                                    <%--</tr>--%>
                                     <%--  <asp:LinkButton ID="btnUpload1" runat="server" Click="btnUpload1_Click" OnClick="btnUpload1_Click">Click to Upload</asp:LinkButton>--%>
                               <%--<asp:LinkButton ID="btnUpload" runat="server" Click="btnUpload_Click" OnClick="btnUpload_Click" ClientIDMode="Static">Click to Upload</asp:LinkButton>--%>
                                    <%--</table>--%>
                                </asp:MultiView>
                            </td>
                        </tr>

                    </table>

                </div>
                <div class="row">
                    <div class="col-lg-12">
                        <div style="float: right; margin-top: 12px;">
                        </div>
                        <div style="float: right; margin-top: 12px;">
                            

                           
                        </div>

                    </div>
                </div>

               

            </div>
        </div>
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
