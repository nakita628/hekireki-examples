class StoredObject < ApplicationRecord
  self.table_name = "StoredObject"
  self.primary_key = %w[tenantId id]

  attribute :mediaTypeVerified, default: false
  attribute :generation, default: 0
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :status, presence: true
  validates :purpose, presence: true
  validates :ownerKind, presence: true
  validates :ownerId, presence: true
  validates :filename, presence: true
  validates :sha256, presence: true
  validates :sizeBytes, presence: true
  validates :mediaType, presence: true
  validates :audiences, exclusion: { in: [ nil ] }
  validates :source, presence: true
end
