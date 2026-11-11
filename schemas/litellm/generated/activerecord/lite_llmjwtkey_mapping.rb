class LiteLLMJWTKeyMapping < ApplicationRecord
  self.table_name = "LiteLLM_JWTKeyMapping"
  self.implicit_order_column = "created_at"

  attribute :id, default: -> { SecureRandom.uuid }
  attribute :jwt_issuer, default: ""
  attribute :is_active, default: true

  validates :jwt_issuer, uniqueness: { scope: %i[jwt_claim_name jwt_claim_value] }
  validates :jwt_claim_name, presence: true
  validates :jwt_claim_value, presence: true

  belongs_to :litellm_verification_token, class_name: "LiteLLMVerificationToken", foreign_key: "token",
                                          inverse_of: :jwt_key_mappings
end
