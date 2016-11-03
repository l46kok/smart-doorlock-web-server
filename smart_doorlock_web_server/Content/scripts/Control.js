function appendToLog(txt) {
    var log = $("#mqttLog");
    log.val(log.val() + txt + "\n");
}

$(document).ready(function () {
    $("#myTable").tablesorter();
    $("#btnSubscribe").click(function () {
        var btnElement = $(this);
        var clientId = btnElement.attr("data-cid");
        var txtElement = $("#inputSubscribeTopic" + clientId);
        var data = { "clientId": clientId, "topic": txtElement.val() }
        $.ajax({
            type: "POST",
            url: "/Subscribe",
            data: data,
            success: function (data) { alert(data); },
            dataType: "json"
        });
    });

    $("#btnPublish").click(function () {
        var btnElement = $(this);
        var clientId = btnElement.attr("data-cid");
        var txtTopic = $("#inputPublishTopic" + clientId);
        var txtMessage = $("#inputPublishMessage" + clientId);
        var data = { "clientId": clientId, "topic": txtTopic.val(), "message": txtMessage.val() }
        $.ajax({
            type: "POST",
            url: "/Publish",
            data: data,
            dataType: "json"
        });

        appendToLog("Published to topic: " + txtTopic.val() + ", message: " + txtMessage.val())
    });
});

