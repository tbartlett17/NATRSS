@JoiningCompany
Feature: Joining a Company
	
Background:
	Given the following users exist
	  | UserName   | Email                 | FirstName | LastName | Password  |  
	  | TaliaK     | knott@example.com     | Talia     | Knott    | Hello123# | 
	  | ZaydenC    | clark@example.com     | Zayden    | Clark    | Hello123# | 
	  | DavilaH    | hareem@example.com    | Hareem    | Davila   | Hello123# | 
	  | KrzysztofP | krzysztof@example.com | Krzysztof | Ponce    | Hello123# |
	And the following Companies exist
	  | Company Name | AccessCode |
	  | comp1        | 123AA456    |
	  | comp2        | 123BB456    |

Scenario: Join Company as an employee
	Given the user is an employee
	And they are not connected with a company
	When the user goes to the facility tab 
	And enters a company access code
	Then they should be connected to that specified company

Scenario: Join Company as a facility manager
	Given the user is an facility manager
	And they are not connected with a company
	When the user goes to the facility tab 
	And enters a company access code
	Then they should be connected to that specified company

Scenario: Facility Manager is already with a company
	Given the user is an facility manager
	And they are connected with a company
	When the user goes to the facility tab they should not be prompted to join a company
	Then they should be able to create new facilities