(function () {
    'use strict';
    angular
        .module('scrumPoker')
        .factory('storageService', storageService);

    storageService.$inject = ['$localStorage', '$cookies'];

    function storageService($localStorage, $cookies) {

        var service = {
            getRoom: getRoom,
            saveRoom: saveRoom,

            saveClient: saveClient,
            getClient: getClient,

            clear: clear,

            getUsername: getUsername,
            saveUsername: saveUsername
        };

        return service;

        function saveClient(roomId, clientId) {

            $localStorage.clientId = clientId;
            $localStorage.roomId = roomId;
        }

        function getClient() {

            var client = {
                roomId: $localStorage.roomId,
                clientId: $localStorage.clientId
            }

            return client;
        }

        function clear() {
            delete $localStorage.roomId;
            delete $localStorage.clientId;
            delete $localStorage.secretKey;
        }

        function saveRoom(roomId, secretKey) {
            $localStorage.roomId = roomId;
            $localStorage.secretKey = secretKey;
        }

        function getRoom() {

            var room = {
                roomId: $localStorage.roomId,
                secretKey: $localStorage.secretKey
            };

            return room;
        }

        function getUsername() {

            var username = $cookies.get('username');

            if (typeof username !== 'undefined') {
                return username;
            }

            return '';
        }

        function saveUsername(username) {
            $cookies.put('username', username);
        }
    }
})();
