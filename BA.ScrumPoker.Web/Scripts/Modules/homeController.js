﻿(function () {
    'use strict';
    angular
        .module('scrumPoker')
        .controller('homeController', homeController);

    homeController.$inject = ['$localStorage', '$window', 'BaseUrl', 'homeService', 'storageService'];

    function homeController($localStorage, $window, baseUrl, homeService, storageService) {

        //storageService.clearRoom(); todo uncoment later

        var ctrl = this;

        ctrl.joinRoomErrorMsg = '';
        ctrl.createRoomErrorMsg = '';

        ctrl.joinRoom = joinRoom;
        ctrl.createRoom = createRoom;

        function joinRoom(model) {

            homeService.joinRoom(model).then(success, error);

            function success(response) {

                var clientId = response.data;

                storageService.setClient(clientId);

                $window.location.href = baseUrl + 'Voting/Index/' + clientId;
            }

            function error() {
                ctrl.joinRoomErrorMsg = 'room not found';
            }
        }

        function createRoom() {

            homeService.createRoom().then(success, error);

            function success(response) {

                var roomId = response.data;

                storageService.setRoom(roomId);

                $window.location.href = baseUrl + 'Room/Index/' + roomId;
            }

            function error() {
                ctrl.createRoomErrorMsg = 'failed to create room';
            }
        }

    }

})();