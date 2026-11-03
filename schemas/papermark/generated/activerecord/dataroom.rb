class Dataroom < ApplicationRecord
  self.table_name = "Dataroom"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :conversationsEnabled, default: false
  attribute :agentsEnabled, default: false
  attribute :requestListEnabled, default: false
  attribute :enableChangeNotifications, default: false
  attribute :enableVisitorUploadChangeNotifications, default: false
  attribute :defaultShowBanner, default: true
  attribute :allowBulkDownload, default: true
  attribute :showLastUpdated, default: true
  attribute :introductionEnabled, default: false
  attribute :isFrozen, default: false
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :defaultPermissionStrategy, { inherit_from_parent: "INHERIT_FROM_PARENT", ask_every_time: "ASK_EVERY_TIME", hidden_by_default: "HIDDEN_BY_DEFAULT" },
       default: :inherit_from_parent, validate: true
  enum :defaultGroupPermissionStrategy, { inherit_from_parent: "INHERIT_FROM_PARENT", ask_every_time: "ASK_EVERY_TIME", hidden_by_default: "HIDDEN_BY_DEFAULT" },
       default: :inherit_from_parent, prefix: true, validate: true
  enum :defaultRootItemAccess, { view_only: "VIEW_ONLY", view_and_download: "VIEW_AND_DOWNLOAD", hidden: "HIDDEN" },
       default: :view_only, validate: true
  enum :defaultGroupRootItemAccess, { view_only: "VIEW_ONLY", view_and_download: "VIEW_AND_DOWNLOAD", hidden: "HIDDEN" },
       default: :view_only, prefix: true, validate: true

  validates :pId, presence: true, uniqueness: true
  validates :name, presence: true

  belongs_to :team, foreign_key: "teamId", inverse_of: :datarooms
  belongs_to :base_brand, class_name: "Brand", foreign_key: "brandId", optional: true, inverse_of: :datarooms
  belongs_to :frozen_by_user, class_name: "User", foreign_key: "frozenBy", optional: true, inverse_of: :frozen_datarooms
  has_one :brand, class_name: "DataroomBrand", foreign_key: "dataroomId", inverse_of: :dataroom, dependent: :destroy
  has_many :documents, class_name: "DataroomDocument", foreign_key: "dataroomId", inverse_of: :dataroom,
                       dependent: :destroy
  has_many :folders, class_name: "DataroomFolder", foreign_key: "dataroomId", inverse_of: :dataroom, dependent: :destroy
  has_many :links, foreign_key: "dataroomId", inverse_of: :dataroom, dependent: :destroy
  has_many :views, foreign_key: "dataroomId", inverse_of: :dataroom, dependent: :destroy
  has_many :viewers, foreign_key: "dataroomId", inverse_of: :dataroom, dependent: :nullify
  has_many :viewer_groups, foreign_key: "dataroomId", inverse_of: :dataroom, dependent: :destroy
  has_many :user_datarooms, foreign_key: "dataroomId", inverse_of: :dataroom, dependent: :destroy
  has_many :permission_groups, foreign_key: "dataroomId", inverse_of: :dataroom, dependent: :destroy
  has_many :conversations, foreign_key: "dataroomId", inverse_of: :dataroom, dependent: :destroy
  has_many :questions, class_name: "DataroomQuestion", foreign_key: "dataroomId", inverse_of: :dataroom,
                       dependent: :destroy
  has_many :faq_items, class_name: "DataroomFaqItem", foreign_key: "dataroomId", inverse_of: :dataroom,
                       dependent: :destroy
  has_many :chats, foreign_key: "dataroomId", inverse_of: :dataroom, dependent: :destroy
  has_many :uploaded_documents, class_name: "DocumentUpload", foreign_key: "dataroomId", inverse_of: :dataroom,
                                dependent: :nullify
  has_many :tags, class_name: "TagItem", foreign_key: "dataroomId", inverse_of: :dataroom, dependent: :destroy
  has_many :task_lists, foreign_key: "dataroomId", inverse_of: :dataroom, dependent: :destroy
  has_many :tasks, foreign_key: "dataroomId", inverse_of: :dataroom, dependent: :destroy
end
