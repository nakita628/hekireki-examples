class TopicModelProjection < ApplicationRecord
  self.table_name = "TopicModelProjection"

  validates :projectId, presence: true, uniqueness: true
  validates :projectId, uniqueness: true
  validates :createdAt, presence: true
  validates :updatedAt, presence: true
  validates :occurredAt, presence: true
  validates :acceptedAt, presence: true
  validates :lastEventId, presence: true
  validates :projectionVersion, presence: true
end
