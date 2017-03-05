(function () {
    'use strict';
    angular
        .module('scrumPoker')
        .factory('locationService', locationService);

    locationService.$inject = [];

    function locationService() {

        var service = {
            go: go
        };

        return service;

        function go(url) {
            document.location.href = url;
        }
    }
})();
