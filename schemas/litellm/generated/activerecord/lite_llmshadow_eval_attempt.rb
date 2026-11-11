class LiteLLMShadowEvalAttempt < ApplicationRecord
  self.table_name = "LiteLLM_ShadowEvalAttempt"
  self.implicit_order_column = "created_at"

  attribute :id, default: -> { Cuid.generate }
  attribute :judge_cost, default: 0
  attribute :shadow_cost, default: 0
  attribute :real_classifier_cost, default: 0
  attribute :shadow_classifier_cost, default: 0
  attribute :real_cache_hit, default: false

  validates :job_id, presence: true
  validates :request_id, presence: true
  validates :outcome, presence: true
end
