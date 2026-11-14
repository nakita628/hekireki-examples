class LiteLLMCustomerMemberTable < ApplicationRecord
  self.table_name = "LiteLLM_CustomerMemberTable"
  self.primary_key = "customer_member_id"
  self.implicit_order_column = "created_at"

  attribute :spend, default: 0
  attribute :customer_member_id, default: -> { SecureRandom.uuid }
  attribute :parent_team_ids, default: -> { [] }

  validates :created_by, presence: true
  validates :updated_by, presence: true

  belongs_to :parent_org, class_name: "LiteLLMCustomerOrganizationTable", inverse_of: :members
  belongs_to :budget, class_name: "LiteLLMBudgetTable", optional: true, inverse_of: :customer_members
  belongs_to :budget_override, class_name: "LiteLLMBudgetTable", optional: true, inverse_of: :customer_member_overrides
  has_many :budget_increase_requests, class_name: "LiteLLMCustomerBudgetIncreaseRequest",
                                      foreign_key: "customer_member_id", inverse_of: :customer_member,
                                      dependent: :destroy
end
