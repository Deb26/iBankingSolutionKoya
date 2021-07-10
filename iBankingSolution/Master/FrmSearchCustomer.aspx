<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/ProjectBM.Master" CodeBehind="FrmSearchCustomer.aspx.cs" Inherits="iBankingSolution.Master.FrmSearchCustomer" %>

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
                <h1 class="page-header">Search Details</h1>

            </div>
          
        </div>
        <div class="row">
            <div class="col-lg-4">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                                    Search Category
                        </div>
                     <div class="panel-body">
                        <div class="row">
                            <div class="col-lg-10">
                            <div class="form-group">
                                    <asp:DropDownList ID="cmbx_category" runat="server" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" AutoPostBack="True" CssClass="form-control" Font-Size="10">
                                        <asp:ListItem>--SELECT--</asp:ListItem>
                                        <asp:ListItem>ID</asp:ListItem>
                                        <asp:ListItem>NAME</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                </div>
                                <div class="col-lg-4">
                                    <asp:TextBox ID="txttype" runat="server" Visible="False" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-lg-4">
                                    <asp:Button ID="btnsearch" runat="server" Text="SEARCH" Visible="False" OnClick="btnsearch_Click" CssClass="form-control"/>

                                </div>

                                <div class="clearfix">
                                </div>
                            </div>
                         </div>
                    </div>
               
                </div>
            </div>
            <div class="row">
                <div class="col-lg8">
                    <div class="panel panel-primary">
                        <div class="panel-heading">
                            Search Details
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="form-group">
                                    <div class="col-md-12">
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                                <asp:GridView ID="GVSupplier" runat="server" AutoGenerateColumns="False" DataKeyNames="CUST_ID"
                                                    CssClass="table table-bordered table-striped table-hover" EmptyDataText="No Data Found" OnRowDataBound="OnRowDataBound" AllowPaging="True" OnPageIndexChanging="GVSupplier_PageIndexChanging" PageSize="5" CellPadding="4" ForeColor="#333333" GridLines="None" >
                                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                    <Columns>

                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="CUSTOMER ID">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_CUST_ID" CssClass="form-control input-sm" runat="server"
                                                                    autocomplete="off" ForeColor="Black" Text='<%# Eval("CUST_ID") %>' onFocus="this.select()" AutoPostBack="True" OnTextChanged="txt_CUST_ID_TextChanged"></asp:Label>

                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" />
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="CUSTOMER NAME">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblUserName" CssClass="form-control input-sm" runat="server" Font-Size="10"
                                                                    autocomplete="off" ForeColor="Black" Text='<%# Eval("NAME") %>' onFocus="this.select()"></asp:Label>

                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" />
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>


                                                        <asp:TemplateField HeaderText="GURDIAN NAME">
                                                            <ItemTemplate> 
                                                                <asp:Label ID="lblpocode" CssClass="form-control input-sm" runat="server" Font-Size="10"
                                                                    autocomplete="off" ForeColor="Black" Text='<%# Eval("GUARDIAN_NAME") %>' onFocus="this.select()"></asp:Label>
                                                            </ItemTemplate>

                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="VILLAGE Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblvillcode" CssClass="form-control input-sm" runat="server" Font-Size="10"
                                                                    autocomplete="off" ForeColor="Black" Text='<%# Eval("VILL_CODE") %>' onFocus="this.select()"></asp:Label>
                                                            </ItemTemplate>

                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="TELEPHONE NO">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbltellno" CssClass="form-control input-sm" runat="server" Font-Size="10"
                                                                    autocomplete="off" ForeColor="Black" Text='<%# Eval("TEL_NO") %>' onFocus="this.select()"></asp:Label>
                                                            </ItemTemplate>

                                                        </asp:TemplateField>


                                                        <asp:TemplateField HeaderText="Acc Details" ShowHeader="False">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="false" CommandName="" OnClick="LinkButton1_Click" Text="Details"></asp:LinkButton>
                                                                <br />
                                                                <br />
                                                                <asp:GridView ID="GVChild" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" Font-Size="10">
                                                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                                    <Columns>
                                                                        <asp:BoundField DataField="SL_CODE" HeaderText="SL_CODE" />
                                                                        <asp:BoundField DataField="AC_STATUS" HeaderText="AC_STATUS" />
                                                                        <asp:BoundField DataField="ACTYPE" HeaderText="ACTYPE" />
                                                                        <asp:BoundField DataField="OLD_ACNO" HeaderText="OLD_ACNO" />
                                                                    </Columns>
                                                                    <EditRowStyle BackColor="#999999" />
                                                                    <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" />
                                                                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                                    <PagerStyle ForeColor="White" HorizontalAlign="Center" BackColor="#284775" />
                                                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                                                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                                                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                                                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                                                </asp:GridView>
                                                            </ItemTemplate>
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
                                                <br />
                                                <br />
                                              
                                                <h1>&nbsp;</h1>
                                                <br />
                                                <br />
                                                &nbsp;&nbsp;
                                            </ContentTemplate>
                                            <%--  <Triggers>
                                            <asp:PostBackTrigger ControlID="btnsubmit" />
                                        </Triggers>--%>
                                        </asp:UpdatePanel>
                                    </div>
                                    <div class="clearfix"></div>
                                </div>
                            </div>
                        </div>
                    </div>
                  </div>
             </div>
    


         <div class="row">
                <div class="col-lg-6">
                    <div style="float: right; margin-top: 12px;">
                        
                    </div>
                </div>
                <!-- /.col-lg-12 -->
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
 
    </a>
</asp:Content>
