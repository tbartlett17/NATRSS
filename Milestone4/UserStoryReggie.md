# As a Employee I want to process request forms, review them, see a list of all request forms and have full crud access to the database so that I can update expedition entries according to information on the request form.

## ID: 7
## Effort Points: 2
## Owner: Reggie Johnson
## Feature branch name: EmployeeRequestFeature

## Assumptions/Preconditions
The login feature is implented and users are assigned roles based on the roles table.


## Description
An employee should have the ability to update expeditions based on information from a request form. The employee will need to manually input the change so an approved button will denote the change has been made by the employee. The form should have a boolean atttribute for being accepted or rejected.

1. When a request form is accepted the updates will be applied, and rejected forms will not be applied.
2. When the employee views the list of forms they have access to all forms.
3. When a request is pending, the form will appear on the list of forms. 

Include links to modeling diagrams or anything else referenced or needed to complete this story.

## Acceptance Criteria

1. If employee clicks a button then they will be lead to a view listing all active request forms by date.
2. If employee clicks a request from the list then they will be lead to a request review view where they accept or reject forms or simply review inactive forms.
3. If employee approves a request then they should be able to update the expedition table with the information mentioned in the request.
4. If an expedition provider clicks a button then they are lead to a request form page where there can submit their requests.

## Tasks
1. create requestform controller
2. create request form view and update form table with the form model sent back to the controller.
3. create a request review view that displays an inactive or active request form.
4. create a  list view that lists request forms based on user role; if employee display all forms, if E.P. list forms with that E.P. id.
5. in the form review view, create a drop down for accepted or rejected. 

## Dependencies
6

## Any notes written while implementing this story