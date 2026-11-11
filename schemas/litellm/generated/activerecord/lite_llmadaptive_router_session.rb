class LiteLLMAdaptiveRouterSession < ApplicationRecord
  self.table_name = "LiteLLM_AdaptiveRouterSession"
  self.primary_key = %w[session_id router_name model_name]

  attribute :misalignment_count, default: 0
  attribute :stagnation_count, default: 0
  attribute :disengagement_count, default: 0
  attribute :satisfaction_count, default: 0
  attribute :failure_count, default: 0
  attribute :loop_count, default: 0
  attribute :exhaustion_count, default: 0
  attribute :tool_call_history, default: -> { JSON.parse("[]") }
  attribute :pending_tool_calls, default: -> { JSON.parse("{}") }
  attribute :turn_count, default: 0
  attribute :last_processed_turn, default: -1
  attribute :clean_credit_awarded, default: false
  alias_attribute :updated_at, :last_activity_at

  validates :classified_type, presence: true
end
