class RevokedApiKey < ApplicationRecord
  self.table_name = "RevokedApiKey"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt

  validates :apiKey, presence: true
  validates :expiresAt, presence: true

  belongs_to :runtime_environment, foreign_key: "runtimeEnvironmentId", inverse_of: :revoked_api_keys
end
