class ConversationComment < ApplicationRecord
  self.table_name = "ConversationComment"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt

  validates :clientMutationId, presence: true,
                               uniqueness: { scope: [ :conversationId, :authorMemberId ], conditions: -> { where.not(authorMemberId: nil) } }
  validates :body, presence: true
  validates :revision, presence: true, uniqueness: { scope: :conversationId }

  belongs_to :conversation, foreign_key: "conversationId", inverse_of: :comments
  belongs_to :author, class_name: "Member", foreign_key: "authorMemberId", optional: true,
                      inverse_of: :conversation_comments
  has_many :activity, class_name: "ConversationActivity", foreign_key: "commentId", inverse_of: :comment,
                      dependent: :nullify
end
