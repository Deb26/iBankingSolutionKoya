<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/ProjectBM.Master" CodeBehind="frmSHGAccountOpeningList.aspx.cs" Inherits="iBankingSolution.Transaction.frmSHGAccountOpeningList" %>

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
    <form id="frmAcctOpeningDetails" runat="server">
        <div class="row">
            <div class="col-lg-12">
                <div style="float: right; margin-top: 12px;">
                    <a href="~/Transaction/FrmSHGAccountOpening.aspx" id="add" runat="server" class="btn btn-primary">Add New</a>
                </div>
                <h1 class="page-header">SHG Account Opening List</h1>
            </div>
        </div>
        <div class="row">
            <div class="col-lg-12">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        SHG Account Opening List                
                    </div>
                    <div class="panel-body">
                        <%--<div style="overflow: scroll;">--%>
                        <table width="100%" class="table table-striped table-bordered table-hover" id="dataTables-example">
                            <thead>
                                <tr>
                                    <th>Sr.</th>
                                    <th>Account No</th>
                                    <th>Old Account No</th>
                                    <th>Account type</th>
                                    <th>Date of Opening</th>
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

                                                <asp:Label ID="lblsl_code" runat="server" Text='<%# Eval("sl_code")%>'></asp:Label>
                                                
                                            </td>

                                            <td>

                                                <asp:Label ID="lblold_acno" runat="server" Text='<%# Eval("old_acno")%>'></asp:Label>
                                                
                                            </td>
                                             <td>

                                                <asp:Label ID="lblacctype" runat="server" Text='<%# Eval("acctype")%>'></asp:Label>
                                                
                                            </td>
                                            <td>

                                                <asp:Label ID="lbldateofopening" runat="server" Text='<%# Eval("date_of_opening")%>'></asp:Label>
                                                
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
    </form>
</asp:Content>