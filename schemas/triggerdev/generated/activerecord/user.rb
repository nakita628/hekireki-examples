class User < ApplicationRecord
  self.table_name = "User"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :admin, default: false
  attribute :isOnCloudWaitlist, default: false
  attribute :featureCloud, default: false
  attribute :isOnHostedRepoWaitlist, default: false
  attribute :marketingEmails, default: true
  attribute :confirmedBasicDetails, default: false
  attribute :sessionDuration, default: 31_556_952
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :authenticationMethod, { github: "GITHUB", magic_link: "MAGIC_LINK", google: "GOOGLE", sso: "SSO" },
       validate: true

  validates :email, presence: true, uniqueness: true
  validates :authIdentifier, uniqueness: { allow_nil: true }

  belongs_to :mfa_secret_reference, class_name: "SecretReference", foreign_key: "mfaSecretReferenceId", optional: true,
                                    inverse_of: :user
  belongs_to :invitation_code, foreign_key: "invitationCodeId", optional: true, inverse_of: :users
  has_many :org_memberships, class_name: "OrgMember", foreign_key: "userId", inverse_of: :user, dependent: :destroy
  has_many :sent_invites, class_name: "OrgMemberInvite", foreign_key: "inviterId", inverse_of: :inviter,
                          dependent: :destroy
  has_many :personal_access_tokens, foreign_key: "userId", inverse_of: :user, dependent: :restrict_with_error
  has_many :created_api_keys, class_name: "ApiKey", foreign_key: "created_by_user_id", inverse_of: :created_by,
                              dependent: :nullify
  has_many :deployments, class_name: "WorkerDeployment", foreign_key: "triggeredById", inverse_of: :triggered_by,
                         dependent: :nullify
  has_many :backup_codes, class_name: "MfaBackupCode", foreign_key: "userId", inverse_of: :user,
                          dependent: :restrict_with_error
  has_many :bulk_actions, class_name: "BulkActionGroup", foreign_key: "userId", inverse_of: :user, dependent: :nullify
  has_many :impersonations_performed, class_name: "ImpersonationAuditLog", foreign_key: "adminId", inverse_of: :admin,
                                      dependent: :destroy
  has_many :impersonations_received, class_name: "ImpersonationAuditLog", foreign_key: "targetId", inverse_of: :target,
                                     dependent: :destroy
  has_many :support_access_requests_made, class_name: "SupportAccessRequest", foreign_key: "requestedById",
                                          inverse_of: :requested_by, dependent: :nullify
  has_many :support_access_requests_approved, class_name: "SupportAccessRequest", foreign_key: "approvedById",
                                              inverse_of: :approved_by, dependent: :nullify
  has_many :customer_queries, foreign_key: "userId", inverse_of: :user, dependent: :nullify
  has_many :metrics_dashboards, foreign_key: "ownerId", inverse_of: :owner, dependent: :nullify
  has_many :platform_notifications, foreign_key: "userId", inverse_of: :user, dependent: :destroy
  has_many :platform_notification_interactions, foreign_key: "userId", inverse_of: :user, dependent: :destroy
end
