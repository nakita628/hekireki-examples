class Organization < ApplicationRecord
  self.table_name = "Organization"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Nanoid.generate }
  attribute :kind, default: "customer"
  attribute :maxSessionDurationDays, default: 0
  attribute :mfaRequired, default: false
  attribute :lockoutAfterFailedAttempts, default: 0
  attribute :lockoutMinutes, default: 30
  attribute :sessionIdleTimeoutMinutes, default: 1440
  attribute :sessionMaxLifetimeMinutes, default: 0
  attribute :signedDPA, default: false
  attribute :useCustomElasticsearch, default: false
  attribute :useCustomS3, default: false
  attribute :domainJoin, default: "request"
  attribute :joinDomains, default: -> { [] }
  attribute :presenceEnabled, default: true
  attribute :traceSharingEnabled, default: true
  attribute :selfHostedCustomer, default: false
  attribute :connectServicesDisabled, default: -> { [] }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :joinerRole, { admin: "ADMIN", member: "MEMBER", external: "EXTERNAL", developer: "DEVELOPER" }, default: :member,
                                                                                                        validate: true
  enum :primaryIntent, { agent_governance: "AGENT_GOVERNANCE", llm_ops: "LLM_OPS" }, validate: { allow_nil: true }
  enum :currency, { usd: "USD", eur: "EUR" }, default: :eur, validate: true
  enum :pricingModel, { tiered: "TIERED", seat_event: "SEAT_EVENT" }, default: :tiered, validate: true

  validates :name, presence: true
  validates :slug, presence: true, uniqueness: true
  validates :ssoDomain, uniqueness: { allow_nil: true }
  validates :stripeCustomerId, uniqueness: { allow_nil: true }

  has_one :connected_billing, class_name: "ConnectedBillingAccount", foreign_key: "organizationId",
                              inverse_of: :organization, dependent: :restrict_with_error
  has_many :members, class_name: "OrganizationUser", foreign_key: "organizationId", inverse_of: :organization,
                     dependent: :restrict_with_error
  has_many :teams, foreign_key: "organizationId", inverse_of: :organization, dependent: :restrict_with_error
  has_many :webhook_endpoints, foreign_key: "organizationId", inverse_of: :organization, dependent: :destroy
  has_many :organization_invite, foreign_key: "organizationId", inverse_of: :organization,
                                 dependent: :restrict_with_error
  has_many :custom_roles, foreign_key: "organizationId", inverse_of: :organization, dependent: :restrict_with_error
  has_many :notifications, foreign_key: "organizationId", inverse_of: :organization, dependent: :destroy
  has_many :subscriptions, foreign_key: "organizationId", inverse_of: :organization, dependent: :restrict_with_error
  has_many :scim_tokens, foreign_key: "organizationId", inverse_of: :organization, dependent: :destroy
  has_many :scim_request_log, foreign_key: "organizationId", inverse_of: :organization, dependent: :destroy
  has_many :prompt_tags, foreign_key: "organizationId", inverse_of: :organization, dependent: :destroy
  has_many :groups, foreign_key: "organizationId", inverse_of: :organization, dependent: :destroy
  has_many :role_bindings, foreign_key: "organizationId", inverse_of: :organization, dependent: :destroy
  has_many :api_keys, foreign_key: "organizationId", inverse_of: :organization, dependent: :restrict_with_error
  has_many :issued_licenses, foreign_key: "organizationId", inverse_of: :organization, dependent: :restrict_with_error
  has_many :gateway_change_events, foreign_key: "organizationId", inverse_of: :organization, dependent: :destroy
  has_many :routing_policies, foreign_key: "organizationId", inverse_of: :organization, dependent: :destroy
  has_many :ingestion_sources, foreign_key: "organizationId", inverse_of: :organization, dependent: :destroy
  has_many :anomaly_rules, foreign_key: "organizationId", inverse_of: :organization, dependent: :destroy
  has_many :anomaly_alerts, foreign_key: "organizationId", inverse_of: :organization, dependent: :destroy
  has_many :ai_tool_entries, foreign_key: "organizationId", inverse_of: :organization, dependent: :destroy
  has_many :ingestion_templates, foreign_key: "organizationId", inverse_of: :organization, dependent: :destroy
  has_many :github_installations, foreign_key: "organizationId", inverse_of: :organization, dependent: :destroy
  has_many :github_pull_requests, foreign_key: "organizationId", inverse_of: :organization, dependent: :destroy
  has_many :github_branch_pr_checks, class_name: "GithubBranchPullRequestCheck", foreign_key: "organizationId",
                                     inverse_of: :organization, dependent: :destroy
end
