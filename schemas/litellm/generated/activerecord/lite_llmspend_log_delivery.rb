class LiteLLMSpendLogDelivery < ApplicationRecord
  self.table_name = "LiteLLM_SpendLogDelivery"
  self.primary_key = "delivery_id"

  attribute :attempts, default: 0

  enum :state, { pending: "pending", applied: "applied", parked: "parked" }, default: :pending, validate: true

  validates :request_id, presence: true
  validates :accounting_payload, exclusion: { in: [ nil ] }
  validates :is_batch, inclusion: { in: [ true, false ] }
end
