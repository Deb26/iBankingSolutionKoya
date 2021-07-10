<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/ProjectBM.Master" CodeBehind="frmLoanSecKVPNSC.aspx.cs" Inherits="iBankingSolution.Report.frmLoanSecKVPNSC" %>

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
            <h1 class="page-header">Detail List Security</h1>

        </div>

    </div>
    <div class="row">
        <div class="col-lg-6">
            <div class="panel panel-primary">
                <div class="panel-heading">
                  Detail List Security 
                </div>
                <div class="panel-body">
                    <div class="row">

                        <div class="form-group">
                            <div class="col-lg-6">
                                <asp:DropDownList ID="cmbx_slcode" runat="server" AutoPostBack="True" CssClass="form-control" Font-Size="10" OnSelectedIndexChanged="cmbx_slcode_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                            <div class="col-lg-3">
                                <asp:Button ID="btnshow" Text="Download" runat="server" CssClass="form-control" OnClick="btnshow_Click"   Style="color: white;background: linear-gradient(135deg, green 30%, Yellow 100%);"/>
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
                    Loan Security Details
                </div>
                <div class="panel-body">
                    <div class="row">
                               <div class="col-md-12" style="width: 1100px; height: 200px; overflow: scroll">

                                   <asp:GridView ID="GV_KVPNSC" runat="server" AutoGenerateColumns="False"  CssClass="form-control" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3">
                                       <Columns>
                                           <asp:TemplateField HeaderText="A/C No">
                                               <ItemTemplate>
                                                   <asp:Label ID="lblslcode" runat="server" Text='<%# Bind("SL_CODE") %>'></asp:Label>
                                               </ItemTemplate>
                                           </asp:TemplateField>
                                           
                                           <asp:TemplateField HeaderText="Old A/C No">
                                                <ItemTemplate>
                                                   <asp:Label ID="lbloldacno" runat="server" Text='<%# Bind("OLD_ACNO") %>'></asp:Label>
                                               </ItemTemplate>
                                           </asp:TemplateField>
                                           
                                           <asp:TemplateField HeaderText="Name">
                                                <ItemTemplate>
                                                   <asp:Label ID="lblname" runat="server" Text='<%# Bind("NAME") %>'></asp:Label>
                                               </ItemTemplate>
                                           </asp:TemplateField>
                                           
                                           <asp:TemplateField HeaderText="Loan Type">
                                                <ItemTemplate>
                                                   <asp:Label ID="lblloantype" runat="server" Text='<%# Bind("Sec_Type") %>'></asp:Label>
                                               </ItemTemplate>
                                           </asp:TemplateField>
                                           
                                           <asp:TemplateField HeaderText="Security Type">
                                                <ItemTemplate>
                                                   <asp:Label ID="lblsectype" runat="server" Text='<%# Bind("[TYPE]") %>'></asp:Label>
                                               </ItemTemplate>
                                           </asp:TemplateField>
                                           
                                           <asp:TemplateField HeaderText="Value of Secu">
                                                <ItemTemplate>
                                                   <asp:Label ID="lblsecvalue" runat="server" Text='<%# Bind("FACE_VAL") %>'></asp:Label>
                                               </ItemTemplate>
                                           </asp:TemplateField>
                                           
                                           <asp:TemplateField HeaderText="Certificate No">
                                                <ItemTemplate>
                                                   <asp:Label ID="lblcertno" runat="server" Text='<%# Bind("CERT_NO") %>'></asp:Label>
                                               </ItemTemplate>
                                           </asp:TemplateField>
                                           
                                           <asp:TemplateField HeaderText="Issuing Office">
                                                <ItemTemplate>
                                                   <asp:Label ID="lblissueoffice" runat="server" Text='<%# Bind("ISS_OFF") %>'></asp:Label>
                                               </ItemTemplate>
                                           </asp:TemplateField>
                                           
                                           <asp:TemplateField HeaderText="Pledge Date">
                                                <ItemTemplate>
                                                   <asp:Label ID="lblpledgedt" runat="server" Text='<%# Bind("PLEDG_DATE","{0:dd/MM/yyyy}") %>'></asp:Label>
                                               </ItemTemplate>
                                           </asp:TemplateField>
                                           
                                           <asp:TemplateField HeaderText="Maturity Date">
                                                <ItemTemplate>
                                                   <asp:Label ID="lblmatdate" runat="server" Text='<%# Bind("MAT_DT","{0:dd/MM/yyyy}") %>'></asp:Label>
                                               </ItemTemplate>
                                           </asp:TemplateField>
                                           
                                           <asp:TemplateField HeaderText="FD AcNo">
                                                <ItemTemplate>
                                                   <asp:Label ID="lblfdno" runat="server" Text='<%# Bind("FD_ACC_NO") %>'></asp:Label>
                                               </ItemTemplate>
                                           </asp:TemplateField>
                                           
                                           <asp:TemplateField HeaderText="Maturity Value">
                                                <ItemTemplate>
                                                   <asp:Label ID="lblmatvalue" runat="server" Text='<%# Bind("MaturityAmt","{0:dd/MM/yyyy}") %>'></asp:Label>
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
    
</asp:Content>
