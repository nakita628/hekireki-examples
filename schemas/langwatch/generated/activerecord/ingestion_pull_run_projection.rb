class IngestionPullRunProjection < ApplicationRecord
  self.table_name = "IngestionPullRunProjection"

  attribute :enabled, default: false
  attribute :lastRunEventCount, default: 0
  attribute :consecutiveErrors, default: 0

  validates :sourceId, presence: true, uniqueness: true
  validates :projectId, presence: true
  validates :createdAt, presence: true
  validates :updatedAt, presence: true
  validates :occurredAt, presence: true
  validates :acceptedAt, presence: true
  validates :lastEventId, presence: true
  validates :projectionVersion, presence: true
end
