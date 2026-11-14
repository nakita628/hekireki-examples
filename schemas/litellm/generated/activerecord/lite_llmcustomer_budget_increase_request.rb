class LiteLLMCustomerBudgetIncreaseRequest < ApplicationRecord
  self.table_name = "LiteLLM_CustomerBudgetIncreaseRequest"
  self.primary_key = "request_id"

  attribute :request_id, default: -> { SecureRandom.uuid }
  attribute :status, default: "pending_review"

  validates :requested_max_budget, presence: true
  validates :submitted_by, presence: true

  belongs_to :customer_member, class_name: "LiteLLMCustomerMemberTable", inverse_of: :budget_increase_requests
end
