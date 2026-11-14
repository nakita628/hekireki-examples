class IdentityProjectionCursor < ApplicationRecord
  self.table_name = "IdentityProjectionCursor"
  self.primary_key = "userId"

  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :lastEventId, presence: true
  validates :acceptedAt, presence: true
  validates :occurredAt, presence: true
  validates :projectionVersion, presence: true
end
