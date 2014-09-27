'use strict';

var appMain = angular.module('appMain', ['ngRoute', 'LocalStorageModule', 'httpQRequest']);

appMain.constant("appSettings", {
    // Paths
    templatesFolder: "templates",
    apiUrl: "http://localhost:15313",

    // Configurations

    // License Stuff
    author: "Pesho Dev International",
    authorLink: "http://telerikacademy.com",
    poweredBy: "AngularJs"
});

appMain.config(function ($routeProvider, appSettings) {

    $routeProvider.when("/home", {
        controller: "HomeController",
        templateUrl: appSettings.templatesFolder + "/home.html"
    });

    $routeProvider.when("/login", {
        controller: "LoginController",
        templateUrl: appSettings.templatesFolder + "/login.html"
    });

    $routeProvider.when("/signup", {
        controller: "SignUpController",
        templateUrl: appSettings.templatesFolder + "/signup.html"
    });

    $routeProvider.when("/create-game", {
        controller: "CreateGameController",
        templateUrl: appSettings.templatesFolder + "/createGame.html"
    });

    $routeProvider.when("/play/:id", {
        controller: "PlayGameController",
        templateUrl: appSettings.templatesFolder + "/play.html"
    });

    $routeProvider.otherwise({ redirectTo: "/home" });
});

appMain.config(function ($httpProvider) {
    $httpProvider.interceptors.push('interceptorService');
});

appMain.run(function (AccountService) {
    AccountService.checkIdentity();
});