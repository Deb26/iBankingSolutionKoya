<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/ProjectBM.Master" CodeBehind="frmVoterList.aspx.cs" Inherits="iBankingSolution.Report.frmVoterList" %>

 <asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="http://code.jquery.com/ui/1.10.4/jquery-ui.min.js"></script>
    <script type="text/javascript">
        $ = jQuery.noConflict();
        $(function () {
            $('.select2').select2()
        })

        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;
            return true;
        }

 </script>       
 

<%--    <script>
        function doPrint()
        {
            var prtContent = document.getElementById('<%= GVMemberDetail.ClientID %>');
            prtContent.border = 0; //set no border here
            var WinPrint = window.open('','','left=100,top=100,width=1000,height=1000,toolbar=0,scrollbars=1,status=0,resizable=1');
            WinPrint.document.write(prtContent.outerHTML);
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();
        }
</script>--%>
 

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <asp:ScriptManager ID="scrpt1" runat="server" EnablePageMethods="true"></asp:ScriptManager>
    <div class="row">
        <div class="col-lg-12">
            <div style="float: right; margin-top: 12px;">
                 

 
                
            </div>
            <h1 class="page-header">Voter List Details</h1>
        </div>
        <!-- /.col-lg-12 -->
    </div>
    <div class="row">
        <div class="col-lg-12">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    Voter List Details
                </div>

                <div class="panel-body">
                        <div class="row">

                            <div class="form-group">
  
                               
                                <div  class="col-md-2">
                                     <label style="margin-right: 20PX;">Select Village</label>
                                    
                                    <asp:DropDownList ID="ddlvillage" runat="server" CssClass="form-control" class="styled-select"  BackColor="White" ForeColor="Black" Font-Size="10pt" AutoPostBack="True" OnSelectedIndexChanged="ddlvillage_SelectedIndexChanged">
 
                                    </asp:DropDownList>

                                </div>
                                
                                <hr />
                            </div>

                        </div>
                    </div>
                <div class="panel-heading" runat="server" id="DivID" visible="false">
                    <asp:Label ID="Label1" runat="server" Visible="False" align="center" Font-Bold="True" Font-Italic="False" Font-Names="Verdana" ForeColor="White"></asp:Label>
                </div>

                <div class="panel-body" align="center">
                    
             <div class="panel-body">
                            <asp:GridView ID="GVMemberDetail" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" AllowPaging="True" AllowSorting="True" OnPageIndexChanging="GVMemberDetail_PageIndexChanging" PageSize="50" ShowFooter="True" Font-Names="Verdana" style="margin-right: 51px">
                                <Columns>

                                    <asp:TemplateField HeaderText="SL.No.">
                                        <ItemTemplate>
                                              <asp:Label ID="lblsrL" runat="server" Text='<%# (((GridViewRow)Container).RowIndex + 1).ToString()%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                  
                                               
                                    <asp:TemplateField HeaderText="Cust ID">
                                        <ItemTemplate>
                                            <asp:Label ID="lblcustid" runat="server" Text='<%# Bind("CUST_ID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_name" runat="server" Text='<%# Bind("NAME") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Gurdian">
                                        <ItemTemplate>
                                            <asp:Label ID="lblguarname" runat="server" Text='<%# Bind("GUARDIAN_NAME") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Block  ">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_blkcode" runat="server" Text='<%# Bind("BLK_CODE") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Village  ">
                                        <ItemTemplate>
                                            <asp:Label ID="lblblkcode" runat="server" Text='<%# Bind("VILL_CODE") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="PO ">
                                        <ItemTemplate>
                                            <asp:Label ID="lblblkcode" runat="server" Text='<%# Bind("PO_CODE") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="PS ">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_distcode" runat="server" Text='<%# Bind("PS_CODE") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                 

                                    <asp:TemplateField HeaderText="District ">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_distcode" runat="server" Text='<%# Bind("DIS_CODE") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                  

                                      
                                 <asp:TemplateField HeaderText="Pin">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPincode" runat="server" Text='<%# Bind("PIn") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle BackColor="White" ForeColor="#000066" />
                                <HeaderStyle BackColor="#373737" Font-Bold="True" ForeColor="White" Font-Names="Cambria" Font-Size="14pt" />
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