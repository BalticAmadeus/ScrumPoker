(function () {
    'use strict';
    angular
        .module('scrumPoker')
        .factory('votingService', votingService);

    votingService.$inject = ['$http', 'ClientId'];

    function votingService($http, clientId) {

        var service = {
            getVotingData: getVotingData,

            startVoting: startVoting,
            stopVoting: stopVoting
        }

        return service;

        function startVoting(roomId) {

            return $http.post('./api/StartVoting', { roomId: roomId });
        }

        function stopVoting(roomId) {

            return $http.post('./api/StopVoting', { roomId: roomId });
        }

        function getVotingData() {

            return $http.get('Voting/Get/' + clientId);
        };
    }
})();