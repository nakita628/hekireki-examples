class Conversation < ApplicationRecord
  self.table_name = "Conversation"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :revision, default: 0
  attribute :generation, default: 1
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :status, { active: "ACTIVE", stopped: "STOPPED" }, default: :active, validate: true

  validates :threadId, presence: true, uniqueness: { scope: :publisherId }

  belongs_to :organization, foreign_key: "organizationId", inverse_of: :conversations
  belongs_to :publisher, class_name: "Member", foreign_key: "publisherId", inverse_of: :conversations
  has_many :participants, class_name: "ConversationParticipant", foreign_key: "conversationId",
                          inverse_of: :conversation, dependent: :destroy
  has_many :comments, class_name: "ConversationComment", foreign_key: "conversationId", inverse_of: :conversation,
                      dependent: :destroy
end
