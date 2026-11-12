class Cache < ApplicationRecord
  self.table_name = "cache"

  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :key, presence: true, uniqueness: true
  validates :value, exclusion: { in: [ nil ] }
end
