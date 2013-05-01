
Feature: Verify Deployment

@environment.test
Scenario: Verify that deployment to the test environment was successful
	Given I have already retrieved the application version
	When I retrieve the build version
	Then the application version and the build version should match
