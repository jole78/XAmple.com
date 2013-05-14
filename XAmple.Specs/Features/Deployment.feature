@deployment
Feature: Deployment

Scenario: Making sure that all the servers in the web farm have the same version of the application
	When I have collected the application version from each of the servers in the web farm
	Then they should all be equal

Scenario: Make sure that the deployed version of the application is the correct one
	When I have retrieved the deployed application version
		And I have also retrieved the application version from the build server
	Then they should match


	


