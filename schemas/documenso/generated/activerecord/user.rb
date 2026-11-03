class User < ApplicationRecord
  self.table_name = "User"

  attribute :disabled, default: false
  attribute :twoFactorEnabled, default: false
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :identityProvider, { documenso: "DOCUMENSO", google: "GOOGLE", oidc: "OIDC" }, default: :documenso,
                                                                                      validate: true

  validates :email, presence: true, uniqueness: true

  belongs_to :avatar_image, foreign_key: "avatarImageId", optional: true, inverse_of: :user
  has_many :accounts, foreign_key: "userId", inverse_of: :user, dependent: :destroy
  has_many :sessions, foreign_key: "userId", inverse_of: :user, dependent: :destroy
  has_many :password_reset_tokens, foreign_key: "userId", inverse_of: :user, dependent: :destroy
  has_many :owned_organisations, class_name: "Organisation", foreign_key: "ownerUserId", inverse_of: :owner,
                                 dependent: :destroy
  has_many :organisation_member, class_name: "OrganisationMember", foreign_key: "userId", inverse_of: :user,
                                 dependent: :destroy
  has_many :folders, foreign_key: "userId", inverse_of: :user, dependent: :destroy
  has_many :envelopes, foreign_key: "userId", inverse_of: :user, dependent: :destroy
  has_many :verification_tokens, foreign_key: "userId", inverse_of: :user, dependent: :destroy
  has_many :api_tokens, foreign_key: "userId", inverse_of: :user, dependent: :destroy
  has_many :security_audit_logs, class_name: "UserSecurityAuditLog", foreign_key: "userId", inverse_of: :user,
                                 dependent: :destroy
  has_many :webhooks, foreign_key: "userId", inverse_of: :user, dependent: :destroy
  has_many :site_settings, foreign_key: "lastModifiedByUserId", inverse_of: :last_modified_by_user, dependent: :nullify
  has_many :passkeys, foreign_key: "userId", inverse_of: :user, dependent: :destroy
end
