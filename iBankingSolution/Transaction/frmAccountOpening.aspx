<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/ProjectBM.Master" MaintainScrollPositionOnPostback="true" CodeBehind="frmAccountOpening.aspx.cs" Inherits="iBankingSolution.Transaction.frmAccountOpening" %>

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

                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" class="btn btn-primary" OnClick="btnCancel_Click" />
                    <a href="frmAccountOpeningList.aspx" class="btn btn-danger">Back to List</a>
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
                <h1 class="page-header">Account Opening</h1>
            </div>
            <!-- /.col-lg-12 -->
        </div>
        <div class="row">
            <div class="col-lg-12">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        Account Opening Entry
                    </div>
                    <div class="panel-heading" runat="server" id="DivID" visible="false">
                        <asp:Label ID="Label1" runat="server" Visible="False" align="center" Font-Bold="True" Font-Italic="False" Font-Names="Verdana" ForeColor="White"></asp:Label>
                    </div>
                    <div class="panel-body">
                        <div class="row">

                            <div class="form-group">
                                
                            
                                    <div class="col-md-12">
                                    <div class="panel panel-danger">
                                    
                                   <div class="panel-heading text-center"><b><asp:Label ID="lbltype" runat="server" text=""></asp:Label></b>
                               
                                             </div></div>
                                    
                                        <asp:Label ID="lblDid" runat="server" BackColor="White" ForeColor="White"></asp:Label>
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
                               </div>
                        </div>
                           
                            <br />
                              
                                <asp:Panel runat="server" ID="pnlmain" Visible="false">
                                   
                                    <div class="form-group">

                                    <div class="col-md-12">
                                    <div class="panel panel-primary">
                                    
                                   <div class="panel-heading text-center"><b>Give your Details</b>
                               
                                             </div></div></div>
                                   
                                <div class="col-md-2">
                                    <label style="margin-right: 20PX;">Date of Open</label>
                                    <asp:TextBox ID="dtpkr_dateopen" CssClass="form-control input-sm BootDatepicker"  Font-Size="10" runat="server" onFocus="this.select()" autocomplete="off" required="required" placeholder="DD/MM/YYYY"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    <label style="margin-right: 20PX;">Old Ac.No</label>
                                    <asp:TextBox ID="txt_OldACNo" CssClass="form-control" placeholder="ENTER OLDAC NO" Font-Size="10" runat="server" onFocus="this.select()" autocomplete="off"></asp:TextBox>
                                </div>
                                 <div class="col-md-2">
                                    <label style="margin-right: 20PX;">CBS Ac.No</label>
                                    <asp:TextBox ID="txtCBSAcNo" CssClass="form-control" placeholder="ENTER CBSAC NO" Font-Size="10" runat="server" onFocus="this.select()" autocomplete="off"></asp:TextBox>
                                </div>
                                <div class="col-md-4">
                                    <label style="margin-right: 20PX;">Deposit Scheme</label>
                                    <asp:DropDownList ID="cmbx_DepositScheme" runat="server" placeholder="ENTER DEPO SCHEME" Font-Size="10" CssClass="form-control"></asp:DropDownList>
                                </div>
                                <div class="col-md-2">
                                    <label style="margin-right: 20PX;">Category</label>
                                    <asp:DropDownList ID="cmbx_Category" runat="server" CssClass="form-control" Font-Size="10">
                                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                                        <asp:ListItem Value="Individual" Selected="True">Individual</asp:ListItem>
                                        <asp:ListItem Value="Jointly">Jointly</asp:ListItem>
                                        <asp:ListItem Value="Minor">Minor</asp:ListItem>

                                    </asp:DropDownList>
                                   </div>
                                <%-- <div class="col-md-2">
                                    <label style="margin-right: 20PX;">Select Ledger</label>
                                    <asp:DropDownList ID="cmbx_Ledger" runat="server" CssClass="form-control" Font-Size="10" Enabled="false">
                                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                                        <asp:ListItem Value="Individual" Selected="True">Individual</asp:ListItem>
                                        <asp:ListItem Value="Jointly">Jointly</asp:ListItem>
                                        <asp:ListItem Value="Minor">Minor</asp:ListItem>

                                    </asp:DropDownList>
                                        <br />
                                </div>  --%>   
                                     </div> 
                               
                                
                       <div class="clearfix"></div>
                                    <br />
                               <div class="form-group">
                           
                                <div class="col-md-12">
                                    <div class="panel panel-primary">
                                    
                                   <div class="panel-heading text-center"><b>Total Applicant Details</b>
                               
                                             </div></div></div>

                               
                                <div class="col-md-2">
                                    <label style="margin-right: 10PX;">Total Applicant</label>
                                    <asp:TextBox ID="ntxt_TotApplicant" runat="server" placeholder="ENTER APPLICANT NO" Font-Size="10" onFocus="this.select()" CssClass="form-control" autocomplete="off" AutoPostBack="True" OnTextChanged="ntxt_TotApplicant_TextChanged" Width="160px" MaxLength="3" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                    <span id="errmsg"></span>
                                  
                                </div>
                                <div class="col-md-10">
                                     
                                    <label style="margin-right: 10PX;">Applicants Details</label>
                                    <asp:GridView ID="rgv_ClientKYC" runat="server" AutoGenerateColumns="False"
                                        CssClass="table table-bordered table-striped table-hover" EmptyDataText="No Data Found" GridLines="Both" Enabled="true">
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
                                                        autocomplete="off" ForeColor="Black" Text='<%# Eval("CUST_ID") %>' Font-Size="10" placeholder="En Cust_Id" onFocus="this.select()" OnTextChanged="CUSTCodeTextBox_TextChanged" AutoPostBack="True"></asp:TextBox>

                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Name">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtName" CssClass="form-control input-sm" runat="server"
                                                        autocomplete="off" ForeColor="Black" Text='<%# Eval("Name") %>' Font-Size="10" placeholder="NAME" onFocus="this.select()"></asp:TextBox>

                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Guardian Name">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtGuardianName" CssClass="form-control input-sm" runat="server"
                                                        autocomplete="off" ForeColor="Black" Text='<%# Eval("GUARDIAN_NAME") %>' Font-Size="10" placeholder="GUARDIAN NAME" onFocus="this.select()"></asp:TextBox>

                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="PO">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtPO" CssClass="form-control input-sm" runat="server"
                                                        autocomplete="off" ForeColor="Black" Text='<%# Eval("PO_CODE") %>' Font-Size="10" placeholder="POST" onFocus="this.select()"></asp:TextBox>

                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="PS">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtPS" CssClass="form-control input-sm" runat="server"
                                                        autocomplete="off" ForeColor="Black" Text='<%# Eval("PS_CODE") %>' Font-Size="10" placeholder="ENTER PO" onFocus="this.select()"></asp:TextBox>

                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Block">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtblock" CssClass="form-control input-sm" runat="server"
                                                        autocomplete="off" ForeColor="Black" Text='<%# Eval("BLK_CODE") %>' Font-Size="10" placeholder="BLOCK" onFocus="this.select()"></asp:TextBox>

                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Village">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtvillage" CssClass="form-control input-sm" runat="server"
                                                        autocomplete="off" ForeColor="Black" Text='<%# Eval("VILL_CODE") %>' Font-Size="10" placeholder="VILLAGE" onFocus="this.select()"></asp:TextBox>

                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="District" Visible="false">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtDistrict" CssClass="form-control input-sm" runat="server"
                                                        autocomplete="off" ForeColor="Black" Text='<%# Eval("DIS_CODE") %>' Font-Size="10" placeholder="DISTRICT" onFocus="this.select()"></asp:TextBox>

                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Sex">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtSex" CssClass="form-control input-sm" runat="server"
                                                        autocomplete="off" ForeColor="Black" Text='<%# Eval("SEX") %>' Font-Size="10" placeholder="SEX" onFocus="this.select()"></asp:TextBox>

                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                            <%--<asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Religion" Visible="false">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtReligion" CssClass="form-control input-sm" runat="server"
                                                        autocomplete="off" ForeColor="Black" Text='<%# Eval("REL_CODE") %>' Font-Size="10" placeholder="RELIGION" onFocus="this.select()"></asp:TextBox>

                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Profession" Visible="false">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtprofession" CssClass="form-control input-sm" runat="server"
                                                        autocomplete="off" ForeColor="Black" Text='<%# Eval("PROF_CODE") %>' Font-Size="10" placeholder="PROFESSION CODE" onFocus="this.select()"></asp:TextBox>

                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>--%>
                                        </Columns>
                                    </asp:GridView>
                                    <%--Applicants Details close--%>
                                </div>
                                 

                                <div class="clearfix"></div>

                            
                                   </div>
                               
                            <br />

                          <asp:Panel runat="server" ID="PanelOperationAccountDetails" Visible="true">
                             <div class="form-group">      
                          
                                <div class="col-md-12">
                                    <div class="panel panel-primary">
                                    
                                   <div class="panel-heading text-center"><b>Operation Account Details</b>
                               
                                             </div></div></div>
                           
                                <div class="col-md-2">
                                    <label style="margin-right: 20PX;">Operation</label>
                                    <asp:DropDownList ID="cmbx_Operation" runat="server" CssClass="form-control" Font-Size="9">
                                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                                        <asp:ListItem Value="Singly" Selected="True">Singly</asp:ListItem>
                                        <asp:ListItem Value="Either Or Survivor">Either Or Survivor</asp:ListItem>
                                        <asp:ListItem Value="Former Of Survivor">Former Of Survivor</asp:ListItem>
                                        <asp:ListItem Value="Jointly Of Survivor">Jointly Of Survivor</asp:ListItem>

                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-2">
                                    <label style="margin-right: 20PX;">Introducer A/C No</label>
                                    <asp:DropDownList ID="cmbx_IntroAcNo" runat="server" Font-Size="10" placeholder="ENTER INTRO AC NO" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="cmbx_IntroAcNo_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                                <div class="col-md-3">
                                    <label style="margin-right: 20PX;">Introducer Name</label>
                                    <asp:TextBox ID="txt_IntroName" runat="server" placeholder="ENTER INTRODUCER NAME" Font-Size="10" CssClass="form-control" />
                                </div>
                                <div class="col-md-3">
                                    <label style="margin-right: 20PX;">Introducer Address</label>
                                    <asp:TextBox ID="txt_IntroAddress"  runat="server" placeholder="ENTER INTRODUCER ADDRESS" Font-Size="10"  CssClass="form-control" />
                                </div>
                                <div class="col-md-2">
                                    <label style="margin-right: 20PX;">Introducer Phone</label>
                                    <asp:TextBox ID="txt_IntroPhone" runat="server" CssClass="form-control" MaxLength="10" placeholder="ENTER INTRODUCER PHONE" Font-Size="9" onkeypress="return isNumberKey(event)" />
                                </div>
                                <div class="clearfix"></div>
                            </div>
                           </asp:Panel>     
                             
                           

                                     
                           <asp:Panel runat="server" ID="Inttype"  Visible="false">
                            <div class="col-md-12">
                                    <div class="panel panel-primary">
                                    
                                   <div class="panel-heading text-center"><b>Deposite Details</b>
                               
                                             </div></div></div><br />
                               <div class="form-group">
                               
                                <div class="col-md-2">
                                    <label style="margin-right: 20PX;">IntType</label>
                                    <asp:DropDownList ID="cmbx_IntType" runat="server" CssClass="form-control" Font-Size="10">
                                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                                        <asp:ListItem Value="Normal" Selected="True">Normal</asp:ListItem>
                                        <asp:ListItem Value="Sr. Cirtizen">Sr. Cirtizen</asp:ListItem>
                                        <asp:ListItem Value="DBS">DBS</asp:ListItem>
                                    </asp:DropDownList>

                                </div>
                                  
                                
                                <div class="col-md-2">
                                    <label style="margin-right: 20PX;">Period in Month+days</label><br />
                                    <asp:TextBox ID="txt_PeriodsinMonth" runat="server" placeholder="MONTH" Font-Size="10" Width="66px" Height="30" AutoPostBack="true" OnTextChanged="txt_PeriodsinMonth_TextChanged" onkeypress="return isNumberKey(event)"  MaxLength="3" />
                                    <asp:TextBox ID="txt_PeriodsInDays" Width="66px" Height="30" placeholder="DAYS" Font-Size="10" runat="server" AutoPostBack="true" OnTextChanged="txt_PeriodsInDays_TextChanged" onkeypress="return isNumberKey(event)" MaxLength="3" />
                                </div>
                                   <div class="col-md-1">
                                    <label style="margin-right: 20PX;">ROI</label>
                                    <asp:TextBox ID="ntxt_ROI" runat="server" placeholder="ROI" Font-Size="10"  CssClass="form-control"  required="required" AutoPostBack="true" OnTextChanged="ntxt_ROI_TextChanged"></asp:TextBox>
                                </div>
                                <div class="col-md-1">
                                    <label style="margin-right: 20PX;">DepositAmt</label>
                                    <asp:TextBox ID="ntxt_DepositAmt" runat="server" placeholder="DEPO AMT" Font-Size="10"  CssClass="form-control" onkeypress="return isNumberKey(event)" AutoPostBack="True" OnTextChanged="ntxt_DepositAmt_TextChanged" required="required" ></asp:TextBox>
                                </div>
                                   
                                <div class="col-md-2">
                                    <label style="margin-right: 20PX;">Date of Maturity</label>
                                    <asp:TextBox ID="dtpkr_MaturityDate" placeholder="DD/MM/YYYY" Font-Size="10" CssClass="form-control input-sm BootDatepicker" runat="server" onFocus="this.select()" autocomplete="off" required="required" Enabled="False"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    <label style="margin-right: 20PX;">Maturity Amount</label>
                                    <asp:TextBox ID="ntxt_MaturityAmt" runat="server" placeholder="MATURITY AMT" Font-Size="10" CssClass="form-control" Enabled="False" >0</asp:TextBox>
                                    <div class="clearfix"></div><br />
                                </div>

                                    <div class="col-md-2">
                                    <label style="margin-right: 20PX;">Maturity Transfer To</label>
                                    <asp:DropDownList ID="cmbx_IntTransferredTo" runat="server" placeholder="Maturity Transfer To" Font-Size="10" Width="120px" CssClass="form-control"></asp:DropDownList>
                                </div>
                                   <div class="col-md-4">
                                       <%--<label style="margin-right: 20PX;">Employee Name</label>--%>
                                       <asp:DropDownList ID="cmbx_empname" runat="server" Font-Size="10" Width="120px" CssClass="form-control" Enabled="false" Visible="false"></asp:DropDownList>
                                   </div>
                               </div>
                              
                           
                                </asp:Panel>

                                <div class="clearfix"></div>
                              <br />
                               <br />


                            <div class="form-group">
                                <div class="col-md-12">
                                    <div class="panel panel-primary">
                                    
                                   <div class="panel-heading text-center"><b>Nominee Details</b>
                               
                                             </div></div></div><br />
                                <div class="col-md-2">
                                    
                                    <%--<br /> Nominee Details<br />--%>
                                    
                                    <label style="margin-right: 20PX;">Enter No.Of Nominee</label>
                                    <asp:TextBox ID="txtNoofNominee" runat="server" placeholder="ENTER NOMINEE DETAILS" Font-Size="10" CssClass="form-control" AutoPostBack="True" OnTextChanged="txtNoofNominee_TextChanged" /><br />
                                </div>
                                <div class="col-md-10"><br />
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
                                                <asp:TextBox ID="nomn_NameTextBox" CssClass="form-control input-sm" runat="server" Font-Size="10" placeholder="NAME"
                                                    autocomplete="off" ForeColor="Black" Text='<%# Eval("nomn_name") %>' onFocus="this.select()"></asp:TextBox>

                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Address">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtnomnaddress" CssClass="form-control input-sm" runat="server" Font-Size="10" placeholder="ADDRESS"
                                                    autocomplete="off" ForeColor="Black" Text='<%# Eval("nomn_address") %>' onFocus="this.select()"></asp:TextBox>

                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Relation">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="cmbx_nom_Relation" runat="server" CssClass="form-control input-sm" Font-Size="10">
                                                    <asp:ListItem Value="0">Father</asp:ListItem>
                                                    <asp:ListItem Value="1">Mother</asp:ListItem>
                                                    <asp:ListItem Value="2">Husband</asp:ListItem>
                                                    <asp:ListItem Value="3">Wife</asp:ListItem>
                                                    <asp:ListItem Value="4">Nephew</asp:ListItem>
                                                    <asp:ListItem Value="5">Son</asp:ListItem>
                                                    <asp:ListItem Value="6">Daughter</asp:ListItem>
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                </div>
                            </div>
                            <div class="form-group" runat="server" id="IntroDetails" visible="false">
                                 <div class="col-md-12">
                                    <div class="panel panel-primary" style="font-size:larger;">
                                    
                                   <div class="panel-heading text-center"><b>Athorised Signature</b>
                               
                                             </div></div></div><br />
                                <%-- Introducer Details --%> <%--Athorised Signatory Details--%>
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

                                        <%-- <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Designation">
                                             <ItemTemplate>
                                                 <asp:TextBox ID="DesignationTextBox" CssClass="form-control input-sm" runat="server" Font-Size="10" placeholder="DESIGNATION"
                                                     autocomplete="off" ForeColor="Black" Text='<%# Eval("Designation") %>' onFocus="this.select()"></asp:TextBox>

                                             </ItemTemplate>
                                             <ItemStyle HorizontalAlign="Left" />
                                             <HeaderStyle HorizontalAlign="Left" />
                                         </asp:TemplateField>--%>

                                         <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Status">
                                             <ItemTemplate>
                                                 <asp:DropDownList ID="cmbx_Status" runat="server" CssClass="form-control input-sm" Font-Size="10">
                                                     <asp:ListItem Value="0">MUST BE PRESENT</asp:ListItem>
                                                     <asp:ListItem Value="1">MUST BE PRESENT JOINLY </asp:ListItem>
                                                     <asp:ListItem Value="2">OPTIONAL</asp:ListItem>
                                                 </asp:DropDownList>
                                             </ItemTemplate>
                                             <ItemStyle HorizontalAlign="Left" />
                                             <HeaderStyle HorizontalAlign="Left" />
                                         </asp:TemplateField>
                                     </Columns>
                                 </asp:GridView>
                            </div>
                                       
                        </asp:Panel>
                           
                       
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
                    <a href="frmAccountOpening.aspx" class="btn btn-outline btn-danger">Cancel</a>

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
 
</asp:Content>
