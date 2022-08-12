resource "aws_dynamodb_table" "accountsapi_dynamodb_table" {
    name                  = "Accounts"
    billing_mode          = "PROVISIONED"
    read_capacity         = 10
    write_capacity        = 10
    hash_key              = "id"
    autoscaling_enabled = true

    autoscaling_read {
        scale_in_cooldown  = 50
        scale_out_cooldown = 40
        target_value       = 45
        max_capacity       = 10
    }

    autoscaling_write {
        scale_in_cooldown  = 50
        scale_out_cooldown = 40
        target_value       = 45
        max_capacity       = 10
    }

    autoscaling_indexes {
        account_type_dx = {
            read_max_capacity  = 30
            read_min_capacity  = 10
            write_max_capacity = 30
            write_min_capacity = 10
        }
        target_id_dx = {
            read_max_capacity  = 30
            read_min_capacity  = 10
            write_max_capacity = 30
            write_min_capacity = 10
        }
    }

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
        BackupPolicy      = "Dev"
    }

    global_secondary_index {
        name               = "account_type_dx"
        hash_key           = "account_type"
        write_capacity     = 10
        read_capacity      = 10
        projection_type    = "ALL"
    }

    global_secondary_index {
        name               = "target_id_dx"
        hash_key           = "target_id"
        write_capacity     = 10
        read_capacity      = 10
        projection_type    = "ALL"
    }

    point_in_time_recovery {
        enabled           = true
    }
}
