<%@ Page Language="C#" MasterPageFile="~/MasterPages/ProjectBM.Master" AutoEventWireup="true" CodeBehind="frmShareAccountOpening.aspx.cs" Inherits="iBankingSolution.Transaction.frmShareAccountOpening" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.4.1/js/bootstrap-datepicker.min.js"></script>
         <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.4.1/css/bootstrap-datepicker3.css"/>
    <script type="text/javascript">
        $ = jQuery.noConflict();
        $(function () {
            $('.select2').select2()
        })
    </script>




<script>

    var LINENUMBER = 0;

    function GetSlDedetailsOld() {

        if ($("#oldAcNo").val() == "") return;


        var obj = {};
        obj.SlCode = $("#SlCode").val();
        obj.OldAcNo = $("#oldAcNo").val();


        $.ajax({
            type: "POST",
            url: "/PassBook.aspx/GetClientDetails",
            cache: false,
            contentType: "application/json; charset=utf-8",
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
            url: "/PassBook.aspx/GetClientDetails",
            cache: false,
            contentType: "application/json; charset=utf-8",
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

        var pagesize = 30;
        var counter = 0;
        var pagecounter = 0;


        $.ajax({
            type: "POST",
            url: "/PassBook.aspx/GetPassbookDetails",
            cache: false,
            contentType: "application/json; charset=utf-8",
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
                        url: "/PassBook.aspx/SavePassbookDetails",
                        cache: false,
                        contentType: "application/json; charset=utf-8",
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
        //var printContents = document.getElementById(divName).innerHTML;
        //var originalContents = document.body.innerHTML;

        //document.body.innerHTML = printContents;

        //window.print();

        //   document.body.innerHTML = originalContents;






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
        frameDoc.document.write('<link href="Content/Passbook.css" rel="stylesheet" type="text/css" />');
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





</script>


<div>
    <table>
        <tr>
            <td>Old Ac No.</td>
            <td>SL Code.</td>
            <td>Last Print Date</td>
            <td></td>

        </tr>

        <tr>
            <td>
                <input type="text" id="oldAcNo" onclick="GetSlDedetailsOld();" />
            </td>
            <td>
                <input type="text" id="SlCode" onclick="GetSlDedetailsSL();" />

            </td>
            <td>
                <input type="text" id="lastPrintDate" disabled="disabled" />

            </td>
            <td>
                <button type="button" class="btn btn-success" onclick="PrintPassbook()">Print Passbook</button>
            </td>
            <td>
                <button type="button" class="btn btn-success" >Print License</button>
            </td>



        </tr>


    </table>


    <div id="passbook">
    </div>

</div>
</asp:Content>

