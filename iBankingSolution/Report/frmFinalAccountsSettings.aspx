<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/ProjectBM.Master" CodeBehind="frmFinalAccountsSettings.aspx.cs" Inherits="iBankingSolution.Report.frmFinalAccountsSettings" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">


    <asp:ScriptManager ID="scrpt1" runat="server" EnablePageMethods="true"></asp:ScriptManager>
    <div class="row">
        <div class="col-lg-12">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    Final Accounts Settings
                </div>
                <div class="panel-body">
                    <div class="row">

                        <div class="form-group">

                            <div class="clearfix"></div>
                            <div class="col-md-2">
                                <label style="margin-right: 20PX;">Select Report Type</label>

                                <asp:DropDownList ID="cmbx_ReportType" runat="server" CssClass="form-control" Font-Size="10" AutoPostBack="True" OnSelectedIndexChanged="cmbx_ReportType_SelectedIndexChanged" >  <%----%>
                                    <asp:ListItem Selected="True" Value="0">-- Select Report Type --</asp:ListItem>
                                    <asp:ListItem Value="BS">Balance Sheet</asp:ListItem>
                                    <asp:ListItem Value="PL">Profit &amp; Losss</asp:ListItem>
                                    <asp:ListItem Value="TR">Trading Account</asp:ListItem>


                                </asp:DropDownList>

                            </div>

                            <br />
                         

                            <div class="col-md-8">
                                <label style="margin-right: 20PX;"></label>
                                <%--<asp:Button ID="btnAddNew" runat="server" Text="Add New Record" class="btn btn-primary" OnClick="btnAddNew_Click" />--%>
                                <asp:Button ID="btnSave" runat="server" Text="Save Changes" class="btn btn-primary" OnClick="btnSave_Click" />
                                <%--<asp:Button ID="btnCancel" runat="server" Text="Cancel Changes" class="btn btn-primary" OnClick="btnCancel_Click" />--%>

                            </div>
                            <div class="clearfix"></div>

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
                    <asp:Label ID="Label2" runat="server" Text="Settings"></asp:Label>
                </div>
                <div class="panel-body">

                    <div style="overflow: scroll;">


                        <asp:GridView ID="gridSettings" runat="server" AutoGenerateColumns="False" EmptyDataText="No Data Found" DataKeyNames="R_ID" OnRowCommand="gridSettings_RowCommand" OnRowDeleting="gridSettings_RowDeleting" BackColor="#CCCCCC" BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" CellSpacing="2" ForeColor="Black" Width="1276px">
                            <Columns>
                                <asp:BoundField DataField="LSL" HeaderText="Sl" HeaderStyle-Width="100px">
                                    <HeaderStyle Width="100px"></HeaderStyle>
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Head Name">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtLhead" runat="server" Text='<%# Bind("LSL_HEAD") %>' BorderStyle="None"></asp:TextBox>
                                    </ItemTemplate>

                                    <HeaderStyle Width="80px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Amount">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtLamt" runat="server" Text='<%# Bind("LAMT") %>' BorderStyle="None"></asp:TextBox>
                                    </ItemTemplate>

                                    <HeaderStyle Width="50px" />
                                    <ItemStyle Width="50px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Curr Year">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtLcur" runat="server" Text='<%# Bind("LCURR") %>' BorderStyle="None"></asp:TextBox>
                                    </ItemTemplate>

                                    <HeaderStyle Width="80px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Prev Year">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtLpre" runat="server" Text='<%# Bind("LPRE") %>' BorderStyle="None"></asp:TextBox>
                                    </ItemTemplate>

                                    <HeaderStyle Width="80px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sl">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtRsl" runat="server" Text='<%# Bind("RSL") %>' BorderStyle="None"></asp:TextBox>
                                    </ItemTemplate>

                                    <HeaderStyle Width="80px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Head Name">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtRhead" runat="server" Text='<%# Bind("RSL_HEAD") %>' BorderStyle="None"></asp:TextBox>
                                    </ItemTemplate>

                                    <HeaderStyle Width="100px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Amount">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtRamt" runat="server" Text='<%# Bind("RAMT") %>' BorderStyle="None"></asp:TextBox>
                                    </ItemTemplate>

                                    <HeaderStyle Width="80px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Curr Year">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtRCurr" runat="server" Text='<%# Bind("RCURR") %>' BorderStyle="None"></asp:TextBox>
                                    </ItemTemplate>

                                    <HeaderStyle Width="80px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Prev Year">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtRpre" runat="server" Text='<%# Bind("RPRE") %>' BorderStyle="None"></asp:TextBox>
                                    </ItemTemplate>

                                    <HeaderStyle Width="80px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Actions">
                                    <ItemTemplate>
                                  
                                        <%--<asp:CheckBox  runat="server" CommandName="Select" CommandArgument="<%# Container.DataItemIndex %>" />--%>
                                       <%-- <asp:Button ID="btnSelect" runat="server" Text="Insert" CommandName="Select" CommandArgument="<%# Container.DataItemIndex %>" />--%>
                                     <%--   <asp:Button ID="btnDelete" runat="server" Text="Delete" CommandName="Delete" CommandArgument="<%# Container.DataItemIndex %>" />--%>
                                        <asp:ImageButton ID="btnSelect" runat="server" Text="Insert" CommandName="Select" CommandArgument="<%# Container.DataItemIndex %>" ImageUrl="~/Images/AddRow.png"/> 
                                          <asp:ImageButton ID="btnDelete" runat="server" Text="Delete" CommandName="Delete" CommandArgument="<%# Container.DataItemIndex %>" ImageUrl="~/Images/delete.png"/> 
                                    </ItemTemplate>
                                    <HeaderStyle Width="80px" />
                                </asp:TemplateField>
                            </Columns>
                             
                            <FooterStyle BackColor="#CCCCCC" />
                            <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" />
                            <RowStyle BackColor="White" />
                            <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                            <SortedAscendingCellStyle BackColor="#F1F1F1" />
                            <SortedAscendingHeaderStyle BackColor="#808080" />
                            <SortedDescendingCellStyle BackColor="#CAC9C9" />
                            <SortedDescendingHeaderStyle BackColor="#383838" />
                             
                        </asp:GridView>


                    </div>
                </div>
            </div>
        </div>
    </div>
    <%-- Scripting Section --%>
</asp:Content>