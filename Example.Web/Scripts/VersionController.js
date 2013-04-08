var Version;
(function (Version) {
    var Controller = (function () {
        function Controller($http) {
            this.$http = $http;
        }
        Controller.prototype.Get = function (successCallBack) {
        };
        return Controller;
    })();
    Version.Controller = Controller;    
})(Version || (Version = {}));
