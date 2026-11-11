class LiteLLMToolTable < ApplicationRecord
  self.table_name = "LiteLLM_ToolTable"
  self.primary_key = "tool_id"
  self.implicit_order_column = "created_at"

  attribute :tool_id, default: -> { SecureRandom.uuid }
  attribute :input_policy, default: "untrusted"
  attribute :output_policy, default: "untrusted"
  attribute :call_count, default: 0
  attribute :assignments, default: -> { JSON.parse("{}") }

  validates :tool_name, presence: true, uniqueness: true
end
