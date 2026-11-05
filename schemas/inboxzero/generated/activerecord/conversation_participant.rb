class ConversationParticipant < ApplicationRecord
  self.table_name = "ConversationParticipant"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :active, default: true
  attribute :readRevision, default: 0
  attribute :muted, default: false
  alias_attribute :created_at, :createdAt

  validates :generation, presence: true, uniqueness: { scope: [ :conversationId, :memberId ] }

  belongs_to :conversation, foreign_key: "conversationId", inverse_of: :participants
  belongs_to :member, foreign_key: "memberId", inverse_of: :conversation_participants
  has_many :activity, class_name: "ConversationActivity", foreign_key: "participantId", inverse_of: :participant,
                      dependent: :destroy
end
