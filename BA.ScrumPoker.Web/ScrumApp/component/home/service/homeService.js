(function () {
    'use strict';
    angular
        .module('scrumPoker')
        .factory('homeService', homeService);

    homeService.$inject = ['$http'];

    function homeService($http) {

        var service = {
            joinRoom: joinRoom,
            createRoom: createRoom
        };

        return service;

        function joinRoom(model) {

            var requestModel = {
                RoomId: model.roomId,
                Name: model.username
            };

            return $http.post('./api/client', requestModel);
        }

        function createRoom() {
            return $http.post('./api/room');
        }
    }

})();