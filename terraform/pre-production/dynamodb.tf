resource "aws_dynamodb_table" "accountsapi_dynamodb_table" {
  name           = "Accounts"
  billing_mode   = "PROVISIONED"
  read_capacity  = 10
  write_capacity = 10
  hash_key       = "id"

  attribute {
    name = "id"
    type = "S"
  }

  attribute {
    name = "account_type"
    type = "S"
  }

  attribute {
    name = "target_id"
    type = "S"
  }

  tags = merge(
    local.default_tags,
    { BackupPolicy = "Dev", Backup = false, Confidentiality = "Internal" }
  )

  global_secondary_index {
    name            = "account_type_dx"
    write_capacity  = 10
    read_capacity   = 10
    projection_type = "ALL"

    key_schema {
      attribute_name = "account_type"
      key_type       = "HASH"
    }
  }

  global_secondary_index {
    name            = "target_id_dx"
    write_capacity  = 10
    read_capacity   = 10
    projection_type = "ALL"

    key_schema {
      attribute_name = "target_id"
      key_type       = "HASH"
    }
  }

  point_in_time_recovery {
    enabled = false
  }
}
