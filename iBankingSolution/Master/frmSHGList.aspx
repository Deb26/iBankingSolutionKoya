﻿<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/ProjectBM.Master" CodeBehind="frmSHGList.aspx.cs" Inherits="iBankingSolution.Master.frmSHGList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $('#dataTables-example').DataTable({
                responsive: true
            });
        });

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
            background : url("circle.gif") no-repeat center /*center rgba(255,255,255,255)*/
           }
     </style>
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
   <div id="load"></div>
        <div class="row">
            <div class="col-lg-12">
                <div style="float: right; margin-top: 12px;">
                    <a href="~/Master/frmMasterSHGclient.aspx" id="add" runat="server" class="btn btn-primary">Add New</a>
                    <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#myModal">Edit</button>
                       <div class="modal" id="myModal">
                        <div class="modal-dialog modal-sm">
                          <div class="modal-content">
                          
                            <!-- Modal Header -->
                            <div class="modal-header">
                              <h4 class="modal-title">Search SHG</h4>
                              <button type="button" class="close" data-dismiss="modal">&times;</button>
                            </div>
        
                            <!-- Modal body -->
                                <div class="modal-body">
                                            <h5>Search By:</h5>
                                           <table><tr>   
                                            <td>         
                                            <asp:DropDownList ID="cmbx_ddlsearch" runat="server" CssClass="form-control" Width="120" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                                            <asp:ListItem Value="0">--Select Item--</asp:ListItem>
                                            <asp:ListItem Value="1">CUST_ID</asp:ListItem>
                                            <asp:ListItem Value="2">GROUP NAME</asp:ListItem>

                                            </asp:DropDownList></td>
                                            <td>&nbsp;&nbsp;&nbsp;</td>
                                            <td><asp:TextBox ID="txtsearchkyc" runat="server" Width="150px" CssClass="form-control" AutoPostBack="True" OnTextChanged="txtsearchkyc_TextChanged"></asp:TextBox></td>
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
                <h1 class="page-header">SHG List</h1>
            </div>
        </div>
        <div class="row">
            <div class="col-lg-12">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        SHG List                
                    </div>
                    <div class="panel-body">
                        <%--<div style="overflow: scroll;">--%>
                        <table width="100%" class="table table-striped table-bordered table-hover" id="dataTables-example">
                            <thead>
                                <tr>
                                    <th>Sr.</th>
                                    <th>Cust ID</th>
                                    <th>Group Name</th>
                                    <th>Leaders' Name</th>
                                    <th>Asst. Leaders' Name</th>
                                   <%-- <th>KCC Card</th>
                                    <th>Phone No</th>
                                    <th>Society Code</th>
                                    <th>Member No</th>--%>
                                    
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

                                                <asp:Label ID="lblName" runat="server" Text='<%# Eval("Name")%>'></asp:Label>
                                                
                                            </td>
                                            <td>
                                                <asp:Label ID="lblGUARDIAN_NAME" runat="server" Text='<%# Eval("GUARDIAN_NAME")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblGUARDIAN" runat="server" Text='<%# Eval("GUARDIAN")%>'></asp:Label>
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

