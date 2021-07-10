<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/ProjectBM.Master" CodeBehind="frmCreditFromCCB.aspx.cs" Inherits="iBankingSolution.Transaction.frmCreditFromCCB" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $('#dataTables-example').DataTable({
                responsive: true
            });
        });
    </script>
    <style type="text/css">
        .auto-style1 {
            height: 23px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

     
        <asp:ScriptManager ID="scrpt1" runat="server" EnablePageMethods="true"></asp:ScriptManager>
        <div class="row">
            <div class="col-lg-12">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                       Batch Transaction
                    </div>
                    <div class="panel-body">
                        <div class="row">

                            <div class="form-group">

                                <div class="col-md-2">
                                    <label style="margin-right: 20PX;">Type</label>
                                    <asp:DropDownList ID="cmbx_Type" runat="server" CssClass="form-control" Font-Size="10" AutoPostBack="True" OnSelectedIndexChanged="cmbx_Type_SelectedIndexChanged">
                                        <asp:ListItem Value="0">--Select Type--</asp:ListItem>
                                        <asp:ListItem Value="1">Cash</asp:ListItem>
                                        <asp:ListItem Value="2">Journal</asp:ListItem>
                                    </asp:DropDownList>
                                </div>


                                <div class="col-md-3">
                                    <label style="margin-right: 20PX;">Trans Type</label>
                                    <asp:DropDownList ID="Cmbx_TransType" runat="server" Font-Size="10" CssClass="form-control">
                                        <asp:ListItem Value="0">--Trans Type--</asp:ListItem>
                                        <asp:ListItem Value="1">Dr.</asp:ListItem>
                                        <asp:ListItem Value="2">Cr.</asp:ListItem>
                                    </asp:DropDownList>

                                </div>

                                 <div class="col-md-3" runat="server" id="Divldg" visible="false">
                                 <label style="margin-right: 20PX;">Trans Type</label>
                                    <asp:DropDownList ID="Cmbx_Ledger" runat="server" Font-Size="10" CssClass="form-control">
                                        
                                    </asp:DropDownList>

                                </div>

                                  
                                    <div class="clearfix"></div>

                                    <div class="col-md-2">
                                        <label style="margin-right: 20PX;">TransDate:</label>
                                         <asp:TextBox ID="dtpkr_frmDate" CssClass="form-control input-sm BootDatepicker" placeholder="dd/MM/yyyy" Font-Size="10" runat="server" onFocus="this.select()" autocomplete="off" required="required"></asp:TextBox>

                                    </div>
 

                                    <div class="col-md-2">
                                        <br />
                                        
                                         <asp:Button ID="btnShow" runat="server" Text="Show" class="btn btn-primary" OnClick="btnShow_Click" />
                                         
                                    </div>
  

                              

                                    <div class="clearfix"></div>
                                    <hr />
                       
              

                                   

                                </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
<center>
 <div class="row">
            <div class="col-lg-12">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        Batch Data              
                    </div>
                    <div class="panel-body">



                        <asp:GridView ID="GridView1" runat="server"></asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </center>


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
