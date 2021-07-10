<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/ProjectBM.Master" CodeBehind="frmDateConfiguration.aspx.cs" Inherits="iBankingSolution.Master.frmDateConfiguration" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        $ = jQuery.noConflict();
        $(function () {
            $('.select2').select2()
        })

       
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     
        <asp:ScriptManager ID="scrpt1" runat="server" EnablePageMethods="true"></asp:ScriptManager>
        <div class="row">
            <div class="form-group">
            <div class="col-md-06">
                <div style="float: right; margin-top: 12px;">
              
                </div>
                <h1 class="page-header">Balance Date Configuration</h1>
            </div>
            <!-- /.col-lg-12 -->
        </div>
       </div>

             <div class="row">
            <div class="col-lg-12">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                                     Balance Date Configuration
                        </div>
                     <div class="panel-body">
                      <div class="col-md-4">
                                   <label style="margin-right: 20PX;">Cash In Hand Date:</label>

                                     <asp:TextBox ID="txt_cashinhand" CssClass="form-control input-sm BootDatepicker" runat="server" Font-Size="10" ></asp:TextBox>
                                </div>
                         <div class="col-md-4">
                                   <label style="margin-right: 20PX;">Date Deposite Account:</label>

                                     <asp:TextBox ID="txt_depositeaccount" CssClass="form-control input-sm BootDatepicker" runat="server" Font-Size="10"></asp:TextBox>
                                </div>
                         <div class="col-md-4">
                                   <label style="margin-right: 20PX;">General Ledger Date:</label>

                                     <asp:TextBox ID="txt_generalledger" CssClass="form-control input-sm BootDatepicker" runat="server" Font-Size="10" ></asp:TextBox>
                             <br />
                                </div>
                         <div class="col-md-4">
                                   <label style="margin-right: 20PX;">Final A/C Date:</label>

                                     <asp:TextBox ID="txt_finalacdate" CssClass="form-control input-sm BootDatepicker" runat="server" Font-Size="10"></asp:TextBox>
                                </div>
                         <div class="col-md-4">
                                   <label style="margin-right: 20PX;">Loan A/C Date:</label>

                                     <asp:TextBox ID="txt_loanac" CssClass="form-control input-sm BootDatepicker" runat="server" Font-Size="10"></asp:TextBox>
                                </div>

                          <div class="col-md-4">
                                    <asp:Button ID="btnsave" runat="server" CssClass="btn btn-primary"  Text="Save" OnClick="btnsave_Click" />
                            
                              </div>

                                <div class="clearfix">
                                </div>
                            </div>
                         </div>
                    </div>
               
                </div>

   <%-- DATE PICKER--%>
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