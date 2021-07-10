<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/ProjectBM.Master" CodeBehind="Cashier_Counterbook.aspx.cs" Inherits="iBankingSolution.Transaction.Cashier_Counterbook" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
     <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.4.1/js/bootstrap-datepicker.min.js"></script>
         <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.4.1/css/bootstrap-datepicker3.css"/>
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

    <asp:ScriptManager ID="scrpt1" runat="server" EnablePageMethods="true"></asp:ScriptManager>
    <div class="row">
        <div class="col-lg-12">
            <div style="float: right; margin-top: 12px;">
                <asp:Button ID="btnsubmit1" runat="server" Text="Save" class="btn btn-primary" OnClick="btnsubmit_Click" Width="100px" />

                <asp:Button ID="btnCancel" runat="server" Text="Cancel" class="btn btn-primary" OnClick="btnCancel_Click" Width="74px" />

            </div>
            <h1 class="page-header">Cashier Counter Book</h1>
        </div>
        <!-- /.col-lg-12 -->
    </div>

    <div class="row">
        <div class="col-lg-12">

            <div class="panel panel-primary">
                <div class="panel-heading">
                    Cashier Counter Book
                </div>
                <div class="panel-heading" runat="server" id="DivID" visible="false">
                    <asp:Label ID="Label1" runat="server" Visible="False" align="center" Font-Bold="True" Font-Italic="False" Font-Names="Verdana" ForeColor="White"></asp:Label>
                </div>
                <div class="panel-body">
                    <div class="row" style="width: 50%; float: left">

                        <div class="form-group">
                            <div class="col-md-12">
                                <div class="panel panel-primary">

                                    <div class="panel-heading text-center">
                                        <b>Give Details</b>

                                    </div>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <label style="margin-right: 10PX;">Select Entry type.</label>
                                <asp:Label ID="lblDid" runat="server" BackColor="White" ForeColor="White"></asp:Label>

                                <asp:DropDownList ID="cmbx_EntryType" runat="server" CssClass="form-control" Font-Size="10" AutoPostBack="True" OnSelectedIndexChanged="cmbx_EntryType_SelectedIndexChanged">
                                    <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                    <asp:ListItem Text="Receipt" Value="r"></asp:ListItem>
                                    <asp:ListItem Text="Payment" Value="p"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-3">
                                <label style="margin-right: 20PX;Font-Size :larger;">Date Of Entry</label>



                                <%--<input id="dtpkr" type="date" value="" runat="server" />--%>
                                <asp:TextBox ID="dtpkr_EntryDate" CssClass="form-control input-sm BootDatepicker" runat="server"></asp:TextBox>

                            </div>
                            <div class="col-md-6">
                                <label style="margin-right: 20PX;">&nbsp;</label>
                                <label style="margin-right: 20PX;Font-Size :larger;">Naration</label>
                                &nbsp;&nbsp;<asp:TextBox ID="txt_Narration" CssClass="form-control" placeholder="ENTER NARATION" Font-Size="10" runat="server" onFocus="this.select()" autocomplete="off"></asp:TextBox>
                                <br />

                            </div>

                        </div>









                        <div class="clearfix"></div>
                        <%-- <div class="col-md-2">
                                    <label style="margin-right: 20PX;">Cash Book</label>
                                    <asp:TextBox ID="txt_CashBook" CssClass="form-control" runat="server" onFocus="this.select()" autocomplete="off" placeholder="Cash-In-Hand"></asp:TextBox>
                                </div>--%>
                        <div class="form-group">
                            <div class="col-md-12">
                                <div class="panel panel-primary">

                                    <div class="panel-heading text-center">
                                        <b>Operation Details</b>

                                    </div>
                                </div>
                            </div>
                            <%-- onFocus="this.select()"--%>
                            <%--onFocus="this.select()"--%>

                            <div class="col-md-4">
                                <label style="margin-right: 20PX;Font-Size :larger;">Old A/c No</label>
                                <asp:TextBox ID="txt_OldAcctNo" runat="server" CssClass="form-control" placeholder="EMTER OLDACN/O" Font-Size="10" autocomplete="off" AutoPostBack="true"
                                    OnTextChanged="txt_OldAcctNo_TextChanged"></asp:TextBox>
                            </div>
                            <div class="col-md-4">
                                        <label style="margin-right: 20PX;Font-Size :larger;">Select A/c No</label>
                                        <asp:TextBox ID="txt_AcctNo" runat="server"  placeholder="SELECT AC N/O" Font-Size="10" CssClass="form-control" autocomplete="off" AutoPostBack="true"
                                            OnTextChanged="txt_AcctNo_TextChanged"></asp:TextBox>

                                    </div>
                            <div class="col-md-4">
                                        <label style="margin-right: 20PX;Font-Size :larger;">CBS A/c No</label>
                                        <asp:TextBox ID="txtCBS_AcNo" runat="server" onFocus="this.select()" placeholder="ENTER CBS ACN/O" Font-Size="10" CssClass="form-control" autocomplete="off" AutoPostBack="true"
                                            OnTextChanged="txtCBS_TextChanged"></asp:TextBox>
                                        <br />
                                    </div>
                            <asp:UpdatePanel ID="panel1" runat="server">
                                <ContentTemplate>
                                    


                                    
                                </ContentTemplate>
                            </asp:UpdatePanel>

                            <div class="clearfix"></div>
                            <div class="col-md-4">
                                <asp:Label Style="margin-right: 20PX;" ID="lbl_AmountCredited" Text="Amount Credited" runat="server" Font-Bold="True" Font-Size="10"></asp:Label><br />
                                <asp:TextBox ID="ntxt_AmountCredited" runat="server" onFocus="this.select()" Font-Size="10" CssClass="form-control" autocomplete="off" ></asp:TextBox>
                            </div>

                            <div class="col-md-4">
                                <asp:Label Style="margin-right: 20PX;" ID="lbl_AmountDebited" Text="Amount Credited" runat="server" Font-Bold="True" Font-Size="10"></asp:Label>
                                <br />
                                <%--<asp:TextBox ID="ntxt_AmountDebited" runat="server" placeholder="AMT DEBIT" Font-Size="10" onFocus="this.select()" data-toggle="modal" data-target="#myModal" CssClass="form-control" autocomplete="off" AutoPostBack="True" OnTextChanged="ntxt_AmountDebited_TextChanged"></asp:TextBox>--%>
                                <asp:TextBox ID="ntxt_AmountDebited" runat="server" Font-Size="10" onFocus="this.select()" CssClass="form-control" autocomplete="off" AutoPostBack="True" OnTextChanged="ntxt_AmountDebited_TextChanged"></asp:TextBox>

                                <%--Modal window for Denomination--%>
                                <%--add modal for denomination--%>
                                <div class="modal" id="myModal">
                                    <div class="modal-dialog modal-sm">
                                        <div class="modal-content">

                                            <!-- Modal Header -->
                                            <div class="modal-header">
                                                <h4 class="modal-title">Denomination</h4>
                                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                                            </div>

                                            <!-- Modal body -->
                                            <div class="modal-body">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <label style="margin-right: 20PX;">SL_CODE</label>
                                                            <asp:TextBox ID="txtslcode" runat="server" Width="150px" CssClass="form-control" onkeypress="return isNumberKey(event)"></asp:TextBox></td>

                                                    </tr>

                                                    <tr>
                                                        <td>
                                                            <label style="margin-right: 20PX;">2000RS</label>
                                                            <asp:TextBox ID="txtrs2000" runat="server" Width="150px" CssClass="form-control" onkeypress="return isNumberKey(event)"></asp:TextBox></td>
                                                        <td>
                                                            <label style="margin-right: 20PX;">Total:</label>
                                                            <asp:Label ID="calculate2000" runat="server" Width="200px"></asp:Label></td>
                                                    </tr>

                                                    <tr>
                                                        <td>
                                                            <label style="margin-right: 20PX;">500RS</label>
                                                            <asp:TextBox ID="txtrs500" runat="server" Width="150px" CssClass="form-control" onkeypress="return isNumberKey(event)"></asp:TextBox></td>
                                                        <td>
                                                            <label style="margin-right: 20PX;">Total:</label>
                                                            <asp:Label ID="calculate500" runat="server" Width="200px"></asp:Label></td>
                                                    </tr>

                                                    <tr>
                                                        <td>
                                                            <label style="margin-right: 20PX;">200RS</label>
                                                            <asp:TextBox ID="txtrs200" runat="server" Width="150px" CssClass="form-control" onkeypress="return isNumberKey(event)"></asp:TextBox></td>
                                                        <td>
                                                            <label style="margin-right: 20PX;">Total:</label>
                                                            <asp:Label ID="calculate200" runat="server" Width="200px"></asp:Label></td>

                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <label style="margin-right: 20PX;">100RS</label>
                                                            <asp:TextBox ID="txtrs100" runat="server" Width="150px" CssClass="form-control" onkeypress="return isNumberKey(event)"></asp:TextBox></td>

                                                        <td>
                                                            <label style="margin-right: 20PX;">Total:</label>
                                                            <asp:Label ID="calculate100" runat="server" Width="200px"></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <label style="margin-right: 20PX;">50RS</label>
                                                            <asp:TextBox ID="txtrs50" runat="server" Width="150px" CssClass="form-control" onkeypress="return isNumberKey(event)"></asp:TextBox></td>
                                                        <td>
                                                            <label style="margin-right: 20PX;">Total:</label>
                                                            <asp:Label ID="calculate50" runat="server" Width="200px"></asp:Label></td>

                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <label style="margin-right: 20PX;">20RS</label>
                                                            <asp:TextBox ID="txtrs20" runat="server" Width="150px" CssClass="form-control" onkeypress="return isNumberKey(event)"></asp:TextBox></td>
                                                        <td>
                                                            <label style="margin-right: 20PX;">Total:</label>
                                                            <asp:Label ID="calculate20" runat="server" Width="200px"></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <label style="margin-right: 20PX;">10RS</label>
                                                            <asp:TextBox ID="txtrs10" runat="server" Width="150px" CssClass="form-control" onkeypress="return isNumberKey(event)"></asp:TextBox></td>
                                                        <td>
                                                            <label style="margin-right: 20PX;">Total:</label>
                                                            <asp:Label ID="calculate10" runat="server" Width="200px"></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <label style="margin-right: 20PX;">5RS</label>
                                                            <asp:TextBox ID="txtrs5" runat="server" Width="150px" CssClass="form-control" onkeypress="return isNumberKey(event)"></asp:TextBox></td>
                                                        <td>
                                                            <label style="margin-right: 20PX;">Total:</label>
                                                            <asp:Label ID="calculate5" runat="server" Width="200px"></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <label style="margin-right: 20PX;">2RS</label>
                                                            <asp:TextBox ID="txtrs2" runat="server" Width="150px" CssClass="form-control" onkeypress="return isNumberKey(event)"></asp:TextBox></td>
                                                        <td>
                                                            <label style="margin-right: 20PX;">Total:</label>
                                                            <asp:Label ID="calculate2" runat="server" Width="200px"></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <label style="margin-right: 20PX;">1RS</label>
                                                            <asp:TextBox ID="txtrs1" runat="server" Width="150px" CssClass="form-control" onkeypress="return isNumberKey(event)"></asp:TextBox></td>
                                                        <td>
                                                            <label style="margin-right: 20PX;">Total:</label>
                                                            <asp:Label ID="calculate1" runat="server" Width="200px"></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblfindtotal" runat="server" Width="200px">Total Amount:</asp:Label>
                                                            <asp:TextBox ID="calculatetotamt" runat="server" Width="150px" CssClass="form-control" AutoPostBack="True"></asp:TextBox></td>

                                                    </tr>

                                                </table>

                                            </div>

                                            <!-- Modal footer -->
                                            <div class="modal-footer">
                                                <%--<button type="button" class="btn btn-danger" data-dismiss="modal">Show</button>--%>

                                                <asp:Button ID="btnshow" runat="server" Text="Show" OnClick="btnshow_Click" />
                                                <button type="button" class="btn btn-danger" data-dismiss="modal">Submit</button>
                                            </div>

                                        </div>
                                    </div>
                                </div>


                                <%----Modal close--%>
                            </div>




                            <div class="col-md-4">
                                <asp:Label Style="margin-right: 20PX;" Text="Avail.Balance" runat="server" Font-Bold="True" Font-Size="10"></asp:Label>
                                <asp:TextBox ID="ntxt_AvailableBalance" CssClass="form-control" placeholder="AVAIL BALANCE" Font-Size="10" runat="server" ForeColor="#CC3300" onFocus="this.select()" autocomplete="off" Enabled="False"></asp:TextBox>
                                <br />
                            </div>


                        </div>


                        <%--                                <div class="col-md-3">
                                    <label style="margin-right: 20PX;">IFSC CODE</label>
                                    <asp:TextBox ID="txt_IfscCode" runat="server" onFocus="this.select()" CssClass="form-control" autocomplete="off"></asp:TextBox>

                                </div>--%>





                        <div class="form-group" runat="server" id="divMis" visible="false">
                            <div style="background-color: #E9F7FE">
                                <%-- <h5>&nbsp;&nbsp;&nbsp;&nbsp;<span class="auto-style1">MIS Interest Paid</span></h5>--%>
                                <div class="col-md-12">
                                    <div class="panel panel-primary">

                                        <div class="panel-heading text-center">
                                            <b>MIS Interest Paid</b>

                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3" title="MIS Interest Paid">

                                    <label style="margin-right: 20PX;">Interest Issue From</label>
                                    <asp:DropDownList ID="cmbx_InterestIssueFrom" runat="server" CssClass="form-control" Font-Size="10">
                                    </asp:DropDownList>
                                </div>

                                <div class="col-md-3">
                                    <label style="margin-right: 20PX;">Ledger</label>
                                    <asp:DropDownList ID="cmbx_Ledger" runat="server" placeholder="ENTER LEDGER" Font-Size="10" CssClass="form-control">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-3">
                                    <label style="margin-right: 20PX;">Transfer To</label><br />
                                    <asp:DropDownList ID="cmbx_TransferTo" runat="server" placeholder="TRANSFER AC N/O" Font-Size="10" CssClass="form-control">
                                    </asp:DropDownList>

                                </div>
                            </div>
                        </div>

                        <%--  <div class="form-group" runat="server" id="divKYCDtls">
                                    <div class="form-group">
                                       <%-- <h5>&nbsp;&nbsp;&nbsp;&nbsp;<span class="auto-style2">KYC Details</span></h5> 
                                        <div class="col-md-10">--%>
                        <%-- <div class="panel panel-primary">--%>

                        <%--  <div class="panel-heading text-center"><b>KYC Details</b>
                               
                                             </div>

                                    </div></div>
                                        <div>
                                            <div class="col-md-3" title="KYC Details">
                                                <label style="margin-right: 20PX;">PAN</label>
                                                <asp:TextBox ID="Txt_Pan" runat="server" CssClass="form-control" placeholder="ENTER PAN" Font-Size="10"  ForeColor="#CC3300" Enabled="False" />
                                            </div>
                                            <div class="col-md-3">
                                                <label style="margin-right: 20PX;">ADHAR</label>
                                                <asp:TextBox ID="Txt_Adhar" runat="server" CssClass="form-control" placeholder="ENTER ADHAR" Font-Size="10" ForeColor="#CC3300" Enabled="False" />
                                            </div>
                                               <div class="col-md-3">
                                                <label style="margin-right: 20PX;">Voter</label>
                                                <asp:TextBox ID="txt_Voter" runat="server" CssClass="form-control" placeholder="ENTER VOTER" Font-Size="10" ForeColor="#CC3300" Enabled="False" />
                                            <br />

                                               </div>


                                        </div>--%>

                        <%-- </div>--%>
                        <%--</div>--%>
                        <div class="form-group" runat="server" id="divActDtls">
                            <div class="clearfix"></div>
                            <div class="form-group">
                                <div style="background-color: #E9F7FE">
                                    <%-- <h5>&nbsp;&nbsp;&nbsp;&nbsp;<span class="auto-style1">Account Details</span></h5>--%>
                                    <div class="col-md-12">
                                        <div class="panel panel-primary">

                                            <div class="panel-heading text-center">
                                                <b>Account Details</b>

                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-4" title="Account Details">

                                        <label style="margin-right: 20PX;Font-Size :larger;" >A/c Type</label>
                                        <asp:TextBox ID="txt_AcctType" runat="server" CssClass="form-control" placeholder="A/C NO" Font-Size="10" ForeColor="#CC3300" Enabled="False" />
                                    </div>
                                    <div class="col-md-4">
                                        <label style="margin-right: 20PX;Font-Size :larger;">Actual Balance</label>
                                        <asp:TextBox ID="txt_ActualBalance" runat="server" CssClass="form-control" placeholder="ACTUAL BALANCE " Font-Size="10" ForeColor="#CC3300" Enabled="False" />
                                    </div>
                                    <div class="col-md-4">
                                        <label style="margin-right: 20PX;Font-Size :larger;">A/c Status</label>
                                        <asp:TextBox ID="txt_AcctStatus" runat="server" CssClass="form-control" placeholder="A/C STATUS" Font-Size="10" ForeColor="#CC3300" Enabled="False" />
                                    </div>
                                    <div class="clearfix"></div>
                                    <%-- <div class="col-md-3">
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
                                            </div>--%>
                                </div>
                            </div>
                        </div>

                        <div class="form-group" runat="server" id="div1">
                            <div class="clearfix"></div>
                            <div class="form-group">
                                <div style="background-color: #E9F7FE">
                                    <%-- <h5>&nbsp;&nbsp;&nbsp;&nbsp;<span class="auto-style1">Account Details</span></h5>--%>
                                    <div class="col-md-12">
                                        <div class="panel panel-primary">

                                            <div class="panel-heading text-center" style="font-size:larger;">
                                                <b>Account Belongs From</b>

                                            </div>
                                        </div>
                                    </div>
                                     <div class="col-md-8" title="Account Belongs From">

                                        <label style="margin-right: 20PX;Font-Size :larger;" >Branch Name</label>
                                        <asp:TextBox ID="txtBranchName" runat="server" CssClass="form-control" placeholder="Branch Name" Font-Size="10" ForeColor="#CC3300" Enabled="False" />
                                    </div>
                                    </div>
                                </div>
                            </div>
                    </div>
                    
                    <div class="row">
                      <div class="col-lg-6">
                        <div class="panel panel-primary">
                              <div class="panel-heading">
                        Account Holders Details
                    </div>
                    <div class="panel-body">
                    <asp:ListView ID="lv_AcctHolders" runat="server" ItemPlaceholderID="AcctHoldersContainer" OnItemDataBound="lv_AcctHolders_ItemDataBound" RenderMode="Classic">
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
                                                <td style="width: 60%; font-weight: bold;">CUST ID:</td>
                                                <td style="width: 50%; padding-left: 1px"><%#Eval("CUST_ID")%></td>
                                            </tr>
                                  
                                            <tr>
                                                <td style="width: 60%; font-weight: bold;">NAME:</td>
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
                                                <td style="font-weight: bold;">PAN:</td>
                                                <td><%#Eval("PAN_CARD_NO")%></td>
                                            </tr>
                                            <tr>
                                                <td style="font-weight: bold;">AADHAAR:</td>
                                                <td><%#Eval("ADHAR_NO")%></td>
                                            </tr>
                                            <tr>
                                                <td style="font-weight: bold;">VOTER:</td>
                                                <td><%#Eval("VOTER_CARD_NO")%></td>
                                            </tr>
                                            <tr>
                                            </tr>
                                        </table>
                                    </td>
                                    <td class="contactPhoto" style="width: 25%">
                                        <asp:Image ID="Photo" runat="server" Height="120" ImageUrl='<%= ResolveClientUrl(Eval("PICTPATH"))%>' Shape="Square" />
                                        <asp:Button ID="btnShowPhoto" runat="server" Text="ShowPhoto" />
                                        <asp:Image ID="Sign" runat="server" BackColor="WhiteSmoke" ImageUrl='<%= ResolveClientUrl(Eval("SIGNPATH"))%>' Shape="Wide" Width="120" />
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

                <%--<div style="width: 30%; float: right; height: 450px; overflow-y: scroll; overflow-x: hidden;">--%>
                </div>

            </div>

        </div>
    </div>

    <asp:TextBox ID="txtphone" runat="server" CssClass="form-control" Visible="false" />

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
