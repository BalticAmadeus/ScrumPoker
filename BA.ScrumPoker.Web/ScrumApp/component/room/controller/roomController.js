(function () {
    'use strict';
    angular
        .module('scrumPoker')
        .controller('roomController', roomController);

    roomController.$inject = ['BaseUrl', 'roomService', 'storageService'];

    function roomController(baseUrl, roomService, storageService) {

        var room = storageService.getRoom();

        var roomId = room.roomId;
        var secretKey = room.secretKey;

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

        loadRoomInfo();
        loadRoomInfoLoop();

        function onbeforeunload(event) {

            var message = 'You are about to close voting room and voting will stop. OK?';
            event.returnValue = message;

            return message;
        }

        function loadRoomInfoLoop() {

            setTimeout(function () {

                loadRoomInfo();

                loadRoomInfoLoop();

            }, 2000);
        }

        function loadRoomInfo() {

            function success(response) {

                ctrl.model.avgScore = response.data.AvgScore;
                ctrl.model.clients = response.data.Clients;
            }

            roomService.getRoomInfo(roomId, secretKey).then(success);
        }

        function flipMe(varl) {

            ctrl.isFlipped = varl;

            if (varl) {
                stopVoting();
            } else {
                startVoting();
            }

            loadRoomInfo();
        }

        function kickClient(clientId) {

            roomService.kickClient(roomId, clientId, secretKey).then(commonSuccess, commonError);
        }

        function startVoting() {

            roomService.startVoting(roomId, secretKey).then(commonSuccess, commonError);
        }

        function stopVoting() {

            roomService.stopVoting(roomId, secretKey).then(commonSuccess, commonError);
        }

        function commonSuccess() {
            loadRoomInfo();
        }

        function commonError() {
            loadRoomInfo();
        }
    }
})();