﻿(function() {
    'use strict';
    angular
        .module('ScrumPoker')
        .controller('RoomController', roomController);

    roomController.$inject = ['$scope', '$http', '$filter', "$timeout", 'roomService'];

    function roomController($scope, $http, $filter, $timeout, roomService) {
        
        $scope.dirtySetter = false;
        $scope.dataLoaded = false;
        $scope.Errors = [];

        $scope.addError = function (message) {
            $scope.Errors.push(
                {
                    Content: message
                }
            );
            $timeout(function () {
                $scope.Errors.pop();
            }, 3000);
        }

        window.onbeforeunload = function (event) {
            var message = 'You are about to close voting room and voting will stop. OK?';
            event.returnValue = message;
            return message;
        }

        getRoomData();
        function getRoomData() {
            roomService.getRoomData()
                .success(function (response) {
                    $scope.ViewModel = response.Data;
                    $scope.dataLoaded = true;
                    $scope.UpdateClients();
                })
                .error(function (error) {
                    $scope.status = 'Unable to load customer data: ' + error.message;
                    console.log($scope.status);
                });
        }

        $scope.StartVoting = function () {
            $http.post('Room/StartVoting', { model: $scope.ViewModel }).success(function (response) {
                $scope.ViewModel = response.Data;

            })
            .error(function (data, status, headers, config) {
                $scope.addError(error);
            }).finally(function () {
                $scope.showOverlay = false;

            });
        }

        $scope.flipMe = function (varl) {

            setTimeout(function () {
                $scope.$apply(function () {
                    $scope.isFlipped = varl;
                });
            }, 400);

            if (varl)
                $scope.StopVoting();
            else
                $scope.StartVoting();
        }

        $scope.StopVoting = function () {
            $http.post('Room/StopVoting', { model: $scope.ViewModel }).success(function (response) {
                $scope.ViewModel = response.Data;


            })
            .error(function (data, status, headers, config) {
                $scope.addError(error);
            }).finally(function () {
                $scope.showOverlay = false;
            });
        }

        $scope.ifServiceCallFailed = function (data) {
            if (data.Error) {
                if (data.Error.HasError) {
                    $scope.addError(data.Error.Message);
                    return true;
                }
            }
            return false;
        }


        $scope.UpdateClients = function () {
            setTimeout(function () {
                $scope.UpdateClientsRequest();
            }, 2000);
        }

        $scope.UpdateClientsRequest = function () {
            $http.post('Room/GetClients', { roomId: $scope.ViewModel.RoomId }).success(function (response) {
                for (var i = 0 ; i < response.Data.length; i++) {
                    var inArray = findOrUpdate(response.Data[i]);
                    if (!inArray)
                        $scope.ViewModel.Clients.push(response.Data[i]);
                }

                $scope.UpdateClients();

            })
            .error(function (data, status, headers, config) {
                $scope.addError(error);
            }).finally(function () {
                $scope.showOverlay = false;
            });
        }

        var findOrUpdate = function (client) {
            for (var i = 0; i < $scope.ViewModel.Clients.length; i++) {
                if ($scope.ViewModel.Clients[i].Id == client.Id) {
                    $scope.ViewModel.Clients[i].VoteValue = client.VoteValue;
                    $scope.ViewModel.Clients[i].IHaveVoted = client.IHaveVoted;
                    return $scope.ViewModel.Clients[i];
                }

            }
            return null;
        };
    }

})();