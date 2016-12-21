(function() {
    'use strict';
    angular
        .module('ScrumPoker')
        .controller('QrController', qrController);

    qrController.$inject = ['RoomId', 'BaseUrl'];

    function qrController(roomId, baseUrl) {

        var ctrl = this;

        ctrl.showQr = false;

        ctrl.changeQrState = changeQrState;

        ctrl.roomUrl = baseUrl + '?roomId=' + roomId;

        function changeQrState() {
            if (ctrl.showQr === true) {
                ctrl.showQr = false;
            } else {
                ctrl.showQr = true;
            }
        }
    }
})();