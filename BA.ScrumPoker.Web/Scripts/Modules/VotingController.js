(function () {
    'use strict';
    angular
        .module('ScrumPoker')
        .controller('VotingController', votingController);

    votingController.$inject = ['$http', '$filter', '$timeout', 'votingService'];

    function votingController($http, $filter, $timeout, votingService) {

        var ctrl = this;

        ctrl.dirtySetter = false;
        ctrl.dataLoaded = false;

        ctrl.Errors = [];

        ctrl.SetMyVote = setMyVote;
        ctrl.ifServiceCallFailed = ifServiceCallFailed;
        ctrl.UpdateClientRequest = updateClientRequest;
        ctrl.UpdateClient = updateClient;
        ctrl.addError = addError;

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

        getVotingData();

        function getVotingData() {

            votingService.getVotingData().then(success, error);

            function success(response) {

                ctrl.ViewModel = response.Data;
                ctrl.dataLoaded = true;
                updateClient();
            }
        }

        function setMyVote(number) {

            if (!ctrl.ViewModel.CanIVote) {
                return;
            }

            ctrl.ViewModel.Client.VoteValue = number;

            function success(response) {

                if (ifServiceCallFailed(response)) {
                    return;
                }

                ctrl.ViewModel.Client = response.Data;
            }

            $http.post('Voting/Vote', { model: ctrl.ViewModel.Client }).then(success, error).finally(afterRequest);

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

                ctrl.ViewModel.Client = response.Data.Client;
                ctrl.ViewModel.CanIVote = response.Data.CanIVote;
                UpdateClient();
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
