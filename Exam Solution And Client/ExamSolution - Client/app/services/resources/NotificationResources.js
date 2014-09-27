'use strict';

appMain.factory('NotificationResources', function (httQ, appSettings) {
    var serviceBase = appSettings.apiUrl + "/api/notifications";

    return {
        getAllUnread: function getUnreadFunc() {
            return httQ.get(serviceBase);
        },
        getLast: function getLastNotifFunc() {
            return httQ.get(serviceBase + "/next");
        }
    };
});