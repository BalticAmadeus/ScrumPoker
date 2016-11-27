
ScrumPoker.controller('QrController', ['$scope', 'RoomId', 'BaseUrl', function ($scope, RoomId, BaseUrl) {
    var ctrl = this;

    ctrl.roomUrl = BaseUrl + '?roomId=' + RoomId;

    var showQrCodeState = {
        showQr: true,
        buttonText: "Hide QR Code"
    };
    var hideQrCodeState = {
        showQr: false,
        buttonText: "Show QR Code"
    };

    ctrl.activeState = hideQrCodeState;

    ctrl.changeQrState = function () {
        if (ctrl.activeState.showQr === true) {
            ctrl.activeState = hideQrCodeState;
        } else {
            ctrl.activeState = showQrCodeState;
        }
    }
}]);