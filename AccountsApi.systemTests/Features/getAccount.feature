Feature: Get Accounts Api
As a finance advisor
I should be able to get a list of account models

@smoke
Scenario Outline: Get Accounts API
    Given I get JWT authentication as a finance user
    When I request a GET on accounts api for '<targetId>' and '<accountType>'
    Then I am returned a valid response
Examples:
    | targetId                             | accountType |
    | c91be1b2-1e81-600d-65b0-d00775501a6b | Master      |
 #   |                                      | Recharge    |
 #   |                                      | Sundry      |

 @smoke
Scenario Outline: Get an error when no authentication is passed for Accounts API
    Given I don't get JWT authentication as a finance user
    When I request a GET request on accounts api for '<targetId>' and '<accountType>'
    Then I am returned an invalid response
Examples:
    | targetId                             | accountType |
    | c91be1b2-1e81-600d-65b0-d00775501a6b | Master      |
