class GatewayConfig < ApplicationRecord
  self.primary_key = "organization_id"

  enum :ingestion_mode, { none: "none", usage: "usage", full: "full" }, default: :usage, prefix: true, validate: true

  belongs_to :organization
  belongs_to :default_ingestion_project, class_name: "Project", optional: true, inverse_of: :gateway_configs
end
