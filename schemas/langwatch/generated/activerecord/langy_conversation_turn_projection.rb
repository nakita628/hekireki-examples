class LangyConversationTurnProjection < ApplicationRecord
  self.table_name = "LangyConversationTurnProjection"

  attribute :id, default: -> { Nanoid.generate }

  validates :projectId, presence: true, uniqueness: { scope: [ :conversationId, :turnId ] }
  validates :conversationId, presence: true
  validates :turnId, presence: true
  validates :status, presence: true
  validates :questionParts, exclusion: { in: [ nil ] }
  validates :answerParts, exclusion: { in: [ nil ] }
  validates :toolCalls, exclusion: { in: [ nil ] }
  validates :createdAt, presence: true
  validates :updatedAt, presence: true
  validates :occurredAt, presence: true
  validates :acceptedAt, presence: true
  validates :lastEventId, presence: true
  validates :projectionVersion, presence: true
end
