class Message < ApplicationRecord
  self.table_name = "Message"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :isRead, default: false
  attribute :isInternalNote, default: false
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :content, presence: true

  belongs_to :conversation, foreign_key: "conversationId", inverse_of: :messages
  belongs_to :user, foreign_key: "userId", optional: true, inverse_of: :messages
  belongs_to :viewer, foreign_key: "viewerId", optional: true, inverse_of: :messages
  belongs_to :view, foreign_key: "viewId", optional: true, inverse_of: :messages
  has_many :faq_as_question, class_name: "DataroomFaqItem", foreign_key: "questionMessageId",
                             inverse_of: :question_message, dependent: :nullify
  has_many :faq_as_answer, class_name: "DataroomFaqItem", foreign_key: "answerMessageId", inverse_of: :answer_message,
                           dependent: :nullify
end
