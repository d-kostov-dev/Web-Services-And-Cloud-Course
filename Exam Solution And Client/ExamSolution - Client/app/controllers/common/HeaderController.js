﻿'use strict';

appMain.controller('HeaderController', function ($scope, $location, AccountService) {
    $scope.title = "AngularJS Single Page Application";

    $scope.logOut = function () {
        AccountService.logOutUser();
        $location.path('/home');
    };

    $scope.userData = AccountService.userData;
});