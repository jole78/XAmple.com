angular.module('XAmple.services', [
    'ngResource'
]).factory('VersionSvc', function ($resource) {
    var provider = $resource("/help/version");
    return provider;
});
