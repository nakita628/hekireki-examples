class OAuthAuthorization < ApplicationRecord
  self.table_name = "OAuthAuthorization"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { SecureRandom.uuid }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :oauthAppId, uniqueness: { scope: [ :userId, :organizationId ] }

  belongs_to :oauth_app, class_name: "OAuthApp", foreign_key: "oauthAppId", inverse_of: :authorizations
  belongs_to :user, foreign_key: "userId", inverse_of: :oauth_authorizations
  belongs_to :organization, foreign_key: "organizationId", inverse_of: :oauth_authorizations
end
