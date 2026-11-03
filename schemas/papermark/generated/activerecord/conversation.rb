class Conversation < ApplicationRecord
  self.table_name = "Conversation"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :isEnabled, default: true
  attribute :origin, default: "visitor"
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :visibilityMode, { private: "PRIVATE", public_link: "PUBLIC_LINK", public_group: "PUBLIC_GROUP", public_document: "PUBLIC_DOCUMENT", public_dataroom: "PUBLIC_DATAROOM" },
       default: :private, prefix: true, validate: true

  validates :questionId,
            uniqueness: { scope: :recipientViewerId, conditions: -> { where.not(recipientViewerId: nil) }, allow_nil: true }

  belongs_to :dataroom, foreign_key: "dataroomId", inverse_of: :conversations
  belongs_to :dataroom_document, foreign_key: "dataroomDocumentId", optional: true, inverse_of: :conversations
  belongs_to :link, foreign_key: "linkId", optional: true, inverse_of: :conversations
  belongs_to :viewer_group, foreign_key: "viewerGroupId", optional: true, inverse_of: :conversations
  belongs_to :question, class_name: "DataroomQuestion", foreign_key: "questionId", optional: true,
                        inverse_of: :conversations
  belongs_to :recipient_viewer, class_name: "Viewer", foreign_key: "recipientViewerId", optional: true,
                                inverse_of: :recipient_conversations
  belongs_to :initial_view, class_name: "View", foreign_key: "initialViewId", optional: true,
                            inverse_of: :initial_conversations
  belongs_to :team, foreign_key: "teamId", inverse_of: :conversations
  has_many :views, class_name: "ConversationView", foreign_key: "conversationId", inverse_of: :conversation,
                   dependent: :destroy
  has_many :participants, class_name: "ConversationParticipant", foreign_key: "conversationId",
                          inverse_of: :conversation, dependent: :destroy
  has_many :messages, foreign_key: "conversationId", inverse_of: :conversation, dependent: :destroy
  has_many :faq_items, class_name: "DataroomFaqItem", foreign_key: "sourceConversationId",
                       inverse_of: :source_conversation, dependent: :nullify
end
