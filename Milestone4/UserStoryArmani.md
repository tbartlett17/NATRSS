## As a public user, I would like to be able to browse or search for peaks so that I can easily find them.

## ID:

8

## Effort Points:

2

## Owner:

Armani

## Feature branch name:

fSearch_Peaks

## Assumptions/Preconditions:

Initially there isn't a current search bar, so I will definitely need to create one. It would probably need to go inside the nav bar.

## Description:

The stakeholder would like there to be a convential way to search for Peaks. They want to be able to browse for potential peaks within the database. In order for the peaks to be found in a convential manner the search will need to be inside the nav bar.

1. Create a search bar
2. Create a button (submit) for the search bar
3. on click, page should return results of Peaks from database  

Include links to modeling diagrams or anything else referenced or needed to complete this story.

## Acceptance Criteria:

1. If I type peaks within the search bar and click the search button then a page should be returned of all the peak names from within the database.

## Tasks:

1. Create a form for the search bar
2. Place the search bar inside the nav of the shared layout page
3. In the Search controller create a get function to retrieve search input
4. In the Search controller create a post functionn to return the search of the user input of  Peak names
5. Make UI pretty

## Dependencies:

None at the moment.

## Any notes written while implementing this story
