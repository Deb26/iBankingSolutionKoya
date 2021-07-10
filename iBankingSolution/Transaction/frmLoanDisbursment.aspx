<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/ProjectBM.Master" CodeBehind="frmLoanDisbursment.aspx.cs" Inherits="iBankingSolution.Transaction.frmLoanDisbursment" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit"  TagPrefix="ajaxToolkit"%> 

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


        function checkDec(el) {
            var ex = /^[0-9]+\.?[0-9]*$/;
            if (ex.test(el.value) == false) {
                alert('Incorrect Number');
            }
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
                    <a href="frmLoanRepaymentList.aspx" class="btn btn-default">Back to List</a>
                </div>
                <h1 class="page-header">Loan Disbursment</h1>
            </div>
            <!-- /.col-lg-12 -->
        </div>
        <div class="row">
            <div class="col-lg-6">
                <div class="panel panel-primary">
                    <div class="panel-heading" style="font-size:larger;">
                        Disbursment
                    </div>
                    <div class="panel-heading" runat="server" id="DivID" visible="false">
                        <asp:Label ID="Label1" runat="server" Visible="False" align="center" Font-Bold="True" Font-Italic="False" Font-Names="Verdana" ForeColor="White"></asp:Label>
                    </div>
                    <div class="panel-body">
                        <div class="row">

                           <%-- <div class="form-group">--%>

                                 <div class="col-md-12">
                                    <div class="panel panel-primary">
                                    
                                   <div class="panel-heading text-center" style="font-size:larger;"><b>Accounts Details</b>
                               
                                             </div></div></div>

                                <div class="col-md-6">
                                    <label style="margin-right: 10PX; font-size:larger;">A/c Head.</label>
                                    <asp:Label ID="lblDid" runat="server" BackColor="White" ForeColor="White"></asp:Label>

                                    <asp:TextBox ID="txt_AcctHead" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                    <label style="margin-right: 20PX;font-size:larger;">Loan A/c No</label>
                                    <asp:DropDownList ID="cmbx_AcctNo" runat="server" CssClass="form-control" EmptyMessage="Select A/c No" AutoPostBack="true" Required="true"
                                        OnSelectedIndexChanged="cmbx_AcctNo_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                                <%--</div>--%>
                                <div class="clearfix"></div>
                                 <br/>
                              
                                   <div class="form-group">

                                          <div class="col-md-12">
                                    <div class="panel panel-primary">
                                    
                                   <div class="panel-heading text-center" style="font-size:larger;" runat="server" id="Div4" visible="false"><b>New Disbursement</b>
                               
                                             </div>
                                   
                                    </div></div>

                                        <div class="col-md-12" style="font-size:larger;">
                                            <asp:CheckBox ID="chkbx_NewDisburse" runat="server" Text="Click Here For New Disbursement" Enabled="false" AutoPostBack="True" OnCheckedChanged="chkbx_NewDisburse_CheckedChanged" Visible="false" />
                                        </div>
                                       <div class="clearfix"></div><br />
                                        
                                            <asp:Panel runat="server" ID="PanelNewDis" Enabled="false" Visible="false">
                                                    <div class="col-md-4">
                                                       <%-- <asp:Label Text="Due Repay Date" runat="server">Due Repay Date</asp:Label>--%>
                                                         <label style="margin-right: 20PX;font-size:larger;">Last Repay Dt.</label>
                                                        <asp:TextBox ID="dtpkr_NewRepayDate" runat="server" CssClass="form-control input-sm BootDatepicker"></asp:TextBox>

                                                    </div>
                                                
                                                     <div class="col-md-2">
                                                        <%--<asp:Label Text="ROI" runat="server">ROI</asp:Label>--%>
                                                          <label style="margin-right: 20PX;font-size:larger;">ROI</label>
                                                        <asp:TextBox ID="ntxt_NewROI" runat="server" CssClass="form-control"></asp:TextBox>
                                                          <AjaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="ntxt_NewROI" FilterType="Custom, Numbers" ValidChars="." />

                                                    </div>

                                                     <div class="col-md-3">
                                                        <%--<asp:Label Text="Sanction Amt" runat="server">Sanction Amt</asp:Label>--%>
                                                         <label style="margin-right: 20PX;font-size:larger;">Sanction Amt</label>
                                                        <asp:TextBox ID="ntxt_NewSancAmt" runat="server" CssClass="form-control" OnTextChanged="ntxtSanctionAmt_TextChange" AutoPostBack="true"></asp:TextBox>
                                                          <AjaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="ntxt_NewSancAmt" FilterType="Custom, Numbers" />

                                                    </div>

                                                    <div class="col-md-3">
                                                        <%--<asp:Label Text="OD ROI" runat="server">OD ROI</asp:Label>--%>
                                                          <label style="margin-right: 20PX;font-size:larger;">OD ROI</label>
                                                        <asp:TextBox ID="ntxt_NewODROI" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <AjaxToolkit:FilteredTextBoxExtender ID="ftbe" runat="server" TargetControlID="ntxt_NewODROI" FilterType="Custom, Numbers" ValidChars="." />

                                                    </div>
                                    </asp:Panel>



                                    </div>

                              
                                <div class="clearfix"></div><br />
                            <%--PANEL FOR DEDUCTION --%>
                            <div class="form-group">

                                          <div class="col-md-12">
                                    <div class="panel panel-primary">
                                    
                                   <div class="panel-heading text-center" style="font-size:larger;" runat="server" id="Div5" visible="false"><b>Deduction</b>
                               
                                             </div>
                                   
                                    </div></div>
                                       <%--<div class="clearfix"></div><br />--%>
                                        
                                            <asp:Panel runat="server" ID="Panel1" Enabled="true" Visible="false">
                                                <div class="col-md-6">
                                                    <label style="margin-right: 20px;font-size:larger;">ShareAccount</label>
                                                    <asp:TextBox ID="txtShareAccount" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                </div>
                                                    <div class="col-md-6">
                                                         <label style="margin-right: 20PX;font-size:larger;">ShareAmount</label>
                                                        <asp:TextBox ID="txtShareAmount" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>

                                                    </div>
                                                <div class="clearfix"></div><br />
                                                <div class="col-md-4">
                                                        <label style="margin-right: 20PX;font-size:larger;">Deduction Amount</label>
                                                        <asp:TextBox ID="txtDeductionAmt" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                    </div>
                                                     <div class="col-md-4">
                                                          <label style="margin-right: 20PX;font-size:larger;">CropInsurance</label>
                                                        <asp:TextBox ID="txtcropinsu" runat="server" CssClass="form-control"></asp:TextBox>
                                                          <%--<AjaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="ntxt_NewROI" FilterType="Custom, Numbers" ValidChars="." />--%>

                                                    </div>

                                                     <div class="col-md-4">
                                                        
                                                         <label style="margin-right: 20PX;font-size:larger;">Miscellaneous</label>
                                                        <asp:TextBox ID="txtmiscellaneous" runat="server" CssClass="form-control"></asp:TextBox>
                                                          <%--<AjaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" TargetControlID="ntxt_NewSancAmt" FilterType="Custom, Numbers" />--%>

                                                    </div>

                                                    <%--<div class="col-md-3">--%>
                                                        <%--<asp:Label Text="OD ROI" runat="server">OD ROI</asp:Label>--%>
                                                          <%--<label style="margin-right: 20PX;font-size:larger;">OD ROI</label>
                                                        <asp:TextBox ID="TextBox4" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <AjaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" TargetControlID="ntxt_NewODROI" FilterType="Custom, Numbers" ValidChars="." />

                                                    </div>--%>
                                    </asp:Panel>



                                    </div>
                            <%--NEXT PANEL--%>
                            <div class="clearfix"></div><br />
                                <div class="form-group">
                                      <div class="col-md-12">
                                    <div class="panel panel-primary" style="font-size:larger;">
                                    
                                   <div class="panel-heading text-center"><b>Voucher Details</b>
                               
                                             </div></div></div>
                                    <div class="col-md-6" style="font-size:larger;">
                                        <asp:RadioButtonList ID="rdobtn_Type" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" onchange="return rdobtn_Type_ClientSelectedIndexChanged();" AutoPostBack="True" OnSelectedIndexChanged="rdobtn_Type_SelectedIndexChanged" Required="true">
                                            <asp:ListItem Text="Cash Withdraw" Value="Cash" Selected="true"></asp:ListItem>
                                            <asp:ListItem Text="A/c Transfer" Value="A/c Trans"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>

                                  
                                
                                     <div class="form-group" runat="server" id="fs_CashWithdrawl">
                                           <div class="col-md-12">
                                    <div class="panel panel-primary">
                                    
                                   </div></div>
                                   
                                                                       
                                        <div class="col-md-12">
                                           <%-- <asp:CheckBox ID="CheckBox1" runat="server" Text="Disburse Kind Component As Cash To Savings A/c" />--%>
                                        </div>
                                        <div class="col-md-3">
                                        <label style="margin-right: 20PX;font-size:larger;">Date</label>
                                        <asp:TextBox ID="dtpkr_DisbDate" runat="server" CssClass="form-control input-sm BootDatepicker " AutoPostBack="True" OnTextChanged="dtpkr_DisbDate_TextChanged" />
                                    </div>
                                        <div class="col-md-3">
                                            <label style="margin-right: 20PX;font-size:larger;">Savings A/c</label>
                                            <asp:DropDownList ID="cmbx_TransferAcNo" runat="server" CssClass="form-control" EmptyMessage="Select A/c No"/>
                                        </div>
                                        <div class="col-md-3">
                                            <label style="margin-right: 20PX;font-size:larger;">Net Disb</label>
                                            <asp:TextBox ID="ntxt_NetDisb" runat="server" CssClass="form-control" AutoPostBack="True" OnTextChanged="ntxt_NetDisb_TextChanged" />
                                             <AjaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="ntxt_NetDisb" FilterType="Custom, Numbers" ValidChars="." />

                                        </div>
                                         <div class="col-md-3">
                                        <label style="margin-right: 20PX;font-size:larger;">Voucher No</label>
                                        <asp:TextBox ID="txt_VoucherNo" runat="server" CssClass="form-control" BorderColor="#0099FF" BorderStyle="Inset" Enabled="False" />
                                    <br />
                                    </div>
                                        
                                    </div>
                                    <%----hidden div--%>
                                     
                                    <div class="form-group">

                                        

                                    </div>
                                   
                                    <div class="clearfix"></div>
                                   

                                </div>
                            </div>
                        </div>
                    </div>
                </div>


            <%--Loanee Member Details --%>
       
           <%--Disbursment Details --%>
             <div class="row">
            <div class="col-lg-6">
                <div class="panel panel-primary">
                    <div class="panel-heading" style="font-size:larger;">
                       Sanction Details
                    </div>

                     <div class="panel-heading" runat="server" id="Div3" visible="false">
                        <asp:Label ID="Label4" runat="server" Visible="False" align="center" Font-Bold="True" Font-Italic="False" Font-Names="Verdana" ForeColor="White"></asp:Label>
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="form-group">
                                <div class="col-md-12">
                                <div class="col-md-6">
                                    
                                    <label style="margin-right: 20PX;">Total</label>
                                    <asp:TextBox ID="ntxt_SancTotal" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>

                                <div class="col-md-6">
                                    <label style="margin-right: 20PX;">Cash</label>
                                    <asp:TextBox ID="ntxt_SancCash" runat="server" CssClass="form-control"></asp:TextBox>

                                </div>


                                    </div>

                            </div>
                            </div>

                        </div>

                    </div>



                <div class="panel panel-primary">
                  <div class="panel-heading" style="font-size:larger;">
                       Loanee Member Details
                    </div>
                    <div class="panel-heading" runat="server" id="Div1" visible="false">
                        <asp:Label ID="Label2" runat="server" Visible="False" align="center" Font-Bold="True" Font-Italic="False" Font-Names="Verdana" ForeColor="White"></asp:Label>
                    </div>

                <div class="panel-body">
                        <div class="row">
                            <div class="form-group"> 
                             
                                <div class="col-md-12" overflow:"scroll">
                                    <asp:GridView ID="GVMember" runat="server" AutoGenerateColumns="False" Font-Size="Medium" style="margin-left: 25px" Width="465px" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Cust ID">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCustID" runat="server" Text='<%# Bind("[CUST_ID]") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblName" runat="server" Text='<%# Bind("[Name]") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Gurdian">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblGurdian" runat="server" Text='<%# Bind("[GUARDIAN_NAME]") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Block">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBlock" runat="server" Text='<%# Bind("[BLK_CODE]") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                              <asp:TemplateField HeaderText="Village">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblVillage" runat="server" Text='<%# Bind("[Vill_CODE]") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="District">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDistrict" runat="server" Text='<%# Bind("[Dis_CODE]") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                        </Columns>
                                        <FooterStyle BackColor="White" ForeColor="#000066" />
                                        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                        <RowStyle ForeColor="#000066" />
                                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                        <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                        <SortedDescendingHeaderStyle BackColor="#00547E" />
                                    </asp:GridView>
                                    </div>
                                <div class="clearfix">
                                </div>
                                </div>

                            </div>
                        </div>
</div>

                <div class="panel panel-primary">
                    <div class="panel-heading" style="font-size:larger;">
                       Previous Disbursment Details
                    </div>
                    <div class="panel-heading" runat="server" id="Div2" visible="false">
                        <asp:Label ID="Label3" runat="server" Visible="False" align="center" Font-Bold="True" Font-Italic="False" Font-Names="Verdana" ForeColor="White"></asp:Label>
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="form-group"> 
                             
                                <div class="col-md-12" overflow:"scroll">
                                    <asp:GridView ID="GV_PREDISBLOAN" runat="server" AutoGenerateColumns="False" OnRowDataBound="GV_PREDISBLOAN_RowDataBound"  BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" Font-Size="Small" style="margin-left: 25px" Width="465px">
                                        <Columns>
                                            <asp:TemplateField HeaderText="DISB. DATE">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbldisbdate" runat="server" Text='<%# Bind("[DISB_DATE]") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="DISB AMT">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbldisbamt" runat="server" Text='<%# Bind("[DISB_AMNT]") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="TOTAL">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbltotal" runat="server" Text="TOTAL"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="INS TYPE">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblinstype" runat="server" Text='<%# Bind("[INS_TYPE]") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <FooterStyle BackColor="White" ForeColor="#000066" />
                                        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                        <RowStyle ForeColor="#000066" />
                                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                        <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                        <SortedDescendingHeaderStyle BackColor="#00547E" />
                                    </asp:GridView>
                                    </div>
                                <div class="clearfix">
                                </div>
                                </div>

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
                    <a href="frmLoanDisbursment.aspx" class="btn btn-outline btn-danger">Cancel
                        </a>
                    </div>
                </div></div>
    
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
