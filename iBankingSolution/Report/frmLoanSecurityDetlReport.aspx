<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/ProjectBM.Master" CodeBehind="frmLoanSecurityDetlReport.aspx.cs" Inherits="iBankingSolution.Report.frmLoanSecurityDetlReport" %>

 <asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
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
                    <asp:Button ID="btnsubmit1" runat="server" Text="Add" class="btn btn-primary" />
           
                </div>
                <h1 class="page-header">    Loan Security Details</h1>
            </div>
            <!-- /.col-lg-12 -->
        </div>
        <div class="row">
            <div class="col-lg-12">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                         Loan Security Details
                    </div>
                    <div class="panel-body">
                         
                        </div>
                    </div>
                </div>
            </div>
      
         
        <div class="row">
            <div class="col-lg-12">
                <div style="float: right; margin-top: 12px;">
                     
                    <a href="#" class="btn btn-outline btn-danger">Cancel</a>
                   
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