class CacheData < ApplicationRecord
  attribute :expiresAt, PrismaDateTime.new
  attribute :createdAt, PrismaDateTime.new, default: -> { Time.current }
  attribute :lastUpdatedAt, PrismaDateTime.new, default: -> { Time.current }
  alias_attribute :created_at, :createdAt

  validates :name, presence: true
  validates :data, presence: true
end
