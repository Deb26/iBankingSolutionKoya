<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmLoanUpdateBalance.aspx.cs" MasterPageFile="~/MasterPages/ProjectBM.Master" Inherits="iBankingSolution.Master.frmLoanUpdateBalance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
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
            <div class="form-group">
            <div class="col-md-12">
                <div style="float: right; margin-top: 12px;">
              
                </div>
                <h1 class="page-header">Loan Balance Update</h1>
            </div>
            <!-- /.col-lg-12 -->
        </div>
       </div>

             <div class="row">
            <div class="col-lg-8">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                                   Loan Balance Details
                        </div>
                     <div class="panel-body">
                        <div class="row">
                          

                                     <div class="col-md-8">
                                   <label style="margin-right: 20PX;">Loan A/C No:</label>

                                     <asp:TextBox ID="txtloanacno" palceholder="Enter Loan A/C no" CssClass="form-control" runat="server" Font-Size="14"  AutoPostBack="True" OnTextChanged="txtloanacno_TextChanged" ></asp:TextBox>
                                <br />
                                     </div>

                                <div class="col-md-4">
                                    <asp:Button ID="btnshow" runat="server" CssClass="btn btn-primary" Width="150px" Height="50px" Text="SHOW" OnClick="btnshow_Click"/>
                                </div>
                                    
                                   <div class="col-md-6">
                                   
                                    <label style="margin-right: 20PX;">Date Upto</label>
                                    <asp:TextBox ID="dtpkr_FormedOn" runat="server" placeholder="dd/MM/yyyy"  Font-Size="14" CssClass="form-control " ></asp:TextBox>
                                </div>
                    <%--  <div class="col-md-6">
                                   
                                    <label style="margin-right: 20PX;">Update Date</label>
                                    <asp:TextBox ID="txtupdatedate" runat="server" placeholder="dd/MM/yyyy"  Font-Size="14" CssClass="form-control input-sm BootDatepicker" ></asp:TextBox>
                                <br />
                            </div>--%>
      
                              <div class="col-md-6">
                                        <label style="margin-right: 20PX;"> Principal Outstanding:</label>
                                        <asp:TextBox ID="txtprinoutstan" palceholder="Enter Principal OD" CssClass="form-control" runat="server" Font-Size="14"  ></asp:TextBox>
                                <br />
                               </div>


                             <div class="col-md-6">
                                   <label style="margin-right: 20PX;">Interest Current:</label>

                                     <asp:TextBox ID="txtintcurr" CssClass="form-control" palceholder="Enter Interest Current" Font-Size="14" runat="server"  ></asp:TextBox>
                               
                                </div>

                           <%--  <div class="col-md-12">
                                 <label style="margin-right: 20PX;"> Principal Current:</label>

                                     <asp:TextBox ID="txtprincurr" CssClass="form-control" Font-Size="14" runat="server" palceholder="Enter Principal Curr" autocomplete="off" ></asp:TextBox>
                               <br />
                                  </div>--%>

                           <%--  <div class="col-md-6">
                                   <label style="margin-right: 20PX;">Principal OverDue:</label>

                                     <asp:TextBox ID="txtlprinod" CssClass="form-control" runat="server" palceholder="Enter Principal OD" Font-Size="14" autocomplete="off" ></asp:TextBox>
                             </div>--%>

                            <div class="col-md-6">
                                   <label style="margin-right: 20PX;">Interest OverDue:</label>
                                   <asp:TextBox ID="txtintod" CssClass="form-control" runat="server" palceholder="Enter Interest OD"  Font-Size="14" ></asp:TextBox>
                            <br />
                            </div>
                            
                          <div class="col-md-6">
                                    <asp:Button ID="btnupdate" runat="server" Width="150px" Height="50px"  CssClass="btn btn-primary"  Text="UPDATE" OnClick="btnupdate_Click" />
                            
                              </div>
                       <div class="col-md-6">
                                    <asp:Button ID="btncancel" runat="server" Width="150px" Height="50px" CssClass="btn btn-primary"  Text="CANCEL" OnClick="btncancel_Click"/>
                       
                             </div>
                              
                          </div>

                                <div class="clearfix">
                                </div>
                            </div>
                         </div>
                    </div>
               
                </div>

     <div class="row">
            <div class="col-lg-8">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        Loan Member Details
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="form-group">
                                <div class="col-md-12">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>

                                            <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="682px">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Cust ID">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtcustid" Font-Size="14" runat="server" Text='<%# Eval("CUST_IDD") %>'></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Name">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtname" Font-Size="14" runat="server" Text='<%# Eval("Namee") %>'></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <FooterStyle BackColor="White" ForeColor="#000066" />
                                                <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                                                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                                <RowStyle ForeColor="#000066" />
                                                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                                <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                                <SortedDescendingHeaderStyle BackColor="#00547E" />
                                            </asp:GridView>
                                           
                                        </ContentTemplate>
                                        <%--  <Triggers>
                                            <asp:PostBackTrigger ControlID="btnsubmit" />
                                        </Triggers>--%>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="clearfix"></div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        
     <script type="text/javascript">

            $('document').ready(function () {

                $("#MainContent_dtpkr_FormedOn").datepicker({
                    numberOfMonths: 1

                });
            });

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

