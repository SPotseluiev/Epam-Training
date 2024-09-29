Feature: Login
	As a registered user, I want a username/password login on the tracking website,
so that only genuine customers can access their tracking data.

Preconditions: 
	Given valid user credentials are already registered

@mytag
Scenario: Only genuine customers can access their tracking data
	Given I am on the login screen for the site 'https://www.example.com'
	When I enter a valid username 'exampleName' and password 'examplePassword' and submit
	Then I am logged in successfully to the 'https://homepage.com'

	Given I am not logged in with a genuine user
	When I navigate to the home page on the tracking site
	Then I am presented with a login screen for the 'https://www.example.com'