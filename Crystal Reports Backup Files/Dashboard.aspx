<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/MasterPages/ProjectBM.Master" CodeBehind="Dashboard.aspx.cs" Inherits="iBankingSolution.Dashboard" %>

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
            <h1 class="page-header">Dashboard</h1>
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
                            <div class="huge"></div><%--<%= this.DB.CountEnquiry %>--%>
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
                            <div class="huge"></div> <%--<%= this.DB.CountPO %>--%>
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
                            <div class="huge"></div><%-- <%= this.DB.CountProject %>--%>
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



    <div class="row">
        <div class="col-lg-6">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <i class="fa fa-bar-chart-o fa-fw"></i>Planning Budget vs Actual Budget
                            <div class="pull-right">
                                <div class="btn-group">
                                </div>
                            </div>
                </div>
                <!-- /.panel-heading -->
                <div class="panel-body">
                    <div style="width: 500px; height: 300px;">
                        <canvas id="Projectdetails"></canvas>
                    </div>
                </div>
                <!-- /.panel-body -->
            </div>
        </div>

        <div class="col-lg-6">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <i class="fa fa-bar-chart-o fa-fw"></i>Enquiry to SaleOrder Conversion
                            <div class="pull-right">
                                <div class="btn-group">
                                </div>
                            </div>
                </div>
                <!-- /.panel-heading -->
                <div class="panel-body">
                    <div style="width: 500px; height: 300px;">
                        <canvas id="EnquirySalesorder"></canvas>
                    </div>

                </div>
                <!-- /.panel-body -->
            </div>
            <!-- /.panel -->

            <!-- /.panel -->

            <!-- /.panel -->
        </div>
    </div>




    <script>
        $(function () {

            var ctx2 = document.getElementById("EnquirySalesorder").getContext("2d");
            var fetch_url;
            fetch_url = '<%=ResolveUrl("General/CallAjax.aspx/EnquirySOChart") %>';
            return $.ajax({
                url: fetch_url,
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (res, textStatus) {

                    var parsedJson = JSON.parse(res.d);
                    var barData = parsedJson;
                    var barOptions = {
                        responsive: true,
                        maintainAspectRatio: true,
                        scales: {
                            yAxes: [{

                                gridLines: {
                                    display: true
                                },
                                scaleLabel: {
                                    display: true,
                                    labelString: 'Count'
                                },
                                ticks: {
                                    beginAtZero: true
                                }
                            }],
                            xAxes: [{
                                gridLines: {
                                    display: true
                                }
                            }]
                        }

                    };

                    var myChart = new Chart(ctx2, { type: 'bar', data: barData, options: barOptions });
                },
                error: function (res, textStatus) {

                }
            });

        });
    </script>

    <script>
        $(function () {

            var ctx2 = document.getElementById("Projectdetails").getContext("2d");

            var fetch_url;
            fetch_url = '<%=ResolveUrl("General/CallAjax.aspx/ProjectBudgetActual") %>';
            return $.ajax({
                url: fetch_url,
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (res, textStatus) {

                    var parsedJson = JSON.parse(res.d);
                    var barData = parsedJson;
                    var barOptions = {
                        responsive: true,
                        maintainAspectRatio: true,
                        scales: {
                            yAxes: [{

                                gridLines: {
                                    display: true
                                },
                                scaleLabel: {
                                    display: true,
                                    labelString: 'Price'
                                },
                                ticks: {
                                    beginAtZero: true
                                }
                            }],
                            xAxes: [{
                                gridLines: {
                                    display: true
                                }
                            }]
                        }

                    };

                    var myChart = new Chart(ctx2, { type: 'bar', data: barData, options: barOptions });
                },
                error: function (res, textStatus) {

                }
            });

        });
    </script>
</asp:Content>
