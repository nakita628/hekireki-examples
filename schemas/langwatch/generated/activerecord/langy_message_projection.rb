class LangyMessageProjection < ApplicationRecord
  self.table_name = "LangyMessageProjection"

  attribute :id, default: -> { Nanoid.generate }

  validates :projectId, presence: true, uniqueness: { scope: [ :conversationId, :messageId ] }
  validates :conversationId, presence: true
  validates :messageId, presence: true
  validates :role, presence: true
  validates :parts, exclusion: { in: [ nil ] }
  validates :sourceEventId, presence: true, uniqueness: { scope: :projectId }
  validates :occurredAt, presence: true
  validates :acceptedAt, presence: true
  validates :createdAt, presence: true
  validates :updatedAt, presence: true
end
