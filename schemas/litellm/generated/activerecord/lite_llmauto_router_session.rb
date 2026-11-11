class LiteLLMAutoRouterSession < ApplicationRecord
  self.table_name = "LiteLLM_AutoRouterSession"
  self.primary_key = %w[api_key session_id router_name]

  attribute :models, default: -> { JSON.parse("{}") }
  attribute :turns, default: 0
  attribute :unordered_turns, default: 0
  attribute :covered_turns, default: 0
  attribute :cache_hits, default: 0
  attribute :same_model_turns, default: 0
  attribute :same_model_hits, default: 0
  attribute :first_visit_turns, default: 0
  attribute :first_visit_hits, default: 0
  attribute :return_turns, default: 0
  attribute :return_hits, default: 0
  attribute :return_expired_misses, default: 0
  attribute :return_within_ttl_misses, default: 0
  attribute :ttl_5m_turns, default: 0
  attribute :ttl_1h_turns, default: 0
  attribute :total_tokens, default: 0
  attribute :spend, default: 0
  attribute :saved_spend, default: 0
  attribute :savings_estimated_turns, default: 0
  attribute :savings_estimated_actual_spend, default: 0
  attribute :savings_estimated_saved_spend, default: 0
  attribute :savings_estimated_baseline_models, default: -> { JSON.parse("{}") }
  attribute :classifier_cost, default: 0
  attribute :classifier_cost_recorded_turns, default: 0
  attribute :tier_turns, default: -> { JSON.parse("{}") }
  attribute :baseline_models, default: -> { JSON.parse("{}") }

  validates :router_type, presence: true
  validates :first_turn_at, presence: true
  validates :last_turn_at, presence: true
  validates :last_model, presence: true
end
