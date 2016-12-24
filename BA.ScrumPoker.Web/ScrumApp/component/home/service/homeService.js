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

            return $http.put('./api/home/joinRoom', { RoomId: model.roomId, Username: model.username });
        }

        function createRoom() {
            return $http.post('./api/home/createRoom');
        }
    }

})();