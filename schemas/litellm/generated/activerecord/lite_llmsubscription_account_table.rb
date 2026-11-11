class LiteLLMSubscriptionAccountTable < ApplicationRecord
  self.table_name = "LiteLLM_SubscriptionAccountTable"
  self.primary_key = "subscription_account_id"
  self.implicit_order_column = "created_at"

  attribute :subscription_account_id, default: -> { SecureRandom.uuid }

  validates :custom_llm_provider, presence: true, uniqueness: { scope: :account_id }
  validates :account_id, presence: true
  validates :monthly_fee, presence: true
  validates :currency, presence: true
  validates :billing_period_start, presence: true
  validates :created_by, presence: true
  validates :updated_by, presence: true
end
