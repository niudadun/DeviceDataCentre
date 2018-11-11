
// Write your JavaScript code.
jQuery(document).ready(function () {
    $("#GetLatestDevice").click(function () {
        searchRunning = true;
        var serial = $('#serial').val();
        getLatestDevice(serial);
    });
    $("#GetHistory").click(function () {
        searchRunning = true;
        var serialNB = $('#serialNB').val();
        getDeviceHistory(serialNB);
    });
    $("#uploadFile").click(function () {
        UploadFile();
    });
    $("#deviceSwitchOn").click(function () {
        var serialOn = $('#serialOn').val();
        var voltage = $('#voltage').val();
        var version = $('#version').val();
        deviceSwitchOnPUT(serialOn, voltage, version);
    });
    $("#deviceUpdate").click(function () {
        var serialUpdate = $('#serialUpdate').val();
        var temperatureUpdate = $('#temperatureUpdate').val();
        var humidityUpdate = $('#humidityUpdate').val();
        deviceDetailsUpdate(serialUpdate, temperatureUpdate, humidityUpdate);
    });
    
    
});
searchRunning = false;

//Ajax call to retrieve data from REST service
function deviceDetailsUpdate(serialUpdate, temperatureUpdate, humidityUpdate) {
    $("#messageInfo").html("Upload to server...");
    $("#messageInfo").css("visibility", "");
    var data = JSON.stringify({ SerialNumber: serialUpdate, Temperature: temperatureUpdate, Humidity: humidityUpdate });
    $.ajax({
        url: '/api/DeviceUpdate',
        type: 'POST',
        data: data,
        contentType: 'application/json',
        headers: { "Token": $("#token").val() },
        error: function (errorThrown) {
            $("#messageInfo").html(errorThrown.responseText);
            searchRunning = false;
        },
        success: function (result) {
            $("#messageInfo").html(result);
            searchRunning = false;
        }
    }).done(function () {
        searchRunning = false;
        $("#messageInfo").css("visibility", "hidden");
        $("#messageInfo").html("Upload done");
    });

}

function deviceSwitchOnPUT(serialOn, voltage, version) {
    $("#messageInfo").html("Upload to server...");
    $("#messageInfo").css("visibility", "");
    var data = JSON.stringify({ SerialNumber: serialOn, BatteryVoltage: voltage, FirmwareVersion: version});
    $.ajax({
        url: '/api/DeviceUpdate',
        type: 'POST',
        data: data,
        contentType: 'application/json',
        headers: { "Token": $("#token").val() },
        error: function (errorThrown) {
            $("#messageInfo").html(errorThrown.responseText);
            searchRunning = false;
        },
        success: function (result) {
            $("#messageInfo").html(result);
            searchRunning = false;
        }
    }).done(function () {
        searchRunning = false;
        $("#messageInfo").css("visibility", "hidden");
    });

}

function UploadFile() {
    $("#messageInfo").html("Upload to server...");
    $("#messageInfo").css("visibility", "");
    var formData = new FormData();
    formData.append('file', $('.input-ghost')[0].files[0]);
    $.ajax({
        url: window.location.origin + "/DevicesDataCentre/UploadFiles",
        type: 'POST',
        processData: false, 
        contentType: false,
        data: formData,
        error: function (errorThrown) {
            $("#messageInfo").html(errorThrown.responseText);
            searchRunning = false;
        },
        success: function (result) {
            $("#messageInfo").html(result);
            searchRunning = false;
        }
    }).done(function () {
        searchRunning = false;
        $("#messageInfo").html("Upload done");
    });

}

function getDeviceHistory(serial) {
    $("#returnData").html("");
    $("#messageInfo").html("Loading Data from API...");
    $("#messageInfo").css("visibility", "");
    $.ajax({
        url: '/api/GetDeviceInfo/' + serial,
        type: 'GET',
        contentType: 'application/json',
        headers: { "Token": $("#token").val() },
        error: function (errorThrown) {
            $("#messageInfo").html(errorThrown.responseText);
            searchRunning = false;
        },
        success: function (result) {
            $("#messageInfo").css("visibility", "hidden");
            var jsonObject = JSON.stringify(result, undefined, 2);
            $("#returnData").html(jsonObject);
            searchRunning = false;
        }
    }).done(function () {
        $("#messageInfo").css("visibility", "hidden");
        searchRunning = false;
    });

}

function getLatestDevice(serial) {
    ClearTable();
    $("#messageInfo").html("Loading Data from API...");
    $("#messageInfo").css("visibility", "");
    $.ajax({
        url: '/api/DeviceUpdate/' + serial,
        type: 'GET',
        contentType: 'application/json',
        headers: { "Token": $("#token").val() },
        error: function (error) {
            $("#messageInfo").html(error.responseText);
            searchRunning = false;
        },
        success: function (result) {
            $("#messageInfo").css("visibility", "hidden");
            searchRunning = false;
            loadTable(result.serialNumber, result.temperature, result.humidity, result.updateTime);
        }
    }).done(function () {
        $("#messageInfo").css("visibility", "hidden");
        searchRunning = false;
        });

}

function loadTable(serialbumber, temperature, humidity,updatetime) {
    $('#serialbumber').html(serialbumber);
    $('#temperature').html(temperature);
    $('#humidity').html(humidity);
    $('#updatetime').html(updatetime); 
}

function ClearTable() {
    $('#serialbumber').html("");
    $('#temperature').html("");
    $('#humidity').html("");
    $('#updatetime').html("");
}

function bs_input_file() {
    $(".input-file").before(
        function () {
            if (!$(this).prev().hasClass('input-ghost')) {
                var element = $("<input type='file' class='input-ghost' style='visibility:hidden; height:0'>");
                element.attr("name", $(this).attr("name"));
                element.change(function () {
                    element.next(element).find('input').val((element.val()).split('\\').pop());
                });
                $(this).find("button.btn-choose").click(function () {
                    element.click();
                });
                $(this).find("button.btn-reset").click(function () {
                    element.val(null);
                    $(this).parents(".input-file").find('input').val('');
                });
                $(this).find('input').css("cursor", "pointer");
                $(this).find('input').mousedown(function () {
                    $(this).parents('.input-file').prev().click();
                    return false;
                });
                return element;
            }
        }
    );
}
$(function () {
    bs_input_file();
});

