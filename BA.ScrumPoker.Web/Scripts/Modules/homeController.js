(function () {
    'use strict';
    angular
        .module('scrumPoker')
        .controller('homeController', homeController);

    homeController.$inject = ['$window', 'roomService', 'BaseUrl'];

    function homeController($window, roomService, baseUrl) {

        var ctrl = this;

        ctrl.joinRoomErrorMsg = '';
        ctrl.createRoomErrorMsg = '';

        ctrl.joinRoom = joinRoom;
        ctrl.createRoom = createRoom;

        function joinRoom(model) {

            roomService.joinRoom(model).then(success, error);

            function success(response) {

                var clientId = response.data;
                $window.location.href = baseUrl + 'Voting/Index/' + clientId;
            }

            function error() {
                ctrl.joinRoomErrorMsg = 'room not found';
            }
        }

        function createRoom() {

            roomService.createRoom().then(success, error);

            function success(response) {

                var roomId = response.data;
                $window.location.href = baseUrl + 'Room/Index/' + roomId;
            }

            function error() {
                ctrl.createRoomErrorMsg = 'failed to create room';
            }
        }

    }

})();