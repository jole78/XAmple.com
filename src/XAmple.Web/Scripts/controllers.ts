/// <reference path="typings/angularjs/angular.d.ts" />
/// <reference path="typings/angularjs/angular-resource.d.ts" />

var V;
module Version
{
    export class Controller
    {       

        constructor(
            private $scope,
            private VersionSvc: ng.resource.IResourceClass)
        {
            $scope.Version = VersionSvc.get();
        }
    }
}



