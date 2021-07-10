<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/ProjectBM.Master" CodeBehind="frmCBSACOpening.aspx.cs" Inherits="iBankingSolution.Transaction.frmCBSACOpening" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        <div class="col-lg-12">
            <div style="float: right; margin-top: 12px;">
            </div>
            <div style="float: right; margin-top: 12px;">
                <asp:Button ID="btnSave" Visible="false" runat="server" Text="Save" CssClass="btn btn-primary" />
                <a href="frmKYCList.aspx" class="btn btn-default">Back to List</a>
            </div>
            <h1 class="page-header">CBS ACCOUNT</h1>
        </div>
    </div>
    <div class="row">
        <div class="col-lg-12">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    Enter Details
                </div>
                <div class="panel-heading" runat="server" id="DivID" visible="false">
                    <asp:Label ID="Label1" runat="server" Visible="False" align="center" Font-Bold="True" Font-Italic="False" Font-Names="Verdana" ForeColor="White"></asp:Label>
                </div>
                <!-- /.panel-heading -->
                <div class="panel-body">

                    <table width="100%" align="center">
                        <tr>
                            <td>
                                <asp:Button Text="A/C Opening" BorderStyle="None" ID="Tab1" CssClass="Initial" runat="server"
                                    Font-Bold="True" Font-Names="Verdana" OnClick="Tab1_Click" />
                                <asp:Button Text="CBS A/C Update" BorderStyle="None" ID="Tab2" CssClass="Initial" runat="server"
                                    Font-Bold="True" Font-Names="Verdana" />
                               


                                <asp:MultiView ID="MainView" runat="server">
                                    <asp:View ID="View1" runat="server">
                                        <h4 class="auto-style1" ><em>A/C Opening</em></h4>
                                        <table style="width: 100%; border-width: 0px; border-color: #666; border-style: solid">
                                            <tr>
                                                <td>

                                                    <div class="col-md-4">
                                                        </div>
                                                    <div class="col-md-4">
                                                        <asp:Label ID="Label2" runat="server" Text="*" Style="color: #FF3300"></asp:Label>
                                                        <label>Cust ID</label>
                                                        <asp:Label ID="lblcustid" runat="server" BackColor="White" ForeColor="White"></asp:Label>
                                                        <asp:TextBox runat="server" ID="txtcustid" placeholder="EnterCustId" Font-Size="10" Style="text-transform: uppercase;" CssClass="form-control" OnTextChanged="txtcustid_TextChanged" AutoPostBack="True"></asp:TextBox>
                                                        <br />
                                                    </div>
                                                     <div class="col-md-4">
                                                        </div>
                                                 <div class="clearfix"></div>
                                                    <div class="col-md-4">
                                                        <asp:Label ID="Label3" runat="server" Text="*" Style="color: #FF3300"></asp:Label>
                                                        <label>First Name</label>
                                                        <asp:Label ID="lblfname" runat="server" BackColor="White" ForeColor="White"></asp:Label>
                                                        <asp:TextBox runat="server" ID="txtfname" placeholder="EnterFirstName" Font-Size="10" Style="text-transform: uppercase;" CssClass="form-control"></asp:TextBox>
                                                    </div>

                                                    <div class="col-md-4">
                                                        <asp:Label ID="Label5" runat="server" Text="*" Style="color: #FF3300"></asp:Label>
                                                        <label>Middle Name</label>
                                                        <asp:Label ID="lblmname" runat="server" BackColor="White" ForeColor="White"></asp:Label>
                                                        <asp:TextBox runat="server" ID="txtmname" placeholder="EnterMiddleName" Font-Size="10" Style="text-transform: uppercase;" CssClass="form-control"></asp:TextBox>
                                                    </div>

                                                    <div class="col-md-4">
                                                        <asp:Label ID="Label7" runat="server" Text="*" Style="color: #FF3300"></asp:Label>
                                                        <label>Last Name</label>
                                                        <asp:Label ID="lbllname" runat="server" BackColor="White" ForeColor="White"></asp:Label>
                                                        <asp:TextBox runat="server" ID="txtlname" placeholder="EnterLastName" Font-Size="10" Style="text-transform: uppercase;" CssClass="form-control"></asp:TextBox>
                                                        <br />
                                                    </div>

                                                    <div class="col-md-4">
                                                        <asp:Label ID="Label9" runat="server" Text="*" Style="color: #FF3300"></asp:Label>
                                                        <label>F/H Name</label>
                                                        <asp:Label ID="lblguardname" runat="server" BackColor="White" ForeColor="White"></asp:Label>
                                                        <asp:TextBox runat="server" ID="txtguardname" placeholder="EnterF/HName" Font-Size="10" Style="text-transform: uppercase;" CssClass="form-control"></asp:TextBox>
                                                    </div>

                                                    <div class="col-md-4">
                                                        <asp:Label ID="Label11" runat="server" Text="*" Style="color: #FF3300"></asp:Label>
                                                        <label>DOB</label>
                                                        <asp:Label ID="lbldob" runat="server" BackColor="White" ForeColor="White"></asp:Label>
                                                        <asp:TextBox runat="server" ID="txtdob" placeholder="DD/MM/YYYY" Font-Size="10" Style="text-transform: uppercase;" CssClass="form-control"></asp:TextBox>
                                                    </div>

                                                    <div class="col-md-4">
                                                        <asp:Label ID="Label13" runat="server" Text="*" Style="color: #FF3300"></asp:Label>
                                                        <label>Gender</label>
                                                        <asp:Label ID="lblgender" runat="server" BackColor="White" ForeColor="White"></asp:Label>
                                                        <asp:TextBox runat="server" ID="txtgender" placeholder="EnterGender" Font-Size="10" Style="text-transform: uppercase;" CssClass="form-control"></asp:TextBox>
                                                        <br />
                                                    </div>

                                                    <div class="col-md-4">
                                                        <asp:Label ID="Label15" runat="server" Text="*" Style="color: #FF3300"></asp:Label>
                                                        <label>Village</label>
                                                        <asp:Label ID="lblvillage" runat="server" BackColor="White" ForeColor="White"></asp:Label>
                                                        <asp:TextBox runat="server" ID="txtvillage" placeholder="EnterVillage" Font-Size="10" Style="text-transform: uppercase;" CssClass="form-control"></asp:TextBox>
                                                    </div>

                                                    <div class="col-md-4">
                                                        <asp:Label ID="Label17" runat="server" Text="*" Style="color: #FF3300"></asp:Label>
                                                        <label>Post Office</label>
                                                        <asp:Label ID="lblpost" runat="server" BackColor="White" ForeColor="White"></asp:Label>
                                                        <asp:TextBox runat="server" ID="txtpost" placeholder="EnterPostOffice" Font-Size="10" Style="text-transform: uppercase;" CssClass="form-control"></asp:TextBox>
                                                    </div>

                                                    <div class="col-md-4">
                                                        <asp:Label ID="Label19" runat="server" Text="*" Style="color: #FF3300"></asp:Label>
                                                        <label>District</label>
                                                        <asp:Label ID="lbldist" runat="server" BackColor="White" ForeColor="White"></asp:Label>
                                                        <asp:TextBox runat="server" ID="txtdist" placeholder="EnterDistrict" Font-Size="10" Style="text-transform: uppercase;" CssClass="form-control"></asp:TextBox>
                                                        <br />
                                                    </div>

                                                    <div class="col-md-4">
                                                        <asp:Label ID="Label21" runat="server" Text="*" Style="color: #FF3300"></asp:Label>
                                                        <label>PIN</label>
                                                        <asp:Label ID="lblpin" runat="server" BackColor="White" ForeColor="White"></asp:Label>
                                                        <asp:TextBox runat="server" ID="txtpin" placeholder="EnterPIN" Font-Size="10" Style="text-transform: uppercase;" CssClass="form-control"></asp:TextBox>
                                                    </div>

                                                    <div class="col-md-4">
                                                        <asp:Label ID="Label23" runat="server" Text="*" Style="color: #FF3300"></asp:Label>
                                                        <label>ID Type</label>                                         
                                                        <asp:DropDownList ID="ddlIDType" runat="server" Font-Size="10" CssClass="form-control">
                                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                            <asp:ListItem Value="06">Voter ID</asp:ListItem>
                                                            <asp:ListItem Value="47">Aadhaar Card</asp:ListItem>
                                                            <asp:ListItem Value="07">PAN Card</asp:ListItem>
                                                            <asp:ListItem Value="04">Passport</asp:ListItem>
                                                            <asp:ListItem Value="10">Driving Licence</asp:ListItem>
                                                            <asp:ListItem Value="44">Ration Card</asp:ListItem>
                                                            <asp:ListItem Value="08">Govt ID</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>

                                                    <div class="col-md-4">
                                                        <asp:Label ID="Label25" runat="server" Text="*" Style="color: #FF3300"></asp:Label>
                                                        <label>ID No</label>
                                                        <asp:Label ID="lblidno" runat="server" BackColor="White" ForeColor="White"></asp:Label>
                                                        <asp:TextBox runat="server" ID="txtidno" placeholder="EnterIdNo" Font-Size="10" Style="text-transform: uppercase;" CssClass="form-control"></asp:TextBox>
                                                        <br />
                                                    </div>

                                                    <div class="col-md-4">
                                                        <asp:Label ID="Label27" runat="server" Text="*" Style="color: #FF3300"></asp:Label>
                                                        <label>Phone No</label>
                                                        <asp:Label ID="lblphoneno" runat="server" BackColor="White" ForeColor="White"></asp:Label>
                                                        <asp:TextBox runat="server" ID="txtphoneno" placeholder="EnterPhoneNo" Font-Size="10" Style="text-transform: uppercase;" CssClass="form-control"></asp:TextBox>
                                                    </div>

                                                    <div class="col-md-4">
                                                        <asp:Label ID="Label29" runat="server" Text="*" Style="color: #FF3300"></asp:Label>
                                                        <label>Society A/C No</label>
                                                        <asp:Label ID="lblsocietyacno" runat="server" BackColor="White" ForeColor="White"></asp:Label>
                                                        <asp:TextBox runat="server" ID="txtsocietyacno" placeholder="EnterSocietyA/CNo" Font-Size="10" Style="text-transform: uppercase;" CssClass="form-control"></asp:TextBox>
                                                    </div>

                                                    <div class="col-md-4">
                                                        <asp:Label ID="Label31" runat="server" Text="*" Style="color: #FF3300"></asp:Label>
                                                        <label>Society Code</label>
                                                        <asp:Label ID="lblsocietycode" runat="server" BackColor="White" ForeColor="White"></asp:Label>
                                                        <asp:TextBox runat="server" ID="txtsocietycode" placeholder="EnterSocietyCode" Font-Size="10" Style="text-transform: uppercase;" CssClass="form-control"></asp:TextBox>
                                                         <br />
                                                    </div>

                                                    <div class="col-md-4">
                                                        <div style="float: right; margin-top: 12px;">
                                                            <asp:Button ID="btnssave" runat="server" Font-Size="Large" Text="Send to CCB" class="btn btn-primary" OnClick="btnssave_Click" />
                                                        </div>
                                                    </div>

                                                    <div class="col-md-4">
                                                        <div style="float: right; margin-top: 12px;">
                                                            <asp:Button ID="btncancel" runat="server" Font-Size="Large" Text="Cancel" class="btn btn-primary" />
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:View>
                                </asp:MultiView>
                            </td>
                        </tr>
                    </table>
                </div>

            </div>
        </div>
    </div>

</asp:Content>
