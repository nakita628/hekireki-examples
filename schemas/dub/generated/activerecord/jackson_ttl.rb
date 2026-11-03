class JacksonTtl < ApplicationRecord
  self.table_name = "jackson_ttl"
  self.primary_key = "key"

  validates :key, length: { maximum: 250 }
  validates :expiresAt, presence: true
end
