/// <reference path="jquery.min.js" />
$(document).ready(function () {
    $('#ctl00$MainContent$txt_purchasebillno').prop("disabled", true);
        $('#MainContent_chk_isbiilreciept').click(function () {
            if ($(this).is(":checked")) {
            

                $('#ctl00$MainContent$txt_purchasebillno').prop("disabled", false);


            }
            else if ($(this).is(":not(:checked)")) {
                $('#ctl00$MainContent$txt_purchasebillno').prop("disabled", true);
            }
        });
   

  
  

});