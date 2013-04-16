angular.module('services.example.com', [
    'ngResource'
]).factory('VersionSvc', function ($resource) {
    var provider = $resource("/help/version");
    return provider;
});
