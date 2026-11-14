class LiteLLMCustomerTeamTable < ApplicationRecord
  self.table_name = "LiteLLM_CustomerTeamTable"
  self.primary_key = "customer_team_id"
  self.implicit_order_column = "created_at"

  attribute :spend, default: 0
  attribute :customer_team_id, default: -> { SecureRandom.uuid }

  validates :created_by, presence: true
  validates :updated_by, presence: true

  belongs_to :parent_org, class_name: "LiteLLMCustomerOrganizationTable", inverse_of: :teams
  belongs_to :budget, class_name: "LiteLLMBudgetTable", optional: true, inverse_of: :customer_teams
  belongs_to :budget_override, class_name: "LiteLLMBudgetTable", optional: true, inverse_of: :customer_team_overrides
end
