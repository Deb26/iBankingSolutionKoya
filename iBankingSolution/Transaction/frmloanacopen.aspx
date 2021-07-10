<%@ Page Title="" Language="C#" MasterPageFile="~/BANKIT.Master" AutoEventWireup="true" CodeBehind="frmloanacopen.aspx.cs" Inherits="IBANK.frmloanacopen" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
        function customerselected(sender, args) {
            var hdnValueID = "<%=hdnValue.ClientID %>";
            document.getElementById(hdnValueID).value = args.get_value();
            __doPostBack(hdnValueID, "");
        }

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BANKITmaster" runat="server">

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <asp:HiddenField ID="hdnValue" runat="server" OnValueChanged="hdnValue_ValueChanged" />
            <asp:HiddenField ID="txtcol" runat="server" />

            <asp:HiddenField ID="hdnAction" runat="server" />

            &nbsp;

            <table class="input-group" runat="server" cellpadding="2" cellspacing="0" width="80%">

                <tr>

                    <td class="header-main" style="font-size: 13px;" height="23">&nbsp;Loan A/C Opening form

                    </td>

                </tr>
                <tr>
                    <td style="border-top: 1px solid #CCCCCC;"></td>
                </tr>
                <tr>
                    <td>


                        <table width="100%" border="0" cellspacing="3" cellpadding="3">

                            <tr>
                                <td height="16%" class="main-label" colspan="8" bgcolor="#C6EFFD">Application Details</td>
                            </tr>

                            <tr>
                                <td colspan="8">

                                    <asp:TextBox ID="txtcustid" Width="100%" CssClass="form-control" Placeholder="Type customer id or name here" runat="server"></asp:TextBox>
                                    <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" ServiceMethod="SearchCustomers" MinimumPrefixLength="2"
                                        CompletionInterval="10" EnableCaching="true" CompletionSetCount="10" FirstRowSelected="false"
                                        TargetControlID="txtcustid" OnClientItemSelected="customerselected" CompletionListCssClass="AutoExtender" CompletionListItemCssClass="AutoExtenderList" CompletionListHighlightedItemCssClass="AutoExtenderHighlight">
                                    </asp:AutoCompleteExtender>

                                </td>
                            </tr>

                            <tr>
                                <td colspan="8">

                                    <asp:Panel ID="scrollPane" Style="width: auto; max-height: 75px;" runat="server" ScrollBars="Auto">
                                        <asp:GridView ID="grdappl" AutoGenerateColumns="false" Width="98%" runat="server" CssClass="grid upper">

                                            <Columns>
                                                <asp:TemplateField HeaderText="Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblname" Text='<%# Eval("name") %>' Width="98%" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="SO/DO/HO/Others">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgname" Text='<%# Eval("gname") %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Village">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblvill" Text='<%# Eval("vill") %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Client Status">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblcstat" Text='<%# Eval("cl_status") %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnclientdel" runat="server" CssClass="btn-grid" ForeColor="#ff5050" Text="&#xf37f;" OnClick="btnclientdel_Click" ToolTip="Delete" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </asp:Panel>

                                </td>

                            </tr>
                            <tr>
                                <td class="main-label" colspan="8" bgcolor="#C6EFFD">Opening Details</td>
                            </tr>
                            <tr>
                                <td colspan="8">
                                    <table width="80%">
                                        <tr>
                                            <td>
                                                <asp:Label ID="lbldateofopening" runat="server" CssClass="label-text" Text="Date Of Opening"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtdateofopening" Width="100" runat="server"
                                                    CssClass="form-control" OnTextChanged="txtdateofopening_TextChanged">
                                                </asp:TextBox>

                                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtdateofopening" Format="dd/MM/yyyy" CssClass="calendar"></asp:CalendarExtender>
                                                <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtdateofopening" Mask="99/99/9999" MaskType="Date"></asp:MaskedEditExtender>
                                            </td>

                                            <td>
                                                <asp:Label ID="lblloancaseno" runat="server" CssClass="label-text" Text="Loan Case No"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtloancaseno" runat="server"
                                                    CssClass="form-control_uppertext" Width="120"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbllfno" runat="server" CssClass="label-text" Text="L/F No"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtlfno" runat="server" CssClass="form-control_uppertext" Width="120"></asp:TextBox>
                                            </td>
                                        </tr>

                                    </table>

                                </td>

                            </tr>

                            <tr>
                                <td height="16%" class="main-label" colspan="8" bgcolor="#C6EFFD">Loan Details</td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblselectloanscheme" runat="server" Width="20%" Height="10%" CssClass="label-text" Text="Select Loan Scheme"></asp:Label>
                                </td>
                                <td colspan="7">
                                    <asp:ComboBox ID="cmbselectloanscheme" runat="server" Width="460"
                                        CssClass="combo" DropDownStyle="DropDownList" AutoCompleteMode="Append"
                                        OnSelectedIndexChanged="cmbselectloanscheme_SelectedIndexChanged" AutoPostBack="true">
                                    </asp:ComboBox>
                                </td>
                            </tr>

                            <tr>
                                <td>
                                    <asp:Label ID="lblloantype" runat="server" Width="35%" Height="10%" CssClass="label-text" Text="Loan Type"></asp:Label>
                                </td>
                                <td>
                                    <asp:ComboBox ID="cmbloantype" runat="server" Width="150" CssClass="combo"
                                        DropDownStyle="DropDownList" AutoCompleteMode="Append" MaxLength="20"
                                        AutoPostBack="true">
                                    </asp:ComboBox>
                                </td>
                                <td>
                                    <asp:Label ID="lbltotloan" runat="server" CssClass="label-text" Text="Sanction Amount"></asp:Label>

                                </td>
                                <td>
                                    <asp:TextBox ID="txttotloan" runat="server" CssClass="form-control_uppertext"
                                        Width="120" OnTextChanged="txttotloan_TextChanged" AutoPostBack="true"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="lblnoofinstt" runat="server" CssClass="label-text" Text="No Of Instt"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtnoofinstt" runat="server" CssClass="form-control_uppertext"
                                        Width="120" OnTextChanged="txtnoofinstt_TextChanged" AutoPostBack="true" ReadOnly="true"></asp:TextBox>
                                </td>

                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblreapymode" runat="server" Width="35%" Height="10%" CssClass="label-text" Text="Reapy Mode"></asp:Label>
                                </td>
                                <td>
                                    <asp:ComboBox ID="cmbreapymode" runat="server" Width="150" CssClass="combo"
                                        DropDownStyle="DropDownList" AutoCompleteMode="Append" MaxLength="20"
                                        AutoPostBack="true" OnSelectedIndexChanged="cmbreapymode_SelectedIndexChanged">
                                    </asp:ComboBox>
                                </td>
                                <td>
                                    <asp:Label ID="lblrateofinterest" runat="server" CssClass="label-text" Text="Rate of Interest"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtrateofinterest" runat="server"
                                        CssClass="form-control_uppertext" Width="120"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="lblfirstoddet" runat="server" CssClass="label-text" Text="First Od Date"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtfirstoddet" Width="120" runat="server" CssClass="form-control">
                                    </asp:TextBox>
                                </td>
                                <td>
                                    <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtfirstoddet" Format="dd/MM/yyyy" CssClass="calendar"></asp:CalendarExtender>
                                    <asp:MaskedEditExtender ID="MaskedEditExtender3" runat="server" TargetControlID="txtfirstoddet" Mask="99/99/9999" MaskType="Date"></asp:MaskedEditExtender>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblinsttapply" runat="server" CssClass="label-text" Text="instt Apply"></asp:Label>
                                </td>
                                <td>
                                    <asp:ComboBox ID="cmbinsttapply" runat="server" Width="150" CssClass="combo"
                                        DropDownStyle="DropDownList" AutoCompleteMode="Append" MaxLength="20"
                                        OnSelectedIndexChanged="cmbinsttapply_SelectedIndexChanged" AutoPostBack="true">

                                        <asp:ListItem Value="">--Select--</asp:ListItem>
                                        <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                        <asp:ListItem Value="No">No</asp:ListItem>
                                    </asp:ComboBox>
                                </td>
                                <td>
                                    <asp:Label ID="lblodroi" runat="server" CssClass="label-text" Text="OD ROI"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtodroi" runat="server" CssClass="form-control_uppertext" Width="120"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="lblinsttamt" runat="server" CssClass="label-text" Text="Instt AMT"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtinsttamt" runat="server" CssClass="form-control_uppertext"
                                        Width="120"></asp:TextBox>
                                </td>


                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbloverduemode" runat="server" CssClass="label-text" Text="Overdue Mode"></asp:Label>
                                </td>
                                <td>
                                    <asp:ComboBox ID="cmboverduemode" runat="server" Width="150" CssClass="combo"
                                        DropDownStyle="DropDownList" AutoCompleteMode="Append"
                                        AppendDataBoundItems="True"
                                        OnSelectedIndexChanged="cmboverduemode_SelectedIndexChanged" AutoPostBack="true">
                                        <asp:ListItem Value="" Selected="True"></asp:ListItem>
                                        <asp:ListItem Value="1">MONTHLY</asp:ListItem>
                                        <asp:ListItem Value="3">QUARTERLY</asp:ListItem>
                                        <asp:ListItem Value="6">HALF YARLY</asp:ListItem>
                                        <asp:ListItem Value="12">YEARLY</asp:ListItem>
                                        
                                        <asp:ListItem Value="EMI">EMI</asp:ListItem>
                                        <asp:ListItem Value="AFTER LAST REPAY DATE">AFTER LAST REPAY DATE</asp:ListItem>
                                        <asp:ListItem Value="NO OD">NO OD</asp:ListItem>
                                    </asp:ComboBox>
                                </td>
                                <td>
                                    <asp:Label ID="lblloanperiod" runat="server" CssClass="label-text" Text="Loan Period (Month)"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtloanprdmon" runat="server"
                                        CssClass="form-control_uppertext" Width="120"
                                        OnTextChanged="txtloavperiod_TextChanged" AutoPostBack="true"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbllastreapydet" runat="server" CssClass="label-text" Text="Last Reapy date"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtlastreapydet" Width="170" runat="server" CssClass="form-control">
                                    </asp:TextBox>
                                    <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtlastreapydet" Format="dd/MM/yyyy" CssClass="calendar"></asp:CalendarExtender>
                                    <asp:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="txtlastreapydet" Mask="99/99/9999" MaskType="Date"></asp:MaskedEditExtender>

                                </td>

                                <td>
                                    <asp:Label ID="lblmonperiod" runat="server" CssClass="label-text" Text="Mon. period (Month)"></asp:Label>
                                </td>
                                <td colspan="4">
                                    <asp:TextBox ID="txtmonperiod" runat="server" CssClass="form-control_uppertext" Width="120"></asp:TextBox>
                                </td>
                            </tr>

                            <tr>
                                <td>
                                    <asp:Button ID="btnsave" runat="server" CssClass="form-button" Text="Save"
                                        OnClick="btnsave_Click" />
                                    <asp:Button ID="btnedit" runat="server" CssClass="form-button" Text="Edit"
                                        OnClick="btnedit_Click" />
                                    <asp:Button ID="btnReset" runat="server" Text="Reset"
                                        CssClass="form-button" OnClick="btnReset_Click" />
                                </td>
                            </tr>



                        </table>
                    </td>
                </tr>
            </table>





        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
