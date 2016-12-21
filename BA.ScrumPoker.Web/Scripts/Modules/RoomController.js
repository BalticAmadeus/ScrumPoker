(function() {
    'use strict';
    angular
        .module('ScrumPoker')
        .controller('RoomController', roomController);

    roomController.$inject = ['$http', '$filter', "$timeout", 'roomService'];

    function roomController($http, $filter, $timeout, roomService) {

        var ctrl = this;

        ctrl.dirtySetter = false;
        ctrl.dataLoaded = false;
        ctrl.Errors = [];

        ctrl.addError = function (message) {
            ctrl.Errors.push(
                {
                    Content: message
                }
            );
            $timeout(function () {
                ctrl.Errors.pop();
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
                    ctrl.ViewModel = response.Data;
                    ctrl.dataLoaded = true;
                    ctrl.UpdateClients();
                })
                .error(function (error) {
                    ctrl.status = 'Unable to load customer data: ' + error.message;
                    console.log(ctrl.status);
                });
        }

        ctrl.StartVoting = function () {
            $http.post('Room/StartVoting', { model: ctrl.ViewModel }).success(function (response) {
                ctrl.ViewModel = response.Data;

            })
            .error(function (data, status, headers, config) {
                ctrl.addError(error);
            }).finally(function () {
                ctrl.showOverlay = false;

            });
        }

        ctrl.flipMe = function (varl) {

            setTimeout(function () {
                ctrl.$apply(function () {
                    ctrl.isFlipped = varl;
                });
            }, 400);

            if (varl)
                ctrl.StopVoting();
            else
                ctrl.StartVoting();
        }

        ctrl.StopVoting = function () {
            ctrl.post('Room/StopVoting', { model: ctrl.ViewModel }).success(function (response) {
                ctrl.ViewModel = response.Data;


            })
            .error(function (data, status, headers, config) {
                ctrl.addError(error);
            }).finally(function () {
                ctrl.showOverlay = false;
            });
        }

        ctrl.ifServiceCallFailed = function (data) {
            if (data.Error) {
                if (data.Error.HasError) {
                    ctrl.addError(data.Error.Message);
                    return true;
                }
            }
            return false;
        }


        ctrl.UpdateClients = function () {
            setTimeout(function () {
                ctrl.UpdateClientsRequest();
            }, 2000);
        }

        var findOrUpdate = function (client) {
            for (var i = 0; i < ctrl.ViewModel.Clients.length; i++) {
                if (ctrl.ViewModel.Clients[i].Id == client.Id) {
                    ctrl.ViewModel.Clients[i].VoteValue = client.VoteValue;
                    ctrl.ViewModel.Clients[i].IHaveVoted = client.IHaveVoted;
                    return ctrl.ViewModel.Clients[i];
                }

            }
            return null;
        };

        ctrl.UpdateClientsRequest = function () {
            $http.post('Room/GetClients', { roomId: ctrl.ViewModel.RoomId }).success(function (response) {
                for (var i = 0 ; i < response.Data.length; i++) {
                    var inArray = findOrUpdate(response.Data[i]);
                    if (!inArray)
                        ctrl.ViewModel.Clients.push(response.Data[i]);
                }

                ctrl.UpdateClients();

            })
            .error(function (data, status, headers, config) {
                ctrl.addError(error);
            }).finally(function () {
                ctrl.showOverlay = false;
            });
        }

    }

})();