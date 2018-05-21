﻿'use strict';

var app = angular.module("rankingOfGamesApp", []);

app.controller("rankingOfGamesCtrl", function ($scope, $http) {

    $scope.name = "";
    $scope.msgErro = "";
    $scope.RankingOfGamesOfPlayers = [];

    $scope.sendCommand = function (name) {

        if (!name) {
            name = "";
        }

        $scope.msgErro = "";
        
        $http.post("http://localhost:52661/api/FindRankingOfGamesOfPlayersBy", { name }).then(

            function (success) {

                if (success) {
                    $scope.RankingOfGamesOfPlayers = success.data;
                }

            }, function (error) {

                if (error.data.errors)
                    $scope.msgErro = error.data.errors;

            }
        );

    };

});
