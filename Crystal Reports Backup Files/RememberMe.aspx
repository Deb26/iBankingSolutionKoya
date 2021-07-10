<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RememberMe.aspx.cs" Inherits="iBankingSolution.RememberMe" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ProjectBM :: Lock Screen</title>
    <script src="<%=  Application["BasePage"] %>/Content/js/jquery.min.js"></script>
    <script src="<%=  Application["BasePage"] %>/Content/js/bootstrap.min.js"></script>
    <script src="<%=  Application["BasePage"] %>/Content/js/sb-admin-2.js"></script>
    <script src="<%=  Application["BasePage"] %>/Content/js/metisMenu.min.js"></script>
    <script src="<%=  Application["BasePage"] %>/Content/js/jquery.dataTables.min.js"></script>
    <script src="<%=  Application["BasePage"] %>/Content/js/dataTables.bootstrap.min.js"></script>
    <script src="<%=  Application["BasePage"] %>/Content/js/dataTables.responsive.js"></script>
    <!-- Js -->

    <!-- Css -->
    <link href="<%=  Application["BasePage"] %>/Content/css/bootstrap.min.css" rel="stylesheet" />
    <link href="<%=  Application["BasePage"] %>/Content/css/sb-admin-2.css" rel="stylesheet" />
    <link href="<%=  Application["BasePage"] %>/Content/css/font-awesome.min.css" rel="stylesheet" />
    <link href="<%=  Application["BasePage"] %>/Content/css/metisMenu.min.css" rel="stylesheet" />
    <link href="<%=  Application["BasePage"] %>/Content/css/dataTables.bootstrap.css" rel="stylesheet" />
    <link href="<%=  Application["BasePage"] %>/Content/css/dataTables.responsive.css" rel="stylesheet" />
    <style>
        body {
            background: #f5f5f5;
            font-family: 'Noto Sans', sans-serif;
            margin: 0;
            color: #4c5667;
            overflow-x: hidden !important;
        }

        .wrapper-page {
            margin: 7.5% auto;
            width: 360px;
        }

            .wrapper-page .form-control-feedback {
                left: 15px;
                top: 3px;
                color: rgba(76, 86, 103, 0.4);
                font-size: 20px;
            }

        .logo {
            color: #3bafda !important;
            font-size: 18px;
            font-weight: 700;
            letter-spacing: .02em;
            line-height: 70px;
        }

        .logo-lg {
            font-size: 28px !important;
        }

        .logo i {
            color: #f76397;
        }

        .user-thumb img {
            height: 88px;
            margin: 0px auto;
            width: 88px;
        }
    </style>
    <style type="text/css">
        .messagealert {
            width: 100%;
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
    <div class="wrapper-page bootstrap snippets">
        <div class="messagealert" id="alert_container">
        </div>
        <div class="text-center">
            <a href="#" class="logo logo-lg">
                <i class="fa fa-th"></i>
                <span>Lock Screen</span>
            </a>
        </div>
        <form runat="server" role="form" class="text-center m-t-20">
            <div class="user-thumb">
                <img src="Content/images/NOImage.png" class="img-responsive img-circle img-thumbnail" alt="thumbnail">
            </div>
            <div class="form-group">
                <h3><%= Session["UserName"] %></h3>
                <p class="text-muted">Enter your password to access the dashboard.</p>
                <div class="input-group m-t-30">
                    <input type="password" name="password" class="form-control" placeholder="Password">
                    <span class="input-group-btn">
                        <asp:Button ID="btnLogin" OnClick="btnLogin_Click" runat="server" CssClass="btn btn-email btn-success waves-effect waves-light" Text="Log In" />
                    </span>
                </div>
            </div>
            <div class="text-right">
                <asp:Button ID="btnDeleteCookie" Visible="false" runat="server" OnClick="btnDeleteCookie_Click"></asp:Button>
                <a href="javascript:deleteCookie(this);" class="text-muted">Or sign in as a different user</a>
                <%--<a href="#" class="text-muted">Not <%= Session["UserName"] %> ?</a>--%>
            </div>
            <script type="text/javascript">
                function deleteCookie(e) {
                    var cntrl = e;
                    <%=Page.ClientScript.GetPostBackEventReference(btnDeleteCookie, string.Empty)%>;
                }
            </script>
        </form>
    </div>
</body>
</html>
