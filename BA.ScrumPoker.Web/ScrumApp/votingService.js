(function () {
    'use strict';
    angular
        .module('scrumPoker')
        .factory('votingService', votingService);

    votingService.$inject = ['$http'];

    function votingService($http) {

        var service = {
            getVotingData: getVotingData,

            startVoting: startVoting,
            stopVoting: stopVoting,
            Vote: vote
        }

        return service;

        function startVoting(roomId) {

            return $http.post('./api/StartVoting', { roomId: roomId });
        }

        function stopVoting(roomId) {

            return $http.post('./api/StopVoting', { roomId: roomId });
        }

        function vote(data) {

            return $http.post('./api/Vote', data);
        }

        function getVotingData(clientId) {

            return $http.get('Voting/Get/' + clientId);
        };
    }
})();