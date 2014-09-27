'use strict';

appMain.controller('CreateGameController', function ($scope, $location, AccountService, GameResources) {
    $scope.gameData = {};
    $scope.errorMessage = "";

    if(AccountService.userData.isAuth === false){
        $location.path("/home");
    }

    $scope.createGame = function createGameFunc() {
        GameResources.createGame($scope.gameData)
            .then(function (response) {
                $location.path("/play/" + response.Id);
            }, function (error) {
                $scope.errorMessage = error.Message;
            });
    };
});