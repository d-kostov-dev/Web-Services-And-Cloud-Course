'use strict';

appMain.controller('PlayGameController', function ($scope, $location, $routeParams,$timeout, GameResources, NotificationResources) {
    $scope.makeGuess = function makeGuessFunc() {
        GameResources.makeGuess($routeParams.id, $scope.guessData)
            .then(function () {
                $scope.getGameInfo();
                $scope.getNotifications();
            }, function (error) {
                alert(error.Message);
            });
    };

    $scope.getGameInfo = function getGameInfoFunc() {
        GameResources.gameInfo($routeParams.id)
            .then(function (response) {
                $scope.gameInfo = response;
            });
    };

    $scope.getNotifications = function getNotificationsFunc() {
        NotificationResources.getLast()
            .then(function(response) {
                // TODO: Append not replace
                $scope.notifications = response;
            });
    };
});