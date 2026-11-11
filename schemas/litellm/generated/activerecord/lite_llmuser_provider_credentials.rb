class LiteLLMUserProviderCredentials < ApplicationRecord
  self.table_name = "LiteLLM_UserProviderCredentials"
  self.implicit_order_column = "created_at"

  attribute :id, default: -> { SecureRandom.uuid }

  validates :user_id, presence: true, uniqueness: { scope: :credential_name }
  validates :credential_name, presence: true
  validates :provider, presence: true
  validates :credential_b64, presence: true
end
