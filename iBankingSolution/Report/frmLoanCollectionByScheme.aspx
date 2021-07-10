<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/ProjectBM.Master" CodeBehind="frmLoanCollectionByScheme.aspx.cs" Inherits="iBankingSolution.Report.frmLoanCollectionByScheme" %>


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
            <h1 class="page-header">Loan Collection Report Scheme Wise</h1>

        </div>

    </div>
    <div class="row">
        <div class="col-lg-12">
            <div class="panel panel-primary">
                <div class="panel-heading">
                 Loan Collection Report Scheme Wise
                </div>
                <div class="panel-body">
                    <div class="row">

                        <div class="form-group">
                            <div class="col-lg-6">
                                <asp:Label ID="lblschemecode" runat="server">Choose Scheme</asp:Label>
                                <asp:DropDownList ID="cmbx_schemecode" runat="server" AutoPostBack="True" CssClass="form-control" Font-Size="10" OnSelectedIndexChanged="cmbx_schemecode_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                             <div class="col-lg-3">
                                 <asp:Label ID="lblformdt" runat="server">Form Date</asp:Label>
                                <asp:TextBox ID="txtformdate"  runat="server" placeholder="dd/MM/yyyy" CssClass="form-control input-sm BootDatepicker"></asp:TextBox>
                            </div>
                             <div class="col-lg-3">
                                 <asp:Label ID="lbltodate" runat="server">To Date</asp:Label>
                              <asp:TextBox ID="txttodate" runat="server" placeholder="dd/MM/yyyy" CssClass="form-control input-sm BootDatepicker"></asp:TextBox>
                                 <br />
                            </div>
                            <div class="col-lg-4">
                                 <asp:Button ID="btnshow" Text="SHOW" runat="server" CssClass="form-control" Style="color: white;background: linear-gradient(135deg, green 30%,  red 100%);" OnClick="btnshow_Click" />
                            </div>
                            <div class="col-lg-3">
                               
                            </div>
                            <div class="col-lg-4">
                                 <asp:Button ID="btndownload" Text="Download" runat="server" CssClass="form-control" Style="color: white;background: linear-gradient(135deg, green 30%, Yellow 100%);" OnClick="btndownload_Click" />
                            </div>
                        </div>
                        <div class="clearfix">
                        </div>
                    </div>

                </div>
            </div>

        </div>
    </div>


    <div class="row">
        <div class="col-lg-12">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    Loan Collection Report Scheme Wise
                </div>
                <div class="panel-body">
                    <div class="row">
                               <div class="col-md-12" style="width: 1100px; height: 350px; overflow: scroll">

                                   <asp:GridView ID="GV_LOANSCHEMEWISE" runat="server" CssClass="form-control" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3">
                                       <Columns>
                                           <asp:TemplateField HeaderText="Benefisiary Name">
                                               <ItemTemplate>
                                                   <asp:Label ID="lblcustid" runat="server" Text='<%# Bind("NAME") %>'></asp:Label>
                                               </ItemTemplate>
                                           </asp:TemplateField>
                                           
                                           <asp:TemplateField HeaderText="A/C No">
                                               <ItemTemplate>
                                                   <asp:Label ID="lblacno" runat="server" Text='<%# Bind("SL_CODE") %>'></asp:Label>
                                               </ItemTemplate>
                                           </asp:TemplateField>
                                          
                                            <asp:TemplateField HeaderText="LoanCaseNo">
                                                <ItemTemplate>
                                                   <asp:Label ID="lblloancaseno" runat="server" Text='<%# Bind("LOAN_CASE_NO") %>'></asp:Label>
                                               </ItemTemplate>
                                            </asp:TemplateField>
                                           
                                           <asp:TemplateField HeaderText="LoanAmount">
                                               <ItemTemplate>
                                                   <asp:Label ID="lblloanamt" runat="server" Text='<%# Bind("CASH") %>'></asp:Label>
                                               </ItemTemplate>
                                           </asp:TemplateField>
                                           
                                           <asp:TemplateField HeaderText="First Disb Dt ">
                                               <ItemTemplate>
                                                   <asp:Label ID="lbldisbdt" runat="server" Text='<%# Bind("FIRSTDISBDATE") %>'></asp:Label>
                                               </ItemTemplate>
                                           </asp:TemplateField>
                                           
                                           <asp:TemplateField HeaderText="Curr Prin">
                                               <ItemTemplate>
                                                   <asp:Label ID="lblcurrprin" runat="server" Text='<%# Bind("PRIN_CURR") %>'></asp:Label>
                                               </ItemTemplate>
                                           </asp:TemplateField>
                                           
                                           <asp:TemplateField HeaderText="OD Prin">
                                               <ItemTemplate>
                                                   <asp:Label ID="lblodprin" runat="server" Text='<%# Bind("PRIN_OD") %>'></asp:Label>
                                               </ItemTemplate>
                                           </asp:TemplateField>
                                           
                                           <asp:TemplateField HeaderText="Curr. Int">
                                               <ItemTemplate>
                                                   <asp:Label ID="lblcurrprin" runat="server" Text='<%# Bind("INT_CURR") %>'></asp:Label>
                                               </ItemTemplate>
                                           </asp:TemplateField>
                                           
                                           <asp:TemplateField HeaderText="OD Int">
                                               <ItemTemplate>
                                                   <asp:Label ID="lblodprin" runat="server" Text='<%# Bind("INT_OD") %>'></asp:Label>
                                               </ItemTemplate>
                                           </asp:TemplateField>
                                           
                                         
                                           
                                        
                                           
                                           <asp:TemplateField HeaderText="Pen Int">
                                               <ItemTemplate>
                                                   <asp:Label ID="lblpenint" runat="server" Text='<%# Bind("INT_PEN") %>'></asp:Label>
                                               </ItemTemplate>
                                           </asp:TemplateField>
                                           
                                           <asp:TemplateField HeaderText="Adv Prin.">
                                               <ItemTemplate>
                                                   <asp:Label ID="lbladvint" runat="server" Text='<%# Bind("PRIN_ADV") %>'></asp:Label>
                                               </ItemTemplate>
                                           </asp:TemplateField>
                                           
                                           <asp:TemplateField HeaderText="Total">
                                               <ItemTemplate>
                                                   <asp:Label ID="lbltotal" runat="server" Text='<%# Bind("TOTAL") %>'></asp:Label>
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