<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/ProjectBM.Master" CodeBehind="frmBatchTransaction.aspx.cs" Inherits="iBankingSolution.Transaction.frmBatchTransaction" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $('#dataTables-example').DataTable({
                responsive: true
            });
        });
    </script>
    <style type="text/css">
        .auto-style1 {
            height: 23px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">


    <asp:ScriptManager ID="scrpt1" runat="server" EnablePageMethods="true"></asp:ScriptManager>
    <div class="row">
        <div class="col-lg-12">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    Batch Transaction
                </div>
                <div class="panel-body">
                    <div class="row">

                        <div class="form-group">

                            <div class="col-md-2">
                                <label style="margin-right: 20PX;">Type</label>
                                <asp:DropDownList ID="cmbx_Type" runat="server" CssClass="form-control" Font-Size="10" AutoPostBack="True" OnSelectedIndexChanged="cmbx_Type_SelectedIndexChanged">
                                    <asp:ListItem Value="0">--Select Type--</asp:ListItem>
                                    <asp:ListItem Value="1">Cash</asp:ListItem>
                                    <asp:ListItem Value="2">Journal</asp:ListItem>
                                </asp:DropDownList>
                            </div>


                            <div class="col-md-2">
                                <label style="margin-right: 20PX;">Trans Type</label>
                                <asp:DropDownList ID="Cmbx_TransType" runat="server" Font-Size="10" CssClass="form-control">
                                    <asp:ListItem Value="0">--Trans Type--</asp:ListItem>
                                    <asp:ListItem Value="1">Debit</asp:ListItem>
                                    <asp:ListItem Value="2">Credit</asp:ListItem>
                                </asp:DropDownList>

                            </div>

                            <div class="col-md-3" runat="server" id="Divldg">
                                <label style="margin-right: 20PX;">Select Ledger</label>
                                <asp:DropDownList ID="Cmbx_Ledger" runat="server" Font-Size="10" CssClass="form-control">
                                </asp:DropDownList>

                            </div>



                            <div class="col-md-2">
                                <label style="margin-right: 20PX;">TransDate:</label>
                                <asp:TextBox ID="dtpkr_frmDate" CssClass="form-control input-sm BootDatepicker" placeholder="dd/MM/yyyy" Font-Size="10" runat="server" onFocus="this.select()" autocomplete="off" required="required"></asp:TextBox>

                            </div>
                          

                    

                              <div class="clearfix"></div>

                              <div class="col-md-2">
                                 
                                <label style="margin-right: 20PX;">Select Excel File:</label>
                                <div runat="server" id="DivMasterUpload">
                                    <asp:FileUpload ID="companyUpload" runat="server" class="form-control"/>
                                    <asp:HyperLink ID="LinkMasterFormat" runat="server" Style="color: #0033CC; font-style: italic" Text="Download Format"><a href="../BatchTransFormat.xlsx"> Download Format</a> </asp:HyperLink>
                                </div>

                            </div>

                            <div class="col-md-2">
                                 <label style="margin-right: 20PX;">Sheet Selection:</label>
                                 <asp:Button ID="btnSheet" runat="server" CssClass="btn btn-primary" OnClick="btnSheet_Click" Text="Select Sheet" ValidationGroup="false" />
                           

                            </div>

                             <div class="col-md-2">
                                 <label style="margin-right: 20PX;">Select Sheet Name:</label>
                                <asp:DropDownList ID="DdlSheetNames" runat="server" class="form-control" Height="50px" Width="185px">
                                </asp:DropDownList>  
                           

                            </div>



                            <div class="col-md-4">
                       
                                      <br />
                                <asp:Button ID="btnShow" runat="server" Text="Show" class="btn btn-primary" OnClick="btnShow_Click" ValidationGroup="False" />
                                <asp:Button ID="btnSave" runat="server" Text="Save" class="btn btn-warning" OnClick="btnSave_Click" Visible="False" ValidationGroup="true" />

                            </div>
                             <div class="clearfix"></div>
                            <div class="col-md-12">
                                <br />
                                 <label style="margin-right: 20PX;">Remarks/Narration:</label>
                                <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>

                            </div>
                      
                            
                            <hr />





                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <center>
 <div class="row">
            <div class="col-lg-12">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        Batch Data:    <asp:Label ID="lblListShow" runat="server" Text=""></asp:Label>          
                    </div>
                    <div class="panel-body">



                        <asp:GridView ID="gridAct" runat="server" CellPadding="3" Font-Names="Arial" ShowFooter="True" Width="735px" Font-Size="11pt" ShowHeaderWhenEmpty="True" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px">
                            <FooterStyle BackColor="White" ForeColor="#000066" />
                            <HeaderStyle BackColor="#006699" Font-Bold="True" Font-Names="Courier New" ForeColor="White" Font-Size="10pt" />
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
    </center>


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
