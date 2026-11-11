class LiteLLMDeprecatedVerificationToken < ApplicationRecord
  self.table_name = "LiteLLM_DeprecatedVerificationToken"
  self.implicit_order_column = "created_at"

  attribute :id, default: -> { SecureRandom.uuid }

  validates :token, presence: true, uniqueness: true
  validates :token, uniqueness: true
  validates :active_token_id, presence: true
  validates :revoke_at, presence: true
end
