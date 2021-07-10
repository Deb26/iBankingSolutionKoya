<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/ProjectBM.Master" CodeBehind="frmMasterKYC.aspx.cs" Inherits="iBankingSolution.MasterPages.frmMasterKYC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        function IsValidEmail(email) {
            var expr = /^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/;
            return expr.test(email);
        };
        function CalculateAge(sender, eventArgs) {
            debugger;
            var dtpkr_DOB = $find("<%= txtDOB.ClientID %>");
            var ntxt_Age = $find("<%= txtAge.ClientID %>");
             var date = dtpkr_DOB.get_selectedDate();
             var CurrentDate = new Date();
             var timeDiff = Math.abs(CurrentDate.getTime() - date.getTime());
             var diffDays = Math.ceil(timeDiff / (1000 * 3600 * 24));
             if (dtpkr_DOB.isEmpty())
                 date = new Date();
             ntxt_Age.set_value(diffDays / 365);
         };


         $('document').ready(function () {

             $("#MainContent_txtDOB").datepicker({
                 numberOfMonths: 1

             });
         });
             $('document').ready(function () {

                 $("#MainContent_txtDateSinceCustomer").datepicker({
                     numberOfMonths: 1
                 });
             });
                 $('document').ready(function () {

                     $("#MainContent_dtpkr_CloseDate").datepicker({
                         numberOfMonths: 1
                     });
                 });

                 
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <form id="formEnquiry" runat="server">
        <div class="row">
            <div class="col-lg-12">
                <div style="float: right; margin-top: 12px;">
                </div>
                <div style="float: right; margin-top: 12px;">
                    <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="btnSave_Click" />
                    <a href="frmKYCList.aspx" class="btn btn-default">Back to List</a>
                </div>
                <h1 class="page-header">KYC Form</h1>
            </div>
        </div>
        <div class="row">
            <div class="col-lg-12">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        Enter Details
                    </div>
                    <div class="panel-heading" runat="server" id="DivID" visible="false">
                        <asp:Label ID="Label1" runat="server" Visible="False" align="center" Font-Bold="True" Font-Italic="False" Font-Names="Verdana" ForeColor="White"></asp:Label>
                    </div>
                    <!-- /.panel-heading -->
                    <div class="panel-body">
                        <!-- Nav tabs -->
                        <ul class="nav nav-tabs">
                            <li class="active"><a href="#home" data-toggle="tab">Personal Details</a></li>

                            <li><a href="#EMD" data-toggle="tab">KYC Details</a></li>

                            <li><a href="#EA" data-toggle="tab">Address</a> </li>

                            <%-- <li><a href="#EPD" data-toggle="tab">Education & Professional Details</a> </li>--%>
                            <%--<li><a href="#ED" data-toggle="tab">Documents</a></li>--%>
                        </ul>

                        <!-- Tab panes -->
                        <div class="tab-content">
                            <div class="tab-pane fade in active" id="home">
                                <h4>Personal Details</h4>

                                <div class="panel-body">
                                    <div class="row">
                                        <div class="form-group">
                                            <div class="col-md-4">
                                                <label>First name</label>
                                                <asp:Label ID="lblDid" runat="server" BackColor="White" ForeColor="White"></asp:Label>
                                                <asp:TextBox runat="server" ID="txtFirstName" style="text-transform:uppercase;" CssClass="form-control" required="required" AutoPostBack="True" OnTextChanged="txtFirstName_TextChanged"></asp:TextBox>
                                            </div>
                                            <div class="col-md-4">
                                                <label>Middle Name</label>
                                                <asp:TextBox runat="server" ID="txtMiddleName" style="text-transform:uppercase;" CssClass="form-control" AutoPostBack="True" OnTextChanged="txtMiddleName_TextChanged"></asp:TextBox>
                                            </div>
                                            <div class="col-md-4">
                                                <label>Last Name</label>
                                                <asp:TextBox runat="server" ID="txtLastName" style="text-transform:uppercase;" CssClass="form-control" requied="required" AutoPostBack="True" OnTextChanged="txtLastName_TextChanged"></asp:TextBox>
                                            </div>
                                            <div class="col-md-3">
                                                <label style="margin-right: 20PX;">Photo Upload</label>
                                                <asp:FileUpload runat="server" ID="ProjectFileUpload" OnLoad="ProjectFileUpload_Load" />
                                                <asp:Button ID="btnUpload" runat="server" Text="Upload" OnClick="btnUpload_Click" CausesValidation="False" />
                                                <asp:HiddenField ID="hiddenImgEmp" runat="server" />
                                            </div>
                                            <div class="col-md-3">
                                                <asp:Image ID="imgEmp" runat="server" Width="117" Height="117" CssClass="img-thumbnail" />

                                            </div>
                                            <div class="col-md-3">
                                                <label style="margin-right: 20PX;">Signature Upload</label>
                                                <asp:FileUpload runat="server" ID="FileSignatureUpload" />
                                                <asp:Button ID="btnUpload1" runat="server" Text="Upload" OnClick="btnUpload1_Click" CausesValidation="False" />
                                                <asp:HiddenField ID="hiddenImgsign" runat="server" />

                                            </div>
                                            <div class="col-md-3">
                                                <asp:Image ID="ImgSig" runat="server" Width="117" Height="117" CssClass="img-thumbnail" />
                                                

                                            </div>

                                            <div class="clearfix"></div>
                                        </div>
                                        <div class="clearfix"></div>
                                        <div class="form-group">
                                            <div class="col-md-12">
                                                <label>Full Name</label>
                                                <asp:TextBox runat="server" ID="txtFullName" style="text-transform:uppercase;" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                            </div>
                                            <div class="col-md-12">
                                                <label>Father/Husband Name</label>
                                                <asp:TextBox runat="server" ID="txtFatherHusName" style="text-transform:uppercase;" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="col-md-3">
                                                <label>Date Of Birth</label>
                                                <asp:TextBox runat="server" ID="txtDOB" CssClass="form-control" OnClick="CalculateAge" AutoPostBack="True" OnTextChanged="txtDOB_TextChanged"></asp:TextBox>
                                            </div>
                                            <div class="col-md-3">
                                                <label>Age</label>
                                                <asp:TextBox runat="server" ID="txtAge" CssClass="form-control"></asp:TextBox>
                                            </div>

                                            <div class="col-md-3">
                                                <label>Gender</label>
                                                <asp:DropDownList ID="cmbx_Gender" runat="server" CssClass="form-control">
                                                    <asp:ListItem Value="0">--SELECT--</asp:ListItem>
                                                    <asp:ListItem Value="1">MALE</asp:ListItem>
                                                    <asp:ListItem Value="1">FEMALE</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>

                                            <div class="col-md-3">
                                                <label>Religion</label>
                                                <asp:DropDownList ID="cmbx_Religion" runat="server" CssClass="form-control">
                                                    <asp:ListItem Value="0">--SELECT--</asp:ListItem>
                                                    <asp:ListItem Value="1">HINDU</asp:ListItem>
                                                    <asp:ListItem Value="2">MUSLIM</asp:ListItem>
                                                    <asp:ListItem Value="3">CHISTIAN</asp:ListItem>
                                                    <asp:ListItem Value="4">OTHER</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="clearfix"></div>
                                        </div>
                                        <div class="clearfix"></div>
                                        <div class="form-group">
                                            <div class="col-md-3">
                                                <label>Category</label>
                                                <asp:DropDownList ID="cmbx_Category" runat="server" CssClass="form-control">
                                                    <asp:ListItem Value="0">--SELECT CATEGORY--</asp:ListItem>
                                                    <asp:ListItem Value="1">AI/CO-OP SOCIETY</asp:ListItem>
                                                    <asp:ListItem Value="2">MEN</asp:ListItem>
                                                    <asp:ListItem Value="3">LCF</asp:ListItem>
                                                    <asp:ListItem Value="4">MIXED</asp:ListItem>
                                                    <asp:ListItem Value="5">OTHERS</asp:ListItem>
                                                    <asp:ListItem Value="6">WOMEN</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-3">
                                                <label>Spcial Category</label>
                                                <asp:DropDownList ID="cmbx_SpecialCategory" runat="server" CssClass="form-control">
                                                    <asp:ListItem Value="0">--SELECT--</asp:ListItem>
                                                    <asp:ListItem Value="1">GENERAL</asp:ListItem>
                                                    <asp:ListItem Value="2">MINORITY</asp:ListItem>
                                                    <asp:ListItem Value="3">OB CLASS</asp:ListItem>
                                                    <asp:ListItem Value="4">OTHERS</asp:ListItem>
                                                    <asp:ListItem Value="5">PHYSICALLY HANDICAPED</asp:ListItem>
                                                    <asp:ListItem Value="6">SC</asp:ListItem>
                                                    <asp:ListItem Value="7">ST</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-3">
                                                <label>Membership No</label>
                                                <asp:TextBox runat="server" ID="txt_MembershipNo" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="col-md-3">
                                                <label>Membership Status</label>
                                                <asp:DropDownList ID="cmbx_MembershipStatus" runat="server" CssClass="form-control">
                                                    <asp:ListItem Value="0">--SELECT--</asp:ListItem>
                                                    <asp:ListItem Value="1">Active</asp:ListItem>
                                                    <asp:ListItem Value="2">Close</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>

                                            <div class="clearfix"></div>
                                        </div>
                                        <div class="clearfix"></div>
                                        <div class="form-group">
                                            <div class="col-md-3">
                                                <label>Client Type</label>
                                                <asp:DropDownList ID="cmbx_ClientType" runat="server" CssClass="form-control">
                                                    <asp:ListItem Value="0">--SELECT--</asp:ListItem>
                                                    <asp:ListItem Value="1">APL</asp:ListItem>
                                                    <asp:ListItem Value="2">BPL</asp:ListItem>
                                                </asp:DropDownList>

                                            </div>
                                            <div class="col-md-3">
                                                <label>Client Status</label>
                                                <asp:DropDownList ID="cmbx_ClientStatus" runat="server" CssClass="form-control">
                                                    <asp:ListItem Value="0">--SELECT--</asp:ListItem>
                                                    <asp:ListItem Value="1">MEMBER</asp:ListItem>
                                                    <asp:ListItem Value="2">NON MEMBER</asp:ListItem>
                                                    <asp:ListItem Value="3">NOMINAL MEMBER</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-3">
                                                <label>Dt.Since Customer</label>
                                                <asp:TextBox runat="server" ID="txtDateSinceCustomer" CssClass="form-control"></asp:TextBox>
                                            </div>
                                             <div class="col-md-3">
                                                <label>Phone</label>
                                                <asp:TextBox runat="server" ID="txt_Telephone" CssClass="form-control"></asp:TextBox>
                                            </div>


                                            <div class="clearfix"></div>
                                        </div>
                                        <div class="clearfix"></div>
                                        <div class="form-group">
                                            <div class="col-md-3">
                                                <label>Close Date</label>
                                                <asp:TextBox runat="server" ID="dtpkr_CloseDate" CssClass="form-control"></asp:TextBox>

                                            </div>

                                            <div class="col-md-9">
                                                <label>Cause of Close</label>
                                                <asp:TextBox runat="server" ID="txt_CloseCause" style="text-transform:uppercase;" CssClass="form-control"></asp:TextBox>
                                            </div>

                                            <div class="clearfix"></div>
                                        </div>




                                    </div>
                                    <hr />
                                    <%--Start Grid--%>
                                    <h4>Education and Professional Details</h4>
                                    <div class="row">
                                        <div class="form-group">
                                            <div class="col-md-3">
                                                <label>Educational Qualification</label>

                                                <asp:DropDownList ID="cmbEducationalQualification" runat="server" CssClass="form-control">
                                                    <asp:ListItem Value="0">--SELECT--</asp:ListItem>
                                                    <asp:ListItem Value="1">MATRICULATION</asp:ListItem>
                                                    <asp:ListItem Value="2">HIGHER SECONDARY</asp:ListItem>
                                                    <asp:ListItem Value="3">GRADUATION</asp:ListItem>
                                                    <asp:ListItem Value="4">POST GRADUATE</asp:ListItem>
                                                </asp:DropDownList>

                                            </div>

                                            <div class="col-md-3">
                                                <label>Profession</label>
                                                <asp:DropDownList ID="cmbx_Profession" runat="server" CssClass="form-control">
                                                    <asp:ListItem Value="0">--SELECT--</asp:ListItem>
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
                                                <asp:TextBox runat="server" ID="ntxt_MonthlyIncome" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="col-md-3">
                                                <label>School certificate</label>
                                                <asp:TextBox runat="server" ID="txt_SchoolCer" style="text-transform:uppercase;" CssClass="form-control"></asp:TextBox>
                                            </div>

                                            <div class="clearfix"></div>
                                        </div>
                                    </div>
                                    <%--End Grid--%>
                                </div>

                            </div>
                            <div class="tab-pane fade" id="EMD">
                                <h4>KCC Details</h4>

                                <div class="panel-body">
                                    <div class="row">
                                        <div class="form-group">
                                            <div class="col-md-3">
                                                <label>KCC Card No</label>
                                                <asp:TextBox runat="server" ID="txt_KCCCardNo" style="text-transform:uppercase;" CssClass="form-control"></asp:TextBox>
                                            </div>

                                            <div class="col-md-3">
                                                <label>Land Less </label>
                                                <asp:DropDownList ID="cmbx_LandLess" runat="server" CssClass="form-control">
                                                    <asp:ListItem Value="0">--SELECT--</asp:ListItem>
                                                    <asp:ListItem Value="1">YES</asp:ListItem>
                                                    <asp:ListItem Value="2">NO</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-3">
                                                <label>Land Acre </label>
                                                <asp:TextBox runat="server" ID="txt_LandHolding" style="text-transform:uppercase;" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="col-md-3">
                                                <label>Adhar No </label>
                                                <asp:TextBox runat="server" ID="txt_AdharNo" style="text-transform:uppercase;" CssClass="form-control"></asp:TextBox>
                                            </div>

                                            <div class="clearfix"></div>
                                        </div>
                                        <div class="clearfix"></div>
                                        <div class="form-group">
                                            <div class="col-md-3">
                                                <label>Votar Card No </label>
                                                <asp:TextBox runat="server" ID="txt_Votar" style="text-transform:uppercase;" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="col-md-3">
                                                <label>Ration Card No</label>
                                                <asp:TextBox runat="server" ID="txt_RationCardNo" style="text-transform:uppercase;" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="col-md-3">
                                                <label>Passport No</label>
                                                <asp:TextBox runat="server" ID="txt_Passport" style="text-transform:uppercase;" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="col-md-3">
                                                <label>PAN Card No</label>
                                                <asp:TextBox runat="server" ID="txt_PAN" style="text-transform:uppercase;" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="clearfix"></div>
                                        </div>
                                        <div class="clearfix"></div>

                                        <div class="form-group">
                                            <div class="col-md-12">
                                                <label>BPL No</label>
                                                <asp:TextBox runat="server" ID="txt_BPLNo" style="text-transform:uppercase;" CssClass="form-control"></asp:TextBox>

                                            </div>

                                            <div class="clearfix"></div>
                                        </div>


                                        <div class="clearfix"></div>
                                    </div>
                                </div>

                            </div>
                            <div class="tab-pane fade" id="EA">
                                <h4>Address Details</h4>

                                <table width="100%" class="table table-striped table-bordered table-hover" id="Table1" runat="server">
                                    <thead>
                                        <tr>
                                            <th>Village</th>
                                            <th>Gram Panchayet(Muni)</th>
                                            <th>Post Office</th>
                                            <th>Police Station</th>
                                            <th>Sub Division</th>
                                            <th>Block</th>
                                            <th>District</th>

                                        </tr>
                                    </thead>
                                    <tbody id="Useraddress">
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txt_Village" runat="server" style="text-transform:uppercase;" CssClass="form-control" /></td>

                                            <td>
                                                <asp:TextBox ID="txt_Municipality" runat="server" style="text-transform:uppercase;" CssClass="form-control" /></td>
                                            <td>
                                                <asp:TextBox ID="txt_PostOffice" runat="server" style="text-transform:uppercase;" CssClass="form-control"/></td>
                                            <td>
                                                <asp:TextBox ID="txt_PoliceStation" runat="server" style="text-transform:uppercase;" CssClass="form-control" />
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txt_SubDivision" runat="server" style="text-transform:uppercase;" CssClass="form-control" />
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txt_Block" runat="server" style="text-transform:uppercase;" CssClass="form-control" />
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txt_District" runat="server" style="text-transform:uppercase;" CssClass="form-control" />
                                            </td>



                                        </tr>

                                    </tbody>
                                </table>


                            </div>




                        </div>

                        <div class="row">
                            <div class="col-lg-12">
                                <div style="float: right; margin-top: 12px;">
                                </div>
                                <div style="float: right; margin-top: 12px;">
                                    <asp:Button ID="btnSave1" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="btnSave_Click" />
                                    <a href="frmKYCList.aspx" class="btn btn-default">Back to List</a>
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
    </form>
</asp:Content>
