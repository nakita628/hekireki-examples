class RateLimit < ApplicationRecord
  self.table_name = "RateLimit"
  self.primary_key = %w[key action bucket]

  attribute :count, default: 1
  alias_attribute :created_at, :createdAt
end
