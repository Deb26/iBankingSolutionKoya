<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/ProjectBM.Master" CodeBehind="frmLoanRepayment.aspx.cs" Inherits="iBankingSolution.Transaction.frmLoanRepayment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.4.1/js/bootstrap-datepicker.min.js"></script>
        <%-- <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.4.1/css/bootstrap-datepicker3.css"/>
    <script src="http://code.jquery.com/ui/1.10.4/jquery-ui.min.js"></script>--%>
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

        
        function DevideTotCollection() {
            debugger;
            var ntxt_TotalCollection = $find("<%= ntxt_TotalCollection.ClientID %>");
            var ntxt_DemandCurrentInterest = $find("<%= ntxt_DemandCurrentInterest.ClientID %>");
            var ntxt_DemandDueInterest = $find("<%= ntxt_DemandDueInterest.ClientID %>");
            var ntxt_DemandPrincipalCurrent = $find("<%= ntxt_DemandPrincipalCurrent.ClientID %>");
            var ntxt_DemandOverdueInterest = $find("<%= ntxt_DemandOverdueInterest.ClientID %>");

            var ntxt_CollectionDueInterest = $find("<%= ntxt_CollectionDueInterest.ClientID %>");
         <%--  // var ntxt_CollectionPrincipalCurrent = $find("<%= ntxt_CollectionPrincipalCurrent.ClientID %>");
             var ntxt_CollectionPrincipalOutstanding = $find("<%= ntxt_CollectionPrincipalOutstanding.ClientID %>");
             var ntxt_CollectionPrincipalCurrent = $find("<%= ntxt_CollectionPrincipalCurrent.ClientID %>");  --%>
            var ntxt_CollectionOverdueInterest = $find("<%= ntxt_CollectionOverdueInterest.ClientID %>");
            var ntxt_CollectionCurrentInterest = $find("<%= ntxt_CollectionCurrentInterest.ClientID %>");

            var TotAmtCollection = parseFloat(ntxt_TotalCollection.get_value());
            var DemandDueInt = parseFloat(ntxt_DemandDueInterest.get_value());
            var DemandODInt = parseFloat(ntxt_DemandOverdueInterest.get_value());
            var DemandCurrInt = parseFloat(ntxt_DemandCurrentInterest.get_value());
            var DemandCurrPrincipal = parseFloat(ntxt_DemandPrincipalCurrent.get_value());

            var C_DueInt = TotAmtCollection >= DemandDueInt ? DemandDueInt : TotAmtCollection;
            TotAmtCollection = TotAmtCollection - C_DueInt;
            var C_ODInt = TotAmtCollection >= DemandODInt ? DemandODInt : TotAmtCollection;
            TotAmtCollection = TotAmtCollection - C_ODInt;
            var C_CurrInt = TotAmtCollection >= DemandCurrInt ? DemandCurrInt : TotAmtCollection;
            TotAmtCollection = TotAmtCollection - C_CurrInt;

            ntxt_CollectionPrincipalCurrent.set_value(TotAmtCollection);
            ntxt_CollectionCurrentInterest.set_value(C_CurrInt);
            ntxt_CollectionOverdueInterest.set_value(C_ODInt);
            <%--ss --%>
            ntxt_CollectionPrincipalCurrent.set_value(TotAmtCollection);
            ntxt_CollectionCurrentInterest.set_value(C_CurrInt);
            ntxt_CollectionOverdueInterest.set_value(C_ODInt);
            ntxt_CollectionDueInterest.set_value(DemandCurrInt + DemandDueInt + DemandODInt - C_CurrInt - C_DueInt - C_ODInt);
            ntxt_CollectionPrincipalOutstanding.set_value(DemandCurrPrincipal - TotAmtCollection);
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
        <script type="text/javascript" lang="js">

        function changetotalcollectionvalue() {
            debugger;
            var TotAmtCollection = document.getElementById('<%= ntxt_TotalCollection.ClientID %>').value;
            var DemandCurrInt = document.getElementById('<%= ntxt_DemandCurrentInterest.ClientID %>').value;
            var DemandODInt = document.getElementById('<%= ntxt_DemandOverdueInterest.ClientID %>').value;
            var DemandCurrPrincipal = document.getElementById('<%= ntxt_DemandPrincipalCurrent.ClientID %>').value;
            var DemandPrincipalOverdue = document.getElementById('<%= ntxt_DemandPrincipalOverdue.ClientID %>').value;

            var TotAmt = parseInt(DemandCurrInt) + parseInt(DemandODInt) + parseInt(DemandCurrPrincipal) + parseInt(DemandPrincipalOverdue);
            document.getElementById('<%= ntxt_TotalCollection.ClientID %>').value = TotAmt;
            document.getElementById('<%= lblAmtPaid.ClientID %>').value = TotAmt;

            document.getElementById('<%= lblCurPrin.ClientID %>').innerHTML = document.getElementById('<%= ntxt_DemandPrincipalCurrent.ClientID %>').value;
            document.getElementById('<%= lblODPrin.ClientID %>').innerHTML = document.getElementById('<%= ntxt_DemandPrincipalOverdue.ClientID %>').value;
            document.getElementById('<%= lblcurInt.ClientID %>').innerHTML = document.getElementById('<%= ntxt_DemandCurrentInterest.ClientID %>').value;
            document.getElementById('<%= lblODInt.ClientID %>').innerHTML = document.getElementById('<%= ntxt_DemandOverdueInterest.ClientID %>').value;

           //Display Due Principal Current
            var DuePrincipalCurrent = '<%=DemandPrincipalCurrent%>';  
            var DemandPrincipalCurrent= document.getElementById('<%= ntxt_DemandPrincipalCurrent.ClientID %>').value;
          <%--  document.getElementById('<%= ntxt_CollectionPrincipalCurrent.ClientID %>').value = parseInt(DuePrincipalCurrent) - parseInt(DemandPrincipalCurrent);--%>

            //Display Due Current interest
            var DueDemandCurrentInterest = '<%=DemandCurrentInterest%>';
            var DemandPrincipalCurrent= document.getElementById('<%= ntxt_DemandCurrentInterest.ClientID %>').value;
            document.getElementById('<%= ntxt_CollectionCurrentInterest.ClientID %>').value = parseInt(DueDemandCurrentInterest) - parseInt(DemandPrincipalCurrent);

           //Display Due Principal OD
            var DueDemandPrincipalOverdue = '<%=DemandPrincipalOverdue%>';
            var DemandPrincipalOverdue = document.getElementById('<%= ntxt_DemandPrincipalOverdue.ClientID %>').value;
         <%--   document.getElementById('<%= ntxt_CollectionPrincipalOverdue.ClientID %>').value = parseInt(DueDemandPrincipalOverdue) - parseInt(DemandPrincipalOverdue);--%>

             //Display Due Demand Overdue Interest
            var DueDemandOverdueInterest = '<%=DemandOverdueInterest%>';
            var DemandOverdueInterest = document.getElementById('<%= ntxt_DemandOverdueInterest.ClientID %>').value;
            document.getElementById('<%= ntxt_CollectionOverdueInterest.ClientID %>').value = parseInt(DueDemandOverdueInterest) - parseInt(DemandOverdueInterest);

            //Display Due Demand principal outstanding
            var DueDemandPrincipalOutstanding = '<%=DemandPrincipalOutstanding%>';
            var DemandPrincipalOutstanding = document.getElementById('<%= ntxt_DemandPrincipalOutstanding.ClientID %>').value;
         <%--   document.getElementById('<%= ntxt_CollectionPrincipalOutstanding.ClientID %>').value = parseInt(DueDemandPrincipalOutstanding) - parseInt(DemandPrincipalOutstanding);--%>

            //Display Due Interest
            var DueDemandDueInterest = '<%=DemandDueInterest%>';
            var DemandDueInterest = document.getElementById('<%= ntxt_DemandDueInterest.ClientID %>').value;
            document.getElementById('<%= ntxt_CollectionDueInterest.ClientID %>').value = parseInt(DueDemandDueInterest) - parseInt(DemandDueInterest);

            document.getElementById("<%=HiddenCurPrin.ClientID %>").value = document.getElementById('<%= ntxt_DemandPrincipalCurrent.ClientID %>').value;
            document.getElementById("<%=HiddenODPrin.ClientID %>").value = document.getElementById('<%= ntxt_DemandPrincipalOverdue.ClientID %>').value; 
            document.getElementById("<%=HiddencurInt.ClientID %>").value = document.getElementById('<%= ntxt_DemandCurrentInterest.ClientID %>').value;
            document.getElementById("<%=HiddenODInt.ClientID %>").value = document.getElementById('<%= ntxt_DemandOverdueInterest.ClientID %>').value;
         
            
          <%--  //display Report Lebel ODPrin
            document.getElementById('<%= lblODPrin.ClientID %>').innerHTML = document.getElementById('<%= ntxt_DemandPrincipalOverdue.ClientID %>').value;
            //display Report Lebel Curr Int
            document.getElementById('<%= lblcurInt.ClientID %>').innerHTML = = document.getElementById('<%= ntxt_DemandCurrentInterest.ClientID %>').value;
             //display Report Lebel OD Int
            document.getElementById('<%= lblODInt.ClientID %>').innerHTML = = document.getElementById('<%= ntxt_DemandOverdueInterest.ClientID %>').value;
         --%>
            
        
        }




    </script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <asp:ScriptManager ID="scrpt1" runat="server" EnablePageMethods="true"></asp:ScriptManager>
    <div class="row">
        <div class="col-lg-12">
            <div style="float: right; margin-top: 12px;">
                <asp:Button ID="btnsubmit1" runat="server" Text="Save" class="btn btn-primary" OnClick="btnsubmit_Click" />

                <asp:Button ID="btnCancel" runat="server" Text="Cancel" class="btn btn-primary" OnClick="btnCancel_Click" />
                <a href="frmLoanRepaymentList.aspx" class="btn btn-default">Back to List</a>
            </div>
            <h1 class="page-header">Loan Repayment</h1>
        </div>
        <!-- /.col-lg-12 -->
    </div>
    <div class="row">
        <div class="col-lg-6">
            <div class="panel panel-primary">
                <div class="panel-heading" style="font-size:larger;">
                    Loan Repayment
                </div>
                <div class="panel-heading" runat="server" id="DivID" visible="false">
                    <asp:Label ID="Label1" runat="server" Visible="False" align="center" Font-Bold="True" Font-Italic="False" Font-Names="Verdana" ForeColor="White"></asp:Label>
                </div>
                <div class="panel-body">
                    <div class="row">

                        <div class="form-group">
                            <div class="col-md-12">
                                <div class="panel panel-primary">

                                    <div class="panel-heading text-center" style="font-size:larger;">
                                        <b>Account Head</b>

                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6">
                                 <label style="margin-right: 20PX;font-size:larger;">Collection Date</label>
                                <asp:TextBox ID="dtpkr_CollectionDate" CssClass="form-control input-sm BootDatepicker" Font-Size="10" Format="dd/MM/yyyy" runat="server" onFocus="this.select()" autocomplete="off" required="required" OnTextChanged="dtpkr_CollectionDate_TextChanged" AutoPostBack ="true"></asp:TextBox>
                       
                            </div>

                            <div class="col-md-6">
                                 <label style="margin-right: 10PX;font-size:larger;">Loan A/c No.</label>
                                <asp:Label ID="lblDid" runat="server" BackColor="White" ForeColor="White"></asp:Label>

                                <asp:DropDownList ID="cmbx_AccountNo" runat="server" CssClass="form-control" Font-Size="10" AutoPostBack="True" OnSelectedIndexChanged="cmbx_AccountNo_SelectedIndexChanged">
                                </asp:DropDownList>
                                       <br />
                            </div>

                            <div class="col-md-6">
                                <label style="margin-right: 20PX;font-size:larger;">Account Head</label>
                                <asp:TextBox ID="txtAccountHead" CssClass= "form-control" Font-Size="10" runat="server" onFocus="this.select()" autocomplete="off" required="required" AutoPostBack="True"  Enabled="False"></asp:TextBox>
                               <%-- <br />--%>
                            </div>
                           <%-- <div class="clearfix"></div><br/>--%><%--CssClass="md-form form-controll"--%>
                            <div class="col-md-6">
                                <label style="margin-right: 20px;font-size:larger;">Account Balance</label>
                                <asp:TextBox ID="txtAccountBalance" CssClass="form-control" Font-Size="10" runat="server" Enabled="false"></asp:TextBox>
                            </div>

                            <div class="clearfix"></div><br />
                           
                            <div class="col-md-12">
                                <div class="panel panel-primary">

                                    <div class="panel-heading text-center" style="font-size:larger;">
                                        <b>Received Type</b>

                                    </div>
                                </div>
                            </div>

                            <div class="col-md-7" style="color:red;">
                                <%--<label style="margin-right: 20PX;"></label>--%>
                                
                                <asp:RadioButtonList ID="rdobtn_ReceivedType" runat="server" RepeatDirection="Horizontal" Font-Size="10" Font-Italic="true" AutoPostBack="true" OnSelectedIndexChanged="rdobtn_ReceivedType_SelectedIndexChanged" CellPadding="5" CellSpacing="5" Width="600px">
                                    <asp:ListItem Text="Cash Received" Value="Cash" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="A/c Adjustment" Value="A/c Adjustment"></asp:ListItem> 
                                    <asp:ListItem Text="Bank Adjustment" Value="Bank"></asp:ListItem>
                                </asp:RadioButtonList>
                                    
                            </div>
                            <br />
                             <div class="clearfix"></div>
                              <div class="col-md-4">

                              </div>
                            <div class="col-md-3">
                               
                                <%--<b><i>Savings A/C</i></b>--%><%--<asp:CheckBox ID="chkbx_IsBank" runat="server" Enabled="false" /> ByA/CNo--%>
                                <asp:DropDownList ID="cmbx_SavingsAcNo" runat="server" CssClass="form-control" Font-Size="10" Visible="false" AutoPostBack="true" OnSelectedIndexChanged="Select_Cmbx_SavingsAcNo">
                                </asp:DropDownList>

                            </div>
                            <div class="col-md-0"></div>
                            <div class="col-md-4">
                                <%--<b><i>Ledger Code</i></b>--%>
                                <asp:DropDownList ID="cmbx_LdgCode" runat="server" CssClass="form-control" Font-Size="10" Visible="false"></asp:DropDownList>
                            </div>
                            <div class="clearfix"></div><br />
                            <div class="col-md-12">
                                <div class="panel panel-primary">

                                    <div class="panel-heading text-center">
                                        <b></b>

                                    </div>
                                </div>
                            </div>
                    

                            <div class="clearfix"></div>
                            <div class="col-md-4">
                                <label style="margin-right: 20PX;font-size:larger;">Total Collection</label>
                                <asp:TextBox ID="ntxt_TotalCollection" CssClass="form-control" runat="server" onFocus="this.select()" autocomplete="off" AutoPostBack="True" OnTextChanged="ntxt_TotalCollection_TextChanged" Enabled="False"></asp:TextBox>
                            </div>

                            <%--  <div class="col-md-6">
                                    <label style="margin-right: 20PX;">Actual Balance</label>
                                    <asp:TextBox ID="ntxt_ActualBalance" CssClass="form-control" runat="server" onFocus="this.select()" autocomplete="off"></asp:TextBox>
                            </div>--%>

                            <div class="col-md-4">
                                <label style="margin-right: 20PX;font-size:larger;">Cash Book</label>
                                <asp:TextBox ID="txt_CashBook" CssClass="form-control" runat="server" onFocus="this.select()" autocomplete="off" Enabled="False"></asp:TextBox>
                            </div>

                            <div class="col-md-4">
                                <label style="margin-right: 20PX;font-size:larger;">Actual Balance</label>
                                <asp:TextBox ID="ntxt_ActualBalance" CssClass="form-control" runat="server" onFocus="this.select()" autocomplete="off" Enabled="False"></asp:TextBox>
                                <br />
                            </div>
                            <div class="clearfix"></div><br />
                            <div class="col-md-12">

                                <div class="panel panel-primary">

                                    <div class="panel-heading text-center" style="font-size:larger;">
                                        <b>Collection Details</b>

                                    </div>
                                </div>
                            </div>

                            <div class="col-md-4">
                                <label style="margin-right: 20PX;font-size:larger;">Prin. Outstand</label>
                                <asp:TextBox ID="ntxt_DemandPrincipalOutstanding" CssClass="form-control" runat="server" onFocus="this.select()" autocomplete="off" onchange="changetotalcollectionvalue();"></asp:TextBox>
                            </div>

                            <div class="col-md-4">
                                <label style="margin-right: 20PX;font-size:larger;">Prin. OD</label>
                                <asp:TextBox ID="ntxt_DemandPrincipalOverdue" CssClass="form-control" runat="server" onFocus="this.select()" autocomplete="off"  OnTextChanged="ntxt_DemandPrincipalOverdue_TextChanged" onchange="changetotalcollectionvalue();"></asp:TextBox>

                            </div>

                            <div class="col-md-4">
                                <label style="margin-right: 20PX;font-size:larger;">Prin. Curr.</label>
                                <asp:TextBox ID="ntxt_DemandPrincipalCurrent" runat="server" onFocus="this.select()" CssClass="form-control" autocomplete="off" OnValueChanged="DevideTotCollection();" onchange="changetotalcollectionvalue();"></asp:TextBox>
                                <br />
                            </div>



                            <div class="col-md-4">
                                <label style="margin-right: 20PX;font-size:larger;">Due Int.</label>
                                <asp:TextBox ID="ntxt_DemandDueInterest" runat="server" onFocus="this.select()" CssClass="form-control" autocomplete="off"  onchange="changetotalcollectionvalue();"></asp:TextBox>
                            </div>

                            <div class="col-md-4">
                                <label style="margin-right: 20PX;font-size:larger;">OD. Int.</label>
                                <asp:TextBox ID="ntxt_DemandOverdueInterest" runat="server" onFocus="this.select()" CssClass="form-control" autocomplete="off"  onchange="changetotalcollectionvalue();"></asp:TextBox>
                            </div>

                            <div class="col-md-4">
                                <label style="margin-right: 20PX;font-size:larger;">Curr. Int.</label>
                                <asp:TextBox ID="ntxt_DemandCurrentInterest" runat="server" onFocus="this.select()" CssClass="form-control" autocomplete="off"  onchange="changetotalcollectionvalue();"></asp:TextBox>
                                <br />
                            </div>

                            <%--<b>Demand Details</b>--%>
                            <div class="form-group">

                                <div class="col-md-12">
                                    <div class="panel panel-primary">

                                        <div class="panel-heading text-center" style="font-size:larger;">
                                            <b>Demand Details</b>

                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-4">
                                    <label style="margin-right: 20PX;font-size:larger;">Due Int. </label>
                                    
                                    
                                       <asp:TextBox ID="ntxt_DemandDueInt" runat="server" onFocus="this.select()" CssClass="form-control" autocomplete="off" AutoPostBack="True" Enabled="False"></asp:TextBox>

                                </div>

                                <div class="col-md-4">
                                    <label style="margin-right: 20PX;font-size:larger;">OD Int.</label>
                                  
                                    <asp:TextBox ID="ntxt_DemandODInt" runat="server" onFocus="this.select()" CssClass="form-control" autocomplete="off" AutoPostBack="True" Enabled="False"></asp:TextBox>
                                </div>
                                <div class="col-md-4">
                                    <label style="margin-right: 20PX;font-size:larger;">Curr Int.</label> <br />
                                    
                                      <asp:TextBox ID="ntxt_DemandCurrInt" runat="server" CssClass="form-control" Enabled="false"/>
                                    
                                </div>
                                
                                

                            </div>
                            <div class="clearfix"></div><br /><br />
                             <%--<b>Due Details</b>--%>
                             <div class="form-group">

                                <div class="col-md-12">
                                    <div class="panel panel-primary">

                                        <div class="panel-heading text-center" style="font-size:larger;">
                                            <b>Due Details</b>

                                        </div>
                                    </div>
                                </div>

                         <%--       <div class="col-md-4">
                                    <label style="margin-right: 20PX;">Prin. Outstand</label>
                                    
                                    <asp:TextBox ID="ntxt_CollectionPrincipalOutstanding" runat="server" onFocus="this.select()" CssClass="form-control" autocomplete="off" AutoPostBack="True" Enabled="False" Font-Size="10"></asp:TextBox>
                                </div>

                                <div class="col-md-4">
                                    <label style="margin-right: 20PX;">Prin. OD.</label>
                                    <asp:TextBox ID="ntxt_CollectionPrincipalOverdue" runat="server" onFocus="this.select()" CssClass="form-control" autocomplete="off" AutoPostBack="True" Enabled="False" Font-Size="10"></asp:TextBox>
                                </div>
                                <div class="col-md-4">
                                    <label style="margin-right: 20PX;">Prin. Curr.</label> <br />
                                    <asp:TextBox ID="ntxt_CollectionPrincipalCurrent" runat="server" CssClass="form-control" Font-Size="10"/>

                                    <br />
                                </div>--%>
                                <div class="col-md-4">
                                    <label style="margin-right: 20PX;font-size:larger;">Due. Int.</label> 
     
                                    <asp:TextBox ID="ntxt_CollectionDueInterest" runat="server" CssClass="form-control" AutoPostBack="True" required="required" Enabled="False" Font-Size="10"/>
                                </div>
                                <div class="col-md-4">
                                    <label style="margin-right: 20PX;font-size:larger;">Overdue Int. </label>       
                                    <asp:TextBox ID="ntxt_CollectionOverdueInterest" runat="server" CssClass="form-control" Enabled="False" Font-Size="10"/>
                                </div>
                                <div class="col-md-4">
                                    <label style="margin-right: 20PX;font-size:larger;">Curr. Int. </label>       
                                    <asp:TextBox ID="ntxt_CollectionCurrentInterest" runat="server" CssClass="form-control" onkeypress="return isNumberKey(event)" Enabled="False" Font-Size="10"/>
                                    <br />
                                </div>

                              <%--      <div class="col-md-4">
                                    <label style="margin-right: 20PX;">Prin. Outstand</label>
                                    
                                    <asp:TextBox ID="ntxt_CollectionPrincipalOutstanding" runat="server" onFocus="this.select()" CssClass="form-control" autocomplete="off" AutoPostBack="True" Enabled="False" Font-Size="10"></asp:TextBox>
                                </div>--%>

                            </div>
                        </div>

                    </div>

                    <div class="row">
                    </div>
                </div>
            </div>
        </div>


        <div class="col-lg-6">
            <div class="panel panel-primary">
                <div class="panel-heading" style="font-size:larger;">
                    Loanee Member Details                                     
              
                    <asp:Label ID="Label2" runat="server" Visible="False" align="center" Font-Bold="True" Font-Italic="False" Font-Names="Verdana" ForeColor="White"></asp:Label>
                </div>
                <asp:Panel ID="Panel1" runat="server" Height="60px">
                    <%--<div class="panel-body">--%>
                        <div class="panel-body">
                            <asp:GridView ID="GVMemberDetail" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" Height="40px" Width="100%">
                                <Columns>
                                    <asp:TemplateField HeaderText="CUST ID">
                                        <ItemTemplate>
                                            <asp:Label ID="lblcustid" runat="server" Text='<%# Bind("CUST_ID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="NAME">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_name" runat="server" Text='<%# Bind("NAME") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="GURDIAN">
                                        <ItemTemplate>
                                            <asp:Label ID="lblguarname" runat="server" Text='<%# Bind("GUARDIAN_NAME") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="BLOCK">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_blkcode" runat="server" Text='<%# Bind("BLK_CODE") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="VILLAGE">
                                        <ItemTemplate>
                                            <asp:Label ID="lblblkcode" runat="server" Text='<%# Bind("VILL_CODE") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="DISTRICT">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_distcode" runat="server" Text='<%# Bind("DIS_CODE") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle BackColor="White" ForeColor="#000066" />
                                <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                <RowStyle ForeColor="#000066" />
                                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                <SortedDescendingHeaderStyle BackColor="#00547E" />
                            </asp:GridView>
                        <%--</div>--%>
                        
                    </div>
                </asp:Panel>
               <%-- <asp:Button ID="Button1" runat="server" Text="Print" OnClientClick="doPrint()"/>--%>  <%--ScrollBars="Both"--%>
            </div>
            <asp:Panel ID="PanelHist" runat="server" Height="120px">
                <div class="panel panel-primary">
                    <div class="panel-heading" style="font-size:larger;">
                        Repayment History
                    </div>
                    <div class="panel-heading" runat="server" id="Div5" visible="false">
                        <asp:Label ID="Label17" runat="server" Visible="False" align="center" Font-Bold="True" Font-Italic="False" Font-Names="Verdana" ForeColor="White"></asp:Label>
                    </div>
                    <div class="panel-body">
                         <div style ="height:50px; width:585px; overflow:auto;">
                        <asp:GridView ID="GVRepayHist" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" Height="50px" Width="100%">
                            <Columns>
                                <asp:TemplateField HeaderText="RepayDate">
                                    <ItemTemplate>
                                        <asp:Label ID="lblrepdate" runat="server"   Text='<%# Bind("REP_DATE","{0:dd/MM/yyyy}") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="PrinCur.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblprin_curr" runat="server" Text='<%# Bind("prin_curr") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Int Curr.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblint_curr" runat="server" Text='<%# Bind("int_curr") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Int OD">
                                    <ItemTemplate>
                                        <asp:Label ID="lblint_od" runat="server" Text='<%# Bind("int_od") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Ins Type">
                                    <ItemTemplate>
                                        <asp:Label ID="lblins_type" runat="server" Text='<%# Bind("ins_type") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>


                            </Columns>
                            <FooterStyle BackColor="White" ForeColor="#000066" />
                            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
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
            </asp:Panel>





            <div class="panel panel-primary">
                <div class="panel-heading" style="font-size:larger;">
                    Repayment Details
                </div>
                <div class="panel-heading" runat="server" id="Div2" visible="false">
                    <asp:Label ID="Label3" runat="server" Visible="False" align="center" Font-Bold="True" Font-Italic="False" Font-Names="Verdana" ForeColor="White"></asp:Label>
                </div>
                <div class="panel-body">
                    ` 
                    <div class="col-md-3">
                        <label style="margin-right: 20PX;">Last Repay Dt</label>
                        <asp:TextBox ID="txt_lrdate" CssClass="form-control input-sm BootDatepicker" Font-Size="10" runat="server" onFocus="this.select()" autocomplete="off" Enabled="false"></asp:TextBox>
                    </div>

                    <div class="col-md-2">
                        <label style="margin-right: 20PX;">IntrestRate</label>
                        <asp:TextBox ID="txt_roii" CssClass="form-control" Font-Size="10" runat="server" onFocus="this.select()" autocomplete="off" Enabled="false"></asp:TextBox>

                    </div>

                    <div class="col-md-3">
                        <label style="margin-right: 20PX;">Demand Days</label>
                        <asp:TextBox ID="txt_daysdemand" CssClass="form-control" Font-Size="10" runat="server" onFocus="this.select()" autocomplete="off" Enabled="false"></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        <label style="margin-right: 20PX;">OD ROI</label>
                        <asp:TextBox ID="txt_odroi" CssClass="form-control" Font-Size="10" runat="server" onFocus="this.select()" autocomplete="off" Enabled="false"></asp:TextBox>
                    </div>
                </div>
            </div>


            <div class="panel panel-primary">
                <div class="panel-heading" style="font-size:larger;">
                    Loan Repayment
                </div>
                <div class="panel-heading" runat="server" id="Div3" visible="false">
                    <asp:Label ID="Label4" runat="server" Visible="False" align="center"  Font-Italic="False" Font-Names="Times New Roman" ForeColor="White"></asp:Label>
                </div>

                <%--<div class="panel-body scrollable-panel">runat="server" style="overflow-y: scroll; height: 200px"--%> <%----%>
                <div class="panel-body" runat="server" style="overflow-y: scroll; height: 240px">

                    <%-- <asp:Panel ID="Panel_dtl" runat="server">--%>
                    <table>
                        <tr>
                            <td>

                                <label style="margin-right: 20PX;">Society Name:</label>
                            </td>
                            <td>
                                <asp:Label ID="lblsocietyname" runat="server" Font-Size="10" Text="" Enabled="false"></asp:Label>

                            </td>

                        </tr>

                        <tr>
                            <td>
                                <label style="margin-right: 20PX;">Receipt:</label>
                            </td>
                            <td>
                                <asp:Label ID="lblReceipt" runat="server" Font-Size="10"></asp:Label>
                            </td>

                        </tr>
                        <tr>
                            <td>
                                <label style="margin-right: 20PX;">A/C Head:</label>
                            </td>
                            <td>
                                <asp:Label ID="lblHead" runat="server" Font-Size="10"></asp:Label>
                            </td>

                        </tr>

                        <tr>
                            <td>
                                <label style="margin-right: 20PX;">System A/C No:</label>
                            </td>
                            <td>
                                <asp:Label ID="lblsysAc" runat="server" Font-Size="10"></asp:Label>
                            </td>

                        </tr>
                        <tr>
                            <td>
                                <label style="margin-right: 20PX;">Name:</label>
                            </td>
                            <td>
                                <asp:Label ID="lblName" runat="server" Font-Size="10"></asp:Label>
                            </td>

                        </tr>
                        <tr>
                            <td>
                                <label style="margin-right: 20PX;">Repay Date:</label>
                            </td>
                            <td>
                                <asp:Label ID="lblRepayDt" runat="server" Font-Size="10" Text='<%# Bind("REP_DATE","{0:dd/MM/yyyy}")%>'></asp:Label>
                            </td>

                        </tr>

                        <tr>
                            <td>
                                <label style="margin-right: 20PX;">Amount Paid:</label>
                            </td>
                            <td>
                                <asp:Label ID="lblAmtPaid" runat="server" Font-Size="10"></asp:Label>
                            </td>

                        </tr>
                        <tr>
                            <td>
                                <label style="margin-right: 20PX;">Curr Prin:</label>
                            </td>
                            <td>
                                <asp:Label ID="lblCurPrin" runat="server" Font-Size="10"></asp:Label>
                                <asp:HiddenField ID="HiddenCurPrin" runat="server" />
                            </td>

                        </tr>
                        <tr>
                            <td>
                                <label style="margin-right: 20PX;">OD Prin:</label>
                            </td>
                            <td>
                                <asp:Label ID="lblODPrin" runat="server" Font-Size="10"></asp:Label>
                                <asp:HiddenField ID="HiddenODPrin" runat="server" />
                            </td>

                        </tr>
                        <tr>
                            <td>
                                <label style="margin-right: 20PX;">Curr Int:</label>
                            </td>
                            <td>
                                <asp:Label ID="lblcurInt" runat="server" Font-Size="10"></asp:Label>
                                <asp:HiddenField ID="HiddencurInt" runat="server" />
                            </td>

                        </tr>
                        <tr>
                            <td>
                                <label style="margin-right: 20PX;">OD Int:</label>
                            </td>
                            <td>
                                <asp:Label ID="lblODInt" runat="server" Font-Size="10"></asp:Label>
                                <asp:HiddenField ID="HiddenODInt" runat="server" />
                            </td>

                        </tr>

                      <%--  <tr>
                            <td>
                                <label style="margin-right: 20PX;">Adv Prin:</label>
                            </td>
                            <td>
                                <asp:Label ID="lblAdvPrin" runat="server" Font-Size="10"></asp:Label>
                            </td>

                        </tr>--%>

                    </table>
                    <div class="col-md-12">
                        <div style="float: right; margin-top: 12px;">
                            <asp:Button ID="btnprint" runat="server" Font-Size="10" Text="Print" class="btn btn-primary" OnClick="btnprint_Click" />
                        </div>
                    </div>
                    <%-- </asp:Panel>--%>
                </div>
            </div>


        </div>


    </div>


    <div class="row">
        <div class="col-lg-4">
            <div style="float: right; margin-top: 12px;">
                <asp:Button ID="btnsubmit" runat="server" Text="Save" class="btn btn-primary" OnClick="btnsubmit_Click" />
                <a href="frmLoanRepayment.aspx" class="btn btn-outline btn-danger">Cancel</a>

            </div>
        </div>
        <!-- /.col-lg-12 -->
    </div>
    <br />
    <br />
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
