class Links < ApplicationRecord
  attribute :views, default: 0
  attribute :enabled, default: true
  attribute :enableDownload, default: false
  attribute :expiresAt, PrismaDateTime.new
  attribute :createdAt, PrismaDateTime.new, default: -> { Time.current }
  attribute :editedAt, PrismaDateTime.new
  alias_attribute :created_at, :createdAt

  validates :uuid, presence: true, uniqueness: true
  validates :userId, presence: true
  validates :albumId, presence: true
  validates :identifier, presence: true, uniqueness: true
end
