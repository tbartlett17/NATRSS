Feature: Updated Form

Scenario: Selecting a facility on the general info tab of the spill form
    Given a user is an Employee or FacilityManager of a company
    When they are viewing the "General Info" tab on the Form page
    And they click the drop-down menu to select a Facility
    Then they will see a list of facilities they have access to

Scenario: Seeing results of the spill calculation on the reluts tab of the spill form when the spill is reportable
    Given a user is an Employee or FacilityManager of a company
    And they have filled out all the information on the spill form
    And they have performed the spill calculation
    When they go to the Results tab on the spill form
    And the calculated spill is reportable 
    Then they will see on the Results Tab that the spill is reportable
    
Scenario: Seeing results of the spill calculation on the reluts tab of the spill form when the spill is not reportable
    Given a user is an Employee or FacilityManager of a company
    And they have filled out all the information on the spill form
    And they have performed the spill calculation
    When they go to the Results tab on the spill form
    And the calculated spill is not reportable 
    Then they will see on the Results Tab that the spill is not reportable

Scenario: Seeing who to contact when a spill is reportable
    Given a user is an Employee or FacilityManager of a company
    And they have filled out all the information on the spill form
    And they have performed the spill calculation
    And the spill is reportable
    When they go the Who To Contact tab on the spill form
    Then they will see a message that says to contact the local authorities

Scenario: Seeing a message that says I do not need to contact authorities if a spill is not reportable
    Given a user is an Employee or FacilityManager of a company
    And they have filled out all the information on the spill form
    And they have performed the spill calculation
    And the spill is not reportable
    When they go the Who To Contact tab on the spill form
    Then they will see a message that says the spill does not need to be reported to authorities