class JacksonStore < ApplicationRecord
  self.table_name = "jackson_store"
  self.primary_key = "key"

  attribute :createdAt, :datetime, precision: 3
  attribute :modifiedAt, :datetime, precision: 3
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :modifiedAt

  validates :key, length: { maximum: 250 }
  validates :value, presence: true
  validates :iv, length: { maximum: 64 }
  validates :tag, length: { maximum: 64 }
  validates :namespace, length: { maximum: 64 }
end
