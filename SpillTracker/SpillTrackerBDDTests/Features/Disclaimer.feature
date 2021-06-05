Feature: Disclaimer

@Disclaimer
Scenario: getting to the disclaimer page
	Given the user is on our site
	And they go to the footer
	When they click the 'disclaimer' link 
	Then they should be directed to our disclaimer page
