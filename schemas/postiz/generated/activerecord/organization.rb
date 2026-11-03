class Organization < ApplicationRecord
  self.table_name = "Organization"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { SecureRandom.uuid }
  attribute :allowTrial, default: false
  attribute :isTrailing, default: false
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :shortlink, { ask: "ASK", yes: "YES", no: "NO" }, default: :ask, validate: true

  validates :name, presence: true

  has_one :subscription, foreign_key: "organizationId", inverse_of: :organization, dependent: :restrict_with_error
  has_many :auto_post, foreign_key: "organizationId", inverse_of: :organization, dependent: :restrict_with_error
  has_many :comments, class_name: "Comments", foreign_key: "organizationId", inverse_of: :organization,
                      dependent: :restrict_with_error
  has_many :credits, class_name: "Credits", foreign_key: "organizationId", inverse_of: :organization,
                     dependent: :restrict_with_error
  has_many :clippings, foreign_key: "organizationId", inverse_of: :organization, dependent: :restrict_with_error
  has_many :customers, foreign_key: "orgId", inverse_of: :organization, dependent: :restrict_with_error
  has_many :organization_errors, class_name: "Errors", foreign_key: "organizationId", inverse_of: :organization,
                                 dependent: :restrict_with_error
  has_many :github, class_name: "GitHub", foreign_key: "organizationId", inverse_of: :organization,
                    dependent: :restrict_with_error
  has_many :integration, foreign_key: "organizationId", inverse_of: :organization, dependent: :restrict_with_error
  has_many :media, class_name: "Media", foreign_key: "organizationId", inverse_of: :organization,
                   dependent: :restrict_with_error
  has_many :buyer_organization, class_name: "MessagesGroup", foreign_key: "buyerOrganizationId",
                                inverse_of: :buyer_organization, dependent: :restrict_with_error
  has_many :notifications, class_name: "Notifications", foreign_key: "organizationId", inverse_of: :organization,
                           dependent: :restrict_with_error
  has_many :plugs, class_name: "Plugs", foreign_key: "organizationId", inverse_of: :organization,
                   dependent: :restrict_with_error
  has_many :post, foreign_key: "organizationId", inverse_of: :organization, dependent: :restrict_with_error
  has_many :submitted_post, class_name: "Post", foreign_key: "submittedForOrganizationId",
                            inverse_of: :submitted_for_organization, dependent: :nullify
  has_many :sets, class_name: "Sets", foreign_key: "organizationId", inverse_of: :organization,
                  dependent: :restrict_with_error
  has_many :signatures, class_name: "Signatures", foreign_key: "organizationId", inverse_of: :organization,
                        dependent: :restrict_with_error
  has_many :tags, class_name: "Tags", foreign_key: "orgId", inverse_of: :organization, dependent: :restrict_with_error
  has_many :third_party, foreign_key: "organizationId", inverse_of: :organization, dependent: :restrict_with_error
  has_many :used_codes, class_name: "UsedCodes", foreign_key: "orgId", inverse_of: :organization,
                        dependent: :restrict_with_error
  has_many :users, class_name: "UserOrganization", foreign_key: "organizationId", inverse_of: :organization,
                   dependent: :restrict_with_error
  has_many :webhooks, class_name: "Webhooks", foreign_key: "organizationId", inverse_of: :organization,
                      dependent: :restrict_with_error
  has_many :oauth_app, class_name: "OAuthApp", foreign_key: "organizationId", inverse_of: :organization,
                       dependent: :nullify
  has_many :oauth_authorizations, class_name: "OAuthAuthorization", foreign_key: "organizationId",
                                  inverse_of: :organization, dependent: :restrict_with_error
end
