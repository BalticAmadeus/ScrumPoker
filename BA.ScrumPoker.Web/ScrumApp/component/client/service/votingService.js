(function () {
    'use strict';
    angular
        .module('scrumPoker')
        .factory('votingService', votingService);

    votingService.$inject = ['$http'];

    function votingService($http) {

        var service = {
            getVotingData: getVotingData,
            Vote: vote
        }

        return service;

        function vote(data) {

            return $http.post('./api/Vote', data);
        }

        function getVotingData(clientId) {

            return $http.get('Voting/Get/' + clientId);
        };
    }
})();