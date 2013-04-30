@AfterDeployment
Feature: Verify Deployment



Scenario: Verify that deployment was successful
	Given I have retrieved the application version
	When I retrieve the build version
	Then the "application" version and the "build" version should match
