# As a user I would like to see my account information.

## ID: 6
## Effort Points: 2
## Owner: Tyler Bartlett
## Feature branch name: f_AcctPage

## Assumptions/Preconditions
That the Login and Register pages and the controller for that already exist and work 

## Description
The stakeholders want the page to the login page to redirect to an account view that shows the user profile. They want the account page to display additional information depending on if the user is an expedition provider or employee. For expedition providers the stakeholders want the page to display a table of all their entered expeditions and include a link for each one that directs the user to a form to request a modification of that entry. For employee accounts, the stakeholders want the page to behave like an admin panel that has links to all the viewable tables on the site, as well as a link to the table that will display open support tickets.

The [database diagram](https://github.com/NickApa/NATRSS/blob/main/Milestone2/BR_ERDiagram_milestone_2.png) may be usefull in implementation of the page.

## Acceptance Criteria
1. If a user logs in or registers as a basic user then redirect to the login view with their user model and display only their account information
2. If a user logs in or registers as an expedition provider then redirect to the login view with their user model and display a table of their expeditions
3.  If a user logs in as an employee then redirect to the login view with their user model and display their account information and links to the viewable tables and support ticket table.

## Tasks
1. Modify the login controller for register and login functions to redirect to the login page passing the user object made/retrieved from the db respectively.  
2. modify the login page to check if a complete user model was passed to the page then display account information instead of the login prompt.
3. display basic account information for any user (name, birthday, email, username account role [basic, expedition provider, employee])
4. Add logic statements to check the user role. if employee: display links to all viewable tables and a link to a page to view open support tickets. if Expedition provider display a table of their expeditions with a link on each one to request  

## Dependencies
This is tied together with user story #7. I will need the table view for employees and the modification request page for expedition providers completed so I can add links to them on my page.

## Any notes written while implementing this story