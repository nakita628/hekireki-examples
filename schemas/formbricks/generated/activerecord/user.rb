class User < ApplicationRecord
  self.table_name = "User"
  self.implicit_order_column = "created_at"

  attribute :id, default: -> { Cuid.generate }
  attribute :email_verified, default: false
  attribute :twoFactorEnabled, default: false
  attribute :notificationSettings, default: -> { JSON.parse("{}") }
  attribute :locale, default: "en-US"
  attribute :isActive, default: true

  enum :identityProvider, { email: "email", github: "github", google: "google", azuread: "azuread", openid: "openid", saml: "saml" },
       default: :email, validate: true
  enum :isBootstrapAdmin, { bootstrap_admin: "bootstrapAdmin" }, validate: { allow_nil: true }

  validates :name, presence: true
  validates :email, presence: true, uniqueness: true

  has_one :two_factor, foreign_key: "userId", inverse_of: :user, dependent: :destroy
  has_one :password_reset_token, foreign_key: "userId", inverse_of: :user, dependent: :destroy
  has_many :memberships, foreign_key: "userId", inverse_of: :user, dependent: :destroy
  has_many :accounts, foreign_key: "userId", inverse_of: :user, dependent: :destroy
  has_many :sessions, foreign_key: "userId", inverse_of: :user, dependent: :destroy
  has_many :oauth_clients, foreign_key: "userId", inverse_of: :user, dependent: :destroy
  has_many :oauth_access_tokens, foreign_key: "userId", inverse_of: :user, dependent: :destroy
  has_many :oauth_refresh_tokens, foreign_key: "userId", inverse_of: :user, dependent: :destroy
  has_many :oauth_consents, foreign_key: "userId", inverse_of: :user, dependent: :destroy
  has_many :invites_created, class_name: "Invite", foreign_key: "creatorId", inverse_of: :creator,
                             dependent: :restrict_with_error
  has_many :invites_accepted, class_name: "Invite", foreign_key: "acceptorId", inverse_of: :acceptor,
                              dependent: :destroy
  has_many :surveys, foreign_key: "createdBy", inverse_of: :creator, dependent: :nullify
  has_many :owned_surveys, class_name: "Survey", foreign_key: "ownerId", inverse_of: :owner, dependent: :nullify
  has_many :charts, foreign_key: "createdBy", inverse_of: :creator, dependent: :nullify
  has_many :dashboards, foreign_key: "createdBy", inverse_of: :creator, dependent: :nullify
  has_many :creatives, foreign_key: "createdBy", inverse_of: :creator, dependent: :nullify
  has_many :feedback_sources, foreign_key: "created_by", inverse_of: :creator, dependent: :nullify
  has_many :team_users, foreign_key: "userId", inverse_of: :user, dependent: :destroy
  has_many :workflows, foreign_key: "createdBy", inverse_of: :creator, dependent: :nullify
  has_many :workflow_versions_published, class_name: "WorkflowVersion", foreign_key: "publishedBy",
                                         inverse_of: :publisher, dependent: :nullify
  has_many :retention_policies_updated, class_name: "RetentionPolicy", foreign_key: "updatedById",
                                        inverse_of: :updated_by, dependent: :nullify
  has_many :exemptions_created, class_name: "RetentionExemption", foreign_key: "createdById", inverse_of: :created_by,
                                dependent: :nullify
  has_many :exemptions_revoked, class_name: "RetentionExemption", foreign_key: "revokedById", inverse_of: :revoked_by,
                                dependent: :nullify
  has_many :retention_notices, foreign_key: "userId", inverse_of: :user, dependent: :destroy
  has_many :attachment_exports, foreign_key: "requestedById", inverse_of: :requested_by, dependent: :nullify
end
