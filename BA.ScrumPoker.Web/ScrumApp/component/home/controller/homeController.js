(function () {
    'use strict';
    angular
        .module('scrumPoker')
        .controller('homeController', homeController);

    homeController.$inject = ['$localStorage', '$window', 'BaseUrl', 'homeService', 'storageService'];

    function homeController($localStorage, $window, baseUrl, homeService, storageService) {

        //homeService.clear(); todo uncoment later

        var ctrl = this;

        ctrl.joinRoomErrorMsg = '';
        ctrl.createRoomErrorMsg = '';

        ctrl.joinRoom = joinRoom;
        ctrl.createRoom = createRoom;

        function joinRoom(model) {

            homeService.joinRoom(model).then(success, error);

            function success(response) {

                var client = response.data;

                storageService.saveClient(client.RoomId, client.ClientId);

                $window.location.href = baseUrl + 'Voting/Index/' + client.ClientId;
            }

            function error() {
                ctrl.joinRoomErrorMsg = 'room not found';
            }
        }

        function createRoom() {

            homeService.createRoom().then(success, error);

            function success(response) {

                var room = response.data;

                storageService.saveRoom(room.RoomId, room.SecretKey);

                $window.location.href = baseUrl + 'Room/Index/' + room.RoomId;
            }

            function error() {
                ctrl.createRoomErrorMsg = 'failed to create room';
            }
        }

    }

})();