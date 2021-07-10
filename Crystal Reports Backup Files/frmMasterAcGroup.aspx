<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/ProjectBM.Master" CodeBehind="frmMasterAcGroup.aspx.cs" Inherits="iBankingSolution.Master.frmMasterAcGroup" %>

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
                    <a href="frmACGroupMasterList.aspx" class="btn btn-default">Back to List</a>
                </div>
                <h1 class="page-header">Account Group</h1>
            </div>
            <!-- /.col-lg-12 -->
        </div>
        <div class="row">
            <div class="col-lg-12">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        Account Group Entry
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="form-group">

                                <div class="col-md-3">

                                    <asp:RadioButtonList ID="rdobtn_GroupType" runat="server" CellPadding="4" CellSpacing="4" RepeatDirection="Horizontal"  Width="100%" AutoPostBack="True" OnSelectedIndexChanged="rdobtn_GroupType_SelectedIndexChanged">

                                        <asp:ListItem Value="m">Main Group</asp:ListItem> 
                                        <asp:ListItem Value="s">Sub Group</asp:ListItem>
                                    </asp:RadioButtonList>

                                </div>
                                
 

                                <div class="clearfix"></div>
                            </div>
                            <div class="form-group">

                                <div class="col-md-12">
                                    <label style="margin-right: 20PX;">Group Name</label>
                                     <asp:Label ID="lblDid" runat="server" BackColor="White" ForeColor="White"></asp:Label>
                                    <asp:TextBox ID="txtGroupName" runat="server" required="required" CssClass="form-control" />
                                </div>
                                <div class="col-md-12">
                                      <label style="margin-right: 20PX;">Parent Group</label>
                                    <asp:DropDownList ID="ddlParentGrp" runat="server"  CssClass="form-control">
                                  
                                      </asp:DropDownList>
                                      
                                </div>
 

                                <div class="clearfix"></div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-4">
                                    <label style="margin-right: 20PX;">Primary FA Type</label>
                                     <asp:DropDownList ID="ddlPrimaryFA" runat="server"  CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlPrimaryFA_SelectedIndexChanged">
                                       <%-- <asp:ListItem Value="0">--SELECT--</asp:ListItem>--%>
                                        <asp:ListItem Value="Balance Sheet">Balance Sheet</asp:ListItem>
                                        <asp:ListItem Value="Profit & Loss A/c">Profit &amp; Loss A/c</asp:ListItem>
                                        <asp:ListItem Value="Trading A/c">Trading A/c</asp:ListItem>

                                     </asp:DropDownList>
                                </div>
                                <div class="col-md-4">
                                    <label style="margin-right: 20PX;">Secondary FA Type</label>
                                   <asp:DropDownList ID="cmbx_SecondaryFAType" runat="server"  CssClass="form-control">
                                      <%-- <asp:ListItem Value="0">--SELECT--</asp:ListItem>--%>
                                        <asp:ListItem Value="Assets">Assets</asp:ListItem>
                                        <asp:ListItem Value="Liabilities">Liabilities</asp:ListItem>
                                        

                                   </asp:DropDownList>
                                </div>
                                <div class="col-md-4">
                                    <label style="margin-right: 20PX;">Index</label>
                                    <asp:TextBox ID="ntxt_Index" runat="server" required="required" CssClass="form-control" />
                                </div>
 

                                <div class="clearfix"></div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-3">
                                    <label style="margin-right: 20PX;">Appropiation</label>

                                    <asp:CheckBox ID="chkbx_Appropiation" runat="server"  CssClass="form-control"/>
                                </div>
                                <div class="col-md-3">
                                    <label style="margin-right: 20PX;">Depriciation</label>

                                     <asp:CheckBox ID="chkbx_Depriciation" runat="server"  CssClass="form-control"/>
                                </div>

                               <div class="col-md-3">
                                    <label style="margin-right: 20PX;">Provisioning</label>
                                   <asp:CheckBox ID="chkbx_Provisioning" runat="server"  CssClass="form-control"/>
                                    
                                </div>
                                <div class="col-md-3">
                                    <label style="margin-right: 20PX;">Other</label>

                                 <asp:CheckBox ID="chkbx_Other" runat="server"  CssClass="form-control"/>
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

 

    </form>
  
</asp:Content>
 
