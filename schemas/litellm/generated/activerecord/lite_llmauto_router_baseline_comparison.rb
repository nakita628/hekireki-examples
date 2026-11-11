class LiteLLMAutoRouterBaselineComparison < ApplicationRecord
  self.table_name = "LiteLLM_AutoRouterBaselineComparison"
  self.primary_key = "scope"

  attribute :revision, default: 0
  attribute :published_revision, default: 0
  attribute :retired, default: false

  validates :api_key, presence: true
  validates :session_id, presence: true
  validates :router_name, presence: true
  validates :initial_equivalent, inclusion: { in: [ true, false ] }
end
