﻿(function () {
    'use strict';
    angular
        .module('ScrumPoker')
        .controller('roomService', roomService);

    roomService.$inject = ['$http', 'RoomId'];

    function roomService($http, roomId) {

        var service = {
            getRoomData: getRoomData
        }

        return service;

        function getRoomData() {
            return $http.get('Room/Get/' + roomId);
        }
    }

})();