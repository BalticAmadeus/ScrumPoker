

ScrumPoker.controller('VotingController', ['$scope', '$http', '$filter', "$timeout", 'VotingService', function ($scope, $http, $filter, $timeout, VotingService) {
	$scope.dirtySetter = false;
	$scope.dataLoaded = false;


	$scope.Errors = [];
	$scope.addError = function (message) {
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
		VotingService.getVotingData()
            .success(function (response) {
            	$scope.ViewModel = response.Data;
            	$scope.dataLoaded = true;
            	$scope.UpdateClient();
            })
            .error(function (error) {
            	$scope.status = 'Unable to load customer data: ' + error.message;
            	console.log($scope.status);
            });
	}



	$scope.SetMyVote = function(number)
	{
		if (!$scope.ViewModel.CanIVote)
			return;

		$scope.ViewModel.Client.VoteValue = number;

		$http.post('Voting/Vote', { model: $scope.ViewModel.Client }).success(function (response) {
			if ($scope.ifServiceCallFailed(response))
				return;

			$scope.ViewModel.Client = response.Data;
		})
		.error(function (data, status, headers, config) {
			$scope.addError(error);
		}).finally(function () {
			$scope.showOverlay = false;
		});

	}

	$scope.ifServiceCallFailed = function (data) {
		if (data.Error) {
			if (data.Error.HasError) {
				$scope.addError(data.Error.Message);
				return true;
			}
		}
		return false;
	}

	$scope.UpdateClient = function () {
		setTimeout(function () {
			$scope.UpdateClientRequest();
		}, 2000);
	}

	$scope.UpdateClientRequest = function(){
		$http.post('Voting/GetUpdates', { model: $scope.ViewModel.Client }).success(function (response) {
			if ($scope.ifServiceCallFailed(response))
			{
				$scope.UpdateClient();
				return;
			}

			$scope.ViewModel.Client = response.Data.Client
			$scope.ViewModel.CanIVote = response.Data.CanIVote;
			$scope.UpdateClient();
			
		})
		.error(function (data, status, headers, config) {
			$scope.addError(error);
		}).finally(function () {
			$scope.showOverlay = false;
		});
	}

	var find = function (id) {
		for (var i = 0; i < ViewModel.Clients.length; i++) {
			if (ViewModel.Clients[i].Id == id) return ViewModel.Clients[i];
		}
		return null;
	};

}]);



ScrumPoker.factory('VotingService', ['$http', 'ClientId', function ($http, clientId) {
	var VotingService = {};
	VotingService.getVotingData = function () {
		return $http.get('Voting/Get/' + clientId);
	};
	return VotingService;

}]);

