class ScimSyncState < ApplicationRecord
  self.table_name = "ScimSyncState"

  attribute :deadLetters, default: -> { JSON.parse("[]") }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :connectionId, presence: true
  validates :organizationId, presence: true
  validates :state, presence: true
  validates :occurredAt, presence: true
  validates :lastEventId, presence: true
  validates :acceptedAt, presence: true
  validates :projectionVersion, presence: true
end
