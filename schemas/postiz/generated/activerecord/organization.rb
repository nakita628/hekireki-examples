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
  has_many :auto_post, class_name: "AutoPost", foreign_key: "organizationId", inverse_of: :organization,
                       dependent: :restrict_with_error
  has_many :comments, foreign_key: "organizationId", inverse_of: :organization, dependent: :restrict_with_error
  has_many :credits, foreign_key: "organizationId", inverse_of: :organization, dependent: :restrict_with_error
  has_many :clippings, foreign_key: "organizationId", inverse_of: :organization, dependent: :restrict_with_error
  has_many :customers, foreign_key: "orgId", inverse_of: :organization, dependent: :restrict_with_error
  has_many :errors, foreign_key: "organizationId", inverse_of: :organization, dependent: :restrict_with_error
  has_many :github, class_name: "GitHub", foreign_key: "organizationId", inverse_of: :organization,
                    dependent: :restrict_with_error
  has_many :integration, class_name: "Integration", foreign_key: "organizationId", inverse_of: :organization,
                         dependent: :restrict_with_error
  has_many :media, foreign_key: "organizationId", inverse_of: :organization, dependent: :restrict_with_error
  has_many :buyer_organization, class_name: "MessagesGroup", foreign_key: "buyerOrganizationId",
                                inverse_of: :buyer_organization, dependent: :restrict_with_error
  has_many :notifications, foreign_key: "organizationId", inverse_of: :organization, dependent: :restrict_with_error
  has_many :plugs, foreign_key: "organizationId", inverse_of: :organization, dependent: :restrict_with_error
  has_many :post, class_name: "Post", foreign_key: "organizationId", inverse_of: :organization,
                  dependent: :restrict_with_error
  has_many :submitted_post, class_name: "Post", foreign_key: "submittedForOrganizationId",
                            inverse_of: :submitted_for_organization, dependent: :nullify
  has_many :sets, foreign_key: "organizationId", inverse_of: :organization, dependent: :restrict_with_error
  has_many :signatures, foreign_key: "organizationId", inverse_of: :organization, dependent: :restrict_with_error
  has_many :tags, foreign_key: "orgId", inverse_of: :organization, dependent: :restrict_with_error
  has_many :third_party, class_name: "ThirdParty", foreign_key: "organizationId", inverse_of: :organization,
                         dependent: :restrict_with_error
  has_many :used_codes, foreign_key: "orgId", inverse_of: :organization, dependent: :restrict_with_error
  has_many :users, class_name: "UserOrganization", foreign_key: "organizationId", inverse_of: :organization,
                   dependent: :restrict_with_error
  has_many :webhooks, foreign_key: "organizationId", inverse_of: :organization, dependent: :restrict_with_error
  has_many :oauth_app, class_name: "OAuthApp", foreign_key: "organizationId", inverse_of: :organization,
                       dependent: :nullify
  has_many :oauth_authorizations, foreign_key: "organizationId", inverse_of: :organization,
                                  dependent: :restrict_with_error
end
