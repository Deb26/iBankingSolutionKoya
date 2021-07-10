<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/ProjectBM.Master" CodeBehind="frmMasterSHGClient.aspx.cs" Inherits="iBankingSolution.Master.frmMasterSHGClient" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <%-- <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.4.1/js/bootstrap-datepicker.min.js"></script>
         <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.4.1/css/bootstrap-datepicker3.css"/>--%>
     <style>
            #load {
            width : 80%;
            height : 80%;
            position : fixed;
            z-index : 9999;
            background : url("circle.gif") no-repeat center /*center rgba(255,255,255,255)*/
           }
     </style>
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
        }


    </script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <div id="load"></div>
        <asp:ScriptManager ID="scrpt1" runat="server"></asp:ScriptManager>
        <div class="row">
            <div class="col-lg-12">
                <div style="float: right; margin-top: 12px;">
                    <asp:Button ID="btnsubmit1" runat="server" Text="Save" class="btn btn-primary" OnClick="btnsubmit_Click" />

                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" class="btn btn-primary" OnClick="btnCancel_Click" />
                    <a href="frmSHGList.aspx" class="btn btn-default">Back to List</a>
                </div>
                <h1 class="page-header">SHG Client Entry</h1>
            </div>
            <!-- /.col-lg-12 -->
        </div>
        <div class="row">
            <div class="col-lg-12">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        Entry
                    </div>
                    <div class="panel-heading" runat="server" id="DivID" visible="false">
                        <asp:Label ID="Label1" runat="server" Visible="False" align="center" Font-Bold="True" Font-Italic="False" Font-Names="Verdana" ForeColor="White"></asp:Label>
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="form-group">

                                <div class="col-md-12">
                                    <asp:Label ID="Label2" runat="server" Text="*" Style="color: #FF3300"></asp:Label>
                                    <label style="margin-right: 20PX;">Group Name</label>
                                    <asp:Label ID="lblDid" runat="server" BackColor="White" ForeColor="White"></asp:Label>
                                    <asp:TextBox ID="txt_GroupName" runat="server" placeholder="ENTER GROUP NAME" Font-Size="10" CssClass="form-control" required="required" Style="text-transform: uppercase;"></asp:TextBox>
                                </div>
                                <div class="clearfix"></div>
                                <br />
                                <div class="col-md-12">
                                    
                                    <asp:Label ID="Label3" runat="server" Text="*" Style="color: #FF3300"></asp:Label>
                                    <label style="margin-right: 20PX;">Leader's Name</label>
                                    <asp:TextBox ID="txt_LeadersName" runat="server" placeholder="ENTER LEADER NAME" Font-Size="10" CssClass="form-control" required="required" Style="text-transform: uppercase;" />
                                </div>
                                <div class="clearfix"></div>
                                <br />
                                <div class="col-md-12">
                                    
                                    <label style="margin-right: 20PX;">Asst. Leader's Name</label>
                                    <asp:TextBox ID="txt_AsstLeadersName" runat="server" placeholder="Asst. Leader's Name" Font-Size="10" CssClass="form-control"  Style="text-transform: uppercase;" />
                                </div>

                                <div class="clearfix"></div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-3">
                                    <asp:Label ID="Label4" runat="server" Text="*" Style="color: #FF3300"></asp:Label>
                                    <label style="margin-right: 20PX;">Gram Panchyat</label>
                                    <asp:TextBox ID="txt_GramPanchyat" runat="server" placeholder="ENTER GRAM PANCHAYAT" Font-Size="10" CssClass="form-control"  Style="text-transform: uppercase;" />
                                </div>
                                <div class="col-md-3">
                                    <label style="margin-right: 20PX;">Block</label>
                                    <asp:TextBox ID="txt_Block" runat="server" placeholder="ENTER BLOCK" CssClass="form-control" Font-Size="10" Style="text-transform: uppercase;" />
                                </div>
                                <div class="col-md-3">
                                    <asp:Label ID="Label5" runat="server" Text="*" Style="color: #FF3300"></asp:Label>
                                    <label style="margin-right: 20PX;">Post Office</label>
                                    <asp:TextBox ID="txt_PostOffice" runat="server" placeholder="ENTER POST OFFICE" Font-Size="10" CssClass="form-control"  Style="text-transform: uppercase;" />
                                </div>
                                <div class="col-md-3">
                                    <label style="margin-right: 20PX;">Police Station</label>
                                    <asp:TextBox ID="txt_PoliceStation" runat="server" placeholder="ENTER POLICE STATION" Font-Size="10" CssClass="form-control" Style="text-transform: uppercase;" />
                                </div>

                                <div class="clearfix"></div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-3">
                                    <asp:Label ID="Label9" runat="server" Text="*" Style="color: #FF3300"></asp:Label>
                                    <label style="margin-right: 20PX;">District</label>

                                    <asp:TextBox ID="txt_District" runat="server" placeholder="ENTER DIST NAME" Font-Size="10"  CssClass="form-control" Style="text-transform: uppercase;" />
                                </div>
                                <div class="col-md-3">
                                    <asp:Label ID="Label10" runat="server" Text="*" Style="color: #FF3300"></asp:Label>
                                    <label style="margin-right: 20PX;">Sub Division</label>

                                    <asp:TextBox ID="txt_SubDivision" CssClass="form-control" placeholder="ENTER SUB DIVISSION" Font-Size="10"  runat="server" Style="text-transform: uppercase;"></asp:TextBox>
                                </div>

                                <div class="col-md-3">
                                    <asp:Label ID="Label11" runat="server" Text="*" Style="color: #FF3300"></asp:Label>
                                    <label style="margin-right: 20PX;">Client Status</label>

                                    <asp:DropDownList ID="cmbx_ClientStatus" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="0">--SELECT CLIENT STATUS--</asp:ListItem>
                                        <asp:ListItem Value="SHG">SHG</asp:ListItem>
                                    </asp:DropDownList>
                                    <%--<asp:RequiredFieldValidator ID="ClientStatus" runat="server" ControlToValidate="cmbx_SpecialCategory"
                                        InitialValue="0" ErrorMessage="Please Select Item"></asp:RequiredFieldValidator>--%>
                                </div>
                                <div class="col-md-3">
                                    <asp:Label ID="Label12" runat="server" Text="*" Style="color: #FF3300"></asp:Label>
                                    <label style="margin-right: 20PX;">Telephone No</label>

                                    <asp:TextBox ID="txt_Telephone" placeholder="XX-XXX-XXX-XX" Font-Size="10" onkeypress="return isNumberKey(event)"  CssClass="form-control" runat="server" Style="text-transform: uppercase;" MaxLength="10"></asp:TextBox>
                                </div>
                                <div class="col-md-12">
                                    <asp:Label ID="Label6" runat="server" Text="*" Style="color: #FF3300"></asp:Label>
                                    <label style="margin-right: 20PX;">Village</label>
                                    <asp:TextBox ID="txt_Village" runat="server" placeholder="ENTER VILLAGE NAME" Font-Size="10" CssClass="form-control"  Style="text-transform: uppercase;" />
                                </div>
                                <div class="clearfix"></div>
                            </div>

                            <div class="col-md-3">
                                <label style="margin-right: 20PX;">Category</label>

                                <asp:DropDownList ID="cmbx_Category" runat="server" CssClass="form-control" Font-Size="10">
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
                                <label style="margin-right: 20PX;">Special Category</label>

                                <asp:DropDownList ID="CMBX_SpecialCategory" runat="server" CssClass="form-control" Font-Size="10">
                                    <asp:ListItem Value="0">--SPECIAL CATEGORY--</asp:ListItem>
                                    <asp:ListItem Value="1">GENERAL</asp:ListItem>
                                    <asp:ListItem Value="2">MINORITY</asp:ListItem>
                                    <asp:ListItem Value="3">OB CLASS</asp:ListItem>
                                    <asp:ListItem Value="4">OTHERS</asp:ListItem>
                                    <asp:ListItem Value="5">PHYSICALLY HANDICAPED</asp:ListItem>
                                    <asp:ListItem Value="6">SC</asp:ListItem>
                                    <asp:ListItem Value="7">ST</asp:ListItem>

                                </asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <div class="col-md-3">
                                    <asp:Label ID="Label7" runat="server" Text="*" Style="color: #FF3300"></asp:Label>
                                    <label style="margin-right: 20PX;">Formed On</label>

                                    <asp:TextBox runat="server" placeholder="FORMED ON" ID="dtpkr_FormedOn" Font-Size="10" CssClass="form-control" ></asp:TextBox>
                                </div>
                                <div class="col-md-3">
                                    <asp:Label ID="Label8" runat="server" Text="*" Style="color: #FF3300"></asp:Label>
                                    <label style="margin-right: 20PX;">No Of Members</label>

                                    <asp:TextBox ID="ntxt_NoOfMembers" placeholder="ENTER TEAM SIZE" Font-Size="10" MaxLength="2"  onkeypress="return isNumberKey(event)" CssClass="form-control" runat="server" AutoPostBack="True" OnTextChanged="ntxt_NoOfMembers_TextChanged"></asp:TextBox>
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
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        SHG Member Details
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="form-group">
                                <div class="col-md-12">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <asp:GridView ID="gv_SHGMemberDetails" runat="server" AutoGenerateColumns="False"
                                                CssClass="table table-bordered table-striped table-hover" EmptyDataText="No Data Found" GridLines="Both" OnRowDataBound="gv_SHGMemberDetails_RowDataBound">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="SL. NO" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>

                                                            <asp:Label ID="lbl_SlNo" Text='<%# Bind("SlNo") %>' runat="server"></asp:Label>
                                                            <%--<%# Container.DataItemIndex + 1 %>--%>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Name">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txt_Name" CssClass="form-control input-sm" runat="server" Font-Size="10" placeholder="ENTER NAME" style="text-transform: uppercase;"
                                                                autocomplete="off" ForeColor="Black" Text='<%# Eval("SHG_NAME") %>' onFocus="this.select()"></asp:TextBox>
                                                            <%--Text='<%# Eval("Description") %>'--%>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Gender" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="cmbx_Gender" runat="server" Width="100%" placeholder="Enter Sex" Font-Size="10">
                                                                <asp:ListItem Value="0">--SELECT--</asp:ListItem>
                                                                <asp:ListItem Value="Male">Male</asp:ListItem>
                                                                <asp:ListItem Value="Female">Female</asp:ListItem>

                                                            </asp:DropDownList>

                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Age" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txt_Age" CssClass="form-control input-sm" runat="server" required="" Font-Size="10" placeholder="ENTER AGE"
                                                                autocomplete="off" ForeColor="Black" Text='<%# Eval("SHG_AGE") %>' onFocus="this.select()"></asp:TextBox>
                                                            <%--Text='<%# Eval("txtCashInHand") %>'--%>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Join Date" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="dtpkr_JoinDate" CssClass="form-control input-sm BootDatepicker" runat="server" Font-Size="10" placeholder="dd/MM/yyyy"
                                                                autocomplete="off" ForeColor="Black" Text='<%# Eval("SHG_JOIN_DT","{0:dd/MM/yyyy}") %>' onFocus="this.select()"></asp:TextBox>

                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Cast" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="cmbx_Cast" runat="server" Width="100%" placeholder="Enter Sex" Font-Size="10">
                                                                <asp:ListItem Value="0">--SELECT CAST--</asp:ListItem>
                                                                <asp:ListItem Value="APL">APL</asp:ListItem>
                                                                <asp:ListItem Value="BPL">BPL</asp:ListItem>
                                                                <asp:ListItem Value="SC">SC</asp:ListItem>
                                                                <asp:ListItem Value="ST">ST</asp:ListItem>
                                                                <asp:ListItem Value="OBC">OBC</asp:ListItem>
                                                                <asp:ListItem Value="Minority">Minority</asp:ListItem>
                                                                <asp:ListItem Value="General">General</asp:ListItem>


                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="No" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txt_No" CssClass="form-control input-sm" runat="server" Font-Size="10" placeholder="ENTER NO" style="text-transform: uppercase;"
                                                                autocomplete="off" ForeColor="Black" Text='<%# Eval("SHG_TYPE_NO") %>' onFocus="this.select()"></asp:TextBox>

                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="PAN No" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txt_PANNo" CssClass="form-control input-sm" runat="server" Font-Size="10" placeholder="ENTER PAN NO" style="text-transform: uppercase;"
                                                                autocomplete="off" ForeColor="Black" Text='<%# Eval("PAN_CARD_NO") %>' onFocus="this.select()"></asp:TextBox>
                                                            <%--Text='<%# Eval("Description") %>'--%>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Adhar No" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txt_AADHARNo" CssClass="form-control input-sm" runat="server" Font-Size="10" MaxLength="12" placeholder="ENTER AADHAAR NO" style="text-transform: uppercase;"
                                                                autocomplete="off" ForeColor="Black" Text='<%# Eval("AADHAR_NO") %>' onFocus="this.select()"></asp:TextBox>
                                                            <%--Text='<%# Eval("Description") %>'--%>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="50px" HeaderText="ACTION">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="itbnNew" runat="server" CausesValidation="false" Height="18"
                                                                ImageUrl="~/Content/images/add.png" Width="18" OnClick="itNew_Click" />
                                                            <asp:ImageButton ID="itbndelete" runat="server" CausesValidation="false"
                                                                CommandName="Delete" Height="18" ImageUrl="~/Content/images/delete.png" Width="18" OnClick="itbndelete_Click" Visible="false" />
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" Width="50px" />
                                                    </asp:TemplateField>

                                                </Columns>
                                            </asp:GridView>
                                        </ContentTemplate>
                                        <%--  <Triggers>
                                            <asp:PostBackTrigger ControlID="btnsubmit" />
                                        </Triggers>--%>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="clearfix"></div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="clearfix"></div>



        <div class="row">

            <div class="col-lg-12">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        Picture and Sign
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="form-group">
                                <div class="col-md-3">
                                    <label style="margin-right: 20PX;">Photo Upload</label>
                                    <asp:FileUpload runat="server" ID="fu_Photo"  onchange="PreviewPhoto(this)"/>
                                    <asp:Button ID="btnUpload" runat="server" Text="Upload" CausesValidation="False" OnClick="btnUpload_Click" Visible="False" />
                                    <asp:HiddenField ID="hiddenImgEmp" runat="server" />
                                </div>
                                <div class="col-md-3" id="div_Photo">
                                    <asp:Image ID="imgPhoto" runat="server" Width="117" Height="117" CssClass="img-thumbnail" ImageUrl="~/Images/noImage.jpg" />

                                </div>

                                <div class="col-md-3">
                                    <label style="margin-right: 20PX;">Signature Upload</label>
                                    <asp:FileUpload runat="server" ID="fu_Signature" onchange="Previewsignature(this)"/>
                                    <asp:Button ID="btnUpload1" runat="server" Text="Upload" OnClick="btnUpload1_Click" CausesValidation="False" Visible="False" />
                                    <asp:HiddenField ID="hiddenImgsign" runat="server" />
                                </div>
                                <div class="col-md-3" id="div_Sign">
                                    <asp:Image ID="ImgSig" runat="server" Width="117" Height="117" CssClass="img-thumbnail" ImageUrl="~/Images/No_Signatures.png" />

                                </div>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </div>

        <div class="clearfix"></div>

        <div class="row">
            <div class="col-lg-12">
                <div style="float: right; margin-top: 12px;">
                    <asp:Button ID="btnsubmit" runat="server" Text="Save" class="btn btn-primary" OnClick="btnsubmit_Click" />
                    <a href="frmMasterSHGClient.aspx" class="btn btn-outline btn-danger">Cancel</a>

                </div>
            </div>
            <!-- /.col-lg-12 -->
        </div>

        <%-- Scripting Section --%>

        <script type="text/javascript">

            $('document').ready(function () {

                $("#MainContent_dtpkr_FormedOn").datepicker({
                    numberOfMonths: 1

                });
            });
            //$('document').ready(function () {

            //    $("#MainContent_dtpkr_JoinDate").datepicker({
            //        numberOfMonths: 1
            //    });
            //});
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

            //$('document').ready(function () {

            //    $("#MainContent_dtpkr_FormedOn").datepicker({
            //        numberOfMonths: 1,
            //        onSelect: function (selected) {
            //            $("#MainContent_dtpkr_FormedOn").datepicker("option", "maxDate", selected)
            //        }
            //    });
            //});

            //$('document').ready(function () {

            //    $("#MainContent_dtpkr_JoinDate").datepicker({
            //        numberOfMonths: 1,
            //        onSelect: function (selected) {
            //            $("#MainContent_dtpkr_JoinDate").datepicker("option", "maxDate", selected)
            //        }
            //    });
            //});



        </script>

     
</asp:Content>
