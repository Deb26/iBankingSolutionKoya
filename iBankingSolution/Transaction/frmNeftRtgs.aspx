<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/ProjectBM.Master" CodeBehind="frmNeftRtgs.aspx.cs" Inherits="iBankingSolution.Transaction.frmNeftRtgs" %>

 <asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

     <%@ Register TagPrefix="Ajax" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>
     <%--<%@ Register Assembly="NumericTextBox" Namespace="LZDollarTextBox" TagPrefix="NumText"  ontextchanged="txtcbAcNo_TextChanged" ontextchanged="txtAmount_TextChanged"%>--%>

    <script type="text/javascript">
        $ = jQuery.noConflict();
        $(function () {
            $('.select2').select2()
        })
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
   
         <asp:ScriptManager ID="scrpt1" runat="server"></asp:ScriptManager>
        <div class="row">
            <div class="col-lg-12">
                <div style="float: right; margin-top: 12px;">
                   <%-- <asp:Button ID="btnsubmit1" runat="server" Text="Add" class="btn btn-primary" />--%>
           
                </div>
                <h1 class="page-header">NEFT/RTGS</h1>
            </div>
            <!-- /.col-lg-12 -->
        </div>
     <marquee direction="right"><strong><asp:Label ID="lblmessage" runat="server" Visible="true" Font-Bold="True" Font-Italic="True" ForeColor="#990000" Font-Size="Medium"></asp:Label></strong></marquee>
     <asp:Panel runat="server" ID="panelneft" Visible="false">
        <div class="row">
            <div class="col-lg-12">
                <div class="panel panel-primary" >
                    <div class="panel-heading" style="font-size:larger;">
                         OUTWARD ENTRY
                    </div>
                    <div class="panel-body"  style="font-size:larger;">
                        <div class="row">

                            <div class="form-group">
                                <div class="col-md-1"></div>
                                <div class="col-md-4">
                                    <label style="margin-right: 20PX;font-size:larger;">Sender ACNo:</label>
                                    <%--<input type="text" id="txtsenderAcNo" class="form-control"   style="font-size:larger;"/>--%>
                                    <asp:TextBox ID="txtsenderAcNo" runat="server" CssClass="form-control" placeholder="EMTER ACCOUNT NO" Font-Size="10" autocomplete="off" AutoPostBack="true" 
                                    OnTextChanged="txtAccountNo_TextChanged"></asp:TextBox>
                                </div>
                             
                                <div class="col-md-3">
                                    <label style="margin-right: 20PX;font-size:larger;">Pacs ACNo:</label>
                                    <asp:TextBox ID="txtSlCode" runat="server" CssClass="form-control" placeholder="EMTER ACCOUNT NO" Font-Size="10" autocomplete="off" AutoPostBack="true" 
                                    OnTextChanged="txtSlCode_TextChanged"></asp:TextBox>
                                </div>
                                <div class="col-md-3">
                                    <label style="margin-right: 20PX;font-size:larger;">Sender Name:</label>
                                    <%--<input type="text" id="txtsenderName" class="form-control" disabled="disabled" style="font-size:larger;"/>--%>

                                    <asp:TextBox ID="txtsenderName" runat="server" CssClass="form-control"  Font-Size="10" autocomplete="off" Enabled="false"
                                      ></asp:TextBox>
                                   
                                </div>
                                <div class="clearfix"></div><br />
                                <div class="col-md-1"></div>
                                <div class="col-md-4">
                                    <label style="margin-right: 20PX;font-size:larger;">Amount:</label>
                                    <%--<input type="text" id="txtamount" class="form-control" style="font-size:larger;"/>--%>
                                    <asp:TextBox ID="txtamount" runat="server" CssClass="form-control" placeholder="EMTER AMOUNT" Font-Size="10" autocomplete="off" 
                                    ></asp:TextBox>
                                </div>
                                <div class="col-md-3">
                                    <label style="margin-right: 20PX;font-size:larger;">Commission(Rs.)</label>
                                    <%--<input type="text" id="txtcommissionRs" class="form-control"  disabled="disabled" style="font-size:larger;"/>--%>
                                     <asp:TextBox ID="txtcommissionRs" runat="server" CssClass="form-control" placeholder="EMTER COMMISSION" Font-Size="10" autocomplete="off" AutoPostBack="true" OnTextChanged="txtAmount_TextChanged"
                                      ></asp:TextBox>
                                </div>
                                <div class="col-md-3">
                                    <label style="margin-right: 20px;font-size:larger;">Total Amount</label>
                                    <%--<input type="text" id="txttotalAmount" class="form-control"  disabled="disabled" style="font-size:larger;"/>--%>
                                    <asp:TextBox ID="txttotalAmount" runat="server" CssClass="form-control"  Font-Size="10" autocomplete="off" Enabled="false"
                                      ></asp:TextBox>
                                </div>
                                <div class="clearfix"></div><br />
                                <div class="col-md-1"></div>
                                <div class="col-md-4">
                                    <label style="margin-right: 20px;font-size:larger;">Beneficiary Ac No:</label>
                                    <%--<input type="text" id="txtbenificiaryAcNo" class="form-control" style="font-size:larger;"/>--%>
                                     <asp:TextBox ID="txtbenificiaryAcNo" runat="server" CssClass="form-control"  Font-Size="10" autocomplete="off" placeholder="EMTER BENEFICIARY ACNO" 
                                        TextMode="password"
                                      ></asp:TextBox>
                                </div>
                                <div class="col-md-3">
                                    <label style="margin-right: 20px;font-size:larger;">Confirm Beneficiary Ac No :</label>
                                    <%--<input type="text" id="txtconfirmBen" class="form-control"  style="font-size:larger;"/>--%>
                                    <asp:TextBox ID="txtconfirmBen" runat="server" CssClass="form-control"  Font-Size="10" autocomplete="off" placeholder="EMTER CONFIRM BENEFICIARY ACNO"
                                        OnTextChanged="txtCbenacno_selectindex" AutoPostBack="true"
                                      ></asp:TextBox>
                                </div>
                                <div class="col-md-3">
                                    <label style="margin-right: 20px;font-size:larger;">Beneficiary Name:</label>
                                    <%--<input type="text" id="txtbeneficiaryName" class="form-control"  style="font-size:larger;"/>--%>
                                     <asp:TextBox ID="txtbeneficiaryName" runat="server" CssClass="form-control"  Font-Size="10" autocomplete="off" placeholder="EMTER BENEFICIARY NAME"
                                      ></asp:TextBox>
                                </div>
                                <div class="clearfix"></div><br />
                                <div class="col-md-1"></div>
                                <div class="col-md-4">
                                    <label style="margin-right: 20px;font-size:larger;">IFSC Code:</label>
                                    <%--<input type="text" id="txtifsCode" class="form-control"   style="font-size:larger;"/>--%>
                                    <asp:TextBox ID="txtifsCode" runat="server" CssClass="form-control"  Font-Size="10" autocomplete="off" Enabled="true" placeholder="EMTER IFSC CODE"
                                        AutoPostBack="True"  OnTextChanged="txtifscCode_TextChanged"
                                      ></asp:TextBox>
                                </div>
                                <div class="col-md-3">
                                    <label style="margin-right: 20px;font-size:larger;">Bank Name:</label>
                                    <asp:dropdownlist runat="server" id="cmbx_BankName" Font-Size="10" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="cmbx_BankName_SelectedIndexChanged">
                                        </asp:dropdownlist>
                                </div>
                                <div class="col-md-3">
                                    <label style="margin-right: 20px;font-size:larger;">Branch Name:</label>
                                    <asp:dropdownlist runat="server" id="cmbx_BranchName" Font-Size="10" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="cmbx_BranchName_SelectedIndexChanged">
                                        </asp:dropdownlist>
                                </div>
                                <div class="clearfix"></div><br />
                                  <div class="col-md-1"></div>
                                 <div class="col-md-4">
                                    <label style="margin-right: 20px;font-size:larger;">Sender Address:</label>
                                    <%--<input type="text" id="txtsenderAddress" class="form-control"  disabled="disabled" style="font-size:larger;"/>--%>
                                     <asp:TextBox ID="txtsenderAddress" runat="server" CssClass="form-control"  Font-Size="10" autocomplete="off" Enabled="false"
                                      ></asp:TextBox>
                                </div>
                                 <div class="col-md-3">
                                    <label style="margin-right: 20px;font-size:larger;">Benificiary Address :</label>
                                    <%--<input type="text" id="txtbenificiaryAddress" class="form-control"   style="font-size:larger;"/>--%>
                                      <asp:TextBox ID="txtbenificiaryAddress" runat="server" CssClass="form-control"  Font-Size="10" autocomplete="off" placeholder="EMTER BENEFICIARY ADDRESS"
                                      ></asp:TextBox>
                                </div>
                                 <div class="col-md-3">
                                    <label style="margin-right: 20px;font-size:larger;">Mobile No:</label>
                                    <%--<input type="text" id="txtMobileNo" class="form-control"   style="font-size:larger;"/>--%>
                                     <asp:TextBox ID="txtMobileNo" runat="server" CssClass="form-control"  Font-Size="10" autocomplete="off" Enabled="false"
                                      ></asp:TextBox>
                                </div>
                                <div class="clearfix"></div><br />
                                <div class="col-md-1"></div>
                                <div class="col-md-4">
                                    <label style="margin-right: 20px;font-size:larger;">Available Balance:</label>
                                    <%--<input type="text" id="txtavailablebalance" class="form-control"   style="font-size:larger;"/>--%>
                                     <asp:TextBox ID="txtavailablebalance" runat="server" CssClass="form-control"  Font-Size="10" autocomplete="off" Enabled="false"
                                      ></asp:TextBox>
                                </div>
                                <div class="col-md-3">
                                    <label style="margin-right: 20px;font-size:larger;">Pacs ID:</label>
                                    <%--<input type="text" id="txtservicecharge" class="form-control"   style="font-size:larger;"/>--%>
                                    <asp:TextBox ID="txtpacsid" runat="server" CssClass="form-control"  Font-Size="10" autocomplete="off" Enabled="false"
                                      ></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    <asp:TextBox ID="txttdt" runat="server" CssClass="form-control"  Font-Size="10" autocomplete="off" visible="false"
                                      ></asp:TextBox>
                                </div>
                                </div>
                            </div>
                       
                         
                        </div>
                    </div>
                </div>
            </div>
            
         </asp:Panel>
         
        <div class="row">
            <div class="col-lg-7">
                <div style="float: right; margin-top: 12px;">

                     <asp:Button ID="btnsubmit1" runat="server" Text="Next" class="btn btn-success" OnClick="btnSave_Click" Visible="false"/>
                     
                    <a href="frmNeftRtgs.aspx" class="btn btn-outline btn-danger">Reset</a>
                   
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

     
</asp:Content>
