class LiteLLMAutoRouterDailySpend < ApplicationRecord
  self.table_name = "LiteLLM_AutoRouterDailySpend"
  self.primary_key = %w[date api_key user_id router_name router_type]

  attribute :turns, default: 0
  attribute :total_tokens, default: 0
  attribute :token_recorded_turns, default: 0
  attribute :spend, default: 0
  attribute :saved_spend, default: 0
  attribute :savings_estimated_turns, default: 0
  attribute :savings_estimated_actual_spend, default: 0
  attribute :savings_estimated_saved_spend, default: 0
  attribute :classifier_cost, default: 0
  attribute :classifier_cost_recorded_turns, default: 0
end
