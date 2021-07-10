<%@ Page Language="C#" MasterPageFile="~/MasterPages/ProjectBM.Master" AutoEventWireup="true" CodeBehind="frmSavingsInterestCalculation.aspx.cs" Inherits="iBankingSolution.Transaction.frmSavingsInterestCalculation" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.4.1/js/bootstrap-datepicker.min.js"></script>
         <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.4.1/css/bootstrap-datepicker3.css"/>
    <script type="text/javascript">

        $ = jQuery.noConflict();
        $(function () {
            $('.select2').select2()
        })

        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;
            return true;
        }

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="scrpt1" runat="server" EnablePageMethods="true"></asp:ScriptManager>
        <div class="row">
            <div class="col-lg-12">
                <div style="float: right; margin-top: 12px;">
                     <asp:Button ID="Button1" runat="server" Text="Save" class="btn btn-primary"  />     <%--OnClick="btnsubmit_Click"--%>
                    <a href="frmSavingsInterestCalculation.aspx" class="btn btn-outline btn-danger">Cancel</a>
                    
                </div>
                <h1 class="page-header">Savings Interest Calculation</h1>
            </div>
            
        </div>
         <div class="row">
            <div class="col-lg-12">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <label style="font-size:larger;">Savings Interest Calculation</label>
                        
                    </div>
                    <div class="panel-heading" runat="server" id="DivID" visible="false">
                        <asp:Label ID="Label1" runat="server" Visible="False" align="center" Font-Bold="True" Font-Italic="False" Font-Names="Verdana" ForeColor="White"></asp:Label>
                    </div>
                    <div class="panel-body">
                        <div class="row">

                            <div class="form-group">

                                <div class="col-md-5">
                                    <asp:Label ID="Label2" runat="server" Text="*" Style="color: #FF3300"></asp:Label>
                                    <label style="margin-right: 20PX;Font-Size :larger;">Select Deposit Scheme</label>
                                    <asp:DropDownList ID="cmbx_DepositScheme" CssClass="form-control" placeholder="SELECT DEPOSIT SCHEME" Font-Size="10" runat="server" AutoPostBack="true"  EmptyMessage="Select Deposit Scheme" OnSelectedIndexChanged="cmbxSelectDepositScheme_SelectedIndexChanged" required="true">
 
                                    </asp:DropDownList>
                                   
                               </div>
                                <div class="col-md-4">
                                    <br />
                                    <asp:Button ID="btnShow" runat="server" Text="Show Interest" class="btn btn-primary" OnClick="btnShow_Click" /> 
                                </div>

                                
                              <div class="clearfix"></div>
                                <br /><br />
                                <div class="col-md-2">
                                    <label style="margin-right: 20px;font-size:larger;">DM Code</label>
                                    <asp:TextBox ID="txt_DmCode" CssClass="form-control" placeholder="ENTER DM CODE" Font-Size="10" runat="server" autocomplete="off" ReadOnly="true"></asp:TextBox>
                                </div>
                                <div class="col-md-4">
                                    <label style="margin-right: 20px;font-size:larger;">Last Interest Calculation Date</label>
                                    <asp:TextBox ID="dtpkr_lastDate" CssClass="form-control input-sm BootDatepicker" runat="server" placeholder="ENTER LAST INTEREST CALCULATION DATE" dataformatstring="{0:dd/MM/yyyy}" ReadOnly="true"></asp:TextBox>
                                </div>
                                <div class="col-md-4">
                                    <label style="margin-right: 20px;font-size:larger;">Next Interest Calculation Date</label>
                                    <asp:TextBox ID="dtpkr_nextDate" CssClass="form-control input-sm BootDatepicker" runat="server" placeholder="ENTER NEXT INTEREST CALCULATION DATE" ReadOnly="true"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    <label style="margin-right: 20px;font-size:larger;">Months</label>
                                    <asp:TextBox ID="txt_Months" CssClass="form-control" placeholder="ENTER Months" Font-Size="10" runat="server" autocomplete="off" ReadOnly="true"></asp:TextBox>
                                </div>
                                </div>
                            </div>
                        <div class="clearfix"></div>
                        <br />
                        <br />
                        <br />
                        <div class="row">
            <div class="col-lg-12">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <label style="font-size:small">Interest Calculation</label>
                        
                    </div>
                    </div>
                </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="form-group">
                                <div class="col-md-12">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                             <div style ="height:500px; width:1237px; overflow:auto;">
                                            <asp:GridView ID="gv_Interest" OnPreRender="gv_Interest_PreRender" runat="server" AutoGenerateColumns="True"
                                                CssClass="table table-bordered table-striped table-hover" EmptyDataText="No Data Found" GridLines="Both" >  
                                                <Columns>
                                                   
                                                </Columns>
                                            </asp:GridView>
                                              </div>
                                        </ContentTemplate>
                                        
                                    </asp:UpdatePanel>
                                </div>
                                <div class="clearfix"></div>
                            </div>
                        </div>
                    </div>
               
        </div>


                        </div>
                    </div>
                </div>
             </div>
    <div class="clearfix"></div>

        <div class="row">
            <div class="col-lg-12">
                <div style="float: right; margin-top: 12px;">
                    <asp:Button ID="btnsubmit" runat="server" Text="Save" class="btn btn-primary"  />     
                    <a href="frmSavingsInterestCalculation.aspx" class="btn btn-outline btn-danger">Cancel</a>
                    

                </div>
            </div>
            <!-- /.col-lg-12 -->
        </div>
  
</asp:Content>
