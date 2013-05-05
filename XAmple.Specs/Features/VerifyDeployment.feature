Feature: Verifying a deployment

# TODO
# add that all servers should have the same version and that it should match the current build
# add urls as [1],[2] and split them in code
# do we do all this in the driver?
# nancy routes - prererred ( /auth/version ?? is authorized)
# fix ability to authrize via basic auth

Background: 
	Given I have knowledge of the desired application version

Scenario Outline: Verify that the application has been pushed to the entire farm	
	When I retrieve the current application version from <url>
	Then the desired version should match the application version


@environment.test
Examples: 
	| url                          |
	| http://wfe1.test.example.com |
	| http://wfe2.test.example.com |

