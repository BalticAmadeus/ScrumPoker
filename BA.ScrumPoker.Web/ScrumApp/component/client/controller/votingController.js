(function () {
    'use strict';
    angular
        .module('scrumPoker')
        .controller('votingController', votingController);

    votingController.$inject = ['storageService', 'clientService'];

    function votingController(storageService, clientService) {

        var client = storageService.getClient();

        var ctrl = this;

        ctrl.clientId = client.clientId;
        ctrl.roomId = client.roomId;

        ctrl.votes = {};
        ctrl.canVote = null;

        ctrl.vote = vote;

        loopLoadVotingInfo();

        function loopLoadVotingInfo() {

            setTimeout(function () {

                loadVotingInfo();

                loopLoadVotingInfo();
            }, 2000);
        }

        function loadVotingInfo() {

            function success(response) {

                ctrl.votes = response.data.Items;
                ctrl.canVote = response.data.CanVote;
            }

            clientService.getClient(ctrl.roomId, ctrl.clientId).then(success, error);

        }

        function vote(number) {

            var model = { ClientId: ctrl.clientId, VoteValue: number, RoomId: ctrl.roomId };

            clientService.vote(model).then(success, error);

            function success(response) {
                loadVotingInfo();
            }
        }

        function error(response) {
            console.log(response);
        }
    }

})();
