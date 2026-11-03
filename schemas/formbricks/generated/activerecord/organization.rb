class Organization < ApplicationRecord
  self.table_name = "Organization"
  self.implicit_order_column = "created_at"

  attribute :id, default: -> { Cuid.generate }
  attribute :whitelabel, default: -> { JSON.parse("{}") }
  attribute :isAISmartToolsEnabled, default: false

  validates :name, presence: true

  has_one :billing, class_name: "OrganizationBilling", inverse_of: :organization, dependent: :destroy
  has_many :memberships, foreign_key: "organizationId", inverse_of: :organization, dependent: :destroy
  has_many :workspaces, foreign_key: "organizationId", inverse_of: :organization, dependent: :destroy
  has_many :invites, foreign_key: "organizationId", inverse_of: :organization, dependent: :destroy
  has_many :teams, foreign_key: "organizationId", inverse_of: :organization, dependent: :destroy
  has_many :api_keys, foreign_key: "organizationId", inverse_of: :organization, dependent: :destroy
  has_many :feedback_directories, foreign_key: "organizationId", inverse_of: :organization, dependent: :destroy
  has_many :retention_policies, foreign_key: "organizationId", inverse_of: :organization, dependent: :destroy
  has_many :retention_exemptions, foreign_key: "organizationId", inverse_of: :organization, dependent: :destroy
  has_many :retention_runs, foreign_key: "organizationId", inverse_of: :organization, dependent: :destroy
  has_many :retention_notices, foreign_key: "organizationId", inverse_of: :organization, dependent: :destroy
end
