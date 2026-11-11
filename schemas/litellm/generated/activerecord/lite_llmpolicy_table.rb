class LiteLLMPolicyTable < ApplicationRecord
  self.table_name = "LiteLLM_PolicyTable"
  self.primary_key = "policy_id"
  self.implicit_order_column = "created_at"

  attribute :policy_id, default: -> { SecureRandom.uuid }
  attribute :version_number, default: 1
  attribute :version_status, default: "production"
  attribute :is_latest, default: true
  attribute :guardrails_add, default: -> { [] }
  attribute :guardrails_remove, default: -> { [] }
  attribute :condition, default: -> { JSON.parse("{}") }

  validates :policy_name, presence: true, uniqueness: { scope: :version_number }
end
