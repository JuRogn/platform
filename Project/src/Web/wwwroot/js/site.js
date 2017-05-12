// Write your Javascript code.
$.ajaxSetup({ cache: false });

$(document).ajaxStart(function () {
    Pace.restart();
});

$(document).ajaxStop(function () {
    $('#Main input[type=text]:first').focus();
    Pace.stop();
});

$(document).ajaxError(function (event, request) {
    if (request.responseText === '' || request.responseText == undefined) {
        $.notify('您的网络无法访问到服务器，请稍后再试！', { "status": "danger", "pos": "bottom-right" });
    } else {
        $.notify(request.responseText, { "status": "danger", "pos": "bottom-right" });
    }
});