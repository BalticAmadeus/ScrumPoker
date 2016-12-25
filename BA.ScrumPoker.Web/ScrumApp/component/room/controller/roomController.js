(function () {
    'use strict';
    angular
        .module('scrumPoker')
        .controller('roomController', roomController);

    roomController.$inject = ['BaseUrl', 'roomService', 'storageService'];

    function roomController(baseUrl, roomService, storageService) {

        var room = storageService.getRoom();

        var roomId = room.roomId;
        var secretKey = room.secretKey; // todo implement this stuff

        var ctrl = this;

        ctrl.isFlipped = false;
        ctrl.showQr = false;
        ctrl.model = {
            voters: {},
            avgScore: null
        };

        ctrl.roomId = roomId;
        ctrl.roomUrl = baseUrl + '?roomId=' + roomId;

        window.onbeforeunload = onbeforeunload;

        ctrl.showHideQr = showHideQr;
        ctrl.flipMe = flipMe;
        ctrl.kickClient = kickClient;

        function showHideQr() {
            ctrl.showQr = !ctrl.showQr;
        }

        loadRoomInfo(roomId, secretKey);

        function onbeforeunload(event) {

            var message = 'You are about to close voting room and voting will stop. OK?';
            event.returnValue = message;

            return message;
        }

        function loadRoomInfo(roomId, secretKey) {

            setTimeout(function () {

                function success(response) {

                    ctrl.model.avgScore = response.data.AvgScore;
                    ctrl.model.clients = response.data.Clients;

                    loadRoomInfo(roomId, secretKey);
                }

                roomService.getRoomInfo(roomId, secretKey).then(success, error);
            }, 2000);
        }

        function flipMe(varl) {

            ctrl.isFlipped = varl;

            if (varl) {
                stopVoting();
            } else {
                startVoting();
            }
        }

        function kickClient(clientId) {

            roomService.kickClient(roomId, clientId, secretKey).then(success, error);

            function success(response) {
                // todo
            }
        }

        function startVoting() {

            roomService.startVoting(roomId, secretKey).then(success, error);

            function success(response) {
                // todo
            }
        }

        function stopVoting() {

            roomService.stopVoting(roomId, secretKey).then(success, error);

            function success(response) {
                // todo
            }
        }

        function error(response) {
            console.log(response);
        }
    }
})();