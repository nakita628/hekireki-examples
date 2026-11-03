class OAuthApp < ApplicationRecord
  self.table_name = "OAuthApp"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { SecureRandom.uuid }
  attribute :dynamic, default: false
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :name, presence: true
  validates :redirectUrl, presence: true
  validates :clientId, presence: true, uniqueness: true
  validates :deletedAt,
            uniqueness: { scope: :organizationId, conditions: -> { where.not(organizationId: nil) }, allow_nil: true }

  belongs_to :organization, foreign_key: "organizationId", optional: true, inverse_of: :oauth_app
  belongs_to :picture, class_name: "Media", foreign_key: "pictureId", optional: true, inverse_of: :oauth_apps
  has_many :authorizations, class_name: "OAuthAuthorization", foreign_key: "oauthAppId", inverse_of: :oauth_app,
                            dependent: :restrict_with_error
  has_many :self_hosted_authorizations, class_name: "OAuthSelfHostedAuthorization", foreign_key: "oauthAppId",
                                        inverse_of: :oauth_app, dependent: :restrict_with_error
end
