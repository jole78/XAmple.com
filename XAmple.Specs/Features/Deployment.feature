@deployment
Feature: Deployment

Scenario: Verifying that a deployment was successful
	Given the build server's application version
	When collecting the application version from all the servers
	Then they should all be equal
	And the first of them should match the build server version


	


