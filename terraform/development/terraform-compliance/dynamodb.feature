Feature: DynamoDB is used as our NoSQL database service
  In order to improve security
  As engineers
  We'll use ensure our DynamoDB tables are configured correctly

  Scenario: Ensure BackupPolicy tag is present
    Given I have aws_dynamodb_table defined
    Then it must contain tags
    And it must contain BackupPolicy