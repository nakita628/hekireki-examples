class LiteLLMAdaptiveRouterState < ApplicationRecord
  self.table_name = "LiteLLM_AdaptiveRouterState"
  self.primary_key = %w[router_name request_type model_name]

  attribute :total_samples, default: 0
  alias_attribute :updated_at, :last_updated_at

  validates :alpha, presence: true
  validates :beta, presence: true
end
