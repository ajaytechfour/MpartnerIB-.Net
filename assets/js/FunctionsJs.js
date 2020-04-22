/// <reference path="jquery-1.10.2.js" />

//Getting All the functions Details

$(function () {
    $.get("../FunctionsGroup/FunctionsDetails", null, function (data) { SetFunctionsGrid(data); }, "Json");
});



//Redirection To Edit
function Edit(id) {
    location.href = "../FunctionsGroup/EditFunctions/" + id;
}

//Set Paging
function SetPages(paging) {
    var pages = "";
    $("#Paging").empty();


    for (var i = 1; i <= parseInt(paging); i++) {

        pages += "<a style='cursor:pointer' onclick=GetPagesValues(" + i + ");> " + i + "  </a>";

        debugger;
    }
    $("#Paging").append("<u>Pages:-</u>" + pages);
}

//Reuest for new Page
function GetPagesValues(id) {
    $.post("../FunctionsGroup/FunctionsDetails", { page: id }, function (data) { SetFunctionsGrid(data); }, "Json");
}

//Delete Record

function Delete(id) {
    if (confirm("Do You Want To Delete This Record")) {

        $.post("../FunctionsGroup/Delete", { id: id }, function (data) {
            $.post("../FunctionsGroup/FunctionsDetails", null, function (data) { SetFunctionsGrid(data); }, "Json");

            alert(data);

        }, "Json");
    }
}