class LiteLLMEndUserTable < ApplicationRecord
  self.table_name = "LiteLLM_EndUserTable"
  self.primary_key = "user_id"

  attribute :spend, default: 0
  attribute :models, default: -> { [] }
  attribute :blocked, default: false

  belongs_to :litellm_budget_table, class_name: "LiteLLMBudgetTable", foreign_key: "budget_id", optional: true,
                                    inverse_of: :end_users
  belongs_to :object_permission, class_name: "LiteLLMObjectPermissionTable", optional: true, inverse_of: :end_users
end
