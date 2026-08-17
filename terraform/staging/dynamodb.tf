resource "aws_dynamodb_table" "accountsapi_dynamodb_table" {
    name                  = "Accounts"
    billing_mode          = "PROVISIONED"
    read_capacity         = 10
    write_capacity        = 10
    hash_key              = "id"

    attribute {
        name              = "id"
        type              = "S"
    }

    attribute {
        name              = "account_type"
        type              = "S"
    }
	
    attribute {
        name              = "target_id"
        type              = "S"
    }

    tags = {
        Name              = "accounts-api-${var.environment_name}"
        Environment       = var.environment_name
        terraform-managed = true
        project_name      = var.project_name
        BackupPolicy      = "Stg"        
    }

    global_secondary_index {
        name               = "account_type_dx"
        write_capacity     = 10
        read_capacity      = 10
        projection_type    = "ALL"

        key_schema {
            attribute_name = "account_type"
            key_type       = "HASH"
        }
    }

    global_secondary_index {
        name               = "target_id_dx"
        write_capacity     = 10
        read_capacity      = 10
        projection_type    = "ALL"

        key_schema {
            attribute_name = "target_id"
            key_type       = "HASH"
        }
    }

    point_in_time_recovery {
        enabled           = true
    }
}

resource "aws_appautoscaling_target" "dynamodb_table_write_target" {
  max_capacity       = 600
  min_capacity       = 100
  resource_id        = "table/${aws_dynamodb_table.accountsapi_dynamodb_table.name}"
  scalable_dimension = "dynamodb:table:WriteCapacityUnits"
  service_namespace  = "dynamodb"
}

resource "aws_appautoscaling_policy" "dynamodb_table_write_policy" {
  name               = "DynamoDBWriteCapacityUtilization:${aws_appautoscaling_target.dynamodb_table_write_target.resource_id}"
  policy_type        = "TargetTrackingScaling"
  resource_id        = aws_appautoscaling_target.dynamodb_table_write_target.resource_id
  scalable_dimension = aws_appautoscaling_target.dynamodb_table_write_target.scalable_dimension
  service_namespace  = aws_appautoscaling_target.dynamodb_table_write_target.service_namespace

  target_tracking_scaling_policy_configuration {
    predefined_metric_specification {
      predefined_metric_type = "DynamoDBWriteCapacityUtilization"
    }

    target_value = 70
  }
}