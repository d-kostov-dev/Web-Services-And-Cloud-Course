'use strict';

appMain.controller('LoginController', function ($scope, $location, AccountService) {
    $scope.loginData = {};
    $scope.message = "";

    if(AccountService.userData.isAuth === true){
        AccountService.logOutUser();
    }

    $scope.login = function loginFunction() {
        AccountService.loginUser($scope.loginData)
            .then(function () {
                $location.path("/home");
            }, function () {
                $scope.message = "Wrong username and/or password!";
            });
    };
});