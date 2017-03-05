(function () {
    'use strict';
    angular
        .module('scrumPoker')
        .controller('homeController', homeController);

    homeController.$inject = ['$localStorage', '$window', '$cookies', 'BaseUrl', 'RoomId', 'homeService', 'storageService'];

    function homeController($localStorage, $window, $cookies, baseUrl, roomId, homeService, storageService) {

        //homeService.clear(); todo uncoment later

        var ctrl = this;

        ctrl.joinRoomErrorMsg = '';
        ctrl.createRoomErrorMsg = '';

        ctrl.joinRoomModel = {
            username: storageService.getUsername(),
            roomId: roomId
        };

        ctrl.joinRoom = joinRoom;
        ctrl.createRoom = createRoom;

        autoJoinRoom();

        function joinRoom(model) {

            storageService.saveUsername(model.username);

            homeService.joinRoom(model).then(success, error);

            function success(response) {

                var client = response.data;
                storageService.saveClient(client.RoomId, client.ClientId);

                $window.location.href = baseUrl + 'Voting/Index/' + client.ClientId;
            }

            function error(response) {

                if (response.status === 404) {
                    ctrl.joinRoomErrorMsg = 'room not found';
                } else {
                    ctrl.joinRoomErrorMsg = 'Upps... 400 error';
                }

            }
        }


        function createRoom() {

            homeService.createRoom().then(success, error);

            function success(response) {

                storageService.saveRoom(response.data.RoomId, response.data.SecretKey);

                $window.location.href = baseUrl + 'Room/Index/' + response.data.RoomId;
            }

            function error() {
                ctrl.createRoomErrorMsg = 'failed to create room';
            }
        }

        function autoJoinRoom() {

            var username = storageService.getUsername();

            if (username === '' || roomId === '') {
                return;
            }

            joinRoom({ username: username, roomId: roomId });
        }
    }

})();