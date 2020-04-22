$(document).ready(function () {
    chkItemAll();
    chkStateAll();
    $("#ddlitem").select2({
        //closeOnSelect: false,
        placeholder: "Select Item",
        //allowHtml: true,
        //allowClear: true,
        //tags: true // создает новые опции на лету
        //templateSelection: function (data, container) {
        //    debugger;
        //    //$(data.element).attr('data-custom-attribute', data.customValue);
        //    if (data.id === '') { // adjust for custom placeholder values
        //        return 'Custom styled placeholder text';
        //    }
        //    return data.text;
        //}
    }).on('change', function (e) {
        $("#ddlitem").find('placeholder').prop("selected", true);
        var count = this.selectedOptions.length // how options selected
        var opt = this.length; // how options total
        if (count > 0) {
            $('#txtMaterialcode').val('');
            $('#txtMaterialcode').attr('readonly', true);
            $('#txtMaterialcode').addClass('input-disabled');
        } else {
            $('#txtMaterialcode').attr('readonly', false);
            $('#txtMaterialcode').removeClass('input-disabled');
        }
        //var selitems = $('#ddlitem').select2().val()
        //if (selitems != null) {
        //    $("#error_message").html("").hide();
        //    return false;
        //}
    });
    $("#ddlstate").select2({
        placeholder: "Select State",
        //allowClear: true,
    }).on('change', function (e) {
        $("#ddlstate").find('placeholder').prop("selected", true);
        var selitems = $('#ddlstate').select2().val()
        if (selitems != null) {
            $("#error_message").html("").hide();
            return false;
        }
    });
    btnsubmit();
    btnUploadsubmit();

    $("#sdate").datepicker();
    $("#edate").datepicker();
    //$('.date').datepicker({ dateFormat: "dd/mm/yy" });
    $("#sdate").datepicker({
        maxDate: '0'
        //  dateFormat: "dd/mm/yy"
        //beforeShow: function () {
        //    $('.sdate').datepicker('option', 'minDate', jQuery('#edate').val() - 6);
        //}
    });
    $("#edate").datepicker({
        maxDate: new Date()
        //  dateFormat: "dd/mm/yy"
    });
    debugger;
    //var LogDownloaddata = '@TempData["LogDownload"]';
    //if (LogDownloaddata != "") {
    //}
});
function chkItemAll() {
    $("#checkboxItem").click(function () {
        if ($("#checkboxItem").is(':checked')) {
            // $("#ddlitem > option").prop("selected", "selected");
            $("#ddlitem").find('option').prop("selected", true);
            $("#ddlitem").trigger("change");
            //return false;
        } else {
            //  $("#ddlitem > option").removeAttr("selected");
            $("#ddlitem").find('option').prop("selected", false);
            $("#ddlitem").trigger("change");
            //return false;
        }
    });
    $("#ddlitem").select2({ closeOnSelect: false });
    //$("#ddlitem").trigger("change");
}
function chkStateAll() {
    $("#checkboxState").click(function () {
        if ($("#checkboxState").is(':checked')) {
            // $("#ddlitem > option").prop("selected", "selected");
            $("#ddlstate").find('option').prop("selected", true);
            $("#ddlstate").trigger("change");
            //return false;
        } else {
            //  $("#ddlitem > option").removeAttr("selected");
            $("#ddlstate").find('option').prop("selected", false);
            //$('#ddlstate').attr('placeholder', 'New Placeholder Text').select2();
            $("#ddlstate").trigger("change");
            //return false;
        }
    });
    $("#ddlstate").select2({ closeOnSelect: false });
}
function btnsubmit() {
    $('#btnsubmit').click(function (e) {
        //$(".loader").show();
        //debugger;
        var items = $('#ddlitem').select2().val()
        if (items == null || items == "") {
            $("#error_message").show().html("No Item selected.");
            $("#ddlitem").select2({
                placeholder: "Select Item"
            });
            return false;
        }
        else {
            $("#error_message").html("").hide();
        }
        var state = $('#ddlstate').select2().val()
        if (state == null || state == "") {
            $("#error_message").show().html("No State selected.");
            $("#ddlstate").select2({
                placeholder: "Select State"
            });
            return false;
        }
        else {
            $("#error_message").html("").hide();
        }
        $('#hfitemId').val(items);
        $('#hfstateId').val(state);
        $('#hdnsdate').val('');
        $('#hdnedate').val('');
        if (e.data == "test") {
            //debugger;
            var ss = "";
        }
    });
    $('#btnsubmitLog').click(function (e) {
        //$(".loader").show();
        //debugger;
        var state = $('#ddlstate').select2().val()
        //if (state == null || state == "") {
        //    $("#error_message").show().html("No State selected.");
        //    $("#ddlstate").select2({
        //        placeholder: "Select State"
        //    });
        //    return false;
        //}
        //else {
        //    $("#error_message").html("").hide();
        //}
        var mcode = $('#txtMaterialcode').val()
        var items = $('#ddlitem').select2().val()
        //if (mcode == null || mcode == "") {
        //    if (items == null || items == "") {
        //        $("#error_message").show().html("Select Item or Enter Material Code.");
        //        $("#ddlitem").select2({
        //            placeholder: "Select Item"
        //        });
        //        return false;
        //    }
        //    else {
        //        $("#error_message").html("").hide();
        //    }
        //}
        var sDate = $('#sdate').val()
        if (sDate == null || sDate == "") {
            $("#error_message_log").show().html("No Start Date selected.");
            return false;
        }
        else {
            $("#error_message_log").html("").hide();
        }
        var eDate = $('#edate').val()
        if (eDate == null || eDate == "") {
            $("#error_message_log").show().html("No End Date selected.");
            return false;
        }
        else {
            $("#error_message_log").html("").hide();
        }
        if (Date.parse(sDate) > Date.parse(eDate)) {
            alert("From Date should be less then To Date.");
            return false;
        }
        //else {
        //    $("#error_message").html("").hide();
        //}
        $('#hfitemId').val(items);
        $('#hfstateId').val(state);
        $('#hdnMaterialcode').val(mcode);
        $('#hdnsdate').val(sDate);
        $('#hdnedate').val(eDate);
    });
    //$(".loader").hide();
}
function btnUploadsubmit() {
    $('#btnUploadsubmit').click(function (e) {
        //debugger;
        var vidFileLength = $("#FileUpload1")[0].files.length;
        if (vidFileLength === 0) {
            $("#error_message_upload").show().html("No file selected.");
            //$(".loader").hide();
            return false;
        }
        else {
            $("#error_message_upload").html("").hide();
        }
    });
}