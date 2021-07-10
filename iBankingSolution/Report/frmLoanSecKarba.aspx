<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/ProjectBM.Master" CodeBehind="frmLoanSecKarba.aspx.cs" Inherits="iBankingSolution.Report.frmLoanSecKarba" %>


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
            <h1 class="page-header">Loan Security Details</h1>

        </div>

    </div>
    <div class="row">
        <div class="col-lg-6">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    Loan Security Details
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
                        <asp:GridView ID="gv_LoanSecKarba" runat="server" AutoGenerateColumns="False" CssClass="form-control" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3">
                            <Columns>
                                <asp:TemplateField HeaderText="SlNo">
                                     <ItemTemplate>
                                        <asp:Label ID="lblslcode" runat="server" Text='<%# (((GridViewRow)Container).RowIndex+1).ToString()%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="CompAcNo">
                                    <ItemTemplate>
                                        <asp:Label ID="lblslcode" runat="server" Text='<%# Bind("SL_CODE") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="L/FNo">
                                     <ItemTemplate>
                                        <asp:Label ID="lblslcode" runat="server" Text='<%# Bind("lf_no") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="LoaneName">
                                     <ItemTemplate>
                                        <asp:Label ID="lblslcode" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="CreditLimitValue">
                                     <ItemTemplate>
                                        <asp:Label ID="lblslcode" runat="server" Text='<%# Bind("CREDIT_LIMIT_VALUE") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="CreditLimitDt">
                                     <ItemTemplate>
                                        <asp:Label ID="lblslcode" runat="server" Text='<%# Bind("K_VALID","{0:dd/MM/yyyy}") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="Value Of Sec">
                                     <ItemTemplate>
                                        <asp:Label ID="lblslcode" runat="server" Text='<%# Bind("K_VALUE") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="Amt.OfLoanIssued">
                                     <ItemTemplate>
                                        <asp:Label ID="lblslcode" runat="server" Text='<%# Bind("DISB_AMNT") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="KarbanamaNo">
                                     <ItemTemplate>
                                        <asp:Label ID="lblslcode" runat="server" Text='<%# Bind("K_NO") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="KarbanamaDt">
                                     <ItemTemplate>
                                        <asp:Label ID="lblslcode" runat="server" Text='<%# Bind("K_DATE","{0:dd/MM/yyyy}") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="KarbaValidUpto">
                                     <ItemTemplate>
                                        <asp:Label ID="lblslcode" runat="server" Text='<%# Bind("K_UPTO","{0:dd/MM/yyyy}") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="TotalLandDeci">
                                     <ItemTemplate>
                                        <asp:Label ID="lblslcode" runat="server" Text='<%# Bind("K_ACRE") %>'></asp:Label>
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
