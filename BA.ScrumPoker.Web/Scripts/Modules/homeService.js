(function () {
    'use strict';
    angular
        .module('scrumPoker')
        .factory('homeService', homeService);

    homeService.$inject = ['$http'];

    function homeService($http) {

        var service = {
            joinRoom: joinRoom
        }

        return service;

        function joinRoom(model) {
            return $http.post('Home/Join', model);
        }
    }
})();