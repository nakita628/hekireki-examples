class TopicClusteringRunProjection < ApplicationRecord
  self.table_name = "TopicClusteringRunProjection"

  attribute :lastRunErrorUserActionable, default: false
  attribute :lastRunTracesProcessed, default: 0
  attribute :lastRunTopicsCount, default: 0
  attribute :lastRunSubtopicsCount, default: 0
  attribute :lastRunPages, default: 0
  attribute :inProgressTraces, default: 0
  attribute :inProgressPages, default: 0

  validates :projectId, presence: true, uniqueness: true
  validates :projectId, uniqueness: true
  validates :createdAt, presence: true
  validates :updatedAt, presence: true
  validates :occurredAt, presence: true
  validates :acceptedAt, presence: true
  validates :lastEventId, presence: true
  validates :projectionVersion, presence: true
end
