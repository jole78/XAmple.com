@deployment
Feature: Deployment

Scenario: Making sure that all the servers in the web farm have the same version of the application
	When I have collected the application version from each of the servers in the web farm
	Then they should all be equal


	


