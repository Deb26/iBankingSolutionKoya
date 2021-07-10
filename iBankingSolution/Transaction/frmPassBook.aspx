<%@ Page Language="C#" MasterPageFile="~/MasterPages/ProjectBM.Master" AutoEventWireup="true" CodeBehind="frmPassBook.aspx.cs" Inherits="iBankingSolution.Transaction.frmPassBook" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
	.lisencePrintHead {
		font-size: 12px;
		text-align: center;
	}
	.lisencePrintBody {
		font-size: 10px;
	}
	.lisencePrintBody left{
		text-align: left;
	}
	.lisencePrintBody center{
		text-align: center;
	}
	</style>

    <script type="text/javascript">

        $(function () {
            //$("#lisence").hide();
        });


        //function printPassBook() {

        //    //debugger;

        //    $.ajax({
        //        type: "GET",
        //        url: "https://pulse-api.apps.recruit.aptask.com/api/v1/voipsystem/dropcallbyagentid?agentext=8007",
        //        contentType: "application/json;",
        //        dataType: "json",
        //        success: function (response) {
        //            console.log(response);
        //        },

        //        error: function (err) {
        //            console.log(err);
        //        }
        //    })

        //    //$.ajax({
        //    //    method: "GET",
        //    //    url: "https://jsonplaceholder.typicode.com/posts",
        //    //    cors: true,
        //    //    contentType: 'application/json',
        //    //    headers: {
        //    //        'Access-Control-Allow-Origin': '*',
        //    //    }
        //    //})
        //    //.done(function (response) {
        //    //    console.log(response);
        //    //});

        //    //debugger;



        //}

        //function printPassBookLicense() {

        //    var obj = {};
        //    obj.SlCode = '100175';
        //    obj.OldAcNo = 'SB-567';

        //    $.ajax({
        //        type: "POST",
        //        url: "/Transaction/frmPassBook.aspx/GetClientDetails",
        //        contentType: "application/json;",
        //        dataType: "json",
        //        data: JSON.stringify(obj),
        //        success: function (response) {
        //            console.log(response);
        //        },
        //        error: function (err) {
        //            console.log(err);
        //        }
        //    })
        //}






        var LINENUMBER = 0;

        function GetSlDedetailsOld() {

            if ($("#oldAcNo").val() == "") return;


            var obj = {};
            obj.SlCode = $("#SlCode").val();
            obj.OldAcNo = $("#oldAcNo").val();


            $.ajax({
                type: "POST",
                url: "/Transaction/frmPassBook.aspx/GetClientDetails",
                cache: false,
                contentType: "application/json;",
                dataType: "json",
                data: JSON.stringify(obj),
                success: function (html) {
                    if (html != null) {
                        var output = html.d.split('~');
                        $("#SlCode").val(output[0]);
                        $("#oldAcNo").val(output[1]);
                        $("#lastPrintDate").val(output[2]);
                        LINENUMBER = parseInt(output[3]);
                    }
                    console.log(output);
                }
            });

        }

        function GetSlDedetailsSL() {

            if ($("#SlCode").val() == "") return;


            var obj = {};
            obj.SlCode = $("#SlCode").val();
            obj.OldAcNo = $("#oldAcNo").val();


            $.ajax({
                type: "POST",
                url: "/Transaction/frmPassBook.aspx/GetClientDetails",
                cache: false,
                contentType: "application/json;",
                dataType: "json",
                data: JSON.stringify(obj),
                success: function (html) {
                    if (html != null) {
                        var output = html.d.split('~');
                        $("#SlCode").val(output[0]);
                        $("#oldAcNo").val(output[1]);
                        $("#lastPrintDate").val(output[2]);
                        LINENUMBER = parseInt(output[3]);


                    }
                    console.log(output);
                }
            });

        }

        function PrintPassbook() {
            var obj = {};
            obj.SlCode = $("#SlCode").val();
            var dynamichtml = "";

            var pagesize = 37;
            var counter = 0;
            var pagecounter = 0;


            $.ajax({
                type: "POST",
                url: "/Transaction/frmPassBook.aspx/GetPassbookDetails",
                cache: false,
                contentType: "application/json;",
                dataType: "json",
                data: JSON.stringify(obj),
                success: function (html) {

                    if (html != null) {
                        //if (pagecounter == 0) {
                        //    counter = counter + 1;
                        //    dynamichtml += "<thead><tr>";
                        //    dynamichtml += "<th>DATE</th>";
                        //    dynamichtml += "<th>PARTICULARS</th>";
                        //    dynamichtml += "<th>TRANSACTION DETAILS</th>";
                        //    dynamichtml += "<th>WITHDRAWALS</th>";
                        //    dynamichtml += "<th>DIPOSIT</th>";
                        //    dynamichtml += "<th>BALANCE</th>";
                        //    dynamichtml += "</tr></thead>";

                        //}


                        if (LINENUMBER > 0) {
                            pagecounter = LINENUMBER + 1;
                            dynamichtml += "<table id='tblpassbook' width='100%'><tbody>";
                            for (var j = 0; j < LINENUMBER; j++) {
                                dynamichtml += "<colgroup>";
                                dynamichtml += "<col class='ten' />      ";
                                dynamichtml += "<col class='ten' />      ";
                                dynamichtml += "<col class='fourty' />   ";
                                dynamichtml += "<col class='fifteen' />  ";
                                dynamichtml += "<col class='fifteen' />  ";
                                dynamichtml += "<col class='fifteen' />  ";
                                dynamichtml += "</colgroup>";


                                dynamichtml += "<tr rowspan='1'>";
                                dynamichtml += "<td>&nbsp;&nbsp;</td>";
                                dynamichtml += "<td>&nbsp;&nbsp;</td>";
                                dynamichtml += "<td>&nbsp;&nbsp;</td>";
                                dynamichtml += "<td>&nbsp;&nbsp;</td>";
                                dynamichtml += "<td>&nbsp;&nbsp;</td>";
                                dynamichtml += "<td>&nbsp;&nbsp;</td>";
                                dynamichtml += "</tr>";
                            }
                        }





                        for (var i = 0; i < html.d.length; i++) {

                            if (pagecounter == 0) {
                                pagecounter = pagecounter + 1;
                                dynamichtml += "<table id='tblpassbook' width='100%'><thead><tr>";

                                dynamichtml += "<colgroup>";
                                dynamichtml += "<col class='ten' />      ";
                                dynamichtml += "<col class='ten' />      ";
                                dynamichtml += "<col class='fourty' />   ";
                                dynamichtml += "<col class='fifteen' />  ";
                                dynamichtml += "<col class='fifteen' />  ";
                                dynamichtml += "<col class='fifteen' />  ";
                                dynamichtml += "</colgroup>";

                                dynamichtml += "<th>DATE</th>";
                                dynamichtml += "<th>PARTICULARS</th>";
                                dynamichtml += "<th>TRANSACTION DETAILS</th>";
                                dynamichtml += "<th>WITHDRAWALS</th>";
                                dynamichtml += "<th>DIPOSIT</th>";
                                dynamichtml += "<th>BALANCE</th>";
                                dynamichtml += "</tr></thead>";

                            }


                            if (pagecounter == 17) {
                                pagecounter = pagecounter + 1;
                                dynamichtml += "<tr rowspan='1'>";
                                dynamichtml += "<td>&nbsp;&nbsp;</td>";
                                dynamichtml += "<td>&nbsp;&nbsp;</td>";
                                dynamichtml += "<td>&nbsp;&nbsp;</td>";
                                dynamichtml += "<td>&nbsp;&nbsp;</td>";
                                dynamichtml += "<td>&nbsp;&nbsp;</td>";
                                dynamichtml += "<td>&nbsp;&nbsp;</td>";
                                dynamichtml += "</tr>";

                            }
                            else if (pagecounter == 36) {
                                pagecounter = pagecounter + 1;
                                dynamichtml += "<tr>";
                                dynamichtml += "<td colspan='6' style='text-align: right;'>Continue ...</td>";
                                dynamichtml += "</tr>";

                                if (pagecounter == pagesize) {
                                    pagecounter = 0;
                                    dynamichtml += "</tbody></table>";
                                }

                            }
                            else if (pagecounter == 1) {
                                pagecounter = pagecounter + 1;
                                dynamichtml += "<tbody><tr>";
                                dynamichtml += "<td>" + html.d[i].T_DATE + "</td>";
                                dynamichtml += "<td>" + html.d[i].Type + "</td>";
                                dynamichtml += "<td>" + html.d[i].Narration2 + "</td>";
                                if (html.d[i].Withdrwal.trim() != "0.00")
                                    dynamichtml += "<td>" + html.d[i].Withdrwal + "</td>";
                                else
                                    dynamichtml += "<td></td>";

                                if (html.d[i].Deposit.trim() != "0.00")
                                    dynamichtml += "<td>" + html.d[i].Deposit + "</td>";
                                else
                                    dynamichtml += "<td></td>";

                                dynamichtml += "<td>" + html.d[i].Balance + "</td>";
                                dynamichtml += "</tr>";
                            }
                            else {
                                pagecounter = pagecounter + 1;
                                dynamichtml += "<tr>";
                                dynamichtml += "<td>" + html.d[i].T_DATE + "</td>";
                                dynamichtml += "<td>" + html.d[i].Type + "</td>";
                                dynamichtml += "<td>" + html.d[i].Narration2 + "</td>";
                                if (html.d[i].Withdrwal.trim() != "0.00")
                                    dynamichtml += "<td>" + html.d[i].Withdrwal + "</td>";
                                else
                                    dynamichtml += "<td></td>";

                                if (html.d[i].Deposit.trim() != "0.00")
                                    dynamichtml += "<td>" + html.d[i].Deposit + "</td>";
                                else
                                    dynamichtml += "<td></td>";
                                dynamichtml += "<td>" + html.d[i].Balance + "</td>";
                                dynamichtml += "</tr>";

                                if (pagecounter == pagesize) {
                                    pagecounter = 0;
                                    dynamichtml += "</tbody></table>";
                                }
                            }

                        }


                        $("#passbook").html(dynamichtml);
                        printDiv('passbook');

                        var newObj = {};
                        newObj.SlCode = $("#SlCode").val();
                        newObj.Line = pagecounter;



                        $.ajax({
                            type: "POST",
                            url: "/Transaction/frmPassBook.aspx/SavePassbookDetails",
                            cache: false,
                            contentType: "application/json;",
                            dataType: "json",
                            data: JSON.stringify(newObj),
                            success: function (html) {
                            }
                        });



                    }

                }
            });
        }


        function printDiv(divName) {

            var contents = $("#passbook").html();
            var frame1 = $('<iframe />');
            frame1[0].name = "frame1";
            frame1.css({ "position": "absolute", "top": "-1000000px" });
            $("body").append(frame1);
            var frameDoc = frame1[0].contentWindow ? frame1[0].contentWindow : frame1[0].contentDocument.document ? frame1[0].contentDocument.document : frame1[0].contentDocument;
            frameDoc.document.open();
            //Create a new HTML document.
            frameDoc.document.write('<html><title></title><head>');
            frameDoc.document.write('</head><body>');
            //Append the external CSS file.
            frameDoc.document.write('<link href="<%=  Application["BasePage"] %>Content/css/Passbook.css" rel="stylesheet" type="text/css" />');
            //Append the DIV contents.
            frameDoc.document.write(contents);
            frameDoc.document.write('</body></html>');
            frameDoc.document.close();
            setTimeout(function () {
                window.frames["frame1"].focus();
                window.frames["frame1"].print();
                frame1.remove();
            }, 500);

        }

        //Print Pass Book License

        function PrintPassBookLicense() {
            var obj = {};
            obj.SlCode = $("#SlCode").val();
            var dynamichtml = "";

            var pagesize = 30;
            var counter = 0;
            var pagecounter = 0;


            $.ajax({
                type: "POST",
                url: "/Transaction/frmPassBook.aspx/GetPassbookLicenseDetails",
                cache: false,
                contentType: "application/json;",
                dataType: "json",
                data: JSON.stringify(obj),
                success: function (lisenceResult) {
                    $('#fe_lisence_name').html(lisenceResult.d.lisenceName);
                    $('#fe_lisence_address').html(lisenceResult.d.lisenceaddress);
                    $('#cbs_account_number').html(lisenceResult.d.cbsacno);
                    $('#society_acno').html(lisenceResult.d.acno);
                    $('#account_holder_name').html(lisenceResult.d.acholdername);
                    $('#account_holder_gurdian_name').html(lisenceResult.d.gurdianname);
                    $('#fe_mobile_no').html(lisenceResult.d.phoneno);
                    $('#micr_code').html(lisenceResult.d.micrno);
                    $('#society_bank_branch').html(lisenceResult.d.societybankbranch);
                    $('#society_bank_branch_address').html(lisenceResult.d.societybankbranchaddress);
                    $('#customer_address').html(lisenceResult.d.address);
                    $('#fe_pancard_no').html(lisenceResult.d.pancardno);



                    $('#ifsc_code').html(lisenceResult.d.ifsccode);

                    $('#lisence').attr("style", "display:block");

                    printLisence();
                }

            });
        }

        function printLisence() {

            var contents = $("#lisence").html();
            var frame1 = $('<iframe />');
            frame1[0].name = "frame1";
            frame1.css({ "position": "absolute", "top": "-1000000px" });
            $("body").append(frame1);
            var frameDoc = frame1[0].contentWindow ? frame1[0].contentWindow : frame1[0].contentDocument.document ? frame1[0].contentDocument.document : frame1[0].contentDocument;
            frameDoc.document.open();
            //Create a new HTML document.
            frameDoc.document.write('<html><title></title><head>');
            frameDoc.document.write('</head><body>');
            //Append the external CSS file.
            //frameDoc.document.write('<link href="Content/Passbook.css" rel="stylesheet" type="text/css" />');
            //Append the DIV contents.
            frameDoc.document.write(contents);
            frameDoc.document.write('</body></html>');




            frameDoc.document.close();

            //console.log((window.frames["frame1"].html);
            setTimeout(function () {
                window.frames["frame1"].focus();
                window.frames["frame1"].print();
                //window.frames["frame1"].op;
                frame1.remove();
            }, 500);

        }

    </script>

    

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager runat="server" id="scr1"></asp:ScriptManager><br /><br />
       
        <div class="row">
            <div class="col-lg-12">
                <div class="panel panel-primary">
                    <div class="panel-heading" style="font-size:larger;">
                        Passbook Print
                    </div>
                    <div class="panel-body" style="font-size:larger;">
                        <div class="row">

                            <div class="form-group">

                                <div class="col-md-2">
                                    <label style="margin-right: 20PX;font-size:larger;">Old A/C No.</label>
                                 <%-- <asp:TextBox ID="txtOldAcNo" CssClass="form-control" placeholder="Old A/C No"  autocomplete="off"  AutoPostBack="false"></asp:TextBox>--%>
                                    <input type="text" id="oldAcNo" class="form-control" onclick="GetSlDedetailsOld();"  style="font-size:larger;"/>
                                </div>
 
                               
                                <div class="col-md-2">
                                    <label style="margin-right: 20PX;font-size:larger;">New A/C No</label>
                                    <input type="text" id="SlCode" class="form-control" onclick="GetSlDedetailsSL();" style="font-size:larger;"/>
                                   <%-- <asp:TextBox ID="txtNewAcNo" CssClass="form-control" placeholder="New A/c NO"  autocomplete="off"   AutoPostBack="false"></asp:TextBox>--%>
                                </div>
                                <div class="col-md-2">
                                    <label style="margin-right: 20PX;font-size:larger;">Last Print Date</label>
                                    <input type="text" class="form-control" id="lastPrintDate" disabled="disabled" style="font-size:larger;"/>
                                </div>

                                 <div class="col-md-2">
                                    <label style="margin-right: 20PX;"></label><br />
                                      <%--<asp:Button ID="btnPrintLicense"  Text="Print License" class="btn btn-success" />--%>
                                     <button type="button" class="btn btn-success" onclick="PrintPassBookLicense()">Print License</button>
                                </div>
                                  <div class="col-md-1">
                                    <label style="margin-right: 20PX;"></label><br />
                                      <%--<button ID="btnPrintPassbook"  Text="Print Passbook" OnClick="printPassBook()" class="btn btn-success"></button>--%>
                                      <button type="button" class="btn btn-success" onclick="PrintPassbook()">Print Passbook</button>
                                </div>

                                 <div class="clearfix"></div>
                               
    
                                <hr />
                            </div>

                        </div>

                        <div  class="row">
                            <div id="passbook">



                            </div>

                            <div id="test">
                               
                            </div>

                            <div id="lisence" style="display: none;">
                                <table width="100%" border="0">

                                        <tr>
                                            <td height="315px" colspan="7">&nbsp;</td>
                                        </tr>
           
                                        <tr>
                                            <td style="font-size: 14px; font-weight: bold; text-align: center;" colspan="7">SAVINGS ACCOUNT PASSBOOK</td>
                                        </tr>

                                        <tr>
                                            <td style="font-size: 14px; font-weight: bold; text-align: center;" colspan="7"><span id="fe_lisence_name">&nbsp;</span></td>
                                        </tr>

                                        <tr>
                                            <td style="font-size: 14px; font-weight: bold; text-align: center;" colspan="7"><span id="fe_lisence_address">&nbsp;</span></td>
                                        </tr>
                                        <tr>
                                            <td style="font-size: 14px; font-weight: bold; text-align: center;" style="border-bottom: 1px solid;"  colspan="7">Apex Bank-IDBI BANK LTD</td>
                                        </tr>

                                        <tr>
                                            <td style="text-align: left;" width="1px">&nbsp;</td>
                                            <td style="font-size: 12px; font-weight: bold; text-align: left;"  width="250px">CBS ACCOUNT NO</td>
                                            <td style="font-size: 12px; font-weight: bold; text-align: center;">:</td>
                                            <td style="font-size: 12px; text-align: left;" width="550px"><span id="cbs_account_number">&nbsp;</span></td>

                                            <td style="font-size: 12px; font-weight: bold; text-align: left;"  width="150px">IFSC CODE</td>
                                            <td style="font-size: 12px; font-weight: bold; text-align: center;">:</td>
                                            <td style="font-size: 12px; text-align: left;" ><span id="ifsc_code">&nbsp;</span></td>
                                        </tr>
              
            
                                        <tr>
                                            <td style="text-align: right;">&nbsp;</td>
                                            <td style="font-size: 12px; font-weight: bold; text-align: left;">SOCIETY A/C NO</td>
                                            <td style="font-size: 12px; font-weight: bold; text-align: center;">:</td>
                                            <td style="font-size: 12px; text-align: left;"><span id="society_acno">&nbsp;</span></td>

                                            <td style="font-size: 12px; font-weight: bold; text-align: left;" >MICR CODE</td>
                                            <td style="font-size: 12px; font-weight: bold; text-align: center;" >:</td>
                                            <td style="font-size: 12px; text-align: left;" ><span id="micr_code">&nbsp;</span></td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right;">&nbsp;</td>
                                            <td style="font-size: 12px; font-weight: bold; text-align: left;" >NAME (S)</td>
                                            <td style="font-size: 12px; font-weight: bold; text-align: center;" >:</td>
                                            <td style="font-size: 12px; text-align: left;"><span id="account_holder_name">&nbsp;</span></td>
                                            <td style="font-size: 12px; font-weight: bold; text-align: left;" >BRANCHNAME</td>
                                            <td style="font-size: 12px; font-weight: bold; text-align: center;" >:</td>
                                            <td style="font-size: 12px; text-align: left;" ><span id="society_bank_branch">&nbsp;</span></td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right;">&nbsp;</td>
                                            <td style="font-size: 12px; font-weight: bold; text-align: left;" >S/O/W/O/D/O</td>
                                            <td style="font-size: 12px; font-weight: bold; text-align: center;" >:</td>
                                            <td style="font-size: 12px; text-align: left;" ><span id="account_holder_gurdian_name">&nbsp;</span></td>
                                            <td style="font-size: 12px; font-weight: bold; text-align: left;" >BRANCHCODE</td>
                                            <td style="font-size: 12px; font-weight: bold; text-align: center;" >:</td>
                                            <td style="font-size: 12px; text-align: left;" ><span id="ifsc_code">&nbsp;</span></td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right;">&nbsp;</td>
                                            <td style="font-size: 12px; font-weight: bold; text-align: left;" >ADDRESS</td>
                                            <td style="font-size: 12px; font-weight: bold; text-align: center;" >:</td>
                                            <td style="font-size: 12px; text-align: left;" ><span id="customer_address">&nbsp;</span></td>
                                            <td style="font-size: 12px; font-weight: bold; text-align: left;" >BRANCHADDRESS</td>
                                            <td style="font-size: 12px; font-weight: bold; text-align: center;" >:</td>
                                            <td style="font-size: 12px; text-align: left;" ><span id="society_bank_branch_address">&nbsp;</span></td>
                                        </tr>

                                        <tr>
                                            <td style="text-align: right;">&nbsp;</td>
                                            <td style="font-size: 12px; font-weight: bold; text-align: left;" >MOBILE NO</td>
                                            <td style="font-size: 12px; font-weight: bold; text-align: center;" >:</td>
                                            <td style="font-size: 12px; text-align: left;" ><span id="fe_mobile_no">&nbsp;</span></td>
                                             <td style="font-size: 12px; font-weight: bold; text-align: left;" >PANCARD NO</td>
                                            <td style="font-size: 12px; font-weight: bold; text-align: center;" >:</td>
                                            <td style="font-size: 12px; text-align: left;" ><span id="fe_pancard_no">&nbsp;</span></td>
                                            <td colspan="4">&nbsp;</td>
                                        </tr>
                                       
                                        <tr>
											<td colspan="7">&nbsp;</td>
									    </tr>
                                        <tr>
                                            <td style="text-align: right;">&nbsp;</td>
                                            <td style="font-size: 12px; font-weight: bold; text-align: right;" colspan="2" >ACCOUTANT</td>
                                            <td style="font-size: 12px; text-align: left;"><span id="">&nbsp;</span></td>
                                            <td style="font-size: 12px; font-weight: bold; text-align: left;" colspan="2">MANAGER</td>
                                            <td style="font-size: 12px; text-align: left;" ><span id="">&nbsp;</span></td>
                                        </tr>
                                    </table>
                                

                                </table>
                                

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>


</asp:Content>