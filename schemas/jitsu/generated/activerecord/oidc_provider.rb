class OidcProvider < ApplicationRecord
  self.table_name = "OidcProvider"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :scopes, default: -> { %w[openid profile email offline_access] }
  attribute :usernameClaim, default: "email"
  attribute :emailClaim, default: "email"
  attribute :nameClaim, default: "name"
  attribute :groupClaim, default: "groups"
  attribute :enabled, default: true
  attribute :autoDiscovery, default: true
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :name, presence: true
  validates :issuer, presence: true, uniqueness: { scope: :clientId }
  validates :clientId, presence: true
  validates :clientSecret, presence: true

  has_many :oidc_login_groups, foreign_key: "oidcProviderId", inverse_of: :oidc_provider,
                               dependent: :restrict_with_error
end
