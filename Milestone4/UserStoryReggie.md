# As a Employee I want to process request forms, review them, see a list of all request forms and have full crud access to the database so that I can update expedition entries according to information on the request form.

## ID: 7
## Effort Points: 2
## Owner: Reggie Johnson
## Feature branch name: EmployeeRequestFeature

## Assumptions/Preconditions
The login feature is implented and users are assigned roles based on the roles table.


## Description
An employee should have the ability to update expeditions based on information from a request form. The employee will need to manually input the change so a link to edit the expedition will lead to the Expeditions Controller edit action. The expedition provider will have a link that leads them to the form page where they can fill out a form and submit it to the form table in the db. 

1. When a request form is submitted it will appear on the request form list.
2. When the employee views the list of forms they have access to all forms.
3. When the employee review the request there will be a link to the expedition associated with the form.

Include links to modeling diagrams or anything else referenced or needed to complete this story.

## Acceptance Criteria

1. If employee clicks a button then they will be lead to a view listing all active request forms by date.
2. If employee clicks a request from the list then they will be lead to a request review view where they can review the form.
3. If employee views a request then they should be able to click a link to that specific expedition and update the expedition table with the information mentioned in the request.
4. If an expedition provider clicks a button then they are lead to a request form page where there can submit their requests.
4. If an expedition provider views the form list page then only their forms will be present in the list.
6. If an expedition provider reviews a form, they do not have access to the changing the expedition in the database.

## Tasks
1. create requestform controller
2. create request form view and send the form back to the controller.
3. create a request review view that displays the contents of a request form.
4. create a  list view that lists request forms based on user role; if employee display all forms, if E.P. list forms with that E.P. id.
5. in the form review view, create a link to the edit action in the expeditions controller sending the expedition id as a query string. 

## Dependencies
6

## Any notes written while implementing this story