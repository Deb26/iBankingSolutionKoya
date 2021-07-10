<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/ProjectBM.Master" AutoEventWireup="true" CodeBehind="frmBankLedgerBalanceUpdate.aspx.cs" Inherits="iBankingSolution.Transaction.frmBankLedgerBalanceUpdate" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="scrpt1" runat="server"></asp:ScriptManager><br />
           <div class="col-lg-13">
            <div class="panel panel-primary">
                <div class="panel-heading" style="font-size:larger;">
                   Branch Ledger Balance Update          
                </div>

                    <%-- <div class="col-lg-7">
                          <div style="float: right; margin-top: 12px;">
                              <label style="margin-right: 20PX;font-size:larger;">Select Branch Name</label>
                              <asp:DropDownList ID="cmbx_Branch" runat="server" CssClass="form-control" placeholder="Select Branch Name" Font-Size="10">
                                </asp:DropDownList>
                              </div>
                      </div>
                    <br /><br />--%>
                
                <div>
                    
                    <div class="panel-body">
                        <div style ="height:390px; width:1230px; overflow:auto;">
                        <asp:GridView ID="gridActs" runat="server" AutoGenerateColumns="False"  CellPadding="3" Height="198px" Width="1924px" ForeColor="#333333" CssClass="table table-bordered table-striped table-hover"
                                            EmptyDataText="No Data Found" GridLines="Both">
                             <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <Columns>
                                 <asp:TemplateField HeaderText="LEDGER CODE" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <%--<asp:Label ID="lblSocietyCode" runat="server" Text='<%# Eval("SocietyCode") %>'></asp:Label>--%>
                                            <asp:TextBox ID="txtldgcode" runat="server" Text='<%# Bind("LDG_CODE") %>' BorderStyle="None" Enabled="false" Width="100px"></asp:TextBox>
                                        </ItemTemplate>
                                     <ItemStyle HorizontalAlign="Left" />
                                     <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="LEDGER NAME " HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <%--<asp:Label ID="lblSocietyACNo" runat="server" Text='<%# Eval("SocietyACNo") %>'></asp:Label>--%>
                                            <asp:TextBox ID="txtnomenclature" runat="server" Text='<%# Bind("NOMENCLATURE") %>' BorderStyle="None" Width="600px" Enabled="false"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                                     <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ACT_OP_CR"  HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <%--<asp:Label ID="lblSenderCBSAcNo" runat="server" Text='<%# Eval("SenderCBSAcNo") %>'></asp:Label>--%>
                                             <asp:TextBox ID="txtactopcr" runat="server" Text='<%# Bind("ACT_OP_CR") %>' BorderStyle="None" Width="100px"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                                     <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ACT_OP_DR"  HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <%--<asp:Label ID="lblSenderName" runat="server" Text='<%# Eval("SenderName") %>'></asp:Label>--%>
                                             <asp:TextBox ID="txtactopdr" runat="server" Text='<%# Bind("ACT_OP_DR") %>' BorderStyle="None" Width="100px"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                                     <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Branch ID"  HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <%--<asp:Label ID="lblSenderName" runat="server" Text='<%# Eval("SenderName") %>'></asp:Label>--%>
                                             <asp:TextBox ID="txtbranchid" runat="server"  BorderStyle="None"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                                     <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                   
                                </Columns>
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
                        <%--<asp:Button ID="btnSave" runat="server" Text="Save" CssClass="myButton" ValidationGroup="ValidEntry"
                                    OnClick="btnSave_Click" CausesValidation="False" /><br />
                                    <asp:Label 
                                            ID="lblmessage" runat="server" Visible="False" Font-Bold="True" 
                                            Font-Italic="True" ForeColor="#990000" Font-Size="Medium"></asp:Label>OnClick="btnSave_Click"--%>
                        
                        
                        </div>
                    </div>
                </div>
            </div>
               <div class="col-lg-7">
                          <div style="float: right; margin-top: 12px;">
                                 <asp:Button ID="btnUpdate" runat="server" Text="Update" class="btn btn-success" OnClick="click_btn_Update"/>
                                    <a href="frmBankLedgerBalanceUpdate.aspx" class="btn btn-outline btn-danger">Reset</a>
                              </div>
                            </div>
</asp:Content>
