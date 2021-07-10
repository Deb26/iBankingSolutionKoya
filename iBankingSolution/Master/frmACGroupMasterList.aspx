<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/ProjectBM.Master" CodeBehind="frmACGroupMasterList.aspx.cs" Inherits="iBankingSolution.Master.frmACGroupMasterList" %>
 <asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $('#dataTables-example').DataTable({
                responsive: true
            });
        });
    </script>
    <style>
        .popover {
            max-width: 1200px;
            left: 820px !important;
        }

        .btn-group {
            width: 270px;
        }

            .btn-group a {
                margin-right: 7px;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
        <div class="row">
            <div class="col-lg-12">
                <div style="float: right; margin-top: 12px;">
                    <a href="~/Master/frmMasterAcGroup.aspx" id="add" runat="server" class="btn btn-primary">Add New</a>
                    <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#myModal">Edit</button>
                       <div class="modal" id="myModal">
                        <div class="modal-dialog modal-sm">
                          <div class="modal-content">
                          
                            <!-- Modal Header -->
                            <div class="modal-header">
                              <h4 class="modal-title">Search Ledger</h4>
                              <button type="button" class="close" data-dismiss="modal">&times;</button>
                            </div>
        
                            <!-- Modal body -->
                                <div class="modal-body">
                                            <h5>Search By:</h5>
                                           <table><tr>   
                                            <td>         
                                            <asp:DropDownList ID="cmbx_ddlsearch" runat="server" CssClass="form-control" Width="120" OnSelectedIndexChanged="cmbx_ddlsearch_SelectedIndexChanged">
                                            <asp:ListItem Value="0">--Select Item--</asp:ListItem>
                                            <asp:ListItem Value="1">GROUP CODE</asp:ListItem>
                                            <asp:ListItem Value="2">Group Name</asp:ListItem>
                                            <asp:ListItem Value="3">GROUP TYPE</asp:ListItem>

                                            </asp:DropDownList></td>
                                            <td>&nbsp;&nbsp;&nbsp;</td>
                                            <td><asp:TextBox ID="txtsearchACC" runat="server" Width="150px" CssClass="form-control" AutoPostBack="True" OnTextChanged="txtsearchACC_TextChanged"></asp:TextBox></td>
                                            </tr>
                                           </table>
                                </div>
        
                        <!-- Modal footer -->
                        <div class="modal-footer">
                          <button type="button" class="btn btn-danger" data-dismiss="modal">Search</button>
                        </div>
        
                      </div>
                    </div>
                  </div>
                </div>
                <h1 class="page-header">Account Group List</h1>
            </div>
        </div>
        <div class="row">
            <div class="col-lg-12">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        Group List                
                    </div>
                    <div class="panel-body">
                        <%--<div style="overflow: scroll;">--%>
                        <table width="100%" class="table table-striped table-bordered table-hover" id="dataTables-example">
                            <thead>
                                <tr>
                                    <th>Sr.</th>
                                    <th>GROUP CODE</th>
                                    <th>Group Name</th>
                                    <th>GROUP TYPE</th>
                                    <th>FA TYPE</th>
                                    <th>FA TYPE2</th>
                                     
                                    
                                    <th>Action</th>
                                </tr>
                            </thead>
                            <tbody>
                               <asp:Repeater ID="RepCCList" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblsrL" runat="server" Text='<%# (((RepeaterItem)Container).ItemIndex+1).ToString()%>'></asp:Label>
                                                

                                            </td>

                                            <td>

                                                <asp:Label ID="lblGROUP_CODE" runat="server" Text='<%# Eval("GROUP_CODE")%>'></asp:Label>
                                                
                                            </td>

                                            <td>

                                                <asp:Label ID="lblGROUP_NAME" runat="server" Text='<%# Eval("GROUP_NAME")%>'></asp:Label>
                                                
                                            </td>
                                            <td>
                                                <asp:Label ID="lblFA_TYPE" runat="server" Text='<%# Eval("FA_TYPE")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblFA_TYPE2" runat="server" Text='<%# Eval("FA_TYPE2")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblGroupType" runat="server" Text='<%# Eval("Group Type")%>'></asp:Label>
                                                <%--<asp:Label ID="lblGroupType" runat="server" Text='<%# Eval("GROUP_TYPE")%>'></asp:Label>--%>
                                            </td>
                                            <td>
                                               
                                                <asp:LinkButton ID="lnkedit" runat="server" OnClick="lnkedit_Click"><img alt="" src="../Images/edit.png" Height="20px" Width="20px" /></asp:LinkButton>
                                                <asp:LinkButton ID="LinkDelete" runat="server" Onclick="LinkDelete_Click"><img alt="" src="../Images/delete.png" Height="20px" Width="20px" /></asp:LinkButton>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                                 
                            </tbody>
                        </table>
                        <%-- </div>--%>
                    </div>
                </div>
            </div>
        </div>
 
</asp:Content>
