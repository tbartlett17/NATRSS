@ViewFormIndexPage
Feature: View Form Index Page

Scenario: A user who has not joined a company views the Form Index page
	Given I am signed in as the seeded user jackio@example.com who has not yet joined a company
	When I navigate to the Forms Index page
	Then I will see a message saying that "It appears you are not a member of a company or you have not yet joined any facilities within your company. Please go to the "My Facilities" page to connect to a company and facility."


Scenario: A user who has joined a company views the Form Index page
	Given I am signed in as the seeded user clark@example.com
	When I navigate to the Forms Index page
	Then I will see a table containing my company's spill forms