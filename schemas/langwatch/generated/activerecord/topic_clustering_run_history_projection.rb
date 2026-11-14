class TopicClusteringRunHistoryProjection < ApplicationRecord
  self.table_name = "TopicClusteringRunHistoryProjection"

  validates :projectId, presence: true, uniqueness: true
  validates :projectId, uniqueness: true
  validates :runs, exclusion: { in: [ nil ] }
  validates :createdAt, presence: true
  validates :updatedAt, presence: true
  validates :occurredAt, presence: true
  validates :acceptedAt, presence: true
  validates :lastEventId, presence: true
  validates :projectionVersion, presence: true
end
