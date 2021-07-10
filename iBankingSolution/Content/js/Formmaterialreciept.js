/// <reference path="jquery.min.js" />

function check_qty(e) {
    debugger;
    var RQty = e.value;
    var NId = e.id;
    var valt = NId.lastIndexOf("_");
    var totallength = NId.length - 1;
    var substr = NId.substr(valt + 1, totallength - valt);
    var ElementId = "MainContent_GVMaterialReceived_txtPOQty_" + substr;
    var ProductValueElementId = "MainContent_GVMaterialReceived_txtProductValue_" + substr;
    var PORateElementId = "MainContent_GVMaterialReceived_txtPORate_" + substr;
    var ProductValue = document.getElementById(ProductValueElementId).value;
    var PORate = document.getElementById(PORateElementId).value;

    var rem_qty = document.getElementById(ElementId).value;
    if (parseFloat(RQty) > parseFloat(rem_qty)) {
        alert("Received Quantiy cannot exceed PO Balance.");
        e.value = '0';
    }
    else {
        document.getElementById(ProductValueElementId).value = parseFloat(PORate) * parseFloat(RQty);
    }
}