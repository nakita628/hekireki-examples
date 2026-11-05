class ConversationActivity < ApplicationRecord
  self.table_name = "ConversationActivity"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt

  enum :kind, { invited: "INVITED", comment: "COMMENT", mention: "MENTION" }, validate: true

  validates :revision, presence: true, uniqueness: { scope: [ :participantId, :kind ] }

  belongs_to :participant, class_name: "ConversationParticipant", foreign_key: "participantId", inverse_of: :activity
  belongs_to :comment, class_name: "ConversationComment", foreign_key: "commentId", optional: true,
                       inverse_of: :activity
end
