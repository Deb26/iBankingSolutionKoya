<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/ProjectBM.Master" CodeBehind="frmLoanSecurityEntry.aspx.cs" Inherits="iBankingSolution.Transaction.frmLoanSecurityEntry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" type="text/css" href="http://fonts.googleapis.com/css?family=Droid Sans" />

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
            <div style="float: right; margin-top: 12px;">
                <asp:Button ID="btnsubmit1" runat="server" Text="Save" class="btn " Style="color: white; background: linear-gradient(135deg, black 30%, Orange 100%);" OnClick="btnsubmit1_Click" />
                <asp:Button ID="btnedit" runat="server" Text="Edit" class="btn " Style="color: white; background: linear-gradient(135deg, green 30%, Orange 100%);" OnClick="btnedit_Click"  />

            </div>
            <h3 class="page-header">Loan Security Entry</h3>
        </div>

    </div>
    <div class="row">
        <div class="col-lg-12">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    Loan Security Entry
                </div>
                <div class="panel-body">
                    <div class="row">

                        <div class="form-group">
                            <div class="col-md-12">
                                <div class="panel panel-primary">

                                    <div class="panel-heading text-center">
                                        <b>Loan Security Details</b>

                                    </div>
                                </div>
                            </div>

                            <div class="col-md-6">
                                <label style="margin-right: 20PX;">Loan A/C No</label>

                                <asp:DropDownList ID="cmbx_Slcode" runat="server" CssClass="form-control">
                                   
                                </asp:DropDownList>
                                <%--<asp:TextBox ID="txtloanaccno" placeholder="ENTER OLD A/C NO" Font-Size="10" runat="server" CssClass="form-control" />--%>
                            </div>
                            <div class="col-md-6">
                                <label style="margin-right: 20PX;">Loan Type Details</label>
                                <asp:DropDownList ID="ddlloantype" runat="server" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="ddlloantype_SelectedIndexChanged">
                                    <asp:ListItem Value="0">--Select--</asp:ListItem>
                                    <asp:ListItem Value="1">KARBARNAMA</asp:ListItem>
                                    <asp:ListItem Value="2">LAND AND BUILDING DETAILS</asp:ListItem>
                                    <asp:ListItem Value="3">LIC</asp:ListItem>
                                    <asp:ListItem Value="4">HYPOTHICATION</asp:ListItem>
                                    <asp:ListItem Value="5">KVP/NSC/LIC AND OTHER BONDS</asp:ListItem>
                                    <asp:ListItem Value="6">FIXED DEPOSITS</asp:ListItem>
                                    <asp:ListItem Value="7">DEPOSITS CERTIFICATE</asp:ListItem>
                                    <asp:ListItem Value="8">RECURRING DEPOSITS</asp:ListItem>
                                    <asp:ListItem Value="9">MIS DEPOSITS</asp:ListItem>
                                    <asp:ListItem Value="10">DAILY DEPOSITS</asp:ListItem>

                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>



        <%--Karbaraname Details Entry--%>

        <asp:Panel ID="KDEpanel" runat="server" Visible="false">
        <div class="row">
            <div class="col-lg-12">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        Karbaraname Details Entry
                    </div>
                    <div class="panel-body">
                        <div class="row">

                            <div class="col-md-12" style="width: 100%; height: 200px; overflow: scroll">
                                <asp:GridView ID="gv_loantype" runat="server" CssClass="form-control" Height="200px" Width="600px" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" AutoGenerateColumns="False" ShowFooter="True">
                                    <AlternatingRowStyle BackColor="#DCDCDC" />
                                    <Columns>
                                       
                                       
                                        <asp:TemplateField HeaderText="Karba Date">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtKARBADATE" runat="server" DataFormatString="{0:dd/MM/yyyy}"  CssClass="input-sm BootDatepicker"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                         <asp:TemplateField HeaderText="Karba No">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtKARBANO" runat="server"></asp:TextBox>
                                            </ItemTemplate>
                                               </asp:TemplateField>
                                              <asp:TemplateField HeaderText="Karba Value">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtKarbaValue" runat="server"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>




                                         <asp:TemplateField HeaderText="Karba Valid">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtKarbaValid" runat="server" DataFormatString="{0:dd/MM/yyyy}"  CssClass="input-sm BootDatepicker"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                         <asp:TemplateField HeaderText="Karba Acre">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtKarbaAcre" runat="server"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                      
                                        <asp:TemplateField HeaderText="Credit Limit Value">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtcreditlivalue" runat="server"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Karba Sees">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtKarbaSees" runat="server"></asp:TextBox>
                                            </ItemTemplate>

                                            <FooterStyle HorizontalAlign="Right" />

                                                <FooterTemplate>

                                                 <asp:Button ID="ButtonAdd" runat="server" Text="Add New Row" OnClick="ButtonAdd_Click" />

                                                </FooterTemplate>
                                        </asp:TemplateField>



                                      <%--  <asp:TemplateField HeaderText="Karba Upto">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtKarbaUpto" runat="server" DataFormatString="{0:dd/MM/yyyy}"   CssClass="input-sm BootDatepicker"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>

                                       
                                       
                                       
                                       
                                        
                                    
                                        <asp:CommandField AccessibleHeaderText="Action" HeaderText="Action" ShowDeleteButton="True" />
                                    </Columns>
                                    <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                    <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                                    <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                    <SortedAscendingHeaderStyle BackColor="#0000A9" />
                                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                    <SortedDescendingHeaderStyle BackColor="#000065" />
                                </asp:GridView>

                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>

        </asp:Panel>


        <%--Land & Building Details--%>
          <asp:Panel ID="LBDPanel" runat="server" Visible="false">
        <div class="row">
            <div class="col-lg-12">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        Land & Building Details Entry
                    </div>
                    <div class="panel-body">
                        <div class="row">

                            <div class="col-md-12" style="width: 100%; height: 200px; overflow: scroll">
                                <asp:GridView ID="gv_LBD" runat="server" CssClass="form-control" Height="200px" Width="600px" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" AutoGenerateColumns="False" ShowFooter="True">
                                    <AlternatingRowStyle BackColor="#DCDCDC" />
                                    <Columns>
                                         <asp:TemplateField HeaderText="Muja No">
                                             <ItemTemplate>
                                                <asp:TextBox ID="txtMujaNo" runat="server"></asp:TextBox>
                                            </ItemTemplate>
                                         </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderText="GL No">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtGlNo" runat="server" CssClass="input-sm BootDatepicker"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                           
                                        <asp:TemplateField HeaderText="DAG No">
                                               <ItemTemplate>
                                                <asp:TextBox ID="txtDagNo" runat="server"></asp:TextBox>
                                            </ItemTemplate>
                                           </asp:TemplateField>
                                           
                                        <asp:TemplateField HeaderText="Khata No">
                                               <ItemTemplate>
                                                <asp:TextBox ID="txtKhataNo" runat="server"></asp:TextBox>
                                            </ItemTemplate>
                                           </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderText="Total Land">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtTotalLand" runat="server"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderText="Value Of Land">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtLandValue" runat="server"></asp:TextBox>
                                            </ItemTemplate>

                                            <FooterStyle HorizontalAlign="Right" />
                                             <FooterTemplate>

                                                 <asp:Button ID="ButtonAddLBD" runat="server" Text="Add New Row" OnClick="ButtonAddLBD_Click" />

                                                </FooterTemplate>
                                        </asp:TemplateField>
                                      
                                    
                                      
                                        <asp:CommandField AccessibleHeaderText="Action" HeaderText="Action" ShowDeleteButton="True" />
                                    </Columns>
                                    <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                    <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                                    <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                    <SortedAscendingHeaderStyle BackColor="#0000A9" />
                                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                    <SortedDescendingHeaderStyle BackColor="#000065" />
                                </asp:GridView>

                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>

        </asp:Panel>

        <%--LIC/GIS Details--%>
         <asp:Panel ID="LICGICPanel" runat="server" Visible="false">
        <div class="row">
            <div class="col-lg-12">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        LIC/GIS Details Entry
                    </div>
                    <div class="panel-body">
                        <div class="row">

                            <div class="col-md-12" style="width: 100%; height: 200px; overflow: scroll">
                                <asp:GridView ID="gv_LicGic" runat="server" CssClass="form-control" Height="200px" Width="600px" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" AutoGenerateColumns="False" ShowFooter="true">
                                    <AlternatingRowStyle BackColor="#DCDCDC" />
                                    <Columns>
                                        
                                         <asp:TemplateField HeaderText="Pol No">
                                             <ItemTemplate>
                                                <asp:TextBox ID="txtPolNo" runat="server"></asp:TextBox>
                                            </ItemTemplate>
                                         </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Sam Assu">
                                             <ItemTemplate>
                                                <asp:TextBox ID="txtSamAssu" runat="server"></asp:TextBox>
                                            </ItemTemplate>
                                         </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Sum Value">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtSUMValue" runat="server"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Assinee_Date">
                                             <ItemTemplate>
                                                <asp:TextBox ID="txtAssineeDate" runat="server" CssClass="input-sm BootDatepicker"></asp:TextBox>
                                            </ItemTemplate>
                                         </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Issue Office">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtIssueOfice" runat="server"></asp:TextBox>
                                            </ItemTemplate>

                                            <FooterStyle HorizontalAlign="Right" />
                                             <FooterTemplate>

                                                 <asp:Button ID="ButtonAddLicGis" runat="server" Text="Add New Row" OnClick="ButtonAddLicGis_Click"  />

                                                </FooterTemplate>

                                        </asp:TemplateField>
                                         
                                    
                                        <asp:CommandField AccessibleHeaderText="Action" HeaderText="Action" ShowDeleteButton="True" />
                                    </Columns>
                                    <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                    <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                                    <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                    <SortedAscendingHeaderStyle BackColor="#0000A9" />
                                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                    <SortedDescendingHeaderStyle BackColor="#000065" />
                                </asp:GridView>

                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>

        </asp:Panel>

        <%--Hypothication Details(Stock/Vehicle/Plant&Machinary)--%>
        <asp:Panel ID="HypothPanel" runat="server" Visible="false">
        <div class="row">
            <div class="col-lg-12">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        Hypothication Details Entry
                    </div>
                    <div class="panel-body">
                        <div class="row">

                            <div class="col-md-12" style="width: 100%; height: 200px; overflow: scroll">
                                <asp:GridView ID="gv_Hypoth" runat="server" CssClass="form-control" Height="200px" Width="600px" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" AutoGenerateColumns="False" ShowFooter="true">
                                    <AlternatingRowStyle BackColor="#DCDCDC" />
                                    <Columns>
                                        
                                       <asp:TemplateField HeaderText="Perticular">
                                           <ItemTemplate>
                                                <asp:TextBox ID="txtPerticular" runat="server"></asp:TextBox>
                                            </ItemTemplate>
                                       </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Vehicle No">
                                             <ItemTemplate>
                                                <asp:TextBox ID="txtVehicleNo" runat="server"></asp:TextBox>
                                            </ItemTemplate>
                                         </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Model No">
                                             <ItemTemplate>
                                                <asp:TextBox ID="txtModelNo" runat="server"></asp:TextBox>
                                            </ItemTemplate>
                                         </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Chese No">
                                             <ItemTemplate>
                                                <asp:TextBox ID="txtCheseno" runat="server"></asp:TextBox>
                                            </ItemTemplate>
                                         </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Value Of Hyp">
                                             <ItemTemplate>
                                                <asp:TextBox ID="txtValueHyp" runat="server"></asp:TextBox>
                                            </ItemTemplate>

                                              <FooterStyle HorizontalAlign="Right" />
                                             <FooterTemplate>

                                                 <asp:Button ID="ButtonAddHypoth" runat="server" Text="Add New Row" OnClick="ButtonAddHypoth_Click"/>

                                                </FooterTemplate>
                                         </asp:TemplateField>
                                        
                                        
                                                                                                                     
                                        
                               
                                        <asp:CommandField AccessibleHeaderText="Action" HeaderText="Action" ShowDeleteButton="True" />
                                    </Columns>
                                    <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                    <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                                    <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                    <SortedAscendingHeaderStyle BackColor="#0000A9" />
                                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                    <SortedDescendingHeaderStyle BackColor="#000065" />
                                </asp:GridView>

                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>

        </asp:Panel>

        <%--KVP/NSC Details--%>
         <asp:Panel ID="KvpNscPanel" runat="server" Visible="false">
        <div class="row">
            <div class="col-lg-12">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        KVP/NSC Details Entry
                    </div>
                    <div class="panel-body">
                        <div class="row">

                            <div class="col-md-12" style="width: 100%; height: 200px; overflow: scroll">
                                <asp:GridView ID="gv_KvpNsc" runat="server" CssClass="form-control" Height="200px" Width="600px" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" AutoGenerateColumns="False" ShowFooter="true">
                                    <AlternatingRowStyle BackColor="#DCDCDC" />
                                    <Columns>
                                        
                                       <asp:TemplateField HeaderText="Type">
                                           <ItemTemplate>
                                                <asp:TextBox ID="txtTypee" runat="server"></asp:TextBox>
                                            </ItemTemplate>
                                       </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Cert No">
                                             <ItemTemplate>
                                                <asp:TextBox ID="txtCertNo" runat="server"></asp:TextBox>
                                            </ItemTemplate>
                                         </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Issue Date">
                                             <ItemTemplate>
                                                <asp:TextBox ID="txtIssueDate" runat="server" CssClass="input-sm BootDatepicker"></asp:TextBox>
                                            </ItemTemplate>
                                         </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Issue Office">
                                             <ItemTemplate>
                                                <asp:TextBox ID="txtIssueOffice" runat="server"></asp:TextBox>
                                            </ItemTemplate>
                                         </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Maturity Value">
                                              <ItemTemplate>
                                                <asp:TextBox ID="txtMatValue" runat="server" ></asp:TextBox>
                                            </ItemTemplate>
                                         </asp:TemplateField>
                                        
                                         <asp:TemplateField HeaderText="Maturity Date">
                                              <ItemTemplate>
                                                <asp:TextBox ID="txtMatDate" runat="server" CssClass="input-sm BootDatepicker"></asp:TextBox>
                                            </ItemTemplate>
                                         </asp:TemplateField>
                                          <asp:TemplateField HeaderText="Face Value">
                                             <ItemTemplate>
                                                <asp:TextBox ID="txtFaceValue" runat="server"></asp:TextBox>
                                            </ItemTemplate>
                                         </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Pledge Date">
                                              <ItemTemplate>
                                                <asp:TextBox ID="txtPledgeDate" runat="server" CssClass="input-sm BootDatepicker"></asp:TextBox>
                                            </ItemTemplate>
                                         </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Remarks">
                                              <ItemTemplate>
                                                <asp:TextBox ID="txtRemarkss" runat="server"></asp:TextBox>
                                            </ItemTemplate>

                                              <FooterStyle HorizontalAlign="Right" />
                                             <FooterTemplate>

                                                 <asp:Button ID="ButtonAddKvpNsc" runat="server" Text="Add New Row" OnClick="ButtonAddKvpNsc_Click"/>

                                                </FooterTemplate>
                                         </asp:TemplateField>
                                        
                                        
                                        
                                        
                                        
                                        
                                       
                                        <asp:CommandField AccessibleHeaderText="Action" HeaderText="Action" ShowDeleteButton="True" />
                                    </Columns>
                                    <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                    <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                                    <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                    <SortedAscendingHeaderStyle BackColor="#0000A9" />
                                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                    <SortedDescendingHeaderStyle BackColor="#000065" />
                                </asp:GridView>

                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>

        </asp:Panel>

        <%--Fix Deposit--%>

          <asp:Panel ID="FixDepoPanel" runat="server" Visible="false">
        <div class="row">
            <div class="col-lg-12">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        Fix Deposit Entry
                    </div>
                    <div class="panel-body">
                        <div class="row">

                            <div class="col-md-12" style="width: 100%; height: 200px; overflow: scroll">
                                <asp:GridView ID="gv_FixDepo" runat="server" CssClass="form-control" Height="200px" Width="600px" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" AutoGenerateColumns="False" ShowFooter="True">
                                    <AlternatingRowStyle BackColor="#DCDCDC" />
                                    <Columns>
                                        
                                    <asp:TemplateField HeaderText="FD AccNo">
                                           <ItemTemplate>
                                                <asp:TextBox ID="txtFDAccNo" runat="server" Text='<%# Bind("SL_CODE") %>'></asp:TextBox>
                                            </ItemTemplate>
                                       </asp:TemplateField>
                                          <asp:TemplateField HeaderText="Dep Amt">
                                             <ItemTemplate>
                                                <asp:TextBox ID="txtDepAmt" runat="server" Text='<%# Bind("ACT_OP_CR") %>'></asp:TextBox>
                                            </ItemTemplate>
                                         </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Maturity Date">
                                             <ItemTemplate>
                                                <asp:TextBox ID="txtMatDate" runat="server" CssClass="input-sm BootDatepicker" Text='<%# Bind("DateofMaturity","{0:dd/MM/yyyy}") %>'></asp:TextBox>
                                            </ItemTemplate>

                                          <FooterStyle HorizontalAlign="Right" />
                                             <FooterTemplate>

                                                 <asp:Button ID="ButtonAddFixDepo" runat="server" Text="Add New Row" OnClick="ButtonAddFixDepo_Click"/>

                                                </FooterTemplate>

                                         </asp:TemplateField>

                                         
                                             
                                        <asp:TemplateField HeaderText="Action">
                                           
                                            <ItemTemplate>
                                                <asp:CheckBox ID="FD_CheckBox" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                    <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                                    <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                    <SortedAscendingHeaderStyle BackColor="#0000A9" />
                                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                    <SortedDescendingHeaderStyle BackColor="#000065" />
                                </asp:GridView>

                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>

        </asp:Panel>

        <%--Deposite Certificate--%>
          <asp:Panel ID="DCPanel" runat="server" Visible="false">
        <div class="row">
            <div class="col-lg-12">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        Deposite Certificat
                    </div>
                    <div class="panel-body">
                        <div class="row">

                            <div class="col-md-12" style="width: 100%; height: 200px; overflow: scroll">
                                <asp:GridView ID="gv_DepoCert" runat="server" CssClass="form-control" Height="200px" Width="600px" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" AutoGenerateColumns="False" ShowFooter="True">
                                    <AlternatingRowStyle BackColor="#DCDCDC" />
                                    <Columns>
                                        
                                    <asp:TemplateField HeaderText="DC AccNo">
                                           <ItemTemplate>
                                                <asp:TextBox ID="txtDCAccNo" runat="server" Text='<%# Bind("SL_CODE") %>'></asp:TextBox>
                                            </ItemTemplate>
                                       </asp:TemplateField>
                                          <asp:TemplateField HeaderText="Dep Amt">
                                             <ItemTemplate>
                                                <asp:TextBox ID="txtDepAmt" runat="server" Text='<%# Bind("ACT_OP_CR") %>'></asp:TextBox>
                                            </ItemTemplate>
                                         </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Maturity Date">
                                             <ItemTemplate>
                                                <asp:TextBox ID="txtMatDate" runat="server" CssClass="input-sm BootDatepicker" Text='<%# Bind("DateofMaturity","{0:dd/MM/yyyy}") %>'></asp:TextBox>
                                            </ItemTemplate>
                                           
                                          
                                           <FooterStyle HorizontalAlign="Right" />
                                             <FooterTemplate>

                                                 <asp:Button ID="ButtonAddDepoCert" runat="server" Text="Add New Row" OnClick="ButtonAddDepoCert_Click"/>

                                                </FooterTemplate>

                                         </asp:TemplateField>
                                             
                                        <asp:TemplateField HeaderText="Action">
                                          
                                            <ItemTemplate>
                                                <asp:CheckBox ID="DC_CheckBox" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                    <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                                    <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                    <SortedAscendingHeaderStyle BackColor="#0000A9" />
                                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                    <SortedDescendingHeaderStyle BackColor="#000065" />
                                </asp:GridView>

                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>

        </asp:Panel>

       <%--Recurring Deposit--%>
         <asp:Panel ID="RDPanel" runat="server" Visible="false">
        <div class="row">
            <div class="col-lg-12">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                       Recurring Deposit
                    </div>
                    <div class="panel-body">
                        <div class="row">

                            <div class="col-md-12" style="width: 100%; height: 200px; overflow: scroll">
                                <asp:GridView ID="gv_recurdepo" runat="server" CssClass="form-control" Height="200px" Width="600px" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" AutoGenerateColumns="False" ShowFooter="True">
                                    <AlternatingRowStyle BackColor="#DCDCDC" />
                                    <Columns>
                                        
                                    <asp:TemplateField HeaderText="RD AccNo">
                                           <ItemTemplate>
                                                <asp:TextBox ID="txtRDAccNo" runat="server" Text='<%# Bind("SL_CODE") %>'></asp:TextBox>
                                            </ItemTemplate>
                                       </asp:TemplateField>
                                          <asp:TemplateField HeaderText="Dep Amt">
                                             <ItemTemplate>
                                                <asp:TextBox ID="txtDepAmt" runat="server" Text='<%# Bind("ACT_OP_CR") %>'></asp:TextBox>
                                            </ItemTemplate>
                                         </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Maturity Date">
                                             <ItemTemplate>
                                                <asp:TextBox ID="txtMatDate" runat="server" CssClass="input-sm BootDatepicker" Text='<%# Bind("DateofMaturity","{0:dd/MM/yyyy}") %>'></asp:TextBox>
                                            </ItemTemplate>

                                           <FooterStyle HorizontalAlign="Right" />
                                             <FooterTemplate>

                                                 <asp:Button ID="ButtonAddRecurdepo" runat="server" Text="Add New Row" OnClick="ButtonAddRecurdepo_Click"/>

                                                </FooterTemplate>

                                         </asp:TemplateField>
                                             
                                        <asp:TemplateField HeaderText="Action">
                                          
                                            <ItemTemplate>
                                                <asp:CheckBox ID="RD_CheckBox" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                    <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                                    <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                    <SortedAscendingHeaderStyle BackColor="#0000A9" />
                                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                    <SortedDescendingHeaderStyle BackColor="#000065" />
                                </asp:GridView>

                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>

        </asp:Panel>

        <%--Mis Deposit--%>

         <asp:Panel ID="MisPanel" runat="server" Visible="false">
        <div class="row">
            <div class="col-lg-12">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                       Mis Deposit
                    </div>
                    <div class="panel-body">
                        <div class="row">

                            <div class="col-md-12" style="width: 100%; height: 200px; overflow: scroll">
                                <asp:GridView ID="gv_MisDepo" runat="server" CssClass="form-control" Height="200px" Width="600px" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" AutoGenerateColumns="False" ShowFooter="True">
                                    <AlternatingRowStyle BackColor="#DCDCDC" />
                                    <Columns>
                                        
                                    <asp:TemplateField HeaderText="MIS AccNo">
                                           <ItemTemplate>
                                                <asp:TextBox ID="txtMisAccNo" runat="server" Text='<%# Bind("SL_CODE") %>'></asp:TextBox>
                                            </ItemTemplate>
                                       </asp:TemplateField>
                                          <asp:TemplateField HeaderText="Dep Amt">
                                             <ItemTemplate>
                                                <asp:TextBox ID="txtDepAmt" runat="server" Text='<%# Bind("ACT_OP_CR") %>'></asp:TextBox>
                                            </ItemTemplate>
                                         </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Maturity Date">
                                             <ItemTemplate>
                                                <asp:TextBox ID="txtMatDate" runat="server" CssClass="input-sm BootDatepicker" Text='<%# Bind("DateofMaturity","{0:dd/MM/yyyy}") %>'></asp:TextBox>
                                            </ItemTemplate>
                                           <FooterStyle HorizontalAlign="Right" />
                                             <FooterTemplate>

                                                 <asp:Button ID="ButtonAddMisDepo" runat="server" Text="Add New Row" OnClick="ButtonAddMisDepo_Click"/>

                                                </FooterTemplate>

                                         </asp:TemplateField>
                                             
                                        <asp:TemplateField HeaderText="Action">
                                           
                                            <ItemTemplate>
                                                <asp:CheckBox ID="MIS_CheckBox" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                    <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                                    <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                    <SortedAscendingHeaderStyle BackColor="#0000A9" />
                                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                    <SortedDescendingHeaderStyle BackColor="#000065" />
                                </asp:GridView>

                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>

        </asp:Panel>


        <%--Daily Deposit--%>
         <asp:Panel ID="DDPanel" runat="server" Visible="false">
        <div class="row">
            <div class="col-lg-12">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                       Daily Deposit Entry
                    </div>
                    <div class="panel-body">
                        <div class="row">

                            <div class="col-md-12" style="width: 100%; height: 200px; overflow: scroll">
                                <asp:GridView ID="gv_DD" runat="server" CssClass="form-control" Height="200px" Width="600px" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" AutoGenerateColumns="False" ShowFooter="True">
                                    <AlternatingRowStyle BackColor="#DCDCDC" />
                                    <Columns>
                                        
                                   <asp:TemplateField HeaderText="DAILY AccNo">
                                           <ItemTemplate>
                                                <asp:TextBox ID="txtDailyAccNo" runat="server" Text='<%# Bind("SL_CODE") %>'></asp:TextBox>
                                            </ItemTemplate>
                                       </asp:TemplateField>
                                          <asp:TemplateField HeaderText="Dep Amt">
                                             <ItemTemplate>
                                                <asp:TextBox ID="txtDepAmt" runat="server" Text='<%# Bind("ACT_OP_CR") %>'></asp:TextBox>
                                            </ItemTemplate>
                                         </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Maturity Date">
                                             <ItemTemplate>
                                                <asp:TextBox ID="txtMatDate" runat="server" CssClass="input-sm BootDatepicker" Text='<%# Bind("DateofMaturity","{0:dd/MM/yyyy}") %>' ></asp:TextBox>
                                            </ItemTemplate>
                                           <FooterStyle HorizontalAlign="Right" />
                                             <FooterTemplate>

                                                 <asp:Button ID="ButtonAddDailyDepo" runat="server" Text="Add New Row" OnClick="ButtonAddDailyDepo_Click"/>

                                                </FooterTemplate>

                                         </asp:TemplateField>
                                        
                                             
                                        <asp:TemplateField HeaderText="Action">
                                          
                                            <ItemTemplate>
                                                <asp:CheckBox ID="DD_CheckBox" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                    <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                                    <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                    <SortedAscendingHeaderStyle BackColor="#0000A9" />
                                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                    <SortedDescendingHeaderStyle BackColor="#000065" />
                                </asp:GridView>

                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>

        </asp:Panel>

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
