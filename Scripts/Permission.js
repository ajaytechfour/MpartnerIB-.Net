/// <reference path="chosen.jquery.js" />
/// <reference path="jquery-2.1.4.js" />

//Script For Region Destributor Dealer

$(function () {
    //Get All Regions
    $.post("../../Destributor/GetRegions", null, function (data) { SetProdcutCategory(data, 'RegionList'); }, "Json");
    $("#DestributorList").chosen({ width: "100px" });
    $("#DealerList").chosen({ width: "100px" });
});

//Fill DropDown
function SetProdcutCategory(data, id) {
    if (data == "Login") {
        location.href = "../Logout/Logout";
    }
    else if (data == "snotallowed") {
        location.href = "../snotallowed/snotallowed";
    }
    else {
        option = "";
        var procat = $("#" + id);
        procat.empty();
        procat.append("<option value=0 disabled='disabled'>Select</option>");
        $.each(data, function (key, value) {
            option += "<option value=" + value.id + ">" + value.Name + "</option>";
        });
        procat.append(option);
        procat.chosen({ width: "100px" });
        procat.trigger("chosen:updated");
    }
   
}

//All Checkbox
function Allch() {
    if ($("#All").is(":checked")) {
        $("#region").attr("disabled", true);
        $("#region").removeAttr("checked");
        $("#destributor").attr("disabled", true);
        $("#destributor").removeAttr("checked");
        $("#Dealer").attr("disabled", true);
        $("#Dealer").removeAttr("checked");
        $("#RegionList_chosen").hide();
        $("#DestributorList_chosen").hide();
        $("#DealerList_chosen").hide();
        $("#checkDistri").hide();
        $("#checkDeal").hide();
    }
    else {
        $("#region").attr("disabled", false);
        $("#destributor").attr("disabled", false);
        $("#Dealer").attr("disabled", false);
        $("#RegionList_chosen").show();
        $("#DestributorList_chosen").show();
        $("#DealerList_chosen").show();
        $("#checkDistri").show();
        $("#checkDeal").show();
    }

}
//GetRegions After Clicking on Region Checkbox
function GetRegions() {
    if ($("#region").is(":checked")) {

        $.post("../../Destributor/GetRegions", null, function (data) { SetProdcutCategory(data, 'RegionList'); },
                 "Json");
        $("#RegionList_chosen").show();

    }
    else {
        $("#RegionList_chosen").hide();
    }

}

//Get Destributor After Clicking on Destributor Checkbox
function GetDestributor() {
    if ($("#destributor").is(":checked")) {
        if ($("#region").is(":checked")) {
            SetDestributor();

        }
        else {
            $.post("../../Dealers/GetAllDestributors", null, function (data) { SetProdcutCategory(data, 'DestributorList'); }, "Json");
        }
        $("#DestributorList_chosen").show();
    }
    else {

        $("#DestributorList_chosen").hide();
    }
}

//Get Dealer After Clicking On Dealer Checkbox
function GetDealer() {
    if ($("#Dealer").is(":checked")) {
        if ($("#destributor").is(":checked")) {
            SetDealer();
        }
        else {

            $.post("../../Dealers/GetAllDealer", null, function (data) { SetProdcutCategory(data, 'DealerList'); }, "Json");
        }
        $("#DealerList_chosen").show();
    }
    else {

        $("#DealerList_chosen").hide();
    }
}

//Set Regions

function SetRegion() {
    $("#RgList").val($("#RegionList").val());
    var regionlist = $("#RgList").val();

    $.post("../../Dealers/GetDestributorsByMultiPleids", { RegionId: regionlist }, function (data) { SetProdcutCategory(data, 'DestributorList'); }, "Json");
}
//Set Destributor

function SetDestributor() {
    $("#disList").val($("#DestributorList").val());

    var regionlist = $("#disList").val();

    $.post("../../Dealers/GetAllDealerByMultiPleIds", { RegionId: regionlist }, function (data) { SetProdcutCategory(data, 'DealerList'); }, "Json");
}

//set Dealer

function SetDealer() {
    $("#Dealist").val($("#DealerList").val());
}