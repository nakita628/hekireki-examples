class Team < ApplicationRecord
  self.table_name = "Team"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :plan, default: "free"
  attribute :enableExcelAdvancedMode, default: false
  attribute :replicateDataroomFolders, default: true
  attribute :timezone, default: "Etc/UTC"
  attribute :ssoEnabled, default: false
  attribute :agentsEnabled, default: false
  attribute :ignoredDomains, default: -> { [] }
  attribute :globalBlockList, default: -> { [] }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :referralTrack, { affiliate: "AFFILIATE", partner_discount: "PARTNER_DISCOUNT" }, validate: { allow_nil: true }

  validates :name, presence: true
  validates :slug, uniqueness: { allow_nil: true }
  validates :stripeId, uniqueness: { allow_nil: true }
  validates :subscriptionId, uniqueness: { allow_nil: true }
  validates :ssoEmailDomain, uniqueness: { allow_nil: true }

  belongs_to :default_brand, class_name: "Brand", foreign_key: "defaultBrandId", optional: true,
                             inverse_of: :default_for_team
  has_many :users, class_name: "UserTeam", foreign_key: "teamId", inverse_of: :team, dependent: :destroy
  has_many :user_datarooms, foreign_key: "teamId", inverse_of: :team, dependent: :destroy
  has_many :documents, foreign_key: "teamId", inverse_of: :team, dependent: :destroy
  has_many :folders, foreign_key: "teamId", inverse_of: :team, dependent: :destroy
  has_many :domains, foreign_key: "teamId", inverse_of: :team, dependent: :destroy
  has_many :invitations, foreign_key: "teamId", inverse_of: :team, dependent: :destroy
  has_many :sent_emails, foreign_key: "teamId", inverse_of: :team, dependent: :destroy
  has_many :brands, foreign_key: "teamId", inverse_of: :team, dependent: :destroy
  has_many :datarooms, foreign_key: "teamId", inverse_of: :team, dependent: :destroy
  has_many :agreements, foreign_key: "teamId", inverse_of: :team, dependent: :destroy
  has_many :viewer_groups, foreign_key: "teamId", inverse_of: :team, dependent: :destroy
  has_many :visitor_groups, foreign_key: "teamId", inverse_of: :team, dependent: :destroy
  has_many :viewers, foreign_key: "teamId", inverse_of: :team, dependent: :destroy
  has_many :permission_groups, foreign_key: "teamId", inverse_of: :team, dependent: :destroy
  has_many :link_presets, foreign_key: "teamId", inverse_of: :team, dependent: :destroy
  has_many :incoming_webhooks, foreign_key: "teamId", inverse_of: :team, dependent: :destroy
  has_many :restricted_tokens, foreign_key: "teamId", inverse_of: :team, dependent: :destroy
  has_many :webhooks, foreign_key: "teamId", inverse_of: :team, dependent: :destroy
  has_many :conversations, foreign_key: "teamId", inverse_of: :team, dependent: :destroy
  has_many :dataroom_questions, foreign_key: "teamId", inverse_of: :team, dependent: :destroy
  has_many :dataroom_faq_items, foreign_key: "teamId", inverse_of: :team, dependent: :destroy
  has_many :uploaded_documents, class_name: "DocumentUpload", foreign_key: "teamId", inverse_of: :team,
                                dependent: :destroy
  has_many :tag, class_name: "Tag", foreign_key: "teamId", inverse_of: :team, dependent: :destroy
  has_many :annotations, class_name: "DocumentAnnotation", foreign_key: "teamId", inverse_of: :team, dependent: :destroy
  has_many :redaction_jobs, class_name: "DocumentRedactionJob", foreign_key: "teamId", inverse_of: :team,
                            dependent: :destroy
  has_many :installed_integrations, foreign_key: "teamId", inverse_of: :team, dependent: :destroy
  has_many :links, foreign_key: "teamId", inverse_of: :team, dependent: :destroy
  has_many :views, foreign_key: "teamId", inverse_of: :team, dependent: :destroy
  has_many :workflows, foreign_key: "teamId", inverse_of: :team, dependent: :destroy
  has_many :task_lists, foreign_key: "teamId", inverse_of: :team, dependent: :destroy
  has_many :tasks, foreign_key: "teamId", inverse_of: :team, dependent: :destroy
  has_many :chats, foreign_key: "teamId", inverse_of: :team, dependent: :destroy
  has_many :notification_preferences, foreign_key: "teamId", inverse_of: :team, dependent: :destroy
  has_many :referral_invites, foreign_key: "teamId", inverse_of: :team, dependent: :destroy
end
