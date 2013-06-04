var V;
var Version;
(function (Version) {
    var Controller = (function () {
        function Controller($scope, VersionSvc) {
            this.$scope = $scope;
            this.VersionSvc = VersionSvc;
            $scope.Version = VersionSvc.get();
        }
        return Controller;
    })();
    Version.Controller = Controller;    
})(Version || (Version = {}));
