(function () {
    'use strict';
    angular
        .module('scrumPoker')
        .factory('clientService', clientService);

    clientService.$inject = ['$http'];

    function clientService($http) {

        var service = {
            getClient: getClient,
            vote: vote
        };

        return service;

        function vote(data) {
            return $http.put('./api/client', data);
        }

        function getClient(roomId, clientId) {
            return $http.get('./api/client?roomId=' + roomId + '&clientId=' + clientId);
        }
    }
})();