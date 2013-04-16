/// <reference path="typings/angularjs/angular-resource.d.ts" />
/// <reference path="typings/angularjs/angular.d.ts" />

angular.module('services.example.com', ['ngResource'])
    .factory('VersionSvc', function ($resource: ng.resource.IResourceService) {

        var provider = $resource("/help/version");
        return provider;
    });