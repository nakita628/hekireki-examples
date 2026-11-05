class Invites < ApplicationRecord
  attribute :used, default: false
  attribute :createdAt, PrismaDateTime.new, default: -> { Time.current }
  attribute :editedAt, PrismaDateTime.new
  alias_attribute :created_at, :createdAt

  validates :createdBy, presence: true
  validates :code, presence: true, uniqueness: true
end
