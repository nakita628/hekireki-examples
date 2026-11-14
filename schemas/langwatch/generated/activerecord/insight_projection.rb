class InsightProjection < ApplicationRecord
  self.table_name = "InsightProjection"

  attribute :filedVia, default: "chat"

  validates :projectId, presence: true
  validates :title, presence: true
  validates :body, presence: true
  validates :tone, presence: true
  validates :validDays, presence: true
  validates :filedAt, presence: true
  validates :createdAt, presence: true
  validates :updatedAt, presence: true
  validates :occurredAt, presence: true
  validates :acceptedAt, presence: true
  validates :lastEventId, presence: true
  validates :projectionVersion, presence: true
end
