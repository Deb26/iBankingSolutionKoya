<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/ProjectBM.Master" AutoEventWireup="true" CodeBehind="frmNEFTRTGSNEXT.aspx.cs" Inherits="iBankingSolution.Transaction.frmNEFTRTGSNEXT" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="scrpt1" runat="server"></asp:ScriptManager>
           <div class="col-lg-13">
            <div class="panel panel-primary">
                <div class="panel-heading" style="font-size:larger;">
                   NEFT/RTGS          
                </div>
                <div class="col-md-2">
                                    <asp:TextBox ID="txttdt" runat="server" CssClass="form-control"  Font-Size="10" autocomplete="off" visible="false"
                                      ></asp:TextBox>
                </div>
                <div>
                    <div class="panel-body">


                        <%--<div style="overflow: scroll;" runat="server" id="RepeaterControls">--%>
                         <%--<div style ="height:350px; width:1230px; overflow:auto;">
                            <table width="100%" class="table table-striped table-bordered table-hover" id="dataTablesexample">
                                <thead>
                                    <tr>
                                        <th>SocietyCode</th>
                                        <th>TSocietyACNo </th>
                                        <th>SenderCBS AcNo</th>
                                        <th>SenderName</th>
                                        <th>Amount</th>
                                        <th>Benficiary Name</th>
                                        <th>Benficiary AcNo</th>
                                        <th>IFSCCode</th>
                                        <th>BankName</th>
                                        <th>BranchName</th>
                                        <th>Sender Address</th>
                                        <th>BenificiaryAddress</th>
                                        <th>MobileNo</th>
                                        <th>Remarks</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="RepCCList" runat="server">
                                        <ItemTemplate>
                                            <tr>
                                                
                                                <td>

                                              
                                                    <asp:Label ID="lblAcNo" runat="server" Text='<%# Eval("SocietyCode")%>'></asp:Label>

                                                </td>

                                                <td>

                                                    <asp:Label ID="lblold_acno" runat="server" Text='<%# Eval("SocietyACNo")%>'></asp:Label>

                                                </td>
                                                <td>

                                                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("SenderCBSAcNo")%>'></asp:Label>

                                                </td>
                                                <td>

                                                    <asp:Label ID="lblacctype" runat="server" Text='<%# Eval("SenderName")%>'></asp:Label>

                                                </td>
                                                <td>

                                                <asp:Label ID="lbldateofopening" runat="server" Text='<%# Eval("Amount")%>'></asp:Label>

                                               </td>
                                                <td>

                                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("Benf_Name")%>'></asp:Label>

                                               </td>
                                                <td>

                                                <asp:Label ID="Label3" runat="server" Text='<%# Eval("Benf_AcNo")%>'></asp:Label>

                                               </td>
                                                <td>

                                                <asp:Label ID="Label4" runat="server" Text='<%# Eval("IFSCCode")%>'></asp:Label>

                                               </td>
                                                <td>

                                                <asp:Label ID="Label5" runat="server" Text='<%# Eval("BankName")%>'></asp:Label>

                                               </td>
                                                <td>

                                                <asp:Label ID="Label6" runat="server" Text='<%# Eval("BranchName")%>'></asp:Label>

                                               </td>
                                                <td>

                                                <asp:Label ID="Label7" runat="server" Text='<%# Eval("SenderAddress")%>'></asp:Label>

                                               </td>
                                                <td>

                                                <asp:Label ID="Label8" runat="server" Text='<%# Eval("BenificiaryAddress")%>'></asp:Label>

                                               </td>
                                                <td>

                                                <asp:Label ID="Label9" runat="server" Text='<%# Eval("MobileNo")%>'></asp:Label>

                                               </td>
                                                <td>

                                                 <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control"  Font-Size="10" autocomplete="off"></asp:TextBox>
                                                  

                                               </td>

                                            
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>

                                </tbody>
                            </table>
                        </div>--%>
                        <div style ="height:350px; width:1250px; overflow:auto;">
                        <asp:GridView ID="gridActs" runat="server" AutoGenerateColumns="False"  CellPadding="3"  ForeColor="#333333" CssClass="table table-bordered table-striped table-hover"
                                            EmptyDataText="No Data Found" GridLines="Both"
                                  onrowdeleting="gridActs_RowDeleting" 
                                onselectedindexchanged="gridActs_SelectedIndexChanged">
                             <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <Columns>
                                 <asp:TemplateField HeaderText="SocietyCode " HeaderStyle-HorizontalAlign="Left" Visible="false">
                                        <ItemTemplate>
                                            <%--<asp:Label ID="lblSocietyCode" runat="server" Text='<%# Eval("SocietyCode") %>'></asp:Label>--%>
                                            <asp:TextBox ID="txtscode" runat="server" Text='<%# Bind("SocietyCode") %>' BorderStyle="None" Visible="false"></asp:TextBox>
                                        </ItemTemplate>
                                     <ItemStyle HorizontalAlign="Left" />
                                     <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="SocietyACNo" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <%--<asp:Label ID="lblSocietyACNo" runat="server" Text='<%# Eval("SocietyACNo") %>'></asp:Label>--%>
                                            <asp:TextBox ID="txtsACNO" runat="server" Text='<%# Bind("SocietyACNo") %>' BorderStyle="None" Width="100px" Enabled="false"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                     <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="SenderCBS AcNo" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <%--<asp:Label ID="lblSenderCBSAcNo" runat="server" Text='<%# Eval("SenderCBSAcNo") %>'></asp:Label>--%>
                                             <asp:TextBox ID="txtcbsacno" runat="server" Text='<%# Bind("SenderCBSAcNo") %>' BorderStyle="None"  Width="100px" Enabled="false"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                     <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="SenderName" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <%--<asp:Label ID="lblSenderName" runat="server" Text='<%# Eval("SenderName") %>'></asp:Label>--%>
                                             <asp:TextBox ID="txtsendername" runat="server" Text='<%# Bind("SenderName") %>' BorderStyle="None"  Width="150px" Enabled="false"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                     <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Comission" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <%--<asp:Label ID="lblAmount" runat="server" Text='<%# Eval("Amount") %>'></asp:Label>--%>
                                             <asp:TextBox ID="txtcomm" runat="server" Text='<%# Bind("comm") %>' BorderStyle="None"  Width="100px" Enabled="false"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                     <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Amount" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <%--<asp:Label ID="lblAmount" runat="server" Text='<%# Eval("Amount") %>'></asp:Label>--%>
                                             <asp:TextBox ID="txtAmt" runat="server" Text='<%# Bind("Amount") %>' BorderStyle="None"  Width="100px" Enabled="false"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                     <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Benficiary Name" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <%--<asp:Label ID="lblBenfName" runat="server" Text='<%# Eval("Benf_Name") %>'></asp:Label>  --%>
                                             <asp:TextBox ID="txtbenName" runat="server" Text='<%# Bind("Benf_Name") %>' BorderStyle="None"  Width="100px" Enabled="false"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                     <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Benficiary AcNo" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <%--<asp:Label ID="lblBenficiaryAcNo" runat="server" Text='<%# Eval("Benf_AcNo") %>'></asp:Label>--%>
                                             <asp:TextBox ID="txtBenAcNo" runat="server" Text='<%# Bind("Benf_AcNo") %>' BorderStyle="None"  Width="100px" Enabled="false"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                     <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="IFSCCode" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <%--<asp:Label ID="lblIFSCCode" runat="server" Text='<%# Eval("IFSCCode") %>'></asp:Label>--%>
                                             <asp:TextBox ID="txtIFSCcode" runat="server" Text='<%# Bind("IFSCCode") %>' BorderStyle="None"  Width="100px" Enabled="false"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                     <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="BankName" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <%--<asp:Label ID="lblBankName" runat="server" Text='<%# Eval("BankName") %>'></asp:Label>--%>
                                             <asp:TextBox ID="txtBankName" runat="server" Text='<%# Bind("BankName") %>' BorderStyle="None" Width="150px" Enabled="false"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                     <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="BranchName" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <%--<asp:Label ID="lblBranchName" runat="server" Text='<%# Eval("BranchName") %>'></asp:Label>--%>
                                             <asp:TextBox ID="txtBranchName" runat="server" Text='<%# Bind("BranchName") %>' BorderStyle="None" Width="150px" Enabled="false"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                     <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sender Address" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <%--<asp:Label ID="lblSenderAddress" runat="server" Text='<%# Eval("SenderAddress") %>'></asp:Label>--%>
                                             <asp:TextBox ID="txtsAddress" runat="server" Text='<%# Bind("SenderAddress") %>' BorderStyle="None" Width="100px" Enabled="false"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                     <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="BenificiaryAddress" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                        <ItemTemplate>
                                            <%--<asp:Label ID="lblBenificiaryAddress" runat="server" Text='<%# Eval("BenificiaryAddress") %>'></asp:Label>--%>
                                             <asp:TextBox ID="txtbenAddress" runat="server" Text='<%# Bind("BenificiaryAddress") %>' BorderStyle="None"  Width="100px" Enabled="false" Visible="false"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                     <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="MobileNo" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <%--<asp:Label ID="lblMobileNo" runat="server" Text='<%# Eval("MobileNo") %>'></asp:Label>--%>
                                             <asp:TextBox ID="txtMobileno" runat="server" Text='<%# Bind("MobileNo") %>' BorderStyle="None"  Width="100px" Enabled="false"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                     <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Entry Date" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                        <ItemTemplate>
                                            <%--<asp:Label ID="lblMobileNo" runat="server" Text='<%# Eval("MobileNo") %>'></asp:Label>--%>
                                             <asp:TextBox ID="txtEntrydt" runat="server" Text='<%# Bind("EntryDate") %>' BorderStyle="None" Visible="false"></asp:TextBox>
                                        </ItemTemplate>
                                         <ItemStyle HorizontalAlign="Left" />
                                     <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Entry Status" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                        <ItemTemplate>
                                            <%--<asp:Label ID="lblMobileNo" runat="server" Text='<%# Eval("MobileNo") %>'></asp:Label>--%>
                                             <asp:TextBox ID="txtstatus" runat="server" Text='<%# Bind("EntryStatus") %>' BorderStyle="None" Visible="false"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                     <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                      
                                    <asp:TemplateField HeaderText="Remarks" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <%--<asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control"  Font-Size="10" autocomplete="off"></asp:TextBox>--%>
                                            <asp:TextBox ID="txtremarks" runat="server"  BorderStyle="None"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                     <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <%--OnClientClick="return confirm('do you sure want to delete these item');"--%>
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
                                 <asp:Button ID="btnsubmit1" runat="server" Text="Save" class="btn btn-success" OnClick="btnSave_Click"/>
                                    <a href="frmNeftRtgs.aspx" class="btn btn-outline btn-danger">Reset</a>
                              </div>
                            </div>
</asp:Content>
