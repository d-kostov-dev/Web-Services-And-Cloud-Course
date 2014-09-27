'use strict';

appMain.factory('AccountService', function ($q, httQ, localStorageService, appSettings) {
    var serviceBase = appSettings.apiUrl;

    var authenticationData = {
        isAuth: false,
        userName: ""
    };

    function registerUser(registration) {
        var data =
            "Email=" + registration.email +
            "&Password=" + registration.password +
            "&ConfirmPassword=" + registration.password;

        return httQ.post(serviceBase + '/api/account/register', data);
    }

    function loginUser(loginData) {
        var deferred = $q.defer();

        var data =
            "grant_type=password" +
            "&UserName=" + loginData.userName +
            "&Password=" + loginData.password;

        httQ.post(serviceBase + '/token', data)
            .then(function (response) {
                localStorageService.set('authorizationData', {
                    token: response.access_token,
                    userName: response.userName
                });

                authenticationData.isAuth = true;
                authenticationData.userName = response.userName;

                deferred.resolve(response);
            }, function (err) {
                logOutUser();
                deferred.reject(err);
            });

        return deferred.promise;
    }

    function logOutUser() {
        httQ.post(serviceBase + '/api/account/logout', "")
            .then(function (response) {
                localStorageService.remove('authorizationData');

                authenticationData.isAuth = false;
                authenticationData.userName = "";
            });
    }

    function checkIdentity() {
        var identityData = localStorageService.get('authorizationData');

        if (identityData) {
            authenticationData.isAuth = true;
            authenticationData.userName = identityData.userName;
        }
    }

    return {
        registerUser: registerUser,
        loginUser: loginUser,
        logOutUser: logOutUser,
        checkIdentity: checkIdentity,
        userData: authenticationData
    };
});