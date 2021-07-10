<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/ProjectBM.Master" CodeBehind="Cashier_Counterbook.aspx.cs" Inherits="iBankingSolution.Transaction.Cashier_Counterbook" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
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



</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <form id="formInitialSetting" runat="server">
        <asp:ScriptManager ID="scrpt1" runat="server" EnablePageMethods="true"></asp:ScriptManager>
        <div class="row">
            <div class="col-lg-12">
                <div style="float: right; margin-top: 12px;">
                    <asp:Button ID="btnsubmit1" runat="server" Text="Save" class="btn btn-primary" OnClick="btnsubmit_Click" />

                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" class="btn btn-primary" OnClick="btnCancel_Click" />
                    <a href="#" class="btn btn-default">Back to List</a>
                </div>
                <h1 class="page-header">Cashier Counter Book</h1>
            </div>
            <!-- /.col-lg-12 -->
        </div>
        <div class="row">
            <div class="col-lg-12">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        Cashier Counter Book
                    </div>
                    <div class="panel-heading" runat="server" id="DivID" visible="false">
                        <asp:Label ID="Label1" runat="server" Visible="False" align="center" Font-Bold="True" Font-Italic="False" Font-Names="Verdana" ForeColor="White"></asp:Label>
                    </div>
                    <div class="panel-body">
                        <div class="row" style="width: 54%; float: left">

                            <div class="form-group">

                                <div class="col-md-4">
                                    <label style="margin-right: 10PX;">Select Entry type.</label>
                                    <asp:Label ID="lblDid" runat="server" BackColor="White" ForeColor="White"></asp:Label>

                                    <asp:DropDownList ID="cmbx_EntryType" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="cmbx_EntryType_SelectedIndexChanged">
                                        <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                        <asp:ListItem Text="Receipt" Value="r"></asp:ListItem>
                                        <asp:ListItem Text="Payment" Value="p"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-6">
                                    <label style="margin-right: 20PX;">Date Of Entry</label>
                                    <asp:TextBox ID="dtpkr_EntryDate" CssClass="form-control input-sm BootDatepicker" runat="server" onFocus="this.select()" autocomplete="off" required="required" AutoPostBack="True" OnTextChanged="dtpkr_CollectionDate_TextChanged"></asp:TextBox>
                                </div>



                                <div class="clearfix"></div>

                                <%-- <div class="col-md-3">
                                    <label style="margin-right: 20PX;">Voucher No</label>
                                    <asp:TextBox ID="txt_VoucherNo" CssClass="form-control" runat="server" onFocus="this.select()" autocomplete="off" required="required" Enabled="False"></asp:TextBox>
                                </div>--%>

                                <div class="col-md-3">
                                    <label style="margin-right: 20PX;">Instrument Type</label>
                                    <asp:DropDownList ID="cmbx_InstrumentType" runat="server" CssClass="form-control">
                                    </asp:DropDownList>

                                </div>

                                <div class="col-md-3">
                                    <label style="margin-right: 20PX;">Instrument No</label>
                                    <asp:TextBox ID="txt_InstrumentNo" CssClass="form-control" runat="server" onFocus="this.select()" autocomplete="off"></asp:TextBox>
                                </div>
                                <div class="col-md-4">
                                    <label style="margin-right: 20PX;">Date</label>
                                    <asp:TextBox ID="dtpkr_DateOfInstrument" CssClass="form-control input-sm BootDatepicker" runat="server" onFocus="this.select()" autocomplete="off"></asp:TextBox>
                                </div>
                                <div class="clearfix"></div>
                                <div class="col-md-10">
                                    <label style="margin-right: 20PX;">Naration</label>
                                    <asp:TextBox ID="txt_Narration" CssClass="form-control" runat="server" onFocus="this.select()" autocomplete="off"></asp:TextBox>
                                </div>


                                <div class="clearfix"></div>
                                <%-- <div class="col-md-2">
                                    <label style="margin-right: 20PX;">Cash Book</label>
                                    <asp:TextBox ID="txt_CashBook" CssClass="form-control" runat="server" onFocus="this.select()" autocomplete="off" placeholder="Cash-In-Hand"></asp:TextBox>
                                </div>--%>
                                <div class="col-md-3">
                                    <asp:Label Style="margin-right: 20PX;" ID="lbl_AmountCredited" Text="Amount Credited" runat="server" Font-Bold="True"></asp:Label>
                                    <asp:TextBox ID="ntxt_AmountCredited" runat="server" onFocus="this.select()" CssClass="form-control" autocomplete="off"></asp:TextBox>
                                </div>
                                <div class="col-md-4">
                                    <label style="margin-right: 20PX;">Available Balance</label>
                                    <asp:TextBox ID="ntxt_AvailableBalance" CssClass="form-control" runat="server" ForeColor="#CC3300" onFocus="this.select()" autocomplete="off" Enabled="False"></asp:TextBox>
                                </div>


                                <div class="col-md-3">
                                    <label style="margin-right: 20PX;">Old A/c No</label>
                                    <asp:TextBox ID="txt_OldAcctNo" runat="server" onFocus="this.select()" CssClass="form-control" autocomplete="off" AutoPostBack="true"
                                        OnTextChanged="txt_OldAcctNo_TextChanged"></asp:TextBox>
                                </div>
                                <div class="clearfix"></div>
                                <div class="col-md-4">
                                    <label style="margin-right: 20PX;">Select A/c No</label>
                                    <asp:TextBox ID="txt_AcctNo" runat="server" onFocus="this.select()" CssClass="form-control" autocomplete="off" AutoPostBack="true"
                                        OnTextChanged="txt_AcctNo_TextChanged"></asp:TextBox>

                                </div>


                                <div class="col-md-3">
                                    <label style="margin-right: 20PX;">IFSC CODE</label>
                                    <asp:TextBox ID="txt_IfscCode" runat="server" onFocus="this.select()" CssClass="form-control" autocomplete="off"></asp:TextBox>

                                </div>

                                <div class="col-md-3">
                                    <asp:Label Style="margin-right: 20PX;" ID="lbl_AmountDebited" Text="Amount Credited" runat="server"></asp:Label>
                                    <asp:TextBox ID="ntxt_AmountDebited" runat="server" onFocus="this.select()" CssClass="form-control" autocomplete="off" AutoPostBack="True" OnTextChanged="ntxt_AmountDebited_TextChanged"></asp:TextBox>

                                </div>

                                <div class="clearfix"></div>

                                <div class="form-group" runat="server" id="divMis" visible="false">
                                    <div style="background-color: #E9F7FE">
                                        <h5>&nbsp;&nbsp;&nbsp;&nbsp;MIS Interest Paid</h5>
                                        <div class="col-md-3" title="MIS Interest Paid">

                                            <label style="margin-right: 20PX;">Interest Issue From</label>
                                            <asp:DropDownList ID="cmbx_InterestIssueFrom" runat="server" CssClass="form-control">
                                            </asp:DropDownList>
                                        </div>

                                        <div class="col-md-4">
                                            <label style="margin-right: 20PX;">Ledger</label>
                                            <asp:DropDownList ID="cmbx_Ledger" runat="server" CssClass="form-control">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-3">
                                            <label style="margin-right: 20PX;">Transfer To</label><br />
                                            <asp:DropDownList ID="cmbx_TransferTo" runat="server" CssClass="form-control">
                                            </asp:DropDownList>

                                        </div>
                                    </div>
                                </div>
                                <div class="form-group" runat="server" id="divActDtls">
                                    <div class="clearfix"></div>
                                    <div class="form-group">
                                        <div style="background-color: #E9F7FE">
                                            <h5>&nbsp;&nbsp;&nbsp;&nbsp;Account Details</h5>

                                            <div class="col-md-3" title="Account Details">

                                                <label style="margin-right: 20PX;">A/c Type</label>
                                                <asp:TextBox ID="txt_AcctType" runat="server" CssClass="form-control" ForeColor="#CC3300" Enabled="False" />
                                            </div>
                                            <div class="col-md-4">
                                                <label style="margin-right: 20PX;">Actual Balance</label>
                                                <asp:TextBox ID="txt_ActualBalance" runat="server" CssClass="form-control" ForeColor="#CC3300" Enabled="False" />
                                            </div>
                                            <div class="col-md-3">
                                                <label style="margin-right: 20PX;">A/c Status</label>
                                                <asp:TextBox ID="txt_AcctStatus" runat="server" CssClass="form-control" ForeColor="#CC3300" Enabled="False" />
                                            </div>
                                            <div class="clearfix"></div>
                                            <div class="col-md-3">
                                                <label style="margin-right: 20PX;">R/D Due InstAmt</label>
                                                <asp:TextBox ID="txt_RDDueInstAmt" runat="server" CssClass="form-control" />
                                            </div>
                                            <div class="col-md-4">
                                                <label style="margin-right: 20PX;">R/D Penal Int</label>
                                                <asp:TextBox ID="txt_RDPenalInt" runat="server" CssClass="form-control" />
                                            </div>
                                            <div class="col-md-3">
                                                <label style="margin-right: 20PX;">R/D Ins</label>
                                                <asp:TextBox ID="txt_RDIns" runat="server" CssClass="form-control" onkeypress="return isNumberKey(event)" />
                                            </div>

                                        </div>
                                    </div>
                                </div>


                            </div>
                        </div>
                        <div style="width: 46%; float: right; height: 450px; overflow-y: scroll; overflow-x: hidden;">
                            <asp:ListView ID="lv_AcctHolders" runat="server" RenderMode="Classic"
                                ItemPlaceholderID="AcctHoldersContainer">
                                <LayoutTemplate>
                                    <fieldset class="layoutFieldset ">
                                        <legend>Account Holders</legend>
                                        <asp:PlaceHolder ID="AcctHoldersContainer" runat="server"></asp:PlaceHolder>
                                    </fieldset>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <table>
                                        <tr>
                                            <td style="width: 75%; vertical-align: top;">
                                                <table style="width: 100%">
                                                    <tr>
                                                        <td style="width: 40%; font-weight: bold;">CUST ID:</td>
                                                        <td style="width: 60%; padding-left: 2px"><%#Eval("CUST_ID")%></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="font-weight: bold;">NAME:</td>
                                                        <td><%#Eval("NAME")%></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="font-weight: bold;">GURDIAN NAME:</td>
                                                        <td><%# Eval("GUARDIAN_NAME")%></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="font-weight: bold;">VILLAGE:</td>
                                                        <td><%# Eval("VILL_CODE")%></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="font-weight: bold;">BLOCK:</td>
                                                        <td><%#Eval("BLK_CODE")%></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="font-weight: bold;">DISTRICT:</td>
                                                        <td><%#Eval("DIS_CODE")%></td>
                                                    </tr>
                                                    <tr> 
                                                         
                                                         
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="contactPhoto" style="width: 25%">
                                                <asp:Image ID="Photo" ImageUrl="~/Images/Empty_DP.jpg" Shape="Square" runat="server" Height="120"></asp:Image>
                                                <asp:Button ID="btnShowPhoto" runat="server" Text="ShowPhoto" />
                                                <asp:Image ID="Sign" ImageUrl="~/Images/logo_sigclub.png" Shape="Wide" runat="server" Width="120" BackColor="WhiteSmoke"></asp:Image>
                                              <asp:Button ID="btnShowSign" runat="server" Text="ShowSign" />

                                            </td>
                                        </tr>
                                       
                                    </table>
                                  
                                    
                                </ItemTemplate>
                            </asp:ListView>
                        </div>
                    </div>

                </div>
            </div>


            <div class="row">
                <div class="col-lg-12">
                    <div style="float: right; margin-top: 12px;">
                        <asp:Button ID="btnsubmit" runat="server" Text="Save" class="btn btn-primary" OnClick="btnsubmit_Click" />
                        <a href="#" class="btn btn-outline btn-danger">Cancel</a>

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
    </form>
</asp:Content>
