/// <reference path="typings/angularjs/angular.d.ts" />
/// <reference path="typings/angularjs/angular-resource.d.ts" />

// Module
module Version {

    export class VersionService
    {
        constructor(private resource: ng.resource.IResourceService)
        {
            
        }

        GetCurrentVersion()
        {
            var provider = this.resource("help/version");


        }
    }

    // Controller
    export class Controller
    {       

        // Constructor
        constructor(
            private $scope, versionService: VersionService)
        {
            $scope.Major = 1;
            $scope.Hello = "hello world";
            
        }


    }

}

