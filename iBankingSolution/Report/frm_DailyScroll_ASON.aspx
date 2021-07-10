<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/ProjectBM.Master"  CodeBehind="frm_DailyScroll_ASON.aspx.cs" Inherits="iBankingSolution.Report.frm_DailyScroll_ASON" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $('#dataTablesexample').DataTable({
                responsive: true
            });
        });
    </script>
    <style type="text/css">
        .auto-style1 {
            width: 146px;
        }
        .auto-style2 {
            width: 70px;
        }
        .auto-style3 {
            width: 178px;
        }
        .auto-style4 {
            width: 71px;
        }
        .form-control {}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

     
        <asp:ScriptManager ID="scrpt1" runat="server" EnablePageMethods="true"></asp:ScriptManager>
        <div class="row">
            <div class="col-lg-12">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        Daily Scroll As On Report
                    </div>
                    <div class="panel-body">
                       
                            <div class="form-group">
                                <div class="col-md-2">
                                    <label style="margin-right: 20PX;">Branch</label>
                                    <asp:DropDownList ID="cmbx_Branch" runat="server" CssClass="form-control" placeholder="Select Branch" Font-Size="10">
                                </asp:DropDownList>
                                </div>

                                <div class="col-md-2">
                                    <label style="margin-right: 20PX;">Date:</label>
                                     <asp:TextBox ID="txt_dailyscroll" runat="server" Font-Size="10" placeholder="dd/MM/yyyy" CssClass="form-control input-sm BootDatepicker"></asp:TextBox>
                                     </div>
                              
                                <div class="col-md-2"><br />
                                  <asp:Button ID="btnShow" runat="server" Text="Show" class="btn btn-primary" OnClick="btnShow_Click" />
                                    </div>
                                    <div  class="col-md-2"><br />
                                                                 
                                    <asp:DropDownList ID="ddlExportReport" runat="server" CssClass="form-control" class="styled-select" Font-Size="10" BackColor="White" ForeColor="Black">
                                        <asp:ListItem Value="0">WORD</asp:ListItem>
                                        <asp:ListItem Value="1">PDF</asp:ListItem>
                                        <asp:ListItem Value="2">EXCEL</asp:ListItem>
                                    </asp:DropDownList>

                                </div>
                                
                                <div class="col-md-2"><br />
                                  <asp:Button ID="btnDownload" runat="server" Text="Download" class="btn btn-primary" OnClick="btnDownload_Click" />
                                </div>
                               
                                
                             
                               
                            </div>
                            
                        </div>
                  
                </div>
            </div>
     
         <asp:Panel ID="DSRPanel" runat="server" Visible="false">
         <div class="row">
            <div class="col-lg-12">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                     Daily Scroll As On           
                    </div>
           
                    <div class="panel-body">
                         <div class="col-md-12" style="width: 100%; height: 400px; overflow: scroll">
                        <asp:GridView ID="gv_dailyscroll" runat="server" CssClass="form-control" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="1066px">
                            <Columns>
                                <asp:TemplateField HeaderText="Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_ccbdate" runat="server" Text='<%# Bind("ccb_date","{0:dd/MM/yyyy}") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="LedgerNo">
                                     <ItemTemplate>
                                        <asp:Label ID="lbl_ldgcode" runat="server" Text='<%# Bind("ldg_code") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Main AcNo">
                                     <ItemTemplate>
                                        <asp:Label ID="lbl_oldacno" runat="server" Text='<%# Bind("old_acno") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Sys AcNo">
                                     <ItemTemplate>
                                        <asp:Label ID="lbl_oldacno" runat="server" Text='<%# Bind("sl_code") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Voucher No">
                                     <ItemTemplate>
                                        <asp:Label ID="lbl_oldacno" runat="server" Text='<%# Bind("voucher_no") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Receipt">
                                     <ItemTemplate>
                                        <asp:Label ID="lbl_oldacno" runat="server" Text='<%# Bind("AMT_CREDIT") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Payment">
                                     <ItemTemplate>
                                        <asp:Label ID="lbl_oldacno" runat="server" Text='<%# Bind("AMT_DEBIT") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="InstrumentType">
                                     <ItemTemplate>
                                        <asp:Label ID="lbl_oldacno" runat="server" Text='<%# Bind("TYPEOFINSTRUMENT") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Instrument No">
                                     <ItemTemplate>
                                        <asp:Label ID="lbl_oldacno" runat="server" Text='<%# Bind("INSTRUMENTNO") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Posted By">
                                     <ItemTemplate>
                                        <asp:Label ID="lbl_oldacno" runat="server" Text='<%# Bind("employeeName") %>'></asp:Label>
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
