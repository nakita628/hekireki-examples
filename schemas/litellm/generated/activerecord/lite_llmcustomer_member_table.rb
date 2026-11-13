class LiteLLMCustomerMemberTable < ApplicationRecord
  self.table_name = "LiteLLM_CustomerMemberTable"
  self.primary_key = "customer_member_id"
  self.implicit_order_column = "created_at"

  attribute :spend, default: 0
  attribute :customer_member_id, default: -> { SecureRandom.uuid }

  validates :created_by, presence: true
  validates :updated_by, presence: true

  belongs_to :parent_team, class_name: "LiteLLMCustomerTeamTable", inverse_of: :members
  belongs_to :budget, class_name: "LiteLLMBudgetTable", optional: true, inverse_of: :customer_members
  belongs_to :budget_template, class_name: "LiteLLMBudgetTable", optional: true, inverse_of: :customer_member_templates
end
