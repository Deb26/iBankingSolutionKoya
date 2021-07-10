<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/ProjectBM.Master" CodeBehind="Dashboard.aspx.cs" Inherits="iBankingSolution.Dashboard" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
        canvas {
            -moz-user-select: none;
            -webkit-user-select: none;
            -ms-user-select: none;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-lg-12">
            <h1 class="page-header">Welcome to Evantage Banking Software</h1>
        </div>
        <!-- /.col-lg-12 -->
    </div>
    <!-- /.row -->
    <div class="row">
        <div class="col-lg-3 col-md-6">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <div class="row">
                        <div class="col-xs-3">
                            <i class="fa fa-comments fa-5x"></i>
                        </div>
                        <div class="col-xs-9 text-right">
                            <div class="huge"></div>
                            <%--<%= this.DB.CountEnquiry %>--%>
                            <div>Total Loan Accounts</div>
                        </div>
                    </div>
                </div>
                <a href="<%=  Application["BasePage"] %>/CRM/frmEnquiryList.aspx">
                    <div class="panel-footer">
                        <span class="pull-left">View Details</span>
                        <span class="pull-right"><i class="fa fa-arrow-circle-right"></i></span>
                        <div class="clearfix"></div>
                    </div>
                </a>
            </div>
        </div>

        <div class="col-lg-3 col-md-6">
            <div class="panel panel-green">
                <div class="panel-heading">
                    <div class="row">
                        <div class="col-xs-3">
                            <i class="fa fa-tasks fa-5x"></i>
                        </div>
                        <div class="col-xs-9 text-right">
                            <div class="huge"></div>
                            <%--<%= this.DB.CountPO %>--%>
                            <div>Total Saving Accounts</div>
                        </div>
                    </div>
                </div>
                <a href="<%=  Application["BasePage"] %>/Purchase/frmPurchaseOrderList.aspx">
                    <div class="panel-footer">
                        <span class="pull-left">View Details</span>
                        <span class="pull-right"><i class="fa fa-arrow-circle-right"></i></span>
                        <div class="clearfix"></div>
                    </div>
                </a>
            </div>
        </div>
        <div class="col-lg-3 col-md-6">
            <div class="panel panel-yellow">
                <div class="panel-heading">
                    <div class="row">
                        <div class="col-xs-3">
                            <i class="fa fa-shopping-cart fa-5x"></i>
                        </div>
                        <div class="col-xs-9 text-right">
                            <%--<div class="huge"><%= this.DB.CountSO %></div>--%>
                            <div>Total Daily Deposits</div>
                        </div>
                    </div>
                </div>
                <a href="<%= Application["BasePage"] %>/Purchase/frmSalesOrderList.aspx">
                    <div class="panel-footer">
                        <span class="pull-left">View Details</span>
                        <span class="pull-right"><i class="fa fa-arrow-circle-right"></i></span>
                        <div class="clearfix"></div>
                    </div>
                </a>
            </div>
        </div>
        <div class="col-lg-3 col-md-6">
            <div class="panel panel-red">
                <div class="panel-heading">
                    <div class="row">
                        <div class="col-xs-3">
                            <i class="fa fa-support fa-5x"></i>
                        </div>
                        <div class="col-xs-9 text-right">
                            <div class="huge"></div>
                            <%-- <%= this.DB.CountProject %>--%>
                            <div>Total Closed Account(s)</div>
                        </div>
                    </div>
                </div>
                <a href="<%= Application["BasePage"] %>/ProjectMgmt/ProjectList.aspx">
                    <div class="panel-footer">
                        <span class="pull-left">View Details</span>
                        <span class="pull-right"><i class="fa fa-arrow-circle-right"></i></span>
                        <div class="clearfix"></div>
                    </div>
                </a>
            </div>
        </div>
    </div>
    <div class="clearfix"></div>
   <%-- Starting For Total Deposite--%>
    <div class="row">
        <div class="col-lg-6">
            <div class="panel panel-default">
                <div class="panel-heading" style="font-size:larger;">
                    <i class="fa fa-bar-chart-o fa-fw"></i>Total Deposit 
                            <div class="pull-right">
                                <div class="btn-group">
                                
                               
                                    

                                </div>
                                
                            </div>
                </div>
                <!-- /.panel-heading -->
                <div class="panel-body">
                     <%--<div style="overflow: scroll;" runat="server" id="RepeaterControls"> --%>
                     <%--<div style ="height:50px; width:35px; overflow:auto;">--%>
                    <%--<div style="width: 80px; height: 100px;">--%>
                       <div style ="height:80px; width:585px; overflow:auto;">
                        <table width="100%" class="table table-striped table-bordered table-hover" id="dataTables-exampleTotalDeposit">
                            <thead>
                                <tr>
                                    <th>Srl.</th>
                                    <th>Type of Deposit</th>
                                    <th>Balance</th>
                                    <th>Active Account</th>
                                   
                                    
                                </tr>
                            </thead>
                            <tbody>
                               <asp:Repeater ID="Repeater2" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblsrL" runat="server" Text='<%# (((RepeaterItem)Container).ItemIndex+1).ToString()%>'></asp:Label>
                                                

                                            </td>

                                            <td>

                                                <asp:Label ID="lblsl_code" runat="server" Text='<%# Eval("SCHEME_TYPE")%>'></asp:Label>
                                                
                                            </td>
                                            <td>
                                                <asp:Label ID="lblScheme_name" runat="server" Text='<%# Eval("CL_BAL_DEPOSIT") %>'></asp:Label>
                                            </td>

                                            <td>

                                                <asp:Label ID="lblAPPL_DT" runat="server" Text='<%# Eval("LDG_CODE")%>'></asp:Label>
                                                
                                            </td>
                                            
                           
  
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                                 
                            </tbody>
                        </table>
                          </div>
                    </div>
                </div>
        
            </div>
        <%--</div>--%>   
         <%--End Of Total Deposite--%>
         <%--starting of total Borrowing--%>
        
        <div class="col-lg-6">
            <div class="panel panel-default">
                <div class="panel-heading" style="font-size:larger;">
                    <i class="fa fa-bar-chart-o fa-fw"></i>Total Borrowing
                            <div class="pull-right">
                                <div class="btn-group">
                                
                               
                                    

                                </div>
                                
                            </div>
                </div>
                <!-- /.panel-heading -->
                <div class="panel-body">
                    <%--<div style="width: 60px; height: 100px;">--%>
                        <%--<div style="overflow: scroll;" runat="server" id="RepeaterControls"> --%>
                    <div style ="height:80px; width:585px; overflow:auto;">
                        <table width="100%" class="table table-striped table-bordered table-hover" id="dataTables-exampleTotalLoanBorrowing">
                            <thead>
                                <tr>
                                    <th>Srl.</th>
                                    <th>Type of Deposit</th>
                                    <th>Balance</th>
                                    <th>Active Account</th>
                                   
                                    
                                </tr>
                            </thead>
                            <tbody>
                               <asp:Repeater ID="Repeater3" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblsrL" runat="server" Text='<%# (((RepeaterItem)Container).ItemIndex+1).ToString()%>'></asp:Label>
                                                

                                            </td>

                                            <td>

                                                <asp:Label ID="lblsl_code" runat="server" Text='<%# Eval("SL_CODE")%>'></asp:Label>
                                                
                                            </td>
                                            <td>
                                                <asp:Label ID="lblScheme_name" runat="server" Text='<%# Eval("NAME") %>'></asp:Label>
                                            </td>

                                            <td>

                                                <asp:Label ID="lblAPPL_DT" runat="server" Text='<%# Eval("INT_AMT")%>'></asp:Label>
                                                
                                            </td>
                                            
                           
  
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                                 
                            </tbody>
                        </table>
                          
                    </div>
                </div>
        
            </div>
        </div>   
        <div class="clearfix"></div>
          <%--end of total Borrowing--%>
         <%--starting of Total Loan--%>
        <div class="col-lg-6">
           <div class="panel panel-default">
                <div class="panel-heading" style="font-size:larger;">
                    <i class="fa fa-bar-chart-o fa-fw"></i>Total Loan
                            <div class="pull-right">
                                <div class="btn-group">
                                
                               
                                    

                                </div>
                                
                            </div>
                </div>
                <!-- /.panel-heading -->
                <div class="panel-body">
                    <%--<div style="width: 160px; height: 100px;">--%>
                        <%--<div style="overflow: scroll;" runat="server" id="RepeaterControls"> --%>
                     <div style ="height:80px; width:585px; overflow:auto;">
                        <table width="100%" class="table table-striped table-bordered table-hover" id="dataTables-exampleTotalLoan">
                            <thead>
                                <tr>
                                    <th>Srl.</th>
                                    <th>Type of Loan</th>
                                    <th>Balance</th>
                                    <th>Active Account</th>
                                   
                                    
                                </tr>
                            </thead>
                            <tbody>
                               <asp:Repeater ID="Repeater4" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblsrL" runat="server" Text='<%# (((RepeaterItem)Container).ItemIndex+1).ToString()%>'></asp:Label>
                                                

                                            </td>

                                            <td>

                                                <asp:Label ID="lblsl_code" runat="server" Text='<%# Eval("SCHEME_TYPE")%>'></asp:Label>
                                                
                                            </td>
                                            <td>
                                                <asp:Label ID="lblScheme_name" runat="server" Text='<%# Eval("CL_BAL_DEPOSIT") %>'></asp:Label>
                                            </td>

                                            <td>

                                                <asp:Label ID="lblAPPL_DT" runat="server" Text='<%# Eval("LDG_CODE")%>'></asp:Label>
                                                
                                            </td>
                                            
                           
  
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                                 
                            </tbody>
                        </table>
                          
                    </div>
                </div>
        
            </div>
        </div>   
    
            <%--end of Total Loan--%>
        <%--starting of total Investment--%>
        <div class="col-lg-6">
           <div class="panel panel-default">
                <div class="panel-heading" style="font-size:larger;">
                    <i class="fa fa-bar-chart-o fa-fw"></i>Total Investment
                            <div class="pull-right">
                                <div class="btn-group">
                                
                               
                                    

                                </div>
                                
                            </div>
                </div>
                <!-- /.panel-heading -->
                <div class="panel-body">
                    <%--<div style="width: 20px; height: 100px;">--%>
                        <%--<div style="overflow: scroll;" runat="server" id="RepeaterControls"> --%>
                     <div style ="height:80px; width:585px; overflow:auto;">
                        <table width="100px" class="table table-striped table-bordered table-hover" id="dataTables-exampleTotalInvestment">
                            <thead>
                                <tr>
                                    <th>Srl.</th>
                                    <th>Type of Investment</th>
                                    <th>Balance</th>
                                    <th>Active Account</th>
                                   
                                    
                                </tr>
                            </thead>
                            <tbody>
                               <asp:Repeater ID="Repeater5" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblsrL" runat="server" Text='<%# (((RepeaterItem)Container).ItemIndex+1).ToString()%>'></asp:Label>
                                                

                                            </td>

                                            <td>

                                                <asp:Label ID="lblsl_code" runat="server" Text='<%# Eval("TYPE")%>'></asp:Label>
                                                
                                            </td>
                                            <td>
                                                <asp:Label ID="lblScheme_name" runat="server" Text='<%# Eval("CL_BAL_DEPOSIT") %>'></asp:Label>
                                            </td>

                                            <td>

                                                <asp:Label ID="lblAPPL_DT" runat="server" Text='<%# Eval("LDG_CODE")%>'></asp:Label>
                                                
                                            </td>
                                            
                           
  
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                                 
                            </tbody>
                        </table>
                          
                    </div>
                </div>
        
            </div>
        </div>   
    </div>
    <div class="clearfix"></div>
        <%--End Of Total Investment--%>
    <div class="row">
        <div class="col-lg-6">
            <div class="panel panel-default">
                <div class="panel-heading" style="font-size:larger;">
                    <i class="fa fa-bar-chart-o fa-fw"></i>Account Opening Details 
                            <div class="pull-right">
                                <div class="btn-group">
                                
                               
                                    

                                </div>
                                
                            </div>
                </div>
                <!-- /.panel-heading -->
                <div class="panel-body">
                   <%-- <div style="width: 590px; height: 160px;">--%>
                        <%--<div style="overflow: scroll;" runat="server" id="RepeaterControls"> --%>
                     <div style ="height:80px; width:585px; overflow:auto;">
                        <table width="100%" class="table table-striped table-bordered table-hover" id="dataTables-example">
                            <thead>
                                <tr>
                                    <th>Srl.</th>
                                    <th>Account Number</th>
                                    <%--<th>Account Holder Name</th>--%>
                                    <th>Interest Amount</th>
                                    <%--<th>Transfer To Savings Account </th>--%>
                                    <th>Transfer To Account</th>
                                    
                                </tr>
                            </thead>
                            <tbody>
                               <asp:Repeater ID="RepCCList" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblsrL" runat="server" Text='<%# (((RepeaterItem)Container).ItemIndex+1).ToString()%>'></asp:Label>
                                                

                                            </td>

                                            <td>

                                                <asp:Label ID="lblsl_code" runat="server" Text='<%# Eval("SL_CODE")%>'></asp:Label>
                                                
                                            </td>
                                            <%--<td>
                                                <asp:Label ID="lblScheme_name" runat="server" Text='<%# Eval("NAME") %>'></asp:Label>
                                            </td>--%>

                                            <td>

                                                <asp:Label ID="lblAPPL_DT" runat="server" Text='<%# Eval("INT_AMT")%>'></asp:Label>
                                                
                                            </td>
                                             <td>

                                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("INT_GOES_TO")%>'></asp:Label>
                                                
                                            </td>

                                            <%-- <td>

                                                <asp:Label ID="lblROI" runat="server" Text='<%# Eval("INT_GOES_TO")%>'></asp:Label>
                                                
                                            </td>--%>
                                            
  
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                                 
                            </tbody>
                        </table>
                           <%-- </div>--%>
                         
                           <%-- </div>--%>
                        <%--<asp:GridView ID="GVRepayHist" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" Height="50px" Width="100%">
                            <Columns>
                                <asp:TemplateField HeaderText="Account_Number">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAcNo" runat="server" Text='<%# Eval("Account_Number")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Name">
                                    <ItemTemplate>
                                         <asp:Label ID="lblName" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Int_Amt">
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("Int_Amt") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Tran_SAc">
                                    <ItemTemplate>
                                        <asp:Label ID="Label2" runat="server" Text='<%# Eval("Tran_SAc") %>'></asp:Label>
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
                        </asp:GridView>--%>
                        
                        

                                <%--<asp:Chart ID="Chart1" runat="server" Height="331px" Width="487px">
                                                                        <Series>
                                                                            <asp:Series Name="Series1" IsValueShownAsLabel="true" IsVisibleInLegend="true" ></asp:Series>
                                                                        </Series>
                                                                        <ChartAreas>
                                                                            <asp:ChartArea Name="ChartArea1"></asp:ChartArea>
                                                                        </ChartAreas>
                                </asp:Chart>--%>


            
                    </div>
                </div>
                <!-- /.panel-body --> 
            </div>
        </div>

        <div class="col-lg-6">
            <div class="panel panel-default">
                <div class="panel-heading" style="font-size:larger;">
                    <i class="fa fa-bar-chart-o fa-fw"></i>Enquiry to SaleOrder Conversion
                            <div class="pull-right">
                                <div class="btn-group">
                                </div>
                            </div>
                </div>
                <!-- /.panel-heading -->
                <div class="panel-body">
                    <%--<div style="width: 500px; height: 160px;">--%>
                        <%--<canvas id="EnquirySalesorder"></canvas>--%>
                     <div style ="height:80px; width:585px; overflow:auto;">
                         <table width="100%" class="table table-striped table-bordered table-hover" id="dataTables-example1">
                            <thead>
                                <tr>
                                    <th>Srl.</th>
                                    <th>LDG CODE</th>
                                    <th>SCHEME TYPE</th>
                                    <th>CL BAL DEPOSIT</th>
                                    
                                    
                                </tr>
                            </thead>
                            <tbody>
                               <asp:Repeater ID="Repeater1" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblsrL" runat="server" Text='<%# (((RepeaterItem)Container).ItemIndex+1).ToString()%>'></asp:Label>
                                                

                                            </td>

                                            <td>

                                                <asp:Label ID="lblsl_code" runat="server" Text='<%# Eval("LDG_CODE")%>'></asp:Label>
                                                
                                            </td>
                                            <td>
                                                <asp:Label ID="lblScheme_name" runat="server" Text='<%# Eval("SCHEME_TYPE") %>'></asp:Label>
                                            </td>

                                            <td>

                                                <asp:Label ID="lblAPPL_DT" runat="server" Text='<%# Eval("CL_BAL_DEPOSIT")%>'></asp:Label>
                                                
                                            </td>
                                           
  
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                                 
                            </tbody>
                        </table>
                    </div>

                </div>
                <!-- /.panel-body -->
            </div>
            <!-- /.panel -->

            <!-- /.panel -->

            <!-- /.panel -->
        </div>
    </div>







</asp:Content>
