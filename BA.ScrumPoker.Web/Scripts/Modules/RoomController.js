(function () {
    'use strict';
    angular
        .module('ScrumPoker')
        .controller('RoomController', roomController);

    roomController.$inject = ["$timeout", 'roomService'];

    function roomController($timeout, roomService) {

        var ctrl = this;

        ctrl.dirtySetter = false;
        ctrl.dataLoaded = false;
        ctrl.isFlipped = false;
        ctrl.showOverlay = false;

        ctrl.flipMe = flipMe;

        window.onbeforeunload = onbeforeunload;

        getRoomData();

        function onbeforeunload(event) {

            var message = 'You are about to close voting room and voting will stop. OK?';
            event.returnValue = message;

            return message;
        }


        function getRoomData() {
            roomService.getRoomData().then(success, error);

            function success(response) {
                console.log(response);

                ctrl.ViewModel = response.data.Data;
                ctrl.dataLoaded = true;
                updateClients();
            }
        }

        function startVoting() {

            roomService.startVoting(ctrl.ViewModel).then(success, error).finally(afterRequest);

            function success(response) {
                ctrl.ViewModel = response.data.Data;
            }
        }

        function flipMe(varl) {

            setTimeout(function () {
                $scope.$apply(function () {
                    ctrl.isFlipped = varl;
                });
            }, 400);

            if (varl) {
                stopVoting();
            } else {
                startVoting();
            }
        }

        function stopVoting() {

            roomService.stopVoting(ctrl.ViewModel).then(success, error).finally(afterRequest);

            function success(response) {
                ctrl.ViewModel = response.data.Data;
            }
        }

        function updateClients() {

            setTimeout(function () {
                updateClientsRequest();
            }, 2000);
        }

        function findOrUpdate(client) {

            for (var i = 0; i < ctrl.ViewModel.Clients.length; i++) {
                if (ctrl.ViewModel.Clients[i].Id === client.Id) {
                    ctrl.ViewModel.Clients[i].VoteValue = client.VoteValue;
                    ctrl.ViewModel.Clients[i].IHaveVoted = client.IHaveVoted;
                    return ctrl.ViewModel.Clients[i];
                }
            }

            return null;
        };

        function updateClientsRequest() {

            var roomId = ctrl.ViewModel.RoomId;

            roomService.getClients(roomId).then(success, error).finally(afterRequest);

            function success(response) {

                for (var i = 0 ; i < response.data.Data.length; i++) {
                    var inArray = findOrUpdate(response.data.Data[i]);
                    if (!inArray) {
                        ctrl.ViewModel.Clients.push(response.data.Data[i]);
                    }
                }

                updateClients();
            }
        }

        function error(response) {
            console.log(response);
        }

        function afterRequest() {
            ctrl.showOverlay = false;
        }
    }
})();