Feature: ContactUs	

Background:
	Given the following users exist
	  | UserName   | Email                 | FirstName | LastName | Password  | 
	  | TaliaK     | knott@example.com     | Talia     | Knott    | Hello123# |
	  | ZaydenC    | clark@example.com     | Zayden    | Clark    | Hello123# |
	  | DavilaH    | hareem@example.com    | Hareem    | Davila   | Hello123# |
	  | KrzysztofP | krzysztof@example.com | Krzysztof | Ponce    | Hello123# |	

@ContactUs
Scenario: getting to the contact us page
	Given the user is a registered user
	And they are on the nav bar
	When they click 'Contact Us'
	Then they should be directed to the Contact Page

Scenario: sending a message to the dev team
	Given the user is on the Contact Page
	And they fill out the subject field
	And they fill out the message field
	When they click the 'Submit' button
	Then our team email should recieve the messsage