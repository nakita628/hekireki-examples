class ApiKey < ApplicationRecord
  self.table_name = "api_key"
  self.primary_key = "api_key_id"

  validates :name, presence: true, length: { maximum: 255 }
  validates :key_hash, presence: true, length: { maximum: 128 }, uniqueness: true
  validates :key_prefix, presence: true, length: { maximum: 20 }

  belongs_to :user, inverse_of: :api_keys
end
