class LiteLLMMCPUserEnvVars < ApplicationRecord
  self.table_name = "LiteLLM_MCPUserEnvVars"
  self.implicit_order_column = "created_at"

  attribute :id, default: -> { SecureRandom.uuid }

  validates :user_id, presence: true, uniqueness: { scope: :server_id }
  validates :server_id, presence: true
  validates :values_b64, presence: true
end
