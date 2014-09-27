'use strict';

appMain.factory('ScoreResources', function (httQ, appSettings) {
    var serviceBase = appSettings.apiUrl + "/api/scores";

    return {
        getTopPlayers: function getUnreadFunc() {
            return httQ.get(serviceBase);
        }
    };
});