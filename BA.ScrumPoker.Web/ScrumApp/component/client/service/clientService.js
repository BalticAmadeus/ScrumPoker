﻿(function () {
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
            return $http.post('./api/client/vote', data);
        }

        function getClient(roomId, clientId) {
            console.log(clientId);
            return $http.get('./api/client/' + roomId + '/' + clientId);
        }
    }
})();