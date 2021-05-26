@JoiningFacility
Feature: Joining a Facility

Background:
	Given the following users exist
	  | UserName   | Email                 | FirstName | LastName | Password  | Company | 
	  | TaliaK     | knott@example.com     | Talia     | Knott    | Hello123# | test1   |
	  | ZaydenC    | clark@example.com     | Zayden    | Clark    | Hello123# | test1   |
	  | DavilaH    | hareem@example.com    | Hareem    | Davila   | Hello123# | test2   |
	  | KrzysztofP | krzysztof@example.com | Krzysztof | Ponce    | Hello123# | test3   |
	And the following facilities exist
	  | Facility Name | AccessCode |
	  | Fac1         | 123456aa    |
	  | Fac2         | 321456bb    |

	
Scenario: Join Facility as an employee
	##Given the user is an employee
	##And they are connected with a company
	When the user goes to the facility tab 
	And enters a facility access code correctly
	Then they should be able to view that facility

Scenario: Join Company as a facility manager
	Given the user is an facility manager
	And they are connected with a company
	When the user goes to the facility tab 
	And enters a facility access code correctly
	Then they should be able to view that facility

