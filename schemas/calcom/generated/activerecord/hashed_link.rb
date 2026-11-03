class HashedLink < ApplicationRecord
  self.table_name = "HashedLink"

  attribute :maxUsageCount, default: 1
  attribute :usageCount, default: 0

  validates :link, presence: true, uniqueness: true

  belongs_to :event_type, foreign_key: "eventTypeId", inverse_of: :hashed_link
end
