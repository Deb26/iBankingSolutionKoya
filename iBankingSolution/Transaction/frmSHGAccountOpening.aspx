<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/ProjectBM.Master" CodeBehind="frmSHGAccountOpening.aspx.cs" Inherits="iBankingSolution.Transaction.frmSHGAccountOpening" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.4.1/js/bootstrap-datepicker.min.js"></script>
         <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.4.1/css/bootstrap-datepicker3.css"/>
    <script type="text/javascript">
        $ = jQuery.noConflict();
        $(function () {
            $('.select2').select2()
        })
    </script>
    <style type="text/css">
        .form-control {}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
   
        <asp:ScriptManager ID="scrpt1" runat="server"></asp:ScriptManager>
        <div class="row">
            <div class="col-lg-12">
                <div style="float: right; margin-top: 12px;">
                    <asp:Button ID="btnsubmit1" runat="server" Text="Save" class="btn btn-primary" OnClick="btnsubmit_Click" />
                     
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" class="btn btn-primary" OnClick="btnCancel_Click" />
                    <a href="frmSHGAccountOpeningList.aspx" class="btn btn-default">Back to List</a>
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
                <h1 class="page-header">SHG Account Opening</h1>
            </div>
            <!-- /.col-lg-12 -->
        </div>
        <div class="row">
            <div class="col-lg-12">
                <div class="panel panel-primary">
                    <div class="panel-heading" style="font-size:larger;">
                        SHG Account Opening
                    </div>
                    <div class="panel-heading" runat="server" id="DivID" visible="false">
                        <asp:Label ID="Label1" runat="server" Visible="False" align="center" Font-Bold="True" Font-Italic="False" Font-Names="Verdana" ForeColor="White"></asp:Label>
                    </div>
                    <div class="panel-body">
                        <div class="row">

                            <div class="form-group">

                                <div class="col-md-4">
                                    <label style="margin-right: 20PX;font-size:larger;">Dt of Open</label><asp:Label ID="lblDid" runat="server" BackColor="White" ForeColor="White"></asp:Label>
                                    <asp:TextBox ID="txtDtOpen" CssClass="form-control input-sm BootDatepicker" runat="server" onFocus="this.select()" autocomplete="off" required="required"></asp:TextBox>
                                    

                                </div>
                                <div class="col-md-4">
                                    <label style="margin-right: 20PX;font-size:larger;">Old A/C No</label>
                                    <asp:TextBox ID="txtOldAcNo" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-md-4">
                                    <label style="margin-right: 20PX;font-size:larger;">Deposit Scheme</label>
                                    <asp:DropDownList ID="cmbx_DepositScheme" runat="server" CssClass="form-control" placeholder="Select Deposit Scheme"></asp:DropDownList>
                                </div>
                                <div class="clearfix"></div><br />
                                <div class="col-md-6">
                                    <label style="margin-right: 20PX;font-size:larger;">New Bank Account No</label>
                                    <asp:TextBox ID="txtnewbankAcNo" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                    <label style="margin-right: 20PX;font-size:larger;">No.of Applicant</label>
                                    <asp:TextBox ID="ntxt_NoOfMembers" CssClass="form-control" runat="server" OnTextChanged="ntxt_NoOfMembers_TextChanged" AutoPostBack="True">0</asp:TextBox>
                                </div>
                              <div class="clearfix"></div><br />
                                <div class="col-md-12" title="Applicant Details" style="font-size:larger;">
                                    <label style="margin-right: 20PX;font-size:larger;">Applicant Details</label><br />
                                    <asp:GridView ID="GVApplicantDtl" runat="server" AutoGenerateColumns="False"
                                        CssClass="table table-bordered table-striped table-hover" EmptyDataText="No Data Found" GridLines="Both">
                                        <Columns>
                                            <asp:TemplateField HeaderText="SL" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_SlNo" Text='<%# Bind("SlNo") %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="GroupID">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="GroupIDTextBox" CssClass="form-control input-sm" runat="server"
                                                        autocomplete="off" ForeColor="Black" onFocus="this.select()" Text='<%# Eval("CUST_ID") %>' OnTextChanged="GroupIDTextBox_TextChanged" AutoPostBack="True"></asp:TextBox>

                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Group Name">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtName" CssClass="form-control input-sm" runat="server"
                                                        autocomplete="off" ForeColor="Black" Text='<%# Eval("Name") %>' onFocus="this.select()"></asp:TextBox>

                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="PO">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtPO_CODE" CssClass="form-control input-sm" runat="server"
                                                        autocomplete="off" ForeColor="Black" Text='<%# Eval("PO_CODE") %>' onFocus="this.select()"></asp:TextBox>

                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="PS">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtPS_CODE" CssClass="form-control input-sm" runat="server"
                                                        autocomplete="off" ForeColor="Black" Text='<%# Eval("PS_CODE") %>' onFocus="this.select()"></asp:TextBox>

                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Block">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtBLK_CODE" CssClass="form-control input-sm" runat="server"
                                                        autocomplete="off" ForeColor="Black" Text='<%# Eval("BLK_CODE") %>' onFocus="this.select()"></asp:TextBox>

                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Village">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtVILL_CODE" CssClass="form-control input-sm" runat="server"
                                                        autocomplete="off" ForeColor="Black" Text='<%# Eval("VILL_CODE") %>' onFocus="this.select()"></asp:TextBox>

                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="District">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtDIS_CODE" CssClass="form-control input-sm" runat="server"
                                                        autocomplete="off" ForeColor="Black" Text='<%# Eval("DIS_CODE") %>' onFocus="this.select()"></asp:TextBox>

                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>



                                        </Columns>
                                    </asp:GridView>

                                </div>
                                <div class="clearfix"></div>

                            </div>
                            <div class="col-md-12">
                            <div class="form-group" title="Authorised Signatory Details" style="font-size:larger;">
                                 <label style="margin-right: 20PX;font-size:larger;">Authorised Signatory Details</label><br />
                                <asp:GridView ID="GvSignature" runat="server" AutoGenerateColumns="False"
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
                                                <asp:TextBox ID="lbl_MemberName" CssClass="form-control input-sm" runat="server"
                                                    autocomplete="off" ForeColor="Black" Visible="false" Text='<%# Eval("Name") %>' onFocus="this.select()"></asp:TextBox>
                                                <asp:DropDownList ID="cmbx_AuthSignatoryName" runat="server" Width="100%" Visible="false"></asp:DropDownList>


                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Designation">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtdesignation" CssClass="form-control input-sm" runat="server"
                                                    autocomplete="off" ForeColor="Black" Text='<%# Eval("Designation") %>' onFocus="this.select()"></asp:TextBox>

                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Status">
                                            <ItemTemplate>
                                               <asp:DropDownList ID="cmbx_AuthSignStatus" runat="server" CssClass="form-control input-sm">
                                                      <asp:ListItem Value="0">-Select-</asp:ListItem>
                                                     <asp:ListItem Value="1">Optional</asp:ListItem>
                                                     <asp:ListItem Value="2">Jointly</asp:ListItem>
                                                  
                                                 </asp:DropDownList>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                    </Columns>
                                </asp:GridView>



                                <div class="clearfix"></div>
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
                    
                    <a href="frmSHGAccountOpening.aspx" class="btn btn-outline btn-danger">Cancel</a>

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
