class SystemSettings < ApplicationRecord
  attribute :createdAt, PrismaDateTime.new, default: -> { Time.current }
  attribute :lastUpdatedAt, PrismaDateTime.new, default: -> { Time.current }
  alias_attribute :created_at, :createdAt

  validates :label, presence: true, uniqueness: true
end
