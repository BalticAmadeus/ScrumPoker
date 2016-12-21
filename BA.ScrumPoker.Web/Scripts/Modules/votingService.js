(function () {
    'use strict';
    angular
        .module('ScrumPoker')
        .controller('votingService', votingService);

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