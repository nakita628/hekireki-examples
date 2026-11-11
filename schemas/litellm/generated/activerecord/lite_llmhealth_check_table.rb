class LiteLLMHealthCheckTable < ApplicationRecord
  self.table_name = "LiteLLM_HealthCheckTable"
  self.primary_key = "health_check_id"
  self.implicit_order_column = "created_at"

  attribute :health_check_id, default: -> { SecureRandom.uuid }
  attribute :healthy_count, default: 0
  attribute :unhealthy_count, default: 0

  validates :model_name, presence: true
  validates :status, presence: true
end
