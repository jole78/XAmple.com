@Deployment
Feature: Verify Deployment

@environment.test
Background: Using the test environment
	Given it is the test environment

Scenario: Verify that deployment was successful
	Given I have already retrieved the application version
	When I retrieve the build version
	Then the application version and the build version should match
