<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/ProjectBM.Master" CodeBehind="frmKYCList.aspx.cs" Inherits="iBankingSolution.Master.frmKYCList" %>
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
    <form id="frmItemList" runat="server">
        <div class="row">
            <div class="col-lg-12">
                <div style="float: right; margin-top: 12px;">
                    <a href="~/Master/frmMasterKYC.aspx" id="add" runat="server" class="btn btn-primary">Add New</a>
                </div>
                <h1 class="page-header">KCC List</h1>
            </div>
        </div>
        <div class="row">
            <div class="col-lg-12">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        Item List                
                    </div>
                    <div class="panel-body">
                        <%--<div style="overflow: scroll;">--%>
                        <table width="100%" class="table table-striped table-bordered table-hover" id="dataTables-example">
                            <thead>
                                <tr>
                                    <th>Sr.</th>
                                    <th>Cust ID</th>
                                    <th>Name</th>
                                    <th>Gurdian Name</th>
                                    <th>KCC Card</th>
                                    <th>Phone No</th>
                                    <th>Society Code</th>
                                    <th>Member No</th>
                                    
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

                                                <asp:Label ID="lblCUST_ID" runat="server" Text='<%# Eval("CUST_ID")%>'></asp:Label>
                                                
                                            </td>
                                            <td>
                                                <asp:Label ID="lblName" runat="server" Text='<%# Eval("NAME")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblGUARDIAN_NAME" runat="server" Text='<%# Eval("GUARDIAN_NAME")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblkcc_card" runat="server" Text='<%# Eval("kcc_card")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbltel_no" runat="server" Text='<%# Eval("tel_no")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblmemno" runat="server" Text='<%# Eval("memno")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblSociety_code" runat="server" Text='<%# Eval("Society_code")%>'></asp:Label>
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