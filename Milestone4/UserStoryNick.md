# As a user I want to be able to go on the expeditions list page I and be able to sort the list by success, season, and by a specific peak so I can see that data instead of just the top 50 most recent expeditions

## ID: 5
## Effort Points: 2
## Owner: Nick
## Feature branch name: f-5-Nick

## Assumptions/Preconditions
The Expodtion List page must already be complete only showing the top 50 expeditions by date. 

## Description
The stake holder wants the Expedition List page to have buttons to sort by peak, success, and the date. They also want a drop down for each peak in the database. When the buttons are clicked for each sort they want the table to update with that specific sort the user requested to see.

1. In the expedition list create buttons and drop down at the top of the page
2. Ensure the buttons and drop down look good to the stakeholder
3. When a button is clicked it should refresh the page with new data
4. When a peak in the drop down is selected the page should refresh with the peak selected

## Acceptance Criteria

1. If Sort by Season is clicked then the table should sort the data by autumn first since it comes first alphabetically 
2. If sort by success is clicked then the table should sort the data by success in the termination reason
3. If Sort by peak is clicked then the table should sort the data by the first peak that comes alphabetically or Ama Dablam
4. If a peak is selected in the drop down list, then the data that is shown in that table will only show that peak

## Tasks
1. Create three standard buttons for each sort (Success, Season, and Peak)
2. Create a drop down to show all of the peaks in the database
3. In the home controller create a function for each button and the drop down
4. In each function create linq statements to query the database and then return the data requested
5. Link the button with the function created in the controller
6. When the button is clicked it will reload the page and table with the correct data

## Dependencies
None available

## Any notes written while implementing this story
Use of linq pad to get data may help with writing database queries