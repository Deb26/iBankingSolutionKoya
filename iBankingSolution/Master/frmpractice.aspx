<%@ Page Language="C#" MasterPageFile="~/MasterPages/ProjectBM.Master" AutoEventWireup="true" CodeBehind="frmpractice.aspx.cs" Inherits="iBankingSolution.Master.frmpractice" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="demo-container size-narrow">
            <h2>Filtering</h2>
            <asp:DropDownList RenderMode="Lightweight" ID="RadComboBox2" AllowCustomText="true" runat="server" Width="500" Height="400px"
                DataSourceID="DBConnect" DataTextField="NOMENCLATURE" EmptyMessage="Search for people...">
            </asp:DropDownList>
        </div>
    <asp:SqlDataSource ID="DBConnect" runat="server" 
        SelectCommand="select  [NOMENCLATURE] as NOMENCLATURE, [LDG_CODE] from Ledger_Master where CASH_BANK != 1 order by LDG_CODE" />
</asp:Content>
