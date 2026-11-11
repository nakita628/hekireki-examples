class LiteLLMProxyModelTable < ApplicationRecord
  self.table_name = "LiteLLM_ProxyModelTable"
  self.primary_key = "model_id"
  self.implicit_order_column = "created_at"

  attribute :model_id, default: -> { SecureRandom.uuid }
  attribute :blocked, default: false

  validates :model_name, presence: true
  validates :litellm_params, exclusion: { in: [ nil ] }
  validates :created_by, presence: true
  validates :updated_by, presence: true
end
