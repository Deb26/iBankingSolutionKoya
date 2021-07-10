<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/ProjectBM.Master" AutoEventWireup="true" CodeBehind="frmTextFileUpload.aspx.cs" Inherits="iBankingSolution.Transaction.frmTextFileUpload" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
     <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.4.1/js/bootstrap-datepicker.min.js"></script>
         <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.4.1/css/bootstrap-datepicker3.css"/>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server"><br />
    <div class="row">
            <div class="col-lg-12">
                <div class="panel panel-primary">
                    <div class="panel-heading" style="font-size:larger;">
                        Handle Machine File Upload
                    </div>
                    <div class="panel-body">
                        <div class="form-group">
                           <div class="col-md-2">
                              <label style="margin-right: 20PX;font-size:larger;">Upload File</label>
                                <asp:FileUpload ID="FileUpload1" runat="server" CssClass="form-control"/>
                            </div>
                            <div class="col-md-2">
                                <label style="margin-right: 20PX;font-size:larger;">Select Date</label>
                                <asp:TextBox ID="dtpkr_EntryDate" CssClass="form-control input-sm BootDatepicker" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-5"><br />
                                <asp:Button ID="btnShow" Text="Show" runat="server" OnClick="btn_Show" class="btn btn-success"/>
                                
                                <asp:Button ID="btnUpload" Text="Import" runat="server" OnClick="btn_upload" class="btn btn-success"/>
                                <asp:Button ID="btnVoucher" Text="Voucher" runat="server" OnClick="btn_Voucher" class="btn btn-success"/>
                                <asp:Button ID="btnExport" Text="Export" runat="server" OnClick="Get_File" class="btn btn-success"/>
                                 <a href="frmTextFileUpload.aspx" class="btn btn-outline btn-danger">Reset</a>
                            </div>
                            <div class="clearfix"></div><br />
                         
                                
                                

                            </div>
                            <div class="col-md-2"><br />
                                 
                            </div>
                        </div>
                            
                    </div>
                </div>
        </div>
    

<hr />
<%--<asp:Button ID="Show" Text="Show" runat="server" OnClick="btn_Show" class="btn btn-success"/>--%>

   
   <hr />
    <div style ="height:400px; width:1300px; overflow:auto;">
    <asp:GridView ID="GridView1" runat="server" CellPadding="3"  ForeColor="#333333" CssClass="table table-bordered table-striped table-hover" EmptyDataText="No Data Found" GridLines="Both">
        
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
<br />
         <asp:Repeater ID="RepCCList" runat="server"  visible="false">
                                    <ItemTemplate>
                                        <tr>
                                            

                                            <td>

                                                <asp:Label ID="lblacno" runat="server" Text='<%# Eval("accountNO")%>'></asp:Label>

                                            </td>

                                           

                                        </tr>


                                    </ItemTemplate>
                                </asp:Repeater>
        <asp:Repeater ID="Repeater1" runat="server"  visible="false">
                                    <ItemTemplate>
                                        <tr>
                                            <td>

                                                <asp:Label ID="lblbal" runat="server" Text='<%# Eval("Balance")%>'></asp:Label>

                                            </td>

                                            <td>

                                                <asp:Label ID="lblsl" runat="server" Text='<%# Eval("accountno")%>'></asp:Label>

                                            </td>

                                           

                                        </tr>


                                    </ItemTemplate>
                                </asp:Repeater>

<asp:Label ID="lblMessage" ForeColor="Green" runat="server" />
  <div class="col-md-2">
        <asp:TextBox ID="txttdt" runat="server" CssClass="form-control"  Font-Size="10" autocomplete="off" visible="false"></asp:TextBox>
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
