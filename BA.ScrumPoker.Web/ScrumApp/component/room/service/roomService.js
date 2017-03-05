(function () {
    'use strict';
    angular
        .module('scrumPoker')
        .factory('roomService', roomService);

    roomService.$inject = ['$http'];

    function roomService($http) {

        return {
            getRoomInfo: getRoomInfo,
            startVoting: startVoting,
            stopVoting: stopVoting,
            kickClient: kickClient
        }

        function getRoomInfo(roomId, secretKey) {

            return $http.get('./api/room/?roomId=' + roomId + '&secretKey=' + secretKey);
        }

        function kickClient(roomId, clientId, secretKey) {

            var requestModel = { RoomId: roomId, ClientId: clientId, SecretKey: secretKey };

            return $http.post('./api/kick', requestModel);
        }

        function startVoting(roomId, secretKey) {

            return $http.put('./api/room', { RoomId: roomId, SecretKey: secretKey, Voting: true });
        }

        function stopVoting(roomId, secretKey) {

            return $http.put('./api/room', { RoomId: roomId, SecretKey: secretKey, Voting: false });
        }
    }

})();