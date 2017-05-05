$(function () {
    var log = $.connection.activityHub;

    log.client.logWatchedVideo = function (videoTitle, username) {
        $('#activityLog')
            .append('<li id="">' + username + ' just watched ' + videoTitle + '</li>')
    }
});