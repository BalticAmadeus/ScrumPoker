(function () {
    'use strict';
    angular
        .module('scrumPoker')
        .factory('votingService', votingService);

    votingService.$inject = ['$http', 'ClientId'];

    function votingService($http, clientId) {

        var service = {
            getVotingData: getVotingData
        }

        return service;

        function getVotingData() {
            return $http.get('Voting/Get/' + clientId);
        };
    }
})();