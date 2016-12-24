(function () {
    'use strict';
    angular
        .module('scrumPoker')
        .controller('votingController', votingController);

    votingController.$inject = ['$http', '$filter', '$timeout', 'votingService', 'storageService', 'clientService'];

    function votingController($http, $filter, $timeout, votingService, stuffService, clientService) {

        var ctrl = this;

        ctrl.dirtySetter = false;
        ctrl.dataLoaded = false;

        ctrl.Errors = [];

        ctrl.SetMyVote = setMyVote;

        ctrl.ifServiceCallFailed = ifServiceCallFailed;

        ctrl.UpdateClientRequest = updateClientRequest;

        ctrl.UpdateClient = updateClient;

        ctrl.addError = addError;

        var client = stuffService.getClient();

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

                ctrl.roomId = response.data.RoomId;
                ctrl.votes = response.data.Items;
                ctrl.canVote = response.data.CanVote;
            }

            clientService.getClient(ctrl.clientId).then(success, error);

        }

        function vote(number) {

            function success(response) {
                console.log(response);
                loadVotingInfo();
            }

            var model = { ClientId: ctrl.clientId, Number: number, RoomId: ctrl.roomId };

            clientService.vote(model).then(success, error);
        }

        function addError(message) {
            ctrl.Errors.push(
                {
                    Content: message
                }
            );
            $timeout(function () {
                ctrl.Errors.pop();
            }, 3000);
        }

        //getVotingData();

        function getVotingData() {

            votingService.getVotingData(ctrl.clientId).then(success, error);

            function success(response) {

                ctrl.ViewModel = response.data.Data;

                ctrl.dataLoaded = true;
                updateClient();
            }
        }

        function setMyVote(number) {

            if (!ctrl.ViewModel.CanIVote) {
                return;
            }

            ctrl.ViewModel.Client.VoteValue = number;

            votingService.Vote(ctrl.ViewModel.Client).then(success, error).finally(afterRequest);

            function success(response) {

                if (ifServiceCallFailed(response)) {
                    return;
                }

                ctrl.ViewModel.Client = response.Data;
            }
        }

        function ifServiceCallFailed(data) {

            if (data.Error) {

                if (data.Error.HasError) {

                    addError(data.Error.Message);
                    return true;
                }
            }
            return false;
        }

        function updateClient() {

            setTimeout(function () {

                updateClientRequest();
            }, 2000);
        }

        function updateClientRequest() {

            $http.post('Voting/GetUpdates', { model: ctrl.ViewModel.Client }).then(success, error).finally(afterRequest);

            function success(response) {
                if (ifServiceCallFailed(response)) {
                    updateClient();
                    return;
                }
                ctrl.ViewModel.Client = response.data.Data.Client;
                ctrl.ViewModel.CanIVote = response.data.Data.CanIVote;
                updateClient();
            }
        }

        function error(response) {
            console.log(response);
        }

        function afterRequest() {
            ctrl.showOverlay = false;
        }

        function find(id) {
            for (var i = 0; i < ViewModel.Clients.length; i++) {
                if (ViewModel.Clients[i].Id === id) {

                    return ViewModel.Clients[i];
                }
            }
            return null;
        };
    }

})();
