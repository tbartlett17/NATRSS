Feature: Joining a Company
	
Scenario: Join Company as an employee
	Given the user is an employee
	And they are not connected with a company
	When the user goes to the facility tab 
	And enters a facility access code
	Then tthey should be connected to that specified company

Scenario: Join Company as a facility manager
	Given the user is an facility manager
	And they are not connected with a company
	When the user goes to the facility tab 
	And enters a facility access code
	Then they should be connected to that specified company

Scenario: Facility Manager is already with a company
	Given the user is an facility manager
	And they are connected with a company
	When the user goes to the facility tab they should not be prompted to join a company
	Then they should be able to create new facilities