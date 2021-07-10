<%@ Page Language="C#" MasterPageFile="~/MasterPages/ProjectBM.Master" AutoEventWireup="true" CodeBehind="frmLoanAcOpening.aspx.cs" Inherits="iBankingSolution.Transaction.frmLoanAcOpening" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.4.1/js/bootstrap-datepicker.min.js"></script>
         <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.4.1/css/bootstrap-datepicker3.css"/>
       
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
    <asp:ScriptManager ID="scrpt1" runat="server" EnablePageMethods="true"></asp:ScriptManager>
        <div class="row">
            <div class="col-lg-12">
                <div style="float: right; margin-top: 12px;">
                    <asp:Button ID="btnsubmit1" runat="server" Text="Save" class="btn btn-primary" OnClick="btnsubmit_Click" />  

                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" class="btn btn-primary"  OnClick="btnCancel_Click"/>   
                    <a href="frmLoanAccountOpeningList.aspx" class="btn btn-danger">Back to List</a>
                     <button type="button" class="btn btn-warning" data-toggle="modal" data-target="#myModal">Edit Account</button>

                    <div class="modal" id="myModal">
                    <div class="modal-dialog modal-sm">
                      <div class="modal-content">
      
                        <!-- Modal Header -->
                        <div class="modal-header">
                          <h4 class="modal-title">Search Account</h4>
                          <button type="button" class="close" data-dismiss="modal">&times;</button>
                        </div>
        
                        <!-- Modal body -->
                        <div class="modal-body">
                            <h5>Search By:</h5>
                            <table><tr>   
                                <td>         <asp:DropDownList ID="cmbx_ddlsearch" runat="server" CssClass="form-control" Width="120" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" Enabled="false">
                                <%--<asp:ListItem Value="0">--Select--</asp:ListItem>--%>
                                <asp:ListItem Value="1">Account Number</asp:ListItem>
                                <%--<asp:ListItem Value="2">NAME</asp:ListItem>--%>

                            </asp:DropDownList></td>
                                <td>&nbsp;&nbsp;&nbsp;</td>
                            <td><asp:TextBox ID="txtsearchkyc" runat="server" Width="140px" CssClass="form-control" AutoPostBack="True" OnTextChanged="txtsearchAccount_TextChanged"></asp:TextBox></td>
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
                <h1 class="page-header">Loan Account Opening</h1>
            </div>
            <!-- /.col-lg-12 -->
        </div>
        <div class="row">
            <div class="col-lg-12">
                <div class="panel panel-primary">
                    <div class="panel-heading" style="font-size:larger;">
                       Loan Account Opening Entry
                    </div>
                    <div class="panel-heading" runat="server" id="DivID" visible="false">
                        <asp:Label ID="Label1" runat="server" Visible="False" align="center" Font-Bold="True" Font-Italic="False" Font-Names="Verdana" ForeColor="White"></asp:Label>
                    </div><br /><br />
                    <div class="panel-body">
                        <div class="row">

                            <div class="form-group">
                                
                            
                                    <div class="col-md-12">
                                    <div class="panel panel-danger">
                                    
                                   <div class="panel-heading text-center" style="font-size:larger;"><b><asp:Label ID="lbltype" runat="server" text="Selected Account Type is:"></asp:Label></b>
                               
                                             </div></div>
                                    
                                        <asp:Label ID="lblDid" runat="server" BackColor="White" ForeColor="White"></asp:Label>
                                    <asp:DropDownList ID="cmbx_AcctType" runat="server" CssClass="form-control" required="required" AutoPostBack="True" OnSelectedIndexChanged="cmbx_AcctType_SelectedIndexChanged" >  <%----%>

                                        <asp:ListItem Value="s">-- SELECT LOAN ACCOUNT TYPE --</asp:ListItem>
                                        <asp:ListItem Value="Farm">KCC LOAN</asp:ListItem>
                                        <asp:ListItem Value="Shg">SHG LOAN</asp:ListItem>
                                        <asp:ListItem Value="General">NO-FARM LOAN</asp:ListItem>
                                    </asp:DropDownList>
                                    
                               </div>
                               </div>
                        </div>
                        <div class="clearfix"></div><br />
                        <%-- Panel For Grid View--%>
                             <asp:Panel runat="server" ID="PanelForGrid" Visible="false">
                                   <div class="form-group">
                                    <div class="col-md-12">
                                    <div class="panel panel-primary">

                                        <div class="panel-heading text-center">
                                            <b>Loan Account Opening Applicants Details</b>
                                        </div>
                                    </div>
                                 <div class="form-group">

                                <div class="col-md-2">
                                    <label style="margin-right: 20PX;">Total Applicants</label><asp:Label ID="lbl2" runat="server" BackColor="White" ForeColor="White"></asp:Label>
                                    <asp:TextBox ID="ntxt_Applicant" runat="server" CssClass="form-control" placeholder="Total Applicant" Font-Size="10" OnTextChanged="ntxt_Applicant_TextChanged" AutoPostBack="true" MinValue="1"></asp:TextBox>

                                </div>
                                   <div class="col-md-10">
                                    <label style="margin-right: 20px;font-size:larger;">Applicants Details</label>
                                    <asp:GridView ID="gv_ClientDetails" runat="server" AutoGenerateColumns="false"
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
                                                    <asp:TextBox ID="CUSTCodeTextBox" CssClass="form-control input-sm" runat="server" placeholder="Cust_id" Font-Size="10"
                                                        autocomplete="off" ForeColor="Black" Text='<%# Eval("CUST_ID") %>' onFocus="this.select()"  AutoPostBack="true" OnTextChanged="CUSTCodeTextBox_TextChanged"></asp:TextBox> <%----%>

                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Name">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtName" CssClass="form-control input-sm" runat="server" placeholder="NAME" Font-Size="10"
                                                        autocomplete="off" ForeColor="Black" Text='<%# Eval("Name") %>' onFocus="this.select()"></asp:TextBox>

                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Guardian Name">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtGuardianName" CssClass="form-control input-sm" runat="server" placeholder="GURDIAN NAME" Font-Size="10"
                                                        autocomplete="off" ForeColor="Black" Text='<%# Eval("GUARDIAN_NAME") %>' onFocus="this.select()"></asp:TextBox>

                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                        
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Village">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtvillage" CssClass="form-control input-sm" runat="server" placeholder="VILLAGE" Font-Size="10"
                                                        autocomplete="off" ForeColor="Black" Text='<%# Eval("VILL_CODE") %>' onFocus="this.select()"></asp:TextBox>

                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="District">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtDistrict" CssClass="form-control input-sm" runat="server" placeholder="DISTRICT" Font-Size="10"
                                                        autocomplete="off" ForeColor="Black" Text='<%# Eval("DIS_CODE") %>' onFocus="this.select()"></asp:TextBox>

                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                           
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="50px" HeaderText="ACTION">
                                               <ItemTemplate>
                                                  <asp:ImageButton ID="itbnNew" runat="server" CausesValidation="false" Height="18"
                                                                ImageUrl="~/Content/images/add.png" Width="18" OnClick="itNew_Click" />
                                                  <asp:ImageButton ID="itbndelete" runat="server" CausesValidation="false"
                                                                CommandName="Delete" Height="18" ImageUrl="~/Content/images/delete.png" Width="18" OnClick="itbndelete_Click" Visible="false" />
                                               </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                               <HeaderStyle HorizontalAlign="Left"/>
                                          </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                       </div>
                                       </div>
                                    </asp:Panel>
                        <%--panel for no form loan--%>
                            <asp:Panel runat="server" ID="PanelForNoFarmLoan" Visible="false">
                            <div class="form-group">
                                <div class="col-md-12">
                                    <div class="panel panel-primary">

                                        <div class="panel-heading text-center">
                                            <b>NO-FARM LOAN ACCOUNT OPENING</b>
                                        </div>
                                    </div>
                                </div>
                              
                                 <div class="clearfix"></div>
                                <div class="col-md-4">
                                    <label style="margin-right: 20PX;font-size:larger;">L/F No</label>
                                    <asp:TextBox ID="txt_LFNO" CssClass="form-control" placeholder="ENTER L/F NO" Font-Size="10" runat="server" onFocus="this.select()" autocomplete="off"></asp:TextBox>
                                </div>
                                <div class="col-md-4">
                                    <label style="margin-right: 20PX;font-size:larger;">Activity</label>
                                    <asp:DropDownList ID="cmbx_Activity" runat="server" placeholder="ACTIVITY" Font-Size="10" CssClass="form-control" OnSelectedIndexChanged="Select_ScanBy" AutoPostBack="true"></asp:DropDownList>
                                </div>
                                <div class="col-md-4">
                                    <label style="margin-right: 20PX;font-size:larger;">Application Date</label>
                                    <asp:TextBox ID="dtpkr_ApplDate" CssClass="form-control input-sm BootDatepicker" placeholder="dd/MM/yyyy" Font-Size="10" runat="server" onFocus="this.select()" autocomplete="off" required="required" AutoPostBack="true" OnTextChanged="ntxt_applDate"></asp:TextBox>
                                </div>
                                    <div class="clearfix"></div>
                                <br />
                                <div class="col-md-3">
                                    <label style="margin-right: 20PX;font-size:larger;">Loan Amount Applied</label>
                                    <asp:TextBox ID="ntxt_AppliedAmount" CssClass="form-control" runat="server" placeholder="ENTER APPLY AMT" Font-Size="10" onFocus="this.select()" autocomplete="off"></asp:TextBox>
                                </div>
                                <div class="col-md-3">
                                    <label style="margin-right: 20PX;font-size:larger;">Sanction Amt</label>
                                    <asp:TextBox ID="ntxt_SancAmount" CssClass="form-control" placeholder="SANC AMOUNT" Font-Size="10" runat="server" onFocus="this.select()" autocomplete="off" onkeypress="return isNumberKey(event)" AutoPostBack="true" OnTextChanged="ntxt_SancAmt"></asp:TextBox>
                                </div>
                                <div class="col-md-3">
                                    <label style="margin-right: 20PX;font-size:larger;">Sanc Date</label>
                                    <asp:TextBox ID="dtpkr_LoanSancDate" runat="server" placeholder="dd/MM/yyyy" Font-Size="10" onFocus="this.select()" CssClass="form-control input-sm BootDatepicker" autocomplete="off" AutoPostBack="false"></asp:TextBox>
                                </div>

                                <div class="col-md-3">
                                    <label style="margin-right: 20PX;font-size:larger;">Sanc By</label>
                                    <asp:TextBox ID="txt_SancBy" runat="server" onFocus="this.select()" placeholder="ENTER SANC BY" Font-Size="10" CssClass="form-control" autocomplete="off" Enabled="false"></asp:TextBox>
                                </div>
                                 
                                    <div class="clearfix"></div>
                            <br />
                            <div class="col-md-4">
                                <label style="margin-right: 20PX;font-size:larger;">Inst Appl</label>

                                <asp:DropDownList ID="cmbx_IntsAppl" runat="server" CssClass="form-control" Font-Size="10">
                                    <asp:ListItem Value="S">-- Select Inst Appl --</asp:ListItem>
                                    <asp:ListItem Value="0">Yes</asp:ListItem>
                                    <asp:ListItem Value="1">No</asp:ListItem>


                                </asp:DropDownList>
                            </div>
                            <div class="col-md-4">
                                <label style="margin-right: 20PX;font-size:larger;">ROI</label>
                                <asp:TextBox ID="ntxt_ROI" runat="server" onFocus="this.select()" placeholder="ENTER ROI" Font-Size="10" CssClass="form-control" autocomplete="off" AutoPostBack="false"></asp:TextBox>

                            </div>
                            <div class="col-md-4">
                                <label style="margin-right: 20PX;font-size:larger;">OD Int Rate</label>
                                <asp:TextBox ID="ntxt_ODIntRate" runat="server" onFocus="this.select()" placeholder="ENTER OD INT RATE" Font-Size="10" CssClass="form-control" autocomplete="off" AutoPostBack="false"></asp:TextBox>

                            </div>
                        </div>
                        <div class="clearfix"></div>
                        <br />
                        <div class="form-group">
                            <div class="col-md-6">
                                <label style="margin-right: 20PX;font-size:larger;">Repay Mode</label>
                                <asp:DropDownList ID="cmbx_RepayMode" runat="server" CssClass="form-control" Font-Size="10" OnSelectedIndexChanged="cmbx_RepayMode_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem Value="0">-- Select Repay Mode --</asp:ListItem>
                                    <asp:ListItem Value="Weekly">Weekly</asp:ListItem>
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
                                <label style="margin-right: 20PX;font-size:larger;">OD Mode</label>
                                <asp:DropDownList ID="cmbx_ODMode" runat="server" CssClass="form-control" Font-Size="10">
                                    <asp:ListItem Value="0">-- Select OD Mode --</asp:ListItem>
                                    <asp:ListItem Value="No OD">No OD</asp:ListItem>
                                    <asp:ListItem Value="Monthly">Monthly</asp:ListItem>
                                    <asp:ListItem Value="Monthly Wise">Monthly Wise</asp:ListItem>
                                    <asp:ListItem Value="Quarterly">Quarterly</asp:ListItem>
                                    
                                    <asp:ListItem Value="Half Yearly">Half Yearly</asp:ListItem>
                                    <asp:ListItem Value="Yearly">Yearly</asp:ListItem>
                                    <asp:ListItem Value="After Last Repay Date">After Last Repay Date</asp:ListItem>
                                    <asp:ListItem Value="EMI">EMI</asp:ListItem>
                                 </asp:DropDownList>
                            </div>
                            <div class="clearfix"></div>
                            <br />
                            <div class="col-md-3">
                                <label style="margin-right: 20PX;font-size:larger;">Loan Period(Months)</label><br />
                                <asp:TextBox ID="ntxt_LoanPeriod" runat="server"  CssClass="form-control" placeholder="LOAN PERIOD" Font-Size="10" onkeypress="return isNumberKey(event)" AutoPostBack="True" OnTextChanged="ntxt_LoanPeriod_TextChanged"  />  <%----%>
                                
                            </div>
                            <div class="col-md-3">
                                <label style="margin-right: 20PX;font-size:larger;">No. of Inst.</label>
                                <asp:TextBox ID="ntxt_NoOfInst" runat="server" CssClass="form-control" placeholder="NO OF INTEREST" Font-Size="10" AutoPostBack="false" required="required" />
                            </div>
                            <div class="col-md-3">
                                <label style="margin-right: 20PX;font-size:larger;">Inst Start Date</label>
                                <asp:TextBox ID="dtpkr_InstStartDate" runat="server" placeholder="ENTER START DT" Font-Size="10" CssClass="form-control input-sm BootDatepicker"/>
                            </div>
                            <div class="col-md-3">
                                <label style="margin-right: 20PX;font-size:larger;">Inst Amount</label>
                                <asp:TextBox ID="ntxt_InstAmount" runat="server" placeholder="ENTER INST AMT" Font-Size="10" ReadOnly="true"  CssClass="form-control" onkeypress="return isNumberKey(event)"/>
                            </div>
                            <div class="clearfix"></div>
                            <br />
                            <div class="col-md-3">
                                <label style="margin-right: 2PX;font-size:larger;">First Disb Date</label>
                                <asp:TextBox ID="dtpkr_FirstDisbDate" runat="server" placeholder="dd/MM/yyyy" Font-Size="10"  CssClass="form-control input-sm BootDatepicker" autocomplete="off"/>
                            </div>

                             <div class="col-md-3">
                                <label style="margin-right: 20PX;font-size:larger;">Cash Disbursement</label>
                                <asp:TextBox ID="ntxt_CashDisbursement" runat="server" placeholder="CASH DISBURSEMENT" Font-Size="10" CssClass="form-control" />
                            </div>
                            <div class="col-md-3">
                                <label style="margin-right: 20PX;font-size:larger;">Repay With in Date</label>
                                
                                <asp:TextBox ID="dtpkr_RepayWithinDate" runat="server" placeholder="dd/MM/yyyy" Font-Size="10" ReadOnly="true" CssClass="form-control input-sm BootDatepicker" />
                               
                            </div>
                             <div class="col-md-3">
                                <label style="margin-right: 20PX;font-size:larger;">Purpose</label>
                                
                                <asp:TextBox ID="txt_Purpose" runat="server" placeholder="ENTER PURPOSE" Font-Size="10" CssClass="form-control" />
                               
                            </div>
                                    <%--Applicants Details close--%>
                               </div>

                                </div>
                         </asp:Panel> 
                        
                         <%--Panel For KCC Loan--%>
                          <asp:Panel runat="server" ID="PanelKCCLoan" Visible="false">
                            <div class="form-group">
                                <div class="col-md-12">
                                    <div class="panel panel-primary">

                                        <div class="panel-heading text-center">
                                            <b>KCC LOAN ACCOUNT OPENING</b>
                                        </div>
                                    </div>
                                </div>
                                   
                                 <div class="col-md-3">
                                <label style="margin-right: 20PX;font-size:larger;">L/F No</label>
                                     
                                    <asp:TextBox ID="txtLfNo" runat="server" placeholder="ENTER L/F NO" Font-Size="10"   CssClass="form-control"/>
                                </div>
                                <div class="col-md-3">
                                    <label style="margin-right: 20PX;font-size:larger;">Activity</label>
                                    <asp:DropDownList ID="txtActivity" runat="server" placeholder="ACTIVITY" Font-Size="10" CssClass="form-control"></asp:DropDownList>
                                </div>
                                <div class="col-md-3">
                                <label style="margin-right: 20PX;font-size:larger;">Appl Date</label>
                                <asp:TextBox ID="txtApplDt" runat="server" placeholder="ENTER APPL DATE" Font-Size="10" CssClass="form-control input-sm BootDatepicker"/>
                                </div>
                                <div class="col-md-3">
                                <label style="margin-right: 20PX;font-size:larger;">Total Loan Amount</label>
                                <asp:TextBox ID="txtLoanAmount" runat="server" placeholder="ENTER LOAN AMOUNT" Font-Size="10" CssClass="form-control"  AutoPostBack="true" required="true"/>
                               </div>
                                <div class="clearfix"></div><br />
                                 
                                <div class="col-md-3">
                                <label style="margin-right: 20PX;font-size:larger;">Cash Disbursement</label>
                                <asp:TextBox ID="txtCashDisbursement" runat="server" placeholder="ENTER CASH DISBURSEMENT" Font-Size="10" CssClass="form-control" />
                               </div>
                                <div class="col-md-3">
                                <label style="margin-right: 20PX;font-size:larger;">Rate Of Interest</label>
                                <asp:TextBox ID="txtRateOfIn" runat="server" placeholder="ENTER RATE OF INTEREST" Font-Size="10" CssClass="form-control" />
                                </div>
                                <div class="col-md-3">
                                <label style="margin-right: 20PX;font-size:larger;">Panel Rt of Int Appl</label>
                                <asp:TextBox ID="txtPRI" runat="server" placeholder="ENTER PANEL RT OF INT APPL" Font-Size="10" CssClass="form-control" />
                                </div>
                                <div class="col-md-3">
                                <label style="margin-right: 20PX;font-size:larger;">Loan Type</label>
                                 <asp:TextBox ID="txtLoanType" runat="server" Font-Size="10" CssClass="form-control" Enabled="false" Text="Farm"/>
                                </div>
             
                                <div class="clearfix"></div><br />
                            

                                <div class="col-md-3">
                                <label style="margin-right: 20PX;font-size:larger;">Loan Period(Months)</label>
                                <asp:TextBox ID="txtLoanPeriod" runat="server" placeholder="ENTER LOAN PERIOD" Font-Size="10" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtloanKCC_select"/>
                                </div>

                                <div class="col-md-3">
                                <label style="margin-right: 20PX;font-size:larger;">Repay With Date</label>
                                <asp:TextBox ID="txtrepaydt" runat="server" placeholder="ENTER REPAY WITH DATE" Font-Size="10" CssClass="form-control input-sm BootDatepicker" Enabled="true"/>
                                </div>
                              
                                <div class="col-md-3">
                                <label style="margin-right: 20PX;font-size:larger;">Repay Mode</label>
                                 <asp:DropDownList ID="cmbx_KCCrepayMode" runat="server" placeholder="ENTER REPAY MODE" Font-Size="10" CssClass="form-control">
                                        <asp:ListItem Value="0">-- Select Repay Mode --</asp:ListItem>
                                        <asp:ListItem Value="Weekly">Weekly</asp:ListItem>
                                        <asp:ListItem Value="Monthly">Monthly</asp:ListItem>
                                        <asp:ListItem Value="Monthly Compound">Monthly Compound</asp:ListItem>
                                        <asp:ListItem Value="Quarterly">Quarterly</asp:ListItem>
                                        <asp:ListItem Value="Quarterly Compound">Quarterly Compound</asp:ListItem>
                                        <asp:ListItem Value="Half Yearly">Half Yearly</asp:ListItem>
                                        <asp:ListItem Value="Yearly">Yearly</asp:ListItem>
                                        <asp:ListItem Value="Yearly Compound">Yearly Compound</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                 <div class="col-md-3">
                                <label style="margin-right: 20PX;font-size:larger;">OD. Mode</label>
                                 <asp:DropDownList ID="cmbx_KCCODmode" runat="server" CssClass="form-control" Font-Size="10">
                                    <asp:ListItem Value="0">-- Select OD Mode --</asp:ListItem>
                                    <asp:ListItem Value="No OD">No OD</asp:ListItem>
                                    <asp:ListItem Value="Monthly">Monthly</asp:ListItem>
                                    <asp:ListItem Value="Monthly Wise">Monthly Wise</asp:ListItem>
                                    <asp:ListItem Value="Quarterly">Quarterly</asp:ListItem>
                                    <asp:ListItem Value="Half Yearly">Half Yearly</asp:ListItem>
                                    <asp:ListItem Value="Yearly">Yearly</asp:ListItem>
                                    <asp:ListItem Value="After Last Repay Date">After Last Repay Date</asp:ListItem>
                                    <asp:ListItem Value="EMI">EMI</asp:ListItem>
                                 </asp:DropDownList>
                                </div>
                                <div class="clearfix"></div><br />
                               
                                <div class="col-md-6">
                                <label style="margin-right: 20PX;font-size:larger;">Installment Amount</label>
                                <asp:TextBox ID="txtPIAmt" runat="server" placeholder="ENTER PRINCIPLE INSTALLMENT AMOUNT" Font-Size="10" CssClass="form-control" ReadOnly="true"/>
                                </div>
                                </div>
                              </div>
                              </asp:Panel>
                              <%--Panel for Shg loan account opening--%> 
                              <asp:Panel runat="server" ID="PanelShgLoanAccount" Visible="false">
                            <div class="form-group">
                                <div class="col-md-12">
                                    <div class="panel panel-primary">

                                        <div class="panel-heading text-center">
                                            <b>SHG LOAN ACCOUNT OPENING</b>
                                        </div>
                                    </div>
                                </div>
                                   
                                 <div class="col-md-3">
                                <label style="margin-right: 20PX;font-size:larger;">L/F No</label>
                                <asp:TextBox ID="txtSHGLFNo" runat="server" placeholder="ENTER L/F NO" Font-Size="10"   CssClass="form-control" />
                                </div>
                                <div class="col-md-3">
                                    <label style="margin-right: 20PX;font-size:larger;">Activity</label>
                                    <asp:DropDownList ID="cmbx_txtactivityShg" runat="server" placeholder="ACTIVITY" Font-Size="10" CssClass="form-control"></asp:DropDownList>
                                </div>
                                <div class="col-md-3">
                                <label style="margin-right: 20PX;font-size:larger;">Appl Date</label>
                                <asp:TextBox ID="txtAppldtSHG" runat="server" placeholder="ENTER APPL DATE" Font-Size="10" CssClass="form-control input-sm BootDatepicker"/>
                                </div>
                                <div class="col-md-3">
                                <label style="margin-right: 20PX;font-size:larger;">Total Deposits Amount</label>
                                <asp:TextBox ID="txtSHGtodpamt" runat="server" placeholder="ENTER DEPOSIT AMOUNT" Font-Size="10" CssClass="form-control" Enabled="false"/>
                               </div>
                                <div class="clearfix"></div><br />
                                 
                                <%--<div class="col-md-3">
                                <label style="margin-right: 20PX;font-size:larger;">Loan Outstanding</label>
                                <asp:TextBox ID="txtSHGloanout" runat="server" placeholder="ENTER LOAN OUTSTANDING" Font-Size="10" CssClass="form-control" onkeypress="return isNumberKey(event)"/>
                               </div>--%>
                                <div class="col-md-3">
                                <label style="margin-right: 20PX;font-size:larger;">Total Loan Amount</label>
                                <asp:TextBox ID="txtSHGtoLoa" runat="server" placeholder="ENTER TOTAL LOAN AMOUNT" Font-Size="10" CssClass="form-control" AutoPostBack="true"/>
                                </div>
                                <div class="col-md-3">
                                <label style="margin-right: 20PX;font-size:larger;">Cash Disburstment</label>
                                <asp:TextBox ID="txtSHGcashdis" runat="server" placeholder="ENTER CASH DISBURSTMENT" Font-Size="10" CssClass="form-control" />
                                </div>
                                <div class="col-md-3">
                                <label style="margin-right: 20PX;font-size:larger;">Rate Of Interest</label>
                                <asp:TextBox ID="txtSHGintrt" runat="server" placeholder="ENTER RATE OF INTEREST" Font-Size="10" CssClass="form-control" />
                                </div>
                                 <div class="col-md-3">
                                <label style="margin-right: 20PX;font-size:larger;">Panel Rate of Interest</label>
                                <asp:TextBox ID="txtSHGPanelintRet" runat="server" placeholder="ENTER PANEL RATE OF INTEREST" Font-Size="10" CssClass="form-control" />
                                </div>
                                <div class="clearfix"></div><br />
                                <div class="col-md-3">
                                    <label style="margin-right :20px;font-size:larger;">Loan Type</label>
                                    <asp:TextBox ID="txtshgltype" runat="server" Font-Size="10" CssClass="form-control" Enabled="false" Text="Shg"></asp:TextBox>
                                </div>
                               <div class="col-md-3">
                                <label style="margin-right: 20PX;font-size:larger;">Loan Period(Months)</label>
                                <asp:TextBox ID="txtSHGLoanPeriod" runat="server" placeholder="ENTER LOAN PERIOD" Font-Size="10" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtSHGLoan_Select"/>
                                </div>
                                <div class="col-md-3">
                                <label style="margin-right: 20PX;font-size:larger;">Repay With Date</label>
                                <asp:TextBox ID="txtSHGRepayWithDate" runat="server" placeholder="ENTER REPAY WITH DATE" Font-Size="10" CssClass="form-control input-sm BootDatepicker"/>
                                </div>
                                <div class="col-md-3">
                                    <label style="margin-right: 20PX;font-size:larger;">Repay Mode</label>
                                    <asp:DropDownList ID="cmbx_SHGRepayMode" runat="server" placeholder="ENTER REPAY MODE" Font-Size="10" CssClass="form-control">
                                        <asp:ListItem Value="0">-- Select Repay Mode --</asp:ListItem>
                                        <asp:ListItem Value="Weekly">Weekly</asp:ListItem>
                                        <asp:ListItem Value="Monthly">Monthly</asp:ListItem>
                                        <asp:ListItem Value="Monthly Compound">Monthly Compound</asp:ListItem>
                                        <asp:ListItem Value="Quarterly">Quarterly</asp:ListItem>
                                        <asp:ListItem Value="Quarterly Compound">Quarterly Compound</asp:ListItem>
                                        <asp:ListItem Value="Half Yearly">Half Yearly</asp:ListItem>
                                        <asp:ListItem Value="Yearly">Yearly</asp:ListItem>
                                        <asp:ListItem Value="Yearly Compound">Yearly Compound</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                
                                <div class="clearfix"></div><br />
                                <div class="col-md-3">
                                <label style="margin-right: 20PX;font-size:larger;">OD. Mode</label>
                                <asp:DropDownList ID="cmbx_SHGODmode" runat="server" CssClass="form-control" Font-Size="10">
                                    <asp:ListItem Value="0">-- Select OD Mode --</asp:ListItem>
                                    <asp:ListItem Value="No OD">No OD</asp:ListItem>
                                    <asp:ListItem Value="Monthly">Monthly</asp:ListItem>
                                    <asp:ListItem Value="Monthly Wise">Monthly Wise</asp:ListItem>
                                    <asp:ListItem Value="Quarterly">Quarterly</asp:ListItem>
                                    <asp:ListItem Value="Half Yearly">Half Yearly</asp:ListItem>
                                    <asp:ListItem Value="Yearly">Yearly</asp:ListItem>
                                    <asp:ListItem Value="After Last Repay Date">After Last Repay Date</asp:ListItem>
                                    <asp:ListItem Value="EMI">EMI</asp:ListItem>
                                 </asp:DropDownList>
                                </div>
                                <div class="col-md-6">
                                <label style="margin-right: 20PX;font-size:larger;">Installment Amount</label>
                                <asp:TextBox ID="txtSHGinstAmt" runat="server" placeholder="ENTER PRINCIPLE INSTALLMENT AMOUNT" Font-Size="10" CssClass="form-control" ReadOnly="true"/>
                                </div>
                                 <div class="col-md-10">
                                    <%--<label style="margin-right: 20px;font-size:larger;">Applicants Details</label>--%>
                                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false"
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
                                                    <asp:TextBox ID="CUSTCodeTextBox" CssClass="form-control input-sm" runat="server" placeholder="Cust_id" Font-Size="10"
                                                        autocomplete="off" ForeColor="Black" Text='<%# Eval("CUST_ID") %>' onFocus="this.select()"  AutoPostBack="true" OnTextChanged="CUSTCodeTextBox_TextChanged"></asp:TextBox> <%----%>

                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Name">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtName" CssClass="form-control input-sm" runat="server" placeholder="NAME" Font-Size="10"
                                                        autocomplete="off" ForeColor="Black" Text='<%# Eval("Name") %>' onFocus="this.select()"></asp:TextBox>

                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            </Columns>
                                    </asp:GridView>
                                </div>
                                  </div>
                              </asp:Panel>        
                        </div>
                <div class="clearfix"></div>
                <br />
           <div class="row">
            <div class="col-lg-12">
                <div style="float: right; margin-top: 12px;">
                    <asp:Button ID="btnsubmit" runat="server" Text="Save" class="btn btn-primary" OnClick="btnsubmit_Click" />   
                    <a href="frmLoanAcOpening.aspx" class="btn btn-outline btn-danger">Cancel</a>

                </div>
            </div>
            
        </div>

     <%-- Scripting Section for calender--%>
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
