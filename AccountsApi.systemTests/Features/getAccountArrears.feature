Feature: Get Accounts Arrears Api
As a finance advisor
I should be able to get a list of account models

@smoke
Scenario Outline: Get Accounts arrears API
    Given I get JWT authentication as a finance user
    When I request a GET on accounts arrears api for '<accountType>', '<sort>' and '<direction>'
    Then I am returned a valid response
Examples:
   | accountType | sort           | direction |
   | Master      | AccountBalance | Asc       |
 
 @smoke
Scenario Outline: Get an error when no authentication is passed for Accounts arrears API
    Given I don't get JWT authentication as a finance user
    When I request a GET request on accounts arrears api for '<accountType>', '<sort>' and '<direction>'
    Then I am returned an invalid response
Examples:
    | accountType | sort           | direction |
    | Master      | AccountBalance | Asc       |
