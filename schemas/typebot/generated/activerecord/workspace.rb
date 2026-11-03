class Workspace < ApplicationRecord
  self.table_name = "Workspace"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :additionalChatsIndex, default: 0
  attribute :additionalStorageIndex, default: 0
  attribute :isQuarantined, default: false
  attribute :isSuspended, default: false
  attribute :isPastDue, default: false
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :plan, { free: "FREE", starter: "STARTER", pro: "PRO", lifetime: "LIFETIME", offered: "OFFERED", custom: "CUSTOM", unlimited: "UNLIMITED", enterprise: "ENTERPRISE" },
       default: :free, validate: true

  validates :name, presence: true
  validates :stripeId, uniqueness: { allow_nil: true }

  has_one :claimable_custom_plan, foreign_key: "workspaceId", inverse_of: :workspace, dependent: :destroy
  has_many :credentials, class_name: "Credentials", foreign_key: "workspaceId", inverse_of: :workspace,
                         dependent: :destroy
  has_many :custom_domains, foreign_key: "workspaceId", inverse_of: :workspace, dependent: :destroy
  has_many :folders, class_name: "DashboardFolder", foreign_key: "workspaceId", inverse_of: :workspace,
                     dependent: :destroy
  has_many :members, class_name: "MemberInWorkspace", foreign_key: "workspaceId", inverse_of: :workspace,
                     dependent: :destroy
  has_many :typebots, foreign_key: "workspaceId", inverse_of: :workspace, dependent: :destroy
  has_many :invitations, class_name: "WorkspaceInvitation", foreign_key: "workspaceId", inverse_of: :workspace,
                         dependent: :destroy
  has_many :theme_templates, foreign_key: "workspaceId", inverse_of: :workspace, dependent: :destroy
  has_many :spaces, foreign_key: "workspaceId", inverse_of: :workspace, dependent: :destroy
end
