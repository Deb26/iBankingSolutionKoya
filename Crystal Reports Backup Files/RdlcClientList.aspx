<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/ProjectBM.Master" CodeBehind="RdlcClientList.aspx.cs" Inherits="iBankingSolution.Report.RdlcClientList" %>


<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <form id="formInitialSetting" runat="server">
        <asp:ScriptManager ID="scrpt1" runat="server" EnablePageMethods="true"></asp:ScriptManager>
        <div class="row" align="center">
            <div class="col-lg-12">
                <table align="center">
                    <tr>
                        <td colspan="4">
                            <h4>Client Report</h4>
                        </td>
                    </tr>
                    <tr>
                        <td>CustID:</td>
                        <td colspan="4" cssc>
                            <asp:TextBox ID="txtCustID" runat="server" CssClass="form-control" AutoPostBack="True" OnTextChanged="txtCustID_TextChanged"></asp:TextBox></td>

                    </tr>

                    <tr>

                        <td>
                            <asp:Button ID="btnshow" runat="server" Text="ShowReport" CssClass="form-control" OnClick="btnshow_Click" />
                        </td>
                        <td>
                            <asp:Button ID="btnReset" runat="server" Text="Reset"  CssClass="form-control"/>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlExportReport" runat="server" CssClass="form-control" class="styled-select" Width="75px" BackColor="White" ForeColor="Black">
                                <asp:ListItem Value="0">WORD</asp:ListItem>
                                <asp:ListItem Value="1">PDF</asp:ListItem>
                                <asp:ListItem Value="2">EXCEL</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:Button ID="btnDownload" runat="server" Text="Download" CssClass="form-control" OnClick="btnDownload_Click" />
                        </td>

                    </tr>






                </table>
                <div class="panel-body">
                    <div class="row">

                        <div class="form-group">
                            <div class="col-md-12" onscroll="true" style="overflow: scroll">
                                <asp:GridView ID="GridClientList" runat="server" AllowPaging="True" CssClass="table table-bordered table-striped table-hover" EmptyDataText="No Data Found" GridLines="Horizontal" AllowSorting="True" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" OnPageIndexChanging="GridClientList_PageIndexChanging" ShowFooter="True" AutoGenerateColumns="False" ForeColor="Black">
                                    <Columns>
                                        <asp:TemplateField HeaderText="NAME">
                                            <ItemTemplate>
                                                <asp:Label ID="Label1" runat="server" Text='<%#Eval("NAME")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="GUARDIAN_NAME">
                                            <ItemTemplate>
                                                <asp:Label ID="Label2" runat="server" Text='<%# Eval("GUARDIAN_NAME") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="POST">
                                            <ItemTemplate>
                                                <asp:Label ID="Label3" runat="server" Text='<%# Eval("Post") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="BLOCK">
                                            <ItemTemplate>
                                                <asp:Label ID="Label4" runat="server" Text='<%# Eval("Block") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="DISTRICT">
                                            <ItemTemplate>
                                                <asp:Label ID="Label5" runat="server" Text='<%# Eval("District") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="AGE">
                                            <ItemTemplate>
                                                <asp:Label ID="Label6" runat="server" Text='<%# Eval("Age") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="DOB">
                                            <ItemTemplate>
                                                <asp:Label ID="Label7" runat="server" Text='<%# Eval("DOB") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ADHAR CARD">
                                            <ItemTemplate>
                                                <asp:Label ID="Label8" runat="server" Text='<%# Eval("AdharCard") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                    <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                                    <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                                    <SortedAscendingCellStyle BackColor="#F7F7F7" />
                                    <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                                    <SortedDescendingCellStyle BackColor="#E5E5E5" />
                                    <SortedDescendingHeaderStyle BackColor="#242121" />
                                </asp:GridView>

                            </div>
                        </div>
                    </div>
                </div>
                <br />
            </div>

        </div>
    </form>
</asp:Content>
