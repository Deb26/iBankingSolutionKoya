<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/ProjectBM.Master" CodeBehind="frmPassbookPrint.aspx.cs" Inherits="iBankingSolution.Report.frmPassbookPrint" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <asp:ScriptManager runat="server" id="scr1"></asp:ScriptManager>
       
        <div class="row">
            <div class="col-lg-12">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        Passbook Print
                    </div>
                    <div class="panel-body">
                        <div class="row">

                            <div class="form-group">

                                <%--  <div class="col-md-6" visible="false">
                                    <label style="margin-right: 20PX;">Select Branch</label>
                                    
                                       <asp:DropDownList ID="cmbx_Branch" runat="server" CssClass="form-control"> </asp:DropDownList>
                                </div>--%>
                                <div class="col-md-3">
                                    <label style="margin-right: 20PX;">Old A/C No.</label>
                                  <asp:TextBox ID="txtOldAcNo" CssClass="form-control" placeholder="Old A/C No" runat="server" onFocus="this.select()" autocomplete="off" required="required" OnTextChanged="txtOldAcNo_TextChanged" AutoPostBack="True"></asp:TextBox>
                                </div>
 
                               
                                <div class="col-md-3">
                                    <label style="margin-right: 20PX;">New A/C No</label>
                                    <asp:TextBox ID="txtNewAcNo" CssClass="form-control" placeholder="New A/c NO" runat="server" onFocus="this.select()" autocomplete="off" required="required" OnTextChanged="txtNewAcNo_TextChanged" AutoPostBack="True"></asp:TextBox>
                                </div>

                                 <div class="col-md-3">
                                    <label style="margin-right: 20PX;">Print License</label>
                                      <asp:Button ID="btnPrintLicense" runat="server" Text="Print License" class="btn btn-primary" OnClick="btnPrintLicense_Click" />
                  
                                </div>
                                  <div class="col-md-3">
                                    <label style="margin-right: 20PX;">Print Passbook</label>
                                      <asp:Button ID="btnPrintPassbook" runat="server" Text="Print Passbook" class="btn btn-primary" OnClick="btnPrintPassbook_Click" />
                  
                                </div>

                                 <div class="clearfix"></div>
                               
    
                                <hr />
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="row" runat="server" id="PABK" align="center">
            <div class="col-lg-12">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                      Passbook             
                    </div>

                    <asp:GridView ID="gridPassBk" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" OnRowDataBound="gridPassBk_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="SLNo">
                                
                                <ItemTemplate>
                                    <asp:Label ID="lblSlNo" runat="server" Text='<%# Eval("LineNum") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="DATE">
                               
                                <ItemTemplate>
                                    <asp:Label ID="lblTransDate" runat="server" Text='<%# Eval("DT","{0:dd/MM/yyyy}") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="NARRATION">
                                
                                <ItemTemplate>
                                    <asp:Label ID="lblNaration" runat="server" Text='<%# Eval("NARRATION") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="WITHDRAWALS">
                                 <ItemTemplate>
                                    <asp:Label ID="lblDebit" runat="server" Text='<%# Eval("DEBIT") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="DEPOSIT">
                                 
                                <ItemTemplate>
                                    <asp:Label ID="lblCredit" runat="server"  Text='<%# Eval("CREDIT") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="BALANCE">
                               
                               
                                <ItemTemplate>
                                    <asp:Label ID="lblBalance" runat="server" Text='<%# Eval("BALANCE") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField />
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

                    <asp:GridView ID="GridView1" runat="server"></asp:GridView>
                        <%--<CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="True"/>--%>  
                           
                   
        </div>
        
                </div>
            </div>
   
 

   
</asp:Content>
