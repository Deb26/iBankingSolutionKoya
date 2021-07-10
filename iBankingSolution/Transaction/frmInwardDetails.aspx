<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/ProjectBM.Master" CodeBehind="frmInwardDetails.aspx.cs" Inherits="iBankingSolution.Transaction.frmInwardDetails" %>

 <asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
     <script type="text/javascript">
        $ = jQuery.noConflict();
        $(function () {
            $('.select2').select2()
        })
    </script>

     <script type="text/javascript">
        function ShowMessage(message, messagetype) {
            var cssclass;
            debugger;
            switch (messagetype) {
                case 'Success':
                    cssclass = 'alert-success'
                    break;
                case 'Error':
                    cssclass = 'alert-danger'
                    break;
                case 'Warning':
                    cssclass = 'alert-warning'
                    break;
                default:
                    cssclass = 'alert-info'
            }
            $('#alert_container').append('<div id="alert_div" style="margin: 0 -37.5%;width: 99%;-webkit-box-shadow: 3px 4px 6px #999;text-align: center;" class="alert fade in ' + cssclass + '"><a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a><strong>' + messagetype + '!</strong> <span>' + message + '</span></div>').fadeOut(20000);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     
        <asp:ScriptManager ID="scrpt1" runat="server"></asp:ScriptManager>
        <div class="row">
            <div class="col-lg-12">
               <%-- <div style="float: right; margin-top: 12px;">
                    <asp:Button ID="btnsubmit1" runat="server" Text="Save" class="btn btn-primary"  />
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" class="btn btn-primary"  />
                    <a href="frmDepositMasterList.aspx" class="btn btn-default">Back to List</a>
                </div>--%>
                <h1 class="page-header">Inward Details</h1>
            </div>
            <!-- /.col-lg-12 -->
        </div>
                                <asp:Button Text="INWARD/NEFT" BorderStyle="None" ID="Tab1" CssClass="Initial" runat="server" ForeColor="Red" Font-Size="Larger"
                                    OnClick="Tab1_Click" Font-Bold="True" Font-Names="Verdana" />
                                <asp:Button Text="INWARD/RTGS" BorderStyle="None" ID="Tab2" CssClass="Initial" runat="server" ForeColor="Red" Font-Size="Larger"
                                    OnClick="Tab2_Click" Font-Bold="True" Font-Names="Verdana" />
                                <asp:Button Text="INWARD/RETURN_NEFT" BorderStyle="None" ID="Tab3" CssClass="Initial" runat="server" ForeColor="Red" Font-Size="Larger"
                                    OnClick="Tab3_Click" Font-Bold="True" Font-Names="Verdana" />
    
      <div class="row">
        <asp:Panel runat="server" ID="paneldetails" Visible="true">
           <div class="form-group">
            <div class="col-lg-12"><br />
                <div class="panel panel-primary">
                    <div class="panel-heading" style="font-size:larger;">
                        Inward Details
                    </div>
                    <div class="panel-body">
                        
                        <div class="row">

                            <div class="form-group">
                                <div class="col-md-3">
                                    <asp:DropDownList ID="cmbx_selectType" runat="server" CssClass="form-control" required="required" AutoPostBack="True" OnSelectedIndexChanged="cmbx_SelectedIndexChanged">

                                        <asp:ListItem Value="0">--Select Type--</asp:ListItem>
                                        <asp:ListItem Value="1">INWARD/NEFT</asp:ListItem>
                                        <asp:ListItem Value="2">INWARD/RTGS</asp:ListItem>
                                        <asp:ListItem Value="3">INWARD/RETURN_NEFT</asp:ListItem>

                                    </asp:DropDownList>
                                    
                                </div>
                                 <div class="col-md-3">
                                    <label style="margin-right: 20PX;font-size:larger;">Select Date</label>
                                    <asp:TextBox ID="dtpkr_DateAsOn" CssClass="form-control input-sm BootDatepicker" placeholder="dd/MM/yyyy" Font-Size="10" runat="server" onFocus="this.select()" autocomplete="off"></asp:TextBox>
                                </div>
                                 <div class="col-md-3">
                                    <label style="margin-right: 20PX;font-size:larger;">Adjustment Date</label>
                                   <asp:TextBox ID="txt_adjustedDt" CssClass="form-control input-sm BootDatepicker" placeholder="dd/MM/yyyy" Font-Size="10" runat="server" onFocus="this.select()" autocomplete="off"></asp:TextBox>
                                </div>

                                  <div class="col-md-3"><br />
                               
                                <asp:Button ID="btnDownload" runat="server" Text="Show" class="btn btn-primary" OnClick="btnDownload_Click"/>
                           
                               
                                <asp:Button ID="btnUpdate" runat="server" Text="Update" class="btn btn-success" OnClick="btnUpdate_Click"/>
                            </div>
                                <div class="clearfix"></div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
          </div>
       </asp:Panel>
        </div>
        <div class="row">
       <asp:Panel runat="server" ID="panelINWARDNEFT" Visible="false">
           <div class="form-group">
            <div class="col-lg-12">
                <div class="panel panel-primary">
                    <div class="panel-heading" style="font-size:larger;">
                      Show Details Of INWARD/NEFT
                    </div>
                   
                    <div class="panel-body" align="center">
                        <div class="row">
                            <div class="form-group">
                                <div class="col-md-12">
                                    
                                    <asp:GridView ID="GVInwardDetl" runat="server" CssClass="my-auto" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" ShowFooter="True" OnRowDataBound="GVInwardDetl_RowDataBound" EmptyDataText="No Records Found">
                                        <AlternatingRowStyle BackColor="#CCCCCC" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="CUSTOMER NAME ">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtname" CssClass="form-control input-sm" runat="server" Text='<%# Bind("CUSTOMERNAME") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="CBS ACCOUNT ">
                                                 <ItemTemplate>
                                                 <asp:TextBox ID="txtcbsacno" CssClass="form-control input-sm" runat="server" Text='<%# Bind("CBSAcno") %>'></asp:TextBox>
                                                 </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="REF NO ">
                                                   <ItemTemplate>
                                                  <asp:TextBox ID="txtRefNo" CssClass="form-control input-sm" runat="server" Text='<%# Bind("Refno") %>'></asp:TextBox>
                                                 </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="STATUS">
                                                 <ItemTemplate>
                                                 <asp:TextBox ID="txtstatus" CssClass="form-control input-sm" runat="server" Text='<%# Bind("status") %>'></asp:TextBox>
                                                 </ItemTemplate>

                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="TRANSACTION AMOUNT ">
                                                 <ItemTemplate>
                                                 <asp:TextBox ID="txttransactionamt" CssClass="form-control input-sm" runat="server" Text='<%# Bind("transactionamount") %>'></asp:TextBox>
                                                 </ItemTemplate>

                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="TRANSACTION DATE ">
                                                  <ItemTemplate>
                                                  <asp:TextBox ID="txttransactionamt" CssClass="form-control input-sm" runat="server" Text='<%# Bind("transactionamount") %>'></asp:TextBox>
                                                 </ItemTemplate>

                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="REM DETAILS ">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtrimdetails" CssClass="form-control input-sm" runat="server" Text='<%# Bind("remdetails") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="PACS NAME ">
                                                 <ItemTemplate>
                                                 <asp:TextBox ID="txtpacsname" CssClass="form-control input-sm" runat="server" Text='<%# Bind("pacsname") %>'></asp:TextBox>
                                                 </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="SYSTEM DATE">
                                                 <ItemTemplate>
                                                 <asp:TextBox ID="txtsystemdate" CssClass="form-control input-sm" runat="server" Text='<%# Bind("systemdate") %>'></asp:TextBox>
                                                 </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>
                                         
                                        <FooterStyle BackColor="#CCCCCC" />
                                        <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" Font-Names="Verdana" />
                                        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                        <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                        <SortedAscendingHeaderStyle BackColor="#808080" />
                                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                        <SortedDescendingHeaderStyle BackColor="#383838" />
                                         
                                    </asp:GridView>

                                </div>
                                <div class="clearfix"></div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
          </div>
        </asp:Panel>
      </div>
    <%--Panel For INWARD/RTGS --%>
        <div class="row">
           
            <asp:Panel runat="server" ID="panelINWARDRTGS" Visible="false">
           <div class="form-group">
            <div class="col-lg-12">
                <div class="panel panel-primary">
                    <div class="panel-heading" style="font-size:larger;">
                      Show Details Of INWARD/RTGS
                    </div>
                   
                    <div class="panel-body" align="center">
                        <div class="row">
                            <div class="form-group">
                                <div class="col-md-12">
                                    
                                    <asp:GridView ID="GridView1" runat="server" CssClass="my-auto" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" ShowFooter="True" OnRowDataBound="GVInwardDetl_RowDataBound" EmptyDataText="No Records Found">
                                        <AlternatingRowStyle BackColor="#CCCCCC" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="CUSTOMER NAME ">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtname" CssClass="form-control input-sm" runat="server" Text='<%# Bind("CUSTOMERNAME") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="CBS ACCOUNT ">
                                                 <ItemTemplate>
                                                 <asp:TextBox ID="txtcbsacno" CssClass="form-control input-sm" runat="server" Text='<%# Bind("CBSAcno") %>'></asp:TextBox>
                                                 </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="REF NO ">
                                                   <ItemTemplate>
                                                  <asp:TextBox ID="txtRefNo" CssClass="form-control input-sm" runat="server" Text='<%# Bind("Refno") %>'></asp:TextBox>
                                                 </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="STATUS">
                                                 <ItemTemplate>
                                                 <asp:TextBox ID="txtstatus" CssClass="form-control input-sm" runat="server" Text='<%# Bind("status") %>'></asp:TextBox>
                                                 </ItemTemplate>

                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="TRANSACTION AMOUNT ">
                                                 <ItemTemplate>
                                                 <asp:TextBox ID="txttransactionamt" CssClass="form-control input-sm" runat="server" Text='<%# Bind("transactionamount") %>'></asp:TextBox>
                                                 </ItemTemplate>

                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="TRANSACTION DATE ">
                                                  <ItemTemplate>
                                                  <asp:TextBox ID="txttransactionamt" CssClass="form-control input-sm" runat="server" Text='<%# Bind("transactionamount") %>'></asp:TextBox>
                                                 </ItemTemplate>

                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="REM DETAILS ">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtrimdetails" CssClass="form-control input-sm" runat="server" Text='<%# Bind("remdetails") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="PACS NAME ">
                                                 <ItemTemplate>
                                                 <asp:TextBox ID="txtpacsname" CssClass="form-control input-sm" runat="server" Text='<%# Bind("pacsname") %>'></asp:TextBox>
                                                 </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="SYSTEM DATE">
                                                 <ItemTemplate>
                                                 <asp:TextBox ID="txtsystemdate" CssClass="form-control input-sm" runat="server" Text='<%# Bind("systemdate") %>'></asp:TextBox>
                                                 </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="GURDIAN NAME ">
                                                 <ItemTemplate>
                                                 <asp:TextBox ID="txtgurdianname" CssClass="form-control input-sm" runat="server" Text='<%# Bind("gurdianname") %>'></asp:TextBox>
                                                 </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>
                                         
                                        <FooterStyle BackColor="#CCCCCC" />
                                        <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" Font-Names="Verdana" />
                                        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                        <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                        <SortedAscendingHeaderStyle BackColor="#808080" />
                                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                        <SortedDescendingHeaderStyle BackColor="#383838" />
                                         
                                    </asp:GridView>

                                </div>
                                <div class="clearfix"></div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
          </div>
        </asp:Panel>
        </div>
    <%--Panel For INWARD/RETURN_NEFT --%>
    <div class="row">
           
            <asp:Panel runat="server" ID="panelINWARDRETURN_NEFT" Visible="false">
           <div class="form-group">
            <div class="col-lg-12">
                <div class="panel panel-primary">
                    <div class="panel-heading" style="font-size:larger;">
                      Show Details Of INWARD/RETURN_NEFT
                    </div>
                   
                    <div class="panel-body" align="center">
                        <div class="row">
                            <div class="form-group">
                                <div class="col-md-12">
                                    
                                    <asp:GridView ID="GridView2" runat="server" CssClass="my-auto" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" ShowFooter="True" OnRowDataBound="GVInwardDetl_RowDataBound" EmptyDataText="No Records Found">
                                        <AlternatingRowStyle BackColor="#CCCCCC" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="CBS ACCOUNT ">
                                                 <ItemTemplate>
                                                 <asp:TextBox ID="txtcbsacno" CssClass="form-control input-sm" runat="server" Text='<%# Bind("CBSAcno") %>'></asp:TextBox>
                                                 </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="REF NO ">
                                                   <ItemTemplate>
                                                  <asp:TextBox ID="txtRefNo" CssClass="form-control input-sm" runat="server" Text='<%# Bind("Refno") %>'></asp:TextBox>
                                                 </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="STATUS">
                                                 <ItemTemplate>
                                                 <asp:TextBox ID="txtstatus" CssClass="form-control input-sm" runat="server" Text='<%# Bind("status") %>'></asp:TextBox>
                                                 </ItemTemplate>

                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="TRANSACTION AMOUNT ">
                                                 <ItemTemplate>
                                                 <asp:TextBox ID="txttransactionamt" CssClass="form-control input-sm" runat="server" Text='<%# Bind("transactionamount") %>'></asp:TextBox>
                                                 </ItemTemplate>

                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="TRANSACTION DATE ">
                                                  <ItemTemplate>
                                                  <asp:TextBox ID="txttransactionamt" CssClass="form-control input-sm" runat="server" Text='<%# Bind("transactionamount") %>'></asp:TextBox>
                                                 </ItemTemplate>

                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="REM DETAILS ">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtrimdetails" CssClass="form-control input-sm" runat="server" Text='<%# Bind("remdetails") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="PACS NAME ">
                                                 <ItemTemplate>
                                                 <asp:TextBox ID="txtpacsname" CssClass="form-control input-sm" runat="server" Text='<%# Bind("pacsname") %>'></asp:TextBox>
                                                 </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="SYSTEM DATE">
                                                 <ItemTemplate>
                                                 <asp:TextBox ID="txtsystemdate" CssClass="form-control input-sm" runat="server" Text='<%# Bind("systemdate") %>'></asp:TextBox>
                                                 </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="REJECTION REASON">
                                                 <ItemTemplate>
                                                 <asp:TextBox ID="txtrejection" CssClass="form-control input-sm" runat="server" Text='<%# Bind("rejectionreson") %>'></asp:TextBox>
                                                 </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>
                                         
                                        <FooterStyle BackColor="#CCCCCC" />
                                        <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" Font-Names="Verdana" />
                                        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                        <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                        <SortedAscendingHeaderStyle BackColor="#808080" />
                                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                        <SortedDescendingHeaderStyle BackColor="#383838" />
                                         
                                    </asp:GridView>

                                </div>
                                <div class="clearfix"></div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
          </div>
        </asp:Panel>
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