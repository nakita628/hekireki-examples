class ConversationParticipant < ApplicationRecord
  self.table_name = "ConversationParticipant"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :receiveNotifications, default: false
  alias_attribute :created_at, :createdAt

  enum :role, { owner: "OWNER", participant: "PARTICIPANT" }, default: :participant, validate: true

  validates :conversationId, uniqueness: { scope: :viewerId, conditions: -> { where.not(viewerId: nil) } }
  validates :conversationId, uniqueness: { scope: :userId, conditions: -> { where.not(userId: nil) } }

  belongs_to :conversation, foreign_key: "conversationId", inverse_of: :participants
  belongs_to :viewer, foreign_key: "viewerId", optional: true, inverse_of: :participated_conversations
  belongs_to :user, foreign_key: "userId", optional: true, inverse_of: :participated_conversations
end
