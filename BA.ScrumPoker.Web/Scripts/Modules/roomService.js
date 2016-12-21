﻿(function () {
    'use strict';
    angular
        .module('scrumPoker')
        .factory('roomService', roomService);

    roomService.$inject = ['$http', 'RoomId'];

    function roomService($http, roomId) {

        var service = {
            getClients: getClients,
            stopVoting: stopVoting,
            startVoting: startVoting,

            joinRoom: joinRoom,
            createRoom: createRoom,
            getRoom: getRoom
        }

        return service;

        function joinRoom(model) {

            return $http.put('./api/room', model);
        }

        function createRoom() {

            return $http.post('./api/room');
        }

        function getRoom() {

            return $http.get('./api/room/' + roomId);
        }

        function startVoting(model) {

            return $http.post('Room/StartVoting', { model: model });
        }

        function stopVoting(model) {

            return $http.post('Room/StopVoting', { model: model });
        }

        function getClients(roomId) {

            return $http.post('Room/GetClients', { roomId: roomId });

        }
    }

})();