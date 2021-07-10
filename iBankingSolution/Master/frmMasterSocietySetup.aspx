<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/ProjectBM.Master" CodeBehind="frmMasterSocietySetup.aspx.cs" Inherits="iBankingSolution.Master.frmMasterSocietySetup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
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
    </script>
    <style>
            #load {
            width : 80%;
            height : 80%;
            position : fixed;
            z-index : 9999;
            background : url("circle.gif") no-repeat center 
           }
     </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
   <div id="load"></div>
         <asp:ScriptManager ID="scrpt1" runat="server"></asp:ScriptManager>
        <div class="row">
            <div class="col-lg-12">
                <div style="float: right; margin-top: 12px;">
                    <asp:Button ID="btnsubmit1" runat="server" Text="Add" class="btn btn-primary" OnClick="btnsubmit1_Click" />
                    <asp:Button ID="btnUpdate" runat="server" Text="Update" class="btn btn-primary" OnClick="btnUpdate_Click" Visible="False" />
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" class="btn btn-primary" OnClick="btnCancel_Click" />
                </div>
                <h1 class="page-header">Initial Settings</h1>
            </div>
            <!-- /.col-lg-12 -->
        </div>
        <div class="row">
            <div class="col-lg-12">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        Society Information
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="form-group">

                                <div class="col-md-12">
                                    <label style="margin-right: 20PX;">Society Name</label>
                                    <asp:TextBox ID="txtSocietyName" runat="server" CssClass="form-control"  required="required"></asp:TextBox>
                                </div>
                                
                                <div class="col-md-6">
                                    <br />
                                    <label style="margin-right: 20PX;">Primary Address</label>
                                    <asp:TextBox ID="txtAddress1" runat="server" CssClass="form-control"  required="required" />
                                </div>
                                <div class="col-md-6">
                                    <br />
                                    <label style="margin-right: 20PX;">Seconday Address</label>
                                    <asp:TextBox ID="txtAddress2" runat="server" CssClass="form-control"  required="required" />
                                </div>

                                <div class="clearfix"></div>
                            </div>
                            <div class="form-group">

                                <div class="col-md-2">
                                    <label style="margin-right: 20PX;">StateCode</label>
                                    <asp:TextBox ID="txtStateCode" runat="server" required="required" CssClass="form-control"  />
                                </div>
                                <div class="col-md-2">
                                    <label style="margin-right: 20PX;">District Code:</label>
                                    <asp:TextBox ID="txtDistrictCode" runat="server" required="required" CssClass="form-control"  />
                                </div>
                                <div class="col-md-2">
                                    <label style="margin-right: 20PX;">Regd No:</label>
                                    <asp:TextBox ID="txtRegdNo" runat="server" required="required" CssClass="form-control"  />
                                </div>

                                <div class="col-md-2">
                                    <label style="margin-right: 20PX;">Regd Date:</label>
                                    <asp:TextBox ID="txtRegdDate" runat="server" required="required" CssClass="form-control input-sm BootDatepicker" placeholder="dd/MM/yyyy" autoComplete="off"/>
                                </div>
                                <div class="col-md-2">
                                    <label style="margin-right: 20PX;">Society Code:</label>
                                    <asp:TextBox ID="txtSocietyCode" runat="server" required="required" CssClass="form-control"  />
                                </div>

                                 <div class="col-md-2">
                                    <label style="margin-right: 20PX;">GSTIN No:</label>
                                    <asp:TextBox ID="txtGSTNo" runat="server"  CssClass="form-control"  />
                                </div>
                                <div class="clearfix"></div>
                            </div>
                      
                           
                            <div class="form-group">
                                 
                                <div class="col-md-2">
                                    <label style="margin-right: 20PX;">Year Start Date:</label>
                                    
                                <asp:TextBox ID="txtYrStartDate" CssClass="form-control input-sm BootDatepicker" runat="server" onFocus="this.select()" autocomplete="off" required="required"></asp:TextBox>
                                </div>

                                <div class="col-md-2">
                                    <label style="margin-right: 20PX;">Year End Date:</label>
                                   
                                <asp:TextBox ID="txtYrEndDate" CssClass="form-control input-sm BootDatepicker" runat="server" onFocus="this.select()" autocomplete="off" required="required"></asp:TextBox>
                                </div>

                                 <div class="col-md-2">
                                    <label style="margin-right: 20PX;">Passbook Print Date:</label>
                                   
                                <asp:TextBox ID="txtpassbkprintdt" CssClass="form-control input-sm BootDatepicker" runat="server" onFocus="this.select()" autocomplete="off" required="required"></asp:TextBox>
                                </div>

                                  <div class="col-md-2">
                                    <label style="margin-right: 20PX;">Session:</label>
                                   
                                <asp:TextBox ID="txtSession" CssClass="form-control" runat="server" onFocus="this.select()" autocomplete="off" required="required"></asp:TextBox>
                                      <br />
                                </div>
                                <div class="col-md-2">
                                    <label style="margin-right: 20PX;">Crop Investment:</label>
                                   
                                <asp:TextBox ID="txtcropinvest" CssClass="form-control" runat="server" onFocus="this.select()" autocomplete="off" required="required"></asp:TextBox>
                                      <br />
                                </div>
                                <div class="col-md-2">
                                    <label style="margin-right: 20PX;">Miscellaneous:</label>
                                   
                                <asp:TextBox ID="txtmiscellaneous" CssClass="form-control" runat="server" onFocus="this.select()" autocomplete="off" required="required"></asp:TextBox>
                                      <br />
                                </div>

                                      </div> 
                             
                                <div class="clearfix"></div>
                            

                            <div class="panel panel-primary">
                            <div class="panel-heading">
                                     Bank Details
                             </div>
                     <%---           <div class="clearfix"></div>
                              <div> &nbsp;&nbsp;</div> ---%>
                                <div class="panel-body">
                                 <div class="col-md-2">
                                    <label style="margin-right: 20PX;">Bank Name:</label>
                                    <asp:TextBox ID="txtBankName" runat="server" required="required" CssClass="form-control" placeholder="Input Bank Name"/>
                                 </div>
                                  <div class="col-md-2">
                                    <label style="margin-right: 20PX;">Branch Name:</label>
                                    <asp:TextBox ID="txtBankBrachname" runat="server" required="required" CssClass="form-control" placeholder="Input Bank Branch"/>
                                 </div>

                                  <div class="col-md-2">
                                    <label style="margin-right: 20PX;">MICR Code:</label>
                                    <asp:TextBox ID="txtMicrCode" runat="server" required="required" CssClass="form-control" placeholder="Input Micr Code"/>
                                 </div>

                                  <div class="col-md-2">
                                    <label style="margin-right: 20PX;">IFSC Code:</label>
                                    <asp:TextBox ID="txtIFSCCode" runat="server" required="required" CssClass="form-control" placeholder="Input IFSC Code "/>
                                  </div>

                                 <div class="col-md-4">
                                    <label style="margin-right: 20PX;">Branch Address:</label>
                                    <asp:TextBox ID="txtBankBranchAddress" runat="server" required="required" CssClass="form-control" placeholder="Input Bank Branch Address "/>
                                  </div>
                           
                            </div>
                                </div>
                         
                             <div class="clearfix"></div>
                           
                          <div class="panel panel-primary">
                            <div class="panel-heading">
                                     Society Designated Account Information
                                     
                             </div>
                               <%--- <div class="clearfix"></div>
                                <br /> ---%>
                              
                                 <div class="col-md-2">
                                    <label style="margin-right: 20PX;">
                                     <br />
                                     Society Current AcNo:</label>
                                    <asp:TextBox ID="txtSocCurAcNo" runat="server" required="required" CssClass="form-control" placeholder="Input CBS AcNo"/>
                                     <br />
                                 </div>

                                 <div class="col-md-2">
                                     <br />
                                    <label style="margin-right: 20PX;">CSP Ledger:</label>
                                    <asp:TextBox ID="txtcspacno" runat="server" required="required" CssClass="form-control" placeholder="Input CBS AcNo"/>
                                 </div>
                                <div class="col-md-2">
                                    <br />
                                    <label style="margin-right: 20PX;">Comm.Ledger:</label>
                                    <asp:TextBox ID="txtcommldg" runat="server" required="required" CssClass="form-control" placeholder="Input CBS AcNo"/>
                                </div>
                                <div class="col-md-2">
                                    <br />
                                    <label style="margin-right: 20PX;">Suspense Ledger:</label>
                                    <asp:TextBox ID="txtsuspenseldg" runat="server" required="required" CssClass="form-control" placeholder="Input Suspense ledger"/>
                                </div>
                                                         
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
                        Branch Details
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="form-group">
                                <div class="col-md-12">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <asp:GridView ID="GVBranch" runat="server" AutoGenerateColumns="False"
                                                CssClass="table table-bordered table-striped table-hover" EmptyDataText="No Data Found" GridLines="Both" OnRowDeleting="GVBranch_RowDeleting">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="SL. NO" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <%--<%# Container.DataItemIndex + 1 %>--%>
                                                             <asp:Label ID="lbl_SlNo" Text='<%# Bind("SlNo") %>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Branch Code">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtBrCode" CssClass="form-control input-sm" placeholder="Enter Branch Code" runat="server" required="required"
                                                                autocomplete="off" ForeColor="Black" Text='<%# Eval("BranchCode") %>' onFocus="this.select()"></asp:TextBox>
                                                            <%--Text='<%# Eval("Description") %>'--%>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Branch Name" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtBranchName" CssClass="form-control input-sm" placeholder="Enter Branch Name" runat="server" required="required"
                                                                autocomplete="off" ForeColor="Black" Text='<%# Eval("BranchName") %>' onFocus="this.select()"></asp:TextBox>
                                                            <%--Text='<%# Eval("Description") %>'--%>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Cash In Hand" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtCashInHand" CssClass="form-control input-sm" placeholder="Enter Cash In Hand" runat="server" required=""
                                                                autocomplete="off" ForeColor="Black" Text='<%# Eval("CashInHand") %>' onFocus="this.select()"></asp:TextBox>
                                                            <%--Text='<%# Eval("txtCashInHand") %>'--%>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="BR_Type" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="cmbx_BrType" CssClass="form-control" runat="server">
                                                                <asp:ListItem Value="0">--SELECT BR_Type--</asp:ListItem>
                                                                <asp:ListItem Value="HO">HO</asp:ListItem> 
                                                                <asp:ListItem Value="BR">BR</asp:ListItem>
                   
                                                            </asp:DropDownList>
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
                                                                CommandName="Delete" Height="18" ImageUrl="~/Content/images/delete.png" Width="18" OnClick="itbndelete_Click" />
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" Width="50px" />
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </ContentTemplate>
                                        <Triggers>
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
        </div>
        <div class="row">
            <div class="col-lg-12">
                <div style="float: right; margin-top: 12px;">
                    <asp:Button ID="btnsubmit" runat="server" Text="Save" class="btn btn-primary" OnClick="btnsubmit_Click" />
                      <asp:Button ID="btnUpdate1" runat="server" Text="Update" class="btn btn-primary" OnClick="btnUpdate_Click" Visible="False" />
                    <a href="frmMasterSocietySetup.aspx" class="btn btn-outline btn-danger">Cancel</a>
                   
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
