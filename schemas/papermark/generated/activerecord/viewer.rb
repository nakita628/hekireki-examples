class Viewer < ApplicationRecord
  self.table_name = "Viewer"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :verified, default: false
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :email, presence: true, uniqueness: { scope: :teamId }

  belongs_to :dataroom, foreign_key: "dataroomId", optional: true, inverse_of: :viewers
  belongs_to :team, foreign_key: "teamId", inverse_of: :viewers
  has_many :views, foreign_key: "viewerId", inverse_of: :viewer, dependent: :destroy
  has_many :groups, class_name: "ViewerGroupMembership", foreign_key: "viewerId", inverse_of: :viewer,
                    dependent: :destroy
  has_many :invitations, class_name: "ViewerInvitation", foreign_key: "viewerId", inverse_of: :viewer,
                         dependent: :destroy
  has_many :participated_conversations, class_name: "ConversationParticipant", foreign_key: "viewerId",
                                        inverse_of: :viewer, dependent: :nullify
  has_many :messages, foreign_key: "viewerId", inverse_of: :viewer, dependent: :nullify
  has_many :recipient_conversations, class_name: "Conversation", foreign_key: "recipientViewerId",
                                     inverse_of: :recipient_viewer, dependent: :nullify
  has_many :question_assignments, class_name: "DataroomQuestionAssignment", foreign_key: "viewerId", inverse_of: :viewer,
                                  dependent: :destroy
  has_many :uploaded_documents, class_name: "DocumentUpload", foreign_key: "viewerId", inverse_of: :viewer,
                                dependent: :nullify
  has_many :task_assignments, foreign_key: "viewerId", inverse_of: :viewer, dependent: :destroy
  has_many :task_activities, foreign_key: "viewerId", inverse_of: :viewer, dependent: :nullify
  has_many :chats, foreign_key: "viewerId", inverse_of: :viewer, dependent: :destroy
end
