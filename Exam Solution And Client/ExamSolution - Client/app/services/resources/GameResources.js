'use strict';

appMain.factory('GameResources', function (httQ, appSettings) {
    var serviceBase = appSettings.apiUrl + "/api/games";

    return {
        getAvailableGames: function getGamesFunc() {
            return httQ.get(serviceBase);
        },
        createGame: function createGameFunc(gameData) {
            return httQ.post(serviceBase, gameData, {'Content-Type': 'application/json'});
        },
        joinGame: function joinGameFunc(id, number) {
            return httQ.put(serviceBase + "/" + id, number, {'Content-Type': 'application/json'});
        },
        gameInfo: function gameInfoFunc(id) {
            return httQ.get(serviceBase + "/" + id);
        },
        makeGuess: function guessFunc(gameId, guessData) {
            return httQ.post(serviceBase + "/" + gameId + "/guess", guessData, {'Content-Type': 'application/json'});
        }
    };
});