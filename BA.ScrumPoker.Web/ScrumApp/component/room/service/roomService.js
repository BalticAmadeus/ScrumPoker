(function () {
    'use strict';
    angular
        .module('scrumPoker')
        .factory('roomService', roomService);

    roomService.$inject = ['$http'];

    function roomService($http) {

        var service = {
            getRoomInfo: getRoomInfo,
            startVoting: startVoting,
            stopVoting: stopVoting
        }

        return service;

        function getRoomInfo(roomId) {

            return $http.get('./api/room/' + roomId);
        }

        function startVoting(roomId, secretKey) {

            return $http.post('./api/room/startVoting', { RoomId: roomId });
        }

        function stopVoting(roomId, secretKey) {

            return $http.post('./api/room/stopVoting', { RoomId: roomId });
        }
    }

})();