(function() {
    'use strict';
    angular
        .module('ScrumPoker')
        .controller('QrController', qrController);

    qrController.$inject = ['RoomId', 'BaseUrl'];

    function qrController(roomId, baseUrl) {

        var ctrl = this;

        ctrl.changeQrState = changeQrState;

        ctrl.roomUrl = baseUrl + '?roomId=' + roomId;

        var showQrCodeState = {
            showQr: true,
            buttonText: "Hide QR Code"
        };
        var hideQrCodeState = {
            showQr: false,
            buttonText: "Show QR Code"
        };

        ctrl.activeState = hideQrCodeState;

        function changeQrState() {
            if (ctrl.activeState.showQr === true) {
                ctrl.activeState = hideQrCodeState;
            } else {
                ctrl.activeState = showQrCodeState;
            }
        }
    }
})();