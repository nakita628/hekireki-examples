class LiteLLMAutoRouterBaselineObservation < ApplicationRecord
  self.table_name = "LiteLLM_AutoRouterBaselineObservation"
  self.primary_key = "request_id"

  attribute :conflicted, default: false

  validates :scope, presence: true
  validates :started_at, presence: true
  validates :revision, presence: true
  validates :data, presence: true
end
