(function () {
    'use strict';
    angular
        .module('scrumPoker')
        .controller('roomController', roomController);

    roomController.$inject = ['$scope', '$timeout', 'BaseUrl', 'roomService', 'storageService'];

    function roomController($scope, $timeout, baseUrl, roomService, storageService) {

        var roomId = storageService.getRoom();
        var secretKey = '';
        var ctrl = this;

        ctrl.isFlipped = false;
        ctrl.showQr = false;
        ctrl.Votes = {};

        ctrl.showHideQr = showHideQr;
        ctrl.flipMe = flipMe;

        ctrl.roomId = roomId;
        ctrl.roomUrl = baseUrl + '?roomId=' + roomId;

        window.onbeforeunload = onbeforeunload;

        function showHideQr() {
            ctrl.showQr = !ctrl.showQr;
        }

        loadRoomInfo(roomId);

        function onbeforeunload(event) {

            var message = 'You are about to close voting room and voting will stop. OK?';
            event.returnValue = message;

            return message;
        }

        function loadRoomInfo(roomId) {

            setTimeout(function () {

                roomService.getRoomInfo(roomId).then(success, error).finally(afterRequest);

                function success(response) {

                    ctrl.Votes = response.data.Votes;

                    loadRoomInfo(roomId);
                }
            }, 2000);
        }

        function flipMe(varl) { // todo refactor

            setTimeout(function () {
                $scope.$apply(function () {
                    ctrl.isFlipped = varl;
                });
            }, 400);

            if (varl) {
                stopVoting();
            } else {
                startVoting();
            }
        }

        function startVoting() {

            roomService.startVoting(roomId, secretKey).then(success, error);

            function success(response) {
            }
        }

        function stopVoting() {

            roomService.stopVoting(roomId, secretKey).then(success, error);

            function success(response) {
            }
        }

        function error(response) {
            console.log(response);
        }

        function afterRequest() {
            console.log('after response');
        }

        // beoyind this delete code

        function getRoomData(roomId) {

            roomService.getRoom(roomId).then(success, error);

            function success(response) {
                console.log(response);
                ctrl.ViewModel = response.data;
                updateClients();
            }
        }

        function updateClients() {

            setTimeout(function () {
                updateClientsRequest();
            }, 2000);
        }

        function findOrUpdate(client) {

            console.log(client);

            for (var i = 0; i < ctrl.ViewModel.Clients.length; i++) {
                if (ctrl.ViewModel.Clients[i].Id === client.Id) {
                    ctrl.ViewModel.Clients[i].VoteValue = client.VoteValue;
                    ctrl.ViewModel.Clients[i].IHaveVoted = client.IHaveVoted;
                    console.log(ctrl.ViewModel);
                    return ctrl.ViewModel.Clients[i];
                }
            }
            console.log(ctrl.ViewModel);
            return null;
        };

        function updateClientsRequest() {

            var roomId = ctrl.ViewModel.RoomId;

            roomService.getClients(roomId).then(success, error).finally(afterRequest);

            function success(response) {

                console.log('updateClientsRequest func');
                console.log(response);

                for (var i = 0 ; i < response.data.Data.length; i++) {
                    var inArray = findOrUpdate(response.data.Data[i]);
                    if (!inArray) {
                        ctrl.ViewModel.Clients.push(response.data.Data[i]);
                    }
                }

                updateClients();
            }
        }

    }
})();