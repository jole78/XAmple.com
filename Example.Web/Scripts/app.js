var Version;
(function (Version) {
    var VersionService = (function () {
        function VersionService(resource) {
            this.resource = resource;
        }
        VersionService.prototype.GetCurrentVersion = function () {
            var provider = this.resource("help/version");
        };
        return VersionService;
    })();
    Version.VersionService = VersionService;    
    var Controller = (function () {
        function Controller($scope, versionService) {
            this.$scope = $scope;
            $scope.Major = 1;
            $scope.Hello = "hello world";
        }
        return Controller;
    })();
    Version.Controller = Controller;    
})(Version || (Version = {}));
