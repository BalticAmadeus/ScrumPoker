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
            stopVoting: stopVoting,
            kickClient: kickClient
        }

        return service;

        function getRoomInfo(roomId, secretKey) {

            return $http.get('./api/room/' + roomId + '/' + secretKey);
        }

        function kickClient(roomId, clientId, secretKey) {

            var requestModel = { RoomId: roomId, ClientId: clientId, SecretKey: secretKey };

            return $http.post('./api/room/kick', requestModel);
        }

        function startVoting(roomId, secretKey) {

            return $http.post('./api/room/startVoting', { RoomId: roomId, SecretKey: secretKey });
        }

        function stopVoting(roomId, secretKey) {

            return $http.post('./api/room/stopVoting', { RoomId: roomId, SecretKey: secretKey });
        }
    }

})();