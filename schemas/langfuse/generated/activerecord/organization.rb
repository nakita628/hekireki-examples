class Organization < ApplicationRecord
  self.implicit_order_column = "created_at"

  attribute :id, default: -> { Cuid.generate }
  attribute :ai_features_enabled, default: false
  attribute :ai_telemetry_enabled, default: true
  attribute :feature_flag_org_defaults, default: -> { [] }

  validates :name, presence: true

  has_one :gateway_config, dependent: :destroy
  has_many :organization_memberships, foreign_key: "org_id", inverse_of: :organization, dependent: :destroy
  has_many :projects, foreign_key: "org_id", inverse_of: :organization, dependent: :destroy
  has_many :membership_invitation, foreign_key: "org_id", inverse_of: :organization, dependent: :destroy
  has_many :api_key, dependent: :destroy
  has_many :surveys, foreign_key: "org_id", inverse_of: :org, dependent: :destroy
  has_many :cloud_spend_alerts, foreign_key: "org_id", inverse_of: :org, dependent: :destroy
  has_many :verified_domains, dependent: :destroy
  has_many :gateway_ai_connections, dependent: :destroy
  has_many :organization_llm_api_keys, class_name: "LlmApiKeys", inverse_of: :organization, dependent: :destroy
  has_many :role_assignments, foreign_key: "org_id", inverse_of: :organization, dependent: :destroy
end
