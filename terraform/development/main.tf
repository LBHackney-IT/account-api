# INSTRUCTIONS:
# 1) ENSURE YOU POPULATE THE LOCALS
# 2) ENSURE YOU REPLACE ALL INPUT PARAMETERS, THAT CURRENTLY STATE 'ENTER VALUE', WITH VALID VALUES
# 3) YOUR CODE WOULD NOT COMPILE IF STEP NUMBER 2 IS NOT PERFORMED!
# 4) ENSURE YOU CREATE A BUCKET FOR YOUR STATE FILE AND YOU ADD THE NAME BELOW - MAINTAINING THE STATE OF THE INFRASTRUCTURE YOU CREATE IS ESSENTIAL - FOR APIS, THE BUCKETS ALREADY EXIST
# 5) THE VALUES OF THE COMMON COMPONENTS THAT YOU WILL NEED ARE PROVIDED IN THE COMMENTS
# 6) IF ADDITIONAL RESOURCES ARE REQUIRED BY YOUR API, ADD THEM TO THIS FILE
# 7) ENSURE THIS FILE IS PLACED WITHIN A 'terraform' FOLDER LOCATED AT THE ROOT PROJECT DIRECTORY

terraform {
    required_providers {
        aws = {
            source  = "hashicorp/aws"
            version = "~> 6.0"
        }
    }
}

provider "aws" {
    region = "eu-west-2"
}

provider "aws" {
  alias  = "auth_account"
  region = "eu-west-2"
  
  assume_role {
    role_arn = "arn:aws:iam::859159924354:role/hackney-central-auth-development-apis-m2m-guest-deployer"
  }
}

data "aws_caller_identity" "current" {}

data "aws_region" "current" {}

locals {
    parameter_store = "arn:aws:ssm:${data.aws_region.current.name}:${data.aws_caller_identity.current.account_id}:parameter"
}

terraform {
    backend "s3" {
        bucket  = "terraform-state-housing-development"
        encrypt = true
        region  = "eu-west-2"
        key     = "services/accounts-api/state"
    }
}

resource "aws_sns_topic" "accounts_topic" {
    name                        = "accounts.fifo"
    fifo_topic                  = true
    content_based_deduplication = true
    kms_master_key_id = "alias/aws/sns"
}

resource "aws_ssm_parameter" "accounts_sns_arn" {
    name  = "/sns-topic/${var.environment_name}/accounts/arn"
    type  = "String"
    value = aws_sns_topic.accounts_topic.arn
}

module "accounts_api_resource_server" {
    source = "github.com/LBHackney-IT/api-gateway-lambda-authorizer.git//terraform/modules/cognito-m2m-resource-server"

    providers = {
        aws.authorizer_account = aws.auth_account
    }

    api_name       = "Accounts API"
    api_identifier = "culd0aqcj0" 

    scopes = [
        {
            name        = "account.read"
            description = "Get single account by id"
        },
        {
            name        = "account.create"
            description = "Create a finance account for tenure"
        }
    ]
}

# This would normally be in the repo of whatever app would consume the above resource server but for now
# which is just a distributed iac test, it will do

# TODO:
# source = "github.com/LBHackney-IT/api-gateway-lambda-authorizer.git//terraform/modules/cognito-m2m-app-client"
