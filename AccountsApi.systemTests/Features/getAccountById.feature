Feature: Get Accounts Api By a given Id
As a finance advisor
I should be able to get details for a given account

@smoke
Scenario Outline: Get Account for a given Id
    Given I get JWT authentication as a finance user
    When I request a GET request for an '<id>'
    Then I am returned a valid account details
Examples:
    | id |
    |  94bef4bc-fe87-4426-8735-bc3736c03b89  |

 @smoke
Scenario Outline: Get an error when no authentication is passed for Accounts for a given Id
    Given I don't get JWT authentication as a finance user
    When I request a GET request on accounts api for an '<id>'
    Then I am returned an invalid response
Examples:
       | id |
    |  94bef4bc-fe87-4426-8735-bc3736c03b89  |
