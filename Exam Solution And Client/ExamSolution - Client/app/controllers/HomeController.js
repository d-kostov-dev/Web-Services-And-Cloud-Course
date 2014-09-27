'use strict';

appMain.controller('HomeController', function ($scope, $location, GameResources, ScoreResources) {
    $scope.askForNumberForm = false;
    $scope.selectedGameId = 0;

    GameResources.getAvailableGames()
        .then(function (response) {
            $scope.games = response;
        });

    ScoreResources.getTopPlayers()
        .then(function (response) {
            $scope.topPlayers = response;
        });

    $scope.joinGame = function joinGameFunc(number) {
        GameResources.joinGame($scope.selectedGameId, {Number: number})
            .then(function () {
                $location.path("/play/" + $scope.selectedGameId);
            }, function (error) {
                alert(error.Message);
                $scope.askForNumberForm = false;
                $scope.selectedGameId = 0;
            });
    };

    $scope.askForNumber = function askNumberFunc(id) {
        $scope.askForNumberForm = true;
        $scope.selectedGameId = id;
    };

    $scope.cancelJoin = function cancelJoinFunc() {
        $scope.askForNumberForm = false;
        $scope.selectedGameId = 0;
    };
});