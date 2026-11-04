class Invites < ApplicationRecord
  attribute :status, default: "pending"
  attribute :createdAt, PrismaDateTime.new, default: -> { Time.current }
  attribute :lastUpdatedAt, PrismaDateTime.new, default: -> { Time.current }
  alias_attribute :created_at, :createdAt

  validates :code, presence: true, uniqueness: true
  validates :createdBy, presence: true
end
