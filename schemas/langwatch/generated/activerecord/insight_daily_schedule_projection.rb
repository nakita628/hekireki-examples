class InsightDailyScheduleProjection < ApplicationRecord
  self.table_name = "InsightDailyScheduleProjection"

  attribute :state, default: "undecided"
  attribute :consecutiveSkips, default: 0

  validates :projectId, presence: true, uniqueness: { scope: [ :userId, :boardKind, :boardId ] }
  validates :userId, presence: true
  validates :boardKind, presence: true
  validates :boardId, presence: true
  validates :boardName, presence: true
  validates :createdAt, presence: true
  validates :updatedAt, presence: true
  validates :occurredAt, presence: true
  validates :acceptedAt, presence: true
  validates :lastEventId, presence: true
  validates :projectionVersion, presence: true
end
