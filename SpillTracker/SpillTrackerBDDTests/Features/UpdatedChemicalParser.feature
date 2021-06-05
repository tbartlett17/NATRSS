Feature: Updated Chemical Parser

Scenario: A scraped chemical has &prime; in its name
    Given a scraped chemical from the cfr list of chemicals
    When the name contains &prime;
    Then translate the &prime; to the appropriate code for a prime symbol

Scenario: A scraped chemical has &#961; in its name
    Given a scraped chemical from the cfr list of chemicals
    When the name contains &#961;
    Then translate the &#961; to the appropriate code for a Rho symbol

Scenario: The name of a scraped chemical is longer than one of its listed synonyms 
    Given A scraped chemical from the cfr list of chemicals
    When the name of the chemical is longer than one of the synonmyn chemical names
    Then change the listed chemcial name to the shortest one
    And add the longer old name to the the synonyms
    And delete the the switched name from the synonyms