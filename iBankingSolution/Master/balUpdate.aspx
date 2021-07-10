<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="balUpdate.aspx.cs" MasterPageFile="~/MasterPages/ProjectBM.Master" Inherits="iBankingSolution.Master.balUpdate" %>

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
            <div class="col-md-06">
                <div style="float: right; margin-top: 12px;">
              
                </div>
                <h1 class="page-header">Balance Update</h1>
            </div>
            <!-- /.col-lg-12 -->
        </div>
       </div>

             <div class="row">
            <div class="col-lg-6">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                                     Balance Details
                        </div>
                     <div class="panel-body">
                        <div class="row">
                          
                            <%--<div class="form-group">--%>
                              <div class="col-md-6">
                                   <label style="margin-right: 20PX;">Old A/C No:</label>

                                     <asp:TextBox ID="txtOldAcNo" CssClass="form-control" runat="server" Font-Size="14" OnTextChanged="txtOldAcNo_TextChanged" AutoPostBack="True" ></asp:TextBox>
                                </div>


                             <div class="col-md-6">
                                   <label style="margin-right: 20PX;">SL CODE:</label>

                                     <asp:TextBox ID="txtAcNo" CssClass="form-control" Font-Size="14" runat="server" OnTextChanged="txtAcNo_TextChanged" AutoPostBack="True" ></asp:TextBox>
                             <br />   
                             </div>

                             <div class="col-md-6">
                                   <label style="margin-right: 20PX;">Name:</label>

                                     <asp:TextBox ID="txtname" CssClass="form-control" Font-Size="14" runat="server"  autocomplete="off" ></asp:TextBox>
                                </div>

                             <div class="col-md-6">
                                   <label style="margin-right: 20PX;">New L/P Number:</label>

                                     <asp:TextBox ID="txtlpnum" CssClass="form-control" runat="server" Font-Size="14" autocomplete="off" ></asp:TextBox>
                             <br />   
                             </div>

                            <div class="col-md-6">
                                   <label style="margin-right: 20PX;">Debit:</label>

                                     <asp:TextBox ID="txtdebit" CssClass="form-control" runat="server" Font-Size="14" AutoPostBack="True" OnTextChanged="txtdebit_TextChanged" ></asp:TextBox>
                              </div>

                            <div class="col-md-6">
                                   <label style="margin-right: 20PX;">Credit:</label>

                                     <asp:TextBox ID="txtcredit" CssClass="form-control" runat="server" Font-Size="14" AutoPostBack="True" OnTextChanged="txtcredit_TextChanged" ></asp:TextBox>
                            <br />    
                            </div>

                            <div class="col-md-12">
                                   <label style="margin-right: 20PX;">Interest Amount For Recurring:</label>

                                     <asp:TextBox ID="txtintamt" CssClass="form-control" runat="server" Font-Size="14" ></asp:TextBox>
                                <br />
                                </div>
                            
                               
                               
                          <div class="col-md-6">
                                    <asp:Button ID="btnupdate" runat="server" Height="40px" Width="120px" CssClass="btn btn-primary"  Text="Update" OnClick="btnupdate_Click" />
                            
                              </div>
                       <div class="col-md-6">
                                    <asp:Button ID="btncancel" runat="server" Height="40px" Width="120px" CssClass="btn btn-primary"  Text="Cancel" OnClick="btncancel_Click" />
                       
                             </div>
                              
                          </div>

                                <%--<div class="clearfix">
                                </div>--%>
                            </div>
                         </div>
                    </div>

                 <div class="col-lg-6">
                            <div class="panel panel-primary">
                                <div class="panel-heading" style="font-size:larger;">
                                                 Balance Details Excel Upload
                                    </div>
                                 <div class="panel-body">
                                    <div class="row">
                                        <%--<div class="col-md-3"></div>--%>
                                        <div class="col-md-8">
                                           <label style="margin-right: 20PX; font-size:larger;">Upload Excel File:</label>
                                                 <asp:FileUpload ID="companyUpload" runat="server" CssClass="form-control"/>
                                             </div>
                                            <div class="clearfix"></div><br />
                                  
                                          <div class="col-md-6">
                                            <asp:Button ID="btnshow" runat="server"  CssClass="btn btn-primary" Text="Upload" OnClick="buttonShow_Click" />                                                        
                                        </div>
                                        <%--<div class="col-md-6">
                                            <asp:Button ID="btnUpload" runat="server"  CssClass="btn btn-primary" Text="Upload" />                                                        
                                        </div>--%>
                                       <%-- <div class="col-md-3">
                                             <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary"/>
                                        </div>
                                       --%>
                                        </div>
                                     </div>
                                </div>
                            </div>
              <%-- OnClick="btnSheet_Click" OnClick="btnSave_Click"OnClick="btnUpload_Click" --%>
                </div>
    <div style ="height:400px; width:1300px; overflow:auto;">
    <asp:GridView ID="GridView1" runat="server" CellPadding="3" Visible="false" ForeColor="#333333" CssClass="table table-bordered table-striped table-hover" EmptyDataText="No Data Found" GridLines="Both">
        
           <EditRowStyle BackColor="#999999" />
           <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
           <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
           <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
           <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
           <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
           <SortedAscendingCellStyle BackColor="#E9E7E2" />
           <SortedAscendingHeaderStyle BackColor="#506C8C" />
           <SortedDescendingCellStyle BackColor="#FFFDF8" />
           <SortedDescendingHeaderStyle BackColor="#6F8DAE" />                                 
    </asp:GridView>
    </div>
          
</asp:Content>

