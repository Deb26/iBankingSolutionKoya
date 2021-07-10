<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/ProjectBM.Master" CodeBehind="frmLoanAccountOpening.aspx.cs" Inherits="iBankingSolution.Transaction.frmLoanAccountOpening" %>

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
                    <a href="frmLoanAccountOpeningList.aspx" class="btn btn-default">Back to List</a>
                </div>
                <h1 class="page-header">Loan Account Opening</h1>
            </div>
            <!-- /.col-lg-12 -->
        </div>
        <div class="row">
            <div class="col-lg-12">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        Loan Account Opening Entry
                    </div>
                    <div class="panel-heading" runat="server" id="DivID" visible="false">
                        <asp:Label ID="Label1" runat="server" Visible="False" align="center" Font-Bold="True" Font-Italic="False" Font-Names="Verdana" ForeColor="White"></asp:Label>
                    </div>
                    <div class="panel-body">
                        <div class="row">

                            <div class="form-group">

                                <div class="col-md-2">
                                    <label style="margin-right: 20PX;">Total Applicants</label><asp:Label ID="lblDid" runat="server" BackColor="White" ForeColor="White"></asp:Label>
                                    <asp:TextBox ID="ntxt_Applicant" runat="server" CssClass="form-control" placeholder="Total Applicant" OnTextChanged="ntxt_Applicant_TextChanged" AutoPostBack="true" MinValue="1"></asp:TextBox>




                                </div>
                                <div class="col-md-10">
                                    <%-- Applicants Details --%> Applicants Details
                                    <asp:GridView ID="gv_ClientDetails" runat="server" AutoGenerateColumns="False"
                                        CssClass="table table-bordered table-striped table-hover" EmptyDataText="No Data Found" GridLines="Both">
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

                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Religion" Visible="true">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtReligion" CssClass="form-control input-sm" runat="server"
                                                        autocomplete="off" ForeColor="Black" Text='<%# Eval("REL_CODE") %>' onFocus="this.select()"></asp:TextBox>

                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Profession" Visible="true">
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
                                <div class="col-md-4">
                                    <label style="margin-right: 20PX;">L/F No</label>
                                    <asp:TextBox ID="txt_LFNO" CssClass="form-control" runat="server" onFocus="this.select()" autocomplete="off"></asp:TextBox>
                                </div>
                                <div class="col-md-4">
                                    <label style="margin-right: 20PX;">Activity</label>
                                    <asp:DropDownList ID="cmbx_Activity" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                                <div class="col-md-4">
                                    <label style="margin-right: 20PX;">Application Date</label>
                                    <asp:TextBox ID="dtpkr_ApplDate" CssClass="form-control input-sm BootDatepicker" runat="server" onFocus="this.select()" autocomplete="off" required="required"></asp:TextBox>
                                </div>
                                <div class="clearfix"></div>
                                <div class="col-md-3">
                                    <label style="margin-right: 20PX;">Loan Amount Applied</label>
                                    <asp:TextBox ID="ntxt_AppliedAmount" CssClass="form-control" runat="server" onFocus="this.select()" autocomplete="off"></asp:TextBox>
                                </div>
                                <div class="col-md-3">
                                    <label style="margin-right: 20PX;">Sanction Amt</label>
                                    <asp:TextBox ID="ntxt_SancAmount" CssClass="form-control" runat="server" onFocus="this.select()" autocomplete="off" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                </div>
                                <div class="col-md-3">
                                    <label style="margin-right: 20PX;">Sanc Date</label>
                                    <asp:TextBox ID="dtpkr_LoanSancDate" runat="server" onFocus="this.select()" CssClass="form-control input-sm BootDatepicker" autocomplete="off" AutoPostBack="True"></asp:TextBox>
                                </div>

                                <div class="col-md-3">
                                    <label style="margin-right: 20PX;">Sanc By</label>
                                    <asp:TextBox ID="txt_SancBy" runat="server" onFocus="this.select()" CssClass="form-control" autocomplete="off" AutoPostBack="True"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-4">
                                <label style="margin-right: 20PX;">Inst Appl</label>

                                <asp:DropDownList ID="cmbx_IntsAppl" runat="server" CssClass="form-control">
                                    <asp:ListItem Value="0">--Select--</asp:ListItem>
                                    <asp:ListItem Value="Yes" Selected="True">True</asp:ListItem>
                                    <asp:ListItem Value="No">False</asp:ListItem>


                                </asp:DropDownList>
                            </div>
                            <div class="col-md-4">
                                <label style="margin-right: 20PX;">ROI</label>
                                <asp:TextBox ID="ntxt_ROI" runat="server" onFocus="this.select()" CssClass="form-control" autocomplete="off" AutoPostBack="True"></asp:TextBox>

                            </div>
                            <div class="col-md-4">
                                <label style="margin-right: 20PX;">OD Int Rate</label>
                                <asp:TextBox ID="ntxt_ODIntRate" runat="server" onFocus="this.select()" CssClass="form-control" autocomplete="off" AutoPostBack="True"></asp:TextBox>

                            </div>
                        </div>

                        <div class="form-group">
                            <div class="col-md-6">
                                <label style="margin-right: 20PX;">Repay Mode</label>
                                <asp:DropDownList ID="cmbx_RepayMode" runat="server" CssClass="form-control">
                                    <asp:ListItem Value="0">--Select--</asp:ListItem>
                                    <asp:ListItem Value="Weekly" Selected="True">Weekly</asp:ListItem>
                                    <asp:ListItem Value="Monthly">Monthly</asp:ListItem>
                                    <asp:ListItem Value="Monthly Compound">Monthly Compound</asp:ListItem>
                                    <asp:ListItem Value="Quarterly">Quarterly</asp:ListItem>
                                    <asp:ListItem Value="Quarterly Compound">Quarterly Compound</asp:ListItem>
                                    <asp:ListItem Value="Half Yearly">Half Yearly</asp:ListItem>
                                    <asp:ListItem Value="Yearly">Yearly</asp:ListItem>
                                    <asp:ListItem Value="Yearly Compound">Yearly Compound</asp:ListItem>
                                 </asp:DropDownList>
                            </div>

                                 <div class="col-md-6">
                                <label style="margin-right: 20PX;">OD Mode</label>
                                <asp:DropDownList ID="cmbx_ODMode" runat="server" CssClass="form-control">
                                    <asp:ListItem Value="0">--Select--</asp:ListItem>
                                    <asp:ListItem Value="No OD" Selected="True">No OD</asp:ListItem>
                                    <asp:ListItem Value="Monthly">Monthly</asp:ListItem>
                                    <asp:ListItem Value="Monthly Wise">Monthly Wise</asp:ListItem>
                                    <asp:ListItem Value="Quarterly">Quarterly</asp:ListItem>
                                    
                                    <asp:ListItem Value="Half Yearly">Half Yearly</asp:ListItem>
                                    <asp:ListItem Value="Yearly">Yearly</asp:ListItem>
                                    <asp:ListItem Value="After Last Repay Date">After Last Repay Date</asp:ListItem>
                                    <asp:ListItem Value="EMI">EMI</asp:ListItem>
                                 </asp:DropDownList>
                            </div>
                            <div class="col-md-3">
                                <label style="margin-right: 20PX;">Loan Period</label><br />
                                <asp:TextBox ID="ntxt_LoanPeriod" runat="server"  CssClass="form-control" onkeypress="return isNumberKey(event)" />
                                
                            </div>
                            <div class="col-md-3">
                                <label style="margin-right: 20PX;">No. of Inst.</label>
                                <asp:TextBox ID="ntxt_NoOfInst" runat="server" CssClass="form-control" AutoPostBack="True" required="required" />
                            </div>
                            <div class="col-md-3">
                                <label style="margin-right: 20PX;">Inst Start Date</label>
                                <asp:TextBox ID="dtpkr_InstStartDate" runat="server"  CssClass="form-control input-sm BootDatepicker"/>
                            </div>
                            <div class="col-md-3">
                                <label style="margin-right: 20PX;">Inst Amount</label>
                                <asp:TextBox ID="ntxt_InstAmount" runat="server"  CssClass="form-control" onkeypress="return isNumberKey(event)"/>
                            </div>

                            <div class="col-md-3">
                                <label style="margin-right: 20PX;">First Disb Date</label>
                                <asp:TextBox ID="dtpkr_FirstDisbDate" runat="server"  CssClass="form-control input-sm BootDatepicker" />
                            </div>

                             <div class="col-md-3">
                                <label style="margin-right: 20PX;">Cash Disbursement</label>
                                <asp:TextBox ID="ntxt_CashDisbursement" runat="server"  CssClass="form-control" />
                            </div>
                            <div class="col-md-3">
                                <label style="margin-right: 20PX;">Stock Verification Clause</label>
                                <asp:TextBox ID="txt_StockVerificationClause" CssClass="form-control" runat="server" onFocus="this.select()" autocomplete="off"></asp:TextBox>
                            </div>
                             <div class="col-md-3">
                                <label style="margin-right: 20PX;">Share Amount</label>
                                <asp:TextBox ID="ntxt_ShareAmount" runat="server" CssClass="form-control" onkeypress="return isNumberKey(event)" />

                            </div>
                            <div class="clearfix"></div>
                        </div>

                        <div class="form-group">
                            
                           
                            <div class="col-md-3">
                                <label style="margin-right: 20PX;">Subsidy Amount</label>
                                
                                <asp:TextBox ID="ntxt_SubsidyAmount" runat="server" CssClass="form-control" onkeypress="return isNumberKey(event)" />
                               
                            </div>
                             <div class="col-md-3">
                                <label style="margin-right: 20PX;">Repay With in Date</label>
                                
                                <asp:TextBox ID="dtpkr_RepayWithinDate" runat="server" CssClass="form-control input-sm BootDatepicker" />
                               
                            </div>
                            <div class="col-md-6">
                                <label style="margin-right: 20PX;">Purpose</label>
                                
                                <asp:TextBox ID="txt_Purpose" runat="server" CssClass="form-control" />
                               
                            </div>

                          

                             

                            <div class="clearfix"></div>
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
