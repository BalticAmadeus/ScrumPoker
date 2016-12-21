(function() {
    'use strict';
    angular
        .module('scrumPoker')
        .controller('homeController', homeController);

    homeController.$inject = ['$location', 'homeService'];

    function homeController($location, homeService) {

        var ctrl = this;

        ctrl.joinRoom = joinRoom;
        ctrl.createRoom = createRoom;

        function joinRoom(model) {

            homeService.joinRoom(model).then(success, error);

            function success(response) {
                console.log(response);
            }

            function error(response) {
                console.log(response);
            }
        }

        function createRoom() {
            
        }

    }

})();