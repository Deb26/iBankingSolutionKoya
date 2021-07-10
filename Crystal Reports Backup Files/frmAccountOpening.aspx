<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/ProjectBM.Master" CodeBehind="frmAccountOpening.aspx.cs" Inherits="iBankingSolution.Transaction.frmAccountOpening" %>

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
                    <a href="frmAccountOpeningList.aspx" class="btn btn-default">Back to List</a>
                </div>
                <h1 class="page-header">Account Opening</h1>
            </div>
            <!-- /.col-lg-12 -->
        </div>
        <div class="row">
            <div class="col-lg-12">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        Account Opening Entry
                    </div>
                    <div class="panel-heading" runat="server" id="DivID" visible="false">
                        <asp:Label ID="Label1" runat="server" Visible="False" align="center" Font-Bold="True" Font-Italic="False" Font-Names="Verdana" ForeColor="White"></asp:Label>
                    </div>
                    <div class="panel-body">
                        <div class="row">

                            <div class="form-group">

                                <div class="col-md-4">
                                    <label style="margin-right: 20PX;">Account Type</label><asp:Label ID="lblDid" runat="server" BackColor="White" ForeColor="White"></asp:Label>
                                    <asp:DropDownList ID="cmbx_AcctType" runat="server" CssClass="form-control" required="required" AutoPostBack="True" OnSelectedIndexChanged="cmbx_AcctType_SelectedIndexChanged">

                                        <asp:ListItem Value="0">--Select Type--</asp:ListItem>
                                        <asp:ListItem Value="s">Savings</asp:ListItem>
                                        <asp:ListItem Value="cc">Deposit Certificate</asp:ListItem>
                                        <asp:ListItem Value="fd">Fixed Deposite</asp:ListItem>
                                        <asp:ListItem Value="r">Recurring Deposite</asp:ListItem>
                                        <asp:ListItem Value="d">Home Savings</asp:ListItem>
                                        <asp:ListItem Value="jlg">JLG Deposite</asp:ListItem>
                                        <asp:ListItem Value="mis">MIS Deposite</asp:ListItem>
                                        <asp:ListItem Value="sus">Suspense Deposite</asp:ListItem>
                                        <asp:ListItem Value="nf">No Frill Deposite</asp:ListItem>



                                    </asp:DropDownList>
                                    
                                </div>
                                <div class="col-md-4">
                                    <label style="margin-right: 20PX;">Date of Open</label>
                                    <asp:TextBox ID="dtpkr_dateopen" CssClass="form-control input-sm BootDatepicker" runat="server" onFocus="this.select()" autocomplete="off" required="required"></asp:TextBox>
                                </div>
                                <div class="col-md-4">
                                    <label style="margin-right: 20PX;">Old Ac.No</label>
                                    <asp:TextBox ID="txt_OldACNo" CssClass="form-control" runat="server" onFocus="this.select()" autocomplete="off"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                    <label style="margin-right: 20PX;">Deposit Scheme</label>
                                    <asp:DropDownList ID="cmbx_DepositScheme" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                                <div class="col-md-6">
                                    <label style="margin-right: 20PX;">Category</label>
                                    <asp:DropDownList ID="cmbx_Category" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                                        <asp:ListItem Value="Individual" Selected="True">Individual</asp:ListItem>
                                        <asp:ListItem Value="Jointly">Jointly</asp:ListItem>
                                        <asp:ListItem Value="Minor">Minor</asp:ListItem>

                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-2">
                                    <label style="margin-right: 20PX;">Total Applicant</label>
                                    <asp:TextBox ID="ntxt_TotApplicant" runat="server" onFocus="this.select()" CssClass="form-control" autocomplete="off" AutoPostBack="True" OnTextChanged="ntxt_TotApplicant_TextChanged" Width="73px" MaxLength="3" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                    <span id="errmsg"></span>
                                     



                                </div>
                                <div class="col-md-12">
                                    <%-- Applicants Details --%> Applicants Details
                                    <asp:GridView ID="rgv_ClientKYC" runat="server" AutoGenerateColumns="False"
                                        CssClass="table table-bordered table-striped table-hover" EmptyDataText="No Data Found" GridLines="Both" Enabled="false">
                                        <Columns>
                                            <asp:TemplateField HeaderText="SL" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_SlNo" Text='<%# Bind("SlNo") %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Cust ID">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="CUSTCodeTextBox" CssClass="form-control input-sm" runat="server"
                                                        autocomplete="off" ForeColor="Black" Text='<%# Eval("CUST_ID") %>' onFocus="this.select()" OnTextChanged="CUSTCodeTextBox_TextChanged" AutoPostBack="True"></asp:TextBox>

                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Name">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtName" CssClass="form-control input-sm" runat="server"
                                                        autocomplete="off" ForeColor="Black" Text='<%# Eval("Name") %>' onFocus="this.select()"></asp:TextBox>

                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Guardian Name">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtGuardianName" CssClass="form-control input-sm" runat="server"
                                                        autocomplete="off" ForeColor="Black" Text='<%# Eval("GUARDIAN_NAME") %>' onFocus="this.select()"></asp:TextBox>

                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="PO">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtPO" CssClass="form-control input-sm" runat="server"
                                                        autocomplete="off" ForeColor="Black" Text='<%# Eval("PO_CODE") %>' onFocus="this.select()"></asp:TextBox>

                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="PS">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtPS" CssClass="form-control input-sm" runat="server"
                                                        autocomplete="off" ForeColor="Black" Text='<%# Eval("PS_CODE") %>' onFocus="this.select()"></asp:TextBox>

                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Block">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtblock" CssClass="form-control input-sm" runat="server"
                                                        autocomplete="off" ForeColor="Black" Text='<%# Eval("BLK_CODE") %>' onFocus="this.select()"></asp:TextBox>

                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Village">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtvillage" CssClass="form-control input-sm" runat="server"
                                                        autocomplete="off" ForeColor="Black" Text='<%# Eval("VILL_CODE") %>' onFocus="this.select()"></asp:TextBox>

                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="District" Visible="false">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtDistrict" CssClass="form-control input-sm" runat="server"
                                                        autocomplete="off" ForeColor="Black" Text='<%# Eval("DIS_CODE") %>' onFocus="this.select()"></asp:TextBox>

                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Sex">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtSex" CssClass="form-control input-sm" runat="server"
                                                        autocomplete="off" ForeColor="Black" Text='<%# Eval("SEX") %>' onFocus="this.select()"></asp:TextBox>

                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Religion" Visible="false">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtReligion" CssClass="form-control input-sm" runat="server"
                                                        autocomplete="off" ForeColor="Black" Text='<%# Eval("REL_CODE") %>' onFocus="this.select()"></asp:TextBox>

                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Profession" Visible="false">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtprofession" CssClass="form-control input-sm" runat="server"
                                                        autocomplete="off" ForeColor="Black" Text='<%# Eval("PROF_CODE") %>' onFocus="this.select()"></asp:TextBox>

                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    <%--Applicants Details close--%>
                                </div>
                                <div class="clearfix"></div>

                            </div>
                            <div class="form-group">
                                <div class="col-md-3">
                                    <label style="margin-right: 20PX;">Int Type</label>
                                    <asp:DropDownList ID="cmbx_IntType" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                                        <asp:ListItem Value="Normal" Selected="True">Normal</asp:ListItem>
                                        <asp:ListItem Value="Sr. Cirtizen">Sr. Cirtizen</asp:ListItem>
                                        <asp:ListItem Value="DBS">DBS</asp:ListItem>

                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-3">
                                    <label style="margin-right: 20PX;">Period in Month+days</label><br />
                                    <asp:TextBox ID="txt_PeriodsinMonth" runat="server" Width="116px" Height="35" OnTextChanged="txt_PeriodsinMonth_TextChanged" onkeypress="return isNumberKey(event)" AutoPostBack="True" MaxLength="3" />
                                    <asp:TextBox ID="txt_PeriodsInDays" Width="116px" Height="35" runat="server" AutoPostBack="True" OnTextChanged="txt_PeriodsInDays_TextChanged" onkeypress="return isNumberKey(event)" MaxLength="3" />
                                </div>
                                <div class="col-md-3">
                                    <label style="margin-right: 20PX;">ROI</label>
                                    <asp:TextBox ID="ntxt_ROI" runat="server" CssClass="form-control" AutoPostBack="True" required="required" />
                                </div>
                                <div class="col-md-3">
                                    <label style="margin-right: 20PX;">Deposit Amount</label>
                                    <asp:TextBox ID="ntxt_DepositAmt" runat="server" CssClass="form-control" onkeypress="return isNumberKey(event)" AutoPostBack="True" OnTextChanged="ntxt_DepositAmt_TextChanged" required="required" />
                                </div>

                                <div class="clearfix"></div>
                            </div>

                            <div class="form-group">
                                <div class="col-md-4">
                                    <label style="margin-right: 20PX;">Date of Maturity</label>
                                    <asp:TextBox ID="dtpkr_MaturityDate" CssClass="form-control input-sm BootDatepicker" runat="server" onFocus="this.select()" autocomplete="off" required="required" Enabled="False"></asp:TextBox>
                                </div>
                                <div class="col-md-4">
                                    <label style="margin-right: 20PX;">Maturity Amount</label>
                                    <asp:TextBox ID="ntxt_MaturityAmt" runat="server" CssClass="form-control" Enabled="False" />

                                </div>
                                <div class="col-md-4">
                                    <label style="margin-right: 20PX;">Int Transfer To</label>
                                    <asp:DropDownList ID="cmbx_IntTransferredTo" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>


                                <div class="clearfix"></div>
                            </div>



                            <div class="form-group">
                                <div class="col-md-6">
                                    <label style="margin-right: 20PX;">Operation</label>
                                    <asp:DropDownList ID="cmbx_Operation" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                                        <asp:ListItem Value="Singly" Selected="True">Singly</asp:ListItem>
                                        <asp:ListItem Value="Either Or Survivor">Either Or Survivor</asp:ListItem>
                                        <asp:ListItem Value="Former Of Survivor">Former Of Survivor</asp:ListItem>
                                        <asp:ListItem Value="Jointly Of Survivor">Jointly Of Survivor</asp:ListItem>

                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-6">
                                    <label style="margin-right: 20PX;">Introducer A/C No</label>
                                    <asp:DropDownList ID="cmbx_IntroAcNo" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="cmbx_IntroAcNo_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                                <div class="col-md-4">
                                    <label style="margin-right: 20PX;">Introducer Name</label>
                                    <asp:TextBox ID="txt_IntroName" runat="server" CssClass="form-control" />
                                </div>
                                <div class="col-md-4">
                                    <label style="margin-right: 20PX;">Introducer Address</label>
                                    <asp:TextBox ID="txt_IntroAddress" runat="server" CssClass="form-control" />
                                </div>
                                <div class="col-md-4">
                                    <label style="margin-right: 20PX;">Introducer Phone</label>
                                    <asp:TextBox ID="txt_IntroPhone" runat="server" CssClass="form-control" MaxLength="10" onkeypress="return isNumberKey(event)" />
                                </div>
                                <div class="clearfix"></div>
                            </div>

                            <div class="form-group">
                                <div class="col-md-4">
                                    <%-- Nominee Details --%> Nominee Details<br />
                                    <label style="margin-right: 20PX;">Enter No.Of Nominee</label>
                                    <asp:TextBox ID="txtNoofNominee" runat="server" CssClass="form-control" AutoPostBack="True" OnTextChanged="txtNoofNominee_TextChanged" /><br />
                                </div>

                                <asp:GridView ID="GVNominee" runat="server" AutoGenerateColumns="False"
                                    CssClass="table table-bordered table-striped table-hover" EmptyDataText="No Data Found" GridLines="Both">
                                    <Columns>
                                        <asp:TemplateField HeaderText="SL" HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_SlNo" Text='<%# Bind("SlNo") %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Name">
                                            <ItemTemplate>
                                                <asp:TextBox ID="nomn_NameTextBox" CssClass="form-control input-sm" runat="server"
                                                    autocomplete="off" ForeColor="Black" Text='<%# Eval("nomn_name") %>' onFocus="this.select()"></asp:TextBox>

                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Address">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtnomnaddress" CssClass="form-control input-sm" runat="server"
                                                    autocomplete="off" ForeColor="Black" Text='<%# Eval("nomn_address") %>' onFocus="this.select()"></asp:TextBox>

                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Relation">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="cmbx_nom_Relation" runat="server" CssClass="form-control input-sm">
                                                    <asp:ListItem Value="0">Father</asp:ListItem>
                                                    <asp:ListItem Value="1">Mother</asp:ListItem>
                                                    <asp:ListItem Value="2">Husband</asp:ListItem>
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>

                            </div>
                            <div class="form-group">
                                <%-- Introducer Details --%> Athorised Signatory Details
                                 <asp:GridView ID="GVIntroducer" runat="server" AutoGenerateColumns="False"
                                     CssClass="table table-bordered table-striped table-hover" EmptyDataText="No Data Found" GridLines="Both">
                                     <Columns>
                                         <asp:TemplateField HeaderText="SL" HeaderStyle-HorizontalAlign="Left">
                                             <ItemTemplate>
                                                 <asp:Label ID="lbl_SlNo" Text='<%# Bind("SlNo") %>' runat="server"></asp:Label>
                                             </ItemTemplate>
                                             <HeaderStyle HorizontalAlign="Left" />
                                         </asp:TemplateField>
                                         <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Name">
                                             <ItemTemplate>
                                                 <asp:DropDownList ID="cmbx_AuthName" runat="server" CssClass="form-control input-sm">
                                                 </asp:DropDownList>
                                             </ItemTemplate>
                                             <ItemStyle HorizontalAlign="Left" />
                                             <HeaderStyle HorizontalAlign="Left" />
                                         </asp:TemplateField>

                                         <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Designation">
                                             <ItemTemplate>
                                                 <asp:TextBox ID="DesignationTextBox" CssClass="form-control input-sm" runat="server"
                                                     autocomplete="off" ForeColor="Black" Text='<%# Eval("Designation") %>' onFocus="this.select()"></asp:TextBox>

                                             </ItemTemplate>
                                             <ItemStyle HorizontalAlign="Left" />
                                             <HeaderStyle HorizontalAlign="Left" />
                                         </asp:TemplateField>

                                         <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Status">
                                             <ItemTemplate>
                                                 <asp:DropDownList ID="cmbx_Status" runat="server" CssClass="form-control input-sm">
                                                     <asp:ListItem Value="0">Optional</asp:ListItem>
                                                     <asp:ListItem Value="1">Test1</asp:ListItem>
                                                     <asp:ListItem Value="2">Test2</asp:ListItem>
                                                 </asp:DropDownList>
                                             </ItemTemplate>
                                             <ItemStyle HorizontalAlign="Left" />
                                             <HeaderStyle HorizontalAlign="Left" />
                                         </asp:TemplateField>
                                     </Columns>
                                 </asp:GridView>
                            </div>


                        </div>
                    </div>
                </div>
            </div>
        </div>
        <%--<div class="row">
            <div class="col-lg-12">
                <div class="panel panel-default">
                    <div class="panel-heading">
                       Account Opening Details
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="form-group">
                                <div class="col-md-12">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <asp:GridView ID="GVAcOpen" runat="server" AutoGenerateColumns="False"
                                                CssClass="table table-bordered table-striped table-hover" EmptyDataText="No Data Found" GridLines="Both">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="SL." HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex + 1 %>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Account No">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtAcNo" CssClass="form-control input-sm" runat="server"
                                                                autocomplete="off" ForeColor="Black" Text='<%# Eval("AccountNo") %>' onFocus="this.select()"></asp:TextBox>

                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>

                                                      <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Name">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtAcName" CssClass="form-control input-sm" runat="server"
                                                                autocomplete="off" ForeColor="Black" Text='<%# Eval("name") %>' onFocus="this.select()"></asp:TextBox>

                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="50px" HeaderText="ACTION">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="itbnEdit" runat="server" CausesValidation="false" Height="18"
                                                                ImageUrl="~/Content/images/edit.png" Width="18" OnClick="itbnEdit_Click" />
                                                            <asp:ImageButton ID="itbndelete" runat="server" CausesValidation="false"
                                                                CommandName="Delete" Height="18" ImageUrl="~/Content/images/delete.png" Width="18" OnClick="itbndelete_Click" />
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" Width="50px" />
                                                    </asp:TemplateField>


                                                </Columns>
                                            </asp:GridView>
                                        </ContentTemplate>
                                        <%--  <Triggers>
                                            <asp:PostBackTrigger ControlID="btnsubmit" />
                                        </Triggers> 
                                    </asp:UpdatePanel>
                                </div>
                                <div class="clearfix"></div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>--%>
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
