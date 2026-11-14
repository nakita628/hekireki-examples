class LangyConversationProjection < ApplicationRecord
  self.table_name = "LangyConversationProjection"

  attribute :id, default: -> { Nanoid.generate }
  attribute :origin, default: "interactive"
  attribute :isShared, default: false
  attribute :messageCount, default: 0

  enum :titleSource, { derived: "derived", auto: "auto", user: "user" }, validate: true

  validates :projectId, presence: true, uniqueness: { scope: :conversationId }
  validates :conversationId, presence: true
  validates :userId, presence: true
  validates :status, presence: true
  validates :createdAt, presence: true
  validates :updatedAt, presence: true
  validates :occurredAt, presence: true
  validates :acceptedAt, presence: true
  validates :lastEventId, presence: true
  validates :projectionVersion, presence: true
end
