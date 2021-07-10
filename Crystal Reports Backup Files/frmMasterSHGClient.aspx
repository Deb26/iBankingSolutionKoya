<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/ProjectBM.Master" CodeBehind="frmMasterSHGClient.aspx.cs" Inherits="iBankingSolution.Master.frmMasterSHGClient" %>

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
                    <a href="frmSHGList.aspx" class="btn btn-default">Back to List</a>
                </div>
                <h1 class="page-header">SHG Client Entry</h1>
            </div>
            <!-- /.col-lg-12 -->
        </div>
        <div class="row">
            <div class="col-lg-12">
                <div class="panel panel-default">
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
                                    <label style="margin-right: 20PX;">Group Name</label>
                                     <asp:Label ID="lblDid" runat="server" BackColor="White" ForeColor="White"></asp:Label>
                                    <asp:TextBox ID="txt_GroupName" runat="server" CssClass="form-control" required="required" style="text-transform:uppercase;"></asp:TextBox>
                                </div>
                                <div class="col-md-12">
                                    <label style="margin-right: 20PX;">Leader's Name</label>
                                    <asp:TextBox ID="txt_LeadersName" runat="server" CssClass="form-control" required="required" style="text-transform:uppercase;"/>
                                </div>
                                <div class="col-md-12">
                                    <label style="margin-right: 20PX;">Asst. Leader's Name</label>
                                    <asp:TextBox ID="txt_AsstLeadersName" runat="server" CssClass="form-control" required="required" style="text-transform:uppercase;"/>
                                </div>

                                <div class="clearfix"></div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-3">
                                    <label style="margin-right: 20PX;">Gram Panchyat</label>
                                    <asp:TextBox ID="txt_GramPanchyat" runat="server" required="required" CssClass="form-control" style="text-transform:uppercase;"/>
                                </div>
                                <div class="col-md-3">
                                    <label style="margin-right: 20PX;">Block</label>
                                    <asp:TextBox ID="txt_Block" runat="server" required="required" CssClass="form-control" style="text-transform:uppercase;"/>
                                </div>
                                <div class="col-md-3">
                                    <label style="margin-right: 20PX;">Post Office</label>
                                    <asp:TextBox ID="txt_PostOffice" runat="server" required="required" CssClass="form-control" style="text-transform:uppercase;"/>
                                </div>
                                <div class="col-md-3">
                                    <label style="margin-right: 20PX;">Police Station</label>
                                    <asp:TextBox ID="txt_PoliceStation" runat="server" required="required" CssClass="form-control" style="text-transform:uppercase;"/>
                                </div>

                                <div class="clearfix"></div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-3">
                                    <label style="margin-right: 20PX;">District</label>

                                    <asp:TextBox ID="txt_District" runat="server" required="required" CssClass="form-control" style="text-transform:uppercase;"/>
                                </div>
                                <div class="col-md-3">
                                    <label style="margin-right: 20PX;">Sub Division</label>

                                    <asp:TextBox ID="txt_SubDivision" CssClass="form-control" runat="server" style="text-transform:uppercase;"></asp:TextBox>
                                </div>

                               <div class="col-md-3">
                                    <label style="margin-right: 20PX;">Client Status</label>

                                   <asp:DropDownList ID="cmbx_ClientStatus" runat="server" CssClass="form-control">
                                       <asp:ListItem Value="0">--SELECT--</asp:ListItem>
                                       <asp:ListItem Value="SHG">SHG</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-3">
                                    <label style="margin-right: 20PX;">Telephone No</label>

                                 <asp:TextBox ID="txt_Telephone" CssClass="form-control" runat="server" style="text-transform:uppercase;"></asp:TextBox>
                                </div>
                                 <div class="col-md-12">
                                    <label style="margin-right: 20PX;">Village</label>
                                    <asp:TextBox ID="txt_Village" runat="server" required="required" CssClass="form-control" style="text-transform:uppercase;"/>
                                </div>
                                <div class="clearfix"></div>
                            </div>

                            <div class="col-md-3">
                                    <label style="margin-right: 20PX;">Category</label>

                                   <asp:DropDownList ID="cmbx_Category" runat="server" CssClass="form-control">
                                       <asp:ListItem Value="0">--SELECT--</asp:ListItem>
                                       <asp:ListItem Value="AL">AL</asp:ListItem>
                                       <asp:ListItem Value="CO-OP SOCIETIES/INDIRECT">CO-OP SOCIETIES/INDIRECT</asp:ListItem>
                                       <asp:ListItem Value="MEN">MEN</asp:ListItem>
                                       <asp:ListItem Value="MF">MF</asp:ListItem>
                                       <asp:ListItem Value="MIXED">MIXED</asp:ListItem>
                                       <asp:ListItem Value="OTHERS">OTHERS</asp:ListItem>
                                       <asp:ListItem Value="SF">SF</asp:ListItem>
                                       <asp:ListItem Value="WOMEN">WOMEN</asp:ListItem>
                                   </asp:DropDownList>
                                </div>
                                <div class="col-md-3">
                                    <label style="margin-right: 20PX;">Special Category</label>

                                  <asp:DropDownList ID="CMBX_SpecialCategory" runat="server" CssClass="form-control">
                                      <asp:ListItem Value="0">--SELECT--</asp:ListItem>
                                       <asp:ListItem Value="CO-OP SOCIETIES/INDIRECT">CO-OP SOCIETIES/INDIRECT</asp:ListItem>
                                       <asp:ListItem Value="GENERAL">GENERAL</asp:ListItem>
                                       <asp:ListItem Value="MINORITY">MINORITY</asp:ListItem>
                                       <asp:ListItem Value="OB CLASS">OB CLASS</asp:ListItem>
                                       <asp:ListItem Value="OTHERS">OTHERS</asp:ListItem>
                                       <asp:ListItem Value="PHYSICALLY HANDICAPPED">PHYSICALLY HANDICAPPED</asp:ListItem>
                                       <asp:ListItem Value="SC">SC</asp:ListItem>
                                       <asp:ListItem Value="ST">ST</asp:ListItem>

                                  </asp:DropDownList>
                                </div>
                           <div class="form-group">
                                <div class="col-md-3">
                                    <label style="margin-right: 20PX;">Formed On</label>

                                     <asp:TextBox runat="server" ID="dtpkr_FormedOn" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-3">
                                    <label style="margin-right: 20PX;">No Of Members</label>

                                    <asp:TextBox ID="ntxt_NoOfMembers" CssClass="form-control" runat="server" AutoPostBack="True" OnTextChanged="ntxt_NoOfMembers_TextChanged"></asp:TextBox>
                                </div>

                                
                            <div class="form-group">

                               
                                <div class="col-md-3">
                                    <label style="margin-right: 20PX;">Photo Upload</label>
                                    <asp:FileUpload runat="server" ID="fu_Photo" />
                                    <asp:Button ID="btnUpload" runat="server" Text="Upload" CausesValidation="False" OnClick="btnUpload_Click" />
                                     <asp:HiddenField ID="hiddenImgEmp" runat="server" />
                                </div>
                                <div class="col-md-3">
                                    <asp:Image ID="imgPhoto" runat="server" Width="117" Height="117" CssClass="img-thumbnail" />

                                </div>

                                <div class="col-md-3">
                                    <label style="margin-right: 20PX;">Signature Upload</label>
                                    <asp:FileUpload runat="server" ID="fu_Signature" />
                                    <asp:Button ID="btnUpload1" runat="server" Text="Upload" OnClick="btnUpload1_Click" CausesValidation="False" />
                                     <asp:HiddenField ID="hiddenImgsign" runat="server" />
                                </div>
                                <div class="col-md-3">
                                    <asp:Image ID="ImgSig" runat="server" Width="117" Height="117" CssClass="img-thumbnail" />

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
        <div class="row">
            <div class="col-lg-12">
                <div class="panel panel-default">
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
                                                            
                                                            <asp:Label ID="lbl_SlNo" Text='<%# Bind("SlNo") %>' runat="server"></asp:Label>  <%--<%# Container.DataItemIndex + 1 %>--%>
                                                                                       
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Name">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txt_Name" CssClass="form-control input-sm"  runat="server"  
                                                                autocomplete="off" ForeColor="Black" Text='<%# Eval("SHG_NAME") %>' onFocus="this.select()"></asp:TextBox>
                                                            <%--Text='<%# Eval("Description") %>'--%>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Gender" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="cmbx_Gender" runat="server" Width="100%" placeholder="Enter Sex">
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
                                                            <asp:TextBox ID="txt_Age" CssClass="form-control input-sm"   runat="server" required=""
                                                                autocomplete="off" ForeColor="Black" Text='<%# Eval("SHG_AGE") %>' onFocus="this.select()"></asp:TextBox>
                                                            <%--Text='<%# Eval("txtCashInHand") %>'--%>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>

                                                     <asp:TemplateField HeaderText="Join Date" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="dtpkr_JoinDate" CssClass="form-control input-sm BootDatepicker" runat="server"  
                                                                autocomplete="off" ForeColor="Black" Text='<%# Eval("SHG_JOIN_DT") %>' onFocus="this.select()"></asp:TextBox>
                                                        
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Cast" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                           <asp:DropDownList ID="cmbx_Cast" runat="server" Width="100%" placeholder="Enter Sex">
                                                                <asp:ListItem Value="0">--SELECT--</asp:ListItem>
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
                                                            <asp:TextBox ID="txt_No" CssClass="form-control input-sm"   runat="server"  
                                                                autocomplete="off" ForeColor="Black" Text='<%# Eval("SHG_TYPE_NO") %>' onFocus="this.select()"></asp:TextBox>
                                                         
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="PAN No" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txt_PANNo" CssClass="form-control input-sm"   runat="server"  
                                                                autocomplete="off" ForeColor="Black" Text='<%# Eval("PAN_CARD_NO") %>' onFocus="this.select()"></asp:TextBox>
                                                            <%--Text='<%# Eval("Description") %>'--%>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                          <asp:TemplateField HeaderText="Adhar No" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txt_AADHARNo" CssClass="form-control input-sm"   runat="server"  
                                                                autocomplete="off" ForeColor="Black" Text='<%# Eval("AADHAR_NO") %>' onFocus="this.select()"></asp:TextBox>
                                                            <%--Text='<%# Eval("Description") %>'--%>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle HorizontalAlign="Left" />
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

    </form>
</asp:Content>
