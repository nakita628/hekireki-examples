class LiteLLMOrganizationTable < ApplicationRecord
  self.table_name = "LiteLLM_OrganizationTable"
  self.primary_key = "organization_id"
  self.implicit_order_column = "created_at"

  attribute :organization_id, default: -> { SecureRandom.uuid }
  attribute :metadata, default: -> { JSON.parse("{}") }
  attribute :models, default: -> { [] }
  attribute :spend, default: 0
  attribute :model_spend, default: -> { JSON.parse("{}") }

  validates :organization_alias, presence: true
  validates :created_by, presence: true
  validates :updated_by, presence: true

  belongs_to :litellm_budget_table, class_name: "LiteLLMBudgetTable", foreign_key: "budget_id", optional: true,
                                    inverse_of: :organization
  belongs_to :object_permission, class_name: "LiteLLMObjectPermissionTable", optional: true, inverse_of: :organizations
  has_many :teams, class_name: "LiteLLMTeamTable", foreign_key: "organization_id",
                   inverse_of: :litellm_organization_table, dependent: :nullify
  has_many :users, class_name: "LiteLLMUserTable", foreign_key: "organization_id",
                   inverse_of: :litellm_organization_table, dependent: :nullify
  has_many :keys, class_name: "LiteLLMVerificationToken", foreign_key: "organization_id",
                  inverse_of: :litellm_organization_table, dependent: :nullify
  has_many :members, class_name: "LiteLLMOrganizationMembership", foreign_key: "organization_id",
                     inverse_of: :organization, dependent: :restrict_with_error
end
