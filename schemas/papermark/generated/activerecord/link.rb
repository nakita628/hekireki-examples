class Link < ApplicationRecord
  self.table_name = "Link"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :allowList, default: -> { [] }
  attribute :denyList, default: -> { [] }
  attribute :emailProtected, default: true
  attribute :emailAuthenticated, default: false
  attribute :allowDownload, default: false
  attribute :isArchived, default: false
  attribute :enableNotification, default: true
  attribute :enableFeedback, default: false
  attribute :enableQuestion, default: false
  attribute :enableScreenshotProtection, default: false
  attribute :enableConfidentialView, default: false
  attribute :enableAgreement, default: false
  attribute :showBanner, default: false
  attribute :enableWatermark, default: false
  attribute :enableCustomMetatag, default: false
  attribute :enableConversation, default: false
  attribute :enableAIAgents, default: false
  attribute :enableUpload, default: false
  attribute :isFileRequestOnly, default: false
  attribute :uploadFolderIds, default: -> { [] }
  attribute :enableIndexFile, default: false
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :linkType, { document_link: "DOCUMENT_LINK", dataroom_link: "DATAROOM_LINK", workflow_link: "WORKFLOW_LINK" },
       default: :document_link, validate: true
  enum :audienceType, { general: "GENERAL", group: "GROUP", team: "TEAM" }, default: :general, prefix: true,
                                                                            validate: true

  validates :url, uniqueness: { allow_nil: true }
  validates :domainSlug, uniqueness: { scope: :slug, conditions: -> { where.not(slug: nil) }, allow_nil: true }

  belongs_to :document, foreign_key: "documentId", optional: true, inverse_of: :links
  belongs_to :dataroom, foreign_key: "dataroomId", optional: true, inverse_of: :links
  belongs_to :domain, foreign_key: "domainId", optional: true, inverse_of: :links
  belongs_to :agreement, foreign_key: "agreementId", optional: true, inverse_of: :links
  belongs_to :group, class_name: "ViewerGroup", foreign_key: "groupId", optional: true, inverse_of: :links
  belongs_to :permission_group, foreign_key: "permissionGroupId", optional: true, inverse_of: :links
  belongs_to :team, foreign_key: "teamId", optional: true, inverse_of: :links
  belongs_to :brand, foreign_key: "brandId", optional: true, inverse_of: :links
  belongs_to :owner, class_name: "User", foreign_key: "ownerId", optional: true, inverse_of: :links
  has_one :feedback, foreign_key: "linkId", inverse_of: :link, dependent: :destroy
  has_one :workflow, foreign_key: "entryLinkId", inverse_of: :entry_link, dependent: :destroy
  has_many :views, foreign_key: "linkId", inverse_of: :link, dependent: :destroy
  has_many :conversations, foreign_key: "linkId", inverse_of: :link, dependent: :nullify
  has_many :question_assignments, class_name: "DataroomQuestionAssignment", foreign_key: "linkId", inverse_of: :link,
                                  dependent: :destroy
  has_many :dataroom_faq_items, foreign_key: "linkId", inverse_of: :link, dependent: :nullify
  has_many :chats, foreign_key: "linkId", inverse_of: :link, dependent: :destroy
  has_many :uploaded_documents, class_name: "DocumentUpload", foreign_key: "linkId", inverse_of: :link,
                                dependent: :destroy
  has_many :custom_fields, foreign_key: "linkId", inverse_of: :link, dependent: :destroy
  has_many :tags, class_name: "TagItem", foreign_key: "linkId", inverse_of: :link, dependent: :destroy
  has_many :viewer_invitations, foreign_key: "linkId", inverse_of: :link, dependent: :destroy
  has_many :visitor_groups, class_name: "LinkVisitorGroup", foreign_key: "linkId", inverse_of: :link,
                            dependent: :destroy
  has_many :task_assignments, foreign_key: "linkId", inverse_of: :link, dependent: :destroy
end
