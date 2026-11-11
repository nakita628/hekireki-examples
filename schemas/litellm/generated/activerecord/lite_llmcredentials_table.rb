class LiteLLMCredentialsTable < ApplicationRecord
  self.table_name = "LiteLLM_CredentialsTable"
  self.primary_key = "credential_id"
  self.implicit_order_column = "created_at"

  attribute :credential_id, default: -> { SecureRandom.uuid }

  validates :credential_name, presence: true, uniqueness: true
  validates :credential_values, exclusion: { in: [ nil ] }
  validates :created_by, presence: true
  validates :updated_by, presence: true
end
