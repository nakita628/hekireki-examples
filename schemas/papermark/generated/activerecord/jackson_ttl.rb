class JacksonTtl < ApplicationRecord
  self.table_name = "jackson_ttl"
  self.primary_key = "key"

  validates :expiresAt, presence: true
end
