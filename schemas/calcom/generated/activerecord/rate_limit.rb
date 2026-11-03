class RateLimit < ApplicationRecord
  self.table_name = "RateLimit"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { SecureRandom.uuid }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :name, presence: true
  validates :ttl, presence: true
  validates :limit, presence: true
  validates :blockDuration, presence: true

  belongs_to :api_key, foreign_key: "apiKeyId", inverse_of: :rate_limits
end
