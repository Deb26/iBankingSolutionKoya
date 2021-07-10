<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="iBankingSolution.Default" %>

 
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ProjectBM:: Login</title>
    <link rel="shortcut icon" href="Content/images/cLogoICO.ico" type="image/x-icon">
    <!-- Js -->
    <script src="Content/js/jquery.min.js" type="text/javascript"></script>
    <script src="Content/js/bootstrap.min.js" type="text/javascript"></script>
    <script src="Content/js/sb-admin-2.js" type="text/javascript"></script>
    <script src="Content/js/metisMenu.min.js" type="text/javascript"></script>
    <script src="Content/js/background.cycle.js" type="text/javascript"></script>
    <%--<script type="text/javascript" src="https://www.jqueryscript.net/demo/Simple-jQuery-Background-Image-Slideshow-with-Fade-Transitions-Background-Cycle/js/background.cycle.js"></script>--%>
    <!-- Js -->

    <!-- Css -->
    <link href="Content/css/bootstrap.min.css" rel="stylesheet" />
    <link href="Content/css/sb-admin-2.css" rel="stylesheet" />
    <link href="Content/css/font-awesome.min.css" rel="stylesheet" />
    <link href="Content/css/metisMenu.min.css" rel="stylesheet" />
    <!-- css -->

    <script type="text/javascript">
        $(document).ready(function () {
            $("body").backgroundCycle({
                imageUrls: [
                    'Content/images/LoginImg1.jpg',
                    'Content/images/LoginImg2.jpg',
                    'Content/images/LoginImg3.png',
                    'Content/images/LoginImg4.jpg'
                    
                ],
                fadeSpeed: 1000,
                duration: 3000,
                backgroundSize: SCALING_MODE_COVER
            });
        });
    </script>
    <style type="text/css">
        .messagealert {            width: 100%;
            position: fixed;
            top: 0px;
            z-index: 100000;
            padding: 0;
            font-size: 15px;
        }
    </style>
    <script type="text/javascript">
        function ShowMessage(message, messagetype) {
            var cssclass;
            debugger;
            switch (messagetype) {
                case 'Success':
                    cssclass = 'alert-success'
                    break;
                case 'Error':
                    cssclass = 'alert-danger'
                    break;
                case 'Warning':
                    cssclass = 'alert-warning'
                    break;
                default:
                    cssclass = 'alert-info'
            }
            $('#alert_container').append('<div id="alert_div" style="margin: 0 -37.5%;width: 99%;-webkit-box-shadow: 3px 4px 6px #999;text-align: center;" class="alert fade in ' + cssclass + '"><a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a><strong>' + messagetype + '!</strong> <span>' + message + '</span></div>').fadeOut(20000);
        }
    </script>
</head>
<body>
    <div class="container">
        <div class="row">
            <div class="col-md-4 col-md-offset-4">
                <div class="login-panel panel panel-default shadow p-3 mb-5 bg-white rounded">
                    <div class="panel-heading" style="background-color:white;border: 1px solid #1D1D1D;">
                        <%--<h3 class="panel-title">Cyrus</h3>--%><img src="Content/images/Logo2.jpg" />
                        <div class="messagealert" id="alert_container">
                        </div>
                    </div>
                    <div class="panel-body">
                        <form role="form" runat="server">
                            <fieldset>
                                <div class="form-group">
                                    <label title="email">Branch</label>
                                    <asp:DropDownList ID="ddlbranch" required="required" x-moz-errormessage="Please Select Branch." runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                                <div class="form-group">
                                    <label title="email">User Id</label>
                                    <input class="form-control" id="txtuid" required="required" placeholder="Enter your User Id" name="email" autofocus />
                                </div>
                                <div class="form-group">
                                    <label title="email">Password</label>
                                    <input class="form-control" required="required" placeholder="Enter your password" id="txtpwd" name="password" type="password" value="" />
                                </div>
                                <div class="form-group">
                                    <label title="email">Financial year</label>
                                    <asp:DropDownList ID="ddlfyi" runat="server" required="required" x-moz-errormessage="Please Select Financial Year" CssClass="form-control"></asp:DropDownList>
                                </div>
                                <div class="form-group">
                                    <asp:CheckBox ID="chkRemember" CssClass="checkbox-inline" runat="server" Text="Remember Me" />
                                </div>
                                <!-- Change this to a button or input when using this as a form -->
                                <%--<a href="index.html" class="btn btn-lg btn-success btn-block">Login</a>--%>
                                <asp:Button ID="btnLogin" runat="server" CssClass="btn btn-lg btn-success btn-block" Text="Login" OnClick="btnLogin_Click" />
                            </fieldset>
                        </form>
                    </div>
                </div>
            </div>
        </div>
    </div>
</body>
</html>
