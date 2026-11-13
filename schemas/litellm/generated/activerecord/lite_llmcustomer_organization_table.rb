class LiteLLMCustomerOrganizationTable < ApplicationRecord
  self.table_name = "LiteLLM_CustomerOrganizationTable"
  self.primary_key = "customer_org_id"
  self.implicit_order_column = "created_at"

  attribute :spend, default: 0
  attribute :budget_ids, default: -> { [] }
  attribute :budget_names, default: -> { JSON.parse("{}") }
  attribute :customer_org_id, default: -> { SecureRandom.uuid }

  validates :created_by, presence: true
  validates :updated_by, presence: true

  belongs_to :budget, class_name: "LiteLLMBudgetTable", optional: true, inverse_of: :customer_organizations
  belongs_to :default_budget, class_name: "LiteLLMBudgetTable", optional: true,
                              inverse_of: :customer_organization_defaults
  has_many :teams, class_name: "LiteLLMCustomerTeamTable", foreign_key: "parent_org_id", inverse_of: :parent_org,
                   dependent: :restrict_with_error
end
