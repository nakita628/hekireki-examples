class Project < ApplicationRecord
  self.table_name = "Project"

  attribute :plan, default: "free"
  attribute :planTier, default: 1
  attribute :totalLinks, default: 0
  attribute :totalClicks, default: 0
  attribute :usage, default: 0
  attribute :usageLimit, default: 1000
  attribute :linksUsage, default: 0
  attribute :linksLimit, default: 25
  attribute :domainsLimit, default: 3
  attribute :tagsLimit, default: 5
  attribute :foldersUsage, default: 0
  attribute :foldersLimit, default: 0
  attribute :usersLimit, default: 1
  attribute :aiUsage, default: 0
  attribute :aiLimit, default: 10
  attribute :conversionEnabled, default: false
  attribute :webhookEnabled, default: false
  attribute :dotLinkClaimed, default: false
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :planPeriod, { monthly: "monthly", yearly: "yearly", quarterly: "quarterly", biweekly: "biweekly" },
       validate: { allow_nil: true }
  enum :stripeConnectMode, { live: "live", test: "test", sandbox: "sandbox" }, prefix: true,
                                                                               validate: { allow_nil: true }

  validates :name, presence: true
  validates :slug, presence: true, uniqueness: true
  validates :inviteCode, uniqueness: { allow_nil: true }
  validates :stripeId, uniqueness: { allow_nil: true }
  validates :billingCycleStart, presence: true
  validates :invoicePrefix, uniqueness: { allow_nil: true }
  validates :stripeConnectId,
            uniqueness: { scope: :stripeConnectMode, conditions: -> { where.not(stripeConnectMode: nil) }, allow_nil: true }
  validates :shopifyStoreId, uniqueness: { allow_nil: true }
  validates :publishableKey, uniqueness: { allow_nil: true }
  validates :ssoEmailDomain, uniqueness: { allow_nil: true }

  has_many :users, class_name: "ProjectUsers", foreign_key: "projectId", inverse_of: :project, dependent: :destroy
  has_many :invites, class_name: "ProjectInvite", foreign_key: "projectId", inverse_of: :project, dependent: :destroy
  has_many :sent_emails, foreign_key: "projectId", inverse_of: :project, dependent: :destroy
  has_many :links, foreign_key: "projectId", inverse_of: :project, dependent: :destroy
  has_many :domains, foreign_key: "projectId", inverse_of: :project, dependent: :destroy
  has_many :tags, foreign_key: "projectId", inverse_of: :project, dependent: :destroy
  has_many :invoices, foreign_key: "workspaceId", inverse_of: :workspace, dependent: :restrict_with_error
  has_many :customers, foreign_key: "projectId", inverse_of: :project, dependent: :restrict_with_error
  has_many :default_domains, foreign_key: "projectId", inverse_of: :project, dependent: :destroy
  has_many :restricted_tokens, foreign_key: "projectId", inverse_of: :project, dependent: :destroy
  has_many :o_auth_codes, class_name: "OAuthCode", foreign_key: "projectId", inverse_of: :project, dependent: :destroy
  has_many :integrations, foreign_key: "projectId", inverse_of: :project, dependent: :destroy
  has_many :installed_integrations, foreign_key: "projectId", inverse_of: :project, dependent: :destroy
  has_many :webhooks, foreign_key: "projectId", inverse_of: :project, dependent: :destroy
  has_many :folders, foreign_key: "projectId", inverse_of: :project, dependent: :restrict_with_error
  has_many :registered_domains, foreign_key: "projectId", inverse_of: :project, dependent: :restrict_with_error
  has_many :dashboards, foreign_key: "projectId", inverse_of: :project, dependent: :destroy
  has_many :utm_templates, foreign_key: "projectId", inverse_of: :project, dependent: :destroy
  has_many :year_in_reviews, foreign_key: "workspaceId", inverse_of: :workspace, dependent: :destroy
end
