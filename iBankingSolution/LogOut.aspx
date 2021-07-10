<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LogOut.aspx.cs" Inherits="iBankingSolution.LogOut" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <body>
        <div class="container">
            <div class="row">
               <div class="col-md-4 col-md-offset-4">
                <div class="login-panel panel panel-default shadow p-3 mb-5 bg-white rounded">
                    <div class="panel-heading" style="background-color: white; border: 1px solid #1D1D1D;">
                        <%--<h3 class="panel-title">Cyrus</h3>--%><img src="/Content/images/Logo2.jpg" />
                        <div class="messagealert" id="alert_container">
                            
                        </div>
                    </div>
                    <div class="panel-body">
                        <center>
    <img src="Images/LogoutPage.png" width="1200px" height="600px" />
    <form id="form1" runat="server">
    <div>
        <%--<h3>You Successfully Log Out !!!</h3>--%>
        <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">LogIn</asp:LinkButton>
    </div>
         <footer class="sticky-footer">
        <div class="copyright text-center my-auto">
            <span>Copyright © 2018-2019 <strong><a href="https://evantageindia.in/">Evantageindia.in</a></strong> All rights reserved.</span>
            <span class="my-auto1"><strong>Version</strong> 1.0</span>
        </div>

    </footer>
    </form>
        </center>
                    </div>
                </div>
            </div>
        </div>

    </body>
</html>
