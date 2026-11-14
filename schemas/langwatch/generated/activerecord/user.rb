class User < ApplicationRecord
  self.table_name = "User"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Nanoid.generate }
  attribute :emailVerified, default: false
  attribute :signupConfirmationPending, default: false
  attribute :pendingSsoSetup, default: false
  attribute :twoFactorEnabled, default: false
  attribute :joinOfferDismissedDomains, default: -> { [] }
  attribute :notificationPreferences, default: -> { JSON.parse("{}") }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :email, uniqueness: { allow_nil: true }

  has_many :accounts, foreign_key: "userId", inverse_of: :user, dependent: :destroy
  has_many :account_credentials, foreign_key: "userId", inverse_of: :user, dependent: :destroy
  has_many :sessions, foreign_key: "userId", inverse_of: :user, dependent: :destroy
  has_many :two_factors, foreign_key: "userId", inverse_of: :user, dependent: :destroy
  has_many :passkeys, foreign_key: "userId", inverse_of: :user, dependent: :destroy
  has_many :team_memberships, class_name: "TeamUser", foreign_key: "userId", inverse_of: :user,
                              dependent: :restrict_with_error
  has_many :org_memberships, class_name: "OrganizationUser", foreign_key: "userId", inverse_of: :user,
                             dependent: :restrict_with_error
  has_many :scim_user_resources, foreign_key: "userId", inverse_of: :user, dependent: :destroy
  has_many :annotation, foreign_key: "userId", inverse_of: :user, dependent: :nullify
  has_many :share_links, foreign_key: "userId", inverse_of: :user, dependent: :nullify
  has_many :workflow, foreign_key: "publishedById", inverse_of: :published_by, dependent: :nullify
  has_many :workflow_version, foreign_key: "authorId", inverse_of: :author, dependent: :restrict_with_error
  has_many :annotation_queues, class_name: "AnnotationQueueMembers", foreign_key: "userId", inverse_of: :user,
                               dependent: :restrict_with_error
  has_many :assigned_queue_items, class_name: "AnnotationQueueItem", foreign_key: "userId", inverse_of: :user,
                                  dependent: :nullify
  has_many :created_queue_items, class_name: "AnnotationQueueItem", foreign_key: "createdByUserId",
                                 inverse_of: :created_by_user, dependent: :nullify
  has_many :llm_prompt_config_versions, foreign_key: "authorId", inverse_of: :author, dependent: :nullify
  has_many :scenarios, foreign_key: "lastUpdatedById", inverse_of: :last_updated_by, dependent: :nullify
  has_many :invite_requests, class_name: "OrganizationInvite", foreign_key: "requestedBy",
                             inverse_of: :requested_by_user, dependent: :nullify
  has_many :secrets_created, class_name: "ProjectSecret", foreign_key: "createdById", inverse_of: :created_by,
                             dependent: :restrict_with_error
  has_many :secrets_updated, class_name: "ProjectSecret", foreign_key: "updatedById", inverse_of: :updated_by,
                             dependent: :restrict_with_error
  has_many :saved_views, foreign_key: "userId", inverse_of: :user, dependent: :nullify
  has_many :version_tags_created, class_name: "PromptTagAssignment", foreign_key: "createdById", inverse_of: :created_by,
                                  dependent: :nullify
  has_many :version_tags_updated, class_name: "PromptTagAssignment", foreign_key: "updatedById", inverse_of: :updated_by,
                                  dependent: :nullify
  has_many :prompt_tags_created, class_name: "PromptTag", foreign_key: "createdById", inverse_of: :created_by,
                                 dependent: :nullify
  has_many :prompt_tags_updated, class_name: "PromptTag", foreign_key: "updatedById", inverse_of: :updated_by,
                                 dependent: :nullify
  has_many :group_memberships, foreign_key: "userId", inverse_of: :user, dependent: :destroy
  has_many :role_bindings, foreign_key: "userId", inverse_of: :user, dependent: :destroy
  has_many :scim_external_ids, foreign_key: "userId", inverse_of: :user, dependent: :destroy
  has_many :api_keys, foreign_key: "userId", inverse_of: :user, dependent: :restrict_with_error
  has_many :gateway_budgets_created, class_name: "GatewayBudget", foreign_key: "createdById", inverse_of: :created_by,
                                     dependent: :restrict_with_error
  has_many :principal_virtual_keys, class_name: "VirtualKey", foreign_key: "principalUserId",
                                    inverse_of: :principal_user, dependent: :nullify
  has_many :personal_teams, class_name: "Team", foreign_key: "ownerUserId", inverse_of: :owner_user, dependent: :nullify
  has_many :personal_projects, class_name: "Project", foreign_key: "ownerUserId", inverse_of: :owner_user,
                               dependent: :nullify
  has_many :routing_policies_created, class_name: "RoutingPolicy", foreign_key: "createdById", inverse_of: :created_by,
                                      dependent: :nullify
  has_many :routing_policies_updated, class_name: "RoutingPolicy", foreign_key: "updatedById", inverse_of: :updated_by,
                                      dependent: :nullify
  has_many :gateway_guardrails_created, class_name: "GatewayGuardrail", foreign_key: "createdById",
                                        inverse_of: :created_by, dependent: :nullify
  has_many :gateway_guardrails_updated, class_name: "GatewayGuardrail", foreign_key: "updatedById",
                                        inverse_of: :updated_by, dependent: :nullify
  has_many :ingestion_sources_created, class_name: "IngestionSource", foreign_key: "createdById",
                                       inverse_of: :created_by, dependent: :nullify
  has_many :anomaly_rules_created, class_name: "AnomalyRule", foreign_key: "createdById", inverse_of: :created_by,
                                   dependent: :nullify
  has_many :pinned_traces, foreign_key: "userId", inverse_of: :user, dependent: :nullify
  has_many :trace_edit_overlays_created, class_name: "TraceEditOverlay", foreign_key: "createdById",
                                         inverse_of: :created_by, dependent: :nullify
  has_many :trace_edit_overlays_updated, class_name: "TraceEditOverlay", foreign_key: "updatedById",
                                         inverse_of: :updated_by, dependent: :nullify
end
