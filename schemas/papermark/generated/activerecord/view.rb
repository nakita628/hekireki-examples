class View < ApplicationRecord
  self.table_name = "View"

  attribute :id, default: -> { Cuid.generate }
  attribute :verified, default: false
  attribute :isArchived, default: false

  enum :downloadType, { single: "SINGLE", bulk: "BULK", folder: "FOLDER" }, validate: { allow_nil: true }
  enum :viewType, { document_view: "DOCUMENT_VIEW", dataroom_view: "DATAROOM_VIEW" }, default: :document_view,
                                                                                      validate: true

  belongs_to :link, foreign_key: "linkId", inverse_of: :views
  belongs_to :document, foreign_key: "documentId", optional: true, inverse_of: :views
  belongs_to :dataroom, foreign_key: "dataroomId", optional: true, inverse_of: :views
  belongs_to :viewer, foreign_key: "viewerId", optional: true, inverse_of: :views
  belongs_to :group, class_name: "ViewerGroup", foreign_key: "groupId", optional: true, inverse_of: :views
  belongs_to :team, foreign_key: "teamId", optional: true, inverse_of: :views
  has_one :feedback_response, foreign_key: "viewId", inverse_of: :view, dependent: :destroy
  has_one :agreement_response, foreign_key: "viewId", inverse_of: :view, dependent: :destroy
  has_one :custom_field_response, foreign_key: "viewId", inverse_of: :view, dependent: :destroy
  has_many :reactions, foreign_key: "viewId", inverse_of: :view, dependent: :destroy
  has_many :conversation_views, foreign_key: "viewId", inverse_of: :view, dependent: :destroy
  has_many :messages, foreign_key: "viewId", inverse_of: :view, dependent: :nullify
  has_many :initial_conversations, class_name: "Conversation", foreign_key: "initialViewId", inverse_of: :initial_view,
                                   dependent: :nullify
  has_many :uploaded_documents, class_name: "DocumentUpload", foreign_key: "viewId", inverse_of: :view,
                                dependent: :nullify
  has_many :task_activities, foreign_key: "viewId", inverse_of: :view, dependent: :nullify
  has_many :chats, foreign_key: "viewId", inverse_of: :view, dependent: :destroy
end
