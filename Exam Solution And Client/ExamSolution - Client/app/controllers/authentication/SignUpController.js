'use strict';

appMain.controller('SignUpController', function ($scope, $location, $timeout, AccountService) {
    $scope.successfulRegistration = false;
    $scope.formMessage = "";
//    $scope.registration = {};

    if(AccountService.userData.isAuth === true){
        AccountService.logOutUser();
    }

    $scope.signUp = function signUpFunction() {
        AccountService.registerUser($scope.registration)
            .then(function () {
                $scope.successfulRegistration = true;
                $scope.formMessage = "Registered successfully, redirecting to login page...";
                startTimer();
            }, function (response) {
                var errors = [];

                for (var key in response.ModelState) {
                    for (var i = 0; i < response.ModelState[key].length; i++) {
                        errors.push(response.ModelState[key][i]);
                    }
                }

                $scope.formMessage = "Registration Failed: " + errors.join(', ');
            });
    };

    function startTimer() {
        var timer = $timeout(function () {
            $timeout.cancel(timer);
            $location.path('/login');
        }, 2000);
    };
});