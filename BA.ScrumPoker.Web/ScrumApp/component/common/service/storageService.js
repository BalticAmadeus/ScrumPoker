(function () {
    'use strict';
    angular
        .module('scrumPoker')
        .factory('storageService', storageService);

    storageService.$inject = ['$localStorage'];

    function storageService($localStorage) {

        var service = {
            getRoom: getRoom,
            saveRoom: saveRoom,

            saveClient: saveClient,
            getClient: getClient,

            clear: clear
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
    }
})();
