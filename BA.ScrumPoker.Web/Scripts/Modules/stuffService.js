(function () {
    'use strict';
    angular
        .module('scrumPoker')
        .factory('stuffService', stuffService);

    stuffService.$inject = ['$localStorage'];

    function stuffService($localStorage) {

        var service = {
            getRoom: getRoom,
            setRoom: setRoom,
            clearRoom: clearRoom,

            getClient: getClient,
            setClient: setClient,
            clearClient: clearClient
        };

        return service;

        function getClient() {
            return $localStorage.clientId;
        }

        function setClient(clientId) {
            $localStorage.clientId = clientId;
        }

        function clearClient() {
            delete $localStorage.clientId;
        }

        function getRoom() {
            return $localStorage.roomId;
        }

        function setRoom(roomId) {
            $localStorage.roomId = roomId;
        }

        function clearRoom() {
            delete $localStorage.roomId;
        }
    }
})();
