(function () {
    'use strict';
    angular
        .module('ScrumPoker')
        .controller('VotingController', votingController);

    votingController.$inject = ['$scope', '$http', '$filter', "$timeout", 'votingService'];

    function votingController($scope, $http, $filter, $timeout, votingService) {

        $scope.dirtySetter = false;
        $scope.dataLoaded = false;

        $scope.Errors = [];

        $scope.SetMyVote = setMyVote;
        $scope.ifServiceCallFailed = ifServiceCallFailed;
        $scope.UpdateClientRequest = updateClientRequest;
        $scope.UpdateClient = updateClient;
        $scope.addError = addError;

        function addError(message) {
            $scope.Errors.push(
                {
                    Content: message
                }
            );
            $timeout(function () {
                $scope.Errors.pop();
            }, 3000);
        }

        getVotingData();

        function getVotingData() {

            votingService.getVotingData()
                .success(function (response) {

                    $scope.ViewModel = response.Data;
                    $scope.dataLoaded = true;
                    updateClient();
                })
                .error(function (error) {

                    $scope.status = 'Unable to load customer data: ' + error.message;
                    console.log($scope.status);
                });
        }

        function setMyVote(number) {

            if (!$scope.ViewModel.CanIVote) {
                return;
            }

            $scope.ViewModel.Client.VoteValue = number;

            $http.post('Voting/Vote', { model: $scope.ViewModel.Client }).success(function (response) {

                if (ifServiceCallFailed(response)) {
                    return;
                }

                $scope.ViewModel.Client = response.Data;
            })
            .error(function (data, status, headers, config) {

                addError(error);
            }).finally(function () {

                $scope.showOverlay = false;
            });

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

            $http.post('Voting/GetUpdates', { model: $scope.ViewModel.Client }).success(function (response) {

                if (ifServiceCallFailed(response)) {
                    updateClient();
                    return;
                }

                $scope.ViewModel.Client = response.Data.Client
                $scope.ViewModel.CanIVote = response.Data.CanIVote;
                UpdateClient();

            })
            .error(function (data, status, headers, config) {

                addError(error);
            }).finally(function () {

                $scope.showOverlay = false;
            });
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
