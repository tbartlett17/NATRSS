Feature: VersionHistory

@VersionHistory
Scenario: A user getting to the version history site
	Given the user is on our site
	And they go to the footer
	When they click onto 'version history'
	Then they should see our version history page

Scenario: A user getting to the commit id on github
	Given the user is on the version history page
	When they click on a version # tag 
	Then they should be taken to github

Scenario: A user should not see unecessary commit message containing Merge
	Given the user is on the version history page
	When they see the 'Message Details' text
	Then they should not be able to see a message containing 'Merge'