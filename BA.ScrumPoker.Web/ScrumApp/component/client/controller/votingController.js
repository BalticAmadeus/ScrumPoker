(function () {
    'use strict';
    angular
        .module('scrumPoker')
        .controller('votingController', votingController);

    votingController.$inject = ['storageService', 'clientService', 'locationService', 'ClientId'];

    function votingController(storageService, clientService, locationService, clientId) {

        var client = storageService.getClient();

        var ctrl = this;
        var isKicked = false;

        ctrl.clientId = clientId; // In case of user will be in multiple rooms in same browser
        ctrl.roomId = client.roomId;

        ctrl.votes = {};
        ctrl.canVote = null;

        ctrl.vote = vote;

        loadVotingInfo();

        loopLoadVotingInfo();

        function loopLoadVotingInfo() {

            setTimeout(function () {

                if (isKicked) {
                    return;
                }

                loadVotingInfo();

                loopLoadVotingInfo();
            }, 2000);
        }

        function loadVotingInfo() {

            function success(response) {

                ctrl.votes = response.data.VoteOptions;
                ctrl.canVote = response.data.CanVote;
            }

            function error() {

                alert('Dude... you was kicked or something...');

                isKicked = true;

                locationService.go('/');
            }

            clientService.getClient(ctrl.roomId, ctrl.clientId).then(success, error);

        }

        function vote(number) {

            if (ctrl.canVote === false) {
                return;
            }

            var model = { ClientId: ctrl.clientId, VoteValue: number, RoomId: ctrl.roomId };

            clientService.vote(model).then(success, error);

            function success(response) {
                loadVotingInfo();
            }
        }

        function error(response) {
            // todo
        }
    }

})();
