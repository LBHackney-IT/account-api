resource "aws_ssm_parameter" "elasticsearch_domain" {
  name  = "/housing-search-api/pre-production/elasticsearch-domain"
  type  = "String"
  value = "to_be_set_manually"

  lifecycle {
    ignore_changes = [
      value,
    ]
  }
}

resource "aws_ssm_parameter" "auth_required_google_groups" {
  name  = "/housing-finance/pre-production/authorization/required-google-groups"
  type  = "String"
  value = "to_be_set_manually"

  lifecycle {
    ignore_changes = [
      value,
    ]
  }
}
