class DataroomFaqItem < ApplicationRecord
  self.table_name = "DataroomFaqItem"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :isAnonymized, default: true
  attribute :viewCount, default: 0
  attribute :tags, default: -> { [] }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :visibilityMode, { public_dataroom: "PUBLIC_DATAROOM", public_link: "PUBLIC_LINK", public_document: "PUBLIC_DOCUMENT" },
       default: :public_dataroom, validate: true
  enum :status, { draft: "DRAFT", published: "PUBLISHED", archived: "ARCHIVED" }, default: :published, validate: true

  validates :editedQuestion, presence: true
  validates :answer, presence: true

  belongs_to :dataroom, foreign_key: "dataroomId", inverse_of: :faq_items
  belongs_to :link, foreign_key: "linkId", optional: true, inverse_of: :dataroom_faq_items
  belongs_to :dataroom_document, foreign_key: "dataroomDocumentId", optional: true, inverse_of: :dataroom_faqs
  belongs_to :source_conversation, class_name: "Conversation", foreign_key: "sourceConversationId", optional: true,
                                   inverse_of: :faq_items
  belongs_to :question_message, class_name: "Message", foreign_key: "questionMessageId", optional: true,
                                inverse_of: :faq_as_question
  belongs_to :answer_message, class_name: "Message", foreign_key: "answerMessageId", optional: true,
                              inverse_of: :faq_as_answer
  belongs_to :team, foreign_key: "teamId", inverse_of: :dataroom_faq_items
  belongs_to :published_by_user, class_name: "User", foreign_key: "publishedByUserId", inverse_of: :published_faq_items
end
