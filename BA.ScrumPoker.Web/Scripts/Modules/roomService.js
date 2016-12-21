(function () {
    'use strict';
    angular
        .module('ScrumPoker')
        .factory('roomService', roomService);

    roomService.$inject = ['$http', 'RoomId'];

    function roomService($http, roomId) {

        var service = {
            getRoomData: getRoomData,
            getClients: getClients,
            stopVoting: stopVoting,
            startVoting: startVoting
        }

        return service;

        function startVoting(model) {
            return $http.post('Room/StartVoting', { model: model });
        }

        function stopVoting(model) {

            return $http.post('Room/StopVoting', { model: model });
        }

        function getClients(roomId) {

            return $http.post('Room/GetClients', { roomId: roomId });

        }

        function getRoomData() {

            return $http.get('Room/Get/' + roomId);
        }
    }

})();