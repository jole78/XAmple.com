Feature: Verify Deployment


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
