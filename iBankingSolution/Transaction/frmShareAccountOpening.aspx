<%@ Page Language="C#" MasterPageFile="~/MasterPages/ProjectBM.Master" AutoEventWireup="true" CodeBehind="frmShareAccountOpening.aspx.cs" Inherits="iBankingSolution.Transaction.frmShareAccountOpening" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.4.1/js/bootstrap-datepicker.min.js"></script>
         <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.4.1/css/bootstrap-datepicker3.css"/>
    <script type="text/javascript">
        $ = jQuery.noConflict();
        $(function () {
            $('.select2').select2()
        })
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="scrpt1" runat="server"></asp:ScriptManager>
        <div class="row">
            <div class="col-lg-12">
                <div style="float: right; margin-top: 12px;">
                    <asp:Button ID="btnsubmit1" runat="server" Text="Save" class="btn btn-primary" OnClick="btnsubmit_Click" /> <%----%>
                     
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" class="btn btn-primary" OnClick="btnCancel_Click" />  <%----%>
                    <a href="frmShareAccountOpeningList.aspx" class="btn btn-default">Back to List</a>
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
                <h1 class="page-header">Share Account Opening</h1>
            </div>
            <!-- /.col-lg-12 -->
        </div>
       <div class="row">
            <div class="col-lg-12">
                <div class="panel panel-primary">
                    <div class="panel-heading" style="font-size:larger;">
                        Account Opening Entry
                    </div>
                    <div class="panel-heading" runat="server" id="DivID" visible="false">
                        <asp:Label ID="Label1" runat="server" Visible="False" align="center" Font-Bold="True" Font-Italic="False" Font-Names="Verdana" ForeColor="White"></asp:Label>
                    </div>
                    <div class="panel-body">
                    <asp:Panel runat="server" ID="pnlmain" Visible="true">
                                   
                                    <div class="form-group">

                                    <div class="col-md-12">
                                    <div class="panel panel-primary">
                                    
                                   <div class="panel-heading text-center" style="font-size:larger;"><b>Give your Details</b>
                               
                                             </div></div></div>
                                   <div class="col-md-4">
                                    <label style="margin-right: 20PX;font-size:larger;">Date of Open</label>
                                   <asp:TextBox ID="dtpkr_dateopen" CssClass="form-control input-sm BootDatepicker"  Font-Size="10" runat="server" onFocus="this.select()" autocomplete="off" required="required" placeholder="DD/MM/YYYY"></asp:TextBox>
                                  </div>
                                  <div class="col-md-4">
                                    <label style="margin-right: 20PX;font-size:larger;">Old Account Number</label>
                                    <asp:TextBox ID="txt_OldACNo" CssClass="form-control" placeholder="ENTER OLDAC NO" Font-Size="10" runat="server" onFocus="this.select()" autocomplete="off"></asp:TextBox>
                                </div>
                                <div class="col-md-4">
                                    <label style="margin-right: 20PX;font-size:larger;">Select Ledger</label>
                                    <asp:DropDownList ID="cmbx_Ledger" runat="server"  Font-Size="10" CssClass="form-control"></asp:DropDownList>
                                </div>
                                </div>
                              <div class="clearfix"></div><br />
                               <div class="form-group">
                           
                                <div class="col-md-12">
                                    <div class="panel panel-primary">
                                    
                                   <div class="panel-heading text-center" style="font-size:larger;"><b>Total Applicant Details</b>
                               
                                             </div></div></div>
                                   <div class="col-md-2">
                                    <label style="margin-right: 10PX;font-size:larger;">Total Applicant</label><asp:Label ID="lblDid" runat="server" BackColor="White" ForeColor="White"></asp:Label>
                                    <asp:TextBox ID="ntxt_TotApplicant" runat="server" placeholder="ENTER APPLICANT NO" Font-Size="10" onFocus="this.select()" CssClass="form-control" autocomplete="off" AutoPostBack="True" Width="160px" MaxLength="3" onkeypress="return isNumberKey(event)" OnTextChanged="ntxt_TotApplicant_TextChanged"></asp:TextBox>
                                    <span id="errmsg"></span> <%-- --%>
                                  
                                </div>
                                 
                                <div class="col-md-10">
                                     
                                    <label style="margin-right: 10PX;font-size:larger;">Applicants Details</label>
                                    <asp:GridView ID="gv_ClientDetails" runat="server" AutoGenerateColumns="False"
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
                                             
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Village">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtvillage" CssClass="form-control input-sm" runat="server"
                                                        autocomplete="off" ForeColor="Black" Text='<%# Eval("VILL_CODE") %>' Font-Size="10" placeholder="VILLAGE" onFocus="this.select()"></asp:TextBox>

                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="District">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtDistrict" CssClass="form-control input-sm" runat="server"
                                                        autocomplete="off" ForeColor="Black" Text='<%# Eval("DIS_CODE") %>' Font-Size="10" placeholder="DISTRICT" onFocus="this.select()"></asp:TextBox>

                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                            
                                        </Columns>
                                    </asp:GridView>
                                   </div>
                                   </div>
                                <div class="clearfix"></div><br />
                                <div class="form-group">
                           
                                <div class="col-md-12">
                                    <div class="panel panel-primary">
                                    
                                   <div class="panel-heading text-center" style="font-size:larger;"><b>Nominee Details</b>
                               
                                             </div></div></div>
                                    </div>
                                   <div class="col-md-2">
                                    <label style="margin-right: 20PX;font-size:larger;">Enter No.Of Nominee</label>
                                    <asp:TextBox ID="txtNoofNominee" runat="server" placeholder="ENTER NO. OF NOMINEE" Font-Size="10" CssClass="form-control" AutoPostBack="True" OnTextChanged="txtNoofNominee_TextChanged" /><br /> <%-- --%>
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
          <div class="row">
            <div class="col-lg-6">
                <div style="float: right; margin-top: 12px;">
                    <asp:Button ID="btnsubmit" runat="server" Text="Save" class="btn btn-primary"  OnClick="btnsubmit_Click"/>  <%----%>
                    <a href="frmShareAccountOpening.aspx" class="btn btn-outline btn-danger">Cancel</a>

                </div>
            </div>
            
        </div> <br /> <br />

        <%-- Scripting Section for calander --%>
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
